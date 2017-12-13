using System.Collections;
using System.Collections.Generic;

namespace CSX.Collections
{
	/// <summary>
	/// Represents an enumerable which contains no elements.
	/// </summary>
	/// <typeparam name="T">The type of the elements.</typeparam>
	public class EmptyEnumerable<T> : IEnumerable<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmptyEnumerable{T}" /> class.
		/// </summary>
		private EmptyEnumerable() { }

		/// <summary>
		/// Gets the only instance of the <see cref="EmptyEnumerable{T}" /> class.
		/// </summary>
		public static EmptyEnumerable<T> Instance { get; } = new EmptyEnumerable<T>();

		/// <summary>
		/// Gets an empty enumerator for type <typeparamref name="T" />.
		/// </summary>
		/// <returns>An empty enumerator for type <typeparamref name="T" />.</returns>
		public IEnumerator<T> GetEnumerator()
			=> EmptyEnumerator<T>.Instance;

		/// <summary>
		/// Gets an empty enumerator.
		/// </summary>
		/// <returns>An empty enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();
	}
}
