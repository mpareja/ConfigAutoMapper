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

		[Test]
		public void converts_arrays()
		{
			AssertValueConversion ("1,2,3,4", new[] { 1, 2, 3, 4 });
			AssertValueConversion ("1,2,3,4", new[] { "1", "2", "3", "4" });
			AssertValueConversion ("true,false,1,0", new[] { true, false, true, false });
		}

		private void AssertValueConversion<T>(string setting, T expected)
		{
			var converter = new ConfigValueConverter();
			var actual = (T) converter.Convert(setting, typeof(T));
			Assert.AreEqual (expected, actual);
		}
	}
}