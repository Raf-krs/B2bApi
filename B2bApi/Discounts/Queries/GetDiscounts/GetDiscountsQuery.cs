using B2bApi.Discounts.Dtos.Requests;
using B2bApi.Discounts.Entities;
using B2bApi.Shared.Dtos;
using B2bApi.Shared.Queries;

namespace B2bApi.Discounts.Queries.GetDiscounts;

public sealed record GetDiscountsQuery(GetDiscountsRequest Request) : IQuery<PagedResponse<Discount>>;