using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Xunit;

using CSX.Collections;
using CSX.Exceptions;

namespace CSX.Results
{
	[SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed")]
	public class ResultTests
	{
		[Fact(DisplayName = "Succeed<TSuccess, TError> returns a Success which contains the value")]
		public void TestSucceed()
		{
			const int value = 1;
			var result = Result.Succeed<int, int>(value);
			Assert.True(result is Success<int, int> success && value == success.Value);
		}

		[Fact(DisplayName = "Succeed<TSuccess> returns a Success which contains the value")]
		public void TestSucceedString()
		{
			const int value = 1;
			var result = Result.Succeed(value);
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
			Assert.Throws<ArgumentNullException>(() => Result.Succeed<string, int>(null));
		}

		[Fact(DisplayName = "Succeed<TSuccess> throws an exception if the value is null")]
		public void TestSucceedStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Succeed<string>(null));
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
			var result = Result.Fail<int, int>(error);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the error")]
		public void TestFailString()
		{
			const string error = "error";
			var result = Result.Fail<int>(error);
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> returns a Failure which contains the errors list")]
		public void TestFailConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = Result.Fail<int, int>(errors);
			Assert.True(
				result is Failure<int, int> failure &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the errors list")]
		public void TestFailConsListString()
		{
			var errors = ConsList.Construct("1", "2");
			var result = Result.Fail<int>(errors);
			Assert.True(
				result is Failure<int, string> failure &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> returns a Failure which contains the errors")]
		public void TestFailIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = Result.Fail<int, int>(errors);
			Assert.True(
				result is Failure<int, int> failure &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the errors")]
		public void TestFailIEnumerableString()
		{
			IEnumerable<string> errors = ConsList.Construct("1", "2");
			var result = Result.Fail<int>(errors);
			Assert.True(
				result is Failure<int, string> failure &&
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
				failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the error")]
		public void TestToFailureString()
		{
			const string error = "error";
			var result = error.ToFailure<int>();
			Assert.True(
				result is Failure<int, string> failure &&
				failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName =
			"ToFailure<TSuccess, TError> returns a Failure which contains the errors list")]
		public void TestToFailureConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = errors.ToFailure<int, int>();
			Assert.True(
				result is Failure<int, int> failure &&
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
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the errors")]
		public void TestToFailureIEnumerableString()
		{
			IEnumerable<string> errors = ConsList.Construct("1", "2");
			var result = errors.ToFailure<int>();
			Assert.True(
				result is Failure<int, string> failure &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess, TError> throws an exception if the error is null")]
		public void TestFailNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int, object>(null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the error is null")]
		public void TestFailStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int>((string)null));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> throws an exception if the errors list is null")]
		public void TestFailConsListNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int, int>(null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the errors list is null")]
		public void TestFailConsListStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int>((ConsList<string>)null));
		}

		[Fact(DisplayName =
			"Fail<TSuccess, TError> throws an exception if the errors are null")]
		public void TestFailIEnumerableNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => Result.Fail<int, int>((IEnumerable<int>)null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the errors are null")]
		public void TestFailIEnumerableStringNull()
		{
			Assert.Throws<ArgumentNullException>(
				() => Result.Fail<int>((IEnumerable<string>)null));
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

		[Fact(DisplayName = "Lift<TSuccess, VSuccess, TError> for Success maps the value")]
		public void TestLiftSuccess()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, int>();

			Func<int, int> add1 = x => x + 1;
			var liftedAdd1 = add1.Lift<int, int, int>();

			Assert.True(
				liftedAdd1(result) is Success<int, int> success &&
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

			Assert.True(
				liftedAdd1(result) is Success<int, string> success &&
				success.Value == actual);
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess, TError> for Failure does nothing")]
		public void TestLiftFailure()
		{
			var result = Result.Fail<int, int>(2);

			Func<int, string> toString = x => x.ToString();
			var liftedToString = toString.Lift<int, string, int>();

			Assert.IsType<Failure<string, int>>(liftedToString(result));
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess> for Failure does nothing")]
		public void TestLiftFailureString()
		{
			var result = Result.Fail<int>("failure");

			Func<int, string> toString = x => x.ToString();
			var liftedToString = toString.Lift();

			Assert.IsType<Failure<string, string>>(liftedToString(result));
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess, TError> throws an exception for null")]
		public void TestLiftNull()
		{
			Func<int, string> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Lift<int, string, int>());
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess> throws an exception for null")]
		public void TestLiftNullString()
		{
			Func<int, string> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Lift());
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess, TError> for a successful function and " +
			"a successful value maps the value")]
		public void TestApplySuccessSuccess()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, int>();

			var add1 = Result.Succeed<Func<int, int>, int>(x => x + 1);
			var appliedAdd1 = add1.Apply();

			Assert.True(
				appliedAdd1(result) is Success<int, int> success &&
				success.Value == actual);
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess> for a successful function and " +
			"a successful value maps the value")]
		public void TestApplySuccessSuccessString()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess();

			var add1 = Result.Succeed<Func<int, int>>(x => x + 1);
			var appliedAdd1 = add1.Apply();

			Assert.True(
				appliedAdd1(result) is Success<int, string> success &&
				success.Value == actual);
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess, TError> for a successful function and " +
			"a failed value returns the value's error")]
		public void TestApplySuccessFailure()
		{
			const int expected = 1;

			var result = Result.Fail<int, int>(expected);

			var toString = Result.Succeed<Func<int, string>, int>(x => x.ToString());
			var appliedToString = toString.Apply();

			Assert.True(
				appliedToString(result) is Failure<string, int> failure &&
				failure.Errors.Equals(ConsList.From(expected)));
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess> for a successful function and " +
			"a failed value returns the value's error")]
		public void TestApplySuccessFailureString()
		{
			const string expected = "failure";

			var result = Result.Fail<int>(expected);

			var toString = Result.Succeed<Func<int, string>>(x => x.ToString());
			var appliedToString = toString.Apply();

			Assert.True(
				appliedToString(result) is Failure<string, string> failure &&
				failure.Errors.Equals(ConsList.From(expected)));
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess, TError> for a failed function and " +
			"a successful value returns the functions's error")]
		public void TestApplyFailureSuccess()
		{
			const int expected = 1;

			var result = 1.ToSuccess<int, int>();

			var toString = Result.Fail<Func<int, string>, int>(expected);
			var appliedToString = toString.Apply();

			Assert.True(
				appliedToString(result) is Failure<string, int> failure &&
				failure.Errors.Equals(ConsList.From(expected)));
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess> for a failed function and " +
			"a successful value returns the functions's error")]
		public void TestApplyFailureSuccessString()
		{
			const string expected = "failure";

			var result = 1.ToSuccess();

			var toString = Result.Fail<Func<int, string>, string>(expected);
			var appliedToString = toString.Apply();

			Assert.True(
				appliedToString(result) is Failure<string, string> failure &&
				failure.Errors.Equals(ConsList.From(expected)));
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess, TError> for a failed function and " +
			"a failed value returns joined errors")]
		public void TestApplyFailureFailure()
		{
			const int expectedValue = 1;
			const int expectedFunc = 2;

			var result = Result.Fail<int, int>(expectedValue);

			var toString = Result.Fail<Func<int, string>, int>(expectedFunc);
			var appliedToString = toString.Apply();

			Assert.True(
				appliedToString(result) is Failure<string, int> failure &&
				failure.Errors.Equals(ConsList.Construct(expectedFunc, expectedValue)));
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess, TError> for a failed function and " +
			"a failed value returns joined errors")]
		public void TestApplyFailureFailureString()
		{
			const string expectedValue = "failure1";
			const string expectedFunc = "failure2";

			var result = Result.Fail<int>(expectedValue);

			var toString = Result.Fail<Func<int, string>>(expectedFunc);
			var appliedToString = toString.Apply();

			Assert.True(
				appliedToString(result) is Failure<string, string> failure &&
				failure.Errors.Equals(ConsList.Construct(expectedFunc, expectedValue)));
		}

		[Fact(DisplayName = "Apply<TSuccess, VSuccess, TError> throws an exception for null")]
		public void TestApplyNull()
		{
			Result<Func<int, string>, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Apply());
		}

		[Fact(DisplayName = "Apply<TSuccess, VSuccess> throws an exception for null")]
		public void TestApplyNullString()
		{
			Result<Func<int, string>, string> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Apply());
		}

		[Fact(DisplayName = "Applied function throws an exception for null")]
		public void TestAppliedFuncNull()
		{
			var func = Result.Succeed<Func<int, int>, int>(x => x);
			var appliedFunc = func.Apply();
			Assert.Throws<ArgumentNullException>(() => appliedFunc(null));
		}

		[Fact(DisplayName = "Applied function throws an exception for null (string)")]
		public void TestAppliedFuncNullString()
		{
			var func = Result.Succeed<Func<int, int>, string>(x => x);
			var appliedFunc = func.Apply();
			Assert.Throws<ArgumentNullException>(() => appliedFunc(null));
		}

		[Fact(DisplayName =
			"Applied function throws an exception if its internal function returns null")]
		public void TestAppliedFuncReturnNull()
		{
			var func = Result.Succeed<Func<int, string>, int>(x => null);
			var appliedFunc = func.Apply();
			Assert.Throws<UnacceptableNullException>(
				() => appliedFunc(1.ToSuccess<int, int>()));
		}

		[Fact(DisplayName =
			"Applied function throws an exception " +
			"if its internal function returns null (string)")]
		public void TestAppliedFuncReturnNullString()
		{
			var func = Result.Succeed<Func<int, string>, string>(x => null);
			var appliedFunc = func.Apply();
			Assert.Throws<UnacceptableNullException>(() => appliedFunc(1.ToSuccess()));
		}

		[Fact(DisplayName =
			"IEnumerable.GetEnumerator returns an equal enumerator " +
			"as GetEnumerator for success")]
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
			var failure = Result.Fail<int>("failure");

			var genericEnumerator = failure.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)failure).GetEnumerator();

			Assert.False(genericEnumerator.MoveNext());
			Assert.False(nonGenericEnumerator.MoveNext());
		}
	}
}
