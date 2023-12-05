using B2bApi.Shared.Repositories;

namespace B2bApi.Availabilities.Repositories;

public interface IAvailabilityRepository : IRepository
{
	Task<IEnumerable<Entities.Availabilities>> GetAllAsync(CancellationToken cancellationToken = default);
	Task AddAsync(Entities.Availabilities availabilities, CancellationToken cancellationToken = default);
}