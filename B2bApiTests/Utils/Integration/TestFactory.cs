using B2bApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2bApiTests.Utils.Integration;

internal sealed class TestFactory : WebApplicationFactory<IApiMarker>
{
	public HttpClient Client { get; }

	public TestFactory(Action<IServiceCollection> services = null, Dictionary<string, string> options = null)
	{
		Client = WithWebHostBuilder(builder =>
		{
			builder.UseEnvironment("tests");
			if(services is not null)
			{
				builder.ConfigureServices(services);
			}

			if(options is not null)
			{
				var configuration = new ConfigurationBuilder()
					.AddInMemoryCollection(options)
					.Build();
				builder.UseConfiguration(configuration);
			}
		}).CreateClient();
	}
}