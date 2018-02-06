using System;

using Xunit;

using static CSX.Functions.Function;

namespace CSX.Functions
{
	public class FunctionTests
	{
		[Fact(DisplayName = "Identity returns the argument")]
		public void TestIdentity()
		{
			const int expected = 1;
			Assert.Equal(expected, Identity(expected));
		}

		[Fact(DisplayName = "Identity returns the argument when it's null")]
		public void TestIdentityNull()
		{
			Assert.Null(Identity<string>(null));
		}

		[Fact(DisplayName = "Cast returns the argument as base type")]
		public void TestCast()
		{
			const int expected = 1;
			object actual = Cast<int, Object>(expected);
			Assert.Equal(expected, actual);
		}

		[Fact(DisplayName = "Cast returns the argument as base type when it's null")]
		public void TestCastNull()
		{
			Assert.Null(Cast<string, Object>(null));
		}

		[Fact(DisplayName = "Curried<TResult> returns the function")]
		public void TestCurriedFunc0()
		{
			Func<int> func = () => 1;

			var curried = func.Curried();

			Assert.Equal(func, curried);
		}

		[Fact(DisplayName = "Curried<T1, TResult> returns the function")]
		public void TestCurriedFunc1()
		{
			Func<int, int> func = a => a;

			var curried = func.Curried();

			Assert.Equal(func, curried);
		}

		[Fact(DisplayName = "Curried<T1, T2, TResult> returns the curried function")]
		public void TestCurriedFunc2()
		{
			Func<int, int, int> func = (arg1, arg2) => arg1 + arg2;

			var curried = func.Curried();

			Assert.Equal(func(1, 2), curried(1)(2));
		}

		[Fact(DisplayName = "Curried<T1, T2, T3, TResult> returns the curried function")]
		public void TestCurriedFunc3()
		{
			Func<int, int, int, int> func = (arg1, arg2, arg3) => arg1 + arg2 + arg3;

			var curried = func.Curried();

			Assert.Equal(func(1, 2, 3), curried(1)(2)(3));
		}

		[Fact(DisplayName = "Curried<T1, ..., T4, TResult> returns the curried function")]
		public void TestCurriedFunc4()
		{
			Func<int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4) => arg1 + arg2 + arg3 + arg4;

			var curried = func.Curried();

			Assert.Equal(func(1, 2, 3, 4), curried(1)(2)(3)(4));
		}

		[Fact(DisplayName = "Curried<T1, ..., T5, TResult> returns the curried function")]
		public void TestCurriedFunc5()
		{
			Func<int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5) => arg1 + arg2 + arg3 + arg4 + arg5;

			var curried = func.Curried();

			Assert.Equal(func(1, 2, 3, 4, 5), curried(1)(2)(3)(4)(5));
		}

		[Fact(DisplayName = "Curried<T1, ..., T6, TResult> returns the curried function")]
		public void TestCurriedFunc6()
		{
			Func<int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6;

			var curried = func.Curried();

			Assert.Equal(func(1, 2, 3, 4, 5, 6), curried(1)(2)(3)(4)(5)(6));
		}

		[Fact(DisplayName = "Curried<T1, ..., T7, TResult> returns the curried function")]
		public void TestCurriedFunc7()
		{
			Func<int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7;

			var curried = func.Curried();

			Assert.Equal(func(1, 2, 3, 4, 5, 6, 7), curried(1)(2)(3)(4)(5)(6)(7));
		}

		[Fact(DisplayName = "Curried<T1, ..., T8, TResult> returns the curried function")]
		public void TestCurriedFunc8()
		{
			Func<int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8;

			var curried = func.Curried();

			Assert.Equal(func(1, 2, 3, 4, 5, 6, 7, 8), curried(1)(2)(3)(4)(5)(6)(7)(8));
		}

		[Fact(DisplayName = "Curried<T1, ..., T9, TResult> returns the curried function")]
		public void TestCurriedFunc9()
		{
			Func<int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9));
		}

		[Fact(DisplayName = "Curried<T1, ..., T10, TResult> returns the curried function")]
		public void TestCurriedFunc10()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 + arg10;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10));
		}

		[Fact(DisplayName = "Curried<T1, ..., T11, TResult> returns the curried function")]
		public void TestCurriedFunc11()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 +
					arg10 + arg11;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11));
		}

		[Fact(DisplayName = "Curried<T1, ..., T12, TResult> returns the curried function")]
		public void TestCurriedFunc12()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 +
					arg10 + arg11 + arg12;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12));
		}

		[Fact(DisplayName = "Curried<T1, ..., T13, TResult> returns the curried function")]
		public void TestCurriedFunc13()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13) =>
					arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 +
					arg10 + arg11 + arg12 + arg13;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13));
		}

		[Fact(DisplayName = "Curried<T1, ..., T14, TResult> returns the curried function")]
		public void TestCurriedFunc14()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int,
				int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13, arg14) =>
						arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 +
						arg10 + arg11 + arg12 + arg13 + arg14;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13)(14));
		}

		[Fact(DisplayName = "Curried<T1, ..., T15, TResult> returns the curried function")]
		public void TestCurriedFunc15()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
						arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 +
						arg10 + arg11 + arg12 + arg13 + arg14 + arg15;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13)(14)(15));
		}

		[Fact(DisplayName = "Curried<T1, ..., T16, TResult> returns the curried function")]
		public void TestCurriedFunc16()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int, int> func =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
						arg1 + arg2 + arg3 + arg4 + arg5 + arg6 + arg7 + arg8 + arg9 +
						arg10 + arg11 + arg12 + arg13 + arg14 + arg15 + arg16;

			var curried = func.Curried();

			Assert.Equal(
				func(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16),
				curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13)(14)(15)(16));
		}

		[Fact(DisplayName = "Curried returns the action")]
		public void TestCurriedAction0()
		{
			Action action = () => { };

			var curried = action.Curried();

			Assert.Equal(action, curried);
		}

		[Fact(DisplayName = "Curried<T1> returns the action")]
		public void TestCurriedAction1()
		{
			Action<int> action = arg1 => { };

			var curried = action.Curried();

			Assert.Equal(action, curried);
		}

		[Fact(DisplayName = "Curried<T1, T2> returns the curried action")]
		public void TestCurriedAction2()
		{
			int callNumber = 0;

			Action<int, int> action = (arg1, arg2) => callNumber++;

			var curried = action.Curried();

			action(1, 2);
			curried(1)(2);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, T2, T3> returns the curried action")]
		public void TestCurriedAction3()
		{
			int callNumber = 0;

			Action<int, int, int> action = (arg1, arg2, arg3) => callNumber++;

			var curried = action.Curried();

			action(1, 2, 3);
			curried(1)(2)(3);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T4> returns the curried action")]
		public void TestCurriedAction4()
		{
			int callNumber = 0;

			Action<int, int, int, int> action = (arg1, arg2, arg3, arg4) =>
				callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4);
			curried(1)(2)(3)(4);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T5> returns the curried action")]
		public void TestCurriedAction5()
		{
			int callNumber = 0;

			Action<int, int, int, int, int> action = (arg1, arg2, arg3, arg4, arg5) =>
				callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5);
			curried(1)(2)(3)(4)(5);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T6> returns the curried action")]
		public void TestCurriedAction6()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6);
			curried(1)(2)(3)(4)(5)(6);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T7> returns the curried action")]
		public void TestCurriedAction7()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7);
			curried(1)(2)(3)(4)(5)(6)(7);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T8> returns the curried action")]
		public void TestCurriedAction8()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8);
			curried(1)(2)(3)(4)(5)(6)(7)(8);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T9> returns the curried action")]
		public void TestCurriedAction9()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T10> returns the curried action")]
		public void TestCurriedAction10()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T11> returns the curried action")]
		public void TestCurriedAction11()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T12> returns the curried action")]
		public void TestCurriedAction12()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T13> returns the curried action")]
		public void TestCurriedAction13()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T14> returns the curried action")]
		public void TestCurriedAction14()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13, arg14) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13)(14);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T15> returns the curried action")]
		public void TestCurriedAction15()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13, arg14, arg15) =>
					callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13)(14)(15);

			Assert.Equal(2, callNumber);
		}

		[Fact(DisplayName = "Curried<T1, ..., T16> returns the curried action")]
		public void TestCurriedAction16()
		{
			int callNumber = 0;

			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int, int> action =
				(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8,
				 arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) =>
						callNumber++;

			var curried = action.Curried();

			action(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
			curried(1)(2)(3)(4)(5)(6)(7)(8)(9)(10)(11)(12)(13)(14)(15)(16);

			Assert.Equal(2, callNumber);
		}
		
		[Fact(DisplayName = "Curried<TResult> throws an exception for a null")]
		public void TestCurriedFunc0Null()
		{
			Func<int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, TResult> throws an exception for a null")]
		public void TestCurriedFunc1Null()
		{
			Func<int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, T2, TResult> throws an exception for a null")]
		public void TestCurriedFunc2Null()
		{
			Func<int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, T2, T3, TResult> throws an exception for a null")]
		public void TestCurriedFunc3Null()
		{
			Func<int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T4, TResult> throws an exception for a null")]
		public void TestCurriedFunc4Null()
		{
			Func<int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T5, TResult> throws an exception for a null")]
		public void TestCurriedFunc5Null()
		{
			Func<int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T6, TResult> throws an exception for a null")]
		public void TestCurriedFunc6Null()
		{
			Func<int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T7, TResult> throws an exception for a null")]
		public void TestCurriedFunc7Null()
		{
			Func<int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T8, TResult> throws an exception for a null")]
		public void TestCurriedFunc8Null()
		{
			Func<int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T9, TResult> throws an exception for a null")]
		public void TestCurriedFunc9Null()
		{
			Func<int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T10, TResult> throws an exception for a null")]
		public void TestCurriedFunc10Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T11, TResult> throws an exception for a null")]
		public void TestCurriedFunc11Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T12, TResult> throws an exception for a null")]
		public void TestCurriedFunc12Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T13, TResult> throws an exception for a null")]
		public void TestCurriedFunc13Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int,
				int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T14, TResult> throws an exception for a null")]
		public void TestCurriedFunc14Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T15, TResult> throws an exception for a null")]
		public void TestCurriedFunc15Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T16, TResult> throws an exception for a null")]
		public void TestCurriedFunc16Null()
		{
			Func<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int, int> func = null;
			Assert.Throws<ArgumentNullException>(() => func.Curried());
		}

		[Fact(DisplayName = "Curried throws an exception for a null")]
		public void TestCurriedAction0Null()
		{
			Action action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1> throws an exception for a null")]
		public void TestCurriedAction1Null()
		{
			Action<int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, T2> throws an exception for a null")]
		public void TestCurriedAction2Null()
		{
			Action<int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, T2, T3> throws an exception for a null")]
		public void TestCurriedAction3Null()
		{
			Action<int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T4> throws an exception for a null")]
		public void TestCurriedAction4Null()
		{
			Action<int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T5> throws an exception for a null")]
		public void TestCurriedAction5Null()
		{
			Action<int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T6> throws an exception for a null")]
		public void TestCurriedAction6Null()
		{
			Action<int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T7> throws an exception for a null")]
		public void TestCurriedAction7Null()
		{
			Action<int, int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T8> throws an exception for a null")]
		public void TestCurriedAction8Null()
		{
			Action<int, int, int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T9> throws an exception for a null")]
		public void TestCurriedAction9Null()
		{
			Action<int, int, int, int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T10> throws an exception for a null")]
		public void TestCurriedAction10Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T11> throws an exception for a null")]
		public void TestCurriedAction11Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T12> throws an exception for a null")]
		public void TestCurriedAction12Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T13> throws an exception for a null")]
		public void TestCurriedAction13Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T14> throws an exception for a null")]
		public void TestCurriedAction14Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T15> throws an exception for a null")]
		public void TestCurriedAction15Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}

		[Fact(DisplayName = "Curried<T1, ..., T16> throws an exception for a null")]
		public void TestCurriedAction16Null()
		{
			Action<int, int, int, int, int, int, int, int, int, int,
				int, int, int, int, int, int> action = null;
			Assert.Throws<ArgumentNullException>(() => action.Curried());
		}
	}
}
