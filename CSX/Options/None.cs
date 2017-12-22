using System;
using System.Collections.Generic;

using CSX.Collections;
using CSX.Results;

namespace CSX.Options
{
	/// <summary>
	/// Represents a case of <see cref="Option{T}" /> which doesn't contain a value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <seealso cref="Option{T}" />
	/// <seealso cref="Some{T}" />
	public class None<T> : Option<T>, IEquatable<None<T>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="None{T}" /> class.
		/// </summary>
		internal None() { }

		/// <summary>
		/// Returns the <paramref name="alternative" /> value.
		/// </summary>
		/// <param name="alternative">The value to return.</param>
		/// <returns>The <paramref name="alternative" /> value.</returns>
		public override T GetOrElse(T alternative)
			=> alternative;

		/// <summary>
		/// Returns an empty option of type <typeparamref name="V" />.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An empty option of type <typeparamref name="V" />.</returns>
		public override Option<V> Map<V>(Func<T, V> _)
			=> Option.Empty<V>();

		/// <summary>
		/// Returns an empty option of type <typeparamref name="V" />.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An empty option of type <typeparamref name="V" />.</returns>
		public override Option<V> Bind<V>(Func<T, Option<V>> _)
			=> Option.Empty<V>();

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns><c>this</c></returns>
		public override Option<T> DoIfSome(Action<T> _)
			=> this;

		/// <summary>
		/// Executes a specified <paramref name="action" />.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public override Option<T> DoIfNone(Action action)
		{
			action();
			return this;
		}
		
		/// <summary>
		/// Converts this option to a failure.
		/// </summary>
		/// <param name="error">The error to return.</param>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <returns>Failure(<paramref name="error" />)</returns>
		public override Result<T, TError> ToResult<TError>(TError error)
			=> Result.Fail<T, TError>(error);

		/// <summary>
		/// Converts this option to a failure.
		/// </summary>
		/// <param name="error">The error to return.</param>
		/// <returns>Failure(<paramref name="error" />)</returns>
		public override Result<T, string> ToResult(string error)
			=> Result.Fail<T>(error);
		
		/// <summary>
		/// Returns an empty enumerator.
		/// </summary>
		/// <returns>An empty enumerator.</returns>
		public override IEnumerator<T> GetEnumerator()
			=> EmptyEnumerator<T>.Instance;

		/// <summary>
		/// Checks whether this object equals another object, i.e.
		/// whether the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		public override bool Equals(object other)
			=> other is None<T>;

		/// <summary>
		/// Checks whether this object equals another object, i.e.
		/// whether the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		public override bool Equals(Option<T> other)
			=> other is None<T>;

		/// <summary>
		/// Checks whether this object equals another object and always returns <c>true</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns><c>true</c></returns>
		public bool Equals(None<T> other)
			=> true;

		/// <summary>
		/// Returns <c>1</c>.
		/// </summary>
		/// <returns><c>1</c></returns>
		public override int GetHashCode()
			=> 1;

		/// <summary>
		/// Returns a string representation of this option in the format: "None[typeof(T)]".
		/// </summary>
		/// <returns>A string representation of this option.</returns>
		public override string ToString()
			=> $"None[{typeof(T)}]";
	}
}
