using System.Net.Http.Headers;
using B2bApi.ExchangeRates.Dtos;
using B2bApi.Shared.Clock;
using FluentResults;
using Newtonsoft.Json;
using PublicHoliday;

namespace B2bApi.ExchangeRates.Services;

public sealed class ExchangeRatesNbpApi : IExchangeRatesNbpApi
{
	private readonly HttpClient _httpClient;
	private readonly IClock _clock;
	private const string BaseAddress = "https://api.nbp.pl/api/exchangerates/tables/";

	public ExchangeRatesNbpApi(IHttpClientFactory factory, IClock clock) 
	{
		_httpClient = factory.CreateClient();
		_httpClient.DefaultRequestHeaders.Accept.Clear();
		_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		_httpClient.BaseAddress = new Uri(BaseAddress);
		_clock = clock;
	}
	
	public async Task<Result<ExchangeRateApiDto>> GetExchangeRatesAsync(ExchangeTables table = ExchangeTables.A)
	{
		if(IsTableValid((char)table) is false)
		{
			return Result.Fail($"Table {table} is not valid. Check [https://api.nbp.pl] for more details.");
		}
		var today = _clock.Current();
		var isHoliday = new PolandPublicHoliday().IsPublicHoliday(today);
		if(isHoliday)
		{
			return Result.Fail("Today is a public holiday.");
		}
		var endpoint = table + "?date=" + today.ToString("yyyy-MM-dd");
		var response = await _httpClient.GetAsync(endpoint);
		if (response.IsSuccessStatusCode is false)
		{
			return Result.Fail($"Error with status code: {response.StatusCode}");
		}
		var content = await response.Content.ReadAsStringAsync();
		ExchangeRateApiDto result;
		try
		{
			result = JsonConvert.DeserializeObject<ExchangeRateApiDto>(PrepareResponseToDeserialize(content));
		}
		catch(Exception exception)
		{
			return Result.Fail($"Error while deserializing response: {exception.Message}");
		}
		
		return Result.Ok(result);
	}
	
	private static bool IsTableValid(char table) => table is 'a' or 'b' or 'c';

	private static string PrepareResponseToDeserialize(string content) 
		=> content.Substring(1, content.Length - 2);
}

public enum ExchangeTables
{
	A = 'a',
	B = 'b',
	C = 'c'
}