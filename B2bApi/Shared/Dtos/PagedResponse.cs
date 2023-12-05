namespace B2bApi.Shared.Dtos;

public class PagedResponse<T>
{
	public IEnumerable<T> Items { get; set; }
	public int TotalCount { get; set; }
	public bool HasNextPage { get; set; }
}