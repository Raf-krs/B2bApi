using B2bApi.Carts.Repositories.Item;
using B2bApi.Shared.Commands;
using MediatR;

namespace B2bApi.Carts.Commands.Item.DeleteItem;

public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand, Unit>
{
	private readonly ICartItemRepository _cartItemRepository;
	
	public DeleteItemCommandHandler(ICartItemRepository cartItemRepository)
	{
		_cartItemRepository = cartItemRepository;
	}

	public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
	{
		await _cartItemRepository.DeleteAsync(request.Id, cancellationToken);
		
		return Unit.Value;
	}
}