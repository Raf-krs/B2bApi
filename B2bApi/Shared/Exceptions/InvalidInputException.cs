namespace B2bApi.Shared.Exceptions;

public class InvalidInputException : CustomException
{
	public InvalidInputException(string error) : base(error) { }
}