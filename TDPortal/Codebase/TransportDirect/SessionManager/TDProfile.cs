using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Logger = System.Diagnostics.Trace;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.ReportDataProvider.TDPCustomEvents;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// During serialization, a formatter transmits the information required to create an instance of an object of
	/// the correct type and version.  This information generally includes the full type name and assembly name of
	/// the object.
	/// Now, if we are deserialising a user profile hashtable that was written to the database by the ProfileMigration
	/// utilty, we will need to use a Binding Converter to override that assembly name when deserialising the profile hashtable.
	/// </summary>
	internal class BindingConverter : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			return System.Type.GetType( typeName );
		}
	}

	#region Transport Direct Profile Property
	/// <summary>
	/// An individual Transport Direct profile property item
	/// </summary>
	[Serializable]
	public class ProfileProperties
	{
		private object item;

		/// <summary>
		/// Creates a new profile item
		/// </summary>
		/// <param name="item"></param>
		public ProfileProperties( object item )
		{
			this.item = item;
		}

		/// <summary>
		/// Provides access to the profile item
		/// </summary>
		public object Value
		{
			get { return item; }
			set { item = value; }
		}
	}
	#endregion

	#region Transport Direct User Profile Properties
	/// <summary>
	/// Transport Direct profile properties
	/// </summary>
	public class TDProperties
	{
		private Hashtable itemCache;
		private string userName;
		public TDProperties()
		{
			// Create a new profile property cache
			itemCache = new Hashtable();
		}

		/// <summary>
		/// Accesses a property cache item, given its key
		/// </summary>
		public ProfileProperties this [ string key ]
		{
			get
			{
				// Check whether the property cache contains an item with the key
				if ( itemCache.ContainsKey( key ) == false )
				{
					// No item found, so create a new profile property
					itemCache[key] = new ProfileProperties( null );
				}
				// Return the (possibly just created) item
				return ((ProfileProperties)itemCache[key]);
			}
			set
			{
				// Store the profile property in the cache with the given key
				itemCache[key] = new ProfileProperties( value );
			}
		}

		/// <summary>
		/// Removes the selected profile property
		/// </summary>
		/// <param name="key">The profile property to be removed</param>
		public void Remove(string key)
		{
			itemCache.Remove(key);
		}

		/// <summary>
		/// Checks if a property exists - even though value might be null.
		/// </summary>
		/// <param name="key">The property to check for</param>
		/// <returns>true if the property exists, false otherwise</returns>
		public bool ContainsKey(string key)
		{
			return itemCache.ContainsKey(key);
		}

		/// <summary>
		/// Read the user's property cache from storage
		/// </summary>
		/// <param name="userName">Identifies whose profile to load</param>
		/// <returns>TRUE if the profile was successfully loaded, FALSE otherwise</returns>
		public bool LoadProfile( string userName )
		{
			// Read in the property cache
			itemCache = ReadPropertyCache( userName );

			// If we got a property cache, remember the user name and return TRUE
			if ( itemCache != null )
			{
				this.userName = userName;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Write the property cache to storage
		/// </summary>
		public void Update()
		{
			WritePropertyCache();
		}


		/// <summary>
		/// Deletes the user's property cache from storage
		/// </summary>
		/// <param name="userName">Identifies whose profile to load</param>
		/// <returns>TRUE if the profile was successfully loaded, FALSE otherwise</returns>
		public bool DeleteProfile( string userName )
		{
			// Read in the property cache
			itemCache = ReadPropertyCache( userName );

			// If we got a property cache, delete it and return success flag
			if ( itemCache != null )
			{
				return DeletePropertyCache( userName);
			}
			else
			{
				return false;
			}
		}

		
		
		/// <summary>
		/// The username associated with this profile
		/// </summary>
		public string Username
		{
			get { return this.userName; }
			set { this.userName = value; }
		}

		#region WritePropertyCache and ReadPropertyCache
		public bool WritePropertyCache()
		{
			// The return value is initially set as UNDEFINED
			bool returnValue = true;

			// Create a connection to the "UserInfoDB" database
			IPropertyProvider currProps = Properties.Current;
			string connectionString = currProps[SqlHelperDatabase.UserInfoDB.ToString()];
			SqlConnection sqlConnection = new SqlConnection( connectionString );

			// Create a SQL command to invoke the stored procedure
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "usp_WriteUserProfile";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Connection = sqlConnection;


			// Create the stored procedure's parameters: RETURN_VALUE, userID, dataItem

			// The RETURN VALUE
			SqlParameter paramReturnValue = new SqlParameter( "RETURN_VALUE", SqlDbType.Int );
			paramReturnValue.Direction = ParameterDirection.ReturnValue;

			// The Username
			SqlParameter paramUserID = new SqlParameter( "@userID",  this.userName );
			paramUserID.SqlDbType = SqlDbType.VarChar;
			paramUserID.Size = 250;

			// The dataItem
			byte[] dataItem = SerializeItem( itemCache );
			SqlParameter paramDataItem = new SqlParameter( "@dataItem",  dataItem );
			paramDataItem.SqlDbType = SqlDbType.Image;
			paramDataItem.Size = dataItem.Length;

			// Add the parameters the SQL commands parameter collection
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add( paramReturnValue );
			sqlCommand.Parameters.Add( paramUserID );
			sqlCommand.Parameters.Add( paramDataItem );

			try
			{
				// Open the connection and invoke the stored procedure
				sqlCommand.Connection.Open();
				sqlCommand.ExecuteNonQuery();

				// Get the return value
				int storedProcedureReturnValue = (int) paramReturnValue.Value;

				// Act on the return value
				switch ( storedProcedureReturnValue )
				{
						// Profile data was updated
					case 0:
						returnValue = true;
						break;

						// Profile data was inserted
					case 1:
						returnValue = true;
						break;

						// Something went wrong
					case 2:
						// Log that something went wrong
						OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist - Stored Procedure usp_WriteUserProfile returned with unexpected return value of '2' for userID = '" + this.userName + "'");
						Logger.Write(operationalEvent);
						returnValue = false;
						break;
				}
			}
			catch (SqlException sqlException )
			{
				// Log the SQL Exception
				OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.SQLException. SQL Procedure = '" + sqlException.Procedure + "' Message = " + sqlException.Message);
				Logger.Write(operationalEvent);
			}
			catch (Exception exception )
			{
				// Log the general exception
				OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
				Logger.Write(operationalEvent);
			}
			finally
			{
				sqlCommand.Connection.Close();
			}
			return (returnValue);
		}

		private Hashtable ReadPropertyCache( string userName )
		{
			// The return value is initially set as NULL
			Hashtable returnValue = null;

			// Create a connection to the "UserInfoDB" database
			IPropertyProvider currProps = Properties.Current;
			string connectionString = currProps[SqlHelperDatabase.UserInfoDB.ToString()];
			SqlConnection sqlConnection = new SqlConnection( connectionString );

			// Create a SQL command to invoke the stored procedure
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "usp_ReadUserProfile";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Connection = sqlConnection;

			// Create the stored procedure's parameters: RETURN_VALUE, userID

			// The RETURN VALUE
			SqlParameter paramReturnValue = new SqlParameter( "RETURN_VALUE", SqlDbType.Int);
			paramReturnValue.Direction = ParameterDirection.ReturnValue;

			// The Username
			SqlParameter paramUserID = new SqlParameter( "@userID",  userName );
			paramUserID.SqlDbType = SqlDbType.VarChar;
			paramUserID.Size = 250;

			// Add the parameters the SQL commands parameter collection
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add( paramReturnValue );
			sqlCommand.Parameters.Add( paramUserID );

			try
			{
				// Open the connection and invoke the stored procedure
				sqlCommand.Connection.Open();
				SqlDataReader dataReader = sqlCommand.ExecuteReader();

				if ( dataReader != null )
				{
					dataReader.Read();
				}

				// Get the object to be deserialised from the dataItem parameter
				//if ( (int) paramReturnValue.Value == 1 )
				if ( dataReader.FieldCount != 0 )
				{
					byte[] dataItem = (byte[]) dataReader[ "dataItem" ];
					if ( dataItem.Length != 0 )
					{
						returnValue = (Hashtable) DeserializeItem( dataItem );
					}
				}
			}
			catch (SqlException sqlException )
			{
				// Log the SQL Exception
				OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.SQLException. SQL Procedure = '" + sqlException.Procedure + "' Message = " + sqlException.Message);
				Logger.Write(operationalEvent);
			}
			catch (Exception exception )
			{
				// Log the general exception
				OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
				Logger.Write(operationalEvent);
			}
			finally
			{
				sqlCommand.Connection.Close();
			}
			return (returnValue);
		}


		public bool DeletePropertyCache( string userName)
		{
			// The return value is initially set as UNDEFINED
			bool returnValue = true;

			// Create a connection to the "UserInfoDB" database
			IPropertyProvider currProps = Properties.Current;
			string connectionString = currProps[SqlHelperDatabase.UserInfoDB.ToString()];
			SqlConnection sqlConnection = new SqlConnection( connectionString );

			// Create a SQL command to invoke the stored procedure
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "usp_DeleteUserProfile";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Connection = sqlConnection;


			// Create the stored procedure's parameters: RETURN_VALUE, userID, dataItem

			// The RETURN VALUE
			SqlParameter paramReturnValue = new SqlParameter( "RETURN_VALUE", SqlDbType.Int );
			paramReturnValue.Direction = ParameterDirection.ReturnValue;

			// The Username
			SqlParameter paramUserID = new SqlParameter( "@userID",  this.userName );
			paramUserID.SqlDbType = SqlDbType.VarChar;
			paramUserID.Size = 250;


			// Add the parameters the SQL commands parameter collection
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add( paramReturnValue );
			sqlCommand.Parameters.Add( paramUserID );

			try
			{
				// Open the connection and invoke the stored procedure
				sqlCommand.Connection.Open();
				sqlCommand.ExecuteNonQuery();

				// Get the return value
				int storedProcedureReturnValue = (int) paramReturnValue.Value;

				// Act on the return value
				switch ( storedProcedureReturnValue )
				{
						// Profile data was updated
					case 0:
						returnValue = true;
						break;

						// Something went wrong
					case 1:
						// Log that something went wrong
						OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist - Stored Procedure usp_DeleteUserProfile returned with unexpected return value of '1' for userID = '" + this.userName + "'");
						Logger.Write(operationalEvent);
						returnValue = false;
						break;
				}
			}
			catch (SqlException sqlException )
			{
				// Log the SQL Exception
				OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.SQLException. SQL Procedure = '" + sqlException.Procedure + "' Message = " + sqlException.Message);
				Logger.Write(operationalEvent);
			}
			catch (Exception exception )
			{
				// Log the general exception
				OperationalEvent operationalEvent = new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
				Logger.Write(operationalEvent);
			}
			finally
			{
				sqlCommand.Connection.Close();
			}
			return (returnValue);
		}







		#endregion

		#region Item Serialization and Deserialization
		/// <summary>
		/// Returns a byte array of the serialized object
		/// </summary>
		/// <param name="objectToSerialize">The object to serialize</param>
		/// <returns>The byte array of the serialized object</returns>
		private byte[] SerializeItem( object objectToSerialize )
		{
			byte[] returnValue = new byte[0];

			// Only serialize non-null objects
			if ( objectToSerialize != null ) 
			{
				// Get a new MemoryStream          
				MemoryStream memoryStream = new MemoryStream(); 
			
				// Get a new Binary Formatter
				BinaryFormatter binaryFormatter = new BinaryFormatter(); 
				
				// Serialize the class into the memory stream and position the stream at the start
				binaryFormatter.Serialize( memoryStream, objectToSerialize ); 
				memoryStream.Seek(0,0); 

				// Assign the return value as the byte array of the memory stream
				returnValue = memoryStream.ToArray();
			}

			// Return the return value
			return ( returnValue );
		}

		/// <summary>
		/// Returns the object represented by a byte array of the serialised object
		/// </summary>
		/// <param name="serializedObject"></param>
		/// <returns></returns>
		private object DeserializeItem( byte[] serializedObject )
		{
			object returnObject = null;

			if ( serializedObject.Length != 0 ) 
			{
				// Get a new MemoryStream          
				MemoryStream memoryStream = new MemoryStream();
 
				// Write the byte array into the memory stream
				memoryStream.Write( serializedObject, 0, serializedObject.Length );
				memoryStream.Seek(0,0);
			
				// Get a binding converter to override the assembly name of the deserialised hastable
				BindingConverter serializationBinder = new BindingConverter();
				serializationBinder.BindToType( this.GetType().Assembly.FullName, "System.Collections.Hashtable");

				// Get a new Binary Formatter and use the binding converter
				BinaryFormatter binaryFormatter = new BinaryFormatter(); 
				binaryFormatter.Binder = serializationBinder;
				
				// Set the return object to the deserialized object in the memory stream
				returnObject = (object) binaryFormatter.Deserialize( memoryStream ); 
			}

			// Return the return object
			return ( returnObject );
		}
		#endregion
    }
	#endregion

	#region Transport Direct User Profile
	/// <summary>
	/// Transport Direct Profile
	/// </summary>
    public class TDProfile : ITDSessionAware
	{
		private TDProperties tdProperties;
        private bool dirty = false;

		/// <summary>
		/// Transport Direct Profile constructor
		/// </summary>
		public TDProfile ()
		{
			tdProperties = new TDProperties();
		}

		/// <summary>
		/// Holds the properties that constitute the user's profile
		/// </summary>
		public TDProperties Properties
		{
			get { return ( tdProperties ); }
		}

		/// <summary>
		/// Loads the profile from storage
		/// </summary>
		/// <param name="userName">The name of the user for whom the profile should be loaded</param>
		/// <returns></returns>
		public bool LoadProfile( string userName )
		{
			return ( tdProperties.LoadProfile( userName ) );
		}


		/// <summary>
		/// Loads the profile from storage
		/// </summary>
		/// <param name="userName">The name of the user for whom the profile should be loaded</param>
		/// <returns></returns>
		public bool DeleteProfile( string userName )
		{
			return ( tdProperties.DeleteProfile( userName ) );
		}

		/// <summary>
		/// Persits the profile
		/// </summary>
		public void Update()
		{
			tdProperties.Update();
            dirty = true;
		}

		/// <summary>
		/// The username associated with the profile
		/// </summary>
		public string Username
		{
			get { return tdProperties.Username; }
			set { tdProperties.Username = value; }
		}

        #region ITDSessionAware Members

        public bool IsDirty
        {
            get
            {
                return dirty;
            }
            set
            {
                dirty = false;
            }
        }

        #endregion
    }
	#endregion
}
