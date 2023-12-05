using B2bApi.Shared.Exceptions;
using FluentResults;

namespace B2bApi.Shared.ValueObjects;

public record Page
{
	public int Value { get; }

	public Page(int page)
	{
		var result = IsValid(page);
		if(result.IsFailed)
		{
			throw new InvalidInputException(result.Errors.First().Message);
		}
		Value = page;
	}
	
	private Result IsValid(int page)
	{
		if(page < 1)
		{
			return Result.Fail("Page must be greater than 0");
		}
		return Result.Ok();
	}
	
	public static implicit operator int(Page page) => page.Value;
	public static implicit operator Page(int page) => new(page);
}