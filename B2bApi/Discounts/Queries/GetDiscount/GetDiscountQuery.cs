using B2bApi.Discounts.Entities;
using B2bApi.Shared.Queries;
using FluentResults;

namespace B2bApi.Discounts.Queries.GetDiscount;

public record GetDiscountQuery(Guid Id) : IQuery<Result<Discount>>;