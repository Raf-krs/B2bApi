using B2bApi.ExchangeRates.Services;
using B2bApi.Shared.Clock;
using NSubstitute;
using Shouldly;
using Xunit;

namespace B2bApiTests.ExchangeRates.Services;

public class ExchangeRatesNbpApiTests
{
	private readonly IExchangeRatesNbpApi _exchangeRatesNbpApi;
	private readonly IClock _clock;
	
	public ExchangeRatesNbpApiTests()
	{
		_clock = Substitute.For<IClock>();
		var clientFactory = Substitute.For<IHttpClientFactory>();
		clientFactory.CreateClient().Returns(new HttpClient());
		_exchangeRatesNbpApi = new ExchangeRatesNbpApi(clientFactory, _clock);
	}
	
	[Fact]
	public async Task GetExchangeRatesAsync_ShouldFail_WhenTableIsNotValid()
	{
		// Arrange
		_clock.Current().Returns(new DateTime(2023, 12, 5));
		const ExchangeTables invalidTable = (ExchangeTables) 'd';
		
		// Act
		var result = await _exchangeRatesNbpApi.GetExchangeRatesAsync(invalidTable);
		
		// Assert
		result.IsFailed.ShouldBeTrue();
	}
	
	[Fact]
	public async Task GetExchangeRatesAsync_ShouldPass_WhenTableIsA()
	{
		// Arrange
		_clock.Current().Returns(new DateTime(2023, 12, 5));
		
		// Act
		var result = await _exchangeRatesNbpApi.GetExchangeRatesAsync();
		
		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldNotBeNull();
		result.Value.Rates.ShouldNotBeNull();
		result.Value.Rates.Count.ShouldBeGreaterThan(0);
	}
}