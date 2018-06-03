﻿using System;
using System.Collections.Generic;

using CSX.Collections;
using CSX.Exceptions;
using CSX.Options;
using CSX.Results.Matchers;

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
		/// <param name="alternative">Not used.</param>
		/// <returns>The value of this result.</returns>
		/// <seealso cref="GetOrElse(Func{ConsList{TError}, TSuccess})" />
		/// <seealso cref="GetOrThrow(Func{ConsList{TError}, Exception})" />
		public override TSuccess GetOrElse(TSuccess alternative)
			=> this.Value;

		/// <summary>
		/// Returns the value of this result.
		/// </summary>
		/// <param name="alternativeProvider">Not used.</param>
		/// <returns>The value of this result.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="alternativeProvider" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		/// <seealso cref="GetOrThrow(Func{ConsList{TError}, Exception})" />
		public override TSuccess GetOrElse(
			Func<ConsList<TError>, TSuccess> alternativeProvider)
			=> alternativeProvider != null
				? this.Value
				: throw new ArgumentNullException(nameof(alternativeProvider));

		/// <summary>
		/// Returns the value of this result.
		/// </summary>
		/// <param name="exceptionProvider">Not used.</param>
		/// <returns>The value of this result.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="exceptionProvider" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		/// <seealso cref="GetOrElse(Func{ConsList{TError}, TSuccess})" />
		public override TSuccess GetOrThrow(
			Func<ConsList<TError>, Exception> exceptionProvider)
			=> exceptionProvider != null
				? this.Value
				: throw new ArgumentNullException(nameof(exceptionProvider));

		/// <summary>
		/// Applies a specified function to the value of this result.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns><c>Success(func(value))</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="MapFailure{VError}(Func{ConsList{TError}, ConsList{VError}})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public override Result<VSuccess, TError> Map<VSuccess>(
			Func<TSuccess, VSuccess> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			var result = func(this.Value);

			return result != null
				? Result.Succeed<VSuccess, TError>(result)
				: throw new UnacceptableNullException("Cannot map to null.");
		}

		/// <summary>
		/// Returns a success with type <typeparamref name="VError" />.
		/// </summary>
		/// <typeparam name="VError">The type of the returned value.</typeparam>
		/// <param name="func">Not used.</param>
		/// <returns>A success with type <typeparamref name="VError" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public override Result<TSuccess, VError> MapFailure<VError>(
			Func<ConsList<TError>, ConsList<VError>> func)
			=> func != null
				? Result.Succeed<TSuccess, VError>(this.Value)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Applies a specified function to the value of this result.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns><c>func(value)</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="MapFailure{VError}(Func{ConsList{TError}, ConsList{VError}})" />
		public override Result<VSuccess, TError> Bind<VSuccess>(
			Func<TSuccess, Result<VSuccess, TError>> func)
			=> func != null
				? func(this.Value)
					?? throw new UnacceptableNullException("Cannot bind to null.")
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the matcher which will return the result of the specified function.
		/// </summary>
		/// <param name="func">The function whose result will be returned.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// The matcher which will return the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchFailure{TResult}(Func{ConsList{TError}, TResult})" />
		/// <seealso cref="Result{TSuccess, TError}.MatchAny{TResult}(Func{TResult})" />
		public override FailureMatcher<TSuccess, TError, TResult> MatchSuccess<TResult>(
			Func<TSuccess, TResult> func)
			=> func != null
				? new FailureMatcher<TSuccess, TError, TResult>(this.Value, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the matcher which will return the result of another function.
		/// </summary>
		/// <param name="func">Not used.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// The matcher which will return the result of another function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchSuccess{TResult}(Func{TSuccess, TResult})" />
		/// <seealso cref="Result{TSuccess, TError}.MatchAny{TResult}(Func{TResult})" />
		public override SuccessMatcher<TSuccess, TError, TResult> MatchFailure<TResult>(
			Func<ConsList<TError>, TResult> func)
			=> func != null
				? new SuccessMatcher<TSuccess, TError, TResult>(this.Value, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Executes a specified <paramref name="action" /> on this value.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public override Result<TSuccess, TError> DoIfSuccess(Action<TSuccess> action)
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
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public override Result<TSuccess, TError> DoIfFailure(Action<ConsList<TError>> action)
			=> action != null ? this : throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Returns <c>Some(value)</c>.
		/// </summary>
		/// <returns><c>Some(value)</c></returns>
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
		/// The other value may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if this value equals other's value.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="Equals(Success{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(object other)
			=> other is Success<TSuccess, TError> otherSuccess &&
			   this.Equals(otherSuccess);

		/// <summary>
		/// Checks whether this value equals another value.
		/// The other value may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if this value equals other's value.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Success{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public override bool Equals(Result<TSuccess, TError> other)
			=> other is Success<TSuccess, TError> otherSuccess &&
			   this.Equals(otherSuccess);

		/// <summary>
		/// Checks whether this value equals another value.
		/// The other value may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if this value equals other's value.
		/// Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public bool Equals(Success<TSuccess, TError> other)
			=> other != null && this.Value.Equals(other.Value);

		/// <summary>
		/// Gets this value's hash code.
		/// </summary>
		/// <returns>This value's hash code.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="Equals(Success{TSuccess, TError})" />
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
