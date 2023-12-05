using System.Reflection;
using B2bApi.Shared.Clock;
using B2bApi.Shared.Exceptions;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;

namespace B2bApi.Shared.Middlewares;

public static class Extension
{
	public static IHostBuilder AddLogger(this IHostBuilder host)
	{
		host.UseSerilog((ctx, config) =>
		{
			config.ReadFrom.Configuration(ctx.Configuration);
		});

		return host;
	}
	
	public static IServiceCollection AddMiddlewares(this IServiceCollection services)
	{
		services.AddSingleton<GlobalErrorHandler>();
		services.AddSingleton<IClock, Clock.Clock>();
		
		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		});

		services.Scan(selector => selector.FromAssemblies(Assembly.GetExecutingAssembly())
							.AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
							.AsImplementedInterfaces());
		
		return services;
	}
	
	public static IServiceCollection AddAuthSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "B2bApi", Version = "v1" });
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = """
							JWT Authorization header using the Bearer scheme.
							Enter 'Bearer' [space] and then your token in the text input below.
							Example: 'Bearer 12345abcdef'
							""",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});
			
			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Scheme = "oauth2",
						Name = "Bearer",
						In = ParameterLocation.Header
					},
					new List<string>()
				}
			});
			
			var docs = Path.Combine(AppContext.BaseDirectory, "B2bApi.xml");
			c.IncludeXmlComments(docs);
		});

		return services;
	}
	
	public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
	{
		app.UseMiddleware<GlobalErrorHandler>();
		
		app.UseReDoc(options =>
		{
			options.DocumentTitle = "API Demo Documentation";
			options.SpecUrl = "/swagger/v1/swagger.json";
		});
		
		return app;
	}
}