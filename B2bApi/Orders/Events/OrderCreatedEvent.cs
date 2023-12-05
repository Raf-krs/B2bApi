using B2bApi.Orders.Entities;
using MediatR;

namespace B2bApi.Orders.Events;

public class OrderCreatedEvent : INotification
{
	public Order Order { get; init; }
}