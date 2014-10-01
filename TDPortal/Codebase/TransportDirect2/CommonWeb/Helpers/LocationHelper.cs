// *********************************************** 
// NAME             : LocationHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Feb 2012
// DESCRIPTION  	: Location helper class for methods related to locations
// ************************************************
// 

using System;
using TDP.Common.EventLogging;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.CoordinateConvertorProvider;
using TDP.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
using LS = TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.Extenders;
using System.Collections.Generic;
using System.Text;
using TDP.UserPortal.SessionManager;
using System.Web.UI.WebControls;
using TDP.Common.LocationService.Gazetteer;
using TDP.Common.ResourceManager;

namespace TDP.Common.Web
{
    public class LocationHelper
    {
        #region Constants

        public const string DEFAULT_ITEM = "Default";

        #endregion

        #region Private members

        private SessionHelper sessionHelper;
        
        // Used to populate locations
        private LS.LocationService locationService = null;

        private List<TDPLocation> venues = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationHelper()
        {
            sessionHelper = new SessionHelper();

            locationService = TDPServiceDiscovery.Current.Get<LS.LocationService>(ServiceDiscoveryKey.LocationService);
        }

        #endregion

        #region Public methods
                
        /// <summary>
        /// Peforms the location search using the location service
        /// </summary>
        /// <param name="search">Search object containing search details. 
        /// The search object is updated with ambiguous locations if found</param>
        /// <returns>Null if no location found</returns>
        public TDPLocation GetLocation(LocationSearch search)
        {
            try
            {
                if (search != null)
                {
                    if (search.SearchType == TDPLocationType.CoordinateLL)
                    {
                        // Use coordinate service to obtain an easting northing coordinate
                        search.GridReference = GetEastingNorthingCoordinate(new LatitudeLongitude(search.SearchId));
                    }
                    else if (search.SearchType == TDPLocationType.CoordinateEN)
                    {
                        search.GridReference = new OSGridReference(search.SearchId);
                    }

                    LogSearchInput(search); // For debugging

                    TDPLocation location = locationService.ResolveLocation(ref search, true, TDPSessionManager.Current.Session.SessionID);

                    LogSearchResult(search, location);

                    return location;
                }
            }
            catch (TDPException tdEx)
            {
                // Any exception, then its an unrecognised value, so default to unknown

                if (!tdEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, tdEx.Message, tdEx));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, ex.Message));
            }

            return null;
        }

        /// <summary>
        /// Gets the ambiguity location selectecd by the user, drilling down location if required
        /// </summary>
        public TDPLocation GetAmbiguityLocation(ref LocationSearch search, DropDownList ambiguityDrop, bool javascriptEnabled)
        {
            if (search == null)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning,
                    "Location search object was null when attempting to resolve ambiguity location"));

                return null;
            }

            // Exit if no ambiguity drop, or a location value hasn't been selected 
            if (ambiguityDrop == null || !ambiguityDrop.Visible || ambiguityDrop.SelectedItem.Value == DEFAULT_ITEM)
                return null;
            
            TDPLocation location = null;

            // Search object should contain the ambiguous locations (either from cache or gaz)

            // Determine which ambiguity list we're working with
            if (search.LocationCacheResultsExist)
            {
                #region Auto-suggest ambiguity resolution

                // Get the location from the selected dropdown value (see LocationControl for how it creates ListItems)
                TDPLocation choice = search.GetLocationCacheResult()
                    [Convert.ToInt32(ambiguityDrop.SelectedItem.Value)];

                // Set search values and perform a search with the chosen location
                search = new LocationSearch(choice.DisplayName, choice.ID, choice.TypeOfLocationActual, javascriptEnabled);

                location = GetLocation(search);

                #endregion
            }
            else if (search.LocationQueryResultsExist)
            {
                #region Gaz ambiguity resolution

                // Get the selected choice; this will be the same choice whether
                // the selected option was for a location or for a drill down into 
                // the location (the "+" will be ignored by the indexer)
                LocationChoice choice = (LocationChoice)search.GetLocationChoices(search.CurrentLevel())
                    [Convert.ToInt32(ambiguityDrop.SelectedItem.Value)];

                // Update the search object with the selected choice to allow the 
                // location service to return the populated location 
                // or further drilled down ambiguity locations
                search.LocationChoiceSelected = choice;

                // Indicate if the ambiguity resolution should dill down
                search.LocationChoiceDrillDown = ambiguityDrop.SelectedItem.Value.StartsWith("+");

                location = GetLocation(search);

                #endregion
            }

            return location;
        }

        /// <summary>
        /// Updates the existing TDPLocation into a TDPStopLocation using the location service
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public TDPStopLocation GetStopLocation(TDPLocation location)
        {
            #region Resolve CRS/SMS/IATA code

            //LocationService.Gazetteer.TDPCodeGazetteer gaz = new LocationService.Gazetteer.TDPCodeGazetteer();

            //// Should be NaPTAN
            //string searchCode = stopLocation.Code;

            //if (!string.IsNullOrEmpty(searchCode))
            //{
            //    LocationService.Gazetteer.CodeDetail[] codeDetails = gaz.FindCode(searchCode);

            //    foreach (LocationService.Gazetteer.CodeDetail codeDetail in codeDetails)
            //    {
            //        switch (codeDetail.CodeType)
            //        {
            //            case TDPCodeType.CRS:
            //                stopLocation.CodeCRS = codeDetail.Code; ;
            //                break;
            //            case TDPCodeType.SMS:
            //                stopLocation.CodeSMS = codeDetail.Code;
            //                break;
            //            case TDPCodeType.IATA:
            //                stopLocation.CodeIATA = codeDetail.Code;
            //                break;
            //            case TDPCodeType.NAPTAN:
            //            case TDPCodeType.Postcode:
            //            default:
            //                // Do nothing
            //                break;
            //        }
            //    }
            //}

            #endregion

            try
            {
                TDPStopLocation stopLocation = locationService.ResolveStopLocation(location);

                if (stopLocation != null)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                        string.Format("StopLocation updated[{0}]", stopLocation.ToString(false))));
                }

                return stopLocation;
            }
            catch (TDPException tdEx)
            {
                // Any exception, then its an invalid value, so default to unknown

                if (!tdEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, tdEx.Message, tdEx));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, ex.Message));
            }

            return null;
        }

        /// <summary>
        /// Checks if the location is accessible, if accessible journey required.
        /// Retrieves the TDPJourneyRequest from session to check
        /// </summary>
        /// <returns>True if its ok to plan accessible journey for location</returns>
        public bool CheckAccessibleLocation(bool isOrigin)
        {
            ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();
            TDPAccessiblePreferences accessiblePreference = tdpJourneyRequest.AccessiblePreferences;

            if (accessiblePreference.Accessible)
            {
                // Check to see if the location is accessible,
                // only when special assistance or step free access required
                if (accessiblePreference.RequireSpecialAssistance || accessiblePreference.RequireStepFreeAccess)
                {
                    LS.TDPLocation location = null;

                    // Only check for non-venues
                    if ((isOrigin) && (tdpJourneyRequest.Origin != null) && !(tdpJourneyRequest.Origin is LS.TDPVenueLocation))
                    {
                        location = tdpJourneyRequest.Origin;
                    }
                    else if ((!isOrigin) && (tdpJourneyRequest.Destination != null) && !(tdpJourneyRequest.Destination is LS.TDPVenueLocation))
                    {
                        location = tdpJourneyRequest.Destination;
                    }


                    if (location != null)
                    {
                        LS.LocationService locationService = TDPServiceDiscovery.Current.Get<LS.LocationService>(ServiceDiscoveryKey.LocationService);

                        // Check 1) Is location admin area/district in a GNAT area?
                        if (locationService.IsGNATAdminArea(location.AdminAreaCode, location.DistrictCode,
                            accessiblePreference.RequireStepFreeAccess,
                            accessiblePreference.RequireSpecialAssistance))
                        {
                            // Location is in a defined GNAT area, return True
                            location.Accessible = true;
                            return true;
                        }
                        // Check 2) Is location GNAT? 
                        // Only allow GNAT look up for locations with 1 Naptan.
                        // If naptan is not provided, possibly a locality, return False
                        // If more than one naptan provided, possibly an exchange group, return False
                        else if (location.Naptan.Count == 1 && locationService.IsGNAT(location.Naptan[0],
                                        accessiblePreference.RequireStepFreeAccess,
                                        accessiblePreference.RequireSpecialAssistance))
                        {
                            location.Accessible = true;
                            return true;
                        }
                        // Check 3) Is location point an accessible location - GIS Query check?
                        // Allow lookup for any location with a coordinate
                        else if ((location.GridRef != null && location.GridRef.IsValid)
                            && LocationService.LocationService.IsAccessibleLocation(
                                    location,
                                    accessiblePreference.RequireStepFreeAccess,
                                    accessiblePreference.RequireSpecialAssistance))
                        {
                            // Location is in the GIS Query accessible area, return True
                            location.Accessible = true;
                            return true;
                        }
                        else
                        {
                            // Location is not accessible
                            location.Accessible = false;
                            return false;
                        }
                    }
                }
            }

            // Otherwise valid to plan the accessible journey
            return true;
        }

        /// <summary>
        /// Checks if the venue location is accessible, if accessible journey required
        /// </summary>
        /// <remarks>
        /// AccessibleNaptans must be populated in the relevant venue locations prior call to this method
        /// </remarks>
        /// <returns>True if its ok to plan accessible journey for venue</returns>
        public bool CheckAccessibleLocationForVenue(bool isOrigin)
        {
            ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();
            TDPAccessiblePreferences accessiblePreference = tdpJourneyRequest.AccessiblePreferences;

            if (accessiblePreference.Accessible)
            {
                if ((isOrigin) && (tdpJourneyRequest.Origin != null))
                {
                    if (tdpJourneyRequest.Origin is LS.TDPVenueLocation)
                    {
                        LS.TDPVenueLocation originVenueLocation = tdpJourneyRequest.Origin as LS.TDPVenueLocation;

                        // Check the IsGNAT property to see if the venue is accessible
                        if (!originVenueLocation.IsGNAT)
                        {
                            // Not GNAT and if accessible naptans list is null or empty return false
                            if (originVenueLocation.AccessibleNaptans != null)
                            {
                                return originVenueLocation.AccessibleNaptans.Count > 0;
                            }
                            else
                                return false;
                        }
                    }
                }
                else if ((!isOrigin) && (tdpJourneyRequest.Destination != null))
                {
                    if (tdpJourneyRequest.Destination is LS.TDPVenueLocation)
                    {
                        LS.TDPVenueLocation destinationVenueLocation = tdpJourneyRequest.Destination as LS.TDPVenueLocation;

                        // Check the IsGNAT property to see if the venue is accessible
                        if (!destinationVenueLocation.IsGNAT)
                        {
                            // Not GNAT and if accessible naptans list is null or empty return false
                            if (destinationVenueLocation.AccessibleNaptans != null)
                            {
                                return destinationVenueLocation.AccessibleNaptans.Count > 0;
                            }
                            else
                                return false;
                        }
                    }
                }
            }

            // Otherwise valid to plan the accessible journey
            return true;
        }
        
        /// <summary>
        /// Method which gets an easting northing coordinate for a latitude longitudes
        /// </summary>
        /// <returns>null if fails or OSGridReference</returns>
        public OSGridReference GetEastingNorthingCoordinate(LatitudeLongitude latlong)
        {
            OSGridReference osgr = null;

            // Only perform the conversion if latitude longitude supported
            bool enabled = Properties.Current["LandingPage.Location.Coordinate.LatitudeLongitude.Switch"].Parse(false);
            
            if (enabled)
            {
                string logMessge = string.Empty;
                bool success = false;

                if (TDPTraceSwitch.TraceVerbose)
                {
                    logMessge = string.Format("Calling CoordinateConvertor to convert latitude/longitude location coordinate[{0}]",
                        (latlong != null) ? latlong.ToString() : string.Empty);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, logMessge));
                }

                try
                {
                    // Get the service used to convert the coordinates
                    ICoordinateConvertor coordinateConvertor = TDPServiceDiscovery.Current.Get<ICoordinateConvertor>(ServiceDiscoveryKey.CoordinateConvertor);

                    // Call web service
                    osgr = coordinateConvertor.GetOSGridReference(latlong);

                    // For logging
                    success = true;
                }
                catch (Exception ex)
                {
                    string message = string.Format("Calling CoordinateConvertor to convert latitude/longitude location coordinate[{0}] threw an exception: {1}",
                        (latlong != null) ? latlong.ToString() : string.Empty,
                        ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message, ex));
                }

                if (TDPTraceSwitch.TraceVerbose)
                {
                    logMessge = string.Format("Calling CoordinateConvertor to convert latitude/longitude location coordinate completed with success[{0}] latlong[{1}] eastnorth[{2}]",
                        success,
                        (latlong != null) ? latlong.ToString() : string.Empty,
                        (osgr != null) ? osgr.ToString() : string.Empty);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, logMessge));
                }
            }

            return osgr;
        }

        /// <summary>
        /// Method which gets a latitude longitude coordinate an easting northing
        /// </summary>
        /// <returns>null if fails or OSGridReference</returns>
        public LatitudeLongitude GetLatitudeLongitudeCoordinate(OSGridReference osgr)
        {
            LatitudeLongitude latlong = null;

            string logMessge = string.Empty;
                bool success = false;

                if (TDPTraceSwitch.TraceVerbose)
                {
                    logMessge = string.Format("Calling CoordinateConvertor to convert easting/northing location coordinate[{0}]",
                        (osgr != null) ? osgr.ToString() : string.Empty);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, logMessge));
                }

                try
                {
                    // Get the service used to convert the coordinates
                    ICoordinateConvertor coordinateConvertor = TDPServiceDiscovery.Current.Get<ICoordinateConvertor>(ServiceDiscoveryKey.CoordinateConvertor);

                    // Call web service
                    latlong = coordinateConvertor.GetLatitudeLongitude(osgr);

                    // For logging
                    success = true;
                }
                catch (Exception ex)
                {
                    string message = string.Format("Calling CoordinateConvertor to convert easting/northing location coordinate[{0}] threw an exception: {1}",
                        (osgr != null) ? osgr.ToString() : string.Empty,
                        ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message, ex));
                }

                if (TDPTraceSwitch.TraceVerbose)
                {
                    logMessge = string.Format("Calling CoordinateConvertor to convert easting/northing location coordinate completed with success[{0}] latlong[{1}] eastnorth[{2}]",
                        success,
                        (latlong != null) ? latlong.ToString() : string.Empty,
                        (osgr != null) ? osgr.ToString() : string.Empty);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, logMessge));
                }
            

            return latlong;
        }

        /// <summary>
        /// Returns a string of names for venue naptans 
        /// </summary>
        /// <returns></returns>
        public string GetLocationNames(List<string> venueNaptans)
        {
            // Get all the venues and store locally to prevent repeated calls to the service
            if (venues == null)
            {
                venues = locationService.GetTDPVenueLocations();
            }

            StringBuilder sb = new StringBuilder();
            TDPLocation venue = null;

            foreach (string naptan in venueNaptans)
            {
                // Find the venue for the naptan
                venue = venues.Find(delegate(TDPLocation loc) { return loc.ID == naptan; });
                if (venue != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append(venue.DisplayName);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns if venue locations are available in the cache
        /// </summary>
        /// <returns></returns>
        public bool VenueLocationsAvailable()
        {
            return (locationService.GetTDPVenueLocations().Count > 0);
        }

        /// <summary>
        /// Returns a validation message for ambiguous location searches, null if no message
        /// </summary>
        /// <param name="search"></param>
        /// <param name="isDestinationLocation"></param>
        /// <returns></returns>
        public TDPMessage GetLocationValidationMessage(LocationSearch search, bool isDestinationLocation, bool isVenueOnly)
        {
            TDPMessage message = new TDPMessage();

            if (search != null)
            {                
                // If coordinate location and if no location was found for it, 
                // then assume it's not able to be resolved.
                if (search.SearchType == TDPLocationType.CoordinateLL)
                {
                    // Assume couldnt resolve the coordiante to an easting northing
                    message.MessageResourceId = "LocationControl.ambiguityText.noLocationUKFound.Text";
                }
                else if (search.SearchType == TDPLocationType.CoordinateEN)
                {
                    // Assume couldnt resolve a locality for the coordinate
                    message.MessageResourceId = "LocationControl.ambiguityText.noLocationLocalityFound.Text";
                }
                else if (isVenueOnly)
                {
                    // Is in unknown mode for venue only input and a venue wasnt selected
                    message.MessageResourceId = "LocationControl.ambiguityText.chooseVenueLocation.Text";
                }
                else
                {
                    // Ambiguous locations
                    if (search.LocationQueryResultsExist)
                    {
                        // Set ambiguity instruction text
                        string locationText = (search.CurrentLevel() == 0) ?
                            search.SearchText : search.GetLocationQueryResult(search.CurrentLevel()).ParentChoice.Description;

                        message.MessageResourceId = "LocationControl.ambiguityText.Text";
                        message.MessageArgs.Add(locationText);
                    }
                    else if (search.LocationCacheResultsExist)
                    {
                        message.MessageResourceId = "LocationControl.ambiguityText.Text";
                        message.MessageArgs.Add(search.SearchText);
                    }
                    else
                    {
                        // No ambiguous location choices
                        if (search.SearchType == TDPLocationType.Address || search.SearchType == TDPLocationType.Postcode)
                        {
                            message.MessageResourceId = "LocationControl.ambiguityText.invalidPostcode.Text";
                            message.MessageArgs.Add(search.SearchText);
                        }
                        else
                        {
                            // No text entered
                            if (string.IsNullOrEmpty(search.SearchText) && string.IsNullOrEmpty(search.SearchId))
                            {
                                message.MessageResourceId = isDestinationLocation ?
                                    "LocationControl.ambiguityText.noLocationEntered.Destination.Text" : 
                                    "LocationControl.ambiguityText.noLocationEntered.Origin.Text";
                            }
                            // No ambiguous location choices
                            else
                            {
                                message.MessageResourceId = isDestinationLocation ?
                                    "LocationControl.ambiguityText.noLocationFound.Destination.Text" :
                                    "LocationControl.ambiguityText.noLocationFound.Origin.Text";
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(message.MessageResourceId))
                return null;
            else
                return message;
        }

        #endregion

        #region Private methods - Log

        /// <summary>
        /// Log search input text
        /// </summary>
        private void LogSearchInput(LocationSearch search)
        {
            // Log selected autosuggest values
            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                    string.Format("Search {0}", search.ToString()));
                Logger.Write(oe);
            }
        }

        /// <summary>
        /// Log search result location
        /// </summary>
        private void LogSearchResult(LocationSearch search, TDPLocation location)
        {
            // Log selected autosuggest values
            if (TDPTraceSwitch.TraceVerbose)
            {
                if (location != null)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                        string.Format("Location found [{0}] for Search[{1}]", location.ToString(false), search.ToString())));
                }
                else
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                        string.Format("No Location found for Search[{0}]", search.ToString())));

                    if (search.LocationCacheResultsExist)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                            string.Format("Ambiguous locations in cache found count[{0}] for Search[{1}]", search.LocationCacheResults.Count, search.ToString())));
                    }
                    else if (search.LocationQueryResultsExist)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                            string.Format("Ambiguous locations in gaz found count[{0}] for Search[{1}]", search.GetLocationChoices(search.CurrentLevel()).Count, search.ToString())));
                    }
                }
            }
        }

        #endregion
    }
}
