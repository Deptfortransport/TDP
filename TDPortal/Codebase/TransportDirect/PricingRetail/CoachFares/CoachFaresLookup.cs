//********************************************************************************
//NAME         : CoachFaresLookup.cs
//AUTHOR       : Mitesh Modi
//DATE CREATED : 08/05/2007
//DESCRIPTION  : Implementation of CoachFaresLookup class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/CoachFaresLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:32   mturner
//Initial revision.
//
//   Rev 1.1   May 10 2007 16:24:48   asinclair
//Set the CoachFaresAction value correctly
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.0   May 09 2007 14:33:18   mmodi
//Initial revision.
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//

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

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// CoachFaresLookup. Singleton.
	/// </summary>
	public class CoachFaresLookup : IServiceFactory, ICoachFaresLookup
	{
		#region Private members
		internal const string DATACHANGENOTIFICATIONGROUP = "CoachFaresLookup";
		internal const string ACTIONEXCLUDE = "EX";
		internal const string ACTIONINCLUDE = "IN";

		/// <summary>
		/// Hashtable used to store Coach Fares Action data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable hashCoachFareAction;

		/// <summary>
		/// Hashtable used to store Coach Fares Restriction data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable hashCoachFareRestictionPriority;

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;	

		private static CoachFaresLookup current;
		#endregion		

		#region Constructor
		/// <summary>
		/// Constructor. Initialises the CoachFaresLookup
		/// </summary>
		public CoachFaresLookup()
		{
			LoadData();
			receivingChangeNotifications = RegisterForChangeNotification();			
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Loads the Exceptional Fares data
		/// </summary>
		private bool LoadData()
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
				
				// Get Exceptional Fares data
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Coach Fares Lookup data started" ));
				}

				//Synchronize CachedData
				lock(this)
				{
                    // Changed to not use a CaseInsentiveHashCodeProvider to remove .Net 2.0 Compiler Warning
                    hashCoachFareAction = new Hashtable();
					hashCoachFareRestictionPriority = new Hashtable();

					// Execute the GetCoachFares stored procedure.					
					reader = helper.GetReader( "GetCoachFares", new Hashtable());
					while (reader.Read())
					{												
						//construct the Coach Fares
						string fareTypeCode = reader.GetString(0);
						//string fareTypeDescription = reader.GetString(1);
						//bool isAmendable = reader.GetBoolean(2);
						//bool isRefundable = reader.GetBoolean(3);
						int restrictionPriority = reader.GetInt32(4);
						CoachFaresAction coachFaresAction = (reader.GetString(5).Equals(ACTIONINCLUDE)) 
							? CoachFaresAction.Include : CoachFaresAction.Exclude;							
						
						//add the actions and fare types to the hash table
						hashCoachFareAction.Add(fareTypeCode.ToUpper(), coachFaresAction);

						//add the restiction priority and fare types to the hash table
						hashCoachFareRestictionPriority.Add(fareTypeCode.ToUpper(), restrictionPriority);
					}
					reader.Close();
				}

				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Coach Fares data completed" ));
					return true;
				}
			

				#endregion Load data into hashtables
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"An SQL exception occurred whilst attempting to load the Coach Fares Lookup data.", sqle));
				return false;
			}
			finally
			{
				//close the database connection
				helper.ConnClose();
			}

			return false;
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, 
						"DataChangeNotificationService was not present when initializing Coach Fares"));
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
			if (e.GroupId == DATACHANGENOTIFICATIONGROUP)
				LoadData();
		}

		#endregion

		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current CoachFaresLookup object
		/// </summary>
		/// <returns>CoachFaresLookup object</returns>
		public object Get()
		{
			if (current == null)
				current = new CoachFaresLookup();			
			return current;
		}

		#endregion

		#region ICoachFaresLookup Members

		/// <summary>
		/// Method for getting the coach fares action using the given fare type (fare type code).
		/// </summary>
		/// <param name="fareType">Fare type code</param>
		/// <returns>CoachFaresAction</returns>
		public CoachFaresAction GetCoachFaresAction(string fareTypeCode)
		{
			if(hashCoachFareAction.ContainsKey(fareTypeCode.ToUpper()))			
				return (CoachFaresAction)hashCoachFareAction[fareTypeCode.ToUpper()];
			else 
			{				
				return CoachFaresAction.NotFound;
			}
		}

		/// <summary>
		/// Method for getting the coach fares restriction priority using the given fare type (fare type code).
		/// </summary>
		/// <param name="fareType">Fare type code</param>
		/// <returns>Restriction priority as int</returns>
		public int GetCoachFaresRestriction(string fareTypeCode)
		{
			if(hashCoachFareRestictionPriority.ContainsKey(fareTypeCode.ToUpper()))			
				return (int)hashCoachFareRestictionPriority[fareTypeCode.ToUpper()];
			else 
			{				
				return 0;
			}
		}

		#endregion
	}
}
