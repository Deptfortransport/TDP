// *********************************************** 
// NAME                 : JourneyDetailsTableGridControl.ascx.cs 
// AUTHOR               : James Haydock
// DATE CREATED         : 09/06/2004
// DESCRIPTION          : A custom control to display
// details of an individual journey in a tabular format
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyDetailsTableGridControl.ascx.cs-arc  $
//
//   Rev 1.30   Mar 21 2013 10:13:16   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.29   Feb 11 2013 11:15:50   mmodi
//Dont display walkit link for accessible journey start end naptan to naptan leg
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.28   Feb 05 2013 13:21:16   mmodi
//Show Walk Interchange in map journey leg dropdown for accessible journey when required
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.27   Jan 24 2013 09:24:42   dlane
//Fixes to table journey view
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.26   Aug 02 2011 16:08:08   mmodi
//Ensure Map button is not shown in printer friendly view
//Resolution for 5720: Journey results in Table form on Printer friendly include Map buttons
//
//   Rev 1.25   Nov 08 2010 08:48:32   apatel
//Updated to remove CJP additional information for Trunk Exchange Point and interchange time
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.24   Nov 01 2010 15:24:00   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.23   Oct 27 2010 11:16:40   apatel
//Updated to add Error handling for CJP power user additional information 
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.22   Oct 26 2010 14:30:20   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.21   Sep 14 2010 09:49:18   apatel
//Updated to correct the walkit link for return journey
//Resolution for 5603: Return Journey - Walkit Links Wrong
//
//   Rev 1.20   Feb 26 2010 16:37:42   mmodi
//Show Service details link for International Coach mode, and correct Arrival boards link for international
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.19   Feb 26 2010 09:51:46   apatel
//Updated for International Planner to show checkin and exit time for Rail and Coach. Also, Updated to show international stop location as links if links available for them.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.18   Feb 25 2010 16:21:00   pghumra
//Code fix applied to resolve issue with date not being displayed on journey details section in the door to door planner when date of travel is different to requested date
//Resolution for 5413: CODE FIX - NEW - DEL 10.x - Issue with seasonal information change from Del 10.8
//
//   Rev 1.17   Feb 16 2010 11:15:54   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.16   Feb 12 2010 11:13:54   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.15   Jan 07 2010 13:38:56   apatel
//Updated for Walkit control on printer friendly page
//Resolution for 5357: Printer friendly page issue of header and walkit links
//
//   Rev 1.14   Dec 08 2009 15:59:14   apatel
//Walkit link code
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.13   Dec 04 2009 11:17:20   apatel
//Walkit control update to put walkit logo
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.12   Nov 16 2009 15:28:58   mmodi
//Updated for new mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 11 2009 16:43:08   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.10   Oct 23 2009 09:03:14   apatel
//Seasonal page link and next day journeys code changes for Day trip planner
//
//   Rev 1.9   Oct 15 2009 13:38:02   apatel
//Seasonal page link and next day journeys changes
//
//   Rev 1.8   Sep 14 2009 11:17:04   apatel
//Stop Information related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.7   Dec 17 2008 11:26:48   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.6   Nov 13 2008 15:26:32   jfrank
//Corrected after error picked up in code review.
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.5   Oct 13 2008 16:45:18   build
//Automatically merged for stream 5014
//
//   Rev 1.4.2.1   Sep 26 2008 13:43:32   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.2.0   Aug 27 2008 10:38:54   pscott
//UK:3104684
//
//   Rev 1.4.1.0   Aug 06 2008 08:39:04   pscott
//SCR5090 - make departureb and arrival boards normal text hyperlink buttons on grid view.
//
//   Rev 1.4   Jul 07 2008 12:57:34   pscott
//Add Space between depart/arrive time and depart/arrival board
//
//   Rev 1.3   Jun 27 2008 12:12:34   pscott
//5011 - Departure board visibility changes
//
//   Rev 1.2   Mar 31 2008 13:21:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:22   mturner
//Initial revision.
//
//   Rev 1.29   Nov 09 2006 12:31:58   mmodi
//Removed Car Park opening times note
//Resolution for 4248: Del 9.1: Remove Opening time note on Car Park Information page
//
//   Rev 1.28   Oct 06 2006 14:46:46   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.27.1.3   Sep 27 2006 11:06:44   esevern
//Amended repeater item created event handler to set opening times label visibility in the leg instructions control
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.27.1.2   Sep 26 2006 12:00:46   esevern
//Added setting of LegInstructionsControl.ShowingInGrid value so that car parking opening times note can be displayed correctly
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.27.1.1   Sep 21 2006 16:57:22   esevern
//corrected so that car park end/start locations for a journey are shown as hyperlinks to the car park information page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.27.1.0   Sep 05 2006 15:28:22   esevern
//Changes to InfoAvailable methods to show the start/end location as a hyperlink to the car park info page if its a car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.27   Jul 07 2006 14:04:00   CRees
//Amended GetArriveTime method to prevent output of time for continuous or frequency legs, except when they are the final leg.
//Resolution for 4133: Frequency and Continuous Legs Arrival times in Table Display
//
//   Rev 1.26   Mar 31 2006 10:40:40   NMoorhouse
//fix problem with walking leg times appearing
//Resolution for 3714: DN068 Replan: Replacing a walk leg preceding a bus service produces odd journey times
//
//   Rev 1.25   Mar 30 2006 10:55:56   NMoorhouse
//Ensure we're not display 'None' date/times for (footer) arrivals
//Resolution for 3714: DN068 Replan: Replacing a walk leg preceding a bus service produces odd journey times
//
//   Rev 1.24   Mar 28 2006 17:33:12   NMoorhouse
//Do not display 'None' date/times
//Resolution for 3714: DN068 Replan: Replacing a walk leg preceding a bus service produces odd journey times
//
//   Rev 1.23   Mar 20 2006 18:07:38   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22   Mar 13 2006 16:31:30   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.19.1.2   Feb 24 2006 14:16:58   NMoorhouse
//Updated for new CarDetails page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.19.1.1   Feb 13 2006 17:51:42   rhopkins
//Further changes to implement use of JourneyLeg as the source.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.19.1.0   Jan 26 2006 20:41:36   rhopkins
//Changed to use JourneyLeg instead of PublicJourneyDetails.  This allows proper mode-agnostic handling.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21   Feb 23 2006 16:12:04   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.20   Feb 17 2006 11:57:16   halkatib
//Added fixes for IR3573
//
//   Rev 1.19   Nov 22 2005 14:52:38   NMoorhouse
//Correct problem with Text being set as tooltip of a label instead of Text element
//Resolution for 3066: Del 8: When journey details are displayed in a table some details are missing and the table border is incomplete
//
//   Rev 1.18   Nov 15 2005 14:05:26   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.17   Nov 10 2005 15:31:38   rgreenwood
//TD089 ES020 Code review actions: fixed tooltips for  buttons when java is disabled
//
//   Rev 1.16   Nov 03 2005 16:18:50   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.15.1.2   Oct 27 2005 11:52:40   rgreenwood
//Removed remaining AltText for HyperlinkPostbackControls changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.15.1.1   Oct 26 2005 18:56:52   rgreenwood
//TD089 ES020 Changes for HyperlinkPostbackControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.15.1.0   Oct 20 2005 14:27:30   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.15   Aug 31 2005 12:00:24   RPhilpott
//Don't show frequency of service as a range if min and max frequencies are the same.
//Resolution for 2741: DN062: frequency of freq-based services
//
//   Rev 1.14   Aug 19 2005 14:07:38   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.13.1.7   Aug 16 2005 15:12:30   RPhilpott
//FxCop fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.6   Aug 02 2005 19:39:28   RPhilpott
//Allow for non-naptan based requests having naptans in locations (after extension).
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.5   Jul 28 2005 16:04:52   RPhilpott
//Service Details changes. 
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.4   Jul 25 2005 18:31:04   RWilby
//Added GetDisplayNotes method
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.3   Jul 25 2005 15:29:16   pcross
//Network map links now handles possibility of multiple services. Interface was changed to allow this.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.2   Jul 20 2005 18:16:06   pcross
//Added operator links and changed the way network map links populated
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.1   Jul 19 2005 17:13:06   pcross
//Non-functional updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.0   Jul 19 2005 11:42:00   pcross
//Addded Network Map Links control
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13   May 12 2005 17:37:30   asinclair
//Changed the constructor for the CarFormatter
//
//   Rev 1.12   Apr 26 2005 13:19:16   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.11   Apr 22 2005 16:06:38   pcross
//IR2192. Now raises event when map button is pressed instead of navigating to a new page so host control can react accordingly
//
//   Rev 1.10   Mar 01 2005 16:26:58   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.9   Oct 08 2004 15:36:12   RPhilpott
//Check for continuous legs instead of just for Mode.Walk, to handle taxis correctly.
//Resolution for 1697: Taxi legs not being treated as continuous legs
//
//   Rev 1.8   Sep 30 2004 12:06:12   rhopkins
//IR1648 Use ReturnOriginLocation and ReturnDestinationLocation when outputting Return Car segments of Extended Journeys.
//
//   Rev 1.7   Sep 17 2004 15:13:50   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.6   Jul 14 2004 14:46:14   jbroome
//IR 1184 - WAI testing. Associate table data with column headings.
//
//   Rev 1.5   Jun 25 2004 11:07:02   RPhilpott
//Handle checkin/checkout times for FindAFlight
//
//   Rev 1.4   Jun 25 2004 10:34:04   asinclair
//Fix for IR 964
//
//   Rev 1.3   Jun 22 2004 12:20:08   JHaydock
//FindMap page done. Corrections to printable map controls and pages. Various updates to Find pages.
//
//   Rev 1.2   Jun 17 2004 12:57:08   JHaydock
//Minor corrections - Return order display and Map button click
//
//   Rev 1.1   Jun 16 2004 20:23:54   JHaydock
//Update for JourneyDetailsTableControl to use ItineraryManager
//
//   Rev 1.0   Jun 09 2004 18:01:28   JHaydock
//Initial Revision

using System;
using TransportDirect.Common.ResourceManager;
using System.Text;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.AdditionalDataModule;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationInformationService;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// User control to display details of a journey in a tabular format.
	/// </summary>
	public partial  class JourneyDetailsTableGridControl : TDPrintableUserControl
    {
        #region Private members

        protected LegInstructionsControl legInstructionsControl;
		protected NetworkMapLinksControl networkMapLink;
        protected TDButton buttonMap;
        protected TDButton departButton;
        protected TDButton arriveButton;
        protected HyperLink hyperlinkDepartureBoard;
        protected HyperLink hyperlinkArrivalBoard;
        protected JavaScriptAdapter javaAdapter;

        protected HyperlinkPostbackControl modeLinkControl;
		protected HyperlinkPostbackControl startLocationInfoLinkControl;
        protected HyperLink startLocationInfoLink;
		protected Label startLocationLabelControl;
		protected HyperlinkPostbackControl endLocationInfoLinkControl;
        protected HyperLink endLocationInfoLink;
		protected Label endLocationLabelControl;
		protected VehicleFeaturesControl vehicleFeaturesControl;
        protected WalkitLinkControl walkitLink;
        protected HyperLink walkitImageLink;
        
		private JourneyControl.Journey journey;
		private PageId belongingPageId = PageId.Empty;
		private int itinerarySegment = -1;
		private bool useRoadJourney;
		private bool outward;
		private bool flight;
		private string minsText = String.Empty;
		private string minText = String.Empty;
		private string maxDurationText = String.Empty;
		private string typicalDurationText = String.Empty;
		private string everyText = String.Empty;
		private string secondsText = String.Empty;
		private string hoursText = String.Empty;
		private string hourText = String.Empty;
        private bool showAccessibleFeatures = false;
		
		private string buttonTextMapButton = String.Empty;
        private string buttonTextDepartureBoardButton = String.Empty;
        private string buttonTextArrivalBoardButton = String.Empty;
        private string toolTipTextLocationLink = String.Empty;
		private string toolTipTextDetailsLink = String.Empty;

		private const string originNaptanString = "Origin";
		private const string destinationNaptanString = "Destination";
		private const string carString = "CAR";
        /// <summary>
        /// Name of script (in script repository) used for opening window
        /// </summary>
        private const string JAVASCRIPT_FILE = "ErrorAndTimeoutLinkHandler";

		// Event to fire when the Map button is pressed
		public event MapButtonClickEventHandler MapButtonClicked;
		private bool isEndCarPark = false;
		private bool isStartCarPark = false;
		protected Label startCarParkLabel;
		protected Label endCarParkLabel;
		private ArrayList uniqueNameList; // list of unique car park references
		private bool useNameList;

        // Variables needed to add javascript to the map button click
        private bool addMapJavascript = false;
        private string mapId = "map";
        private string mapJourneyDisplayDetailsDropDownId = "mapdropdown";
        private string scrollToControlId = "mapControl";
        private string sessionId = "session";
        private int journeyRouteNumber = 0;
        private string journeyType = "PT";
        private MapHelper mapHelper = new MapHelper();

        private PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter();

        private string alternateTextWalkit = string.Empty;

        private FindAMode findAMode = FindAMode.None;

        #region CJPUserInfo controls
        private CJPUserInfoControl cjpUserLocationNaptanInfoStart;
        private CJPUserInfoControl cjpUserLocationCoordinateInfoStart;
        private CJPUserInfoControl cjpUserInfoWalkLength;
        private CJPUserInfoControl cjpUserInfoJourneyLegSource;
        private CJPUserInfoControl cjpUserLocationNaptanInfoEnd;
        private CJPUserInfoControl cjpUserLocationCoordinateInfoEnd;
        #endregion
        
        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
		/// Sets-up event handlers for all the buttons, etc
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			// Need to call UpdateData to populate the Repeater. This isn't required
			// when inheriting from Web.UI.Page (and not TDPage).
			UpdateData();
			AddEventHandlers();
            AddMapButtonEventHandlers();
			base.OnLoad(e);
		}

		/// <summary>
		/// Page Load method - initialises this control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
        }

        /// <summary>
        /// OnPreRender method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            UpdateData();
            AddMapButtonEventHandlers();
            base.OnPreRender(e);

        }

        #endregion

        #region Initialise

        /// <summary>
		/// Method to call to initialise this control.
		/// </summary>
		/// <param name="journey">Journey for which to render details</param>
		/// <param name="outward">Indicates if rendering for outward or return</param>
		/// <param name="printable">Indicates if rendering the printer friendly page</param>
		/// <param name="belongingPageId">Parent page PageId</param>
		/// <param name="itinerarySegment">The itinerary segment
		/// that the journey relates to - use -1 for unspecified</param>
        /// <param name="findAMode">Planner mode</param>
        /// <param name="journeyRequest">Journey Request</param>
		public void Initialise(JourneyControl.Journey journey,
            bool outward, PageId belongingPageId, int itinerarySegment, FindAMode findAMode, ITDJourneyRequest journeyRequest)
		{
			this.useRoadJourney = (journey is RoadJourney);
			this.journey = journey;
			this.outward = outward;
			this.belongingPageId = belongingPageId;
			this.itinerarySegment = itinerarySegment;
            this.findAMode = findAMode;

            if (journey is JourneyControl.PublicJourney)
            {
                showAccessibleFeatures = ((JourneyControl.PublicJourney)journey).AccessibleJourney;
            }

			minsText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minutesString", TDCultureInfo.CurrentUICulture);

			minText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minuteString", TDCultureInfo.CurrentUICulture);

			secondsText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.secondsString", TDCultureInfo.CurrentUICulture);

			hoursText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hoursString", TDCultureInfo.CurrentUICulture);

			hourText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hourString", TDCultureInfo.CurrentUICulture);

			everyText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.every", TDCultureInfo.CurrentUICulture);

			maxDurationText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.maxDuration", TDCultureInfo.CurrentUICulture);
                
			typicalDurationText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.typicalDuration", TDCultureInfo.CurrentUICulture);

			buttonTextMapButton = GetResource("JourneyDetailsControl.MapButton.Text");
            buttonTextDepartureBoardButton = GetResource("JourneyDetailsControl.DepartureBoardButton.Text");
            buttonTextArrivalBoardButton = GetResource("ArrivalsBoardHyperlink.labelArrivalsBoardNavigation");
  


			toolTipTextLocationLink = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.LocationLink.ToolTipText", TDCultureInfo.CurrentUICulture);

			toolTipTextDetailsLink = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.ServiceDetailsLink.ToolTipText", TDCultureInfo.CurrentUICulture);

            
            alternateTextWalkit = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.alternateTextWalkit", TDCultureInfo.CurrentUICulture);

            
        }

        /// <summary>
        /// Method which sets the values needed to add map javascript to the map buttons
        /// </summary>
        /// <param name="addMapJavascript"></param>
        /// <param name="mapId"></param>
        /// <param name="sessionId"></param>
        public void SetMapProperties(bool addMapJavascript, string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            this.addMapJavascript = addMapJavascript;
            this.mapId = mapId;
            this.mapJourneyDisplayDetailsDropDownId = mapJourneyDisplayDetailsDropDownId;
            this.scrollToControlId = scrollToControlId;
            this.sessionId = sessionId;
            this.journeyType = "PT"; // Should always be PT journey being shown

            if (this.journey != null)
            {
                this.journeyRouteNumber = journey.RouteNum;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
		/// Sets the data for the Repeater.
		/// </summary>
		private void UpdateData()
		{
			flight = false;

			if (journey == null)
			{
				detailsRepeater.DataSource = new object[0];
			}
			else
			{
				detailsRepeater.DataSource = journey.JourneyLegs;
				flight = ShowAirColumns();
			}

			detailsRepeater.DataBind();
		}

		/// <summary>
		/// returns a bool indicating whether to show the 'air' related columns
		/// this is done on basis of whether there are any air legs in the journey(s) being displayed
		/// </summary>
		private bool ShowAirColumns()
		{
			//check all details to see if any air modes found
			for (int i = 0; i < journey.JourneyLegs.Length; i++)
			{
				//air columns should be visible if the journey includes a flight leg.
				if (journey.JourneyLegs[i].Mode == ModeType.Air)
				{
					return true;
				}
			}

			//if method has not returned by now, then no air legs have been found so return false
			return false;
		}

		/// <summary>
		/// Method to add the event handlers to add dynamically generated buttons
		/// </summary>
		private void AddEventHandlers()
		{
			for(int i = 0; i < detailsRepeater.Items.Count; i++)
			{
                if	((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("startLocationInfoLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("startLocationInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.InformationStartLinkClick);
				}						

				if	((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("endLocationInfoLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("endLocationInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.InformationEndLinkClick);
				}						

				if	((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("modeLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("modeLinkControl")).link_Clicked +=
						new System.EventHandler(this.ServiceDetailsClick);
				}						

			}
		}

        /// <summary>
        /// Method to add the Map button event handlers to add dynamically generated buttons
        /// </summary>
        private void AddMapButtonEventHandlers()
        {
            TDButton tdButton = null;

            for (int i = 0; i < detailsRepeater.Items.Count; i++)
            {
                tdButton = (TDButton)detailsRepeater.Items[i].FindControl("buttonMap");

                if (tdButton != null)
                {
                    if (addMapJavascript)
                    {
                        // The CommandArgument value is populated with the zoom map value
                        // when the repeater items create event is fired. 
                        // Pick it up here and create the javascript function call
                        string mapZoomValue = tdButton.CommandArgument;

                        // This is the index the map journey segment drop down list needs to be 
                        // set to when zooming in to the map for a journey segment
                        int journeySegmentIndex = i + 1;

                        // Add the map javascript (allows zooming to segment on map without page postback)
                        tdButton.OnClientClick = GetMapButtonClickJavascript(mapZoomValue, journeySegmentIndex);
                    }
                    else
                    {
                        // Add button event handler for the Map button
                        tdButton.Click += new EventHandler(this.MapButtonClick);
                    }
                }
            }
        }

        /// <summary>
        /// Seting the naptan information for Stop Information page
        /// </summary>
        /// <param name="naptan"></param>
        private void SetStopInformation(string naptan)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            inputPageState.StopCode = naptan;
            inputPageState.StopCodeType = TDCodeType.NAPTAN;
            inputPageState.ShowStopInformationPlanJourneyControl = false;
        }

        #region Method to get Javscript for map button

        /// <summary>
        /// Method to return the javascript to fire on the Map image/button click. This calls
        /// a javascript function to zoom the map to the selected journey leg
        /// </summary>
        /// <returns></returns>
        private string GetMapButtonClickJavascript(string mapZoomValue, int journeySegmentIndex)
        {
            StringBuilder mapJavascript = new StringBuilder();

            mapJavascript.Append("return zoomJourneyDetailMap(");
            mapJavascript.Append("'" + mapId + "'");
            mapJavascript.Append(", '" + sessionId + "'");
            mapJavascript.Append(", '" + journeyRouteNumber + "'");
            mapJavascript.Append(", '" + journeyType + "'");
            mapJavascript.Append(", '" + mapZoomValue + "'");
            mapJavascript.Append(", '" + mapJourneyDisplayDetailsDropDownId + "'");
            mapJavascript.Append(", " + journeySegmentIndex.ToString() + "");
            mapJavascript.Append(", '" + scrollToControlId + "'");
            mapJavascript.Append(");");

            return mapJavascript.ToString();
        }

        #endregion

        #endregion

        #region Public methods

        /// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void InformationStartLinkClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( belongingPageId );

			if(IsStartCarPark && !link.CommandArgument.Equals(string.Empty))
			{
				string carParkRef =  link.CommandArgument;
				TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;	

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = 
					TransitionEvent.FindCarParkResultsInfo; 
			}
			else 
			{
				string naptan = link.CommandName;

				if( naptan != null && naptan.Length > 0 )
				{
					TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan;
                    SetStopInformation(naptan);
				}

				TDSessionManager.Current.InputPageState.AdditionalDataDescription = link.Text;

				// Show the information page for the selected location.
				// Write the Transition Event
                // CCN526 changes
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
					TransitionEvent.StopInformation;
			}
		}



		/// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void InformationEndLinkClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( belongingPageId );

			// need to check that this is the last item in the leg, and that its a car park
			if(IsEndCarPark && !link.CommandArgument.Equals(string.Empty))
			{
				string carParkRef =  link.CommandArgument;
				TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;	

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = 
					TransitionEvent.FindCarParkResultsInfo; 
			}
			else 
			{
				string naptan = link.CommandName;

				if( naptan != null && naptan.Length > 0 )
				{
					TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan;
                    SetStopInformation(naptan);
				}

				TDSessionManager.Current.InputPageState.AdditionalDataDescription = link.Text;

				// Show the information page for the selected location.
				// Write the Transition Event
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
					TransitionEvent.StopInformation;
			}
		}

		/// <summary>
		/// Event Handler for the information button.
		/// </summary>
		public void ServiceDetailsClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			int selectedJourneyLeg = Int32.Parse(link.CommandName, CultureInfo.InvariantCulture);

			TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
			journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
			journeyViewState.SelectedIntermediateItinerarySegment = itinerarySegment;
			journeyViewState.CallingPageID = belongingPageId;
	
			TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward] = outward;
		
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( belongingPageId );

			ModeType selectedModeType = ModeType.Rail;

			if (link.CommandArgument != null)
			{
				// get the mode of the select journey
				selectedModeType = (ModeType)Enum.Parse(typeof(ModeType), link.CommandArgument, true);
			}

			if (selectedModeType == ModeType.Car)
			{
				// Show the car details of the selected journey
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.CarDetails;
			}
			else
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
					TransitionEvent.ServiceDetails;
			}
		}


		/// <summary>
		/// Event Handler for the map button.
		/// </summary>
		public void MapButtonClick(object sender, EventArgs e)
		{
            TDButton button = (TDButton)sender;
            int selectedJourneyLeg = Convert.ToInt32(button.CommandName, TDCultureInfo.CurrentCulture.NumberFormat);

            // Write the selected journey leg to TDViewState
            TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
            journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
            journeyViewState.SelectedIntermediateItinerarySegment = itinerarySegment;
            journeyViewState.CallingPageID = belongingPageId;

            TDSessionManager.Current.Session[SessionKey.JourneyMapOutward] = outward;

            // Raise event so the click can be handled in the journey details form and the map displayed
            // as part of that form
            MapButtonClickEventHandler eventHandler = MapButtonClicked;
            if (eventHandler != null)
                eventHandler(this, new MapButtonClickEventArgs(selectedJourneyLeg));

        }

        #endregion

        #region Repeater control methods

        /// <summary>
		/// Set up user controls in the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item :
				case ListItemType.AlternatingItem:

                    #region Vehicle features

                    vehicleFeaturesControl = e.Item.FindControl("vehicleFeaturesControl") as VehicleFeaturesControl;

					if	(!useRoadJourney)
					{
						vehicleFeaturesControl.Features = ((PublicJourneyDetail)e.Item.DataItem).GetVehicleFeatures();
					}
					else
					{
						vehicleFeaturesControl.Visible = false;
                    }

                    #endregion

                    #region Network Map link

                    // Set up network map links control
					networkMapLink = (NetworkMapLinksControl)e.Item.FindControl("networkMapLink");

					if	(!useRoadJourney)
					{
						if  (networkMapLink != null)
						{
							if ((e.Item.DataItem != null) && (e.Item.DataItem is PublicJourneyDetail))
							{
								// Set properties for the control appropriate to the public journey data in the row
								networkMapLink.JourneyDetail = (PublicJourneyDetail)e.Item.DataItem;;
								networkMapLink.PrinterFriendly = this.PrinterFriendly;
							}
							else
							{
								networkMapLink.Visible = false;
							}
						}
					}
					else
					{
						networkMapLink.Visible = false;
                    }

                    #endregion

                    #region Leg instructions

                    // Set up leg instructions control
					legInstructionsControl = (LegInstructionsControl)e.Item.FindControl("legInstructionsControl");
					
					if  (legInstructionsControl != null)
					{
						legInstructionsControl.ShowingInGrid = true;
						legInstructionsControl.UseNameList = UseNameList;

						if (useRoadJourney)
						{
							legInstructionsControl.RoadJourney = (RoadJourney)journey;
						}

						// Set properties for the control appropriate to the journey data in the row
						legInstructionsControl.JourneyLeg = (JourneyLeg)e.Item.DataItem;
                        legInstructionsControl.UseWalkInterchange = plannerOutputAdapter.WalkInterchangeRequired((JourneyLeg)e.Item.DataItem, journey, showAccessibleFeatures);
                        legInstructionsControl.PrinterFriendly = this.PrinterFriendly;
						legInstructionsControl.NoServiceOpenTimeLabel.Text = GetResource("CarParkInformationControl.informationNote");
                        legInstructionsControl.JournyModeType = findAMode;

						// if its a road journey or the journey leg mode type is walk
						if(legInstructionsControl.JourneyLeg.LegStart.Location.CarParking != null)
						{
							string carParkRef = legInstructionsControl.JourneyLeg.LegStart.Location.CarParking.CarParkReference;
	
							// check whether this car park has already appeared in the results.
							// if this is the first occurence, display the opening times note
							if(UseNameList)
							{
								if( (!UniqueNameList.Contains(carParkRef)) )
								{
									UniqueNameList.Add(carParkRef);
									legInstructionsControl.NoServiceOpenTimeLabel.Visible = true;
								}
							}
							else
							{
								legInstructionsControl.NoServiceOpenTimeLabel.Visible = true;
							}

						}
						else
						{
							if(legInstructionsControl.JourneyLeg.LegEnd.Location.CarParking == null)
							{
								legInstructionsControl.NoServiceOpenTimeLabel.Visible = false;
							}
						}
						if(legInstructionsControl.JourneyLeg.LegEnd.Location.CarParking != null)
						{
							string carParkRef = legInstructionsControl.JourneyLeg.LegEnd.Location.CarParking.CarParkReference;

							if(UseNameList)
							{
								if( (!UniqueNameList.Contains(carParkRef)) )
								{
									UniqueNameList.Add(carParkRef);
									legInstructionsControl.NoServiceOpenTimeLabel.Visible = true;
								}
							}
							else
							{
								legInstructionsControl.NoServiceOpenTimeLabel.Visible = true;
							}
						}
						else
						{
							if(legInstructionsControl.JourneyLeg.LegStart.Location.CarParking == null)
							{
								legInstructionsControl.NoServiceOpenTimeLabel.Visible = false;
							}
						}

						// IR4248 - remove the Opening times note
						legInstructionsControl.NoServiceOpenTimeLabel.Visible = false;
                    }

                    #endregion

                    #region Map image/button

                    int selectedJourneyLeg = IndexOf((JourneyLeg)e.Item.DataItem);

                    // Map image/button handling
                    buttonMap = (TDButton)e.Item.FindControl("buttonMap");

                    if (buttonMap != null)
                    {
                        if (addMapJavascript)
                        {
                            // If adding javascript, get the zoom to map values, and attach to the button.
                            // When the click events are added to the repeater items (in Page_PreRender), 
                            // then this value is used to build up the javascript attached to the button.
                            if (!useRoadJourney)
                            {
                                JourneyControl.PublicJourneyDetail pjd = ((PublicJourneyDetail)e.Item.DataItem);
                                JourneyControl.PublicJourney pj = (JourneyControl.PublicJourney)journey;

                                ListItem listItem = mapHelper.GetListItemForDetail(
                                    pjd, selectedJourneyLeg, pj, pj.Details.Length, true);

                                buttonMap.CommandArgument = listItem.Value;
                            }
                        }
                        if (useRoadJourney)
                        {
                            buttonMap.Visible = !PrinterFriendly;
                        }
                        else
                        {
                            buttonMap.Visible = !(PrinterFriendly || journey.JourneyLegs[e.Item.ItemIndex].HasInvalidCoordinates);
                        }

                        // Always hide button for International planner
                        if (buttonMap.Visible && findAMode == FindAMode.International)
                        {
                            buttonMap.Visible = false;
                        }
                    }

                    #endregion

                    #region Departure/Arrival board link

                    // Set up the departure/arrive board link
                    departButton = (TDButton)e.Item.FindControl("departButton");
                    if (departButton != null)
                    {
                        departButton.ScriptName = GetScriptName();
                        departButton.EnableClientScript = ((TDPage)Page).IsJavascriptEnabled;



                        departButton.Text = GetResource("JourneyDetailsControl.DepartureBoardLink.Text");
                        departButton.ToolTip = DepartureBoardButtonText;

                        // departButton is visible only if user has javascript enabled
                        // otherwise we use the hyperlink version of the button
                        departButton.Visible = ((TDPage)Page).IsJavascriptEnabled && DepartureBoardButtonVisible(selectedJourneyLeg);
                    }

                    arriveButton = (TDButton)e.Item.FindControl("arriveButton");
                    if (arriveButton != null)
                    {
                        arriveButton.ScriptName = GetScriptName();
                        arriveButton.EnableClientScript = ((TDPage)Page).IsJavascriptEnabled;

                        arriveButton.Text = GetResource("JourneyDetailsControl.ArrivalBoardLink.Text");
                        arriveButton.ToolTip = ArrivalBoardButtonText;

                        // arriveButton is visible only if user has javascript enabled
                        // otherwise we use the hyperlink version of the button
                        arriveButton.Visible = ((TDPage)Page).IsJavascriptEnabled && ArrivalBoardButtonVisible(selectedJourneyLeg);
                    }

                    hyperlinkDepartureBoard = (HyperLink)e.Item.FindControl("hyperlinkDepartureBoard");
                    hyperlinkDepartureBoard.Text = DepartureBoardButtonText;
                    hyperlinkDepartureBoard.ToolTip = DepartureBoardButtonText;


                    hyperlinkArrivalBoard   = (HyperLink)e.Item.FindControl("hyperlinkArrivalBoard");
                    hyperlinkArrivalBoard.Text = ArrivalBoardButtonText;
                    hyperlinkArrivalBoard.ToolTip = ArrivalBoardButtonText;

           
                    // Determine if we should display the link, if so, set all of its properties
                    hyperlinkDepartureBoard.Visible = DepartureBoardButtonVisible(selectedJourneyLeg);
                    hyperlinkArrivalBoard.Visible = ArrivalBoardButtonVisible(selectedJourneyLeg);

                    if (hyperlinkDepartureBoard.Visible)
                    {
                        if (findAMode != FindAMode.International)
                        {
                            //check is train or airport
                            if (journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.ContainsNaptansForStationType(StationType.Airport)
                                  || journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.ContainsNaptansForStationType(StationType.AirportNoGroup))
                            {
                                try
                                {
                                    hyperlinkDepartureBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                                    // find air hyperlink here
                                    LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                                    hyperlinkDepartureBoard.NavigateUrl = refData.GetLocationInformation(journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan).DepartureLink.Url;


                                }
                                catch
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing airport URL for naptan: " + journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan);
                                    Logger.Write(oe);
                                    hyperlinkArrivalBoard.Visible = false;
                                }
                            }
                            else
                            {

                                IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
                                string crs = addData.LookupCrsForNaptan(journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan);

                                try
                                {
                                    hyperlinkDepartureBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                                    hyperlinkDepartureBoard.NavigateUrl =
                                    string.Format(Properties.Current["locationinformation.departureboardurl"], crs);

                                }
                                catch
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.departureboardurl");
                                    Logger.Write(oe);
                                    throw new TDException("missing property in PropertyService : locationinformation.departureboardurl", true, TDExceptionIdentifier.PSMissingProperty);
                                }
                            }
                        }
                        else
                        {

                            hyperlinkDepartureBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                            hyperlinkDepartureBoard.NavigateUrl =
                                journey.JourneyLegs[selectedJourneyLeg].LegStart.DepartureURL;
                        }

                        if (departButton != null)
                        {
                            departButton.Action = GetAction(hyperlinkDepartureBoard.NavigateUrl);
                            hyperlinkDepartureBoard.Visible = false;
                        }
                        hyperlinkDepartureBoard.Target = "blank";
                    }

                    if (hyperlinkArrivalBoard.Visible)
                    {

                        if (findAMode != FindAMode.International)
                        {
                            //check is train or airport
                            if (journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.ContainsNaptansForStationType(StationType.Airport)
                                  || journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.ContainsNaptansForStationType(StationType.AirportNoGroup))
                            {
                                try
                                {
                                    hyperlinkArrivalBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                                    // find air hyperlink here
                                    LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                                    hyperlinkArrivalBoard.NavigateUrl = refData.GetLocationInformation(journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.NaPTANs[0].Naptan).ArrivalLink.Url;
                                }
                                catch
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing airport URL for naptan: " + journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan);
                                    Logger.Write(oe);
                                    hyperlinkArrivalBoard.Visible = false;
                                }
                            }
                            else
                            {


                                IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
                                string crs = addData.LookupCrsForNaptan(journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.NaPTANs[0].Naptan);

                                try
                                {
                                    hyperlinkArrivalBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                                    hyperlinkArrivalBoard.NavigateUrl =
                                    string.Format(Properties.Current["locationinformation.arrivalboardurl"], crs);

                                }
                                catch
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.departureboardurl");
                                    Logger.Write(oe);
                                    throw new TDException("missing property in PropertyService : locationinformation.arrivalboardurl", true, TDExceptionIdentifier.PSMissingProperty);
                                }
                            }
                        }
                        else
                        {

                            hyperlinkArrivalBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                            hyperlinkArrivalBoard.NavigateUrl =
                                journey.JourneyLegs[selectedJourneyLeg].LegEnd.ArrivalURL;
                        }

                        if (arriveButton != null)
                        {
                            arriveButton.Action = GetAction(hyperlinkArrivalBoard.NavigateUrl);
                            hyperlinkArrivalBoard.Visible = false;
                        }
 
                        hyperlinkArrivalBoard.Target = "blank";
                    }

                    #endregion

                    #region Start Location Information link

                    startLocationInfoLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("startLocationInfoLinkControl");
                    startLocationInfoLink = e.Item.FindControl("startLocationInfoLink") as HyperLink;
					startLocationLabelControl = (Label)e.Item.FindControl("startLocationLabelControl");
					startLocationInfoLinkControl.PrinterFriendly = this.PrinterFriendly;
				
					bool startLocationLinkAvailable = false;

					startLocationLinkAvailable = StartLocationInfoAvailable((JourneyLeg)e.Item.DataItem);
					startLocationInfoLinkControl.Text = GetFromLocation((JourneyLeg)e.Item.DataItem);
					startLocationInfoLinkControl.CommandName = GetNaptan(IndexOf((JourneyLeg)e.Item.DataItem), true);

					if	(startLocationLinkAvailable)
					{
                        if (findAMode == FindAMode.International && ((JourneyLeg)e.Item.DataItem).LegStart.Location.Country.CountryCode != "GB")
                        {
                            startLocationInfoLink.NavigateUrl = ((JourneyLeg)e.Item.DataItem).LegStart.InformationURL;
                            startLocationInfoLink.Text = string.Format("{0} {1}", GetFromLocation((JourneyLeg)e.Item.DataItem), GetResource("ExternalLinks.OpensNewWindowImage"));
                            startLocationInfoLink.Visible = !PrinterFriendly;
                            startLocationInfoLinkControl.Visible = false;
                            startLocationLabelControl.Visible = false;
                        }
                        else
                        {
                            if (IsStartCarPark)
                            {
                                string carParkRef = GetStartCarParkRef((JourneyLeg)e.Item.DataItem);
                                startLocationInfoLinkControl.CommandName = carString;
                                startLocationInfoLinkControl.CommandArgument = carParkRef;
                            }

                            startLocationInfoLinkControl.ToolTipText = string.Format(CultureInfo.InvariantCulture, toolTipTextLocationLink, startLocationInfoLinkControl.Text);
                            startLocationInfoLinkControl.Visible = true;
                            startLocationLabelControl.Visible = false;
                        }
					}
					else
					{
						startLocationLabelControl.Text = startLocationInfoLinkControl.Text;
						startLocationLabelControl.Visible = true;
						startLocationInfoLinkControl.Visible = false;
                    }

                    #endregion

                    #region End Location Information link

                    endLocationInfoLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("endLocationInfoLinkControl");
                    endLocationInfoLink = e.Item.FindControl("endLocationInfoLink") as HyperLink;
					endLocationLabelControl = (Label)e.Item.FindControl("endLocationLabelControl");
					bool endLocationLinkAvailable = false;

					endLocationLinkAvailable = EndLocationInfoAvailable((JourneyLeg)e.Item.DataItem);
					endLocationInfoLinkControl.Text = GetToLocation((JourneyLeg)e.Item.DataItem);
					endLocationInfoLinkControl.CommandName = GetNaptan(IndexOf((JourneyLeg)e.Item.DataItem), false);

					if	(endLocationLinkAvailable)
					{
                        if (findAMode == FindAMode.International && ((JourneyLeg)e.Item.DataItem).LegEnd.Location.Country.CountryCode != "GB")
                        {
                            endLocationInfoLink.NavigateUrl = ((JourneyLeg)e.Item.DataItem).LegEnd.InformationURL;
                            endLocationInfoLink.Text = string.Format("{0} {1}", GetToLocation((JourneyLeg)e.Item.DataItem), GetResource("ExternalLinks.OpensNewWindowImage"));
                            endLocationInfoLink.Visible = !PrinterFriendly;
                            endLocationInfoLinkControl.Visible = false;
                            endLocationLabelControl.Visible = false;
                        }
                        else
                        {

                            if (IsEndCarPark)
                            {
                                string carParkRef = GetEndCarParkRef(IndexOf((JourneyLeg)e.Item.DataItem));
                                endLocationInfoLinkControl.CommandName = carString;
                                endLocationInfoLinkControl.CommandArgument = carParkRef;
                            }
                            endLocationInfoLinkControl.ToolTipText = string.Format(CultureInfo.InvariantCulture, toolTipTextLocationLink, endLocationInfoLinkControl.Text);
                            endLocationInfoLinkControl.Visible = true;
                            endLocationLabelControl.Visible = false;
                        }
					}
					else
					{
						endLocationLabelControl.Text = endLocationInfoLinkControl.Text;
						endLocationLabelControl.Visible = true;
						endLocationInfoLinkControl.Visible = false;
                    }

                    #endregion

                    #region Mode link

                    modeLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("modeLinkControl");

					if	(!useRoadJourney)
					{

						modeLinkControl.PrinterFriendly = this.PrinterFriendly;

						if	(HasServiceDetails((PublicJourneyDetail)e.Item.DataItem))
						{
							modeLinkControl.Text = GetMode((PublicJourneyDetail)e.Item.DataItem, false);
							modeLinkControl.ToolTipText = string.Format(CultureInfo.InvariantCulture, toolTipTextDetailsLink, GetMode((PublicJourneyDetail)e.Item.DataItem, true));
							modeLinkControl.Visible = true;
						}
						else
						{
							modeLinkControl.Visible = false;
						}
					}
					else
					{
						modeLinkControl.PrinterFriendly = this.PrinterFriendly;

						if	(HasServiceDetails((JourneyLeg)e.Item.DataItem))
						{
							modeLinkControl.Text = GetMode((JourneyLeg)e.Item.DataItem, false);
							modeLinkControl.ToolTipText = string.Format(CultureInfo.InvariantCulture, toolTipTextDetailsLink, GetMode((JourneyLeg)e.Item.DataItem, true));
							modeLinkControl.Visible = true;
						}
						else
						{
							modeLinkControl.Visible = false;
						}
                    }

                    #endregion

                    #region Walkit link

                    walkitLink = e.Item.FindControl("walkitLink") as WalkitLinkControl;
                    walkitImageLink = e.Item.FindControl("walkitImageLink") as HyperLink;
                    walkitLink.PrinterFriendly = PrinterFriendly;

                    if ((((JourneyLeg)e.Item.DataItem).Mode == ModeType.Walk)
                        && (!plannerOutputAdapter.WalkInterchangeRequired((JourneyLeg)e.Item.DataItem, journey, showAccessibleFeatures)))
                    {
                        walkitLink.Initialise(journey, (JourneyLeg)e.Item.DataItem, IndexOf((JourneyLeg)e.Item.DataItem), TDItineraryManager.Current.JourneyRequest, outward);
                        walkitLink.Visible = walkitLink.IsWalkitLinkAvailable;

                    }
                    else
                    {
                        walkitLink.Visible = false;
                    }

                    if (walkitLink.Visible)
                        walkitLink.Visible = bool.Parse(Properties.Current["WalkitLinkControl.ShowLink"]);

                    if (walkitLink.Visible)
                    {
                        JourneyLeg journeyLeg = (JourneyLeg)e.Item.DataItem;

                        // Fix to not show for accessible journeys where leg is naptan to naptan
                        if (showAccessibleFeatures && (IsFirstLeg(journeyLeg) || (IsLastLeg(journeyLeg))) 
                            && ((journeyLeg.LegStart.Location.NaPTANs.Length > 0 && !journeyLeg.LegStart.Location.NaPTANs[0].Naptan.Equals(originNaptanString) && !journeyLeg.LegStart.Location.NaPTANs[0].Naptan.Equals(destinationNaptanString))
                            && (journeyLeg.LegEnd.Location.NaPTANs.Length > 0 && !journeyLeg.LegEnd.Location.NaPTANs[0].Naptan.Equals(originNaptanString) && !journeyLeg.LegEnd.Location.NaPTANs[0].Naptan.Equals(destinationNaptanString))))
                        {
                            walkitLink.Visible = false;
                        }
                    }

                    if (walkitLink.Visible)
                    {
                        // Map image/button handling
                        buttonMap.Visible = false;
                        walkitImageLink.Visible = !PrinterFriendly;
                        walkitImageLink.NavigateUrl = walkitLink.WalkitUrl;
                        walkitImageLink.ToolTip = alternateTextWalkit;
                        walkitImageLink.Text = GetResource("TransportMode.Walk");
                    }
                    else
                    {
                        walkitImageLink.Visible = false;
                    }

                    #endregion

                    #region CJP User Info
                    cjpUserInfoWalkLength = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoWalkLength");
                    cjpUserLocationNaptanInfoStart = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationNaptanInfoStart");
                    cjpUserLocationCoordinateInfoStart = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationCoordinateInfoStart");
                    cjpUserLocationNaptanInfoEnd = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationNaptanInfoEnd");
                    cjpUserLocationCoordinateInfoEnd = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationCoordinateInfoEnd");
                    cjpUserInfoJourneyLegSource = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoJourneyLegSource");

                    CJPUserInfoHelper journeyInfoHelper = new CJPUserInfoHelper(journey, (JourneyLeg)e.Item.DataItem, IndexOf((JourneyLeg)e.Item.DataItem));
                    cjpUserInfoWalkLength.Initialise(journeyInfoHelper);
                    cjpUserInfoJourneyLegSource.Initialise(journeyInfoHelper);

                    CJPUserInfoHelper startLocationInfoHelper = new CJPUserInfoHelper(((JourneyLeg)e.Item.DataItem).LegStart.Location);
                    CJPUserInfoHelper endLocationInfoHelper = new CJPUserInfoHelper(((JourneyLeg)e.Item.DataItem).LegEnd.Location);

                    cjpUserLocationNaptanInfoStart.Initialise(startLocationInfoHelper);
                    cjpUserLocationCoordinateInfoStart.Initialise(startLocationInfoHelper);
                    cjpUserLocationNaptanInfoEnd.Initialise(endLocationInfoHelper);
                    cjpUserLocationCoordinateInfoEnd.Initialise(endLocationInfoHelper);
                    #endregion

                    break;


				default :
					break;
			}	
		}
		
		/// <summary>
		/// Returns the html table cell entry for the given column
		/// </summary>
		public string HeaderItem(int column)
		{
			if (!ColumnVisible(column))
			{
				return String.Empty;
			}

			string headerItemText = Global.tdResourceManager.GetString("JourneyDetailsTableControl.HeaderItemText" 
																			+ column, TDCultureInfo.CurrentUICulture);

			return "<th class=\"jdtheader" + column.ToString(CultureInfo.InvariantCulture) + "\">" + headerItemText + "</th>";
		}

		/// <summary>
		/// Returns whether the cell should be visible
		/// </summary>
		public bool ColumnVisible(int column)
		{
			switch (column)
			{
				case 3:
				case 7:
					return (flight) || (findAMode == FindAMode.International);
				case 10:
					return !PrinterFriendly && findAMode != FindAMode.International ;
				default:
					return true;
			}
		}

		public string CellStart(int column)
		{
			return (ColumnVisible(column) ? "<td class=\"jdtbody" + column + "\" >" : String.Empty);
		}

		public string CellEnd(int column)
		{
			return (ColumnVisible(column) ? "</td>" : String.Empty);
		}

		/// <summary>
		/// Returns the details text for the specified row and column
		/// </summary>
		public string DetailsItem(int column, int indexRow)
		{
			if (!ColumnVisible(column))
			{
				return string.Empty;
			}

			string detailsItemText = string.Empty;

			switch (column)
			{
				case 1:
					if  (useRoadJourney)
					{
						detailsItemText = "";
					}
					else
					{
						detailsItemText = FormatModeDetails(((JourneyControl.PublicJourney)journey).Details[indexRow]);
					}
					break;
				
				case 3:
					if (!useRoadJourney)
					{
						detailsItemText = GetCheckInTime(((JourneyControl.PublicJourney)journey).Details[indexRow]);
					}
					break;

				case 4:
					detailsItemText = GetDepartTime(journey.JourneyLegs[indexRow], indexRow);
					break;


				case 6:
					detailsItemText = GetArriveTime(journey.JourneyLegs[indexRow], indexRow);
					break;
				
				case 7:
					if (!useRoadJourney)
					{
						detailsItemText = GetExitTime(((JourneyControl.PublicJourney)journey).Details[indexRow]);
					}
					break;

				case 8:
					if (!useRoadJourney)
					{
						detailsItemText = GetDisplayNotes(((JourneyControl.PublicJourney)journey).Details[indexRow]);
					}
					break;

				case 9:
					if  (useRoadJourney)
					{
						detailsItemText = FormatDurationDetails();
					}
					else
					{
						detailsItemText = FormatDurationDetails(((JourneyControl.PublicJourney)journey).Details[indexRow]);
					}
					break;
                case 10:
                    detailsItemText = "<br /><br />";
                    break;
			}

			return detailsItemText;
		}


		/// <summary>
		/// Returns a formatted string containing mode type, service details
		/// (if present) and frequency (if leg is a frequency leg)
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing mode, service and frequency details</returns>
		public string FormatModeDetails(JourneyLeg detail)
		{
			StringBuilder output = new StringBuilder();
			
			// if service details are available, mode will be rendered 
			//  as a hyperlink to ServiceDetails page, otherwise it is 
			//  to be included here as plain text ...

			if	(!HasServiceDetails(detail))    
			{
				output.Append(GetMode(detail, false));
			}
			
			string services = GetServices((PublicJourneyDetail)detail);
			
			if (services.Length > 0) 
			{
				output.Append("<br />" + GetServices((PublicJourneyDetail)detail));
			}
			
			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail = (PublicJourneyFrequencyDetail)detail;

                if (frequencyDetail.MinFrequency == frequencyDetail.MaxFrequency)
                {
                    output.Append("<br />");
                    output.Append(string.Format(
                        everyText,
                        frequencyDetail.MinFrequency,
                        GetResource("JourneyDetailsTableControl.minutesString.Long")));
                }
                else
                {
                    output.Append("<br />");
                    output.Append(string.Format(
                        everyText,
                        string.Format("{0}-{1}", frequencyDetail.MinFrequency, frequencyDetail.MaxFrequency),
                        GetResource("JourneyDetailsTableControl.minutesString.Long")));
                }
			}
			
			return output.ToString();
		}

		/// <summary>
		/// Returns a formatted string containing road journey mode type
		/// </summary>
		/// <returns>Formatted string containing road journey mode type</returns>
		public string FormatModeDetails()
		{
			string resourceManagerKey = "TransportMode." + ((RoadJourney)journey).GetUsedModes()[0].ToString();
			return Global.tdResourceManager.GetString(resourceManagerKey, TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// If the supplied leg is not a rail leg and services details are
		/// present, returns a string containing every service number
		/// delimited by commas. Otherwise an empty string is returned. 
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing service numbers or empty string</returns>
		public string GetServices(PublicJourneyDetail publicJourneyDetail) 
		{
			if (publicJourneyDetail.Mode != ModeType.Rail && 
				publicJourneyDetail.Services != null && 
				publicJourneyDetail.Services.Length > 0) 
			{
				StringBuilder serviceDetailsText = new StringBuilder();
				for (int count=0; count < publicJourneyDetail.Services.Length; count++) 
				{
					serviceDetailsText.Append(publicJourneyDetail.Services[count].ServiceNumber);
					if (count < publicJourneyDetail.Services.Length -1) 
					{
						serviceDetailsText.Append(",");
					}
				}
				return serviceDetailsText.ToString();         
			} 
			else 
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Returns the text for the transport mode of the specified leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Mode string formatted.</returns>
		public string GetMode(JourneyLeg journeyDetail, bool lowerCase)
		{
            string resourceManagerKey = string.Empty;

            if ((journeyDetail.Mode == ModeType.Walk) && plannerOutputAdapter.WalkInterchangeRequired(journeyDetail, journey, showAccessibleFeatures))
            {
                resourceManagerKey =
                (lowerCase ? "TransportModeLowerCase.WalkInterchange" : "TransportMode.WalkInterchange");
            }
            else
            {
                resourceManagerKey = (lowerCase ? "TransportModeLowerCase." : "TransportMode.")
                    + journeyDetail.Mode.ToString();
            }
            
			return Global.tdResourceManager.GetString(
				resourceManagerKey, TDCultureInfo.CurrentUICulture);

		}

		public string GetNaptan(int index, bool startLocation)
		{
			return GetNaptan(journey.JourneyLegs[index], startLocation);
		}

		/// <summary>
		/// Returns the naptan of the start location
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <param name="startLocation"></param>
		/// <returns></returns>
		public string GetNaptan(JourneyLeg journeyDetail, bool startLocation)
		{
			TDNaptan[] naptans = null;
			
			if	(startLocation)
			{
				naptans = journeyDetail.LegStart.Location.NaPTANs;
			}
			else
			{
				naptans = journeyDetail.LegEnd.Location.NaPTANs;
			}

			string firstNaptan = string.Empty; 

			if  (naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length > 0)
			{
				if	(!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString))
				{
					firstNaptan = naptans[0].Naptan;
				}
			}

			return firstNaptan;
		}

		/// <summary>
		/// Returns the From Location string for a journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted from location string</returns>
		public string GetFromLocation(JourneyLeg detail)
		{
			return detail.LegStart.Location.Description;
		}

		/// <summary>
		/// Returns the car park reference of the start location
		/// </summary>
		/// <param name="journeyLeg">Current data item being rendered.</param>
		/// <returns>Car park reference of the end location</returns>
		public string GetStartCarParkRef(JourneyLeg journeyLeg)
		{
			if(journeyLeg.LegStart.Location.CarParking != null)
				return journeyLeg.LegStart.Location.CarParking.CarParkReference;	
			else
				return string.Empty;
		}

		/// <summary>
		/// Returns the car park reference of the end location of the previous leg
		/// </summary>
		/// <param name="index">Index data item currently being rendered.</param>
		/// <returns>Car park reference of the end location</returns>
		public string GetEndCarParkRef(int index)
		{
			if	(index == 0) 
			{
				return string.Empty;
			}
			else
			{
				JourneyLeg detail = journey.JourneyLegs[journey.JourneyLegs.Length  - 1];
				if(detail.LegEnd.Location.CarParking != null)
				{
					return detail.LegEnd.Location.CarParking.CarParkReference;
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Returns the To Location string for a journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted to location string.</returns>
		public string GetToLocation(JourneyLeg detail)
		{
			return detail.LegEnd.Location.Description;
		}

		/// <summary>
		/// Returns the Checkin Time string for a journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted depart time string.</returns>
		public string GetCheckInTime(PublicJourneyDetail detail)
		{
			if (detail.Mode == ModeType.Air && detail.CheckInTime != null)
			{
				return detail.CheckInTime.ToString("HH:mm");
			}
            else if ((findAMode == FindAMode.International) && (detail != null) && (detail.CheckInTime != null))
            {
                return detail.CheckInTime.ToString("HH:mm");
            }
			else
			{
				return "&nbsp;";
			}
		}

		/// <summary>
		/// Returns the Depart Time string for a journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted depart time string.</returns>
		public string GetDepartTime (JourneyLeg detail, int indexRow)
		{	
			// Perform castings
			PublicJourneyContinuousDetail continuousDetail = (detail as PublicJourneyContinuousDetail);
			PublicJourneyFrequencyDetail frequencyDetail = (detail as PublicJourneyFrequencyDetail);
			PublicJourneyDetail publicJourneyDetail = (detail as PublicJourneyDetail);

			// Don't display depart time for Walk or other continuous leg
			// or for frequency based services, unless it is the first leg.
			if (frequencyDetail != null || continuousDetail != null)
			{
				if (detail.LegStart.DepartureDateTime.Year == 1 || indexRow != 0)
				{
					return "&nbsp;";
				}
				else
				{
                    return FormatDateTime(detail.LegStart.DepartureDateTime);
				}
			}
			else if (publicJourneyDetail != null && (detail.Mode == ModeType.Air))
			{
                return FormatDateTime(publicJourneyDetail.FlightDepartDateTime);
			}
			else
			{
                return FormatDateTime(detail.LegStart.DepartureDateTime);
			}
		}


		/// <summary>
		/// Returns the arrive time string for a journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted from arrive timestring.</returns>
		public string GetArriveTime(JourneyLeg detail, int indexRow)
		{
			// Perform castings
			PublicJourneyContinuousDetail continuousDetail = (detail as PublicJourneyContinuousDetail);
			PublicJourneyFrequencyDetail frequencyDetail = (detail as PublicJourneyFrequencyDetail);
			PublicJourneyDetail publicJourneyDetail = (detail as PublicJourneyDetail);

			// Don't display arrive time for Walk leg
			if (frequencyDetail != null || continuousDetail != null)
			{
				if ((detail.LegEnd.ArrivalDateTime.Year == 1)||(indexRow != journey.JourneyLegs.Length - 1 ))
				{
					return "&nbsp;";
				}
				else
				{
                    return FormatDateTime(detail.LegEnd.ArrivalDateTime);
				}
			}
			else if (publicJourneyDetail != null && (detail.Mode == ModeType.Air))
			{
                return FormatDateTime(publicJourneyDetail.FlightArriveDateTime);
			}
			else 
			{
				// Round the arrival time
                return FormatDateTime(detail.LegEnd.ArrivalDateTime);
			}
		}

        /// <summary>
        /// Returns the formatted arrive/depart time.
        /// </summary>
        /// <param name="time">Datetime to format.</param>
        /// <returns>Formatted date time</returns>
        public string FormatDateTime(TDDateTime time)
        {
            int requestedDay = 0;

            // getting requested day for journey planner
            if (TDSessionManager.Current.IsFindAMode && TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest != null)
            {
                if ((outward) && (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime != null))
                {
                    requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0].Day;
                }
                else if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime != null)
                {
                    requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0].Day;
                }
            }

            if ((TDSessionManager.Current.FindAMode == FindAMode.None) && (TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.VisitPlanner))
            {
                if ((TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.ExtendJourney) && (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest != null))
                {
                    if ((outward) && (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime != null))
                    {
                        requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0].Day;
                    }
                    else if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime != null)
                    {
                        requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0].Day;
                    }
                }
                else
                {
                    if ((outward) && (TDSessionManager.Current.InputPageState.OriginalOutwardDateTime != null))
                    {
                        requestedDay = TDSessionManager.Current.InputPageState.OriginalOutwardDateTime.Day;
                    }
                    else if (TDSessionManager.Current.InputPageState.OriginalReturnDateTime != null)
                    {
                        requestedDay = TDSessionManager.Current.InputPageState.OriginalReturnDateTime.Day;
                    }
                }
            }

            if (requestedDay == 0)
            {
                // keeping the requested day same as actual day
                requestedDay = time.Day;
            }

            if (time.Second >= 30)
                time = time.AddMinutes(1);

            // Check to see if the date is different from the request date.
            // For example, if the user has searched for a journey commencing on
            // a Sunday, but the first available train is on a Monday
            int actualDay = time.Day;
            if ((actualDay != requestedDay))
            {
                // Days are different, return the time with the dates appended
                string date = time.ToString("dd/MM");
                return DisplayFormatAdapter.StandardTimeFormat(time) + "<br />(" + date + ")";
            }
            else
            {
                // Dates are the same, simply return the time.
                return DisplayFormatAdapter.StandardTimeFormat(time);
            }
        }

		/// <summary>
		/// Returns the Exit Time string for a journey
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted depart time string.</returns>
		public string GetExitTime(PublicJourneyDetail detail)
		{
			if (detail.Mode == ModeType.Air && detail.ExitTime != null)
			{
				return detail.ExitTime.ToString("HH:mm");
			}
            else if (findAMode == FindAMode.International && detail.ExitTime != null)
            {
                return detail.ExitTime.ToString("HH:mm");
            }
            else
            {
                return "&nbsp;";
            }
		}

		/// <summary>
		/// If the specified leg is a frequency leg, returns a formatted 
		/// string containing maximum and typical duration times. Otherwise
		/// returns the duration time for the leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing duration details</returns>
		public string FormatDurationDetails(PublicJourneyDetail detail) 
		{
			if (detail is PublicJourneyFrequencyDetail) 
			{
				return "<br />" + GetMaxJourneyDurationText(detail) +
					"<br />" + GetTypicalDurationText(detail);
			} 
			else
			{
				return GetDuration(detail.Duration);
			}
		}

		/// <summary>
		/// Returns the Duration string for a road journey
		/// </summary>
		/// <returns>Formatted string containing duration details</returns>
		public string FormatDurationDetails() 
		{
			return GetDuration(((RoadJourney)journey).TotalDuration);
		}

		/// <summary>
		/// Returns formatted string containing the maximum duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the maximum duration or empty string</returns>
		public string GetMaxJourneyDurationText(PublicJourneyDetail detail)
		{
			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail =
					(PublicJourneyFrequencyDetail)detail;

				return maxDurationText + ": " + frequencyDetail.MaxDuration +
					(frequencyDetail.MaxDuration > 1 ? minsText : minText);
			} 
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns formatted string containing the typical duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the typical duration or empty string</returns>        
		public string GetTypicalDurationText(PublicJourneyDetail detail)
		{
			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail =
					(PublicJourneyFrequencyDetail)detail;

				return typicalDurationText + ": " + frequencyDetail.Duration +
					(frequencyDetail.Duration > 1 ? minsText : minText);
			} 
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the Duration string
		/// </summary>
		/// <param name="durationInSeconds">Total duration in seconds</param>
		/// <returns>Formatted duration string</returns>
		public string GetDuration(long durationInSeconds)
		{
			// Get the minutes
			double durationInMinutes = durationInSeconds / 60;

			// Check to see if seconds is less than 30 seconds.
			if( durationInMinutes / 60.0  < 1.00 &&
				durationInMinutes % 60.0 < 0.5 )
			{
				return "< 30 " + secondsText;
			}
			else
			{
				// Round to the nearest minute
				durationInMinutes = Round(durationInMinutes);

				// Calculate the number of hours in the minute
				int hours = (int)durationInMinutes / 60;

				// Get the minutes (afer the hours has been subracted so always < 60)
				int minutes = (int)durationInMinutes % 60;

				// If greater than 1 hour - retrieve "hours", if 1 or less, retrieve "hour"
				string hourString = hours > 1 ? hoursText : hourText;

				// If greater than 1 minute - retrive "minutes", if 1 or less, retrieve "minute"
				string minuteString = minutes > 1 ? minsText : minText;
        
				string formattedString = string.Empty;

				if(hours > 0)
					formattedString += hours + " " + hourString + " ";

				formattedString += minutes + " " + minuteString;

				return formattedString;
			}
		}

		/// <summary>
		/// Rounds the given double to the nearest int.
		/// If double is 0.5, then rounds up.
		/// Using this instead of Math.Round because Math.Round
		/// ALWAYS returns the even number when rounding a .5 -
		/// this is not behaviour we want.
		/// </summary>
		/// <param name="valueToRound">Value to round.</param>
		/// <returns>Nearest integer</returns>
		private static int Round(double valueToRound)
		{
			// Get the decimal point
			double valueFloored = Math.Floor(valueToRound);
			double remain = valueToRound - valueFloored;

			if  (remain >= 0.5)
				return (int)Math.Ceiling(valueToRound);
			else
				return (int)Math.Floor(valueToRound);
		}

		/// <summary>
		/// Checks if current journey detail has additional service details to be dispalyed.
		/// Currently based on detail mode only - returning true for rail or rail replacement bus.
		/// </summary>
		private bool HasServiceDetails(JourneyLeg detail)
		{
            bool internationalCoach = (findAMode == FindAMode.International && detail.Mode == ModeType.Coach);
			return (detail.Mode == ModeType.Rail || detail.Mode == ModeType.RailReplacementBus || detail.Mode == ModeType.Car || internationalCoach);
		}

		/// <summary>
		/// Returns the index of the given leg details
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Index of the data</returns>
		private int IndexOf(JourneyLeg detail)
		{
			int index=0;

			for(index=0; index < journey.JourneyLegs.Length; index++)
			{
				if  (journey.JourneyLegs[index] == detail)
				{
					break;
				}
			}

			return index;
		}

        /// <summary>
        /// Returns true if the given legDetail is the first leg of the journey, and false if not.
        /// </summary>
        /// <param name="legDetails">Leg data to check.</param>
        /// <returns>True or false.</returns>
        private bool IsFirstLeg(JourneyLeg detail)
        {
            return journey.JourneyLegs[0] == detail;
        }

        /// <summary>
        /// Returns true if the given legDetail is the last leg of the journey, and false if not.
        /// </summary>
        /// <param name="legDetails">Leg data to check.</param>
        /// <returns>True or false.</returns>
        private bool IsLastLeg(JourneyLeg detail)
        {
            return journey.JourneyLegs[journey.JourneyLegs.Length - 1] == detail;
        }

		/// <summary>
		/// Read only property, returning true if end location is car park 
		/// </summary>
		public bool IsEndCarPark
		{
			get
			{
				return isEndCarPark;
			}
		}

		/// <summary>
		/// Read only property, returning true if start location is car park 
		/// </summary>
		public bool IsStartCarPark
		{
			get
			{
				return isStartCarPark;
			}
		}

		/// <summary>
		/// Returns whether the leg start station name should be rendered as a hyperlink
		/// </summary>
		public bool StartLocationInfoAvailable(JourneyLeg detail)
		{
			// The location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the start location of leg being currently rendered.
			// Or its not a printable page and there is a car park 
			// reference available.
			JourneyLeg journeyLeg = journey.JourneyLegs[0];
			
			if ( (detail == journeyLeg) && (detail.LegStart.Location.CarParking != null) )
			{
				isStartCarPark = true;
			}

            if (findAMode != FindAMode.International)
            {
                return ((!PrinterFriendly && (GetNaptan(detail, true).Length > 0))
                    || (!PrinterFriendly && isStartCarPark));
            }
            else
            {
                if (detail.LegStart.Location.Country.CountryCode == "GB")
                {
                    return (!PrinterFriendly && (GetNaptan(detail, true).Length > 0));
                }
                else
                {
                    return (!PrinterFriendly && (!string.IsNullOrEmpty(detail.LegStart.InformationURL)));
                }
                
            }
		}

		/// <summary>
		/// Returns whether the end start station name should be rendered as a hyperlink
		/// </summary>
		public bool EndLocationInfoAvailable(JourneyLeg detail)
		{
			// The location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the start location of leg being currently rendered.
			JourneyLeg journeyLeg = journey.JourneyLegs[journey.JourneyLegs.Length - 1];
			
			if ( (detail == journeyLeg) && (journeyLeg.LegEnd.Location.CarParking != null) )
			{
				isEndCarPark = true;
			}

            if (findAMode != FindAMode.International)
            {
                return ((!PrinterFriendly && (GetNaptan(detail, false).Length > 0))
                    || (!PrinterFriendly && isEndCarPark));
            }
            else
            {
                if (detail.LegEnd.Location.Country.CountryCode == "GB")
                {
                    return (!PrinterFriendly && (GetNaptan(detail, false).Length > 0));
                }
                else
                {
                    return (!PrinterFriendly && (!string.IsNullOrEmpty(detail.LegEnd.InformationURL)));
                }
            }
		}


		/// <summary>
		/// Get property - returns the text label for the map button.
		/// </summary>
		public string MapButtonText
		{
			get
			{
				return buttonTextMapButton;
			}
		}
        /// <summary>
        /// Get property - returns the text label for the map button.
        /// </summary>
       public string DepartureBoardButtonText
        {
            get
            {
                return buttonTextDepartureBoardButton;
            }
        }
        public string ArrivalBoardButtonText
        {
            get
            {
                return buttonTextArrivalBoardButton;
            }
        }


		/// <summary>
		/// Returns true if map button should be displayed, false otherwise
		/// </summary>
		/// <param name="indexRow"></param>
		/// <returns>map button visibility</returns>
		public bool MapButtonVisible(int indexRow)
		{
			if	(useRoadJourney)
			{
				return !PrinterFriendly;
			}
			else
			{
                return !(PrinterFriendly || journey.JourneyLegs[indexRow].HasInvalidCoordinates);
			}
		}
        /// <summary>
        /// Returns true if DepartureBoard button should be displayed, false otherwise
        /// </summary>
        /// <param name="indexRow"></param>
        /// <returns>DepartureBoard button visibility</returns>
        public bool DepartureBoardButtonVisible(int indexRow)
        {
            bool DB_Visible = false;
            DB_Visible = (
                ( journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.Rail)
                 || journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.Airport)
                 || journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.RailNoGroup)
                 || journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.AirportNoGroup)
                )
                       && !(PrinterFriendly
                            || journey.JourneyLegs[indexRow].HasInvalidCoordinates
                            || PageId == PageId.JourneyAdjust
                           )
                       && (journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Air
                            || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail
                            || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.RailReplacementBus
                        )
                       );

            if (findAMode == FindAMode.International && !DB_Visible)
            {
                DB_Visible = ((journey.JourneyLegs[indexRow].Mode == ModeType.Air
                                || journey.JourneyLegs[indexRow].Mode == ModeType.Rail
                                || journey.JourneyLegs[indexRow].Mode == ModeType.Coach)
                                && !string.IsNullOrEmpty(journey.JourneyLegs[indexRow].LegStart.DepartureURL)
                                && !PrinterFriendly);
            }

            return DB_Visible;
            
        }
        /// <summary>
        /// Returns true if ArrivalBoard button should be displayed, false otherwise
        /// </summary>
        /// <param name="indexRow"></param>
        /// <returns>ArrivalBoard button visibility</returns>
        public bool ArrivalBoardButtonVisible(int indexRow)
        {
            bool DB_Visible = false;
            DB_Visible = (
                (journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.Rail)
               || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.Rail)
               || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.Airport)
               || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.RailNoGroup)
               || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.AirportNoGroup)
                )
                       && !(PrinterFriendly
                            || journey.JourneyLegs[indexRow].HasInvalidCoordinates
                            || PageId == PageId.JourneyAdjust
                           )
                       && (journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Air
                            || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail
                            || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.RailReplacementBus
                        )
                       );

            if (findAMode == FindAMode.International && !DB_Visible)
            {
                if (indexRow > 0)
                {
                    JourneyLeg leg = journey.JourneyLegs[indexRow];
                    DB_Visible = ((leg.Mode == ModeType.Air 
                                || leg.Mode == ModeType.Coach 
                                || leg.Mode == ModeType.Rail)
                                && (findAMode == FindAMode.International)
                                && (!string.IsNullOrEmpty(leg.LegEnd.ArrivalURL))
                                && !PrinterFriendly
                                );
                }
            }
            
            return DB_Visible;

        }
        
        #region methods to make popup not blocked by popupblockers
        /// <summary>
        /// Returns the Javascript function to execute when the button is clicked
        /// </summary>
        /// <returns>string</returns>
        /// <param name="url">url</param>
        protected string GetAction(string url)
        {
            return "return OpenWindow('" + url + "');";
        }

        /// <summary>
        /// Returns the name of the Javascript file containing the code to execute when the 
        /// button is clicked
        /// </summary>
        /// <returns></returns>
        protected string GetScriptName()
        {
            return JAVASCRIPT_FILE;
        }

        #endregion

		/// <summary>
		/// Returns the command name that should be associated with the map button.
		/// </summary>
		/// <returns>Returns the command name.</returns>
		public string GetCommandName(int index)
		{
			return index.ToString(TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Returns the command argument that should be associated with the mode type.
		/// </summary>
		/// <param name="journeyLeg">Current item being rendered.</param>
		/// <returns>Returns the command argument</returns>
		public string GetCommandArgument(JourneyLeg journeyLeg)
		{
			return journeyLeg.Mode.ToString();
		}

		/// <summary>
		/// Get property - journey object to give information about what legs on this journey
		/// </summary>
		public JourneyControl.Journey Journey
		{
			get { return journey; }
		}

		/// <summary>
		/// Read/write property returning list of unique car park references
		/// </summary>
		public ArrayList UniqueNameList
		{
			get { return uniqueNameList; }
			set { uniqueNameList = value; }
		}

		/// <summary>
		/// Read/write property returning true if control should use array list of
		/// unique car park references
		/// </summary>
		public bool UseNameList
		{
			get{ return useNameList; }
			set{ useNameList = value; }
		}


		/// <summary>
		/// Gets Display Notes
		/// </summary>
		/// <param name="detail">Journey Detail</param>
		/// <returns>HTML Formatted Notes</returns>
		public string GetDisplayNotes(JourneyLeg detail)
		{
			NotesDisplayAdapter notesDisplayAdapter = new NotesDisplayAdapter();

			return	notesDisplayAdapter.GetDisplayableNotes(journey, detail); 
		}

        /// <summary>
        /// Read Only.
        /// Determines if the cjp info summary div should be visible
        /// </summary>
        public bool IsCJPInfoSummaryAvailable
        {
            get
            {
                return CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.TrunkExchangePoint)
                    || CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.InterchangeTime);
            }
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
        
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.detailsRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.detailsRepeater_ItemCreated);

		}
		#endregion
	}
}

