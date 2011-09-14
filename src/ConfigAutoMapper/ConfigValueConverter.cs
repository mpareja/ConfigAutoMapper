using System;
using System.Collections.Generic;

namespace ConfigAutoMapper
{
	public class ConfigValueConverter
	{
		public object Convert(string s, Type type)
		{
			Func<string, object> conversion;
			if (_converters.TryGetValue(type, out conversion))
				return conversion(s);
			return null; 
		}

		private static object ConvertBool(string value)
		{
			switch(value)
			{
				case "1": return true;
				case "0": return false;
				default: return System.Convert.ToBoolean(value);
			}
		}
		private static readonly Dictionary<Type, Func<string, object>> _converters =
			new Dictionary<Type, Func<string, object>> {
				{ typeof(int), v => System.Convert.ToInt32(v) },
				{ typeof(string), v => v },
				{ typeof(bool), ConvertBool },
			};
	}
}