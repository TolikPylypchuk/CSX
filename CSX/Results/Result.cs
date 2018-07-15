using System;
using System.Collections;
using System.Collections.Generic;

using CSX.Collections;
using CSX.Exceptions;
using CSX.Options;
using CSX.Results.Matchers;

namespace CSX.Results
{
	/// <summary>
	/// Represents a result of a computation that can be either a success or a failure.
	/// </summary>
	/// <typeparam name="TSuccess">The type of the success value.</typeparam>
	/// <typeparam name="TError">The type of the failure value.</typeparam>
	/// <seealso cref="Result" />
	/// <seealso cref="Success{TSuccess, TError}" />
	/// <seealso cref="Failure{TSuccess, TError}" />
	public abstract class Result<TSuccess, TError> :
		IEquatable<Result<TSuccess, TError>>, IEnumerable, IEnumerable<TSuccess>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Result{TSuccess, TError}" /> class.
		/// </summary>
		private protected Result() { }

		/// <summary>
		/// Gets the value if it's a success, or an alternative value otherwise.
		/// The alternative value may be <see langword="null" />.
		/// </summary>
		/// <param name="alternative">The value to provide if this result is a failure.</param>
		/// <returns>The value if it's a succcess, or an alternative value otherwise.</returns>
		/// <seealso cref="GetOrElse(Func{ConsList{TError}, TSuccess})" />
		/// <seealso cref="GetOrThrow(Func{ConsList{TError}, Exception})" />
		public abstract TSuccess GetOrElse(TSuccess alternative);

		/// <summary>
		/// Gets the value if it's a success, or an alternative value otherwise.
		/// The alternative value may be <see langword="null" />.
		/// </summary>
		/// <param name="alternativeProvider">
		/// The function which provides the alternative value if this result is a failure.
		/// </param>
		/// <returns>The value if it's a succcess, or an alternative value otherwise.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="alternativeProvider" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		/// <seealso cref="GetOrThrow(Func{ConsList{TError}, Exception})" />
		public abstract TSuccess GetOrElse(Func<ConsList<TError>, TSuccess> alternativeProvider);

		/// <summary>
		/// Gets the value if it's a success, or throws a provided exception otherwise.
		/// </summary>
		/// <param name="exceptionProvider">The function which provides an exception to throw.</param>
		/// <returns>The result if it's a success.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="exceptionProvider" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="exceptionProvider" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(TSuccess)" />
		/// <seealso cref="GetOrElse(Func{ConsList{TError}, TSuccess})" />
		public abstract TSuccess GetOrThrow(Func<ConsList<TError>, Exception> exceptionProvider);

		/// <summary>
		/// Applies a specified function to this value if it's a success.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A mapped success or a failure.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="MapFailure{VError}(Func{ConsList{TError}, ConsList{VError}})" />
		/// <seealso cref="Bind{VSuccess}(Func{TSuccess, Result{VSuccess, TError}})" />
		public abstract Result<VSuccess, TError> Map<VSuccess>(Func<TSuccess, VSuccess> func);

		/// <summary>
		/// Applies a specified function to the errors if it's a failure.
		/// </summary>
		/// <typeparam name="VError">The type of the returned errors.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A mapped failure or a success.</returns>
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
		public abstract Result<TSuccess, VError> MapFailure<VError>(Func<ConsList<TError>, ConsList<VError>> func);

		/// <summary>
		/// Applies a specified function to this value if it's a success.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>A bound success or a failure.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="MapFailure{VError}(Func{ConsList{TError}, ConsList{VError}})" />
		public abstract Result<VSuccess, TError> Bind<VSuccess>(
			Func<TSuccess, Result<VSuccess, TError>> func);

		/// <summary>
		/// Returns the result of the specified function if this result is a success.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this result is a success, then the result of the function, provided to the failure matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchFailure{TResult}(Func{ConsList{TError}, TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public abstract FailureMatcher<TSuccess, TError, TResult> MatchSuccess<TResult>(Func<TSuccess, TResult> func);

		/// <summary>
		/// Returns the result of the specified function if this result is a failure.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this result is a success, then the result of the function, provided to the success matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchSuccess{TResult}(Func{TSuccess, TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public abstract SuccessMatcher<TSuccess, TError, TResult> MatchFailure<TResult>(
			Func<ConsList<TError>, TResult> func);

		/// <summary>
		/// Returns a result of the specified function.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>The result of <paramref name="func" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchSuccess{TResult}(Func{TSuccess, TResult})" />
		/// <seealso cref="MatchFailure{TResult}(Func{ConsList{TError}, TResult})" />
		public TResult MatchAny<TResult>(Func<TResult> func)
			=> func != null ? func() : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Executes a specified action if this result is a success.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public abstract Result<TSuccess, TError> DoIfSuccess(Action<TSuccess> action);

		/// <summary>
		/// Executes a specified action if this result is a failure.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		public abstract Result<TSuccess, TError> DoIfFailure(Action<ConsList<TError>> action);

		/// <summary>
		/// Converts this result to an option.
		/// </summary>
		/// <returns>An option containing the value if this result is a success. Otherwise, an empty option.</returns>
		public abstract Option<TSuccess> ToOption();
		
		/// <summary>
		/// Gets an enumerator which contains this value if it's a success, or is empty otherwise.
		/// </summary>
		/// <returns>An enumerator which contains this value if it's a success, or is empty otherwise.</returns>
		public abstract IEnumerator<TSuccess> GetEnumerator();

		/// <summary>
		/// Checks whether this value equals another value. The other value may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if this value equals other's value. Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		/// <seealso cref="GetHashCode" />
		public abstract override bool Equals(object other);

		/// <summary>
		/// Checks whether this value equals another value. The other value may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if this value equals other's value. Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="GetHashCode" />
		public abstract bool Equals(Result<TSuccess, TError> other);

		/// <summary>
		/// Gets the hash code of this value or errors.
		/// </summary>
		/// <returns>The hash code of this value or errors.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Result{TSuccess, TError})" />
		public abstract override int GetHashCode();

		/// <summary>
		/// Returns a string representation of this result.
		/// </summary>
		/// <returns>A string representation of this result.</returns>
		public abstract override string ToString();

		/// <summary>
		/// Gets an enumerator which contains this value if it's a success, or is empty otherwise.
		/// </summary>
		/// <returns>An enumerator which contains this value if it's a success, or is empty otherwise.</returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();
	}

	/// <summary>
	/// Contains helper and extension methods to work with results.
	/// </summary>
	/// <seealso cref="Result{TSuccess, TError}" />
	public static class Result
	{
		/// <summary>
		/// Returns a successful result containing the specified value.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="value">The value of the result.</param>
		/// <returns>A successful result containing the specified value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="value" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Succeed{TSuccess}(TSuccess)" />
		/// <seealso cref="ToSuccess{TSuccess, TError}(TSuccess)" />
		/// <seealso cref="ToSuccess{TSuccess}(TSuccess)" />
		public static Result<TSuccess, TError> Succeed<TSuccess, TError>(TSuccess value)
			=> value != null
				? new Success<TSuccess, TError>(value)
				: throw new ArgumentNullException(nameof(value));

		/// <summary>
		/// Returns a successful result containing the specified value.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="value">The value of the result.</param>
		/// <returns>A successful result containing the specified value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="value" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Succeed{TSuccess, TError}(TSuccess)" />
		/// <seealso cref="ToSuccess{TSuccess, TError}(TSuccess)" />
		/// <seealso cref="ToSuccess{TSuccess}(TSuccess)" />
		public static Result<TSuccess, string> Succeed<TSuccess>(TSuccess value)
			=> Succeed<TSuccess, string>(value);

		/// <summary>
		/// Returns a successful result containing the specified value.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="value">The value of the result.</param>
		/// <returns>A successful result containing the specified value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="value" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Succeed{TSuccess, TError}(TSuccess)" />
		/// <seealso cref="Succeed{TSuccess}(TSuccess)" />
		/// <seealso cref="ToSuccess{TSuccess}(TSuccess)" />
		public static Result<TSuccess, TError> ToSuccess<TSuccess, TError>(this TSuccess value)
			=> Succeed<TSuccess, TError>(value);

		/// <summary>
		/// Returns a successful result containing the specified value.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="value">The value of the result.</param>
		/// <returns>A successful result containing the specified value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="value" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Succeed{TSuccess, TError}(TSuccess)" />
		/// <seealso cref="Succeed{TSuccess}(TSuccess)" />
		/// <seealso cref="ToSuccess{TSuccess, TError}(TSuccess)" />
		public static Result<TSuccess, string> ToSuccess<TSuccess>(this TSuccess value)
			=> Succeed<TSuccess, string>(value);

		/// <summary>
		/// Returns a failed result containing the specified error.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="error">The error of the result.</param>
		/// <returns>A failed result containing the specified error.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, TError> Fail<TSuccess, TError>(TError error)
			=> error != null
				? new Failure<TSuccess, TError>(error)
				: throw new ArgumentNullException(nameof(error));

		/// <summary>
		/// Returns a failed result containing the specified error.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="error">The error of the result.</param>
		/// <returns>A failed result containing the specified error.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, string> Fail<TSuccess>(string error)
			=> Fail<TSuccess, string>(error);

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, TError> Fail<TSuccess, TError>(ConsList<TError> errors)
			=> errors != null
				? new Failure<TSuccess, TError>(errors)
				: throw new ArgumentNullException(nameof(errors));

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, string> Fail<TSuccess>(ConsList<string> errors)
			=> Fail<TSuccess, string>(errors);

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, TError> Fail<TSuccess, TError>(IEnumerable<TError> errors)
			=> errors != null
				? Fail<TSuccess, TError>(ConsList.Copy(errors))
				: throw new ArgumentNullException(nameof(errors));

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, string> Fail<TSuccess>(IEnumerable<string> errors)
			=> Fail<TSuccess, string>(ConsList.Copy(errors));

		/// <summary>
		/// Returns a failed result containing the specified error.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="error">The error of the result.</param>
		/// <returns>A failed result containing the specified error.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, TError> ToFailure<TSuccess, TError>(this TError error)
			=> Fail<TSuccess, TError>(error);

		/// <summary>
		/// Returns a failed result containing the specified error.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="error">The error of the result.</param>
		/// <returns>A failed result containing the specified error.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, string> ToFailure<TSuccess>(this string error)
			=> Fail<TSuccess, string>(error);

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, TError> ToFailure<TSuccess, TError>(this ConsList<TError> errors)
			=> Fail<TSuccess, TError>(errors);

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, string> ToFailure<TSuccess>(this ConsList<string> errors)
			=> Fail<TSuccess, string>(errors);

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess}(IEnumerable{string})" />
		public static Result<TSuccess, TError> ToFailure<TSuccess, TError>(this IEnumerable<TError> errors)
			=> Fail<TSuccess, TError>(errors);

		/// <summary>
		/// Returns a failed result containing the specified errors.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="errors">The errors of the result.</param>
		/// <returns>A failed result containing the specified errors.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="errors" /> are <see langword="null" />.
		/// </exception>
		/// <seealso cref="Fail{TSuccess, TError}(TError)" />
		/// <seealso cref="Fail{TSuccess}(string)" />
		/// <seealso cref="Fail{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="Fail{TSuccess}(ConsList{string})" />
		/// <seealso cref="Fail{TSuccess, TError}(IEnumerable{TError})" />
		/// <seealso cref="Fail{TSuccess}(IEnumerable{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(TError)" />
		/// <seealso cref="ToFailure{TSuccess}(string)" />
		/// <seealso cref="ToFailure{TSuccess, TError}(ConsList{TError})" />
		/// <seealso cref="ToFailure{TSuccess}(ConsList{string})" />
		/// <seealso cref="ToFailure{TSuccess, TError}(IEnumerable{TError})" />
		public static Result<TSuccess, string> ToFailure<TSuccess>(this IEnumerable<string> errors)
			=> Fail<TSuccess, string>(errors);

		/// <summary>
		/// Returns a function which maps the provided result when called.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="func">The funciton used during the mapping.</param>
		/// <returns>A function which when called will map the provided value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Lift{TSuccess, VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Apply{TSuccess, VSuccess, TError}(Result{Func{TSuccess, VSuccess}, TError})" />
		/// <seealso cref="Apply{TSuccess, VSuccess}(Result{Func{TSuccess, VSuccess}, string})" />
		public static Func<Result<TSuccess, TError>, Result<VSuccess, TError>> Lift<TSuccess, VSuccess, TError>(
				this Func<TSuccess, VSuccess> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return value =>
				value != null
					? value.Map(func)
					: throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Returns a function which maps the provided result when called.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <param name="func">The funciton to lift.</param>
		/// <returns>A function which when called will map the provided value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Lift{TSuccess, VSuccess, TError}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Apply{TSuccess, VSuccess, TError}(Result{Func{TSuccess, VSuccess}, TError})" />
		/// <seealso cref="Apply{TSuccess, VSuccess}(Result{Func{TSuccess, VSuccess}, string})" />
		public static Func<Result<TSuccess, string>, Result<VSuccess, string>> Lift<TSuccess, VSuccess>(
			this Func<TSuccess, VSuccess> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return value =>
				value != null
					? value.Map(func)
					: throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Applies a specified function, if it's a success, to a value, if it's a success.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="funcResult">
		/// The function to apply, if it's a success. It must not return a <see langword="null" />.
		/// </param>
		/// <returns>
		/// A lifted version of the specified function, if it's a success.
		/// Otherwise, a function which always returns a failure.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="funcResult" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Lift{TSuccess, VSuccess, TError}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Lift{TSuccess, VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Apply{TSuccess, VSuccess}(Result{Func{TSuccess, VSuccess}, string})" />
		public static Func<Result<TSuccess, TError>, Result<VSuccess, TError>> Apply<TSuccess, VSuccess, TError>(
				this Result<Func<TSuccess, VSuccess>, TError> funcResult)
		{
			if (funcResult == null)
			{
				throw new ArgumentNullException(nameof(funcResult));
			}

			return valueResult =>
			{
				if (valueResult == null)
				{
					throw new ArgumentNullException(nameof(valueResult));
				}

				Func<ConsList<TError>, Result<VSuccess, TError>> fail = Fail<VSuccess, TError>;

				return funcResult
					.MatchSuccess(func =>
						valueResult
							.MatchSuccess(value =>
							{
								var result = func(value);

								if (result == null)
								{
									throw new UnacceptableNullException("The result must not be null.");
								}

								return Succeed<VSuccess, TError>(result);
							})
							.MatchFailure(valueErrors => fail(valueErrors)))
					.MatchFailure(funcErrors =>
						valueResult
							.MatchSuccess(_ => fail(funcErrors))
							.MatchFailure(valueErrors => fail(funcErrors.Add(valueErrors))));
			};
		}

		/// <summary>
		/// Applies a specified function, if it's a success, to a value, if it's a success.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <param name="funcResult">
		/// The function to apply, if it's a success. It must not return a <see langword="null" />.
		/// </param>
		/// <returns>
		/// A lifted version of the specified function, if it's a success.
		/// Otherwise, a function which always returns a failure.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="funcResult" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Lift{TSuccess, VSuccess, TError}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Lift{TSuccess, VSuccess}(Func{TSuccess, VSuccess})" />
		/// <seealso cref="Apply{TSuccess, VSuccess, TError}(Result{Func{TSuccess, VSuccess}, TError})" />
		public static Func<Result<TSuccess, string>, Result<VSuccess, string>> Apply<TSuccess, VSuccess>(
				this Result<Func<TSuccess, VSuccess>, string> funcResult)
			=> funcResult.Apply<TSuccess, VSuccess, string>();

		/// <summary>
		/// Returns a function, which returns a success if there were no exceptions, or a failure containing
		/// the exception if it is thrown. The resulting function can accept <see langword="null" />.
		/// </summary>
		/// <typeparam name="TInput">The input type of the function.</typeparam>
		/// <typeparam name="TSuccess">The output type of the function.</typeparam>
		/// <param name="func">
		/// The function which may throw an exception. It must not return a <see langword="null" />.
		/// </param>
		/// <returns>
		/// A funciton, which returns a success if there were no exceptions, or a failure containing
		/// the exception if it is thrown.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		public static Func<TInput, Result<TSuccess, Exception>> Catch<TInput, TSuccess>(
			this Func<TInput, TSuccess> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return value =>
			{
				TSuccess result;

				try
				{
					result = func(value);
				} catch (Exception e)
				{
					return Fail<TSuccess, Exception>(e);
				}

				if (result == null)
				{
					throw new UnacceptableNullException("The result must not be null.");
				}

				return Succeed<TSuccess, Exception>(result);
			};
		}
	}
}
