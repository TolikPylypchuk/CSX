using System;

using CSX.Lists;

namespace CSX.Results.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when Success is already matched.
	/// </summary>
	/// <typeparam name="TSuccess">The type of the successful result.</typeparam>
	/// <typeparam name="TError">The type of the failed result.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	/// <seealso cref="SuccessMatcher{TSuccess, TError, TResult}" />
	/// <seealso cref="Result{TSuccess, TError}" />
	/// <seealso cref="Success{TSuccess, TError}" />
	/// <seealso cref="Failure{TSuccess, TError}" />
	public class FailureMatcher<TSuccess, TError, TResult>
	{
		/// <summary>
		/// The result to match against.
		/// </summary>
		private readonly Result<TSuccess, TError> result;

		/// <summary>
		/// The function that is executed when the result is a success.
		/// </summary>
		private readonly Func<TSuccess, TResult> funcIfSuccess;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="FailureMatcher{TSuccess, TError, TResult}" /> class.
		/// </summary>
		/// <param name="result">The result to match against.</param>
		/// <param name="funcIfFailure">
		/// The function that is executed when the result is a success.
		/// </param>
		internal FailureMatcher(
			Result<TSuccess, TError> result,
			Func<TSuccess, TResult> funcIfFailure)
		{
			this.result = result;
			this.funcIfSuccess = funcIfFailure;
		}

		/// <summary>
		/// Returns the result of the specified function if this result is
		/// a <see cref="Failure{TSuccess, TError}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this result is a <see cref="Success{TSuccess, TError}" />,
		/// then the result of the function, provided to the
		/// <see cref="Success{TSuccess, TError}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		public TResult MatchFailure(Func<ConsList<TError>, TResult> func)
		{
			switch (this.result)
			{
				case Success<TSuccess, TError> success:
					return this.funcIfSuccess(success.Value);
				case Failure<TSuccess, TError> failure:
					return func(failure.Errors);
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
			foreach (var value in this.result)
			{
				return this.funcIfSuccess(value);
			}

			return func();
		}
	}
}
