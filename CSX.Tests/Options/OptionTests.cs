using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

using Xunit;

namespace CSX.Options
{
	[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
	[SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed")]
	public class OptionTests
	{
		[Fact(DisplayName = "From returns Some which contains the value")]
		public void TestFrom()
		{
			const int value = 1;
			var option = Option.From(value);
			Assert.True(option is Some<int> some && value == some.Value);
		}

		[Fact(DisplayName = "From returns None for null")]
		public void TestFromNull()
		{
			var option = Option.From<string>(null);
			Assert.IsType<None<string>>(option);
		}

		[Fact(DisplayName = "From returns Some which contains the nullable value")]
		public void TestFromNullable()
		{
			int? value = 1;
			var option = Option.From(value);
			Assert.True(option is Some<int> some && value.Value == some.Value);
		}

		[Fact(DisplayName = "From returns None when a nullable is null")]
		public void TestFromNullableNull()
		{
			var option = Option.From<int>(null);
			Assert.IsType<None<int>>(option);
		}

		[Fact(DisplayName = "Empty returns None")]
		public void TestEmpty()
		{
			var option = Option.Empty<int>();
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

		[Fact(DisplayName = "Lift for Some maps the value")]
		public void TestLiftSome()
		{
			const int expected = 1;
			const int actual = 2;

			var option = expected.ToOption();

			Func<int, int> add1 = x => x + 1;
			var liftedAdd1 = add1.Lift();

			Assert.True(liftedAdd1(option) is Some<int> some && some.Value == actual);
		}

		[Fact(DisplayName = "Lift for None does nothing")]
		public void TestLiftNone()
		{
			var option = Option.Empty<int>();

			Func<int, string> toString = x => x.ToString();
			var liftedToString = toString.Lift();

			Assert.IsType<None<string>>(liftedToString(option));
		}

		[Fact(DisplayName = "Lift throws an exception for null")]
		public void TestLiftNull()
		{
			Func<int, string> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Lift());
		}

		[Fact(DisplayName = "Lifted function throws an exception for null")]
		public void TestLiftedFuncNull()
		{
			Func<int, int> func = x => x;
			var liftedFunc = func.Lift();
			Assert.Throws<ArgumentNullException>(() => liftedFunc(null));
		}

		[Fact(DisplayName = "Apply for Some function and Some value maps the value")]
		public void TestApplySomeSome()
		{
			const int expected = 1;
			const int actual = 2;

			var option = expected.ToOption();

			var add1 = Option.From<Func<int, int>>(x => x + 1);
			var appliedAdd1 = add1.Apply();

			Assert.True(appliedAdd1(option) is Some<int> some && some.Value == actual);
		}

		[Fact(DisplayName = "Apply for Some function and for None value does nothing")]
		public void TestApplySomeNone()
		{
			var option = Option.Empty<int>();

			var toString = Option.From<Func<int, string>>(x => x.ToString());
			var appliedToString = toString.Apply();

			Assert.IsType<None<string>>(appliedToString(option));
		}

		[Fact(DisplayName = "Apply for None function and for Some value does nothing")]
		public void TestApplyNoneSome()
		{
			var option = 1.ToOption();

			var toString = Option.Empty<Func<int, string>>();
			var appliedToString = toString.Apply();

			Assert.IsType<None<string>>(appliedToString(option));
		}

		[Fact(DisplayName = "Apply for None function and for None value does nothing")]
		public void TestApplyNoneNone()
		{
			var option = Option.Empty<int>();

			var toString = Option.Empty<Func<int, string>>();
			var appliedToString = toString.Apply();

			Assert.IsType<None<string>>(appliedToString(option));
		}

		[Fact(DisplayName = "Apply throws an exception for null")]
		public void TestApplyNull()
		{
			Option<Func<int, string>> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Apply());
		}

		[Fact(DisplayName = "Applied function throws an exception for null")]
		public void TestAppliedFuncNull()
		{
			var func = Option.From<Func<int, int>>(x => x);
			var appliedFunc = func.Apply();
			Assert.Throws<ArgumentNullException>(() => appliedFunc(null));
		}

		[Fact(DisplayName =
			"IEnumerable.GetEnumerator returns an equal enumerator as GetEnumerator for Some")]
		public void TestIEnumerableGetEnumeratorSome()
		{
			var some = 1.ToOption();

			var genericEnumerator = some.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)some).GetEnumerator();

			Assert.True(genericEnumerator.MoveNext());
			Assert.True(nonGenericEnumerator.MoveNext());
			Assert.Equal(genericEnumerator.Current, nonGenericEnumerator.Current);
		}

		[Fact(DisplayName =
			"IEnumerable.GetEnumerator returns an equal enumerator as GetEnumerator for None")]
		public void TestIEnumerableGetEnumeratorNone()
		{
			var none = Option.Empty<int>();

			var genericEnumerator = none.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)none).GetEnumerator();
			
			Assert.False(genericEnumerator.MoveNext());
			Assert.False(nonGenericEnumerator.MoveNext());
		}
	}
}
