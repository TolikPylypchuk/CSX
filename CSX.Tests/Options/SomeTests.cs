using System;
using System.Diagnostics.CodeAnalysis;

using Xunit;

using CSX.Exceptions;
using CSX.Results;

namespace CSX.Options
{
	[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
	[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
	public class SomeTests
	{
		[Fact(DisplayName = "GetOrElse returns the value")]
		public void TestGetOrElse()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.Equal(expected, option.GetOrElse(0));
		}

		[Fact(DisplayName = "GetOrElse returns the provided value")]
		public void TestGetOrElseFunc()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.Equal(expected, option.GetOrElse(() => 0));
		}

		[Fact(DisplayName = "GetOrThrow returns the value")]
		public void TestGetOrThrow()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.Equal(expected, option.GetOrThrow(() => new Exception()));
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

		[Fact(DisplayName = "Map throws an exception when mapper returns null")]
		public void TestMapMapperNull()
		{
			var option = 1.ToOption();
			Assert.Throws<UnacceptableNullException>(() => option.Map<string>(value => null));
		}

		[Fact(DisplayName = "Bind maps the value")]
		public void TestBind()
		{
			var option = 1.ToOption();
			Assert.True(
				option.Bind(value =>
					Option.From(value + 1)) is Some<int> some && some.Value == 2);
		}

		[Fact(DisplayName = "Bind throws an exception for null")]
		public void TestBindNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.Bind<int>(null));
		}

		[Fact(DisplayName = "Bind throws an exception when mapper returns null")]
		public void TestBindMapperNull()
		{
			var option = 1.ToOption();
			Assert.Throws<UnacceptableNullException>(() => option.Bind<string>(value => null));
		}

		[Fact(DisplayName = "DoIfSome executes the action")]
		public void TestDoIfSome()
		{
			int counter = 0;
			var option = 1.ToOption();

			option.DoIfSome(value => counter++);

			Assert.Equal(1, counter);
		}

		[Fact(DisplayName = "DoIfNone does nothing")]
		public void TestDoIfNone()
		{
			int counter = 0;
			var option = 1.ToOption();

			option.DoIfNone(() => counter++);

			Assert.Equal(0, counter);
		}

		[Fact(DisplayName = "DoIfSome throws an exception for null")]
		public void TestDoIfSomeNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.DoIfSome(null));
		}

		[Fact(DisplayName = "DoIfNone throws an exception for null")]
		public void TestDoIfNoneNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.DoIfNone(null));
		}

		[Fact(DisplayName = "ToResult returns a successful result")]
		public void TestToResult()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.True(
				option.ToResult(new Exception()) is Success<int, Exception> result &&
				result.Value == expected);
		}

		[Fact(DisplayName = "ToResult(string) returns a successful result")]
		public void TestToResultString()
		{
			const int expected = 1;
			var option = expected.ToOption();
			Assert.True(
				option.ToResult(String.Empty) is Success<int, string> result &&
				result.Value == expected);
		}

		[Fact(DisplayName = "ToResult throws an exception for null")]
		public void TestToResultNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.ToResult(null));
		}

		[Fact(DisplayName = "ToResult(string) throws an exception for null")]
		public void TestToResultStringNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.ToResult(null));
		}

		[Fact(DisplayName = "GetEnumerator returns an enumerator for the value")]
		public void TestGetEnumerator()
		{
			const int expected = 1;
			int counter = 0;

			foreach (int value in expected.ToOption())
			{
				Assert.Equal(expected, value);
				counter++;
			}

			Assert.Equal(1, counter);
		}

		[Fact(DisplayName = "Equals(object) returns true for same values")]
		public void TestEqualsObjectSameValues()
		{
			const int value = 1;
			var option1 = value.ToOption();
			object option2 = value.ToOption();
			Assert.True(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(object) returns false for different values")]
		public void TestEqualsObjectDifferentValues()
		{
			const int value = 1;
			var option1 = value.ToOption();
			object option2 = 2.ToOption();
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(object) returns false for different types")]
		public void TestEqualsObjectDifferentType()
		{
			const int value = 1;
			var option1 = value.ToOption();
			object option2 = 2;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(object) returns false for null")]
		public void TestEqualsObjectNull()
		{
			const int value = 1;
			var option1 = value.ToOption();
			object option2 = null;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns true for same values")]
		public void TestEqualsOptionSameValues()
		{
			const int value = 1;
			var option1 = value.ToOption();
			var option2 = value.ToOption();
			Assert.True(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns false for different values")]
		public void TestEqualsOptionDifferentValues()
		{
			const int value = 1;
			var option1 = value.ToOption();
			var option2 = 2.ToOption();
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns false for different types")]
		public void TestEqualsOptionDifferentType()
		{
			const int value = 1;
			var option1 = value.ToOption();
			var option2 = Option.Empty<int>();
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns false for null")]
		public void TestEqualsOptionNull()
		{
			const int value = 1;
			var option1 = value.ToOption();
			Option<int> option2 = null;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Some) returns true for same values")]
		public void TestEqualsSomeSameValues()
		{
			const int value = 1;
			var option1 = value.ToOption() as Some<int>;
			var option2 = value.ToOption() as Some<int>;
			Assert.True(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Some) returns false for different values")]
		public void TestEqualsSomeDifferentValues()
		{
			const int value = 1;
			var option1 = value.ToOption() as Some<int>;
			var option2 = 2.ToOption() as Some<int>;
			Assert.False(option1.Equals(option2));
		}
		
		[Fact(DisplayName = "Equals(Some) returns false for null")]
		public void TestEqualsSomeNull()
		{
			const int value = 1;
			var option1 = value.ToOption() as Some<int>;
			Some<int> option2 = null;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "GetHashCode returns value's hash code")]
		public void TestGetHashCode()
		{
			const string value = "Hello world!";
			var option = value.ToOption();
			Assert.Equal(value.GetHashCode(), option.GetHashCode());
		}

		[Fact(DisplayName = "ToString returns Some[value]")]
		public void TestToString()
		{
			const int value = 1;
			var option = value.ToOption();
			Assert.Equal($"Some[{value}]", option.ToString());
		}
	}
}
