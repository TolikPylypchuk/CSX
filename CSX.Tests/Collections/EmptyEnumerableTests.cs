using System.Collections;

using Xunit;

namespace CSX.Collections
{
	public class EmptyEnumerableTests
	{
		[Fact(DisplayName = "GetEnumerator returns EmptyEnumerator")]
		public void TestGetEnumerator()
		{
			var enumerator = EmptyEnumerable<int>.Instance.GetEnumerator();

			Assert.True(enumerator is EmptyEnumerator<int>);
		}

		[Fact(DisplayName = "IEnumerable.GetEnumerator returns EmptyEnumerator")]
		public void TestGetEnumeratorNonGeneric()
		{
			var enumerator = ((IEnumerable)EmptyEnumerable<int>.Instance).GetEnumerator();

			Assert.True(enumerator is EmptyEnumerator<int>);
		}
	}
}
