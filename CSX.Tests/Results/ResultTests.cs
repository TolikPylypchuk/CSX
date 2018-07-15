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

		[Fact(DisplayName = "ToSuccess<TSuccess, TError> returns a Success which contains the value")]
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

		[Fact(DisplayName = "Succeed<TSuccess, TError> throws an exception if the value is null")]
		public void TestSucceedNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Succeed<string, int>(null));
		}

		[Fact(DisplayName = "Succeed<TSuccess> throws an exception if the value is null")]
		public void TestSucceedStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Succeed<string>(null));
		}

		[Fact(DisplayName = "ToSuccess<TSuccess, TError> throws an exception if the value is null")]
		public void TestToSuccessNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((string)null).ToSuccess<string, int>());
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
			Assert.True(result is Failure<int, int> failure && failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the error")]
		public void TestFailString()
		{
			const string error = "error";
			var result = Result.Fail<int>(error);
			Assert.True(result is Failure<int, string> failure && failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName = "Fail<TSuccess, TError> returns a Failure which contains the errors list")]
		public void TestFailConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = Result.Fail<int, int>(errors);
			Assert.True(result is Failure<int, int> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the errors list")]
		public void TestFailConsListString()
		{
			var errors = ConsList.Construct("1", "2");
			var result = Result.Fail<int>(errors);
			Assert.True(result is Failure<int, string> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess, TError> returns a Failure which contains the errors")]
		public void TestFailIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = Result.Fail<int, int>(errors);
			Assert.True(result is Failure<int, int> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns a Failure which contains the errors")]
		public void TestFailIEnumerableString()
		{
			IEnumerable<string> errors = ConsList.Construct("1", "2");
			var result = Result.Fail<int>(errors);
			Assert.True(result is Failure<int, string> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess, TError> returns a Failure which contains the error")]
		public void TestToFailure()
		{
			const int error = 1;
			var result = error.ToFailure<int, int>();
			Assert.True(result is Failure<int, int> failure && failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the error")]
		public void TestToFailureString()
		{
			const string error = "error";
			var result = error.ToFailure<int>();
			Assert.True(result is Failure<int, string> failure && failure.Errors.Equals(ConsList.From(error)));
		}

		[Fact(DisplayName = "ToFailure<TSuccess, TError> returns a Failure which contains the errors list")]
		public void TestToFailureConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = errors.ToFailure<int, int>();
			Assert.True(result is Failure<int, int> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the errors list")]
		public void TestToFailureConsListString()
		{
			var errors = ConsList.Construct("1", "2");
			var result = errors.ToFailure<int>();
			Assert.True(result is Failure<int, string> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess, TError> returns a Failure which contains the errors")]
		public void TestToFailureIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = errors.ToFailure<int, int>();
			Assert.True(result is Failure<int, int> failure && errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns a Failure which contains the errors")]
		public void TestToFailureIEnumerableString()
		{
			IEnumerable<string> errors = ConsList.Construct("1", "2");
			var result = errors.ToFailure<int>();
			Assert.True(result is Failure<int, string> failure && errors.Equals(failure.Errors));
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

		[Fact(DisplayName = "Fail<TSuccess, TError> throws an exception if the errors list is null")]
		public void TestFailConsListNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int, int>(null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the errors list is null")]
		public void TestFailConsListStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int>((ConsList<string>)null));
		}

		[Fact(DisplayName = "Fail<TSuccess, TError> throws an exception if the errors are null")]
		public void TestFailIEnumerableNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int, int>((IEnumerable<int>)null));
		}

		[Fact(DisplayName = "Fail<TSuccess> throws an exception if the errors are null")]
		public void TestFailIEnumerableStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => Result.Fail<int>((IEnumerable<string>)null));
		}

		[Fact(DisplayName = "ToFailure<TSuccess, TError> throws an exception if the error is null")]
		public void TestToFailureNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((object)null).ToFailure<int, object>());
		}

		[Fact(DisplayName = "ToFailure<TSuccess> throws an exception if the error is null")]
		public void TestToFailureStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((string)null).ToFailure<int>());
		}

		[Fact(DisplayName = "ToFailure<TSuccess, TError> throws an exception if the errors list is null")]
		public void TestToFailureConsListNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((ConsList<int>)null).ToFailure<int, int>());
		}

		[Fact(DisplayName = "ToFailure<TSuccess> throws an exception if the errors list is null")]
		public void TestToFailureConsListStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((ConsList<string>)null).ToFailure<int>());
		}

		[Fact(DisplayName = "ToFailure<TSuccess, TError> throws an exception if the errors are null")]
		public void TestToFailureIEnumerableNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).ToFailure<int, int>());
		}

		[Fact(DisplayName = "ToFailure<TSuccess> throws an exception if the errors are null")]
		public void TestToFailureIEnumerableStringNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<string>)null).ToFailure<int>());
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess, TError> for Success maps the value")]
		public void TestLiftSuccess()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, int>();

			Func<int, int> add1 = x => x + 1;
			var liftedAdd1 = add1.Lift<int, int, int>();

			Assert.True(liftedAdd1(result) is Success<int, int> success && success.Value == actual);
		}

		[Fact(DisplayName = "Lift<TSuccess, VSuccess> for Success maps the value")]
		public void TestLiftSuccessString()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, string>();

			Func<int, int> add1 = x => x + 1;
			var liftedAdd1 = add1.Lift();

			Assert.True(liftedAdd1(result) is Success<int, string> success && success.Value == actual);
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
			"Apply<TSuccess, VSuccess, TError> for a successful function and a successful value maps the value")]
		public void TestApplySuccessSuccess()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess<int, int>();

			var add1 = Result.Succeed<Func<int, int>, int>(x => x + 1);
			var appliedAdd1 = add1.Apply();

			Assert.True(appliedAdd1(result) is Success<int, int> success && success.Value == actual);
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess> for a successful function and a successful value maps the value")]
		public void TestApplySuccessSuccessString()
		{
			const int expected = 1;
			const int actual = 2;

			var result = expected.ToSuccess();

			var add1 = Result.Succeed<Func<int, int>>(x => x + 1);
			var appliedAdd1 = add1.Apply();

			Assert.True(appliedAdd1(result) is Success<int, string> success && success.Value == actual);
		}

		[Fact(DisplayName =
			"Apply<TSuccess, VSuccess, TError> for a successful function and a failed value returns the value's error")]
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
			"Apply<TSuccess, VSuccess> for a successful function and a failed value returns the value's error")]
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
			"Apply<TSuccess, VSuccess, TError> for a failed function and a successful value returns the functions's error")]
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
			"Apply<TSuccess, VSuccess> for a failed function and a successful value returns the functions's error")]
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
			"Apply<TSuccess, VSuccess, TError> for a failed function and a failed value returns joined errors")]
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
			"Apply<TSuccess, VSuccess, TError> for a failed function and a failed value returns joined errors")]
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

		[Fact(DisplayName = "Applied function throws an exception if its internal function returns null")]
		public void TestAppliedFuncReturnNull()
		{
			var func = Result.Succeed<Func<int, string>, int>(x => null);
			var appliedFunc = func.Apply();
			Assert.Throws<UnacceptableNullException>(
				() => appliedFunc(1.ToSuccess<int, int>()));
		}

		[Fact(DisplayName = "Applied function throws an exception if its internal function returns null (string)")]
		public void TestAppliedFuncReturnNullString()
		{
			var func = Result.Succeed<Func<int, string>, string>(x => null);
			var appliedFunc = func.Apply();
			Assert.Throws<UnacceptableNullException>(() => appliedFunc(1.ToSuccess()));
		}
		
		[Fact(DisplayName = "IEnumerable.GetEnumerator returns an equal enumerator as GetEnumerator for success")]
		public void TestIEnumerableGetEnumeratorSome()
		{
			var success = 1.ToSuccess();

			var genericEnumerator = success.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)success).GetEnumerator();

			Assert.True(genericEnumerator.MoveNext());
			Assert.True(nonGenericEnumerator.MoveNext());
			Assert.Equal(genericEnumerator.Current, nonGenericEnumerator.Current);
		}

		[Fact(DisplayName = "IEnumerable.GetEnumerator returns an equal enumerator as GetEnumerator for Failure")]
		public void TestIEnumerableGetEnumeratorNone()
		{
			var failure = Result.Fail<int>("failure");

			var genericEnumerator = failure.GetEnumerator();
			var nonGenericEnumerator = ((IEnumerable)failure).GetEnumerator();

			Assert.False(genericEnumerator.MoveNext());
			Assert.False(nonGenericEnumerator.MoveNext());
		}

		[Fact(DisplayName = "Catch<TSuccess> returns a success if there are no exceptions")]
		public void TestCatch0Result()
		{
			const int result = 1;

			Func<int> func = () => result;

			Assert.True(func.Catch()() is Success<int, Exception> success && success.Value == func());
		}

		[Fact(DisplayName = "Catch<T1, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch1Result()
		{
			const int x = 1;

			Func<int, int> func = arg => arg + 1;

			Assert.True(func.Catch()(x) is Success<int, Exception> success && success.Value == func(x));
		}

		[Fact(DisplayName = "Catch<T1, T2, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch2Result()
		{
			const int x1 = 1;
			const int x2 = 2;

			Func<int, int, int> func = (arg1, arg2) => arg1 + arg2;

			Assert.True(func.Catch()(x1, x2) is Success<int, Exception> success && success.Value == func(x1, x2));
		}

		[Fact(DisplayName = "Catch<T1, T2, T3, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch3Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;

			Func<int, int, int, int> func = (arg1, arg2, arg3) => arg1 + arg2 + arg3;

			Assert.True(
				func.Catch()(x1, x2, x3) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3));
		}

		[Fact(DisplayName = "Catch<T1, ..., T4, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch4Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;

			Func<int, int, int, int, int> func = (arg1, arg2, arg3, arg4) => arg1 + arg2 + arg3 + arg4;

			Assert.True(
				func.Catch()(x1, x2, x3, x4) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4));
		}

		[Fact(DisplayName = "Catch<T1, ..., T5, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch5Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;

			Func<int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5) => arg1 + arg2 + arg3 + arg4 + arg5;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5));
		}

		[Fact(DisplayName = "Catch<T1, ..., T6, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch6Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;

			Func<int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6) => arg1 + arg2 + arg3 + arg4 + arg5 + arg6;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6));
		}

		[Fact(DisplayName = "Catch<T1, ..., T7, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch7Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;

			Func<int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7) => arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7));
		}

		[Fact(DisplayName = "Catch<T1, ..., T8, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch8Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;

			Func<int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8));
		}

		[Fact(DisplayName = "Catch<T1, ..., T9, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch9Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;

			Func<int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9));
		}

		[Fact(DisplayName = "Catch<T1, ..., T10, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch10Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;

			Func<int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10));
		}

		[Fact(DisplayName = "Catch<T1, ..., T10, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch11Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;
			const int x11 = 11;

			Func<int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10 + arg11;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11));
		}

		[Fact(DisplayName = "Catch<T1, ..., T12, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch12Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;
			const int x11 = 11;
			const int x12 = 12;

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10 + arg11 + arg12;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12) is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12));
		}

		[Fact(DisplayName = "Catch<T1, ..., T13, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch13Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;
			const int x11 = 11;
			const int x12 = 12;
			const int x13 = 13;

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10 + arg11 + arg12 + arg13;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13)
					is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13));
		}

		[Fact(DisplayName = "Catch<T1, ..., T14, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch14Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;
			const int x11 = 11;
			const int x12 = 12;
			const int x13 = 13;
			const int x14 = 14;

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10 +
					   arg11 + arg12 + arg13 + arg14;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14)
					is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14));
		}

		[Fact(DisplayName = "Catch<T1, ..., T15, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch15Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;
			const int x11 = 11;
			const int x12 = 12;
			const int x13 = 13;
			const int x14 = 14;
			const int x15 = 14;

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10 +
					   arg11 + arg12 + arg13 + arg14 + arg15;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15)
					is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15));
		}

		[Fact(DisplayName = "Catch<T1, ..., T16, TSuccess> returns a success if there are no exceptions")]
		public void TestCatch16Result()
		{
			const int x1 = 1;
			const int x2 = 2;
			const int x3 = 3;
			const int x4 = 4;
			const int x5 = 5;
			const int x6 = 6;
			const int x7 = 7;
			const int x8 = 8;
			const int x9 = 9;
			const int x10 = 10;
			const int x11 = 11;
			const int x12 = 12;
			const int x13 = 13;
			const int x14 = 14;
			const int x15 = 14;
			const int x16 = 14;

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)
					=> arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10 +
					   arg11 + arg12 + arg13 + arg14 + arg15 + arg16;

			Assert.True(
				func.Catch()(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16)
					is Success<int, Exception> success &&
				success.Value == func(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16));
		}

		[Fact(DisplayName = "Catch<TSuccess> returns a failure if there was an exception")]
		public void TestCatch0Exception()
		{
			var exp = new Exception();

			Func<int> func = () => throw exp;

			Assert.True(
				func.Catch()() is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, TSuccess> returns a failure if there was an exception")]
		public void TestCatch1Exception()
		{
			var exp = new Exception();

			Func<int, int> func = arg => throw exp;

			Assert.True(
				func.Catch()(1) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, T2, TSuccess> returns a failure if there was an exception")]
		public void TestCatch2Exception()
		{
			var exp = new Exception();

			Func<int, int, int> func = (arg1, arg2) => throw exp;

			Assert.True(
				func.Catch()(1, 2) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, T2, T3, TSuccess> returns a failure if there was an exception")]
		public void TestCatch3Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int> func = (arg1, arg2, arg3) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T4, TSuccess> returns a failure if there was an exception")]
		public void TestCatch4Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int> func = (arg1, arg2, arg3, arg4) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T5, TSuccess> returns a failure if there was an exception")]
		public void TestCatch5Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int> func = (arg1, arg2, arg3, arg4, arg5) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T6, TSuccess> returns a failure if there was an exception")]
		public void TestCatch6Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int> func = (arg1, arg2, arg3, arg4, arg5, arg6) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T7, TSuccess> returns a failure if there was an exception")]
		public void TestCatch7Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T8, TSuccess> returns a failure if there was an exception")]
		public void TestCatch8Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T9, TSuccess> returns a failure if there was an exception")]
		public void TestCatch9Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T10, TSuccess> returns a failure if there was an exception")]
		public void TestCatch10Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T11, TSuccess> returns a failure if there was an exception")]
		public void TestCatch11Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T12, TSuccess> returns a failure if there was an exception")]
		public void TestCatch12Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T13, TSuccess> returns a failure if there was an exception")]
		public void TestCatch13Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T14, TSuccess> returns a failure if there was an exception")]
		public void TestCatch14Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T15, TSuccess> returns a failure if there was an exception")]
		public void TestCatch15Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)
					=> throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15) is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<T1, ..., T16, TSuccess> returns a failure if there was an exception")]
		public void TestCatch16Exception()
		{
			var exp = new Exception();

			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)
					=> throw exp;

			Assert.True(
				func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)
					is Failure<int, Exception> failure &&
				failure.Errors.Count == 1 &&
				failure.Errors[0].Equals(exp));
		}

		[Fact(DisplayName = "Catch<TSuccess> throws an exception if the function is null")]
		public void TestCatch0Null()
		{
			Func<int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, TSuccess> throws an exception if the function is null")]
		public void TestCatch1Null()
		{
			Func<int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, T2, TSuccess> throws an exception if the function is null")]
		public void TestCatch2Null()
		{
			Func<int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, T2, T3, TSuccess> throws an exception if the function is null")]
		public void TestCatch3Null()
		{
			Func<int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T4, TSuccess> throws an exception if the function is null")]
		public void TestCatch4Null()
		{
			Func<int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T5, TSuccess> throws an exception if the function is null")]
		public void TestCatch5Null()
		{
			Func<int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T6, TSuccess> throws an exception if the function is null")]
		public void TestCatch6Null()
		{
			Func<int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T7, TSuccess> throws an exception if the function is null")]
		public void TestCatch7Null()
		{
			Func<int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T8, TSuccess> throws an exception if the function is null")]
		public void TestCatch8Null()
		{
			Func<int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T9, TSuccess> throws an exception if the function is null")]
		public void TestCatch9Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T10, TSuccess> throws an exception if the function is null")]
		public void TestCatch10Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T11, TSuccess> throws an exception if the function is null")]
		public void TestCatch11Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T12, TSuccess> throws an exception if the function is null")]
		public void TestCatch12Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T13, TSuccess> throws an exception if the function is null")]
		public void TestCatch13Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T14, TSuccess> throws an exception if the function is null")]
		public void TestCatch14Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}
		
		[Fact(DisplayName = "Catch<T1, ..., T15, TSuccess> throws an exception if the function is null")]
		public void TestCatch15Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<T1, ..., T16, TSuccess> throws an exception if the function is null")]
		public void TestCatch16Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Catch());
		}

		[Fact(DisplayName = "Catch<TSuccess> throws an exception if the function returns null")]
		public void TestCatch0ReturnNull()
		{
			Func<object> func = () => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()());
		}

		[Fact(DisplayName = "Catch<T1, TSuccess> throws an exception if the function returns null")]
		public void TestCatch1ReturnNull()
		{
			Func<int, object> func = arg => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1));
		}

		[Fact(DisplayName = "Catch<T1, T2, TSuccess> throws an exception if the function returns null")]
		public void TestCatch2ReturnNull()
		{
			Func<int, int, object> func = (arg1, arg2) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2));
		}

		[Fact(DisplayName = "Catch<T1, T2, T3, TSuccess> throws an exception if the function returns null")]
		public void TestCatch3ReturnNull()
		{
			Func<int, int, int, object> func = (arg1, arg2, arg3) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3));
		}

		[Fact(DisplayName = "Catch<T1, ..., T4, TSuccess> throws an exception if the function returns null")]
		public void TestCatch4ReturnNull()
		{
			Func<int, int, int, int, object> func = (arg1, arg2, arg3, arg4) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4));
		}

		[Fact(DisplayName = "Catch<T1, ..., T5, TSuccess> throws an exception if the function returns null")]
		public void TestCatch5ReturnNull()
		{
			Func<int, int, int, int, int, object> func = (arg1, arg2, arg3, arg4, arg5) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5));
		}

		[Fact(DisplayName = "Catch<T1, ..., T6, TSuccess> throws an exception if the function returns null")]
		public void TestCatch6ReturnNull()
		{
			Func<int, int, int, int, int, int, object> func = (arg1, arg2, arg3, arg4, arg5, arg6) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6));
		}

		[Fact(DisplayName = "Catch<T1, ..., T7, TSuccess> throws an exception if the function returns null")]
		public void TestCatch7ReturnNull()
		{
			Func<int, int, int, int, int, int, int, object> func = (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7));
		}

		[Fact(DisplayName = "Catch<T1, ..., T8, TSuccess> throws an exception if the function returns null")]
		public void TestCatch8ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8));
		}

		[Fact(DisplayName = "Catch<T1, ..., T9, TSuccess> throws an exception if the function returns null")]
		public void TestCatch9ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9));
		}

		[Fact(DisplayName = "Catch<T1, ..., T10, TSuccess> throws an exception if the function returns null")]
		public void TestCatch10ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
		}

		[Fact(DisplayName = "Catch<T1, ..., T11, TSuccess> throws an exception if the function returns null")]
		public void TestCatch11ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11));
		}

		[Fact(DisplayName = "Catch<T1, ..., T12, TSuccess> throws an exception if the function returns null")]
		public void TestCatch12ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12));
		}

		[Fact(DisplayName = "Catch<T1, ..., T13, TSuccess> throws an exception if the function returns null")]
		public void TestCatch13ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => null;
			Assert.Throws<UnacceptableNullException>(() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13));
		}

		[Fact(DisplayName = "Catch<T1, ..., T14, TSuccess> throws an exception if the function returns null")]
		public void TestCatch14ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => null;
			Assert.Throws<UnacceptableNullException>(
				() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14));
		}

		[Fact(DisplayName = "Catch<T1, ..., T15, TSuccess> throws an exception if the function returns null")]
		public void TestCatch15ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15)
					=> null;
			Assert.Throws<UnacceptableNullException>(
				() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15));
		}

		[Fact(DisplayName = "Catch<T1, ..., T16, TSuccess> throws an exception if the function returns null")]
		public void TestCatch16ReturnNull()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, object> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16)
					=> null;
			Assert.Throws<UnacceptableNullException>(
				() => func.Catch()(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16));
		}
	}
}
