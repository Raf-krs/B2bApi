using B2bApi.Shared.Commands;
using B2bApi.Shared.Security.Dtos;

namespace B2bApi.Users.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<JwtDto>;