using B2bApi.Shared.Dtos;

namespace B2bApi.Discounts.Dtos.Requests;

public record GetDiscountsRequest : PagedRequest
{
	public required DateOnly StartDate { get; set; }
	public required DateOnly EndDate { get; set; }
}