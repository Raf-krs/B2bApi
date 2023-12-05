using B2bApi.Points.Policies;
using B2bApi.Points.Repositories;
using MediatR;

namespace B2bApi.Orders.Events;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
	private readonly IEnumerable<IPointsPolicy> _pointsPolicies;
	private readonly IPointsRepository _pointsRepository;

	public OrderCreatedEventHandler(IEnumerable<IPointsPolicy> pointsPolicies, IPointsRepository pointsRepository)
	{
		_pointsPolicies = pointsPolicies;
		_pointsRepository = pointsRepository;
	}

	public async Task Handle(OrderCreatedEvent @event, CancellationToken cancellationToken)
	{
		// Do something with notification.Order
		await AddPoints(@event.Order.UserId, @event.Order.TotalAmount, cancellationToken);
	}

	private async Task AddPoints(Guid userId, decimal total, CancellationToken cancellationToken)
	{
		var policies = _pointsPolicies.Where(x => x.CanBeApplied(total));
		var points = policies.Sum(x => x.IncreasePoints(userId));
		await _pointsRepository.IncreasePointsAsync(userId, points, cancellationToken);
	}
}