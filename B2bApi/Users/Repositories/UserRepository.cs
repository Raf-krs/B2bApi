using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using B2bApi.Users.Dtos;
using B2bApi.Users.Entities;
using Dapper;

namespace B2bApi.Users.Repositories;

public class UserRepository : IUserRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;

	public UserRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}
	
	public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = "SELECT * FROM Users WHERE Id = @Id";
		var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
		var result = await connection.QueryFirstOrDefaultAsync<UserDto>(command);
		
		return result?.ToEntity();
	}
	
	public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
	{
		using var connection = _sqlConnectionFactory.Create();
		// INFO -> This is just an example of how to use Dapper, in real life I would use view for this query
		const string sql = "SELECT * FROM Users WHERE email = @Email";
		var command = new CommandDefinition(sql, new { Email = email }, cancellationToken: cancellationToken);
		var result = await connection.QueryFirstOrDefaultAsync<UserDto>(command);
		
		return result?.ToEntity();
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken)
	{
		await _dbContext.Users.AddAsync(user, cancellationToken);
	}

	public Task UpdateAsync(User user, CancellationToken cancellationToken)
	{
		_dbContext.Users.Update(user);
		return Task.CompletedTask;
	}
}