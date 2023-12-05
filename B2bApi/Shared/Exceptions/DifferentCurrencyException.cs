namespace B2bApi.Shared.Exceptions;

public class DifferentCurrencyException : CustomException
{
	public DifferentCurrencyException() : base("Cannot add money of different currencies") { }
}