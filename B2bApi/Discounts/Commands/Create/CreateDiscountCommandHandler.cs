using B2bApi.Discounts.Entities;
using B2bApi.Discounts.Repositories;
using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Discounts.Commands.Create;

public class CreateDiscountCommandHandler : ICommandHandler<CreateDiscountCommand, Result>
{
	private readonly IDiscountRepository _discountRepository;
	
	public CreateDiscountCommandHandler(IDiscountRepository discountRepository)
	{
		_discountRepository = discountRepository;
	}

	public async Task<Result> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
	{
		if(request.Start > request.End)
		{
			return Result.Fail("Start date cannot be greater than end date");
		}
		
		var isExistsDiscountInDateRange = await _discountRepository.ExistsDiscountInDateRangeAsync(
			request.ProductId, request.Start, cancellationToken);
		if(isExistsDiscountInDateRange)
		{
			return Result.Fail("Discount for this product already exists in this date range");
		}
		
		var discount = Discount.Create(Guid.NewGuid(), request.ProductId, request.Price, request.Start, request.End);
		await _discountRepository.AddAsync(discount, cancellationToken);
		
		return Result.Ok();
	}
}