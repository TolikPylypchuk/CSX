using System;
using System.Collections.Generic;

namespace CSX.Lists
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
		/// Returns a concatenation of this list with another list.
		/// </summary>
		/// <param name="other">The other list.</param>
		/// <returns>A concatenation of this list with another list.</returns>
		public override ConsList<T> Add(ConsList<T> other)
			=> this.Head.AddTo(this.Tail.Add(other));

		/// <summary>
		/// Executes a specified action.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <seealso cref="DoIfEmpty(Action)" />
		public override ConsList<T> DoIfConsCell(Action<T, ConsList<T>> action)
		{
			action(this.Head, this.Tail);
			return this;
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns><c>this</c></returns>
		/// <seealso cref="DoIfConsCell(Action{T, ConsList{T}})" />
		public override ConsList<T> DoIfEmpty(Action _)
			=> this;

		/// <summary>
		/// Applies a specified function to the value of this cell
		/// and to the rest of the list and returns a list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A list consisting of results of the function application.</returns>
		public override ConsList<V> Map<V>(Func<T, V> func)
			=> ConsList.From(func(this.Head)).Add(this.Tail.Map(func));

		/// <summary>
		/// Applies a specified function to the value of this cell
		/// and to the rest of the list and returns a list consisting of results.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A list consisting of results of the function application.</returns>
		public override ConsList<V> FlatMap<V>(Func<T, ConsList<V>> func)
			=> func(this.Head).Add(this.Tail.FlatMap(func));

		/// <summary>
		/// Executes a specified <paramref name="action" /> for this cell's value and for
		/// the rest of the list.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public override ConsList<T> ForEach(Action<T> action)
		{
			action(this.Head);
			this.Tail.ForEach(action);
			return this;
		}

		/// <summary>
		/// Folds this list to a single value from left to right.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The first parameter of the chain of calls to func.</param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		/// <seealso cref="FoldBack{V}(V, Func{T, V, V})" />
		public override V Fold<V>(V seed, Func<V, T, V> func)
			=> this.Tail.Fold(func(seed, this.Head), func);

		/// <summary>
		/// Folds this list to a single value from right to left.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="seed">The first parameter of the chain of calls to func.</param>
		/// <param name="func">The folder function.</param>
		/// <returns>The folded value.</returns>
		/// <seealso cref="Fold{V}(V, Func{V, T, V})" />
		public override V FoldBack<V>(V seed, Func<T, V, V> func)
			=> func(this.Head, this.Tail.FoldBack(seed, func));

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
		/// </summary>
		/// <param name="other">The list to compare to.</param>
		/// <returns>
		/// <c>true</c> if every element of this list equals equals
		/// another list's corresponding element.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object other)
			=> other is ConsCell<T> otherCell && this.Equals(otherCell);

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
		public override bool Equals(ConsList<T> other)
			=> other is ConsCell<T> otherCell && this.Equals(otherCell);

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
		public bool Equals(ConsCell<T> other)
			=> this.Head.Equals(other.Head) && this.Tail.Equals(other.Tail);

		/// <summary>
		/// Gets this object's hash code.
		/// </summary>
		/// <returns>This object's hash code.</returns>
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
	}
}
