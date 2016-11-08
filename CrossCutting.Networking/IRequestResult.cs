// Author: Mads Mikkel Rasmussen.

using CrossCutting.Entities;

namespace CrossCutting.Networking
{
	/// <summary>
	/// Interface for request result. Possibly obsolete.
	/// </summary>
	public interface IRequestResult
	{
		/// <summary>
		/// The user. Getter only.
		/// </summary>
		User User { get; }
	}
}