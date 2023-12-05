using B2bApi.Carts.Entities;
using B2bApi.Carts.Repositories.Item;
using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Carts.Commands.Item.UpdateItem;

public class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand, Result>
{
	private readonly ICartItemRepository _itemRepository;
	
	public UpdateItemCommandHandler(ICartItemRepository itemRepository)
	{
		_itemRepository = itemRepository;
	}

	public async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
	{
		var item = await _itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
		if(item is null)
		{
			return Result.Fail("Item not found");
		}
		var updatedItem = Items.Update(item, request);
		await _itemRepository.UpdateAsync(updatedItem, cancellationToken);
		
		return Result.Ok();
	}
}