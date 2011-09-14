using NUnit.Framework;

namespace ConfigAutoMapper.Tests
{
	[TestFixture]
	public class ConfigAutoMapperTests
	{
		[Test]
		public void can_load_configuration_class()
		{
			var mapper = GetConfigAutoMapper();
			StuffConfig config = mapper.Load<StuffConfig>();

			Assert.IsNotNull(config);
		}

		[Test]
		public void can_load_string_property()
		{
			var source = new DictionaryConfigSource(){
				{ "Stuff.MySetting", "MySettingValue" }
			};
			var config = LoadConfig<StuffConfig>(source);

			Assert.AreEqual("MySettingValue", config.MySetting);
		}

		[Test]
		public void can_load_integer_property()
		{
			var source = new DictionaryConfigSource(){
		        { "Stuff.MyIntSetting", "12" }
		    };
			var config = LoadConfig<StuffConfig>(source);

			Assert.AreEqual(12, config.MyIntSetting);
		}

		[Test]
		public void dont_load_properties_without_settings()
		{
			var source = new DictionaryConfigSource();
			var config = LoadConfig<StuffConfig>(source);

			Assert.AreEqual("DefaultValue", config.SettingWithDefault);
		}

		[Test]
		public void can_override_property_with_default_value()
		{
			var source = new DictionaryConfigSource{
		        { "Stuff.SettingWithDefault", "NonDefault" }
			};
			var config = LoadConfig<StuffConfig>(source);

			Assert.AreEqual("NonDefault", config.SettingWithDefault);
		}

		[Test]
		public void can_populate_protected_properties()
		{
			var source = new DictionaryConfigSource(){
				{ "Derived.ProtectedSetting", "ProtectedSettingValue" }
			};
			var config = LoadConfig<DerivedConfig>(source);

			Assert.AreEqual("ProtectedSettingValue", config.GetProtectedSetting());
		}

		[Test]
		public void dont_populate_properties_without_setters()
		{
			var source = new DictionaryConfigSource(){
				{ "Derived.DerivedSetting", "BOGUS" }
			};
			var config = LoadConfig<DerivedConfig>(source);
			Assert.AreNotEqual("BOGUS", config.DerivedSetting);
		}

		[Test]
		public void can_load_items_with_derived_settings()
		{
			var source = new DictionaryConfigSource(){
				{ "Derived.ProtectedSetting", "ProtectedSettingValue" }
			};
			var config = LoadConfig<DerivedConfig>(source);

			Assert.AreEqual("MY_ProtectedSettingValue", config.DerivedSetting);
		}

		private T LoadConfig<T>(IConfigSource source) where T : new()
		{
			var mapper = GetConfigAutoMapper(source);
			return mapper.Load<T>();
		}

		private ConfigAutoMapper GetConfigAutoMapper()
		{
			return new ConfigAutoMapper(new DictionaryConfigSource());
		}

		private ConfigAutoMapper GetConfigAutoMapper(IConfigSource configSource)
		{
			return new ConfigAutoMapper(configSource);
		}

		public class StuffConfig
		{
			public StuffConfig()
			{
				SettingWithDefault = "DefaultValue";
			}
			public string MySetting { get; set; }
			public int MyIntSetting { get; set; }
			public string SettingWithDefault { get; set; }
		}

		public class DerivedConfig
		{
			public string DerivedSetting { get { return "MY_" + ProtectedSetting; } }
			protected string ProtectedSetting { get; set; }

			public string GetProtectedSetting()
			{
				return ProtectedSetting;
			}
		}
	}
}