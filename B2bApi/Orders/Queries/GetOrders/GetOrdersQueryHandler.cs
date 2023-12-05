using B2bApi.Orders.Entities;
using B2bApi.Orders.Repositories;
using B2bApi.Shared.Queries;

namespace B2bApi.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, IEnumerable<Order>>
{
	private readonly IOrderRepository _orderRepository;
	
	public GetOrdersQueryHandler(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken) 
		=> await _orderRepository.GetByUserId(request.UserId, cancellationToken);
}