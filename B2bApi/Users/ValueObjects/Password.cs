using System.Text.RegularExpressions;
using B2bApi.Shared.Exceptions;

namespace B2bApi.Users.ValueObjects;

public sealed record Password
{
	public string Value { get; }
	private readonly Regex _regex = new(@"^(?!.*\s)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

	public Password(string value)
	{
		if(IsValid(value) is false)
		{
			throw new InvalidInputException("Password is invalid. Must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character.");
		}

		Value = value;
	}

	public static implicit operator Password(string password) => new(password);
	public static implicit operator string(Password password) => password.Value;
	public override string ToString() => Value;

	private bool IsValid(string value) => _regex.IsMatch(value);
}