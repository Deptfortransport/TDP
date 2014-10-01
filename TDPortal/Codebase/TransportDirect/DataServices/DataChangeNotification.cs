// ***********************************************
// NAME 		: DataChangeNotificationService.cs
// AUTHOR 		: Rob Greenwood
// DATE CREATED : 10-Jun-2004
// DESCRIPTION 	: Service that is accessed as part
// of DataServices. Provides a polling service that
// checks for updates to ChangeNotificiation tables
// in nominated databases, and raises an event when
// a change occurs. In order to run, this service requires 
// at least the PollingInterval property to be present and
// hold a valid value, and for the Groups property to be
// present (can be null).
// NOTE: This code requires that the ChangeNotification
// table and GetChangeTable stored procedure have been
// created in the nominated database(s). To do this,
// use the SQL template DataNotificationDBSetup.sql
// located in \TDPortal\DEL5\TransportDirect\DataServices\
// for each databse as required.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/DataChangeNotification.cs-arc  $
//
//   Rev 1.1   Mar 21 2013 10:12:02   mmodi
//Commented out some verbose logging which had little use and clogged up logs
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.0   Nov 08 2007 12:20:44   mturner
//Initial revision.
//
//   Rev 1.14   Sep 06 2004 21:08:26   JHaydock
//Major update to travel news
//
//   Rev 1.13   Aug 09 2004 15:42:24   jgeorge
//Bug fix
//
//   Rev 1.12   Jun 24 2004 13:50:02   rgreenwood
//Updated to pass unit tests.
//
//   Rev 1.11   Jun 18 2004 15:10:02   rgreenwood
//Initial debug version. Unit tests outstanding
//
//   Rev 1.10   Jun 16 2004 17:45:12   rgreenwood
//Changed to implement IDataChangeNotification
//
//   Rev 1.9   Jun 15 2004 20:39:44   CHosegood
//No longer using the service discover to find the properties service.
//
//   Rev 1.8   Jun 15 2004 20:26:40   CHosegood
//Added #region's
//
//   Rev 1.7   Jun 15 2004 20:08:54   CHosegood
//Added logging events
//
//   Rev 1.6   Jun 15 2004 18:53:42   rgreenwood
//Added XML Documentation
//
//   Rev 1.5   Jun 15 2004 15:41:00   CHosegood
//Refactored
//
//   Rev 1.4   Jun 15 2004 14:56:52   CHosegood
//Added code documentaiton
//
//   Rev 1.3   Jun 15 2004 11:45:20   rgreenwood
//Added error handling code for completeness
//
//   Rev 1.2   Jun 11 2004 14:40:56   rgreenwood
//Added Header information

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Resources;
using System.Timers;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;
using System.Text;

namespace TransportDirect.UserPortal.DataServices
{
    #region DataChangeNotification class
	/// <summary>
	/// Summary description for DataChangeNotificationService.
	/// </summary>
	public class DataChangeNotification : IDataChangeNotification
	{
        #region Instance members
		/// <summary>
		/// Hashtable that holds the sqlhelperDB object (key), and another
		/// hashtable (value) that contains a copy of the ChangeNotification table
		/// read from the database on initialisation.
		/// </summary>
		private Hashtable changeNotificationTables;

		/// <summary>
		/// Holds DataChangeNotificationGroup objects
		/// </summary>
		private ArrayList changeGroups;

		/// <summary>
		/// Timer that fires an elapsed event whenever the configurable PollingInterval
		/// elapses, in order to execute the Poll method (listener).
		/// </summary>
		private Timer pollingTimer = null;

		/// <summary>
		/// Configurable duration between elapsed events. Configured by property service.
		/// </summary>
		private int pollingInterval = 0;
        #endregion

		#region Internal properties

		internal ArrayList ChangeGroups
		{
			get { return (ArrayList)this.changeGroups.Clone(); }
		}

		internal int PollInterval
		{
			get { return Convert.ToInt32(pollingTimer.Interval); }
		}

		internal Hashtable ChangeNotificationTables
		{
			get { return (Hashtable)changeNotificationTables.Clone(); }
		}

		#endregion

        #region Change Event

		/// <summary>
		/// 
		/// </summary>
		public event ChangedEventHandler Changed;

		/// <summary>
		/// Raises the Changed event to notify clients of a change in the 
		/// database concerned, provided that the delegate exists.
		/// </summary>
		/// <param name="e"></param>
		private void OnChanged(ChangedEventArgs e)
		{
			// If delegate not null raise event
			if (Changed != null) 
			{
				Changed(this, e);
			}
		}

        #endregion

        #region Constructor
		/// <summary>
		/// The class constructor exeutes the Initialise method, which uses the Property Service
		/// to: initialise the class with the Polling Interval and list of groups defined in the
		/// Properties Database; perform initialisation checks on the nominated Databases;
		/// instantiate required DataChangeNotificationGroup classes (one per group).
		/// The Constructor then starts the pollingTimer, which fires an elapsed event every
		/// polling interval, which is handled by the Poll Event Handler. 
		/// </summary>
		public DataChangeNotification( )
		{
			//Instantiate a new timer
			pollingTimer = new Timer();

			// Execute Initialise method to perform initial data load
			Initialise();

			// Set up Polling via a timer using Polling Interval in properties DB
			// Polling interval in the database is in minutes, we need to convert to milliseconds
			pollingTimer.Interval = (double)pollingInterval * 1000;
			pollingTimer.AutoReset = false;

			// Initialisation code to connect to the Elapsed EventHandler
			pollingTimer.Elapsed += new ElapsedEventHandler(this.Poll);

			// Start timer
			pollingTimer.Start();
		}

        #endregion

        #region protected behaviours
		/// <summary>
		/// Initialises the class by using the Property Service to get:
		/// 1) The list of groups, and their associated tables
		/// 2) The polling interval (in milliseconds)
		/// 3) The Database and nominated tables for each group
		/// It then creates one group object per group that stores the Database
		/// and Tables of the group (later used for checking for changes).
		/// </summary>
		protected void Initialise()
		{
			string strAllGroups;
			string[] groupsArray = null;
			int noOfGroups = 0;

			// Read global properties
			string pollingIntervalString = Properties.Current["DataServices.DataNotification.PollingInterval"];
			if (pollingIntervalString == null || pollingIntervalString.Length == 0)
			{
				// This exception will be thrown if there is no/null PollingInterval property.
				Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					"PollingInterval not found in Properties service.") );

				//Throw TDException error
				throw new TDException("PollingInterval not found in Properties service.", 
					true, TDExceptionIdentifier.DSPollingIntervalNotFound);
			}

			try
			{
				// Cast and store PollingInterval from properties table in DB
				pollingInterval = Convert.ToInt32( pollingIntervalString );
				if ( TDTraceSwitch.TraceInfo )
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Database,
						TDTraceLevel.Info, "Polling Interval set to: " + pollingInterval + " minutes" ) );
				}
			}
			catch (FormatException e) 
			{
				// This exception will be thrown if there is no/null PollingInterval property.
				Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					"PollingInterval not a valid integer.", e) );

				//Throw TDException error
				throw new TDException("PollingInterval not a valid integer.", 
					e, true, TDExceptionIdentifier.DSPollingIntervalNotFound);
			}


			// Retrieve the list of groups as a string
			strAllGroups = Properties.Current["DataServices.DataNotification.Groups"];

			if (strAllGroups == null)
			{
				// Raise error
				// This exception will be thrown if there is no/null Groups property.
				Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					"Groups property is null or not found in Properties table.") );

				//Throw TDException error
				throw new TDException("Groups property is null or not found in Properties table.", 
					true, TDExceptionIdentifier.DSGroupsNotFound);

			}

			// If not null, split values & store in an array
			if (strAllGroups.Trim().Length > 0)
			{
				groupsArray = strAllGroups.Split(",".ToCharArray());

				ArrayList processedGroups = new ArrayList();

				//trim off whitespace
				foreach (string strGroup in groupsArray)
				{
					string groupCur = strGroup.Trim();
					if (groupCur.Length != 0 && !processedGroups.Contains(groupCur))
						processedGroups.Add(groupCur);
				}

                // If the number of processed (trimmed) groups is not equal 
				// to the original number of groups from the DB, log a warning
				if ((groupsArray.Length != processedGroups.Count) && TDTraceSwitch.TraceWarning)
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Database,
						TDTraceLevel.Warning , "Notification groups property contained zero-length or duplicate entries." ) );
				}

                // Copy the processed groups back into the groupsArray
				groupsArray = (string[])processedGroups.ToArray(typeof(string));
				noOfGroups = groupsArray.Length;
				if ( TDTraceSwitch.TraceVerbose ) 
				{
                    StringBuilder sb = new StringBuilder();

                    sb.Append("[");
                    
                    foreach (string group in groupsArray)
                    {
                        sb.Append(group);
                        sb.Append(",");
                    }
                    
                    sb.Append("]");

					Logger.Write( new OperationalEvent( TDEventCategory.Database,
						TDTraceLevel.Verbose, "Notification Groups: " + sb.ToString() ) );
				}
				
			}
			else
			{
				// If groups property is present but null, initialise as null for now and continue
				groupsArray = new string[0];
			}



			string currDatabaseName;
			string strTables;
			string[] currTables;
			SqlHelperDatabase currDatabase = new SqlHelperDatabase();
			DataChangeNotificationGroup currGroup;

			//Instance of SQLHelper to handle DB connection
			SqlHelper sql = new SqlHelper();

			this.changeNotificationTables = new Hashtable();
			this.changeGroups = new ArrayList(noOfGroups);

			foreach (string s in groupsArray)
			{
				if ( TDTraceSwitch.TraceInfo ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Database,
						TDTraceLevel.Info, "Gathering change data for group: " + s ) );
				}


				// Read properties to determine Database for the group
				currDatabaseName = Properties.Current["DataServices.DataNotification." + s + ".Database"];

				// Parse the database name to get a SqlHelperDatabase enum type
				currDatabase = ParseDatabase ( currDatabaseName );

				// Read properties to determine Tables for the group
				strTables = Properties.Current["DataServices.DataNotification." + s + ".Tables"];

				// If not null, split values & store in an array
				if (strTables.Trim().Length > 0)
				{
					currTables = strTables.Split(",".ToCharArray());

					ArrayList processedTables = new ArrayList();

					//trim off whitespace
					foreach (string strGroup in currTables)
					{
						// If the number of processed (trimmed) table is not equal 
						// to the original number of tables from the DB, log a warning
						string groupCur = strGroup.Trim();
						if (groupCur.Length != 0 && !processedTables.Contains(groupCur))
							processedTables.Add(groupCur);
					}

					if ((currTables.Length != processedTables.Count) && TDTraceSwitch.TraceWarning)
					{
						Logger.Write( new OperationalEvent( TDEventCategory.Database,
							TDTraceLevel.Warning , "Notification tables property contained zero-length or duplicate entries." ) );
					}

					// Copy the processed groups back into the groupsArray
					currTables = (string[])processedTables.ToArray(typeof(string));
					noOfGroups = currTables.Length;
					if ( TDTraceSwitch.TraceVerbose ) 
					{
                        StringBuilder sb = new StringBuilder();

                        sb.Append("[");

                        foreach (string table in currTables)
                        {
                            sb.Append(table);
                            sb.Append(",");
                        }

                        sb.Append("]");

						Logger.Write( new OperationalEvent( TDEventCategory.Database,
							TDTraceLevel.Verbose, "Notification Tables: " + sb.ToString() ) );
					}
				
				}
				else
				{
					// If groups property is present but null, initialise as null for now and continue
					currTables = new string[0];
				}

				currTables = strTables.Split(",".ToCharArray());


				// Create Group object and add to changeGroups list.
				currGroup = new DataChangeNotificationGroup(s, currDatabase, currTables);
				changeGroups.Add(currGroup);

				//If we have not already gathered the change notification info for this
				//data base
				if (!changeNotificationTables.ContainsKey(currGroup.DataBase ))
				{
					try
					{
						if ( TDTraceSwitch.TraceVerbose ) 
						{
							Logger.Write( new OperationalEvent( TDEventCategory.Database,
								TDTraceLevel.Verbose, "Opening database connection to: " + currGroup.DataBase.ToString() ) );
						}

						// Connect to DB and check for Stored Proc.
						sql.ConnOpen(currGroup.DataBase);


						if ( TDTraceSwitch.TraceVerbose ) 
						{
							Logger.Write( new OperationalEvent( TDEventCategory.Database,
								TDTraceLevel.Verbose, "Checking for GetChangeTable stored procedure in database: " + currGroup.DataBase.ToString() ) );
						}

						bool storedProcedureExists = (int)sql.GetScalar("SELECT Count(1) FROM sysobjects WHERE xtype = 'P' AND NAME = 'GetChangeTable'") == 1;

						if (storedProcedureExists)
						{
							changeNotificationTables.Add( currDatabase, changeNotificationData( sql, currDatabase ) );
						}
						else
						{					
							// Log exception
							Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, 
								"StoredProcedure not found in database " + s + ".") );

							// throw TDException error
							throw new TDException("StoredProcedure not found in database " + s + ".",
								true, TDExceptionIdentifier.DSStoredProcedureNotPresent);
						}
					}  
					catch ( TDException e)
					{
						// Log exception if it hasn't already been done
						if (!e.Logged)
							Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, e.Message));

						// Rethrow exception
						throw e;
					}
					catch ( Exception e ) 
					{
						// Log exception
						Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, 
							"Unexpected error encountered: " + e.Message ) );

						// throw TDException error
						throw new TDException("StoredProcedure not found in database " + s + ".",
							true, TDExceptionIdentifier.DSUnknownType);
					}
					finally 
					{
						// Close DB connection
						sql.ConnClose();
					}
				}
			}
		}


		/// <summary>
		/// This method first checks whether, for a given group, the ChangeNotification
		/// Table in the group's database has changed. It does this by getting a copy of
		/// the latest ChangeNotification table and comparing it against the original, as
		/// at the initialisation (or last time the Poll method executed).
		/// In the event of a change, the method will then determine whether this group
		/// is affected by the change, and sets the group's RaiseEvent flag. It then
		/// iterates through the groups, raising events for those affected, and restarts the timer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Poll(object sender, ElapsedEventArgs e)
		{
			try
			{
				if ( TDTraceSwitch.TraceInfo ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Database,
						TDTraceLevel.Info, "Polling for data updates to ChangeNotification tables" ) );
				}

				Hashtable updatedChangeNotificationTables = new Hashtable();

				foreach (object database in changeNotificationTables.Keys)
				{
					SqlHelperDatabase s;
					try
					{
						s = (SqlHelperDatabase)database;
					}
					catch (InvalidCastException badCast)
					{
						Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Warning, "An unexpected value was found in the Keys collection of the changeNotificationTables hashtable", badCast));
						continue;
					}

					if ( TDTraceSwitch.TraceInfo ) 
					{
						Logger.Write( new OperationalEvent( TDEventCategory.Database,
							TDTraceLevel.Info, "Polling for data changes to database: " + s.ToString() ) );
					}

					// Check DB for changes to ChangeNotification table
					SqlHelper sql = null;
					SqlDataReader dReader = null;

					try
					{
						sql = new SqlHelper();
                        //if ( TDTraceSwitch.TraceVerbose ) 
                        //{
                        //    Logger.Write( new OperationalEvent( TDEventCategory.Database,
                        //        TDTraceLevel.Verbose, "Opening connection to database: " + s.ToString() ) );
                        //}

						sql.ConnOpen(s);
						// Create a new hashtable that references the values in memory
						Hashtable existingChangeNotificationTables = (Hashtable)changeNotificationTables[s];

                        //if ( TDTraceSwitch.TraceVerbose ) 
                        //{
                        //    Logger.Write( new OperationalEvent( TDEventCategory.Database,
                        //        TDTraceLevel.Verbose, "Retreiving DataReader for database: " + s.ToString() ) );
                        //}
						// Store in temporary hashtable				
						Hashtable currentChangeNotificationTables = changeNotificationData( sql, s );

                        //if ( TDTraceSwitch.TraceVerbose ) 
                        //{
                        //    Logger.Write( new OperationalEvent( TDEventCategory.Database,
                        //        TDTraceLevel.Verbose, "Completed reading from DataReader for database: " + s.ToString() ) );
                        //}

						// Access data from changeNotificationTables for this DB
						// Iterate through hashtable
						foreach (string strTable in currentChangeNotificationTables.Keys)
						{
                            //if ( TDTraceSwitch.TraceVerbose ) 
                            //{
                            //    Logger.Write( new OperationalEvent( TDEventCategory.Database,
                            //        TDTraceLevel.Verbose, "Checking if any changes required for table: " + strTable ) );
                            //}

							// Compare Version against in-memory changeNotificationTables Version
							if (existingChangeNotificationTables[strTable] == null ||
								!currentChangeNotificationTables[strTable].Equals(existingChangeNotificationTables[strTable]))
							{
								if ( TDTraceSwitch.TraceVerbose ) 
								{
									Logger.Write( new OperationalEvent( TDEventCategory.Database,
										TDTraceLevel.Info, string.Format("Table[{0}] differs from last poll", strTable) ) );
								}
								// Version number of this table has changed
								// Determine which Group(s) contain these tables
								foreach (DataChangeNotificationGroup dcng in changeGroups)
								{
									dcng.RaiseEvent = dcng.RaiseEvent || dcng.IsAffected(s, strTable);
								}
							}
						}

                        if (TDTraceSwitch.TraceInfo)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Info, "Updating currect notification data cache"));
                        }
						//Completed comparision between old and new change data, setting
						//old data to current
						updatedChangeNotificationTables[s] = currentChangeNotificationTables;
					}
					catch (Exception unexpected)
					{
						// An error has occurred. Don't raise it further up, but log it as an error
						Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Error, "An error occurred whilst polling for data changes", unexpected));
					}
					finally
					{
						// Close database reader
						if ( dReader != null && !dReader.IsClosed )
						{
                            //if ( TDTraceSwitch.TraceVerbose ) 
                            //{
                            //    Logger.Write( new OperationalEvent( TDEventCategory.Database,
                            //        TDTraceLevel.Verbose, "Closing dataq reader") );
                            //}
							dReader.Close();
						}

						// Close database connection
						if ( sql != null && sql.ConnIsOpen ) 
						{
                            //if ( TDTraceSwitch.TraceVerbose ) 
                            //{
                            //    Logger.Write( new OperationalEvent( TDEventCategory.Database,
                            //        TDTraceLevel.Verbose, "Closing SqlHelper connection") );
                            //}
							sql.ConnClose();
						}
					}
				}

				changeNotificationTables = updatedChangeNotificationTables;
			

				//Loop through groups and raise events where RaiseEvent = true
				foreach (DataChangeNotificationGroup dcng in changeGroups)
				{
					if ( dcng.RaiseEvent )
					{
						if ( TDTraceSwitch.TraceInfo ) 
						{
							Logger.Write( new OperationalEvent( TDEventCategory.Database,
								TDTraceLevel.Info, "Raising event for group: " + dcng.GroupId ) );
						}

						//Raise Event for this group
						OnChanged(new ChangedEventArgs(dcng.GroupId));

						dcng.RaiseEvent = false;
					}
				}


				if ( TDTraceSwitch.TraceInfo ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Database,
						TDTraceLevel.Info, "Completed sending all required data change notification events" ) );
				}
			}
			catch (Exception unexpected)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, "An unexpected error occurred whilst polling the databases for change notifications", unexpected));
			}
			finally
			{
				if (!pollingTimer.AutoReset)
					pollingTimer.Start();
			}
				
		}

        #endregion

        #region helper methods
		/// <summary>
		/// Parse the database name and return its associated SqlHelperDatabase enum type
		/// </summary>
		/// <param name="databaseName">The name of the SqlHelperDatabase to return</param>
		/// <returns>The SqlHelperDatabase represented by databaseName</returns>
		private SqlHelperDatabase ParseDatabase( string databaseName )
		{
			SqlHelperDatabase db; 

			// Convert database name to enumerated value
			try
			{
				db = (SqlHelperDatabase)Enum.Parse(typeof(SqlHelperDatabase), databaseName );
			}
			catch(ArgumentNullException e)
			{
				// This exception will be thrown if currDatabaseName is null i.e. if properties haven't been set up!
				Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					"Parameter currDatabaseName is null. The Database name in the Properties table was null or not found.", e) );

				//Throw TDException error
				throw new TDException("Parameter currDatabaseName is null. The Database name in the Properties table was null or not found.", 
					e, true, TDExceptionIdentifier.DSDatabaseNamePropertyNotPresent);
			}
			catch(ArgumentException e)
			{
				// currDatabaseName is not null, but is not a vaild value.
				// Exception thrown if currDatabaseName is not null, but an unexpected value
				Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					"Parameter currDatabaseName is an unexpected value. The Database name in the Properties table does not match one of the specified enums.", e) );

				//Throw TDException error
				throw new TDException("Parameter currDatabaseName is an unexpected value. The Database name in the Properties table does not match one of the specified enums.", 
					e, true, TDExceptionIdentifier.DSDatabaseNamePropertyNotValid);
			}

			return db;
		}


		/// <summary>
		/// Returns a hashtable of the ChangeNotificationTable data for the supplied db
		/// </summary>
		/// <param name="sql">SqlHelper connection</param>
		/// <param name="db">The db to get notification data for</param>
		private Hashtable changeNotificationData( SqlHelper sql, SqlHelperDatabase db ) 
		{
			SqlDataReader dReader = null;
			Hashtable tempHash = null;

			try
			{
				// Execute Stored Procedure to get data from ChangeNotification table
				tempHash = new Hashtable();
				dReader = sql.GetReader( "GetChangeTable", new Hashtable());

				// Store data from DataReader in a hashtable (table, version)
				while ( dReader.Read() )
				{
					if ( TDTraceSwitch.TraceVerbose ) 
					{
						string foo = 
							string.Format( "For database[{0}] read table=[{1}] version=[{2}]",
							new object[] { db.ToString(), dReader.GetString(0), dReader.GetInt32(1) } );

						Logger.Write( new OperationalEvent( TDEventCategory.Database,
							TDTraceLevel.Verbose,
							foo ) );
					}

					tempHash.Add(dReader.GetString(0), dReader.GetInt32(1));
				}
			}

			finally
			{
				// Close the DataReader
				if ((dReader != null) && (!dReader.IsClosed))
					dReader.Close();
			}
			return tempHash;
		}
        #endregion
	}

    #endregion

}