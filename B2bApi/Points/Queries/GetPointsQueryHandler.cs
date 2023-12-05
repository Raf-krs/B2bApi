using B2bApi.Points.Repositories;
using B2bApi.Shared.Queries;

namespace B2bApi.Points.Queries;

public class GetPointsQueryHandler : IQueryHandler<GetPointsQuery, int>
{
	private readonly IPointsRepository _pointsRepository;
	
	public GetPointsQueryHandler(IPointsRepository pointsRepository)
	{
		_pointsRepository = pointsRepository;
	}

	public Task<int> Handle(GetPointsQuery request, CancellationToken cancellationToken) 
		=> _pointsRepository.GetPointsAsync(request.UserId, cancellationToken);
}