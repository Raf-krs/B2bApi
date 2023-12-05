using System.Net;
using System.Net.Http.Json;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.Commands.Password;
using B2bApi.Users.Commands.Register;
using B2bApi.Users.Entities;
using B2bApi.Users.Repositories;
using B2bApi.Users.ValueObjects;
using B2bApiTests.Utils.Integration;
using Shouldly;
using Xunit;

namespace B2bApiTests.Users.Integration;

public class UsersControllerTests : TestApi
{
	[Fact]
	public async Task Register_ShouldPass_WhenUserIsValid()
	{
		// Arrange
		var command = new RegisterCommand("test@mail.com", "Test", "Test123$", "PLN");
		
		// Act
		var response = await Client.PostAsJsonAsync("/api/users/register", command);

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.Created);
		var userRepository = new UserRepository(TestDbContext, ConnectionFactory);
		var user = await userRepository.GetByEmailAsync(command.Email, default);
		command.Email.ShouldBe(user.Email);
	}
	
	[Fact]
	public async Task ChangePassword_ShouldPass_WhenPasswordAndTokenIsValid()
	{
		// Arrange
		var user = User.Create(UserId.New(), "test@mail.com", "Test", "Test123$", 
								Role.User, Currency.Pln.ToString());
		await AddEntityAsync(user);
		var command = new ChangePasswordCommand(user.Email, "Test1234$");
		Authorize(user);
		
		// Act
		var response = await Client.PatchAsJsonAsync("/api/users/me/password", command);

		// Assert
		response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
	}
}