using System;
using System.Collections.Generic;

using CSX.Collections;
using CSX.Exceptions;
using CSX.Options;

using static CSX.Functions.Function;

namespace CSX.Results
{
	/// <summary>
	/// Represents a case of <see cref="Result{TSuccess, TError}" /> which is failed.
	/// </summary>
	/// <typeparam name="TSuccess">The type of the success value.</typeparam>
	/// <typeparam name="TError">The type of the failure value.</typeparam>
	/// <seealso cref="Result{TSuccess, TError}" />
	/// <seealso cref="Success{TSuccess, TError}" />
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
		/// <seealso cref="GetOrThrow" />
		public override TSuccess GetOrElse(TSuccess alternative)
			=> alternative;

		/// <summary>
		/// Throws a <see cref="ResultFailedException" />.
		/// </summary>
		/// <returns>Nothing.</returns>
		/// <exception cref="ResultFailedException">
		/// Thrown unconditionally.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		public override TSuccess GetOrThrow()
		{
			if (typeof(TError) == typeof(Exception) ||
				typeof(TError).IsSubclassOf(typeof(Exception)))
			{
				throw new ResultFailedException(
					this.Errors.Map(UnsafeCast<TError, Exception>));
			}

			throw new ResultFailedException(
				this.Errors.Map(error => error.ToString()));
		}

		/// <summary>
		/// Returns a failure with type <typeparamref name="VSuccess" />.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>An failure with type <typeparamref name="VSuccess" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MapFailure{VError}(Func{TError, VError})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public override Result<VSuccess, TError> Map<VSuccess>(
			Func<TSuccess, VSuccess> func)
			=> func != null
				? Result.Fail<VSuccess, TError>(this.Errors)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Applies a specified function to the errors of this result.
		/// </summary>
		/// <typeparam name="VError">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>Failure(func(value)).</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public override Result<TSuccess, VError> MapFailure<VError>(
			Func<TError, VError> func)
			=> func != null
				? Result.Fail<TSuccess, VError>(this.Errors.Map(func))
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns a failure with type <typeparamref name="VSuccess" />.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>An failure with type <typeparamref name="VSuccess" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="MapFailure{VError}(Func{TError, VError})" />
		public override Result<VSuccess, TError> Bind<VSuccess>(
			Func<TSuccess, Result<VSuccess, TError>> func)
			=> func != null
				? Result.Fail<VSuccess, TError>(this.Errors)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="action">Not used.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfFailure(Action{ConsList{TError}})" />
		public override Result<TSuccess, TError> DoIfSuccess(Action<TSuccess> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Executes a specified <paramref name="action" /> on the errors of this result.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfSuccess(Action{TSuccess})" />
		public override Result<TSuccess, TError> DoIfFailure(
			Action<ConsList<TError>> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			action(this.Errors);
			return this;
		}

		/// <summary>
		/// Returns an empty option.
		/// </summary>
		/// <returns><see cref="None{TSuccess}" />.</returns>
		public override Option<TSuccess> ToOption()
			=> Option.Empty<TSuccess>();

		/// <summary>
		/// Combines this failure's errors with a provided error.
		/// </summary>
		/// <param name="error">The error to add.</param>
		/// <returns>A failure with the added error.</returns>
		public override Result<TSuccess, TError> CombineErrors(TError error)
			=> this.Errors.Add(ConsList.From(error)).ToFailure<TSuccess, TError>();

		/// <summary>
		/// Combines this failure's errors with provided errors.
		/// </summary>
		/// <param name="errors">The errors to add.</param>
		/// <returns>A failure with the added errors.</returns>
		public override Result<TSuccess, TError> CombineErrors(ConsList<TError> errors)
			=> this.Errors.Add(errors).ToFailure<TSuccess, TError>();

		/// <summary>
		/// Combines this failure's errors with provided errors.
		/// </summary>
		/// <param name="errors">The errors to add.</param>
		/// <returns>A failure with the added errors.</returns>
		public override Result<TSuccess, TError> CombineErrors(IEnumerable<TError> errors)
			=> this.Errors.Add(ConsList.Copy(errors)).ToFailure<TSuccess, TError>();
		
		/// <summary>
		/// Combines this failure's errors with a provided result's errors.
		/// </summary>
		/// <param name="result">The result whose errors are added.</param>
		/// <returns>A failure with the added errors.</returns>
		public override Result<TSuccess, TError> CombineErrors(
			Result<TSuccess, TError> result)
			=> result
			   .MatchFailure(this.CombineErrors)
			   .MatchSuccess(value => this);

		/// <summary>
		/// Returns an empty enumerator.
		/// </summary>
		/// <returns>An empty enumerator.</returns>
		public override IEnumerator<TSuccess> GetEnumerator()
			=> EmptyEnumerator<TSuccess>.Instance;

		/// <summary>
		/// Checks whether the errors of this result equal the errors of another result.
		/// The other errors may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The result to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if the errors of this result equal the errors of another result.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="Equals(Failure{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(object other)
			=> other is Failure<TSuccess, TError> otherFailure &&
			   this.Equals(otherFailure);

		/// <summary>
		/// Checks whether the errors of this result equal the errors of another result.
		/// The other errors may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The result to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if the errors of this result equal the errors of another result.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Failure{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(Result<TSuccess, TError> other)
			=> other is Failure<TSuccess, TError> otherFailure &&
			   this.Equals(otherFailure);

		/// <summary>
		/// Checks whether the errors of this result equal the errors of another result.
		/// The other errors may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The result to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if the errors of this result equal the errors of another result.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public bool Equals(Failure<TSuccess, TError> other)
			=> other != null && this.Errors.Equals(other.Errors);

		/// <summary>
		/// Gets the hash code of the errors of this result.
		/// </summary>
		/// <returns>The hash code of the errors of this result.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="Equals(Failure{TSuccess, TError})" />
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
