using System;
using System.Collections.Generic;

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
		/// Returns the <paramref name="other" /> list.
		/// </summary>
		/// <param name="other">The list to add.</param>
		/// <returns>The <paramref name="other" /> list.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="other" /> is <c>null</c>.
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
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
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
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public override ConsList<V> FlatMap<V>(Func<T, ConsList<V>> func)
			=> func != null
				? ConsList.Empty<V>()
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="action">Not used.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfEmpty(Action)" />
		public override ConsList<T> DoIfConsCell(Action<T, ConsList<T>> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Executes a specified action.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
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
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		public override ConsList<T> ForEach(Action<T> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Folds this list to a single value from left to right,
		/// i.e. just returns the seed.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The return value. May be <c>null</c>.</param>
		/// <param name="func">Not used.</param>
		/// <returns><paramref name="seed" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="FoldBack{V}(V, Func{T, V, V})" />
		public override V Fold<V>(V seed, Func<V, T, V> func)
			=> func != null ? seed : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Folds this list to a single value from right to left,
		/// i.e. just returns the seed.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The return value. May be <c>null</c>.</param>
		/// <param name="func">Not used.</param>
		/// <returns><paramref name="seed" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Fold{V}(V, Func{V, T, V})" />
		public override V FoldBack<V>(V seed, Func<T, V, V> func)
			=> func != null ? seed : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns an empty enumerator.
		/// </summary>
		/// <returns>An empty enumerator.</returns>
		public override IEnumerator<T> GetEnumerator()
			=> EmptyEnumerator<T>.Instance;

		/// <summary>
		/// Checks whether this list equals another list, i.e.
		/// whether the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <c>null</c>.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		public override bool Equals(object other)
			=> other is Empty<T>;

		/// <summary>
		/// Checks whether this list equals another list, i.e.
		/// whether the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <c>null</c>.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		public override bool Equals(ConsList<T> other)
			=> other is Empty<T>;

		/// <summary>
		/// Checks whether this list equals another list and always returns <c>true</c>.
		/// </summary>
		/// <param name="other">The list to compare to. May be <c>null</c>.</param>
		/// <returns><c>true</c></returns>
		public bool Equals(Empty<T> other)
			=> true;

		/// <summary>
		/// Returns <c>1</c>.
		/// </summary>
		/// <returns><c>1</c></returns>
		public override int GetHashCode()
			=> 1;

		/// <summary>
		/// Returns an empty string.
		/// </summary>
		/// <returns>An empty string.</returns>
		public override string ToString()
			=> String.Empty;
	}
}
