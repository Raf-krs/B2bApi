using B2bApi.ExchangeRates.Repositories;
using B2bApi.ExchangeRates.Services;
using Quartz;

namespace B2bApi.ExchangeRates.Jobs;

public class ExchangeRatesJob : IJob
{
	private readonly IExchangeRatesNbpApi _nbpApi;
	private readonly IExchangeRatesRepository _exchangeRatesRepository; 
	private readonly ILogger<ExchangeRatesJob> _logger;
	private const string JobName = "ExchangeRatesJob";
	
	public ExchangeRatesJob(IExchangeRatesNbpApi nbpApi, IExchangeRatesRepository exchangeRatesRepository, 
							ILogger<ExchangeRatesJob> logger)
	{
		_nbpApi = nbpApi;
		_exchangeRatesRepository = exchangeRatesRepository;
		_logger = logger;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		// TODO -> check currency code when is saved in database
		_logger.LogInformation("Execute job: {JobName}", JobName);
		
		var response = await _nbpApi.GetExchangeRatesAsync();
		if (response.IsFailed)
		{
			_logger.LogError("Error while processing job: {JobName} with errors: {Errors}", 
							JobName, string.Join(";" + Environment.NewLine, response.Errors));
			return;
		}
		await _exchangeRatesRepository.AddAsync(response.Value.ToEntity());
		
		_logger.LogInformation("Finished job: {JobName}", JobName);
	}
}