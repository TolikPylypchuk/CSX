using System;

using Xunit;

using static CSX.Options.Option;

namespace CSX.Options
{
	public class MatcherTests
	{
		[Fact(DisplayName = "Match returns the provided value for Some in the first place")]
		public void TestMatchForSome1()
		{
			const int expected = 1;
			var option = expected.ToOption();

			int actual = option
				.MatchSome(value => value)
				.MatchNone(() => 0);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for None in the first place")]
		public void TestMatchForNone1()
		{
			const int expected = 0;
			var option = Empty<int>();

			int actual = option
				.MatchNone(() => expected)
				.MatchSome(value => value);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for Some in the second place")]
		public void TestMatchForSome2()
		{
			const int expected = 1;
			var option = expected.ToOption();

			int actual = option
				.MatchNone(() => 0)
				.MatchSome(value => value);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for None in the second place")]
		public void TestMatchForNone2()
		{
			const int expected = 0;
			var option = Empty<int>();

			int actual = option
				.MatchSome(value => value)
				.MatchNone(() => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for anything other than Some")]
		public void TestMatchForAnyAfterSome()
		{
			const int expected = 0;
			var option = Empty<int>();

			int actual = option
				.MatchSome(value => value)
				.MatchAny(() => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for anything other than None")]
		public void TestMatchForAnyAfterNone()
		{
			const int expected = 1;
			var option = expected.ToOption();

			int actual = option
				.MatchNone(() => 0)
				.MatchAny(() => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName =
			"Match returns the provided value for Some when anything else is also matched")]
		public void TestMatchForAnyBeforeSome()
		{
			const int expected = 1;
			var option = expected.ToOption();

			int actual = option
				.MatchSome(value => value)
				.MatchAny(() => 0);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName =
			"Match returns the provided value for None when anything else is also matched")]
		public void TestMatchForAnyBeforeNone()
		{
			const int expected = 0;
			var option = Empty<int>();

			int actual = option
			    .MatchNone(() => expected)
			    .MatchAny(() => 1);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for anything")]
		public void TestMatchForAny()
		{
			const int expected = 0;
			var option = 1.ToOption();
			
			int actual = option.MatchAny(() => expected);

			Assert.Equal(expected, actual);
		}
		
		[Fact(DisplayName = "MatchSome throws an exception for null")]
		public void TestMatchSomeNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.MatchSome<int>(null));
		}

		[Fact(DisplayName = "MatchNone throws an exception for null")]
		public void TestMatchNoneNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.MatchNone<int>(null));
		}

		[Fact(DisplayName = "MatchAny throws an exception for null")]
		public void TestMatchAnyNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() => option.MatchAny<int>(null));
		}

		[Fact(DisplayName = "MatchNone throws an exception for null after Some was matched")]
		public void TestMatchNoneAfterSomeNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() =>
				option
					.MatchSome(value => value)
					.MatchNone(null));
		}

		[Fact(DisplayName = "MatchAny throws an exception for null after Some was matched")]
		public void TestMatchAnyAfterSomeNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() =>
				option
					.MatchSome(value => value)
					.MatchAny(null));
		}

		[Fact(DisplayName = "MatchSome throws an exception for null after None was matched")]
		public void TestMatchSomeAfterNoneNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() =>
				option
					.MatchNone(() => 1)
					.MatchSome(null));
		}

		[Fact(DisplayName = "MatchAny throws an exception for null after None was matched")]
		public void TestMatchAnyAfterNoneNull()
		{
			var option = 1.ToOption();
			Assert.Throws<ArgumentNullException>(() =>
				option
					.MatchNone(() => 1)
					.MatchAny(null));
		}
	}
}
