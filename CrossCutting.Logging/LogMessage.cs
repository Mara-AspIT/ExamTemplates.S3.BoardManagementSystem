// Author: Mads Mikkel Rasmussen.
using System;

namespace CrossCutting.Logging
{
	/// <summary>
	/// Represents a log message. Can be either a string message or an exception.
	/// </summary>
	public class LogMessage
	{
		#region Fields
		/// <summary>
		/// The instant in time when a new instance of this class is created.
		/// </summary>
		protected readonly DateTime TimeStamp = DateTime.Now;

		/// <summary>
		/// The textual content of a log message.
		/// </summary>
		protected string content;
		#endregion


		#region Constructors
		/// <summary>
		/// Initializes a new instance of this class. Populates the Content with the provided exception's message, source and stacktrace.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		public LogMessage( Exception exception )
		{
			try
			{
				Content = $"{exception.Message}, {exception.Source}, {exception.StackTrace}.";
			}
			catch( ArgumentNullException ) { throw; }
		}

		public LogMessage( string message )
		{
			if( message == null )
				throw new ArgumentNullException( $"The provided argument {nameof( message )} was null." );
			content = message;
		}
		#endregion


		#region Methods
		/// <summary>
		/// Creates a textual log message with the DateTime.Now at the instant when this object was created, along with the log message. Overrides Object.ToString().
		/// </summary>
		/// <returns>A textual log message with the DateTime.Now at the instant when this object was created, along with the log message.</returns>
		public override string ToString()
		{
			try
			{
				return TimeStamp.ToString( "yyyy.MM.dd HH:mm:ss.fffffff" ) + ": " + content;
			}
			catch( FormatException f )
			{
				throw new LoggingException( "An error occured while attempting to create a text representation of a log message. See inner exception for details.", f );
			}
			catch( ArgumentOutOfRangeException a )
			{
				throw new LoggingException( "An error occured while attempting to create a text representation of a log message. See inner exception for details.", a );
			}
		}
		#endregion


		#region Properties
		/// <summary>
		/// Get the text content of this log message.
		/// </summary>
		public virtual string Content
		{
			get
			{
				return content;
			}
			protected set
			{
				if( value != null )
				{
					if( value != content )
					{
						content = value;
					}
				}
				else
				{
					throw new ArgumentNullException( $"Argument for {nameof( Content )} setter was null" );
				}

			}
		}
		#endregion
	}
}