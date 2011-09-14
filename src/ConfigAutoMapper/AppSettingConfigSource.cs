using System.Configuration;

namespace ConfigAutoMapper
{
	public class AppSettingsConfigSource : IConfigSource
	{
		public string Get(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}