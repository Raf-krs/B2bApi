using System.Text;
using B2bApi.Shared.Options;
using B2bApi.Shared.Security.Abstractions;
using B2bApi.Shared.Security.Options;
using B2bApi.Shared.Security.Services;
using B2bApi.Users.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace B2bApi.Shared.Security;

public static class Extensions
{
	public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
	{
		var options = services.GetOptions<AuthOptions>(config, AuthOptions.SectionName);

		services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
		services.AddSingleton<IPasswordManager, PasswordManager>();
		services.AddSingleton<IAuthenticator, Authenticator>();

		services.AddAuthentication(ao =>
		{
			ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			ao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(jbo =>
		{
			jbo.Audience = options.Audience;
			jbo.IncludeErrorDetails = true;
			jbo.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = options.Issuer,
				ClockSkew = TimeSpan.Zero,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key))
			};
		});
		
		services.AddAuthorization(authorization =>
		{
			authorization.AddPolicy("admin", policy =>
			{
				policy.RequireRole("admin");
			});
		});

		services.AddHttpContextAccessor();
		services.AddScoped<IUserContext, UserContext>();

		return services;
	}
}