using Xunit;

using static CSX.Options.Option;

namespace CSX.Exceptions
{
	public class OptionAbsentExceptionTests
	{
		[Fact(DisplayName = "The exception has the message which was passed to GetOrThrow")]
		public void TestMessage()
		{
			const string message = "message";
			var exp = Assert.Throws<OptionAbsentException>(
				() => Empty<int>().GetOrThrow(message));
			Assert.Equal(message, exp.Message);
		}

		[Fact(DisplayName =
			"The exception has the default message if none was passed to GetOrThrow")]
		public void TestDefaultMessage()
		{
			var exp = Assert.Throws<OptionAbsentException>(() => Empty<int>().GetOrThrow());
			Assert.Equal("The value is not present.", exp.Message);
		}
	}
}
