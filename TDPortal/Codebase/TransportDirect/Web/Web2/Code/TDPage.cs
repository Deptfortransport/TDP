//******************************************************************************
//NAME			: TDPage.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 01/07/2003
//DESCRIPTION	: A 'base page template' class derived from System.Web.UI.Page, 
//all other pages on a web site to derive from this new class. This provides a 
//single place where behaviour can be altered for all pages on the web site.
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/TDPage.cs-arc  $
//
//   Rev 1.25   Jan 04 2013 15:31:08   mmodi
//Allow logging of a partial postback page entry event
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.24   Dec 10 2010 12:02:32   apatel
//Code in onLoad event handler updated to take care of landing page urls with post data with new session
//Resolution for 5655: Page Landing Urls fails to plan the journey for the first time.
//
//   Rev 1.23   Nov 08 2010 08:49:52   apatel
//Updated to resolve the issue with the extended session time out where page was throwing error after user press go multiple times
//Resolution for 5633: Session time out value increases to unexpected value
//
//   Rev 1.22   Oct 29 2010 09:08:08   apatel
//updated to enable logged in user to have feature of extended session time out
//Resolution for 5625: Users not able to extend their session timeout
//
//   Rev 1.21   Sep 27 2010 16:08:36   apatel
//Updated to correctly handle the navigation tab click on session time out as at present it just post backs instead of navigate to main/mini home pages
//Resolution for 5610: Issue with clicking home page tab when session timed out
//
//   Rev 1.20   Sep 14 2010 12:47:12   apatel
//Updated to resovle the issue with the system error page displays when home tab clicked after landing page url response left to time out
//Resolution for 5604: System error when clicking home page tab
//
//   Rev 1.19   Apr 12 2010 15:48:26   mmodi
//Specific handling for FindMapResult session timeout
//Resolution for 5503: Maps : When maps timeout you can still zoom and pan the static tiles
//
//   Rev 1.18   Nov 17 2009 17:59:32   mmodi
//Update session time out transition to new map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.17   Apr 28 2009 12:04:48   jfrank
//Amended tag on which header detection is done.  So that when a session timeout occurs and the user clicks the tab the session timeout code is bypassed.
//Resolution for 5280: Session Timeout when using tabs
//
//   Rev 1.16   Oct 09 2008 11:20:14   mturner
//Update for XHTML compliance issue
//
//   Rev 1.15   Sep 02 2008 13:53:26   mmodi
//Reverted back to revision 1.12
//Resolution for 5104: Error page displayed
//
//   Rev 1.14   Jul 24 2008 13:40:08   mmodi
//Corrected LandingPage Get request issue following previous change
//Resolution for 5084: repeatingloop=Y being added to pages are sending 302 response code
//
//   Rev 1.13   Jul 23 2008 14:01:00   mmodi
//Removed repeatingloop code. No longer required because of cookies disabled handling having been changed since logic was introduced
//Resolution for 5084: repeatingloop=Y being added to pages are sending 302 response code
//
//   Rev 1.12   Jul 17 2008 11:17:36   mmodi
//Updated Navigation tab click session timeout logic
//Resolution for 5072: Session Timeout - Input page shown when Homepage tab selected
//
//   Rev 1.11   Jun 05 2008 11:17:48   jfrank
//Fixed bug for page landing case sensitivity.
//Resolution for 5015: Page landing from www.gettingabout.info not working
//
//   Rev 1.10   May 15 2008 18:17:02   mmodi
//Various session timeout bugs
//Resolution for 4958: Session Timeout: Find a train input page does not go to Cost input
//Resolution for 4963: Session Timeout - Server Error.
//Resolution for 4968: Session Timeout - Incorrect timeout message.
//Resolution for 4974: Session Timeout - No Timeout message on Find a Map.
//Resolution for 4976: Session Timeout - Welsh issue
//
//   Rev 1.9   May 14 2008 15:37:52   mmodi
//Updated for repeat visitor cookie handling
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.8   May 08 2008 16:44:06   mmodi
//Amended session timeout to detect Train input page search by icon click
//Resolution for 4958: Session Timeout: Find a train input page does not go to Cost input
//
//   Rev 1.7   May 06 2008 16:54:44   mmodi
//SB
//
//   Rev 1.6   May 01 2008 17:39:12   mmodi
//Updated session timeout handling
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.5   May 01 2008 11:16:46   mmodi
//Needed to re-check-in. Original check in did not work.
//Resolution for 4917: Get generic error page when cookies disabled. Need to show correct cookie error page.
//
//   Rev 1.4   Apr 08 2008 14:39:26   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// Rev DevFactory May 1 sbarker
// Corrected location to redirect to when a cookie error occurs.
//
// Rev DevFactory Apr 8 sbarker
// Additional method BlankPanelText to clear the text from panels.
//
//   Rev 1.3   Apr 02 2008 16:29:30   pscott
//SCR4627 - Error reported setting up multi-lingual resources
//
//due to wrongly comparing empty string with null in TDPage.cs
//
//   Rev 1.2   Mar 31 2008 13:18:50   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Mar 03 2008 13:03:00 apatel
//  Added overload method for configurelefthandmenu
//
//  Rev Devfactory Feb 25 2008 09:21:00 apatel
//  new property IsMenuExpandable added to make expandable menu expanding switch on and off.
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.2   Nov 29 2007 15:34:16   mturner
//Changes to remove compiler warnings caused by merge of Del 9.8
//
//   Rev 1.1   Nov 29 2007 11:41:24   build
//Updated for Del 9.8
//
//   Rev 1.75 devfactory Jan 31 2008 9:00 sbarker
//Added ConfigureLeftMenu method to aid with white labelling
//
//   Rev 1.74   Nov 07 2007 14:48:58   mmodi
//Corrected to apply to all landing pages
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.73   Sep 28 2007 09:28:12   pscott
//SCR4509  UK1390718
//error page for no cookies added
//
//   Rev 1.72   Sep 20 2007 10:47:52   mmodi
//Amended redirect logic when cookies are disabled 
//Resolution for 4501: Landing Page requests not working
//
//   Rev 1.71   Aug 17 2007 13:36:28   mmodi
//Changed tracelevel to Error
//Resolution for 4478: Portal shows Page cannot be displayed error when Cookies disabled
//
//   Rev 1.70   Aug 16 2007 18:16:56   mmodi
//Redirect to error page if unable to process a Post request
//Resolution for 4478: Portal shows Page cannot be displayed error when Cookies disabled
//
//   Rev 1.69   Mar 02 2007 11:00:54   jfrank
//Update to stop page landing with autoplan from word 2003 from timing out.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.68   Feb 28 2007 14:55:06   jfrank
//CCN0366 - Enhancement to enable Page Landing from Word 2003 and to ignore session timeouts for Page Landing due to future usage by National Trust.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.67   Jun 08 2006 13:47:44   rphilpott
//Handle postbacks within the StaticNoPrint page (PageId.Links) to support use of Login/Register control on static pages. 
//Resolution for 4101: Logon failure from static pages
//
//   Rev 1.66   Apr 21 2006 15:42:54   mtillett
//Fix handling of session cookies disabled
//Resolution for 3638: Apps: Handling of case when cookies not enabled.
//
//   Rev 1.65   Mar 28 2006 09:46:42   AViitanen
//Manual merge for Travel news (stream0024)
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.64   Mar 21 2006 17:06:24   jmcallister
//Prior change prevents Landing Pages from working in certain circumstances. Extra If condition introduced to ensure Landing Pages will now function correctly.
//
//   Rev 1.63   Mar 15 2006 17:07:32   RPhilpott
//Do not redirect to original URL if no cookies found in request, because this sets up an infinte loop between browser and server.
//Resolution for 3638: Handling of case when cookies not enabled.
//
//   Rev 1.62   Feb 23 2006 16:43:22   RWilby
//Merged stream3129
//
//   Rev 1.61   Feb 10 2006 15:04:36   build
//Automatically merged from branch for stream3180
//
//   Rev 1.60.1.4   Dec 05 2005 15:17:04   AViitanen
//Minor changes to DetectJavaScript and IsJavaScriptSettingKnown.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60.1.3   Nov 29 2005 17:26:04   AViitanen
//Changed pageTitle to be private. Amended DetectJavaScript method.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60.1.2   Nov 29 2005 17:07:10   AViitanen
//Added IsJavaScriptSettingKnown property and updated DetectJavaScript, so JavaScript is assumed to be enabled. 
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60.1.1   Nov 24 2005 15:45:40   RGriffith
//Change to initialisation of PageTitle
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60.1.0   Nov 24 2005 10:54:22   RGriffith
//Homepage phase 2 change to add PageTitle property
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60   Nov 18 2005 16:52:46   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface.
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.59   Nov 06 2005 16:16:14   rgreenwood
//Manual merge for stream2816 - replaced systemCache ResourceManager with GetResource() method
//
//   Rev 1.58   Nov 01 2005 09:15:38   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.57   Oct 11 2005 16:38:26   halkatib
//Added code previously on landing page to here in order to handle Post data that has been redirected to the page. 
//
//   Rev 1.56   Sep 26 2005 09:15:32   MTurner
//Resolution for IR 2776 - Inserted a Page.Response.Redirect to allow users to begin a session on any page within the portal.
//
//   Rev 1.55   Sep 02 2005 17:05:12   RWilby
//Converted IsReentrant  from a protected property to a public property.
//Resolution for 2687: DN018 - When map region is selected the date is re-set (IE 5.5 only?)
//
//   Rev 1.54   Aug 18 2005 11:30:00   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.53.1.0   Aug 03 2005 14:19:56   jgeorge
//Moved JavaScript detection to page load rather than submit to avoid problem with JavaScript postbacks bypassing this event.
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.53   May 26 2005 11:16:36   rgeraghty
//Check added for UserSurvey when detecting timeout
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.52   May 20 2005 09:34:52   rgeraghty
//Updated OnLoad to check for additional querystring value
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.51   Apr 22 2005 10:27:24   bflenk
//Checks that an existing session ID exists in the cookie or in the query parameters to ensure that TimeOut page is the destination from TicketRetailersHandOff & FindFareSelectionSingles after session expiry.
//
//   Rev 1.49   Mar 18 2005 15:20:00   COwczarek
//Move call to TDSessionManager.Current.OnPreUnload from
//OnPreRender to OnUnload so that deferred session data is
//saved if modified in screen flow or pre render event handler of
//a page.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.48   Mar 10 2005 12:47:28   jgeorge
//Added ScrollManager functionality
//
//   Rev 1.47   Mar 08 2005 16:35:14   bflenk
//Added TimeOut functionality - IR 1720.
//
//   Rev 1.46   Feb 25 2005 14:22:10   PNorell
//Performance fix.
//
//   Rev 1.45   Feb 18 2005 14:33:34   tmollart
//Updated so when FindFareInput page is requested and one use key has been specified the URL is appended to to force FindFareInput to use the correct cost based mode.
//
//   Rev 1.44   Feb 15 2005 11:46:02   COwczarek
//Add methods IsJavascriptEnabled and JavascriptDom
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.43   Feb 10 2005 09:23:48   COwczarek
//Changes to allow for multiple resource managers
//Resolution for 1921: DEL 7 Development : Find A Fare Ticket Selection
//
//   Rev 1.42   Nov 01 2004 15:27:52   passuied
//Changes in OnPreRender to enable addition of special queryString for pages FindStationInput and FindTrunkInput when OneUseKey NotFindAMode and ClassicMode are respectively used.
//
//   Rev 1.41   Oct 08 2004 12:27:56   jmorrissey
//SetUpPageLanguage method made virtual so that it can be overridden.
//
//   Rev 1.40   Jul 29 2004 11:15:26   passuied
//Added a static method in TDPage to close all calendar and help windows. Replaced local use.
//
//   Rev 1.39   Jul 23 2004 11:47:18   passuied
//Changes to add a GetResource method to TDPage and TDUserControl to ease resources access.
//Also removal of existing local methods
//
//   Rev 1.38   Jul 14 2004 16:40:04   rgreenwood
//IR1063 OnPreRender method now adds incremental cacheparam to each page URL for browser back button
//
//   Rev 1.37   Apr 30 2004 13:43:30   jbroome
//DEL 5.4 Merge
//JavaScript Controls
//
//   Rev 1.36   Mar 29 2004 17:23:36   PNorell
//Fix for IR683
//
//   Rev 1.35   Nov 03 2003 15:43:06   passuied
//implemented anchorage for help and input page
//
//   Rev 1.34   Nov 03 2003 13:30:14   PNorell
//Added initial support for scrolling down on a given page.
//
//   Rev 1.33   Oct 16 2003 13:21:16   passuied
//Added get Property for PageId
//
//   Rev 1.32   Oct 08 2003 19:47:54   PNorell
//Updated for the once-logging for pages that only wants to register once even though multiple tries has been accessed.
//
//   Rev 1.31   Oct 08 2003 18:54:20   PNorell
//Corrected the PageEntryEvent logging occur.
//
//   Rev 1.30   Oct 03 2003 12:47:14   JMorrissey
//Included commented out error handling code
//
//   Rev 1.29   Oct 03 2003 10:12:20   PNorell
//Ensured the logging works properly.
//
//   Rev 1.28   Sep 30 2003 11:32:46   PNorell
//Corrected printable outlook.
//Added support for moving outside the screen flow state as is needed by some printable pages.
//
//   Rev 1.27   Sep 26 2003 18:39:52   RPhilpott
//Add "ForceRedirect" flag check
//
//   Rev 1.26   Sep 26 2003 16:59:52   JMorrissey
//Removed a line of commented out test code
//
//   Rev 1.25   Sep 23 2003 10:21:28   asinclair
//Updated the code for creating urls depending upon the lang channel of the user
//
//   Rev 1.24   Sep 22 2003 16:16:42   PNorell
//Ensured the base href is included in the links when it is a posting.
//
//   Rev 1.23   Sep 19 2003 19:57:54   PNorell
//Updated all journey details screens.
//Support for Adjusted journeys added and Validate And Run.
//
//   Rev 1.22   Sep 18 2003 18:20:40   PNorell
//Restored life-cycle events.
//
//   Rev 1.21   Sep 18 2003 11:03:42   PNorell
//Updated SessionManager lifecycle
//
//   Rev 1.20   Sep 18 2003 09:56:40   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.19   Sep 16 2003 17:08:00   PNorell
//Commented out temporarily for people to run the web project.
//
//   Rev 1.18   Sep 16 2003 14:31:06   jcotton
//Uncommented OnLoad Event
//
//   Rev 1.17   Sep 16 2003 14:06:48   jcotton
//1.	Added to the PageID enumeration the following PageIDs:
//·	InitialPage
//·	JourneyPlannerInput
//·	JourneyPlannerAmbiguity
//·	JourneySummary
//·	JourneyPlannerLocationMap
//·	JourneyDetails
//·	JourneyMaps
//·	JourneyAdjust
//·	CompareAdjustedJourney
//·	DetailedLegMap
//·	WaitPage
//·	PrintableJourneySummary
//·	PrintableJourneyDetails
//·	PrintableJourneyMaps
//·	PrintableCompareAdjustedJourney
//·	Feedback
//·	Links
//·	Help
//·	GeneralMaps
//·	SiteMap
//2.	Updated PageTransferDetails.xml and PageTransferEvents.xml.
//3.	Corrected "Test Pages" that referenced PageID enumerations no longer used.
//
//   Rev 1.16   Sep 10 2003 10:41:54   kcheung
//Added base() in constructor
//
//   Rev 1.15   Sep 09 2003 15:12:00   jcotton
//Added Page activity logging to Onload. (OnLoad & PreRender Commented out waiting integration)
//
//   Rev 1.14   Sep 03 2003 12:01:52   JMorrissey
//Updated SessionChannelName property so that it returns channel.URL rather than channel.Path
//
//   Rev 1.13   Aug 06 2003 10:16:12   hahad
//Added commented out code that toggles between English and Welsh
//
//   Rev 1.12   Aug 06 2003 10:08:34   hahad
//No Changes
//
//   Rev 1.11   Jul 25 2003 17:10:42   kcheung
//Updated ScreenFlow stuff.
//
//   Rev 1.10   Jul 24 2003 15:15:22   kcheung
//Commented Onprender and onload.
//
//   Rev 1.9   Jul 23 2003 17:50:52   kcheung
//Added ScreenFlow OnLoad and OnPreRender methods.
//
//   Rev 1.8   Jul 18 2003 16:25:12   JMorrissey
//Updated code so new class of LanguageHandler is used instead of Global.LanguageManager where appropriate
//
//   Rev 1.7   Jul 11 2003 14:54:16   JMorrissey
//Small change to SetUpPageLanguage routine
//
//   Rev 1.6   Jul 11 2003 13:58:04   JMorrissey
//Updated LanguageHandler.SetControlsText to use new method name of SetTextOnControls 
//
//   Rev 1.5   Jul 09 2003 15:26:06   JMorrissey
//Updated header log info


using Logger = System.Diagnostics.Trace;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Code;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Web.Support;

using CmsMockObject.Objects;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Base class for all web TDP pages.
	/// </summary>
	public class TDPage : System.Web.UI.Page
	{
        #region Private Constants

        private const string requestLanguageQueryStringKey = "CurrentLanguage";
        private const string sessionErrorKey = "SessionError";
        private const string plannerModeKey = "PlannerMode";

        private const string plannerModeVisitPlanner = "VisitPlanner";
        private const string plannerModeDoorToDoor = "DoorToDoor";
        private const string plannerModeNone = "None";

        private const string abandonKey = "abandon";
        private const string abandonQueryString = "abandon=true";
        
        #endregion

        #region Variables

        /// <summary>
        /// This is required by white labelling to allow language switches
        /// based on query strings
        /// </summary>
        private NameValueCollection persistedQueryString = null;

		// All sub-classes must have this value set.  It indicates the unique Id of
		// the page.
        protected PageId pageId = PageId.Empty;
        protected PageId pageIdPostback = PageId.Empty;

		// New property for setting the HTML Title of the page
		private string pageTitle;
		
		// Reference to the ScriptRepository. This will be retrieved from ServiceDiscovery
		private static ScriptRepository.ScriptRepository scriptRepository;
		// Name of the session parameter to hold the JavaScript support field
		protected const string JAVASCRIPT_SUPPORT = "JavaScriptSupport";
		// Name of the session parameter to hold the JavaScript DOM type field
		protected const string JAVASCRIPT_DOM = "JavaScriptDOM";
		// Name of the session parameter to hold JavaScript set value
		protected const string JAVASCRIPT_SET = "JavaScriptSet";
		// The hidden fields we place on all pages to capture JavaScript support and DOM type
		// of the browser.
		// Note: the values of these fields should match that in the JavaScript detection script
		protected const string JAVASCRIPT_SUPPORT_FIELD ="hdnTest";
		protected const string JAVASCRIPT_DOM_FIELD = "hdnDOMStyle";
		//Querystring parameter that allows JavaScript setting to be indicated by user
		protected const string JAVASCRIPT_SUPPORT_SET = "hdnSet";
		// this field is used to store resources to be used in the javascript
		protected Hashtable scriptResources = new Hashtable();

		private readonly ScrollManager scrollManager;

		/// <summary>
		/// The resource manager for this page
		/// </summary>
		protected TDResourceManager resourceManager;

        // Process cookies to detect and raise visitor events
        protected CookieHelper cookieHelper = null;

		//arrays used to hold Languages in use in TDP  	
		private static string[] Languages = new string[2] {"en-GB", "cy-GB"};

		//arrays used to hold CMS language channel indicators   
		private static string[] ChannelIndicator = new string[2] {"/en/","/cy/"};

        // CCN 0427 determines if page is static, by default its value is false and it get set up in 
        // the LoadDatabaseContentIfNecessary method
        private bool isStaticPage = false;

		/// <summary>
		/// Sets the local resource manager for the page (but not nested controls). 
		/// Setting this property means that resource strings will be obtained from the local
		/// resource manager and not the global instance (langstrings).
		/// </summary>
		public string LocalResourceManager 
		{
			set 
			{
				resourceManager = TDResourceManager.GetResourceManagerFromCache(value);
			}
		}

		public ScrollManager ScrollManager
		{
			get { return scrollManager; }
		}

		/// <summary>
		/// constructor
		/// </summary>
        public TDPage() : base()
		{
            //The following code ensures that the 
            //default resource manager (globally available)
            //is up, running and ready to go. Note that
            //it is only instantiated if null, so overhead
            //created by this call is minimal:
            Global.InstantiateResourceManagerIfRequired();
            
            //This code used to be fired in the field
            //declaration. It is now here, since it is
            //important that the previous line fires first:
            resourceManager = Global.tdResourceManager;
            
            //Set up the scroll manager as usual:
			scrollManager = new ScrollManager(this);
		}

		/// <summary>
		/// Calls the EmitScript method of the ScrollManager to add necessary
		/// javascript into the output and als
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			scrollManager.EmitScript();
			base.Render(writer);
		}

		private void getUrlTransitionEvent() 
		{
			string te = (string)Request.QueryString["te"];
			TransitionEvent transitionEvent;
			if (te != null) 
			{
				transitionEvent = (TransitionEvent)Enum.Parse(TransitionEvent.Empty.GetType(),te);
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = transitionEvent;
			}
		}

		private bool isUrlTransitionEvent() 
		{
			string te = (string)Request.QueryString["te"];
			if (te != null) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// English culture string. This is a read only property.
		/// </summary>				
		public static string EnglishLanguage
		{
			get
			{
				return "en-GB";
			}
			
		}

		/// <summary>
		/// Welsh culture string. This is a read only property.
		/// </summary>		
		public static string WelshLanguage
		{
			get
			{
				return "cy-GB";
			}
			
		}

		/// <summary>
		/// String used to establish if channel is Welsh.
		/// This is a read only property.
		/// </summary>		
		private static string welshChannelIndicator = "/cy/";
		public static string WelshChannelIndicator
		{
			get
			{
				return welshChannelIndicator;
				
			}
			
		}		

		/// <summary>
		/// String used to establish if channel is English.
		/// This is a read only property.
		/// </summary>		
		private static string englishChannelIndicator = "/en/";
		public static string EnglishChannelIndicator
		{
			get
			{
				return englishChannelIndicator;				
			}			
		}		

		public string BuildTransitionEventUrl(TransitionEvent transitionEvent) 
		{
			//return SessionChannelFullPath + "?te=" + transitionEvent.ToString();
			return SessionChannelItemPath + "?te=" + transitionEvent.ToString();
        }

        #endregion

        #region OnPreRender
        /// <summary>
		/// Overrides the base OnPreRender method.  Calls GetNextPageId to find
		/// the Id of the next page to be loaded.  If the returned Id is differs
		/// from the current PageId, a Redirect to the next page is performed.
		/// However, if the returned Id is the same as the current PageId, then
		/// the base OnPreRender is called to continue loading.
		/// </summary>
		override protected void OnPreRender(EventArgs e)
		{
            //See if there is language information in the query string:
            ExtractLanguageFromQueryStringAndRedirectIfRequired();

            //We must perform our control looping code here to ensure that
            //all controls are given a value:

            if (!isStaticPage)
            {
                PageControlPropertyPopulator.Populate(this);
            }

			TDSessionManager.Current.OnFormShift();
			// NOTE: Get PageController in this method rather than have a private
			// global otherwise when the page is created in Visual Studio .Net
			// it will cause an exception because Service Discovery will not
			// have been initialised yet!

			if( !TDSessionManager.Current.FormShift[ SessionKey.SkipScreenFlow ] )
			{
                // Add the mode to viewstate/cookie for use in Session Timeout
                AddModeToViewState();
                
				IPageController pageController =
					(PageController)TDServiceDiscovery.Current
					[ServiceDiscoveryKey.PageController];
			

				getUrlTransitionEvent();

				PageId nextPageId = pageController.GetNextPageId(pageId);

				// If the "ForceRedirect" flag is set, do a redirect even if the new page-id is 
				//  the same, to force a new Page_Load event after the Event handler has fired.

				if(nextPageId != pageId || TDSessionManager.Current.FormShift[SessionKey.ForceRedirect])
				{
					// Returned Page-id differs from the current value, so
					// call GetPageTransferDetails to get the corresponding Url
					// for the Response.Redirect.

					PageTransferDetails pageTransferDetails =
						pageController.GetPageTransferDetails(nextPageId);

					string url = "";

					// PageId of PageId.Links is a special case, because it represents "StaticNoPrint.aspx". 
					// This is used by a number of different CMS templates, so we cannot redirect to it 
					// directly. However, we should never be going *to* these pages via a PostBack, so   
					// we can assume that any post to these has come from the same page and so we can
					// use the CMS URL of the current page instead. 
					
					if	(nextPageId == PageId.Links)
					{
						url = CmsHttpContext.Current.ChannelItem.Url.ToString();
					}
					else
					{
						// Perform the response redirect
						url = pageTransferDetails.PageUrl;
						
						if (TDPage.SessionChannelName !=  null )
						{
							url = getBaseChannelURL(TDPage.SessionChannelName) + url; 
						}

						// IR1063 - Add cacheParam for Browser Back button functionality

						if (url.IndexOf("?") > 0)
						{
							url += "&" + "cacheparam=" + TDSessionManager.Current.CacheParam;					
							TDSessionManager.Current.CacheParam = TDSessionManager.Current.CacheParam + 1;
						}
						else 
						{
							url += "?" + "cacheparam=" + TDSessionManager.Current.CacheParam;					
							TDSessionManager.Current.CacheParam = TDSessionManager.Current.CacheParam + 1;
						}
                        
						// Add anchor #<id> here
						if( TDSessionManager.Current.Session[SessionKey.Anchor] != null )
						{
							string anch = TDSessionManager.Current.Session[SessionKey.Anchor];
							url += "#"+anch;
							
						}

						// Add querystring to FindStationInput url in case OneUseKey NotFindAMode is used
						string notFindAMode = TDSessionManager.Current.GetOneUseKey(SessionKey.NotFindAMode) as string;
						if (nextPageId == PageId.FindStationInput)
						{
							if (notFindAMode != null)
							{
								url += "?NotFindAMode=true";
							}
						}

						// Add querystring to FindTrunkInput url in case OneUseKey ClassicMode is used
						string classicMode = TDSessionManager.Current.GetOneUseKey(SessionKey.ClassicMode) as string;
						if (nextPageId == PageId.FindTrunkInput)
						{
							if (classicMode != null)
							{
								url += "?ClassicMode=true";
							}
						}

						//Add query string to FindFareInput url to determine correct mode. Check if OneUseKey
						//is in use.
						string findModeTrunkCostBased = TDSessionManager.Current.GetOneUseKey(SessionKey.FindModeTrunkCostBased) as string;
						if (nextPageId == PageId.FindFareInput)
						{
							if (findModeTrunkCostBased != null)
							{
								url += "?FindMode=TrunkCostBased";
							}
						}
					}
					
					if ( url.IndexOf("WaitPage") >= 0 )
					{
						url += "&SID=" + TDSessionManager.Current.Session.SessionID;
					}

                    // Finally remove the abandon from query string as no longer needed.
                    // Ensures we detect future timeouts correctly
                    if (url.IndexOf(abandonKey) >= 0)
                    {
                        url = url.Replace("abandon=true", string.Empty);
                    }

					Response.Redirect(url);

				}
				// otherwise, just continue the OnPreRender
			}

			if( !this.IsPostBack && !IsReentrant )
			{
				// Log the page activity to the TD Page Entry Event.
				PageEntryEvent logPage = new PageEntryEvent( pageId, Session.SessionID, TDSessionManager.Current.Authenticated );
				Logger.Write(logPage);
			}
            else if (this.IsPostBack && IsPartialPostback)
            {
                // Log page entry to indicate this is a partial postback (AJAX)
                if (pageIdPostback != PageId.Empty)
                {
                    PageEntryEvent logPage = new PageEntryEvent(pageIdPostback, Session.SessionID, TDSessionManager.Current.Authenticated);
                    Logger.Write(logPage);
                }
            }

			//add resources if they are needed for javascript translations
			if (scriptResources.Count > 0)
				AddResourcesToClientScript();

            // Process the cookie, which raises Visitor events
            cookieHelper.ProcessPersistentCookie();


            base.OnPreRender(e);
		}


		private bool reentrant = false;
		/// <summary>
		/// Get/Set property for deciding if this page view is reentrying the same page _even_ though
		/// it has been requested without postback.
		/// </summary>
		public bool IsReentrant
		{
			get { return reentrant; }
			set { reentrant = value; }
        }

        private bool partialPostback = false;
        /// <summary>
        /// Get/Set property for deciding if this page is a partial postback (e.g. AJAX postback)
        /// </summary>
        public bool IsPartialPostback
        {
            get { return partialPostback; }
            set { partialPostback = value; }
        }

        #endregion

        #region OnUnload, OnLoad, OnInit
        /// <summary>
		/// Overrides the base OnUnload method.
		/// 
		/// Handles the life-cycle of the session manager.
		/// </summary>
		override protected void OnUnload(EventArgs e)
		{
			TDSessionManager.Current.OnPreUnload();
			TDSessionManager.Current.OnUnload();
			base.OnUnload(e);
		}


		/// <summary>
		/// Overrides the base OnLoad method.  
		/// 
		/// 1. Calls ValidatePageTransition of the PageController to check if the 
		/// current page can be displayed. If the current page cannot be displayed 
		/// then a Response Redirect is performed, otherwise the base OnLoad is 
		/// called to continue loading.
		/// 
		/// 2. Calls the ReportDataProvider.TDPCustomEvents page logging service
		/// to save page activity data for management reporting.  
		/// 
		/// 3. Performs JavaScript support detection.
		/// </summary>
		override protected void OnLoad(EventArgs e)
		{
            persistedQueryString = this.Request.QueryString;

            // Check if there are any errors in the Session we need to transfer in to the TDSessionManager.
            // There should only be session timeout errors added
            ProcessSessionErrors();

            // Move javascript detection earlier, so that we know prior to session timeout check occuring
            scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            // Determine if JavaScript is supported
            DetectJavaScript();

            if( !TDSessionManager.Current.FormShift[ SessionKey.SkipScreenFlow ] )
			{
				IPageController pageController =
					(PageController)TDServiceDiscovery.Current
					[ServiceDiscoveryKey.PageController];

                #region Timeout test, and landing page handling		
				if( Session.IsNewSession && Request.QueryString[abandonKey]== null)
				{
					//Note: the "abandon" key is only set when a user has clicked the 'go back to homepage' link
					//on the error or timeout page i.e. after a session expiry has already occurred

					// If it says it is a new session, but an existing session ID exists in 
					// the cookie or in the query parameters then it must have timed out
					// (can't use the cookie collection because even on first request it
					// already contains the cookie (request and response seem to share the collection)
					string cookieHeader = Request.Headers["Cookie"];
					string queryStringSessionId = Request.QueryString.Get("ASP.NET_SessionId");
                    string query_string = string.Empty;

					if ( (((queryStringSessionId != null) && (queryStringSessionId.Length > 0))
						  || ((cookieHeader != null) && (cookieHeader.IndexOf("ASP.NET_SessionId") >= 0)) 
						  //JP landing should not be allowed to time out
						  && PageId != PageId.JPLandingPage)
						 //If JPlanding from Word 2003 with autoplan, don't time out
						 && !(PageId == PageId.WaitPage && this.Request.QueryString["SID"] != null 
						      && TDSessionManager.Current.Session.SessionID != this.Request.QueryString["SID"])
						)
					{
                        ProcessSessionTimeout();						
					}
					else
                    {
                        #region Landing Page
                        //changes required for Landing page.
                        if (Request.HttpMethod == "POST")
                        {
                            // Note: I do think we can't use query string to find out what we need
                            // If this is the case we would need to read from the input stream from
                            // the request. Number of chars to read would be defined in Request.Totalbytes
                            int size = Request.TotalBytes;
                            byte[] buff = new byte[size];
                            try
                            {
                                Request.InputStream.Read(buff, 0, size);
                                query_string = System.Text.ASCIIEncoding.ASCII.GetString(buff, 0, size);
                                if (Page.Request.RawUrl.IndexOf("?") >= 0)
                                {
                                    Response.Redirect(Page.Request.RawUrl + "&" + query_string + "&repeatingloop=Y", false);
                                }
                                else
                                {
                                    Response.Redirect(Page.Request.RawUrl  + query_string + "&repeatingloop=Y", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                // If we reach this point, we have two possible scenarios 
                                // 1) its a landing page request being rejected first time, so just ignore and carry on
                                // or 2) the  browser has cookies disabled, so the Portal will not work, therefore redirect to Error page

                                // Determine if this is a landing page request
                                string requestURL = Page.Request.RawUrl.ToString();
                                if (requestURL.ToLower().IndexOf("landingpage") >= 0)
                                {	// Landing page
                                    #region Landing page error
                                    // Log a warning
                                    OperationalEvent oe =
                                        new OperationalEvent(
                                            TDEventCategory.Business, TDTraceLevel.Warning,
                                            "Received a LandingPage request using method POST that aborts before full stream can be read"
                                            + ". client-ip : " + Page.Request.UserHostAddress
                                            + ". url-referrer : " + Page.Request.UrlReferrer
                                            + ". url-request: " + Page.Request.RawUrl
                                            + ". exception : " + ex.Message
                                        );

                                    Logger.Write(oe);
                                    #endregion
                                }
                                else
                                {	// Cookies disabled, and all other scenarios
                                    #region Cookies error
                                    // Log an error
                                    OperationalEvent oe =
                                        new OperationalEvent(
                                            TDEventCategory.Business, TDTraceLevel.Error,
                                            "Received a page request using method POST that aborts before full stream can be read. Possible Cookies disabled scenario detected"
                                            + ". client-ip : " + Page.Request.UserHostAddress
                                            + ". url-referrer : " + Page.Request.UrlReferrer
                                            + ". url-request: " + Page.Request.RawUrl
                                            + ". exception : " + ex.Message
                                        );

                                    Logger.Write(oe);
                                    #endregion

                                    Response.Redirect("/web2/ErrorPageCookies.aspx?abandon=true?", true);
                                }

                                //throw;
                            }
                        }
                        
                        #endregion

                        //handle GET requests
                        string queryRepeatingLoop = Request.QueryString.Get("repeatingloop");
                        if (queryRepeatingLoop != null && queryRepeatingLoop.Length > 0)
                        {
                            //do not redirect continuously
                        }
                        else
                        {
                            if (Page.Request.RawUrl.IndexOf("?") >= 0)
                            {
                                Response.Redirect(Page.Request.RawUrl + "&" + query_string + "&repeatingloop=Y", true);
                            }
                            else
                            {
                                Response.Redirect(Page.Request.RawUrl + "?" + query_string + "&repeatingloop=Y", true);
                            }
                        }

                        
                    }
                }
                #endregion

                #region Session Timeout - extended/default
                // Set the session time out - Implemented to enable longer extended sessions for 
                // login page and authenticated page
                if (TDSessionManager.Current.Authenticated)
                {
                    TDUser user = TDSessionManager.Current.CurrentUser;

                    bool sessionExtended = false;
                    if (user.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value != null)
                    {
                        sessionExtended = Convert.ToBoolean(user.UserProfile.Properties[ProfileKeys.EXTENDED_SESSION].Value);
                    }

                    if (sessionExtended)
                    {
                        if (Session.Timeout != GetDefaultSessionTimeOut() * 10)
                        {
                            Session.Timeout = GetDefaultSessionTimeOut() * 10;
                        }
                    }
                    else // set time out to the default value specified in the web config
                    {
                        Session.Timeout = GetDefaultSessionTimeOut();
                    }
                }
                else
                {
                    Session.Timeout = GetDefaultSessionTimeOut();
                }
                #endregion

                // If current url has #<anchor> it is presumed reentrant
				// if( this.Request.RawUrl.LastIndexOf("#") != -1)
				// if( this.Request.Params["plocid"] != null )
				if( TDSessionManager.Current.Session[SessionKey.Anchor] != null )
				{
					// Form shift contains the anchor used for this page request only
					TDSessionManager.Current.FormShift[SessionKey.Anchor] = TDSessionManager.Current.Session[SessionKey.Anchor];
					// The session should be cleared as soon as we know it is reentrant.
					TDSessionManager.Current.Session[SessionKey.Anchor] = null;
					IsReentrant = true;
				}

				PageId nextPageId = pageController.ValidatePageTransition(pageId);

				if(nextPageId != pageId)
				{
					// Returned PageId differs from the current PageId.
					// Perform a redirect to the returned pageId.

					// Get the PageTransferDetails object to get the Url
					PageTransferDetails pageTransferDetails =
						pageController.GetPageTransferDetails(nextPageId);

					string url = pageTransferDetails.PageUrl;
					if (TDPage.SessionChannelName !=  null )
					{
						url = getBaseChannelURL(TDPage.SessionChannelName) + url;
					}

                    // This is an illegal state so redirect to closest best page and end the response.
					Response.Redirect(url,true);
				}
			

				// reset the "expected next page id" in the session to empty
				TDSessionManager.Current.Session[SessionKey.NextPageId] = PageId.Empty;
			}

            // Determine if Cookies are supported (used for Repeat visitor tracking)
            cookieHelper = new CookieHelper(pageId);
            cookieHelper.DetectCookies();

			// otherwise, just continue the OnPreRender
			base.OnLoad(e);
		}
				
		/// <summary>
		/// Overrides the base OnInit method. Calls private method SetUpPageLanguage, 
		/// which retrieves language resources for the page, then calls base OnInit method
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{	
			// Onload the session manager
			TDSessionManager.Current.OnLoad();

            PerformSiteRedirect();

			//set up language resources  
			this.SetUpPageLanguage(e);

			//call base class OnInit method
			base.OnInit(e);				

			// Default value set to the "JourneyPlanner.DefaultPageTitle" value in langstrings.resx
			pageTitle = GetResource("JourneyPlanner.DefaultPageTitle");
        }
        #endregion

        #region Menus
        /// <summary>
        /// Configures left hand menu - overloaded method used when client link is not on page
        /// </summary>
        /// <param name="expandableMenuControl"></param>
        /// <param name="context"></param>
        protected void ConfigureLeftMenu(ExpandableMenuControl expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context context)
        {
            ConfigureLeftMenu(null, null, null, expandableMenuControl, context);
        }

        protected void ConfigureLeftMenu(string bookmarkTitle, string linkText, ClientLinkControl clientLink, ExpandableMenuControl expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context context)
        {
            // Information column set up
            expandableMenuControl.AddContext(context);

            if (bookmarkTitle == "ClientLinks.DoorToDoor.LinkText" && linkText == "")
            {
                if (clientLink != null)
                {
                    ResultsAdapter resultsAdapter = new ResultsAdapter();
                    resultsAdapter.PopulateJourneyResultsClientLink(clientLink);
                }
            }
            else
            {
               

                // Determine url to save as bookmark
                IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

                string baseChannel = string.Empty;

                if (TDPage.SessionChannelName != null)
                {
                    baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
                }

                PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(pageId);

                string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                string url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;

                if (clientLink != null)
                {
                    // Set up client link for bookmark on expandable menu
                    clientLink.BookmarkTitle = GetResource(bookmarkTitle);
                    clientLink.LinkText = GetResource(linkText);
                    clientLink.BookmarkUrl = url;
                }
                
            }
        }
        #endregion

        #region Language
        /// <summary>
		/// checks current channel in MCMS before setting the 
		/// correct language and resources to use on web page 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		public virtual void SetUpPageLanguage(System.EventArgs eventArgs)
		{	
			try 
			{
				//set pageLanguage to the CurrentUICulture setting	
				string pageLanguage = Thread.CurrentThread.CurrentUICulture.ToString();				
										
				// If the channel is null, assume not using Content Management Server			
				if (TDPage.SessionChannelName.ToString().Length !=  0 )

				{	//get ISO language code for this channel
					pageLanguage = GetChannelLanguage(TDPage.SessionChannelName.ToString());					
				}
				
				//set the correct language cultures this page	
				LanguageHandler.SetThreadLanguageCulture(pageLanguage);
				//load correct language strings for page based on language culture			
				LanguageHandler.SetTextOnControls(this, resourceManager);
			}	
			
				//catch any exceptions
			catch
			{				
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Error, 
					"An error has occurred setting up multi-lingual resources.");
				Logger.Write(oe);		
				
				// in the event of an unhandled exception, set the page language to the 
				//default of English
				LanguageHandler.SetThreadLanguageCulture(Global.TDLang);
				// Load up correct language strings for web page			
				LanguageHandler.SetTextOnControls(this, resourceManager);							
			}
        }
        #endregion

        #region Channel
        /// <summary>
		/// Returns language root for current CMS channel URL (i.e. without the
		/// "/Channels/" bit at the start) as a string. This is a read only property.
		/// If the page is not under CMS, this will return null.
		/// </summary>		
		public static string SessionChannelName
		{
			get
			{				
				Channel channel = SessionChannel();
				if (channel != null) 
				{
					return channel.Url;
				}   
				else 
				{
					return null;
				}
				
			}			
		}

		public static string SessionChannelFullPath
		{
			get
			{				
				Channel channel = SessionChannel();
				if (channel != null) 
				{
					return channel.Path + channel.Name;
				}   
				else 
				{
					return null;
				}
				
			}			
		}
		
		public static string SessionChannelItemPath
		{
			get
			{				
				if( CmsHttpContext.Current.Posting != null) 
				{
					ChannelItem item = CmsHttpContext.Current.ChannelItem;
					Channel channel = SessionChannel();
					if (item != null) 
					{
						return channel.Url + item.Name;
					} 
					else 
					{
						return null;
					}
                        
				}
				else 
				{
					return null;
				}
				
			}			
		}

		/// <summary>
		/// If a web page is a CMS posting,this will return the current CMS channel,
		/// otherwise it returns null.
		/// </summary>
		public static Channel SessionChannel ()
		{
			try
			{
				if( CmsHttpContext.Current.Posting != null) 
				{
					return CmsHttpContext.Current.Channel;		
				}
				else
				{
					return null;
				}
			}
				//this will catch an error if the current thread is not 
				//processing a HTTP request
			catch (System.InvalidOperationException)
			{						
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Error, 
					"A System.InvalidOperationException has occurred when the TDPage.SessionChannel method was called.");
				Logger.Write(oe);				
				return null;
			}
        }
        #endregion

        #region Javascript
        /// <summary>
		/// This method checks whether the user has indicated that JavaScript is enabled. The default 
		/// setting is true. It places a block of JavaScript in all pages and then checks to see if and 
		/// how the JavaScript has updated a hidden field in subsequent requests. This is then used 
		/// as an indication of the browser's JavaScript support. 
		/// IsJavaScriptSettingKnown is stored in the session and cannot be reset.
		/// </summary>
		private void DetectJavaScript()
		{			
			// Check whether JavaScript indication is detected
			if (Request.Params[JAVASCRIPT_SUPPORT_SET] != null && Session[JAVASCRIPT_SET] == null)
			{
				Session[JAVASCRIPT_SUPPORT] = Request.Params[JAVASCRIPT_SUPPORT_SET];
				Session.Add(JAVASCRIPT_SUPPORT_SET, "true");
				Session.Add(JAVASCRIPT_SET, "true");
			}
			
			// Check the latest request from the client to determine if JavaScript is enabled
			if (Request.Params[JAVASCRIPT_SUPPORT_FIELD] != null)
			{
				Session.Add(JAVASCRIPT_SUPPORT, Request.Params[JAVASCRIPT_SUPPORT_FIELD]);		
				Session.Add(JAVASCRIPT_DOM, Request.Params[JAVASCRIPT_DOM_FIELD]);	
				Session.Add(JAVASCRIPT_SUPPORT_SET, "true");
				Session.Add(JAVASCRIPT_SET, "true");
			}

			// By default, javascript is enabled
			else if (Session[JAVASCRIPT_SUPPORT] == null) 
			{
				Session.Add(JAVASCRIPT_SUPPORT, "true");
				Session.Add(JAVASCRIPT_DOM, "");
			}
			
			// Place the JavaScript detection script in the page to check its presence next time around
            Page.ClientScript.RegisterClientScriptBlock(typeof(TDPage), scriptRepository.DetectionScriptName, scriptRepository.DetectionScript);
            Page.ClientScript.RegisterStartupScript(typeof(TDPage), scriptRepository.DetectionScriptName, string.Format("<script language=\"javascript\" type=\"text/javascript\" >{0}</script>", scriptRepository.DetectionScriptAction));
            Page.ClientScript.RegisterHiddenField(JAVASCRIPT_SUPPORT_FIELD, "false");
            Page.ClientScript.RegisterHiddenField(JAVASCRIPT_DOM_FIELD, "");
        }
        #endregion

        #region Public properties
        /// <summary>
		/// Get Property. returns the name of the page.
		/// </summary>
		public PageId PageId
		{
			get
			{
				return pageId;
			}
		}

        /// <summary>
        /// Get/Set Property. Page Id for a patial postback (AJAX), used to log a page entry event for tracking.
        /// The IsPartialPostback property must be set to ensure the event is logged
        /// </summary>
        public PageId PageIdPostback
        {
            get { return pageIdPostback; }
            set { pageIdPostback = value; }
        }

		/// <summary>
		/// Get/Set Property. Returns/Sets the HTML title for the page.
		/// </summary>
		public string PageTitle
		{
			get
			{
				return pageTitle;
			}
			set
			{
				pageTitle = value;
			}
		}
		
		/// <summary>
		/// Get property that returns true if Javascript is enabled on the client browser
		/// or false otherwise
		/// </summary>
		public bool IsJavascriptEnabled
		{
			get 
			{
				return bool.Parse((string) Session[JAVASCRIPT_SUPPORT]);
			}
		}

		/// <summary>
		/// Get property that returns the name of DOM used by the client browser
		/// </summary>
		public string JavascriptDom
		{
			get
			{
				return (string)Session[JAVASCRIPT_DOM];
			}
		}	    
    
		/// <summary>
		/// Property for determining if JavaScript setting has been indicated by client.
		/// Returns whether supported, false by default
		/// </summary>
		public bool IsJavaScriptSettingKnown
		{			
			get 
			{
				if (Session[JAVASCRIPT_SUPPORT_SET] != null)
				{
					return true;
				}
				else 
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Get Property. Returns the constant used for Javascript support session varible
		/// </summary>
		public string Javascript_Support
		{
			get
			{
				return JAVASCRIPT_SUPPORT;
			}
		}

		/// <summary>
		/// Get Property. Returns the constant used for Javascript Dom session varible
		/// </summary>
		public string Javascript_Dom
		{
			get
			{
				return JAVASCRIPT_DOM;
			}
		}

        public bool IsMenuExpandable
        {
            get
            {
                string property = Properties.Current["LeftHandMenuExpandable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }
        #endregion

        #region Public methods
        /// <summary>
		/// Method that returns the resource associated with the given key
		/// using the Current UI Culture. This method will first attempt to get the 
		/// resource from the local resource manager if one has been specified.
		/// </summary>
		/// <param name="key">resource key</param>
		/// <returns>resource string</returns>
		public string GetResource(string key)
		{
//            string groupName = PageNameParser.GetPageNameFromPageToString(this);
//            return ContentProvider.Instance["General"].GetControlProperties().GetPropertyValue("langStrings", key);
            return resourceManager.GetString(key);
		}

		/// <summary>
		/// Method that returns the resource associated with the given key from 
		/// the specified non-default resource manager. 
		/// </summary>
		/// <param name="key">resource key</param>
		/// <param name="resourceFileName">name of resource file</param>
		/// <returns>resource string</returns>
		public string GetResource(string resourceFileName, string key)
		{
			return resourceManager.GetString (resourceFileName, key);
		}

		/// <summary>
		/// Static Method. Looks recursively for all ISingleWindow control
		/// within the root control and close them.
		/// </summary>
		/// <param name="root"></param>
		static public void CloseAllSingleWindows(System.Web.UI.Control root)
		{
			//hide any other single window currently visible
			foreach (System.Web.UI.Control c in root.Controls)			
			{		
				ISingleWindow isw = c as ISingleWindow;
				if( isw != null)
				{
					isw.Close();
				}
				//loop through any child controls contained within each control
				if (c.Controls.Count > 0)
				{
					CloseAllSingleWindows(c);
				}
				
			}
		}

		/// <summary>
		/// Takes a string representing the channel path e.g. channel path "web\cy\default"
		/// and returns the RFC 1766 language code for this channel e.g. "cy-GB" 
		/// </summary>
		/// <param name="channelName"></param>
		/// <returns>string</returns>
		public static string GetChannelLanguage(string channelName)
		{
			
			string channelLanguage = null;			
					
			//loop through the array of channel indicators
			for(int i = 0; i < ChannelIndicator.Length; i++ )
			{
				//if a match for the channelName is found, IndexOf returns a 
				//positive value. If not check for an exact match.			
				if ((channelName.IndexOf(ChannelIndicator[i]) > 0) || (channelName == (ChannelIndicator[i])))					
				{
					//set the channelLanguage when a match has been found
					channelLanguage = Languages[i];
					break;
				}
			}
			//return channelLanguage, which will be null if no match
			//has been found
			return channelLanguage;
		}


		public static string getBaseChannelURL(string channelName)
		{
			string url = channelName;
			//loop through the array of channel indicators
			for(int i = 0; i < ChannelIndicator.Length; i++ )
			{
				int index = channelName.IndexOf(ChannelIndicator[i]);
				if( index > -1 )
				{
					// Include current channel indicator + the famous /
					url = channelName.Substring(0,index + ChannelIndicator[i].Length);
					break;
				}
			}
			//return channelLanguage, which will be null if no match
			//has been found
			return @"~/";
		}

        public static string getBookmarkBaseChannelURL(string channelName)
        {
            return "/Web2/";
        }

        /// <summary>
        /// Gets the session time out specified in the web config file
        /// </summary>
        /// <returns></returns>
        public int GetDefaultSessionTimeOut()
        {
            System.Web.Configuration.SessionStateSection sessionStateSection =
                (System.Web.Configuration.SessionStateSection)System.Configuration.ConfigurationManager.GetSection("system.web/sessionState");
            TimeSpan sessionTimeOut = sessionStateSection.Timeout;
            return sessionTimeOut.Minutes;
        }
        #endregion

        #region Private methods
        /// <summary>
		/// This method will add the resources array to the client side page and also register
		/// the resources script that will handle the array. 
		/// </summary>		
		private void AddResourcesToClientScript()
		{
			StringBuilder resourceDeclaration = new StringBuilder();

			//add an opening tag
			resourceDeclaration.Append("{");
			
			//loop through the resources and create a string of this type "ResourceKey: 'ResourceString'"
			int count = 1;
			foreach(string key in scriptResources.Keys)
			{				
				resourceDeclaration.Append(key.Replace(".", "_"));
				resourceDeclaration.Append(": '");
				resourceDeclaration.Append(scriptResources[key]);
				resourceDeclaration.Append("'");
				if (count != scriptResources.Count)
					resourceDeclaration.Append(",");
				count++;
			}

			//add the closing tag
			resourceDeclaration.Append("}");

			//add the array to the page
            Page.ClientScript.RegisterArrayDeclaration("resources", resourceDeclaration.ToString());

			//add the script to handle the resources in the page						
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
            Page.ClientScript.RegisterClientScriptBlock(typeof(TDPage), "Resources", scriptRepository.GetScript("Resources", JAVASCRIPT_DOM));			
		}

        private void ExtractLanguageFromQueryStringAndRedirectIfRequired()
        {
            string requestedLanguageString = persistedQueryString[requestLanguageQueryStringKey];

            if (requestedLanguageString != null)
            {
                Language language = GetRequestedLanguageFromQueryString(requestedLanguageString);
                CurrentLanguage.Value = language;

                //Now we server transfer, ensuring that the language is removed
                //from the query string:
                string replaceText = string.Format("{0}={1}", requestLanguageQueryStringKey, requestedLanguageString).ToLower();
                string newUrl = Request.RawUrl.ToLower().Replace(replaceText, "");
                Server.Transfer(newUrl);
                //string newUrl = Request.Url.ToString().ToLower().Replace(replaceText, "");
                //Response.Redirect(newUrl);
            }
        }

        private Language GetRequestedLanguageFromQueryString(string value)
        {
            Language returnValue = Language.English;

            try
            {
                returnValue = (Language)Enum.Parse(typeof(Language), value, true);

                //Note that the above could be wrong if a number is passed, so
                //make sure it is defined in the Enum:
                if (!Enum.IsDefined(typeof(Language), returnValue))
                {
                    returnValue = Language.English;
                }
            }
            catch
            {
                returnValue = Language.English;
            }

            return returnValue;
        }

        public bool LoadDatabaseContentIfNecessary()
        {
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
            if (id != "")
            {
                ControlPropertyCollection properties = ContentProvider.Instance["ContentDatabase"].GetControlProperties();

                this.PageTitle = properties.GetPropertyValue(id, "title");
                string channel = properties.GetPropertyValue(id, "channel");

                string pageID = HttpContext.Current.Request.FilePath.Replace("/", "_").Replace(".aspx", "");

                ControlPropertyCollection inner_properties = ContentProvider.Instance[properties.GetPropertyValue(id, "querystring")].GetControlProperties();

                foreach (Control c in this.Form.Controls)
                {
                    if (c is Panel)
                    {
                        Panel control = (c as Panel);
                        string panelName = control.ID.Replace("_", " ").ToLower();
                        string value = inner_properties.GetPropertyValue(panelName, channel) == null ? "" : inner_properties.GetPropertyValue(panelName, channel);
                        if (!string.IsNullOrEmpty(value))
                        {
                            isStaticPage = true;
                            if (!control.HasControls())
                                control.Controls.Add(new LiteralControl(value));
                            else
                                (control.Controls[0] as LiteralControl).Text = value;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        protected void BlankPanelText(Panel panel)
        {
            if (panel.Controls.Count > 0)
            {
                LiteralControl literal = panel.Controls[0] as LiteralControl;

                if (literal != null)
                {
                    literal.Text = @"<br/><br/>";
                }
            }
        }

        /// <summary>
        /// Method to add the current planner mode to the view state (and cookie)
        /// </summary>
        private void AddModeToViewState()
        {
            string mode = string.Empty;

            if (TDSessionManager.Current.FindAMode == FindAMode.None)
            {
                // If FindAMode is none, then not sure if we're actually in door to door or not.
                // Assume we are in door to door mode by checking if it is in the results page group.
                // Finally override mode for specific case handling.

                if (IsPageInGroup(this.PageId, PageGroup.Result))
                {
                    // Specific page mode overide
                    switch (this.PageId)
                    {
                        case PageId.VisitPlannerInput:
                        case PageId.VisitPlannerResults:
                            mode = plannerModeVisitPlanner;
                            break;

                        default:
                            mode = plannerModeDoorToDoor;
                            break;
                    }
                }
                else
                {
                    mode = plannerModeNone;
                }   
            }
            else
            {
                // Specific handling for car park as the findamode will switch to car if user continues to plan a journey
                if ((TDSessionManager.Current.FindAMode == FindAMode.Car) && (TDSessionManager.Current.IsFromNearestCarParks))
                {
                    mode = FindAMode.CarPark.ToString();
                }
                else
                {   // Otherwise use the Find a mode value to determine mode
                    mode = TDSessionManager.Current.FindAMode.ToString();
                }
            }

            this.ViewState.Add(plannerModeKey, mode);
            PersistentCookie.PlannerMode = mode;
        }

        #endregion

        #region Session Errors and Timeout
        /// <summary>
        /// Checks the Session if there are any errors in the sessionErrorKey, if there are then
        /// they are moved to the InputPageState.InputSessionErrors to allow the Portal pages to use. 
        /// </summary>
        private void ProcessSessionErrors()
        {
            string sessionErrors = (string)Session[sessionErrorKey];

            // Are there any errors
            if (!string.IsNullOrEmpty(sessionErrors))
            {
                string[] sessionErrorsArray = sessionErrors.Split(',');

                ArrayList sessionErrorsArrayList = new ArrayList();

                // Errors should be an int of the enum InputSessionError
                foreach (string error in sessionErrorsArray)
                {
                    int errorID = 0;
                    if (int.TryParse(error, out errorID))
                    {
                        sessionErrorsArrayList.Add((InputSessionError)errorID);
                    }
                }

                // There are errors, add them to the InputPageState
                if (sessionErrorsArrayList.Count > 0)
                {
                    TDSessionManager.Current.InputPageState.InputSessionErrors = (InputSessionError[])sessionErrorsArrayList.ToArray(typeof(InputSessionError));
                }

                // Remove the errors to prevent them being reported again
                Session.Remove(sessionErrorKey);
            }
        }

        /// <summary>
        /// Performs session timeout handling. Redirects user to appropriate page based on the action
        /// the user has performed once their session has timed out
        /// </summary>
        private void ProcessSessionTimeout()
        {
            // The order we check whats been requested following timeout
            // 1. Check if its a header tab click (as these come via postback, so need to explicitly check the events being raised)
            // 2. Check if its static page url
            // 3. Check if its an input page url and the user has clicked a submit button
            // 4. Check if its a input page url (can be absolute url, or a postback)
            // 5. Check if its a result page (assume any postback from a result page is a button click)
            // 6. All other, send to Timeout page

            #region method variables
            string path = string.Empty;
            string queryString = string.Empty;
            StringBuilder redirectUrl = new StringBuilder();
            #endregion

            // Can only perform targetted session timeout if we can detect what user wanted
            if (this.Context != null)
            {
                path = this.Context.Request.Url.LocalPath.ToLower();
                queryString = this.Context.Request.QueryString.ToString();
            }

            TransitionEvent transitionEvent = IsHeaderTabClick();
            if (transitionEvent != TransitionEvent.Empty)
            {
                #region Handle header tab click redirect
                // Specific check to make sure we keep session if user clicked on the Train cost/time icon on input page
                if (transitionEvent != TransitionEvent.FindTrainInputDefault &&
                    transitionEvent != TransitionEvent.FindTrainCostInputDefault)
                {
                    // Clear the session to ensure fresh start
                    Session.Abandon();

                    // Determine the path to go to 
                    redirectUrl.Append(GetPageTransferURL(transitionEvent, true));
                }
                else
                {
                    // Determine the path to go to 
                    redirectUrl.Append(GetPageTransferURL(transitionEvent, false));
                }
                #endregion
            }
            else if ((IsRequestPathInGroup(path, PageGroup.JourneyInput)) && (IsButtonClick()))
            {
                #region Handle journey input page button click redirect
                redirectUrl.Append(path);

                if (!string.IsNullOrEmpty(queryString))
                {
                    redirectUrl.Append((redirectUrl.ToString().IndexOf("?") > -1) ? "&" : "?");
                    redirectUrl.Append(queryString);
                }

                AddSessionErrors();
                #endregion
            }
            else if ((IsRequestPathInGroup(path, PageGroup.StaticInput)) && (IsButtonClick()))
            {
                #region Handle static input page button click redirect

                redirectUrl.Append(GetPageTransferURL(TransitionEvent.TimeOut, true));

                #endregion
            }
            else if ((IsRequestPathInGroup(path, PageGroup.JourneyInput)) || (IsRequestPathInGroup(path, PageGroup.StaticInput)))
            {
                #region Handle input page redirect
                // As the page user is attempting to go to doesn't rely on journeys in session, we're ok to go there

                // Can't clear the session because input page relies on items populated

                HandleLanguageChangeTimeout();

                if (IsRadioButtonClick())
                {
                    // Specific handling for Find a train input radio buttons
                    if (this.PageId == PageId.FindTrainInput)
                        redirectUrl.Append(GetPageTransferURL(TransitionEvent.FindTrainCostInputDefault, false));
                    else
                        // Determine the path to go to 
                        redirectUrl.Append(GetPageTransferURL(TransitionEvent.FindTrainInputDefault, false));
                }
                else if ((this.PageId == PageId.JourneyPlannerLocationMap) && (!string.IsNullOrEmpty(IsImageClick())))
                {   
                    // Specific handling if user is on a map page and clicks the map/zoom button
                    redirectUrl.Append(path);
                    
                    AddSessionErrors();
                }
                else if ((this.PageId == PageId.FindMapResult))
                {
                    // Specific handling if user is on a map page and does anything on the map
                    redirectUrl.Append(GetPageTransferURL(TransitionEvent.FindMapInputDefault, false));

                    AddSessionErrors();
                }
                else
                {
                    // And go to the page they wanted, abandon flag to prevent endless Session Timeout loop
                    redirectUrl.Append(path);

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        redirectUrl.Append((redirectUrl.ToString().IndexOf("?") > -1) ? "&" : "?");
                        redirectUrl.Append(queryString);
                    }
                }
                #endregion
            }
            else if (IsRequestPathInGroup(path, PageGroup.Static))
            {
                #region Handle static page redirect

                // Specific home page handling
                ArrayList clickActions = new ArrayList();
                clickActions.Add("go");
                clickActions.Add("advanced");
                if ((this.PageId == PageId.Home) && (IsButtonClick(clickActions)))
                {
                    redirectUrl.Append(HandleHomePageSessionTimeout());

                    AddSessionErrors();
                }
                else
                {
                    // As the page user is attempting to go to doesn't rely on journeys in session, we're ok to go there
                    // Clear the session to ensure fresh start
                    Session.Abandon();

                    // And go to the page they wanted, abandon flag to prevent endless Session Timeout loop
                    redirectUrl.Append(path);
                    redirectUrl.Append((redirectUrl.ToString().IndexOf("?") > -1) ? "&" : "?");
                    redirectUrl.Append(abandonQueryString);

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        redirectUrl.Append((redirectUrl.ToString().IndexOf("?") > -1) ? "&" : "?");
                        redirectUrl.Append(queryString);
                    }
                }
                #endregion
            }
            else if (IsRequestForResultPage(path, PageGroup.Result, this.PageId))
            {
                #region Handle result page redirect
                // If the user clicks a button, then the postback occurs with the current page path sent 
                // in the Context and the current Page Id. 
                // If we receive a Page Id which is in the Result page group, we assume it is a button click event 
                // (e.g. details, summary) so we do a redirect back to the input page, displaying a timeout error

                // Only way to determine what mode user was in is by looking first in the viewstate, then cookie
                string plannerMode = (string)this.ViewState[plannerModeKey];
                if (string.IsNullOrEmpty(plannerMode))
                {
                    // otherwise check cookie
                    plannerMode = PersistentCookie.PlannerMode;
                }

                if ((string.IsNullOrEmpty(plannerMode)) || (plannerMode == plannerModeNone))
                {
                    // No planner mode, so default behaviour is to send to Timeout page
                    redirectUrl.Append(GetPageTransferURL(TransitionEvent.TimeOut, true));
                }
                else
                {
                    try
                    {
                        #region Determine redirect URL
                        transitionEvent = TransitionEvent.TimeOut;

                        // Determine the page to go to by establising the transition
                        if (plannerMode == plannerModeDoorToDoor)
                        {
                            transitionEvent = TransitionEvent.JourneyPlannerInputDefault;
                        }
                        else if (plannerMode == plannerModeVisitPlanner)
                        {
                            transitionEvent = TransitionEvent.VisitPlannerNewClear;
                        }
                        else
                        {
                            FindAMode mode = (FindAMode)Enum.Parse(typeof(FindAMode), plannerMode, true);
                            transitionEvent = FindInputAdapter.GetTransitionEventFromModeAll(mode);
                        }

                        redirectUrl.Append(GetPageTransferURL(transitionEvent, false));

                        #endregion

                        // If the user clicked new search, then no need to show session errors
                        ArrayList clickActions = new ArrayList();
                        clickActions.Add("new search");

                        if (!IsButtonClick(clickActions))
                        {
                            // Session errors to be displayed on the input page
                            AddSessionErrors();
                        }
                    }
                    catch
                    {
                        // Any error, take user to Timeout page
                        redirectUrl.Append(GetPageTransferURL(TransitionEvent.TimeOut, true));
                    }
                }

                #endregion
            }
            else
            {
                #region Handle timeout page redirect

                redirectUrl.Append(GetPageTransferURL(TransitionEvent.TimeOut, true));

                #endregion
            }

            // We've determined where we can send user, so take them there and end the response.
            Response.Redirect(redirectUrl.ToString(), true);
        }

        #region Timeout helper methods
        /// <summary>
        /// Determines if the page contains the header click (imageclick) event 
        /// Can be expanded in future to also find other click events.
        /// Assumes the control can be found on the page at time of method call
        /// </summary>
        /// <returns></returns>
        private TransitionEvent IsHeaderTabClick()
        {
            // First try as an Image e.g. could be the Home logo being clicked
            string controlName = IsImageClick();

            // Find the control and return the transition event
            if (!string.IsNullOrEmpty(controlName))
            {
                TDImageButton imageButton = this.Page.FindControl(controlName) as TDImageButton;
                if (imageButton != null)
                {
                    // We can only work with the ID as the control hasn't yet been setup on the page
                    return GetTransitionEventForControlName(imageButton.ID);
                }
            }

            // Second try as a Button e.g. could be the Home tab being clicked
            if (string.IsNullOrEmpty(controlName))
            {
                // Because header tabs are no longer image buttons, search 
                // specifically for the actual header tab key in the request
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.ToLower().StartsWith("headercontrol$"))
                    {
                        controlName = key;
                        break;
                    }
                }

                // IE behaves differently and header tab clicked control can be found 
                // by inspecting the form event target and not form name-value collection
                if (string.IsNullOrEmpty(controlName))
                {
                    if (!string.IsNullOrEmpty(Request.Form["__EVENTTARGET"]))
                    {
                        if (Request.Form["__EVENTTARGET"].ToLower().StartsWith("headercontrol$"))
                        {
                            controlName = Request.Form["__EVENTTARGET"];
                           
                        }
                    }
                }
            }

            // Find the control and return the transition event
            if (!string.IsNullOrEmpty(controlName))
            {
                Control button = this.Page.FindControl(controlName);
                if (button != null)
                {
                    // We can only work with the ID as the control hasn't yet been setup on the page
                    return GetTransitionEventForControlName(button.ID);
                }
            }
            
        
            // Not a click
            return TransitionEvent.Empty;
        }

        /// <summary>
        /// Returns the transition event for a control Id. This only contains the 
        /// header navigation tab control ids
        /// </summary>
        /// <param name="controlId"></param>
        /// <returns>TransitionEvent.Empty if no match found</returns>
        private TransitionEvent GetTransitionEventForControlName(string controlId)
        {
            switch (controlId)
            {
                case "homeImageButton":
                case "headerHomepageLink":
                case "homeImageLink":
                    return TransitionEvent.GoHome;
                case "planAJourneyImageButton":
                case "planAJourneyImageLink":
                    return TransitionEvent.PlanAJourneyTab;
                case "findAImageButton":
                case "findAImageLink":
                    return TransitionEvent.FindAPlaceTab;
                case "travelInfoImageButton":
                case "travelInfoImageLink":
                    return TransitionEvent.TravelInfoTab;
                case "tipsAndToolsImageButton":
                case "tipsAndToolsImageLink":
                    return TransitionEvent.TipsToolsTab;
                // Handling for train input image buttons
                case "imageCost":
                    return TransitionEvent.FindTrainCostInputDefault;
                case "imageTime":
                    return TransitionEvent.FindTrainInputDefault;
                default:
                    return TransitionEvent.Empty;
            }
        }

        /// <summary>
        /// Determines if the user has performed an image click. 
        /// Loops through all the form keys and checks if any end with ".x" and ."y". This indicates
        /// user has clicked on an image which throws an event
        /// </summary>
        /// <returns>The control name of the image clicked on. String.Empty if no click event found</returns>
        private string IsImageClick()
        {
            string controlName = string.Empty;

            // Loop through all the keys until we find the event we want
            foreach (string key in Request.Form.AllKeys)
            {
                // Image click events have .x and .y on the end
                if (key.ToLower().EndsWith(".x") || key.ToLower().EndsWith(".y"))
                {
                    // Ensure we have the x and y pair
                    if (controlName == key.Substring(0, key.Length - 2))
                    {
                        break;
                    }

                    controlName = key.Substring(0, key.Length - 2);
                }
            }

            return controlName;
        }

        /// <summary>
        /// Determines if the user has performed a button click, for the supplied button name array
        /// </summary>
        /// <returns></returns>
        private bool IsButtonClick(ArrayList clickActions)
        {
            // Loop through all the keys until we find the event we want
            foreach (string key in Request.Form.AllKeys)
            {
                string[] keyValues = Request.Form.GetValues(key);
                foreach (string keyValue in keyValues)
                {
                    if (clickActions.Contains(keyValue.ToLower()))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the user has performed a button click.
        /// Checks for most "input" page buttons: Next, Clear Page, Advanced Options...
        /// </summary>
        /// <returns></returns>
        private bool IsButtonClick()
        {
            #region buttons
            // Button actions we need to check for, have to use the display text to detect the button click
            // as there is no other information we can go on at this point in page life cycle
            ArrayList clickActions = new ArrayList();

            clickActions.Add(GetResource("langStrings", "FindPageOptionsControl.ShowAdvancedOptions.Text").ToLower()); //"advanced options"
            clickActions.Add(GetResource("langStrings", "FindPageOptionsControl.Back.Text").ToLower()); //"back"
            clickActions.Add(GetResource("langStrings", "MapPlanJourneyLocationControl.buttonCancel.Text").ToLower()); //"cancel"
            clickActions.Add(GetResource("langStrings", "FindLocationControl.findNearestTrainButtonText").ToLower()); //"find nearest..."
            clickActions.Add(GetResource("langStrings", "HelpButtonControl.Text").ToLower()); //"help"
            clickActions.Add(GetResource("langStrings", "JourneyEmissionsCompareControl.CompareJourney.More").ToLower()); //"more"
            clickActions.Add(GetResource("langStrings", "FindPageOptionsControl.Submit.Text").ToLower()); //"next"
            clickActions.Add(GetResource("langStrings", "AmbiguousLocationSelectControl2.commandNewLocation.Text").ToLower()); //"new location"
            clickActions.Add(GetResource("langStrings", "MapSelectLocationControl.buttonOK.Text").ToLower()); //"ok"
            clickActions.Add(GetResource("langStrings", "MapLocationControl.buttonPlanAJourney.Text").ToLower()); //"plan a journey"
            clickActions.Add(GetResource("langStrings", "PrinterFriendlyPageButton.Text").ToLower()); //"printer friendly"
            clickActions.Add(GetResource("langStrings", "MapLocationControl.buttonSelectLocation.Text").ToLower()); //"select new location"
            clickActions.Add(GetResource("langStrings", "JourneyDetails.buttonShowMap.Text").ToLower()); //"show map"
            clickActions.Add(GetResource("langStrings", "JourneyEmissionsCompareControl.ChangeViewHyperlink.ShowGraphical.Text").ToLower()); //"show graphical view"
            clickActions.Add(GetResource("langStrings", "JourneyEmissionsCompareControl.ChangeViewHyperlink.ShowTable.Text").ToLower()); //"show table view"
            clickActions.Add(GetResource("langStrings", "JourneyMapControl.JourneyPlanner.imageShowOnMap.Text").ToLower()); //"show selected symbols"
            clickActions.Add(GetResource("langStrings", "MapToolsControl.buttonMapPlus.Text").ToLower()); // + Zoom button
            clickActions.Add(GetResource("langStrings", "MapToolsControl.buttonMapMinus.Text").ToLower()); // - Zoom button
            #endregion

            return IsButtonClick(clickActions);
        }

        /// <summary>
        /// Method to determine if user has clicked a radio button
        /// </summary>
        /// <returns></returns>
        private bool IsRadioButtonClick()
        {
            // Specific handling for Find a train input page
            string[] formValues = Request.Form.GetValues("searchType");
            if ((formValues != null) && (formValues.Length > 0))
            {
                if ((formValues[0] == "searchTypeControl:costRadioButton") && (this.PageId == PageId.FindTrainInput))
                {
                    return true;
                }
                else if ((formValues[0] == "searchTypeControl:timeRadioButton") && (this.PageId == PageId.FindTrainCostInput))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if the url path requested is in the provided Group, e.g. Static, Input...
        /// </summary>
        /// <param name="path">URL path</param>
        /// <returns></returns>
        private bool IsRequestPathInGroup(string path, PageGroup pageGroup)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            else
            {
                // Get the list of pages we can allow user to go to if sesssion timeout occurs
                // Get the PageController from Service Discovery
                IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
                PageGroupDetails[] pageGroupDetails = pageController.GetPageGroupDetails(pageGroup);

                foreach (PageGroupDetails pageGroupDetail in pageGroupDetails)
                {
                    // Does the path contain any of the pages in this group
                    if (path.IndexOf(pageGroupDetail.PageUrl.ToLower()) > 0)
                    {
                        return true;
                    }
                }

                // if we get to here than path is not for a page in the group being checked
                return false;
            }
        }


        /// <summary>
        /// Determines if the requested Page is in the provided Group, , e.g. Static, Input...
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="pageGroup"></param>
        /// <returns></returns>
        private bool IsPageInGroup(PageId pageId, PageGroup pageGroup)
        {
            // Get the list of pages for the group
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            PageId[] pageIds = pageController.GetPageIdsForGroup(pageGroup);

            ArrayList pageIdsArray = new ArrayList(pageIds);

            if (pageIdsArray.Contains(pageId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Determines if the url path and page id is for a results page, e.g. Journey Summary, Find Station results,
        /// </summary>
        /// <param name="path"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private bool IsRequestForResultPage(string path, PageGroup pageGroup, PageId page)
        {
            if ((string.IsNullOrEmpty(path)) || (page == PageId.Empty))
            {
                return false;
            }
            else
            {
                if (IsPageInGroup(page, pageGroup))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        
        /// <summary>
        /// Returns the page transfer url object for the provided TransitionEvent
        /// Flag to add the abandon query string value
        /// </summary>
        /// <param name="transitionEvent"></param>
        /// <returns></returns>
        private string GetPageTransferURL(TransitionEvent transitionEvent, bool abandonFlag)
        {
            // Get the PageController from Service Discovery
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

            // Get the PageTransferDataCache from the pageController
            IPageTransferDataCache pageTransferDataCache = pageController.PageTransferDataCache;

            // Now, get the pageId associated with the transiton event.
            PageId transferPage = pageTransferDataCache.GetPageEvent(transitionEvent);

            // Get the PageTransferDetails object to which holds the Url
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(transferPage);

            StringBuilder url = new StringBuilder();

            // url prefix
            if (TDPage.SessionChannelName != null)
            {
                url.Append(getBaseChannelURL(TDPage.SessionChannelName));
            }

            // the page to transfer to 
            url.Append(pageTransferDetails.PageUrl);


            #region Specific parameters
            //add  a querystring to the URL if the page originally to be displayed was the User Survey page
            if (PageId == PageId.UserSurvey)
            {
                url.Append("?UserSurvey=true");
            }

            // Check to see if the page opens in a new page seperate to the portal
            // if so then add the fromTDP=true string to the url so that we can identify if the 
            // new window was generated by the portal
            if (this is INewWindowPage)
            {
                url.Append((url.ToString().IndexOf("?") > -1) ? "&" : "?");
                url.Append("fromTDP=true");
            }
            #endregion

            if (abandonFlag)
            {
                // abandon to prevent endless session timeout loop
                url.Append((url.ToString().IndexOf("?") > -1) ? "&" : "?");
                url.Append(abandonQueryString);
            }

            return url.ToString();
        }

        /// <summary>
        /// Adds the timeout session errors
        /// </summary>
        private void AddSessionErrors()
        {
            // Add session errors to be displayed on input page
            string sessionErrors = ((int)InputSessionError.TimeoutSorry).ToString() + "," +
                                   ((int)InputSessionError.TimeoutExpires).ToString();

            Session.Add(sessionErrorKey, sessionErrors);
        }

        /// <summary>
        /// Method to set up the redirect url when user times out on the homepage and 
        /// does a miniplanner journey or find a place loation
        /// </summary>
        /// <returns></returns>
        private string HandleHomePageSessionTimeout()
        {
            string redirectUrl = string.Empty;

            TransitionEvent transitionEvent = TransitionEvent.TimeOut;

            try
            {
                // If these keys are in the request, then button on miniplanner selected
                if ((!string.IsNullOrEmpty(Request.Form["planAJourneyControl:buttonAdvanced"]))
                    ||
                    (!string.IsNullOrEmpty(Request.Form["planAJourneyControl:buttonSubmit"])))
                {
                    transitionEvent = TransitionEvent.JourneyPlannerInputDefault;
                }
                else if (!string.IsNullOrEmpty(Request.Form["findAPlaceControl1:buttonSubmit"]))
                {
                    // Go on find a place selected, set transition based on dropdown
                    string selectedShowDropdownValue = Request.Form["findAPlaceControl1:dropDownLocationShowOptions"];

                    if (!string.IsNullOrEmpty(selectedShowDropdownValue))
                    {
                        switch (selectedShowDropdownValue)
                        {
                            case "SelectOption":
                            case "MapOfArea":
                                transitionEvent = TransitionEvent.FindMapInputDefault;
                                break;
                            case "StationAirport":
                                transitionEvent = TransitionEvent.FindStationInputDefault;
                                break;
                            case "CarPark":
                                transitionEvent = TransitionEvent.FindCarParkInputDefault;
                                break;
                            case "TrafficLevels":
                                transitionEvent = TransitionEvent.GoTrafficMap;
                                break;
                        }
                    }
                }
            }
            catch
            {
                transitionEvent = TransitionEvent.TimeOut;
            }

            if (transitionEvent == TransitionEvent.TimeOut)
            {
                return GetPageTransferURL(transitionEvent, true);
            }
            else
            {
                return GetPageTransferURL(transitionEvent, false);
            }
                
        }

        /// <summary>
        /// Updates the users language if a timeout has occured and the language footer link is clicked
        /// </summary>
        private void HandleLanguageChangeTimeout()
        {
            if ((!string.IsNullOrEmpty(Request.Form["Footercontrol:lnkLanguageSwitch"]))
                ||
                (!string.IsNullOrEmpty(Request.Form["FooterControl1:lnkLanguageSwitch"])))
            {
                CurrentLanguage.Value = (CurrentLanguage.Value == Language.English) ? Language.Welsh : Language.English;
            }
        }

        #endregion

        #endregion
        
        #region Site Redirect

        /// <summary>
        /// Redirect to the mobile site if appropriate
        /// </summary>
        protected virtual void PerformSiteRedirect()
        {
            SiteRedirectHelper.PerformSiteRedirect(Request, Response, Context, Session, pageId);
        }

        #endregion
    }
}
