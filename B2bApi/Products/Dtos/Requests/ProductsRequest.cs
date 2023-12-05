using B2bApi.Shared.Dtos;

namespace B2bApi.Products.Dtos.Requests;

public record ProductsRequest : PagedRequest
{
	public string Name { get; init; }
	public string OrderBy { get; init; }
}