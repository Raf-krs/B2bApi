using B2bApi.Products.Dtos.Requests;
using B2bApi.Products.Dtos.Responses;
using B2bApi.Products.Entities;
using B2bApi.Shared.Dtos;
using B2bApi.Shared.Repositories;

namespace B2bApi.Products.Repositories;

public interface IProductRepository : IRepository
{
	Task AddAsync(Product product, CancellationToken cancellationToken);
	Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<PagedResponse<ProductDto>> GetAllAsync(ProductsRequestOptions options, CancellationToken cancellationToken);
}