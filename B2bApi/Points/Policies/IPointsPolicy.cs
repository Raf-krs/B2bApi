namespace B2bApi.Points.Policies;

public interface IPointsPolicy
{
	bool CanBeApplied(decimal totalAmount);
	int IncreasePoints(Guid userId);
}