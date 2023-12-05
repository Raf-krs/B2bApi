using B2bApi.Carts.Dtos;
using B2bApi.Carts.Entities;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using Dapper;

namespace B2bApi.Carts.Repositories.Item;

public class CartItemRepository : ICartItemRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public CartItemRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public async Task<Items> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT * FROM cart_items WHERE Id = @Id";
		var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
		var result = await connection.QueryFirstOrDefaultAsync<ItemDto>(command);

		return result?.ToEntity();
	}

	public async Task AddAsync(Entities.Items items, CancellationToken cancellationToken)
	{
		await _dbContext.CartItems.AddAsync(items, cancellationToken);
	}

	public Task UpdateAsync(Entities.Items items, CancellationToken cancellationToken)
	{
		_dbContext.CartItems.Update(items);
		return Task.CompletedTask;
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "DELETE FROM cart_items WHERE Id = @Id";
		var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
		await connection.ExecuteAsync(command);
	}
}