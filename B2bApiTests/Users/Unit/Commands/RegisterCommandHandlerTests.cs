using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Commands.Register;
using B2bApi.Users.Entities;
using B2bApi.Users.Events.UserCreate;
using B2bApi.Users.Exceptions;
using B2bApi.Users.Repositories;
using B2bApi.Users.ValueObjects;
using MediatR;
using NSubstitute;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Unit.Commands;

public class RegisterCommandHandlerTests
{
	private readonly RegisterCommandHandler _commandHandler;
	private readonly IUserRepository _userRepositoryMock;
	private readonly IPasswordManager _passwordManagerMock;
	private readonly IPublisher _publisherMock;
	
	public RegisterCommandHandlerTests()
	{
		_userRepositoryMock = Substitute.For<IUserRepository>();
		_passwordManagerMock = Substitute.For<IPasswordManager>();
		_publisherMock = Substitute.For<IPublisher>();
		_commandHandler = new RegisterCommandHandler(_userRepositoryMock, _passwordManagerMock, _publisherMock);
	}

	[Fact]
	public void Handle_ShouldFail_WhenEmailAlreadyExists()
	{
		// Arrange
		var newUser = CreateTestUser("test@mail.com", "test", "Test1234%$#");
		var existingUser = User.Create(UserId.New(), newUser.Email, newUser.Username, newUser.Password, 
										Role.User, Currency.Pln.ToString());
		var command = new RegisterCommand(newUser.Email, newUser.Username, newUser.Password, "PLN");
		_userRepositoryMock.GetByEmailAsync(newUser.Email).Returns(existingUser);

		// Act & Assert
		Should.Throw<UserAlreadyExistsException>(async () => await _commandHandler.Handle(command, default));
	}

	[Fact]
	public async Task Handle_ShouldCreateUser_WhenEmailIsUnique()
	{
		// Arrange
		var newUser = CreateTestUser("test@mail.com", "test", "Test1234%$#");
		var command = new RegisterCommand(newUser.Email, newUser.Username, newUser.Password, "PLN");
		_userRepositoryMock.GetByEmailAsync(newUser.Email)!.Returns(null as User);
		_passwordManagerMock.Secure(newUser.Password).Returns(newUser.Password.ToString());
		
		// Act
		await _commandHandler.Handle(command, default);
		
		// Assert
		await _userRepositoryMock.Received(1).AddAsync(Arg.Any<User>(), default);
		await _publisherMock.Received(1).Publish(Arg.Any<UserCreateEvent>());
	}
	
	private static User CreateTestUser(string email, string username, string password)
	{
		return User.Create(UserId.New(), email, username, password, Role.User, Currency.Pln.ToString());
	}
}