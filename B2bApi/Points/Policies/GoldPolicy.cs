namespace B2bApi.Points.Policies;

public class GoldPolicy : IPointsPolicy
{
	public bool CanBeApplied(decimal totalAmount) => totalAmount >= 500;
	public int IncreasePoints(Guid userId) => 15;
}