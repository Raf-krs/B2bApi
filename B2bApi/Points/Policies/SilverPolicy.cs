namespace B2bApi.Points.Policies;

public class SilverPolicy : IPointsPolicy
{
	public bool CanBeApplied(decimal totalAmount) => totalAmount >= 100;
	public int IncreasePoints(Guid userId) => 5;
}