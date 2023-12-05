using B2bApi.Availabilities.Repositories;
using B2bApi.Products.Repositories;
using B2bApi.Shared.Commands;
using FluentResults;

namespace B2bApi.Availabilities.Commands;

public class AddCommandHandler : ICommandHandler<AddCommand, Result>
{
	private readonly IAvailabilityRepository _availabilityRepository;
	private readonly IProductRepository _productRepository;
	
	public AddCommandHandler(IAvailabilityRepository availabilityRepository, IProductRepository productRepository)
	{
		_availabilityRepository = availabilityRepository;
		_productRepository = productRepository;
	}

	public async Task<Result> Handle(AddCommand request, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
		if(product is null)
		{
			return Result.Fail("Product not found");
		}
		
		var availability = Entities.Availabilities.Create(product, request.Quantity);
		await _availabilityRepository.AddAsync(availability, cancellationToken);
		return Result.Ok();
	}
}