using System;
using System.Collections;

using Xunit;

namespace CSX.Collections
{
	public class EmptyEnumeratorTests
	{
		[Fact(DisplayName = "Current always gets the default value of T")]
		public void TestCurrent()
		{
			Assert.Throws<InvalidOperationException>(() => EmptyEnumerator<int>.Instance.Current);
		}

		[Fact(DisplayName = "IEnumerator.Current always throws an exception")]
		public void TestIEnumeratorCurrent()
		{
			Assert.Throws<InvalidOperationException>(() => ((IEnumerator)EmptyEnumerator<int>.Instance).Current);
		}

		[Fact(DisplayName = "MoveNext always returns false")]
		public void TestMoveNext()
		{
			Assert.False(EmptyEnumerator<int>.Instance.MoveNext());
		}

		[Fact(DisplayName = "Reset does nothing")]
		public void TestReset()
		{
			EmptyEnumerator<int>.Instance.Reset();
		}

		[Fact(DisplayName = "Dispose does nothing")]
		public void TestDispose()
		{
			((IDisposable)EmptyEnumerator<int>.Instance).Dispose();
		}
	}
}
