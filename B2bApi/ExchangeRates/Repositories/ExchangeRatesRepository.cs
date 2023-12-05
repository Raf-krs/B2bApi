using B2bApi.ExchangeRates.Dtos;
using B2bApi.ExchangeRates.Entities;
using B2bApi.ExchangeRates.Exceptions;
using B2bApi.ExchangeRates.Mapper;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.ExchangeRates.Repositories;

public class ExchangeRatesRepository : IExchangeRatesRepository
{
	private readonly AppDbContext _dbContext;
	private readonly ISqlConnectionFactory _sqlConnectionFactory;
	
	public ExchangeRatesRepository(AppDbContext dbContext, ISqlConnectionFactory sqlConnectionFactory)
	{
		_dbContext = dbContext;
		_sqlConnectionFactory = sqlConnectionFactory;
	}

	public async Task AddAsync(ExchangeRate exchangeRate, CancellationToken cancellationToken = default)
	{
		if(ExchangeRateExistsAsync(exchangeRate.No, cancellationToken).Result)
		{
			throw new ExchangeRateAlreadyExistsException(exchangeRate.No);
		}
		await _dbContext.ExchangeRates.AddAsync(exchangeRate, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<IEnumerable<ExchangeRatesTableResponse>> GetAsync(CancellationToken cancellationToken = default)
	{
		var result = await _dbContext.Database
			.SqlQuery<ExchangeRatesTableResponse>($"SELECT no, date FROM exchange_rates")
			.ToListAsync(cancellationToken);

		return result;
	}

	public async Task<ExchangeRatesResponse> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
	{
		FormattableString query = $"SELECT * FROM v_rates WHERE code = {code} ORDER BY date DESC";
		var result = await _dbContext.Database
			.SqlQuery<RatesViewDto>(query)
			.ToListAsync(cancellationToken);
		
		return ExchangeRatesMapper.ToResponse(result);
	}
	
	public async Task<RatesResponse> GetLatestRateByCodeAsync(string code, CancellationToken cancellationToken = default)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = """
							SELECT TOP 1 r.name, r.code, r.mid FROM rates AS r
							LEFT JOIN dbo.exchange_rates er on er.no = r.exchange_rate_no
							WHERE r.code = @Code
							ORDER BY er.date DESC
							""";
		var command = new CommandDefinition(sql, cancellationToken);
		var result = await connection.QueryFirstOrDefaultAsync<RatesResponse>(command);

		return result;
	}
	
	private async Task<bool> ExchangeRateExistsAsync(string no, CancellationToken cancellationToken = default)
	{
		using var connection = _sqlConnectionFactory.Create();
		const string sql = """
							SELECT IIF(EXISTS (
							    SELECT 1 FROM exchange_rates WHERE no = @No
							), CAST(1 AS BIT), CAST(0 AS BIT))
							""";
		var command = new CommandDefinition(sql, new { No = no }, cancellationToken: cancellationToken);
		
		return await connection.ExecuteScalarAsync<bool>(command);
	}
}