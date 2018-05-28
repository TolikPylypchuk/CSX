using System;
using System.Collections.Generic;
using CSX.Collections.Matchers;

namespace CSX.Collections
{
	/// <summary>
	/// Represents an empty cons list.
	/// </summary>
	/// <typeparam name="T">The type of the elements of this list.</typeparam>
	/// <seealso cref="ConsList{T}" />
	/// <seealso cref="ConsCell{T}" />
	public class Empty<T> : ConsList<T>, IEquatable<Empty<T>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Empty{T}" /> class.
		/// </summary>
		internal Empty() { }

		/// <summary>
		/// Gets the number of items in this list, which is 0.
		/// </summary>
		public override int Count
			=> 0;

		/// <summary>
		/// Returns the <paramref name="other" /> list.
		/// </summary>
		/// <param name="other">The list to add.</param>
		/// <returns>The <paramref name="other" /> list.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="other" /> is <see langword="null" />.
		/// </exception>
		public override ConsList<T> Add(ConsList<T> other)
			=> other ?? throw new ArgumentNullException(nameof(other));

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>An empty list of type <typeparamref name="V" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="FlatMap{V}(Func{T, ConsList{V}})" />
		public override ConsList<V> Map<V>(Func<T, V> func)
			=> func != null
				? ConsList.Empty<V>()
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>An empty list of type <typeparamref name="V" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{V}(Func{T, V})" />
		public override ConsList<V> FlatMap<V>(Func<T, ConsList<V>> func)
			=> func != null
				? ConsList.Empty<V>()
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the matcher which will return the result of another function.
		/// </summary>
		/// <param name="func">Not used.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// The matcher which will return the result of another function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchEmpty{TResult}(Func{TResult})" />
		/// <seealso cref="ConsList{T}.MatchAny{TResult}(Func{TResult})" />
		public override EmptyMatcher<T, TResult> MatchConsCell<TResult>(
			Func<T, ConsList<T>, TResult> func)
			=> func != null
				? new EmptyMatcher<T, TResult>(func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the matcher which will return the result of the specified function.
		/// </summary>
		/// <param name="func">The function whose result will be returned.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// The matcher which will return the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchConsCell{TResult}(Func{T, ConsList{T}, TResult})" />
		/// <seealso cref="ConsList{T}.MatchAny{TResult}(Func{TResult})" />
		public override ConsCellMatcher<T, TResult> MatchEmpty<TResult>(Func<TResult> func)
			=> func != null
				? new ConsCellMatcher<T, TResult>(func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="action">Not used.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfEmpty(Action)" />
		public override ConsList<T> DoIfConsCell(Action<T, ConsList<T>> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Executes a specified action.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfConsCell(Action{T, ConsList{T}})" />
		public override ConsList<T> DoIfEmpty(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			action();
			return this;
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="action">Not used.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public override ConsList<T> ForEach(Action<T> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Folds this list to a single value from left to right,
		/// i.e. just returns the seed.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The return value. May be <see langword="null" />.</param>
		/// <param name="func">Not used.</param>
		/// <returns><paramref name="seed" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="FoldBack{V}(V, Func{T, V, V})" />
		public override V Fold<V>(V seed, Func<V, T, V> func)
			=> func != null ? seed : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Folds this list to a single value from right to left,
		/// i.e. just returns the seed.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The return value. May be <see langword="null" />.</param>
		/// <param name="func">Not used.</param>
		/// <returns><paramref name="seed" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fold{V}(V, Func{V, T, V})" />
		public override V FoldBack<V>(V seed, Func<T, V, V> func)
			=> func != null ? seed : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Always returns <see langword="false" />.
		/// </summary>
		/// <param name="item">Not used.</param>
		/// <returns><see langword="false" /></returns>
		public override bool Contains(T item)
			=> false;

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="array">Not used.</param>
		/// <param name="arrayIndex">Not used.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="arrayIndex" /> is less than 0.
		/// </exception>
		public override void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			}
		}

		/// <summary>
		/// Returns an empty enumerator.
		/// </summary>
		/// <returns>An empty enumerator.</returns>
		public override IEnumerator<T> GetEnumerator()
			=> EmptyEnumerator<T>.Instance;

		/// <summary>
		/// Checks whether this list equals another list, i.e.
		/// whether the <paramref name="other" /> list is also empty.
		/// The other list may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <see langword="null" />.</param>
		/// <returns>
		/// Returns <see langword="true" /> if the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />. Otherwise, returns <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="Equals(Empty{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(object other)
			=> other is Empty<T>;

		/// <summary>
		/// Checks whether this list equals another list, i.e.
		/// whether the <paramref name="other" /> list is also empty.
		/// The other list may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <see langword="null" />.</param>
		/// <returns>
		/// Returns <see langword="true" /> if the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />. Otherwise, returns <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Empty{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(ConsList<T> other)
			=> other is Empty<T>;

		/// <summary>
		/// Checks whether this list equals another list.
		/// The other list may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <see langword="null" />.</param>
		/// <returns>
		/// <see langword="true" /> if <paramref name="other" /> isn't <see langword="null" />.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="GetHashCode" />
		public bool Equals(Empty<T> other)
			=> other != null;

		/// <summary>
		/// Returns 1.
		/// </summary>
		/// <returns>1</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="Equals(Empty{T})" />
		public override int GetHashCode()
			=> 1;

		/// <summary>
		/// Returns an empty string.
		/// </summary>
		/// <returns>An empty string.</returns>
		public override string ToString()
			=> String.Empty;

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="array">Not used.</param>
		/// <param name="arrayIndex">Not used.</param>
		internal override void CopyToImpl(T[] array, int arrayIndex) { }

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException" />.
		/// </summary>
		/// <param name="index">Not used.</param>
		/// <param name="currentIndex">Not used.</param>
		/// <returns>Nothing.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown unconditionally.
		/// </exception>
		internal override T GetItemImpl(int index, int currentIndex)
			=> throw new ArgumentOutOfRangeException(nameof(index));
		
		/// <summary>
		/// Always returns -1.
		/// </summary>
		/// <param name="item">Not used.</param>
		/// <param name="currentIndex">Not used.</param>
		/// <returns>-1</returns>
		internal override int IndexOfImpl(T item, int currentIndex)
			=> -1;
	}
}
