// *********************************************** 
// NAME			: BayTextFilter.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 14/07/2005
// DESCRIPTION	: Implemention of the BayTextFilter class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/BayTextFilter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:36   mturner
//Initial revision.
//
//   Rev 1.1   Jul 25 2005 14:18:12   RWilby
//Module association added
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 25 2005 13:18:46   RWilby
//Initial revision.
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for BayTextFilter.
	/// </summary>
	[Serializable()]
	public class BayTextFilter : IBayTextFilter , IServiceFactory
	{
		#region Private members
		/// <summary>
		/// Hashtable used to store Bay Text Filter data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable CachedData;

		private const string DataChangeNotificationGroup = "BayTextFilter";

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;
		#endregion

		#region Constructor
		/// <summary>
		/// Private constructor. Initialises the BayTextFilter.
		/// </summary>
		public BayTextFilter()
		{
			LoadData();	
			receivingChangeNotifications = RegisterForChangeNotification();
		}
	
		#endregion
		
		#region Public methods
		/// <summary>
		/// Tests if the Bay Text is displayable for a given Traveline
		/// </summary>
		/// <param name="travelineRegion">The Traveline Region</param>
		/// <returns>True if Bay Text is displayable for the Traveline
		/// or false otherwise</returns>
		public bool FilterText (string travelineRegion)
		{ 
			if(CachedData.ContainsKey(travelineRegion))
			{
				return (bool)CachedData[travelineRegion];
			}
			else
			{
				return true;
			}
		}
		#endregion

        #region Private methods
		/// <summary>
		/// Loads the BayTextFilter data
		/// </summary>
		private void LoadData()
		{
			SqlDataReader reader;
			SqlHelper helper = new SqlHelper();

			try
			{
				// Initialise a SqlHelper and connect to the database.
				Logger.Write( new OperationalEvent( TDEventCategory.Business,
					TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.DefaultDB.ToString() ));
				
				helper.ConnOpen(SqlHelperDatabase.DefaultDB);

				#region Load data into hashtables
				
				
				// Get Bay Text Filter data
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Bay Text Filter data started" ));
				}

				//Synchronize CachedData
				lock(this)
				{
					CachedData = new Hashtable(
						new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture),
						new CaseInsensitiveComparer(CultureInfo.InvariantCulture));
				
					// Execute the GetBayTextFilter stored procedure.
					// This returns the entire contents of the BayTextFilter table
					reader = helper.GetReader( "GetBayTextFilter", new Hashtable());
					while (reader.Read())
					{
						CachedData.Add(reader.GetString(0), 
							string.Compare(reader.GetString(1),"Y",true,CultureInfo.InvariantCulture) ==0? true : false);
					}
					reader.Close();
				}

				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Bay Text Filter data completed" ));
				}
			

				#endregion Load data into hashtables
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An SQL exception occurred whilst attempting to load the Bay Text Filter data.", sqle));
			}
			finally
			{
				//close the database connection
				helper.ConnClose();
			}
		}


		
		/// <summary>
		/// Registers an event handler with the data change notification service
		/// </summary>
		private bool RegisterForChangeNotification()
		{
			IDataChangeNotification notificationService;
			try
			{
				notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
			}
			catch (TDException e)
			{
				// If the SDInvalidKey TDException is thrown, return false as the notification service
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing BayTextFilter"));
					return false;
				}
				else
					throw;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChanged);
			return true;
		}
		#endregion

		#region Data Changed Event handler

		/// <summary>
		/// Used by the Data Change Notification service to reload the data if it is changed in the DB
		/// </summary>
		private void DataChanged(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == DataChangeNotificationGroup)
				LoadData();
		}

		#endregion

		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current BayTextFilter object
		/// </summary>
		/// <returns>BayTextFilter object</returns>
		public object Get()
		{
			return this;
		}

		#endregion
	}


}
