using B2bApi.Points.Policies;

namespace B2bApi.Points;

public static class Extension
{
	public static IServiceCollection AddPolicies(this IServiceCollection services)
	{
		services.Scan(s => s.FromAssemblyOf<IPointsPolicy>()
							.AddClasses(c => c.AssignableTo<IPointsPolicy>())
							.AsImplementedInterfaces()
							.WithScopedLifetime());
		
		return services;
	}
}