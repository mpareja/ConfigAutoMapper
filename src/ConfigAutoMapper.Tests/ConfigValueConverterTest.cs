using NUnit.Framework;

namespace ConfigAutoMapper.Tests
{
	[TestFixture]
	public class ConfigValueConverterTest
	{
		[Test]
		public void converts_int()
		{
			AssertValueConversion("1", 1);
		}

		[Test]
		public void converts_string()
		{
			AssertValueConversion("my string", "my string");
		}

		[Test]
		public void converts_boolean()
		{
			AssertValueConversion("true", true);
			AssertValueConversion("tRue", true);
			AssertValueConversion("false", false);
			AssertValueConversion("fAlse", false);
			AssertValueConversion("1", true);
			AssertValueConversion("0", false);
		}

		private void AssertValueConversion<T>(string setting, T expected)
		{
			var converter = new ConfigValueConverter();
			var actual = (T) converter.Convert(setting, typeof(T));
			Assert.AreEqual (expected, actual);
		}
	}
}