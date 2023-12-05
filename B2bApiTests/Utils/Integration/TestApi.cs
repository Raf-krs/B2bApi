using System.Net.Http.Headers;
using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Clock;
using B2bApi.Shared.Db;
using B2bApi.Shared.Security.Options;
using B2bApi.Shared.Security.Services;
using B2bApi.Users.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Testcontainers.MsSql;
using Xunit;

namespace B2bApiTests.Utils.Integration;

public abstract class TestApi : IAsyncLifetime
{
	private TestFactory _factory = null!;
	private MsSqlContainer MsSqlContainer { get; set; }
	protected AppDbContext TestDbContext { get; private set; }
	protected ISqlConnectionFactory ConnectionFactory { get; private set; }
	protected HttpClient Client => _factory.Client;
	protected virtual Action<IServiceCollection> ConfigureServices => null!;
	
	public virtual async Task InitializeAsync()
	{
		MsSqlContainer = await TestDatabase.InitMsSqlAsync();
		var connectionString = MsSqlContainer.GetConnectionString();
		TestDbContext = TestDatabase.CreateDbContext(connectionString);
		ConnectionFactory = TestDatabase.CreateDbConnection(connectionString);
		_factory = new TestFactory(ConfigureServices, new Dictionary<string, string>
		{
			{ "Db:ConnectionString", connectionString }
		});
	}

	public virtual async Task DisposeAsync()
	{
		await _factory.DisposeAsync();
		await MsSqlContainer.DisposeAsync();
	}
	
	protected void Authorize(User user)
	{
		var clock = new Clock();
		var authOptions = OptionsHelper.GetOptions<AuthOptions>(AuthOptions.SectionName);
		var authenticator = new Authenticator(Options.Create(authOptions), clock);
		var jwtToken = authenticator.CreateToken(user.Id, user.Email, user.Role, user.Currency.Code);
		Client.DefaultRequestHeaders.Clear();
		Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.AccessToken);
	}
	
	protected async Task AddEntityAsync<T>(T entity) where T : class, IEntity
	{
		await TestDbContext.AddAsync(entity);
		await TestDbContext.SaveChangesAsync();
	}
	
	protected async Task AddEntitiesAsync<T>(IEnumerable<T> entities) where T : class, IEntity
	{
		await TestDbContext.AddRangeAsync(entities);
		await TestDbContext.SaveChangesAsync();
	}

	protected async Task<T> DeserializeObject<T>(HttpResponseMessage response)
	{
		var responseContent = await response.Content.ReadAsStringAsync();

		return JsonConvert.DeserializeObject<T>(responseContent)!;
	}
}