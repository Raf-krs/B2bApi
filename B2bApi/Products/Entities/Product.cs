using B2bApi.Products.ValueObjects;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.ValueObjects;

namespace B2bApi.Products.Entities;

public sealed class Product : IEntity
{
	public Guid Id { get; private set; }
	public Name Name { get; private set; }
	public Description Description { get; private set; }
	public Money Price { get; private set; }
	
	private Product() {}

	private Product(Guid id, Name name, Description description, Money price)
	{
		Id = id;
		Name = name;
		Description = description;
		Price = price;
	}
	
	public static Product Create(Guid id, string name, string description, decimal price)
		=> new(id, name, description, new Money(price, Currency.Pln));
}