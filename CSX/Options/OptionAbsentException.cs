using System;

namespace CSX.Options
{
	/// <summary>
	/// Represents an exception which occurs when a get operation is performed
	/// on a value which is absent.
	/// </summary>
	public class OptionAbsentException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OptionAbsentException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public OptionAbsentException(string message) : base(message) { }
	}
}
