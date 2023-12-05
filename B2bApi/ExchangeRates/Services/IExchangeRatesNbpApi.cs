using B2bApi.ExchangeRates.Dtos;
using FluentResults;

namespace B2bApi.ExchangeRates.Services;

public interface IExchangeRatesNbpApi
{
	Task<Result<ExchangeRateApiDto>> GetExchangeRatesAsync(ExchangeTables table = ExchangeTables.A);
}