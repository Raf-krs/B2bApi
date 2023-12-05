using B2bApi.Carts.Dtos;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.Carts.Repositories.Cart;

public class CartRepository : ICartRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public CartRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public async Task<CartItemsResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{ 
		using var connection = _sqlConnectionFactory.Create();
		const string sql = """
							SELECT c.id, c.name, c.comment, c.currency, c.order_id as OrderId, 
							       c.created_at as CreatedAt, c.user_id as UserId, 
							       ci.id as CartId, p.id as ProductId, ci.price, ci.quantity, p.name as ProductName
							FROM carts as c
							LEFT JOIN cart_items as ci ON ci.cart_id = c.id
							LEFT JOIN products as p ON p.id = ci.product_id
							WHERE c.id = @Id
							""";
		var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
		var result = await connection.QueryFirstOrDefaultAsync<CartItemsDto>(command);

		return result.ToResponse();
	}

	public async Task<IEnumerable<CartItemsResponse>> GetActiveCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = """
							SELECT c.id, c.name, c.comment, c.currency, c.order_id as OrderId, 
							       c.created_at as CreatedAt, c.user_id as UserId, 
							       ci.id as CartId, p.id as ProductId, ci.price, ci.quantity, p.name as ProductName
							FROM carts as c
							LEFT JOIN cart_items as ci ON ci.cart_id = c.id
							LEFT JOIN products as p ON p.id = ci.product_id
							WHERE user_id = @UserId AND order_id IS NULL
							""";
		var command = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: cancellationToken);
		var result = await connection.QueryAsync<CartItemsDto>(command);

		return result.Select(x => x.ToResponse());
	}

	public async Task AddAsync(Entities.Cart cart, CancellationToken cancellationToken)
	{
		await _dbContext.Carts.AddAsync(cart, cancellationToken);
	}

	public Task UpdateAsync(Entities.Cart cart, CancellationToken cancellationToken)
	{
		_dbContext.Carts.Update(cart);
		return Task.CompletedTask;
	}

	public Task<Entities.Cart> GetByEfCoreIdAsync(Guid id, CancellationToken cancellationToken) 
		=> _dbContext
			.Carts
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

	public async Task DeleteAsync(Entities.Cart cart, CancellationToken cancellationToken) 
		=> _dbContext.Carts.Remove(cart);
}