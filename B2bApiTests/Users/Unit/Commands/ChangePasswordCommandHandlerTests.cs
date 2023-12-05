using B2bApi.Shared.Exceptions;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Commands.Password;
using B2bApi.Users.Entities;
using B2bApi.Users.Repositories;
using B2bApi.Users.ValueObjects;
using NSubstitute;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.Commands;

public class ChangePasswordCommandHandlerTests
{
	private readonly ChangePasswordCommandHandler _commandHandler;
	private readonly IUserRepository _userRepositoryMock;
	private readonly IPasswordManager _passwordManagerMock;

	public ChangePasswordCommandHandlerTests()
	{
		_userRepositoryMock = Substitute.For<IUserRepository>();
		_passwordManagerMock = Substitute.For<IPasswordManager>();
		_commandHandler = new ChangePasswordCommandHandler(_userRepositoryMock, _passwordManagerMock);
	}
	
	[Fact]
	public void Handle_ShouldFail_WhenPasswordIsInvalid()
	{
		// Arrange
		var newUser = CreateTestUser();
		var existingUser = User.Create(UserId.New(), newUser.Email, newUser.Username, newUser.Password, 
										Role.User, Currency.Pln.ToString());
		var command = new ChangePasswordCommand(newUser.Email, newUser.Password);
		_userRepositoryMock.GetByEmailAsync(newUser.Email).Returns(existingUser);

		// Act & Assert
		Should.Throw<InvalidInputException>(async () => await _commandHandler.Handle(command, default));
	}

	[Fact]
	public async Task Handle_ShouldPass_WhenPasswordIsValid()
	{
		// Arrange
		var newUser = CreateTestUser();
		var user = User.Create(UserId.New(), newUser.Email, newUser.Username, newUser.Password, 
								Role.User, Currency.Pln.ToString());
		const string newPassword = "Test1234%$#12";
		var command = new ChangePasswordCommand(newUser.Email, newPassword);
		
		_userRepositoryMock.GetByEmailAsync(newUser.Email).Returns(user);
		_passwordManagerMock.Secure(newPassword).Returns(newPassword);
		
		// Act
		await _commandHandler.Handle(command, default);
		
		// Assert
		await _userRepositoryMock.Received().UpdateAsync(Arg.Any<User>());
	}
	
	private static User CreateTestUser()
	{
		return User.Create(UserId.New(), "test@mail.com", "test", "Test1234%$#", 
							Role.User, Currency.Pln.ToString());
	}
}