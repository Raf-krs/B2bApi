using B2bApi.Shared.Queries;
using B2bApi.Users.Entities;

namespace B2bApi.Users.Queries;

public record GetUserByEmailQuery(string Email) : IQuery<User>;