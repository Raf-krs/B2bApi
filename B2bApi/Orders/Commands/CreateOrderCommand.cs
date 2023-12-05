using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Orders.Commands;

public sealed record CreateOrderCommand(Guid CartId) : ICommand<Result<Guid>>;