namespace B2bApi.Shared.Clock;

public class Clock : IClock
{
	public DateTime Current() => DateTime.UtcNow;
}