using System;
using System.Collections.Generic;
using System.Linq;

namespace CSX.Results
{
	/// <summary>
	/// Represents an exception which occurs when a get operation is performed
	/// on a failed result.
	/// </summary>
	public class ResultFailedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResultFailedException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ResultFailedException(string message)
			: base(message, new Exception(message))
		{
			this.Messages.Add(message);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultFailedException" /> class.
		/// </summary>
		/// <param name="inner">The cause of this exception.</param>
		public ResultFailedException(Exception inner)
			: base(inner.Message, inner)
		{
			this.Messages.Add(inner.Message);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultFailedException" /> class.
		/// </summary>
		/// <param name="messages">The messages that describe the error.</param>
		public ResultFailedException(IEnumerable<string> messages)
			: base(
				String.Empty,
			    new AggregateException(messages.Select(message => new Exception(message))))
		{
			foreach (string message in messages)
			{
				this.Messages.Add(message);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultFailedException" /> class.
		/// </summary>
		/// <param name="exceptions">The causes of this exception.</param>
		public ResultFailedException(IEnumerable<Exception> exceptions)
			: base(String.Empty, new AggregateException(exceptions))
		{
			foreach (var exp in exceptions)
			{
				this.Messages.Add(exp.Message);
			}
		}

		/// <summary>
		/// Gets the messages of this error.
		/// </summary>
		public IList<string> Messages { get; } = new List<string>();

		/// <summary>
		/// Gets the message of this exception.
		/// </summary>
		public override string Message
			=> this.Messages.Aggregate((acc, item) => $"{acc}; {item}");
	}
}
