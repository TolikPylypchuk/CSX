using System;
using System.Diagnostics.CodeAnalysis;

using Xunit;

using CSX.Collections;
using CSX.Exceptions;
using CSX.Results;

namespace CSX.Options
{
	[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
	[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
	public class NoneTests
	{
		[Fact(DisplayName = "GetOrElse returns the alternative")]
		public void TestGetOrElse()
		{
			const int expected = 1;
			var option = Option.Empty<int>();
			Assert.Equal(expected, option.GetOrElse(expected));
		}

		[Fact(DisplayName = "GetOrElse returns the provided alternative")]
		public void TestGetOrElseFunc()
		{
			const int expected = 1;
			var option = Option.Empty<int>();
			Assert.Equal(expected, option.GetOrElse(() => expected));
		}

		[Fact(DisplayName = "GetOrElse returns the alternative when it's null")]
		public void TestGetOrElseNull()
		{
			const string expected = null;
			var option = Option.Empty<string>();
			Assert.Equal(expected, option.GetOrElse(expected));
		}

		[Fact(DisplayName = "GetOrElse returns the provided alternative when it's null")]
		public void TestGetOrElseFuncNull()
		{
			const string expected = null;
			var option = Option.Empty<string>();
			Assert.Equal(expected, option.GetOrElse(() => expected));
		}

		[Fact(DisplayName = "GetOrThrow throws a provided exception")]
		public void TestGetOrThrow()
		{
			var option = Option.Empty<int>();
			Assert.Throws<Exception>(() => option.GetOrThrow(() => new Exception()));
		}

		[Fact(DisplayName = "GetOrThrow throws an exception if the provider is null")]
		public void TestGetOrThrowNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.GetOrThrow(null));
		}

		[Fact(DisplayName = "GetOrThrow throws an exception if the provider returns null")]
		public void TestGetOrThrowNullException()
		{
			var option = Option.Empty<int>();
			Assert.Throws<UnacceptableNullException>(() => option.GetOrThrow(() => null));
		}

		[Fact(DisplayName = "Map does nothing")]
		public void TestMap()
		{
			var option = Option.Empty<int>();
			Assert.IsType<None<string>>(option.Map(value => value.ToString()));
		}

		[Fact(DisplayName = "Map throws an exception for null")]
		public void TestMapNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.Map<int>(null));
		}
		
		[Fact(DisplayName = "Bind does nothing")]
		public void TestBind()
		{
			var option = Option.Empty<int>();
			Assert.IsType<None<string>>(option.Bind(value => Option.From(value.ToString())));
		}

		[Fact(DisplayName = "Bind throws an exception for null")]
		public void TestBindNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.Bind<int>(null));
		}

		[Fact(DisplayName = "DoIfSome does nothing")]
		public void TestDoIfNone()
		{
			int counter = 0;
			var option = Option.Empty<int>();

			option.DoIfSome(value => counter++);

			Assert.Equal(0, counter);
		}

		[Fact(DisplayName = "DoIfNone executes the action")]
		public void TestDoIfSome()
		{
			int counter = 0;
			var option = Option.Empty<int>();

			option.DoIfNone(() => counter++);

			Assert.Equal(1, counter);
		}

		[Fact(DisplayName = "DoIfSome throws an exception for null")]
		public void TestDoIfSomeNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.DoIfSome(null));
		}

		[Fact(DisplayName = "DoIfNone throws an exception for null")]
		public void TestDoIfNoneNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.DoIfNone(null));
		}

		[Fact(DisplayName = "ToResult returns a failed result")]
		public void TestToResult()
		{
			var expected = new Exception();
			var option = Option.Empty<int>();
			Assert.True(
				option.ToResult(expected) is Failure<int, Exception> result &&
				result.Errors is ConsCell<Exception> cell &&
				cell.Head == expected &&
				cell.Tail is Empty<Exception>);
		}

		[Fact(DisplayName = "ToResult(string) returns a failed result")]
		public void TestToResultString()
		{
			string expected = String.Empty;
			var option = Option.Empty<int>();
			Assert.True(
				option.ToResult(expected) is Failure<int, string> result &&
				result.Errors is ConsCell<string> cell &&
				cell.Head == expected &&
				cell.Tail is Empty<string>);
		}

		[Fact(DisplayName = "ToResult throws an exception for null")]
		public void TestToResultNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.ToResult(null));
		}

		[Fact(DisplayName = "ToResult(string) throws an exception for null")]
		public void TestToResultStringNull()
		{
			var option = Option.Empty<int>();
			Assert.Throws<ArgumentNullException>(() => option.ToResult(null));
		}

		[Fact(DisplayName = "GetEnumerator returns an enumerator for the value")]
		public void TestGetEnumerator()
		{
			int counter = 0;

			foreach (int _ in Option.Empty<int>())
			{
				counter++;
			}

			Assert.Equal(0, counter);
		}

		[Fact(DisplayName = "Equals(object) returns true for same types")]
		public void TestEqualsObjectSameValues()
		{
			var option1 = Option.Empty<int>();
			object option2 = Option.Empty<int>();
			Assert.True(option1.Equals(option2));
		}
		
		[Fact(DisplayName = "Equals(object) returns false for different types")]
		public void TestEqualsObjectDifferentType()
		{
			var option1 = Option.Empty<int>();
			object option2 = 2;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(object) returns false for null")]
		public void TestEqualsObjectNull()
		{
			var option1 = Option.Empty<int>();
			object option2 = null;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns true for same types")]
		public void TestEqualsOptionSameValues()
		{
			var option1 = Option.Empty<int>();
			var option2 = Option.Empty<int>();
			Assert.True(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns false for different types")]
		public void TestEqualsOptionDifferentType()
		{
			const int value = 1;
			var option1 = Option.Empty<int>();
			var option2 = value.ToOption();
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(Option) returns false for null")]
		public void TestEqualsOptionNull()
		{
			var option1 = Option.Empty<int>();
			Option<int> option2 = null;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "Equals(None) returns true")]
		public void TestEqualsSomeSameValues()
		{
			var option1 = Option.Empty<int>() as None<int>;
			var option2 = Option.Empty<int>() as None<int>;
			Assert.True(option1.Equals(option2));
		}
		
		[Fact(DisplayName = "Equals(Some) returns false for null")]
		public void TestEqualsSomeNull()
		{
			var option1 = Option.Empty<int>() as None<int>;
			Some<int> option2 = null;
			Assert.False(option1.Equals(option2));
		}

		[Fact(DisplayName = "GetHashCode returns 1")]
		public void TestGetHashCode()
		{
			Assert.Equal(1, Option.Empty<int>().GetHashCode());
		}

		[Fact(DisplayName = "ToString returns None[type]")]
		public void TestToString()
		{
			Assert.Equal($"None[{typeof(int)}]", Option.Empty<int>().ToString());
		}
	}
}
