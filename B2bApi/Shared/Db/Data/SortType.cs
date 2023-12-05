namespace B2bApi.Shared.Db.Data;

public class SortType
{
	public string Field { get; private set; }
	public SortDirection SortDirection { get; private set; }

	public SortType(string request)
	{
		Field = string.Empty;
		SortDirection = SortDirection.Unsorted;
		if(IsProcessable(request))
		{
			Field = request.Trim('-');
			SortDirection = GetSortOrder(request);
		}
	}

	private static bool IsProcessable(string request) => !string.IsNullOrEmpty(request) && request.Length > 1;

	private static SortDirection GetSortOrder(string request)
	{
		return request[0] switch
		{
			'-' => SortDirection.Descending,
			_ => SortDirection.Ascending
		};
	}
}

public enum SortDirection
{
	Unsorted,
	Ascending,
	Descending
}