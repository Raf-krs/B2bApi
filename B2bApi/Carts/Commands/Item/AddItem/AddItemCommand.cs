using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Item.AddItem;

public sealed record AddItemCommand(
	Guid CartId,
	Guid ProductId,
	int Quantity,
	decimal Price
) : ICommand<Result>;