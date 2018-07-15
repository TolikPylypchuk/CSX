using System;

namespace CSX.Options.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when some is already matched.
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
		/// The value to provide to the function.
		/// </summary>
		private readonly TValue value;

		/// <summary>
		/// Indicates whether the value is present.
		/// </summary>
		private readonly bool isValuePresent;

		/// <summary>
		/// The function that is executed when the value is present.
		/// </summary>
		private readonly Func<TValue, TResult> funcIfSome;

		/// <summary>
		/// Initializes a new instance of the <see cref="NoneMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="value">The value to provide to the function.</param>
		/// <param name="funcIfSome">The function that is executed when the value is present.</param>
		internal NoneMatcher(TValue value, Func<TValue, TResult> funcIfSome)
		{
			this.value = value;
			this.isValuePresent = true;
			this.funcIfSome = funcIfSome;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NoneMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="funcIfSome">The function that is executed when the value is present.</param>
		internal NoneMatcher(Func<TValue, TResult> funcIfSome)
		{
			this.value = default;
			this.isValuePresent = false;
			this.funcIfSome = funcIfSome;
		}

		/// <summary>
		/// Returns the result of the specified function if this option is empty.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <returns>
		/// If this option is present, then the result of the function, provided to the some matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchNone(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return this.isValuePresent
				? this.funcIfSome(this.value)
				: func();
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
				? this.funcIfSome(this.value)
				: func();
		}
	}
}
