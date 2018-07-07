﻿using System;

namespace CSX.Collections.Matchers
{
	/// <summary>
	/// Represents a matcher that is used when <see cref="Empty{T}" /> is already matched.
	/// </summary>
	/// <typeparam name="TValue">The type of the value of the list.</typeparam>
	/// <typeparam name="TResult">The type of the match result.</typeparam>
	/// <seealso cref="EmptyMatcher{TValue, TResult}" />
	/// <seealso cref="ConsList{T}" />
	/// <seealso cref="ConsCell{T}" />
	/// <seealso cref="Empty{T}" />
	public class ConsCellMatcher<TValue, TResult>
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
		/// The function that is executed if this list is empty.
		/// </summary>
		private readonly Func<TResult> funcIfEmpty;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ConsCellMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="cell">The cell to provide to the function.</param>
		/// <param name="funcIfEmpty">The function that is executed if this list is empty.</param>
		internal ConsCellMatcher(ConsCell<TValue> cell, Func<TResult> funcIfEmpty)
		{
			this.cell = cell;
			this.isCellPresent = true;
			this.funcIfEmpty = funcIfEmpty;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ConsCellMatcher{TValue, TResult}" /> class.
		/// </summary>
		/// <param name="funcIfEmpty">The function that is executed if this list is empty.</param>
		internal ConsCellMatcher(Func<TResult> funcIfEmpty)
		{
			this.cell = null;
			this.isCellPresent = false;
			this.funcIfEmpty = funcIfEmpty;
		}

		/// <summary>
		/// Returns the result of the specified function if this list is a cons cell.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <returns>
		/// If this list is <see cref="Empty{T}" />, then the result of the function, provided to the
		/// <see cref="Empty{T}" /> matcher. Otherwise, the result of <paramref name="func" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public TResult MatchConsCell(Func<TValue, ConsList<TValue>, TResult> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return this.isCellPresent
				? func(this.cell.Head, this.cell.Tail)
				: this.funcIfEmpty();
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
				? func()
				: this.funcIfEmpty();
		}
	}
}
