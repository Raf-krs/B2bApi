using B2bApi.Discounts.Entities;
using B2bApi.Discounts.Repositories;
using B2bApi.Shared.Queries;
using FluentResults;

namespace B2bApi.Discounts.Queries.GetDiscount;

public class GetDiscountQueryHandler : IQueryHandler<GetDiscountQuery, Result<Discount>>
{
	private readonly IDiscountRepository _discountRepository;
	
	public GetDiscountQueryHandler(IDiscountRepository discountRepository)
	{
		_discountRepository = discountRepository;
	}

	public async Task<Result<Discount>> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
	{
		var discount = await _discountRepository.GetByIdAsync(request.Id, cancellationToken);
		if(discount is null)
		{
			return Result.Fail("Discount not found");
		}
		await _discountRepository.GetByIdAsync(request.Id, cancellationToken);
		
		return Result.Ok();
	}
}