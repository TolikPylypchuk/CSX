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
		/// The result to match against.
		/// </summary>
		private readonly Result<TSuccess, TError> result;

		/// <summary>
		/// The function that is executed when the result is a failure.
		/// </summary>
		private readonly Func<ConsList<TError>, TResult> funcIfFailure;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SuccessMatcher{TSuccess, TError, TResult}" /> class.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="funcIfFailure"></param>
		internal SuccessMatcher(
			Result<TSuccess, TError> result,
			Func<ConsList<TError>, TResult> funcIfFailure)
		{
			this.result = result;
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
		public TResult MatchSuccess(Func<TSuccess, TResult> func)
		{
			switch (this.result)
			{
				case Success<TSuccess, TError> success:
					return func(success.Value);
				case Failure<TSuccess, TError> failure:
					return this.funcIfFailure(failure.Errors);
			}

			return default;
		}

		/// <summary>
		/// Returns a result of the specified function if all other matches failed.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <returns>The result of <paramref name="func" />.</returns>
		public TResult MatchAny(Func<TResult> func)
		{
			switch (this.result)
			{
				case Failure<TSuccess, TError> failure:
					return this.funcIfFailure(failure.Errors);
			}

			return func();
		}
	}
}
