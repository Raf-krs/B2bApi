using B2bApi.Shared.Commands;

namespace B2bApi.Carts.Commands.Cart.Create;

public sealed record CreateCartCommand(string Name) : ICommand<Guid>;