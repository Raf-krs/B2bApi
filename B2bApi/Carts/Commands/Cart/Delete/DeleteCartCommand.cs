using B2bApi.Shared.Commands;
using FluentResults;
using MediatR;

namespace B2bApi.Carts.Commands.Cart.Delete;

public sealed record DeleteCartCommand(Guid Id) : ICommand<Result>;