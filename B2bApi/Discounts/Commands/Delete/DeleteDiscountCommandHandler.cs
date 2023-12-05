using B2bApi.Discounts.Repositories;
using B2bApi.Shared.Commands;
using MediatR;

namespace B2bApi.Discounts.Commands.Delete;

public class DeleteDiscountCommandHandler : ICommandHandler<DeleteDiscountCommand, Unit>
{
	private readonly IDiscountRepository _discountRepository;
	
	public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
	{
		_discountRepository = discountRepository;
	}

	public async Task<Unit> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
	{
		var discount = await _discountRepository.GetByIdAsync(request.Id, cancellationToken);
		if(discount is not null)
		{
			await _discountRepository.DeleteAsync(discount, cancellationToken);			
		}
		return Unit.Value;
	}
}