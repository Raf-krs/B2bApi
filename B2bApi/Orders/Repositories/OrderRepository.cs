using B2bApi.Orders.Entities;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using Dapper;

namespace B2bApi.Orders.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public OrderRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public async Task<IEnumerable<Order>> GetByUserId(Guid id, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT * FROM Orders WHERE user_id = @Id";
		var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
		var order = await connection.QueryAsync<Order>(command);

		return order;
	}

	public async Task AddAsync(Order order, CancellationToken cancellationToken)
	{
		await _dbContext.Orders.AddAsync(order, cancellationToken);
	}
}