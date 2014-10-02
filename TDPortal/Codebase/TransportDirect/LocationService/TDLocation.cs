// *********************************************** 
// NAME			: TDLocation.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TDLocation class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDLocation.cs-arc  $
//
//   Rev 1.17   Apr 08 2013 15:43:52   mmodi
//Added null check for naptans list for safety
//Resolution for 5919: Error with walkit link generation for journey Redhill Rail Station to Addiscombe, Leslie Grove
//
//   Rev 1.16   Mar 27 2013 09:30:10   mmodi
//Added clone method
//Resolution for 5908: Mapping walk leg shows intermediate change count icon in different place to actual start of leg
//
//   Rev 1.15   Jan 09 2013 11:41:54   mmodi
//Populate NPTG data call for Locality location
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.14   Dec 05 2012 14:10:50   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.13   Aug 28 2012 10:50:34   mmodi
//Added LocationSuggest functionality
//Resolution for 5832: CCN Gaz
//
//   Rev 1.12   Feb 16 2010 17:52:20   mmodi
//Helper method to update the naptan for the location
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 09 2010 09:45:16   apatel
//Updated for TD International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Nov 11 2009 16:42:44   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.9   Aug 04 2009 14:12:12   mmodi
//Added method to return a point as an osgr
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.8   Jun 03 2009 11:20:12   mmodi
//Added LatitudeLongitude property
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.7   Feb 02 2009 17:49:32   mmodi
//Populate the userSpecifiedVia flag for Routing Guide
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.6   Oct 20 2008 11:13:08   mmodi
//Updated to allow override location coordinates to be used for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Oct 13 2008 16:46:16   build
//Automatically merged from branch for stream5014
//
//   Rev 1.4.1.0   Jun 20 2008 14:56:58   mmodi
//Added new Point property
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   May 07 2008 11:31:18   RBRODDLE
//CCN0451 Main Coach Given Name filtering
//Resolution for 4915: CCN0451 - Control use of "GivenName" tag in cjp requests
//
//   Rev 1.3   Mar 10 2008 15:19:02   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev DevFactory Feb 06 2008 22:14:apatel
//  CCN 0426 added ToLowere() method which checking for parkandrideindicator.
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 12:25:24   mturner
//Initial revision.
//
//   Rev 1.56   Apr 25 2007 13:42:44   mmodi
//Reset carparking object in the ClearAll method
//Resolution for 4394: Car Park: Using Browser back button causes journey planning issue
//
//   Rev 1.55   Mar 13 2007 14:52:50   tmollart
//Updated code to get airport terminal naptans. Added new request place method to support alternate vias on ITP.
//
//   Rev 1.54   Mar 07 2007 15:34:42   rbroddle
//Altered TDLocation.cs ToRequestPlace method to prevent blank airport names being added to the NaPTAN cache 
//Resolution for 4364: Airports with no FlightRoutes not displayed correctly in Door-door
//
//   Rev 1.53   Oct 20 2006 15:17:18   mmodi
//Removed XmlIgnoreAttribute for CarParking, problem was with CarParkAccessPoint not having a default constructor
//Resolution for 4229: Transaction Injector fails with Car Parks
//
//   Rev 1.52   Oct 18 2006 10:26:24   mmodi
//Added XmlIgnoreAttribute to CarParking due to problem caused with TransactionInjector (temporary fix until actual cause found with CarPark.cs)
//Resolution for 4229: Transaction Injector fails with Car Parks
//
//   Rev 1.51   Oct 06 2006 12:51:06   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.50.1.5   Sep 22 2006 14:19:52   esevern
//Code review  comments - string initialisation now string.empty
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.50.1.4   Sep 21 2006 16:50:22   mmodi
//Modified to correctly call and set the TOIDs for a car park location
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4182: Car Parking: Entrance and Exit coordinates not used in journey planning
//
//   Rev 1.50.1.3   Sep 18 2006 17:09:20   tmollart
//Changed call for how a car park is retrieved from the catalogue.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.50.1.2   Sep 08 2006 14:51:32   esevern
//Amended call to CarParkCatalogue.LoadData - now only loads data on specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.50.1.1   Sep 05 2006 11:34:28   mmodi
//Added constructor to create a location using Car Park details
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.50.1.0   Aug 14 2006 10:57:22   esevern
//Added carParkReferences property for find nearest car parks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.50   Jun 01 2006 17:41:40   rphilpott
//Fix multi-threading problems in ToRequestPlace() method.
//Resolution for 4103: Find Cheaper - journeys not always returned.
//
//   Rev 1.49   Mar 31 2006 18:44:38   RPhilpott
//Add support for new "GetStreetsForPostcode()" method.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.48   Mar 30 2006 13:52:12   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.47   Mar 23 2006 17:40:24   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.46   Mar 17 2006 15:58:22   tmollart
//Added public method to populate toids.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.45   Mar 14 2006 10:44:58   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.44   Jan 05 2006 17:45:50   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.43   Dec 13 2005 11:38:48   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.42.1.0   Feb 07 2006 19:55:54   tmollart
//Added code to look up non popualated TOIDS.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.42   Dec 01 2005 16:14:00   RPhilpott
//Use NaptanCache lookup instead of calling GISQuery directly. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.41   Nov 15 2005 18:31:40   RPhilpott
//Use city-to-city database or GISQuery to populate locality, OSGR and name when these are not present for a naptan that is being used for find-a-fare coach journeys.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.40   Nov 02 2005 18:35:20   kjosling
//Automatically merged from branch for stream2877
//
//   Rev 1.39.1.0   Oct 28 2005 16:24:54   halkatib
//Changes made for Landing page phase 2
//Resolution for 2877: Del 8  Landing Page Phase2
//
//   Rev 1.39   Mar 22 2005 16:13:30   RPhilpott
//Move shared Naptan lookup and caching code to new NaptanLookup class.
//
//   Rev 1.38   Nov 03 2004 16:17:16   jgeorge
//Added ContainsNaptansForStationType method
//
//   Rev 1.37   Oct 08 2004 16:29:08   RPhilpott
//Reinsert code changes from rev 1.35 lost by PVCS.
//Resolution for 1683: Door-to-Door to/from some airports fails (such as Exeter)
//
//   Rev 1.36   Oct 05 2004 17:19:36   jgeorge
//Amended comparison method to take into account special case for airport naptans. 
//Resolution for 1685: Extend Journey - same origin/destination location not being caught
//
//   Rev 1.34   Sep 21 2004 20:07:40   RPhilpott
//Make handling of cached naptans a bit cleverer to prevent unnecessary searches when we have no name and cannot find naptan.
//
//   Rev 1.33   Sep 21 2004 17:26:22   RPhilpott
//Use location name obtained from GIS Query if no name provided by CJP/travelines, and include this name in the cache.  
//Resolution for 1612: Find-A-Train - initial walk leg can cause Null Reference Exception
//
//   Rev 1.32   Sep 13 2004 17:58:34   RPhilpott
//Support new TTBO station group structure by using only "group" airport naptans for all Find-A-Flight locations (origin, destination and via). 
//Resolution for 1402: Find a Flight STN to BEB via 9200GLA gives no results
//Resolution for 1455: Air stopovers returns no journeys.
//
//   Rev 1.31   Sep 11 2004 13:16:12   RPhilpott
//Move airport "puffing up" logic into TDLocation so that it is available in all three situations where it is needed.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1328: Find nearest stations/airports does not return any results
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.30   Sep 10 2004 15:35:56   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.29   Sep 09 2004 17:50:16   RPhilpott
//1) Change Find-A-Flight via location to pass new "group" NaPTAN instead of individual terminals.
//
//2) Modify Intersects() method to take a StationType parameter.
//Resolution for 1402: Find a Flight STN to BEB via 9200GLA gives no results
//Resolution for 1455: Air stopovers returns no journeys.
//Resolution for 1507: Find a flight will not find same region flight
//Resolution for 1527: Find a Variety of Transport - Will not return a journey for two nearby cities
//
//   Rev 1.28   Sep 06 2004 12:05:12   RPhilpott
//When constructing a TDLocation from a returned CJP leg airport naptan, transform 920F to 9200 where necessary.
//Resolution for 1506: Extending a flight to the start location returns a wrong journey
//
//   Rev 1.27   Sep 03 2004 11:33:08   RPhilpott
//Extra checks for unexpected nulls (mainly for NUnit tests).
//Resolution for 1354: Find A Flight results - wrong origin/dest location on output
//
//   Rev 1.26   Sep 02 2004 20:30:10   RPhilpott
//Use modified Naptan to get Airport name from AirDataProvider where necessary.
//Resolution for 1354: Find A Flight results - wrong origin/dest location on output
//
//   Rev 1.25   Aug 20 2004 12:41:58   RPhilpott
//Obtain OSGR and Locality from GIS Query for TDAirNaptan.
//Resolution for 1376: Find a flight journey map not displayed
//Resolution for 1404: No locality passed to CJP for Find-A-Flight requests
//
//   Rev 1.24   Aug 19 2004 11:56:52   RPhilpott
//Make RequestPlaceType NaPTAN for all P/T trunk requests.
//Resolution for 1388: Journey request type for Trunk journeys should be NaPTAN, not coordinate.
//
//   Rev 1.23   Aug 10 2004 19:47:02   RPhilpott
//Changes to ToRequestPlace(), to change airport Naptan prefix for non-stopover vias on find-a-flight.
//
//   Rev 1.22   Aug 09 2004 13:48:24   RPhilpott
//Only use Naptans/TOIDs that are relevant in ToRequestPlace(). 
//
//   Rev 1.21   Aug 02 2004 14:19:36   RPhilpott
//For trunk journeys, only pass Naptans of appropriate statiion type for this mode to the CJP.
//
//   Rev 1.20   Jul 26 2004 16:01:38   RPhilpott
//1) TOID's passed to CJP are links, not nodes;
//2)  Add TOID prefix ("osgb", but obtained from properties service) to TOID's before passing to CJP. 
//Resolution for 1152: Technical errors in road requests to CJP
//
//   Rev 1.19   Jun 11 2004 17:28:12   RPhilpott
//Move cache and timeout variables - make them local instead of private instance variables, to avoid problems trying to store non-serializable objects in the session data.
//
//   Rev 1.18   Jun 10 2004 12:34:00   RPhilpott
//Cache Naptan OSGR's for later use by output maps.
//
//
//   Rev 1.17   Jun 02 2004 17:42:16   acaunt
//IsMatchingNaPTANGroup updated for Tiploc and Coach naptans
//
//   Rev 1.16   Apr 27 2004 13:44:40   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.15   Nov 19 2003 13:55:44   PNorell
//Updated for handling northing and eastings from the journey result
//
//   Rev 1.14   Oct 15 2003 14:37:02   RPhilpott
//Correct handling of alternate locations
//
//   Rev 1.13   Oct 14 2003 14:32:42   acaunt
//Bug fixed IsMatchingNaPTANGroup method
//
//   Rev 1.12   Oct 14 2003 13:53:20   RPhilpott
//Workaround for TTBO "order of NAPTANs" problem.
//
//   Rev 1.11   Oct 14 2003 11:25:24   RPhilpott
//Add OSGRs to Naptans (now supported in CJP 1.8) 
//
//   Rev 1.10   Oct 13 2003 17:42:58   acaunt
//IsMatchingNaPTANGroup added
//
//   Rev 1.9   Oct 13 2003 13:59:18   passuied
//set default value for strings
//
//   Rev 1.8   Oct 11 2003 19:34:36   RPhilpott
//Correct initialisation in default ctors.
//
//   Rev 1.7   Sep 24 2003 17:35:00   RPhilpott
//Pass time into ToRequestPlace()
//
//   Rev 1.6   Sep 22 2003 13:47:28   passuied
//updated
//
//   Rev 1.5   Sep 20 2003 19:24:52   RPhilpott
//Support for passing OSGR's with NaPTAN's, various other fixes
//
//   Rev 1.4   Sep 20 2003 16:59:54   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.3   Sep 11 2003 16:33:56   jcotton
//Made Class Serializable
//
//   Rev 1.2   Sep 08 2003 17:43:54   RPhilpott
//More minor corrections
//
//   Rev 1.1   Sep 08 2003 17:28:42   RPhilpott
//Minor corrections.
//
//   Rev 1.0   Sep 05 2003 15:30:44   passuied
//Initial Revision
//
//   Rev 1.2   Aug 20 2003 17:55:56   AToner
//Work in progress

using System;
using System.Collections;
using System.Xml.Serialization;

using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Presentation.InteractiveMapping;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	public enum TDLocationStatus
	{
		Unspecified,
		Ambiguous,
		Valid
	}

	/// <summary>
	/// Summary description for TDLocation.
	/// </summary>
	[Serializable()]
	public class TDLocation
    {
        #region Static Private members

        static private string coachPrefix   = Properties.Current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Coach.ToString())];
		static private string railPrefix    = Properties.Current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Rail.ToString())];
		static private string airPrefix     = Properties.Current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Airport.ToString())];
		static private string airViaPrefix  = Properties.Current[LSKeys.NaptanPrefixAirportVia];

		static private string TOID_PREFIX = "JourneyControl.ToidPrefix";

        static private bool filterGivenNameTag = Convert.ToBoolean(Properties.Current[LSKeys.FilterGivenNameTag]);
        static private string[] givenNameTagFilterValues = Properties.Current[LSKeys.GivenNameTagFilterValues].Split('|');

        #endregion

        #region Private members

        private const string GEOCODE_MAP = "Map";

		private const string PARKING_EXIT = "Exit";
		private const string PARKING_ENTRANCE = "Entrance";
		private const string PARKING_MAP = "Map";

		private const int PREFIX_LENGTH		= 4;
		private const int IATA_LENGTH		= 3;
		private const int TERMINAL_LENGTH	= 1;

        private string dataSetId = string.Empty;
        private string parentId = string.Empty;
		private string description = string.Empty;
		private OSGridReference osGridReference;
        private LatitudeLongitude latitudeLongitude;
        private Point point;
		private string locality = string.Empty;
		private TDNaptan[] naptans;
		private string[] toid;
        private NPTGAdminDistrict nptgAdminDistrict = null;
		private SearchType locationSearchType;
		private RequestPlaceType requestType;
        private TDStopType stopType = TDStopType.Unknown;
		private TDLocationStatus locationStatus = TDLocationStatus.Unspecified;
		private bool boolPartPostcode = false;
		private double dblPartPostcodeMaxX = 0;
		private double dblPartPostcodeMaxY = 0;
		private double dblPartPostcodeMinX = 0;
		private double dblPartPostcodeMinY = 0;
		private ParkAndRideInfo parkAndRideScheme;
		private CarParkInfo carPark;
		private CarPark carParking;
		private CityInterchange[] cityInterchanges;
		private string addressToMatch = string.Empty;
		private string [] carParkRefs;
        private string cityId = string.Empty;
        private TDCountry country = null;
        private bool accessible = false;

        #endregion

        #region Constructors
        /// <summary>
		/// Default constructor.
		/// </summary>
		public TDLocation()
		{
			osGridReference = new OSGridReference();
			naptans = new TDNaptan[0];
			toid = new string[0];
            point = new Point(0, 0);
            latitudeLongitude = new LatitudeLongitude();
            nptgAdminDistrict = new NPTGAdminDistrict();
		}

		/// <summary>
		/// Overloaded constructor that takes a ParkAndRideInfo object
		/// </summary>
		/// <param name="parkAndRideInfo">The ParkANdRideInfo object</param>
		/// <param name="carParkID">The car park id. If id is not know or null then -1 should be used</param>
		public TDLocation(int parkAndRideID, int carParkID) : this()
		{
			IParkAndRideCatalogue parkAndRideCatalog = (IParkAndRideCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ParkAndRideCatalogue];
			ParkAndRideInfo parkAndRideInfo = parkAndRideCatalog.GetScheme(parkAndRideID);
			
			//get easting and northing values
			int easting = parkAndRideInfo.SchemeGridReference.Easting;
			int northing = parkAndRideInfo.SchemeGridReference.Northing;

			//set the locality
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
			locality = gisQuery.FindNearestLocality(easting, northing);

			//set the OSGridReference
			osGridReference = new OSGridReference(easting, northing);

			//get all the toids
			ArrayList toidArrayList = new ArrayList();
			foreach (CarParkInfo carParkInfo in parkAndRideInfo.GetCarParks())
			{
				foreach (string toidCarPark in carParkInfo.GetToids())
				{
					toidArrayList.Add(toidCarPark);
				}
			}

			toid = (string[])toidArrayList.ToArray(typeof(string));
			requestType = RequestPlaceType.Coordinate;

			description = parkAndRideInfo.Location;

			//set the SearchType
			locationSearchType = SearchType.ParkAndRide;

			//set ParkAndRideScheme
			parkAndRideScheme = parkAndRideInfo;
		}

		/// <summary>
		/// Overloaded constructor that takes a CarParking object
		/// </summary>
		/// <param name="carParkRef">The CarParking reference</param>
		/// <param name="origin">Origin indicator, used to determine coordinates</param>
		public TDLocation(string carParkRef, string locationDescription, bool origin) : this()
		{
			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];
			CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);
			
			// get easting and northing values, and set the toids 
			// by determining which access point to use
			string geocodeType;
			if(carPark.PlanningPoint)
				geocodeType = PARKING_MAP;
			else
			{
				if (origin)
					geocodeType = PARKING_EXIT;
				else
					geocodeType = PARKING_ENTRANCE;
			}
			
			int easting = 0;
			int northing = 0;
			CarParkAccessPoint[] pointsList = carPark.AccessPoints;

			for(int i=0; i<pointsList.Length; i++)
			{
				CarParkAccessPoint accessPoint = pointsList[i];
				
				if( string.Compare(geocodeType, accessPoint.GeocodeType, true) == 0)
				{
					easting = accessPoint.GridReference.Easting;
					northing = accessPoint.GridReference.Northing;

					switch (geocodeType)
					{
						case PARKING_MAP :
							toid = carPark.GetMapToids();
							break;
						case PARKING_EXIT :
							toid = carPark.GetExitToids();
							break;
						case PARKING_ENTRANCE :
							toid = carPark.GetEntranceToids();
							break;
						default :
							toid = carPark.GetMapToids();
							break;
					}
				}							
			}

			//set the OSGridReference
			osGridReference = new OSGridReference(easting, northing);

			//set the locality
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
			locality = gisQuery.FindNearestLocality(easting, northing);

			requestType = RequestPlaceType.Coordinate;

			string carLocation;
			// in case there is no location, then this prevents car name being displayed incorrectly
			if (carPark.Location != string.Empty)
				carLocation = carPark.Location + ", ";
			else
				carLocation = string.Empty;

            //set the description to the locationDescription specified
            if (locationDescription != string.Empty)
            {
                description = locationDescription;
            }
            else
            {
                // CCN 426 Added ToLower() to be safe side.
                if (carPark.ParkAndRideIndicator.Trim().ToLower() == "true")
                    description = carLocation + carPark.Name + " park & ride car park";
                else
                    description = carLocation + carPark.Name + " car park";
            }
			
			//set CarParking
			carParking = carPark;
        }
        
        /// <summary>
		/// Takes a CJP Leg and uses the information to populate this TDLocation
		/// </summary>
		/// <param name="legEvent">A CJP Leg class</param>
		public TDLocation(Event legEvent) : this()
		{
			if( legEvent != null )
			{
				string newNaptan = legEvent.stop.NaPTANID;

				description = legEvent.stop.name;

				if  (description == null)
				{
					description = string.Empty;
				}

				if	(legEvent.stop != null && legEvent.stop.NaPTANID != null && airPrefix != null && airViaPrefix != null) 
				{
					if	(legEvent.stop.NaPTANID.Length == airPrefix.Length + IATA_LENGTH + TERMINAL_LENGTH
						&& legEvent.stop.NaPTANID.StartsWith(airViaPrefix))
					{
						AirDataProvider.IAirDataProvider airData = (AirDataProvider.IAirDataProvider) TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

						newNaptan = airPrefix + legEvent.stop.NaPTANID.Substring(IATA_LENGTH + TERMINAL_LENGTH);

						Airport airport = airData.GetAirportFromNaptan(newNaptan);

						string newDescription = null;

						if	(airport != null) 
						{
							newDescription = airport.Name;
						}

						if	(newDescription != null && newDescription.Length > 0) 
						{
							description = newDescription;
						}
					}
				}

				if  (legEvent.stop.coordinate != null)
				{
					osGridReference.Easting  = legEvent.stop.coordinate.easting;
					osGridReference.Northing = legEvent.stop.coordinate.northing;
				}

				naptans = new TDNaptan[1];
				naptans[0] = new TDNaptan(newNaptan, osGridReference, description);
			}
		}
        
        #endregion

        #region Public methods

        /// <summary>
		/// Resets all the parameters of the Location object
		/// Required for Landing page autoplan off functionality
		/// </summary>
		public void ClearAll()
		{
            dataSetId = string.Empty;
            parentId = string.Empty;
            description = string.Empty;
			osGridReference = new OSGridReference();
            latitudeLongitude = new LatitudeLongitude();
            point = new Point(0, 0);
			locality = string.Empty;
			naptans = new TDNaptan[0];
			toid = new string[0];
            nptgAdminDistrict = new NPTGAdminDistrict();
			locationSearchType = new SearchType();
			requestType = new RequestPlaceType();
            stopType = TDStopType.Unknown;
			locationStatus = TDLocationStatus.Unspecified;
			boolPartPostcode = false;
			dblPartPostcodeMaxX = 0;
			dblPartPostcodeMaxY = 0;
			dblPartPostcodeMinX = 0;
			dblPartPostcodeMinY = 0;
            //parkAndRideScheme = null;
			//carPark = null;
            carParking = null;
            //cityInterchanges = null;
            //addressToMatch = string.Empty;
            //carParkRefs = null;
            cityId = string.Empty;
            country = null;
            accessible = false;
		}

        /// <summary>
        /// Creates a deep clone of this TDLocation object
        /// </summary>
        /// <returns></returns>
        public TDLocation Clone()
        {
            TDLocation loc = new TDLocation();

            loc.dataSetId = this.dataSetId;
            loc.parentId = this.parentId;
            loc.description = this.description;
            loc.osGridReference = new OSGridReference(this.osGridReference.Easting, this.osGridReference.Northing);
            loc.latitudeLongitude = new LatitudeLongitude(this.latitudeLongitude.Latitude, this.latitudeLongitude.Longitude);
            loc.point = new Point(this.point.X, this.point.Y);
            loc.locality = this.locality;
            if (this.naptans != null)
            {
                loc.naptans = new TDNaptan[this.naptans.Length];

                OSGridReference osgr = null;
                TDNaptan naptan = null;

                for (int i = 0; i < this.naptans.Length; i++)
                {
                    naptan = this.naptans[i];
                    osgr = new OSGridReference(naptan.GridReference.Easting, naptan.GridReference.Northing);

                    loc.naptans[i] = new TDNaptan(naptan.Naptan, osgr, naptan.Name, naptan.UseForFareEnquiries, naptan.Locality);
                }
            }
            else
                loc.naptans = new TDNaptan[0];
            loc.toid = this.toid;
            loc.nptgAdminDistrict = new NPTGAdminDistrict(this.nptgAdminDistrict.NPTGAdminCode, this.nptgAdminDistrict.NPTGDistrictCode);
            loc.locationSearchType = this.locationSearchType;
            loc.requestType = this.requestType;
            loc.stopType = this.stopType;
            loc.locationStatus = this.locationStatus;
            loc.boolPartPostcode = this.boolPartPostcode;
            loc.dblPartPostcodeMaxX = this.dblPartPostcodeMaxX;
            loc.dblPartPostcodeMaxY = this.dblPartPostcodeMaxY;
            loc.dblPartPostcodeMinX = this.dblPartPostcodeMinX;
            loc.dblPartPostcodeMinY = this.dblPartPostcodeMinY;
            // Commented out as not required at present, clone methods should be added on the objects if needed in the future
            //loc.parkAndRideScheme = null; 
            //loc.carPark = null;
            //loc.carParking = null;
            //loc.cityInterchanges = null;
            loc.addressToMatch = this.addressToMatch;
            loc.carParkRefs = this.carParkRefs;
            loc.cityId = this.cityId;
            loc.country = (this.country != null) ? this.country.Clone() : null;
            loc.accessible = this.accessible;

            return loc;
        }

		/// <summary>
		/// Checks to see if two TDLocations intersect
		/// </summary>
		/// <param name="otherTDLocation">TDLocation - The TDLocation to test</param>
		/// <param name="stationType">limit ceck to naptans of this station type - Undetermined for all</param>
		/// <returns>true if the NapTANs intersect</returns>
		public bool Intersects(TDLocation otherTDLocation, StationType stationType)
		{
			if	(otherTDLocation != null && otherTDLocation.NaPTANs != null && naptans != null)
			{
				foreach (TDNaptan naptanItem in naptans)
				{
					if	((stationType == StationType.Undetermined) || (stationType == naptanItem.StationType))
					{
						foreach (TDNaptan otherNaptanItem in otherTDLocation.NaPTANs)
						{
							if	(naptanItem.CheckEquals(otherNaptanItem, true))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Checks the location for NaPTANS of the specified station type.
		/// </summary>
		/// <param name="stationType">The StationType to search for</param>
		/// <returns>True if this location has one or more NaPTANS for the given type of station, otherwise false</returns>
		public bool ContainsNaptansForStationType(StationType stationType)
		{
            if (naptans != null)
            {
                foreach (TDNaptan n in naptans)
                {
                    if (n.StationType == stationType)
                        return true;
                }
            }
			return false;
		}
        				
		/// <summary>
		/// Checks whether the specified station type array includes the specified station type.
		/// </summary>
		/// <param name="stationTypes"></param>
		/// <param name="stationType"></param>
		/// <returns></returns>
		private bool StationTypeIncluded(StationType[] stationTypes,StationType stationType)
		{
			foreach (StationType stationTypeItem in stationTypes)
			{
				if (stationTypeItem == stationType)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Take this TDLocation, and convert it into a CJP RequestPlace
		/// </summary>
		/// <returns></returns>
		public RequestPlace ToRequestPlace(TDDateTime dateTime, StationType[] stationTypes, bool publicRequired, bool privateRequired, bool isVia)				
		{
			RequestPlace requestPlaceResult = new RequestPlace();

			requestPlaceResult.givenName = this.Description;
			requestPlaceResult.locality	 = this.Locality;

            // is this a via location
            requestPlaceResult.userSpecifiedVia = isVia;

            // Null check on naptans
            if (naptans == null)
                naptans = new TDNaptan[0];

			if	(StationTypeIncluded(stationTypes,StationType.Undetermined))			
			{
				// If StationType.Undetermined, this is for multimodal request, not Find-A-Flight.
				//  In this case we need to allow for the fact that MajorStationsGazetteer 
				//  and FindNearestStations obtain NaPTANs from the NPTG database, 
				//  which holds "group" airport codes without terminals -- puff them 
				//  up into the whole group ...  (Find-A-Flight does use groups, though). 
				
				// Also, Stops database contains some "terminal zero" entries to 
				//  represent terminal groups -- these need puffing up too ... 

				AirportTerminalNaptans atn = new AirportTerminalNaptans();

                NaPTANs = atn.GetTerminalNaptans(naptans);

			}

			if	(StationTypeIncluded(stationTypes, StationType.Airport))			
			{
				// no locality available yet -- this occurs if the airports have been 
				//  obtained from the AirDataProvider rather than Gazetteers ...
				
				if	(requestPlaceResult.locality == null || requestPlaceResult.locality.Length == 0)
				{
					foreach (TDNaptan naptan in naptans)
					{
						if	(naptan is TDAirportNaptan)
						{
							requestPlaceResult.locality = ((TDAirportNaptan)naptan).AirportLocality;

							if (requestPlaceResult.locality.Length > 0)
							{
								break;
							}
						}
					}
				}
			}

			// for other naptan types, or if not found, look up in Naptan/NPTG database via cache ...

			if	(requestPlaceResult.locality == null || requestPlaceResult.locality.Length == 0)
			{
				foreach (TDNaptan naptan in naptans)
				{
					NaptanCacheEntry nce = NaptanLookup.Get(naptan.Naptan, string.Empty);

					if	(nce != null && nce.Found) 
					{
						this.GridReference = nce.OSGR;

						requestPlaceResult.locality = nce.Locality;
						requestPlaceResult.givenName = nce.Description;

						requestPlaceResult.coordinate = new Coordinate();
						requestPlaceResult.coordinate.easting  = nce.OSGR.Easting;
						requestPlaceResult.coordinate.northing = nce.OSGR.Northing;
						
						break;
					}
				}
			}

			// if a specific station type has been requested, this
			//  must be a trunk request for particular naptans, even 
			//  if the location was originally derived from an OSGR ...

			if	(StationTypeIncluded(stationTypes,StationType.Undetermined))						
			{
				requestPlaceResult.type = this.RequestPlaceType;
			}
			else
			{
				requestPlaceResult.type = RequestPlaceType.NaPTAN;
			}

			// if there is already an OSGR associated with the location 
			//  (as opposed to individual naptans) we use that for the 
			//  RequestPlace OSGR, otherwise we will later fill it with 
			//  the first naptan we find that has a valid OSGR ...

			if  (requestPlaceResult.coordinate == null
					&& this.osGridReference != null 
					&& this.osGridReference.Easting > 0 
					&& this.osGridReference.Northing > 0)
			{
				requestPlaceResult.coordinate = new Coordinate();
				requestPlaceResult.coordinate.easting = this.osGridReference.Easting;
				requestPlaceResult.coordinate.northing = this.osGridReference.Northing;
			}

			if	(publicRequired) 
			{
				// determine how many naptans we need to pass, if we  
				//  are being selective about the types we can use

				int naptanCount = 0;

				if	(StationTypeIncluded(stationTypes,StationType.Undetermined))				
				{
					naptanCount = naptans.Length;
				}
				else
				{
					foreach (TDNaptan naptan in naptans)
					{
						if (StationTypeIncluded(stationTypes,naptan.StationType))						
						{
							naptanCount++;
						}
					}
				}

				bool dummyNaptanNeeded = false;

				// always need at least one naptan, even if it's a dummy empty string,
				//  so that we've got somewhere to hang the time ...
				if	(naptanCount == 0)
				{
					naptanCount = 1;
					dummyNaptanNeeded = true;
				}

				RequestStop[] requestStops = new RequestStop[naptanCount];

				int requiredNaptans = 0;

				if	(dummyNaptanNeeded)
				{
					requestStops[0] = new RequestStop();
					requestStops[0].NaPTANID = string.Empty;

					if	(dateTime != null)
					{
						requestStops[0].timeDate = dateTime.GetDateTime();
					}
				}
				else
				{
					foreach (TDNaptan naptan in naptans)
					{
						if	((StationTypeIncluded(stationTypes,StationType.Undetermined))||
							(StationTypeIncluded(stationTypes,naptan.StationType)))						
						{
							requestStops[requiredNaptans] = new RequestStop();
						
							requestStops[requiredNaptans].NaPTANID = naptan.Naptan;

							if	(dateTime != null)
							{
								requestStops[requiredNaptans].timeDate = dateTime.GetDateTime();
							}
						
							if	(naptan.GridReference.Easting > 0 && naptan.GridReference.Northing > 0) 
							{
								requestStops[requiredNaptans].coordinate = new Coordinate();
								requestStops[requiredNaptans].coordinate.easting  = naptan.GridReference.Easting;
								requestStops[requiredNaptans].coordinate.northing = naptan.GridReference.Northing;
						
								if	(requestPlaceResult.coordinate == null)
								{
									requestPlaceResult.coordinate = new Coordinate();
									requestPlaceResult.coordinate.easting = naptan.GridReference.Easting;
									requestPlaceResult.coordinate.northing = naptan.GridReference.Northing;
								}
							}

							// Add Naptan details (OSGR, locality, name) to cache to avoid having  
							// to look up them again later if the CJP returns them in the result  
							// (but don't cache an entry with no locality). 
							
							string tempLocality = naptan.Locality;

							if	(tempLocality == null || tempLocality.Length == 0)
							{
								tempLocality = requestPlaceResult.locality;
							}

							// RB Altered this to skip the addition to the NaPTAN Cache if naptan
							// is an airport - resolution for IR4364 
							if	(tempLocality != null && tempLocality.Length > 0 && naptan.StationType != StationType.Airport)
							{
								NaptanCache.Add(naptan.Naptan, tempLocality, naptan.Name, naptan.GridReference, true);
							}

							requiredNaptans++;
						}
					}
				}

				requestPlaceResult.stops = requestStops;
			}

			if	(privateRequired)
			{
				// Populate toids for this location using internal method.
				this.PopulateToids();

				// ESRI's queries often return large numbers of duplicate 
				// TOIDs for an OSGR - this code removes duplicates ...
				SortedList sortedToids = new SortedList();

				for  (int i = 0; i < toid.Length; i++)
				{
					try
					{
						sortedToids.Add(toid[i], toid[i]);
					}
					catch (ArgumentException)
					{
						// nothing to do - just means it's a duplicate
					}
				}

				IList editedToidList = sortedToids.GetValueList(); 

				string toidPrefix = Properties.Current[TOID_PREFIX];

				if	(toidPrefix == null) 
				{
					toidPrefix = string.Empty;
				}

				ITN[] requestRoadPoints = new ITN[editedToidList.Count];

				for  (int i = 0; i < editedToidList.Count; i++)
				{
					string currentToid = (string) editedToidList[i];

					requestRoadPoints[i] = new ITN();

					if	((toidPrefix.Length > 0) && !(currentToid.StartsWith(toidPrefix)))
					{
						requestRoadPoints[i].TOID = toidPrefix + currentToid;
					}
					else
					{
						requestRoadPoints[i].TOID = currentToid;
					}

					requestRoadPoints[i].node = false;		// ESRI supplies us with links, not nodes

					if	(dateTime != null)
					{
						requestRoadPoints[i].timeDate = dateTime.GetDateTime();
					}
				}

				requestPlaceResult.roadPoints = requestRoadPoints;
            }

            #region CCN0451 - control inclusion of "GivenName"
            //CCN0451 - control inclusion of "GivenName" in the request dependent on
            //properties, which work as follows:
            //"JourneyControl.FilterGivenNameTag" set to false - switches filtering off
            //   so GivenName tag will be included as always was, if set to true switches 
            //   filtering on so GivenName inclusion will be controlled depending on 
            //   "JourneyControl.GivenNameTagFilterValues" property values.  
            //   Property "JourneyControl.GivenNameTagFilterValues" is a delimited string containing
            //   strings (e.g. "Main Coach") which, if found in the location name, will cause 
            //   the "GivenName" tag to be omitted from the cjp request - resulting in travelines' 
            //   own location names being used in results.

            if (filterGivenNameTag)
            {
                //only for NaPTAN based requests - dont want to do this for co-ordinate / locality
                //based requests as GivenName is all we have to go on for them!
                if (requestPlaceResult.type == RequestPlaceType.NaPTAN)
                {
                    //if the string list is empty that means we just strip GivenName out for 
                    //ALL NaPTAN based requests, otherwise cycle through the collection comparing
                    if (givenNameTagFilterValues.GetLength(0) == 0)
                    {
                        requestPlaceResult.givenName = "";
                    }
                    else
                    {
                        foreach (string filterVal in givenNameTagFilterValues)
                        {
                            if (requestPlaceResult.givenName.Contains(filterVal))
                            {
                                requestPlaceResult.givenName = "";
                                break;
                            }
                        };
                    }
                }
            }
            #endregion

            return requestPlaceResult;
		}

		/// <summary>
		/// Take this TDLocation, and convert it into a CJP RequestPlace.
		/// Overload of the ToRequestPlace method with one station type in order to support the existing code.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <param name="stationType"></param>
		/// <param name="publicRequired"></param>
		/// <param name="privateRequired"></param>
		/// <param name="isVia"></param>
		/// <returns></returns>
		public RequestPlace ToRequestPlace(TDDateTime dateTime, StationType stationType, bool publicRequired, bool privateRequired, bool isVia)		
		{
			StationType[] stationTypes = new StationType[1];
			stationTypes[0] = stationType;

			return ToRequestPlace(dateTime,stationTypes,publicRequired,privateRequired,isVia);
		}

		/// <summary>
		/// Take this TDLocation, and convert it into a CJP RequestPlace.
		/// Overload of the ToRequestPlace method with an additional alternative via 
		/// in order to support the existing code.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <param name="stationType"></param>
		/// <param name="publicRequired"></param>
		/// <param name="privateRequired"></param>
		/// <param name="isAlternativeVia"></param>
		/// <returns></returns>
		public RequestPlace ToRequestPlace(TDDateTime dateTime, StationType stationType, bool publicRequired, bool privateRequired, bool isVia, bool isAlternativeVia)		
		{
			StationType[] stationTypes = new StationType[1];
			stationTypes[0] = stationType;

			RequestPlace place;
			place = ToRequestPlace(dateTime, stationTypes, publicRequired, privateRequired, isVia);
			place.isAlternativeVia = isAlternativeVia;

			return place;
		}

		/// <summary>
		/// Checks to see if two TDLocations are in the same NaPTAN group
		/// </summary>
		/// <param name="otherTDLocation">TDLocation - The TDLocation to test</param>
		/// <returns>true if the NapTANs are in the same group</returns>
		public bool IsMatchingNaPTANGroup( TDLocation otherTDLocation )
		{
			bool match = false;
			if( otherTDLocation != null )
			{
				// Retrieve the naptan ids for both locations
				string[] thisIds = GetNaPTANIds();
				string[] thatIds = otherTDLocation.GetNaPTANIds();
				// If we have a non-coach NaPTAN, look for direct matches between the NaPTANs of the two locations
				if (!thatIds[0].StartsWith(coachPrefix))
				{
					match = AreIntersectingArrays(thisIds, thatIds);
				}
				// Otherwise if we have a coach NaPTAN, do the same but strip off any bay related suffix in the NapTANS
				// (these appear as a non-numeric character)
				else 
				{
					string[] trimmedThisIds = new string[thisIds.Length];
					string[] trimmedThatIds = new string[thatIds.Length];
					char endChar;
					for (int i=0;i<thisIds.Length;++i)
					{
						endChar = thisIds[i].Substring(thisIds[i].Length-1,1).ToCharArray()[0];
						trimmedThisIds[i] = (endChar < '0' || endChar > '9')? thisIds[i].Substring(0, thisIds[i].Length-1): thisIds[i];
					}
					for (int i=0;i<thatIds.Length;++i)
					{
						endChar = thatIds[i].Substring(thatIds[i].Length-1,1).ToCharArray()[0];
						trimmedThatIds[i] = (endChar < '0' || endChar > '9')? thatIds[i].Substring(0, thatIds[i].Length-1): thatIds[i];
					}
					match = AreIntersectingArrays(trimmedThisIds, trimmedThatIds);
				}

				// Finally if the NaPTAN isn't a coach or rail one, we can use the gazeteer to obtain all the NaPTANs in the
				// NaPTAN group and look for a match using the group
				if (!match && !thatIds[0].StartsWith(coachPrefix) && !thatIds[0].StartsWith(railPrefix))
				{
					// Pass this list to the GIS Service to obtain a list of all the NaPTAN nodes that are in the same group.
					IGisQuery query = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
					string dummy = string.Empty;
					QuerySchema result = query.FindStopsInGroupForStops(thisIds);
					string[] groupIds = new string[result.Stops.Rows.Count];
					for (int i=0;i<groupIds.Length;++i) 
					{
						groupIds[i] = ((QuerySchema.StopsRow)result.Stops.Rows[i]).atcocode;
					}
					match = AreIntersectingArrays(groupIds, thatIds);

				}
			}
			return match;
		}

		/// <summary>
		/// Checks a TDLocation object with this one to see if any of the 
		/// toids match.
		/// </summary>
		/// <param name="otherLocation">Location to check against this one.</param>
		/// <returns>True if there are any matching toids.</returns>
		public bool IsMatchingTOIDGroup(TDLocation otherLocation)
		{
			return AreIntersectingArrays(this.toid, otherLocation.toid);
		}

		/// <summary>
		/// Determine if two arrays have common elements.
		/// </summary>
		/// <param name="array1"></param>
		/// <param name="array2"></param>
		/// <returns></returns>
		private bool AreIntersectingArrays(string[] array1, String[] array2)
		{
			bool intersecting = false;
			ArrayList arrayAsList = ArrayList.Adapter(array1);
			foreach (string value in array2) 
			{
				if (arrayAsList.Contains(value))
				{
					intersecting = true;
					break;
				}
			}
			return intersecting;
		}

		/// <summary>
		/// Obtain an array of all the NaPTAN ids of the locations (rather than the TDNaPTANs)
		/// </summary>
		/// <returns>A string array containing NaPTAN ids</returns>
		public string[] GetNaPTANIds()
		{
			string[] ids = new string[NaPTANs.Length];
			for (int i=0; i < ids.Length; ++i) 
			{
				ids[i] = NaPTANs[i].Naptan;									 
			}
			return ids;
		}

        /// <summary>
        /// Looks up and populates the NPTG Admin and District code.
        /// Currently only populates if a NaPTAN exists
        /// </summary>
        public void PopulateAdminDistrictCode()
        {
            if (nptgAdminDistrict == null || !nptgAdminDistrict.IsValid())
            {
                string adminCode = string.Empty;
                string districtCode = string.Empty;
                                
                if (naptans != null && naptans.Length >= 1)
                {
                    string naptan = naptans[0].Naptan;
                    try
                    {
                        

                        IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                        NPTGInfo nptgInfo = gisQuery.GetNPTGInfoForNaPTAN(naptan);

                        if (nptgInfo != null)
                        {
                            adminCode = nptgInfo.AdminAreaID;
                            districtCode = nptgInfo.DistrictID;
                        }

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                string.Format("GIS query call GetNPTGInfoForNaPTAN to update NPTG Admin and District code for location[{0}] with result[{1},{2}]",
                                ID, adminCode, districtCode)));
                        }
                    }
                    catch (Exception ex)
                    {
                        // GISQuery unavailable 
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning,
                            string.Format("GIS query error populating NPTG Admin and District code for location[{0}]. Message: {1}",
                                naptan, ex.Message)));
                    }
                }
                else if (stopType == TDStopType.Locality && !string.IsNullOrEmpty(locality))
                {
                    try
                    {
                        IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                        LocalityNameInfo localityInfo = gisQuery.GetLocalityInfoForNatGazID(locality);

                        if (localityInfo != null)
                        {
                            adminCode = localityInfo.AdminAreaID;
                            districtCode = localityInfo.DistrictID;
                        }

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                string.Format("GIS query call GetLocalityInfoForNatGazID to update NPTG Admin and District code for location[{0}] with result[{1},{2}]",
                                ID, adminCode, districtCode)));
                        }
                    }
                    catch (Exception ex)
                    {
                        // GISQuery unavailable 
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning,
                            string.Format("GIS query error populating NPTG Admin and District code for location[{0}]. Message: {1}",
                                ID, ex.Message)));
                    }
                }

                nptgAdminDistrict = new NPTGAdminDistrict(adminCode, districtCode);
            }
        }

		/// <summary>
		/// Looks up and populates toids on this object. Location must have a valid
		/// grid reference and no toids already populated.
		/// </summary>
		public void PopulateToids()
		{
			// Dont overwrite existing TOID's and only do this if
			// there is a valid grid reference.
			if (this.toid.Length == 0 && this.GridReference.IsValid)
			{
				// Perform GIS query.
				IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
				QuerySchema gisResult = gisQuery.FindNearestITNs(this.GridReference.Easting, this.GridReference.Northing);

				// Resize toid string array based on result of query.
				this.toid = new string[gisResult.ITN.Rows.Count];

				// Loop through returned toids and update string array.
				for ( int i=0; i < gisResult.ITN.Rows.Count; i++)
				{
					QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
					this.toid[i] = row.toid;
				}

				// Trace output.
				if (TDTraceSwitch.TraceVerbose)
				{
					StringBuilder logMsg = new StringBuilder();
					logMsg.Append("GetLocationDetails : description = " + this.Description + " -- ");
					logMsg.Append(gisResult.ITN.Rows.Count + " TOIDs: ");

					foreach (string td in toid)
					{
						logMsg.Append(td);
						logMsg.Append(" ");
					}

					Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()) );
				}
			}
		}

        /// <summary>
        /// Looks up and populates the Point closet to the toid for this object. Location must have a valid
        /// grid reference. FindNearestITN query is performed to obtain a toid based on the grid reference
        /// </summary>
        public void PopulatePoint()
        {
            this.PopulatePoint(true);
        }

        /// <summary>
        /// Looks up and populates the Point closet to the toid for this object. Location must have a valid
        /// grid reference. FindNearestITN query is performed to obtain a toid based on the grid reference
        /// </summary>
        /// <param name="useFindPointOnToid">Flag which sets the Point using the FindNearestPointOnToid method.
        /// False will set the Point to be this locations GridReference</param>
        public void PopulatePoint(bool useFindPointOnToid)
        {
            // Only do this if there is a valid grid reference.
            if (this.GridReference.IsValid)
            {
                if (useFindPointOnToid)
                {
                    string[] toidsFoundForLocation;

                    // Perform GIS query.
                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    // Get the Toid
                    QuerySchema gisResult = gisQuery.FindNearestITN(this.GridReference.Easting,
                                                                this.GridReference.Northing,
                                                                this.AddressToMatch,
                                                                true);

                    // Resize toid string array based on result of query.
                    toidsFoundForLocation = new string[gisResult.ITN.Rows.Count];

                    for (int i = 0; i < gisResult.ITN.Rows.Count; i++)
                    {
                        QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
                        toidsFoundForLocation[i] = row.toid;
                    }

                    #region Log output
                    // Trace output.
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        StringBuilder logMsg = new StringBuilder();

                        logMsg.Append("GetLocationDetails: description = " + this.Description
                            + " address to match = " + this.AddressToMatch + " -- ");
                        logMsg.Append(gisResult.ITN.Rows.Count + " TOIDs: ");

                        foreach (string td in toidsFoundForLocation)
                        {
                            logMsg.Append(td);
                            logMsg.Append(" ");
                        }

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                    }
                    #endregion

                    // Get the Point for the toid
                    if (toidsFoundForLocation.Length > 0)
                    {
                        Point pointForToid = gisQuery.FindNearestPointOnTOID(this.GridReference.Easting, this.GridReference.Northing, toidsFoundForLocation[0]);

                        this.point = pointForToid;

                        #region Log output
                        // Trace output.
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            StringBuilder logMsg = new StringBuilder();

                            logMsg.Append("GetPointOnTOID: easting: ");
                            logMsg.Append(this.GridReference.Easting);
                            logMsg.Append(" northing: ");
                            logMsg.Append(this.GridReference.Northing);
                            logMsg.Append(" toid: ");
                            logMsg.Append(toidsFoundForLocation[0]);
                            logMsg.Append(" Point found: ");
                            logMsg.Append(pointForToid.X.ToString() + "," + pointForToid.Y.ToString());

                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                        }
                        #endregion
                    }
                }
                else
                {
                    this.point = new Point(this.GridReference.Easting, this.GridReference.Northing);

                    #region Log output
                    // Trace output.
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        StringBuilder logMsg = new StringBuilder();

                        logMsg.Append("Point used: ");
                        logMsg.Append(point.X.ToString() + "," + point.Y.ToString());

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Returns the Point coordinate rounded to be an OSGR coordinate
        /// </summary>
        /// <returns></returns>
        public OSGridReference PointAsOSGR()
        {
            // if null, then make it the same as the current osgr
            if (point == null)
            {
                point = new Point(osGridReference.Easting, osGridReference.Northing);
            }

            OSGridReference osgr = new OSGridReference();

            osgr.Easting = Convert.ToInt32(Math.Round(point.X, 0));
            osgr.Northing = Convert.ToInt32(Math.Round(point.Y, 0));

            return osgr;
        }

        /// <summary>
        /// Returns the OSGR coordinate as a Point coordinate
        /// </summary>
        /// <returns></returns>
        public Point OSGRAsPoint()
        {
            if (osGridReference == null)
            {
                osGridReference = new OSGridReference(Convert.ToInt32(Math.Round(point.X)), Convert.ToInt32(Math.Round(point.Y)));
            }

            Point p = new Point();
            p.X = Convert.ToDouble(osGridReference.Easting);
            p.Y = Convert.ToDouble(osGridReference.Northing);

            return p;
        }

        /// <summary>
        /// Method which updates the location naptans. If the naptan supplied is detected as an airport,
        /// then the AirDataProvider is used to setup the naptan and description
        /// </summary>
        public void UpdateLocationNaptan(string naptan, string description, OSGridReference osGridReference)
        {
            if (!string.IsNullOrEmpty(naptan))
            {
                string newNaptan = naptan;
                OSGridReference newOsgr = osGridReference;

                // Update this location description if provided
                this.description = (!string.IsNullOrEmpty(description)) ? description : this.description;

                // Check for airport naptan
                if (airPrefix != null && airViaPrefix != null)
                {
                    if (newNaptan.Length == airPrefix.Length + IATA_LENGTH + TERMINAL_LENGTH
                        && newNaptan.StartsWith(airViaPrefix))
                    {
                        AirDataProvider.IAirDataProvider airData = (AirDataProvider.IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

                        // Create the actual naptan using the air data
                        newNaptan = airPrefix + naptan.Substring(IATA_LENGTH + TERMINAL_LENGTH);

                        Airport airport = airData.GetAirportFromNaptan(newNaptan);

                        // If airport found, then use its description as the display name
                        if ((airport != null) && (!string.IsNullOrEmpty(airport.Name)))
                        {
                            this.description = airport.Name;
                        }
                    }
                }

                // Check and update this location osgr
                if ((newOsgr != null) && (newOsgr.IsValid))
                {
                    this.osGridReference = newOsgr;
                }

                // And finally update this locations naptan
                naptans = new TDNaptan[1];
                naptans[0] = new TDNaptan(newNaptan, this.osGridReference, this.description);
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Returns an ID value dependent on TDStopType of the location, 
        /// e.g. Locality location returns the Locality
        /// </summary>
        public string ID
        {
            get 
            {
                switch (stopType)
                {
                    case TDStopType.Air:
                    case TDStopType.Bus:
                    case TDStopType.Coach:
                    case TDStopType.DLR:
                    case TDStopType.Ferry:
                    case TDStopType.LightRail:
                    case TDStopType.Rail:
                    case TDStopType.Underground:
                        if (naptans != null && naptans.Length == 1)
                        {
                            return naptans[0].Naptan;
                        }
                        break;
                    case TDStopType.POI:
                    case TDStopType.Group:
                        return dataSetId;
                    case TDStopType.Locality:
                        return locality;
                    case TDStopType.Unknown:
                        return string.Empty;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Read/Write. DataSet ID value from the TD Location database table used by the TDLocationCache
        /// </summary>
        public string DataSetID
        {
            get { return dataSetId; }
            set { dataSetId = value; }
        }

        /// <summary>
        /// Read/Write. Parent ID value from the TD Location database table used by the TDLocationCache
        /// </summary>
        public string ParentID
        {
            get { return parentId; }
            set { parentId = value; }
        }

        /// <summary>
        /// Read/Write property.
        /// </summary>
        public string Description
		{
			get { return description; }
			set { description = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public OSGridReference GridReference
		{
			get { return osGridReference; }
			set { osGridReference = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        /// <summary>
        /// Read/Write property. Holds the latitude longitude value.
        /// This is currently only set for use by the Cycle Planner - GPX download page
        /// </summary>
        public LatitudeLongitude LatitudeLongitudeCoordinate
        {
            get { return latitudeLongitude; }
            set { latitudeLongitude = value; }
        }

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public string Locality
		{
			get { return locality; }
			set { locality = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public TDNaptan[] NaPTANs
		{
			get { return naptans; }
			set { naptans = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public string[] Toid
		{
			get { return toid; }
			set { toid = value; }
		}

        /// <summary>
        /// Read/Write. NPTG Admin and District code
        /// </summary>
        public NPTGAdminDistrict AdminDistrict
        {
            get { return nptgAdminDistrict; }
            set { nptgAdminDistrict = value; }
        }

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public SearchType SearchType
		{
			get { return locationSearchType; }
			set { locationSearchType = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public RequestPlaceType RequestPlaceType
		{
			get { return requestType; }
			set { requestType = value; }
		}

        /// <summary>
        /// Read/Write. TDStopType defaults to unknown.
        /// Used by the TDLocationCache to identify the stop location type to allow filtering on a specific type
        /// </summary>
        public TDStopType StopType
        {
            get { return stopType; }
            set { stopType = value; }
        }

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public TDLocationStatus Status
		{
			get { return locationStatus;}
			set { locationStatus = value;}
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public bool PartPostcode
		{
			get	{ return boolPartPostcode;	}
			set	{ boolPartPostcode = value;	}
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public double PartPostcodeMaxX
		{
			get { return dblPartPostcodeMaxX; }
			set	{ dblPartPostcodeMaxX = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public double PartPostcodeMaxY
		{
			get { return dblPartPostcodeMaxY; }
			set	{ dblPartPostcodeMaxY = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public double PartPostcodeMinX
		{
			get { return dblPartPostcodeMinX; }
			set	{ dblPartPostcodeMinX = value; }
		}

        /// <summary>
        /// Read/Write property.
        /// </summary>
		public double PartPostcodeMinY
		{
			get { return dblPartPostcodeMinY; }
			set	{ dblPartPostcodeMinY = value; }
		}

		/// <summary>
		/// Read-write property. Stores the park and ride info object
		/// </summary>
		public ParkAndRideInfo ParkAndRideScheme
		{
			get { return parkAndRideScheme; }
			set { parkAndRideScheme = value;}
		}

		/// <summary>
		/// Read-write property. Stores the CarParkInfo object
		/// </summary>
		public CarParkInfo CarPark
		{
			get { return carPark; }
			set { carPark = value;}
		}

		/// <summary>
		/// Read-write property. Stores the CarParkInfo object
		/// </summary>
		public CarPark CarParking
		{
			get { return carParking; }
			set { carParking = value;}
		}

		/// <summary>
		/// Read/write property returning string array of Car Park References 
		/// </summary>
		public string[] CarParkReferences
		{
			get { return carParkRefs; }
			set { carParkRefs = value; }
		}

		/// <summary>
		/// City interchange points. [r/w]
		/// </summary>
		public CityInterchange[] CityInterchanges
		{
			get { return cityInterchanges; }
			set { cityInterchanges = value; }
		}	
	
		/// <summary>
		/// String to be used in address matching for car journey start/end TOIDs [r/w]
		/// </summary>
		public string AddressToMatch
		{
			get { return addressToMatch; }
			set { addressToMatch = value; }
        }

        /// <summary>
        /// Read/write property providing city id of the location
        /// </summary>
        public string CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        /// <summary>
        /// Read/write property providing country code of the location
        /// </summary>
        public TDCountry Country
        {
            get { return country; }
            set { country = value; }
        }

        /// <summary>
        /// Read/write property indicating if this location is valid for an accessible journey plan.
        /// Default is false
        /// </summary>
        public bool Accessible 
        {
            get { return accessible; }
            set { accessible = value; }
        }

        #endregion
    }
}
