using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Discounts.Commands.Create;

public sealed record CreateDiscountCommand(Guid ProductId, decimal Price, DateOnly Start, DateOnly End) 
	: ICommand<Result>;