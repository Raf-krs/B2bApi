using B2bApi.Products.Entities;
using B2bApi.Shared.Abstractions.Data;

namespace B2bApi.Availabilities.Entities;

public sealed class Availabilities : IEntity
{
	public Guid Id { get; private set; }
	public Product Product { get; private set; }
	public int Quantity { get; private set; }
	
	private Availabilities() { }
	
	private Availabilities(Product product, int quantity)
	{
		Id = Guid.NewGuid();
		Product = product;
		Quantity = quantity;
	}
	
	public static Availabilities Create(Product product, int quantity) => new(product, quantity);
}