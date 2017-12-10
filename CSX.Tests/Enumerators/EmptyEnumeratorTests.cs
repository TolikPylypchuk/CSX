using System.Collections;

using Xunit;

namespace CSX.Enumerators
{
	public class EmptyEnumeratorTests
	{
		[Fact(DisplayName = "Current always gets the default value of T")]
		public void TestMoveNext()
		{
			Assert.Equal(default, EmptyEnumerator<int>.Instance.Current);
		}

		[Fact(DisplayName = "IEnumerator.Current always gets the default value of T")]
		public void TestIEnumeratorCurrent()
		{
			Assert.Equal(
				default(int),
				((IEnumerator)EmptyEnumerator<int>.Instance).Current);
		}

		[Fact(DisplayName = "MoveNext always returns false")]
		public void TestCurrent()
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
			EmptyEnumerator<int>.Instance.Dispose();
		}
	}
}
