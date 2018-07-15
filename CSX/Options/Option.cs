﻿using System;
using System.Collections;
using System.Collections.Generic;

using CSX.Exceptions;
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
		/// Gets the value if it's present, or an alternative value otherwise.
		/// The alternative value may be <see langword="null" />.
		/// </summary>
		/// <param name="alternative">The value to provide if this option doesn't have one.</param>
		/// <returns>The value if it's present, or an alternative otherwise.</returns>
		/// <seealso cref="GetOrElse(Func{T})" />
		/// <seealso cref="GetOrThrow(Func{Exception})" />
		public abstract T GetOrElse(T alternative);

		/// <summary>
		/// Gets the value if it's present, or an alternative value otherwise.
		/// The alternative value may be <see langword="null" />.
		/// </summary>
		/// <param name="alternativeProvider">
		/// The function which provides the alternative value if this option doesn't have one.
		/// </param>
		/// <returns>The value if it's present, or an alternative value otherwise.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="alternativeProvider" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(T)" />
		/// <seealso cref="GetOrThrow(Func{Exception})" />
		public abstract T GetOrElse(Func<T> alternativeProvider);

		/// <summary>
		/// Gets the value if it's present, or throws an exception otherwise.
		/// </summary>
		/// <param name="exceptionProvider">The function which provides an exception to throw.</param>
		/// <returns>The value if it's present.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="exceptionProvider" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="exceptionProvider" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="GetOrElse(T)" />
		/// <seealso cref="GetOrElse(Func{T})" />
		public abstract T GetOrThrow(Func<Exception> exceptionProvider);

		/// <summary>
		/// Applies a specified function to this value if it's present.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>An option which contains the mapped value or is empty.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="Bind{V}(Func{T, Option{V}})" />
		public abstract Option<V> Map<V>(Func<T, V> func);

		/// <summary>
		/// Applies a specified function to this value if it's present.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>An option which is bound or is empty.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="UnacceptableNullException">
		/// <paramref name="func" /> returns <see langword="null" />.
		/// </exception>
		/// <seealso cref="Map{V}(Func{T, V})" />
		public abstract Option<V> Bind<V>(Func<T, Option<V>> func);

		/// <summary>
		/// Returns the result of the specified function if this option is present.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this option is empty, then the result of the function, provided to the none matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchNone{TResult}(Func{TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public abstract NoneMatcher<T, TResult> MatchSome<TResult>(Func<T, TResult> func);

		/// <summary>
		/// Returns the result of the specified function if this option is empty.
		/// </summary>
		/// <param name="func">The function whose result is returned if this match succeeds.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>
		/// If this option is not empty, then the result of the function, provided to the some matcher.
		/// Otherwise, the result of the specified function.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchSome{TResult}(Func{T, TResult})" />
		/// <seealso cref="MatchAny{TResult}(Func{TResult})" />
		public abstract SomeMatcher<T, TResult> MatchNone<TResult>(Func<TResult> func);

		/// <summary>
		/// Returns a result of the specified function.
		/// </summary>
		/// <param name="func">The function that provides the match result.</param>
		/// <typeparam name="TResult">The type of the match result.</typeparam>
		/// <returns>The result of <paramref name="func" />.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="MatchSome{TResult}(Func{T, TResult})" />
		/// <seealso cref="MatchNone{TResult}(Func{TResult})" />
		public TResult MatchAny<TResult>(Func<TResult> func)
			=> func != null ? func() : throw new ArgumentNullException(nameof(func));

		/// <summary>
		/// Executes a specified action if the value is present.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfNone(Action)" />
		public abstract Option<T> DoIfSome(Action<T> action);

		/// <summary>
		/// Executes a specified <paramref name="action" /> if the value is absent.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><see langword="this" /></returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="action" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="DoIfSome(Action{T})" />
		public abstract Option<T> DoIfNone(Action action);

		/// <summary>
		/// Converts this option to a result.
		/// </summary>
		/// <param name="error">The error to return if the value is absent.</param>
		/// <typeparam name="TError">The type of the error.</typeparam>
		/// <returns>
		/// A success, which contains the value, if the value is present.
		/// Otherwise, a faliure, which contains the error.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="ToResult(string)" />
		public abstract Result<T, TError> ToResult<TError>(TError error);

		/// <summary>
		/// Converts this option to a result.
		/// </summary>
		/// <param name="error">The error to return if the value is absent.</param>
		/// <returns>
		/// A success, which contains the value, if the value is present.
		/// Otherwise, a faliure, which contains the error.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="error" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="ToResult{TError}(TError)" />
		public abstract Result<T, string> ToResult(string error);

		/// <summary>
		/// Gets an enumerator which contains this value if it's present or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's present or is empty otherwise.
		/// </returns>
		public abstract IEnumerator<T> GetEnumerator();

		/// <summary>
		/// Checks whether this value equals another value. The other value may be <see langword="null" />.
		/// </summary>
		/// <param name="other">The object to compare to.</param>
		/// <returns>
		/// <see langword="true" /> if this value equals other's value. Otherwise, <see langword="false" />.
		/// </returns>
		/// <seealso cref="Equals(Option{T})" />
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
		/// Gets an enumerator which contains this value if it's present or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's present or is empty otherwise.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();
	}

	/// <summary>
	/// Contains helper and extension methods to work with options.
	/// </summary>
	/// <seealso cref="Option{T}" />
	public static class Option
	{
		/// <summary>
		/// Constructs an option from a specified value.
		/// If the value is <see langword="null" />, returns an empty option.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">The value that the constructed option will contain.</param>
		/// <returns>
		/// An option which contains the value or an empty option if the value is <see langword="null" />.
		/// </returns>
		/// <seealso cref="From{T}(T?)" />
		/// <seealso cref="ToOption{T}(T)" />
		/// <seealso cref="ToOption{T}(T?)" />
		public static Option<T> From<T>(T value)
			=> value != null ? new Some<T>(value) : Empty<T>();

		/// <summary>
		/// Constructs an option from a specified nullable value. If the value is <see langword="null" />,
		/// returns an empty option.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">The value that the constructed option will contain.</param>
		/// <returns>
		/// An option which contains the value or an empty option if the value is <see langword="null" />.
		/// </returns>
		/// <seealso cref="From{T}(T)" />
		/// <seealso cref="ToOption{T}(T)" />
		/// <seealso cref="ToOption{T}(T?)" />
		public static Option<T> From<T>(T? value)
			where T : struct
			=> value.HasValue ? From(value.Value) : Empty<T>();

		/// <summary>
		/// Constructs an empty option.
		/// </summary>
		/// <typeparam name="T">The type of the value this option doesn't contain.</typeparam>
		/// <returns><see cref="None{T}" /></returns>
		public static Option<T> Empty<T>()
			=> new None<T>();

		/// <summary>
		/// Constructs an option from specified value. If the value is <see langword="null" />,
		/// returns an empty option.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">The value that the constructed option will contain.</param>
		/// <returns>
		/// An option which contains the value or an empty option if the value is <see langword="null" />.
		/// </returns>
		/// <seealso cref="From{T}(T)" />
		/// <seealso cref="From{T}(T?)" />
		/// <seealso cref="ToOption{T}(T?)" />
		public static Option<T> ToOption<T>(this T value)
			=> From(value);

		/// <summary>
		/// Constructs an option from a specified nullable value. If the value is <see langword="null" />,
		/// returns an empty option.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">The value that the constructed option will contain.</param>
		/// <returns>
		/// An option which contains the value or an empty option if the value is <see langword="null" />.
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
		/// <paramref name="func" /> is <see langword="null" />.
		/// </exception>
		/// <seealso cref="Apply{T, V}(Option{Func{T, V}})" />
		public static Func<Option<T>, Option<V>> Lift<T, V>(this Func<T, V> func)
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
		/// Applies a specified function, if it's present, to a value, if it's present.
		/// </summary>
		/// <typeparam name="T">The input type of the function.</typeparam>
		/// <typeparam name="V">The output type of the function.</typeparam>
		/// <param name="func">The function to apply, if it's present.</param>
		/// <returns>
		/// A lifted version of the specified function, if it's present. Otherwise, a function
		/// which always returns an empty option.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="func" /> is <see langword="null" />.
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
