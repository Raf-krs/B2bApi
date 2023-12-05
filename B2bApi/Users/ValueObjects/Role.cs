using B2bApi.Shared.Exceptions;

namespace B2bApi.Users.ValueObjects;

public sealed record Role
{
	public string Value { get; }
	public static Role User => new("user");
	public static Role Admin => new("admin");
	
	private readonly List<string> _roles = new()
	{
		"admin",
		"user"
	};
	
	public Role(string value)
	{
		var error = IsValid(value);
		if(!string.IsNullOrEmpty(error))
		{
			throw new InvalidInputException(error);
		}	
		Value = value;
	}

	public static implicit operator Role(string role) => new(role);
	public static implicit operator string(Role role) => role.Value;
	public override string ToString() => Value;
	
	private string IsValid(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return "Role cannot be empty.";
		}

		if(!_roles.Contains(value.ToLower()))
		{
			return "Invalid role type.";
		}
		
		return string.Empty;
	}
}