// *********************************************** 
// NAME                 : LocationSearch.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Provides the public interface to the LocationService component. It delegates the actual searching functions to the relevant ITDGazetteer class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocationSearch.cs-arc  $ 
//
//   Rev 1.16   Jan 29 2013 13:03:44   mmodi
//Updated to log coordinates for find nearest accessible search
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.15   Jan 17 2013 11:07:00   mmodi
//Corrected search distance override
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.14   Jan 15 2013 13:30:28   mmodi
//Check for accessible max results value before making gis call
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.13   Jan 15 2013 10:29:30   mmodi
//Specify a search distance override for find accessible stop
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.12   Jan 09 2013 16:09:06   mmodi
//Accessible search distance properties update
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.11   Jan 09 2013 11:41:20   mmodi
//Updated to perform GIS query to find accessible locations
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.10   Jan 04 2013 15:33:02   mmodi
//Backed out populating locality and gridreference for accessible location
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.9   Dec 18 2012 16:55:02   dlane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Dec 06 2012 09:09:10   mmodi
//Updated to use temporary method to return accessible locations from location service class
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Dec 05 2012 14:10:48   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Aug 28 2012 10:19:54   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.5   Sep 10 2009 11:22:10   MTurner
//Changes for ATO598 following code review
//
//   Rev 1.4   Jun 08 2009 16:19:46   mturner
//Further changes for ATO598
//
//   Rev 1.3   May 22 2009 14:46:22   mturner
//Updates for ATO598
//Resolution for 5291: Mobile & DigiTV web service changes
//
//   Rev 1.2   Nov 29 2007 12:38:42   mturner
//Changes to remove .Net2 compiler warnings
//
//   Rev 1.1   Nov 29 2007 10:52:56   mturner
//Updated for Del 9.8
//
//   Rev 1.32   Nov 08 2007 14:22:28   mmodi
//Updated FindCarParks to accept a max number of car parks value
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.31   Jun 26 2007 16:14:18   mmodi
//Added code to do a second car park search if first fails
//Resolution for 4457: 9.7 - Amendments to car parks
//
//   Rev 1.30   Oct 06 2006 11:59:40   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.29.1.0   Aug 14 2006 10:56:04   esevern
//Added FindCarParks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.29   Apr 20 2006 15:51:52   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.28   Feb 23 2006 19:15:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.27.1.2   Feb 08 2006 15:34:40   RWilby
//Updated for code review
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.27.1.1   Feb 03 2006 11:28:22   RWilby
//Updated for Code Review
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.27.1.0   Jan 19 2006 09:34:24   RWilby
//Changed for DigiTV Exposed Services. Added New methods: FindBusStops and FindPostcode. Created new overloads for FindStations method. Changed FilterNaptans method to create naptans of type TDRailNaptan for rail station naptans.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.27   Apr 15 2005 15:17:58   rscott
//Code added for IR1980
//
//   Rev 1.26   Apr 13 2005 09:10:16   rscott
//DEL 7 Additional Tasks - IR1978 enhancements added - reject single word address.
//
//   Rev 1.25   Apr 07 2005 16:23:52   rscott
//Updated with DEL7 additional task outlined in IR1977.
//
//   Rev 1.24   Sep 11 2004 13:16:50   RPhilpott
//Minor tweaks to case-manipulation of rail station names.
//
//   Rev 1.23   Sep 10 2004 19:13:32   RPhilpott
//Use more meaningful name for DisableLocalityQuery().
//Resolution for 1570: Find-A-Car  --  unnecessary calls to FindNearestLocality()
//
//   Rev 1.22   Sep 10 2004 15:35:50   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.21   Aug 20 2004 15:01:50   passuied
//Added implementation for FindStations optimisation. When some stations were found but not for all station types, store the existing result and update the given station type to search for the missing station types only.
//Resolution for 1415: FindNearest Station/Airport Optimisation
//
//   Rev 1.20   Aug 18 2004 14:17:38   RPhilpott
//Temporary bodge to exclude coach stations from FindNearestStations() searches.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//
//   Rev 1.19   Aug 13 2004 14:17:42   passuied
//Changes for FindA station distinct error message
//Resolution for 1309: Find a Coach - Wrong error message displayed when no coach station found
//
//   Rev 1.18   Jul 22 2004 16:12:12   RPhilpott
//Pull most of the FindStations code into the LocationService.  
//
//   Rev 1.17   Jul 12 2004 18:56:50   JHaydock
//DEL 5.4.7 Merge: IR 1089
//
//   Rev 1.16   Jul 09 2004 13:09:12   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.15   Jun 03 2004 16:14:18   passuied
//changes for integration with FindFlight
//
//   Rev 1.14   May 28 2004 17:52:20   passuied
//update as part of FindStation development
//
//   Rev 1.13   May 19 2004 14:03:46   acaunt
//LocationFixed property added to indicate if a user can or cannot modify a location. Change for ExtendJourney
//
//   Rev 1.12   May 14 2004 15:29:16   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.11   Mar 11 2004 09:15:58   COwczarek
//Prevent DecrementLevel method removing first level of drillable search
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.10   Mar 03 2004 15:22:54   COwczarek
//LocationSearch is now associated with an ArrayList of LocationQueryResult objects rather than an array. Elements are removed from the list to support backward navigation through a hierarchic search.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.9   Dec 16 2003 11:53:28   PNorell
//Updated for dealing with error when chosing a desolate place with no naptans close from the map.
//
//   Rev 1.8   Dec 03 2003 12:21:26   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.7   Oct 15 2003 11:52:00   passuied
//removed SearchType init in ClearAll method. useless and causes pbs when this search type is unavailable for  a particular location (public via)
//
//   Rev 1.6   Oct 14 2003 12:48:20   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.5   Sep 25 2003 12:38:18   passuied
//Added property and allowed locality to get location details when the location has children
//
//   Rev 1.4   Sep 22 2003 17:31:24   passuied
//made all objects serializable
//
//   Rev 1.3   Sep 22 2003 10:29:26   passuied
//changed initialisation of LocationQueryResult
//
//   Rev 1.2   Sep 15 2003 10:51:26   passuied
//Design changes
//
//   Rev 1.1   Sep 09 2003 17:23:50   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:32   passuied
//Initial Revision
//
//   Rev 1.0   Aug 29 2003 11:24:06   passuied
//Initial Revision


using System;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Configuration;
using System.Globalization;

using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.Common;
using TransportDirect.UserPortal.AdditionalDataModule;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Enumeration for station types. Needed for FindStation functionality
	/// </summary>
	public enum StationType
	{
		Undetermined,
		Airport,
		Rail,
		Coach,
		UndeterminedNoGroup,
		AirportNoGroup,
		RailNoGroup,
		CoachNoGroup
	}

    /// <summary>
    /// Provides the public interface to the LocationService component. It delegates the actual searching functions to the relevant ITDGazetteer class.
    /// </summary>
    [Serializable()]
    public class LocationSearch
    {
        #region Member variables declaration
        private ITDGazetteer gazetteerService;
        private ArrayList queryResults; // collection of LocationQueryResult
        private List<TDLocation> ambiguityResultsFromLocationCache;
        private string stringInputText = string.Empty;
        private SearchType type;
        private bool boolFuzzy = false;
        private int intMaxWalkingDistance = 0;
		private bool isDisableGisQuery = false;
		private bool locationFixed = false;
		private bool boolVague = false;
		private bool boolSingleWord = false;
		private bool boolNoGroup = false;
        private static Dictionary<string, string> stopsIdentifiers;
        private bool javascriptEnabled = false;
        #endregion

		#region Constants
		public const string KeyInitRadiusProperties = "FindStationResults.InitRadius.{0}";
		public const string KeyMaxRadiusProperties = "FindStationResults.MaxRadius.{0}";
		public const string KeyRadiusIncrementProperties = "FindStationResults.RadiusIncrement.{0}";
		public const string KeyMaxStationReturn = "FindStationResults.ReturnedStationsMaxNumber.{0}";
		
		public const string KeyCarRadius1Init = "FindNearestCarParks.Radius1.Initial";
		public const string KeyCarRadius1Max = "FindNearestCarParks.Radius1.Maximum";
		public const string KeyCarRadius2Init = "FindNearestCarParks.Radius2.Initial";
		public const string KeyCarRadius2Max = "FindNearestCarParks.Radius2.Maximum";		
		public const string KeyMaxCarParksReturn = "FindNearestCarParks.NumberCarParksReturned";
        
        // Stored Procs
        private const string SPGetIdentifiers = "GetIdentifiers";

		#endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationSearch()
        {
            queryResults = new ArrayList();
            ambiguityResultsFromLocationCache =  new List<TDLocation>();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read-write property. Returns the input text of the search
        /// </summary>
        public string InputText
        {
            get
            {
                return stringInputText;
            }

            set
            {
                stringInputText = value;
            }
        }

        /// <summary>
        /// Read-write property. Returns the Search type of the search
        /// </summary>
        public SearchType SearchType
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
   
		/// <summary>
		/// Read-write propery. Returns if the Search is to use Grouping or not.
		/// For IR1980
		/// </summary>
		public bool NoGroup
		{
			get
			{
				return boolNoGroup;
			}
			set
			{
				boolNoGroup = value;
			}
		}

		/// <summary>
		/// Read-write propery. Returns if the Search is vague or not
		/// </summary>
		public bool VagueSearch
		{
			get
			{
				return boolVague;
			}
			set
			{
				boolVague = value;
			}
		}

		/// <summary>
		/// Read-write propery. Returns if the Search is vague or not
		/// </summary>
		public bool SingleWord
		{
			get
			{
				return boolSingleWord;
			}
			set
			{
				boolSingleWord = value;
			}
		}

        /// <summary>
        /// Read-write propery. Returns if the Search is fuzzy of not
        /// </summary>
        public bool FuzzySearch
        {
            get
            {
                return boolFuzzy;
            }
            set
            {
                boolFuzzy = value;
            }
        }

		/// <summary>
		/// Read-write property. It determines whether the Location associated with the 
		/// LocationSearch is fixed (and so can't be changed by the user) or not. 
		/// The default is false (so the Location can be changed).
		/// </summary>
		public bool LocationFixed
		{
			get 
			{
				return locationFixed;
			}
			set
			{
				locationFixed = value;
			}
		}

        /// <summary>
        /// The current level number for a hierarchical search (zero based).
        /// A value of -1 indicates no query result has yet been associated 
        /// with this search
        /// </summary>
        public int CurrentLevel
        {
            get
            {
                return queryResults.Count-1;
            }
        }

        /// <summary>
        /// Read-write propery. Returns if the MaxWalkingDistance
        /// </summary>
        public int MaxWalkingDistance
        {
            get
            {
                return intMaxWalkingDistance;
            }

            set
            {
                intMaxWalkingDistance = value;
            }
        }

        /// <summary>
        /// Read-only property. indicates if the search is hiearchic
        /// </summary>
        public bool SupportHierarchic
        {
            get
            {
                return gazetteerService != null && gazetteerService.SupportHierarchicSearch;
            }
        }

        /// <summary>
        /// Read/Write. Indicates if javascript is enabled on the location control,
        /// default is false
        /// </summary>
        public bool JavascriptEnabled
        {
            get { return javascriptEnabled; }
            set { javascriptEnabled = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method called to start an new Location search
        /// </summary>
        /// <param name="text">Input text string</param>
        /// <param name="type">Search type</param>
        /// <param name="fuzzy">Indicates if search is fuzzy</param>
        /// <param name="maxDistance">Max Walking distance</param>
        /// <param name="sessionID">Session ID</param>
        /// <param name="userLoggedOn">Indicates if user is logged on</param>
        /// <returns>Return a list of choices to be displayed to the user</returns>
        public LocationChoiceList StartSearch(  string text, 
                                                SearchType type, 
                                                bool fuzzy,
                                                int maxDistance, 
                                                string sessionID,
                                                bool userLoggedOn)
        {

			return StartSearch(text, type, fuzzy, maxDistance, sessionID, userLoggedOn, StationType.Undetermined);
        }

		/// <summary>
		/// Method called to start an new Location search
		/// </summary>
		/// <param name="text">Input text string</param>
		/// <param name="type">Search type</param>
		/// <param name="fuzzy">Indicates if search is fuzzy</param>
		/// <param name="maxDistance">Max Walking distance</param>
		/// <param name="sessionID">Session ID</param>
		/// <param name="userLoggedOn">Indicates if user is logged on</param>
		/// <param name="stationType">StationType the search has to apply to</param>
		/// <returns>Return a list of choices to be displayed to the user</returns>
		public LocationChoiceList StartSearch(  string text, 
			SearchType type, 
			bool fuzzy,
			int maxDistance, 
			string sessionID,
			bool userLoggedOn,
			StationType stationType)
		{
			// Initialisation
			stringInputText = text;


			// if text contains a postcode (alone or with other text), or is
			// a partial postode, update the search type to AddressPostCode
			if (PostcodeSyntaxChecker.IsPostCode(text)
				|| PostcodeSyntaxChecker.ContainsPostcode(text)
				|| PostcodeSyntaxChecker.IsPartPostCode(text))
				type = SearchType.AddressPostCode;

			this.type = type;
			boolFuzzy = fuzzy;
			intMaxWalkingDistance = maxDistance;

			// Get ITDGazetteerFactory from Service Discovery
			ITDGazetteerFactory factory = (ITDGazetteerFactory) TDServiceDiscovery.Current[ServiceDiscoveryKey.GazetteerFactory];

			// Get ITDGazetteer
			gazetteerService = factory.Gazetteer(type, sessionID, userLoggedOn, stationType );

			// initialise Query Results container
			queryResults.Add(gazetteerService.FindLocation(text, fuzzy));

			//if the response from the gazetteer is vague then send back a 
			//dummy LocationChoiceList with a vague flag
			if (((LocationQueryResult)queryResults[0]).IsVague == true)
			{
				LocationChoiceList lcl = new LocationChoiceList();
				lcl.IsVague = true;
				return lcl;
			}

			//if the address query text is a single word then return 
			if (((LocationQueryResult)queryResults[0]).IsSingleWordAddress == true)
			{
				LocationChoiceList lcl = new LocationChoiceList();
				lcl.IsSingleWordAddress = true;
				return lcl;
			}

            
			return ((LocationQueryResult)queryResults[0]).LocationChoiceList;

		}
		
        
        /// <summary>
        /// Drills down into the search by passing a choice 
        /// </summary>
        /// <param name="level">Level to start drill from</param></param>
        /// <param name="choice">Choice to drill with</param>
        /// <returns>Returns a list of choices to be displayed to the user</returns>
        public LocationChoiceList DrillDown ( int level, LocationChoice choice )
        {
            
			LocationQueryResult result = gazetteerService.DrillDown (
                                    stringInputText, 
                                    boolFuzzy,
                                    ((LocationQueryResult)queryResults[level]).PickListUsed,
                                    ((LocationQueryResult)queryResults[level]).QueryReference,
                                    choice);

            // If the search is hierarchical, add the query result to the
            // query results collection so that navigation if the hierarchy
            // is possible
            if (gazetteerService.SupportHierarchicSearch)
            {
                queryResults.Add(result);
                result.ParentChoice = choice;           
            } 
			else 
			{
                // A non-hierarchical search will only ever contain one query result
                // since navigation is not supported in this case
                queryResults.Clear();
                queryResults.Add(result);
            }

            return result.LocationChoiceList;
        }

		/// <summary>
        /// Disable the GetLocationDetails made when a location becomes valid
        /// Needed for performance issues in FindAirports Functionality
        /// </summary>
		public void DisableGisQuery()
		{
			isDisableGisQuery = true;
		}
        
		/// <summary>
        /// Get the location details for a definite choice
        /// </summary>
        /// <param name="choice">choice</param>
        /// <returns>Returns a TDLocation object giving location information</returns>
        public void GetLocationDetails (ref TDLocation location,  LocationChoice choice)
        {
            if  (gazetteerService != null)
			{
				gazetteerService.GetLocationDetails(
					ref location,
					stringInputText, 
					boolFuzzy, 
					((LocationQueryResult)queryResults[CurrentLevel]).PickListUsed,
					((LocationQueryResult)queryResults[CurrentLevel]).QueryReference, 
					choice, 
					intMaxWalkingDistance,
					isDisableGisQuery);
				
            }
        }

        public void RefreshLocationDetails(ref TDLocation location)
        {
            if( gazetteerService == null )
            {
                // If gazetterservice is null, try using AddressPostcodeGazetter for the population
                // of naptans and toids.
                // This is not stored in the main gazetter service as the location and location search
                // is not associated with this service for normal handling
                ITDGazetteerFactory factory = (ITDGazetteerFactory)TDServiceDiscovery.Current[ ServiceDiscoveryKey.GazetteerFactory ];
                ITDGazetteer serv = factory.Gazetteer(SearchType.AddressPostCode, string.Empty, false, StationType.Undetermined);
				serv.PopulateLocality(ref location);
				serv.PopulateToids(ref location);
			}
            else
            {
				gazetteerService.PopulateLocality(ref location);
				gazetteerService.PopulateToids(ref location);
			}
        }

		/// <summary>
		/// Get a the list of choices for the given level
		/// </summary>
		/// <param name="level">level</param>
		/// <returns>Returns the list of choices of the given level</returns>
		public LocationChoiceList GetCurrentChoices( int level )
		{
			if (level < queryResults.Count && queryResults[level] != null)
				return ((LocationQueryResult)queryResults[level]).LocationChoiceList;
			else
				return null;
		}

		/// <summary>
		/// Get the query result object for the given level
		/// </summary>
		/// <param name="level">level</param>
		/// <returns>Returns the query result object the given level</returns>
		public LocationQueryResult GetQueryResult( int level ) 
		{
			return (LocationQueryResult)queryResults[level];
		}

        /// <summary>
        /// Adds a list of TDLocations found from the "auto-suggest" ambiguous search 
        /// using the TDLocationCache
        /// </summary>
        /// <param name="ambiguityLocations"></param>
        public void AddAmbiguitySearchResult(List<TDLocation> ambiguityLocations)
        {
            if (ambiguityLocations != null)
            {
                ambiguityResultsFromLocationCache = ambiguityLocations;
            }
        }

        /// <summary>
        /// Returns a readonly list of TDLocations found from the "auto-suggest" ambiguous search
        /// using the TDLocationCache
        /// </summary>
        /// <returns></returns>
        public IList<TDLocation> GetAmbiguitySearchResult()
        {
            if (ambiguityResultsFromLocationCache != null)
            {
                return ambiguityResultsFromLocationCache.AsReadOnly();
            }
            else
                return new List<TDLocation>().AsReadOnly();
        }

		/// <summary>
		/// Reset the search results and all user inputs
		/// </summary>
		public void ClearAll()
		{
			// Reset
			stringInputText = string.Empty;
			boolFuzzy = false;
			intMaxWalkingDistance = 0;

            ClearSearch();
		}

		/// <summary>
		/// Reset the search results only
		/// </summary>
		public void ClearSearch()
		{
			// initialise Query Results collection
			if (queryResults != null) 
			{
				queryResults.Clear();
			}

            if (ambiguityResultsFromLocationCache != null)
            {
                ambiguityResultsFromLocationCache.Clear();
            }
		}

		/// <summary>
		/// Removes the search results at the lowest level of a drillable
		/// search. The search results for the first level cannot be removed
		/// using this method.
		/// </summary>
		public void DecrementLevel()
		{
			if (queryResults.Count > 1)
			{
				queryResults.RemoveAt(queryResults.Count-1);
			}
		}

        /// <summary>
		/// Compiles a string to be used as a bus stop name.
        /// The logic used is based on the criteria defined in ATO598
		/// </summary>
        public static string getStopName(QuerySchema.StopsRow row)
		{
            string tempString = string.Empty;
            string tempIdentifier = string.Empty;
            bool oppIdentifierFound = false;

            IGisQuery query = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
            // Obtain locality name from TDInteractiveMapping
            LocalityNameInfo locInfo = query.GetLocalityInfoForNatGazID(row.natgazid);
            string locName = locInfo.LocalityName;

            if (!string.IsNullOrEmpty(locName))
            {
                locName = locName + ", ";
            }

            // Check if identifier denotes stop is Opposite something
            if (row.identifier == "OPP")
            {
                oppIdentifierFound = true;
            }

            // Check if identifier is in list of idenitifiers that need to be replaced 
            // with a more user friendly string. If it is perform the replacement.
            if (stopsIdentifiers != null && stopsIdentifiers.ContainsKey(row.identifier))
            {
                tempIdentifier = stopsIdentifiers[row.identifier];
            }
            else
            {
                // use original identifier
                tempIdentifier = row.identifier;
            }

            if (oppIdentifierFound)
            {
                if (row.street.Equals(row.commonname, StringComparison.OrdinalIgnoreCase))
                {
                    tempString = locName + tempIdentifier + ", " + row.commonname;
                    tempString = tempIdentifier + ", " + row.commonname;
                }
                else
                {
                    tempString = locName + row.street + ", " + tempIdentifier + ", " + row.commonname;
                    tempString = row.street + ", " + tempIdentifier + ", " + row.commonname;
                }
            }
            else
            {
                if (row.street.Equals(row.commonname, StringComparison.OrdinalIgnoreCase))
                {
                    tempString = locName + row.commonname + ", " + tempIdentifier;
                    tempString = row.commonname + ", " + tempIdentifier;
                }
                else
                {
                    tempString = locName + row.street + ", " + row.commonname + ", " + tempIdentifier;
                    tempString = row.street + ", " + row.commonname + ", " + tempIdentifier;
                }
            }
            
            // Remove certain banned sub strings frrom the text displayed to an end user.
            StringBuilder sb = new StringBuilder(tempString);
            sb.Replace("-", null);
            sb.Replace("*", null);
            sb.Replace("_", null);
            sb.Replace("N/A", null);
            sb.Replace("TBA", null);
            sb.Replace("?", null);
            sb.Replace("/", null);
            sb.Replace("/\\", null);
            sb.Replace("No Name", null);
            //If the entire first field was made up of banned chars remove the comma delimiter
            sb.Replace(", ", null, 0, 2);
            //If the entire last field was made up of banned chars remove the comma delimiter
            sb.Replace(", ", null, sb.Length - 2, 2);
            return sb.ToString();
		}
		
        #endregion

		#region Public Static methods

        static public void SetStopIdentifiers(Dictionary<string, string> stopsDictionary)
        {
            stopsIdentifiers = stopsDictionary;
        }

		/// <summary>
		/// Populates TDLocation.NaPTANS property with NaPTANs for nearest bus stops to TDLocation.GridReference 
		/// </summary>
		/// <param name="thisLocation">Pass TDLocation object by Reference</param>
		/// <param name="maxReturn">Maximum number of results to return</param>
		static public void FindBusStops(ref TDLocation thisLocation,int maxReturn)
		{
			int SearchRadius = 0; //in metres

			try
			{
				//Retrieve SearchRadius from property service
				SearchRadius = Convert.ToInt32(Properties.Current["FindBusStops.SearchRadius"],CultureInfo.InvariantCulture);		
			}
			catch
			{
				const string SearchRadiusPropertyError = "Missing/Bad format for FindBusStops.SearchRadius property";

				OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, SearchRadiusPropertyError);
				Logger.Write(operationalEvent);

				throw new TDException(SearchRadiusPropertyError,true,TDExceptionIdentifier.PSMissingProperty);
			}

			//Obtain GisQuery service from service discovery
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
			
			//Perform gisQuery to retrieve stop Naptans within the specified radius of the given point
			QuerySchema gisQueryResult = 
				gisQuery.FindStopsInRadius(
				thisLocation.GridReference.Easting,thisLocation.GridReference.Northing,SearchRadius,new string[]{"BCS","BCT"});

			//Create TDNaptan array of TDBusStopNaptan objects using data from the gisQuery
			TDNaptan[] results = new TDNaptan[gisQueryResult.Stops.Rows.Count];

            Dictionary<string, string> stopsDictionary = new Dictionary<string, string>();
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
            DataSet ds = sqlHelper.GetDataSet(SPGetIdentifiers, new Hashtable());
            DataTable dt = ds.Tables[0];
            DataRow[] rows = dt.Select();
            foreach (DataRow row in rows)
            {
                stopsDictionary.Add(row[0].ToString(),row[1].ToString());
            }

            SetStopIdentifiers(stopsDictionary);
            
            string stopName = string.Empty;
            for (int i = 0; i < gisQueryResult.Stops.Rows.Count; i++)
			{
				QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisQueryResult.Stops.Rows[i];
                stopName = getStopName(row);
				results[i] = new TDBusStopNaptan(
                    new TDNaptan(row.atcocode, new OSGridReference((int)row.X, (int)row.Y), stopName), row.smsnumber);				
			}
			
			ArrayList inputNaptans = new ArrayList();
			inputNaptans.AddRange(results);
			
			//Filter and sort Naptans
			ArrayList filteredNaptans = FilterStandardNaptans(thisLocation.GridReference, inputNaptans, maxReturn, StationType.Undetermined);
 
			//Assign filtered Naptans to the TDLocation
			thisLocation.NaPTANs = (TDNaptan[]) filteredNaptans.ToArray(typeof(TDNaptan));
		}

        /// <summary>
		/// Performs a postcode gazetter search for a full UK postcode
		/// </summary>
		/// <param name="postcode">Full UK Postcode</param>
		/// <param name="sessionid">Sessionid</param>
		/// <param name="userLoggedOn">bool userLoggedOn</param>
		/// <returns>LocationChoiceList</returns>
		static public LocationChoiceList FindPostcode(string postcode,string sessionId, bool userLoggedOn)
		{
			//Check that the postcode string is a full UK postcode else throw exception
			if(!PostcodeSyntaxChecker.IsPostCode(postcode))
			{
				throw new TDException("Invalid Full UK Postcode",false,TDExceptionIdentifier.LSInvalidFullUKPostcode);
			}

			//Obtain AddressPostcodeGazetteer service from service discovery
			ITDGazetteerFactory factory = (ITDGazetteerFactory)TDServiceDiscovery.Current[ ServiceDiscoveryKey.GazetteerFactory ];
			ITDGazetteer addressPostcodeGazetteer = factory.Gazetteer(SearchType.AddressPostCode,sessionId,userLoggedOn,StationType.Undetermined);
			
			//Perform Postcode gazetter search
			LocationQueryResult locationQueryResult = addressPostcodeGazetteer.FindLocation(postcode,false);
			
			return locationQueryResult.LocationChoiceList;
		}

		/// <summary>
		/// Searches for stations of a selected given types within radius.
		/// 
		/// This overload can be called when the DirectFlight flag is not
		/// relevant (eg, from Trunk requests that are not from FindAFlight). 
		/// </summary>
		/// <param name="thisLocation"></param>
		/// <param name="stationTypes"></param>
		static public int FindStations(ref TDLocation thisLocation, StationType[] stationTypes)
		{
			return FindStations(ref thisLocation, stationTypes, null, null, false, false, false);
		}
				
		/// <summary>
		/// Searches for stations of a selected given types within radius (stored in properties)
		/// and loops until it finds the minimum number of stations or the maximum radius is 
		/// reached. If so, it throws an exception.
		/// </summary>
		/// <param name="thisLocation"></param>
		/// <param name="stationTypes"></param>
		/// <param name="originLocation"></param>
		/// <param name="destinationLocation"></param>
		/// <param name="isFromLocation"></param>
		/// <param name="isToLocation"></param>
		/// <param name="isDirectFlight"></param>
		static public int FindStations(
			ref TDLocation thisLocation,
			StationType[] stationTypes,
			TDLocation originLocation,
			TDLocation destinationLocation,
			bool isFromLocation,
			bool isToLocation,
			bool isDirectFlight)
		{
			return FindStations(ref thisLocation, stationTypes,originLocation, destinationLocation, isFromLocation,
				isToLocation, isDirectFlight,0,true);
		}

		/// <summary>
		/// Searches for stations of a selected given types within radius.
		/// 
		/// This overloaded FindStations accepts the maximum number of results to return
		/// Instead of retrieving the maximum number of results from the Properties service
		/// </summary>
		/// <param name="thisLocation">TDLocation</param>
		/// <param name="stationTypes">StationType array</param>
		/// <param name="maxReturn">Maximum number of results to return</param>
		static public int FindStations(ref TDLocation thisLocation, StationType[] stationTypes,int maxReturn)
		{
			return FindStations(ref thisLocation, stationTypes, null, null, false, false, false,maxReturn,false);
		}

        /// <summary>
		/// Searches for car parks within given radius (stored in properties) and loops 
		/// until it finds the maximum number of car parks or the maximum radius is reached. 
		/// </summary>
		/// <param name="thisLocation">selected TDLocation</param>
		static public int FindCarParks(ref TDLocation thisLocation, int maxNoCarParksRequested)
		{
			int radiusInit1 = 0;			
			int radiusMax1 = 0;
			int radiusInit2 = 0;
			int radiusMax2 = 0;
			int maxNoCarParks = 10;

			try
			{		
				radiusInit1 = Convert.ToInt32(Properties.Current[KeyCarRadius1Init],CultureInfo.InvariantCulture);
				radiusMax1 =  Convert.ToInt32(Properties.Current[KeyCarRadius1Max],CultureInfo.InvariantCulture);
				radiusInit2 = Convert.ToInt32(Properties.Current[KeyCarRadius2Init], CultureInfo.InvariantCulture);
				radiusMax2 = Convert.ToInt32(Properties.Current[KeyCarRadius2Max], CultureInfo.InvariantCulture);
				int maxNoCarParksAllowed = Convert.ToInt32(Properties.Current[KeyMaxCarParksReturn], CultureInfo.InvariantCulture);
				if ((maxNoCarParksRequested > 0) && (maxNoCarParksRequested <= maxNoCarParksAllowed))
					maxNoCarParks = maxNoCarParksRequested;
				else
					maxNoCarParks = maxNoCarParksAllowed;
			}
			catch
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Missing/Bad format for FindStationResults Properties");
				Logger.Write(oe);

				throw new TDException("Missing/Bad format for FindCarParkResults Properties",
					true, TDExceptionIdentifier.PSMissingProperty); 
			}

			//Obtain GisQuery service from service discovery
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
			
			//Perform gisQuery to retrieve stop Naptans within the specified radius of the given point
			QuerySchema gisQueryResult = gisQuery.FindNearestCarParks(
				thisLocation.GridReference.Easting, thisLocation.GridReference.Northing, 
				radiusInit1, radiusMax1, maxNoCarParks);

			// If the result didnt find any car parks, attempt search again with the second radius
			if (gisQueryResult.CarParks.Rows.Count <= 0)
			{
				gisQueryResult = gisQuery.FindNearestCarParks(
					thisLocation.GridReference.Easting, thisLocation.GridReference.Northing, 
					radiusInit2, radiusMax2, maxNoCarParks);
			}

			//Create string array of car park reference numbers uing data from the gisQuery
			string [] results = new string [gisQueryResult.CarParks.Rows.Count];

			// create array of car park refs for this location
			for (int i = 0; i < gisQueryResult.CarParks.Rows.Count; i++)
			{
				QuerySchema.CarParksRow row = (QuerySchema.CarParksRow)gisQueryResult.CarParks.Rows[i];
				results[i] = row.CarParkRef;
			}

			thisLocation.CarParkReferences = results;
			thisLocation.Locality = gisQuery.FindNearestLocality(thisLocation.GridReference.Easting, thisLocation.GridReference.Northing); 

			return thisLocation.CarParkReferences.Length;
		}

        /// <summary>
        /// This method returns the distinct station types for the naptans in the location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static StationType[] GetLocationStationTypes(TDLocation location)
        {
            ArrayList listTypes = new ArrayList();
            foreach (TDNaptan naptan in location.NaPTANs)
            {
                if (!listTypes.Contains(naptan.StationType))
                    listTypes.Add(naptan.StationType);
            }
            return (StationType[])listTypes.ToArray(typeof(StationType));
        }

        static public String MixCase(String inputString)
        {
            StringBuilder outputString = new StringBuilder(inputString.Length);

            bool wordBreak = true;
            bool firstHyphen = true;

            for (int i = 0; i < inputString.Length; i++)
            {
                if (wordBreak)
                {
                    outputString.Append(Char.ToUpper(inputString[i]));

                    if (!Char.IsPunctuation(inputString[i]) && !Char.IsWhiteSpace(inputString[i]))
                    {
                        wordBreak = false;
                    }
                }
                else if (Char.IsLetterOrDigit(inputString[i]))
                {
                    outputString.Append(Char.ToLower(inputString[i]));
                }
                else
                {
                    outputString.Append(inputString[i]);

                    if (Char.IsWhiteSpace(inputString[i]))
                    {
                        wordBreak = true;
                        firstHyphen = true;
                        continue;

                    }

                    if (Char.IsPunctuation(inputString[i]))
                    {
                        if (inputString[i] == '-')
                        {
                            if (!firstHyphen)
                            {
                                wordBreak = true;
                            }
                            else
                            {
                                firstHyphen = false;
                            }
                        }
                        else
                        {
                            wordBreak = true;
                        }
                    }
                }
            }

            return outputString.ToString();
        }		

        /// <summary>
        /// Performs a GIS query to find accessible locations for the provided location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static List<TDLocationAccessible> FindAccessibleLocations(ref TDLocation location, 
            bool requireWheelchair, bool requireAssistance, List<TDStopType> tdStopTypes, TDDateTime dateTime,
            int searchDistanceOverride)
        {
            List<TDLocationAccessible> results = new List<TDLocationAccessible>();

            if (location != null)
            {
                try
                {
                    #region Properties
                    // Read search properties
                    int searchDistanceMetresStops = 10000;
                    int searchDistanceMetresLocalities = 10000;
                    int maxResultStops = 10;
                    int maxResultLocalities = 3;

                    if (!Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres"], out searchDistanceMetresStops))
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, string.Format("Error parsing property[{0}] into an integer", "AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres")));

                    if (!Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Localities.SearchDistance.Metres"], out searchDistanceMetresLocalities))
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, string.Format("Error parsing property[{0}] into an integer", "AccessibleOptions.FindNearestLocations.Localities.SearchDistance.Metres")));
                    
                    if (!Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Stops.Count.Max"], out maxResultStops))
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,  string.Format("Error parsing property[{0}] into an integer", "AccessibleOptions.FindNearestLocations.Stops.Count.Max")));

                    if (!Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Localities.Count.Max"], out maxResultLocalities))
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,  string.Format("Error parsing property[{0}] into an integer", "AccessibleOptions.FindNearestLocations.Localities.Count.Max")));

                    // Check if an search override value has been specified
                    if (searchDistanceOverride > 0)
                    {
                        searchDistanceMetresStops = searchDistanceOverride;
                        searchDistanceMetresLocalities = searchDistanceOverride;
                    }
                    #endregion

                    List<string> stopTypes = GetAccessibleStopTypes(tdStopTypes);

                    // Obtain GisQuery service from service discovery
                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    #region Stops search

                    // Check if Stops query should be performed,
                    // assume more than 1 stop type, or if 1 stop type it isn't a locality
                    if (((tdStopTypes.Count > 1) || (tdStopTypes.Count == 1 && tdStopTypes[0] != TDStopType.Locality))
                        && (maxResultStops > 0))
                    {
                        // Perform gisQuery to retrieve accessible locations for the location coordinate
                        AccessibleStopInfo[] accessibleStops = gisQuery.FindNearestAccessibleStops(
                            location.GridReference.Easting, location.GridReference.Northing,
                            searchDistanceMetresStops, maxResultStops,
                            dateTime.GetDateTime(), requireWheelchair, requireAssistance, stopTypes.ToArray());


                        // Create results uing data from the gisQuery
                        if (accessibleStops != null)
                        {
                            TDLocationAccessible loc = null;

                            foreach (AccessibleStopInfo asi in accessibleStops)
                            {
                                loc = new TDLocationAccessible();
                                loc.Description = asi.StopName;
                                loc.Accessible = true;
                                loc.StopType = TDStopTypeHelper.GetTDStopTypeForNaPTAN(asi.Atcocode);
                                loc.NaPTANs = new TDNaptan[1];
                                loc.NaPTANs[0] = new TDNaptan(asi.Atcocode, new OSGridReference(), asi.StopName);
                                loc.DistanceFromSearchOSGR = asi.Distance;

                                results.Add(loc);
                            }
                        }

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            string.Format("GIS query call FindNearestAccessibleStops for location[{0} {1}][{2},{3}] found [{4}] stops",
                                location.ID, location.Description, location.GridReference.Easting, location.GridReference.Northing,
                                (accessibleStops != null) ? accessibleStops.Length : 0)));
                        }
                    }

                    #endregion

                    #region Localities search

                    if (tdStopTypes.Contains(TDStopType.Locality) && maxResultLocalities > 0)
                    {
                        AccessibleLocationInfo[] accessibleLocalities = gisQuery.FindNearestAccessibleLocalities(
                            location.GridReference.Easting, location.GridReference.Northing,
                            searchDistanceMetresLocalities, maxResultLocalities,
                            requireWheelchair, requireAssistance);


                        // Create results uing data from the gisQuery
                        if (accessibleLocalities != null)
                        {
                            TDLocationAccessible loc = null;

                            foreach (AccessibleLocationInfo ali in accessibleLocalities)
                            {
                                loc = new TDLocationAccessible();
                                loc.Description = ali.LocalityName;
                                loc.Accessible = true;
                                loc.StopType = TDStopType.Locality;
                                loc.Locality = ali.NationalGazetteerID;
                                loc.NaPTANs = new TDNaptan[0];
                                loc.DistanceFromSearchOSGR = ali.Distance;

                                results.Add(loc);
                            }
                        }

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            string.Format("GIS query call FindNearestAccessibleLocalities for location[{0} {1}][{2},{3}] found [{4}] localities",
                                location.ID, location.Description, location.GridReference.Easting, location.GridReference.Northing,
                                (accessibleLocalities != null) ? accessibleLocalities.Length : 0)));
                        }
                    }

                    #endregion

                    #region Sort

                    results.Sort(
                        delegate(TDLocationAccessible loc1, TDLocationAccessible loc2)
                        {
                            return loc1.DistanceFromSearchOSGR.CompareTo(loc2.DistanceFromSearchOSGR);
                        });

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error attempting to find accessible locations for [{0} {1},{2}]. Exception: {3}. {4}",
                        location.Description, location.GridReference.Easting, location.GridReference.Northing,
                        ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message));

                    throw new TDException(message, true, TDExceptionIdentifier.LSGISQueryCallError);
                }
            }

            return results;
        }

        /// <summary>
        /// Performs a GIS query to check if the location coordinate is in an accessible area
        /// </summary>
        /// <param name="location"></param>
        /// <param name="requireWheelchair"></param>
        /// <param name="requireAssistance"></param>
        /// <returns></returns>
        public static bool IsAccessibleLocation(TDLocation location,
            bool requireWheelchair, bool requireAssistance)
        {
            try
            {
                int searchDistanceMetresStops = 10000;
                int searchDistanceMetresLocalities = 10000;
                bool isWheelchair = false;
                bool isAssistance = false;

                if (!Int32.TryParse(Properties.Current["AccessibleOptions.IsPointAccessible.Stops.SearchDistance.Metres"], out searchDistanceMetresStops))
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, string.Format("Error parsing property[{0}] into an integer", "AccessibleOptions.IsPointAccessible.Stops.SearchDistance.Metres")));

                if (!Int32.TryParse(Properties.Current["AccessibleOptions.IsPointAccessible.Localities.SearchDistance.Metres"], out searchDistanceMetresLocalities))
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, string.Format("Error parsing property[{0}] into an integer", "AccessibleOptions.IsPointAccessible.Localities.SearchDistance.Metres")));


                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                                
                gisQuery.IsPointInAccessibleLocation(location.GridReference.Easting, location.GridReference.Northing,
                    (location.StopType == TDStopType.Locality) ? searchDistanceMetresLocalities : searchDistanceMetresStops,
                    out isWheelchair, out isAssistance);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        string.Format("GIS query call IsPointInAccessibleLocation for location[{0} {1} {2},{3}] with result wheelchair[{4}] assistance[{5}]",
                            location.ID, location.Description, location.GridReference.Easting, location.GridReference.Northing, isWheelchair, isAssistance)));
                }

                // Return value
                if (requireWheelchair && requireAssistance)
                    return isWheelchair && isAssistance;
                else if (requireWheelchair)
                    return isWheelchair;
                else if (requireAssistance)
                    return isAssistance;

            }
            catch (Exception ex)
            {
                // GIS query error
                string message = string.Format("Error doing GIS query IsPointInAccessibleLocation for location[{0} {1} {2} {3}]. Exception: {4}. {5}",
                    location.ID, location.Description, location.GridReference.Easting, location.GridReference.Northing,
                    ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message));
            }

            return false;
        }

        #endregion

        #region Private Static methods

        /// <summary>
		/// Searches for stations of a selected given types within radius (stored in properties)
		/// and loops until it finds the minimum number of stations or the maximum radius is 
		/// reached. If so, it throws an exception.
		/// 
		/// This overloaded FindStations accepts the maximum number of results to return
		/// Instead of retrieving the maximum number of results from the Properties service
		/// </summary>
		/// <param name="thisLocation">TDLocation</param>
		/// <param name="stationTypes">StationType array</param>
		/// <param name="originLocation">TDLocation</param>
		/// <param name="destinationLocation">TDLocation</param>
		/// <param name="isFromLocation">bool isFromLocation</param>
		/// <param name="isToLocation">bool isToLocation</param>
		/// <param name="isDirectFlight">bool isDirectFlight</param>
		/// <param name="maxReturn">Maximum number of results to return</param>
		/// <param name="getMaxReturn">bool get maxReturn value from the property service</param>
		static private int FindStations(
			ref TDLocation thisLocation,
			StationType[] stationTypes,
			TDLocation originLocation,
			TDLocation destinationLocation,
			bool isFromLocation,
			bool isToLocation,
			bool isDirectFlight,
			int maxReturn,
			bool getMaxReturn)
		{
			int returnedStationsMinNumber = 0;
			int radiusIncrement = 0;			// in metres
			int maxRadius  = 0;					// in metres
			int initRadius = 0;					// in metres

			returnedStationsMinNumber = 
				Convert.ToInt32(Properties.Current["FindStationResults.ReturnedStationsMinNumber"],CultureInfo.InvariantCulture);
	
			ArrayList totalList = new ArrayList();

			foreach (StationType type in stationTypes)
			{
				try
				{		
					radiusIncrement = Convert.ToInt32(Properties.Current[string.Format(CultureInfo.InvariantCulture,KeyRadiusIncrementProperties, type.ToString())],CultureInfo.InvariantCulture);
					maxRadius  	    = Convert.ToInt32(Properties.Current[string.Format(CultureInfo.InvariantCulture,KeyMaxRadiusProperties, type.ToString())],CultureInfo.InvariantCulture);
					initRadius	    = Convert.ToInt32(Properties.Current[string.Format(CultureInfo.InvariantCulture,KeyInitRadiusProperties, type.ToString())],CultureInfo.InvariantCulture);
					
					if (getMaxReturn)
					{
						maxReturn    = Convert.ToInt32(Properties.Current[string.Format(CultureInfo.InvariantCulture,KeyMaxStationReturn, type.ToString())],CultureInfo.InvariantCulture);
					}
				}
				catch
				{
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Missing/Bad format for FindStationResults Properties");
					Logger.Write(oe);

					throw new TDException(
						"Missing/Bad format for FindStationResults Properties",
						true,
						TDExceptionIdentifier.PSMissingProperty);
				}

				ArrayList naptanList = new ArrayList();

				int currentRadius = initRadius;

				naptanList.AddRange(FindStationsInRadius(thisLocation, currentRadius, type, maxReturn));
				
				naptanList = FilterNaptans(naptanList, thisLocation.GridReference, type, originLocation, destinationLocation, isFromLocation, isToLocation, isDirectFlight, maxReturn);

				while ((naptanList.Count < returnedStationsMinNumber) && (currentRadius <= maxRadius))
				{
					currentRadius += radiusIncrement;
					naptanList.AddRange(FindStationsInRadius(thisLocation, currentRadius, type, maxReturn));
					naptanList = FilterNaptans(naptanList, thisLocation.GridReference, type, originLocation, destinationLocation, isFromLocation, isToLocation, isDirectFlight, maxReturn);
				} 

				totalList.AddRange(naptanList);
			}

			thisLocation.NaPTANs = GetSortedList(totalList, thisLocation.GridReference);
			
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
			thisLocation.Locality = gisQuery.FindNearestLocality(thisLocation.GridReference.Easting, thisLocation.GridReference.Northing); 

			return thisLocation.NaPTANs.Length;
		}

		static private TDNaptan[] GetSortedList(ArrayList inputList, OSGridReference OSGR)
		{
			SortedList sorted = new SortedList();
					
			foreach (TDNaptan naptan in inputList) 
			{
				int distance = naptan.GridReference.DistanceFrom(OSGR);
				int attempts = 0;

				while (attempts < 10) 
				{
					if (!sorted.ContainsKey(distance))
					{
						sorted.Add(distance, naptan);
						break; 
					}
					else
					{	
						distance++;
						attempts++;
					}
				}
			}	

			ArrayList tempList = new ArrayList(sorted.GetValueList());
			return (TDNaptan[])tempList.ToArray(typeof(TDNaptan));
		}
        
		/// <summary>
		/// Filters Naptans based on parameters
		/// </summary>
		/// <param name="inputNaptans">TDNaptan Array</param>
		/// <param name="OSGR">OSGridReference</param>
		/// <param name="stationType">StationType enum</param>
		/// <param name="originLocation">TDLocation</param>
		/// <param name="destinationLocation">TDLocation</param>
		/// <param name="isFromLocation">bool isFromLocation</param>
		/// <param name="isToLocation">bool isToLocation</param>
		/// <param name="isDirectFlight">bool isDirectFlight</param>
		/// <param name="maxReturn">Maximum number of results to return</param>
		/// <returns>Filtered TDNaptan Array</returns>
		static private ArrayList FilterNaptans(ArrayList inputNaptans, OSGridReference OSGR, StationType stationType, TDLocation originLocation, TDLocation destinationLocation, bool isFromLocation, bool isToLocation, bool isDirectFlight, int maxReturn)
		{
			ArrayList filteredNaptans = null;

			switch (stationType)
			{
				case StationType.Airport:
				{
					ArrayList airportNaptans = new ArrayList(inputNaptans.Count);
					
					foreach (TDNaptan naptan in inputNaptans)
					{
						airportNaptans.Add(new TDAirportNaptan(naptan));
					}

					filteredNaptans = FilterAirportNaptans(OSGR, airportNaptans, originLocation, destinationLocation, isFromLocation, isToLocation, isDirectFlight, maxReturn);
					break;
				}

				case StationType.Rail:
				{
					IAdditionalData additionalData = (IAdditionalData) TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];

					ArrayList railNaptans = new ArrayList(inputNaptans.Count);

					foreach (TDNaptan naptan in inputNaptans)
					{
						//Create TDRailNaptan from TDNaptan, and retrieve CRS code using AdditionalData service
						string crsCode = additionalData.LookupCrsForNaptan(naptan.Naptan);

						TDRailNaptan tdRailNaptan = new TDRailNaptan(naptan,crsCode);

						railNaptans.Add(tdRailNaptan);
					}

					filteredNaptans = FilterStandardNaptans(OSGR, railNaptans, maxReturn, stationType); 
					break;
				}
				case StationType.Coach:
				{
					filteredNaptans = FilterStandardNaptans(OSGR, inputNaptans, maxReturn, stationType); 
					break;
				}
			}

			return filteredNaptans;
		}	
	
		static private ArrayList FilterAirportNaptans(OSGridReference OSGR, ArrayList airportNaptans, TDLocation originLocation, TDLocation destinationLocation, bool isFromLocation, bool isToLocation, bool isDirectFlight, int maxReturn)
		{
			SortedList listNaptans = new SortedList();

			foreach (TDAirportNaptan aNaptan in airportNaptans)
			{
				int distanceToOrigin = OSGR.DistanceFrom(aNaptan.GridReference);
		
				if (aNaptan.IsAirport)
				{
					if (isDirectFlight)
					{
						// If origin is defined, check a direct route exists with current naptan
						if  (!isFromLocation && originLocation.Status == TDLocationStatus.Valid)
						{
							if (aNaptan.HasDirectRouteWithAirport(originLocation.NaPTANs)
								&& aNaptan.Terminal != "0")
							{
								listNaptans.Add(distanceToOrigin, aNaptan);
							}
						}
						else
						{
							// If destination is defined, check a direct route exists with current naptan
							if  (!isToLocation && destinationLocation.Status == TDLocationStatus.Valid)
							{
								if (aNaptan.HasDirectRouteWithAirport(destinationLocation.NaPTANs)
									&& aNaptan.Terminal != "0")
								{
									listNaptans.Add(distanceToOrigin, aNaptan);
								}
							}
							else
							{
								if (aNaptan.IsActive && aNaptan.Terminal != "0")
									listNaptans.Add(distanceToOrigin, aNaptan );
							}
						}
					}
					else
					{
						if (aNaptan.IsActive && aNaptan.Terminal != "0")
							listNaptans.Add(distanceToOrigin, aNaptan);
					}
		
				}
			}

			TrimSortedList(listNaptans, maxReturn);
			return new ArrayList(listNaptans.GetValueList());
		}

		static private ArrayList FilterStandardNaptans(OSGridReference OSGR, ArrayList naptans, int maxReturn, StationType stationType)
		{
			SortedList listNaptans = new SortedList(); 

			foreach (TDNaptan naptan in naptans)
			{
				if (naptan.StationType == stationType)
				{
					int distance = naptan.GridReference.DistanceFrom(OSGR);
					int attempts = 0;
					
					// Distance may not be unique key. 
					// Following is workaround to sort them anyway.

					while (attempts < 10) // limit to 10 attempts to avoid falling in infinite loops
					{
						if (!listNaptans.ContainsKey(distance))
						{
							listNaptans.Add(distance, naptan);
							break; // break if manages to add naptan. get out the while loop
						}
						else
						{	// if key already exists add 1 to try to find the following key.
							// This should solve the problem of having 2 same distances = 2 same keys
							distance++;
							attempts++;
						}
					}
				}
			}

			TrimSortedList(listNaptans, maxReturn);
			return new ArrayList(listNaptans.GetValueList());
		}
        
		/// <summary>
		/// remove all last items in list until only the required number of items is left
		/// </summary>
		/// <param name="list">list to trim</param>
		/// <param name="maxNumber">max number of items in list</param>
		static private void TrimSortedList(SortedList list, int maxNumber)
		{
			while (list.Count > maxNumber)
			{
				list.RemoveAt(list.Count - 1);
			}
		}
		
		public static TDNaptan[] FindStationsInRadius(TDLocation location, int radius, StationType stationType, int maximum)
		{

			string mode = null;

			switch (stationType)
			{
				case (StationType.Airport):
					mode = "Air";
					break;

				case (StationType.Rail):
					mode = "Rail";
					break;

				case (StationType.Coach):
					mode = "Coach";
					break;
			}
			
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
		
			ExchangePointSchema gisResult 
				= gisQuery.FindExchangePointsInRadius(location.GridReference.Easting, 
													  location.GridReference.Northing, 
													  radius, 
													  mode,
													  maximum);

			TDNaptan[] results = new TDNaptan[gisResult.ExchangePoints.Rows.Count];

			for (int i = 0; i < gisResult.ExchangePoints.Rows.Count; i++)
			{
				ExchangePointSchema.ExchangePointsRow row = (ExchangePointSchema.ExchangePointsRow) gisResult.ExchangePoints.Rows[i];
				results[i] = new TDNaptan();
				results[i].Naptan = row.naptan;
				results[i].GridReference = new OSGridReference((int) row.easting, (int)row.northing);
		
				if	(stationType == StationType.Rail)
				{
					results[i].Name = MixCase(row.name);
				}
				else
				{
					results[i].Name = row.name;
				}
			}

			return results;
		}

        /// <summary>
        /// Returns a string list for stop types to pass into the GISQuery method to FindNearestAccessibleStops
        /// </summary>
        /// <param name="tdStopTypes"></param>
        /// <returns></returns>
        private static List<string> GetAccessibleStopTypes(List<TDStopType> tdStopTypes)
        {
            List<String> stopTypes = new List<string>();

            if (tdStopTypes != null)
            {
                // ESRI: stopTypes are any of: "rail","coach,"ferry","air","lulmetro","dlr","lightrail"

                foreach (TDStopType tdStopType in tdStopTypes)
                {
                    switch (tdStopType)
                    {
                        case TDStopType.Air:
                        case TDStopType.Coach:
                        case TDStopType.DLR:
                        case TDStopType.Ferry:
                        case TDStopType.LightRail:
                        case TDStopType.POI:
                        case TDStopType.Rail:
                            if (!stopTypes.Contains(tdStopType.ToString().ToLower()))
                                stopTypes.Add(tdStopType.ToString().ToLower());
                            break;
                        case TDStopType.Bus:
                            if (!stopTypes.Contains(TDStopType.Coach.ToString().ToLower()))
                                stopTypes.Add(TDStopType.Coach.ToString().ToLower());
                            break;
                        case TDStopType.Underground:
                            if (!stopTypes.Contains("lulmetro"))
                                stopTypes.Add("lulmetro");
                            break;
                    }
                }
            }

            return stopTypes;
        }

        #endregion
    }
}
