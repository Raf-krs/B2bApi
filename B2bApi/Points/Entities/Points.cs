using B2bApi.Shared.Abstractions.Data;

namespace B2bApi.Points.Entities;

public sealed class Points : IEntity
{
	public Guid Id { get; private set; }
	public Guid UserId { get; private set; }
	public int Amount { get; private set; }
	
	private Points() { }
	
	private Points(Guid id, Guid userId, int amount)
	{
		Id = id;
		UserId = userId;
		Amount = amount;
	}
	
	public static Points Create(Guid userId, int amount) => new(Guid.NewGuid(), userId, amount);
}