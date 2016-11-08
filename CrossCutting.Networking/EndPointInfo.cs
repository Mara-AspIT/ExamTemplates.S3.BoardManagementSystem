// Author: Mads Mikkel Rasmussen.

namespace CrossCutting.Networking
{
	/// <summary>
	/// Static class whos members provides endpoint exposure information.
	/// </summary>
	public static class EndPointInfo
	{
		/// <summary>
		/// The server's external IP Adress.
		/// </summary>
		public static readonly string ServerIp = "127.0.0.1";	// Do not change this.

		/// <summary>
		/// The port number to be used with log ins.
		/// </summary>
		public static readonly ushort LoginPort = 65430;
		// Add more ports here - just remember the allowed range [65430;65439].
	}
}
