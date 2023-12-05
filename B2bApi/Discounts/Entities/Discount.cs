using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.ValueObjects;

namespace B2bApi.Discounts.Entities;

public sealed class Discount : IEntity
{
	public Guid Id { get; private set; }
	public Guid ProductId { get; private set; }
	public Money Price { get; private set; }
	public DateOnly StartDate { get; private set; }
	public DateOnly EndDate { get; private set; }
	
	private Discount() { }
	
	private Discount(Guid id, Guid productId, Money price, DateOnly startDate, DateOnly endDate)
	{
		Id = id;
		ProductId = productId;
		Price = price;
		StartDate = startDate;
		EndDate = endDate;
	}
	
	public static Discount Create(Guid id, Guid productId, decimal price, DateOnly startDate, DateOnly endDate) 
		=> new(id, productId, new Money(price, Currency.Pln), startDate, endDate);
}