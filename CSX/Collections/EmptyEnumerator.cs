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
		/// This property should never be called as it doesn't get anything useful.
		/// </summary>
		public T Current => default;

		/// <summary>
		/// Does nothing.
		/// </summary>
		public void Dispose() { }

		/// <summary>
		/// Does nothing. Always returns <c>false</c>.
		/// </summary>
		/// <returns><c>false</c></returns>
		public bool MoveNext() => false;

		/// <summary>
		/// Does nothing.
		/// </summary>
		public void Reset() { }

		/// <summary>
		/// This property should never be called as it doesn't get anything useful.
		/// </summary>
		object IEnumerator.Current => this.Current;
	}
}
