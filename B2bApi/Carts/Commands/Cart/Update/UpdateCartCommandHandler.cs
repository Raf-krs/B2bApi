using B2bApi.Carts.Repositories.Cart;
using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Cart.Update;

public class UpdateCartCommandHandler : ICommandHandler<UpdateCartCommand, Result>
{
	private readonly ICartRepository _cartRepository;
	
	public UpdateCartCommandHandler(ICartRepository cartRepository)
	{
		_cartRepository = cartRepository;
	}

	public async Task<Result> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetByEfCoreIdAsync(request.Id, cancellationToken);
		if(cart is null)
		{
			// TODO -> add error type objects
			return Result.Fail("Cart not found");
		}
		if(cart.OrderId is not null)
		{
			// TODO -> add error type objects
			return Result.Fail("Cart is ordered");
		}
		var updatedCart = Entities.Cart.Update(cart, request);
		await _cartRepository.UpdateAsync(updatedCart, cancellationToken);
		
		return Result.Ok();
	}
}