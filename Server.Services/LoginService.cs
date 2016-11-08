// Author: Mads Mikkel Rasmussen.

using CrossCutting.Entities;
using CrossCutting.Logging;
using CrossCutting.Networking;
using Server.DataAccess;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server.Services
{
	/// <summary>
	/// Represents a log in service, and is used by clients to log in on the server. Inherits Service. This class cannot be inherited.
	/// </summary>
	public sealed class LoginService : Service
	{
		#region Fields
		/// <summary>
		/// The instance of the login handler.
		/// </summary>
		private LogInHandler dbLoginHandler;
		#endregion


		#region Constructors
		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		/// <param name="endPoint">The IPEndpoint to expose the service to clients.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public LoginService( IPEndPoint endPoint ) : base( endPoint )
		{
			dbLoginHandler = new LogInHandler();
			Logger.Log( new LogMessage( "Login service started." ) );
		}
		#endregion


		#region Methods
		/// <summary>
		/// Starts the login service. Overrides abstract base method.
		/// </summary>
		public override void Run()
		{

			try
			{
				base.Start();
				while( true )
				{
					TcpClient client = base.AcceptTcpClient();
					Logger.Log( new LogMessage( "Client connect request accepted." ) );
					NetworkStream stream = client.GetStream();
					byte[] readBuffer = new byte[1024];

					if( stream.CanRead )
					{
						int numbersOfBytesToRead = 0;
						do
						{
							numbersOfBytesToRead = stream.Read( readBuffer, offset: 0, size: readBuffer.Length );

						} while( stream.DataAvailable );
						UserCredentials credentials = Serializer<UserCredentials>.Deserialize( readBuffer );
						LoginRequestResult result = ( LoginRequestResult )HandleLogInRequest( credentials );
						if( stream.CanWrite )
						{
							byte[] writeBuffer = Serializer<LoginRequestResult>.Serialize( result );
							stream.Write( writeBuffer, offset: 0, size: writeBuffer.Length );
							Logger.Log( new LogMessage( "Response to client sent." ) );
						}
						client.Close();
						Logger.Log( new LogMessage( "Client connection closed." ) );
					}
				}
			}
			catch( Exception e )
			{
				// throw?
				Logger.Log( new LogMessage( e ) );
			}

		}

		/// <summary>
		/// Handles requsts accepted by the Run method. Returns a LoginRequestResult as an IRequestresult.
		/// </summary>
		/// <param name="credentials">The credentials to match in the database.</param>
		/// <returns> Returns a LoginRequestResult indicating the result of the request.</returns>
		internal LoginRequestResult HandleLogInRequest( UserCredentials credentials )
		{
			User user = dbLoginHandler.GetUser( credentials );
			LogInRequestStatus status = default( LogInRequestStatus );
			if( user != null )
			{
				status = LogInRequestStatus.Success;
			}
			else
			{
				status = LogInRequestStatus.WrongCredentials;
			}
			return new LoginRequestResult( status, user );
		} 
		#endregion
	}
}