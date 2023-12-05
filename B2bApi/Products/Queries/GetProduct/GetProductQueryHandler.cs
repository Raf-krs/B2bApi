using B2bApi.Products.Entities;
using B2bApi.Products.Repositories;
using B2bApi.Shared.Queries;

namespace B2bApi.Products.Queries.GetProduct;

public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, Product> 
{
	private readonly IProductRepository _productRepository;
	
	public GetProductQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken) 
		=> _productRepository.GetByIdAsync(request.Id, cancellationToken);
}