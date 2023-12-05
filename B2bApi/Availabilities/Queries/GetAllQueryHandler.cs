using B2bApi.Availabilities.Repositories;
using B2bApi.Shared.Queries;

namespace B2bApi.Availabilities.Queries;

public class GetAllQueryHandler : IQueryHandler<GetAllQuery, IEnumerable<Entities.Availabilities>>
{
	private readonly IAvailabilityRepository _availabilityRepository;
	
	public GetAllQueryHandler(IAvailabilityRepository availabilityRepository)
	{
		_availabilityRepository = availabilityRepository;
	}

	public Task<IEnumerable<Entities.Availabilities>> Handle(GetAllQuery request, CancellationToken cancellationToken) 
		=> _availabilityRepository.GetAllAsync(cancellationToken);
}