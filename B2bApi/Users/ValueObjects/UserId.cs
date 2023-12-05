namespace B2bApi.Users.ValueObjects;

public record UserId(Guid Value)
{
	public static UserId New() => new(Guid.NewGuid());
	
	public static implicit operator UserId(Guid id) => new(id);
	public static implicit operator Guid(UserId id) => id.Value;
}