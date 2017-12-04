using System.Collections;
using System.Collections.Generic;

namespace CSX.Enumerators
{
	/// <summary>
	/// Represents an empty enumerator.
	/// </summary>
	/// <typeparam name="T">
	/// The type of the values this enumerator doesn't contain.
	/// </typeparam>
	public class EmptyEnumerator<T> : IEnumerator<T>
	{
		/// <summary>
		/// Constructs the only instance of the <see cref="EmptyEnumerator{T}" /> class.
		/// </summary>
		static EmptyEnumerator() =>
			Instance = new EmptyEnumerator<T>();

		/// <summary>
		/// Constructs a new instance of the <see cref="EmptyEnumerator{T}" /> class.
		/// </summary>
		private EmptyEnumerator() { }

		/// <summary>
		/// The only instance of the <see cref="EmptyEnumerator{T}" /> class.
		/// </summary>
		public static EmptyEnumerator<T> Instance { get; }

		/// <summary>
		/// This method should never be called as it doesn't return anything useful.
		/// </summary>
		public T Current => default(T);

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
		/// This method should never be called as it doesn't return anything useful.
		/// </summary>
		object IEnumerator.Current => this.Current;
	}
}
