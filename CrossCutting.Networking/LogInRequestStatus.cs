// Author: Mads Mikkel Rasmussen.

namespace CrossCutting.Networking
{
	/// <summary>
	/// The status of a login.
	/// </summary>
	public enum LogInRequestStatus
	{
		/// <summary>
		/// Default.
		/// </summary>
		Default = 0,

		/// <summary>
		/// Indicates a succesfull login.
		/// </summary>
		Success,

		/// <summary>
		/// Indicates the user entered wrong credentials.
		/// </summary>
		WrongCredentials,

		/// <summary>
		/// Indicates a network error.
		/// </summary>
		NetworkError
	}
}
