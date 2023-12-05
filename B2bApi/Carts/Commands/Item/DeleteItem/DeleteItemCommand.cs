using B2bApi.Shared.Commands;
using MediatR;

namespace B2bApi.Carts.Commands.Item.DeleteItem;

public sealed record DeleteItemCommand(Guid Id) : ICommand<Unit>;