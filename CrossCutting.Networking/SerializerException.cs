// Author: Mads Mikkel Rasmussen.

using System;

namespace CrossCutting.Networking
{

	/// <summary>
	/// Represents an error that occured while serializing or deserializing. See inner exception for details.
	/// </summary>
	[Serializable]
	public class SerializerException : Exception
	{
		/// <summary>
		/// Initializes a new instance of this class, with a provided error message and inner exception
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="inner">The exception that casued this exception to be thrown.</param>
		public SerializerException( string message, Exception inner ) : base( message, inner ) { }
	}
}
