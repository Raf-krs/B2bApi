using B2bApi.Shared.Exceptions;
using FluentResults;

namespace B2bApi.Shared.ValueObjects;

public sealed record Name
{
	public string Value { get; }
	
	public Name(string value)
	{
		var result = IsValid(value);
		if(result.IsFailed)
		{
			throw new InvalidInputException(string.Join(";" + Environment.NewLine, result.Errors));
		}	
		Value = value;
	}

	public static implicit operator string(Name name) => name.Value;
	public static implicit operator Name(string name) => new(name);
	
	private Result IsValid(string value) 
		=> string.IsNullOrWhiteSpace(value) ? Result.Fail("Name cannot be empty.") : Result.Ok();
}