using B2bApi.Shared.Exceptions;
using FluentResults;

namespace B2bApi.Users.ValueObjects;

public sealed record Username
{
	public string Value { get; }
	
	public Username(string value)
	{
		var result = IsValid(value);
		if(result.IsFailed)
		{
			throw new InvalidInputException(string.Join(";" + Environment.NewLine, result.Errors));
		}	
		Value = value;
	}

	public static implicit operator string(Username username) => username.Value;
	public static implicit operator Username(string username) => new(username);
	
	private Result IsValid(string value) 
		=> string.IsNullOrWhiteSpace(value) ? Result.Fail("Username cannot be empty.") : Result.Ok();
}