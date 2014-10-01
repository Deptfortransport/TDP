// *********************************************** 
// NAME			: AirDataProvider.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 12/05/2004
// DESCRIPTION	: Provides airport/air region/operator data to clients
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirDataProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:18   mturner
//Initial revision.
//
//   Rev 1.16   Mar 16 2007 10:00:52   build
//Automatically merged from branch for stream4362
//
//   Rev 1.15.1.0   Mar 15 2007 18:09:14   dsawe
//added methods for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.15   Sep 03 2004 11:32:34   jgeorge
//Corrected locking
//
//   Rev 1.14   Aug 13 2004 10:33:22   CHosegood
//Added verbose logging
//
//   Rev 1.13   Jul 08 2004 14:11:32   jgeorge
//Actioned review comments
//
//   Rev 1.12   Jul 08 2004 13:18:26   jgeorge
//Updated with FxCop recommendations
//
//   Rev 1.11   Jul 08 2004 12:59:16   jgeorge
//Added additional verbose logging
//
//   Rev 1.10   Jun 21 2004 12:27:28   jgeorge
//Update to reduce the length of time for which the object is locked when loading data
//
//   Rev 1.9   Jun 17 2004 09:38:14   jgeorge
//Changed to use interface for DataChangeNotification service
//
//   Rev 1.8   Jun 16 2004 18:09:36   jgeorge
//Added integration with DataChangeNotification service
//
//   Rev 1.7   Jun 10 2004 11:46:34   jgeorge
//Added further logging
//
//   Rev 1.6   Jun 10 2004 10:52:04   CHosegood
//Added verbose logging
//
//   Rev 1.5   Jun 09 2004 17:02:16   jgeorge
//Work in progress
//
//   Rev 1.4   May 26 2004 10:42:44   jgeorge
//Work in progress
//
//   Rev 1.3   May 26 2004 08:53:22   jgeorge
//Work in progress
//
//   Rev 1.2   May 13 2004 11:31:34   jgeorge
//Updates for Naptans and commenting
//
//   Rev 1.1   May 13 2004 09:28:10   jgeorge
//Modified namespace to TransportDirect.UserPortal.AirDataProvider
//
//   Rev 1.0   May 12 2004 15:59:44   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.AirDataProvider
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Data.SqlClient;
	using System.IO;
	using System.Text;
	using System.Reflection;
	using System.Globalization;

	using TransportDirect.Common;
	using TransportDirect.Common.DatabaseInfrastructure;
	using TransportDirect.Common.Logging;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.ScriptRepository;
	using TransportDirect.UserPortal.DataServices;

	using Logger = System.Diagnostics.Trace;

	/// <summary>
	/// Structure used to define hashkey for accessing data cached in operators hashtable
	/// </summary>
	public struct OperatorKey
	{
		public string operatorcode, operatorname;

		public OperatorKey(string operatorcode, string operatorname)
		{
			this.operatorcode = operatorcode;
			this.operatorname = operatorname;
		}
	}

	/// <summary>
	/// Provides airport/air region/operator data to clients
	/// </summary>
	public sealed class AirDataProvider : IAirDataProvider
	{
		#region Public constants

		// Name to use when generating the JavaScript file
		public const string ScriptName = "AirDataDeclarations";

		#endregion

		#region Private variables

		private const string DataChangeNotificationGroup = "Air";

		private ArrayList airports;
		private ArrayList regions;
		private ArrayList routes;
		private ArrayList operators;
		private Hashtable regionAirports;
		private Hashtable airportRegions;
		private Hashtable routeMatrix;
		private Hashtable regionMatrix;
		private Hashtable airportOperators;
		private Hashtable routeOperators;

		/// <summary>
		/// Hastable storing operator codes used in local zonal services
		/// </summary>
		private Hashtable hashZonalOperatorCodes = new Hashtable();
		private Hashtable hashZonalOperatorCodesCurrentLoad = new Hashtable();

		

		private bool scriptGenerated = false;
		private bool receivingChangeNotifications = false;

		#endregion

		#region Constructor

		/// <summary>
		/// Data should be loaded when the item is first created
		/// </summary>
		public AirDataProvider()
		{
			LoadData();
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion

		#region Private methods

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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising AirDataProvider"));
					return false;
				}
				else
					throw e;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}

		/// <summary>
		/// Loads the data and performs pre processing
		/// </summary>
		private void LoadData()
		{
			try
			{
				// Temporary variables to hold the new data
				ArrayList newAirports;
				ArrayList newRegions;
				ArrayList newRoutes;
				ArrayList newOperators;
				Hashtable newRegionAirports;
				Hashtable newAirportRegions;
				Hashtable newRouteMatrix;
				Hashtable newRegionMatrix;
				Hashtable newAirportOperators;
				Hashtable newRouteOperators;

				// Initialise the arraylists and hashtables we need at this point.
				// The rest are left until later, as we can use data that is loaded
				// in the first stage to initialise them more appropriately.
				newAirports = new ArrayList();
				newRegions = new ArrayList();
				newRegionAirports = new Hashtable();
				newOperators = new ArrayList();
				newRoutes = new ArrayList();
				newRouteOperators = new Hashtable();

				// Initialise temporary variables
				AirRegion currRegion = null;
				ArrayList currRegionAirports = null;
				AirRoute currRoute = null;
				AirRoute tempRoute = null;
				ArrayList currRouteOperators = null;
				ArrayList temp = null;
				AirOperator tempOperator = null;

				Logger.Write( new OperationalEvent( TDEventCategory.Business,
					TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.AirDataProviderDB.ToString() ));
				// Initialise a SqlHelper and connect to the database.
				SqlHelper helper = new SqlHelper();
				helper.ConnOpen(SqlHelperDatabase.AirDataProviderDB);

				#region Load Local Zonal Airport Operators
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Local Zonal Airport Operators" ));
				}
				SqlDataReader reader = helper.GetReader("GetLocalZonalAirportOperators", new Hashtable());
				hashZonalOperatorCodesCurrentLoad.Clear();

				while (reader.Read())
					AddRecord(reader.GetString(0), reader.GetString(1), reader.GetString(2));
				
				reader.Close();
				hashZonalOperatorCodes = hashZonalOperatorCodesCurrentLoad;
				// Record the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Local Zonal Airport Operators completed" ));
				}

				#endregion

				#region Load airports
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading airports starting" ));
				}

				// Execute the GetAirports stored procedure. This returns a list of airports
				// containing code, name and no of terminals, in alphabetical order of name.
				// At this point, we are relying on the ArrayList maintaining the list internally
				// in order.
				reader = helper.GetReader("GetAirports", new Hashtable());
				while (reader.Read())
					newAirports.Add(new Airport(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
				reader.Close();

				// Record the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading airports completed" ));
				}
				#endregion

				#region Load regions
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading regions starting" ));
				}

				// Execute the GetAirports stored procedure. This returns a list of regions and airports,
				// meaning that each region is most likely duplicated. The results table is used to 
				// build both the ArrayList of regions and the region/airports hashtable
				reader = helper.GetReader("GetRegionAirports", new Hashtable());
				while (reader.Read())
				{
					if ( (currRegion == null) || (currRegion.Code != reader.GetInt32(0)) )
					{
						// New region
						currRegion = new AirRegion(reader.GetInt32(0), reader.GetString(1));
						newRegions.Add(currRegion);
						currRegionAirports = new ArrayList();
						newRegionAirports.Add(currRegion.Code, currRegionAirports);
					}
					currRegionAirports.Add(GetAirport(reader.GetString(2), newAirports));
				}
				reader.Close();

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading regions completed" ));
				}
				#endregion

				#region Load operators
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading operators starting" ));
				}
				reader = helper.GetReader("GetAirOperators", new Hashtable());
				while (reader.Read())
					newOperators.Add(new AirOperator(reader.GetString(0), reader.GetString(1)));
				reader.Close();

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading operators completed" ));
				}
				#endregion

				#region Load routes and route operators
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading routes starting" ));
				}
				reader = helper.GetReader("GetAirRoutes", new Hashtable());
				while (reader.Read())
				{
					tempRoute = new AirRoute(reader.GetString(0), reader.GetString(1));
					if ( (currRoute == null) || !(currRoute.Equals(tempRoute)) )
					{
						// New route
						currRoute = tempRoute;
						
						// Ensure routes are added only once (remember A - B is same route as B - A)
						if (!newRoutes.Contains(currRoute))
							newRoutes.Add(currRoute);

						if (!newRouteOperators.ContainsKey(currRoute))
							newRouteOperators.Add(currRoute, new ArrayList());

						currRouteOperators = (ArrayList)newRouteOperators[currRoute];
					}
					tempOperator = GetOperator(reader.GetString(2), newOperators);
					if (!currRouteOperators.Contains(tempOperator))
						currRouteOperators.Add(tempOperator);
				}
				reader.Close();

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading routes completed" ));
				}

				#endregion

				#region Initialise remaining hashtables

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Initialising remaining hashtables starting" ));
				}

				newAirportRegions = new Hashtable(newAirports.Count);
				newRouteMatrix = new Hashtable(newAirports.Count);
				newRegionMatrix = new Hashtable(newAirports.Count);
				newAirportOperators = new Hashtable(newAirports.Count);

				foreach (Airport a in newAirports)
				{
					newAirportRegions.Add(a.IATACode, new ArrayList());
					newRouteMatrix.Add(a.IATACode, new ArrayList());
					newAirportOperators.Add(a.IATACode, new ArrayList());
				}

				foreach (AirRegion r in newRegions)
					newRegionMatrix.Add(r.Code, new ArrayList());

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Initialising remaining hashtables completed" ));
				}

				#endregion

				#region Populate airportRegions hashtable

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populate airportRegions hashtable starting" ));
				}

				foreach (AirRegion r in newRegions)
					foreach (Airport a in (ArrayList)newRegionAirports[r.Code])
						((ArrayList)newAirportRegions[a.IATACode]).Add(r);

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populate airportRegions hashtable completed" ));
				}

				#endregion

				#region Populate routeMatrix and regionMatrix hashtables

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populate routeMatrix and regionMatrix hashtables starting" ));
				}

				// routeMatrix and regionMatrix hashtables
				ArrayList tempOrigin, tempDestination;
				ArrayList matrixEntryCurr;
				foreach (AirRoute r in newRoutes)
				{
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.Write( new OperationalEvent( TDEventCategory.Business,
                            TDTraceLevel.Verbose, "STARTING AirRoute origin[" + r.OriginAirport + "] destination[" + r.DestinationAirport + "]" ));

                        Logger.Write( new OperationalEvent( TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Origin Airport found [" + (GetAirport(r.OriginAirport, newAirports) != null ) +"]" ) );

                        Logger.Write( new OperationalEvent( TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Destination Airport found [" + (GetAirport(r.DestinationAirport, newAirports) != null ) +"]" ) );
                    }

					((ArrayList)newRouteMatrix[r.OriginAirport]).Add(GetAirport(r.DestinationAirport, newAirports));
					((ArrayList)newRouteMatrix[r.DestinationAirport]).Add(GetAirport(r.OriginAirport, newAirports));
					
					tempOrigin = (ArrayList)newAirportRegions[r.OriginAirport];
					tempDestination = (ArrayList)newAirportRegions[r.DestinationAirport];
					foreach (AirRegion r1 in tempOrigin)
						foreach (AirRegion r2 in tempDestination)
						{
							matrixEntryCurr = (ArrayList)newRegionMatrix[r1.Code];
							if (!matrixEntryCurr.Contains(r2))
								matrixEntryCurr.Add(r2);
							matrixEntryCurr = (ArrayList)newRegionMatrix[r2.Code];
							if (!matrixEntryCurr.Contains(r1))
								matrixEntryCurr.Add(r1);
						}
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.Write( new OperationalEvent( TDEventCategory.Business,
                            TDTraceLevel.Verbose, "COMPLETED AirRoute origin[" + r.OriginAirport + "] destination[" + r.DestinationAirport + "]" ));
                    }
				}					

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populate routeMatrix and regionMatrix hashtables completed" ));
				}

				#endregion

				#region Populate airportOperators hashtable

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populate airportOperators hashtable starting" ));
				}

				foreach (AirRoute r in newRoutes)
					foreach (AirOperator o in (ArrayList)newRouteOperators[r])
					{
						temp = (ArrayList)newAirportOperators[r.OriginAirport];
						if (!temp.Contains(o))
							temp.Add(o);

						temp = (ArrayList)newAirportOperators[r.DestinationAirport];
						if (!temp.Contains(o))
							temp.Add(o);
					}

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Populate airportOperators hashtable completed" ));
				}

				#endregion			
				
				if(TDTraceSwitch.TraceVerbose)
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "All data loaded"));


				//				The following line calls the DumpDataToFile function, which is commented out below.
				//				This will write the contents of the data structures to file for verification.
				//				DumpDataToFile();

				lock(this)
				{
					// Copy the new values back into the instance variables.
					// It is assumed that this code will never cause errors. If it does, there is
					// the potential for the AirDataProvider to be internally inconsistent.
					this.airports = newAirports;
					this.regions = newRegions;
					this.routes = newRoutes;
					this.operators = newOperators;
					this.regionAirports = newRegionAirports;
					this.airportRegions = newAirportRegions;
					this.routeMatrix = newRouteMatrix;
					this.regionMatrix = newRegionMatrix;
					this.airportOperators = newAirportOperators;
					this.routeOperators = newRouteOperators;

					if(TDTraceSwitch.TraceVerbose)
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Updated Air Data Provider"));

					// Try and create the script. This is done separately as the GenerateJavascript
					// method needs to use some of the methods in IAirDataProvider, which operate on
					// the module level data.
					this.scriptGenerated = GenerateJavascript();
				}
			}
			catch (Exception e)
			{
				// Catching the base Exception class because we don't want any possibility
				// of this raising any errors outside of the class in case it causes the
				// application to fall over. As long as the exception doesn't occur in 
				// the final block of code, which copies the new data into the module-level
				// hashtables and arraylists, the object will still be internally consistant,
				// although the data will be inconsistant with that stored in the database.
				// Once exception to this: if this is the first time LoadData has been run,
				// (if it is, this.airports == null will be true), the exception should be
				// raised.
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the AirDataProvider data.", e));
				if (this.airports == null)
				{
					throw e;
				}
			}

		}

		/// <summary>
		/// Adds Local Zonal operator codes to the cache
		/// </summary>
		/// <param name="naptan">The naptan</param>
		/// <param name="externalLinkId">The unique identifier for the corresponding external link</param>
		private void AddRecord(string AirportNaptan, string OperatorCode, string OperatorName)
		{
			ArrayList arrayOperatorCodes;
			OperatorKey tempOperatorKey = new OperatorKey(OperatorCode, OperatorName);
			//If the hashtable currently contains external links for the current naptan, then
			//add the link, else insert a new entry into the hashtable for the current naptan
			if(hashZonalOperatorCodesCurrentLoad.ContainsKey(AirportNaptan))
			{
				arrayOperatorCodes = (ArrayList)hashZonalOperatorCodesCurrentLoad[AirportNaptan];
				arrayOperatorCodes.Add(tempOperatorKey);
			}
			else
			{
				arrayOperatorCodes = new ArrayList();
				arrayOperatorCodes.Add(tempOperatorKey);
				hashZonalOperatorCodesCurrentLoad.Add(AirportNaptan, arrayOperatorCodes);
			}
		}

		/// <summary>
		/// Generates a javascript file containing the declarations for client side code.
		/// Only works if the script repository service is present in TDServiceDiscovery.
		/// </summary>
		/// <returns></returns>
		private bool GenerateJavascript()
		{
			ScriptRepository scriptRep;
			try
			{
				scriptRep = (ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			}
			catch
			{
				// Failed to get a reference to script repository, so don't create the file
				if ( TDTraceSwitch.TraceWarning ) 
					Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Warning, "Script repository not present in TDServiceDiscovery, so JavaScript file will not be created." ));
				return false;
			}

			try
			{
				// A StringWriter is used to build the file. This maintains the text in an underlying
				// StringBuilder, which is then passed to the script repository to create the file.
				StringWriter js = new StringWriter(CultureInfo.InvariantCulture);
				DateTime now = DateTime.Now;
				js.WriteLine(String.Format("// {0} file generated at {1}/{2}/{3} {4}:{5}.{6}", ScriptName, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second));

				js.WriteLine("var airports = new Array({0});", ArrayListJoin(airports, typeof(Airport), "IATACode", "\"{0}\"", ", "));
				js.WriteLine("var regions = new Array({0});", ArrayListJoin(regions, typeof(AirRegion), "Code", "\"{0}\"", ", "));
				js.WriteLine("var operators = new Array({0})", ArrayListJoin(operators, typeof(AirOperator), "IATACode", "\"{0}\"", ", "));
				js.WriteLine("var routes = new Array({0})", ArrayListJoin(routes, typeof(AirRoute), "CompoundName", "\"{0}\"", ", "));
				js.WriteLine("var airportNames;");
				js.WriteLine("var regionNames;");
				js.WriteLine("var regionAirports;");
				js.WriteLine("var routeTable;");
				js.WriteLine("var routeOperators;");

				js.WriteLine("populateAirDataArrays();");
				js.WriteLine();
				
				js.WriteLine("function populateAirDataArrays()");
				js.WriteLine("{");

				js.WriteLine("airportNames = new Array();");
				js.WriteLine("regionNames = new Array();");
				js.WriteLine("regionAirports = new Array();");
				js.WriteLine("routeOperators = new Array();");
				js.WriteLine("routeTable = new Array();");

				foreach (AirRegion currRegion in regions)
				{
					js.WriteLine("regionNames[\"{0}\"] = \"{1}\";", currRegion.Code, currRegion.Name);
					js.WriteLine("regionAirports[\"{0}\"] = new Array({1});", currRegion.Code, ArrayListJoin(GetRegionAirports(currRegion.Code), typeof(Airport), "IATACode", "\"{0}\"", ", "));
				}

				foreach (Airport currAirport in airports)
				{
					js.WriteLine("airportNames[\"{0}\"] = \"{1}\";", currAirport.IATACode, currAirport.Name);
					js.WriteLine("routeTable[\"{0}\"] = new Array({1});", currAirport.IATACode, ArrayListJoin(GetValidDestinationAirports( new Airport[] { currAirport } ), typeof(Airport), "IATACode", "\"{0}\"", ", "));
				}

				foreach (AirRoute currRoute in routes)
					js.WriteLine("routeOperators[\"{0}\"] = new Array({1});", currRoute.CompoundName, ArrayListJoin((ArrayList)routeOperators[currRoute], typeof(AirOperator), "IATACode", "\"{0}\"", ", "));

				js.WriteLine("}");
			
				scriptRep.AddTempScript(ScriptName, "W3C_STYLE", js.GetStringBuilder());

				js.Close();

				if(TDTraceSwitch.TraceVerbose)
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Temporary JavaScript file generated"));

				return true;
			}
			catch (Exception e)
			{
				if ( TDTraceSwitch.TraceWarning ) 
					Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Warning, "Unexpected error occurred.", e ));
				return false;
			}
		}

		/// <summary>
		/// Helper function.
		/// Given an arraylist containing objects of a specific type, builds a string containing
		/// the value of the given property of each object, formatted and concatenated into a
		/// single string - similar the String.Join function.
		/// </summary>
		/// <param name="items">The ArrayList of items to be joined. All items in the list must be of the same type.</param>
		/// <param name="objectType">A Type object containing the type of the objects in the ArrayList</param>
		/// <param name="propertyName">Name of the property whose value will be used in the output string</param>
		/// <param name="formatString">Format string that will be applied to the property.</param>
		/// <param name="separator">String to insert between each value</param>
		/// <returns></returns>
		private string ArrayListJoin(ArrayList items, Type objectType, string propertyName, string formatString, string separator)
		{
			PropertyInfo p = objectType.GetProperty(propertyName);
			StringBuilder s = new StringBuilder();
			bool isFirst = true;
			foreach (object o in items)
			{
				if (!isFirst)
					s.Append(separator);
				else
					isFirst = false;

				s.AppendFormat(formatString, p.GetValue(o, null).ToString());
			}
			return s.ToString();
		}

		/// <summary>
		/// Given an array of ArrayLists, returns an ArrayList contain all the elements in 
		/// all the ArrayLists. No duplicate items are added.
		/// </summary>
		/// <param name="arrays"></param>
		/// <returns></returns>
		private ArrayList ArrayListUnion(ArrayList[] arrays)
		{
			ArrayList results;
			if ((arrays.Length == 0) || (arrays[0] == null))
				results = new ArrayList();
			else
				results = arrays[0];

			for (int index = 1; index < arrays.Length; index++)
				if (arrays[index] != null)
					foreach (object o in arrays[index])
						if (!results.Contains(o))
							results.Add(o);

			return results;
		}

		/// <summary>
		/// Converts an array of airports into a string array of their
		/// IATACode properties.
		/// </summary>
		/// <param name="airports"></param>
		/// <returns></returns>
		private string[] AirportArrayToIATACodeArray(Airport[] airports)
		{
			string[] results = new string[airports.Length];
			for (int index = 0; index < airports.Length; index++)
				results[index] = airports[index].IATACode;
			return results;
		}

		/// <summary>
		/// Builds an ArrayList containing route objects representing all potential routes between
		/// the origin and destination airports. These can then be used to check against the list
		/// of valid routes.
		/// </summary>
		/// <param name="originAirports"></param>
		/// <param name="destinationAirports"></param>
		/// <returns></returns>
		private ArrayList BuildPotentialRouteList(Airport[] originAirports, Airport[] destinationAirports)
		{
			ArrayList results = new ArrayList(originAirports.Length * destinationAirports.Length);
			AirRoute route;
			foreach (Airport origin in originAirports)
				foreach (Airport destination in destinationAirports)
				{
					route = new AirRoute(origin.IATACode, destination.IATACode);
					if (!results.Contains(route))
						results.Add(route);
				}
			return results;
		}

		/// <summary>
		/// Builds an ArrayList containing route objects representing all potential routes between
		/// the origin and destination airports. These can then be used to check against the list
		/// of valid routes.
		/// </summary>
		/// <param name="originAirportCodes">Airport IATA Codes</param>
		/// <param name="destinationAirportCodes">Airport IATA Codes</param>
		/// <returns></returns>
		private ArrayList BuildPotentialRouteList(string[] originAirportCodes, string[] destinationAirportCodes)
		{
			ArrayList results = new ArrayList(originAirportCodes.Length * destinationAirportCodes.Length);
			AirRoute route;
			foreach (string origin in originAirportCodes)
				foreach (string destination in destinationAirportCodes)
				{
					route = new AirRoute(origin, destination);
					if (!results.Contains(route))
						results.Add(route);
				}
			return results;
		}

		//		The following commented block of code contains a routine to write the contents of all
		//		the internal data structures to a file. This can be used to verify that the data 
		//		structures correctly represent what is stored in the database.
		//		There is a corresponding commented line in LoadData that calls this routine.
		//		/// <summary>
		//		/// Writes the contents of all the hashtables to a file C:\AirDataProviderDump.txt
		//		/// </summary>
		//		private void DumpDataToFile()
		//		{
		//			StringWriter s = new StringWriter();
		//			s.WriteLine("Beginning write of data at " + DateTime.Now.ToString());
		//
		//			s.WriteLine("Airports ArrayList");
		//			foreach (Airport a in airports)
		//				s.WriteLine("    IATA: {0}; Name: {1}; Terminals: {2}; Naptans: {3}", a.IATACode, a.Name, a.NoOfTerminals, String.Join(", ", a.Naptans));
		//			s.WriteLine();
		//
		//			s.WriteLine("Regions ArrayList");
		//			foreach (AirRegion r in regions)
		//				s.WriteLine("    Code: {0}; Name: {1}", r.Code, r.Name);
		//			s.WriteLine();
		//
		//			s.WriteLine("Routes ArrayList");
		//			foreach (AirRoute r in routes)
		//				s.WriteLine("    Origin Code: {0}; Destination Code: {1}; Compound Name: {2}", r.OriginAirport, r.DestinationAirport, r.CompoundName);
		//			s.WriteLine();
		//
		//			s.WriteLine("Operators ArrayList");
		//			foreach (AirOperator o in operators)
		//				s.WriteLine("    IATA: {0}; Name: {1}", o.IATACode, o.Name);
		//			s.WriteLine();
		//
		//			ICollection keysCurr;
		//
		//			s.WriteLine("regionAirports Hashtable");
		//			keysCurr = regionAirports.Keys;
		//			foreach (int k in keysCurr)
		//			{
		//				s.WriteLine("    Region: {0}", k);
		//				foreach (Airport a in (ArrayList)regionAirports[k])
		//					s.WriteLine("        IATA: {0}; Name: {1}; Terminals: {2}; Naptans: {3}", a.IATACode, a.Name, a.NoOfTerminals, String.Join(", ", a.Naptans));
		//			}
		//			s.WriteLine();
		//
		//			s.WriteLine("airportRegions Hashtable");
		//			keysCurr = airportRegions.Keys;
		//			foreach (string k in keysCurr)
		//			{
		//				s.WriteLine("    Airport: {0}", k);
		//				foreach (AirRegion r in (ArrayList)airportRegions[k])
		//					s.WriteLine("        Code: {0}; Name: {1}", r.Code, r.Name);
		//			}
		//			s.WriteLine();
		//
		//			s.WriteLine("routeMatrix Hashtable");
		//			keysCurr = routeMatrix.Keys;
		//			foreach (string k in keysCurr)
		//			{
		//				s.WriteLine("    Airport: {0}", k);
		//				foreach (Airport a in (ArrayList)routeMatrix[k])
		//					s.WriteLine("        IATA: {0}; Name: {1}; Terminals: {2}; Naptans: {3}", a.IATACode, a.Name, a.NoOfTerminals, String.Join(", ", a.Naptans));
		//			}
		//			s.WriteLine();
		//
		//			s.WriteLine("regionMatrix Hashtable");
		//			keysCurr = regionMatrix.Keys;
		//			foreach (int k in keysCurr)
		//			{
		//				s.WriteLine("    Region: {0}", k);
		//				foreach (AirRegion r in (ArrayList)regionMatrix[k])
		//					s.WriteLine("        Code: {0}; Name: {1}", r.Code, r.Name);
		//			}
		//			s.WriteLine();
		//
		//			s.WriteLine("airportOperators Hashtable");
		//			keysCurr = airportOperators.Keys;
		//			foreach (string k in keysCurr)
		//			{
		//				s.WriteLine("    Airport: {0}", k);
		//				if (airportOperators[k] == null)
		//					s.WriteLine("        No operators");
		//				else
		//					foreach (AirOperator o in (ArrayList)airportOperators[k])
		//						s.WriteLine("        IATA: {0}; Name: {1}", o.IATACode, o.Name);
		//			}
		//			s.WriteLine();
		//
		//			s.WriteLine("routeOperators Hashtable");
		//			keysCurr = routeOperators.Keys;
		//			foreach (AirRoute k in keysCurr)
		//			{
		//				s.WriteLine("    Compound Route: {0}", k.CompoundName);
		//				if (routeOperators[k] == null)
		//					s.WriteLine("        No operators");
		//				else
		//					foreach (AirOperator o in (ArrayList)routeOperators[k])
		//						s.WriteLine("        IATA: {0}; Name: {1}", o.IATACode, o.Name);
		//			}
		//			s.WriteLine();
		//
		//
		//			using (StreamWriter file = new StreamWriter(@"C:\AirDataProviderDump.txt", false))
		//			{
		//				file.Write(s.GetStringBuilder().ToString());
		//			}
		//
		//		}

		#endregion

		#region Event handler

		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == DataChangeNotificationGroup)
				LoadData();
		}

		#endregion

		#region Implementation of IAirDataProvider

		/// <summary>
		/// Returns all Airports
		/// </summary>
		/// <returns></returns>
		public ArrayList GetAirports()
		{
			return (ArrayList)airports.Clone();
		}

		/// <summary>
		/// Returns all AirRegions
		/// </summary>
		/// <returns></returns>
		public ArrayList GetRegions()
		{
			return (ArrayList)regions.Clone();
		}

		/// <summary>
		/// Returns all AirRoutes
		/// </summary>
		/// <returns></returns>
		public ArrayList GetRoutes()
		{
			return (ArrayList)routes.Clone();
		}

		/// <summary>
		/// Returns all AirOperators
		/// </summary>
		/// <returns></returns>
		public ArrayList GetOperators()
		{
			return (ArrayList)operators.Clone();
		}

		/// <summary>
		/// Retrieve an airport object given its IATA code.
		/// Null is returned if the airport is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public Airport GetAirport(string airportCode)
		{
			return GetAirport(airportCode, this.airports);
		}

		/// <summary>
		/// Retrieve an airport object from the supplied Arraylist 
		/// given its IATA code.
		/// Null is returned if the airport is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		private Airport GetAirport(string airportCode, ArrayList airportsToSearch)
		{
			foreach (Airport a in airportsToSearch)
				if (a.IATACode == airportCode)
					return a;

			return null;
		}

		/// <summary>
		/// Retrieve a region object given its code.
		/// Null is returned if the region is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public AirRegion GetRegion(int regionCode)
		{
			return GetRegion(regionCode, this.regions);
		}

		/// <summary>
		/// Retrieve a region object from the supplied ArrayList
		/// given its region code.
		/// Null is returned if the region is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		private AirRegion GetRegion(int regionCode, ArrayList regionsToSearch)
		{
			foreach (AirRegion r in regionsToSearch)
				if (r.Code == regionCode)
					return r;

			return null;
		}

		/// <summary>
		/// Method used to get Airport Operator Codes for Local Zonal services
		/// </summary>
		/// <param name="AirportNaptan">Naptan Code for Airport</param>
		/// <returns></returns>
		public ArrayList GetLocalZonalAirportOperators(string AirportNaptan)
		{
			if (!hashZonalOperatorCodes.ContainsKey(AirportNaptan)) 
				return null;
			else
				return (ArrayList)hashZonalOperatorCodes[AirportNaptan];
		}

		/// <summary>
		/// Retrieve an airoperator object given its IATA code.
		/// Null is returned if the operator is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public AirOperator GetOperator(string operatorCode)
		{
			return GetOperator(operatorCode, this.operators);
		}

		/// <summary>
		/// Retrieve an airoperator object from the supplied ArrayList
		/// given its IATA code.
		/// Null is returned if the operator is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		private AirOperator GetOperator(string operatorCode, ArrayList operatorsToSearch)
		{
			foreach (AirOperator o in operatorsToSearch)
				if (o.IATACode == operatorCode)
					return o;

			return null;
		}

		/// <summary>
		/// Retrieve all airports in a given region
		/// </summary>
		/// <param name="regionCode"></param>
		/// <returns></returns>
		public ArrayList GetRegionAirports(int regionCode)
		{
			return (ArrayList)((ArrayList)regionAirports[regionCode]).Clone();
		}

		/// <summary>
		/// Retrieve all regions for an airport
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public ArrayList GetAirportRegions(string airportCode)
		{
			return (ArrayList)((ArrayList)airportRegions[airportCode]).Clone();
		}

		/// <summary>
		/// Retrieve airports from a list of naptans 
		/// </summary>
		/// <param name="naptans"></param>
		/// <returns></returns>
		public ArrayList GetAirportsFromNaptans(string[] naptans)
		{
			Airport current;
			ArrayList results = new ArrayList();
			foreach (string s in naptans)
			{
				current = GetAirportFromNaptan(s);
				if ( (current != null) && (!results.Contains(current)) )
					results.Add(current);
			}
			return results;
		}

		/// <summary>
		/// Retrieve a single airport from a naptan
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public Airport GetAirportFromNaptan(string naptan)
		{
			// Verify that the naptan is the expected length
			if (naptan.Length < Airport.NaptanPrefix.Length + 3)
				return null;
			else
				return GetAirport(naptan.Substring(Airport.NaptanPrefix.Length, 3));
		}

		/* The following methods are for retrieving data for and validating direct
		 * flights only. None of the below apply to indirect flights, as the routes
		 * are worked out by the CJP.
		 */

		/// <summary>
		///  Retrieve valid destination airports from given origin region
		/// </summary>
		/// <param name="originRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationAirports(AirRegion originRegion)
		{
			ArrayList airports = GetRegionAirports(originRegion.Code);
			return GetValidDestinationAirports( (Airport[]) airports.ToArray(typeof(Airport)) );
		}

		/// <summary>
		///  Retrieve valid destination airports from given origin airports
		/// </summary>
		/// <param name="originRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationAirports(Airport[] originAirport)
		{
			ArrayList[] currDests = new ArrayList[originAirport.Length];
			for (int index = 0; index < originAirport.Length; index++)
				currDests[index] = (ArrayList)routeMatrix[originAirport[index].IATACode];

			return ArrayListUnion(currDests);
		}

		/// <summary>
		/// Retrieve valid destination regions from given origin region
		/// </summary>
		/// <param name="originRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationRegions(AirRegion originRegion)
		{
			ArrayList airports = GetRegionAirports(originRegion.Code);
			return GetValidDestinationRegions( (Airport[]) airports.ToArray(typeof(Airport)) );
		}

		/// <summary>
		/// Retrieve valid destination regions from given origin airports
		/// </summary>
		/// <param name="originAirport"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationRegions(Airport[] originAirport)
		{
			ArrayList validDestinationAirports = GetValidDestinationAirports(originAirport);
			ArrayList[] currDestRegions = new ArrayList[validDestinationAirports.Count];
			for (int index = 0; index < validDestinationAirports.Count; index++)
				currDestRegions[index] = (ArrayList)airportRegions[((Airport)validDestinationAirports[index]).IATACode];
			return ArrayListUnion(currDestRegions);
		}


		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginAirports(AirRegion destinationRegion)
		{
			return GetValidDestinationAirports(destinationRegion);
		}

		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationAirports"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginAirports(Airport[] destinationAirports)
		{
			return GetValidDestinationAirports(destinationAirports);
		}

		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginRegions(AirRegion destinationRegion)
		{
			return GetValidDestinationRegions(destinationRegion);
		}

		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationAirports"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginRegions(Airport[] destinationAirports)
		{
			return GetValidDestinationRegions(destinationAirports);
		}


		/// <summary>
		/// Retrieve valid operators for an air region.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public ArrayList GetAirportOperators(AirRegion region)
		{
			return GetAirportOperators( (Airport[])((ArrayList)regionAirports[region.Code]).ToArray(typeof(Airport)) );
		}

		/// <summary>
		/// Retrieve valid operators for a list of airports.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public ArrayList GetAirportOperators(Airport[] airport)
		{
			ArrayList[] results = new ArrayList[airport.Length];
			for (int index = 0; index < airport.Length; index++)
				results[index] = (ArrayList)airportOperators[airport[index].IATACode];

			return ArrayListUnion(results);
		}

		/// <summary>
		/// Retrieve valid operators for routes between given origin(s) and destination(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetRouteOperators(Airport[] originAirportCodes, Airport[] destinationAirportCodes)
		{
			ArrayList validRoutes = GetValidRoutes(originAirportCodes, destinationAirportCodes);
			return GetRouteOperators( (AirRoute[])validRoutes.ToArray(typeof(AirRoute)) );
		}
		
		/// <summary>
		/// Retrieve valid operators for routes between given origin(s) and destination(s)
		/// </summary>
		/// <param name="routes"></param>
		/// <returns></returns>
		public ArrayList GetRouteOperators(AirRoute[] routes)
		{
			ArrayList[] results = new ArrayList[routes.Length];
			for (int index = 0; index < routes.Length; index++)
				results[index] = (ArrayList)routeOperators[routes[index]];
			return ArrayListUnion(results);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(Airport[] originAirports, Airport[] destinationAirports)
		{
			// Build a list of all possible routes
			ArrayList possibleRoutes = BuildPotentialRouteList(originAirports, destinationAirports);
			ArrayList validRoutes = new ArrayList(possibleRoutes.Count);
			foreach (AirRoute r in possibleRoutes)
				if (!validRoutes.Contains(r) &&  routes.Contains(r))
					validRoutes.Add(r);

			return validRoutes;
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(AirRegion originRegion, AirRegion destinationRegion)
		{
			return GetValidRoutes( 
				(Airport[])GetRegionAirports(originRegion.Code).ToArray(typeof(Airport)), 
				(Airport[])GetRegionAirports(destinationRegion.Code).ToArray(typeof(Airport))
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(AirRegion originRegion, Airport[] destinationAirports)
		{
			return GetValidRoutes( 
				(Airport[])GetRegionAirports(originRegion.Code).ToArray(typeof(Airport)), 
				destinationAirports
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(Airport[] originAirports, AirRegion destinationRegion)
		{
			return GetValidRoutes( 
				originAirports, 
				(Airport[])GetRegionAirports(destinationRegion.Code).ToArray(typeof(Airport))
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public bool ValidRouteExists(string[] originAirportCodes, string[] destinationAirportCodes)
		{
			// Build a list of all possible routes
			ArrayList possibleRoutes = BuildPotentialRouteList(originAirportCodes, destinationAirportCodes);
			foreach (AirRoute r in possibleRoutes)
				if (routes.Contains(r))
					return true;

			return false;
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originRegionCode"></param>
		/// <param name="destinationRegionCode"></param>
		/// <returns></returns>
		public bool ValidRouteExists(int originRegionCode, int destinationRegionCode)
		{
			return ValidRouteExists( 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(originRegionCode).ToArray(typeof(Airport))), 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(destinationRegionCode).ToArray(typeof(Airport)))
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originRegionCode"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public bool ValidRouteExists(int originRegionCode, string[] destinationAirportCodes)
		{
			return ValidRouteExists( 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(originRegionCode).ToArray(typeof(Airport))), 
				destinationAirportCodes
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportsCode"></param>
		/// <param name="destinationRegionCode"></param>
		/// <returns></returns>
		public bool ValidRouteExists(string[] originAirportsCode, int destinationRegionCode)
		{
			return ValidRouteExists( 
				originAirportsCode, 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(destinationRegionCode).ToArray(typeof(Airport)))
				);
		}

		/// <summary>
		/// Returns true if a JavaScript declarations file has been generated for the current
		/// data set.
		/// </summary>
		public bool ScriptGenerated
		{
			get { return scriptGenerated; }
		}

		#endregion

		#region Additional property

		/// <summary>
		/// Returns true if the AirDataProvider is registered to recieve notifications
		/// from the DataChangeNotification service.
		/// </summary>
		public bool ReceivingChangeNotifications
		{
			get { return receivingChangeNotifications; }
		}

		#endregion
	}
}
