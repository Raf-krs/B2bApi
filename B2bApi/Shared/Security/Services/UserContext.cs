using System.Security.Claims;
using B2bApi.Shared.Security.Abstractions;

namespace B2bApi.Shared.Security.Services;

public sealed class UserContext : IUserContext
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	
	public UserContext(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string Identity 
		=> _httpContextAccessor
			.HttpContext?
			.User
			.Identity?
			.Name;

	public string Email 
		=> _httpContextAccessor
			.HttpContext?
			.User
			.Claims
			.FirstOrDefault(x => x.Type == ClaimTypes.Email)?
			.Value;

	public string Role 
		=> _httpContextAccessor
			.HttpContext?
			.User
			.Claims
			.FirstOrDefault(x => x.Type == ClaimTypes.Role)?
			.Value;
	
	public string Currency
		=> _httpContextAccessor
			.HttpContext?
			.User
			.Claims
			.FirstOrDefault(x => x.Type.Equals("Currency", StringComparison.OrdinalIgnoreCase))?
			.Value;
}