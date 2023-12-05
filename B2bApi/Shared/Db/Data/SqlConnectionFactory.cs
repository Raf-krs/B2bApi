using System.Data;
using B2bApi.Shared.Abstractions.Data;
using Microsoft.Data.SqlClient;

namespace B2bApi.Shared.Db.Data;

public class SqlConnectionFactory : ISqlConnectionFactory
{
	private readonly string _connectionString;
	
	public SqlConnectionFactory(string connectionString)
	{
		_connectionString = connectionString;
	}

	public IDbConnection Create()
	{
		var connection = new SqlConnection(_connectionString);
		connection.Open();

		return connection;
	}
}