using System;

namespace CSX.Options.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when Some is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the option.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	public class NoneMatcher<TValue, TResult>
	{
		private readonly Option<TValue> option;
		private readonly Func<TValue, TResult> funcIfSome;

		internal NoneMatcher(Option<TValue> option, Func<TValue, TResult> funcIfSome)
		{
			this.option = option;
			this.funcIfSome = funcIfSome;
		}

		/// <summary>
		/// Returns the result of the specified function if this option is None.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this option is Some, then the result of the function,
		/// provided to the Some matcher. Otherwise, the result of the specified function.
		/// </returns>
		public TResult MatchNone(Func<TResult> func)
		{
			foreach (var value in this.option)
			{
				return this.funcIfSome(value);
			}

			return func();
		}

		/// <summary>
		/// Returns a result of the specified function if all other matches failed.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <returns>The result of <paramref name="func" />.</returns>
		public TResult MatchAny(Func<TResult> func)
			=> this.MatchNone(func);
	}
}
