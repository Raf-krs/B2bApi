using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using B2bApi.Shared.Db.Data;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace B2bApiTests.Utils.Integration;

internal static class TestDatabase
{
	public static AppDbContext CreateDbContext(string connectionString) 
		=> new(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options);
	
	public static ISqlConnectionFactory CreateDbConnection(string connectionString) 
		=> new SqlConnectionFactory(connectionString);

	public static async Task<MsSqlContainer> InitMsSqlAsync()
	{
		var container = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
			.WithPassword("Strong_password_123!")
			.Build();
		await container.StartAsync();
		
		return container;
	}
}