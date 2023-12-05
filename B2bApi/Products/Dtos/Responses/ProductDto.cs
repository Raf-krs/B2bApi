using B2bApi.Products.Entities;

namespace B2bApi.Products.Dtos.Responses;

public class ProductDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	
	public Product ToEntity() => Product.Create(Id, Name, Description, Price);
}