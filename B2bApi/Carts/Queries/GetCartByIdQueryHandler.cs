using B2bApi.Carts.Entities;
using B2bApi.Carts.Repositories.Cart;
using B2bApi.Shared.Queries;
using FluentResults;

namespace B2bApi.Carts.Queries;

public class GetCartByIdQueryHandler : IQueryHandler<GetCartByIdQuery, Result<Cart>>
{
	private readonly ICartRepository _cartRepository;
	
	public GetCartByIdQueryHandler(ICartRepository cartRepository)
	{
		_cartRepository = cartRepository;
	}

	public async Task<Result<Cart>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
		if(cart is null)
		{
			return Result.Fail("Cart not found");
		}
		
		return Result.Ok(cart.ToEntity());
	}
}