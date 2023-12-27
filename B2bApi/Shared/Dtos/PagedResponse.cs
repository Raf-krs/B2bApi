namespace B2bApi.Shared.Dtos;

public class PagedResponse<T>
{
	public IEnumerable<T> Items { get; init; }
	public int TotalCount { get; init; }
	public bool HasNextPage { get; init; }
}