using B2bApi.Carts.Entities;
using B2bApi.Carts.Repositories.Cart;
using B2bApi.Orders.Events;
using B2bApi.Orders.Repositories;
using B2bApi.Shared.Clock;
using B2bApi.Shared.Commands;
using FluentResults;
using MediatR;

namespace B2bApi.Orders.Commands;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result<Guid>>
{
	private readonly IOrderRepository _orderRepository;
	private readonly ICartRepository _cartRepository;
	private readonly IPublisher _publisher;
	private readonly IClock _clock;
	
	public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartRepository cartRepository, 
									IPublisher publisher, IClock clock)
	{
		_orderRepository = orderRepository;
		_cartRepository = cartRepository;
		_publisher = publisher;
		_clock = clock;
	}

	public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken);
		if(cart is null)
		{
			return Result.Fail("Cart not found");
		}
		if(cart.Items.Count == 0)
		{
			return Result.Fail("Cart is empty");
		}
		var total = cart.Items.Sum(x => x.Price * x.Quantity);
		var order = Entities.Order.Create(cart.UserId, total, _clock.Current());
		await _orderRepository.AddAsync(order, cancellationToken);
		
		var cartEfCore = await _cartRepository.GetByEfCoreIdAsync(cart.Id, cancellationToken);
		await _cartRepository.UpdateAsync(cartEfCore, cancellationToken);
		
		_publisher.Publish(new OrderCreatedEvent { Order = order }, cancellationToken);
		
		return Result.Ok(order.Id);
	}
}