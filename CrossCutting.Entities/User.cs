// Author: Mads Mikkel Rasmussen.

using System;

namespace CrossCutting.Entities
{
	/// <summary>
	/// Abstract base class for users.
	/// </summary>
	[Serializable]
	public abstract class User
	{

		#region Fields
		/// <summary>
		/// The credentials for the user. Is nullable, due to security risks over networks.
		/// </summary>
		protected UserCredentials? credentials;

		/// <summary>
		/// The username of the user.
		/// </summary>
		protected string username;

		/// <summary>
		/// Indicates whether the user is logged in or not.
		/// </summary>
		protected bool isLoggedIn;  // Currently not used, but probably should be.
		#endregion


		public User(string username)
		{
			Username = username;
		}

		#region Properties
		/// <summary>
		/// gets or sets a value indicating whether the user is logged in or not.
		/// </summary>
		public bool IsLoggedIn
		{
			get
			{
				return isLoggedIn;
			}
			set
			{
				if( value != isLoggedIn )
				{
					isLoggedIn = !isLoggedIn;
				}
			}
		}

		public UserCredentials? Credentials
		{
			get
			{
				return credentials;
			}

			set
			{
				credentials = value;
			}
		}

		public string Username
		{
			get { return username; }
			protected set
			{
				username = value;
			}
		} 
		#endregion
	}
}
