using B2bApi.Shared.Security.Dtos;

namespace B2bApi.Shared.Security.Abstractions;

public interface IAuthenticator
{
	JwtDto CreateToken(Guid userId, string email, string role, string currency);
}