namespace B2bApi.Shared.Options;

public static class Options
{
	public static T GetOptions<T>(this IServiceCollection services, IConfiguration config, string sectionName)
		where T : class, new()
	{
		var section = config.GetRequiredSection(sectionName);
		services.Configure<T>(section);
		var options = new T();
		section.Bind(options);

		return options;
	}
}