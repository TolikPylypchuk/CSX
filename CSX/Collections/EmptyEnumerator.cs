using System;
using System.Collections;
using System.Collections.Generic;

namespace CSX.Collections
{
	/// <summary>
	/// Represents an empty enumerator.
	/// </summary>
	/// <typeparam name="T">The type of the elements.</typeparam>
	/// <seealso cref="EmptyEnumerable{T}" />
	public class EmptyEnumerator<T> : IEnumerator<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyEnumerator{T}" /> class.
		/// </summary>
		private EmptyEnumerator() { }

		/// <summary>
		/// Gets the only instance of the <see cref="EmptyEnumerator{T}" /> class.
		/// </summary>
		public static EmptyEnumerator<T> Instance { get; } = new EmptyEnumerator<T>();

		/// <summary>
		/// Throws an <see cref="InvalidOperationException" />.
		/// </summary>
		/// <exception cref="InvalidOperationException">
		/// Thrown unconditionally.
		/// </exception>
		public T Current => throw new InvalidOperationException("The enumerator is empty.");

		/// <summary>
		/// Does nothing. Always returns <see langword="false" />.
		/// </summary>
		/// <returns><see langword="false" /></returns>
		public bool MoveNext() => false;

		/// <summary>
		/// Does nothing.
		/// </summary>
		public void Reset() { }

		/// <summary>
		/// Throws an <see cref="InvalidOperationException" />.
		/// </summary>
		/// <exception cref="InvalidOperationException">
		/// Thrown unconditionally.
		/// </exception>
		object IEnumerator.Current => this.Current;

		/// <summary>
		/// Does nothing.
		/// </summary>
		void IDisposable.Dispose() { }
	}
}
