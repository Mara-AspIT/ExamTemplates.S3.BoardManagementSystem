// Author: Mads Mikkel Rasmussen.

using System;

namespace Server.DataAccess
{
	/// <summary>
	/// Represents an error in or while handeling the database. Inherits System.Exception.
	/// </summary>
	[Serializable]
	public class DatabaseException : Exception
	{
		/// <summary>
		/// Initializes a new instance of this class. 
		/// </summary>
		/// <param name="message">The message describing the cause of this esception.</param>
		/// <param name="inner">The exception that caused this exception.</param>
		public DatabaseException( string message, Exception inner ) : base( message, inner ) { }
	}
}
