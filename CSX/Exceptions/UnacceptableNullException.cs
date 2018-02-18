using System;
using System.Runtime.Serialization;

namespace CSX.Exceptions
{
	/// <summary>
	/// Represents an exception which indicates that a value,
	/// which must not be <c>null</c>, is <c>null</c>.
	/// </summary>
	public class UnacceptableNullException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnacceptableNullException" /> class.
		/// </summary>
		public UnacceptableNullException() { }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="UnacceptableNullException" /> class.
		/// </summary>
		/// <param name="message">The message of this exception.</param>
		public UnacceptableNullException(string message) : base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnacceptableNullException" /> class.
		/// </summary>
		/// <param name="message">The message of this exception.</param>
		/// <param name="innerException">The exception which caused this exception.</param>
		public UnacceptableNullException(string message, Exception innerException)
			: base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnacceptableNullException" /> class.
		/// </summary>
		/// <param name="info">The serialization info of this exception.</param>
		/// <param name="context">The streaming context of this exception.</param>
		public UnacceptableNullException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}
