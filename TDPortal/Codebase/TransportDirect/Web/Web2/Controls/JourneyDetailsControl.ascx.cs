// *********************************************** 
// NAME                 : JourneyDetailsControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 20/08/2003
// DESCRIPTION          : A custom control to display
// details of a given journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyDetailsControl.ascx.cs-arc  $
//
//   Rev 1.6   Dec 05 2012 13:48:00   mmodi
//Populate for showing accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Feb 12 2010 11:13:30   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Nov 15 2009 11:05:10   mmodi
//Updated to add map button javascript to zoom the map to the selected leg
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 11 2009 16:42:48   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.2   Mar 31 2008 13:21:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:16   mturner
//Initial revision.
//
//   Rev 1.87   Oct 11 2007 11:15:50   asinclair
//Added JourneyLegCount int and property and code to set the value
//Resolution for 4513: 9.8 - Journey Leg Numbering
//
//   Rev 1.86   Oct 06 2006 14:37:58   mturner
//Merge for stream SCR-4143
//
//   Rev 1.85.1.0   Sep 26 2006 16:41:58   esevern
//Added setting of unique car park references list so that car parking opening times note is only displayed for the inital occurence of a car park name
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.85   Apr 10 2006 11:02:52   mdambrine
//Refine details fix, if a private journey is the only segment in the list we need to deduct the offset to make the map show the entire journey instead of the first entry in the list when the show map button for that leg is clicked
//Resolution for 3824: DN068 Extend: Map of car route shows 'Direction 1' rather than whole journey by default
//
//   Rev 1.84   Mar 13 2006 16:43:58   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.82.3.0   Jan 26 2006 20:34:38   rhopkins
//Changed to use Journey, rather than subclass PublicJourney - this allows proper mode-agnostic handling of Road and PT journeys.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.82   Aug 19 2005 14:07:12   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.81.1.3   Aug 16 2005 15:12:32   RPhilpott
//FxCop fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.81.1.2   Aug 02 2005 19:39:38   RPhilpott
//Allow for non-naptan based requests having naptans in locations (after extension).
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.81.1.1   Jul 28 2005 10:46:00   RPhilpott
//Include Naptans in transformed road journey.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.81.1.0   Jul 28 2005 10:36:12   RPhilpott
//Use new calling points on PublicJourneyDetail
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.81   Apr 26 2005 13:17:58   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.80   Apr 22 2005 16:01:58   pcross
//IR2192. Now consumes and raises event when map button is pressed on hosted control
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.79   Mar 02 2005 10:54:26   rscott
//Updated to rectify problem with TDDateTime[ ]
//
//   Rev 1.78   Sep 30 2004 12:01:36   rhopkins
//IR1648 Use ReturnOriginLocation and ReturnDestinationLocation when outputting Return Car segments of Extended Journeys
//
//   Rev 1.77   Sep 17 2004 15:13:46   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.76   Sep 13 2004 11:33:36   RHopkins
//IR1571 Changed logic that determines which segment to use for maps/info when the User is viewing Full Itinerary.
//
//   Rev 1.75   Jul 28 2004 16:01:40   rgreenwood
//IR1105 - Updated detailsRepeater_ItemDataBound method
//
//   Rev 1.74   Jul 12 2004 15:13:56   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.73   Jun 23 2004 15:34:10   JHaydock
//Updates/Corrections for JourneyDetails page
//
//   Rev 1.72   Jun 17 2004 17:20:26   jbroome
//Passes selected itinerary journey index into initialisation method of JourneyDetailsSegmentsControl
//
//   Rev 1.71   Jun 16 2004 15:24:20   COwczarek
//Correctly calculate road journey start/end times
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.70   Jun 16 2004 09:24:40   jbroome
//Extend Journey. Removed majority of functionality and display to new DisplayDetailsSegmentsControl for showing multiple journeys.
//
//   Rev 1.69   Jun 15 2004 10:20:36   jgeorge
//Work in progress - amended for displaying itineraries
//
//   Rev 1.60   Apr 19 2004 10:43:58   COwczarek
//Display new transport mode icon for rail replacement bus
//Resolution for 697: Bus replacement change
//
//   Rev 1.59   Apr 06 2004 13:43:12   AWindley
//DEL5.2 QA Changes: Resolution for 729
//
//   Rev 1.58   Mar 16 2004 17:37:30   PNorell
//Updated for timings calculation.
//
//   Rev 1.57   Feb 19 2004 17:21:46   COwczarek
//Changes for new DEL 5.2 display format, including display of frequency based legs
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.56   Dec 09 2003 10:12:58   kcheung
//Walk to string added for JourneyDetailsTable control - Del 5.1 update.
//
//   Rev 1.55   Dec 08 2003 17:09:20   kcheung
//Removed Depart/Arrive header for Del 5.1
//
//   Rev 1.54   Dec 08 2003 16:38:40   kcheung
//Journey Details display updated for Del 5.1
//
//   Rev 1.53   Dec 04 2003 18:37:32   kcheung
//Del 5.1 updates
//
//   Rev 1.52   Nov 26 2003 16:46:52   kcheung
//Fixed time rounding errors.
//Previously using Math.Round - this always rounds to the  nearest EVEN number when the value to round is a point 5.  This means if a duration is 1:30, you get 2 minutes, and when it is 2:30 you also get 2 minutes - which creates inconsistency.
//Custom round method has been added to remove this problem.
//
//   Rev 1.51   Nov 26 2003 11:37:26   passuied
//retrieved channel language to pass to ValidateAndRun
//Resolution for 397: Wrong language passed to JW
//
//   Rev 1.50   Nov 25 2003 18:21:20   COwczarek
//SCR#145: CSS style does not appear correctly in Mozilla browsers  
//Resolution for 145: CSS style does not appear correctly in Mozilla browsers
//
//   Rev 1.49   Nov 05 2003 12:22:04   kcheung
//Added property header
//
//   Rev 1.48   Nov 05 2003 10:12:02   kcheung
//Fixed font size of arrival time text.  Put : back into times as requested in QA
//
//   Rev 1.47   Nov 04 2003 15:04:44   kcheung
//Updated so that if a leg is less than 30 seconds, it will displayed as < 30 secs as requested in QA
//
//   Rev 1.46   Oct 28 2003 10:38:04   kcheung
//Added CultureInfo paramter to ToString and Convert method calls to satisfy FXCOP
//
//   Rev 1.45   Oct 22 2003 12:20:16   RPhilpott
//Improve CJP error handling
//
//   Rev 1.44   Oct 21 2003 18:10:52   kcheung
//Tidied up for FXCOP
//
//   Rev 1.43   Oct 20 2003 10:39:50   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.42   Oct 17 2003 16:56:54   kcheung
//Fixed for FXCOP comments
//
//   Rev 1.41   Oct 16 2003 17:53:32   kcheung
//Fixed so that Original view resets the drop down box
//
//   Rev 1.40   Oct 15 2003 11:19:42   kcheung
//Fixed the alt text because a full stop was missing in the key string,
//
//   Rev 1.39   Oct 13 2003 14:27:04   kcheung
//Fixed alt tags
//
//   Rev 1.38   Oct 11 2003 16:55:52   RPhilpott
//Tidy up output of instructions for leg when no service info available.
//
//   Rev 1.37   Oct 09 2003 17:49:56   kcheung
//Updated to make "i" button visible only if a naptan ID exists for the leg. Updated so that "i" appears for destination location if naptan exists.
//
//   Rev 1.36   Oct 09 2003 14:25:52   kcheung
//Fixed information button to get the naptan of the start location instead of the end location.
//
//   Rev 1.35   Oct 09 2003 13:59:40   PNorell
//Fixed small bugs with outlook.
//
//   Rev 1.34   Oct 06 2003 16:18:24   PNorell
//Updated when and how icons are shown.
//
//   Rev 1.33   Oct 01 2003 16:08:14   PNorell
//Small bugfix with off-by-one index.
//
//   Rev 1.32   Sep 30 2003 11:32:48   PNorell
//Corrected printable outlook.
//Added support for moving outside the screen flow state as is needed by some printable pages.
//
//   Rev 1.31   Sep 29 2003 16:03:52   kcheung
//Added code to make map button disappear when printable
//
//   Rev 1.30   Sep 25 2003 11:47:50   PNorell
//Fixed bug with how the original and adjusted journey was handled.
//
//   Rev 1.29   Sep 23 2003 18:45:58   PNorell
//Updated and bugfixes with event handling and transition stages.
//
//   Rev 1.28   Sep 23 2003 14:49:46   PNorell
//Updated page states and the wait page to function according to spec.
//Updated the different controls to ensure they have correct PageId and that they call the ValidateAndRun properly.
//Removed some 'warning' messages - a clean project is nice to see.
//
//   Rev 1.27   Sep 22 2003 18:57:20   PNorell
//Updated all transition events and page ids and interaction events.
//
//   Rev 1.26   Sep 22 2003 17:20:40   PNorell
//Integrated help controls and associated resources.
//Fixed bug in event handling in JourneyDetails.
//
//   Rev 1.25   Sep 19 2003 19:58:02   PNorell
//Updated all journey details screens.
//Support for Adjusted journeys added and Validate And Run.
//
//   Rev 1.24   Sep 18 2003 11:03:58   PNorell
//Fixed reference to use interface instead of concrete implimentation.
//
//   Rev 1.23   Sep 16 2003 18:07:02   kcheung
//Updated
//
//   Rev 1.22   Sep 15 2003 16:08:10   kcheung
//Updated
//
//   Rev 1.21   Sep 12 2003 12:15:40   kcheung
//Updated
//
//   Rev 1.20   Sep 11 2003 17:14:12   kcheung
//Updated 
//
//   Rev 1.19   Sep 11 2003 10:58:36   kcheung
//Updated - added new mode
//
//   Rev 1.18   Sep 10 2003 15:25:08   kcheung
//Updated to make it work with TDPage... need to get and bind the data twice
//
//   Rev 1.17   Sep 09 2003 11:29:52   kcheung
//Updated Initialise header text.
//
//   Rev 1.16   Sep 05 2003 11:19:48   kcheung
//Included skeleton code to 'Adjust Journey'
//
//   Rev 1.15   Sep 04 2003 15:14:22   kcheung
//Updated outward / return flag errors
//
//   Rev 1.14   Sep 03 2003 17:10:52   kcheung
//Removed object cast from class and added it into the HTML
//
//   Rev 1.13   Sep 02 2003 12:42:14   kcheung
//Updated 
//
//   Rev 1.12   Sep 02 2003 10:55:06   kcheung
//Updated - good working version after integration with Journey Control - TODO - integration with HTML stuff and pictures.  Button handlers need to be updated later with information and map stuff.
//
//   Rev 1.11   Sep 01 2003 17:18:44   kcheung
//Updated working version
//
//   Rev 1.10   Aug 29 2003 16:46:12   kcheung
//Updated - working version for integration
//
//   Rev 1.9   Aug 29 2003 13:11:24   kcheung
//Updated for integration
//
//   Rev 1.8   Aug 20 2003 15:14:08   kcheung
//Minor update to Rowpadding
//
//   Rev 1.7   Aug 20 2003 14:47:36   kcheung
//Updated - stable working version

#region Using Statements

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Logger = System.Diagnostics.Trace;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Adapters;

#endregion


namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to render the details of a given journey.
    /// </summary>
	public partial  class JourneyDetailsControl : TDUserControl
	{
		protected JourneyDetailsSegmentsControl journeyDetailsSegment;

		// Data for the Repeater.
		private JourneyControl.Journey[] journeys;

		private bool compareMode;
		private bool adjustable;
		private bool stationInfo;
		private bool printable;
		protected Repeater journeySegmentsRepeater;
		private ArrayList uniqueCarParkRefs;
        private ITDJourneyRequest jr;

		private bool outward = true;
		private PageId belongingPageId = PageId.Empty;

		private int journeyLegCount;
        private FindAMode findAMode = FindAMode.None;

        // Variables needed to add javascript to the map button click
        private bool addMapJavascript = false;
        private string mapId = "map";
        private string mapJourneyDisplayDetailsDropDownId = "mapdropdown";
        private string scrollToControlId = "mapControl";
        private string sessionId = "session";

		#region Public Events

		// Event to fire to pass through that the map button has been pressed on segment control
		public event MapButtonClickEventHandler MapButtonClicked;
		
		#endregion

		#region Event Handlers

		/// <summary>
		/// Event Handler for the map button (pressed on JourneyDetailsSegmentControl)
		/// </summary>
		public void JourneyDetailsSegmentMapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
			// Find index of journey and adjust leg index accordingly
			JourneyDetailsSegmentsControl selectedSegment = sender as JourneyDetailsSegmentsControl;
			
			int offset = 0;

			foreach (RepeaterItem currentItem in detailsRepeater.Items)
			{
				JourneyDetailsSegmentsControl currentSegment = (JourneyDetailsSegmentsControl)currentItem.FindControl("journeyDetailsSegment");
				if (currentSegment.Equals(selectedSegment))
					break;
				else
				{
					if ((currentSegment.Journey as JourneyControl.PublicJourney) != null)
					{
						offset += ((JourneyControl.PublicJourney)currentSegment.Journey).Details.Length;
					}
					else
					{												
						offset++;						
					}
				}
			}
			
			// Refine details fix, if a private journey is the only segment in the list we need to deduct the offset 
			// to make the map show the entire journey instead of the first entry in the list
			if (detailsRepeater.Items.Count == 1 && (selectedSegment.Journey as JourneyControl.PublicJourney) == null)
			{
				offset--;
			}

			// Raise event so the click can be handled in the journey details form and the map displayed
			// as part of that form
			MapButtonClickEventHandler eventHandler = MapButtonClicked;
			if (eventHandler != null)
				eventHandler(this, new MapButtonClickEventArgs( e.LegIndex + offset ));
		}

		#endregion

        #region Initialisation and initialisation properties
		/// <summary>
		/// Initialises the control. This method must be called.
		/// </summary>
		/// <param name="adjustable">Indicates if the control is being rendered in adjust mode.</param>
		/// <param name="compareMode">Indicates if the control is being rendered in compare mode.</param>
		/// <param name="outward">Indicates if the control is being rendered for outward or return journey.</param>
		/// <param name="journey">Journey to render journey for.</param>
		/// <param name="stationInfo">Inidicates if station info button should be displayed.</param>
		public void Initialise(
			JourneyControl.Journey journey, bool outward, bool compareMode, bool adjustable, bool stationInfo, ITDJourneyRequest jr, FindAMode findAMode)
		{
			// All stored in internal viewstate so that initialise is called only once
			this.journeys = new JourneyControl.Journey[] {journey};
			this.outward = outward;
			this.compareMode = compareMode;
			this.adjustable = adjustable;
			this.stationInfo = stationInfo;
            this.jr = jr;
            this.findAMode = findAMode;

			uniqueCarParkRefs = new ArrayList();
		
			detailsRepeater.DataSource = this.journeys;
			detailsRepeater.DataBind();
		}

		/// <summary>
		/// Used to initialise the control when the itinerary is being displayed. 
		/// </summary>
		/// <param name="outward">Indicates if the control is being rendered for outward or return journey.</param>	
		/// <param name="compareMode">Indicates if the control is being rendered in compare mode.</param>
		/// <param name="adjustable">Indicates if the control is being rendered in adjust mode.</param>	
		/// <param name="stationInfo">Inidicates if station info button should be displayed.</param>
        public void Initialise(bool outward, bool compareMode, bool adjustable, bool stationInfo, FindAMode findAMode)
		{
			this.outward = outward;
            
			JourneyControl.Journey[] aJourneys = getItineraryJourneys();

			uniqueCarParkRefs = new ArrayList();

			this.journeys = aJourneys;
			this.compareMode = compareMode;
			this.adjustable = adjustable;
			this.stationInfo = stationInfo;
            this.findAMode = findAMode;

			detailsRepeater.DataSource = this.journeys;
			detailsRepeater.DataBind();
		}

        /// <summary>
        /// Method which sets the values needed to add map javascript to the map buttons
        /// </summary>
        /// <param name="mapId">The id of the map to zoom in</param>
        /// <param name="scrollToControlId">The id of the control page should scroll to</param>
        /// <param name="sessionId">The session id to use when zooming to the journey on the map</param>
        public void SetMapProperties(string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            this.mapId = mapId;
            this.mapJourneyDisplayDetailsDropDownId = mapJourneyDisplayDetailsDropDownId;
            this.scrollToControlId = scrollToControlId;
            this.sessionId = sessionId;
        }

		#endregion

		#region Itinerary journeys handling routines.

		private bool usingItinerary()
		{
			return (TDItineraryManager.Current.Length > 0 && !TDItineraryManager.Current.ExtendInProgress);
		}
		
		/// <summary>
		/// This method retrieves all the journeys stored in the Itinerary Manager 
		/// as an array of Journey objects which can be data bound to the 
		/// detailsRepeater control.
		/// </summary>
		/// <returns>Array of Journey objects</returns>
		private JourneyControl.Journey[] getItineraryJourneys()
		{
			TDItineraryManager itinerary = TDItineraryManager.Current;
			return (outward) ? itinerary.OutwardJourneyItinerary : itinerary.ReturnJourneyItinerary;
		}

		#endregion

		#region Public properties
		/// <summary>
		/// Set and get property if this component is in printable mode or not.
		/// </summary>
		public bool Printable
		{
			get 
			{
				return printable;
			}

			set
			{
				printable = value;
			}
		}

		/// <summary>
		/// Set and get property for the number of Journey legs in the total journey.
		/// </summary>
		public int JourneyLegCount
		{
			get 
			{
				return journeyLegCount;
			}

			set
			{
				journeyLegCount = value;
			}
		}

		/// <summary>
		/// Get/Set property - get or sets the page Id. This should be the page Id
		/// of the page that contains this control.
		/// </summary>
		public PageId MyPageId
		{
			get
			{
				return belongingPageId;
			}

			set
			{
				belongingPageId = value;
			}
		}

        #endregion

        #region OnLoad / Page Load / OnPreRender Methods
            
		/// <summary>
		/// Page Load.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			detailsRepeater.DataSource = this.journeys;
			detailsRepeater.DataBind();
		}

		/// <summary>
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
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
			this.detailsRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.detailsRepeater_ItemDataBound);
		}
        #endregion

		#region Databinding routines

		/// <summary>
		/// This method is called by the ItemDataBound event handler of the detailsRepeater.
		/// When the data binding is occuring for each item in the repeater, a check is made 
		/// to determine the status of the journey in question. The associated JourneyDetailsSegmentsControl
		/// within the Item Template can then be initialised as appropriate.
		/// This is to ensure that the "Start" label is only displayed for the first journey in the array
		/// and the "End" label is only displayed for the last journey in the array.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			int journeyItineraryIndex = -1;
			if (usingItinerary())
			{
				if (!TDItineraryManager.Current.FullItinerarySelected)
				{
					// Showing only a single journey segment, so use the specific segment from
					// TDItineraryManager
					journeyItineraryIndex = TDItineraryManager.Current.SelectedItinerarySegment;
				}
				else
				{
					if (outward)
					{
						// Use the index number of the button clicked
						journeyItineraryIndex = e.Item.ItemIndex;
					}
					else
					{
						// Showing return journey so calculate the correct corresponding leg
						journeyItineraryIndex = (TDItineraryManager.Current.Length - 1) - e.Item.ItemIndex;
					}
				}
			}

            // Journey to display
            JourneyControl.Journey journey = (JourneyControl.Journey)e.Item.DataItem;

            // Set the map buttons addJavascript flag from session
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            addMapJavascript = outward ? viewState.OutwardShowMap : viewState.ReturnShowMap;

            // Set the show accessible flags
            bool showAccessibleFeatures = false;
            bool showAccessibleAssistanceInfo = false;  
            bool showAccessibleStepFreeInfo = false;
            if (journey is JourneyControl.PublicJourney)
            {
                showAccessibleFeatures = ((JourneyControl.PublicJourney)journey).AccessibleJourney;

                if (jr != null && jr.AccessiblePreferences != null)
                {
                    showAccessibleAssistanceInfo = jr.AccessiblePreferences.RequireSpecialAssistance;
                    showAccessibleStepFreeInfo = jr.AccessiblePreferences.RequireStepFreeAccess;
                }
            }

			JourneyDetailsSegmentsControl jdsc = e.Item.FindControl("journeyDetailsSegment") as JourneyDetailsSegmentsControl;
			if (journeys.Length == 1)
			{
				JourneyLegCount = 0;
				// Only one journey
                jdsc.Initialise(journey, outward, compareMode, adjustable, stationInfo, printable, true, true, journeyItineraryIndex, JourneyLegCount, jr, findAMode, 
                    showAccessibleFeatures, showAccessibleAssistanceInfo, showAccessibleStepFreeInfo);
                jdsc.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
				jdsc.UseNameList = false;
				JourneyLegCount = journey.JourneyLegs.Length;
			}
			else if (journeys[0].Equals(e.Item.DataItem))
			{
				// First journey of numerous
				JourneyLegCount = 0;
                jdsc.Initialise(journey, outward, compareMode, adjustable, stationInfo, printable, true, false, journeyItineraryIndex, JourneyLegCount, jr, findAMode,
                    showAccessibleFeatures, showAccessibleAssistanceInfo, showAccessibleStepFreeInfo);
                jdsc.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
				jdsc.UseNameList = false;
				JourneyLegCount = journey.JourneyLegs.Length;
			}
			else if (journeys[journeys.Length - 1].Equals(e.Item.DataItem))
			{
				// Last journey of numerous
                jdsc.Initialise(journey, outward, compareMode, adjustable, stationInfo, printable, false, true, journeyItineraryIndex, JourneyLegCount, jr, findAMode,
                    showAccessibleFeatures, showAccessibleAssistanceInfo, showAccessibleStepFreeInfo);
                jdsc.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);

				// add new car park references 
				JourneyLeg[] legs = journeys[journeys.Length - 1].JourneyLegs;
				
				for(int i=0; i<legs.Length; i++) 
				{
					JourneyLeg leg = legs[i];
					if(leg.LegStart.Location.CarParking != null)
					{
						string carRef = leg.LegStart.Location.CarParking.CarParkReference;
						if(!uniqueCarParkRefs.Contains(carRef))
						{
							uniqueCarParkRefs.Add(carRef);
						}
					}
					if(leg.LegEnd.Location.CarParking != null)
					{
						string carRef = leg.LegEnd.Location.CarParking.CarParkReference;
						if(!uniqueCarParkRefs.Contains(carRef))
						{
							uniqueCarParkRefs.Add(carRef);
						}
					}
				}
				jdsc.UniqueNameList = uniqueCarParkRefs;
				jdsc.UseNameList = true;
			}
			else
			{
				// Journey in middle of numerous
                jdsc.Initialise(journey, outward, compareMode, adjustable, stationInfo, printable, false, false, journeyItineraryIndex, JourneyLegCount, jr, findAMode,
                    showAccessibleFeatures, showAccessibleAssistanceInfo, showAccessibleStepFreeInfo);
                jdsc.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
				
				// set the previous arraylist details
                JourneyLeg[] legs = journey.JourneyLegs;

				for(int i=0; i<legs.Length; i++) 
				{
					JourneyLeg leg = legs[i];
					if(leg.LegStart.Location.CarParking != null)
					{
						string carRef = leg.LegStart.Location.CarParking.CarParkReference;
						if(!uniqueCarParkRefs.Contains(carRef))
						{
							uniqueCarParkRefs.Add(carRef);
						}
					}
					if(leg.LegEnd.Location.CarParking != null)
					{
						string carRef = leg.LegEnd.Location.CarParking.CarParkReference;
						if(!uniqueCarParkRefs.Contains(carRef))
						{
							uniqueCarParkRefs.Add(carRef);
						}
					}
				}
				jdsc.UniqueNameList = uniqueCarParkRefs;
				jdsc.UseNameList = true;
				JourneyLegCount += journey.JourneyLegs.Length;
			}

			jdsc.MyPageId = belongingPageId;
			jdsc.MapButtonClicked += new MapButtonClickEventHandler(this.JourneyDetailsSegmentMapButtonClicked);


		}
		#endregion
	}    
}
