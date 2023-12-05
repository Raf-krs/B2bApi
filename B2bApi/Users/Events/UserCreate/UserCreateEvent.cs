using B2bApi.Users.Entities;
using MediatR;

namespace B2bApi.Users.Events.UserCreate;

public class UserCreateEvent : INotification
{
	public User User { get; init; }
}