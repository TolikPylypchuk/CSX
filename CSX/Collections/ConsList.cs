using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CSX.Collections.Matchers;
using CSX.Exceptions;

namespace CSX.Collections
{
	/// <summary>
	/// Represents a list, constructed from cons cells.
	/// </summary>
	/// <typeparam name="T">The type of the values this list holds.</typeparam>
	/// <seealso cref="ConsList" />
	/// <seealso cref="ConsCell{T}" />
	/// <seealso cref="Empty{T}" />
	public abstract class ConsList<T> :
		IEquatable<ConsList<T>>,
		IEnumerable, IEnumerable<T>,
		ICollection, ICollection<T>,
		IList, IList<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConsList{T}" /> class.
		/// </summary>
		private protected ConsList() { }

		/// <summary>
		/// Gets the number of items in this list.
		/// </summary>
		public abstract int Count { get; }

		/// <summary>
		/// Gets the item at the specified index.
		/// </summary>
		/// <param name="index">The index of the item to get.</param>
		/// <returns>The item at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index" /> is not a valid index.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally on an attempt to set an item.
		/// </exception>
		public T this[int index]
		{
			get => this.GetItemImpl(index, 0);
			set => throw new NotSupportedException("Cannot set a new item in a cons list.");
		}

		/// <summary>
		/// Returns a concatenation of this list with another list.
		/// </summary>
		/// <param name="other">The other list.</param>
		/// <returns>A concatenation of this list with another list.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="other" /> is <see langword="null" />.
		/// </exception>
		public abstract ConsList<T> Add(ConsList<T> other);

		/// <summary>
		/// Applies a specified function to every element of this list
		/// and returns a list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A list consisting of results of the function application.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="FlatMap{V}(Func{T, ConsList{V}})" />
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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{V}(Func{T, V})" />
		public abstract ConsList<V> FlatMap<V>(Func<T, ConsList<V>> func);

		/// <summary>
		/// Returns the result of the specified function if this list is a
		/// <see cref="ConsCell{T}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this list is <see cref="Empty{T}" />, then the result of the function,
		/// provided to the <see cref="Empty{T}" /> matcher.
		/// Otherwise, the result of <paramref name="func" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchEmpty{TResult}(Func{TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public EmptyMatcher<T, TResult> MatchConsCell<TResult>(
			Func<T, ConsList<T>, TResult> func)
			=> func != null
				? new EmptyMatcher<T, TResult>(this, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the result of the specified function if this list is
		/// <see cref="Empty{T}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this list is a <see cref="ConsCell{T}" />, then the result of the function,
		/// provided to the <see cref="ConsCell{T}" /> matcher.
		/// Otherwise, the result of <paramref name="func" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchConsCell{TResult}(Func{T, ConsList{T}, TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public ConsCellMatcher<T, TResult> MatchEmpty<TResult>(Func<TResult> func)
			=> func != null
				? new ConsCellMatcher<T, TResult>(this, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns a result of the specified function.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>The result of <paramref name="func" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchConsCell{TResult}(Func{T, ConsList{T}, TResult})" />
		/// <seealso cref="MatchEmpty{TResult}(Func{TResult})" />
		public TResult MatchAny<TResult>(Func<TResult> func)
			=> func != null
				? func()
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Executes a specified <paramref name="action" /> if this list is a cons cell.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfEmpty(Action)" />
		public abstract ConsList<T> DoIfConsCell(Action<T, ConsList<T>> action);

		/// <summary>
		/// Executes a specified <paramref name="action" /> if this list is empty.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfConsCell(Action{T, ConsList{T}})" />
		public abstract ConsList<T> DoIfEmpty(Action action);

		/// <summary>
		/// Executes a specified <paramref name="action" /> to each element of this list.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public abstract ConsList<T> ForEach(Action<T> action);

		/// <summary>
		/// Folds this list to a single value from left to right.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">
		/// The first parameter of the chain of calls to <paramref name="func" />.
		/// May be <see langword="null" />.
		/// </param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="FoldBack{V}(V, Func{T, V, V})" />
		public abstract V Fold<V>(V seed, Func<V, T, V> func);

		/// <summary>
		/// Folds this list to a single value from right to left.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">
		/// The first parameter of the chain of calls to <paramref name="func" />.
		/// May be <see langword="null" />.
		/// </param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fold{V}(V, Func{V, T, V})" />
		public abstract V FoldBack<V>(V seed, Func<T, V, V> func);
		
		/// <summary>
		/// Checks whether the specified item is in this list.
		/// </summary>
		/// <param name="item">The item to check.</param>
		/// <returns>
		/// <see langword="true" />, if this item is contained in this list.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		public abstract bool Contains(T item);

		/// <summary>
		/// Copies the items of this list into a specified array.
		/// </summary>
		/// <param name="array">The array into which the items will be copied.</param>
		/// <param name="arrayIndex">The start index of the array.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="arrayIndex" /> is less than 0.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// This list contains more items that can fit into the <paramref name="array" />.
		/// </exception>
		public abstract void CopyTo(T[] array, int arrayIndex);
		
		/// <summary>
		/// Gets an enumerator that enumerates every element of this list.
		/// </summary>
		/// <returns>
		/// An enumerator that enumerates every element of this list.
		/// </returns>
		public abstract IEnumerator<T> GetEnumerator();

		/// <summary>
		/// Returns the index of the first occurence of the specified item in this list.
		/// </summary>
		/// <param name="item">The value to find.</param>
		/// <returns>
		/// The index of the first occurence of the specified item in this list or
		/// -1 if this item is not present in this list.
		/// </returns>
		public int IndexOf(T item)
			=> item != null
				? this.IndexOfImpl(item, 0)
				: -1;

		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// The other list may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <see langword="null" />.</param>
		/// <returns>
		/// <see langword="true" /> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="GetHashCode" />
		public abstract override bool Equals(object other);

		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// The other list may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The list to compare to. May be <see langword="null" />.</param>
		/// <returns>
		/// <see langword="true" /> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		public abstract bool Equals(ConsList<T> other);

		/// <summary>
		/// Gets this object's hash code.
		/// </summary>
		/// <returns>This object's hash code.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(ConsList{T})" />
		public abstract override int GetHashCode();

		/// <summary>
		/// Returns a string representation of this list.
		/// </summary>
		/// <returns>A semicolon-delimited list of elements.</returns>
		public abstract override string ToString();

		/// <summary>
		/// Copies the items of this list into a specified array.
		/// </summary>
		/// <param name="array">The array into which the items will be copied.</param>
		/// <param name="arrayIndex">The start index of the array.</param>
		internal abstract void CopyToImpl(T[] array, int arrayIndex);

		/// <summary>
		/// Gets the item at the specified index.
		/// </summary>
		/// <param name="index">The index of the item to get.</param>
		/// <param name="currentIndex">The index of the current cell.</param>
		/// <returns>The item at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index" /> is not a valid index.
		/// </exception>
		internal abstract T GetItemImpl(int index, int currentIndex);

		/// <summary>
		/// Returns the index of the first occurence of the specified item in this list.
		/// </summary>
		/// <param name="item">The value to find.</param>
		/// <param name="currentIndex">The index of the current cell.</param>
		/// <returns>
		/// The index of the first occurence of the specified item in this list or
		/// -1 if this item is not present in this list.
		/// </returns>
		internal abstract int IndexOfImpl(T item, int currentIndex);

		/// <summary>
		/// Always gets <see langword="true" />.
		/// </summary>
		bool IList.IsFixedSize => true;

		/// <summary>
		/// Always gets <see langword="true" />.
		/// </summary>
		bool IList.IsReadOnly => true;

		/// <summary>
		/// Always gets <see langword="true" /> since cons lists are immutable.
		/// </summary>
		bool ICollection<T>.IsReadOnly => true;

		/// <summary>
		/// Always gets <see langword="true" /> since cons lists are immutable.
		/// </summary>
		bool ICollection.IsSynchronized => true;

		/// <summary>
		/// Gets an object which can be used to synchronize access to this list.
		/// The object is the list itself.
		/// </summary>
		/// <remarks>
		/// Cons lists are immutable, so all operations on them are thread safe
		/// and a sync root is not needed.
		/// </remarks>
		object ICollection.SyncRoot => this;

		/// <summary>
		/// Gets the value at the specified index.
		/// </summary>
		/// <param name="index">The index of the value to get.</param>
		/// <returns>The value at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index" /> is not a valid index.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally on an attempt to set a value.
		/// </exception>
		object IList.this[int index]
		{
			get => this[index];
			set => throw new NotSupportedException("Cannot set a new value in a cons list.");
		}

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="item">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void ICollection<T>.Add(T item)
			=> throw new NotSupportedException("Cannot add an item to a cons list.");
		
		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="value">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		int IList.Add(object value)
			=> throw new NotSupportedException("Cannot add an item to a cons list.");

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void IList.Clear()
			=> throw new NotSupportedException("Cannot clear a cons list.");

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void ICollection<T>.Clear()
			=> throw new NotSupportedException("Cannot clear a cons list.");

		/// <summary>
		/// Checks whether the specified value is in this list.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>
		/// <see langword="true" />, if this value is of type <typeparamref name="T" />
		/// and is contained in this list. Otherwise, <see langword="false" />.
		/// </returns>
		bool IList.Contains(object value)
			=> value is T item && this.Contains(item);

		/// <summary>
		/// Copies the items of this list into a specified array.
		/// </summary>
		/// <param name="array">The array into which the items will be copied.</param>
		/// <param name="index">The start index of the array.</param>
		/// <remarks>
		/// <paramref name="array" /> must be of type <typeparamref name="T" />[].
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="array" /> is multidimensional, or not sufficient to store
		/// the items from the list, or the type of the items of this list cannot
		/// be cast to the type of the array.
		/// </exception>
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			if (array.Rank != 1)
			{
				throw new ArgumentException(
					"Cannot copy into a multidimensional array.", nameof(array));
			}

			if (array is T[] typedArray)
			{
				this.CopyTo(typedArray, index);
			} else
			{
				throw new ArgumentException(
					"Cannot copy into an array of a different type.", nameof(array));
			}
		}

		/// <summary>
		/// Gets an enumerator that enumerates every element of this list.
		/// </summary>
		/// <returns>
		/// An enumerator that enumerates every element of this list.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();

		/// <summary>
		/// Returns the index of the first occurence of the specified value in this list.
		/// </summary>
		/// <param name="value">The value to find.</param>
		/// <returns>
		/// The index of the first occurence of the specified value in this list or
		/// -1 if this value is not present in this list.
		/// </returns>
		int IList.IndexOf(object value)
			=> value is T item ? this.IndexOf(item) : -1;

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="index">Not used.</param>
		/// <param name="value">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void IList.Insert(int index, object value)
			=> throw new NotSupportedException("Cannot insert a value into a cons list.");

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="index">Not used.</param>
		/// <param name="item">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void IList<T>.Insert(int index, T item)
			=> throw new NotSupportedException("Cannot insert an item into a cons list.");

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="value">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void IList.Remove(object value)
			=> throw new NotSupportedException("Cannot remove a value from a cons list.");

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="item">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		bool ICollection<T>.Remove(T item)
			=> throw new NotSupportedException("Cannot remove an item from a cons list.");

		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="index">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void IList.RemoveAt(int index)
			=> throw new NotSupportedException("Cannot remove a value from a cons list.");
		
		/// <summary>
		/// Throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <param name="index">Not used.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown unconditionally.
		/// </exception>
		void IList<T>.RemoveAt(int index)
			=> throw new NotSupportedException("Cannot remove an item from a cons list.");

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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="item" /> or <paramref name="list" /> is <see langword="null" />.
		/// </exception>
		public static ConsList<T> operator +(T item, ConsList<T> list)
			=> item != null
				? list != null
					? item.AddTo(list)
					: throw new ArgumentNullException(nameof(list))
				: throw new ArgumentNullException(nameof(item));

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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="list" /> or <paramref name="item" /> is <see langword="null" />.
		/// </exception>
		public static ConsList<T> operator +(ConsList<T> list, T item)
			=> list != null
				? item != null
					? list.Add(ConsList.From(item))
					: throw new ArgumentNullException(nameof(item))
				: throw new ArgumentNullException(nameof(list));

		/// <summary>
		/// Returns a concatenation of two lists.
		/// </summary>
		/// <param name="a">The first list.</param>
		/// <param name="b">The second list.</param>
		/// <returns>A concatenation of two lists.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="a" /> or <paramref name="b" /> is <see langword="null" />.
		/// </exception>
		public static ConsList<T> operator +(ConsList<T> a, ConsList<T> b)
			=> a != null
				? b != null
					? a.Add(b)
					: throw new ArgumentNullException(nameof(b))
				: throw new ArgumentNullException(nameof(a));
	}

	/// <summary>
	/// Constains helper and extension methods to work with cons lists.
	/// </summary>
	/// <seealso cref="ConsList{T}" />
	public static class ConsList
	{
		/// <summary>
		/// Constructs a list containing one <paramref name="item" />.
		/// </summary>
		/// <typeparam name="T">The type of the item.</typeparam>
		/// <param name="item">The item of the list.</param>
		/// <returns>
		/// A list containing one <paramref name="item" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="item" /> is <see langword="null" />.
		/// </exception>
		public static ConsList<T> From<T>(T item)
			=> item != null
				? new ConsCell<T>(item, Empty<T>())
				: throw new ArgumentNullException(nameof(item));

		/// <summary>
		/// Constructs a shallow copy of the specified <paramref name="collection" />.
		/// </summary>
		/// <typeparam name="T">The type of elements of the collection.</typeparam>
		/// <param name="collection">The collection to copy.</param>
		/// <returns>
		/// A shallow copy of the specified <paramref name="collection" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection" /> is <see langword="null" />.
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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="items" /> is <see langword="null" />.
		/// </exception>
		public static ConsList<T> Construct<T>(params T[] items)
			=> items != null
				? Copy(items)
				: throw new ArgumentNullException(nameof(items));

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
		/// <exception cref="ArgumentNullException">
		/// <paramref name="item" /> or <paramref name="list" /> is <see langword="null" />.
		/// </exception>
		public static ConsList<T> AddTo<T>(this T item, ConsList<T> list)
			=> item != null
				? list != null
					? new ConsCell<T>(item, list)
					: throw new ArgumentNullException(nameof(list))
				: throw new ArgumentNullException(nameof(item));

		/// <summary>
		/// Returns a function which maps the provided list when called.
		/// </summary>
		/// <typeparam name="T">The input type of the function.</typeparam>
		/// <typeparam name="V">The output type of the function.</typeparam>
		/// <param name="func">The funciton to lift.</param>
		/// <returns>A function which maps the provided list when called.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Apply{T, V}(ConsList{Func{T, V}})" />
		public static Func<ConsList<T>, ConsList<V>> Lift<T, V>(this Func<T, V> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return values =>
				values != null
					? values.Map(func)
					: throw new ArgumentNullException(nameof(values));
		}

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
		/// <paramref name="funcList" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Lift{T, V}(Func{T, V})" />
		public static Func<ConsList<T>, ConsList<V>> Apply<T, V>(
			this ConsList<Func<T, V>> funcList)
		{
			if (funcList == null)
			{
				throw new ArgumentNullException(nameof(funcList));
			}

			return values =>
				values != null
					? funcList.FlatMap(values.Map)
					: throw new ArgumentNullException(nameof(values));
		}
	}
}
