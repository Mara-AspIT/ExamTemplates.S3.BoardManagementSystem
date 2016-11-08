// Author: Mads Mikkel Rasmussen.

using CrossCutting.Entities;
using CrossCutting.Networking;
using System;
using System.Net;
using System.Net.Sockets;

namespace Client.Controllers
{

	/// <summary>
	/// Handles user requests from a log in form. Implemented as singleton. Cannot be inherited.
	/// </summary>
	public sealed class LoginController
	{

		#region Fields
		/// <summary>
		/// The single instance of this class.
		/// </summary>
		private static LoginController instance;

		/// <summary>
		/// The server's endpoint.
		/// </summary>
		private readonly IPEndPoint serverEndpoint;

		/// <summary>
		/// The client's endpoint.
		/// </summary>
		private readonly IPEndPoint localEndPoint;
		#endregion


		#region Constructors
		/// <summary>
		/// Sets up local and server endpoints.
		/// </summary>
		/// <exception cref="ControllerException"></exception>
		private LoginController()
		{
			try
			{
				serverEndpoint = new IPEndPoint( IPAddress.Parse( EndPointInfo.ServerIp ), EndPointInfo.LoginPort );
				localEndPoint = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 45678 );
			}
			catch( FormatException f )
			{
				throw new ControllerException( "An error occurred while attempting to parse an IP address. See inner exception for details.", f );
			}
			catch( ArgumentOutOfRangeException a )
			{
				throw new ControllerException( "An error occurred while attempting to set up network ports. See inner exception for details.", a );
			}
			catch( ArgumentNullException n )
			{
				throw new ControllerException( "An error occurred while attempting to set up network infrastructure to remote server. See inner exception for details.", n );
			}
		}
		#endregion


		#region Methods
		/// <summary>
		/// Attempts to verify the credentials on the server.
		/// </summary>
		/// <param name="credentials">The credentials to verify.</param>
		/// <param name="user">The instance of the User who has logged in. If login fails this output variable is null.</param>
		/// <returns>A value indicating the state of the log in request.</returns>
		/// <exception cref="ControllerException"></exception>
		public LogInRequestStatus TryLogin( UserCredentials credentials, out User user )
		{
			NetworkStream stream = null;
			try
			{
				TcpClient client = new TcpClient( serverEndpoint.Address.ToString(), serverEndpoint.Port );
				stream = client.GetStream();
			}
			catch( SocketException se )
			{
				throw new ControllerException( "A networking error occured while atttempting to contact remote server. See inner exception for details.", se );
			}
			catch( ObjectDisposedException ode )
			{
				throw new ControllerException( "A networking error occured while atttempting to contact remote server. See inner exception for details.", ode );
			}
			catch( InvalidOperationException ioe )
			{
				throw new ControllerException( "A networking error occured while atttempting to contact remote server. See inner exception for details.", ioe );
			}
			catch( ArgumentOutOfRangeException aoore )
			{
				throw new ControllerException( "A networking error occured while atttempting to contact remote server. See inner exception for details.", aoore );
			}
			catch( ArgumentNullException ane )
			{
				throw new ControllerException( "A networking error occured while atttempting to contact remote server. See inner exception for details.", ane );
			}

			LoginRequestResult result = default( LoginRequestResult );
			if( stream.CanWrite )
			{
				byte[] writeBuffer = Serializer<UserCredentials>.Serialize( credentials );
				stream.Write( writeBuffer, offset: 0, size: writeBuffer.Length );
				byte[] readBuffer = new byte[1024];
				if( stream.CanRead )
				{
					int numberOfBytesToRead = 0;
					do
					{
						numberOfBytesToRead = stream.Read( readBuffer, offset: 0, size: readBuffer.Length );
					} while( stream.DataAvailable );
					result = Serializer<LoginRequestResult>.Deserialize( readBuffer );
				}
				stream.Close();
			} // The rest could be nicer and cleaner...
			else
			{
				result = new LoginRequestResult( LogInRequestStatus.NetworkError, user: null );
			}
			
			if( result.Status == LogInRequestStatus.Success )
			{
				user = result.User;
			}
			else
			{
				user = null;
			}
			return result.Status;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets the single instance of this class.
		/// </summary>
		public static LoginController Instance
		{
			get
			{
				return instance ?? new LoginController();
			}
		}
		#endregion
	}
}
