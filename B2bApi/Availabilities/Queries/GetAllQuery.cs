using B2bApi.Shared.Queries;

namespace B2bApi.Availabilities.Queries;

public sealed record GetAllQuery : IQuery<IEnumerable<Entities.Availabilities>>;