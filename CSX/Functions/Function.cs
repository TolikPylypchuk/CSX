using System;

namespace CSX.Functions
{
	/// <summary>
	/// Contains helper and extension methods to work with functions.
	/// </summary>
	public static class Function
	{
		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<TResult> Curry<TResult>(this Func<TResult> function)
			=> function;

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, TResult> Curry<T1, TResult>(
			this Func<T1, TResult> function)
			=> function;

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(
			this Func<T1, T2, TResult> function)
			=> a => b => function(a, b);

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(
			Func<T1, T2, T3, TResult> function)
			=> a => b => c => function(a, b, c);

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
			Curry<T1, T2, T3, T4, TResult>(
				this Func<T1, T2, T3, T4, TResult> function)
			=> a => b => c => d => function(a, b, c, d);
		
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
			Curry<T1, T2, T3, T4, T5, TResult>(
				this Func<T1, T2, T3, T4, T5, TResult> function)
			=> a => b => c => d => e => function(a, b, c, d, e);

		/// <summary>
		/// Creates a curried version of the specified function.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument.</typeparam>
		/// <typeparam name="T2">The type of the second argument.</typeparam>
		/// <typeparam name="T3">The type of the third argument.</typeparam>
		/// <typeparam name="T4">The type of the fourth argument.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument.</typeparam>
		/// <typeparam name="T6">The type of the fifth argument.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="function">The function to curry.</param>
		/// <returns>A curried function.</returns>
		public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>>
			Curry<T1, T2, T3, T4, T5, T6, TResult>(
				this Func<T1, T2, T3, T4, T5, T6, TResult> function)
			=> a => b => c => d => e => f => function(a, b, c, d, e, f);
	}
}
