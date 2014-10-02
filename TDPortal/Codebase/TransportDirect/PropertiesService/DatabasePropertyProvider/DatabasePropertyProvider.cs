// *********************************************** 
// NAME                 : DatabasePropertyProvider.cs
// AUTHOR               : Patrick ASSUIED 
// DATE CREATED         : 2/07/2003 
// DESCRIPTION  : A DLL, component of the PropertyService that access a DB to get 
// get the properties
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/DatabasePropertyProvider/DatabasePropertyProvider.cs-arc  $ 
//
//   Rev 1.1   Mar 10 2008 15:22:50   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:37:38   mturner
//Initial revision.
//
//   Rev 1.23   Feb 23 2006 19:15:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.22.2.0   Nov 25 2005 18:17:16   schand
//Code changes for New version to get properties with Partner Id.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.22   Dec 18 2003 12:17:16   PNorell
//Resolution for IR296.
//
//   Rev 1.21   Oct 30 2003 16:15:42   PNorell
//Declared the property for new version public again.
//
//   Rev 1.20   Oct 30 2003 14:53:16   PNorell
//Updated to support crypto.
//
//   Rev 1.19   Oct 23 2003 18:31:16   PNorell
//Fixed second location and removed catch.
//
//   Rev 1.18   Oct 23 2003 18:27:48   PNorell
//Ensured null-reference is not caused by closing the non-open connection.
//
//   Rev 1.17   Oct 10 2003 16:42:48   passuied
//Added Try catch + finally for Population method
//
//   Rev 1.16   Oct 09 2003 09:54:54   pscott
//Added details to exception message to help determine property database error
//
//   Rev 1.15   Oct 08 2003 17:18:36   PNorell
//Updated to overwrite duplicate parameters on the same level.
//
//   Rev 1.14   Oct 03 2003 13:38:38   PNorell
//Updated the new exception identifier.
//
//   Rev 1.13   Jul 30 2003 18:48:40   geaton
//Changed OperationalEvent constructor.
//
//   Rev 1.12   Jul 29 2003 18:32:20   geaton
//Swapped OperationalEvent parameter order after change in OperationalEvent constructor.
//
//   Rev 1.11   Jul 25 2003 10:38:06   passuied
//addition of CLSCompliant
//
//   Rev 1.10   Jul 23 2003 10:22:50   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.9   Jul 18 2003 11:00:38   passuied
//useless exceptions removed
//
//   Rev 1.8   Jul 17 2003 17:16:50   passuied
//updated
//
//   Rev 1.7   Jul 17 2003 12:43:40   passuied
//changes after code review


using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Timers;

using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure.Content;


using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Common.PropertyService.DatabasePropertyProvider
{
	/// <summary>
	/// component loading properties from a Database
	/// </summary>

	public class DatabasePropertyProvider : Properties.Properties
	{
		#region Constant declarations
		private const int INITIAL_VERSION = -1;

		private const int KEY = 0;
		private const int VALUE = 1;
        private const int THEME = 2;

		private const string PROP_APPID = "propertyservice.applicationid";
		private const string PROP_GRPID = "propertyservice.groupid";
		private const string PROP_VERSION = "propertyservice.version";
		private const string PROP_LOCKEDPROP = "propertyservice.lockedproperties";
												
		private const string PROP_CONSTR = "propertyservice.providers.databaseprovider.connectionstring";

        private const string SPROC_SELCON = "SelectContentDatabaseProperty";
		private const string SPROC_SELAPP = "SelectApplicationPropertiesWithPartnerId";
		private const string SPROC_SELGRP = "SelectGroupPropertiesWithPartnerId";
		private const string SPROC_SELGLB = "SelectGlobalPropertiesWithPartnerId";
		private const string SPROC_GETVER = "GetVersion";
		
		private const string AID = "@AID";
		private const string GID = "@GID";

		#endregion

		#region Private variables
		/// <summary>
		/// The connection string in unencrypted form
		/// </summary>
		private string connectionString;

		/// <summary>
		/// The decryption engine used. 
		/// This is updated everytime before it should load the properties.
		/// </summary>
		private ITDCrypt decryptionEngine;

		#endregion

		#region Constructors
		/// <summary>
		/// Creates a database property provider which reads its initial settings from App settings.
		/// It will also load the decryption engine from service discovery and use that to decrypt
		/// the connection string if nessecary.
		/// </summary>
		public DatabasePropertyProvider()
		{
			// Read General ConfigurationSettings (AppID Group ID property assembly and class)
			strApplicationID = ConfigurationManager.AppSettings[ PROP_APPID ];
			strGroupID = ConfigurationManager.AppSettings[ PROP_GRPID ];

			// Initialise property table - this is not used any more, propertyDictionary getting used instead.
			propertyTable = new Hashtable();

            // Initialise the property dictionary
            propertyDictionary = new System.Collections.Generic.Dictionary<int, Hashtable>();

			// Get latest decryption engine - changed from using the service discovery because of IR296
			decryptionEngine = (ITDCrypt)new CryptoFactory().Get();
			// This should be changed back to the below statement if it is fixed
			// decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ ServiceDiscoveryKey.Crypto ];
			// This applies to any of the below methods as well.

			// Get connection string
			connectionString = ConfigurationManager.AppSettings[PROP_CONSTR];
			// Ensure connection string is in clear
			ToClear( ref connectionString );

			// Ensure initial version
			intVersion = INITIAL_VERSION;

		}
		#endregion

		#region Public methods
		/// <summary>
		/// Checks if there is a newer version of the properties available.
		/// </summary>
		public bool IsNewVersion
		{
			get
			{
				// create the db connection object
				SqlConnection dbConnection = new SqlConnection(connectionString);
				SqlDataReader dr = null;

				try
				{
					// Initialise the sql command
					SqlCommand cm = new SqlCommand(SPROC_GETVER, dbConnection);
					cm.CommandType = CommandType.StoredProcedure;

					// Open the connection
					dbConnection.Open();
					// Execute the command
					dr = cm.ExecuteReader(CommandBehavior.CloseConnection);
				
					// read the current version - this should _always_ exist
					dr.Read();

					// transform value to int
					int currentVersion = Convert.ToInt32(dr.GetString(0), CultureInfo.CurrentCulture.NumberFormat);

					// If value differs from what we previously had - there is a new version available
					if (currentVersion != Version)
						return true;
					else
						return false;
				}
				catch (InvalidOperationException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Database,
						TDTraceLevel.Error,
						"An error occurred accessing the Version property in the Properties Database"+e.Message);
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to get the version property from property Database: " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidVersion);
				}
				catch (SqlException e)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Database,
						TDTraceLevel.Error,
						"An error has occurred accessing the Version property in the Properties Database :- "+e.Message);
					Logger.Write(oe);
					throw new TDException(
						"Exception trying to get the version property from property Database: " +e.Message, 
						true, 
						TDExceptionIdentifier.PSInvalidVersion);
				}
				finally
				{
					// Close the data reader
					if( dr != null )
					{
						dr.Close();
					}
					// Close the db connection
					if( dbConnection != null )
					{
						dbConnection.Close();
					}
				}
			}
		}

		/// <summary>
		/// Asks the property to either populate itself or check if there is new properties 
		/// and return a new instance. It is up to the called of this method to ensure that
		/// the old property provider properly gets superseeded.
		/// </summary>
		/// <returns>Null if no new properties exists, or a property provider if new properties is available.</returns>
		public override Properties.IPropertyProvider Load()
		{
			try
			{
				if (Version == INITIAL_VERSION)
				{
					// For initial version, only need to populate my own properties
					PopulateProperties();
					// return myself
					return this;
				}
				else if (IsNewVersion)
				{
					// if there is a new version in the database - create new instance 
					DatabasePropertyProvider newInstance = new DatabasePropertyProvider();
					// Ensure new instance loads itself and return its prefered provider (itself in this case)
					return newInstance.Load();
				}
			}
			catch (SqlException e)
			{
				string message = "An SQL error has occurred manipulating the Properties Database: SQLNUM="+e.Number+" : Message " + e.Message;
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error,
					message);
				Logger.Write(oe);

				throw new TDException(
					message, 
					true, 
					TDExceptionIdentifier.PSDatabaseFailure);
			}
			catch (InvalidOperationException e)
			{

				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error,
					"An error has occurred manipulating the Properties Database");
				Logger.Write(oe);
				throw new TDException(
					"Exception trying to manipulate the Properties Database" +e.Message, 
					true, 
					TDExceptionIdentifier.PSDatabaseFailure);
			}

			// If no changes are needed - just return null
			return null;

		}
		#endregion

		#region Private convience methods
		/// <summary>
		/// Populates the property lists with a new set of properties.
		/// It ensures that application properties are handled first, group secondly and the global ones last.
		/// </summary>
		private void PopulateProperties()
		{
			SqlConnection dbConnection = new SqlConnection(connectionString);
			// Get latest decryption engine - this serves no purpose now as the decryptionEngine is not retrieved from the service discovery
			// decryptionEngine = (ITDCrypt)new CryptoFactory().Get();


			try
			{
				// Store encrypted values
				ArrayList enc = new ArrayList();

				// Open connection - connection  is open while all the properties are being fetched.
				dbConnection.Open();

                // ----- Get Application Properties ------
                AddAll(SPROC_SELAPP, dbConnection, CreateParam(AID, ApplicationID), enc);
                // ---- End Get Application properties	

                // ----- Get Group Properties --------
                AddAll(SPROC_SELGRP, dbConnection, CreateParam(GID, GroupID), enc);
                // ------ End Get Group Properties ---------
                			
				// ------ Get Global properties ------------
				AddAll(SPROC_SELGLB, dbConnection, null, enc ); 
				// ------ End Global properties ------------

				// Ensure that the locked property was encrypted
				if( !enc.Contains( "0." + PROP_LOCKEDPROP ) )
				{
					// Fatal - the locked prop has been tampered - this as an fatal error
					string message = "The property ["+PROP_LOCKEDPROP+"] should have been encrypted but is not. The property service will not update with new settings. This property might have been tampered";
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Infrastructure,
						TDTraceLevel.Error,
						message );
					Logger.Write(oe);
					throw new TDException(
						message, 
						true, 
						TDExceptionIdentifier.PSLockedPropertyNotEncrypted);

				}
				// Ensure all other locked properties was encrypted
				char[] sep = { ' ' };
				string[] locked = this[PROP_LOCKEDPROP].Split( sep );
				foreach( string prop in locked )
				{
					if( !enc.Contains( prop ) )
					{
						// If it does not contain a crypted property with this name
						// we return false as this object can not be used as property
						// Also, log this as an fatal error
						string message = "Property ["+prop+"] should have been encrypted but is not. The property service will not update with new settings. Please correct and ensure the property is encrypted";
						OperationalEvent oe = 
							new  OperationalEvent(	TDEventCategory.Infrastructure,
							TDTraceLevel.Error,
							message );
						Logger.Write(oe);
						throw new TDException(
							message, 
							true, 
							TDExceptionIdentifier.PSUnencryptedPropertyExists);

					}
				}

				// Populate Version
				intVersion = Convert.ToInt32(this[ PROP_VERSION ], CultureInfo.InvariantCulture.NumberFormat);

			}
			finally
			{
				if( dbConnection != null )
				{
					dbConnection.Close();
				}
			}
		}

		/// <summary>
		/// Adds all properties (if they are unset previously) to the property table.
		/// All properties are decrypted and added to the decryption list before being added.
		/// </summary>
		/// <param name="storedProc">The stored procedure to call</param>
		/// <param name="con">The connection to use</param>
		/// <param name="param">An SQL parameter that should be included (or null)</param>
		/// <param name="enc">The encryption list to add any encrypted keys</param>
		private void AddAll(string storedProc, SqlConnection con, SqlParameter param, ArrayList enc)
		{
            //We need an exception to handle the content database:


			// The data reader
			SqlDataReader dr = null;
			try
			{
				// Execute the correct stored procedure
				dr = ExecuteCommand(storedProc, con, param); 

				// For every returned answer
				while (dr.Read())
				{
                    //retrive the theme 
                    int themeId = dr.GetInt32(THEME);
					// retrive key
					string key = dr.GetString( KEY );
					// retrive value
					string val = dr.GetString( VALUE );

					// Add them (after possible decryption) to the property table
					Add( themeId, key, val, enc);
				}
			}
			finally
			{
				// If we have a data reader
				if( dr != null )
				{
					// close it
					dr.Close();
				}
			}

		}

		/// <summary>
		/// Creates a SQL Parameter with the given name and value.
		/// It will only create parameters with the NVarChar type and
		/// read only.
		/// </summary>
		/// <param name="name">The name of the parameter, ie "@GID"</param>
		/// <param name="val">The value of the parameter</param>
		/// <returns>A constructed SqlParameter object</returns>
		private SqlParameter CreateParam( string name, string val)
		{
			SqlParameter sp = new SqlParameter(name, SqlDbType.NVarChar);
			sp.Direction = ParameterDirection.Input;
			sp.Value = val;
			return sp;
		}

		/// <summary>
		/// Executes a stored procedure and returns the appropriate reader from that
		/// procedure.
		/// </summary>
		/// <param name="storedProc">The name of the procedure</param>
		/// <param name="con">The connection to use</param>
		/// <param name="param">The sql parameter if such exists</param>
        /// <param name="requestUrl">The requested url, used to deduce the theme/partner</param>
		/// <returns>A Sql data reader</returns>
        private SqlDataReader ExecuteCommand(string storedProc, SqlConnection con, SqlParameter param)
		{
			// The command
			SqlCommand cm = new SqlCommand(storedProc, con);
			// As a stored procedure
			cm.CommandType = CommandType.StoredProcedure;
			// Only add the parameter is there is one
			if( param != null )
			{
				cm.Parameters.Add(param);
			}

           
			// return the reader
			return cm.ExecuteReader();
		}

		/// <summary>
		/// Adds the value and key to the property table after ensuring both of them are in clear-text.
		/// If the value is encrypted, the key is added to the encryption list.
		/// A key or value is treated as encrypted if it starts with TDCrypt.CRYPT_PREFIX
		/// </summary>
		/// <param name="key">The key of the property (encrypted or in clear)</param>
		/// <param name="val">The value of the property (encrypted or in clear)</param>
		/// <param name="encrypted">The encryption list to add any keys for the properties that has been encrypted</param>
		/// <returns>true if the property was added, false otherwise</returns>
		private bool Add(int theme, string key, string val, ArrayList encrypted)
		{
			ToClear( ref key );
			// Add it if it is non-existant
            if (!propertyDictionary.ContainsKey(theme))
                propertyDictionary.Add(theme, new Hashtable());

			if( !propertyDictionary[theme].Contains(key) )
			{
				// Check for encryption
				if( ToClear( ref val ) )
				{
					// Clear text it (key and value) and add it to encrypted list
					encrypted.Add( key );
				}
				propertyDictionary[theme].Add( key, val);
				// return true if it was added
				return true;
			}
			return false;
		}

		/// <summary>
		/// Changes a value to clear text if encrypted, otherwise leaves value untouched.
		/// </summary>
		/// <param name="val">The value that should be in clear text</param>
		/// <returns>true if the value was encrypted and it succeded decrypting it</returns>
		private bool ToClear( ref string val )
		{
			// Check if value claims it is encrypted
			if( val.StartsWith( TDCrypt.CRYPT_PREFIX ) )
			{
				// Decrypt value
				val = decryptionEngine.Decrypt( val.Substring( TDCrypt.CRYPT_PREFIX.Length ) );
				return true;
			}
			return false;
		}

		#endregion



	}
}
