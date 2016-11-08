// Author: Mads Mikkel Rasmussen.

using System;
using System.Data.SqlClient;


namespace Server.DataAccess
{

	/// <summary>
	/// Abstract base class for performing data manipulation on a MS-SQL database hosted on a MS-SQL Server. 
	/// </summary>
	public abstract class DatabaseHandler
	{

		#region Fields
		/// <summary>
		/// The connection string.
		/// </summary>
		protected const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bmsDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		/// <summary>
		/// The connection. Is initialized with the connection string in the constructor.
		/// </summary>
		protected SqlConnection connection;
		#endregion


		#region Constructors
		/// <summary>
		/// Initializes a new instance of this class. Attempts to open and close a connection to the database. If this fials, a DatabaseException is thrown.
		/// </summary>
		/// <exception cref="DatabaseException"></exception>
		public DatabaseHandler()
		{
			try
			{
				connection = new SqlConnection( connectionString );
				connection.Open();
				connection.Close();
			}
			catch( System.Configuration.ConfigurationException ce )
			{
				throw new DatabaseException( "Configuration error. See inner exception for details.", ce );
			}
			catch( SqlException se )
			{
				throw new DatabaseException( "Sql error. See inner exception for details.", se );
			}
			catch( InvalidOperationException ioe )
			{
				throw new DatabaseException( "Error. See inner exception for details.", ioe );
			}
		} 
		#endregion
	}
}