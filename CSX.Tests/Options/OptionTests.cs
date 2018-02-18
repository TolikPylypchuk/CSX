using System.Diagnostics.CodeAnalysis;

using Xunit;

using static CSX.Options.Option;

namespace CSX.Options
{
	[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
	public class OptionTests
	{
		[Fact(DisplayName = "From returns Some which contains the value")]
		public void TestFrom()
		{
			const int value = 1;
			var option = From(value);
			Assert.True(option is Some<int> some && value == some.Value);
		}

		[Fact(DisplayName = "From returns None for null")]
		public void TestFromNull()
		{
			var option = From<string>(null);
			Assert.IsType<None<string>>(option);
		}

		[Fact(DisplayName = "From returns Some which contains the nullable value")]
		public void TestFromNullable()
		{
			int? value = 1;
			var option = From(value);
			Assert.True(option is Some<int> some && value.Value == some.Value);
		}

		[Fact(DisplayName = "From returns None when a nullable is null")]
		public void TestFromNullableNull()
		{
			var option = From<int>(null);
			Assert.IsType<None<int>>(option);
		}

		[Fact(DisplayName = "Empty returns None")]
		public void TestEmpty()
		{
			var option = Empty<int>();
			Assert.IsType<None<int>>(option);
		}

		[Fact(DisplayName = "ToOption returns Some which contains the value")]
		public void TestToOption()
		{
			const int value = 1;
			var option = value.ToOption();
			Assert.True(option is Some<int> some && value == some.Value);
		}

		[Fact(DisplayName = "ToOption returns None for null")]
		public void TestToOptionNull()
		{
			const string value = null;
			var option = value.ToOption();
			Assert.IsType<None<string>>(option);
		}

		[Fact(DisplayName = "ToOption returns Some which contains the nullable value")]
		public void TestToOptionNullable()
		{
			int? value = 1;
			var option = value.ToOption();
			Assert.True(option is Some<int> some && value.Value == some.Value);
		}

		[Fact(DisplayName = "ToOption returns None when a nullable is null")]
		public void TestToOptionNullableNull()
		{
			int? value = null;
			var option = value.ToOption();
			Assert.IsType<None<int>>(option);
		}
	}
}
