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
		/// The value to provide to the function.
		/// </summary>
		private readonly TValue value;

		/// <summary>
		/// Indicates whether the value is present.
		/// </summary>
		private readonly bool isValuePresent;

		/// <summary>
		/// The function that is executed when the value is absent.
		/// </summary>
		private readonly Func<TResult> funcIfNone;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SomeMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="value">The value to provide to the function.</param>
		/// <param name="funcIfNone">
		/// The function that is executed when the value is absent.
		/// </param>
		internal SomeMatcher(TValue value, Func<TResult> funcIfNone)
		{
			this.value = value;
			this.isValuePresent = true;
			this.funcIfNone = funcIfNone;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SomeMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="funcIfNone">
		/// The function that is executed when the value is absent.
		/// </param>
		internal SomeMatcher(Func<TResult> funcIfNone)
		{
			this.value = default;
			this.isValuePresent = false;
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

			return this.isValuePresent
				? func(this.value)
				: this.funcIfNone();
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

			return this.isValuePresent
				? func()
				: this.funcIfNone();
		}
	}
}
