using B2bApi.Shared.Commands;

namespace B2bApi.Users.Commands.Register;

public sealed record RegisterCommand(string Email, string Username, string Password, string Currency) : ICommand<Guid>;