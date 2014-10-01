// *********************************************** 
// NAME             : LandingPageHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 13 Apr 2011
// DESCRIPTION  	: LandingPageHelper class providing methods to aid in page landing
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
using LS = TDP.Common.LocationService;
using System.Text;
using TDP.UserPortal.SessionManager;

namespace TDP.Common.Web
{
    /// <summary>
    /// LandingPageHelper class providing methods to aid in page landing
    /// </summary>
    public class LandingPageHelper
    {
        #region Private members

        // Used to hold all original query string values in a request
        private NameValueCollection queryString = new NameValueCollection();
        private bool isMobile = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LandingPageHelper()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LandingPageHelper(NameValueCollection queryString)
            : this()
        {
            if (queryString != null)
            {
                this.queryString = queryString;
            }
        }

        /// <summary>
        /// Constructor that sets isMobile property
        /// </summary>
        public LandingPageHelper(NameValueCollection queryString, bool isMobile)
            : this()
        {
            if (queryString != null)
            {
                this.queryString = queryString;
            }

            this.isMobile = isMobile;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Checks the query string to determine if there are values which would indicate this is a
        /// landing page request
        /// </summary>
        /// <returns></returns>
        public bool IsLandingPageRequest(PageId pageId, List<PageId> bookmarkJourneyPageIds, bool pageTransfered, bool isPostback)
        {
            bool result = false;

            if (bookmarkJourneyPageIds == null)
                bookmarkJourneyPageIds = new List<PageId>();

            // Check for any parameters considered as a landing page parameter
            if (ContainsLandingPageParameters(queryString)) 
            {
                // If auto plan is provided, and isn't a postback, then user has manually created 
                // landing page url as does not add
                if ((queryString[QueryStringKey.AutoPlan] != null) && (!isPostback))
                {
                    result = true;
                }
                // If there isnt a journey request hash, and isn't a postback, then user has manually 
                // created landing page url as does add to all pages other than JourneyPlannerInput
                else if ((queryString[QueryStringKey.JourneyRequestHash] == null) && (!isPostback))
                {
                    result = true;
                }
                // If there is a journey request hash, and page id is a bookmark journey page (e.g. JourneyOptions), 
                // and page hasn't been  transferred (i.e. transitioned from another page on site), and isn't a postback,
                // then likely user has bookmarked the journey (e.f. on JourneyOptions page), and is returning using that link
                else if ((queryString[QueryStringKey.JourneyRequestHash] != null) &&
                         (bookmarkJourneyPageIds.Contains(pageId)) && (!pageTransfered) && (!isPostback))
                {
                    result = true;
                }
                // If there is a journey request hash, and page id is a bookmark journey page (e.g. JourneyOptions), 
                // and page has been transferred (i.e. transitioned from another page on site), and isn't a postback,
                // then likely user has submitted a journey from the input page - this isn't a landing page request. 
                // To check if the user has submitted "another" landing page URL, test the journey 
                // request hash doesn't exist in their session - thus confirming it is a new landing page request
                else if ((queryString[QueryStringKey.JourneyRequestHash] != null) &&
                         (bookmarkJourneyPageIds.Contains(pageId)) && (pageTransfered) && (!isPostback))
                {
                    string journeyRequestHash = queryString[QueryStringKey.JourneyRequestHash];

                    SessionHelper sessionHelper = new SessionHelper();

                    ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest(journeyRequestHash);

                    if (tdpJourneyRequest == null)
                    {
                        result = true;
                    }
                    // If the request is in session, but hasn't been submitted, then likely user has submitted the same
                    // landing page url twice, without planning the journey, therefore treat as a new landing page request
                    else if (!tdpJourneyRequest.JourneyRequestSubmitted)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns true if the Landing Page Auto Plan flag is found in the query string
        /// </summary>
        /// <returns></returns>
        public bool IsAutoPlan()
        {
            bool result = false;

            if ((queryString.Count > 0) && (queryString[QueryStringKey.AutoPlan] != null))
            {
                result = GetBoolean(queryString[QueryStringKey.AutoPlan]);
            }

            return result;
        }

        /// <summary>
        /// Returns the Landing Page Partner value from the query string
        /// </summary>
        /// <returns></returns>
        public string GetPartner()
        {
            string result = string.Empty;

            if ((queryString.Count > 0) && (queryString[QueryStringKey.Partner] != null))
            {
                result = queryString[QueryStringKey.Partner];

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Trim().ToUpper();
                }

                // Database column length limit
                if (result.Length > 50)
                {
                    result = result.Substring(0, 50);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns true if the query string contains any parameters used for landing page
        /// </summary>
        /// <returns></returns>
        public bool ContainsLandingPageParameters(NameValueCollection queryString)
        {
            if (queryString != null)
            {
                if (!string.IsNullOrEmpty(queryString[QueryStringKey.OriginId]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.OriginType]))
                    return true;
                // Not checking name because it is only used with the location id and type
                //else if (!string.IsNullOrEmpty(queryString[QueryStringKey.OriginName]))
                //    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.DestinationId]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.DestinationType]))
                    return true;
                // Not checking name because it is only used with the location id and type
                //else if (!string.IsNullOrEmpty(queryString[QueryStringKey.DestinationName]))
                //    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.OutwardDate]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.OutwardTime]))
                    return true;
                // Not checking name because it is only used with the outward time
                //else if (!string.IsNullOrEmpty(queryString[QueryStringKey.OutwardTimeType]))
                //    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.ReturnDate]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.ReturnTime]))
                    return true;
                // Not checking name because it is only used with the return time
                //else if (!string.IsNullOrEmpty(queryString[QueryStringKey.ReturnTimeType]))
                //    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.OutwardRequired]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.ReturnRequired]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.PlannerMode]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.ExcludeTransportMode]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.Partner]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.AccessibleOption]))
                    return true;
                else if (!string.IsNullOrEmpty(queryString[QueryStringKey.FewestChanges]))
                    return true;
            }

            // No landing page parameters
            return false;
        }

        /// <summary>
        /// Builds a dictionary of QueryStringKey strings and values which represent the 
        /// TDPJourneyRequest that can be appended to a page url, which then becomes a 
        /// landing page url for the current request
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        /// <returns></returns>
        public Dictionary<string,string> BuildJourneyRequestForQueryString(ITDPJourneyRequest tdpJourneyRequest)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (tdpJourneyRequest != null)
            {
                // Origin location
                result.Add(QueryStringKey.OriginId, tdpJourneyRequest.Origin.ID);
                result.Add(QueryStringKey.OriginType, GetTDPLocationType(tdpJourneyRequest.Origin.TypeOfLocation));

                // Origin location name for coordinate locations only
                if (tdpJourneyRequest.Origin.TypeOfLocation == TDPLocationType.CoordinateEN)
                {
                    result.Add(QueryStringKey.OriginName, HttpContext.Current.Server.UrlEncode(tdpJourneyRequest.Origin.Name.SubstringFirst(30)));
                }

                // Destination location
                result.Add(QueryStringKey.DestinationId, tdpJourneyRequest.Destination.ID);
                result.Add(QueryStringKey.DestinationType, GetTDPLocationType(tdpJourneyRequest.Destination.TypeOfLocation));

                // Destination location name for coordinate locations only
                if (tdpJourneyRequest.Destination.TypeOfLocation == TDPLocationType.CoordinateEN)
                {
                    result.Add(QueryStringKey.DestinationName, HttpContext.Current.Server.UrlEncode(tdpJourneyRequest.Destination.Name.SubstringFirst(30)));
                }

                // Datetimes
                result.Add(QueryStringKey.OutwardDate, tdpJourneyRequest.OutwardDateTime.ToString("yyyyMMdd"));
                result.Add(QueryStringKey.OutwardTime, tdpJourneyRequest.OutwardDateTime.ToString("HHmm"));
                result.Add(QueryStringKey.OutwardTimeType, GetArriveByBoolean(tdpJourneyRequest.OutwardArriveBefore));

                // Outward journey not required, determines if input page is shown in "plan return journey only" mode
                if (!tdpJourneyRequest.IsOutwardRequired)
                {
                    result.Add(QueryStringKey.OutwardRequired, GetBoolean(tdpJourneyRequest.IsOutwardRequired));
                }

                // Return date time and is required
                if (tdpJourneyRequest.IsReturnRequired)
                {
                    result.Add(QueryStringKey.ReturnDate, tdpJourneyRequest.ReturnDateTime.ToString("yyyyMMdd"));
                    result.Add(QueryStringKey.ReturnTime, tdpJourneyRequest.ReturnDateTime.ToString("HHmm"));
                    result.Add(QueryStringKey.ReturnTimeType, GetArriveByBoolean(tdpJourneyRequest.ReturnArriveBefore));
                    result.Add(QueryStringKey.ReturnRequired, GetBoolean(tdpJourneyRequest.IsReturnRequired));
                }

                // Planner mode
                result.Add(QueryStringKey.PlannerMode, GetPlannerMode(tdpJourneyRequest.PlannerMode));
                
                // Accessible preferences and only if required)
                if (tdpJourneyRequest.AccessiblePreferences != null && tdpJourneyRequest.AccessiblePreferences.Accessible)
                {
                    result.Add(QueryStringKey.AccessibleOption, GetAccessiblePreferences(tdpJourneyRequest.AccessiblePreferences));

                    if (tdpJourneyRequest.AccessiblePreferences.RequireFewerInterchanges)
                    {
                        result.Add(QueryStringKey.FewestChanges, GetBoolean(tdpJourneyRequest.AccessiblePreferences.RequireFewerInterchanges));
                    }
                }

                // Transport modes to exclude
                string excludeModes = GetTransportModes(tdpJourneyRequest.Modes, tdpJourneyRequest.PlannerMode, tdpJourneyRequest.AccessiblePreferences);
                if (excludeModes.Length > 0)
                    result.Add(QueryStringKey.ExcludeTransportMode, excludeModes);

                // DO NOT ADD AUTOPLAN FLAG
            }

            return result;
        }

        /// <summary>
        /// Uses values from the Query string to return a populated TDPJourneyRequest.
        /// Returns null if no query string values available. 
        /// Returns request with as much populated values as it can create.
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyRequest RetrieveJourneyRequestFromQueryString()
        {
            ITDPJourneyRequest tdpJourneyRequest = null;

            // Error message keys
            string invalidLocationsKey = isMobile ? "Landing.Message.InvalidLocations.Mobile.Text" : "Landing.Message.InvalidLocations.Text";
            string invalidDestinationKey = isMobile ? "Landing.Message.InvalidDestination.Mobile.Text" : "Landing.Message.InvalidDestination.Text";
            string invalidOriginKey = isMobile ? "Landing.Message.InvalidOrigin.Mobile.Text" : "Landing.Message.InvalidOrigin.Text";

            if ((queryString != null) && (queryString.Count > 0))
            {
                // Validation messages
                List<TDPMessage> messages = new List<TDPMessage>();
                
                // Firsty message, want to display this first if any other messages are added
                messages.Add(new TDPMessage("Landing.Message.CheckTravelOptions.Text", TDPMessageType.Error));

                #region Read values

                // Read values from query string
                string originId = queryString[QueryStringKey.OriginId];
                string originType = queryString[QueryStringKey.OriginType];
                string originName = queryString[QueryStringKey.OriginName];
                string destinationId = queryString[QueryStringKey.DestinationId];
                string destinationType = queryString[QueryStringKey.DestinationType];
                string destinationName = queryString[QueryStringKey.DestinationName];

                string outwardDate = queryString[QueryStringKey.OutwardDate];
                string outwardTime = queryString[QueryStringKey.OutwardTime];
                string outwardTimeType = queryString[QueryStringKey.OutwardTimeType];
                string returnDate = queryString[QueryStringKey.ReturnDate];
                string returnTime = queryString[QueryStringKey.ReturnTime];
                string returnTimeType = queryString[QueryStringKey.ReturnTimeType];

                string outwardRequired = queryString[QueryStringKey.OutwardRequired];
                string returnRequired = queryString[QueryStringKey.ReturnRequired];

                string plannerMode = queryString[QueryStringKey.PlannerMode];
                string excludeModes = queryString[QueryStringKey.ExcludeTransportMode];

                string accessibleOption = queryString[QueryStringKey.AccessibleOption];
                string fewestChanges = queryString[QueryStringKey.FewestChanges];

                #endregion

                #region Parse and validate values

                #region Resolve locations

                // Parse values to be used in building the request
                TDPLocationType originLocationType = GetTDPLocationType(originType, messages);
                TDPLocationType destinationLocationType = GetTDPLocationType(destinationType, messages);

                // Outward required, assume true
                bool isOutwardRequired = true;
                if (!string.IsNullOrEmpty(outwardRequired))
                {
                    isOutwardRequired = GetBoolean(outwardRequired);
                }

                // Return required 
                bool isReturnRequired = GetBoolean(returnRequired);

                // Locations

                // Handle Unknown location type scenario
                UpdateLocationValues(ref originLocationType, ref originId, ref originName);
                UpdateLocationValues(ref destinationLocationType, ref destinationId, ref destinationName);
                
                LocationSearch originSearch = new LocationSearch(
                        HttpContext.Current.Server.UrlDecode(originName),
                        HttpContext.Current.Server.UrlDecode(originId),
                        originLocationType,
                        true);
                LocationSearch destinationSearch = new LocationSearch(
                        HttpContext.Current.Server.UrlDecode(destinationName), 
                        HttpContext.Current.Server.UrlDecode(destinationId), 
                        destinationLocationType,
                        true);

                LocationHelper locationHelper = new LocationHelper();

                TDPLocation originLocation = locationHelper.GetLocation(originSearch);
                TDPLocation destinationLocation = locationHelper.GetLocation(destinationSearch);

                #region Persist ambiguous locations to session

                // Persist the location search values if request contained and were invalid, 
                // to allow location input page to display/resolve ambiguous where necessary
                InputPageState pageState = TDPSessionManager.Current.PageState;
                TDPMessage tdpMessage = null;
                if (originLocation == null && (!string.IsNullOrEmpty(originId) || !string.IsNullOrEmpty(originName)))
                {
                    pageState.OriginSearch = originSearch;

                    // Message to display
                    tdpMessage = locationHelper.GetLocationValidationMessage(originSearch, false, false);
                }
                else if (IsAutoPlan())
                {
                    // If auto plan in progress then persist search to allow location control to handle
                    // and display validation message
                    pageState.OriginSearch = originSearch;
                }
                else
                    pageState.OriginSearch = null;

                if (destinationLocation == null && (!string.IsNullOrEmpty(destinationId) || !string.IsNullOrEmpty(destinationName)))
                {
                    pageState.DestinationSearch = destinationSearch;

                    // Give preference to origin message
                    if (tdpMessage == null)
                        // Message to display
                        tdpMessage = locationHelper.GetLocationValidationMessage(destinationSearch, true, false);
                }
                else if (IsAutoPlan())
                {
                    // If auto plan in progress then persist search to allow location control to handle
                    // and display validation message
                    pageState.DestinationSearch = destinationSearch;
                }
                else
                    pageState.DestinationSearch = null;

                if (tdpMessage != null && !string.IsNullOrEmpty(tdpMessage.MessageResourceId))
                {
                    messages.Add(tdpMessage);
                }

                #endregion

                #endregion

                #region Validate location types and direction required

                // Commented out as no longer need to force one location to be a venue

                // Both origin and destination was supplied, check they are not both non-venues
                if ((!string.IsNullOrEmpty(originId)) && (!string.IsNullOrEmpty(destinationId)))
                {
                    //// Test they are not both non-venues
                    //if ((originLocation != null) && (destinationLocation != null))
                    //{
                    //    if (originLocation.TypeOfLocation != TDPLocationType.Venue
                    //        && destinationLocation.TypeOfLocation != TDPLocationType.Venue)
                    //    {
                    //        messages.Add(new TDPMessage(invalidLocationsKey, TDPMessageType.Error));

                    //        // Because both are not a venue, null one location to ensure location control renders correctly
                    //        // with a venue dropdown
                    //        if (isOutwardRequired)
                    //        {
                    //            destinationLocation = null;
                    //        }
                    //        else
                    //        {
                    //            originLocation = null;
                    //        }
                    //    }
                    //}
                    //else if ((originLocation == null) && (destinationLocation != null))
                    //{
                    //    // No origin location, and destination is not a venue
                    //    if (destinationLocation.TypeOfLocation != TDPLocationType.Venue)
                    //    {
                    //        messages.Add(new TDPMessage(invalidLocationsKey, TDPMessageType.Error));
                    //    }
                    //}
                    //else if ((originLocation != null) && (destinationLocation == null))
                    //{
                    //    // No destination location, and origin is not a venue
                    //    if (originLocation.TypeOfLocation != TDPLocationType.Venue)
                    //    {
                    //        messages.Add(new TDPMessage(invalidLocationsKey, TDPMessageType.Error));
                    //    }
                    //}
                    //else if ((originLocation == null) && (destinationLocation == null))
                    //{
                    //    // Neither destination was resolved correctly
                    //    messages.Add(new TDPMessage(invalidLocationsKey, TDPMessageType.Error));
                    //}
                }
                // Only destination was supplied, check it is a venue
                else if ((!string.IsNullOrEmpty(destinationId)) && (isOutwardRequired))
                {
                    //// Test destination is a venue
                    //if ((destinationLocation == null) ||
                    //    ((destinationLocation != null) && (destinationLocation.TypeOfLocation != TDPLocationType.Venue)))
                    //{
                    //    destinationLocation = null;

                    //    messages.Add(new TDPMessage(invalidDestinationKey, TDPMessageType.Error));
                    //}
                }
                // Only origin was supplied (in a "return only" journey), check it is a venue
                else if ((!string.IsNullOrEmpty(originId)) && (!isOutwardRequired) && (isReturnRequired))
                {
                    //// Test origin is a venue
                    //if ((originLocation == null) ||
                    //    ((originLocation != null) && (originLocation.TypeOfLocation != TDPLocationType.Venue)))
                    //{
                    //    originLocation = null;

                    //    messages.Add(new TDPMessage(invalidOriginKey, TDPMessageType.Error));
                    //}
                }

                // Confirm location and required direction are for a valid scenario, 
                // i.e. if origin location was supplied for a "return only" journey, and it isn't a venue,
                // then make it an outward journey required rather than losing the origin location which might be required
                if (originLocation != null
                    && originLocation.TypeOfLocation != TDPLocationType.Venue
                    && !isOutwardRequired)
                {
                    isOutwardRequired = true;
                }

                #endregion

                // Datetimes
                DateTime outwardDateTime = GetDateTime(outwardDate, outwardTime, true, messages);
                DateTime returnDateTime = DateTime.MinValue;

                if (isReturnRequired)
                {
                    returnDateTime = GetDateTime(returnDate, returnTime, false, messages);
                }

                bool outwardArriveBy = GetArriveByBoolean(outwardTimeType);
                bool returnArriveBy = GetArriveByBoolean(returnTimeType);
                                
                // Planner mode
                TDPJourneyPlannerMode tdpPlannerMode = GetPlannerMode(plannerMode, messages);
                                
                // Accessible preferences
                TDPAccessiblePreferences accessiblePreferences = GetAccessiblePreferences(accessibleOption, messages);

                bool isFewestChangesRequired = GetBoolean(fewestChanges);
                accessiblePreferences.RequireFewerInterchanges = isFewestChangesRequired;

                // Transport modes
                List<TDPModeType> modes = GetTransportModes(excludeModes, tdpPlannerMode, accessiblePreferences);
                
                #endregion

                try
                {
                    JourneyRequestHelper jrh = new JourneyRequestHelper();

                    tdpJourneyRequest = jrh.BuildTDPJourneyRequest(
                        originLocation, destinationLocation, 
                        outwardDateTime, returnDateTime,
                        outwardArriveBy, returnArriveBy,
                        isOutwardRequired, isReturnRequired, (!isOutwardRequired && isReturnRequired),
                        tdpPlannerMode, modes,
                        accessiblePreferences);
                }
                catch (TDPException tdpEx)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error,
                        string.Format("Error building TDPJourneyRequest from QueryString values"), tdpEx));
                }

                // Add any messages to display
                SessionHelper sessionHelper = new SessionHelper();

                if (messages.Count > 1) // Default message is always added (see above) so check for any more validations
                {
                    sessionHelper.AddMessages(messages);
                }
            }
            
            return tdpJourneyRequest;
        }

        #region Public parse methods

        /// <summary>
        /// Parses the query string exclude transport modes and returns a list of modes to use
        /// for the journey request
        /// </summary>
        /// <param name="excludeModes"></param>
        /// <returns></returns>
        public List<TDPModeType> GetTransportModes(string excludeModes, TDPJourneyPlannerMode plannerMode, TDPAccessiblePreferences accessiblePreferences)
        {
            // Get the default list of transport modes for this planner mode
            List<TDPModeType> modes = JourneyRequestHelper.PopulateModes(plannerMode, accessiblePreferences);

            if (!string.IsNullOrEmpty(excludeModes))
            {
                // Remove any modes specified in the exclude list
                foreach (char c in excludeModes.ToCharArray())
                {
                    TDPModeType exMode = TDPModeTypeHelper.GetTDPModeTypeQS(c.ToString());

                    if (modes.Contains(exMode))
                    {
                        modes.Remove(exMode);
                    }

                    // Special conditions:
                    // Bus and Coach to be treated as one exclude mode
                    if (exMode == TDPModeType.Bus || exMode == TDPModeType.Coach)
                    {
                        if (modes.Contains(TDPModeType.Bus))
                            modes.Remove(TDPModeType.Bus);
                        if (modes.Contains(TDPModeType.Coach))
                            modes.Remove(TDPModeType.Coach);
                    }
                    // Underground and Metro to be treated as one exclude mode
                    else if (exMode == TDPModeType.Underground || exMode == TDPModeType.Metro)
                    {
                        if (modes.Contains(TDPModeType.Underground))
                            modes.Remove(TDPModeType.Underground);
                        if (modes.Contains(TDPModeType.Metro))
                            modes.Remove(TDPModeType.Metro);
                    }
                    // Tram and Drt to be treated as one exclude mode
                    else if (exMode == TDPModeType.Tram || exMode == TDPModeType.Drt)
                    {
                        if (modes.Contains(TDPModeType.Tram))
                            modes.Remove(TDPModeType.Tram);
                        if (modes.Contains(TDPModeType.Drt))
                            modes.Remove(TDPModeType.Drt);
                    }
                }
            }

            return modes;
        }

        /// <summary>
        /// Returns a query string representation of the TDPModeTypes
        /// </summary>
        /// <param name="modes"></param>
        /// <returns></returns>
        public string GetTransportModes(List<TDPModeType> modes, TDPJourneyPlannerMode plannerMode, TDPAccessiblePreferences accessiblePreferences)
        {
            // Get the default list of transport modes for this planner mode
            List<TDPModeType> allModes = JourneyRequestHelper.PopulateModes(plannerMode, accessiblePreferences);
                        
            StringBuilder sbModes = new StringBuilder();

            // Identify any modes which are not in the default list
            foreach (TDPModeType mode in allModes)
            {
                if (!modes.Contains(mode))
                {
                    sbModes.Append(TDPModeTypeHelper.GetTDPModeTypeQS(mode));
                }
            }

            return sbModes.ToString();
        }

        #endregion

        #endregion

        #region Private methods

        #region Parse methods

        /// <summary>
        /// Parses the query string location type value into an TDPLocationType
        /// </summary>
        /// <returns>Default is TDPLocationType.Unknown </returns>
        private TDPLocationType GetTDPLocationType(string qsLocationType, List<TDPMessage> messages)
        {
            try
            {
                // If location type hasn't been specified set it to unknown 
                // - no longer treaing unspecified locations as Venues
                if (string.IsNullOrEmpty(qsLocationType))
                {
                    return TDPLocationType.Unknown;
                }


                return TDPLocationTypeHelper.GetTDPLocationTypeQS(qsLocationType);
            }
            catch
            {
                // Any exception, then its an unrecognised value, so default to unknown
                return TDPLocationType.Unknown;
            }
        }

        /// <summary>
        /// Parses the query string location type value into an TDPLocationType
        /// </summary>
        /// <returns>Default is TDPLocationType.Unknown </returns>
        private string GetTDPLocationType(TDPLocationType locationType)
        {
            return TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);
        }

        /// <summary>
        /// Uses the query string date and time values to return a DateTime
        /// </summary>
        /// <param name="date">Date in format yyyymmdd</param>
        /// <param name="time">Time in format hhmm</param>
        /// <returns>Default DateTime as now</returns>
        private DateTime GetDateTime(string qsDate, string qsTime, bool isOutward, List<TDPMessage> messages)
        {
            #region Set initial datetime, and the configured start and end datetimes

            DateTime defStartDate = DateTime.MinValue;
            DateTime defEndDate = DateTime.MaxValue;
            DateTime start = DateTime.Now;
            DateTime end = DateTime.MaxValue;

            // Define the start and end datetimes
            DateTimeHelper.SetDefaultDateRange(ref defStartDate, ref defEndDate, true);
            DateTimeHelper.SetDefaultTimeForDate(defStartDate, ref start, false);
            DateTimeHelper.SetDefaultTimeForDate(defEndDate, ref end, true);
                        
            // Get now (ignoring seconds)
            DateTime dtNow = DateTime.Now;
            DateTime now = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, 0);

            // Initial defaults to now
            DateTime dtInitial = DateTimeHelper.GetRoundedTime(now);

            // If now earlier than configured Start date, set to default Start datetime
            if (dtInitial < defStartDate)
            {
                dtInitial = start;
            }
            // If "today", then update initial to either be the configured Start time, or Now time, whichever is later
            if ((dtInitial.Date == now.Date) && (now.TimeOfDay < start.TimeOfDay))
            {
                dtInitial = new DateTime(now.Year, now.Month, now.Day, start.Hour, start.Minute, 0);
            }
            // If initial after configured End date, set to End datetime
            if (dtInitial > defEndDate)
            {
                dtInitial = end;
            }
                        
            #endregion

            try
            {
                // Set the initial datetime values
                int year = dtInitial.Year;
                int month = dtInitial.Month;
                int day = dtInitial.Day;
                int hour = dtInitial.Hour;
                int minute = dtInitial.Minute;
                int second = dtInitial.Second;
                
                // Build up the datetime using querystring, any errors then return the default datetime 
                if (!string.IsNullOrEmpty(qsDate))
                {
                    year = Convert.ToInt32(qsDate.Substring(0, 4));
                    month = Convert.ToInt32(qsDate.Substring(4, 2));
                    day = Convert.ToInt32(qsDate.Substring(6, 2));
                }
                if (!string.IsNullOrEmpty(qsTime))
                {
                    hour = Convert.ToInt32(qsTime.Substring(0, 2));
                    minute = Convert.ToInt32(qsTime.Substring(2, 2));
                }

                DateTime datetime = new DateTime(year, month, day, hour, minute, 0);

                // Anything before configured Start date is an invalid datetime, so default to initial
                if (datetime < defStartDate)
                {
                    datetime = dtInitial;
                    
                    messages.Add(new TDPMessage("Landing.Message.InvalidDateInPast.Text", TDPMessageType.Error));
                }
                // Or datetime is for "today", and is before time now for today
                else if ((datetime.Date == now.Date) && (datetime.TimeOfDay < now.TimeOfDay))
                {
                    datetime = dtInitial;

                    messages.Add(new TDPMessage("Landing.Message.InvalidDateInPast.Text", TDPMessageType.Error));
                }
                // Anything later then the configured End date is an invalid datetime, so default to initial
                else if (datetime > defEndDate)
                {
                    datetime = dtInitial;

                    messages.Add(new TDPMessage("Landing.Message.InvalidDateInPast.Text", TDPMessageType.Error));
                }
                
                return datetime;
            }
            catch
            {
                messages.Add(new TDPMessage("Landing.Message.InvalidDate.Text", TDPMessageType.Error));

                // Any exception, then return the min datetime. The date control, journey plan runner will
                // validate/update it if needed
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Uses the query string boolean flag values to return a Boolean
        /// </summary>
        /// <param name="flag">Default to false</param>
        private bool GetBoolean(string qsFlag)
        {
            try
            {
                // Check for 0 or 1
                if (qsFlag.Trim() == "0")
                {
                    return false;
                }
                else if (qsFlag.Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return Convert.ToBoolean(qsFlag.Trim().ToLower());
                }
            }
            catch
            {
                // Any exception, return false
                return false;
            }
        }

        /// <summary>
        /// Returns a query string representation of a Bool
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private string GetBoolean(bool flag)
        {
            if (flag)
                return "1";
            else
                return "0";
        }

        /// <summary>
        /// Uses the query string arrive by/leave at flag values to return a boolean
        /// </summary>
        /// <param name="qsFlag"></param>
        /// <returns>True if arrive by, False (default) if leave at</returns>
        private bool GetArriveByBoolean(string qsFlag)
        {
            try
            {
                // Check for a (arrive by) or d (depart/leave at)
                if (qsFlag.ToLower().Trim() == "a")
                {
                    return true;
                }
                else if (qsFlag.ToLower().Trim() == "d")
                {
                    return false;
                }
            }
            catch
            {
                // Any exceptions ignore, return false
            }
            return false;
        }

        /// <summary>
        /// Returns a query string representation of an Arrive By boolean
        /// </summary>
        /// <param name="flag">True for Arrive by, False for leave at</param>
        private string GetArriveByBoolean(bool flag)
        {
            if (flag)
                return "a";
            else
                return "d";
        }

        /// <summary>
        /// Parses the query string planner mode value into an TDPJourneyPlannerMode
        /// </summary>
        /// <param name="qsPlannerMode"></param>
        /// <returns></returns>
        private TDPJourneyPlannerMode GetPlannerMode(string qsPlannerMode, List<TDPMessage> messages)
        {
            return TDPJourneyPlannerModeHelper.GetTDPJourneyPlannerModeQS(qsPlannerMode);            
        }

        /// <summary>
        /// Returns a query string representation of the TDPJourneyPlannerMode
        /// </summary>
        /// <param name="qsPlannerMode"></param>
        /// <returns></returns>
        private string GetPlannerMode(TDPJourneyPlannerMode plannerMode)
        {
            return TDPJourneyPlannerModeHelper.GetTDPJourneyPlannerModeQS(plannerMode);
        }
        
        /// <summary>
        /// Parses the query string accessible options value into an TDPAccessiblePreferences
        /// </summary>
        /// <param name="qsAccessibleOptions"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        private TDPAccessiblePreferences GetAccessiblePreferences(string qsAccessibleOption, List<TDPMessage> messages)
        {
            TDPAccessiblePreferences tdpAccessiblePreference = new TDPAccessiblePreferences();

            tdpAccessiblePreference.PopulateAccessiblePreference(qsAccessibleOption);
                        
            return tdpAccessiblePreference;
        }

        /// <summary>
        /// Returns a query string representation of the TDPAccessiblePreferences
        /// </summary>
        /// <param name="qsPlannerMode"></param>
        /// <returns></returns>
        private string GetAccessiblePreferences(TDPAccessiblePreferences tdpAccessiblePreference)
        {
            if (tdpAccessiblePreference != null)
            {
                return tdpAccessiblePreference.GetAccessiblePreferenceString();
            }

            return string.Empty;
        }

        #endregion

        /// <summary>
        /// Updates the location values for Unknown location type supplied
        /// </summary>
        private void UpdateLocationValues(ref TDPLocationType locationType, ref string locationId, ref string locationName)
        {
            // Check if Unknown type is supplied and no location name, then need to fall into 
            // ambiguity location search
            if (locationType == TDPLocationType.Unknown)
            {
                if (string.IsNullOrEmpty(locationName) && !string.IsNullOrEmpty(locationId))
                {
                    // id was supplied, this is used as the search text
                    locationName = locationId;
                }

                if (!string.IsNullOrEmpty(locationName))
                {
                    // Check for postcode location and update search type,
                    // this will allow location resolution to fall into the appropriate logic
                    if (locationName.IsValidPostcode() || locationName.IsValidPartPostcode())
                    {
                        locationType = TDPLocationType.Postcode;
                    }
                    else if (locationName.IsContainsPostcode() && locationName.IsNotSingleWord())
                    {
                        locationType = TDPLocationType.Address;
                    }
                }
            }
            else if (string.IsNullOrEmpty(locationId) && string.IsNullOrEmpty(locationName))
            {
                // Where no location id name provided, reset to unknown to remove location 
                // type specific error message on location control
                locationType = TDPLocationType.Unknown;
            }
        }

        #endregion
    }
}