using System;
using System.Collections;
using System.Collections.Generic;

using CSX.Options.Matchers;
using CSX.Results;

namespace CSX.Options
{
	/// <summary>
	/// Represents a value that may or may not be present.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <seealso cref="Option" />
	/// <seealso cref="Some{T}" />
	/// <seealso cref="None{T}" />
	public abstract class Option<T> : IEquatable<Option<T>>, IEnumerable, IEnumerable<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Option{T}" /> class.
		/// </summary>
		private protected Option() { }

		/// <summary>
		/// Gets the value if it's present, or an alternative otherwise.
		/// The alternative may be <c>null</c>.
		/// </summary>
		/// <param name="alternative">
		/// The value to provide if this option doesn't have one.
		/// </param>
		/// <returns>The value if it's present, or an alternative otherwise.</returns>
		/// <seealso cref="GetOrThrow(string)" />
		public abstract T GetOrElse(T alternative);

		/// <summary>
		/// Gets the value if it's present, or throws an exception otherwise.
		/// </summary>
		/// <param name="message">The message of the exception.</param>
		/// <returns>The value if it's present.</returns>
		/// <exception cref="OptionAbsentException">
		/// The value is not present.
		/// </exception>
		/// <seealso cref="GetOrElse(T)" />
		public abstract T GetOrThrow(string message = "The value is not present.");

		/// <summary>
		/// Applies a specified function to this value if it's present.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// <c>Some(func(value))</c> if the value is present and <c>None</c> otherwise.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Bind{V}(Func{T, Option{V}})" />
		public abstract Option<V> Map<V>(Func<T, V> func);

		/// <summary>
		/// Applies a specified function to this value if it's present.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// <c>func(value)</c> if the value is present and None otherwise.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Map{V}(Func{T, V})" />
		public abstract Option<V> Bind<V>(Func<T, Option<V>> func);

		/// <summary>
		/// Returns the result of the specified function if this option is
		/// <see cref="Some{T}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this option is <see cref="None{T}" />, then the result of the function,
		/// provided to the <see cref="None{T}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="MatchNone{TResult}(Func{TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public NoneMatcher<T, TResult> MatchSome<TResult>(Func<T, TResult> func)
			=> func != null
				? new NoneMatcher<T, TResult>(this, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns the result of the specified function if this option is
		/// <see cref="None{T}" />.
		/// </summary>
		/// <param name="func">
		/// The function whose result is returned if this match succeeds.
		/// </param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this option is <see cref="Some{T}" />, then the result of the function,
		/// provided to the <see cref="Some{T}" /> matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="MatchSome{TResult}(Func{T, TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public SomeMatcher<T, TResult> MatchNone<TResult>(Func<TResult> func)
			=> func != null
				? new SomeMatcher<T, TResult>(this, func)
				: throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Returns a result of the specified function.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>The result of <paramref name="func" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="MatchSome{TResult}(Func{T, TResult})" />
		/// <seealso cref="MatchNone{TResult}(Func{TResult})" />
		public TResult MatchAny<TResult>(Func<TResult> func)
			=> func != null ? func() : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Executes a specified <paramref name="action" /> if the value is present.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfNone(Action)" />
		public abstract Option<T> DoIfSome(Action<T> action);

		/// <summary>
		/// Executes a specified <paramref name="action" /> if the value is absent.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="DoIfSome(Action{T})" />
		public abstract Option<T> DoIfNone(Action action);

		/// <summary>
		/// Converts this option to a result.
		/// </summary>
		/// <param name="error">
		/// The error to return if the value is absent.
		/// </param>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <returns>
		/// <c>Success(value)</c> if the value is present.
		/// Otherwise, <c>Failure(error)</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="ToResult(string)" />
		public abstract Result<T, TError> ToResult<TError>(TError error);

		/// <summary>
		/// Converts this option to a result.
		/// </summary>
		/// <param name="error">
		/// The error to return if the value is absent.
		/// </param>
		/// <returns>
		/// <c>Success(value)</c> if the value is present.
		/// Otherwise, <c>Failure(error)</c>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="ToResult{TError}(TError)" />
		public abstract Result<T, string> ToResult(string error);

		/// <summary>
		/// Gets an enumerator which contains this value if it's present
		/// or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's present
		/// or is empty otherwise.
		/// </returns>
		public abstract IEnumerator<T> GetEnumerator();

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
		/// <seealso cref="GetHashCode" />
		public abstract override bool Equals(object other);

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
		/// <seealso cref="GetHashCode" />
		public abstract bool Equals(Option<T> other);

		/// <summary>
		/// Gets this value's hash code.
		/// </summary>
		/// <returns>This value's hash code.</returns>
		/// <seealso cref="Equals(object)" />
		/// <seealso cref="Equals(Option{T})" />
		public abstract override int GetHashCode();

		/// <summary>
		/// Returns a string representation of this option.
		/// </summary>
		/// <returns>A string representation of this option.</returns>
		public abstract override string ToString();

		/// <summary>
		/// Gets an enumerator which contains this value if it's present
		/// or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's present
		/// or is empty otherwise.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();
	}

	/// <summary>
	/// Constains helper and extension methods to work with options.
	/// </summary>
	/// <seealso cref="Option{T}" />
	public static class Option
	{
		/// <summary>
		/// Constructs an option from a <paramref name="value" />.
		/// If the <paramref name="value" /> is <c>null</c>,
		/// returns <see cref="None{T}" />.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">
		/// The value that the constructed option will contain.
		/// </param>
		/// <returns>
		/// An option which contains the <paramref name="value" />
		/// or <see cref="None{T}" /> if the value is <c>null</c>.
		/// </returns>
		/// <seealso cref="From{T}(T?)" />
		/// <seealso cref="ToOption{T}(T)" />
		/// <seealso cref="ToOption{T}(T?)" />
		public static Option<T> From<T>(T value)
			=> value != null ? new Some<T>(value) : Empty<T>();

		/// <summary>
		/// Constructs an option from a nullable <paramref name="value" />.
		/// If the <paramref name="value" /> is <c>null</c>,
		/// returns <see cref="None{T}" />.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">
		/// The value that the constructed option will contain.
		/// </param>
		/// <returns>
		/// An option which contains the <paramref name="value" />
		/// or <see cref="None{T}" /> if the value is <c>null</c>.
		/// </returns>
		/// <seealso cref="From{T}(T)" />
		/// <seealso cref="ToOption{T}(T)" />
		/// <seealso cref="ToOption{T}(T?)" />
		public static Option<T> From<T>(T? value)
			where T : struct
			=> value.HasValue ? From(value.Value) : Empty<T>();

		/// <summary>
		/// Constructs an empty option, i.e. <see cref="None{T}" />.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the value this option doesn't contain.
		/// </typeparam>
		/// <returns><see cref="None{T}" /></returns>
		public static Option<T> Empty<T>()
			=> new None<T>();

		/// <summary>
		/// Constructs an option from a <paramref name="value" />.
		/// If the <paramref name="value" /> is <c>null</c>,
		/// returns <see cref="None{T}" />.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">
		/// The value that the constructed option will contain.
		/// </param>
		/// <returns>
		/// An option which contains the <paramref name="value" />
		/// or <see cref="None{T}" /> if the value is <c>null</c>.
		/// </returns>
		/// <seealso cref="From{T}(T)" />
		/// <seealso cref="From{T}(T?)" />
		/// <seealso cref="ToOption{T}(T?)" />
		public static Option<T> ToOption<T>(this T value)
			=> From(value);

		/// <summary>
		/// Constructs an option from a nullable <paramref name="value" />.
		/// If the <paramref name="value" /> is <c>null</c>,
		/// returns <see cref="None{T}" />.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">
		/// The value that the constructed option will contain.
		/// </param>
		/// <returns>
		/// An option which contains the <paramref name="value" />
		/// or <see cref="None{T}" /> if the value is <c>null</c>.
		/// </returns>
		/// <seealso cref="From{T}(T)" />
		/// <seealso cref="From{T}(T?)" />
		/// <seealso cref="ToOption{T}(T)" />
		public static Option<T> ToOption<T>(this T? value)
			where T : struct
			=> From(value);

		/// <summary>
		/// Returns a function which maps the provided option when called.
		/// </summary>
		/// <typeparam name="T">The input type of the function.</typeparam>
		/// <typeparam name="V">The output type of the function.</typeparam>
		/// <param name="func">The funciton to lift.</param>
		/// <returns>A function which maps the provided option when called.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Apply{T, V}(Option{Func{T, V}})" />
		public static Func<Option<T>, Option<V>> Lift<T, V>(Func<T, V> func)
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
		/// Applies a specified function, if it's present, to a value,
		/// if it's present.
		/// </summary>
		/// <typeparam name="T">The input type of the function.</typeparam>
		/// <typeparam name="V">The output type of the function.</typeparam>
		/// <param name="func">The function to apply, if it's present.</param>
		/// <returns>
		/// A lifted version of the specified function, if it's present.
		/// Otherwise, a function which always returns <see cref="None{T}" />.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Lift{T, V}(Func{T, V})" />
		public static Func<Option<T>, Option<V>> Apply<T, V>(
			this Option<Func<T, V>> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException(nameof(func));
			}

			return value =>
				value != null
					? func.Bind(value.Map)
					: throw new ArgumentNullException(nameof(value));
		}
	}
}
