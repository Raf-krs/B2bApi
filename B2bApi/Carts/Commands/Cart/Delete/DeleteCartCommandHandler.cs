using B2bApi.Carts.Repositories.Cart;
using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Cart.Delete;

public class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand, Result>
{
	private readonly ICartRepository _cartRepository;
	
	public DeleteCartCommandHandler(ICartRepository cartRepository)
	{
		_cartRepository = cartRepository;
	}

	public async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetByEfCoreIdAsync(request.Id, cancellationToken);
		if(cart?.OrderId is not null)
		{
			return Result.Fail("Cart is ordered");
		}
		await _cartRepository.DeleteAsync(cart, cancellationToken);
		
		return Result.Ok();
	}
}