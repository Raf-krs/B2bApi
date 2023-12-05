using B2bApi.Shared.Exceptions;
using FluentResults;

namespace B2bApi.Carts.ValueObjects;

public record Quantity
{
	public int Value { get; }
	
	public Quantity(int value)
	{
		var result = IsValid(value);
		if(result.IsFailed)
		{
			throw new InvalidInputException(string.Join(";" + Environment.NewLine, result.Errors));
		}	
		Value = value;
	}

	public static implicit operator int(Quantity name) => name.Value;
	public static implicit operator Quantity(int name) => new(name);
	
	private Result IsValid(int value) 
		=> value <= 0 ? Result.Fail("Quantity must be greater than zero.") : Result.Ok();
}