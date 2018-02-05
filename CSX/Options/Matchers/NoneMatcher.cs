using System;

namespace CSX.Options.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when <see cref="Some{T}" /> is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the option.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	/// <seealso cref="SomeMatcher{TValue, TResult}" />
	/// <seealso cref="Option{T}" />
	/// <seealso cref="Some{T}" />
	/// <seealso cref="None{T}" />
	public class NoneMatcher<TValue, TResult>
	{
		/// <summary>
		/// The option to match against.
		/// </summary>
		private readonly Option<TValue> option;

		/// <summary>
		/// The function that is executed when the value is present.
		/// </summary>
		private readonly Func<TValue, TResult> funcIfSome;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="NoneMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="option">The option to match against.</param>
		/// <param name="funcIfSome">
		/// The function that is executed when the value is present.
		/// </param>
		internal NoneMatcher(Option<TValue> option, Func<TValue, TResult> funcIfSome)
		{
			this.option = option;
			this.funcIfSome = funcIfSome;
		}

		/// <summary>
		/// Returns the result of the specified function if this option is
		/// <see cref="None{T}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this option is <see cref="Some{T}" />, then the result of the function,
		/// provided to the <see cref="Some{T}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public TResult MatchNone(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public TResult MatchAny(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return this.MatchNone(func);
		}
	}
}
