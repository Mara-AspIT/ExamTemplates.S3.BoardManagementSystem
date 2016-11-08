// Author: Mads Mikkel Rasmussen.

using System;

namespace CrossCutting.Entities
{
	/// <summary>
	/// Rerpresents a user's credentials.
	/// </summary>
	[Serializable]
	public struct UserCredentials
	{

		#region Fields
		/// <summary>
		/// The username of the user.
		/// </summary>
		private string username;

		/// <summary>
		/// The password of the user.
		/// </summary>
		private string password;
		#endregion


		#region Constructor
		/// <summary>
		/// Initializes a new instance of this struct. This type is immutable. Throws ArgumentException if either argument is null, consists only of whitespace characters or an empty string.
		/// </summary>
		/// <param name="username">The username of the user.</param>
		/// <param name="password">The password of the user.</param>
		/// <exception cref="ArgumentException"></exception>
		public UserCredentials( string username, string password )
		{
			if( String.IsNullOrWhiteSpace( username ) )
			{
				throw new ArgumentException( $"Argument for {nameof( username )} was either null, consisted only of whitespace characters or was an empty string." );
			}
			if( String.IsNullOrWhiteSpace( password ) )
			{
				throw new ArgumentException( $"Argument for {nameof( password )} was either null, consisted only of whitespace characters or was an empty string." );
			}
			this.username = username;
			this.password = password;
		}
		#endregion
		

		#region Properties
		/// <summary>
		/// Gets the username.
		/// </summary>
		public string Username
		{
			get
			{
				return username;
			}
		}

		/// <summary>
		/// Gets the password.
		/// </summary>
		public string Password
		{
			get
			{
				return password;
			}
		} 
		#endregion
	}
}