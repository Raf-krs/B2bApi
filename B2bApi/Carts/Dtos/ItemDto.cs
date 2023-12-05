namespace B2bApi.Carts.Dtos;

public record ItemDto(Guid Id, Guid CartId, Guid ProductId, decimal Price, int Quantity)
{
	public Entities.Items ToEntity() => Entities.Items.Create(Id, CartId, ProductId, Quantity, Price);
}