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
		/// Returns the alternative value.
		/// The alternative may be <c>null</c>.
		/// </summary>
		/// <param name="alternative">The value to return.</param>
		/// <returns>The <paramref name="alternative" /> value.</returns>
		/// <seealso cref="GetOrThrow(string)" />
		public override T GetOrElse(T alternative)
			=> alternative;

		/// <summary>
		/// Throws an <see cref="InvalidOperationException" />.
		/// </summary>
		/// <param name="message">The message of the exception.</param>
		/// <returns>Nothing.</returns>
		/// <exception cref="OptionAbsentException">
		/// Thrown unconditionally.
		/// </exception>
		/// <seealso cref="GetOrElse(T)" />
		public override T GetOrThrow(string message = "The value is not present.")
			=> throw new OptionAbsentException(message);

		/// <summary>
		/// Returns an empty option of type <typeparamref name="V" />.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>An empty option of type <typeparamref name="V" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Bind{V}(Func{T, Option{V}})" />
		public override Option<V> Map<V>(Func<T, V> func)
			=> func != null
				? Option.Empty<V>()
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns an empty option of type <typeparamref name="V" />.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>An empty option of type <typeparamref name="V" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <see cref="Map{V}(Func{T, V})" />
		public override Option<V> Bind<V>(Func<T, Option<V>> func)
			=> func != null
				? Option.Empty<V>()
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="action">Not used.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfNone(Action)" />
		public override Option<T> DoIfSome(Action<T> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Executes a specified <paramref name="action" />.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfSome(Action{T})" />
		public override Option<T> DoIfNone(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			action();
			return this;
		}

		/// <summary>
		/// Converts this option to a failure.
		/// </summary>
		/// <param name="error">The error to return.</param>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <returns>Failure(<paramref name="error" />)</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="ToResult(string)" />
		public override Result<T, TError> ToResult<TError>(TError error)
			=> error != null
				? Result.Fail<T, TError>(error)
				: throw new ArgumentNullException(nameof(error));

		/// <summary>
		/// Converts this option to a failure.
		/// </summary>
		/// <param name="error">The error to return.</param>
		/// <returns>Failure(<paramref name="error" />)</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="ToResult{TError}(TError)" />
		public override Result<T, string> ToResult(string error)
			=> error != null
				? Result.Fail<T>(error)
				: throw new ArgumentNullException(nameof(error));
		
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
		/// The other object may be <c>null</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(Option{T})" />
		/// <seealso cref="Equals(None{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(object other)
			=> other is None<T>;

		/// <summary>
		/// Checks whether this object equals another object, i.e.
		/// whether the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />.
		/// The other object may be <c>null</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// Returns <c>true</c> if the <paramref name="other" /> object also has type
		/// <see cref="None{T}" />. Otherwise, returns <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(None{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(Option<T> other)
			=> other is None<T>;

		/// <summary>
		/// Checks whether this object equals another object.
		/// The other object may be <c>null</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="other" /> isn't <c>null</c>.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Option{T})" />
		/// <seealso cref="GetHashCode" />
		public bool Equals(None<T> other)
			=> other != null;

		/// <summary>
		/// Returns <c>1</c>.
		/// </summary>
		/// <returns><c>1</c></returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Option{T})" />
		/// <seealso cref="Equals(None{T})" />
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
