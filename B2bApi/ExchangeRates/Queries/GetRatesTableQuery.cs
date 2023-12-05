using B2bApi.ExchangeRates.Dtos;
using B2bApi.Shared.Queries;

namespace B2bApi.ExchangeRates.Queries;

public record GetRatesTableQuery() : IQuery<IEnumerable<ExchangeRatesTableResponse>>;