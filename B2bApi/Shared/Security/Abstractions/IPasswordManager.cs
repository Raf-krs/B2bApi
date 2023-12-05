namespace B2bApi.Shared.Security.Abstractions;

public interface IPasswordManager
{
	string Secure(string password);
	bool Verify(string password, string hash);
}