//********************************************************************************
//NAME         : ExceptionalFaresLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 28/11/2005
//DESCRIPTION  : Implementation of ExceptionalFaresLookup class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/ExceptionalFaresLookup.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:11:34   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Mar 20 2008 15:00 mmodi
//Placed try catch around get Theme as in some instances the current theme cannot be determined
//
//   Rev 1.0   Nov 08 2007 12:36:34   mturner
//Initial revision.
//
//   Rev 1.1   Nov 29 2005 20:05:18   mguney
//Logging for unfound fare types removed.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.0   Nov 28 2005 16:17:08   mguney
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
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TD.ThemeInfrastructure;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// ExceptionalFaresLookup. Singleton.
	/// </summary>
	public class ExceptionalFaresLookup : IServiceFactory, IExceptionalFaresLookup
	{
		#region Private members
		internal const string DATACHANGENOTIFICATIONGROUP = "ExceptionalFaresLookup";
		internal const string ACTIONEXCLUDE = "EX";
		internal const string ACTIONDAYRETURN = "DR";

		/// <summary>
		/// Hashtable used to store Exceptional Fares data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable CachedData;	

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;	

		private static ExceptionalFaresLookup current;
		#endregion		

		#region Constructor
		/// <summary>
		/// Private constructor. Initialises the ExceptionalFaresLookup.
		/// </summary>
		public ExceptionalFaresLookup()
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
						TDTraceLevel.Verbose, "Loading Exceptional Fares Lookup data started" ));
				}

				//Synchronize CachedData
				lock(this)
				{
                    // Changed to not use CaseInsensitiveHashCodeProvider to remove .Net 2.0 Compiler warning
                    CachedData = new Hashtable();
                    Hashtable htParameters = new Hashtable();

                    #region Get Theme
                    // May not have a HttpContext, so try to get current theme, otherwise default
                    Theme theme = null;
                    try
                    {
                        theme = ThemeProvider.Instance.GetTheme();
                    }
                    catch
                    {
                        theme = ThemeProvider.Instance.GetDefaultTheme();
                    }
                    #endregion

                    htParameters.Add("@ThemeName", theme.Name );
				
					// Execute the GetExceptionalFares stored procedure.					
                    reader = helper.GetReader("GetExceptionalFares", htParameters);
					while (reader.Read())
					{												
						//construct the Exceptional Fares
						string fareType = reader.GetString(0);						
						ExceptionalFaresAction exceptionalFaresAction = (reader.GetString(1).Equals(ACTIONEXCLUDE)) 
							? ExceptionalFaresAction.Exclude : ExceptionalFaresAction.DayReturn;							
						//add the actions and fare types to the hash table						
						CachedData.Add(fareType.ToUpper(),exceptionalFaresAction);							
					}
					reader.Close();
				}

				// Log the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Exceptional Fares data completed" ));
					return true;
				}
			

				#endregion Load data into hashtables
			}
			catch (SqlException sqle)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"An SQL exception occurred whilst attempting to load the Exceptional Fares Lookup data.", sqle));
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
						"DataChangeNotificationService was not present when initializing Exceptional Fares"));
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
		/// Returns the current ExceptionalFaresLookup object
		/// </summary>
		/// <returns>ExceptionalFaresLookup object</returns>
		public object Get()
		{
			if (current == null)
				current = new ExceptionalFaresLookup();			
			return current;
		}

		#endregion

		#region Public methods

		

		#endregion

		#region IExceptionalFaresLookup Members

		/// <summary>
		/// Method for getting the exceptional fares using the given fare type (fare name or code).
		/// </summary>
		/// <param name="fareType">Fare type</param>
		/// <returns>ExceptionalFaresAction</returns>
		public ExceptionalFaresAction GetExceptionalFaresAction(string fareType)
		{
			if(CachedData.ContainsKey(fareType.ToUpper()))			
				return (ExceptionalFaresAction)CachedData[fareType.ToUpper()];
			else 
			{				
				return ExceptionalFaresAction.NotFound;
			}
		}

		#endregion
	}
}
