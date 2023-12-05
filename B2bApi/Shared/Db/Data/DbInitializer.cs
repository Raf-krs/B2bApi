using B2bApi.ExchangeRates.Services;
using B2bApi.Shared.Db.Data.Seed;
using B2bApi.Shared.Security.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.Shared.Db.Data;

public class DbInitializer : IHostedService
{
	private readonly IServiceProvider _serviceProvider;
	
	public DbInitializer(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
		
		// INFO -> Add seed data here or in migrations
		await AddAdmin(dbContext, scope.ServiceProvider, cancellationToken);
		await AddExchangeRates(dbContext, scope.ServiceProvider, cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

	private async Task AddAdmin(AppDbContext dbContext, IServiceProvider serviceProvider, 
								CancellationToken cancellationToken)
	{
		var passwordManager = serviceProvider.GetRequiredService<IPasswordManager>();
		var adminSeed = new AdminSeed(dbContext, passwordManager);
		await adminSeed.Seed(cancellationToken);
	}
	
	private async Task AddExchangeRates(AppDbContext dbContext, IServiceProvider serviceProvider, 
										CancellationToken cancellationToken)
	{
		var exchangeRatesNbpApi = serviceProvider.GetRequiredService<IExchangeRatesNbpApi>();
		var exchangeRatesSeed = new ExchangeRatesSeed(dbContext, exchangeRatesNbpApi);
		await exchangeRatesSeed.Seed(cancellationToken);
	}
}