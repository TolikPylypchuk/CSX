using System;

namespace CSX.Functions
{
	/// <summary>
	/// Contains helper and extension methods to work with functions.
	/// </summary>
	public static class Function
	{
		/// <summary>
		/// Returns the provided <paramref name="value" />, even if it is <c>null</c>.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="value" />.</typeparam>
		/// <param name="value">The value to return.</param>
		/// <returns>The provided <paramref name="value" />.</returns>
		public static T Identity<T>(T value)
			=> value;

		/// <summary>
		/// Returns the provided <paramref name="value" /> cast to a base type,
		/// even if it is <c>null</c>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the <paramref name="value" />.
		/// </typeparam>
		/// <typeparam name="TBase">
		/// The base type of the <paramref name="value" />.
		/// </typeparam>
		/// <param name="value">The value to return.</param>
		/// <returns>
		/// The provided <paramref name="value" /> cast to a base type.
		/// </returns>
		public static TBase Cast<T, TBase>(T value) where T : TBase
			=> value;

		/// <summary>
		/// Returns the provided <paramref name="value" /> cast to another type,
		/// even if it is <c>null</c>.
		/// </summary>
		/// <typeparam name="TFrom">The type of the <paramref name="value" />.</typeparam>
		/// <typeparam name="TTo">The base type to cast to</typeparam>
		/// <param name="value">The value to return.</param>
		/// <returns>
		/// The provided <paramref name="value" /> cast to another type.
		/// </returns>
		public static TTo UnsafeCast<TFrom, TTo>(TFrom value) where TTo : class
			=> value as TTo;

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="function" /> is <c>null</c>.
		/// </exception>
		public static Func<TResult> Curried<TResult>(this Func<TResult> function)
			=> function ?? throw new ArgumentNullException(nameof(function));

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="function" /> is <c>null</c>.
		/// </exception>
		public static Func<T1, TResult> Curried<T1, TResult>(
			this Func<T1, TResult> function)
			=> function ?? throw new ArgumentNullException(nameof(function));

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, TResult>> Curried<T1, T2, TResult>(
			this Func<T1, T2, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => function(arg1, arg2);
		}
			

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, TResult>>> Curried<T1, T2, T3, TResult>(
			this Func<T1, T2, T3, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => function(arg1, arg2, arg3);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>>
			Curried<T1, T2, T3, T4, TResult>(
				this Func<T1, T2, T3, T4, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => function(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>>
			Curried<T1, T2, T3, T4, T5, TResult>(
				this Func<T1, T2, T3, T4, T5, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5
				=> function(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6
				=> function(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
			Func<T7, TResult>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7
				=> function(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
			Func<T7, Func<T8, TResult>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8
				=> function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, TResult>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> function(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, TResult>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => function(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, TResult>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => function(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, TResult>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
				function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => function(
					arg1, arg2, arg3, arg4, arg5, arg6,
					arg7, arg8, arg9, arg10, arg11, arg12);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12,
					Func<T13, TResult>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
					function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => function(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7,
					arg8, arg9, arg10, arg11, arg12, arg13);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13,
					Func<T14, TResult>>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
					T13, T14, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => arg14 => function(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
					arg9, arg10, arg11, arg12, arg13, arg14);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13,
					Func<T14, Func<T15, TResult>>>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
				T13, T14, T15, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
					T13, T14, T15, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => arg14 => arg15 => function(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
					arg9, arg10, arg11, arg12, arg13, arg14, arg15);
		}

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13,
					Func<T14, Func<T15, Func<T16, TResult>>>>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
				T13, T14, T15, T16, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
					T13, T14, T15, T16, TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException(nameof(function));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => arg14 => arg15 => arg16 => function(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
					arg10, arg11, arg12, arg13, arg14, arg15, arg16);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Action Curried(this Action action)
			=> action ?? throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Action<T1> Curried<T1>(
			this Action<T1> action)
			=> action ?? throw new ArgumentNullException(nameof(action));

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Action<T2>> Curried<T1, T2>(
			this Action<T1, T2> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => action(arg1, arg2);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Action<T3>>> Curried<T1, T2, T3>(
			this Action<T1, T2, T3> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => action(arg1, arg2, arg3);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Action<T4>>>>
			Curried<T1, T2, T3, T4>(
				this Action<T1, T2, T3, T4> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => action(arg1, arg2, arg3, arg4);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>>
			Curried<T1, T2, T3, T4, T5>(
				this Action<T1, T2, T3, T4, T5> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5
				=> action(arg1, arg2, arg3, arg4, arg5);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>>
			Curried<T1, T2, T3, T4, T5, T6>(
				this Action<T1, T2, T3, T4, T5, T6> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6
				=> action(arg1, arg2, arg3, arg4, arg5, arg6);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7>(
				this Action<T1, T2, T3, T4, T5, T6, T7> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7
				=> action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
			Func<T7, Action<T8>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8
				=> action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Action<T9>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => action(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Action<T11>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => action(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Action<T12>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
					action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => action(
					arg1, arg2, arg3, arg4, arg5, arg6,
					arg7, arg8, arg9, arg10, arg11, arg12);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12,
					Action<T13>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
					action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => action(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7,
					arg8, arg9, arg10, arg11, arg12, arg13);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13,
					Action<T14>>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
				action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => arg14 => action(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
					arg9, arg10, arg11, arg12, arg13, arg14);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13,
					Func<T14, Action<T15>>>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
				T13, T14, T15>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
					T13, T14, T15> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => arg14 => arg15 => action(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
					arg9, arg10, arg11, arg12, arg13, arg14, arg15);
		}

		/// <summary>
		/// Creates a curried version of the specified action.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the sixth argument.</typeparam>
		/// <typeparam name="T7">The type of the seventh argument.</typeparam>
		/// <typeparam name="T8">The type of the eighth argument.</typeparam>
		/// <typeparam name="T9">The type of the ninth argument.</typeparam>
		/// <typeparam name="T10">The type of the tenth argument.</typeparam>
		/// <typeparam name="T11">The type of the eleventh argument.</typeparam>
		/// <typeparam name="T12">The type of the twelfth argument.</typeparam>
		/// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
		/// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
		/// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
		/// <typeparam name="T16">The type of the sixteenth argument.</typeparam>
		/// <param name="action">The action to curry.</param>
		/// <returns>A curried action.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6,
				Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13,
					Func<T14, Func<T15, Action<T16>>>>>>>>>>>>>>>>
			Curried<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
				T13, T14, T15, T16>(
				this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12,
					T13, T14, T15, T16> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}

			return arg1 => arg2 => arg3 => arg4 => arg5 => arg6 => arg7 => arg8 => arg9
				=> arg10 => arg11 => arg12 => arg13 => arg14 => arg15 => arg16 => action(
					arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
					arg10, arg11, arg12, arg13, arg14, arg15, arg16);
		}
	}
}
