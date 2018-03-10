using System;
using System.Collections.Generic;
using CSX.Collections;
using Xunit;

using static CSX.Results.Result;

namespace CSX.Results
{
	public class ResultTests
	{
		[Fact(DisplayName = "Succeed<TSuccess, TError> returns Success which contains the value")]
		public void TestSucceed()
		{
			const int value = 1;
			var result = Succeed<int, int>(value);
			Assert.True(result is Success<int, int> success && value == success.Value);
		}

		[Fact(DisplayName = "Succeed<TSuccess> returns Success which contains the value")]
		public void TestSucceedString()
		{
			const int value = 1;
			var result = Succeed(value);
			Assert.True(result is Success<int, string> success && value == success.Value);
		}

		[Fact(DisplayName =
			"ToSuccess<TSuccess, TError> returns Success which contains the value")]
		public void TestToSuccess()
		{
			const int value = 1;
			var result = value.ToSuccess<int, int>();
			Assert.True(result is Success<int, int> success && value == success.Value);
		}

		[Fact(DisplayName = "ToSuccess<TSuccess> returns Success which contains the value")]
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
		
		[Fact(DisplayName = "Fail<TSuccess, TError> returns Failure which contains the error")]
		public void TestFail()
		{
			const int error = 1;
			var result = Fail<int, int>(error);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 1 &&
				error == failure.Errors[0]);
		}

		[Fact(DisplayName = "Fail<TSuccess> returns Failure which contains the error")]
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
			"Fail<TSuccess, TError> returns Failure which contains the errors list")]
		public void TestFailConsList()
		{
			var errors = ConsList.Construct(1, 2);
			var result = Fail<int, int>(errors);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns Failure which contains the errors list")]
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
			"Fail<TSuccess, TError> returns Failure which contains the errors")]
		public void TestFailIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = Fail<int, int>(errors);
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "Fail<TSuccess> returns Failure which contains the errors")]
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
			"ToFailure<TSuccess, TError> returns Failure which contains the error")]
		public void TestToFailure()
		{
			const int error = 1;
			var result = error.ToFailure<int, int>();
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 1 &&
				error == failure.Errors[0]);
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns Failure which contains the error")]
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
			"ToFailure<TSuccess, TError> returns Failure which contains the errors list")]
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
			"ToFailure<TSuccess> returns Failure which contains the errors list")]
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
			"ToFailure<TSuccess, TError> returns Failure which contains the errors")]
		public void TestToFailureIEnumerable()
		{
			IEnumerable<int> errors = ConsList.Construct(1, 2);
			var result = errors.ToFailure<int, int>();
			Assert.True(
				result is Failure<int, int> failure &&
				failure.Errors.Count == 2 &&
				errors.Equals(failure.Errors));
		}

		[Fact(DisplayName = "ToFailure<TSuccess> returns Failure which contains the errors")]
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
	}
}
