// Author: Mads Mikkel Rasmussen.

using System;
using System.IO;

namespace CrossCutting.Logging
{

	/// <summary>
	/// Static class used to log messages and exceptions.
	/// </summary>
	public static class Logger
	{

		#region Fields
		/// <summary>
		/// The path to the logfile.
		/// </summary>
		private static readonly string path;

		/// <summary>
		/// Toggles display in the console og everything logged.
		/// </summary>
		public static bool EnableConsole;
		#endregion

		/// <summary>
		/// Static constructor. Used to set the path to the logfile (AppData folder).
		/// </summary>
		/// <exception cref="LoggingException"></exception>
		static Logger()
		{
			try
			{
				path = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "ExceptionLog.txt" );	// It should be verified that a file is actually used...
			}
			catch( PlatformNotSupportedException p ) { throw new LoggingException( "Wrong platform. See inner exception for details.", p ); }
			catch( ArgumentNullException an ) { throw new LoggingException( "The argument for Path.Combine was null. See inner exception for details.", an ); }
			catch( ArgumentException a ) { throw new LoggingException( "An argument was not valid. See inner exception for details.", a ); }
			catch( Exception e ) { throw new LoggingException( "A general error occured. See inner exception for details.", e ); }
		}

		/// <summary>
		/// Logs a message to the specified log file. If EnableConsole is true, the logMessage will also appear in the console.
		/// </summary>
		/// <param name="logMessage">The log message to log.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Log( LogMessage logMessage )
		{
			if( logMessage == null )
			{
				throw new ArgumentNullException( $"The argument {nameof( logMessage )} was null." );
			}
			if( EnableConsole )
			{
				Console.WriteLine( logMessage.ToString() );
			}
			try
			{
				using( StreamWriter writer = new StreamWriter( path ) )
				{
					writer.Write( logMessage.ToString() + "\n" );
				}
			}
			catch( Exception e ) { throw new LoggingException( "An error occured while attempting to write a log message. See inner exception for details.", e ); }
		}
	}
}
