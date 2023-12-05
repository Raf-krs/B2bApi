using B2bApi.Shared.Exceptions;
using FluentResults;

namespace B2bApi.Shared.ValueObjects;

public record PageSize
{
	public int Value { get; }
	private readonly int[] _allowedPageSizes = { 5, 10, 15, 20, 25 };

	public PageSize(int page)
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
		if(_allowedPageSizes.Contains(page) is false)
		{
			return Result.Fail("Page size is not allowed");
		}
		return Result.Ok();
	}
	
	public static implicit operator int(PageSize pageSize) => pageSize.Value;
	public static implicit operator PageSize(int pageSize) => new(pageSize);
	public override string ToString() => Value.ToString();
}