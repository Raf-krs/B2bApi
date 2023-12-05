using B2bApi.Shared.Repositories;

namespace B2bApi.Points.Repositories;

public interface IPointsRepository : IRepository
{
	Task<int> GetPointsAsync(Guid userId, CancellationToken cancellationToken);
	Task IncreasePointsAsync(Guid userId, int points, CancellationToken cancellationToken);
}