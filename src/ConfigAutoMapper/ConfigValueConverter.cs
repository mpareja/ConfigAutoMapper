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
		private static object ConvertArray<T>(string arg, Func<string, T> conversion)
		{
			if (string.IsNullOrEmpty(arg))
				return new T[0];

			var pieces = arg.Split(',');
			var rg = new T[pieces.Length];
			for (var i = 0; i < pieces.Length; i++)
			{
				rg[i] = conversion(pieces[i]);
			}
			return rg;
		}

		private static readonly Dictionary<Type, Func<string, object>> _converters =
			new Dictionary<Type, Func<string, object>> {
				{ typeof(int), v => System.Convert.ToInt32(v) },
				{ typeof(string), v => v },
				{ typeof(bool), ConvertBool },
				{ typeof(int[]), array => ConvertArray(array, item => System.Convert.ToInt32(item)) },
				{ typeof(string[]), array => ConvertArray(array, item => item) },
				{ typeof(bool[]), array => ConvertArray(array, item => (bool) ConvertBool(item)) }
			};
	}
}