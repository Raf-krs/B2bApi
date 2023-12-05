using B2bApi.ExchangeRates.Dtos;
using B2bApi.ExchangeRates.Entities;
using B2bApi.Shared.Repositories;

namespace B2bApi.ExchangeRates.Repositories;

public interface IExchangeRatesRepository : IRepository
{
	Task AddAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken = default);
	Task<IEnumerable<ExchangeRatesTableResponse>> GetAsync(CancellationToken cancellationToken = default);
	Task<ExchangeRatesResponse> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
	Task<RatesResponse> GetLatestRateByCodeAsync(string code, CancellationToken cancellationToken = default);
}