// Author: Mads Mikkel Rasmussen.

using CrossCutting.Networking;
using System;
using System.Net;
using System.Collections.Generic;

namespace Server.Services
{
	/// <summary>
	/// Static factory to create services consumed by clients. Contains a private static constructor that sets the server ip address.
	/// </summary>
	/// /// <exception cref="FormatException"></exception>
	/// <exception cref="ArgumentNullException"></exception>
	internal static class ServiceFactory
	{
		/// <summary>
		/// The server's IP address.
		/// </summary>
		private static IPAddress serverIp;

		/// <summary>
		/// Private static constructor. Sets the server ip address.
		/// </summary>
		static ServiceFactory()
		{
			try
			{
				serverIp = IPAddress.Parse( EndPointInfo.ServerIp );
			}
			catch( FormatException formatException )
			{
				Console.WriteLine( $"{formatException.Message}\n{formatException.Source}\n{formatException.StackTrace}\nServer will close when you hit any key. Please correct the server ip address and try to restart." );
				Console.ReadLine();
				System.Environment.Exit( 0 );
			}
			catch( ArgumentNullException argumentNullException )
			{
				Console.WriteLine( $"{argumentNullException.Message}\n{argumentNullException.Source}\n{argumentNullException.StackTrace}\nServer will close when you hit any key. Please correct the server ip address and try to restart." );
				Console.ReadLine();
				System.Environment.Exit( 0 );
			}
		}

		/// <summary>
		/// Gets a list of not yet started services by calling private 'Create' methods.
		/// </summary>
		/// <returns>A List<Service> containing all services.</returns>
		internal static List<Service> GetServices()
		{
			List<Service> services = new List<Service>();
			services.Add( CreateLoginService() );
			return services;
		}

		/// <summary>
		/// Creates the login service.
		/// </summary>
		/// <returns>A LoginService</returns>
		private static LoginService CreateLoginService()
		{
			LoginService service = null;
			try
			{
				IPEndPoint endPoint = new IPEndPoint( serverIp, EndPointInfo.LoginPort );
				service = new LoginService( endPoint );
			}
			catch( ArgumentOutOfRangeException )
			{
				throw;
			}
			catch( ArgumentNullException )
			{
				throw;
			}
			catch( ArgumentException )
			{
				throw;
			}
			return service;
		}
	}
}