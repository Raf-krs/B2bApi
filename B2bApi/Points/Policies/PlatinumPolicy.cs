namespace B2bApi.Points.Policies;

public class PlatinumPolicy : IPointsPolicy
{
	public bool CanBeApplied(decimal totalAmount) => totalAmount >= 1500;
	public int IncreasePoints(Guid userId) => 40;
}