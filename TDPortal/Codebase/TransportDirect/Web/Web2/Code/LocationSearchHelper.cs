// *********************************************** 
// NAME                 : LocationSearchHelper.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 19/09/2003 
// DESCRIPTION          : Class containing location search helper methods
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/LocationSearchHelper.cs-arc  $ 
//
//   Rev 1.9   Feb 04 2013 15:35:14   mmodi
//Fixed accessible flag being incorrect for a resolved location set using a previous accessible query 
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.8   Jan 21 2013 13:38:50   mmodi
//Updated accessible check for all location types
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Jan 15 2013 10:30:54   mmodi
//Specify a search distance override for find accessible stop
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Jan 09 2013 11:42:52   mmodi
//Updated is accessible location check to use GIS query and made switchable
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Jan 04 2013 15:36:50   mmodi
//Return accessible location list for find method
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Dec 05 2012 13:52:22   mmodi
//Added check and find accessible locations methods
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Aug 28 2012 10:20:48   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.2   Mar 31 2008 13:18:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.2   Nov 29 2007 15:11:58   mturner
//Removed redundant cls compliance tag
//
//   Rev 1.1   Nov 29 2007 11:40:24   build
//Updated for Del 9.8
//
//   Rev 1.43   Nov 08 2007 14:26:02   mmodi
//Overloaded FindCarParks to accept max number of car parks
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.42   Oct 11 2006 14:11:50   rbroddle
//CCN338 Single Ambiguity Changes
//Resolution for 4217: CCN338 Small Change Fund change ATO412 Single Ambiguity
//
//   Rev 1.41   Oct 06 2006 16:32:26   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.40.1.0   Aug 14 2006 11:03:14   esevern
//Added FindCarParks()
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.40   Apr 20 2006 15:53:40   tmollart
//Removed DisableLocailityQuery method.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.39   Feb 23 2006 16:25:34   RWilby
//Merged stream3129
//
//   Rev 1.38   Jan 19 2006 18:12:24   RPhilpott
//When checking a drilldown result to see if it has children, need to check the result that has just been returned, not the previous one. 
//Resolution for 3473: UEE: Gazzeteer - single address option in drop down box
//
//   Rev 1.37   Oct 11 2005 15:06:14   scraddock
//ref vantive 3963120 - IR2812 added addtional decision criteria in case a single location is returned that DOES have children
//
//   Rev 1.36   Apr 13 2005 09:10:24   rscott
//DEL 7 Additional Tasks - IR1978 enhancements added - reject single word address.
//
//   Rev 1.35   Apr 11 2005 14:13:20   rscott
//Some logging removed on behalf of Peter Norrell
//
//   Rev 1.34   Apr 07 2005 16:24:10   rscott
//Updated with DEL7 additional task outlined in IR1977.
//
//   Rev 1.33   Feb 14 2005 15:39:38   PNorell
//Updated with the latest requirements for IR1905
//Resolution for 1905: Cumbria causes error when issued as a locality
//
//   Rev 1.32   Sep 10 2004 19:11:46   RPhilpott
//Use more meaningful name for DisableLocalityQuery().
//Resolution for 1570: Find-A-Car  --  unnecessary calls to FindNearestLocality()
//
//   Rev 1.31   Sep 10 2004 15:36:20   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.30   Aug 24 2004 18:07:40   RPhilpott
//Accept StationType on StartSearch to allow MajorStations gazetteer to filter by mode.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.29   Aug 20 2004 16:25:10   passuied
//call ClearSearch instead of ClearAll() in StartSearch() when InputText is empty.
//Resolution for 1240: Gazetteer default on JP input page set to address postcode
//
//   Rev 1.28   Aug 13 2004 14:29:06   passuied
//Changes for FindA station distinct error message
//Resolution for 1309: Find a Coach - Wrong error message displayed when no coach station found
//
//   Rev 1.27   Jul 22 2004 16:13:48   RPhilpott
//Pull most of the FindStations code into the LocationService.  
//
//   Rev 1.26   Jul 21 2004 10:48:24   passuied
//Re work for FindStation func 6.1.
//Working
//
//   Rev 1.25   Jul 14 2004 13:00:24   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.24   Jul 12 2004 14:12:46   passuied
//Usage of new property Mode of FindPageState base class.
//
//   Rev 1.23   Jul 09 2004 13:35:24   passuied
//change to avoid warning
//
//   Rev 1.22   Jul 09 2004 13:08:12   passuied
//Updated FindStation and filter methods for FindStation 6.1 back end
//
//   Rev 1.21   Jul 01 2004 14:12:50   passuied
//changes following exhaustive testing
//
//   Rev 1.20   Jun 30 2004 15:43:02   passuied
//Cleaning up
//
//   Rev 1.19   Jun 23 2004 15:54:48   passuied
//addition for Results message functionality
//
//   Rev 1.18   Jun 09 2004 09:49:00   passuied
//fixed bug in FindStations method when airport is found in max radius but was still throwing an error
//
//   Rev 1.17   Jun 03 2004 16:13:58   passuied
//changes for integration with FindAFlight
//
//   Rev 1.16   Jun 03 2004 11:39:46   passuied
//removed TDAirportNaptan from file. Moved to LocationService
//
//   Rev 1.15   Jun 02 2004 16:38:26   passuied
//working version
//
//   Rev 1.14   May 28 2004 17:48:10   passuied
//update as part of FindStation development
//
//   Rev 1.13   May 14 2004 15:27:32   passuied
//Changes for FindAirports Functionality. Added FindAirport method + DisableGisQuery to avoid searching naptans and toids before searching airports
//
//   Rev 1.12   May 12 2004 17:46:44   passuied
//compiling check in for FindStation pages and related
//
//   Rev 1.11   May 10 2004 09:55:56   jbroome
//IR853
//
//   Rev 1.10   Apr 27 2004 13:52:00   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.9   Dec 03 2003 12:19:50   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.8   Nov 27 2003 17:56:02   passuied
//changed SetUpLocationSearch so if location doesn't accept  postcodes it rejects them
//
//   Rev 1.7   Oct 21 2003 16:13:20   passuied
//changes after FxCop
//
//   Rev 1.6   Oct 14 2003 12:48:06   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.5   Oct 07 2003 15:32:20   passuied
//corrected error for getLocationdetails when drilldown returns 1 choicelist with only one choice
//
//   Rev 1.4   Oct 03 2003 16:59:22   passuied
//session reference bug fixed
//
//   Rev 1.3   Sep 25 2003 14:00:08   passuied
//last working version (LocationSearch fixed)
//
//   Rev 1.2   Sep 24 2003 15:03:18   passuied
//last working version
//
//   Rev 1.1   Sep 20 2003 14:40:12   passuied
//working version
//
//   Rev 1.0   Sep 19 2003 21:19:58   passuied
//Initial Revision


using System;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
using LSC = TransportDirect.UserPortal.LocationService.Cache;
using System.Collections.Generic;


namespace TransportDirect.UserPortal.Web
{
	/// <summary>
    /// Class containing location search helper methods
	/// </summary>
	public sealed class LocationSearchHelper
    {
        #region Public variables

        public const string DEFAULT_ITEM = "Default";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private LocationSearchHelper()
		{
        }

        #endregion

        #region Public methods

        #region Location methods

        /// <summary>
        /// Setup a location search, checking for postcode locations
        /// </summary>
        public static LocationChoiceList SetupLocationParameters(
			string text,
			SearchType searchType,
			bool fuzzy,
			int level,
			int maxWalkingTime,
			int maxWalkingSpeed,
			ref LocationSearch search,
			ref TDLocation location,
			bool acceptsPostcode,
			bool acceptsPartPostcode,
			StationType stationType)
		{

			// if the location does not support postcodes or partial postcodes
			// (i.e. we don't want to find the location if a (part) postcode is identified -- typically Public Via)
			// don't call the gazetteer
			if ((!acceptsPostcode && PostcodeSyntaxChecker.ContainsPostcode(text)) ||
				(!acceptsPartPostcode && PostcodeSyntaxChecker.IsPartPostCode(text)))
			{
				search.InputText = text;
				search.SearchType = searchType;
				search.FuzzySearch = fuzzy;
				search.MaxWalkingDistance = maxWalkingSpeed*maxWalkingTime;
				return null;
			}

			//If the location supports a partial postcode and the input text is a 
			//partial postcode match, then update InputPageState to show this.
			//This is needed when the map is displayed to differentiate between
			//exact and partial postcode matches when setting the map scale.
			if (acceptsPartPostcode && PostcodeSyntaxChecker.IsPartPostCode(text))
			{ 
				location.PartPostcode = true;	
			}
			else
			{ 
				location.PartPostcode = false; 
			}

			if (text.Length == 0)
			{
				location.Status = TDLocationStatus.Unspecified;	
				search.ClearSearch();
				return null;
			}
			else if ( location.Status == TDLocationStatus.Valid)
			{
				return null;
			}
			else if (location.Status == TDLocationStatus.Unspecified)
			{
				LocationChoiceList result = 
					search.StartSearch( 
					text, 
					searchType, 
					fuzzy, 
					maxWalkingSpeed*maxWalkingTime, 
					TDSessionManager.Current.Session.SessionID,
					TDSessionManager.Current.Authenticated,
					stationType);

				if ( result.Count == 1)
				{
					LocationChoice lc = (LocationChoice)result[0];
					// CCN338 - added check on fuzzy flag for single ambiguity change
					if(( searchType == SearchType.Locality && lc.HasChilden ) || (fuzzy))
					{
						location.Status = TDLocationStatus.Ambiguous;
						return result;
					}
					else
					{
						search.GetLocationDetails(ref location, lc);
						search.InputText = location.Description;
						return null;
					}
				}
				else if (result.Count >1)
				{
					location.Status = TDLocationStatus.Ambiguous;
					return result;
				}
				else
				{
					// Vague flag and Single word address flag
					if (result.IsVague == true)
					{
						search.VagueSearch = true;
						return null;
					}
					else if (result.IsSingleWordAddress == true)
					{
						search.SingleWord = true;
						return null;
					}
					else
					{
						search.VagueSearch = false;
						search.SingleWord = false;
						return null;
					}
				}
			}
			else
			{
				return null;
			}
		}

		static public LocationChoiceList LocationDrillDown( 
			int level,
			LocationChoice selection,
			ref LocationSearch thisSearch,
			ref TDLocation thisLocation)
		{
			if (!selection.HasChilden)
			{
				thisSearch.GetLocationDetails(ref thisLocation, selection);
				thisSearch.InputText = thisLocation.Description;
				return null;

			}
			else
			{
				LocationChoiceList result = thisSearch.DrillDown(
					level,
					selection);

				if ((result.Count == 1) && (!((LocationChoice)result[0]).HasChilden))
				{
					thisSearch.GetLocationDetails(ref thisLocation, (LocationChoice)result[0]);
					thisSearch.InputText = thisLocation.Description;
					return null;
				}
				else
					return result;
			}

		}

		
		static public TDLocation GetLocationDetails (
			LocationChoice selection,
			ref LocationSearch thisSearch,
			ref TDLocation thisLocation)
		{
			if (thisSearch.SupportHierarchic)
			{
				thisSearch.GetLocationDetails(ref thisLocation, selection);
				thisSearch.InputText = thisLocation.Description;
				
			}
			
			return thisLocation;
			
		}

		static public void RefreshLocationDetails(
			ref LocationSearch thisSearch,
			ref TDLocation thisLocation,
			int maxWalkingTime,
			int maxWalkingSpeed)
		{
			thisSearch.RefreshLocationDetails(ref thisLocation);
        }

        #endregion

        #region Location "auto-suggest" methods

        /// <summary>
        /// Creates new search and location objects
        /// </summary>
        public static void ResetSearchAndLocation(ref LocationSearch search, ref TDLocation location,
            string locationInputText, SearchType searchType, bool fuzzySearch, bool allowGroupLocations, bool javascriptEnabled)
        {
            // Create default search and location objects
            search = new LocationSearch();
            location = new TDLocation();

            // Set search values
            search.InputText = locationInputText.Trim();
            search.SearchType = searchType;
            search.FuzzySearch = fuzzySearch;
            search.NoGroup = !allowGroupLocations;

            search.JavascriptEnabled = javascriptEnabled;
        }

        /// <summary>
        /// Peforms the location search using locationCache/gazetteers/GIS, 
        /// locationId is from the "auto-suggest" location dropdown
        /// </summary>
        public static void SearchLocation(
            ref LocationSearch search, ref TDLocation location,
            TDJourneyParameters journeyParameters,
            bool acceptsPostcode, bool acceptsPartPostcode, StationType stationType,
            string locationId, TDStopType locationType)
        {
            bool isPostcode = PostcodeSyntaxChecker.ContainsPostcode(search.InputText) 
                            || PostcodeSyntaxChecker.IsPartPostCode(search.InputText);

            // If javascript enabled, then resolve using the LocationService cache, 
            // as long as it is not a postcode
            if (search.JavascriptEnabled && !isPostcode)
            {
                #region Auto-suggest resolution

                LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

                locationService.ResolveLocation(ref search, ref location, locationId, locationType, stationType,
                    journeyParameters.MaxWalkingTime, journeyParameters.WalkingSpeed,
                    true, TDSessionManager.Current.Session.SessionID, TDSessionManager.Current.Authenticated);

                #endregion
            }
            else
            {
                #region Gaz location resolution

                // Perform a standard gaz search
                LocationChoiceList locationsList =
                    LocationSearchHelper.SetupLocationParameters(
                                search.InputText,
                                search.SearchType,
                                search.FuzzySearch,
                                0,
                                journeyParameters.MaxWalkingTime,
                                journeyParameters.WalkingSpeed,
                                ref search,
                                ref location,
                                acceptsPostcode,
                                acceptsPartPostcode,
                                stationType);

                #endregion
            }
        }

        /// <summary>
        /// Gets the ambiguity location selectecd by the user, drilling down location if required
        /// </summary>
        public static void AmbiguityLocation(ref LocationSearch search, ref TDLocation location, 
            TDJourneyParameters journeyParameters,
            DropDownList ambiguityDrop,
            bool acceptsPostcode, bool acceptsPartPostcode, StationType stationType,
            bool javascriptEnabled)
        {
            // Exit if no ambiguity drop, or the current value is "unrefined"
            if (!ambiguityDrop.Visible || ambiguityDrop.SelectedItem.Value == DEFAULT_ITEM)
                return;

            // Determine which ambiguity list we're working with
            if (search.GetAmbiguitySearchResult().Count > 0)
            {
                #region Auto-suggest ambiguity resolution

                // Get the location
                TDLocation choice = search.GetAmbiguitySearchResult()
                    [Convert.ToInt32(ambiguityDrop.SelectedItem.Value)];

                // Clear existing search and location
                search.ClearAll();
                location.ClearAll();

                // Set search values and perform a search with the chosen location
                search.InputText = choice.Description;
                search.SearchType = SearchTypeHelper.GetSearchType(choice.StopType);
                search.JavascriptEnabled = javascriptEnabled;

                SearchLocation(ref search, ref location, journeyParameters,
                    acceptsPartPostcode, acceptsPartPostcode, stationType,
                    choice.ID, choice.StopType);

                #endregion
            }
            else
            {
                #region Gaz ambiguity resolution

                // Determine if the selected option was to drill down
                bool drillable = ambiguityDrop.SelectedItem.Value.StartsWith("+");

                // Get the selected choice; this will be the same choice whether
                // the selected option was for a location or for a drill down into 
                // the location (the "+" will be ignored by the indexer)
                LocationChoice choice = (LocationChoice)search.GetCurrentChoices(search.CurrentLevel)
                    [Convert.ToInt32(ambiguityDrop.SelectedItem.Value)];

                // Drill down if drill down option selected or
                // this is not a hierarchic search
                if (drillable || !search.SupportHierarchic)
                {
                    LocationSearchHelper.LocationDrillDown(
                        search.CurrentLevel,
                        choice,
                        ref search,
                        ref location);

                }
                // Option selected is not a drill down option on
                // a hierarchic search
                else
                {
                    LocationSearchHelper.GetLocationDetails(
                        choice,
                        ref search,
                        ref location);
                }

                #endregion
            }
        }

        #endregion

        #region Accessible Location methods

        /// <summary>
        /// Checks if the location is accessible, if accessible journey required.
        /// </summary>
        /// <returns>True if its ok to plan an accessible journey for location</returns>
        public static bool CheckAccessibleLocation(ref TDLocation location, TDJourneyParameters journeyParameters)
        {
            bool result = false;

            TDJourneyParametersMulti journeyParametersMulti = null;

            if (journeyParameters != null && journeyParameters is TDJourneyParametersMulti)
            {
                journeyParametersMulti = (TDJourneyParametersMulti)journeyParameters;
            }

            if (journeyParametersMulti != null)
            {
                // Check to see if the location is accessible,
                // only when special assistance or step free access required
                if (journeyParametersMulti.RequireSpecialAssistance || journeyParametersMulti.RequireStepFreeAccess)
                {
                    if (location != null && location.Status == TDLocationStatus.Valid)
                    {
                        // Ensure admin and district code is populated for the location
                        location.PopulateAdminDistrictCode();

                        bool adminareaCheck = false;
                        bool accessibleCacheCheck = false;
                        bool accessibleGISCheck = false;
                        bool.TryParse(Properties.Current["AccessibleOptions.LocationAccessible.AdminAreaCheck.Switch"], out adminareaCheck);
                        bool.TryParse(Properties.Current["AccessibleOptions.LocationAccessible.AccessibleCacheCheck.Switch"], out accessibleCacheCheck);
                        bool.TryParse(Properties.Current["AccessibleOptions.LocationAccessible.AccessibleGISQueryCheck.Switch"], out accessibleGISCheck);

                        LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

                        // Check 1) Is location admin area/district in an Accessible area?
                        if (adminareaCheck &&
                            locationService.IsAccessibleAdminArea(
                            location.AdminDistrict.NPTGAdminCode, location.AdminDistrict.NPTGDistrictCode,
                            journeyParametersMulti.RequireStepFreeAccess,
                            journeyParametersMulti.RequireSpecialAssistance))
                        {
                            // Location is in a defined Accessible area, return True
                            location.Accessible = true;
                            result = true;
                        }
                        // Check 2) Is location an accessible location - TDAN check? 
                        // Only allow Accessible look up for locations with 1 Naptan.
                        // This checks local TD Accessible locations cache.
                        // If more than one naptan provided, possibly an exchange group, return False
                        else if (accessibleCacheCheck &&
                            (location.NaPTANs.Length == 1)
                            && (locationService.IsAccessibleLocation(
                                        location.NaPTANs[0].Naptan,
                                        journeyParametersMulti.RequireStepFreeAccess,
                                        journeyParametersMulti.RequireSpecialAssistance)))
                        {

                            // Location is in the defined Accessible locations list, return True
                            location.Accessible = true;
                            result = true;

                        }
                        // Check 3) Is location point an accessible location - GIS Query check?
                        // Allow lookup for any location with a coordinate
                        else if (accessibleGISCheck &&
                            (location.GridReference != null && location.GridReference.IsValid)
                            && LocationSearch.IsAccessibleLocation(
                                    location,
                                    journeyParametersMulti.RequireStepFreeAccess,
                                    journeyParametersMulti.RequireSpecialAssistance))
                        {
                            // Location is in the GIS Query accessible area, return True
                            location.Accessible = true;
                            result = true;
                        }
                        else
                        {
                            // Location is not Accessible
                            location.Accessible = false;
                            result = false;
                        }

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                string.Format("Accessible status for location[{0} {1}] updated with result[{2}]",
                                location.ID, location.Description, result)));
                        }

                    }
                }
            }

            // Otherwise false, not ok to use location for an accessible journey
            return result;
        }

        #endregion

        #region Find methods

        /// <summary>
		/// Searchs for car parks within radius (stored in properties)
		/// Throws an exception if none can be found within maximum possible radius.
		/// </summary>
		/// <param name="thisLocation"></param>
		static public int FindCarParks(ref TDLocation thisLocation)
		{
			try
			{
				int maxNoCarParks = Convert.ToInt32(Properties.Current["FindNearestCarParks.NumberCarParksReturned"]);
				
				return FindCarParks(ref thisLocation, maxNoCarParks);
			}
			catch
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Missing/Bad format for FindStationResults Properties");
				Logger.Write(oe);

				throw new TDException("Missing/Bad format for FindCarParkResults Properties",
					true, TDExceptionIdentifier.PSMissingProperty); 
			}
		}

		/// <summary>
		/// Searchs for car parks within radius (stored in properties)
		/// Throws an exception if none can be found within maximum possible radius.
		/// </summary>
		/// <param name="thisLocation"></param>
		/// <param name="maxNumberOfCarParks">Max Number of car parks to return</param>
		static public int FindCarParks(ref TDLocation thisLocation, int maxNoCarParks)
		{
			TDJourneyParameters jp = TDSessionManager.Current.JourneyParameters;
			int results = 0;

			try
			{
				results = LocationSearch.FindCarParks(ref thisLocation, maxNoCarParks);
			
				// Extra check, if no car parks found, throw exception. 
				// Same identifier as
				// in Location Service.
				if (thisLocation.CarParkReferences.Length == 0 
					&& TDSessionManager.Current.FindPageState.Mode == FindAMode.CarPark)
				{
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
						"Find Station : No car parks found in Max Radius. Maximum radius reached.");
					Logger.Write(oe);

					throw new TDException(
						"Find Station : No car parks found in Max Radius. Maximum radius reached.",
						true,
						TDExceptionIdentifier.LSNoCarParksFoundInMaxRadius);
				}
			}
			catch (TDException e)
			{
				// rethrow the exception.
				throw e;
			}	
			return results;
		}

		/// <summary>
		/// Searchs for stations of a selected given types within radius (stored in properties)
		/// Throws an exception if none can be found within maximum possible radius.
		/// </summary>
		/// <param name="thisLocation"></param>
		/// <param name="stationType"></param>
		static public int FindStations(ref TDLocation thisLocation, StationType[] stationTypes)
		{
			TDJourneyParameters jp = TDSessionManager.Current.JourneyParameters;

			int results = 0;
			try
			{
				results = LocationSearch.FindStations(ref thisLocation,
					stationTypes,
					jp.OriginLocation,
					jp.DestinationLocation,
					IsFromLocation,
					IsToLocation,
					IsDirectFlight);

			
				// Extra check, if no naptan found and in station mode. Throw exception. Same identifier as
				// in Location Service.
				if (thisLocation.NaPTANs.Length == 0 
				&& TDSessionManager.Current.FindPageState.Mode == FindAMode.Station)
				{
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
						"Find Station : No stations found in Max Radius. Maximum radius reached.");
					Logger.Write(oe);

					throw new TDException(
						"Find Station : No stations found in Max Radius. Maximum radius reached.",
						true,
						TDExceptionIdentifier.LSNoStationFoundInMaxRadius);
				}

			}
			catch (TDException e)
			{
				// if no all station types found, let the system carry on and display
				// the found results with an error message only if there is some results to display.
				// Otherwise, rethrow the exception.
				if (e.Identifier == TDExceptionIdentifier.LSNoAllStationTypesFound)
				{
					if (thisLocation.NaPTANs.Length == 0)
						throw e;
					else
					{
						// flag to indicate to the result page to display an error message
						TDSessionManager.Current.FindStationPageState.NotAllStationTypesFound = true;
					}
				}
				else
					throw e;
			}	
			
			return results;


        }

        /// <summary>
        /// Searches for accessible locations for the supplied TDLocation
        /// </summary>
        /// <param name="thisLocation"></param>
        /// <returns></returns>
        static public List<TDLocationAccessible> FindAccessibleLocations(ref TDLocation location,
            bool requireWheelchair, bool requireAssistance, List<TDStopType> stopTypes, TDDateTime tdDateTime,
            int searchDistanceOverride)
        {
            List<TDLocationAccessible> locations = new List<TDLocationAccessible>();
            try
            {
                locations = LocationSearch.FindAccessibleLocations(ref location,
                    requireWheelchair, requireAssistance, stopTypes, tdDateTime, searchDistanceOverride);
            }
            catch (TDException e)
            {
                // rethrow the exception.
                throw e;
            }
            return locations;
        }

        #endregion

        /// <summary>
		/// Disable the call to GISQuery for the given search
		/// </summary>
		/// <param name="thisSearch">search to disable</param>
		public static void DisableGisQuery(LocationSearch thisSearch)
		{
			thisSearch.DisableGisQuery();
		}

        #endregion

        #region Private methods

        /// <summary>
		/// Get property. Returns if "Direct flight only" option was ticked
		/// </summary>
		static private bool IsDirectFlight
		{
			get
			{
				if  (TDSessionManager.Current.JourneyParameters is TDJourneyParametersFlight)
				{
					return ((TDJourneyParametersFlight)TDSessionManager.Current.JourneyParameters).DirectFlightsOnly;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Get property. Returns if location is of type From
		/// </summary>
		static private bool IsFromLocation
		{
			get
			{
				FindStationPageState stationPageState = TDSessionManager.Current.FindStationPageState;
				FindPageState pageState = TDSessionManager.Current.FindPageState;

				return (pageState.Mode == FindAMode.Flight
					&& stationPageState.LocationType == FindStationPageState.CurrentLocationType.From);
			}
		}

		/// <summary>
		/// Get property. Returns if location is of type To
		/// </summary>
		static private bool IsToLocation
		{
			get
			{
				FindStationPageState stationPageState = TDSessionManager.Current.FindStationPageState;
				FindPageState pageState = TDSessionManager.Current.FindPageState;

				return (pageState.Mode == FindAMode.Flight
					&& stationPageState.LocationType == FindStationPageState.CurrentLocationType.To);
			}
        }

        

        #endregion
    }
}

