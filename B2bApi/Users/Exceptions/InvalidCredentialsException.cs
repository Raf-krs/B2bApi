using B2bApi.Shared.Exceptions;

namespace B2bApi.Users.Exceptions;

public sealed class InvalidCredentialsException : CustomException
{
	public InvalidCredentialsException(string email) : base($"Invalid credentials for email: {email}") { }
}