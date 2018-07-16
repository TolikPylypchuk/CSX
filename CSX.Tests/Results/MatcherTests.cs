using System;

using Xunit;

using CSX.Collections;

namespace CSX.Results
{
    public class MatcherTests
    {
		[Fact(DisplayName = "Match returns the provided value for a success in the first place")]
		public void TestMatchForSuccess1()
		{
			const int expected = 1;
			var result = expected.ToSuccess();

			int actual = result
				.MatchSuccess(value => value)
				.MatchFailure(errors => 0);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for a failure in the first place")]
		public void TestMatchForFailure1()
		{
			const string expected = "expected";
			var result = expected.ToFailure<string>();

			var actual = result
				.MatchFailure(errors => errors)
				.MatchSuccess(ConsList.From);

			Assert.Equal(ConsList.From(expected), actual);
		}

		[Fact(DisplayName = "Match returns the provided value for a success in the second place")]
		public void TestMatchForSuccess2()
		{
			const int expected = 1;
			var result = expected.ToSuccess();

			int actual = result
				.MatchFailure(errors => 0)
				.MatchSuccess(value => value);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for a failure in the second place")]
		public void TestMatchForFailure2()
		{
			const string expected = "expected";
			var result = expected.ToFailure<string>();

			string actual = result
				.MatchSuccess(value => value)
				.MatchFailure(errors => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for anything other than a success")]
		public void TestMatchForAnyAfterSuccess()
		{
			const string expected = "expected";
			var result = expected.ToFailure<string>();

			string actual = result
				.MatchSuccess(value => value)
				.MatchAny(() => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for anything other than a failure")]
		public void TestMatchForAnyAfterFailure()
		{
			const string expected = "expected";
			var result = expected.ToSuccess();

			string actual = result
				.MatchFailure(errors => String.Empty)
				.MatchAny(() => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for a success when anything else is also matched")]
		public void TestMatchForAnyBeforeSuccess()
		{
			const string expected = "expected";
			var result = expected.ToSuccess();

			string actual = result
				.MatchSuccess(value => value)
				.MatchAny(() => String.Empty);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for a failure when anything else is also matched")]
		public void TestMatchForAnyBeforeFailure()
		{
			const string expected = "expected";
			var result = expected.ToFailure<string>();

			string actual = result
				.MatchFailure(errors => expected)
				.MatchAny(() => String.Empty);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Match returns the provided value for anything")]
		public void TestMatchForAny()
		{
			const string expected = "expected";
			var result = 1.ToSuccess();

			string actual = result.MatchAny(() => expected);

			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "MatchSuccess throws an exception for null")]
		public void TestMatchSuccessNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() => result.MatchSuccess<int>(null));
		}

		[Fact(DisplayName = "MatchFailure throws an exception for null")]
		public void TestMatchFailureNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() => result.MatchFailure<int>(null));
		}

		[Fact(DisplayName = "MatchAny throws an exception for null")]
		public void TestMatchAnyNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() => result.MatchAny<int>(null));
		}

		[Fact(DisplayName = "MatchFailure throws an exception for null after a success was matched")]
		public void TestMatchFailureAfterSuccessNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() =>
				result
					.MatchSuccess(value => value)
					.MatchFailure(null));
		}

		[Fact(DisplayName = "MatchAny throws an exception for null after a success was matched")]
		public void TestMatchAnyAfterSuccessNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() =>
				result
					.MatchSuccess(value => value)
					.MatchAny(null));
		}

		[Fact(DisplayName = "MatchSome throws an exception for null after a failure was matched")]
		public void TestMatchSuccessAfterFailureNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() =>
				result
					.MatchFailure(errors => 1)
					.MatchSuccess(null));
		}

		[Fact(DisplayName = "MatchAny throws an exception for null after a failure was matched")]
		public void TestMatchAnyAfterFailureNull()
		{
			var result = 1.ToSuccess();
			Assert.Throws<ArgumentNullException>(() =>
				result
					.MatchFailure(errors => 1)
					.MatchAny(null));
		}
	}
}
