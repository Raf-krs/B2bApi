using B2bApi.Shared.Abstractions.Data;
using Dapper;

namespace B2bApi.Points.Repositories;

public class PointsRepository : IPointsRepository
{
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public PointsRepository(ISqlConnectionFactory sqlConnectionFactory)
	{
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public Task<int> GetPointsAsync(Guid userId, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT amount FROM points WHERE user_id = @UserId";
		var command = new CommandDefinition(sql, new { UserId = userId }, 
											cancellationToken: cancellationToken);
		var result = connection.ExecuteScalarAsync<int>(command);

		return result;
	}

	public async Task IncreasePointsAsync(Guid userId, int points, CancellationToken cancellationToken)
	{
		if(await ExistsAsync(userId, cancellationToken))
		{
			await UpdatePointsAsync(userId, points, cancellationToken);
			return;
		}
		await InsertPointsAsync(userId, points, cancellationToken);
	}
	
	private async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT EXISTS(SELECT 1 FROM points WHERE user_id = @UserId)";
		var command = new CommandDefinition(sql, new { UserId = userId }, 
											cancellationToken: cancellationToken);
		var result = await connection.ExecuteScalarAsync<bool>(command);

		return result;
	}
	
	private async Task UpdatePointsAsync(Guid userId, int points, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "UPDATE points SET amount = amount + @Points WHERE user_id = @UserId";
		var command = new CommandDefinition(sql, new { UserId = userId, Points = points }, 
											cancellationToken: cancellationToken);
		await connection.ExecuteAsync(command);
	}
	
	private async Task InsertPointsAsync(Guid userId, int points, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "INSERT INTO points (user_id, amount) VALUES (@UserId, @Points)";
		var command = new CommandDefinition(sql, new { UserId = userId, Points = points }, 
											cancellationToken: cancellationToken);
		await connection.ExecuteAsync(command);
	}
}