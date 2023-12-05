using B2bApi.Carts.Dtos;

namespace B2bApi.Carts.Repositories.Cart;

public class CartItemsDto
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid? OrderId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Comment { get; set; } = string.Empty;
	public string Currency { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public Guid ItemId { get; set; }
	public Guid CartId { get; set; }
	public Guid ProductId { get; set; }
	public string ProductName { get; set; } = string.Empty;
	public decimal Price { get; set; } 
	public int Quantity { get; set; }
	
	public CartItemsResponse ToResponse()
	{
		return new CartItemsResponse
		{
			Id = Id,
			Name = Name,
			OrderId = OrderId,
			UserId = UserId,
			CreatedAt = CreatedAt,
			Comment = Comment,
			Currency = Currency,
			Items = new List<ItemDto>
			{
				new(ItemId, CartId, ProductId, Price, Quantity)
			}
		};
	}
}