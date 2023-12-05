namespace B2bApi.ExchangeRates.Dtos;

public record RatesViewDto(string No, DateTime Date, string Name, string Code, decimal Value);