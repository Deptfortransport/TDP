//********************************************************************************
//NAME         : CoachOperatorLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 12/10/2005
//DESCRIPTION  : Implementation of CoachOperatorLookup class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/CoachOperatorLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:34   mturner
//Initial revision.
//
//   Rev 1.2   Oct 26 2005 11:02:32   mguney
//Logging added to GetOperatorDetails.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 14 2005 08:50:28   mguney
//Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 13 2005 09:35:24   mguney
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
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// CoachOperatorLookup. Singleton.
	/// </summary>
	public class CoachOperatorLookup : IServiceFactory, ICoachOperatorLookup
	{
		#region Private members
		private const string DataChangeNotificationGroup = "CoachOperatorLookup";

		/// <summary>
		/// Hashtable used to store Coach Operator data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable CachedData;	

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;	

		private static CoachOperatorLookup current;
		#endregion		

		#region Constructor
		/// <summary>
		/// Private constructor. Initialises the CoachOperatorLookup.
		/// </summary>
		public CoachOperatorLookup()
		{
			LoadData();
			receivingChangeNotifications = RegisterForChangeNotification();			
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Loads the Coach Operator data
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
				
				
				// Get Coach Operator data
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Coach Operator data started" ));
				}

				//Synchronize CachedData
				lock(this)
				{
                    // Changed to not use CaseInsensitiveHashCodeProvider to remove .Net 2.0 compiler warning
                    CachedData = new Hashtable();
				
					// Execute the GetCoachOperators stored procedure.					
					reader = helper.GetReader( "GetCoachOperators", new Hashtable());
					while (reader.Read())
					{												
						//construct the operator details object
						string tdpOperatorCode = reader.GetString(1);
						string operatorName = reader.GetString(3);
						string url = reader.GetString(4);
						CoachFaresInterfaceType interfaceType = (reader.GetString(2) == "R") ? 
							CoachFaresInterfaceType.ForRoute : CoachFaresInterfaceType.ForJourney;
						CoachOperator coachOperator = 
							new CoachOperator(interfaceType,tdpOperatorCode,operatorName,url);		
						//add the constructed object to the hash table
						string cjpOperatorCode = reader.GetString(0);
						CachedData.Add(cjpOperatorCode.ToUpper(),coachOperator);							
					}
					reader.Close();
				}

				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Coach Operator data completed" ));
					return true;
				}
			

				#endregion Load data into hashtables
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"An SQL exception occurred whilst attempting to load the Coach Operator data.", sqle));
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
						"DataChangeNotificationService was not present when initializing CoachOperator"));
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
		/// Returns the current CoachOperatorLookup object
		/// </summary>
		/// <returns>CoachOperatorLookup object</returns>
		public object Get()
		{
			if (current == null)
				current = new CoachOperatorLookup();			
			return current;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Method for getting the coach operator details using the given cjp operator code.
		/// In CoachOperatorCodes table, there will be dummy entries which will contain the 
		/// tdp operator code in the cjp operator code column. So this method will return the 
		/// tdp operator code even if a tdp operator code is given as the input.
		/// </summary>
		/// <param name="operatorCode">Operator code</param>
		/// <returns>CoachOperator object.</returns>
		public CoachOperator GetOperatorDetails(string cjpOperatorCode)
		{
			if(CachedData.ContainsKey(cjpOperatorCode.ToUpper()))			
				return (CoachOperator)CachedData[cjpOperatorCode.ToUpper()];
			else 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					"The operator code not found:" + cjpOperatorCode));
				return null;
			}
		}

		#endregion
	}
}
