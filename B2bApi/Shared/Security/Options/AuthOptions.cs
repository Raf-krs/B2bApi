namespace B2bApi.Shared.Security.Options;

public sealed class AuthOptions
{
	public const string SectionName = "Auth";

	public string Issuer { get; set; }
	public string Audience { get; set; }
	public string Key { get; set; }
	public TimeSpan? Expiry { get; set; }
}