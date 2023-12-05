namespace B2bApi.ExchangeRates.Dtos;

public record ExchangeRatesResponse(
	string No,
	DateTime Date,
	List<RatesResponse> Rates
);