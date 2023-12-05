using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Item.UpdateItem;

public sealed record UpdateItemCommand(
	Guid ItemId,
	int Quantity,
	decimal Price
) : ICommand<Result>;