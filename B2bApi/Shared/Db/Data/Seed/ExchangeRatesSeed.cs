using B2bApi.ExchangeRates.Services;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.Shared.Db.Data.Seed;

public class ExchangeRatesSeed
{
	private readonly AppDbContext _dbContext;
	private readonly IExchangeRatesNbpApi _nbpApi;
	
	public ExchangeRatesSeed(AppDbContext dbContext, IExchangeRatesNbpApi nbpApi)
	{
		_dbContext = dbContext;
		_nbpApi = nbpApi;
	}

	public async Task Seed(CancellationToken cancellationToken)
	{
		var seeded = await _dbContext.ExchangeRates.AnyAsync(cancellationToken);
		if(seeded is false)
		{
			var response = await _nbpApi.GetExchangeRatesAsync();
			if (response.IsFailed)
			{
				return;
			}
			await _dbContext.ExchangeRates.AddAsync(response.Value.ToEntity(), cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}