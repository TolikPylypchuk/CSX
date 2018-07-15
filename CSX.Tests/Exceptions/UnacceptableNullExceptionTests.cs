using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Xunit;

namespace CSX.Exceptions
{
	public class UnacceptableNullExceptionTests
	{
		[Fact(DisplayName = "The exception can be constructed with the default constructor")]
		public void TestConstructDefault()
		{
			var exp = new UnacceptableNullException();
			Assert.Null(exp.InnerException);
		}

		[Fact(DisplayName =
			"The exception can be constructed with the constructor with message")]
		public void TestConstructWithMessage()
		{
			const string message = "message";

			var exp = new UnacceptableNullException(message);

			Assert.Equal(message, exp.Message);
			Assert.Null(exp.InnerException);
		}

		[Fact(DisplayName = "The exception can be constructed with the constructor with message and inner exception")]
		public void TestConstructWithMessageAndInnerException()
		{
			const string message = "message";
			var inner = new Exception();

			var exp = new UnacceptableNullException(message, inner);

			Assert.Equal(message, exp.Message);
			Assert.Equal(inner, exp.InnerException);
		}

		[Fact(DisplayName = "The exception is serializable")]
		public void TestSerialize()
		{
			const string message = "message";
			var exp = new UnacceptableNullException(message);

			string exceptionToString = exp.ToString();

			var bf = new BinaryFormatter();

			using (var stream = new MemoryStream())
			{
				bf.Serialize(stream, exp);
				stream.Seek(0, 0);
				exp = (UnacceptableNullException)bf.Deserialize(stream);
			}

			Assert.Equal(message, exp.Message);
			Assert.Equal(exceptionToString, exp.ToString());
		}
	}
}
