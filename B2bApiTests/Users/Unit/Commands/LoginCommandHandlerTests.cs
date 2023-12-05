using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.Security.Dtos;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Commands.Login;
using B2bApi.Users.Entities;
using B2bApi.Users.Exceptions;
using B2bApi.Users.Repositories;
using B2bApi.Users.ValueObjects;
using NSubstitute;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.Commands;

public class LoginCommandHandlerTests
{
	private readonly LoginCommandHandler _commandHandler;
	private readonly IUserRepository _userRepositoryMock;
	private readonly IPasswordManager _passwordManagerMock;
	private readonly IAuthenticator _authenticatorMock;
	
	public LoginCommandHandlerTests()
	{
		_userRepositoryMock = Substitute.For<IUserRepository>();
		_passwordManagerMock = Substitute.For<IPasswordManager>();
		_authenticatorMock = Substitute.For<IAuthenticator>();
		_commandHandler = new LoginCommandHandler(_userRepositoryMock, _passwordManagerMock, _authenticatorMock);
	}
	
	[Fact]
	public void Handle_ShouldFail_WhenEmailAlreadyExists()
	{
		// Arrange
		var newUser = CreateTestUser();
		var existingUser = User.Create(UserId.New(), newUser.Email, newUser.Username, newUser.Password, 
										Role.User, Currency.Pln.ToString());
		var command = new LoginCommand(newUser.Email, newUser.Password);
		_userRepositoryMock.GetByEmailAsync(newUser.Email).Returns(existingUser);

		// Act & Assert
		Should.Throw<InvalidCredentialsException>(async () => await _commandHandler.Handle(command, default));
	}
	
	[Fact]
	public void Handle_ShouldFail_WhenPasswordIsNotTheSame()
	{
		// Arrange
		var newUser = CreateTestUser();
		var command = new LoginCommand(newUser.Email, "Test1234%$#12");
		_userRepositoryMock.GetByEmailAsync(newUser.Email).Returns(newUser);
		_passwordManagerMock.Verify(command.Password, newUser.Password).Returns(false);
		
		// Act & Assert
		Should.Throw<InvalidCredentialsException>(async () => await _commandHandler.Handle(command, default));
	}
	
	[Fact]
	public async Task Handle_Should_ReturnToken()
	{
		// Arrange
		var newUser = CreateTestUser();
		var command = new LoginCommand(newUser.Email, newUser.Password);
		var existingUser = User.Create(UserId.New(), newUser.Email, newUser.Username, newUser.Password, 
										Role.User, Currency.Pln.ToString());
		var token = new JwtDto("token");
		
		_userRepositoryMock.GetByEmailAsync(newUser.Email).Returns(existingUser);
		_passwordManagerMock.Verify(command.Password, Arg.Any<string>()).Returns(true);
		_authenticatorMock.CreateToken(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
			.Returns(token);
		
		// Act
		var result = await _commandHandler.Handle(command, default);
		
		// Assert
		result.ShouldBe(token);
	}
	
	private static User CreateTestUser()
	{
		return User.Create(UserId.New(), "test@mail.com", "test", "Test1234%$#", 
							Role.User, Currency.Pln.ToString());
	}
}