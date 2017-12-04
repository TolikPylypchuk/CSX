using System.Diagnostics.CodeAnalysis;

using CSX.Options;

namespace CSX.Examples.Options
{
	/// <summary>
	/// Contains usage examples of the <see cref="Option{T}" /> class.
	/// </summary>
	[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
	[SuppressMessage("ReSharper", "UnusedVariable")]
	public class Examples
	{
		/// <summary>
		/// Contains examples of construction of <see cref="Option{T}" /> instances.
		/// </summary>
		public void Construct()
		{
			// Using the static Option class
			var option1 = Option.From(1);

			// Using extension methods
			var option2 = 1.ToOption();

			// Both methods return None if value is null

			string value = null;

			var thisWillBeNone = Option.From(value);
			var andThisToo = value.ToOption();
		}
	}
}
