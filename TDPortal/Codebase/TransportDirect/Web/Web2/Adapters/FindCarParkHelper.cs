// *********************************************** 
// NAME                 : FindCarParkHelper.ascx
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/08/2006 
// DESCRIPTION			: Class providing helper methods 
//						  for Find Nearest Car Parks
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindCarParkHelper.cs-arc  $ 
//
//   Rev 1.3   May 08 2008 11:40:48   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Mar 28 2008 13:00:00   mmodi
//Updated to include set up for Door to door redirect
//
//   Rev DevFactory   Feb 14 2008 13:00:00   mmodi
//Updated check for Park and ride car park
//
//   Rev DevFactory   Feb 06 2008 17:00:00   mmodi
//Added Car park name and has language changed methods
//
//   Rev 1.0   Nov 08 2007 13:11:18   mturner
//Initial revision.
//
//   Rev 1.13   Apr 25 2007 13:40:12   mmodi
//Reset locations when setting up origin/destination
//Resolution for 4394: Car Park: Using Browser back button causes journey planning issue
//
//   Rev 1.12   Sep 30 2006 16:51:14   mmodi
//Updated CarParkingAvailable property in a try catch
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.11   Sep 27 2006 16:10:08   esevern
//Added CarParkingAvailable property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.10   Sep 18 2006 17:20:16   tmollart
//Modified calls to retrieve car park from catalogue.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.9   Sep 08 2006 14:55:34   esevern
//Amended call to CarParkCatalogue.LoadData - now only loads data on specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.8   Sep 05 2006 15:00:14   mmodi
//Car park name display updated
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.7   Sep 05 2006 11:26:48   mmodi
//Removed transition to be controlled by user class
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Sep 01 2006 13:08:58   esevern
//added check for location being null before setting access point grid ref info
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Sep 01 2006 12:59:30   esevern
//Added location GridReference to data table row
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 31 2006 17:06:18   MModi
//Added method to return all grid references for supplied location containing car parks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 30 2006 16:00:56   mmodi
//Amended to prevent Null car parks causing error 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 22 2006 15:00:52   esevern
//Amended event handler for Drive from/Drive to to shift to the D2D input page. Removed 'Next' button event handler as not required for Find nearest car parks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 15 2006 15:49:44   esevern
//Interim check in for developer integration
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 14 2006 11:49:38   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2

using System;
using System.Data;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
using System.Threading;
using System.Web;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for FindCarParkHelper.
	/// </summary>
	public class FindCarParkHelper
	{
		#region Constants declaration

		public const string RES_FROM = "FindCarPark.from";
		public const string RES_TO = "FindCarPark.to";
		public const string RES_SCALE = "FindCarPark.scale";
		
		public const string columnIndex = "Index";
		public const string columnCarParkRef = "CarParkRef";
		public const string columnCarParkName = "CarParkName";
		public const string columnDistance = "Distance";
        public const string columnHasDisabledSpaces = "DisabledSpaces";
        public const string columnNumberOfSpaces = "Spaces";
        public const string columnisSecure = "Secure";
		public const string columnSelected = "Selected";
		public const string columnGridRef = "GridRef";

		public const string GEOCODE_MAP = "Map";

		public const string PARKING_EXIT = "Exit";
		public const string PARKING_ENTRANCE = "Entrance";
		public const string PARKING_MAP = "Map";

		private const string parkAndRideTag = " park & ride";
		#endregion

		/// <summary>
		/// Builds a DataTable for the FindNearestCarParks results from the 
		/// location provided
		/// </summary>
		/// <param name="location">location to get the naptans from</param>
		/// <returns>Created DataTable</returns>
		static public DataTable BuildResultsDataTable(TDLocation location)
		{
			DataTable resultTable = new DataTable();

			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];

			resultTable.Columns.Add(columnIndex, typeof(int));
			resultTable.Columns.Add(columnCarParkRef, typeof(string));
			resultTable.Columns.Add(columnCarParkName, typeof(string));
			resultTable.Columns.Add(columnDistance, typeof(int));
			resultTable.Columns.Add(columnSelected, typeof(bool));
			resultTable.Columns.Add(columnGridRef, typeof(OSGridReference));
            resultTable.Columns.Add(columnNumberOfSpaces, typeof(int));
            resultTable.Columns.Add(columnisSecure, typeof(int));
            resultTable.Columns.Add(columnHasDisabledSpaces, typeof(int));

			int index = 0;

			foreach( string carParkRef in location.CarParkReferences)
			{
							
				//use the car park ref to obtain further info on the 
				// car park (stored procedure) and create a new CarPark obj. 
				CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);

				if (carPark != null)
				{

					DataRow row = resultTable.NewRow();
				
					row[columnIndex] = index;
					row[columnCarParkRef] = carParkRef;

					// Set Car park name
					string carLocation;

                    if (carPark.CarParkAdditionalData != null)
                    {
                        row[columnNumberOfSpaces] = carPark.CarParkAdditionalData.TotalSpaces;
                        row[columnisSecure] = carPark.CarParkAdditionalData.IsSecure;
                        row[columnHasDisabledSpaces] = carPark.CarParkAdditionalData.TotalDisabledSpaces;
                    }
                    else
                    {
                        row[columnNumberOfSpaces] = -1;
                        row[columnisSecure] = -1;
                        row[columnHasDisabledSpaces] = -1;
                    }

					// in case there is no location, then this prevents car name being displayed incorrectly
					if (carPark.Location != "")
						carLocation = carPark.Location + ", ";
					else
						carLocation = "";

                    if (carPark.ParkAndRideIndicator.Trim().ToLower() == "true")
						row[columnCarParkName] = carLocation + carPark.Name + parkAndRideTag;
					else
						row[columnCarParkName] = carLocation + carPark.Name;

					//use the Map, Entrance, or Exit accessPoint grid reference
					if (GetGridReference(carPark.AccessPoints) != null)
					{
						OSGridReference gridReference = GetGridReference(carPark.AccessPoints);
						row[columnDistance] = location.GridReference.DistanceFrom(gridReference);
						row[columnGridRef] = gridReference;
					}
					else
					{
					//dont set Distance to anything
					}
				
					// set the first car park in the list to be selected	
					if(index == 0)
					{
						row[columnSelected] = true;
					}
					else 
					{
						row[columnSelected] = false;
					} 

					resultTable.Rows.Add(row);
				}
				index++;
			} 
			return resultTable;
		}

		/// <summary>
		/// Sort the datatable by Car Park name
		/// </summary>
		/// <param name="resultTable">table to sort</param>
		/// <param name="isAscendant">ascendent/descendent sortin</param>
		/// <returns>sorted data rows</returns>
		static public DataRow[] SortResultsDataTableByName(DataTable resultTable, bool isAscendant)
		{
			string ascDescIndicator = string.Empty;
			ascDescIndicator = isAscendant?  " ASC" : " DESC";
			string sortExpression = columnCarParkName +ascDescIndicator;
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
        /// Sort the table by IsSecure
        /// </summary>
        /// <param name="resultTable"></param>
        /// <param name="isAscendant"></param>
        /// <returns></returns>
        static public DataRow[] SortResultsDataTableByIsSecure(DataTable resultTable, bool isAscendant)
        {
            string ascDescIndicator = string.Empty;
            ascDescIndicator = isAscendant ? " ASC" : " DESC";
            string sortExpression = columnisSecure + ascDescIndicator;
            return resultTable.Select(string.Empty, sortExpression);
        }
        /// <summary>
        /// Sort the data table by HasDisabledSpaces
        /// </summary>
        /// <param name="resultTable"></param>
        /// <param name="isAscendant"></param>
        /// <returns></returns>
        static public DataRow[] SortResultsDataTableByHasDisabledSpaces(DataTable resultTable, bool isAscendant)
        {
            string ascDescIndicator = string.Empty;
            ascDescIndicator = isAscendant ? " ASC" : " DESC";
            string sortExpression = columnHasDisabledSpaces + ascDescIndicator;
            return resultTable.Select(string.Empty, sortExpression);
        }
        /// <summary>
        /// Sort the data table by TotalNumberOfSpaces
        /// </summary>
        /// <param name="resultTable"></param>
        /// <param name="isAscendant"></param>
        /// <returns></returns>
        static public DataRow[] SortResultsDataTableByTotalSpaces(DataTable resultTable, bool isAscendant)
        {
            string ascDescIndicator = string.Empty;
            ascDescIndicator = isAscendant ? " ASC" : " DESC";
            string sortExpression = columnNumberOfSpaces + ascDescIndicator;
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
		/// Standard Behaviour after Click on TravelFrom button In results pages (result/Map)
		/// </summary>
		static public void ResultsTravelFrom()
		{

			TDLocation location = new TDLocation();
			LocationSearch search = new LocationSearch();
			FindCarParkPageState carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			FindPageState pageState = TDSessionManager.Current.FindPageState;

			SetupCarParkPlanning(true);

			if (carParkPageState.CurrentLocation.Status == TDLocationStatus.Valid)
			{
				location = carParkPageState.CurrentLocation;
				search = carParkPageState.CurrentSearch;
			}
			else
			{
				// If the TravelFrom button is on screen but CurrentLocation is not valid 
				// then the User must have used the client's back button
				if ((carParkPageState.LocationType == FindCarParkPageState.CurrentLocationType.To)
					&& (carParkPageState.LocationFrom.Status == TDLocationStatus.Valid))
				{
					location = carParkPageState.LocationFrom;
					search = carParkPageState.SearchFrom;
				}
				else if ((carParkPageState.LocationType == FindCarParkPageState.CurrentLocationType.From)
					&& (carParkPageState.LocationTo.Status == TDLocationStatus.Valid))
				{
					location = carParkPageState.LocationTo;
					search = carParkPageState.SearchTo;
					carParkPageState.LocationTo = new TDLocation();
					carParkPageState.SearchTo = new LocationSearch();
				}
			}
			if (location.Status == TDLocationStatus.Valid)
			{
				carParkPageState.LocationFrom = location;
				carParkPageState.SearchFrom = search;
				carParkPageState.LocationType = FindCarParkPageState.CurrentLocationType.From;		
		
				// Clear out the other location to avoid scenario 
				// where From and To can both be populated to Car parks
				carParkPageState.LocationTo = new TDLocation();
				carParkPageState.SearchTo = new LocationSearch();
			}
		}

		/// <summary>
		/// Standard Behaviour after Click on TravelTo button In results pages (result/Map)
		/// </summary>
		static public void ResultsTravelTo()
		{
			TDLocation location = new TDLocation();
			LocationSearch search = new LocationSearch();
			FindCarParkPageState carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			FindPageState pageState = TDSessionManager.Current.FindPageState;

            SetupCarParkPlanning(false);

            // set the location locality, prevents ambiguity page being displayed in journey planning
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
            carParkPageState.CurrentLocation.Locality = gisQuery.FindNearestLocality(
                carParkPageState.CurrentLocation.GridReference.Easting,
                carParkPageState.CurrentLocation.GridReference.Northing);

			if (carParkPageState.CurrentLocation.Status == TDLocationStatus.Valid)
			{
				location = carParkPageState.CurrentLocation;
				search = carParkPageState.CurrentSearch;
			}
			else
			{
				// If the TravelTo button is on screen but CurrentLocation is not valid 
				// then the User must have used the client's back button
				if ((carParkPageState.LocationType == FindCarParkPageState.CurrentLocationType.To)
					&& (carParkPageState.LocationFrom.Status == TDLocationStatus.Valid))
				{
					location = carParkPageState.LocationFrom;
					search = carParkPageState.SearchFrom;
					carParkPageState.LocationFrom = new TDLocation();
					carParkPageState.SearchFrom = new LocationSearch();
				}
				else if ((carParkPageState.LocationType == FindCarParkPageState.CurrentLocationType.From)
					&& (carParkPageState.LocationTo.Status == TDLocationStatus.Valid))
				{
					location = carParkPageState.LocationTo;
					search = carParkPageState.SearchTo;
				}
			}
			if (location.Status == TDLocationStatus.Valid)
			{
				carParkPageState.LocationTo = location;
				carParkPageState.SearchTo = search;
				carParkPageState.LocationType = FindCarParkPageState.CurrentLocationType.To;
				
				// Clear out the other location to avoid scenario 
				// where From and To can both be populated to Car parks
				carParkPageState.LocationFrom = new TDLocation();
				carParkPageState.SearchFrom = new LocationSearch();
			}
		}

		/// <summary>
		/// Checks the planning point of the car park to determine which grid
		/// reference should be used for journey planning. If the car park 
		/// should be used as a planning point, the map grid reference of 
		/// the centre of the car park should be used. If the car park is 
		/// not a planning point, and there is a street name for the entrance
		/// (if the car park is the journey destination), or the exit (if
		/// the car park is the journey origin), then use the grid reference
		/// for the street. If no street name is available, use road name
		/// search ...
		/// </summary>
		/// <param name="origin">boolean true if the car park is the journey origin location</param>
		static public void SetupCarParkPlanning(bool origin) 
		{
			// access the car park data
			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];
			
			string carParkRef = TDSessionManager.Current.InputPageState.CarParkReference;
			CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);				
				
			// set the car park
			TDLocation theLocation;
			if(TDSessionManager.Current.FindCarParkPageState.CurrentLocation != null)
			{
				theLocation = TDSessionManager.Current.FindCarParkPageState.CurrentLocation;
			}
			else 
			{
				theLocation = new TDLocation();
			}
			theLocation.CarParking = carPark;
			
			// check the access point
			CarParkAccessPoint[] pointsList = carPark.AccessPoints;
			
			for(int i=0; i<pointsList.Length; i++)
			{
				CarParkAccessPoint accessPoint = pointsList[i];
				string geocodeType = accessPoint.GeocodeType;

				if(carPark.PlanningPoint)
				{
					// if its a planning point and a map access point
					if( string.Compare(geocodeType,PARKING_MAP, true) == 0)
					{
						theLocation.GridReference = accessPoint.GridReference;
                        theLocation.Toid = carPark.GetMapToids();
					}
				}
				else 
				{
					// car park is the origin so use the exit co-ordinates if there are any
					if(accessPoint.StreetName != null) 
					{
						if(origin)
						{
							// get the exit street name to use for name search
                            if (string.Compare(geocodeType, PARKING_EXIT, true) == 0)
                            {
                                theLocation.AddressToMatch = accessPoint.StreetName;
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetExitToids();
                            }
						}
						else
						{
							// get the entrance street name to use for name search
                            if (string.Compare(geocodeType, PARKING_ENTRANCE, true) == 0)
                            {
                                theLocation.AddressToMatch = accessPoint.StreetName;
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetEntranceToids();
                            }
						}
					}
					else
					{
						if(origin)
						{
							// use the exit grid reference
                            if (string.Compare(geocodeType, PARKING_EXIT, true) == 0)
                            {
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetExitToids();
                            }
						}
						else 
						{
							// use the entrance grid reference
                            if (string.Compare(geocodeType, PARKING_ENTRANCE, true) == 0)
                            {
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetEntranceToids();
                            }
						}
					}
				}
			}
			// set location
			TDSessionManager.Current.FindCarParkPageState.CurrentLocation = theLocation;
		}

        /// <summary>
        /// Sets session values ready to return to the Door to door planner
        /// This should be called after Car park location has already been setup
        /// </summary>
        /// <param name="origin"></param>
        static public void SetupCarParkLocationForDoorToDoor(bool origin)
        {
            FindCarParkPageState carParkPageState = TDSessionManager.Current.FindCarParkPageState;

            // Take our current car park location and overwrite the old Door to Door location
            if (origin)
            {
                TDSessionManager.Current.JourneyParameters.OriginLocation = carParkPageState.CurrentLocation;
                
                // Clear out any naptans, and change searchtype as journey is now planned to coordinates
                TDSessionManager.Current.JourneyParameters.OriginLocation.NaPTANs = new TDNaptan[0];
                TDSessionManager.Current.JourneyParameters.OriginLocation.RequestPlaceType = RequestPlaceType.Coordinate;
                TDSessionManager.Current.JourneyParameters.OriginLocation.SearchType = SearchType.CarPark;

                TDSessionManager.Current.JourneyParameters.Origin.LocationFixed = true;
            }
            else
            {
                TDSessionManager.Current.JourneyParameters.DestinationLocation = carParkPageState.CurrentLocation;
                
                // Clear out any naptans, and change searchtype as journey is now planned to coordinates
                TDSessionManager.Current.JourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[0];
                TDSessionManager.Current.JourneyParameters.DestinationLocation.RequestPlaceType = RequestPlaceType.Coordinate;
                TDSessionManager.Current.JourneyParameters.DestinationLocation.SearchType = SearchType.CarPark;

                TDSessionManager.Current.JourneyParameters.Destination.LocationFixed = true;
            }

            // Force only car journeys
            TDJourneyParametersMulti jpm = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            jpm.PublicRequired = false;

            // To ensure we go back to Door to door mode, clear all reference to Find a mode
            TDSessionManager.Current.FindPageState = null;
        }

        /// <summary>
        /// Sets session values ready to return to the City to City planner
        /// This should be called after Car park location has already been setup
        /// </summary>
        /// <param name="origin"></param>
        static public void SetupCarParkLocationForCityToCity(bool origin)
        {
            FindCarParkPageState carParkPageState = TDSessionManager.Current.FindCarParkPageState;

            // Take our current car park location and overwrite the old city to city location
            if (origin)
            {
                TDSessionManager.Current.JourneyParameters.OriginLocation.Toid = carParkPageState.CurrentLocation.Toid;
                TDSessionManager.Current.JourneyParameters.OriginLocation.CarParking = carParkPageState.CurrentLocation.CarParking;
            }
            else
            {
                TDSessionManager.Current.JourneyParameters.DestinationLocation.Toid = carParkPageState.CurrentLocation.Toid;
                TDSessionManager.Current.JourneyParameters.DestinationLocation.CarParking = carParkPageState.CurrentLocation.CarParking;
            }
        }

		/// <summary>
		/// Returns a OSGridReference for the given access points
		/// </summary>
		/// <param name="accessPoints">Array of CarParkAccessPoints</param>
		/// <returns>OSGridReference</returns>
		static public OSGridReference GetGridReference(CarParkAccessPoint[] accessPoints)
		{
			
			for(int i=0; i<accessPoints.Length; i++)
			{
				if(accessPoints[i].GridReference != null)
				{
					return accessPoints[i].GridReference;
				}
			}
			return null;

		}


		/// <summary>
		/// Returns an Array of OSGridReferences for all the CarParkReferences in
		/// the current location
		/// </summary>
		/// <param name="location">location to get the carparkrefs from</param>
		/// <returns>OSGridReference array</returns>
		static public OSGridReference[] GetCarParkGridReference(TDLocation location)
		{
			OSGridReference[] osGridReference = new OSGridReference[location.CarParkReferences.Length];

			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];

			int index = 0;

			foreach( string carParkRef in location.CarParkReferences)
			{
				CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);

				if (carPark != null)
				{
					//use the Map, Entrance, or Exit accessPoint grid reference
					if (GetGridReference(carPark.AccessPoints) != null)
					{
						OSGridReference gridReference = GetGridReference(carPark.AccessPoints);
						osGridReference[index] = gridReference;
					}
					else
					{
						//dont set Distance to anything
					}
				}
				index++;
			} 
			return osGridReference;
		}

		/// <summary>
		/// Static read-only property indicating whether the car parking functionality
		/// should be made available
		/// </summary>
		/// <returns>boolean</returns>
		public static bool CarParkingAvailable
		{
			get { 
                try
				{
					return bool.Parse(Properties.Current["CarParkingAvailable"]); 
				}
				catch
				{
					return false;
				}
				}
		}

        /// <summary>
        /// Performs a check to find out if the language has been changed while
        /// on this page, uses the TravelNewsPageLanguage in the TravelNewsState
        /// </summary>
        public static bool HasLanguageChanged()
        {
            // Added to prevent Car park page details/view being reset
            // if language is changed while on this page
            FindCarParkPageState carParkState = TDSessionManager.Current.FindCarParkPageState;
            if (carParkState != null)
            {
                string previousLanguage = carParkState.PageLanguage;
                string currentLanguage = GetPageLanguage();

                // An empty string indicates this page has been accessed or loaded for the first time, 
                // therefore the language has not been changed and is the same as the current language
                if (string.IsNullOrEmpty(previousLanguage))
                    previousLanguage = currentLanguage;

                if (currentLanguage == previousLanguage)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the current page language to the InputPageState
        /// </summary>
        public static void SavePageLanguageToSession()
        {
            string currentLanguage = GetPageLanguage();

            // Update session variable to the current language
            FindCarParkPageState carParkState = TDSessionManager.Current.FindCarParkPageState;
            carParkState.PageLanguage = currentLanguage;
        }

        /// <summary>
        /// Sends back the current page language as a string
        /// </summary>
        private static string GetPageLanguage()
        {
            // Obtain page language using the CurrentUICulture setting	
            string currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();

            // If the channel is null, assume not using Content Management Server
            if (TDPage.SessionChannelName != null)
            {
                // get ISO language code for this channel
                currentLanguage = TDPage.GetChannelLanguage(TDPage.SessionChannelName.ToString());
            }
            return currentLanguage;
        }

        /// <summary>
        /// Returns the name of the car park in the format "Location, Name car park"
        /// </summary>
        /// <param name="carparking"></param>
        /// <returns></returns>
        public static string GetCarParkName(CarPark carparking)
        {
            string carLocation = string.Empty;
            string carParkName = string.Empty;

            if (carparking != null)
            {
                // in case there is no location, then this prevents car name being displayed incorrectly
                if (carparking.Location != string.Empty)
                    carLocation = HttpUtility.HtmlDecode(carparking.Location) + ", ";
                else
                    carLocation = string.Empty;

                carParkName = carLocation + HttpUtility.HtmlDecode(carparking.Name);

                // add on the car park end 
                if (carparking.ParkAndRideIndicator.Trim().ToLower() == "true")
                    carParkName += Global.tdResourceManager.GetString("ParkAndRide.Suffix") + Global.tdResourceManager.GetString("ParkAndRide.CarkPark.Suffix");
                else
                    carParkName += Global.tdResourceManager.GetString("ParkAndRide.CarkPark.Suffix");
            }

            return carParkName;
        }
	}
}
