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
	public class FailureMatcher<TSuccess, TError, TResult>
	{
		private readonly Result<TSuccess, TError> result;
		private readonly Func<TSuccess, TResult> funcIfSuccess;

		internal FailureMatcher(
			Result<TSuccess, TError> result,
			Func<TSuccess, TResult> funcIfFailure)
		{
			this.result = result;
			this.funcIfSuccess = funcIfFailure;
		}

		/// <summary>
		/// Returns the result of the specified function if this result is failed.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this result is successful, then the result of the function,
		/// provided to the Success matcher. Otherwise, the result of the specified function.
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
			switch (this.result)
			{
				case Success<TSuccess, TError> success:
					return this.funcIfSuccess(success.Value);
			}

			return func();
		}
	}
}
