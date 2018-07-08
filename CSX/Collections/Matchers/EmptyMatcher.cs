using System;

namespace CSX.Collections.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when a cons cell is already matched.
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
		/// The cell to provide to the function.
		/// </summary>
		private readonly ConsCell<TValue> cell;

		/// <summary>
		/// Indicates whether the cell is present.
		/// </summary>
		private readonly bool isCellPresent;

		/// <summary>
		/// The function that is executed if this list is a cons cell.
		/// </summary>
		private readonly Func<TValue, ConsList<TValue>, TResult> funcIfConsCell;

		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="cell">The cell to provide to the function.</param>
		/// <param name="funcIfConsCell">The function that is executed if this list is a cons cell.</param>
		internal EmptyMatcher(ConsCell<TValue> cell, Func<TValue, ConsList<TValue>, TResult> funcIfConsCell)
		{
			this.cell = cell;
			this.isCellPresent = true;
			this.funcIfConsCell = funcIfConsCell;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="funcIfConsCell">The function that is executed if this list is a cons cell.</param>
		internal EmptyMatcher(Func<TValue, ConsList<TValue>, TResult> funcIfConsCell)
		{
			this.cell = null;
			this.isCellPresent = default;
			this.funcIfConsCell = funcIfConsCell;
		}

		/// <summary>
		/// Returns the result of the specified function if this list is empty.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <returns>
		/// If this list is a cons cell, then the result of the function, provided to the cons cell matcher.
		/// Otherwise, the result of <paramref name="func" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchEmpty(Func<TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return this.isCellPresent
				? this.funcIfConsCell(this.cell.Head, this.cell.Tail)
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

			return this.isCellPresent
				? this.funcIfConsCell(this.cell.Head, this.cell.Tail)
				: func();
		}
	}
}
