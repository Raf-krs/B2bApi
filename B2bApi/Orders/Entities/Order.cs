using B2bApi.Shared.Abstractions.Data;

namespace B2bApi.Orders.Entities;

public sealed class Order : IEntity
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }
	public decimal TotalAmount { get; private set; }
	public DateTime CreatedAt { get; private set; }
	
	private Order() { }
	
	private Order(Guid id, Guid userId, decimal totalAmount, DateTime createdAt)
	{
		Id = id;
		UserId = userId;
		TotalAmount = totalAmount;
		CreatedAt = createdAt;
	}
	
	public static Order Create(Guid userId, decimal totalAmount, DateTime createdAt) 
		=> new(Guid.NewGuid(), userId, totalAmount, createdAt);
}