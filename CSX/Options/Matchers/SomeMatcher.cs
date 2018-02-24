using System;

namespace CSX.Options.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when <see cref="None{T}" /> is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the option.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	/// <seealso cref="NoneMatcher{TValue, TResult}" />
	/// <seealso cref="Option{T}" />
	/// <seealso cref="Some{T}" />
	/// <seealso cref="None{T}" />
	public class SomeMatcher<TValue, TResult>
	{
		/// <summary>
		/// The option to match against.
		/// </summary>
		private readonly Option<TValue> option;

		/// <summary>
		/// The function that is executed when the value is absent.
		/// </summary>
		private readonly Func<TResult> funcIfNone;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SomeMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="option">The option to match against.</param>
		/// <param name="funcIfNone">
		/// The function that is executed when the value is absent.
		/// </param>
		internal SomeMatcher(Option<TValue> option, Func<TResult> funcIfNone)
		{
			this.option = option;
			this.funcIfNone = funcIfNone;
		}

		/// <summary>
		/// Returns the result of the specified function if this option is
		/// <see cref="Some{T}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this option is <see cref="None{T}" />, then the result of the function,
		/// provided to the <see cref="None{T}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchSome(Func<TValue, TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchAny(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			foreach (var _ in this.option)
			{
				return func();
			}

			return this.funcIfNone();
		}
	}
}
