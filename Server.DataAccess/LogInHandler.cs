// Author: Mads Mikkel Rasmussen.

using CrossCutting.Entities;
using System;
using System.Data.SqlClient;

namespace Server.DataAccess
{
	/// <summary>
	/// Handler for login requests. Inherits DatabaseHandler.
	/// </summary>
	public class LogInHandler : DatabaseHandler
	{

		/// <summary>
		/// Gets the user from the database. If no match is found null is returned.
		/// </summary>
		/// <param name="credentials">The user credentials to match to a user in the database Users table.</param>
		/// <returns>A user of the correct type. If no match is found null is returned.</returns>
		public User GetUser( UserCredentials credentials )
		{
			User user = null;
			string query = $"SELECT * FROM Users WHERE Username = '{credentials.Username}' AND Password = '{credentials.Password}';";
			SqlDataReader reader = null;
			try
			{
				using( connection )
				{
					connection.Open();
					SqlCommand command = new SqlCommand( query, connection );
					reader = command.ExecuteReader();
					if( reader.HasRows )
					{
						while( reader.Read() )
						{
							string username = reader.GetString( 1 );
							string password = reader.GetString( 2 );
							if( username == credentials.Username && password == credentials.Password )
							{
								string userKind = reader.GetString( 3 );
								switch( userKind )
								{
									case "TestUserType":
										user = new TestUserType( username );
										break;
									default:
										break;
								}
								break;
							}
						}
						connection.Close();
					}
				}
			}
			catch( Exception e )
			{
				throw new DatabaseException( "Database error. See inner exception for details", e );
			}
			finally
			{
				connection.Close();
			}
			return user;
		}
	}
}