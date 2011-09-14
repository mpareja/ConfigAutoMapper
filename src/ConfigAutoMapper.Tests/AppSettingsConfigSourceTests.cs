using System;
using System.Configuration;

using NUnit.Framework;

namespace ConfigAutoMapper.Tests
{
	public class AppSettingsConfigSourceTests
	{
		[Test]
		public void can_query_for_top_level_configuration_setting()
		{
			var key = "Test1";
			var value = "test1";
			ConfigurationManager.AppSettings[key] = value;

			var source = new AppSettingsConfigSource();
			Assert.AreEqual(value, source.Get(key));
		}

		[Test]
		public void can_query_for_subsetting()
		{
			var key = "Test2.SubSetting";
			var value = "test2";
			ConfigurationManager.AppSettings[key] = value;

			var source = new AppSettingsConfigSource();
			Assert.AreEqual(value, source.Get(key));
		}

		[Test]
		public void returns_null_for_settings_that_dont_exist()
		{
			var source = new AppSettingsConfigSource();
			Assert.IsNull(source.Get("SettingThatDoesntExist"));
		}

		[Test]
		public void can_query_for_setting_in_App_Config_file()
		{
			var source = new AppSettingsConfigSource();
			Assert.AreEqual("AppConfigSettingValue", source.Get("AppConfigSetting"));
		}
	}
}