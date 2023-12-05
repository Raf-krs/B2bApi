using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.ValueObjects;
using B2bApi.Users.ValueObjects;

namespace B2bApi.Users.Entities;

public sealed class User : IEntity
{
	public Guid Id { get; private set; }
	public Email Email { get; private set; }
	public Username Username { get; private set; }
	public Password Password { get; private set; }
	public Role Role { get; private set; }
	public Currency Currency { get; private set; }

	private User()
	{
	}

	private User(Guid id, Email email, Username username, Password password, Role role, Currency currency)
	{
		Id = id;
		Email = email;
		Username = username;
		Password = password;
		Role = role;
		Currency = currency;
	}
	
	public static User Create(Guid id, string email, string username, string password, string role, string currency) 
		=> new(id, email, username, password, role, Currency.FromCode(currency));
	
	public static User UpdatePassword(User user, Password password)
	{
		user.Password = password;
		return user;
	}
}