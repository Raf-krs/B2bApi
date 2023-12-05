using B2bApi.Shared.Queries;

namespace B2bApi.Points.Queries;

public sealed record GetPointsQuery(Guid UserId) : IQuery<int>;