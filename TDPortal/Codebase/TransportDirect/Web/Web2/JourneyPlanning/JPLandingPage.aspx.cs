// *********************************************** 
// NAME                 : JourneyPlannerInput.aspx
// AUTHOR               : Hassan AL KATIB
// DATE CREATED         : 13/09/2005 
// DESCRIPTION  : Journey Planner Input page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JPLandingPage.aspx.cs-arc  $ 
//
//   Rev 1.14   Mar 11 2013 10:46:06   mmodi
//Removed TODO comment
//
//   Rev 1.13   Feb 18 2013 10:05:52   RBroddle
//Updated to add default accessibility parameters
//Resolution for 5890: DEL12 - Page landing is failing for accessible and non-accessible journeys on SITEST/ BBP
//
//   Rev 1.12   Jan 20 2013 16:27:30   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.11   Jan 15 2013 15:42:12   mmodi
//Page landing updates for accessible locations
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.10   Dec 10 2012 15:46:50   mmodi
//Added accessible options landing parameters
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.9   Sep 04 2012 11:17:04   mmodi
//Updated to handle landing page auto plan with the new auto-suggest location control
//Resolution for 5837: Gaz - Page landing autoplan links fail on Cycle input page
//
//   Rev 1.8   Oct 07 2010 11:20:52   apatel
//Updated to resolve the issue with bug in businesslink and gadget javascript for date time dropdown
//Resolution for 5618: Issue with google gadget and bussiness link template with month having extra 0 at bigining
//
//   Rev 1.7   Oct 07 2010 10:52:56   apatel
//Updated to resolve the issue with bug in javascript for datetime
//
//   Rev 1.6   Sep 17 2010 12:54:40   RBroddle
//Updated to decode HTML encoded origin/location locations to avoid problems with display on journey results / maps etc.
//Resolution for 5607: Problem with HTML Encoded origin/destination from page landing
//
//   Rev 1.5   Jun 14 2010 15:07:46   pghumra
//Fixed FindACycle page landing functionality so that it now correctly reports mode as Cycle as opposed to Coach.
//Resolution for 5553: CODEFIX - REQUIRED - 10.12 - Betterbybike.info page landing to CTP is being reported as Find A Coach
//
//   Rev 1.4   Mar 10 2010 16:13:08   MTurner
//Changed so that name is prepopulated if no name is supplied and location type is NaPTAN.
//Resolution for 5447: Page Landing location text should be auto populated
//
//   Rev 1.3   Oct 01 2009 10:54:24   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.2   Mar 31 2008 13:25:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:16   mturner
//Initial revision.
//
//   Rev 1.57   Jun 04 2007 10:32:28   mmodi
//Only allow CRS code when Multi, Train, and Car planners
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.56   May 30 2007 13:59:38   mmodi
//Amended CRS code resolution to determine text description for Naptan using Naptancacheentry
//Resolution for 4434: Landing page: CRS code to use NaPTAN common name
//
//   Rev 1.55   May 29 2007 15:08:46   mmodi
//Corrected issue when invalid NaPTAN supplied and to prevent CRS code being accepted for Find a Flight mode
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.54   May 29 2007 12:49:02   mmodi
//Updates to only accept 1 CRS code, and updated CRS error logging
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//Resolution for 4430: Landing page: Only allow one CRS code
//
//   Rev 1.53   May 24 2007 15:28:34   mmodi
//Updated to test for CRS code, and to redirect to the SorryPage
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.52   May 23 2007 16:57:52   mmodi
//Added code to accept a CRS code
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.51   Feb 28 2007 14:57:38   jfrank
//CCN0366 - Enhancement to enable Page Landing from Word 2003 and to ignore session timeouts for Page Landing due to future usage by National Trust.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.50   Jun 21 2006 17:55:58   rphilpott
//Fix stupid bug if partner id starts with https
//Resolution for 4121: Landing Page: Truncation of Partner ID incorrect when HTTPS used
//
//   Rev 1.49   Jun 07 2006 18:12:04   rphilpott
//Prevent unnecessary truncation of partner-ids on GET requests.
//Resolution for 4100: Landing Page: Truncation of Partner-IDs
//
//   Rev 1.48   Apr 21 2006 10:06:40   COwczarek
//The SetFindACarParameters method now sets the SearchType property on both origin and destination search objects to SearchType.City.
//DataServiceType.FindCarLocationDrop now passed to TriStateLocatioControl object in SetFindACarParameters.
//Moved call to SetCommonParameters in SetFindACarParameters to ensure session initalised correctly before any response.redirect occurs.
//Resolution for 3916: DN077 Landing Page: Find a Car default gazetteer
//
//   Rev 1.47   Apr 20 2006 13:05:12   COwczarek
//- Ensure landing page check flag is always set regardless if error detected in request (SCR#3910)
//- Ensure journey parameters correctly instantiated and intialised regardless if error detected in request (SCR#3910)
//- Do not overwrite find a flight page state data after calling SetFindALocationParameters (SCR#3908 & SCR#3914)
//Resolution for 3908: DN077 Landing Page Phase 3 - Find a Flight quickplanner does not display airport drop-down list
//Resolution for 3910: DN077 Landing Page Phase 3 - Find a Flight quickplanner server error
//Resolution for 3914: DN077 Landing Page Phase 3 - Find a Flight quickplanner doesnot display airport drop-down list
//
//   Rev 1.46   Apr 13 2006 17:01:32   COwczarek
//Initialise PublicViaType and PrivateViaType journey parameters to ControlType.Default.
//Resolution for 3787: Landing Page: Advanced options Via location is shown with Red border in ambiguous state
//
//   Rev 1.45   Apr 10 2006 12:00:14   jbroome
//Fix for IR 3797
//Resolution for 3797: Landing Page: Find a Flight - input page is resolved on entry preventing dates from being changed
//
//   Rev 1.44   Apr 07 2006 15:34:24   jbroome
//Fixes for various itegration IR issues
//Resolution for 3775: Landing Page: Find a train planner shown in default state when Postcode entered
//Resolution for 3776: Landing Page: Comma separated list of Naptans with Spaces
//Resolution for 3778: Landing Page: Time value ignored when Autoplan is True
//Resolution for 3794: Landing Page: No error shown when Return time is before Outward time on same day
//Resolution for 3800: Landing Page: Find a Flight - time value specified is not used
//Resolution for 3812: Landing Page: Naptan in lower case is not processed
//Resolution for 3821: DN077 Landing Page: Server error when Naptan supplied for coordinate search
//
//   Rev 1.43   Apr 06 2006 10:42:28   jbroome
//updated UpdateJourneyParametesModes() method
//Resolution for 3811: Landing Page: Server error when all Transport modes and car excluded
//
//   Rev 1.42   Mar 30 2006 10:45:12   halkatib
//The error handling in the populatenaptans method is not required. The Landing page method calling already has the appropriate exception handling.There fore if a Naptan is not found throw an exception and let the calling method handle it. 
//The landingpagecheck session parameter setter has been moved to before the journey parameter setter methods. This is because it is required by the TDSessionManager that is called. 
//
//   Rev 1.41   Mar 24 2006 17:03:34   halkatib
//Applied fixes to landing page functionalitt post phase 3 merge
//
//   Rev 1.40   Mar 22 2006 17:30:38   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.39   Mar 21 2006 17:31:44   jmcallister
//Amended to call updated Get method on NaptanLookup class. Also hardened to handle invalid naptans gracefully.
//
//   Rev 1.38   Mar 17 2006 16:01:12   tmollart
//Updated to use new PopulateToids method on location object.
//
//   Rev 1.37   Feb 24 2006 11:00:54   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.36   Feb 17 2006 15:40:34   aviitanen
//Merge from Del8.0 to 8.1
//
//   Rev 1.35   Feb 10 2006 18:00:24   kjosling
//Fixed
//
//   Rev 1.34   Feb 10 2006 12:24:58   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.33   Jan 26 2006 10:04:08   jbroome
//Fixed bug
//
//   Rev 1.32   Jan 25 2006 15:26:20   halkatib
//Updated Page Landing date handling
//Resolution for 3480: Landing Page: Entering past dates gives different results depending on the time value.
//Resolution for 3497: Landing page: anomoly in default times when no time value or invalid time value is used
//Resolution for 3499: Landing Page:  Allows user to search with Street and City name, and resolves the location even though it is ambiguous
//
//   Rev 1.31   Jan 24 2006 16:09:36   jmcallister
//Ensure that location fixed flag is true for all OSGR and Naptan cases.
//Resolution for 3441: Del8.1 stream - Removing Partner id validation for Landing Page
//Resolution for 3494: Client Links: Locations always displayed as type 'Station/Airport' when journey is planned using bookmark
//
//   Rev 1.30   Jan 19 2006 14:46:46   jbroome
//Added missing brackets
//
//   Rev 1.29   Jan 19 2006 12:52:58   jbroome
//Fixed bug in population of destination location only
//
//   Rev 1.28   Jan 16 2006 13:44:28   jmcallister
//IR3449 - if outward and return dates are not specified, they will be defaulted to current date plus an offset to avoid portal validation errors. (Plus five minutes for outward time, and plus fifteen minutes for return time - though this will be rounded to nearest five minute block by portal processing)
//Resolution for 3441: Del8.1 stream - Removing Partner id validation for Landing Page
//Resolution for 3449: Page Landing: Request with a date but no time value passed to ambiguity page
//
//   Rev 1.27   Jan 11 2006 11:22:18   jmcallister
//IR3441 - Remove validation of Partner IDs
//Resolution for 3441: Del8.1 stream - Removing Partner id validation for Landing Page
//
//   Rev 1.26   Jan 10 2006 14:16:50   tolomolaiye
//Code review comments
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.25   Jan 09 2006 14:46:10   rgreenwood
//IR3419 Added PadDate() method and called from GetValidMultiParam() method to correctly format dates when converted from DateTime object to strings
//Resolution for 3419: Page Landing: Future date not accepted
//
//   Rev 1.24   Jan 05 2006 17:46:04   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.23   Dec 13 2005 10:41:34   jmcallister
//IR3360. Landing page appends fake querystring argument to redirect url. This prevents IE from using cached page when inputting a changed Landing Page URL in the same browser instance. Latest results are now always displayed without need to press view-refresh in IE.
//Resolution for 3360: Del 8.0 Page Landing - Ability to update URL on the same page
//
//   Rev 1.22   Dec 08 2005 12:47:28   jmcallister
//FXCop and code review comments resulting from IR3320
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.21   Dec 07 2005 09:51:26   jmcallister
//Code review comments.
//
//   Rev 1.20   Dec 06 2005 11:15:50   asinclair
//Manual (early) merge of Client and Business Links code.  This code will not affect existing functionality.  The remaining CL and BL code will be merged at a later date.
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.19   Dec 01 2005 14:27:14   jmcallister
//Secured Landing Pages against XSS attacks by HTML encoding text params. Also altered processing so that queries with unencrypted parameters will be allowed as long as a valid partner id is supplied.
//Resolution for 3255: Landing Page and x-site scripting exploit
//
//   Rev 1.18.5.1   Jan 11 2006 14:28:26   tmollart
//Updated with comments from code review. Removed redudant code.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.18.5.0   Dec 12 2005 17:01:38   tmollart
//Modified to use new initialise parameters method on session manager.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.18   Nov 02 2005 18:32:06   kjosling
//Automatically merged from branch for stream2877
//
//   Rev 1.17.1.1   Oct 28 2005 16:23:52   halkatib
//Changes made for landing page phase 2
//Resolution for 2877: Del 8  Landing Page Phase2
//
//   Rev 1.17.1.0   Oct 21 2005 16:45:48   halkatib
//Changes applied for Landing Page phase 2
//Resolution for 2877: Del 8  Landing Page Phase2
//
//   Rev 1.17   Oct 12 2005 09:50:06   halkatib
//Fixed IR 2858: Added extra check for string length == 0 for the origin and destination. 
//
//   Rev 1.16   Oct 11 2005 16:40:36   halkatib
//Fixed IR 2855 : Removed code that handles new sessions. This code has been relocated to the TDPage.cs file, which the Landing Pge inherits from
//
//   Rev 1.15   Oct 05 2005 13:52:30   halkatib
//Fixed IR2820. Changed error handling of Date and Time objects passed to the Landing Page as per Judith's request
//
//   Rev 1.14   Oct 05 2005 10:15:38   halkatib
//Fixed IR2824. Added extra else statements to deal with null origin or destination data in the ecrypted parameter. 
//
//   Rev 1.13   Oct 04 2005 15:57:36   halkatib
//Fixed IR2825. Assigned the result of the strBoolTrueDefault.ToLower() to strBoolTrueDefault
//
//   Rev 1.12   Sep 30 2005 11:39:08   halkatib
//Correct bug in the post method
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.11   Sep 30 2005 10:22:22   halkatib
//Fixed bug for Car default and return default
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.10   Sep 29 2005 19:18:30   jbroome
//Fixed bug with bool parsing
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.9   Sep 29 2005 12:19:26   halkatib
//Removed system.exception on page since this does not allow incorrect postcode data to pass through to ambiguity. 
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.8   Sep 28 2005 17:33:08   halkatib
//Code refactoring completed by MTillet. 
//Fxcop has been run on this code
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.7   Sep 27 2005 11:17:38   MTillett
//Part complete code refactoring done by Hasan
//
//   Rev 1.6   Sep 22 2005 17:23:10   halkatib
//Removed encryption method from page, and reworded an error message
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.5   Sep 21 2005 17:22:36   halkatib
//Removed capitalisation of input parameters. Corrected the method for reading http POST methods. 
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.4   Sep 21 2005 10:30:52   halkatib
//Added Error checking for mandatory fields
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.3   Sep 19 2005 16:40:44   halkatib
//Removed test logging that was not required.
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.2   Sep 19 2005 12:00:38   halkatib
//Added code to allow entry from cold to work
//Resolution for 2610: DEL 8 Stream: Landing page

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.UserSupport;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.ScreenFlow;


using Logger = System.Diagnostics.Trace;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for JPLandingPage.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JPLandingPage : TDPage
	{

		protected TransportDirect.UserPortal.Web.Controls.BiStateLocationControl originSelect;
		protected TransportDirect.UserPortal.Web.Controls.BiStateLocationControl destinationSelect;
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl findAOriginLocationsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl findADestinationLocationsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindCoachTrainPreferencesControl findACoachTrainPreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl findADateControl;
		protected TransportDirect.UserPortal.Web.Controls.FindCarPreferencesControl findACarPreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.FindCarJourneyOptionsControl findACarJourneyOptionsControl;

		private TDJourneyParametersMulti journeyParameters;
		private TDJourneyParametersFlight journeyParametersFlight;
		//page state for find a flight
		private FindFlightPageState findFlightPageState;
		private FindCoachTrainPageState findCoachTrainPageState;
        private FindCyclePageState findCyclePageState;
        private InputPageState inputPageState;

		// Declaration of search/location object members
		private LocationSearch originSearch;
		private LocationSearch destinationSearch;
		private TDLocation destinationLocation;
		private TDLocation originLocation;

		// Declaration of FindA search/location object members
		private LocationSelectControlType originLocationSelectControlType;
		private LocationSelectControlType destinationLocationSelectControlType;	
			
		#region Search parameters
		/// <summary>
		/// Partner Id (shortcut "id")
		/// </summary>
		private string partnerId = String.Empty;
		/// <summary>
		/// Origin type (shortcut "oo")
		/// </summary>
		private	string origintype = String.Empty; 
		/// <summary>
		/// Origin data (shortcut "o")
		/// </summary>
		private	string origindata; 
		/// <summary>
		/// Origin text (shortcut "on")
		/// </summary>
		private	string origintext = String.Empty; 
		/// <summary>
		/// Destination type (shortcut "do")
		/// </summary>
		private string destinationtype = String.Empty;
		/// <summary>
		/// Destination data (shortcut "d")
		/// </summary>
		private string destinationdata;
		/// <summary>
		/// Destination text (shortcut "dn")
		/// </summary>
		private string destinationtext = String.Empty;
		/// <summary>
		/// Mode (shortcut "m")
		/// (m=multimode, r=road, t=train, c=coach, a=air)
		/// </summary>
		private string mode = String.Empty;
		/// <summary>
		/// Auto plan flag (shortcut "p")
		/// </summary>
		private bool autoplanflag = false;
		#endregion
	
		//Data services object
		private IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];				
		//air data provider for use of Airport naptans
		private	IAirDataProvider airData = (AirDataProvider.IAirDataProvider) TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
	
		#region Global definitions
		private static string IATA_PREFIX = Properties.Current["FindA.NaptanPrefix.Airport"];
		private static readonly char[] DELIMITER = new char[] {','};
		private static readonly char[] SECTION_SEP = new char[] { '&' };
		private static readonly char[] SUBSECTION_SEP = new char[] { ':' };
		private const int ID_SECTION = 0;
		private const int PLACE_SECTION = 1;

		private const int ID_KEY = 0;
		private const int ID_VAL = 1;

		private const int PLACE_KEY = 0; // will be O or D
		private const int PLACE_VAL = 1; // Will contain the origin/destination as sent by vendor

		private const string HTTP_REQUEST_TYPE_GET  = "GET";
		private const string HTTP_REQUEST_ID_STRING1 = "?id=";
		private const string HTTP_REQUEST_ID_STRING2 = "&id=";
		private const char AMPERSAND = '&';

		private const string PREFIX_HTTP  = "http";
		private const string PREFIX_HTTPS = "https";
		private const string PREFIX_WWW	  = "www";

		private const int SAFE_PARTNERID_LENGTH = 10;

		private const string AUTOPLANOFF = "off";

		//use flags to detect if origin or destination is being processed
		private const string ORIGIN_FLAG = "origin";
		private const string DEST_FLAG = "destination";
	
		//origin destination flag
		private string odFlag = string.Empty;

        //final url that will do the redirect
		private string complete_url = String.Empty;				

		// Switch to use CRS code
		private bool useCRS = bool.Parse(Properties.Current["LandingPage.UseCRSCode"]);
		#endregion

		/// <summary>
		/// Page constructor
		/// </summary>
		public JPLandingPage() : base()
		{
			pageId = PageId.JPLandingPage;
		}
		/// <summary>
		/// Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Determine if CRS code was supplied. This is used to redirect user to alternative page if the 
			// useCRS code has been switched off. If this is the case, we don't want to create workload in setting up a session
			origintype = GetValidSingleParam("oo");
			destinationtype = GetValidSingleParam("do");

			if (!useCRS && ((origintype.Equals("c")) || (destinationtype.Equals("c"))))
			{
				complete_url = GetRedirectUrl("sorrypage");
			}
			else
			{
				//need to set the landingpage switch to true to identify the request as a Landing page request
				//this needs to be done before the journey parameters are initialised since there are 
				//dependencies upon it
				TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ] = true;

				//create new session and itinerary managers
				ITDSessionManager sessionManager = TDSessionManager.Current;			
				TDItineraryManager itineraryManager = TDItineraryManager.Current;
			
				mode = GetValidSingleParam("m");
				if (TDPage.SessionChannelName !=  null )
				{
					//Add system time to end of redirect URL (fake querystring param). This ensures IE always requests latest version of page rather than relying on cache. IR3360.
					complete_url = GetRedirectUrl(mode) + "?x=" + Server.UrlEncode(DateTime.UtcNow.ToLongTimeString()) + "&SID=" + TDSessionManager.Current.Session.SessionID + "&IsJPLanding=true";			
				}
						
				// Reset the itinerary manager
				itineraryManager.NewSearch();

                //set page result as invalid if previous results have been planned
				if (sessionManager.JourneyResult != null) 
				{ 
					sessionManager.JourneyResult.IsValid = false;
				}

                if (sessionManager.CycleResult != null)
                {
                    sessionManager.CycleResult.IsValid = false;
                }
                
				autoplanflag = GetBooleanSingleParam("p");
			
				//deternime what type of search is required and Set Parameters accordingly
				switch (mode)
				{	
					case "r":
						SetFindACarParameters();					
						break;
					case "t":
						SetFindATrainParameters();					
						break;
					case "c":
						SetFindACoachParameters();
						break;
					case "a":
						SetFindAFlightParameters();					
						break;
                    case "b":
                        SetFindACycleParameters();
                        break;
					default:
						SetMultiModalParameters();					
						break;
				}			

				#region set parameters required in session			
				//place the autoplan switch into the session object
				TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ] = autoplanflag;
				//set the origin and destination types in the session object
				TDSessionManager.Current.Session[ SessionKey.LandingPageOriginInputType ] = origintype;
				TDSessionManager.Current.Session[ SessionKey.LandingPageDestinationInputType ] = destinationtype;	
				//check that both origin and destination have been provided.
				if ((origindata != null && origindata.Length !=0) && (destinationdata != null && destinationdata.Length != 0))
				{
					TDSessionManager.Current.Session[ SessionKey.LandingPageBothDataNotNull ] = true;
				}	
				#endregion

			}

			Response.Redirect(complete_url);

		}		
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
				ITDCrypt decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ ServiceDiscoveryKey.Crypto ];
				string decryptedText = decryptionEngine.AsymmetricDecrypt(encrypteddata);
				resultsCache = decryptedText.Split( SECTION_SEP );	
			}
			catch (System.ArgumentNullException)
			{
				//enc argument has not been found
				resultsCache = new string[0];
			}
			
			return resultsCache;
		}	
		
		/// <summary>
		/// Check that the partner Id is valid
		/// </summary>
		/// <param name="partnerId">string representation of Partner ID</param>
		/// <returns>string Partner Id</returns>
		private string CheckPartnerId(string partnerId)
		{
			//******************************************************************************
			//THIS SECTION COMMENTED OUT FOR IR XXX. SUGGEST IT IS LEFT IN THE CODE BASE UNTIL 30/06/2006
			//			//verify if Partner Id is true.			
			//			ArrayList PidList = ds.GetList(DataServiceType.White LabelPartnerId);
			//			if(!PidList.Contains(partnerId))
			//			{
			//				LogError("Partner ID provided is not a recognised partner id. ", partnerId);	
			//
			//				//Redirect to Blank input page
			//				Response.Redirect(complete_url);
			//			}
			//******************************************************************************

			Logger.Write(new OperationalEvent(TDEventCategory.Business,
				TDTraceLevel.Verbose, "RawUrl (decoded) = " + HttpUtility.UrlDecode(Page.Request.RawUrl)));

			if	(Page.Request.RequestType == HTTP_REQUEST_TYPE_GET)
			{
				string urlText = HttpUtility.UrlDecode(Page.Request.RawUrl);

				int startIndex	= urlText.IndexOf(HTTP_REQUEST_ID_STRING1); 

				if	(startIndex > 0) 
				{
					startIndex += HTTP_REQUEST_ID_STRING1.Length;
				}
				else 
				{
					startIndex	= urlText.IndexOf(HTTP_REQUEST_ID_STRING2);

					if	(startIndex > 0) 
					{
						startIndex += HTTP_REQUEST_ID_STRING2.Length;
					}
				}

				if	(startIndex > 0)
				{
					int endIndex = urlText.IndexOf(AMPERSAND, startIndex);
				
					if	(endIndex > 0)
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

			if	(safePartnerId.StartsWith(PREFIX_HTTPS))
			{
				safePartnerId = safePartnerId.Substring(PREFIX_HTTPS.Length);
			}
			else if	(safePartnerId.StartsWith(PREFIX_HTTP))
			{
				safePartnerId = safePartnerId.Substring(PREFIX_HTTP.Length);
			}

			if	(safePartnerId.StartsWith(PREFIX_WWW))
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
			string[] vendor_section = decodedInput[ID_SECTION].Split( SUBSECTION_SEP );
			// Verify/fail if vendor_section length != 2 and that part vendor_section[ID_KEY] == "id"
			// Does not conform to 
			// id=[vendorid]
			if ((vendor_section.Length != 2) || (vendor_section[ID_KEY].CompareTo("id") != 0))
			{
				OperationalEvent oe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"Partner section provided does not conform to the id=[partnerID] "+
					"format. client-ip : " + Page.Request.UserHostAddress 
					+ " url-referrer : " + Page.Request.UrlReferrer);
				Logger.Write(oe);
				//Redirect to Blank input page
				Response.Redirect(complete_url);
			}

			return CheckPartnerId(vendor_section[ID_VAL]);
			
		}
		/// <summary>
		/// Check that the place section is valid
		/// </summary>
		/// <param name="decodedInput">Decoded input data</param>
		/// <returns>String array with o=[place] or d=[place]</returns>
		private string[] CheckPlaceSection(string[] decodedInput)
		{
			string[] place_section = decodedInput[PLACE_SECTION].Split( SUBSECTION_SEP );
			if( place_section.Length != 2 )
			{
				// BUG BUG!
				// Throw exception -> the place does not conform to 
				// o=[place] or d=[place]
				LogError("Origin/Destination section provided does not conform to the o=[place] format.", partnerId);

				//Redirect to Blank input page
				Response.Redirect(complete_url);
			}
			return place_section;
		}

		/// <summary>
		/// Write operational events to the log, logging ip address and Partner ID
		/// </summary>
		/// <param name="description">The description of the error</param>
		/// <param name="partnerId">The error id</param>
		private void LogError (string description, string partnerId)
		{
			StringBuilder message = new StringBuilder(string.Empty);
			message.Append(description);
			message.Append(" Partner ID : ");
			message.Append(partnerId);
			message.Append(" client-ip : ");
			message.Append(Page.Request.UserHostAddress.ToString(CultureInfo.InvariantCulture));
			message.Append(" url-referrer : ");
			message.Append(Page.Request.UrlReferrer);

			OperationalEvent oe = new  OperationalEvent(
				TDEventCategory.Business, TDTraceLevel.Error, message.ToString());

			Logger.Write(oe);
		}

		/// <summary>
		/// determine if origin or destination has been given and set the appropriate location
		/// </summary>
		/// <param name="place_type"></param>
		private void SetLocation(string place_type, string place_loc)
		{
			switch (place_type)
			{
				case "o" :
					origindata = place_loc;
					destinationdata = GetValidSingleParam("d");
					if (origindata == null || origindata.Length == 0)
					{
						//Origin is set to an empty string 
						//Log the error
						LogError("The origin data provided is an empty string", partnerId);

						//Redirect to Blank input page
						Response.Redirect(complete_url);
					}
					break;
				case "d":
					destinationdata = place_loc;
					origindata = GetValidSingleParam("o");
					if (destinationdata == null || destinationdata.Length == 0)
					{
						//Origin is set to an empty string 
						//Log the error
						LogError("The destination data provided is an empty string", partnerId);

						//Redirect to Blank input page
						Response.Redirect(complete_url);
					}
					break;
				case "" : //Compiler will not accept string.Empty for case condition.
					origindata = GetValidSingleParam("o");
					destinationdata = GetValidSingleParam("d");
					if (autoplanflag)
					{
						if ((destinationdata == null || destinationdata.Length ==0) && (origindata == null || origindata.Length ==0))
						{
							//Autoplan is set but both origin and destination are not set therefore 
							//redirect to blank input page
							//Log the error
							LogError("Auto Plan set but origin and destination are missing.", partnerId);

							//Redirect to Blank input page
							Response.Redirect(complete_url);					
						}
					}
					break;
				default:			
					// Neither d, o or string.Empty
					// CAUSE ERROR/EXCEPTION HERE -> Bad data in
					// This is the case when neither d or o was passed to us or bad format was used for the data type
					//Log the error
					LogError("Neither origin or destination included in encrypted parameter, or bad format used.", partnerId);

					//Redirect to Blank input page
					Response.Redirect(complete_url);
					break;
			}		
		}
		/// <summary>
		/// Populates the date parameters on the TDJourneyParameters object. 
		/// populates with defaults if the dates are not provided or incorrect.
		/// </summary>
		private void UpdateJourneyParametersDateTime(TDJourneyParameters parameters)
		{
			//outward depart/arrive
			parameters.OutwardArriveBefore = GetBooleanSingleParam("da");
			//outward date input
			//check if date provided. If not provided check for alternative date formats. 
			// If no date info is provided, do nothing as the date will be set to now automatically

			bool outwardDateProvided = false; // Local variable to control flow

			string[] strOutwardDate = GetValidMultiParam("dt");
			if (strOutwardDate.Length == 3)
			{				
				outwardDateProvided = true;
				if (!IsValidMonthYear(strOutwardDate[1], strOutwardDate[2]))
				{
					//invalid month year used. Reset the day and month parameters and set autoplan flag to false
					//to allow user to reenter the date
					parameters.OutwardDayOfMonth = PadDate(DateTime.Now.Day.ToString(CultureInfo.InvariantCulture));
					parameters.OutwardMonthYear = GetValidMonthYear(strOutwardDate[1], strOutwardDate[2]);
					//set autoplan off
					autoplanflag = false;

				}
				else
				{
					parameters.OutwardDayOfMonth = strOutwardDate[0];
					parameters.OutwardMonthYear = GetValidMonthYear(strOutwardDate[1], strOutwardDate[2]);
				}
			}
			else if ((strOutwardDate.Length == 1) && (strOutwardDate[0] == AUTOPLANOFF))
			{
				outwardDateProvided = true;
				//invalid day used. Day and month parameters remain unset and wil default in the portal code. 
				//Set autoplan flag to false to allow user to re-enter the date
				autoplanflag = false;
			}
			else
			{
				//Alternative date processing
				string[] strOutwardDateMonthYear = GetValidMultiParam("dtm");
				string strOutwardDateDay = GetValidSingleParam("dtd");

				if ((strOutwardDateMonthYear.Length == 2) && (strOutwardDateDay != null ))
				{					
					outwardDateProvided = true;
					if (!IsValidMonthYear(strOutwardDateMonthYear[0], strOutwardDateMonthYear[1]) || !IsValidDay(strOutwardDateDay))
					{
						//invalid month/year or day used. Reset the day and month/year parameters and set autoplan flag to false
						//to allow user to reenter the date
						parameters.OutwardDayOfMonth = PadDate(DateTime.Now.Day.ToString(CultureInfo.InvariantCulture));
						parameters.OutwardMonthYear = GetValidMonthYear(strOutwardDateMonthYear[0], strOutwardDateMonthYear[1]);
						//set autoplan off
						autoplanflag = false;
					}
					else
					{
						parameters.OutwardDayOfMonth = GetValidDay(strOutwardDateDay);
						parameters.OutwardMonthYear = GetValidMonthYear(strOutwardDateMonthYear[0], strOutwardDateMonthYear[1]);
					}
				}
				else if (strOutwardDateMonthYear.Length == 1 && strOutwardDateMonthYear[0] == AUTOPLANOFF)
				{
					outwardDateProvided = true;
					autoplanflag = false;
				}
			}

			if (outwardDateProvided)
			{
				bool validTime = false;
				//outward time input
				//(time will only be set if a valid date has been set)
				//Check if time provided. If not provided check for alternative time formats. 
				// If no time info is provided, do nothing as the date will be set to now automatically
				string[] strOutwardTime = GetValidMultiParam("t");
				if (strOutwardTime.Length == 2)
				{
					parameters.OutwardHour = strOutwardTime[0];
					parameters.OutwardMinute = strOutwardTime[1];
					validTime = true;
				}
				else
				{
					string strOutwardTimeHour = GetValidSingleParam("th");
					string strOutwardTimeMinute = GetValidSingleParam("tm");

					try
					{ 
						//verify that this is a time
						DateTime testTime = DateTime.ParseExact(strOutwardTimeHour+strOutwardTimeMinute, "HHmm", CultureInfo.InvariantCulture);
						if ((strOutwardTimeHour != null) && (strOutwardTimeMinute != null) 
							&& (strOutwardTimeHour != string.Empty ) && (strOutwardTimeMinute != string.Empty )
							&& (strOutwardTimeHour.Length == 2) && (strOutwardTimeMinute.Length == 2 ))
						{
							parameters.OutwardHour = strOutwardTimeHour;
							parameters.OutwardMinute = strOutwardTimeMinute;
							validTime = true;
						}
					}
					catch (FormatException)
					{
						validTime = false;

					}                    
				}
				// if date specified, but no time, then switch off auto plan
				if (!validTime)
				{
					autoplanflag = false;		
				}
				else
				{
					// If valid time has been set, set OutwardAnytime to false
					// as this is defaulted to true and used in "Find a"s
					parameters.OutwardAnyTime = false;
				}
			}

			parameters.IsReturnRequired = GetBooleanSingleParam("r");
			//only execute if a return is required
			bool returnDateProvided = false; // Local variable to control flow
			if (parameters.IsReturnRequired)
			{
				//return depart/arrive
				parameters.ReturnArriveBefore = GetBooleanSingleParam("rda");
				//return date input					
				string[] strReturnDate = GetValidMultiParam("rdt");
				if (strReturnDate.Length == 3)
				{					
					returnDateProvided = true;
					parameters.ReturnMonthYear = GetValidMonthYear(strReturnDate[1], strReturnDate[2]);
					if (!IsValidMonthYear(strReturnDate[1], strReturnDate[2]))
					{
                        //invalid month year used. Reset the day and month parameters and set autoplan flag to false
						//to allow user to reenter the date
                        parameters.ReturnDayOfMonth = string.Empty;
						parameters.ReturnMonthYear = "NoReturn";
						parameters.IsReturnRequired = false;
						autoplanflag = false;
					}
					else
					{
						parameters.ReturnDayOfMonth = GetValidDay(strReturnDate[0]);
					}
					
				}
				else if (strReturnDate.Length == 1 && strReturnDate[0] == AUTOPLANOFF)
				{
					returnDateProvided = true;
					//invalid day used. Day and month parameters remain unset and wil default in the portal code. 
					//Set autoplan flag to false to allow user to re-enter the date
					autoplanflag = false;
				}
				else
				{
					//Search for alternative params
					string[] strReturnDateMonthYear = GetValidMultiParam("rdtm");
					string strReturnDateDay = GetValidSingleParam("rdtd");
					if ((strReturnDateMonthYear.Length == 2)&&(strReturnDateDay != null))
					{						
						returnDateProvided = true;
						if (!IsValidMonthYear(strReturnDateMonthYear[0], strReturnDateMonthYear[1]) || !IsValidDay(strReturnDateDay))
						{
							//invalid month/year or day used. Reset the day and month/year parameters and set autoplan flag to false
							//to allow user to reenter the date
							parameters.ReturnDayOfMonth = string.Empty;
							parameters.ReturnMonthYear = "NoReturn";
							parameters.IsReturnRequired = false;
							//set autoplan off
							autoplanflag = false;
						}
						else
						{
							parameters.ReturnDayOfMonth = GetValidDay(strReturnDateDay);
							parameters.ReturnMonthYear = GetValidMonthYear(strReturnDateMonthYear[0], strReturnDateMonthYear[1]);
						}
						
					}
					else if (strReturnDateMonthYear.Length == 1 && strReturnDateMonthYear[0] == AUTOPLANOFF)
					{
						returnDateProvided = true;
						autoplanflag = false;
					}
				}

				if (returnDateProvided)
				{
					bool validTime = false;
					//Get TIME data
					//return time input					
					string[] strReturnTime = GetValidMultiParam("rt");
					if (strReturnTime.Length == 2)
					{
						parameters.ReturnHour = strReturnTime[0];
						parameters.ReturnMinute = strReturnTime[1];
						validTime = true;
					}
					else
					{
						//Search for alternative params
						string strReturnTimeHour = GetValidSingleParam("rth");
						string strReturnTimeMinute = GetValidSingleParam("rtm");

						try
						{
							//verify that this is a time
							DateTime testTime = DateTime.ParseExact(strReturnTimeHour+strReturnTimeMinute, "HHmm", CultureInfo.InvariantCulture);
							if ((strReturnTimeHour != null)&&(strReturnTimeMinute != null)
								&& (strReturnTimeHour != string.Empty )&&(strReturnTimeMinute != string.Empty )
								&& (strReturnTimeHour.Length == 2 )&&(strReturnTimeMinute.Length == 2 ) )
							{
								parameters.ReturnHour = strReturnTimeHour;
								parameters.ReturnMinute = strReturnTimeMinute;
								validTime = true;
							}
						}
						catch (FormatException)
						{
							validTime = false;
						}
					}
					// if date specified, but no time, then switch off auto plan
					if (!validTime)
					{
						autoplanflag = false;		
					}
				}
			}
		}		

		/// <summary>
		/// Removes the Car ModeType if not rquired by the Door to Door search
		/// This method is only called by the SetMultiModalParameters method. Therefore no handling is 
		/// required to force this method to only work with door to door. 
		/// </summary>
		private void UpdateJourneyParametersModes(TDJourneyParametersMulti parameters)
		{
			parameters.PrivateRequired = GetBooleanSingleParam("c");

			if (!parameters.PrivateRequired)
			{
				// As this is multi modal, we want to include all modes
				// except car
				ArrayList modes = new ArrayList();
				foreach (ModeType modeType in parameters.PublicModes)
				{
					if (modeType != ModeType.Car)
						modes.Add(modeType);
				}
				parameters.PublicModes = (ModeType[])modes.ToArray(typeof(ModeType));
			}

			
			// Check if we need to exclude any public transport modes
			string[] modesToExclude = GetValidMultiParam("ex");			

			if (modesToExclude != null && modesToExclude.Length != 0)
			{
				// Convert array to array list for manipulation

				ArrayList includedModes = new ArrayList(parameters.PublicModes);

				foreach (string excludedMode in modesToExclude)
				{
					switch (excludedMode)
					{
						case "r":
							includedModes.Remove(ModeType.Rail);
							break;
						case "b":
							includedModes.Remove(ModeType.Bus);
							includedModes.Remove(ModeType.Coach);
							break;
						case "u":
							includedModes.Remove(ModeType.Underground);
							includedModes.Remove(ModeType.Metro);
							break;
                        case "c":
                            includedModes.Remove(ModeType.Telecabine);
                            break;
						case "t":
							includedModes.Remove(ModeType.Tram);
							//modes.Remove(ModeType.LightRail);
							break;
						case "f":
							includedModes.Remove(ModeType.Ferry);
							break;
						case "p":
							includedModes.Remove(ModeType.Air);
							break;
						default:
							// Don't remove anything
							break;
					}
				}

                // If modes have been excluded, then ensure advanced options displayed expanded
                if (parameters.PublicModes.Length != includedModes.Count)
                {
                    if (inputPageState != null)
                        inputPageState.PublicTransportTypesVisible = true;
                }

				parameters.PublicModes = (ModeType[])includedModes.ToArray(typeof(ModeType));			
			}
		}
		/// <summary>
		/// Provides a SINGLE mode of transport to the PublicModes parameter.
		/// For use with the FindAPages
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="modeType"></param>
		private void UpdateFindAJourneyParametersModes(TDJourneyParameters parameters, ModeType modeType)
		{
			parameters.PublicModes = new ModeType[] { modeType };
		}
		/// <summary>
		/// Updates the necessary Car Fuel parameters required for a car journey. 
		/// </summary>
		private void UpdateJourneyParametersCarFuelData(TDJourneyParametersMulti parameters)
		{
			CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];
			string carSize = ds.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			string fuelType = ds.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			// Get default fuel consumption
			parameters.FuelConsumptionOption = true;
			parameters.FuelConsumptionEntered = costCalculator.GetFuelConsumption(carSize, fuelType).ToString(CultureInfo.InvariantCulture);
			// Get default fuel cost
			parameters.FuelCostOption = true;
			parameters.FuelCostEntered = costCalculator.GetFuelCost(fuelType).ToString();
		}

        /// <summary>
        /// Updates the necessary Accessible parameters requred for a public journey
        /// </summary>
        /// <param name="parameters"></param>
        private void UpdateJourneyParametersAccessible(TDJourneyParametersMulti parameters)
        {
            string accessibiltyOption = GetValidSingleParam("ao");

            if (!string.IsNullOrEmpty(accessibiltyOption))
            {
                switch (accessibiltyOption.ToLower())
                {
                    case "wa":
                        parameters.RequireStepFreeAccess = true;
                        parameters.RequireSpecialAssistance = true;
                        break;
                    case "a":
                        parameters.RequireStepFreeAccess = false;
                        parameters.RequireSpecialAssistance = true;
                        break;
                    case "w":
                        parameters.RequireStepFreeAccess = true;
                        parameters.RequireSpecialAssistance = false;
                        break;
                    default:
                        parameters.RequireStepFreeAccess = false;
                        parameters.RequireSpecialAssistance = false;
                        break;
                }

                // Only set fewer changes if a valid accessibility option
                if (parameters.RequireStepFreeAccess || parameters.RequireSpecialAssistance)
                {
                    parameters.RequireFewerInterchanges = GetBooleanSingleParam("fc");

                    // If accessibility options selected, then ensure advanced options displayed expanded
                    if (inputPageState != null)
                        inputPageState.PublicTransportOptionsVisible = true;
                }
            }
            else
            {
                parameters.RequireStepFreeAccess = false;
                parameters.RequireSpecialAssistance = false;
            }
        }

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

		}
		#endregion

		/// <summary>
		/// Return parameters from querystring arguments.
		/// If args are invalid, (non numeric or out of range) convert them to todays date as a starter
		/// This is necessary because some of the controls that we pass these args
		/// to raise exceptions if data is duff.
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns>String lookup of the parameter</returns>
		private string GetValidSingleParam (string paramName)
		{
			string tempCache = string.Empty;

			switch (paramName)
			{
				//Protect against XSS attacks (cross site scripting) by HTML Encoding the text.
				// Some of these parameter strings are displayed as is on portal, so must be made safe.
				case "m":
				case "oo":		
				case "o":
				case "on":
				case "do":	
				case "d":
				case "dn":					
				case "id":	
				case "dtd" :									
				case "rdtd":
                case "ao":
					tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
					break;
				case "enc":
					tempCache = Page.Request.Params.Get(paramName);
					break;
				case "th":
				case "tm":
				case "rth":
				case "rtm":
					try
					{
						int value = Convert.ToInt32(HttpUtility.UrlDecode(Page.Request.Params.Get(paramName)));
						tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
					}
					catch (FormatException)
					{
						return string.Empty;
					}
					break;
				default:
					tempCache = string.Empty;
					break;
			}

			if (tempCache!= null)
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
		private bool GetBooleanSingleParam (string paramName)
		{
			switch (paramName)
			{	
				//auto plan flag
				case "p":
                    // Fewer changes
                case "fc":
					if (Page.Request.Params.Get(paramName) == "1")
					{
						return true;
					}
					else
					{
						return false;
					}
				//Car default
				case "c":
					string strBoolTrueDefault = Page.Request.Params.Get(paramName);
					if (strBoolTrueDefault != null && strBoolTrueDefault.Length != 0)
					{
						strBoolTrueDefault = strBoolTrueDefault.ToLower(CultureInfo.InvariantCulture);
					}
					switch (strBoolTrueDefault)
					{
						case "true": 
							return true;
						case "false": 
							return false;
						default : 
							return true;							
					}					
				
				//Return Required
				case "r":
					string strBoolFalseDefault = Page.Request.Params.Get(paramName);
					if (strBoolFalseDefault != null && strBoolFalseDefault.Length != 0)
					{
						strBoolFalseDefault = strBoolFalseDefault.ToLower(CultureInfo.InvariantCulture);
					}
					switch (strBoolFalseDefault)
					{
						case "true": 
							return true;
						case "false": 
							return false;
						default : 
							return false;							
					}	

				//outward Depart / arrive
				case "da":
				//return Depart / arrive
				case "rda":
					string departArrive = Page.Request.Params.Get(paramName);
					switch (departArrive)
					{
						case "d": 
							return false;  //depart at
						case "a":
							return true;  //arrive by
						default :
							return false; //depart at
					}
				default:
					return false;
			}			
		}

		/// <summary>
		/// Reference Required Session variables
		/// </summary>
		private void ReferenceSessionVariables(TDJourneyParameters parameters)
		{
			originSearch = parameters.Origin;
			originLocation = parameters.OriginLocation;
			destinationSearch = parameters.Destination;
			destinationLocation = parameters.DestinationLocation;
			originLocationSelectControlType =  new LocationSelectControlType(ControlType.Default);
			destinationLocationSelectControlType =  new LocationSelectControlType(ControlType.Default);
		}

		/// <summary>
		/// Load Required Session variables
		/// </summary>
		private void LoadSessionVariables(TDJourneyParameters parameters)
		{
			parameters.Origin = originSearch;
			parameters.OriginLocation = originLocation;
			parameters.Destination = destinationSearch;
			parameters.DestinationLocation = destinationLocation;			
		}

		/// <summary>
		/// Helper method to validate day values
		/// 23112005 - This method accepts extra parameters of month and year. This is
		/// because the criteria for what constitutes a valid date are being tightened and the full date must be taken into account.
		/// </summary>
		/// <param name="day" ></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		private string GetValidDay (string day)
		{
			if (IsValidDay(day))
			{
				return day;
			}
			else
			{
				return DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Helper method to validate date values
		/// </summary>
		/// <param name="day" ></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		private bool IsValidDay (string day)
		{
			bool InvalidDate = true;

			try
			{
				int specifiedDay = Convert.ToInt32(day);
				
				if ((specifiedDay < 1) || (specifiedDay > 31))
				{
					InvalidDate = true;
				}
				else
				{
					InvalidDate = false;
				}
			}
			catch (System.FormatException)
			{
				InvalidDate = true;
			}
			catch (System.NullReferenceException)
			{
				InvalidDate = true;
			}

			return !InvalidDate;
			
		}

		/// <summary>
		/// Helper method to validate month/year values
		/// </summary>
		/// <param name="day" ></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		private bool IsValidMonthYear (string month, string year)
		{
			bool InvalidDate = true;

			try
			{
                // Code added due to existing bug in javascript for business link templates and google gadget.
                // month was coming as 010, 011, 012 when journey planned in october
                // So trim the code if it have 3 digits
                if (!string.IsNullOrEmpty(month) && month.Length == 3)
                {
                    month = month.Remove(0, 1);
                }

				DateTime specifiedDate = DateTime.ParseExact(month+year, "MMyyyy", CultureInfo.InvariantCulture );				
				//need to initalise a datetime variable to todays month and year but with date and time at 
				//the beginning of the month. 
				DateTime timeNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
				
				//If month before this month or more than three months in future, we're not happy
				if(specifiedDate.Year < timeNow.Year)
				{
					InvalidDate = true;
				}
				else if (specifiedDate.Year > timeNow.Year)
				{
					if (specifiedDate >= timeNow.AddMonths(3))
					{
						InvalidDate = true;
					}
					else
					{
						InvalidDate = false;
					}
				 }
				else
				{
					if ((specifiedDate.Month < timeNow.Month) || (specifiedDate >= timeNow.AddMonths(3)))
					{
						InvalidDate = true;
					}
					else
					{
						InvalidDate = false;
					}
				}				
			}
			catch (System.FormatException)
			{
				InvalidDate = true;
			}
			catch (System.NullReferenceException)
			{
				InvalidDate = true;
			}

			return !InvalidDate;
			
		}

		/// <summary>
		/// Helper method to format month and year values together
		/// Journey Parameters object has property that requires this format
		/// 23112005 - This method has been modified to accept extra parameter of day of month. This is
		/// because the criteria for what constitutes a valid date are being tightened and the day must be taken into account.
		/// </summary>
		/// <param name="day"></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		private string GetValidMonthYear (string month, string year)
		{
            // Code added due to existing bug in javascript for business link templates and google gadget.
            // month was coming as 010, 011, 012 when journey planned in october
            // So trim the code if it have 3 digits
            if (!string.IsNullOrEmpty(month) && month.Length == 3)
            {
                month = month.Remove(0, 1);
            }

			if (!IsValidMonthYear(month, year))
			{
				month = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
				year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
			}
			
			string monthYear;

			//Pad the month parameter with leading zero if neccessary. 
			//Controls do not function properly without this
			if (month.Length < 2)
			{
				month = "0" + month;
			}

			monthYear = month + "/" + year;		

			return monthYear;
		}

		/// <summary>
		/// Return multiple parameters from querystring arguments.		
		/// </summary>		
		private string[] GetValidMultiParam (string paramName)
		{			
			try
			{
				switch (paramName)
				{

					case "dtm":						
						DateTime OutwardDateMonthYear = DateTime.ParseExact(Page.Request.Params.Get("dtm"), "MMyyyy", CultureInfo.InvariantCulture);
						string[] strOutwardDateMonthYearArray = new string[]{PadDate(OutwardDateMonthYear.Month.ToString(CultureInfo.InvariantCulture)),PadDate(OutwardDateMonthYear.Year.ToString(CultureInfo.InvariantCulture))};
						return strOutwardDateMonthYearArray;
						
					case "rdtm":
						DateTime ReturnDateMonthYear = DateTime.ParseExact(Page.Request.Params.Get("rdtm"), "MMyyyy", CultureInfo.InvariantCulture);
						string[] strReturnDateMonthYearArray = new string[]{PadDate(ReturnDateMonthYear.Month.ToString(CultureInfo.InvariantCulture)),PadDate(ReturnDateMonthYear.Year.ToString(CultureInfo.InvariantCulture))};
						return strReturnDateMonthYearArray;

					case "dt":						
						string[] strOutwardDateArray = GetDateStringArray(Page.Request.Params.Get("dt"));
						return strOutwardDateArray;
						
					case "rdt":
						string[] strReturnDateArray = GetDateStringArray(Page.Request.Params.Get("rdt"));						
						return strReturnDateArray;
						
					case "t":
						DateTime OutwardTime = DateTime.ParseExact(Page.Request.Params.Get("t"), "HHmm", CultureInfo.InvariantCulture);
						string[] strOutwardTimeArray = new string[]{OutwardTime.Hour.ToString(CultureInfo.InvariantCulture),OutwardTime.Minute.ToString(CultureInfo.InvariantCulture)};
						return strOutwardTimeArray;
						
					case "rt":
						DateTime ReturnTime = DateTime.ParseExact(Page.Request.Params.Get("rt"), "HHmm", CultureInfo.InvariantCulture);
						string[] strReturnTimeArray = new string[]{ReturnTime.Hour.ToString(CultureInfo.InvariantCulture),ReturnTime.Minute.ToString(CultureInfo.InvariantCulture)};
						return strReturnTimeArray;
					
					case "ex":
						if (Page.Request.Params.Get("ex") != null && Page.Request.Params.Get("ex").Length != 0)
						{
							string[] excludedModes = Page.Request.Params.Get("ex").Split(DELIMITER);
							return excludedModes;
						}
						else
							return new string[0];

					default:
						return new string[0];
				}
			}
			catch (System.FormatException)
			{
				return new string[]{AUTOPLANOFF};
				}
			catch (System.ArgumentNullException)
			{
				return new string[0];
			}
		}

		/// <summary>
		/// Finds the Naptans that are closest to the provided naptan input
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns>TDNaptan[] naptans</returns>
		private TDNaptan[] PopulateNaptans(string[] naptan)
		{
			TDNaptan[] naptans = new TDNaptan[naptan.Length ];
			int i = 0;
			try
			{
				foreach (string tempNaptan in naptan)
				{
					NaptanCacheEntry x = NaptanLookup.Get(tempNaptan.Trim(), "Naptan");

					if (x.Found)
					{
						naptans[i] = new TDNaptan();				
						naptans[i].Naptan = x.Naptan;
						naptans[i].GridReference = x.OSGR;
						naptans[i].Name = x.Description;
						i++;
					}
					else
					{
						//If any Naptans are not found abort throw a format exception and allow the Landing 
						//page processing to handle the the page redirection.					
						//					Response.Redirect(complete_url);
						throw(new FormatException("Naptan code not found or invalid Naptan code used"));
					}
				}
			}
			catch // Catch's any errors from NaptanLookup.Get, e.g. where "ABC" is the naptan submitted
			{
				throw(new FormatException("Naptan code not found or invalid Naptan code used"));
			}
		
			return naptans;
		}
		
		/// <summary>
		/// Populate toids data into relevant object hierarchy
		/// </summary>
		/// <param name="osgr"></param>
		/// <returns></returns>
		private string[] PopulateToids(OSGridReference osgr)
		{
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			QuerySchema gisResult = gisQuery.FindNearestITNs(osgr.Easting, osgr.Northing);

			string[] toids = new string[gisResult.ITN.Rows.Count];

			for ( int i=0; i < gisResult.ITN.Rows.Count; i++)
			{
				QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
				toids[i] = row.toid;
			}

			return toids;
		}

		/// <summary>
		/// Populate locality data into relevant object hierarchy
		/// </summary>
		/// <param name="osgr"></param>
		/// <returns></returns>
		private string PopulateLocality(OSGridReference osgr)
		{
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
		
			return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
		}
		/// <summary>
		/// Uses the mode parameter to determine which page to redirect to.
		/// </summary>
		/// <param name="mode">Mode</param>
		/// <returns>Full URL for redirection</returns>
		private string GetRedirectUrl (string mode)
		{
			string pageURL = String.Empty;
			//create a page controller object in order to get the page transfer details for the 
			//required mode
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			PageTransferDetails pageTransferDetails;

			switch (mode)
			{
				case "r":
					pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarInput);
					break;
				case "t":
					pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindTrainInput);
					break;
				case "c":
					pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCoachInput);
					break;
				case "a":
					pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindFlightInput);					
					break;
                case "b":
                    pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCycleInput);
                    break;
				case "sorrypage":
					pageTransferDetails = pageController.GetPageTransferDetails(PageId.SorryPage);
					break;
				default:
					pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyPlannerInput);										
					break;
			}
			pageURL = pageTransferDetails.PageUrl;
			return getBaseChannelURL(TDPage.SessionChannelName) + pageURL;
		}

		/// <summary>
		/// Sets the required origin and destination parameters required for 
		/// journey planning using door to door and Find a car according to the inputs provided
		/// </summary>
		/// <param name="locationtype"></param>
		/// <param name="text"></param>
		/// <param name="locationdata"></param>
		/// <param name="location"></param>
		/// <param name="search"></param>
		private void SetLocationParameters(string locationtype, string text, string locationdata, TDLocation location, LocationSearch search, TDJourneyParameters parameters)
		{
			if (locationdata != null && locationdata.Length != 0)
			{
				try
				{
					switch (locationtype)
					{
						case "n":
							SetNaptanLocationParameters(text, locationdata, location, search);						
							break;
						case "p":			
							SetPostcodeLocationParameters(locationdata, location, search);						
							break;
						case "c":
							SetCRSLocationParameters(text, locationdata, location, search);
							break;
                        case "l":
                            SetLatLongLocationParameters(text, locationdata, location, search);
                            break;
						default:
							SetOsgridLocationParameters(text, locationdata, location, search);												
							break;						
					}	
				}
				catch (FormatException)
				{
					//reset this location only
					ResetSingleLocation(location, search, parameters);
					//reset flight parameters if in flight mode
					if (mode == "a")
					{
						ResetFlightParameters();
					}
				}
			}
			else
			{
				//reset this location only
				ResetSingleLocation(location, search, parameters);
				//reset flight parameters if in flight mode
				if (mode == "a")
				{
					ResetFlightParameters();
				}
			}
		}

		/// <summary>
		/// Sets the required origin and destination parameters required for 
		/// journey planning using Find A flight, train, coach, according to the inputs provided
		/// ONLY ACCEPTS NAPTANS or CRS
		/// </summary>
		/// <param name="locationtype"></param>
		/// <param name="text"></param>
		/// <param name="locationdata"></param>
		/// <param name="location"></param>
		/// <param name="search"></param>
		private void SetFindALocationParameters(string locationtype, string text, string locationdata, TDLocation location, LocationSearch search, TDJourneyParameters parameters)
		{
			if (locationdata != null && locationdata.Length != 0)
			{
				try
				{
					switch (locationtype)
					{
						case "c":			
							// Only set up a CRS code location if a train or car planner mode, as the other modes
							// will not plan the journey
							if ((mode == "r") || (mode == "t"))
							{
								SetCRSLocationParameters(text, locationdata, location, search);
							}
							else
							{
								throw new FormatException(); // To ensure locations are reset
							}							
							break;
						default :
							SetNaptanLocationParameters(text, locationdata, location, search);
							break;
					}					
				}
				catch (System.FormatException)
				{
					//reset this location only
					ResetSingleLocation(location, search, parameters);
					//reset flight parameters if in flight mode
					if (mode == "a")
					{
						ResetFlightParameters();
					}
				}
			}
			else
			{
				//reset this location only
				ResetSingleLocation(location, search, parameters);
				//reset flight parameters if in flight mode
				if (mode == "a")
				{
					ResetFlightParameters();
				}
			}
		}

        /// <summary>
        /// Obtains partner id, origin and destination location details from request. If parameters
        /// are encrypted then performs decryption.
        /// </summary>
        private void SetCommonParameters() 
        {

            string encryptedParam = GetValidSingleParam("enc");
		
            if (encryptedParam.Length > 0)
            {
                string[] decodedInput = DecryptedData(encryptedParam);
                //Get and verify the encrypted partner id
                partnerId = CheckPartnerId(decodedInput);
                //Get and verify the place section
                string[] locationsection = CheckPlaceSection(decodedInput);
                string type = locationsection[ PLACE_KEY ]; // Gives us either O or D
                string locationdata = locationsection[ PLACE_VAL]; // Gives us what O or D is set to
                // decrypt html encoded origin/destination data 
                locationdata = HttpUtility.UrlDecode(locationdata);			
                //determine if origin or destination has been given and set the appropriate location
                SetLocation(type, locationdata);
            }
            else
            {
                //Get and verify the unencrypted partner id
                partnerId = CheckPartnerId(GetValidSingleParam("id"));
                //We don't have to decrypt any origin or destination data, so pass helper function empty strings
                SetLocation(string.Empty, string.Empty);
            }
			
            // Origin & Destination Text and Type
            origintype = GetValidSingleParam("oo");
            destinationtype = GetValidSingleParam("do");
            origintext = GetValidSingleParam("on");
            destinationtext = GetValidSingleParam("dn");

        }

		/// <summary>
		/// Method to set parameters required on the Journey planner input page
		/// </summary>
		private void SetMultiModalParameters()
		{
			//create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;			
			
			//initialise journey parameters
			sessionManager.InitialiseJourneyParameters(FindAMode.None);
			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = sessionManager.InputPageState;

            SetCommonParameters();

            //load the session variables from the JourneyParameters
			ReferenceSessionVariables(journeyParameters);
			
			try
			{
				//set location paramters for the origin
				odFlag = ORIGIN_FLAG;
				SetLocationParameters(origintype, origintext, origindata, originLocation, originSearch, journeyParameters);
			
				//set location paramters for the destination
				odFlag = DEST_FLAG;
				SetLocationParameters(destinationtype, destinationtext, destinationdata, destinationLocation, destinationSearch, journeyParameters);
			}
			catch(TransportDirect.Presentation.InteractiveMapping.MapExceptionGeneral)
			{
				//catch any format exceptions thrown when unable to resove input
				ResetAndRedirect(journeyParameters);
			}

			//Set the journeyParameters outward and return date/time
			UpdateJourneyParametersDateTime(journeyParameters);

			//Set the journeyParameters public and private modes
			UpdateJourneyParametersModes(journeyParameters);
				
			//Set the journeyParameters  fuel consumption and cost
			UpdateJourneyParametersCarFuelData(journeyParameters);

            //Set the accessible journey options
            UpdateJourneyParametersAccessible(journeyParameters);

			// Final parameter set to false so that the search type is not overwrtten by the 
			// default value			
			originSelect.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, false, true, false);
			destinationSelect.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, false, true, false);

            // Update the location accessible flag, this will only be done if the 
            // journey parameters has accessible preferences set and location status is valid
            LocationSearchHelper.CheckAccessibleLocation(ref originLocation, journeyParameters);
            LocationSearchHelper.CheckAccessibleLocation(ref destinationLocation, journeyParameters);

            journeyParameters.PublicViaType = new LocationSelectControlType(ControlType.Default);
            journeyParameters.PrivateViaType = new LocationSelectControlType(ControlType.Default);

            // If public or car options are set to visible, then ensure both are
            if (inputPageState != null
                && (inputPageState.PublicTransportOptionsVisible
                    || inputPageState.PublicTransportTypesVisible
                    || inputPageState.CarOptionsVisible))
            {
                inputPageState.PublicTransportOptionsVisible = true;
                inputPageState.PublicTransportTypesVisible = true;
                inputPageState.CarOptionsVisible = true;
            }

			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.DoorToDoor, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);	
		}
		
		/// <summary>
		/// Method to set parameters required on the Find A Train input page
		/// </summary>
		private void SetFindATrainParameters()
		{
			//create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;						

			//initialise journey parameters
			sessionManager.InitialiseJourneyParameters(FindAMode.Train);
			findCoachTrainPageState = (FindCoachTrainPageState)TDSessionManager.Current.FindPageState;
			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;			

            SetCommonParameters();

			//load the session variables from the JourneyParameters
			ReferenceSessionVariables(journeyParameters);
			
			//Set the journeyParameters public and private modes
			journeyParameters.PrivateRequired = false;
            journeyParameters.PublicViaType = new LocationSelectControlType(ControlType.Default);

			UpdateFindAJourneyParametersModes(journeyParameters, ModeType.Rail);

			try
			{
				//set location paramters for the origin
				odFlag = ORIGIN_FLAG;
				SetFindALocationParameters(origintype, origintext, origindata, originLocation, originSearch, journeyParameters);
			
				//set location paramters for the destination
				odFlag = DEST_FLAG;
				SetFindALocationParameters(destinationtype, destinationtext, destinationdata, destinationLocation, destinationSearch, journeyParameters);
			}
			catch(TransportDirect.Presentation.InteractiveMapping.MapExceptionGeneral)
			{
				//catch any format exceptions thrown when unable to resove input
				ResetAndRedirect(journeyParameters);
			}			

			//Set the journeyParameters outward and return date/time
			UpdateJourneyParametersDateTime(journeyParameters);						

			// Final parameter set to false so that the search type is not overwrtten by the 
			// default value			
			findAOriginLocationsControl.FromLocationControl.TriLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, false, true, false);
			findADestinationLocationsControl.ToLocationControl.TriLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, false, true, false);

			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.FindATrain, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);
		}

		/// <summary>
		/// Method to set parameters required on the Find A Car input page
		/// </summary>
		private void SetFindACarParameters()
		{
			//create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;						

			//initialise journey parameters
			sessionManager.InitialiseJourneyParameters(FindAMode.Car);
			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;			

            //load the session variables from the JourneyParameters
            ReferenceSessionVariables(journeyParameters);

            // Intialise search type to set default gazetteer 
            originSearch.SearchType = SearchType.City;
            destinationSearch.SearchType = SearchType.City;

			//Set the journeyParameters public and private modes
			journeyParameters.PublicModes = new ModeType[0];
			journeyParameters.PublicRequired = false;
			journeyParameters.PrivateRequired = true;		
            journeyParameters.PrivateViaType = new LocationSelectControlType(ControlType.Default);

            SetCommonParameters();
			
            try
			{
				//set location paramters for the origin
				odFlag = ORIGIN_FLAG;
				SetLocationParameters(origintype, origintext, origindata, originLocation, originSearch, journeyParameters);
			
				//set location paramters for the destination
				odFlag = DEST_FLAG;
				SetLocationParameters(destinationtype, destinationtext, destinationdata, destinationLocation, destinationSearch, journeyParameters);
			}
			catch(TransportDirect.Presentation.InteractiveMapping.MapExceptionGeneral)
			{
				//catch any format exceptions thrown when unable to resove input
				ResetAndRedirect(journeyParameters);
			}

			//Set the journeyParameters outward and return date/time
			UpdateJourneyParametersDateTime(journeyParameters);

					
			//Set the journeyParameters  fuel consumption and cost
			UpdateJourneyParametersCarFuelData(journeyParameters);

			// Final parameter set to false so that the search type is not overwrtten by the 
			// default value			
			findAOriginLocationsControl.FromLocationControl.TriLocationControl.Populate(DataServiceType.FindCarLocationDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, false, true, false);
			findADestinationLocationsControl.ToLocationControl.TriLocationControl.Populate(DataServiceType.FindCarLocationDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, false, true, false);

			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.FindACar, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);
		}

        /// <summary>
        /// Method to set parameters required on the Find a Cycle input page
        /// </summary>
        private void SetFindACycleParameters()
        {
            //create new session and itinerary managers
            ITDSessionManager sessionManager = TDSessionManager.Current;

            //initialise journey parameters
            sessionManager.InitialiseJourneyParameters(FindAMode.Cycle);
            findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;
            journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = TDSessionManager.Current.InputPageState;
            FindCycleInputAdapter findCycleInputAdapter = new FindCycleInputAdapter(journeyParameters, findCyclePageState, inputPageState);
            findCycleInputAdapter.InitJourneyParameters();

            ReferenceSessionVariables(journeyParameters);

            SetCommonParameters();

            try
            {
                odFlag = ORIGIN_FLAG;
                SetLocationParameters(origintype, origintext, origindata, originLocation, originSearch, journeyParameters);

                odFlag = DEST_FLAG;
                SetLocationParameters(destinationtype, destinationtext, destinationdata, destinationLocation, destinationSearch, journeyParameters);
            }
            catch (MapExceptionGeneral)
            {
                ResetAndRedirect(journeyParameters);
            }
            UpdateJourneyParametersDateTime(journeyParameters);

            findAOriginLocationsControl.FromLocationControl.TriLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, false, true, false);
            findADestinationLocationsControl.ToLocationControl.TriLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, false, true, false);

            //Mis logging				
            LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.FindACycle, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(lpee);
        }

		/// <summary>
		/// Method to set parameters required on the Find A Flight input page
		/// </summary>
		private void SetFindAFlightParameters()
		{
			//create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;						

			//initialise journey parameters
			sessionManager.InitialiseJourneyParameters(FindAMode.Flight);
			findFlightPageState = (FindFlightPageState)TDSessionManager.Current.FindPageState;
			journeyParametersFlight = sessionManager.JourneyParameters as TDJourneyParametersFlight;						

            SetCommonParameters();

            //load the session variables from the JourneyParametersFlight
			ReferenceSessionVariables(journeyParametersFlight);

			//Set the journeyParameters public and private modes
			//journeyParametersFlight.PrivateRequired = false;
            UpdateFindAJourneyParametersModes(journeyParametersFlight, ModeType.Air);
			
            //set the page
            findFlightPageState.OriginLocationFixed = true;
            findFlightPageState.DestinationLocationFixed = true;
            findFlightPageState.ResolvedFromLocation = originLocation;
            findFlightPageState.ResolvedToLocation = destinationLocation;

            try
			{
				//set flag & location paramters for the origin
				odFlag = ORIGIN_FLAG;
				SetFindALocationParameters(origintype, origintext, origindata, originLocation, originSearch, journeyParametersFlight);
			
				//set flag & location paramters for the destination
				odFlag = DEST_FLAG;
				SetFindALocationParameters(destinationtype, destinationtext, destinationdata, destinationLocation, destinationSearch, journeyParametersFlight);
			}
			catch(TransportDirect.Presentation.InteractiveMapping.MapExceptionGeneral)
			{
				//catch any format exceptions thrown when unable to resove input
				ResetFlightParameters();
				ResetAndRedirect(journeyParametersFlight);
			}

			//Set the journeyParameters outward and return date/time
			UpdateJourneyParametersDateTime(journeyParametersFlight);
			
			//load the populatesd session variable to the journeyparametersflight object
			LoadSessionVariables(journeyParametersFlight);
				
			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.FindAFlight, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);
		}

		/// <summary>
		/// Method to set parameters required on the Find A Coach input page
		/// </summary>
		private void SetFindACoachParameters()
		{
			//create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;						

			//initialise journey parameters
			sessionManager.InitialiseJourneyParameters(FindAMode.Coach);
			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;				

            SetCommonParameters();

            //load the session variables from the JourneyParameters
			ReferenceSessionVariables(journeyParameters);

			//Set the journeyParameters public and private modes
			journeyParameters.PrivateRequired = false;
            journeyParameters.PublicViaType = new LocationSelectControlType(ControlType.Default);
            UpdateFindAJourneyParametersModes(journeyParameters, ModeType.Coach);
			
			try
			{
				//set location paramters for the origin
				odFlag = ORIGIN_FLAG;
				SetFindALocationParameters(origintype, origintext, origindata, originLocation, originSearch, journeyParameters);
			
				//set location paramters for the destination
				odFlag = DEST_FLAG;
				SetFindALocationParameters(destinationtype, destinationtext, destinationdata, destinationLocation, destinationSearch, journeyParameters);
			}
			catch(TransportDirect.Presentation.InteractiveMapping.MapExceptionGeneral)
			{
				//catch any format exceptions thrown when unable to resove input
				ResetAndRedirect(journeyParameters);
			}

			//Set the journeyParameters outward and return date/time
			UpdateJourneyParametersDateTime(journeyParameters);					

			// Final parameter set to false so that the search type is not overwrtten by the 
			// default value			
			findAOriginLocationsControl.FromLocationControl.TriLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, false, true, false);
			findADestinationLocationsControl.ToLocationControl.TriLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, false, true, false);

			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.FindACoach, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);
		}

		/// <summary>
		/// Checks if the naptan code provided is an Airpot naptan
		/// Airport naptans always start with 9200
		/// </summary>
		/// <param name="naptan">naptan string provided by user</param>
		/// <returns>whether naptan is airport naptan</returns>
		private bool IsAirportNaptan(string naptan)
		{
			return naptan.StartsWith(IATA_PREFIX);
		}

		/// <summary>
		/// Method called when a format exception occurs when trying to assign naptans/osgrids
		/// Resets the origin and destination objects and redirects to a blank input page 
		/// </summary>
		private void ResetAndRedirect(TDJourneyParameters parameters)
		{
			//reset origin and destination objects and journeyparameter origin/destination objects
			originLocation.ClearAll();
			destinationLocation.ClearAll();
			originSearch.ClearAll();
			destinationSearch.ClearAll();
			originSearch.LocationFixed = false;
			destinationSearch.LocationFixed = false;
			parameters.OriginType = new LocationSelectControlType(ControlType.Default);
			parameters.DestinationType = new LocationSelectControlType(ControlType.Default);			

			//Redirect to Blank input page
			Response.Redirect(complete_url);
		}

		/// <summary>
		/// Resets the parameters used by find a flight
		/// </summary>
		/// <param name="odFlag"></param>
		private void ResetFlightParameters()
		{
			if(odFlag == ORIGIN_FLAG)
			{
				journeyParametersFlight.SetOriginDetails(null, null);
				findFlightPageState.ResetOriginLocation();								
				journeyParametersFlight.SelectedOperators = new string[0];
				journeyParametersFlight.OnlyUseSpecifiedOperators = false;
			}
			else
			{
				journeyParametersFlight.SetDestinationDetails(null, null);
				findFlightPageState.ResetDestinationLocation();
				journeyParametersFlight.SelectedOperators = new string[0];
				journeyParametersFlight.OnlyUseSpecifiedOperators = false;
			}
		}

		private void ResetSingleLocation(TDLocation location, LocationSearch search, TDJourneyParameters parameters)
		{
			location.Status = TDLocationStatus.Unspecified;	
			search.LocationFixed = false;
			location.ClearAll();
			search.ClearAll();
			if (odFlag == ORIGIN_FLAG)
			{
				parameters.OriginType = new LocationSelectControlType(ControlType.Default);					
			}
			else if (odFlag == DEST_FLAG)
			{
				parameters.DestinationType = new LocationSelectControlType(ControlType.Default);
			}
		}

		/// <summary>
		/// Sets the location paramters when a naptan input is recieved
		/// </summary>
		/// <param name="text">The name of the location passed in in the url as an 'on' or 'dn' parameter</param>
		/// <param name="locationdata"></param>
		/// <param name="location"></param>
		/// <param name="search"></param>
		private void SetNaptanLocationParameters(string text, string locationdata, TDLocation location, LocationSearch search)
		{
			try
			{
				location.Description = HttpUtility.HtmlDecode(text);
                search.InputText = location.Description;

				OSGridReference osgr = new OSGridReference();
				// Create array of naptans from string, but convert to upper first
				string[] strNaptan = locationdata.ToUpper().Split(DELIMITER);
				location.NaPTANs = PopulateNaptans(strNaptan);									
				//need to find osgrid to populate the toid and locality					
				foreach (TDNaptan naptan in location.NaPTANs)
				{
					if (naptan.Naptan == strNaptan[0])
					{
						osgr = naptan.GridReference;								
					}
				}

                // Fix for USD UK:5129772 - Page Landing origin and destination text should be auto populated
                // when the user doesn't supply text and the type is NaPTAN.
                if (location.Description == string.Empty && location.NaPTANs.Length == 1)
                {
                    location.Description = location.NaPTANs[0].Name;
                }

				location.GridReference = osgr;
				location.PopulateToids();
				location.Locality = PopulateLocality(osgr);
				location.Status = TDLocationStatus.Valid;
                location.SearchType = SearchType.MainStationAirport;
                location.RequestPlaceType = RequestPlaceType.NaPTAN;
                
                search.SearchType = SearchType.MainStationAirport;
                search.LocationFixed = true;
			}
			catch (FormatException)
			{											
				OperationalEvent defaultoe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"Naptan Provided has not been found."+
					" Partner ID : " + partnerId + " client-ip : " 
					+ Page.Request.UserHostAddress.ToString() + " url-referrer : " 
					+ Page.Request.UrlReferrer);
				Logger.Write(defaultoe);

				//throw exception to the caller
				throw;
			}
			catch (TransportDirect.Presentation.InteractiveMapping.MapExceptionGeneral)
			{
				OperationalEvent defaultoe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"Locality could not be found for Naptan Provided."+
					" Partner ID : " + partnerId + " client-ip : " 
					+ Page.Request.UserHostAddress.ToString() + " url-referrer : " 
					+ Page.Request.UrlReferrer);
				Logger.Write(defaultoe);

				//throw exception to the caller
				throw;
			}
		}
		/// <summary>
		/// Sets the location paramters when a postcode input is recieved
		/// </summary>
		/// <param name="text"></param>
		/// <param name="locationdata"></param>
		/// <param name="location"></param>
		/// <param name="search"></param>
		private void SetPostcodeLocationParameters(string locationdata, TDLocation location, LocationSearch search)
		{
			// The 1000 is defined somewhere as a property -> needs to be fixed
			LocationChoiceList lcl = search.StartSearch(locationdata, SearchType.AddressPostCode, false, 1000, TDSessionManager.Current.Session.SessionID, false );
			if(lcl.Count == 0 ) 
			{
				// No such post code was found
				// Do what-ever we need to do if this post code does not exist
				// Might just leave it as be and let the input page handle it with input enabled?
				return;
			}
			// For a post code - there should only be _ONE_ location choice for us
			// If not, then ambiguity page will handle any ambiguity as normal.
			if (lcl.Count == 1)
			{
				LocationChoice lc = (LocationChoice)lcl[0];
				search.GetLocationDetails( ref location, lc);
				location.Status = TDLocationStatus.Valid;
                location.SearchType = SearchType.AddressPostCode;
			}
							
			search.LocationFixed = !autoplanflag;
            search.SearchType = SearchType.AddressPostCode;
		}

		/// <summary>
		/// Sets the location paramters when a crs input is recieved.
		/// Converts crs code into a naptan code, then calls SetNaptanLocationParameters
		/// </summary>
		/// <param name="text"></param>
		/// <param name="locationdata"></param>
		/// <param name="location"></param>
		/// <param name="search"></param>
		private void SetCRSLocationParameters(string text, string locationdata, TDLocation location, LocationSearch search)
		{
			try
			{	
				if (useCRS)
				{
					// In case multiple CRS codes supplied
					string[] strCRS = locationdata.ToUpper().Split(DELIMITER);

					// DN states to only allow 1 CRS code, otherwise throw error
					if (strCRS.Length == 1)
					{
						#region Code to handle 1 or more CRS codes

						// Create a new string containing the naptans to be assigned to "locationdata"
						StringBuilder sbNaptans = new StringBuilder();

						// Attempt to find NaPTAN codes for the CRS codes supplied
						TDCodeGazetteer cg = new TDCodeGazetteer();
						TDCodeDetail[] codedetail;

						foreach (string codeCRS in strCRS)
						{
							codedetail =  cg.FindCode(codeCRS.Trim());

							if (codedetail.Length > 0)
							{
								for (int i=0; i < codedetail.Length; i++)
								{
									TDCodeDetail cd = codedetail[i];

									// Only add the Rail NaPTANs to the string
									if (cd.ModeType == TDModeType.Rail)
									{

										sbNaptans.Append( cd.NaptanId );
										sbNaptans.Append( DELIMITER );

										#region Log success
										// Log CRS code was converted to a Naptan
										StringBuilder message = new StringBuilder();
										message.Append( "CRS code: " );
										message.Append( codeCRS.Trim() );
										message.Append( " converted to Rail NaPTAN: " );
										message.Append( cd.NaptanId );
										message.Append( " " );

										OperationalEvent oe = new  OperationalEvent(
											TDEventCategory.Business, TDTraceLevel.Info, message.ToString());

										Logger.Write(oe);
										#endregion
										
										// If text entered by user is empty, then take the first Naptan's description
										if (text == string.Empty)
										{
											NaptanCacheEntry nce = NaptanLookup.Get(cd.NaptanId.Trim(), "Naptan");
											if (nce.Found)
												text = nce.Description;
											else
												text = cd.Description;
										}
									}
								} //end for loop
							}
							else // Log the CRS code couldnt be resolved to a NaPTAN
							{
								StringBuilder message = new StringBuilder();
								message.Append( "CRS code provided did not return any NaPTANs using TDCodeGazetteer.FindCode." );
								message.Append( " CRS code: " );
								message.Append( codeCRS.Trim() );
								
								LogError(message.ToString(), partnerId);
							}
						}
						#endregion

						// CRS codes have now been converted to NaPTANS, so handover to SetNaptanLocationParameters to finish the work
						if (sbNaptans.Length > 0)
						{
							locationdata = sbNaptans.ToString();
							locationdata = locationdata.TrimEnd(DELIMITER); // remove trailing ',' to prevent search for empty string error in Naptan lookup
							SetNaptanLocationParameters(text, locationdata, location, search);
						}
						else
						{
							// Do nothing, the input page will display blank input location field
							throw new Exception();
						}
					}
					else  // Detected multiple "CRS codes", therefore error out of code
					{
						throw new Exception();
					}
				}
				else // UseCRS turned off
				{
					// Do nothing, the input page has an empty location field
				}
			}
			catch
			{
				StringBuilder message = new StringBuilder();
				message.Append( "CRS code provided failed to be converted to a Rail NaPTAN." );
				message.Append( " CRS code: " );
				message.Append( locationdata );

                LogError(message.ToString(), partnerId);
			}
		}

        private void SetLatLongLocationParameters(string text, string locationdata, TDLocation location, LocationSearch search)
        {
            try
            {
                location.Description = HttpUtility.HtmlDecode(text);
                search.InputText = location.Description;
                string strdefaultOsgren = locationdata;
                string[] defaultgrids = strdefaultOsgren.Split(DELIMITER);
                if (defaultgrids != null && defaultgrids.Length == 2)
                {
                    double defaultlatitude = double.Parse(defaultgrids[0], CultureInfo.InvariantCulture);
                    double defaultlongitude = double.Parse(defaultgrids[1], CultureInfo.InvariantCulture);
                    LatitudeLongitude defaultlatlong = new LatitudeLongitude(defaultlatitude, defaultlongitude);
                    LandingPageHelper lphelper = new LandingPageHelper();

                    OSGridReference osgr = lphelper.GetOSGRFromLatLong(defaultlatlong);
                    SetOsgridLocationParameters(text, osgr.Easting.ToString() + ", " + osgr.Northing.ToString(), location, search);
                }
                else
                {
                    // OSGR is default location type. If location data is not two comma seperated values
                    // then not valid coordinates, so have to ignore values. 
                    ResetSingleLocation(location, search, journeyParameters);
                }
            }
            catch (FormatException)
            {
                //osgrid not resolved, redirect to input page
                OperationalEvent defaultoe =
                    new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    "LatitudeLongitude Provided could not be resolved." +
                    " Partner ID : " + partnerId + " client-ip : "
                    + Page.Request.UserHostAddress.ToString() + " url-referrer : "
                    + Page.Request.UrlReferrer);
                Logger.Write(defaultoe);

                throw;
            }
            
        }

		/// <summary>
		/// Sets the location paramters when a osgrid input is recieved
		/// </summary>
		/// <param name="text"></param>
		/// <param name="locationdata"></param>
		/// <param name="location"></param>
		/// <param name="search"></param>
		private void SetOsgridLocationParameters(string text, string locationdata, TDLocation location, LocationSearch search)
		{
			try
			{
				location.Description = HttpUtility.HtmlDecode(text);
                search.InputText = location.Description;
				string strdefaultOsgren = locationdata;
				string[] defaultgrids = strdefaultOsgren.Split(DELIMITER);
				if (defaultgrids != null && defaultgrids.Length == 2)
				{
					int defaulteasting = int.Parse(defaultgrids[0], CultureInfo.InvariantCulture);
					int defaultnorthing = int.Parse(defaultgrids[1], CultureInfo.InvariantCulture);						
					OSGridReference defaultosgren = new OSGridReference(defaulteasting, defaultnorthing);
					location.GridReference = defaultosgren;
					location.PopulateToids();
					location.Locality = PopulateLocality(defaultosgren);
					location.RequestPlaceType = RequestPlaceType.Coordinate;
					location.Status = TDLocationStatus.Valid;
							
					search.LocationFixed = true;
				}
				else
				{
					// OSGR is default location type. If location data is not two comma seperated values
					// then not valid coordinates, so have to ignore values. 
					ResetSingleLocation(location, search, journeyParameters);
				}
			}
			catch (FormatException)
			{
				//osgrid not resolved, redirect to input page
				OperationalEvent defaultoe = 
					new  OperationalEvent(	TDEventCategory.Business,
					TDTraceLevel.Error,
					"OSgrid Provided could not be resolved."+
					" Partner ID : " + partnerId + " client-ip : " 
					+ Page.Request.UserHostAddress.ToString() + " url-referrer : " 
					+ Page.Request.UrlReferrer);
				Logger.Write(defaultoe);				

				throw;
			}
		}

		/// <summary>
		/// Method to Pad the date with zeros if only one digit.
		/// </summary>
		/// <param name="s"></param>
		/// <returns>string</returns>
		private string PadDate(string s)
		{
			string date = s;

			if (date.Length < 2)
			{
				string paddedDate = s.PadLeft(2, '0');

				return paddedDate;
			}

			else
			{
				return date;
			}

		}

		/// <summary>
		/// Helper method to converts the date input into a array of string containing day, month, year
		/// Tries to convert to a date time object. 
		/// If this does not work then we need to check if the date can be selected on the portal, if so 
		/// then this date should be allowed to go through to ambiguity
		/// </summary>
		/// <param name="dateInputString"></param>
		/// <returns></returns>
		private string[] GetDateStringArray(string dateInputString)
		{
			string[] result = new string[0];
			try
			{
				DateTime OutwardDate = DateTime.ParseExact(dateInputString, "ddMMyyyy", CultureInfo.InvariantCulture);
				string[] strDateArray = new string[]{PadDate(OutwardDate.Day.ToString(CultureInfo.InvariantCulture)),PadDate(OutwardDate.Month.ToString(CultureInfo.InvariantCulture)),OutwardDate.Year.ToString(CultureInfo.InvariantCulture)};
				return strDateArray;
			}
			catch (System.FormatException)
			{
				if (dateInputString.Length == 8)
				{
					string day = dateInputString.Substring(0,2);
					string month = dateInputString.Substring(2,2);
					string year = dateInputString.Substring(4,4);

					try
					{
						int daycheck = Convert.ToInt32(day);
						if (daycheck < 1 || daycheck > 31)
						{   //date entered but day outside selectable range
							return new string[]{AUTOPLANOFF};                    
						}
						return new string[]{day,month,year};
					}
					catch 
					{   //date entered cannot convert to and integer
						return new string[]{AUTOPLANOFF};
					}									
				}
                // Found an issue with the SetDateTimeDropDown method in businesslink and gadget template
                // to resolve the issue with template which get used by older client following code is added
                else if (dateInputString.Length == 9) 
                {
                    string day = dateInputString.Substring(0, 2);
                    string month = dateInputString.Substring(3, 2);
                    string year = dateInputString.Substring(5, 4);

                    try
                    {
                        int daycheck = Convert.ToInt32(day);
                        if (daycheck < 1 || daycheck > 31)
                        {   //date entered but day outside selectable range
                            return new string[] { AUTOPLANOFF };
                        }
                        return new string[] { day, month, year };
                    }
                    catch
                    {   //date entered cannot convert to and integer
                        return new string[] { AUTOPLANOFF };
                    }
                }
				else if (dateInputString.Length != 0)
				{  //date entered but length not as expected
					return new string[]{AUTOPLANOFF}; 
				}
				else if (dateInputString.Length == 0)
				{
					// nothing entered
					return new string[0];
				}
			}
			catch (System.ArgumentNullException)
			{
				return new string[0];
			}
			// if got to here then something wrong
			return result;
		}

	}
}