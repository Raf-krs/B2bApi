using B2bApi.Users.Entities;

namespace B2bApi.Users.Dtos;

public record UserDto(Guid Id, string Email, string Username, string Password, string Role, string Currency)
{
	public User ToEntity() => User.Create(Id, Email, Username, Password, Role, Currency);
}