using B2bApi.Shared.Exceptions;

namespace B2bApi.Users.Exceptions;

public class UserAlreadyExistsException : CustomException
{
	public UserAlreadyExistsException(string email) : base($"User with email: {email} already exists.") { }
}