// Author: Mads Mikkel Rasmussen.
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CrossCutting.Networking
{
	/// <summary>
	/// Static class used to serialize an object og type T into a byte buffer that can be transmitted acress a network.
	/// </summary>
	/// <typeparam name="T">The type of the object to either serialize or deserialize.</typeparam>
	/// <exception cref="SerializerException"></exception>
	/// <exception cref="Exception"></exception>
	public static class Serializer<T>
	{

		/// <summary>
		/// Serializes an object to a binary byte buffer.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>A byte buffer containing the binary representation of the serialized object.</returns>
		/// <exception cref="SerializerException"></exception>
		/// <exception cref="Exception"></exception>
		public static byte[] Serialize( T obj )
		{
			byte[] result;
			BinaryFormatter serializer = new BinaryFormatter();
			try
			{
				using( MemoryStream stream = new MemoryStream() )
				{
					serializer.Serialize( stream, obj );
					result = stream.GetBuffer();
				}
			}
			catch( System.Runtime.Serialization.SerializationException serializationException )
			{
				throw new SerializerException( "Attempt to serialize failed. See inner exception for details.", serializationException );
			}
			catch( UnauthorizedAccessException accessException )
			{
				throw new SerializerException( "Access denied error. See inner exception for details.", accessException );
			}
			catch( System.Security.SecurityException securityException )
			{
				throw new SerializerException( "Security error. See inner exception for details.", securityException );
			}
			catch( ArgumentNullException nullException )
			{
				throw new SerializerException( "Security error. See inner exception for details.", nullException );
			}
			catch( Exception )
			{
				throw;
			}
			return result;
		}

		/// <summary>
		/// Deserializes a byte buffer into an object of type T.
		/// </summary>
		/// <param name="buffer">The byte buffer to deserialize.</param>
		/// <returns>An object of type T.</returns>
		public static T Deserialize( byte[] buffer )
		{
			T result;
			BinaryFormatter deserializer = new BinaryFormatter();
			try
			{
				using( MemoryStream stream = new MemoryStream( buffer ) )
				{
					result = ( T )deserializer.Deserialize( stream );
				}
			}
			catch( System.Runtime.Serialization.SerializationException serializationException )
			{
				throw new SerializerException( "Attempt to serialize failed. See inner exception for details.", serializationException );
			}
			catch( InvalidCastException castException )
			{
				throw new SerializerException( "Could not cast byte buffer to an object of type T. See inner exception for details.", castException );
			}
			catch( System.Security.SecurityException securityException )
			{
				throw new SerializerException( "Security error. See inner exception for details.", securityException );
			}
			catch( Exception up )
			{
				throw up;
			}
			return result;
		}
	}
}