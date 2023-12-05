using B2bApi.Shared.Commands;
using MediatR;

namespace B2bApi.Discounts.Commands.Delete;

public sealed record DeleteDiscountCommand(Guid Id) : ICommand<Unit>;