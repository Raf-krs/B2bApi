using B2bApi.Carts.Entities;

namespace B2bApi.Carts.Dtos;

public class CartItemsResponse
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid? OrderId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Comment { get; set; } = string.Empty;
	public string Currency { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public List<ItemDto> Items { get; set; } = new();

	public Cart ToEntity() => Cart.Create(UserId, OrderId, Name, Currency, Comment, CreatedAt);
}