using System;
using CrossCutting.Networking;
using CrossCutting.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.CrossCutting.Networking
{
	/// <summary>
	/// Tests the type CrossCutting.Networking.Serializer.
	/// </summary>
	[TestClass]
	public class SerializerTests
	{
		/// <summary>
		/// Tests whether or not an instance UserCredentials can be correctly serialized and deserialized, by comparing fields.
		/// </summary>
		[TestMethod]
		public void RoundTripTest()
		{
			// Arrange:
			string username = "Mads";
			string password = "1234";
			UserCredentials expected = new UserCredentials( username, password );

			// Act:
			byte[] buffer = Serializer<UserCredentials>.Serialize( expected );
			UserCredentials actual = Serializer<UserCredentials>.Deserialize( buffer );


			// Assert:
			Assert.AreEqual( expected, actual );
		}
	}
}
