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
		/// <param name="_">Not used.</param>
		/// <returns>The value of this option.</returns>
		public override T GetOrElse(T _) => this.Value;

		/// <summary>
		/// Returns the value of this option.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns>The value of this option.</returns>
		public override T GetOrThrow(string _ = "The value is not present.")
			=> this.Value;

		/// <summary>
		/// Applies a specified function to this value.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// Some(func(value)).
		/// </returns>
		public override Option<V> Map<V>(Func<T, V> func)
			=> Option.From(func(this.Value));

		/// <summary>
		/// Applies a specified function to this value.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// func(value).
		/// </returns>
		public override Option<V> Bind<V>(Func<T, Option<V>> func)
			=> func(this.Value);

		/// <summary>
		/// Executes a specified <paramref name="action" /> on this value.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public override Option<T> DoIfSome(Action<T> action)
		{
			action(this.Value);
			return this;
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns><c>this</c></returns>
		public override Option<T> DoIfNone(Action _)
			=> this;
		
		/// <summary>
		/// Converts this option to a success.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <returns>Success(value)</returns>
		public override Result<T, TError> ToResult<TError>(TError _)
			=> Result.Succeed<T, TError>(this.Value);

		/// <summary>
		/// Converts this option to a success.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns>Success(value)</returns>
		public override Result<T, string> ToResult(string _)
			=> Result.Succeed(this.Value);
		
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
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object other)
			=> other is Some<T> otherSome && this.Equals(otherSome);

		/// <summary>
		/// Checks whether this value equals another value.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Option<T> other)
			=> other is Some<T> otherSome && this.Equals(otherSome);

		/// <summary>
		/// Checks whether this value equals another value.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(Some<T> other)
			=> this.Value.Equals(other.Value);

		/// <summary>
		/// Gets this value's hash code.
		/// </summary>
		/// <returns>This value's hash code.</returns>
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
