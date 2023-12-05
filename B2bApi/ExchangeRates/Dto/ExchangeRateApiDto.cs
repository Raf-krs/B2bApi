using B2bApi.ExchangeRates.Entities;
using Newtonsoft.Json;

namespace B2bApi.ExchangeRates.Dtos;

public class ExchangeRateApiDto
{
	[JsonProperty("no")]
	public string No { get; set; }
	
	[JsonProperty("table")]
	public char Table { get; set; }
	
	[JsonProperty("effectiveDate")]
	public DateTime Date { get; set; }
	
	[JsonProperty("rates")]
	public List<RateApiDto> Rates { get; set; }
	
	public ExchangeRate ToEntity()
	{
		return ExchangeRate.Create(No, Table, Date, Rates.Select(x => x.ToEntity()).ToList());
	}
}