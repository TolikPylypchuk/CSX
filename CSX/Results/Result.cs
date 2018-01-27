using System;
using System.Collections;
using System.Collections.Generic;

using CSX.Lists;
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
		/// Gets the value if it's a success, or an alternative otherwise.
		/// </summary>
		/// <param name="alternative">
		/// The value to provide if this result is a failure.
		/// </param>
		/// <returns>The value if it's a succcess, or an alternative otherwise.</returns>
		public abstract TSuccess GetOrElse(TSuccess alternative);

		/// <summary>
		/// Applies a specified function to this value if it's a success.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// Success(func(value)) if it's a success and Failure otherwise.
		/// </returns>
		public abstract Result<VSuccess, TError> Map<VSuccess>(
			Func<TSuccess, VSuccess> func);

		/// <summary>
		/// Applies a specified function to this error if it's a failure.
		/// </summary>
		/// <typeparam name="VError">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// Failure(func(value)) if it's a failure and Success otherwise.
		/// </returns>
		public abstract Result<TSuccess, VError> MapFailure<VError>(
			Func<TError, VError> func);

		/// <summary>
		/// Applies a specified function to this value if it's a success.
		/// </summary>
		/// <typeparam name="VSuccess">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// func(value) if it's a success and Failure otherwise.
		/// </returns>
		public abstract Result<VSuccess, TError> Bind<VSuccess>(
			Func<TSuccess, Result<VSuccess, TError>> func);

		/// <summary>
		/// Returns the result of the specified function if this result is
		/// a <see cref="Success{TSuccess, TError}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="V">The type of the match result.</typeparam>
		/// <returns>
		/// If this result is a <see cref="Failure{TSuccess, TError}" />,
		/// then the result of the function, provided to the
		/// <see cref="Failure{TSuccess, TError}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		public FailureMatcher<TSuccess, TError, V> MatchSuccess<V>(Func<TSuccess, V> func)
			=> new FailureMatcher<TSuccess, TError, V>(this, func);

		/// <summary>
		/// Returns the result of the specified function if this result is
		/// a <see cref="Failure{TSuccess, TError}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="V">The type of the match result.</typeparam>
		/// <returns>
		/// If this result is a <see cref="Success{TSuccess, TError}" />,
		/// then the result of the function, provided to the
		/// <see cref="Success{TSuccess, TError}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		public SuccessMatcher<TSuccess, TError, V> MatchFailure<V>(
			Func<ConsList<TError>, V> func)
			=> new SuccessMatcher<TSuccess, TError, V>(this, func);

		/// <summary>
		/// Returns a result of the specified function.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <returns>The result of <paramref name="func" />.</returns>
		public V MatchAny<V>(Func<V> func)
			=> func();

		/// <summary>
		/// Executes a specified action if it's a success.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public abstract Result<TSuccess, TError> DoIfSuccess(Action<TSuccess> action);

		/// <summary>
		/// Executes a specified action if it's a failure.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public abstract Result<TSuccess, TError> DoIfFailure(Action<ConsList<TError>> action);

		/// <summary>
		/// Converts this result to an option.
		/// </summary>
		/// <returns>Some(value) if it's a success. Otherwise, None.</returns>
		public abstract Option<TSuccess> ToOption();

		/// <summary>
		/// Gets an enumerator which contains this value if it's a success
		/// or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's a success
		/// or is empty otherwise.
		/// </returns>
		public abstract IEnumerator<TSuccess> GetEnumerator();

		/// <summary>
		/// Checks whether this value equals another value.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public abstract override bool Equals(object other);

		/// <summary>
		/// Checks whether this value equals another value.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <c>true</c> if this value equals other's value.
		/// Otherwise, <c>false</c>.
		/// </returns>
		public abstract bool Equals(Result<TSuccess, TError> other);

		/// <summary>
		/// Gets the hash code of this value or error.
		/// </summary>
		/// <returns>The hash code of this value or error.</returns>
		public abstract override int GetHashCode();

		/// <summary>
		/// Returns a string representation of this result.
		/// </summary>
		/// <returns>A string representation of this result.</returns>
		public abstract override string ToString();

		/// <summary>
		/// Gets an enumerator which contains this value if it's a success
		/// or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's a success
		/// or is empty otherwise.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();
	}

	/// <summary>
	/// Constains helper and extension methods to work with results.
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
		/// <paramref name="value" /> is <c>null</c>.
		/// </exception>
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
		/// <paramref name="value" /> is <c>null</c>.
		/// </exception>
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
		/// <paramref name="value" /> is <c>null</c>.
		/// </exception>
		public static Result<TSuccess, TError> ToSuccess<TSuccess, TError>(
			this TSuccess value)
			=> Succeed<TSuccess, TError>(value);

		/// <summary>
		/// Returns a successful result containing the specified value.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="value">The value of the result.</param>
		/// <returns>A successful result containing the specified value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="value" /> is <c>null</c>.
		/// </exception>
		public static Result<TSuccess, string> ToSuccess<TSuccess>(
			this TSuccess value)
			=> Succeed<TSuccess, string>(value);

		/// <summary>
		/// Returns a failed result containing the specified error.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="error">The error of the result.</param>
		/// <returns>A failed result containing the specified error.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
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
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
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
		/// <paramref name="errors" /> are <c>null</c>.
		/// </exception>
		public static Result<TSuccess, TError> Fail<TSuccess, TError>(
			ConsList<TError> errors)
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
		/// <paramref name="errors" /> are <c>null</c>.
		/// </exception>
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
		/// <paramref name="errors" /> are <c>null</c>.
		/// </exception>
		public static Result<TSuccess, TError> Fail<TSuccess, TError>(
			IEnumerable<TError> errors)
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
		/// <paramref name="errors" /> are <c>null</c>.
		/// </exception>
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
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		public static Result<TSuccess, TError> ToFailure<TSuccess, TError>(
			this TError error)
			=> Fail<TSuccess, TError>(error);

		/// <summary>
		/// Returns a failed result containing the specified error.
		/// </summary>
		/// <typeparam name="TSuccess">The type of the success value.</typeparam>
		/// <param name="error">The error of the result.</param>
		/// <returns>A failed result containing the specified error.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		public static Result<TSuccess, string> ToFailure<TSuccess>(this string error)
			=> Fail<TSuccess, string>(error);

		/// <summary>
		/// Returns a function which when called will map the provided value.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="func">The funciton used during the mapping.</param>
		/// <returns>A function which when called will map the provided value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public static Func<Result<TSuccess, TError>, Result<VSuccess, TError>>
			Lift<TSuccess, VSuccess, TError>(
				this Func<TSuccess, VSuccess> func)
			=> func != null
				? (Func<Result<TSuccess, TError>, Result<VSuccess, TError>>)
					(value =>
						value != null
							? value.Map(func)
							: throw new ArgumentNullException(nameof(value)))
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns a function which maps the provided result when called.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <param name="func">The funciton to lift.</param>
		/// <returns>A function which when called will map the provided value.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public static Func<Result<TSuccess, string>, Result<VSuccess, string>>
			Lift<TSuccess, VSuccess>(this Func<TSuccess, VSuccess> func)
			=> func != null
				? (Func<Result<TSuccess, string>, Result<VSuccess, string>>)
					(value =>
						value != null
							? value.Map(func)
							: throw new ArgumentNullException(nameof(value)))
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Applies a specified function, if it's a success, to a value,
		/// if it's a success.
		/// </summary>
		/// <typeparam name="TSuccess">The input type of the function.</typeparam>
		/// <typeparam name="VSuccess">The output type of the function.</typeparam>
		/// <typeparam name="TError">The type of the failure value.</typeparam>
		/// <param name="funcResult">The function to apply, if it's a success.</param>
		/// <returns>
		/// A lifted version of the specified function, if it's a success.
		/// Otherwise, a function which always returns
		/// <see cref="Failure{TSuccess, TError}" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="funcResult" /> is <c>null</c>.
		/// </exception>
		public static Func<Result<TSuccess, TError>, Result<VSuccess, TError>>
			Apply<TSuccess, VSuccess, TError>(
				this Result<Func<TSuccess, VSuccess>, TError> funcResult)
			=> funcResult != null
				? (Func<Result<TSuccess, TError>, Result<VSuccess, TError>>)
					(valueResult =>
					{
						valueResult = valueResult ??
							throw new ArgumentNullException(nameof(valueResult));

						Func<ConsList<TError>, Result<VSuccess, TError>> fail =
							Fail<VSuccess, TError>;

						return funcResult
							.MatchSuccess(func =>
								valueResult
									.MatchSuccess(value =>
										Succeed<VSuccess, TError>(func(value)))
									.MatchFailure(valueErrors => fail(valueErrors)))
							.MatchFailure(funcErrors =>
								valueResult
									.MatchSuccess(_ => fail(funcErrors))
									.MatchFailure(valueErrors =>
										fail(funcErrors.Add(valueErrors))));
					})
				: throw new ArgumentNullException(nameof(funcResult));

		/// <summary>
		/// Returns a function, which returns a success if there were no exceptions,
		/// or a failure containing an exception if it is thrown.
		/// The resulting function can accept <c>null</c>.
		/// </summary>
		/// <typeparam name="TInput">The input type of the function.</typeparam>
		/// <typeparam name="TSuccess">The output type of the function.</typeparam>
		/// <param name="func">The function which may throw an exception.</param>
		/// <returns>
		/// A funciton, which returns a success if there were no exceptions,
		/// or a failure containing an exception if it is thrown.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		public static Func<TInput, Result<TSuccess, Exception>> Catch<TInput, TSuccess>(
			this Func<TInput, TSuccess> func)
			=> func != null
				? (Func<TInput, Result<TSuccess, Exception>>)
					(value =>
					{
						try
						{
							return Succeed<TSuccess, Exception>(func(value));
						} catch (Exception e)
						{
							return Fail<TSuccess, Exception>(e);
						}
					})
				: throw new ArgumentNullException(nameof(func));
	}
}
