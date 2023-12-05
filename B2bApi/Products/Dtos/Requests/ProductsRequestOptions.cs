using B2bApi.Shared.Db.Data;
using B2bApi.Shared.ValueObjects;

namespace B2bApi.Products.Dtos.Requests;

public class ProductsRequestOptions
{
	public Page Page { get; private set; }
	public PageSize PageSize { get; private set; }
	public string Name { get; private set; }
	public Currency Currency { get; private set; }
	public SortField SortField { get; private set; }
	public SortDirection SortDirection { get; private set; }
	
	public ProductsRequestOptions(int page, int pageSize, string name, string currency, string orderBy)
	{
		Page = page;
		PageSize = pageSize;
		Name = name;
		Currency = Currency.FromCode(currency);
		var order = new SortType(orderBy);
		SortField = new SortField(order.Field);
		SortDirection = order.SortDirection;
	}
}