using System;
using System.Collections;
using System.Collections.Generic;

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
		// TODO Make this constructor private-protected when C# 7.2 is out
		/// <summary>
		/// Initializes a new instance of the <see cref="Option{T}" /> class.
		/// </summary>
		internal Option() { }

		/// <summary>
		/// Gets the value if it's present, or an alternative otherwise.
		/// </summary>
		/// <param name="alternative">
		/// The value to provide if this option doesn't have one.
		/// </param>
		/// <returns>The value if it's present, or an alternative otherwise.</returns>
		public abstract T GetOrDefault(T alternative);

		/// <summary>
		/// Applies a specified function to this value if it's present.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// Some(func(value)) if the value is present and None otherwise.
		/// </returns>
		public abstract Option<V> Map<V>(Func<T, V> func);

		/// <summary>
		/// Applies a specified function to this value if it's present.
		/// </summary>
		/// <typeparam name="V">The type of the returned value.</typeparam>
		/// <param name="func">The function to apply.</param>
		/// <returns>
		/// func(value) if the value is present and None otherwise.
		/// </returns>
		public abstract Option<V> Bind<V>(Func<T, Option<V>> func);

		/// <summary>
		/// Executes a specified action if the value is present.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public abstract Option<T> IfSome(Action<T> action);

		/// <summary>
		/// Executes a specified action if the value is absent.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		/// <returns><c>this</c></returns>
		public abstract Option<T> IfNone(Action action);

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
		/// Gets an enumerator which contains this value if it's present
		/// or is empty otherwise.
		/// </summary>
		/// <returns>
		/// An enumerator which contains this value if it's present
		/// or is empty otherwise.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();

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
		public abstract bool Equals(Option<T> other);

		/// <summary>
		/// Gets this value's hash code.
		/// </summary>
		/// <returns>This value's hash code.</returns>
		public abstract override int GetHashCode();

		/// <summary>
		/// Returns a string representation of this option.
		/// </summary>
		/// <returns>A string representation of this option.</returns>
		public abstract override string ToString();
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
		/// <seealso cref="ToOption{T}(T)" />
		public static Option<T> From<T>(T value)
			=> value == null ? Empty<T>() : new Some<T>(value);

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
		public static Option<T> ToOption<T>(this T value)
			=> From(value);

		/// <summary>
		/// Lifts a specified function to the <seealso cref="Option{T}" /> world.
		/// </summary>
		/// <typeparam name="T">The input type of the function.</typeparam>
		/// <typeparam name="V">The output type of the function.</typeparam>
		/// <param name="func">The function to lift.</param>
		/// <returns>A lifted version of the specified function.</returns>
		public static Func<Option<T>, Option<V>> Lift<T, V>(Func<T, V> func)
			=> value => value.Map(func);

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
		public static Func<Option<T>, Option<V>> Apply<T, V>(
			this Option<Func<T, V>> func)
			=> value => func.Bind(value.Map);
	}
}
