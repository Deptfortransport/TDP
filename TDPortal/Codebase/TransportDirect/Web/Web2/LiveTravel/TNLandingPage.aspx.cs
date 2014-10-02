// ****************************************************************************************** 
// NAME                 : TNLandingPage.aspx.cs 
// AUTHOR               : Jamie McAllister
// DATE CREATED         : 31/10/2005 
// DESCRIPTION          : Non-visual page providing a Landing Page entry point for the Travel News page. 
// ****************************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/LiveTravel/TNLandingPage.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:25:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:52   mturner
//Initial revision.
//
//   Rev 1.10   Jun 21 2006 17:55:38   rphilpott
//Fix stupid bug if partner id starts with https
//Resolution for 4121: Landing Page: Truncation of Partner ID incorrect when HTTPS used
//
//   Rev 1.9   Jun 07 2006 18:12:06   rphilpott
//Prevent unnecessary truncation of partner-ids on GET requests.
//Resolution for 4100: Landing Page: Truncation of Partner-IDs
//
//   Rev 1.8   Feb 24 2006 10:17:36   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.7   Feb 13 2006 10:52:50   kjosling
//Fixed
//
//   Rev 1.6   Jan 11 2006 11:22:18   jmcallister
//IR3441 - Remove validation of Partner IDs
//Resolution for 3441: Del8.1 stream - Removing Partner id validation for Landing Page
//
//   Rev 1.5   Dec 13 2005 10:41:28   jmcallister
//IR3360. Landing page appends fake querystring argument to redirect url. This prevents IE from using cached page when inputting a changed Landing Page URL in the same browser instance. Latest results are now always displayed without need to press view-refresh in IE.
//Resolution for 3360: Del 8.0 Page Landing - Ability to update URL on the same page
//
//   Rev 1.4   Dec 08 2005 18:31:16   jmcallister
//Last revision had a tricky issue that prevented unencrypted Landing from working correctly. Solved this version.
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.3   Dec 08 2005 12:47:30   jmcallister
//FXCop and code review comments resulting from IR3320
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.2   Dec 01 2005 14:27:12   jmcallister
//Secured Landing Pages against XSS attacks by HTML encoding text params. Also altered processing so that queries with unencrypted parameters will be allowed as long as a valid partner id is supplied.
//Resolution for 3255: Landing Page and x-site scripting exploit
//
//   Rev 1.1   Nov 02 2005 16:13:36   jmcallister
//Code review comments enacted
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.UserSupport;
using TransportDirect.Web.Support;

using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface;


using Logger = System.Diagnostics.Trace;

using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// TNLandingPage provides an entry point into the Travel News page where user parameters are provided via 
	/// querystring arguments or a POST operation. The parameters are parsed, and allow the user to view 
	/// Travel News without manually specifying any parameters themselves.
	/// </summary>
	public partial class TNLandingPage : TDPage
	{
		/// <summary>
		/// Partner Id (shortcut "id")
		/// </summary>
		private string partnerId = string.Empty;
		/// <summary>
		/// Display type (shortcut "tm")
		/// </summary>
		private string displaytype = string.Empty;
		/// <summary>
		/// Region type (shortcut "rg")
		/// </summary>
		private string regiontype = string.Empty;
		/// <summary>
		/// Severity type (shortcut "sv")
		/// </summary>
		private string severitytype = string.Empty;
		/// <summary>
		/// Transport type (shortcut "nt")
		/// </summary>
		private string transporttype = string.Empty;
		
		//Data services object
		private IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];				
		
		#region Global definitions
		private static readonly char[] DELIMITER = new char[] {','};
		private static readonly char[] SECTION_SEP = new char[] { '&' };
		private static readonly char[] SUBSECTION_SEP = new char[] { ':' };
		private const int ID_SECTION = 0;
		private const int PLACE_SECTION = 1;

		private const int ID_KEY = 0;
		private const int ID_VAL = 1;

		private const int PLACE_KEY = 0; // will be rg
		private const int PLACE_VAL = 1; // Will contain the region as sent by vendor

		private const string HTTP_REQUEST_TYPE_GET  = "GET";
		private const string HTTP_REQUEST_ID_STRING1 = "?id=";
		private const string HTTP_REQUEST_ID_STRING2 = "&id=";
		private const char AMPERSAND = '&';

		private const string PREFIX_HTTP  = "http";
		private const string PREFIX_HTTPS = "https";
		private const string PREFIX_WWW	  = "www";

		private const int SAFE_PARTNERID_LENGTH = 10;

		//page redirection urls
		private string travelnewsurl = "LiveTravel/TravelNews.aspx";
		//final url that will do the redirect
		private string complete_url = string.Empty;	
		
		
		#endregion

		/// <summary>
		/// Constructor for the page. Used to set PageId value.
		/// </summary>
		public TNLandingPage()
		{
			pageId = PageId.TNLandingPage;
		}

		/// <summary>
		/// Page_Load controls the control of flow for this page, as no user interacctions occur with TNLanding.
		/// (The page is invisible!)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			regiontype = GetValidSingleParam("rg");
			if (TDPage.SessionChannelName !=  null )
			{
				string pageURL = travelnewsurl;
			
				//Add system time to end of redirect URL (fake querystring param). This ensures IE always requests latest version of page rather than relying on cache. IR3360.
				complete_url = getBaseChannelURL(TDPage.SessionChannelName) + pageURL + "?x=" + Server.UrlEncode(DateTime.UtcNow.ToLongTimeString());				
			}

			//create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;			
			TDItineraryManager itineraryManager = TDItineraryManager.Current;			
			// Reset the itinerary manager
			itineraryManager.NewSearch();	

			//Get encrypted section
			string encryptedParam = GetValidSingleParam("enc");
	
			if (encryptedParam.Length > 0)
			{
				string[] decodedInput = DecryptedData(encryptedParam);

				//Get and verify the encrypted partner id
				partnerId = CheckPartnerId(decodedInput);
		
				// decrypt html encoded origin/destination data 

				string[] locationsection = CheckPlaceSection(decodedInput);
				//string type = locationsection[ PLACE_KEY ]; // Gives us rg
				string locationdata = locationsection[ PLACE_VAL]; // Gives us what rg is set to
				// decrypt html encoded region data 
				regiontype = HttpUtility.UrlDecode(locationdata);
			}
			else
			{
				//No encrypted section so process unencrypted params
				//Get and verify the unencrypted partner id
				partnerId = CheckPartnerId(GetValidSingleParam("id"));
				regiontype = GetValidSingleParam("rg");
			}		
		
			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.TravelNews, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);	
					
			// Get News Filter Params
			transporttype = GetValidSingleParam("nt");
			severitytype = GetValidSingleParam("sv");
			displaytype = GetValidSingleParam("tm");
			
			//set the landingpage switch to true to identify the request as a Landing page request
			TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ] = true;
			TDSessionManager.Current.Session[ SessionKey.LandingPageNewsShowNewsComplete] = false;
			TDSessionManager.Current.Session[ SessionKey.LandingPageNewsRegionSelectComplete] = false;
			
			//set the news filters in the session object

			TDSessionManager.Current.Session[ SessionKey.LandingPageNewsDisplayInputType ] = displaytype;
			TDSessionManager.Current.Session[ SessionKey.LandingPageNewsRegionInputType ] = regiontype;
			TDSessionManager.Current.Session[ SessionKey.LandingPageNewsSeverityInputType ] = severitytype;
			TDSessionManager.Current.Session[ SessionKey.LandingPageNewsTransportInputType ] = transporttype;

			Response.Redirect(complete_url);
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
		/// <returns>string lookup of the parameter</returns>
		private string GetValidSingleParam (string paramName)
		{
			string tempCache = string.Empty;

			switch (paramName)
			{
				case "rg":
				case "nt":		
				case "sv":
				case "tm":
				case "lo":	
				case "l":
				case "ln":		
				case "id":
					tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
					break;
				case "enc":
					tempCache = Page.Request.Params.Get(paramName);
					break;

				default:
					tempCache = string.Empty;
					break;
			}

			if (tempCache!= null)
			{
				//HTML encode output to eliminate chance of XSS attacks.
				return Server.HtmlEncode(tempCache);
			}
			else
			{
				return string.Empty;
			}
			
		}


		/// <summary>
		/// Decrypts encrypted data and parses into a string array
		/// </summary>
		/// <param name="encrypteddata">Encrypted data</param>
		/// <returns>string array of decrpyted data</returns>
		private string[] DecryptedData(string encrypteddata)
		{
			ITDCrypt decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ ServiceDiscoveryKey.Crypto ];
			string decryptedText = decryptionEngine.AsymmetricDecrypt(encrypteddata);
			return decryptedText.Split( SECTION_SEP );				
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
		/// Check that the partner Id is valid
		/// </summary>
		/// <param name="decodedInput">Decoded input data</param>
		/// <returns>Partner Id</returns>
		private string CheckPartnerId(string[] decodedInput)
		{
			// Probably ensure the size of the decoded input
			string[] vendor_section = decodedInput[ID_SECTION].Split( SUBSECTION_SEP );
			// Verify/fail if vendor_section length != 2 and that part vendor_section[ID_KEY] == "id"
			// Does not conform to 
			// id=[vendorid]
			if ((vendor_section.Length != 2) || (vendor_section[ID_KEY].CompareTo("id") != 0))
			{
				LogError("Partner section provided does not conform to the id=[partnerID] format.", partnerId);

				//Redirect to Blank input page
				Response.Redirect(complete_url);
			}
			
			//Check partner ID using overload of this method
			return CheckPartnerId(vendor_section[ID_VAL]);
		}

		/// <summary>
		/// Check that the place section is valid
		/// </summary>
		/// <param name="decodedInput">Decoded input data</param>
		/// <returns>string array with rg=[place]</returns>
		private string[] CheckPlaceSection(string[] decodedInput)
		{
			string[] place_section = decodedInput[PLACE_SECTION].Split( SUBSECTION_SEP );
			if( place_section.Length != 2 )
			{
				// BUG BUG!
				// Log incorrect data -> the place does not conform to 
				// rg=[place]
				LogError("Region section provided does not conform to the rg=[place] format. ", partnerId);		

				//Redirect to Blank input page
				Response.Redirect(complete_url);
			}
			return place_section;
		}

		/// <summary>
		/// Write operational events to the log, logging ip address and Partner ID
		/// </summary>
		/// <param name="description"></param>
		/// <param name="partnerId"></param>
		private void LogError (string description, string partnerId)
		{
			OperationalEvent oe = 
				new  OperationalEvent(	TDEventCategory.Business,
				TDTraceLevel.Error,
				description +
				" Partner ID : " + partnerId + " client-ip : " 
				+ Page.Request.UserHostAddress.ToString() + " url-referrer : " 
				+ Page.Request.UrlReferrer);
			Logger.Write(oe);
		}

	}
}
