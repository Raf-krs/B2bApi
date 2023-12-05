using B2bApi.Products.Dtos.Requests;
using B2bApi.Products.Dtos.Responses;
using B2bApi.Shared.Dtos;
using B2bApi.Shared.Queries;

namespace B2bApi.Products.Queries.GetProducts;

public sealed record GetProductsQuery(ProductsRequest Request, Guid UserId) : IQuery<PagedResponse<ProductDto>>;