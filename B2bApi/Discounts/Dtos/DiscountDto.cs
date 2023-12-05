using B2bApi.Discounts.Entities;

namespace B2bApi.Discounts.Dtos;

public record DiscountDto(Guid Id, Guid ProductId, decimal Price, DateOnly StartDate, DateOnly EndDate)
{
	public Discount ToEntity() => Discount.Create(Id, ProductId, Price, StartDate, EndDate);
}