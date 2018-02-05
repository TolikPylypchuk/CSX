using System;

namespace CSX.Collections.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when <see cref="ConsCell{T}" /> is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the list.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	/// <seealso cref="ConsCellMatcher{TValue, TResult}" />
	/// <seealso cref="ConsList{T}" />
	/// <seealso cref="ConsCell{T}" />
	/// <seealso cref="Empty{T}" />
	public class EmptyMatcher<TValue, TResult>
	{
		/// <summary>
		/// The list to match against.
		/// </summary>
		private readonly ConsList<TValue> list;

		/// <summary>
		/// The function that is executed if this list is a cons cell.
		/// </summary>
		private readonly Func<TValue, ConsList<TValue>, TResult> funcIfConsCell;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="EmptyMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="list">The list to match against.</param>
		/// <param name="funcIfConsCell">
		/// The function that is executed if this list is a cons cell.
		/// </param>
		internal EmptyMatcher(
			ConsList<TValue> list,
			Func<TValue, ConsList<TValue>, TResult> funcIfConsCell)
		{
			this.list = list;
			this.funcIfConsCell = funcIfConsCell;
		}

		/// <summary>
		/// Returns the result of the specified function if this list is empty.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this list is <see cref="ConsCell{T}" />, then the result of the function,
		/// provided to the <see cref="ConsCell{T}" /> matcher.
		/// Otherwise, the result of <paramref name="func" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public TResult MatchEmpty(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			switch (this.list)
			{
				case ConsCell<TValue> cell:
					return this.funcIfConsCell(cell.Head, cell.Tail);
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

			switch (this.list)
			{
				case ConsCell<TValue> cell:
					return this.funcIfConsCell(cell.Head, cell.Tail);
			}

			return func();
		}
	}
}
