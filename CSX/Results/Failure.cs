using System;
using System.Collections.Generic;

using CSX.Collections;
using CSX.Exceptions;
using CSX.Options;
using CSX.Results.Matchers;

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
		/// Initializes a new instance of the <see cref="Failure{TSuccess, TError}" /> class.
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
		/// Returns the alternative value.
		/// </summary>
		/// <param name="alternative">The value to return.</param>
		/// <returns>The <paramref name="alternative" /> value.</returns>
		/// <seealso cref="GetOrThrow(Func{ConsList{TError}, Exception})" />
		public override TSuccess GetOrElse(TSuccess alternative)
			=> alternative;

		/// <summary>
		/// Returns the alternative value.
		/// </summary>
		/// <param name="alternativeProvider">The function which provides the alternative value.</param>
		/// <returns>The alternative value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="alternativeProvider" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		/// <seealso cref="GetOrThrow(Func{ConsList{TError}, Exception})" />
		public override TSuccess GetOrElse(Func<ConsList<TError>, TSuccess> alternativeProvider)
			=> alternativeProvider != null
				? alternativeProvider(this.Errors)
				: throw new ArgumentNullException(nameof(alternativeProvider));

		/// <summary>
		/// Throws a provided exception.
		/// </summary>
		/// <param name="exceptionProvider">The function which provides an exception to throw.</param>
		/// <returns>Nothing.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="exceptionProvider" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="exceptionProvider" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		public override TSuccess GetOrThrow(Func<ConsList<TError>, Exception> exceptionProvider)
		{
			if (exceptionProvider == null)
			{
				throw new ArgumentNullException(nameof(exceptionProvider));
			}

			var exception = exceptionProvider(this.Errors)
				?? throw new UnacceptableNullException("Cannot throw null.");

			throw exception;
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
		/// <seealso cref="MapFailure{VError}(Func{ConsList{TError}, ConsList{VError}})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public override Result<VSuccess, TError> Map<VSuccess>(Func<TSuccess, VSuccess> func)
			=> func != null
				? Result.Fail<VSuccess, TError>(this.Errors)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Applies a specified function to the errors of this result.
		/// </summary>
		/// <typeparam name="VError">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A result which contains the mapped errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// <paramref name="func" /> returns an empty list.
		/// </exception>
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public override Result<TSuccess, VError> MapFailure<VError>(Func<ConsList<TError>, ConsList<VError>> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			var newErrors = func(this.Errors);

			if (newErrors == null)
			{
				throw new UnacceptableNullException("Cannot map to null.");
			}

			if (newErrors.Count == 0)
			{
				throw new InvalidOperationException("Cannot map to an empty error list.");
			}

			return Result.Fail<TSuccess, VError>(newErrors);
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
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="MapFailure{VError}(Func{ConsList{TError}, ConsList{VError}})" />
		public override Result<VSuccess, TError> Bind<VSuccess>(Func<TSuccess, Result<VSuccess, TError>> func)
			=> func != null
				? Result.Fail<VSuccess, TError>(this.Errors)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the matcher which will return the result of another function.
		/// </summary>
		/// <param name="func">Not used.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns> The matcher which will return the result of another function.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchFailure{TResult}(Func{ConsList{TError}, TResult})" />
		/// <seealso cref="Result{TSuccess, TError}.MatchAny{TResult}(Func{TResult})" />
		public override FailureMatcher<TSuccess, TError, TResult> MatchSuccess<TResult>(Func<TSuccess, TResult> func)
			=> func != null
				? new FailureMatcher<TSuccess, TError, TResult>(this.Errors, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the matcher which will return the result of the specified function.
		/// </summary>
		/// <param name="func">The function whose result will be returned.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>The matcher which will return the result of the specified function.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchSuccess{TResult}(Func{TSuccess, TResult})" />
		/// <seealso cref="Result{TSuccess, TError}.MatchAny{TResult}(Func{TResult})" />
		public override SuccessMatcher<TSuccess, TError, TResult> MatchFailure<TResult>(
			Func<ConsList<TError>, TResult> func)
			=> func != null
				? new SuccessMatcher<TSuccess, TError, TResult>(this.Errors, func)
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
		/// Executes a specified action on the errors of this result.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfSuccess(Action{TSuccess})" />
		public override Result<TSuccess, TError> DoIfFailure(Action<ConsList<TError>> action)
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
