using B2bApi.ExchangeRates.Dtos;
using B2bApi.ExchangeRates.Repositories;
using B2bApi.Shared.Queries;

namespace B2bApi.ExchangeRates.Queries;

public class GetRatesByCodeQueryHandler : IQueryHandler<GetRatesByCodeQuery, ExchangeRatesResponse>
{
	private readonly IExchangeRatesRepository _exchangeRatesRepository;
	
	public GetRatesByCodeQueryHandler(IExchangeRatesRepository exchangeRatesRepository)
	{
		_exchangeRatesRepository = exchangeRatesRepository;
	}

	public Task<ExchangeRatesResponse> Handle(GetRatesByCodeQuery request, CancellationToken cancellationToken) 
		=> _exchangeRatesRepository.GetByCodeAsync(request.Code, cancellationToken);
}