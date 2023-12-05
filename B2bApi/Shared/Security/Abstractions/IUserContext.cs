namespace B2bApi.Shared.Security.Abstractions;

public interface IUserContext
{
	string Identity { get; }
	string Email { get; }
	string Role { get; }
	string Currency { get; }
}