// *********************************************** 
// NAME             : StopInformationHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: StopInformationHelper class providing helper methods for stop information
// ************************************************
// 

using System.Collections.Specialized;
using System.Web;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.UserPortal.ScreenFlow;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.Web
{
    /// <summary>
    /// StopInformationHelper class providing helper methods for stop information
    /// </summary>
    public class StopInformationHelper
    {
        #region Private variables

        private LocationHelper locationHelper;
        private SessionHelper sessionHelper;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StopInformationHelper()
        {
            locationHelper = new LocationHelper();
            sessionHelper = new SessionHelper();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns true if stop information is available
        /// </summary>
        /// <returns></returns>
        public bool StopInformationAvailable()
        {
            return Properties.Current["StopInformation.Enabled.Switch"].Parse(false);
        }

        /// <summary>
        /// Returns the stop information mode for the stop information page
        /// </summary>
        /// <param name="readFromQueryString"></param>
        /// <returns></returns>
        public StopInformationMode GetStopInformationMode(bool readFromQueryString)
        {
            // Set to default
            StopInformationMode stopInformationMode = StopInformationModeHelper.GetStopInformationModeQS(string.Empty);

            if (readFromQueryString)
            {
                if (HttpContext.Current != null)
                {
                    NameValueCollection queryString = HttpContext.Current.Request.QueryString;

                    if ((queryString != null) && (queryString.Count > 0))
                    {
                        // Read values from query string
                        string modeString = queryString[QueryStringKey.StopInfoMode];

                        try
                        {
                            // Parse values to be used in building the request
                            stopInformationMode = StopInformationModeHelper.GetStopInformationModeQS(modeString);
                        }
                        catch (TDPException tdEx)
                        {
                            if (!tdEx.Logged)
                                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, tdEx.Message));
                        }

                        // Persist stopInformationMode to session for future use
                        sessionHelper.UpdateStopInformationMode(stopInformationMode);
                    }
                }
            }
            else
            {
                // Check session and retrieve mode from there
                stopInformationMode = sessionHelper.GetStopInformationMode();
            }

            return stopInformationMode;
        }

        /// <summary>
        /// Saves the stop information mode to session
        /// </summary>
        /// <param name="stopInformationMode"></param>
        public void UpdateStopInformationMode(StopInformationMode stopInformationMode)
        {
            // Persist stopInformationMode to session for future use
            sessionHelper.UpdateStopInformationMode(stopInformationMode);
        }

        /// <summary>
        /// Returns the location to display for the stop information page
        /// </summary>
        /// <param name="readFromQueryString"></param>
        /// <returns></returns>
        public TDPStopLocation GetStopInformationLocation(bool readFromQueryString)
        {
            TDPStopLocation stopLocation = null;

            if (readFromQueryString)
            {
                if (HttpContext.Current != null)
                {
                    NameValueCollection queryString = HttpContext.Current.Request.QueryString;

                    if ((queryString != null) && (queryString.Count > 0))
                    {
                        // Read values from query string
                        string originId = queryString[QueryStringKey.StopInfoOriginId];
                        string originType = queryString[QueryStringKey.StopInfoOriginType];

                        // Parse values to be used in building the request
                        TDPLocationType originLocationType = GetTDPLocationType(originType);

                        if (originLocationType != TDPLocationType.Unknown)
                        {
                            // Resolve the location
                            LocationSearch originSearch = new LocationSearch(
                                string.Empty,
                                HttpContext.Current.Server.UrlDecode(originId),
                                originLocationType,
                                true);

                            TDPLocation location = locationHelper.GetLocation(originSearch);

                            if (location != null)
                            {
                                // Populate stop location details
                                stopLocation = locationHelper.GetStopLocation(location);
                            }

                            // Persist stop location to session for future use
                            sessionHelper.UpdateStopLocation(stopLocation);
                        }
                    }
                }
            }
            else
            {
                // Check session and retrieve location from there
                stopLocation = sessionHelper.GetStopLocation();
            }

            return stopLocation;
        }

        /// <summary>
        /// Returns the service id to show on the stop information details page
        /// </summary>
        /// <param name="readFromQueryString"></param>
        /// <returns></returns>
        public string GetStopInformationServiceId(bool readFromQueryString)
        {
            string serviceId = string.Empty;

            if (readFromQueryString)
            {
                if (HttpContext.Current != null)
                {
                    NameValueCollection queryString = HttpContext.Current.Request.QueryString;

                    if ((queryString != null) && (queryString.Count > 0))
                    {
                        // Read values from query string
                        serviceId = queryString[QueryStringKey.StopInfoDetailServiceId];

                        if (!string.IsNullOrEmpty(serviceId))
                            serviceId = HttpContext.Current.Server.UrlDecode(serviceId);
                    }
                }
            }
            else
            {
                // Currently should only ever read from the querystring
            }

            return serviceId;
        }

        /// <summary>
        /// Builds stop information landing page url
        /// </summary>
        public string BuildStopInformationUrl(TDPPageMobile page, TDPLocation location, StopInformationMode mode)
        {
            URLHelper urlHelper = new URLHelper();
            NameValueCollection nvc = new NameValueCollection();

            if (location != null)
            {
                string locId = location.ID;
                string locType = TDPLocationTypeHelper.GetTDPLocationTypeQS(location.TypeOfLocation);

                if (string.IsNullOrEmpty(locId))
                {
                    // Check for naptan
                    if (location.Naptan != null && location.Naptan.Count > 0)
                    {
                        locId = location.Naptan[0];
                        locType = TDPLocationTypeHelper.GetTDPLocationTypeQS(TDPLocationType.Station);
                    }
                }

                // URL Parameters
                nvc.Add(QueryStringKey.StopInfoOriginId, locId);
                nvc.Add(QueryStringKey.StopInfoOriginType, locType);
                nvc.Add(QueryStringKey.StopInfoMode, StopInformationModeHelper.GetStopInformationModeQS(mode));
            }

            // Stop Info page url
            PageTransferDetail transferDetail = page.GetPageTransferDetail(PageId.MobileStopInformation);

            string url = page.ResolveClientUrl(transferDetail.PageUrl);

            url = urlHelper.AddQueryStringParts(url, nvc);

            return url;
        }
        
        /// <summary>
        /// Builds stop information detail landing page url, currently only for a station board service detail
        /// </summary>
        public string BuildStopInformationDetailUrl(TDPPageMobile page, string serviceId)
        {
            URLHelper urlHelper = new URLHelper();
            NameValueCollection nvc = new NameValueCollection();

            if (!string.IsNullOrEmpty(serviceId))
            {
                // URL Parameters
                nvc.Add(QueryStringKey.StopInfoDetailServiceId, 
                    HttpContext.Current.Server.UrlEncode(serviceId));
            }

            // Stop Info Detail page url
            PageTransferDetail transferDetail = page.GetPageTransferDetail(PageId.MobileStopInformationDetail);

            string url = page.ResolveClientUrl(transferDetail.PageUrl);

            url = urlHelper.AddQueryStringParts(url, nvc);

            return url;
        }
        
        #endregion

        #region Parse methods

        /// <summary>
        /// Parses the query string location type value into an TDPLocationType
        /// </summary>
        /// <returns>Default is TDPLocationType.Station </returns>
        private TDPLocationType GetTDPLocationType(string qsLocationType)
        {
            try
            {
                // If location type hasn't been specified set it to Station 
                if (string.IsNullOrEmpty(qsLocationType))
                {
                    return TDPLocationType.Station;
                }


                return TDPLocationTypeHelper.GetTDPLocationTypeQS(qsLocationType);
            }
            catch
            {
                // Any exception, then its an unrecognised value, so default to unknown
                return TDPLocationType.Unknown;
            }
        }

        #endregion
    }
}
