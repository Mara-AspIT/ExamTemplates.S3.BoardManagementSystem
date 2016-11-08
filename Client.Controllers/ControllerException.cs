using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
	/// <summary>
	/// Represents an error in the client side controller layer. Inherits System.Exception.
	/// </summary>
	[Serializable]
	public class ControllerException : Exception
	{
		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		/// <param name="message">The message describing the cause for this exception.</param>
		/// <param name="inner">The exception that caused this exception.</param>
		public ControllerException( string message, Exception inner ) : base( message, inner ) { }
	}
}
