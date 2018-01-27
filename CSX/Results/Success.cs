using System;
using System.Collections.Generic;

using CSX.Lists;
using CSX.Options;

namespace CSX.Results
{
	/// <summary>
	/// Represents a case of <see cref="Result{TSuccess, TError}" /> which is successful.
	/// </summary>
	/// <typeparam name="TSuccess">The type of the success value.</typeparam>
	/// <typeparam name="TError">The type of the failure value.</typeparam>
	/// <seealso cref="Result{TSuccess, TError}" />
	/// <seealso cref="Failure{TSuccess, TError}" />
	public class Success<TSuccess, TError> :
		Result<TSuccess, TError>, IEquatable<Success<TSuccess, TError>>
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Success{TSuccess, TError}" /> class.
		/// </summary>
		/// <param name="value">The value of the result.</param>
		internal Success(TSuccess value)
			=> this.Value = value;

		/// <summary>
		/// Gets the value of this result.
		/// </summary>
		public TSuccess Value { get; }

		/// <summary>
		/// Gets the value of this result.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns>The value of this result.</returns>
		public override TSuccess GetOrElse(TSuccess _) => this.Value;

		/// <summary>
		/// Applies a specified function to the value of this result.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>Success(func(value)).</returns>
		public override Result<VSuccess, TError> Map<VSuccess>(
			Func<TSuccess, VSuccess> func)
			=> Result.Succeed<VSuccess, TError>(func(this.Value));

		/// <summary>
		/// Returns a success with type <typeparamref name="VError" />.
		/// </summary>
		/// <typeparam name="VError">The type of the returned value.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An success with type <typeparamref name="VError" />.</returns>
		public override Result<TSuccess, VError> MapFailure<VError>(
			Func<TError, VError> _)
			=> Result.Succeed<TSuccess, VError>(this.Value);

		/// <summary>
		/// Applies a specified function to the value of this result.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>func(value).</returns>
		public override Result<VSuccess, TError> Bind<VSuccess>(
			Func<TSuccess, Result<VSuccess, TError>> func)
			=> func(this.Value);

		/// <summary>
		/// Executes a specified <paramref name="action" /> on this value.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public override Result<TSuccess, TError> DoIfSuccess(Action<TSuccess> action)
		{
			action(this.Value);
			return this;
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns><c>this</c></returns>
		public override Result<TSuccess, TError> DoIfFailure(Action<ConsList<TError>> _)
			=> this;

		/// <summary>
		/// Returns Some(value).
		/// </summary>
		/// <returns>Some(value).</returns>
		public override Option<TSuccess> ToOption()
			=> Option.From(this.Value);

		/// <summary>
		/// Gets an enumerator which contains this value.
		/// </summary>
		/// <returns>An enumerator which contains this value.</returns>
		public override IEnumerator<TSuccess> GetEnumerator()
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
			=> other is Success<TSuccess, TError> otherSuccess &&
			   this.Equals(otherSuccess);

		/// <summary>
		/// Checks whether this value equals another value.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Result<TSuccess, TError> other)
			=> other is Success<TSuccess, TError> otherSuccess &&
			   this.Equals(otherSuccess);

		/// <summary>
		/// Checks whether this value equals another value.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(Success<TSuccess, TError> other)
			=> this.Value.Equals(other.Value);

		/// <summary>
		/// Gets this value's hash code.
		/// </summary>
		/// <returns>This value's hash code.</returns>
		public override int GetHashCode()
			=> this.Value.GetHashCode();

		/// <summary>
		/// Returns a string representation of this result in the format: "Success[value]".
		/// </summary>
		/// <returns>A string representation of this result.</returns>
		public override string ToString()
			=> $"Success[{this.Value}]";
	}
}
