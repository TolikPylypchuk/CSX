using System;

namespace CSX.Options.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when None is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the option.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	public class SomeMatcher<TValue, TResult>
	{
		private readonly Option<TValue> option;
		private readonly Func<TResult> funcIfNone;

		internal SomeMatcher(Option<TValue> option, Func<TResult> funcIfNone)
		{
			this.option = option;
			this.funcIfNone = funcIfNone;
		}

		/// <summary>
		/// Returns the result of the specified function if this option is Some.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this option is None, then the result of the function,
		/// provided to the None matcher. Otherwise, the result of the specified function.
		/// </returns>
		public TResult MatchSome(Func<TValue, TResult> func)
		{
			foreach (var value in this.option)
			{
				return func(value);
			}

			return this.funcIfNone();
		}

		/// <summary>
		/// Returns a result of the specified function if all other matches failed.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <returns>The result of <paramref name="func" />.</returns>
		public TResult MatchAny(Func<TResult> func)
		{
			foreach (var _ in this.option)
			{
				return func();
			}

			return this.funcIfNone();
		}
	}
}
