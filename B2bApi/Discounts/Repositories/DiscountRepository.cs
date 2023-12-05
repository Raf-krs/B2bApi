using B2bApi.Discounts.Dtos;
using B2bApi.Discounts.Entities;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using Dapper;

namespace B2bApi.Discounts.Repositories;

public class DiscountRepository : IDiscountRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public DiscountRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}
	
	public async Task<IEnumerable<Discount>> GetAllAsync(CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT * FROM discounts";
		var command = new CommandDefinition(sql, cancellationToken);
		var result = await connection.QueryAsync<DiscountDto>(command);

		return result.Select(x => x.ToEntity());
	}
	
	public async Task<Discount> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT * FROM discounts WHERE id = @Id";
		var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
		var result = await connection.QueryFirstOrDefaultAsync<DiscountDto>(command);

		return result?.ToEntity();
	}

	public async Task AddAsync(Discount discount, CancellationToken cancellationToken)
	{
		await _dbContext.Discounts.AddAsync(discount, cancellationToken);
	}
	
	public Task DeleteAsync(Discount discount, CancellationToken cancellationToken)
	{
		_dbContext.Remove(discount);
		return Task.CompletedTask;
	}

	public async Task<bool> ExistsDiscountInDateRangeAsync(Guid productId, DateOnly start, 
															CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = """
							SELECT IIF(EXISTS (
							    SELECT 1 FROM discounts
							    WHERE product_id = @Product AND start_date <= @StartDate AND end_date >= @StartDate
							), CAST(1 AS BIT), CAST(0 AS BIT))
							""";
		var command = new CommandDefinition(sql, new { Product = productId, StartDate = start }, 
											cancellationToken: cancellationToken);
		var result = await connection.ExecuteScalarAsync<bool>(command);

		return result;
	}
}