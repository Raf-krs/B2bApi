using B2bApi.ExchangeRates.Dtos;
using B2bApi.ExchangeRates.Repositories;
using B2bApi.Shared.Queries;

namespace B2bApi.ExchangeRates.Queries;

public class GetRatesTableQueryHandler : IQueryHandler<GetRatesTableQuery, IEnumerable<ExchangeRatesTableResponse>>
{
	private readonly IExchangeRatesRepository _exchangeRatesRepository;
	
	public GetRatesTableQueryHandler(IExchangeRatesRepository exchangeRatesRepository)
	{
		_exchangeRatesRepository = exchangeRatesRepository;
	}

	public Task<IEnumerable<ExchangeRatesTableResponse>> Handle(GetRatesTableQuery request, 
																CancellationToken cancellationToken) 
		=> _exchangeRatesRepository.GetAsync(cancellationToken);
}