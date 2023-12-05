using B2bApi.Shared.Db;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.Availabilities.Repositories;

public class AvailabilityRepository : IAvailabilityRepository
{
	private readonly AppDbContext _dbContext;
	
	public AvailabilityRepository(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IEnumerable<Entities.Availabilities>> GetAllAsync(CancellationToken cancellationToken = default) 
		=> await _dbContext.Availabilities.ToListAsync(cancellationToken);

	public async Task AddAsync(Entities.Availabilities availabilities, CancellationToken cancellationToken = default)
	{
		await _dbContext.Availabilities.AddAsync(availabilities, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}