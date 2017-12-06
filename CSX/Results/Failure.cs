using System;
using System.Collections.Generic;

using CSX.Enumerators;
using CSX.Lists;
using CSX.Options;

namespace CSX.Results
{
	/// <summary>
	/// Represents a case of <see cref="Result{TSuccess, TError}" /> which is failed.
	/// </summary>
	/// <typeparam name="TSuccess">The type of the success value.</typeparam>
	/// <typeparam name="TError">The type of the failure value.</typeparam>
	public class Failure<TSuccess, TError> :
		Result<TSuccess, TError>, IEquatable<Failure<TSuccess, TError>>
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Failure{TSuccess, TError}" /> class.
		/// </summary>
		/// <param name="error">The error of this result.</param>
		internal Failure(TError error)
			=> this.Errors = ConsList.From(error);

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Failure{TSuccess, TError}" /> class.
		/// </summary>
		/// <param name="errors">The errors of this result.</param>
		internal Failure(ConsList<TError> errors)
			=> this.Errors = errors;

		/// <summary>
		/// Gets the errors of this result.
		/// </summary>
		public ConsList<TError> Errors { get; }

		/// <summary>
		/// Returns the <paramref name="alternative" /> value.
		/// </summary>
		/// <param name="alternative">The value to return.</param>
		/// <returns>The <paramref name="alternative" /> value.</returns>
		public override TSuccess GetOrDefault(TSuccess alternative)
			=> alternative;

		/// <summary>
		/// Returns a failure with type <typeparamref name="VSuccess" />.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An failure with type <typeparamref name="VSuccess" />.</returns>
		public override Result<VSuccess, TError> Map<VSuccess>(
			Func<TSuccess, VSuccess> _)
			=> Result.Fail<VSuccess, TError>(this.Errors);

		/// <summary>
		/// Applies a specified function to the errors of this result.
		/// </summary>
		/// <typeparam name="VError">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>Failure(func(value)).</returns>
		public override Result<TSuccess, VError> MapFailure<VError>(
			Func<TError, VError> func)
			=> Result.Fail<TSuccess, VError>(this.Errors.Map(func));

		/// <summary>
		/// Returns a failure with type <typeparamref name="VSuccess" />.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="_">Not used.</param>
		/// <returns>An failure with type <typeparamref name="VSuccess" />.</returns>
		public override Result<VSuccess, TError> Bind<VSuccess>(
			Func<TSuccess, Result<VSuccess, TError>> _)
			=> Result.Fail<VSuccess, TError>(this.Errors);

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="_">Not used.</param>
		/// <returns><c>this</c></returns>
		public override Result<TSuccess, TError> IfSuccess(Action<TSuccess> _)
			=> this;

		/// <summary>
		/// Executes a specified <paramref name="action" /> on the errors of this result.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public override Result<TSuccess, TError> IfFailure(
			Action<ConsList<TError>> action)
		{
			action(this.Errors);
			return this;
		}

		/// <summary>
		/// Returns None.
		/// </summary>
		/// <returns>None.</returns>
		public override Option<TSuccess> ToOption()
			=> Option.Empty<TSuccess>();

		/// <summary>
		/// Returns an empty enumerator.
		/// </summary>
		/// <returns>An empty enumerator.</returns>
		public override IEnumerator<TSuccess> GetEnumerator()
			=> EmptyEnumerator<TSuccess>.Instance;

		/// <summary>
		/// Checks whether the errors of this result equal the errors of another result.
		/// </summary>
		/// <param name="other">The result to compare to.</param>
		/// <returns>
		/// <c>true</c> if the errors of this result equal the errors of another result.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object other)
			=> other is Failure<TSuccess, TError> otherFailure &&
			   this.Equals(otherFailure);

		/// <summary>
		/// Checks whether the errors of this result equal the errors of another result.
		/// </summary>
		/// <param name="other">The result to compare to.</param>
		/// <returns>
		/// <c>true</c> if the errors of this result equal the errors of another result.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Result<TSuccess, TError> other)
			=> other is Failure<TSuccess, TError> otherFailure &&
			   this.Equals(otherFailure);

		/// <summary>
		/// Checks whether the errors of this result equal the errors of another result.
		/// </summary>
		/// <param name="other">The result to compare to.</param>
		/// <returns>
		/// <c>true</c> if the errors of this result equal the errors of another result.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(Failure<TSuccess, TError> other)
			=> this.Errors.Equals(other.Errors);

		/// <summary>
		/// Gets the hash code of the errors of this result.
		/// </summary>
		/// <returns>The hash code of the errors of this result.</returns>
		public override int GetHashCode()
			=> this.Errors.GetHashCode();

		/// <summary>
		/// Returns a string representation of this result in the format: "Failure[errors]".
		/// </summary>
		/// <returns>A string representation of this result.</returns>
		public override string ToString()
			=> $"Failure[{this.Errors}]";
	}
}
