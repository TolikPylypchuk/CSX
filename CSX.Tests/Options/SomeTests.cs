using System;

using Xunit;

namespace CSX.Options
{
	public class SomeTests
	{
		[Fact(DisplayName = "GetOrElse returns the value")]
		public void TestGetOrElse()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.Equal(expected, option.GetOrElse(0));
		}

		[Fact(DisplayName = "GetOrThrow returns the value")]
		public void TestGetOrThrow()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.Equal(expected, option.GetOrThrow());
		}

		[Fact(DisplayName = "Map maps the value")]
		public void TestMap()
		{
			var option = 1.ToOption();
			Assert.True(option.Map(value => value + 1) is Some<int> some && some.Value == 2);
		}

		[Fact(DisplayName = "Map throws an exception for null")]
		public void TestMapNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.Map<int>(null));
		}
	}
}
