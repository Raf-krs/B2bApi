using B2bApi.Shared.Security.Abstractions;
using B2bApi.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace B2bApi.Shared.Security.Services;

public sealed class PasswordManager : IPasswordManager
{
	private readonly IPasswordHasher<User> _passwordHasher;
	
	public PasswordManager(IPasswordHasher<User> passwordHasher)
	{
		_passwordHasher = passwordHasher;
	}

	public string Secure(string password) => _passwordHasher.HashPassword(default!, password);

	public bool Verify(string password, string hash) 
		=> _passwordHasher.VerifyHashedPassword(default!, hash, password) 
			== PasswordVerificationResult.Success;
}