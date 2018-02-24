using System;
using System.Collections.Generic;

using CSX.Exceptions;

namespace CSX.Collections
{
	/// <summary>
	/// Represents cons cell - a case of <see cref="ConsList{T}" />
	/// which contains a value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <seealso cref="ConsList{T}" />
	/// <seealso cref="Empty{T}" />
	public class ConsCell<T> : ConsList<T>, IEquatable<ConsCell<T>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConsCell{T}" /> class.
		/// </summary>
		/// <param name="head">The values stored in this cell.</param>
		/// <param name="tail">The rest of the list.</param>
		internal ConsCell(T head, ConsList<T> tail)
		{
			this.Head = head;
			this.Tail = tail;
		}

		/// <summary>
		/// Gets the value stored in this cell.
		/// </summary>
		/// <seealso cref="Tail" />
		public T Head { get; }

		/// <summary>
		/// Gets the rest of the list.
		/// </summary>
		/// <seealso cref="Head" />
		public ConsList<T> Tail { get; }

		/// <summary>
		/// Gets the number of items in this list.
		/// </summary>
		public override int Count => 1 + this.Tail.Count;

		/// <summary>
		/// Returns a concatenation of this list with another list.
		/// </summary>
		/// <param name="other">The other list.</param>
		/// <returns>A concatenation of this list with another list.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="other" /> is <c>null</c>.
		/// </exception>
		public override ConsList<T> Add(ConsList<T> other)
			=> other != null
				? this.Head.AddTo(this.Tail.Add(other))
				: throw new ArgumentNullException(nameof(other));

		/// <summary>
		/// Applies a specified function to the value of this cell
		/// and to the rest of the list and returns a list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A list consisting of results of the function application.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <c>null</c>.
		/// </exception>
		/// <seealso cref="FlatMap{V}(Func{T, ConsList{V}})" />
		public override ConsList<V> Map<V>(Func<T, V> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			var result = func(this.Head);

			return result != null
				? ConsList.From(result).Add(this.Tail.Map(func))
				: throw new UnacceptableNullException("Cannot map to null.");
		}

		/// <summary>
		/// Applies a specified function to the value of this cell
		/// and to the rest of the list and returns a list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A list consisting of results of the function application.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <c>null</c>.
		/// </exception>
		/// <seealso cref="Map{V}(Func{T, V})" />
		public override ConsList<V> FlatMap<V>(Func<T, ConsList<V>> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			var result = func(this.Head);

			return result != null
				? result.Add(this.Tail.FlatMap(func))
				: throw new UnacceptableNullException("Cannot flat map to null.");
		}

		/// <summary>
		/// Executes a specified action.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfEmpty(Action)" />
		public override ConsList<T> DoIfConsCell(Action<T, ConsList<T>> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			action(this.Head, this.Tail);
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
		/// <seealso cref="DoIfConsCell(Action{T, ConsList{T}})" />
		public override ConsList<T> DoIfEmpty(Action action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Executes a specified <paramref name="action" /> for this cell's value and for
		/// the rest of the list.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		public override ConsList<T> ForEach(Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			action(this.Head);
			return this.Tail.ForEach(action);
		}

		/// <summary>
		/// Folds this list to a single value from left to right.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">
		/// The first parameter of the chain of calls to <paramref name="func" />.
		/// May be <c>null</c>.
		/// </param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="FoldBack{V}(V, Func{T, V, V})" />
		public override V Fold<V>(V seed, Func<V, T, V> func)
			=> func != null
				? this.Tail.Fold(func(seed, this.Head), func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Folds this list to a single value from right to left.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">
		/// The first parameter of the chain of calls to <paramref name="func" />.
		/// May be <c>null</c>.
		/// </param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Fold{V}(V, Func{V, T, V})" />
		public override V FoldBack<V>(V seed, Func<T, V, V> func)
			=> func != null
				? func(this.Head, this.Tail.FoldBack(seed, func))
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Checks whether the specified item is in this list.
		/// </summary>
		/// <param name="item">The item to check.</param>
		/// <returns>
		/// <see langword="true" />, if this item is contained in this list.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		public override bool Contains(T item)
			=> item != null && (item.Equals(this.Head) || this.Tail.Contains(item));

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

			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException(
					"The list contains more items that can fit into the array.");
			}

			this.CopyToImpl(array, arrayIndex);
		}

		/// <summary>
		/// Gets an enumerator that enumerates every element of this list.
		/// </summary>
		/// <returns>
		/// An enumerator that enumerates every element of this list.
		/// </returns>
		public override IEnumerator<T> GetEnumerator()
		{
			yield return this.Head;

			foreach (var item in this.Tail)
			{
				yield return item;
			}
		}
		
		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// The other list may be <c>null</c>.
		/// </summary>
		/// <param name="other">The list to compare to. May be <c>null</c>.</param>
		/// <returns>
		/// <c>true</c> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="Equals(ConsCell{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(object other)
			=> other is ConsCell<T> otherCell && this.Equals(otherCell);

		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// The other list may be <c>null</c>.
		/// </summary>
		/// <param name="other">The list to compare to. May be <c>null</c>.</param>
		/// <returns>
		/// <c>true</c> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(ConsCell{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(ConsList<T> other)
			=> other is ConsCell<T> otherCell && this.Equals(otherCell);

		/// <summary>
		/// Checks whether every element of this list equals
		/// another list's corresponding element.
		/// The other list may be <c>null</c>.
		/// </summary>
		/// <param name="other">The list to compare to. May be null.</param>
		/// <returns>
		/// <c>true</c> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="GetHashCode" />
		public bool Equals(ConsCell<T> other)
			=> other != null && this.Head.Equals(other.Head) && this.Tail.Equals(other.Tail);

		/// <summary>
		/// Gets this object's hash code.
		/// </summary>
		/// <returns>This object's hash code.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(ConsList{T})" />
		/// <seealso cref="Equals(ConsCell{T})" />
		public override int GetHashCode()
			=> this.Map(item => item.GetHashCode())
			       .Fold(String.Empty, (acc, code) => $"{acc} {code}")
			       .GetHashCode();

		/// <summary>
		/// Returns a string representation of this list.
		/// </summary>
		/// <returns>A semicolon-delimited list of elements.</returns>
		public override string ToString()
			=> $"{this.Head}; {this.Tail}";

		/// <summary>
		/// Copies the items of this list into a specified array.
		/// </summary>
		/// <param name="array">The array into which the items will be copied.</param>
		/// <param name="arrayIndex">The start index of the array.</param>
		internal override void CopyToImpl(T[] array, int arrayIndex)
		{
			array[arrayIndex] = this.Head;
			this.Tail.CopyToImpl(array, arrayIndex + 1);
		}

		/// <summary>
		/// Gets the item at the specified index.
		/// </summary>
		/// <param name="index">The index of the item to get.</param>
		/// <param name="currentIndex">The index of the current cell.</param>
		/// <returns>The item at the specified index.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="index" /> is not a valid index.
		/// </exception>
		internal override T GetItemImpl(int index, int currentIndex)
			=> index == currentIndex
				? this.Head
				: this.Tail.GetItemImpl(index, currentIndex + 1);

		/// <summary>
		/// Returns the index of the first occurence of the specified item in this list.
		/// </summary>
		/// <param name="item">The value to find.</param>
		/// <param name="currentIndex">The index of the current cell.</param>
		/// <returns>
		/// The index of the first occurence of the specified item in this list or
		/// -1 if this item is not present in this list.
		/// </returns>
		internal override int IndexOfImpl(T item, int currentIndex)
			=> this.Head.Equals(item)
				? currentIndex
				: this.Tail.IndexOfImpl(item, currentIndex + 1);
	}
}
