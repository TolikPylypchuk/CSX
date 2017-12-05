using System;
using System.Collections.Generic;

using CSX.Enumerators;

namespace CSX.Lists
{
	/// <summary>
	/// Represents an empty cons list.
	/// </summary>
	/// <typeparam name="T">The type of the elements of this list.</typeparam>
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
		public override ConsList<T> Add(ConsList<T> other)
			=> other;

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An empty list of type <typeparamref name="V" />.</returns>
		public override ConsList<V> Map<V>(Func<T, V> _)
			=> ConsList.Empty<V>();

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <typeparam name="V">The type of results.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An empty list of type <typeparamref name="V" />.</returns>
		public override ConsList<V> Bind<V>(Func<T, ConsList<V>> _)
			=> ConsList.Empty<V>();

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns><c>this</c></returns>
		public override ConsList<T> ForEach(Action<T> _)
			=> this;

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
		/// <param name="other">The list to compare to.</param>
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
		/// <param name="other">The list to compare to.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> list also has type
		/// <see cref="Empty{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		public override bool Equals(ConsList<T> other)
			=> other is Empty<T>;

		/// <summary>
		/// Checks whether this list equals another list and always returns <c>true</c>.
		/// </summary>
		/// <param name="other">The list to compare to.</param>
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
