using B2bApi.Products.Entities;
using B2bApi.Shared.Queries;

namespace B2bApi.Products.Queries.GetProduct;

public record GetProductQuery(Guid Id) : IQuery<Product?>;