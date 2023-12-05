namespace B2bApi.Shared.Dtos;

public record PagedRequest
{
	public int Page { get; init; } = 1;
	public int PageSize { get; init; } = 10;
}