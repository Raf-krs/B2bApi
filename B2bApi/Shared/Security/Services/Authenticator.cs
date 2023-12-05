using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using B2bApi.Shared.Clock;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.Security.Dtos;
using B2bApi.Shared.Security.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace B2bApi.Shared.Security.Services;

public sealed class Authenticator : IAuthenticator
{
	private readonly IClock _clock;
	private readonly string _issuer;
	private readonly string _audience;
	private readonly TimeSpan? _expiry;
	private readonly SigningCredentials _signingCredentials;
	private readonly JwtSecurityTokenHandler _tokenHandler;
	
	public Authenticator(IOptions<AuthOptions> options, IClock clock)
	{
		_clock = clock;
		_issuer = options.Value.Issuer;
		_audience = options.Value.Audience;
		_expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
		_signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key)), 
													SecurityAlgorithms.HmacSha256);
		_tokenHandler = new JwtSecurityTokenHandler();
	}
	
	public JwtDto CreateToken(Guid userId, string email, string role, string currency)
	{
		var now = _clock.Current();
		var expires = now.Add(_expiry!.Value);

		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
			new(ClaimTypes.Email, email),
			new(ClaimTypes.Role, role),
			new("Currency", currency)
		};
		
		var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
		var token = _tokenHandler.WriteToken(jwt);
		
		return new JwtDto(token);
	}
}