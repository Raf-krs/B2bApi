using B2bApi.Products.Dtos.Requests;
using B2bApi.Products.Dtos.Responses;
using B2bApi.Products.Exceptions;
using B2bApi.Products.Repositories;
using B2bApi.Shared.Dtos;
using B2bApi.Shared.Queries;
using B2bApi.Users.Repositories;

namespace B2bApi.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PagedResponse<ProductDto>>
{
	private readonly IProductRepository _productRepository;
	private readonly IUserRepository _userRepository;
	
	public GetProductsQueryHandler(IProductRepository productRepository, IUserRepository userRepository)
	{
		_productRepository = productRepository;
		_userRepository = userRepository;
	}

	public async Task<PagedResponse<ProductDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
	{
		var currencyCode = "PLN";
		if(query.UserId != Guid.Empty)
		{
			var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);
			currencyCode = user.Currency.Code;
		}
		var options = new ProductsRequestOptions(query.Request.Page, query.Request.PageSize, query.Request.Name, 
												currencyCode, query.Request.OrderBy);
		
		var result = await _productRepository.GetAllAsync(options, cancellationToken);
		if(result.Items.Any(x => x.Price == null))
		{
			throw new CalculatePriceException();
		}
		
		return result;
	}
}