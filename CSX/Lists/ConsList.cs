using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CSX.Lists.Matchers;

namespace CSX.Lists
{
	/// <summary>
	/// Represents a list, constructed from cons cells.
	/// </summary>
	/// <typeparam name="T">The type of the values this list holds.</typeparam>
	/// <seealso cref="ConsList" />
	/// <seealso cref="ConsCell{T}" />
	/// <seealso cref="Empty{T}" />
	public abstract class ConsList<T> :
		IEquatable<ConsList<T>>, IEnumerable, IEnumerable<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConsList{T}" /> class.
		/// </summary>
		private protected ConsList() { }

		/// <summary>
		/// Returns a concatenation of this list with another list.
		/// </summary>
		/// <param name="other">The other list.</param>
		/// <returns>A concatenation of this list with another list.</returns>
		public abstract ConsList<T> Add(ConsList<T> other);

		/// <summary>
		/// Applies a specified function to every element of this list
		/// and returns a list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A list consisting of results of the function application.</returns>
		public abstract ConsList<V> Map<V>(Func<T, V> func);

		/// <summary>
		/// Applies a specified function to every element of this list
		/// and returns a flattened list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// A flattened list consisting of results of the function application.
		/// </returns>
		public abstract ConsList<V> Bind<V>(Func<T, ConsList<V>> func);

		/// <summary>
		/// Returns the result of the specified function if this list is a ConsCell.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="V">The type of the match result.</typeparam>
		/// <returns>
		/// If this list is Empty, then the result of the function,
		/// provided to the Empty matcher. Otherwise, the result of the specified function.
		/// </returns>
		public EmptyMatcher<T, V> MatchConsCell<V>(Func<T, ConsList<T>, V> func)
			=> new EmptyMatcher<T, V>(this, func);

		/// <summary>
		/// Returns the result of the specified function if this list is Empty.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="V">The type of the match result.</typeparam>
		/// <returns>
		/// If this list is a ConsCell, then the result of the function,
		/// provided to the ConsCell matcher. Otherwise, the result of the specified function.
		/// </returns>
		public ConsCellMatcher<T, V> MatchEmpty<V>(Func<V> func)
			=> new ConsCellMatcher<T, V>(this, func);

		/// <summary>
		/// Returns a result of the specified function.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <returns>The result of <paramref name="func" />.</returns>
		public V MatchAny<V>(Func<V> func)
			=> func();

		/// <summary>
		/// Applies a specified function to each element of this list.
		/// </summary>
		/// <param name="action">The function to apply.</param>
		/// <returns><c>this</c></returns>
		public abstract ConsList<T> ForEach(Action<T> action);

		/// <summary>
		/// Folds this list to a single value from left to right.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The first parameter of the chain of calls to func.</param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		public abstract V Fold<V>(V seed, Func<V, T, V> func);

		/// <summary>
		/// Folds this list to a single value from right to left.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The first parameter of the chain of calls to func.</param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		public abstract V FoldBack<V>(V seed, Func<T, V, V> func);

		/// <summary>
		/// Gets an enumerator that enumerates every element of this list.
		/// </summary>
		/// <returns>
		/// An enumerator that enumerates every element of this list.
		/// </returns>
		public abstract IEnumerator<T> GetEnumerator();

		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// </summary>
		/// <param name="other">The list to compare to.</param>
		/// <returns>
		/// <c>true</c> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public abstract override bool Equals(object other);

		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// </summary>
		/// <param name="other">The list to compare to.</param>
		/// <returns>
		/// <c>true</c> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public abstract bool Equals(ConsList<T> other);

		/// <summary>
		/// Gets this object's hash code.
		/// </summary>
		/// <returns>This object's hash code.</returns>
		public abstract override int GetHashCode();

		/// <summary>
		/// Returns a string representation of this list.
		/// </summary>
		/// <returns>A semicolon-delimited list of elements.</returns>
		public abstract override string ToString();

		/// <summary>
		/// Gets an enumerator that enumerates every element of this list.
		/// </summary>
		/// <returns>
		/// An enumerator that enumerates every element of this list.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();

		/// <summary>
		/// Concatenates a <paramref name="list" /> to an <paramref name="item" />
		/// and returns the result.
		/// </summary>
		/// <param name="item">The first element of a new list.</param>
		/// <param name="list">The other elements of a new list.</param>
		/// <returns>
		/// A result of concatenation of the <paramref name="list" />
		/// to the <paramref name="item" />.
		/// </returns>
		public static ConsList<T> operator +(T item, ConsList<T> list)
			=> item.AddTo(list);

		/// <summary>
		/// Concatenates an <paramref name="item" /> to a <paramref name="list" />
		/// and returns the result.
		/// </summary>
		/// <param name="list">The first elements of a new list.</param>
		/// <param name="item">The last element of a new list.</param>
		/// <returns>
		/// A result of concatenation of the <paramref name="item" />
		/// to the <paramref name="list" />.
		/// </returns>
		public static ConsList<T> operator +(ConsList<T> list, T item)
			=> list.Add(ConsList.From(item));

		/// <summary>
		/// Returns a concatenation of two lists.
		/// </summary>
		/// <param name="a">The first list.</param>
		/// <param name="b">The second list.</param>
		/// <returns>A concatenation of two lists.</returns>
		public static ConsList<T> operator +(ConsList<T> a, ConsList<T> b)
			=> a.Add(b);
	}

	/// <summary>
	/// Constains helper and extension methods to work with cons lists.
	/// </summary>
	/// <seealso cref="ConsList{T}" />
	public static class ConsList
	{
		/// <summary>
		/// Constructs a list containing one <paramref name="item" />
		/// or an empty list if <paramref name="item" /> is <c>null</c>.
		/// </summary>
		/// <typeparam name="T">The type of the item.</typeparam>
		/// <param name="item">The item of the list.</param>
		/// <returns>
		/// A list containing one <paramref name="item" />
		/// or an empty list if <paramref name="item" /> is <c>null</c>.
		/// </returns>
		public static ConsList<T> From<T>(T item)
			=> item != null ? new ConsCell<T>(item, Empty<T>()) : Empty<T>();

		/// <summary>
		/// Constructs a shallow copy of the specified <paramref name="collection" />.
		/// </summary>
		/// <typeparam name="T">The type of elements of the collection.</typeparam>
		/// <param name="collection">The collection to copy.</param>
		/// <returns>
		/// A shallow copy of the specified <paramref name="collection" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection" /> is <c>null</c>.
		/// </exception>
		public static ConsList<T> Copy<T>(IEnumerable<T> collection)
			=> collection != null
				? collection.Aggregate(Empty<T>(), (acc, item) => acc + item)
				: throw new ArgumentNullException(nameof(collection));

		/// <summary>
		/// Constructs a list from specified <paramref name="items" />.
		/// </summary>
		/// <typeparam name="T">The type of the items.</typeparam>
		/// <param name="items">The items of the list.</param>
		/// <returns>A list from specified <paramref name="items" />.</returns>
		public static ConsList<T> Construct<T>(params T[] items)
			=> Copy(items);

		/// <summary>
		/// Constructs an empty list.
		/// </summary>
		/// <typeparam name="T">The type of elements in this list.</typeparam>
		/// <returns>An empty list.</returns>
		public static ConsList<T> Empty<T>()
			=> new Empty<T>();

		/// <summary>
		/// Concatenates a <paramref name="list" /> to an <paramref name="item" />
		/// and returns the result.
		/// </summary>
		/// <param name="item">The first element of a new list.</param>
		/// <param name="list">The other elements of a new list.</param>
		/// <returns>
		/// A result of concatenation of the <paramref name="list" />
		/// to the <paramref name="item" />.
		/// </returns>
		public static ConsList<T> AddTo<T>(this T item, ConsList<T> list)
			=> item != null ? new ConsCell<T>(item, list) : list;

		/// <summary>
		/// Returns a function which maps the provided list when called.
		/// </summary>
		/// <typeparam name="T">The input type of the function.</typeparam>
		/// <typeparam name="V">The output type of the function.</typeparam>
		/// <param name="func">The funciton to lift.</param>
		/// <returns>A function which maps the provided list when called.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public static Func<ConsList<T>, ConsList<V>> Lift<T, V>(this Func<T, V> func)
			=> func != null
				? (Func<ConsList<T>, ConsList<V>>)
					(values =>
						values != null
							? values.Map(func)
							: throw new ArgumentNullException(nameof(values)))
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Creates a cartesian product of two lists, which consists
		/// of results of function application to elements of the
		/// second list.
		/// </summary>
		/// <typeparam name="T">The input type of the functions.</typeparam>
		/// <typeparam name="V">The output type of the functions.</typeparam>
		/// <param name="funcList">The list of functions to apply.</param>
		/// <returns>
		/// A function which returns the cartesian product.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="funcList" /> is <c>null</c>.
		/// </exception>
		public static Func<ConsList<T>, ConsList<V>> Apply<T, V>(
			this ConsList<Func<T, V>> funcList)
			=> funcList != null
				? (Func<ConsList<T>, ConsList<V>>)
					(values =>
						values != null
							? funcList.Bind(values.Map)
							: throw new ArgumentNullException(nameof(values)))
				: throw new ArgumentNullException(nameof(funcList));
	}
}
