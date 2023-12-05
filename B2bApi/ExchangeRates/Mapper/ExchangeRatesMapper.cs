using B2bApi.ExchangeRates.Dtos;

namespace B2bApi.ExchangeRates.Mapper;

public static class ExchangeRatesMapper
{
	public static ExchangeRatesResponse ToResponse(IEnumerable<RatesViewDto> ratesViewDtos)
	{
		var exchangeData = ratesViewDtos.ToList();
		var rates = exchangeData.Select(ratesViewDto => new RatesResponse(ratesViewDto.Name, ratesViewDto.Code, ratesViewDto.Value)).ToList();
		var response = new ExchangeRatesResponse(exchangeData.First().No, exchangeData.First().Date, rates);
		
		return response;
	}
}