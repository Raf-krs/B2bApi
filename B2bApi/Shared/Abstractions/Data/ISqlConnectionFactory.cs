using System.Data;

namespace B2bApi.Shared.Abstractions.Data;

public interface ISqlConnectionFactory
{
	IDbConnection Create();
}