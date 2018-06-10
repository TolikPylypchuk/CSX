using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Xunit;

using CSX.Collections;

using static CSX.Results.Result;

namespace CSX.Results
{
	[SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed")]
	public class ResultTests
	{
		[Fact(DisplayName = "Succeed<TSuccess, TError> returns a Success which contains the value")]
		public void TestSucceed()
		{
			const int value = 1;
			var result = Succeed<int, int>(value);
			Assert.True(result is Success<int, int> success && value == success.Value);
		}

		[Fact(DisplayName = "Succeed<TSuccess> returns a Success which contains the value")]
		public void TestSucceedString()
		{
			const int value = 1;
			var result = Succeed(value);
			Assert.True(result is Success<int, string> success && value == success.Value);
		}

		[Fact(DisplayName =
			"ToSuccess<TSuccess, TError> returns a Success which contains the value")]
		public void TestToSuccess()
		{
			const int value = 1;
			var result = value.ToSuccess<int, int>();
			Assert.True(result is Success<int, int> success && value == success.Value);
		}

		[Fact(DisplayName = "ToSuccess<TSuccess> returns a Success which contains the value")]
		public void TestToSuccessString()
		{
			const int value = 1;
			var result = value.ToSuccess();
			Assert.True(result is Success<int, string> success && value == success.Value);
		}

		[Fact(DisplayName =
			"Succeed<TSuccess, TError> throws an exception if the value is null")]
		public void TestSucceedNull()
		{
			Assert.Throws<ArgumentNullException>(() => Succeed<string, int>(null));
		}

		[Fact(DisplayName = "Succeed<TSuccess> throws an exception if the value is null")]
		public void TestSucceedStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Succeed<string>(null));
		}

		[Fact(DisplayName =
			"ToSuccess<TSuccess, TError> throws an exception if the value is null")]
		public void TestToSuccessNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => ((string)null).ToSuccess<string, int>());
		}

		[Fact(DisplayName = "ToSuccess<TSuccess> throws an exception if the value is null")]
		public void TestToSuccessStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((string)null).ToSuccess());
		}
		
		[Fact(DisplayName = "Fail<TSuccess, TError> returns a Failure which contains the error")]
		public void TestFail()
		{
			const int error = 1;
			var result = Fail<int, int>(error);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 1 &&
				error == failure.Errors[0]);
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the error")]
		public void TestFailString()
		{
			const string error = "error";
			var result = Fail<int>(error);
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Count == 1 &&
				error == failure.Errors[0]);
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> returns a Failure which contains the errors list")]
		public void TestFailConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = Fail<int, int>(errors);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the errors list")]
		public void TestFailConsListString()
		{
			var errors = ConsList.Construct("1", "2");
			var result = Fail<int>(errors);
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> returns a Failure which contains the errors")]
		public void TestFailIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = Fail<int, int>(errors);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the errors")]
		public void TestFailIEnumerableString()
		{
			IEnumerable<string> errors = ConsList.Construct("1", "2");
			var result = Fail<int>(errors);
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> returns a Failure which contains the error")]
		public void TestToFailure()
		{
			const int error = 1;
			var result = error.ToFailure<int, int>();
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 1 &&
				error == failure.Errors[0]);
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the error")]
		public void TestToFailureString()
		{
			const string error = "error";
			var result = error.ToFailure<int>();
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Count == 1 &&
				error == failure.Errors[0]);
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> returns a Failure which contains the errors list")]
		public void TestToFailureConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = errors.ToFailure<int, int>();
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess> returns a Failure which contains the errors list")]
		public void TestToFailureConsListString()
		{
			var errors = ConsList.Construct("1", "2");
			var result = errors.ToFailure<int>();
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> returns a Failure which contains the errors")]
		public void TestToFailureIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = errors.ToFailure<int, int>();
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the errors")]
		public void TestToFailureIEnumerableString()
		{
			IEnumerable<string> errors = ConsList.Construct("1", "2");
			var result = errors.ToFailure<int>();
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess, TError> throws an exception if the error is null")]
		public void TestFailNull()
		{
			Assert.Throws<ArgumentNullException>(() => Fail<int, object>(null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the error is null")]
		public void TestFailStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Fail<int>((string)null));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> throws an exception if the errors list is null")]
		public void TestFailConsListNull()
		{
			Assert.Throws<ArgumentNullException>(() => Fail<int, int>(null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the errors list is null")]
		public void TestFailConsListStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Fail<int>((ConsList<string>)null));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> throws an exception if the errors are null")]
		public void TestFailIEnumerableNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => Fail<int, int>((IEnumerable<int>)null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the errors are null")]
		public void TestFailIEnumerableStringNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => Fail<int>((IEnumerable<string>)null));
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> throws an exception if the error is null")]
		public void TestToFailureNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => ((object)null).ToFailure<int, object>());
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess> throws an exception if the error is null")]
		public void TestToFailureStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((string)null).ToFailure<int>());
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> throws an exception if the errors list is null")]
		public void TestToFailureConsListNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => ((ConsList<int>)null).ToFailure<int, int>());
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess> throws an exception if the errors list is null")]
		public void TestToFailureConsListStringNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => ((ConsList<string>)null).ToFailure<int>());
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> throws an exception if the errors are null")]
		public void TestToFailureIEnumerableNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => ((IEnumerable<int>)null).ToFailure<int, int>());
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess> throws an exception if the errors are null")]
		public void TestToFailureIEnumerableStringNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => ((IEnumerable<string>)null).ToFailure<int>());
		}

		[Fact(DisplayName = "Lift<TResult, VResult, TError> for Success maps the value")]
		public void TestLiftSuccess()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, int>();

			Func<int, int> add1 = x => x + 1;
			var liftedAdd1 = add1.Lift<int, int, int>();

			Assert.True(liftedAdd1(result) is Success<int, int> success &&
			            success.Value == actual);
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess> for Success maps the value")]
		public void TestLiftSuccessString()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, string>();

			Func<int, int> add1 = x => x + 1;
			var liftedAdd1 = add1.Lift();

			Assert.True(liftedAdd1(result) is Success<int, string> success &&
			            success.Value == actual);
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess, TError> for Failure does nothing")]
		public void TestLiftFailure()
		{
			var result = Fail<int, int>(2);

			Func<int, string> toString = x => x.ToString();
			var liftedToString = toString.Lift<int, string, int>();

			Assert.IsType<Failure<string, int>>(liftedToString(result));
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess> for Failure does nothing")]
		public void TestLiftFailureString()
		{
			var result = Fail<int>("failure");

			Func<int, string> toString = x => x.ToString();
			var liftedToString = toString.Lift();

			Assert.IsType<Failure<string, string>>(liftedToString(result));
		}

		[Fact(DisplayName = "Lift<TResult, VResult, TError> throws an exception for null")]
		public void TestLiftNull()
		{
			Func<int, string> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Lift<int, string, int>());
		}

		[Fact(DisplayName = "Lift<TResult, VResult> throws an exception for null")]
		public void TestLiftNullString()
		{
			Func<int, string> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Lift());
		}

		[Fact(DisplayName =
			"IEnumerable.GetEnumerator returns an equal enumerator as GetEnumerator for success")]
		public void TestIEnumerableGetEnumeratorSome()
		{
			var success = 1.ToSuccess();

			var genericEnumerator = success.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)success).GetEnumerator();

			Assert.True(genericEnumerator.MoveNext());
			Assert.True(nonGenericEnumerator.MoveNext());
			Assert.Equal(genericEnumerator.Current, nonGenericEnumerator.Current);
		}

		[Fact(DisplayName =
			"IEnumerable.GetEnumerator returns an equal enumerator as GetEnumerator for Failure")]
		public void TestIEnumerableGetEnumeratorNone()
		{
			var failure = Fail<int>("failure");

			var genericEnumerator = failure.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)failure).GetEnumerator();

			Assert.False(genericEnumerator.MoveNext());
			Assert.False(nonGenericEnumerator.MoveNext());
		}
	}
}
