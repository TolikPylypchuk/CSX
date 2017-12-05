using System;
using System.Collections.Generic;

namespace CSX.Lists
{
	/// <summary>
	/// Represents cons cell - a case of <see cref="ConsList{T}" />
	/// which contains a value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
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
		public T Head { get; }

		/// <summary>
		/// Gets the rest of the list.
		/// </summary>
		public ConsList<T> Tail { get; }

		/// <summary>
		/// Returns a concatenation of this list with another list.
		/// </summary>
		/// <param name="other">The other list.</param>
		/// <returns>A concatenation of this list with another list.</returns>
		public override ConsList<T> Add(ConsList<T> other)
			=> this.Head.AddTo(this.Tail.Add(other));

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
		public override ConsList<V> Bind<V>(Func<T, ConsList<V>> func)
			=> func(this.Head).Add(this.Tail.Bind(func));

		/// <summary>
		/// Applies a specified function to this cell's value and to
		/// the rest of the list.
		/// </summary>
		/// <param name="action">The function to apply.</param>
		/// <returns><c>this</c></returns>
		public override ConsList<T> ForEach(Action<T> action)
		{
			action(this.Head);
			this.Tail.ForEach(action);
			return this;
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
			=> this.ToString().GetHashCode();

		/// <summary>
		/// Returns a string representation of this list.
		/// </summary>
		/// <returns>A semicolon-delimited list of elements.</returns>
		public override string ToString()
			=> $"{this.Head}; {this.Tail}";
	}
}
