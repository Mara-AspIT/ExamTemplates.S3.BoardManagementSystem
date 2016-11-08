// Author: Mads Mikkel Rasmussen.

using System;
using CrossCutting.Entities;

namespace CrossCutting.Networking
{
	/// <summary>
	/// Represents the result of an attempted login request. Implements IRequestresult. Serializable for network transfer.
	/// </summary>
	[Serializable]
	public struct LoginRequestResult : IRequestResult
	{
		#region Fields
		/// <summary>
		/// The user.
		/// </summary>
		private User user;

		/// <summary>
		/// The log in status.
		/// </summary>
		private LogInRequestStatus status;
		#endregion


		#region Constructors
		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		/// <param name="status">The status of the log in attempt.</param>
		/// <param name="user">[OPTIONAL] The user to log in.</param>
		public LoginRequestResult( LogInRequestStatus status, User user = null )
		{
			this.status = status;
			this.user = user;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the user.
		/// </summary>
		public User User
		{
			get
			{
				return user;
			}
		}

		/// <summary>
		/// Gets the log in status.
		/// </summary>
		public LogInRequestStatus Status
		{
			get
			{
				return status;
			}
		} 
		#endregion
	}
}
