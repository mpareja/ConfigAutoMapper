using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConfigAutoMapper
{
	public class ConfigMapper
	{
		private readonly ConfigValueConverter _converter = new ConfigValueConverter();
		private readonly IConfigSource _configSource;

		public ConfigMapper() : this(new AppSettingsConfigSource()) {}

		public ConfigMapper(IConfigSource configSource)
		{
			_configSource = configSource;
		}

		public T Load<T>() where T : new()
		{
			var type = typeof(T);
			var instance = new T();
			var typename = GetTypeName(type);

			foreach (var prop in GetReadWriteInstanceProps(type))
			{
				var rawvalue = GetConfigValue(typename, prop);
				if (rawvalue != null)
				{
					UpdateSetting(instance, prop, rawvalue);
				}
			}
			return instance;
		}

		private void UpdateSetting<T>(T instance, PropertyInfo prop, string rawvalue)
		{
			var value = _converter.Convert(rawvalue, prop.PropertyType);
			prop.SetValue(instance, value, null);
		}

		private string GetConfigValue(string typename, PropertyInfo prop)
		{
			string rawvalue;
			var key = string.Format("{0}.{1}", typename, prop.Name);
			rawvalue = _configSource.Get(key);
			return rawvalue;
		}

		private IEnumerable<PropertyInfo> GetReadWriteInstanceProps(Type type)
		{
			var allNonStatic = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			return type
				.GetProperties(allNonStatic)
				.Where(p => p.CanRead && p.CanWrite);
		}

		private static string GetTypeName(Type type)
		{
			// strip out Config suffix
			var original = type.Name;
			if (original.EndsWith("Config"))
				return original.Substring(0, original.Length - 6);
			return original;
		}
	}
}