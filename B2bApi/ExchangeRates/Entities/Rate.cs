using B2bApi.Shared.Abstractions.Data;

namespace B2bApi.ExchangeRates.Entities;

public sealed class Rate : IEntity
{
	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public string Code { get; private set; }
	public decimal Mid { get; private set; }
	public decimal Bid { get; private set; }
	public decimal Ask { get; private set; }
	public ExchangeRate ExchangeRate { get; private set; }
	
	private Rate() { }

	private Rate(Guid id, string name, string code, decimal mid, decimal bid, decimal ask, ExchangeRate exchangeRate)
	{
		Id = id;
		Name = name;
		Code = code;
		Mid = mid;
		Bid = bid;
		Ask = ask;
		ExchangeRate = exchangeRate;
	}
	
	public static Rate Create(string name, string code, decimal mid, decimal bid, decimal ask, ExchangeRate exchangeRate) 
		=> new(Guid.NewGuid(), name, code, mid, bid, ask, exchangeRate);
}
