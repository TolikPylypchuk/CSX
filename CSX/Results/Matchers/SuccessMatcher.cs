using System;

using CSX.Collections;

namespace CSX.Results.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when <see cref="Failure{TSuccess, TError}" />
	/// is already matched.
	/// </summary>
	/// <typeparam name="TSuccess">The type of the successful result.</typeparam>
	/// <typeparam name="TError">The type of the failed result.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	/// <seealso cref="FailureMatcher{TSuccess, TError, TResult}" />
	/// <seealso cref="Result{TSuccess, TError}" />
	/// <seealso cref="Success{TSuccess, TError}" />
	/// <seealso cref="Failure{TSuccess, TError}" />
	public class SuccessMatcher<TSuccess, TError, TResult>
	{
		/// <summary>
		/// The value to provide to the function.
		/// </summary>
		private readonly TSuccess value;
		
		/// <summary>
		/// The errors to provide to the function.
		/// </summary>
		private readonly ConsList<TError> errors;

		/// <summary>
		/// Indicates whether the result is a success.
		/// </summary>
		private readonly bool isSuccess;

		/// <summary>
		/// The function that is executed when the result is a failure.
		/// </summary>
		private readonly Func<ConsList<TError>, TResult> funcIfFailure;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SuccessMatcher{TSuccess, TError, TResult}" /> class.
		/// </summary>
		/// <param name="value">The value to provide to the function.</param>
		/// <param name="funcIfFailure">
		/// The function that is executed when the result is a failure.
		/// </param>
		internal SuccessMatcher(
			TSuccess value,
			Func<ConsList<TError>, TResult> funcIfFailure)
		{
			this.value = value;
			this.errors = null;
			this.isSuccess = true;
			this.funcIfFailure = funcIfFailure;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SuccessMatcher{TSuccess, TError, TResult}" /> class.
		/// </summary>
		/// <param name="errors">The errors to provide to the function.</param>
		/// <param name="funcIfFailure">
		/// The function that is executed when the result is a failure.
		/// </param>
		internal SuccessMatcher(
			ConsList<TError> errors,
			Func<ConsList<TError>, TResult> funcIfFailure)
		{
			this.value = default;
			this.errors = errors;
			this.isSuccess = false;
			this.funcIfFailure = funcIfFailure;
		}

		/// <summary>
		/// Returns the result of the specified function if this result is
		/// a <see cref="Success{TSuccess, TError}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this result is a <see cref="Failure{TSuccess, TError}" />,
		/// then the result of the function, provided to the
		/// <see cref="Failure{TSuccess, TError}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchSuccess(Func<TSuccess, TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return this.isSuccess
				? func(this.value)
				: this.funcIfFailure(this.errors);
		}

		/// <summary>
		/// Returns a result of the specified function if all other matches failed.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <returns>The result of <paramref name="func" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchAny(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return this.isSuccess
				? func()
				: this.funcIfFailure(this.errors);
		}
	}
}
