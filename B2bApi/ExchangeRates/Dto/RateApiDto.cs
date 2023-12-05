using B2bApi.ExchangeRates.Entities;
using Newtonsoft.Json;

namespace B2bApi.ExchangeRates.Dtos;

public class RateApiDto
{
	[JsonProperty("currency")]
	public string Name { get; set; }
	
	[JsonProperty("code")]
	public string Code { get; set; }

	[JsonProperty("mid")]
	public decimal Mid { get; set; }
	
	[JsonProperty("bid")]
	public decimal Bid { get; set; }
	
	[JsonProperty("Ask")]
	public decimal Ask { get; set; }
	
	public Rate ToEntity()
	{
		return Rate.Create(Name, Code, Mid, Bid, Ask, default);
	}
}