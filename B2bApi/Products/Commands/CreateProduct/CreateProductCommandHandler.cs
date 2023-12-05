using B2bApi.Products.Entities;
using B2bApi.Products.Repositories;
using B2bApi.Shared.Commands;

namespace B2bApi.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
	private readonly IProductRepository _productRepository;
	
	public CreateProductCommandHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		var product = Product.Create(Guid.NewGuid(), command.Name, command.Description, command.Price);
		await _productRepository.AddAsync(product, cancellationToken);
		
		return product.Id;
	}
}