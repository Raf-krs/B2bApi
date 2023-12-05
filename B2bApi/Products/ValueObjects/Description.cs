using B2bApi.Shared.Exceptions;
using FluentResults;

namespace B2bApi.Products.ValueObjects;

public sealed record Description
{
	public string Value { get; }
	
	public Description(string value)
	{
		var result = IsValid(value);
		if(result.IsFailed)
		{
			throw new InvalidInputException(string.Join(";" + Environment.NewLine, result.Errors));
		}
		Value = value;
	}

	public static implicit operator string(Description description) => description.Value;
	public static implicit operator Description(string description) => new(description);

	private Result IsValid(string value) 
		=> string.IsNullOrWhiteSpace(value) ? Result.Fail("Description cannot be empty.") : Result.Ok();
}