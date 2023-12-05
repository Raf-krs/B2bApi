namespace B2bApi.Shared.Exceptions;

public class CustomException : Exception
{
	public CustomException(string message) : base(message) { }
}