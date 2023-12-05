using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Cart.Update;

public sealed record UpdateCartCommand(
	Guid Id,
	string Name,
	string Comment
) : ICommand<Result>;