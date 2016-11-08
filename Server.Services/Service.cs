// Author Mads Mikkel Rasmussen.

using CrossCutting.Logging;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server.Services
{

	/// <summary>
	/// Abstract base class for TCP based services. Inherits System.Net.Sockets.TcpListener. Implements IDisposable that stops the listener.
	/// </summary>
	public abstract class Service : TcpListener, IDisposable
	{
		#region Fields
		private bool disposedValue = false; // To detect redundant calls (IDisposable support).
		// protected DbHandler
		#endregion


		#region Constructors
		/// <summary>
		/// Initializes a new instance of a derived class and starts the listener.
		/// </summary>
		/// <param name="endPoint">The IPEndpoint to expose the service to clients.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		public Service( IPEndPoint endPoint ) : base( endPoint )
		{
			if( endPoint == null )
			{
				throw new ArgumentNullException( $"The provided argument {nameof( endPoint )} was null." );
			}
			else if( endPoint.Port < 65430 || endPoint.Port > 65439 )
			{
				throw new ArgumentException( "Port number was not in the allowed range [65430;65439]." );
			}
		}
		#endregion


		#region Methods
		/// <summary>
		/// Abstrart base method all inheriting classes must implement based on port number.
		/// </summary>
		public abstract void Run();
		

		#region IDisposable Support
		protected virtual void Dispose( bool disposing )
		{
			if( !disposedValue )
			{
				if( disposing )
				{
					// TODO: dispose managed state (managed objects).
					try
					{
						this.Stop();
					}
					catch( SocketException socketException )
					{
						Logger.Log( new LogMessage( socketException ) );
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~Service() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		void IDisposable.Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose( true );
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion 
		#endregion

	}
}
