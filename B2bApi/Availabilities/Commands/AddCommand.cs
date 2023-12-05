using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Availabilities.Commands;

public sealed record AddCommand(Guid ProductId, int Quantity) : ICommand<Result>;