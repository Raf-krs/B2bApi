namespace B2bApi.ExchangeRates.Options;

public class SchedulerOptions
{
	public const string SectionName = "Scheduler";

	public bool Enabled { get; set; }
	public string Cron { get; set; }
}