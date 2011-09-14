using NUnit.Framework;

namespace ConfigAutoMapper.Tests
{
	public class DictionaryConfigSourceTests
	{
		[Test]
		public void can_query_for_top_level_configuration_setting()
		{
			var key = "Test1";
			var value = "test1";
			var source = new DictionaryConfigSource{ { key, value } };

			Assert.AreEqual(value, source.Get(key));
		}

		[Test]
		public void can_query_for_subsetting()
		{
			var key = "Test2.SubSetting";
			var value = "test2";
			var source = new DictionaryConfigSource { { key, value } };

			Assert.AreEqual(value, source.Get(key));
		}
	}
}