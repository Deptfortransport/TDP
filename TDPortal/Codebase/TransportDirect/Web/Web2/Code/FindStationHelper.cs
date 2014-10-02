// *********************************************** 
// NAME                 : FindStationResultsHelper.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/05/2004 
// DESCRIPTION  : Class provinding methods used by FindStations UI
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/FindStationHelper.cs-arc  $ 
//
//   Rev 1.4   Nov 24 2009 11:04:48   mmodi
//Corrected to pass in coordinate of naptan
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 20 2009 09:26:08   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:18:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:10:50   mturner
//Initial revision.
//
//   Rev 1.33   Oct 06 2006 16:26:30   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.32.1.0   Aug 04 2006 10:19:08   esevern
//added FindCarPark mode to GetStationTypeString
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.32   Jun 02 2006 15:16:10   jfrank
//Fix for vantive 3839955.
//Stops the error message incorrectly being displayed when the user doesn't request a rail journey, using the find nearest trunk planner.
//Resolution for 4102: Find Nearest Trunk Planner - error when Train mode not selected
//
//   Rev 1.31   Feb 23 2006 19:15:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.30.1.0   Jan 10 2006 15:53:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.30   Nov 03 2004 12:53:54   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.29   Nov 01 2004 18:02:44   passuied
//Redirection changes. If FindStation used redirect to FindTrunk
//
//   Rev 1.28   Oct 08 2004 14:58:08   jgeorge
//Changed default selection of stations. 
//Resolution for 1693: Changes to 'tick all' options in Find nearest station/airport results
//
//   Rev 1.27   Oct 07 2004 11:48:40   esevern
//removed commented out text, added string constants
//
//   Rev 1.26   Oct 04 2004 12:27:30   jgeorge
//Added constant for Station/Airport resource. Change made by J McAllister.
//
//   Rev 1.25   Sep 23 2004 18:27:18   passuied
//stored the enum.ToString value of the transport instead of the resource value
//Resolution for 1633: Find Nearest station: The transport column doesn't get translated when switching between eng/welsh
//
//   Rev 1.24   Sep 04 2004 18:16:44   passuied
//Added new sortable and hiddable Transport column
//Resolution for 1459: Find a Sorting order and arrows not working as requested by the DfT
//
//   Rev 1.23   Aug 20 2004 15:00:30   passuied
//Moved GetLocationStationTypes to LocationService for non duplication of code.
//Resolution for 1415: FindNearest Station/Airport Optimisation
//
//   Rev 1.22   Aug 20 2004 10:45:42   passuied
//Call to appropriate FindInputAdapter InitJourneyParameters to perform initialisation for page which won't be performed inside the page.
//
//De activate JourneyResult when leaving StationAirport functionality.
//
//Fixed bug in RemoveNotInTypes (was deleting all naptans!)
//Resolution for 1347: Cannot use Find A Station/Airport to plan a trunk journey
//Resolution for 1348: Not possible to plan Find A Train journey using Find A Station/Airport
//
//   Rev 1.21   Aug 19 2004 18:17:12   RHopkins
//IR1150 To allow use of browser "Back" button changes have been made to the methods that add found stations to the results set.
//
//   Rev 1.20   Aug 18 2004 14:19:20   passuied
//Use of non duplicated code to Get transitionevent from FindAMode mode
//Resolution for 1373: Duplication of code for redirecting to appropriate FindA page
//
//   Rev 1.19   Aug 13 2004 14:29:08   passuied
//Changes for FindA station distinct error message
//
//   Rev 1.18   Jul 30 2004 12:09:22   passuied
//added back redirection for station mode
//
//   Rev 1.17   Jul 27 2004 14:03:08   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.16   Jul 26 2004 20:21:02   passuied
//Changes in ResultsTravelFrom, ResultsTravelTo, ResultsNext.
//Now only changes the SearchType to FindNearest in ResultsNext when all needed locations are valid and leaving the FindNearest Stations functionality. This is because we don't need this info for the functionality, and if set before, we lose the SearchType info set by the user and it cannot be set back in case of AmendSearch
//
//   Rev 1.15   Jul 26 2004 10:41:12   passuied
//removed extra terminal info in BuildResultTable
//
//   Rev 1.14   Jul 23 2004 17:36:30   passuied
//FindStation 6.1. Labels and text updates
//
//   Rev 1.13   Jul 22 2004 18:05:52   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.12   Jul 22 2004 10:12:44   RPhilpott
//Changed oSGR GetDistance to return int, not double.
//
//   Rev 1.11   Jul 21 2004 10:49:00   passuied
//Addition of methods and rework for FindStation func 6.1. Working
//
//   Rev 1.10   Jul 15 2004 11:14:40   jmorrissey
//Added BuildStationAirportNames and checked new method against FxCop.
//
//   Rev 1.9   Jul 14 2004 13:00:22   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.8   Jul 09 2004 13:08:12   passuied
//Updated FindStation and filter methods for FindStation 6.1 back end
//
//   Rev 1.7   Jul 01 2004 14:12:50   passuied
//changes following exhaustive testing
//
//   Rev 1.6   Jun 30 2004 15:43:02   passuied
//Cleaning up
//
//   Rev 1.5   Jun 07 2004 17:07:06   passuied
//added check for no selected terminals and all reselection
//
//   Rev 1.4   Jun 02 2004 16:38:26   passuied
//working version
//
//   Rev 1.3   May 28 2004 17:48:10   passuied
//update as part of FindStation development
//
//   Rev 1.2   May 24 2004 12:12:32   passuied
//checked in to comply with control changes
//
//   Rev 1.1   May 21 2004 15:49:40   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.0   May 14 2004 17:35:28   passuied
//Initial Revision

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Collections;


using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Class provinding methods used by FindStations UI
	/// </summary>
	public class FindStationHelper
	{
		#region Constants declaration
		public const string columnIndex = "Index";
		public const string columnTransport = "Transport";
		public const string columnNaptanId = "NaptanId";
		public const string columnStationName = "StationName";
		public const string columnDistance = "Distance";
		public const string columnSelected = "Selected";
        public const string columnEasting = "Easting";
        public const string columnNorthing = "Northing";
		#endregion

		#region Common Resource Keys for FindStation
		public const string RES_FROM			= "FindStation.from";
		public const string RES_TO				= "FindStation.to";
		public const string RES_AIRPORT			= "FindStation.airport";
		public const string RES_STATION			= "FindStation.station";
		public const string RES_STATION_AIRPORT	= "FindStation.stationairport";
		public const string RES_TITLE_NEAREST	= "FindStation.findNearest";
		public const string RES_TITLE_TRAVEL	= "FindStation.toTravel";
		public const string RES_URL_PRINTER		= "JourneyPlanner.imageButtonPrinterFormat.ImageUrl";
		public const string RES_ALT_PRINTER		= "JourneyPlanner.imagePrinter.AlternateText";
		public const string RES_ANCHOR_PRINTER	= "FindStation.UrlPrintableFindStationResults";
		public const string RES_NEXT_URL		= "FindStationMap.commandNext.ImageUrl";
		public const string RES_NEXT_ALT		= "FindStationMap.commandNext.AlternateText";
		public const string RES_BACK_URL		= "FindStationMap.commandBack.ImageUrl";
		public const string RES_BACK_ALT		= "FindStationMap.commandBack.AlternateText";
		public const string RES_TO_URL			= "FindStationMap.commandTravelTo.ImageUrl";
		public const string RES_TO_ALT			= "FindStationMap.commandTravelTo.AlternateText";
		public const string RES_FROM_URL		= "FindStationMap.commandTravelFrom.ImageUrl";
		public const string RES_FROM_ALT		= "FindStationMap.commandTravelFrom.AlternateText";
		public const string RES_SHMAP_URL		= "FindStationMap.commandShowMap.ImageUrl";
		public const string RES_SHMAP_ALT		= "FindStationMap.commandShowMap.AlternateText";
		public const string RES_HMAP_URL		= "FindStationMap.commandHideMap.ImageUrl";
		public const string RES_HMAP_ALT		= "FindStationMap.commandHideMap.AlternateText";
		public const string RES_SCALE			= "FindStation.scale";
		public const string RES_NEWSEARCH_URL	= "FindStationMap.commandNewSearch.ImageUrl";
		public const string RES_NEWSEARCH_ALT	= "FindStationMap.commandNewSearch.AlternateText";
		public const string RES_AMENDSEARCH_URL	= "FindStationMap.commandAmendSearch.ImageUrl";
		public const string RES_AMENDSEARCH_ALT	= "FindStationMap.commandAmendSearch.AlternateText";
		public const string RES_RAIL			= "FindStation.rail";
		public const string RES_COACH			= "FindStation.coach";
		public const string RES_CARPARK			= "FindCarPark";

		public const string TRANSPORT_TYPE		= "FindStationTransportType.{0}";


		#endregion


		/// <summary>
		/// Builds a DataTable from the Naptans of airports included in location in parameter
		/// </summary>
		/// <param name="location">location to get the naptans from</param>
		/// <returns>Created DataTable</returns>
		static public DataTable BuildResultsDataTable(TDLocation location)
		{
			DataTable resultTable = new DataTable();

			resultTable.Columns.Add(columnIndex, typeof(int));
			resultTable.Columns.Add(columnTransport, typeof(string));
			resultTable.Columns.Add(columnNaptanId, typeof(string));
			resultTable.Columns.Add(columnStationName, typeof(string));
			resultTable.Columns.Add(columnDistance, typeof(int));
			resultTable.Columns.Add(columnSelected, typeof(bool));
            resultTable.Columns.Add(columnEasting, typeof(int));
            resultTable.Columns.Add(columnNorthing, typeof(int));

			int index = 0;

			StationType[] stationTypes = (StationType[])Enum.GetValues(typeof(StationType));
			Hashtable nearestRows = new Hashtable(stationTypes.Length);

			foreach (StationType s in stationTypes)
				nearestRows.Add(s, null);

			foreach( TDNaptan aNaptan in location.NaPTANs)
			{
				
				DataRow row = resultTable.NewRow();

				row[columnIndex] = index;
				row[columnTransport] = aNaptan.StationType.ToString();
				row[columnNaptanId] = aNaptan.Naptan;
				string stationName = aNaptan.Name;
			
				row[columnStationName] = stationName;
				row[columnDistance] = location.GridReference.DistanceFrom( aNaptan.GridReference);

				row[columnSelected] = false;

                row[columnEasting] = aNaptan.GridReference.Easting;
                row[columnNorthing] = aNaptan.GridReference.Northing;
			
				resultTable.Rows.Add(row);
			
				// Check to see if this station is closer than the current closest
				if ( (nearestRows[aNaptan.StationType] == null) || ( ((int)((DataRow)nearestRows[aNaptan.StationType])[columnDistance]) > ((int)row[columnDistance])) )
					// It is
					nearestRows[aNaptan.StationType] = row;

				index++;
				
			}

			// Now set the closest stations to be selected
			foreach (StationType s in stationTypes)
			{
				if (nearestRows[s] != null)
                    ((DataRow)nearestRows[s])[columnSelected] = true;
			}


			return resultTable;
		}

		/// <summary>
		/// Sort the datatable by AirportName
		/// </summary>
		/// <param name="resultTable">table to sort</param>
		/// <param name="isAscendant">ascendent/descendent sortin</param>
		/// <returns>sorted data rows</returns>
		static public DataRow[] SortResultsDataTableByName(DataTable resultTable, bool isAscendant)
		{
			string ascDescIndicator = string.Empty;

			ascDescIndicator = isAscendant?  " ASC" : " DESC";

			string sortExpression = columnStationName +ascDescIndicator;
			
			return resultTable.Select(string.Empty, sortExpression);


		}

		/// <summary>
		/// Sort the datatable by Transport mode
		/// </summary>
		/// <param name="resultTable">table to sort</param>
		/// <param name="isAscendant">ascendent/descendent sortin</param>
		/// <returns>sorted data rows</returns>
		static public DataRow[] SortResultsDataTableByTransport(DataTable resultTable, bool isAscendant)
		{
			string ascDescIndicator = string.Empty;

			ascDescIndicator = isAscendant?  " ASC" : " DESC";

			string sortExpression = columnTransport +ascDescIndicator;
			
			return resultTable.Select(string.Empty, sortExpression);


		}

		/// <summary>
		/// Sort the datatable by Distance
		/// </summary>
		/// <param name="resultTable">table to sort</param>
		/// <param name="isAscendant">ascendent/descendent sorting</param>
		/// <returns>sorted data rows</returns>
		static public DataRow[] SortResultsDataTableByDistance(DataTable resultTable, bool isAscendant)
		{
			string ascDescIndicator = string.Empty;

			ascDescIndicator = isAscendant?  " ASC" : " DESC";

			string sortExpression = columnDistance +ascDescIndicator;
			
			return resultTable.Select(string.Empty, sortExpression);
		}

		/// <summary>
		/// Sort the datatable by Option
		/// </summary>
		/// <param name="resultTable">table to sort</param>
		/// <param name="isAscendant">ascendent/descendent sorting</param>
		/// <returns>sorted data rows</returns>
		static public DataRow[] SortResultsDataTableByOption(DataTable resultTable, bool isAscendant)
		{
			string ascDescIndicator = string.Empty;

			ascDescIndicator = isAscendant?  " ASC" : " DESC";

			string sortExpression = columnIndex +ascDescIndicator;
			
			return resultTable.Select(string.Empty, sortExpression);
		}

		/// <summary>
		/// Determines the coordinates of the zoom box to apply to the map
		/// according to airports contained in the given location
		/// </summary>
		/// <param name="resultTable">table containing the airports</param>
		/// <param name="location">location to center the envelope to</param>
		/// <param name="minGridReference">Coordinates of the lower left corner of the envelope</param>
		/// <param name="maxGridReference">Coordinates of the upper right corner of the envelope</param>
		/// <returns>returns true if successful</returns>
		static public bool DetermineZoomEnvelope(
			DataTable resultTable, 
			TDLocation location, 
			ref OSGridReference minGridReference, 
			ref OSGridReference maxGridReference)
		{
			// determine more distant naptan
			DataRow[] rows = SortResultsDataTableByDistance(resultTable, false);

			// first item is the most distant one
			// radius to zoom into is max distance

			if (rows.Length == 0)
				return false;

			int radius = (int)rows[0][columnDistance];

			// minGridReference = X0-Radius ; Y0-Radius
			// 5% tolerance because of icons sometimes not shown entirely,
			minGridReference.Easting = location.GridReference.Easting - ((int)(radius*1.05)); 
			minGridReference.Northing = location.GridReference.Northing - ((int)(radius*1.05));

			// maxGridReference = X0+Radius ; Y0+Radius
			maxGridReference.Easting = location.GridReference.Easting + ((int)(radius*1.05));
			maxGridReference.Northing = location.GridReference.Northing + ((int)(radius*1.05));

			return true;


		}

		/// <summary>
		/// First checks for each row in the table if one instance of an airport terminal is selected and processing hasn't already been done.
		/// Then , it checks in the whole table for other terminals of the same airport that have not be selected
		/// If found, they are reselected
		/// Also checks if all terminals have been deselected. If so, reselect all.
		/// Finally, updates location with selected naptan terminals
		/// </summary>
		/// <param name="resultTable">table of airports process</param>
		/// <param name="location">location to update</param>
		static public void ReselectUncheckedTerminals(DataTable resultTable, ref TDLocation location)
		{
			ArrayList listTerminals = new ArrayList();
			string storedIATA = string.Empty;

			bool nothingSelected = true;
			for (int i=0 ; i<resultTable.Rows.Count; i++)
			{
				DataRow row = resultTable.Rows[i];
				
				// detects if one row is selected
				if ((bool)row[columnSelected])
					nothingSelected = false;

				// Terminal Reselect Processing
				TDNaptan naptan = location.NaPTANs[(int)row[columnIndex]];
				TDAirportNaptan selectedNaptan = null;
				if (naptan.StationType == StationType.Airport)
				{
					selectedNaptan = (TDAirportNaptan)naptan;
					string selectedIATA = selectedNaptan.IATA;
				
					// if the airport is selected and the processing hasn't been done for this IATA (airport)
					if ((bool)row[columnSelected] && storedIATA != selectedIATA)
					{	
						// for all rows in table, recheck terminals belonging to the selected airport
						foreach (DataRow innerRow in resultTable.Rows) 
						{
							TDNaptan tempNaptan = location.NaPTANs[(int)innerRow[columnIndex]];
							TDAirportNaptan aNaptan = null;
							if (tempNaptan.StationType == StationType.Airport)
							{
								aNaptan = (TDAirportNaptan)tempNaptan;

								if (selectedIATA == aNaptan.IATA && !((bool)innerRow[columnSelected]))
								{
									innerRow[columnSelected] = true;
								}
						

								storedIATA = selectedIATA;
							}
						}
					}
				}
			}

			if (nothingSelected)
			{
				foreach( DataRow row in resultTable.Rows)
				{
					row[columnSelected] = true;
				}
			}
			
			

			// Now fills the array with selected terminals
			foreach (DataRow row in resultTable.Rows)
			{
				if ((bool)row[columnSelected])
				{
					listTerminals.Add(location.NaPTANs[(int)row[columnIndex]]);

				}
			}

			location.NaPTANs = (TDNaptan[])listTerminals.ToArray(typeof(TDNaptan));

		}
		
		

		/// <summary>
		/// This method compares the different Travel Modes selected
		/// by user for From and To location and removes all uncommon ones
		/// It also return a StationTypeArray corresponding to the remaining 
		/// travel modes
		/// </summary>
		public static StationType[] ValidateTravelModes(TDLocation locationFrom, TDLocation locationTo)
		{
			// First look for all existing station types for From			
			StationType[] typesFrom = LocationSearch.GetLocationStationTypes(locationFrom);
			
			// then look for all existing station types for To
			StationType[] typesTo = LocationSearch.GetLocationStationTypes(locationTo);
			
			// compare them and get the common stations types
			ArrayList listCommonTypes = new ArrayList();

			foreach (StationType typeFrom in typesFrom)
			{
				foreach (StationType typeTo in typesTo)
				{
					if (typeFrom == typeTo)
						listCommonTypes.Add(typeFrom);
				}
			}
			
			StationType[] commonTypes = (StationType[])listCommonTypes.ToArray(typeof (StationType));
			// Remove in From and To all the naptans that don't meet the common type requirements
			
			locationFrom.NaPTANs = RemoveNotInTypes(locationFrom.NaPTANs, commonTypes);
			locationTo.NaPTANs = RemoveNotInTypes(locationTo.NaPTANs, commonTypes);


			return commonTypes;
			
		}

		/// <summary>
		/// Returns an array of naptans with only the given station types
		/// </summary>
		/// <param name="naptans">naptan to filter out</param>
		/// <param name="types">type whose naptans we want to keep in array</param>
		/// <returns></returns>
		private static TDNaptan[] RemoveNotInTypes(TDNaptan[] naptans, StationType[] types)
		{
			ArrayList listNaptans = new ArrayList();

			foreach (StationType type in types)
			{
				foreach (TDNaptan naptan in naptans)
				{
					if (naptan.StationType == type)
						listNaptans.Add(naptan);
				}
			}

			return (TDNaptan[])listNaptans.ToArray(typeof(TDNaptan));
		}

		/// <summary>
		/// Returns an array of airport and station names. 
		/// Any airport name is a single entry with individual terminal names not returned
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public static string[] BuildStationAirportNames(TDLocation location)
		{			

			//the return array
			ArrayList arrayStationAirportNames = new ArrayList();	

			//temp boolean used to decide whether to add each location naptan to the return array
			bool addName = true;		

			//check all naptans for the location
			for( int i = 0; i < location.NaPTANs.Length; i++)
			{	
				//get the naptan name
				TDNaptan aNaptan = location.NaPTANs[i];

				//check whether this name alraedy exists in the return array
				addName = true;
				for (int j = 0; j < arrayStationAirportNames.Count; j++)
				{
					//if the name already has been added to the return array then set bool to false
					//so that it doesn't get added again
					if ((string)arrayStationAirportNames[j] == aNaptan.Name)
					{
						addName = false;
					}
				}

				//add the unique naptan name 
				if (addName)
				{
					arrayStationAirportNames.Add(aNaptan.Name);
				}
			}				

			//return the array
			return (string[])arrayStationAirportNames.ToArray(typeof(string));
		}

		/// <summary>
		/// Standard Behaviour after Click on TravelFrom button In results pages (result/Map)
		/// </summary>
		static public void ResultsTravelFrom()
		{
			TDLocation location = new TDLocation();
			LocationSearch search = new LocationSearch();

			FindStationPageState stationPageState = TDSessionManager.Current.FindStationPageState;
			FindPageState pageState = TDSessionManager.Current.FindPageState;

			if (stationPageState.CurrentLocation.Status == TDLocationStatus.Valid)
			{
				location = stationPageState.CurrentLocation;
				search = stationPageState.CurrentSearch;
			}
			else
			{
				// If the TravelFrom button is on screen but CurrentLocation is not valid then the User must have used the client's back button

				if ((stationPageState.LocationType == FindStationPageState.CurrentLocationType.To)
					&& (stationPageState.LocationFrom.Status == TDLocationStatus.Valid))
				{
					location = stationPageState.LocationFrom;
					search = stationPageState.SearchFrom;
				}
				else if ((stationPageState.LocationType == FindStationPageState.CurrentLocationType.From)
					&& (stationPageState.LocationTo.Status == TDLocationStatus.Valid))
				{
					location = stationPageState.LocationTo;
					search = stationPageState.SearchTo;

					stationPageState.LocationTo = new TDLocation();
					stationPageState.SearchTo = new LocationSearch();
				}
			}

			if (location.Status == TDLocationStatus.Valid)
			{
				// Ensure that all of the stations for this location have been found.
				LocationSearchHelper.FindStations(ref location, stationPageState.StationTypes);

				FindStationHelper.ReselectUncheckedTerminals(
					stationPageState.StationResultsTable,
					ref location);
			
				// We want to update StationTypes in pageState and get rid
				// of all the stationTypes that the user de-selected. Therefore,
				// it won't look for those for the To location
				stationPageState.StationTypes = LocationSearch.GetLocationStationTypes(location);
			
				stationPageState.LocationFrom = location;
				stationPageState.SearchFrom = search;

				// indicates the remaining location to be defined
				stationPageState.LocationType = FindStationPageState.CurrentLocationType.To;

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
			}
		}

		/// <summary>
		/// Standard Behaviour after Click on TravelTo button In results pages (result/Map)
		/// </summary>
		static public void ResultsTravelTo()
		{
			TDLocation location = new TDLocation();
			LocationSearch search = new LocationSearch();

			FindStationPageState stationPageState = TDSessionManager.Current.FindStationPageState;
			FindPageState pageState = TDSessionManager.Current.FindPageState;

			if (stationPageState.CurrentLocation.Status == TDLocationStatus.Valid)
			{
				location = stationPageState.CurrentLocation;
				search = stationPageState.CurrentSearch;
			}
			else
			{
				// If the TravelTo button is on screen but CurrentLocation is not valid then the User must have used the client's back button

				if ((stationPageState.LocationType == FindStationPageState.CurrentLocationType.To)
					&& (stationPageState.LocationFrom.Status == TDLocationStatus.Valid))
				{
					location = stationPageState.LocationFrom;
					search = stationPageState.SearchFrom;

					stationPageState.LocationFrom = new TDLocation();
					stationPageState.SearchFrom = new LocationSearch();
				}
				else if ((stationPageState.LocationType == FindStationPageState.CurrentLocationType.From)
					&& (stationPageState.LocationTo.Status == TDLocationStatus.Valid))
				{
					location = stationPageState.LocationTo;
					search = stationPageState.SearchTo;
				}
			}

			if (location.Status == TDLocationStatus.Valid)
			{
				// Ensure that all of the stations for this location have been found.
				LocationSearchHelper.FindStations(ref location, stationPageState.StationTypes);

				FindStationHelper.ReselectUncheckedTerminals(
					stationPageState.StationResultsTable,
					ref location);
			
				// We want to update StationTypes in pageState and get rid
				// of all the stationTypes that the user de-selected. Therefore,
				// it won't look for those for the From location
				stationPageState.StationTypes = LocationSearch.GetLocationStationTypes(location);

				stationPageState.LocationTo = location;
				stationPageState.SearchTo = search;

				// indicates the remaining location to be defined
				stationPageState.LocationType = FindStationPageState.CurrentLocationType.From;

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
			}
		}

		/// <summary>
		/// Standard Behaviour after Click on Next button In results pages (result/Map)
		/// </summary>
		static public void ResultsNext()
		{
			TDLocation location = new TDLocation();
			LocationSearch search = new LocationSearch();

			TDJourneyParameters journeyParameters = TDSessionManager.Current.JourneyParameters;
			FindStationPageState stationPageState = TDSessionManager.Current.FindStationPageState;
			FindPageState pageState = TDSessionManager.Current.FindPageState;


			// The CurrentLocation may be invalid because the User pressed the client's back button.
			// If they did then the respective From or To location might be populated already.
			if ((stationPageState.LocationType == FindStationPageState.CurrentLocationType.From)
				&& (stationPageState.LocationFrom.Status == TDLocationStatus.Valid))
			{
				location = stationPageState.LocationFrom;
				search = stationPageState.SearchFrom;
			}
			else if ((stationPageState.LocationType == FindStationPageState.CurrentLocationType.To)
				&& (stationPageState.LocationTo.Status == TDLocationStatus.Valid))
			{
				location = stationPageState.LocationTo;
				search = stationPageState.SearchTo;
			}
			else
			{
				location = stationPageState.CurrentLocation;
				search = stationPageState.CurrentSearch;
			}

			if (location.Status != TDLocationStatus.Valid)
			{
				// If we were unable to obtain a valid location then quit now
				return;
			}

			// Ensure that all of the stations for this location have been found.
			LocationSearchHelper.FindStations(ref location, stationPageState.StationTypes);

			FindStationHelper.ReselectUncheckedTerminals(
				stationPageState.StationResultsTable,
				ref location);

			switch (pageState.Mode)
			{
					// station mode : Copy the missing location 
					// then Run the ValidateTravelModes to keep common travel modes
					// Then Redirect to the appropriate page according to remaining stationTypes
				case FindAMode.Station:
				{
					switch (stationPageState.LocationType)
					{
						case FindStationPageState.CurrentLocationType.From:
							stationPageState.LocationFrom = location;
							stationPageState.SearchFrom = search;
							break;
						case FindStationPageState.CurrentLocationType.To:
							stationPageState.LocationTo = location;
							stationPageState.SearchTo = search;
							break;
					}

					// Validate Travel Modes and update them
					stationPageState.StationTypes =
						FindStationHelper.ValidateTravelModes(stationPageState.LocationFrom, stationPageState.LocationTo);

					// Redirect to appropriate page:
					// If more than one distinct StationType, go to FindATrunk
					// If one only Go to the appropriate one
					// If none, Display error message
					if (stationPageState.StationTypes.Length == 0)
					{
						// stay in this page, next postback will display message
					}
					else
					{
						
						// Change DN049 : If chosen locations through FindStationInput,
						// Go to FindTrunkInput!
						
						journeyParameters = new TDJourneyParametersMulti();
						pageState = FindPageState.CreateInstance(FindAMode.TrunkStation);
						// Initialise Journey Parameters as the page would do when starts from scratch. Needed because when we go to the page, this Init won't be done!
						(new FindTrunkInputAdapter((TDJourneyParametersMulti)journeyParameters, (FindTrunkPageState)pageState, TDSessionManager.Current.InputPageState)).InitJourneyParameters();
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;	

	
						// save locations and Searches in  Journey Parameters
						journeyParameters.OriginLocation = stationPageState.LocationFrom;
						journeyParameters.Origin = stationPageState.SearchFrom;
						journeyParameters.DestinationLocation = stationPageState.LocationTo;
						journeyParameters.Destination = stationPageState.SearchTo;

						// set the SearchType property to FindNearest
						journeyParameters.Origin.SearchType = SearchType.FindNearest;
						journeyParameters.Destination.SearchType = SearchType.FindNearest;
						journeyParameters.OriginLocation.SearchType = SearchType.FindNearest;
						journeyParameters.DestinationLocation.SearchType = SearchType.FindNearest;
						
						TDSessionManager.Current.InputPageState = new InputPageState();

						//Start of code for vantive 3839955
						Boolean hasTrain = false;
						Boolean hasCoach = false;
						Boolean hasFlight = false;
			
						//Sets booleans to true for modes of transport that have been requested
						for (int i=0; i < journeyParameters.OriginLocation.NaPTANs.Length; i++)
						{
							String naptan = journeyParameters.OriginLocation.NaPTANs[i].Naptan;
							naptan = naptan.Substring(0, 4);

							switch (naptan)
							{
								case ("9100"):
									hasTrain = true;
									break;
								case ("9000"):
									hasCoach = true;
									break;
								case ("9200"):
									hasFlight = true;
									break;
							}
						}

						for (int i=0; i < journeyParameters.DestinationLocation.NaPTANs.Length; i++)
						{
							String naptan = journeyParameters.DestinationLocation.NaPTANs[i].Naptan;
							naptan = naptan.Substring(0, 4);

							switch (naptan)
							{
								case ("9100"):
									hasTrain = true;
									break;
								case ("9000"):
									hasCoach = true;
									break;
								case ("9200"):
									hasFlight = true;
									break;
							}
						}

						int noOfModes = 0;

						if (hasTrain)
						{
							noOfModes++;
						}
						if (hasCoach)
						{
							noOfModes++;
						}
						if (hasFlight)
						{
							noOfModes++;
						}

						//If three modes have been requested Public modes is already set for
						//Rail, Coach and Air.  If less the 3 modes recreate the array with the
						//correct transport modes for the jounrey request.
						if (noOfModes < 3)
						{

							journeyParameters.PublicModes = new ModeType[noOfModes];

							int index = 0;

							if (hasTrain)
							{
								journeyParameters.PublicModes[index] = ModeType.Rail;
								index++;
							}
							if (hasCoach)
							{
								journeyParameters.PublicModes[index] = ModeType.Coach;
								index++;
							}
							if (hasFlight)
							{
								journeyParameters.PublicModes[index] = ModeType.Air;
							}

						}
						//End of code for vantive 3839955

						// De activate Results in case there are some
						if (TDSessionManager.Current.JourneyResult != null)
							TDSessionManager.Current.JourneyResult.IsValid = false;


					}
					

				}
					break;
				// From FindXXXInput
				default:
				{
					// detect which location to update
					switch (stationPageState.LocationType)
					{
						case FindStationPageState.CurrentLocationType.From:
							// save locations in  Journey Parameters
							journeyParameters.OriginLocation = location;
							journeyParameters.OriginLocation.SearchType = SearchType.FindNearest;
							journeyParameters.Origin.SearchType = SearchType.FindNearest;
							break;
						case FindStationPageState.CurrentLocationType.To:
							// save locations in  Journey Parameters
							journeyParameters.DestinationLocation = location;
							journeyParameters.DestinationLocation.SearchType = SearchType.FindNearest;
							journeyParameters.Destination.SearchType = SearchType.FindNearest;
							break;
					}
					
					// Redirect to page according to FindAMode
					switch (pageState.Mode)
					{
						case FindAMode.Coach:
							TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCoachInputDefault;
							break;
						case FindAMode.Flight:
						{
							// Special update for FindAFlight
							FindFlightPageState flightPageState = (FindFlightPageState)pageState;
							// detect which location to update
							switch (stationPageState.LocationType)
							{
								case FindStationPageState.CurrentLocationType.From:
									flightPageState.OriginLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
									break;
								case FindStationPageState.CurrentLocationType.To:
									flightPageState.DestinationLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
									break;
							}
							TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFlightInputDefault;
							break;
						}
						case FindAMode.Train:
							TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrainInputDefault;
							break;
						case FindAMode.TrunkStation:
						case FindAMode.Trunk:
							TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;
							break;
					}
					break;
				}
			}

			TDSessionManager.Current.FindPageState = pageState;
			TDSessionManager.Current.JourneyParameters = journeyParameters;
		}



		/// <summary>
		/// Get string corresponding to stationType
		/// </summary>
		/// <returns>string stationtype</returns>
		public static string GetStationTypeString()
		{
			string sStationType = string.Empty;
			// Get Station type string
			if (TDSessionManager.Current.FindPageState.Mode == FindAMode.Station)
			{
				sStationType = Global.tdResourceManager.GetString(FindStationHelper.RES_STATION_AIRPORT , TDCultureInfo.CurrentUICulture);
				return sStationType;
			}
			else if (TDSessionManager.Current.FindPageState.Mode == FindAMode.Flight)
			{
				sStationType = Global.tdResourceManager.GetString(
					FindStationHelper.RES_AIRPORT,
					TDCultureInfo.CurrentUICulture);
			}
			else if (TDSessionManager.Current.FindPageState.Mode == FindAMode.CarPark)
			{
				sStationType = Global.tdResourceManager.GetString(
					FindStationHelper.RES_CARPARK, TDCultureInfo.CurrentUICulture);
			}
			else
			{
				sStationType = Global.tdResourceManager.GetString(
					RES_STATION,
					TDCultureInfo.CurrentUICulture);
			}

			return sStationType;
		}

		/// <summary>
		/// Get direction (from/to)string in current language
		/// </summary>
		/// <returns>string direction</returns>
		public static string GetDirectionString()
		{
			string sDirection = string.Empty;
			if (TDSessionManager.Current.FindStationPageState.LocationType == FindStationPageState.CurrentLocationType.From)
			{
				sDirection = Global.tdResourceManager.GetString(FindStationHelper.RES_FROM,
					TDCultureInfo.CurrentUICulture);
			}
			else
			{
				sDirection = Global.tdResourceManager.GetString(FindStationHelper.RES_TO,
					TDCultureInfo.CurrentUICulture);
			}

			return sDirection;
		}
		
	}
}
