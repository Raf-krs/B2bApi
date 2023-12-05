using System.Net.Mail;
using B2bApi.Shared.Exceptions;

namespace B2bApi.Users.ValueObjects;

public sealed record Email
{
	public string Value { get; }
	
	public Email(string value)
	{
		var error = IsValid(value);
		if(!string.IsNullOrEmpty(error))
		{
			throw new InvalidInputException(error);
		}
		Value = value;
	}

	public static implicit operator Email(string email) => new(email);
	public static implicit operator string(Email email) => email.Value;
	public override string ToString() => Value;
	
	private string IsValid(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return "Email cannot be empty.";
		}

		if(!CheckEmail(value!))
		{
			return $"Email: {value} is invalid.";
		}
		
		return string.Empty;
	}

	private bool CheckEmail(string value)
	{
		var valid = true;
		try
		{ 
			_ = new MailAddress(value);
		}
		catch
		{
			valid = false;
		}

		return valid;
	}
}