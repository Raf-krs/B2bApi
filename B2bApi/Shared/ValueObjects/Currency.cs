namespace B2bApi.Shared.ValueObjects;

public sealed record Currency
{
	public string Code { get; }
	
	public static Currency None => new("");
	public static Currency Pln => new("PLN");
	public static Currency Usd => new("USD");
	public static Currency Eur => new("EUR");
	public static Currency Chf => new("CHF");
	public static Currency Gbp => new("GBP");

	private Currency(string code) => Code = code;

	public static Currency FromCode(string code)
		=> code.ToUpper() switch
		{
			"PLN" => Pln,
			"USD" => Usd,
			"EUR" => Eur,
			"CHF" => Chf,
			"GBP" => Gbp,
			_ => None
		};
	
	public static readonly IReadOnlyCollection<Currency> All = new[]
	{
		Pln, Usd, Eur, Chf, Gbp
	};

	public override string ToString() => Code.ToUpper();
}