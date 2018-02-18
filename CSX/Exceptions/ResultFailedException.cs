using System;
using System.Linq;

using CSX.Collections;

namespace CSX.Exceptions
{
	/// <summary>
	/// Represents an exception which occurs when a get operation is performed
	/// on a failed result.
	/// </summary>
	public class ResultFailedException : Exception
	{
		internal ResultFailedException(ConsList<string> errors)
		{
			this.Messages = errors;
			this.Exceptions = errors.Map(str => new Exception(str));
		}

		internal ResultFailedException(ConsList<Exception> errors)
		{
			this.Exceptions = errors;
			this.Messages = errors.Map(exp => exp.Message);
		}

		/// <summary>
		/// Gets the exceptions of this error.
		/// </summary>
		public ConsList<Exception> Exceptions { get; }
		
		/// <summary>
		/// Gets the messages of this error.
		/// </summary>
		public ConsList<string> Messages { get; }

		/// <summary>
		/// Gets the message of this exception.
		/// </summary>
		public override string Message
			=> this.Messages.Aggregate((acc, item) => $"{acc}; {item}");
	}
}
