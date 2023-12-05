using B2bApi.Shared.Exceptions;

namespace B2bApi.Shared.ValueObjects;

public record Money(decimal Amount, Currency Currency)
{
	public static Money operator +(Money left, Money right)
	{
		if(left.Currency != right.Currency)
		{
			throw new DifferentCurrencyException();
		}
		return left with { Amount = left.Amount + right.Amount };
	}
}