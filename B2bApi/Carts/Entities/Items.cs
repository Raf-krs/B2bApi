using B2bApi.Carts.Commands.Item.UpdateItem;
using B2bApi.Carts.ValueObjects;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.ValueObjects;

namespace B2bApi.Carts.Entities;

public sealed class Items : IEntity
{
	public Guid Id { get; private set; }
	public Guid CartId { get; private set; }
	public Guid ProductId { get; private set; }
	public Quantity Quantity { get; private set; }
	public Money Price { get; private set; }
	
	private Items() { }
	
	private Items(Guid id, Guid cartId, Guid productId, Quantity quantity, Money price)
	{
		Id = id;
		CartId = cartId;
		ProductId = productId;
		Quantity = quantity;
		Price = price;
	}
	
	public static Items Create(Guid id, Guid cartId, Guid productId, int quantity, decimal price)
		=> new(id, cartId, productId, quantity, new Money(price, Currency.None));

	public static Items Update(Items item, UpdateItemCommand command)
	{
		item.Quantity = command.Quantity > 0 ? command.Quantity : item.Quantity;
		item.Price = command.Price > 0 ? new Money(command.Price, Currency.None) : item.Price;
		
		return item;
	}
}