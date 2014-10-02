// *********************************************** 
// NAME                 : FindSummaryResultControl.ascx.cs 
// AUTHOR               : James Haydock
// DATE CREATED         : 11/05/2004 
// DESCRIPTION		: A custom user control to
// display summary journey results in a tabular format.
// The control allows the user to select any of the rows
// in the table. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindSummaryResultControl.ascx.cs-arc  $
//
//   Rev 1.17   Feb 08 2012 16:15:08   RBroddle
//corrected show 10 / show all button text defaulting if switching to Welsh
//Resolution for 5789: Show 10 / Show all button changing from drop downs to toggle button
//
//   Rev 1.16   Feb 08 2012 15:07:10   rbroddle
//corrected initial default of show 10 / show all button
//Resolution for 5789: Show 10 / Show all button changing from drop downs to toggle button
//
//   Rev 1.15   Feb 06 2012 17:58:50   RBroddle
//Added show ten / show all toggle button to replace the drop down on "find a" results, under minor portal UI changes CCN
//Resolution for 5789: Show 10 / Show all button changing from drop downs to toggle button
//
//   Rev 1.14   Jan 20 2011 15:29:58   apatel
//Updated to remove RailCost find a planner from showing "Show All Show 10" feature.
//Resolution for 5665: Show ALL Show 10 - Seach By Price should show all the results
//
//   Rev 1.13   Dec 06 2010 12:55:02   apatel
//Code updated to implement show all show 10 feature for journey results and to remove anytime option from the input page.
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.12   Mar 05 2010 10:00:34   apatel
//Updated for customised intellitracker tags
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.11   Mar 04 2010 14:05:32   mmodi
//Removed Changes count code added for International planner
//Resolution for 5432: TD Extra - Script 003 : Changes count is incorrect on Journey details page
//
//   Rev 1.10   Feb 25 2010 14:47:46   mmodi
//Pass through flag to ignore Transfer legs when getting SummaryLines
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 12 2010 11:13:28   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Jan 15 2009 14:20:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.7   Oct 14 2008 13:10:48   mmodi
//Manual merge for stream5014
//
//   Rev 1.6   Jun 18 2008 16:11:42   dgath
//fixed ITP issues
//Resolution for 5025: ITP: Workstream
//
//   Rev 1.5.1.3   Oct 07 2008 11:39:20   mmodi
//Updateed to apply cycle specific style
//Resolution for 5125: Cycle Planner - "Server Error" is displayed when user clicks on 'Printer Friendly' button on 'Journey Summary' page
//
//   Rev 1.5.1.2   Sep 18 2008 14:56:10   mmodi
//Corrected display issues
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5.1.1   Sep 15 2008 10:59:56   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5.1.0   Jun 20 2008 14:34:38   mmodi
//Updated to detect cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   May 06 2008 17:50:14   mmodi
//Drop down list hidden, and all results shown.
//Resolution for 4950: Hide 10/All Results drop down list from journey results pages.
//
//   Rev 1.4   Apr 24 2008 15:47:08   jfrank
//Change to make the journey selected the third journey rather than the first when there is a scroll bar.
//Resolution for 4896: Selected journey disapears when using show all results on Find a Train
//
//   Rev 1.3   Apr 02 2008 10:17:30   mmodi
//Updated image strings
//
//   Rev 1.2   Mar 31 2008 13:20:52   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory JaFeb 11 2008 19:00:00 mmodi
//Updated to reload data if it detects the Num of journeys summary data does not equal max results to show
//
//   Rev 1.0   Nov 08 2007 13:14:32   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to show journeys for a specific journey mode type, 
//and to not display the 'show all results' dropdown for city to city journeys
//
//   Rev 1.55   May 31 2007 14:55:54   asinclair
//Added missing button and set langstrings values correctly
//Resolution for 4433: 9.6 - Button with no text on results page for City to City
//
//   Rev 1.54   May 22 2007 14:55:04   asinclair
//Fixed error from Unit Testing
//Resolution for 4412: 9.6 - WAI / Accessibility Issues
//
//   Rev 1.53   May 21 2007 18:00:18   asinclair
//Added code to use TDButtons (looking like Hyperlinks) instead of ImageButtons
//Resolution for 4412: 9.6 - WAI / Accessibility Issues
//
//   Rev 1.52   Nov 16 2006 16:26:20   tmollart
//Modified code so that control is displayed with "Up to..." and "Show all results" options for RailCost FindAMode.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.51   Nov 14 2006 10:04:54   rbroddle
//Merge for stream4220
//
//   Rev 1.50.1.0   Nov 09 2006 16:29:54   dsawe
//Updated operatorvisible  & changevisible property to include FindAMode.RailCost condition
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.51   Nov 09 2006 16:26:12   dsawe
//added condition FindAMode.RailCost for displaying operator & changes in repeater control 
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.50   Apr 28 2006 13:28:12   COwczarek
//Pass summary type to FormattedJourneySummaryLines constructor.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.49   Apr 20 2006 13:16:14   mtillett
//Using a CSS style to hide the table cell when contents not visible. Updated CSS widths for the Duration column and other headering elements to compensate by 12px.
//Resolution for 2504: From DEL 8.0: Find a car -  Cosmetic: blue line around journey summary option box is discontinuous
//Resolution for 3924: DN068 Extend: Firefox display issue on quickplanner results pages
//
//   Rev 1.48   Apr 04 2006 15:48:28   RGriffith
//IR3701 Fix: Addition of direction to FormattedJourneySummaryLine adapter to set correct origin/destination locations
//
//   Rev 1.47   Feb 23 2006 16:11:16   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.46   Dec 03 2005 15:08:08   jgeorge
//Use table row id in call to ScrollManager instead of mock radio button. This ensures that the whole row is visible when it's a bit taller than normal.
//Resolution for 3234: DN39: Positioning of the fares results
//
//   Rev 1.45   Nov 15 2005 12:20:20   mguney
//journeyViewState values are saved instead of TDSessionManager.Current.JourneyViewState in SaveViewState method.
//
//   Rev 1.44   Nov 03 2005 17:08:46   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.43.1.0   Oct 19 2005 10:30:32   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.43   May 11 2005 14:37:12   jbroome
//Radio buttons and "Select" column heading not displayed when only one results row.
//Resolution for 2481: Remove "Select" button from journey summary tables when only one journey result displayed
//
//   Rev 1.42   Apr 08 2005 14:49:16   rgeraghty
//Removed visibility check in Page_Load as was preventing correct journey being saved to viewstate
//
//   Rev 1.41   Apr 05 2005 15:04:22   rgeraghty
//Moved ReadResources() method in Page_Load so that select button Urls get set
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.40   Apr 01 2005 19:39:48   rhopkins
//Changed to handle Fare and TrunkCostBased results
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.39   Mar 21 2005 15:49:42   jgeorge
//Updated with FxCop fixes
//
//   Rev 1.38   Mar 18 2005 16:27:08   COwczarek
//Page load and prerender event handlers now do nothing if 
//control is not visible. This was only causing a problem in unit
//test but is a performance improvement nonetheless.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.37   Mar 10 2005 12:51:12   jgeorge
//Changed to use ScrollManager instead of custom JavaScript
//
//   Rev 1.36   Mar 07 2005 19:02:42   rhopkins
//If FindAMode is cost-based then force sorting to work as though the User had selected "leave after".
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.35   Mar 02 2005 17:13:14   rscott
//DEL 7 - Updated to resolve problem with returnDateTime nulls
//
//   Rev 1.34   Mar 01 2005 16:26:44   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.33   Nov 03 2004 12:54:08   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.32   Oct 05 2004 09:50:08   RGeraghty
//IR1337 Correction to column header alt text
//
//   Rev 1.31   Oct 04 2004 11:57:18   rgeraghty
//IR1337 Amend header alt text and add alt text to sort icon
//
//   Rev 1.30   Sep 27 2004 18:06:46   jbroome
//IR 1528 - re-write of the JavaScript functionality of the control.
//
//   Rev 1.29   Sep 27 2004 14:31:14   passuied
//Added CheckSelectedIndexFitsInResultsToShow method called in PageLoad
//Resolution for 1647: Selected journey not always visible after coming back from extend journey
//
//   Rev 1.28   Sep 22 2004 15:25:28   jbroome
//Minor tweaks to the JavaScript functionality of the control.
//
//   Rev 1.27   Sep 19 2004 13:52:26   jbroome
//Correct output of javascript call.
//
//   Rev 1.26   Sep 17 2004 13:53:10   RHopkins
//IR1577 Check for ItineraryManagerModeChanged in Page_PreRender to see if we need to reload the data
//
//   Rev 1.25   Sep 14 2004 13:26:08   passuied
//Different fixes :
//- hidden journeyIndex in all cases.
//- fixed some lining up div issues
//- Added javascript functionality to allow selected item to be always visible
//Resolution for 1528: Find a Variety of Transport - All results expect to show option numbers in sequence they are not
//
//   Rev 1.24   Sep 08 2004 17:27:12   RHopkins
//IR1479 Added client viewstate logic so that use of browser "Back" button produces sensible results.  Same logic as in SummaryResultTableControl.
//
//   Rev 1.23   Sep 07 2004 14:53:36   jmorrissey
//IR1513 - in the Page_PreRender event, the control is not made visible when there are no JourneySummaryLines to show.
//
//   Rev 1.22   Sep 05 2004 15:21:14   CHosegood
//Now only fires SelectionChangesEvent when the Selection has changed.
//Resolution for 1495: Find a car: Maps zoom out button does not work as expected
//Resolution for 1496: Find a car: Undjusted route, beginning and end of journey cannot be displayed on map
//Resolution for 1497: Find a car: Cannot zoom past level 7 by manually clicking on map
//
//   Rev 1.21   Sep 04 2004 17:23:02   jbroome
//IR1504 - Ensure correct setting of the selected journey in the grid.
//
//   Rev 1.20   Sep 04 2004 13:14:24   jbroome
//IR 1500 - Column headers only sortable if more than one journey returned.
//
//   Rev 1.19   Sep 03 2004 12:25:34   jgeorge
//Updated GetAdditionalStyleAttribute method to take account of undefined number of results to show.
//Resolution for 1485: Find a Flight should use scroll bar within Journey results
//
//   Rev 1.18   Sep 02 2004 18:13:10   RHopkins
//IR1467 When setting selected Return journey index in SessionManager check whether the control is in Return-mode.
//
//   Rev 1.17   Sep 02 2004 10:36:48   passuied
//Added Functionality to show hide columns according of which view mode we are in (FlightTrainCoach, Car or Trunk)
//Resolution for 1460: All Find a...The number of each journey in the Journey summary options should be dispalyed
//
//   Rev 1.16   Aug 21 2004 12:31:54   COwczarek
//Derive the number of results to show in pre-render event handler in case itinerary manager has reset view state.
//Resolution for 1319: Extend Find a coach - Returning from extend does not display find a coach summary options page
//
//   Rev 1.15   Aug 25 2004 11:24:28   passuied
//Forced the control to select the index for the first time when index=0.
//Resolution for 1435: Find A Car Results. Problem with the summary/ details control
//
//   Rev 1.14   Aug 24 2004 16:42:16   jgeorge
//IR1366
//
//   Rev 1.13   Aug 23 2004 14:04:50   jgeorge
//IR1319
//
//   Rev 1.12   Aug 20 2004 12:12:46   jgeorge
//IR1338
//
//   Rev 1.11   Aug 17 2004 11:10:34   esevern
//added select best match when journey summary lines not null
//
//   Rev 1.10   Aug 03 2004 11:54:40   COwczarek
//Use new IsFindAMode and FindAMode properties
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.9   Jul 29 2004 17:03:06   COwczarek
//Prevent pre-render processing if no journey results available
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.8   Jul 28 2004 18:07:18   RPhilpott
//Fix bug when no summary lines to display.
//
//   Rev 1.7   Jul 23 2004 16:36:14   jgeorge
//Updates for Del 6.1
//
//   Rev 1.6   Jul 20 2004 11:35:16   jgeorge
//Added check to ensure that control can be included on pages when there are no Find a... results
//
//   Rev 1.5   Jul 19 2004 15:25:16   jgeorge
//Del 6.1 updates

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using Logger = System.Diagnostics.Trace;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	A custom user control to
	/// display summary journey results in a tabular format.
	/// The control allows the user to select any of the rows in the table. 
	/// </summary>
	[ComVisible(false)]
	public partial  class FindSummaryResultControl : TDUserControl
	{
		#region Controls

		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonFrom;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonTo;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonChanges;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonOperator;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonLeave;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonArrive;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonDuration;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonTransport;
        protected TransportDirect.UserPortal.Web.Controls.TDButton buttonToggleShowTenShowAll;






		#endregion

		#region Constants

		// Keys for the resource file
		private const string labelShowKey = "FindSummaryResultControl.labelShow";
		private const string commandShowTextKey = "FindSummaryResultControl.commandShow.Text";

		// Property keys
		private const string ScrollPointKey = "FindSummaryResultControl.Scrollpoint.{0}";
		private const string FixedHeightKey = "FindSummaryResultControl.FixedHeight.{0}";

		#endregion

		#region Private variables

		// Indicates the current FindA mode
		private FindAMode findAMode = FindAMode.None;

        // Indicates if we're in FindA cycle journey mode
        private bool isCycleMode = false;

		// Indicates if the table should be rendered in print mode or selectable mode.
		private bool printMode;
		
		// Indicates if this control should load data for the outward journey
		// or the return journey.
		private bool outward;

		// Indicates if results were generated using "arrive before" or "depart after"
		private bool arriveBefore;

		//Urls for Asc/Desc icon
        private string sortAscendingUrl = string.Empty;
        private string sortDescendingUrl = string.Empty;

		// Event to fire when the selection has changed
		public event SelectionChangedEventHandler SelectionChanged;

		private string uncheckedRadioButtonImageUrl = string.Empty;
		private string checkedRadioButtonImageUrl = string.Empty;

		private int scrollPoint = int.MaxValue;
		private string fixedHeight = string.Empty;

		// Whether or not the map state should be reset
		private FormattedJourneySummaryLines journeySummaryLines;
		private TDJourneyViewState journeyViewState;

		private IDataServices populator;

        private ModeType[] modeType;

		#endregion

		#region Initialisation methods

		/// <summary>
		/// Initialisation method for this control.
		/// </summary>
		/// <param name="printMode">Indicates if the control should be rendered in print mode or not.</param>
		/// <param name="outward">Indicates if the journey rendered should be outward or return.</param>
		/// <param name="arriveBefore">Indicates the search type of the journey.</param>
		/// <param name="selectedJourneyRowIndex"></param>
		public void Initialise(bool printMode, bool outward, bool arriveBefore)
		{
			// All of these values will be saved in the viewstate so that Initialise
			// needs to be called once only.
			this.printMode = printMode;
			this.outward = outward;
			this.arriveBefore = arriveBefore;
		}

        public void Initialise(bool printMode, bool outward, bool arriveBefore, ModeType[] modeType)
        {
            // All of these values will be saved in the viewstate so that Initialise
            // needs to be called once only.
            this.printMode = printMode;
            this.outward = outward;
            this.arriveBefore = arriveBefore;
            this.modeType = modeType;
        }

		#endregion

		#region Page lifecycle event handlers

		/// <summary>
		/// Handler for the Init event. Sets up page-level variables
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Get a reference to the current dataservices service
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			journeyViewState = TDItineraryManager.Current.JourneyViewState;

		}

		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Back up the selection in the combo box to an int, ready to show later:
            int dropShowSelectedIndex = dropShow.SelectedIndex;

            // Set the cycle mode flag
            isCycleMode = (TDSessionManager.Current.FindAMode == FindAMode.Cycle);

            // Load the "number of results to display" dropdown
			populator.LoadListControl(DataServiceType.FSCResultsToDisplayDrop, dropShow);
			
			ReadResources();
			ReadProperties();

			if (TDSessionManager.Current.IsFindAMode)
			{
                ITDJourneyResult result;
                ITDCyclePlannerResult cycleResult;

                if (isCycleMode)
                {
                    cycleResult = TDSessionManager.Current.CycleResult;
                    if (cycleResult == null || !cycleResult.IsValid)
                        return;
                }
                else
                {
                    result = TDItineraryManager.Current.JourneyResult;
                    if (result == null || !result.IsValid)
                        return;
                }

				// If we've just come to the summary page from the wait page (ie if this is the first time the user
				// gets to see these results) we need to select the best match for them. Otherwise we need to
				// leave the selection as it was.
				string firstViewing = TDSessionManager.Current.GetOneUseKey(TransportDirect.UserPortal.Resource.SessionKey.FirstViewingOfResults);
				if (firstViewing == null)
					firstViewing = string.Empty;

				// Find out if we will be showing the dropdown and if so then retrive the default
				// value and set the property
				if (firstViewing.Length != 0 || MaxResultsToShow == TDJourneyViewState.RESULTS_TO_SHOW_UNDEFINED)
				{
					if (AllowShowDropdown(int.MaxValue))
					{
						MaxResultsToShow = getDefaultResultsToShow();
					}
				}

				// Load the data and bind it to the grid. If this is not done, the events will not
				// be raised
				LoadFormattedSummaryData(false);

                if (firstViewing.Length != 0)
                {
                    SelectedJourneyIndex = journeySummaryLines.BestMatch;
                    //Show 10 / Show all toggle button default to "show all"
                    buttonToggleShowTenShowAll.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowAll.Text");
                }
                else
                {
                    //Show 10 / Show all toggle button - set appropriate text in case user switched to Welsh
                    if (MaxResultsToShow == 10)
                    {
                        buttonToggleShowTenShowAll.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowAll.Text");
                    }
                    else
                    {
                        buttonToggleShowTenShowAll.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowTen.Text");
                    }

                    if (outward)
                        SelectedJourneyIndex = TDItineraryManager.Current.SelectedOutwardJourneyID;
                    else
                        SelectedJourneyIndex = TDItineraryManager.Current.SelectedReturnJourneyID;
                }

				// check selected index is visible in selected results to show.
				// Problem happens when coming back from Extendjourney
				CheckSelectedIndexFitsInResultsToShow();

				BindGrid();
			}

            //Reselect the correct item in the drop down list:
            dropShow.SelectedIndex = dropShowSelectedIndex;

            //Show ten / Show all button has now replaced the "show..." drop down - set not visible
            dropShow.Visible = false;
            commandShow.Visible = false;
            labelShow.Visible = false;
                 
		}

		/// <summary>
		/// Handler for the prerender event
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{

            if (!Visible) 
            {
                return;
            }

			ReadProperties();

			ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;
            ITDCyclePlannerResult cycleResult = TDSessionManager.Current.CycleResult;
			ITDSessionManager sessionManager = TDSessionManager.Current;

            if (sessionManager.IsFindAMode &&
                (((result != null) && (result.IsValid)) || ((cycleResult != null) && (cycleResult.IsValid))))
			{

                // Itinerary manager may have reset view state data if itinerary was reset
                // therefore reinitialise the number of results to show
                if (MaxResultsToShow == TDJourneyViewState.RESULTS_TO_SHOW_UNDEFINED)
                {
                    if (AllowShowDropdown(int.MaxValue))
                    {
                        MaxResultsToShow = getDefaultResultsToShow();
                    }
                }

				populator.Select(dropShow, MaxResultsToShow.ToString(TDCultureInfo.InvariantCulture));

                if (outward)
                    fscPanelShow.Visible = AllowShowDropdown(GetNumberOfJourneys(result, cycleResult, true));
                else
                    fscPanelShow.Visible = AllowShowDropdown(GetNumberOfJourneys(result, cycleResult, false));

				// See if we need to reload the data
				if (TDItineraryManager.Current.ItineraryManagerModeChanged || (journeySummaryLines == null) || (journeySummaryLines.Count == 0)
                    || (journeySummaryLines.MaxNumberOfResults != MaxResultsToShow)
                    || (journeySummaryLines.Count != MaxResultsToShow))
				{
					LoadFormattedSummaryData(true);
				}

				// No need to render this control if there are no journeySummaryLines 			
				if ( (journeySummaryLines != null) && (journeySummaryLines.Count > 0) )
				{
					// See if we need to resort the data						
					if ((SortedColumnId != journeySummaryLines.CurrentSortColumn) || (SortedDescending == journeySummaryLines.CurrentSortColumnAscending))
					{
						// Store the current selected journey
						journeySummaryLines.Sort(SortedColumnId, !SortedDescending);
						if (outward)
							sessionManager.FindPageState.StoredSummaryDataOutward = journeySummaryLines.GetSavedSummaryData();
						else
							sessionManager.FindPageState.StoredSummaryDataReturn = journeySummaryLines.GetSavedSummaryData();
					}				

					// Bind the grid
					BindGrid();

                    if (MaxResultsToShow != 0)
                    {
                        populator.Select(dropShow, "0");
                    }
                    else
                    {
                        populator.Select(dropShow, "10");
                    }
					// Ensure that the correct value is displayed in the "Show" dropdown
					//populator.Select(dropShow, MaxResultsToShow.ToString(TDCultureInfo.InvariantCulture));
				}
				else
				{
					this.Visible = false;
				}
			}
			else
			{
				this.Visible = false;
			}

			if (!printMode && result != null && result.IsValid )
			{
				PopulateTDButtons();
			}

            		
		}

		#endregion

		#region Viewstate methods

		/// <summary>
		/// Loads the ViewState
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					printMode = (bool)myState[1];
				if (myState[2] != null)
					outward = (bool)myState[2];
				if (myState[3] != null)
					arriveBefore = (bool)myState[3];
				if(outward)
				{
					if (myState[4] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedOutwardJourney = (int)myState[4];
					}
					if (myState[5] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyID = (int)myState[5];
					}
					if (myState[6] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType = (TDJourneyType)myState[6];
					}
				}
				else 
				{
					if (myState[4] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedReturnJourney = (int)myState[4];
					}
					if (myState[5] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyID = (int)myState[5];
					}
					if (myState[6] != null)
					{
						TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType = (TDJourneyType)myState[6];
					}
				}
			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viewstate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[7];
			allStates[0] = baseState;
			allStates[1] = printMode;
			allStates[2] = outward;
			allStates[3] = arriveBefore;
			if(outward) 
			{
				allStates[4] = journeyViewState.SelectedOutwardJourney;
				allStates[5] = journeyViewState.SelectedOutwardJourneyID;
				allStates[6] = journeyViewState.SelectedOutwardJourneyType;
			}
			else 
			{
				allStates[4] = journeyViewState.SelectedReturnJourney;
				allStates[5] = journeyViewState.SelectedReturnJourneyID;
				allStates[6] = journeyViewState.SelectedReturnJourneyType;
			}

			return allStates;
		}		

		#endregion

		#region Private helper methods

		/// <summary>
		/// Sets up class level variables using Resource Manager
		/// </summary>
		private void ReadResources()
		{
			// Set label text and image urls
			labelShow.Text = Global.tdResourceManager.GetString(labelShowKey, TDCultureInfo.CurrentUICulture);
			commandShow.Text = GetResource(commandShowTextKey);

			// Read the button urls from resourcing manager.
			uncheckedRadioButtonImageUrl = Global.tdResourceManager.GetString("SummaryResultTableControl.imageButton.SummaryRadioButtonUnchecked", TDCultureInfo.CurrentUICulture);
			checkedRadioButtonImageUrl = Global.tdResourceManager.GetString("SummaryResultTableControl.imageButton.SummaryRadioButtonChecked", TDCultureInfo.CurrentUICulture);

            // Sort icons
            sortDescendingUrl = Global.tdResourceManager.GetString("SummaryResultTableControl.ImageSortIconDescending");
            sortAscendingUrl = Global.tdResourceManager.GetString("SummaryResultTableControl.ImageSortIconAscending");
		}


		/// <summary>
		/// Populates the TDButton controls with text, cssClass, and CssClassMouseOver properties.
		/// </summary>
		private void PopulateTDButtons()
		{

			buttonTransport = (TDButton)summaryRepeater.Controls[0].FindControl("buttonTransport");
			buttonTransport.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.Transport", TDCultureInfo.CurrentUICulture);
			buttonTransport.CssClass = "TDHyperLinkStyleButton";
			buttonTransport.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

			buttonFrom = (TDButton)summaryRepeater.Controls[0].FindControl("buttonFrom");
			buttonFrom.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.From", TDCultureInfo.CurrentUICulture);
			buttonFrom.CssClass = "TDHyperLinkStyleButton";
			buttonFrom.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";
		
			buttonTo = (TDButton)summaryRepeater.Controls[0].FindControl("buttonTo");
			buttonTo.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.To", TDCultureInfo.CurrentUICulture);
			buttonTo.CssClass = "TDHyperLinkStyleButton";
			buttonTo.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";
		

			buttonChanges = (TDButton)summaryRepeater.Controls[0].FindControl("buttonChanges");
			buttonChanges.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.Changes", TDCultureInfo.CurrentUICulture);
			buttonChanges.CssClass = "TDHyperLinkStyleButton";
			buttonChanges.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

			buttonOperator = (TDButton)summaryRepeater.Controls[0].FindControl("buttonOperator");
			buttonOperator.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.Operator", TDCultureInfo.CurrentUICulture);
			buttonOperator.CssClass = "TDHyperLinkStyleButton";
			buttonOperator.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

			buttonLeave = (TDButton)summaryRepeater.Controls[0].FindControl("buttonLeave");
			buttonLeave.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.Leave", TDCultureInfo.CurrentUICulture);
			buttonLeave.CssClass = "TDHyperLinkStyleButton";
			buttonLeave.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

			buttonArrive = (TDButton)summaryRepeater.Controls[0].FindControl("buttonArrive");
			buttonArrive.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.Arrive", TDCultureInfo.CurrentUICulture);
			buttonArrive.CssClass = "TDHyperLinkStyleButton";
			buttonArrive.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

			buttonDuration = (TDButton)summaryRepeater.Controls[0].FindControl("buttonDuration");
			buttonDuration.Text = Global.tdResourceManager.GetString("SummaryResultTableControl.tdButton.Duration", TDCultureInfo.CurrentUICulture);
			buttonDuration.CssClass = "TDHyperLinkStyleButton";
			buttonDuration.CssClassMouseOver = "TDHyperLinkStyleButtonMouseOver";

		}

		/// <summary>
		/// Sets up class level variables using Properties service.
		/// </summary>
		private void ReadProperties()
		{
			findAMode = TDSessionManager.Current.FindAMode;

			if (TDSessionManager.Current.IsFindAMode)
			{
				// Read other data items from the property provider
				string scrollPointS = Properties.Current[String.Format(TDCultureInfo.InvariantCulture, ScrollPointKey, findAMode.ToString())];
				if (scrollPointS != null && scrollPointS.Length > 0)
					scrollPoint = Convert.ToInt32(scrollPointS, TDCultureInfo.InvariantCulture);
				else
					scrollPoint = int.MaxValue;

				fixedHeight = Properties.Current[String.Format(TDCultureInfo.InvariantCulture, FixedHeightKey, findAMode.ToString())];
				if (fixedHeight == null || fixedHeight.Length == 0)
					fixedHeight = string.Empty;
			}
		}

		/// <summary>
		/// Adds the event handlers to the controls in the bound repeater
		/// </summary>
		private void AddEventHandlers()
		{
			// Add handlers if appropriate
			if (!printMode)
			{		
				// Add event handlers
				ImageButton imageButton;

				TDButton tdbutton;

				if (summaryRepeater.Items.Count > 1)
				{
					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonFrom");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(tdbuttonFrom_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonTo");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(tdbuttonTo_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonChanges");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(tdbuttonchanges_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonOperator");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(buttonOperator_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonLeave");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(buttonLeave_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonArrive");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(buttonArrive_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonDuration");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(buttonDuration_Click);

					tdbutton = (TDButton)summaryRepeater.Controls[0].FindControl("buttonTransport");
					if (tdbutton != null)
						tdbutton.Click +=new EventHandler(buttonTransport_Click);
				}

				for (int i = 0; i < summaryRepeater.Items.Count; i++)
				{
					imageButton = (ImageButton)summaryRepeater.Items[i].FindControl("ImageButton");
					if (imageButton != null)
					{
						imageButton.Click += new System.Web.UI.ImageClickEventHandler(RadioButtonClick);
					}
				}
            }		
		}

        /// <summary>
        /// Returns the number of journeys (public + road + cycle)
        /// </summary>
        private int GetNumberOfJourneys(ITDJourneyResult result, ITDCyclePlannerResult cycleResult, bool outward)
        {
            int numberOfJourneys = 0;
            if (outward)
            {
                if (result != null)
                {
                    numberOfJourneys += result.OutwardPublicJourneyCount;
                    numberOfJourneys += result.OutwardRoadJourneyCount;
                }
                if (cycleResult != null)
                {
                    numberOfJourneys += cycleResult.OutwardCycleJourneyCount;
                }
            }
            else
            {
                if (result != null)
                {
                    numberOfJourneys += result.ReturnPublicJourneyCount;
                    numberOfJourneys += result.ReturnRoadJourneyCount;
                }
                if (cycleResult != null)
                {
                    numberOfJourneys += cycleResult.ReturnCycleJourneyCount;
                }
            }

            return numberOfJourneys;
        }

		/// <summary>
		/// Returns true if the controls for changing the number of results displayed are
		/// visible
		/// </summary>
		private bool AllowShowDropdown(int numberOfResults)
		{
			switch (findAMode) 
			{
				case FindAMode.Train:
				case FindAMode.Trunk: // City to City
				case FindAMode.TrunkStation:
                case FindAMode.Coach:
                case FindAMode.Flight:
					if (printMode)
						return false;
					else
					{
						// Only display if there are more than the minimum number of results in the dropdown
						int lowestNum = int.MaxValue;
						foreach (ListItem i in dropShow.Items)
						{
							// All list items contain a number in their
							// Value property
							int val = 0;
							try
							{
								val = Convert.ToInt32(populator.GetValue(DataServiceType.FSCResultsToDisplayDrop, i.Value), TDCultureInfo.InvariantCulture);
							}
							catch (FormatException e)
							{
								// Log operational event
								Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "A non numeric value has been found in the DataServiceType.FSCResultsToDisplayDrop data", e, TDSessionManager.Current.Session.SessionID));
							}
							catch (OverflowException e)
							{
								// Log operational event
								Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "An invalid value has been found in the DataServiceType.FSCResultsToDisplayDrop data", e, TDSessionManager.Current.Session.SessionID));
							}
							if ((val != 0) && (val < lowestNum))
								lowestNum = val;
						}

						return (numberOfResults > lowestNum);
					}
				default:
					return false;
			}
		}

		/// <summary>
		/// Determines if an index is the currently selected index specified by TDSessionManager
		/// </summary>
		/// <param name="index">The candidate index</param>
		/// <returns>true if the index is the selected index, false otherwise</returns>
		private bool IsSelectedIndex(int index)
		{
			//return index == SelectedJourneyRowIndex;
			return journeySummaryLines[index].JourneyIndex == SelectedJourneyIndex;
		}

		/// <summary>
		/// Returns the Resource string for Printable.
		/// Also checks if only one result returned - in 
		/// this case, column headers are not sortable and
		/// so displayed similar to printable version.
		/// </summary>
		private string GetPrintableResourceString(int column)
		{
			if (journeySummaryLines.Count == 1)
				return "Text";
			else if (column == 8)
				return "Text";
			else
				return printMode ? "Text" : "Url";
		}

		/// <summary>
		/// Load the formatted data
		/// </summary>
		/// <param name="forceLoad"></param>
		private void LoadFormattedSummaryData(bool forceLoad)
		{
			ITDJourneyRequest journeyRequest = TDItineraryManager.Current.JourneyRequest;
			ITDJourneyResult journeyResult = TDItineraryManager.Current.JourneyResult;

			ITDSessionManager sessionManager = TDSessionManager.Current;

            ITDCyclePlannerRequest cycleRequest = sessionManager.CycleRequest;
            ITDCyclePlannerResult cycleResult = sessionManager.CycleResult;

			// Check that we are in a Find a... mode and see if there are any return results - do nothing if not
			TDDateTime dateTime = null;
			
			if	(outward)
			{
                if (isCycleMode)
                {
                    dateTime = ((cycleRequest.OutwardDateTime != null) && (cycleRequest.OutwardDateTime.Length > 0))
                        ? cycleRequest.OutwardDateTime[0] : null;
                }
                else
                {
                    dateTime = ((journeyRequest.OutwardDateTime != null) && (journeyRequest.OutwardDateTime.Length > 0))
                        ? journeyRequest.OutwardDateTime[0] : null;
                }
			}
			else
			{
                if (isCycleMode)
                {
                    dateTime = ((cycleRequest.ReturnDateTime != null) && (cycleRequest.ReturnDateTime.Length > 0))
                        ? cycleRequest.ReturnDateTime[0] : null;
                }
                else
                {
                    dateTime = ((journeyRequest.ReturnDateTime != null) && (journeyRequest.ReturnDateTime.Length > 0))
                        ? journeyRequest.ReturnDateTime[0] : null;
                }
			}

			if ((TDSessionManager.Current.FindPageState == null) || (!outward && (dateTime == null)))
			{
				journeySummaryLines = new FormattedJourneySummaryLines();
				if (outward)
					sessionManager.FindPageState.StoredSummaryDataOutward = null;
				else
					sessionManager.FindPageState.StoredSummaryDataReturn = null;
			}
			else
			{
				FindJourneySummaryData storedData = null;

                int journeyReferenceNumber = (isCycleMode ? cycleResult.JourneyReferenceNumber : journeyResult.JourneyReferenceNumber);

				if (!forceLoad)
				{
					// Try loading stored data				
					storedData = outward ? TDSessionManager.Current.FindPageState.StoredSummaryDataOutward : TDSessionManager.Current.FindPageState.StoredSummaryDataReturn;

					if (storedData != null)
					{
						// Check to see whether the stored data corresponds to the current journey results
                        if (storedData.JourneyReferenceNumber != journeyReferenceNumber)
							storedData = null;
					}
				}

                // For International planner, the summary line Origin and Destination should show the 
                // station to station rather than the transfer to transfer names
                bool ignoreTransfers = (findAMode == FindAMode.International);

				JourneySummaryLine[] sourceLines;
				if (outward)
                    sourceLines = (isCycleMode ? cycleResult.OutwardJourneySummary(arriveBefore, modeType) : journeyResult.OutwardJourneySummary(arriveBefore, modeType, ignoreTransfers, ignoreTransfers));
				else
                    sourceLines = (isCycleMode ? cycleResult.ReturnJourneySummary(arriveBefore, modeType) : journeyResult.ReturnJourneySummary(arriveBefore, modeType, ignoreTransfers, ignoreTransfers));

                if (sessionManager.FindPageState.ITPJourney)
                {
                    List<JourneySummaryLine> itpOnlySourceLines = new List<JourneySummaryLine>();



                    foreach (JourneySummaryLine jsl in sourceLines)
                    {
                        ArrayList journeyModes = new ArrayList();
                        journeyModes.AddRange(jsl.Modes);

                        bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                        bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                                   ||
                                 (journeyModes.Contains(ModeType.RailReplacementBus))
                                   ||
                                 (journeyModes.Contains(ModeType.Metro))
                                   ||
                                 (journeyModes.Contains(ModeType.Tram))
                                   ||
                                 (journeyModes.Contains(ModeType.Underground)));

                        bool airmode = journeyModes.Contains(ModeType.Air);

                        if (airmode && (coachmode || trainmode))
                            itpOnlySourceLines.Add(jsl);
                        if (coachmode && trainmode && !airmode)
                            itpOnlySourceLines.Add(jsl);


                    }

                    sourceLines = itpOnlySourceLines.ToArray();
                }

                #region Set up values required for summary lines
                bool anyTime;
                bool leaveAfter;
                string defaultOrigin;
                string defaultDestination;

                if (isCycleMode)
                {
                    anyTime = outward ? cycleRequest.OutwardAnyTime : cycleRequest.ReturnAnyTime;
                    leaveAfter = outward ? !cycleRequest.OutwardArriveBefore : !cycleRequest.ReturnArriveBefore;
                    defaultOrigin = cycleRequest.OriginLocation.Description;
                    defaultDestination = cycleRequest.DestinationLocation.Description;
                }
                else
                {
                    anyTime = outward ? journeyRequest.OutwardAnyTime : journeyRequest.ReturnAnyTime;
                    leaveAfter = outward ? !journeyRequest.OutwardArriveBefore : !journeyRequest.ReturnArriveBefore;
                    defaultOrigin = journeyRequest.OriginLocation.Description;
                    defaultDestination = journeyRequest.DestinationLocation.Description;
                }
                #endregion


				if (storedData == null)
				{
                    int requestedDay = (dateTime != null) ? dateTime.Day : 0;
                    double conversionFactor = Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

                    if (!IsPostBack)
                    {
                        journeySummaryLines = new FormattedJourneySummaryLines(
                            journeyReferenceNumber,
                            sourceLines,
                            dateTime,
                            anyTime,
                            leaveAfter,
                            requestedDay,
                            conversionFactor,
                            defaultOrigin,
                            defaultDestination,
                            MaxResultsToShow,
                            SortedColumnId,
                            !SortedDescending,
                            findAMode,
                            outward,
                            FormattedSummaryType.FindA,
                            modeType);
                    }
                    else
                    {
                        journeySummaryLines = new FormattedJourneySummaryLines(
                            journeyReferenceNumber,
                            sourceLines,
                            dateTime,
                            anyTime,
                            leaveAfter,
                            requestedDay,
                            conversionFactor,
                            defaultOrigin,
                            defaultDestination,
                            MaxResultsToShow,
                            SortedColumnId,
                            !SortedDescending,
                            findAMode,
                            outward,
                            FormattedSummaryType.FindA,
                            modeType,
                            SelectedJourneyIndex);
                    }

					if (outward)
						sessionManager.FindPageState.StoredSummaryDataOutward = journeySummaryLines.GetSavedSummaryData();
					else
						sessionManager.FindPageState.StoredSummaryDataReturn = journeySummaryLines.GetSavedSummaryData();

				}
				else
                {
                    journeySummaryLines = new FormattedJourneySummaryLines(storedData, sourceLines);

                    // Force the best selected journey to be set again
                    journeySummaryLines.ResetBestMatch(dateTime,
                        anyTime,
                        leaveAfter,
                        modeType);
				}
			}

			// If the sort order is unset, read it from journey summary lines now
			if ((journeySummaryLines.Count != 0) && (this.SortedColumnId == JourneySummaryColumn.None))
			{
				this.SortedColumnId = journeySummaryLines.CurrentSortColumn;
				this.SortedDescending = !journeySummaryLines.CurrentSortColumnAscending;
			}
		}

		/// <summary>
		/// Method to fire off the Selection Changed Event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectionChanged(EventArgs e)
		{
            if (SelectionChanged != null)
            {
                // Invoke the delegate
                SelectionChanged(this, e);

                TrackingControlHelper trackingHelper = new TrackingControlHelper();
                trackingHelper.AddTrackingParameter(this, SelectedJourneyIndex.ToString());
            }
		}

		/// <summary>
		/// Binds the grid
		/// </summary>
		private void BindGrid()
		{

			if (printMode)
			{
				summaryRepeater.Visible = false;
				summaryRepeaterPrintable.DataSource = journeySummaryLines;
				summaryRepeaterPrintable.DataBind();
				summaryRepeaterPrintable.Visible = true;
			}
			else
			{
				summaryRepeaterPrintable.Visible = false;
				summaryRepeater.DataSource = journeySummaryLines;
				summaryRepeater.DataBind();
				summaryRepeater.Visible = true;
				AddEventHandlers();
			}
		}

		/// <summary>
		/// Returns the column index corresponding to a specific column Id
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		private static int GetColumnIndexFromId(JourneySummaryColumn column)
		{
			switch (column)
			{
				case JourneySummaryColumn.Origin:
					return 1;
				case JourneySummaryColumn.Destination:
					return 2;
				case JourneySummaryColumn.InterchangeCount:
					return 3;
				case JourneySummaryColumn.OperatorName:
					return 4;
				case JourneySummaryColumn.DepartureTime:
					return 5;
				case JourneySummaryColumn.ArrivalTime:
					return 6;
				case JourneySummaryColumn.Duration:
					return 7;
				case JourneySummaryColumn.Mode:
					return 9;
				case JourneySummaryColumn.None:
				default:
					return 0;
			}
		}

		/// <summary>
		/// Returns the column Id corresponding to a specific index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private static JourneySummaryColumn GetColumnIdFromIndex(int index)
		{
			switch (index)
			{
				case 1:
					return JourneySummaryColumn.Origin;
				case 2:
					return JourneySummaryColumn.Destination;
				case 3:
					return JourneySummaryColumn.InterchangeCount;
				case 4:
					return JourneySummaryColumn.OperatorName;
				case 5:
					return JourneySummaryColumn.DepartureTime;
				case 6:
					return JourneySummaryColumn.ArrivalTime;
				case 7:
					return JourneySummaryColumn.Duration;
				case 9:
					return JourneySummaryColumn.Mode;
				case 0:
				default:
					return JourneySummaryColumn.None;
			}
		}

        /// <summary>
        /// Returns the default number of results to show in the summary control
        /// </summary>
        /// <returns>the default number of results to show in the summary control</returns>
        private int getDefaultResultsToShow() 
        {
            string defaultValue = populator.GetDefaultListControlValue(DataServiceType.FSCResultsToDisplayDrop);
            return Convert.ToInt32(defaultValue, TDCultureInfo.InvariantCulture);
        }

		private void CheckSelectedIndexFitsInResultsToShow()
		{
		
			int resultsToShow = MaxResultsToShow;
			if (resultsToShow > 0)
			{
				if (outward)
				{
					if (journeySummaryLines.IndexFromJourneyIndex(journeyViewState.OutwardSelectedJourneyRowIndex) > resultsToShow)
						resultsToShow = 0;
					
				}
				else
				{
					if (journeySummaryLines.IndexFromJourneyIndex(journeyViewState.ReturnSelectedJourneyRowIndex) > resultsToShow)
						resultsToShow = 0;
				}

			}

			if (resultsToShow != MaxResultsToShow)
				MaxResultsToShow = resultsToShow;
		}

		#endregion

		#region Public helper methods (used for data binding)

		/// <summary>
		/// read-only property used to hide table cell using CSS
		/// </summary>
		/// <param name="visible"></param>
		/// <returns></returns>
		protected string CellCss(bool visible)
		{
			if (!visible)
			{
                return "hide";
			}
			else
			{
				return String.Empty;
			}
		}
	
		/// <summary>
		/// Read-only property. Returns the string to append to the css class
		/// related to which mode we are
		/// </summary>
		private string ModeCssString
		{
			get
			{
				const string flightCoachTrainKey = "";
				const string carKey = "c";
				const string trunkKey = "t";
                const string cycleKey = "cycle";

				switch (findAMode)
				{
					case FindAMode.Car:
						return carKey;
                    case FindAMode.Cycle:
                        return cycleKey;
					case FindAMode.Trunk:
					case FindAMode.TrunkStation:
                    case FindAMode.International:
						return trunkKey;
					default:
						return flightCoachTrainKey;
				}
			}

		}

		/// <summary>
		/// Returns the Css class that the row text should be rendered with.
		/// </summary>
		/// <returns>Css class string.</returns>
		public string GetHeaderRowCssClass()
		{
			string result = string.Empty;

			// append the mode css string
			result += ModeCssString;

			
			return result;
		}
		/// <summary>
		/// Returns the Css class that the row text should be rendered with.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Css class string.</returns>
		public string GetBodyRowCssClass(int index)
		{
			string result = string.Empty;

			// append the mode css string
			result += ModeCssString;


			// If there is only one result then no rows should
			// be highlighed. Check to see if this is the case.
			if(journeySummaryLines.Count != 1)
			{

				if(IsSelectedIndex(index))
				{
					result += "y";
				}
				else
				{

					result += ( (index % 2) == 0 ? "g" : string.Empty);
				}

			}

			
			return result;
		}

		/// <summary>
		/// Returns the Url to the radio button image.
		/// </summary>
		/// <param name="summary">The index of the line being rendered</param>
		/// <returns>Url string to the radio button image.</returns>
		public string GetButtonImageUrl(int index)
		{
			if (IsSelectedIndex(index))
			{
				return checkedRadioButtonImageUrl;
			}
			else 
			{
				return uncheckedRadioButtonImageUrl;
			}
		}

		/// <summary>
		///Returns the alternate text for the radio button.
		/// </summary>
		public string AlternateText(int index)
		{
			if (IsSelectedIndex(index))
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTableControl.RadioButtonSelectedAlternateText", TDCultureInfo.CurrentUICulture);
			}
			else
			{
				return Global.tdResourceManager.GetString(
					"SummaryResultTableControl.RadioButtonAlternateText", TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>
		/// Returns the resource item for the column header text item
		/// </summary>
		public string HeaderItem(int column)
		{
			// If only showing one result, do not need to display "Select" column header
			if ((column == 8) && (journeySummaryLines.Count == 1))
			{
				return "&nbsp;";
			}
			else
			{
				return Global.tdResourceManager.GetString("FindSummaryResultControl.HeaderItem" 
					+ GetPrintableResourceString(column) + column, TDCultureInfo.CurrentUICulture);
			}
		}

		public static string HeaderAltText(int column)
		{
			return Global.tdResourceManager.GetString("FindSummaryResultControl.HeaderItemAltText" + column, TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Returns the Asc/Desc icon url if that colunm is currently sorted
		/// </summary>
		public string HeaderIconUrl(int column)
		{
			if (column == SortedColumnIndex)
			{
				if (SortedDescending)
					return sortDescendingUrl;
				else
					return sortAscendingUrl;
			}

			return "";
		}

		/// <summary>
		///Returns the alternate text for the Asc/Desc icon.
		/// </summary>
		public string HeaderIconAltText()
		{
			return Global.tdResourceManager.GetString("FindSummaryResultControl.HeaderIconAltText", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Returns none, one or both of an overflow attribute and a height attribute
		/// </summary>
		/// <returns></returns>
		public string GetAdditionalStyleAttribute()
		{
			bool scrollPointExceeded = (journeySummaryLines.Count > scrollPoint);
			if (  ((MaxResultsToShow == 0) || (MaxResultsToShow == TDJourneyViewState.RESULTS_TO_SHOW_UNDEFINED) ) && scrollPointExceeded)
                return "overflow: auto; overflow-x: hidden; height: " + fixedHeight;
			else
				return string.Empty;
		}

		/// <summary>
		/// Returns whether the Asc/Desc icon should be visible based on whether that column is currently sorted
		/// </summary>
		public bool HeaderIconVisible(int column)
		{
			if (journeySummaryLines.Count == 1)
				return false;
			else
				return column == SortedColumnIndex;
		}

		/// <summary>
		/// Used to set the Id attribute of the Html row in the
		/// item template of the repeater control.
		/// This can than be accessed in order to scroll it into view.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string GetItemRowId(int index)
		{
			return summaryRepeater.ClientID + "_itemRow_" + index.ToString(TDCultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Determines if the radio button should be visible or not.
		/// </summary>
		/// <returns>True if the button should be rendered, false otherwise.</returns>
		public bool RadioButtonsVisible
		{
			get {return !printMode && (journeySummaryLines.Count > 1);}
		}

		/// <summary>
		/// Read-Only property. Indicates if from column should be visible
		/// </summary>
		public bool FromVisible
		{
			get
			{
                return (findAMode != FindAMode.Car
                    && findAMode != FindAMode.Cycle);
			}
		}

		/// <summary>
		/// Read-Only property. Indicates if to column should be visible
		/// </summary>
		public bool ToVisible
		{
			get
			{
                return (findAMode != FindAMode.Car
                    && findAMode != FindAMode.Cycle);
			}
		}

		/// <summary>
		/// Read-Only property. Indicates if Transport column should be visible
		/// </summary>
		public bool TransportVisible
		{
			get
			{
				return (findAMode == FindAMode.Trunk
                    || findAMode == FindAMode.TrunkStation
                    || findAMode == FindAMode.Cycle
                    || findAMode == FindAMode.International);
			}
		}

		/// <summary>
		/// Read-Only property. Indicates if Index column should be visible
		/// </summary>
		public bool IndexVisible
		{
			get
			{	// Hide all the time now! but won't remove all code for Index column as
				// DFT might change their mind...
				return false;
				
			}
		}

		/// <summary>
		/// Read-Only property. Indicates if Operator column should be visible
		/// </summary>
		public bool OperatorVisible
		{
			get
			{
				return (findAMode == FindAMode.Train)
					|| (findAMode == FindAMode.Coach)
					|| (findAMode == FindAMode.Flight)
					|| (findAMode == FindAMode.Fare)
					|| (findAMode == FindAMode.TrunkCostBased)
					|| (findAMode == FindAMode.RailCost);
			}
		}

		/// <summary>
		/// Read-Only property. Indicates if Changes column should be visible
		/// </summary>
		public bool ChangesVisible
		{
			get
			{
				return (findAMode == FindAMode.Train)
					|| (findAMode == FindAMode.Coach)
					|| (findAMode == FindAMode.Trunk)
					|| (findAMode == FindAMode.TrunkStation)
					|| (findAMode == FindAMode.Flight)
					|| (findAMode == FindAMode.Fare)
					|| (findAMode == FindAMode.TrunkCostBased)
					|| (findAMode == FindAMode.RailCost)
                    || (findAMode == FindAMode.International);
			}
		}

		/// <summary>
		/// Read-Only property. Indicates if sortable link buttons should be visible
		/// If only one result returned, then no need for sortable column headers.
		/// </summary>
		public bool ShowSortableLinks
		{
			get
			{
				return (journeySummaryLines.Count > 1);
			}
		}

		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// OnInit Method.
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			commandShow.Click += new EventHandler(this.commandShow_Click);
            buttonToggleShowTenShowAll.Click += new EventHandler(this.buttonToggleShowTenShowAll_Click);
		}

        #endregion

        private void HideResultsCountDropDownRow()
        {
            //Note that we set the row with the All/Top 10 drop down list to invisible,
            //and set to show all results by default:
            fscPanelShow.Visible = false;
            dropShow.SelectedValue = "0";
            //Simulate the Show button being pressed...
            commandShow_Click(this, EventArgs.Empty);
        }

		#region Event handlers
        
		/// <summary>
		/// Handler for the Radio Button Click.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		public void RadioButtonClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{	
			ImageButton sendingButton = (ImageButton)sender;

			// Find the row index number of the button that was pressed
			int journeyIndex = Convert.ToInt32(sendingButton.CommandName, TDCultureInfo.InvariantCulture);

			SelectedJourneyIndex = journeyIndex;

			OnSelectionChanged(EventArgs.Empty);

		}

		/// <summary>
		/// Handler for the Click event of the "Ok" image button.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		public void commandShow_Click(object sender, EventArgs e)
		{
			string val = populator.GetValue(DataServiceType.FSCResultsToDisplayDrop, dropShow.SelectedItem.Value);
			if (val == null || val.Length == 0)
				MaxResultsToShow = 0;
			else
				MaxResultsToShow = Convert.ToInt32( val, TDCultureInfo.InvariantCulture );
		}
        

		/// <summary>
        /// Handler for the Click event of the button to Toggle Show Ten or Show All results.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        public void buttonToggleShowTenShowAll_Click(object sender, EventArgs e)
		{
			if (MaxResultsToShow == 0) 
            {
                MaxResultsToShow = 10;
                buttonToggleShowTenShowAll.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowAll.Text");
            }
            else
            {
                MaxResultsToShow = 0;
                buttonToggleShowTenShowAll.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowTen.Text");
            }
		}

		
		/// <summary>
		/// Click event handler for the Origin column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void tdbuttonFrom_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.Origin);
		}

		/// <summary>
		/// Click event handler for the Destination column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void tdbuttonTo_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.Destination);
		}


		/// <summary>
		/// Click event handler for the Changes column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void tdbuttonchanges_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.InterchangeCount);
		}

		/// <summary>
		/// Click event handler for the Operator column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void buttonOperator_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.OperatorName);
		}

		/// <summary>
		/// Click event handler for the DepartureTime column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void buttonLeave_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.DepartureTime);
		}

	
		/// <summary>
		/// Click event handler for the ArrivalTime column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void buttonArrive_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.ArrivalTime);
		}

		/// <summary>
		/// Click event handler for the Duration column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void buttonDuration_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.Duration);			
		}

		/// <summary>
		/// Click event handler for the Duration column header
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void buttonTransport_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(JourneySummaryColumn.Mode);		
		}

		/// <summary>
		/// Sets the sort order to the specified column, or changes it if it's already
		/// selected
		/// </summary>
		/// <param name="column"></param>
		private void UpdateSortOrder(JourneySummaryColumn column)
		{
			if (SortedColumnId == column)
				SortedDescending = !SortedDescending;
			else
			{
				SortedColumnId = column;
				SortedDescending = false;
			}
		}

		/// <summary>
		/// The event handler for the summaryRepeater Item Data Bound event
		/// A call to the ScrollManager is added to scroll the correct row into view.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void summaryRepeater_ItemDatabound(object sender, RepeaterItemEventArgs e)
		{
			// If dealing with selected item
			if ((e.Item.ItemIndex > 0)&&(IsSelectedIndex(e.Item.ItemIndex)))
			{
				bool scrollPointExceeded = (journeySummaryLines.Count > scrollPoint);
				if (  ((MaxResultsToShow == 0) || (MaxResultsToShow == TDJourneyViewState.RESULTS_TO_SHOW_UNDEFINED) ) && scrollPointExceeded)
					((TDPage)this.Page).ScrollManager.ScrollElementToView(GetItemRowId(e.Item.ItemIndex -2));
            }

            #region Populate the screen reader headers and table cell styles
            if ((e.Item.ItemType == ListItemType.Item) 
                || (e.Item.ItemType == ListItemType.AlternatingItem)
                || (e.Item.ItemType == ListItemType.SelectedItem))
            {
                HtmlTableCell th = null;
                HtmlTableCell td = null;

                #region Column 10 - Journey index

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader10");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader10" + GetHeaderRowCssClass() + " " + CellCss(IndexVisible));

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild10");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody10" + GetBodyRowCssClass(e.Item.ItemIndex) + " " + CellCss(IndexVisible));
                    }
                }

                #endregion

                #region Column 9 - Transport

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader9");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader9" + GetHeaderRowCssClass() + " " + CellCss( TransportVisible ));

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild9");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);                       
                        td.Attributes.Add("class", "fscbody9" + GetBodyRowCssClass( e.Item.ItemIndex ) + " " + CellCss(TransportVisible));
                    }
                }

                #endregion

                #region Column 1 - From location

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader1");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader1" + GetHeaderRowCssClass() + " " + CellCss(FromVisible));

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild1");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody1" + GetBodyRowCssClass(e.Item.ItemIndex) + " " + CellCss(FromVisible));
                    }
                }

                #endregion

                #region Column 2 - To location

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader2");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader2" + GetHeaderRowCssClass() + " " + CellCss(ToVisible));

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild2");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody2" + GetBodyRowCssClass(e.Item.ItemIndex) + " " + CellCss(ToVisible));
                    }
                }

                #endregion

                #region Column 3 - Changes

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader3");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader3" + GetHeaderRowCssClass() + " " + CellCss(ChangesVisible));

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild3");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody3" + GetBodyRowCssClass(e.Item.ItemIndex) + " " + CellCss(ChangesVisible));
                    }
                }

                #endregion

                #region Column 4 - Operator

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader4");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader4" + GetHeaderRowCssClass() + " " + CellCss(OperatorVisible));

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild4");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody4" + GetBodyRowCssClass(e.Item.ItemIndex) + " " + CellCss(OperatorVisible));
                    }
                }

                #endregion

                #region Column 5 - Leave

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader5");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader5" + GetHeaderRowCssClass());

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild5");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody5" + GetBodyRowCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 6 - Arrive

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader6");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader6" + GetHeaderRowCssClass());

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild6");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody6" + GetBodyRowCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 7 - Duration

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader7");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader7" + GetHeaderRowCssClass());

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild7");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody7" + GetBodyRowCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion

                #region Column 8 - Select

                th = (HtmlTableCell)summaryRepeater.Controls[0].FindControl("screenreaderheader8");
                if (th != null)
                {
                    // Add the class for the table cell
                    th.Attributes.Add("class", "fscheader8" + GetHeaderRowCssClass());

                    td = (HtmlTableCell)e.Item.FindControl("screenreaderchild8");
                    if (td != null)
                    {
                        // Associate table cell with the header (used by screen readers)
                        td.Attributes.Add("headers", th.ClientID);
                        td.Attributes.Add("class", "fscbody8" + GetBodyRowCssClass(e.Item.ItemIndex));
                    }
                }

                #endregion
            }

            #endregion
        }
		#endregion

		#region Properties

		/// <summary>
		/// Get/set the column currently sorted column
		/// </summary>
		private JourneySummaryColumn SortedColumnId
		{
			get { return outward ? journeyViewState.OutwardSortedColumnID : journeyViewState.ReturnSortedColumnID; }
			set
			{
				if (outward)
					journeyViewState.OutwardSortedColumnID = value;
				else
					journeyViewState.ReturnSortedColumnID = value;

				int index = GetColumnIndexFromId(value);
				if (SortedColumnIndex != index)
					SortedColumnIndex = index;
			}
		}

		/// <summary>
		/// Get the index of the currently sorted column
		/// </summary>
		private int SortedColumnIndex
		{
			get { return outward ? journeyViewState.OutwardSortedColumnIndex : journeyViewState.ReturnSortedColumnIndex; }
			set
			{
				if (outward)
					journeyViewState.OutwardSortedColumnIndex = value;
				else
					journeyViewState.ReturnSortedColumnIndex = value;

				JourneySummaryColumn columnId = GetColumnIdFromIndex(value);
				if (SortedColumnId != columnId)
					SortedColumnId = columnId;
			}
		}

		/// <summary>
		/// Returns true if the currently selected column is sorted in descending order
		/// </summary>
		private bool SortedDescending
		{
			get { return outward ? journeyViewState.OutwardSortedDescending : journeyViewState.ReturnSortedDescending; }
			set
			{
				if (outward)
					journeyViewState.OutwardSortedDescending = value;
				else
					journeyViewState.ReturnSortedDescending = value;
			}
		}

		/// <summary>
		/// The index of the currently selected row
		/// </summary>
		private int SelectedJourneyIndex
		{
			get { return outward ? journeyViewState.OutwardSelectedJourneyRowIndex : journeyViewState.ReturnSelectedJourneyRowIndex; }
			set
			{
				if	(journeySummaryLines.Count > 0)
				{
					if (outward
                        && (journeyViewState.OutwardSelectedJourneyRowIndex != value
                        || TDSessionManager.Current.GetOneUseKey(TransportDirect.UserPortal.Resource.SessionKey.FirstViewingOfResults) != null )
                        )
					{
						int index = journeySummaryLines.IndexFromJourneyIndex(value);
						journeyViewState.OutwardSelectedJourneyRowIndex = value;
						journeyViewState.SelectedOutwardJourney = journeySummaryLines[index].OriginalArrayIndex;
						journeyViewState.SelectedOutwardJourneyID = journeySummaryLines[index].JourneyIndex;
						journeyViewState.SelectedOutwardJourneyType = journeySummaryLines[index].Type;
						TDSessionManager.Current.ReturnJourneyMapState.Initialise();
						OnSelectionChanged(new EventArgs());
					}
                    else if (!outward
                        && (journeyViewState.ReturnSelectedJourneyRowIndex != value
                        || TDSessionManager.Current.GetOneUseKey(TransportDirect.UserPortal.Resource.SessionKey.FirstViewingOfResults) != null )
                        )
					{
						int index = journeySummaryLines.IndexFromJourneyIndex(value);
						journeyViewState.ReturnSelectedJourneyRowIndex = value;
						journeyViewState.SelectedReturnJourney = journeySummaryLines[index].OriginalArrayIndex;
						journeyViewState.SelectedReturnJourneyID = journeySummaryLines[index].JourneyIndex;
						journeyViewState.SelectedReturnJourneyType = journeySummaryLines[index].Type;
						TDSessionManager.Current.ReturnJourneyMapState.Initialise();
						OnSelectionChanged(new EventArgs());
					}
				}
			}
		}

		/// <summary>
		/// The maximum number of results to show in the table
		/// </summary>
		private int MaxResultsToShow
		{
			get { return outward ? journeyViewState.MaxResultsToShowOutward : journeyViewState.MaxResultsToShowReturn; }
			set 
			{ 
				if (outward && ( journeyViewState.MaxResultsToShowOutward != value ))
					journeyViewState.MaxResultsToShowOutward = value;
				else if ( journeyViewState.MaxResultsToShowReturn != value )
					journeyViewState.MaxResultsToShowReturn = value; 

				// See if we need to reload the data. If so, do it now as we need to have the 
				// SelectedJourneyRowIndex set before anything which relies on it (ie maps, details)
				// is finalised.
				if ((journeySummaryLines != null) && (journeySummaryLines.MaxNumberOfResults != MaxResultsToShow))
				{
					LoadFormattedSummaryData(true);

					// Select the best match
					SelectedJourneyIndex = journeySummaryLines.BestMatch;
				}

			}
		}

		#endregion

	}
}