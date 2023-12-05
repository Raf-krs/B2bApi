using Microsoft.Extensions.Configuration;

namespace B2bApiTests.Utils.Integration;

public static class OptionsHelper
{
	private const string SettingsFileName = "appsettings.tests.json";
	
	public static TSettings GetOptions<TSettings>(string section, string? settingsFileName = null) 
		where TSettings : class, new()
	{
		settingsFileName ??= SettingsFileName;
		var configuration = new TSettings();
		
		GetConfigurationRoot(settingsFileName)
			.GetSection(section)
			.Bind(configuration);

		return configuration;
	}
	
	private static IConfigurationRoot GetConfigurationRoot(string settingsFileName)
		=> new ConfigurationBuilder()
			.AddJsonFile(settingsFileName, optional: false)
			.AddEnvironmentVariables()
			.Build();
}