// *********************************************** 
// NAME                 : CO2LandingPage.aspx.cs
// AUTHOR               : Phil Scott
// DATE CREATED         : 17/01/2008
// DESCRIPTION			: CO2 page landing redirector
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/CO2LandingPage.aspx.cs-arc  $
//
//   Rev 1.3   Jul 19 2010 16:17:00   mmodi
//Corrected setting Distance value for fix for page landing from Word 2003
//Resolution for 5077: CO2 page landing not working from Word 2003
//
//   Rev 1.2   Mar 31 2008 13:24:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Jan 17 2008 13:29:04   p.scott
//Initial revision.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;


namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Summary description for CO2LandingPage.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class CO2LandingPage : TDPage
    {
        #region Declarations
        #region Controls
        protected TransportDirect.UserPortal.Web.Controls.HeadElementControl headElementControl;

        #endregion

        #region Private members
        // Default page on error
        PageId defaultPage = PageId.JourneyEmissionsCompare;

        // To ensure all parameters are valid
        bool validParameters = true;



        #endregion

        #region Search parameters
        /// <summary>
        /// Partner Id (shortcut "id")
        /// </summary>
        private string partnerId = String.Empty;
        /// <summary>
        /// Entry type (shortcut "et")
        /// </summary>
        private string entrytype = String.Empty;
        /// <summary>
        /// landing type (shortcut "lt")
        /// </summary>
        private string CO2Landingtype = String.Empty;
        /// <summary>
        /// Distance (shortcut "di")
        /// </summary>
        private string distance = String.Empty;
        /// <summary>
        /// units (shortcut "un") 
        /// e.g. "miles"  "km"
        /// </summary>
        private string units = String.Empty;



        /// <summary>
        /// modes (shortcut "lm")
        /// </summary>
        private string landingModes = "a";

        private bool modeAll = false;
        private bool modeSmallCar = true;
        private bool modeLargeCar = true;
        private bool modeTrain = true;
        private bool modeCoach = true;
        private bool modePlane = true;


        //private string mode = String.Empty;
        /// <summary>
        /// Auto plan flag (shortcut "p")
        /// </summary>
        private bool autoplanflag = false;

        #endregion

        #region Global definitions
        private static readonly char[] DELIMITER = new char[] { ',' };
        private static readonly char[] SECTION_SEP = new char[] { '&' };
        private static readonly char[] SUBSECTION_SEP = new char[] { ':' };
        private const int ID_SECTION = 0;
        private const int DISTANCE_SECTION = 1;
        private const int UNITS_SECTION = 2;
        private const int MODE_SECTION = 3;

        private const int ID_KEY = 0;
        private const int ID_VAL = 1;

        private const int DISTANCE_KEY = 0;
        private const int DISTANCE_VAL = 1;

        private const int UNITS_KEY = 0;
        private const int UNITS_VAL = 1;

        private const int MODE_KEY = 0;
        private const int MODE_VAL = 1;

        private const string HTTP_REQUEST_TYPE_GET = "GET";
        private const string HTTP_REQUEST_ID_STRING1 = "?id=";
        private const string HTTP_REQUEST_ID_STRING2 = "&id=";
        private const char AMPERSAND = '&';

        private const string PREFIX_HTTP = "http";
        private const string PREFIX_HTTPS = "https";
        private const string PREFIX_WWW = "www";

        private const int SAFE_PARTNERID_LENGTH = 10;

        private const string AUTOPLANOFF = "off";

        //final url that will do the redirect
        private string complete_url = String.Empty;

        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Page constructor
        /// </summary>
        public CO2LandingPage()
            : base()
        {
            pageId = PageId.CO2LandingPage;
        }
        #endregion

        #region Page Load
        private void Page_Load(object sender, System.EventArgs e)
        {
            #region Create session
            // create new session and itinerary managers
            ITDSessionManager sessionManager = TDSessionManager.Current;
            TDItineraryManager itineraryManager = TDItineraryManager.Current;
            // Reset the itinerary manager
            itineraryManager.NewSearch();

            #endregion

            #region Set parameters required in session
            // need to set the landingpage switch to true to identify the request as a Landing page request
            // this needs to be done before the search parameters are initialised since there are 
            // dependencies upon it
            TDSessionManager.Current.Session[SessionKey.LandingPageCheck] = true;

            //place the autoplan switch into the session object
            autoplanflag = GetBooleanSingleParam("p");

            TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan] = autoplanflag;

            #endregion

            #region Set redirect URL
            // Determine which page we need to redirect to
            entrytype = GetValidSingleParam("et");
            CO2Landingtype = GetValidSingleParam("lt");
            if (TDPage.SessionChannelName != null)
            {
                // Add system time to end of redirect URL (fake querystring param). This ensures IE always requests latest version of page rather than relying on cache. IR3360.
                complete_url = GetRedirectUrl(entrytype, CO2Landingtype) + "?x=" + Server.UrlEncode(DateTime.UtcNow.ToLongTimeString()) + "&SID=" + TDSessionManager.Current.Session.SessionID + "&IsFNLanding=true";
            }
            #endregion

            #region Call method to set up planner specific parameters

            SetCommonParameters();

            // determine what type of search is required and Set Parameters accordingly
            switch (CO2Landingtype)
            {
                case "co":
                    SetCO2Parameters();
                    break;
                default:
                    validParameters = false;

                    // Log an error and do nothing, it'll redirect to the default page set in GetRedirectUrl()
                    string message = "CO2LandingPage request was abandoned due to one or more invalid parameters in requeststring: "
                        + HttpUtility.UrlDecode(Page.Request.RawUrl);
                    LogError(message, partnerId);

                    break;
            }
            #endregion

            if (!validParameters)
                // Reset the autoplan to prevent planner from submitting invalid request
                TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan] = false;

            Response.Redirect(complete_url);
        }
        #endregion

        #region CO2 set up methods
        /// <summary>
        /// Method to set parameters required on the Find A Car Park input page
        /// </summary>
        private void SetCO2Parameters()
        {
            // Get the parmeters we need from the query string
            distance = GetValidSingleParam("di");
            units = GetValidSingleParam("un");
            landingModes = GetValidSingleParam("lm");



            // Check if there are any encrypted parameters and extract.
            // if there are, then expecting distance and units to be encrypted
            string encryptedParam = GetValidSingleParam("enc");

            if (encryptedParam.Length > 0)
            {
                string[] decodedInput = DecryptedData(encryptedParam);
                GetEncryptedCO2Parameters(decodedInput);
            }


            // Clear and intialise session ready for CO2 page landing

            TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

            JourneyEmissionsPageState pageState = new JourneyEmissionsPageState();


            pageState.JourneyDistanceToDisplay = distance;
            pageState.JourneyDistanceUnit = 1;
            if (units == "km") pageState.JourneyDistanceUnit = 2;


            pageState.LandingModeAll = modeAll;
            pageState.LandingModeSmallCar = modeSmallCar;
            pageState.LandingModeLargeCar = modeLargeCar;
            pageState.LandingModeCoach = modeCoach;
            pageState.LandingModeTrain = modeTrain;
            pageState.LandingModePlane = modePlane;


            if (!validParameters)
            {
                string message = "CO2LandingPage request was abandoned due to one or more invalid parameters in requeststring: "
                    + HttpUtility.UrlDecode(Page.Request.RawUrl);
                LogError(message, partnerId);
            }
            else
            {
                pageState.IsLandingPageActive = true;
                TDSessionManager.Current.JourneyEmissionsPageState = pageState;
            }

            //MIS logging				
            LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.CO2LandingPage, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(lpee);
        }

        #endregion

        #region Private methods

        #region Private methods - Get redirect, Set common

        /// <summary>
        /// Uses the CO2Landingtype parameter to determine which page to redirect to.
        /// </summary>
        /// <param name="entrytype">Entry Type</param>
        /// <returns>Full URL for redirection</returns>
        private string GetRedirectUrl(string entrytype, string CO2Landingtype)
        {
            string pageURL = String.Empty;
            //create a page controller object in order to get the page transfer details for the 
            //required mode
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            PageTransferDetails pageTransferDetails;

            // Check if an entry type is provided, that its correct. 
            // Not providing one is ok, as we can assume requester wanted the findnearest as they used this landingpage url
            if ((entrytype == string.Empty) || (entrytype == "co"))
            {
                switch (CO2Landingtype)
                {
                    case "co":
                        pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyEmissionsCompare);
                        break;
                    default: // send to default page
                        pageTransferDetails = pageController.GetPageTransferDetails(defaultPage);
                        validParameters = false;
                        break;
                }
            }
            else
            {	// Incorrct entry type, so redirect to default page
                pageTransferDetails = pageController.GetPageTransferDetails(defaultPage);

                string message = "CO2 LandingPage request was made with an incorrect parameter entrytype: " + entrytype
                    + " . Expected entry type 'co' ";
                LogError(message, partnerId);

                validParameters = false;
            }

            pageURL = pageTransferDetails.PageUrl;

            return getBaseChannelURL(TDPage.SessionChannelName) + pageURL;
        }

        /// <summary>
        /// Obtains partner id from request. If parameters are encrypted then performs decryption.
        /// </summary>
        private void SetCommonParameters()
        {
            string encryptedParam = GetValidSingleParam("enc");

            if (encryptedParam.Length > 0)
            {
                string[] decodedInput = DecryptedData(encryptedParam);
                //Get and verify the encrypted partner id
                partnerId = CheckPartnerId(decodedInput);
            }
            else
            {
                //Get and verify the unencrypted partner id
                partnerId = CheckPartnerId(GetValidSingleParam("id"));
            }
        }
        #endregion


        #region Private methods - query string parameters
        /// <summary>
        /// Return parameters from querystring arguments.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns>String lookup of the parameter</returns>
        private string GetValidSingleParam(string paramName)
        {
            string tempCache = string.Empty;

            switch (paramName)
            {
                //Protect against XSS attacks (cross site scripting) by HTML Encoding the text.
                // Some of these parameter strings are displayed as is on portal, so must be made safe.				
                case "id":
                case "et":
                case "lt":
                case "un":
                    tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
                    break;
                case "enc":
                    tempCache = Page.Request.Params.Get(paramName);
                    break;
                case "di":
                    try
                    {
                        decimal value = Convert.ToDecimal(HttpUtility.UrlDecode(Page.Request.Params.Get(paramName)));
                        tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
                    }
                    catch (FormatException)
                    {
                        validParameters = false;

                        return string.Empty;
                    }
                    break;
                case "lm":
                    tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
                    // break out excluded modes from combined string passed to landing bage
                    if (tempCache.IndexOf("s") >= 0) modeSmallCar = false;
                    if (tempCache.IndexOf("l") >= 0) modeLargeCar = false;
                    if (tempCache.IndexOf("b") >= 0) modeCoach = false;
                    if (tempCache.IndexOf("r") >= 0) modeTrain = false;
                    if (tempCache.IndexOf("p") >= 0) modePlane = false;
                    if (modeSmallCar && modeLargeCar && modeCoach && modeTrain && modePlane) modeAll = true;

                    break;
                default:
                    tempCache = string.Empty;
                    break;
            }

            if (tempCache != null)
            {
                return Server.HtmlEncode(tempCache);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Find Boolean result according to input recieved from the query sting.
        /// Provides the default boolean value if the value is not specified. 
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns>Boolean lookup of the parameter</returns>
        private bool GetBooleanSingleParam(string paramName)
        {
            switch (paramName)
            {
                //auto plan flag
                case "p":
                    if (Page.Request.Params.Get(paramName) == "yes")
                        return true;
                    else
                        return false;



                default:
                    return false;
            }
        }

        #endregion

        #region Private methods - encryption/decription
        /// <summary>
        /// Decrypts encrypted data and parses into a string array
        /// </summary>
        /// <param name="encrypteddata">Encrypted data</param>
        /// <returns>String array of decrypted data</returns>
        private string[] DecryptedData(string encrypteddata)
        {
            string[] resultsCache;
            try
            {
                ITDCrypt decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ServiceDiscoveryKey.Crypto];
                string decryptedText = decryptionEngine.AsymmetricDecrypt(encrypteddata);
                resultsCache = decryptedText.Split(SECTION_SEP);
            }
            catch (System.ArgumentNullException)
            {
                //enc argument has not been found
                resultsCache = new string[0];
            }

            return resultsCache;
        }

        private void GetEncryptedCO2Parameters(string[] decodedInput)
        {
            // Ensure the size of the decoded input
            string[] distance_section = decodedInput[DISTANCE_SECTION].Split(SUBSECTION_SEP);

            // Verify/fail if distance_section length != 2 and that part distance_section[ID_KEY] == "di"
            if ((distance_section.Length != 2) || (distance_section[DISTANCE_KEY].CompareTo("di") != 0))
            {
                string message = "CO2LandingPage encrypted parameter Distance provided does not conform to the di=[Distance] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                distance = distance_section[DISTANCE_VAL];
            }

            string[] units_section = decodedInput[UNITS_SECTION].Split(SUBSECTION_SEP);

            if ((units_section.Length != 2) || (units_section[UNITS_KEY].CompareTo("un") != 0))
            {
                string message = "CO2 encrypted parameter units provided does not conform to the un=[Units] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                units = units_section[UNITS_VAL];
            }

            string[] mode_section = decodedInput[MODE_SECTION].Split(SUBSECTION_SEP);

            if ((mode_section.Length != 2) || (mode_section[MODE_KEY].CompareTo("lm") != 0))
            {
                string message = "CO2 encrypted parameter units provided does not conform to the lm=[landingMode] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                landingModes = mode_section[MODE_VAL];
            }

        }

        #endregion

        #region Private methods - Partner id
        /// <summary>
        /// Check that the partner Id is valid
        /// </summary>
        /// <param name="partnerId">string representation of Partner ID</param>
        /// <returns>string Partner Id</returns>
        private string CheckPartnerId(string partnerId)
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                TDTraceLevel.Verbose, "RawUrl (decoded) = " + HttpUtility.UrlDecode(Page.Request.RawUrl)));

            if (Page.Request.RequestType == HTTP_REQUEST_TYPE_GET)
            {
                string urlText = HttpUtility.UrlDecode(Page.Request.RawUrl);

                int startIndex = urlText.IndexOf(HTTP_REQUEST_ID_STRING1);

                if (startIndex > 0)
                {
                    startIndex += HTTP_REQUEST_ID_STRING1.Length;
                }
                else
                {
                    startIndex = urlText.IndexOf(HTTP_REQUEST_ID_STRING2);

                    if (startIndex > 0)
                    {
                        startIndex += HTTP_REQUEST_ID_STRING2.Length;
                    }
                }

                if (startIndex > 0)
                {
                    int endIndex = urlText.IndexOf(AMPERSAND, startIndex);

                    if (endIndex > 0)
                    {
                        partnerId = urlText.Substring(startIndex, endIndex - startIndex);
                    }
                    else
                    {
                        partnerId = urlText.Substring(startIndex);
                    }
                }
            }

            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                TDTraceLevel.Verbose, "PartnerId = " + partnerId));

            // replace invalid characters with empty strings, and limit to ten characters.
            string safePartnerId = System.Text.RegularExpressions.Regex.Replace(partnerId, @"[^\w]", "");

            // before truncating to ten chars, strip off any leading "http" or "https" or "www",
            //  since they waste valuable characters and don't add to identifying the client ...

            if (safePartnerId.StartsWith(PREFIX_HTTPS))
            {
                safePartnerId = safePartnerId.Substring(PREFIX_HTTPS.Length);
            }
            else if (safePartnerId.StartsWith(PREFIX_HTTP))
            {
                safePartnerId = safePartnerId.Substring(PREFIX_HTTP.Length);
            }

            if (safePartnerId.StartsWith(PREFIX_WWW))
            {
                safePartnerId = safePartnerId.Substring(PREFIX_WWW.Length);
            }

            if (safePartnerId.Length > SAFE_PARTNERID_LENGTH)
            {
                safePartnerId = safePartnerId.Substring(0, SAFE_PARTNERID_LENGTH);
            }

            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                TDTraceLevel.Verbose, "safePartnerId = " + safePartnerId));

            return safePartnerId;
        }

        /// <summary>
        /// Check that the encrypted partner Id is valid
        /// </summary>
        /// <param name="decodedInput">Decoded input data</param>
        /// <returns>string Partner Id</returns>
        private string CheckPartnerId(string[] decodedInput)
        {
            // Probably ensure the size of the decoded input
            string[] vendor_section = decodedInput[ID_SECTION].Split(SUBSECTION_SEP);
            // Verify/fail if vendor_section length != 2 and that part vendor_section[ID_KEY] == "id"
            // Does not conform to 
            // id=[vendorid]
            if ((vendor_section.Length != 2) || (vendor_section[ID_KEY].CompareTo("id") != 0))
            {
                string message = "Partner section provided does not conform to the id=[partnerID] " + "format.";

                LogError(message, "[partnerID]");

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }

            return CheckPartnerId(vendor_section[ID_VAL]);

        }
        #endregion

        #endregion

        #region Error logging
        /// <summary>
        /// Write operational events to the log, logging ip address and Partner ID
        /// </summary>
        /// <param name="description">The description of the error</param>
        /// <param name="partnerId">The error id</param>
        private void LogError(string description, string partnerId)
        {
            StringBuilder message = new StringBuilder(string.Empty);
            message.Append(description);
            message.Append(" Partner ID : ");
            message.Append(partnerId);
            message.Append(" client-ip : ");
            message.Append(Page.Request.UserHostAddress.ToString(CultureInfo.InvariantCulture));
            message.Append(" url-referrer : ");
            message.Append(Page.Request.UrlReferrer);

            OperationalEvent oe = new OperationalEvent(
                TDEventCategory.Business, TDTraceLevel.Error, message.ToString());

            Logger.Write(oe);
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}
