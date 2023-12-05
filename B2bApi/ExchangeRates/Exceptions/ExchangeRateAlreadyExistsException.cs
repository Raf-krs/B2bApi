using B2bApi.Shared.Exceptions;

namespace B2bApi.ExchangeRates.Exceptions;

public class ExchangeRateAlreadyExistsException : CustomException
{
	public ExchangeRateAlreadyExistsException(string no) 
		: base($"Exchange rate with number: {no} already exists.") { }
}