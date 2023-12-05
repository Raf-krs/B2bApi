using B2bApi.Carts.Entities;
using B2bApi.Shared.Queries;
using FluentResults;

namespace B2bApi.Carts.Queries;

public sealed record GetCartByIdQuery(Guid Id) : IQuery<Result<Cart>>;