using B2bApi.Shared.Abstractions.Data;

namespace B2bApi.ExchangeRates.Entities;

public sealed class ExchangeRate : IEntity
{
	public string No { get; private set; }
	public char Table { get; private set; }
	public DateTime Date { get; private set; }
	public List<Rate> Rates { get; private set; }
	
	private ExchangeRate() { }

	private ExchangeRate(string no, char table, DateTime date, List<Rate> rates)
	{
		No = no;
		Table = table;
		Date = date;
		Rates = rates;
	}

	public static ExchangeRate Create(string no, char table, DateTime date, List<Rate> rates) 
		=> new(no, table, date, rates);
}