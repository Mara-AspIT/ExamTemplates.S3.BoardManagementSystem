// Author: Mads Mikkel Rasmussen.

using CrossCutting.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Server.Services
{

	/// <summary>
	/// Contains the main method for the server program. Starts all services.
	/// </summary>
	public class ServerProgram
	{
		#region Methods
		/// <summary>
		/// The main entry point of the server program. 
		/// </summary>
		/// <param name="args">Correct args info will restart the server from a monitoring application.</param>
		public static void Main( string[] args )
		{
			Logger.EnableConsole = true;    // set this to false if you want to disable text in console.
			Logger.Log( new LogMessage( "Starting server..." ) );

			try
			{
				StartServices();
				Logger.Log( new LogMessage( "Server started, services running." ) );
			}
			catch( Exception e )
			{
				LogMessage m = new LogMessage( e );
				Console.WriteLine( m.ToString() );
			}
			//Console.ReadLine();
		}

		/// <summary>
		/// Start all services. Each service is spawned in its own thread, and on its own port on the server ip address.
		/// </summary>
		static void StartServices()
		{
			List<Service> services = ServiceFactory.GetServices();
			if( services.Count > 0 )
			{
				try
				{
					foreach( Service service in services )
					{
						Thread newThread = new Thread( new ThreadStart( service.Run ) );
						newThread.Start();
					}
				}
				catch( Exception )
				{
					throw;
				}
			}
			else
			{
				// no services to start... buhu...
			}
		}
		#endregion
	}
}
