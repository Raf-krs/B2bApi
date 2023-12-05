using B2bApi.Shared.Commands;
using MediatR;

namespace B2bApi.Users.Commands.Password;

public sealed record ChangePasswordCommand(string Email, string Password) : ICommand<Unit>;