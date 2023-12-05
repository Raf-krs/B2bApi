using B2bApi.Shared.Abstractions.Data;
using B2bApi.Shared.Db.Data;
using B2bApi.Shared.Options;
using B2bApi.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace B2bApi.Shared.Db;

public static class Extension
{
	public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration config)
	{
		var options = services.GetOptions<DbOptions>(config, DbOptions.SectionName);
		var connectionString = options.ConnectionString;
		
		services.AddDbContext<AppDbContext>(optionsAction =>
		{
			optionsAction.UseSqlServer(connectionString)
				.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, 
						LogLevel.Information);
		});
		services.AddScoped<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
		
		services.Scan(s => s.FromAssemblyOf<IRepository>()
							.AddClasses(c => c.AssignableTo<IRepository>())
							.AsImplementedInterfaces()
							.WithScopedLifetime());
		
		services.AddHostedService<DbInitializer>();
		
		return services;
	}
}