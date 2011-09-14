using System.Collections.Generic;

namespace ConfigAutoMapper
{
	public class DictionaryConfigSource
		: Dictionary<string, string>
		  , IConfigSource
	{
		public string Get(string key)
		{
			return ContainsKey(key) ? this[key] : null;
		}
	}
}