using System;
using System.Collections.Generic;

using CSX.Results;

namespace CSX.Options
{
	/// <summary>
	/// Represents a case of <see cref="Option{T}" /> which contains a value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <seealso cref="Option{T}" />
	/// <seealso cref="None{T}" />
	public class Some<T> : Option<T>, IEquatable<Some<T>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Some{T}" /> class.
		/// </summary>
		/// <param name="value">The value that this option will contain.</param>
		internal Some(T value)
			=> this.Value = value;

		/// <summary>
		/// Gets the value that this option contains.
		/// </summary>
		public T Value { get; }

		/// <summary>
		/// Returns the value of this option.
		/// </summary>
		/// <param name="alternative">Not used.</param>
		/// <returns>The value of this option.</returns>
		/// <seealso cref="GetOrThrow(string)" />
		public override T GetOrElse(T alternative)
			=> this.Value;

		/// <summary>
		/// Returns the value of this option.
		/// </summary>
		/// <param name="message">Not used.</param>
		/// <returns>The value of this option.</returns>
		/// <seealso cref="GetOrElse(T)" />
		public override T GetOrThrow(string message = "The value is not present.")
			=> this.Value;

		/// <summary>
		/// Applies a specified function to this value.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// <c>Some(func(value))</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Bind{V}(Func{T, Option{V}})" />
		public override Option<V> Map<V>(Func<T, V> func)
			=> func != null
				? Option.From(func(this.Value))
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Applies a specified function to this value.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// <c>func(value)</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Map{V}(Func{T, V})" />
		public override Option<V> Bind<V>(Func<T, Option<V>> func)
			=> func != null
				? func(this.Value)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Executes a specified <paramref name="action" /> on this value.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfNone(Action)" />
		public override Option<T> DoIfSome(Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			action(this.Value);
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
		/// <seealso cref="DoIfSome(Action{T})" />
		public override Option<T> DoIfNone(Action action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Converts this option to a success.
		/// </summary>
		/// <param name="error">Not used.</param>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <returns><c>Success(value)</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="ToResult(string)" />
		public override Result<T, TError> ToResult<TError>(TError error)
			=> error != null
				? Result.Succeed<T, TError>(this.Value)
				: throw new ArgumentNullException(nameof(error));

		/// <summary>
		/// Converts this option to a success.
		/// </summary>
		/// <param name="error">Not used.</param>
		/// <returns><c>Success(value)</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="ToResult{TError}(TError)" />
		public override Result<T, string> ToResult(string error)
			=> error != null
				? Result.Succeed(this.Value)
				: throw new ArgumentNullException(nameof(error));
		
		/// <summary>
		/// Returns an enumerator which contains this value.
		/// </summary>
		/// <returns>An enumerator which contains this value.</returns>
		public override IEnumerator<T> GetEnumerator()
		{
			yield return this.Value;
		}

		/// <summary>
		/// Checks whether this value equals another value.
		/// The other value may be <c>null</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(Option{T})" />
		/// <seealso cref="Equals(Some{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(object other)
			=> other is Some<T> otherSome && this.Equals(otherSome);

		/// <summary>
		/// Checks whether this value equals another value.
		/// The other value may be <c>null</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Some{T})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(Option<T> other)
			=> other is Some<T> otherSome && this.Equals(otherSome);

		/// <summary>
		/// Checks whether this value equals another value.
		/// The other value may be <c>null</c>.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Option{T})" />
		/// <seealso cref="GetHashCode" />
		public bool Equals(Some<T> other)
			=> other != null && this.Value.Equals(other.Value);

		/// <summary>
		/// Gets this value's hash code.
		/// </summary>
		/// <returns>This value's hash code.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Option{T})" />
		/// <seealso cref="Equals(Some{T})" />
		public override int GetHashCode()
			=> this.Value.GetHashCode();

		/// <summary>
		/// Returns a string representation of this option in the format: "Some[value]".
		/// </summary>
		/// <returns>A string representation of this option.</returns>
		public override string ToString()
			=> $"Some[{this.Value}]";
	}
}
