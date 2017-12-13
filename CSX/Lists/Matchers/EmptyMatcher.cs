using System;

namespace CSX.Lists.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when ConsCell is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the list.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	public class EmptyMatcher<TValue, TResult>
	{
		private readonly ConsList<TValue> list;
		private readonly Func<TValue, ConsList<TValue>, TResult> funcIfConsCell;

		internal EmptyMatcher(
			ConsList<TValue> option,
			Func<TValue, ConsList<TValue>, TResult> funcIfConsCell)
		{
			this.list = option;
			this.funcIfConsCell = funcIfConsCell;
		}

		/// <summary>
		/// Returns the result of the specified function if this list is a cons cell.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <returns>
		/// If this list is ConsCell, then the result of the function,
		/// provided to the ConsCell matcher. Otherwise, the result of the specified function.
		/// </returns>
		public TResult MatchEmpty(Func<TResult> func)
		{
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
		public TResult MatchAny(Func<TResult> func)
		{
			switch (this.list)
			{
				case ConsCell<TValue> cell:
					return this.funcIfConsCell(cell.Head, cell.Tail);
			}

			return func();
		}
	}
}
