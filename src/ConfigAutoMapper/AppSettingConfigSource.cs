using System.Configuration;

namespace ConfigAutoMapper
{
	public class AppSettingConfigSource : IConfigSource
	{
		public string Get(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}