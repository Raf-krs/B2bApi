using B2bApi.ExchangeRates.Jobs;
using B2bApi.ExchangeRates.Options;
using B2bApi.ExchangeRates.Services;
using B2bApi.Shared.Options;
using Quartz;

namespace B2bApi.ExchangeRates;

public static class Extension
{
	public static IServiceCollection AddExchangeRates(this IServiceCollection services, IConfiguration config)
	{
		services.AddSingleton<IExchangeRatesNbpApi, ExchangeRatesNbpApi>();
		
		var options = services.GetOptions<SchedulerOptions>(config, SchedulerOptions.SectionName);
		if(options.Enabled)
		{
			services.AddExchangeRatesJob(options.Cron);	
		}
		
		return services;
	}
	
	private static IServiceCollection AddExchangeRatesJob(this IServiceCollection services, string cron)
	{
		services.AddQuartz(q =>
		{
			var exchangeRatesKey = new JobKey("exchangeRatesJob");
			q.AddJob<ExchangeRatesJob>(config => config.WithIdentity(exchangeRatesKey));
			q.AddTrigger(config => config
							.ForJob(exchangeRatesKey)
							.WithIdentity("exchangeRates-trigger")
							.WithCronSchedule(cron));
		});
		services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
		
		return services;
	}
}