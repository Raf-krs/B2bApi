using B2bApi.Shared.Clock;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.Security.Options;
using B2bApi.Shared.Security.Services;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.ValueObjects;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace B2bApiTests.Security;

public class AuthenticatorTests
{
	private readonly IAuthenticator _authenticator;

	public AuthenticatorTests()
	{
		var options = Options.Create(new AuthOptions
		{
			Issuer = "test",
			Audience = "test",
			Key = "6N],Gdt1Cz@LdnVfD)x6}Uzo:m,q]KGZ:=]swC21fGw_?,sZ2>m@X-~z]%YVG.:w3EDF>..}su5@JGj?U^_~,}dHV##M7nE>TZoW"
		});
		IClock clock = new Clock();
		_authenticator = new Authenticator(options, clock);
	}

	[Fact]
	public void CreateToken_Should_ReturnToken()
	{
		// Arrange
		var userId = Guid.NewGuid();
		const string email = "test@mail.com";
		var role = Role.User.ToString();
		var currency = Currency.Pln.ToString();

		// Act
		var jwtDto = _authenticator.CreateToken(userId, email, role, currency);

		// Assert
		jwtDto.AccessToken.ShouldNotBeNullOrEmpty();
	}
}