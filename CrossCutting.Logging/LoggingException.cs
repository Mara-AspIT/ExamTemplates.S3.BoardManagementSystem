// Author: Mads Mikkel Rasmussen.

using System;

namespace CrossCutting.Logging
{
	/// <summary>
	/// The exception that is trown when an unresolvable logging error occurs. Inherits System.Exception. This is a wrapper class.
	/// </summary>
	[Serializable]
	public class LoggingException : Exception
	{
		/// <summary>
		/// Initializes a new instance of this class with an exception message and the inner exception that caused this exception.
		/// </summary>
		/// <param name="message">The message describing the cause of this exception.</param>
		/// <param name="inner">The exception that caused this exception.</param>
		public LoggingException( string message, Exception inner ) : base( message, inner ) { }
	}
}
