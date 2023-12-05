using B2bApi.Carts.Commands.Item.UpdateItem;
using B2bApi.Carts.Entities;
using B2bApi.Carts.Repositories.Cart;
using B2bApi.Carts.Repositories.Item;
using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Item.AddItem;

public class AddItemCommandHandler : ICommandHandler<AddItemCommand, Result>
{
	private readonly ICartItemRepository _itemRepository;
	private readonly ICartRepository _cartRepository;
	
	public AddItemCommandHandler(ICartItemRepository itemRepository, ICartRepository cartRepository)
	{
		_itemRepository = itemRepository;
		_cartRepository = cartRepository;
	}

	public async Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken);
		if(cart is null)
		{
			return Result.Fail("Cart not found");
		}
		
		var product = await _itemRepository.GetByIdAsync(request.ProductId, cancellationToken);
		if(product is null)
		{
			var item = Items.Create(Guid.NewGuid(), request.CartId, request.ProductId, request.Quantity, request.Price);
			await _itemRepository.AddAsync(item, cancellationToken);	
		}
		else
		{
			var newQuantity = product.Quantity.Value + request.Quantity;
			var updatedItem = Items.Update(product, new UpdateItemCommand(product.Id, newQuantity, request.Price));
			await _itemRepository.UpdateAsync(updatedItem, cancellationToken);
		}
		
		return Result.Ok();
	}
}