using B2bApi.Carts.Commands.Cart.Update;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.ValueObjects;

namespace B2bApi.Carts.Entities;

public sealed class Cart : IEntity
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }
	public Guid? OrderId { get; private set; }
	public Name Name { get; private set; }
	public Currency Currency { get; private set; }
	public string Comment { get; private set; }
	public DateTime CreatedAt { get; private set; }
	
	private Cart() { }
	
	private Cart(Guid userId, Guid? orderId, Name name, Currency currency, string comment, DateTime createdAt)
	{
		Id = Guid.NewGuid();
		UserId = userId;
		OrderId = orderId;
		Name = name;
		Currency = currency;
		Comment = comment;
		CreatedAt = createdAt;
	}

	public override bool Equals(object obj)
	{
		return ((Cart)obj)!.Id == Id;
	}

	public static Cart Create(Guid userId, Guid? orderId, Name name, string currency, string comment, DateTime createdAt) 
		=> new(userId, orderId, name, Currency.FromCode(currency), comment, createdAt);
	
	public static Cart Update(Cart cart, UpdateCartCommand command)
	{
		cart.Name = new Name(command.Name);
		cart.Comment = command.Comment;	
		
		return cart;
	}
	
	public static Cart AddOrder(Cart cart, Guid orderId)
	{
		cart.OrderId = orderId;
		
		return cart;
	}
}