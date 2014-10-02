// *********************************************** 
// NAME                 : OutputNavigationControl.ascx.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 05/09/2003
// DESCRIPTION			: Control for output pages that
// contains all the Summary/Details/Maps/Fares/TicketRetailers
// buttons.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/OutputNavigationControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 15 2008 17:12:04   mmodi
//Updated to set buttons for cycle pages
//
//   Rev 1.4   Oct 13 2008 16:44:20   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.0   Jun 20 2008 14:25:22   mmodi
//Updated for cycle pages
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Apr 30 2008 15:41:18   mmodi
//No change.
//Resolution for 4911: When a journey is modified in any way, the default results tab is the summary tab
//
//   Rev 1.2   Mar 31 2008 13:22:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:50   mturner
//Initial revision.
//
//   Rev 1.48   Oct 12 2007 13:56:34   asinclair
//Corrected logic for car journey checking
//
//   Rev 1.47   Oct 01 2007 17:10:54   asinclair
//Updated for Del 9.7 patch
//
//   Rev 1.46   Aug 31 2007 16:21:56   build
//Automatically merged from branch for stream4474
//
//   Rev 1.45.1.0   Aug 30 2007 17:53:48   asinclair
//Added Check CO2 button and code
//Resolution for 4474: DEL 9.7 Stream : Public Transport C02
//
//   Rev 1.45   Mar 28 2007 14:10:14   dsawe
//enabled 'cost/tickets button' for air mode
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.44   Jun 14 2006 12:00:20   esevern
//Code fix for vantive 3918704 - Enable buttons when no outward journey is returned but a return journey is.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.43   Mar 13 2006 17:28:32   asinclair
//Merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.42   Feb 23 2006 16:13:06   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.41   Jan 25 2006 12:35:34   pcross
//Removed unnecessary tooltips
//Resolution for 3505: UEE: Inconsistency in use of tooltips
//
//   Rev 1.40   Nov 22 2005 14:30:20   ralonso
//labelJourneyButtonsDescription removed. It does not exists in the HTML document
//
//   Rev 1.39   Nov 15 2005 14:27:52   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.38   Nov 04 2005 11:13:40   ralonso
//Manual merge of stream2816
//
//   Rev 1.37   Oct 31 2005 15:20:52   tmollart
//Merge with stream 2638.
//Resolution for 2929: Visit Planner Merge Activity
//
//
//   Rev 1.36   Sep 29 2005 12:49:36   build
//Automatically merged from branch for stream2673
//
//   Rev 1.35.2.0   Sep 13 2005 09:44:30   rgreenwood
//DN079 UEE TD088 JourneyExtension Tracking: Added extension button click logging
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.35   May 09 2005 11:02:18   COwczarek
//Disable all buttons if no outward results exist
//Resolution for 2449: Intermittant crash on car journeys
//
//   Rev 1.34   Apr 15 2005 12:48:02   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.33   Apr 13 2005 09:39:08   asinclair
//Reset the units to miles when the Map or Details button is clicked.
//
//   Rev 1.32   Apr 07 2005 18:30:06   rgeraghty
//Set visibilty of Tickets/Costs button in CheckSelectedJourneyTypes - resolves visibility problem for extend journey
//
//   Rev 1.31   Mar 31 2005 15:09:32   rgreenwood
//IR1901: removed code that hides Ticket/Costs button if a flight is selected, or forms a leg of a selected journey in door-to-door
//Resolution for 1901: Fares button now shown if journey contains a flight leg (DEL 7)
//
//   Rev 1.30   Mar 21 2005 16:02:34   pcross
//Updated to give the summary button alt text a slightly different message dependent on the host page
//
//   Rev 1.29   Mar 17 2005 17:50:04   pcross
//Updated journey buttons screenreader description text
//
//   Rev 1.28   Mar 17 2005 13:07:42   pcross
//No change.
//
//   Rev 1.27   Mar 16 2005 12:59:02   esevern
//allow display of costs/tickets button if the journey is private
//
//   Rev 1.26   Mar 07 2005 15:38:12   rhopkins
//Hide "Tickets/costs" button when in FindAFare or TrunkCostBased mode
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.25   Mar 01 2005 15:06:04   rgeraghty
//Removed fares and retailers buttons
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.24   Oct 08 2004 16:25:56   passuied
//changed code to handle Fares and Ticket Retailers visibility setting for normal results and Extended journey
//Resolution for 1698: Extended Journey: Exception when selectin Fares for Flight segment
//
//   Rev 1.23   Sep 09 2004 20:35:48   RHopkins
//Check that summaryLines contains the element that we want to use before trying to use it.
//
//   Rev 1.22   Sep 09 2004 11:02:34   passuied
//Implemented hiding of Fares and Retailers tab when a flight is selected.
//Resolution for 1536: Find a choices - Fares and Ticket retailer tabs are not hidden for a flight journey
//
//   Rev 1.21   Aug 19 2004 13:21:36   COwczarek
//When clicking extend journey button, set header to multi modal
//journey planner header.
//Resolution for 1318: When you submit a Find  a journey the Journey Planner header displayed
//
//   Rev 1.20   Aug 03 2004 11:52:18   COwczarek
//Use new FindAMode property
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.19   Jul 23 2004 16:36:12   jgeorge
//Updates for Del 6.1
//
//   Rev 1.18   Jul 22 2004 11:13:36   jgeorge
//Updates for Find a (Del 6.1)
//
//   Rev 1.17   Jun 23 2004 15:58:06   RHopkins
//Corrected logic for determining whether to hide or display the 'Fares' and 'Ticket retailers' buttons
//
//   Rev 1.16   Jun 17 2004 17:39:48   RHopkins
//Do not show 'Extend journey' if there are no Outward results
//
//   Rev 1.15   Jun 08 2004 15:56:28   RHopkins
//Suppress "Fares" and "Ticket retailers" when Full Itinerary is selected.
//
//   Rev 1.14   Jun 03 2004 16:27:56   RHopkins
//Reinstate click-event handler assignments that VisualStudio took upon itself to delete in previous version
//
//   Rev 1.13   Jun 03 2004 15:11:14   RHopkins
//Correction to server-based table definition
//
//   Rev 1.12   Jun 03 2004 14:30:20   RHopkins
//Allow for "Fares", "Ticket retailers" and "Extend journey" buttons to be completely suppressed, depending on selected journey type or presence of Itinerary
//
//   Rev 1.11   Jun 02 2004 12:06:04   RHopkins
//Correction to "Extend journey" button handling
//
//   Rev 1.10   May 27 2004 10:38:54   ESevern
//hidefarestickets() public
//
//   Rev 1.9   May 25 2004 09:53:48   JHaydock
//Update to FindSummary and related OutputNavigationControl
//
//   Rev 1.8   May 24 2004 17:13:24   ESevern
//correction to extend button display
//
//   Rev 1.7   May 24 2004 16:55:26   ESevern
//Added extend journey option
//
//   Rev 1.6   Mar 16 2004 16:39:26   CHosegood
//Del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.5   Mar 12 2004 19:31:04   AWindley
//DEL5.2 Inactive buttons should not be displayed
//
//   Rev 1.4   Nov 24 2003 16:05:52   kcheung
//Fixed so that if result is not valid then correctly handled.
//
//   Rev 1.3   Nov 21 2003 09:43:10   kcheung
//Updated for "no outward journey but return journey" case
//
//   Rev 1.2   Nov 17 2003 17:46:22   kcheung
//Updated commenting
//
//   Rev 1.1   Nov 13 2003 12:48:54   kcheung
//Full working version
//Resolution for 115: Journey planner buttons require disabling if no journeys found
//Resolution for 149: Streamline code for result summary buttons

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	A control that contains all the functionality of the
	///	navigation buttons on the output pages.
	/// </summary>
	public partial  class OutputNavigationControl : TDUserControl
	{

		protected System.Web.UI.HtmlControls.HtmlControl span1;

		//TD Buttons

		private PageId myPageId = PageId.Empty;
		private FindAMode currentMode;

		private TDItineraryManager itineraryManager;

		// State of results
		private bool resultExists = false;
		private bool outwardExists = false;
		private bool returnExists = false;
		private bool itineraryExists = false;
		private bool extendInProgress = false;
		private bool returnArriveBefore = false;

		private bool outwardArriveBefore = false;


		#region Initialise Method

		/// <summary>
		/// Initialises this control.
		/// </summary>
		/// <param name="callingPageId">The page id of the page
		/// containing this control.</param>
		public void Initialise(PageId callingPageId)
		{
			myPageId = callingPageId;
		}

		#endregion

		#region Page Load method

		/// <summary>
		/// Page Load Method. Initialise image buttons and images from
		/// the resourcing manager.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Load the image buttons and images from resourcing manager.
			
			// Tdbuttons
			buttonSummary.Text = Global.tdResourceManager.GetString(
				"JourneyPlanner.buttonSummary.Text", TDCultureInfo.CurrentUICulture);

			buttonDetails.Text = Global.tdResourceManager.GetString(
				"JourneyPlanner.buttonDetails.Text", TDCultureInfo.CurrentUICulture);

			buttonMaps.Text = Global.tdResourceManager.GetString(
				"JourneyPlanner.buttonMaps.Text", TDCultureInfo.CurrentUICulture);

			buttonCosts.Text = Global.tdResourceManager.GetString(
				"JourneyPlanner.buttonCosts.Text", TDCultureInfo.CurrentUICulture);

			buttonRefineJourney.Text = GetResource("OutputNavigationControl.buttonRefineThisJourney.Text");

			buttonCheckC02.Text = GetResource("OutputNavigationControl.buttonCheckC02.Text");

			// Images (disabled image buttons)
			imageSummary.ImageUrl = Global.tdResourceManager.GetString(
				"JourneySummary.imageDisabledSummaryButton.ImageUrl", TDCultureInfo.CurrentUICulture);

			imageDetails.ImageUrl = Global.tdResourceManager.GetString(
				"JourneyDetails.imageDisabledDetailsButton.ImageUrl", TDCultureInfo.CurrentUICulture);

			imageMaps.ImageUrl = Global.tdResourceManager.GetString(
				"JourneyMap.imageMapsDisabledButton.ImageUrl", TDCultureInfo.CurrentUICulture);

			imageCosts.ImageUrl = Global.tdResourceManager.GetString(
				"JourneyFares.imageDisabledCostsButton.ImageUrl", TDCultureInfo.CurrentUICulture);
			
			// Alternate text for Images
			imageSummary.AlternateText = Global.tdResourceManager.GetString(
				"OutputNavigationControl.buttonSummary.AlternateText", TDCultureInfo.CurrentUICulture);
            
			imageDetails.AlternateText = Global.tdResourceManager.GetString(
				"OutputNavigationControl.buttonDetails.AlternateText", TDCultureInfo.CurrentUICulture);

			imageMaps.AlternateText= Global.tdResourceManager.GetString(
				"OutputNavigationControl.buttonMaps.AlternateText", TDCultureInfo.CurrentUICulture);

			imageCosts.AlternateText = Global.tdResourceManager.GetString(
				"OutputNavigationControl.buttonCosts.AlternateText", TDCultureInfo.CurrentUICulture);								

			itineraryManager = TDItineraryManager.Current;

			SetSummaryButtonAltTextForCurrentPage();

		}

		#endregion 

		#region OnPreRender Method

		/// <summary>
		/// OnPreRender - updates the state of the buttons.
		/// </summary>
		protected override void OnPreRender(System.EventArgs e)
		{
			#region Determine state of Results
			currentMode = TDSessionManager.Current.FindAMode;

			TDItineraryManager itinerary = TDItineraryManager.Current;

			itineraryExists = (itinerary.Length > 0);
			extendInProgress = itinerary.ExtendInProgress;

			if ( itineraryExists && !extendInProgress )
			{
				resultExists = true;
				outwardExists = true;
				returnExists = (itinerary.ReturnLength > 0);
			}
			else
			{
                // outwardExists and returnExists flags are false by default
                
                // check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);
                resultExists = (outwardExists || returnExists);
                

				//check for normal result
				ITDJourneyResult result = TDSessionManager.Current.JourneyResult;
				if(result != null) 
				{
                    outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists; // or with itself because may have cycle journeys
					returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;
					resultExists = (outwardExists || returnExists);

					// Get time types for journey.
					outwardArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}

			#endregion Determine state of Results

			// Call method to update the state of the buttons.
			UpdateButtons();

			// Call method to check if the "Fares" and "Ticket Retailers"
			// button need to be disabled. This would be the case if
			// only private journeys were selected. This needs to be that
			// OnPreRender because the selected journey is updated by
			// an event handler on the Summary Control.
			CheckSelectedJourneyTypes();

			// Call base
			base.OnPreRender(e);
		}

		#endregion

		#region Methods to check/update the state of the buttons

		/// <summary>
		/// Updates the state of the buttons depending on whether
		/// a result exists or not.
		/// </summary>
		private void UpdateButtons()
		{
			// If an output result exists, then the button for the
			// current page should be disabled.  If no output result
			// exists, then all buttons should be disabled.

			//If there is only a return journey do not show the modify
			//journey button.
			if (outwardExists)
			{
				buttonRefineJourney.Visible = true;
			}
			else if (!outwardExists && returnExists)
			{
				buttonRefineJourney.Visible = false;
			}

			if (outwardExists || returnExists)
			{
				// A result was found, so disable the button
				// for the current page.
				DisableButtonForCurrentPage();
			}
			else
			{
				// No results were found. Disable all buttons.
				DisableAllButtons();
			}

			// commmented follwing code for displaying tickets cost button for local zonal services phase 2 & 3
//			if (currentMode == FindAMode.Flight)
//				buttonCosts.Visible = false; //hide the 'Tickets/Costs' button'

		}

		/// <summary>
		/// Detects if oneMode is in the array of ModeType
		/// </summary>
		/// <param name="modes">mode types to look into</param>
		/// <param name="oneMode">mode to look for</param>
		/// <returns>true if found. False otherwise</returns>
		private bool ContainsMode(ModeType[] modes, ModeType oneMode)
		{
			if (modes.Length ==0)
				return false;
			foreach(ModeType mode in modes)
			{
				if (mode == oneMode)
					return true;
				
			}

			return false;
		}

		/// <summary>
		/// Checks the current selected journey types. If the selected
		/// type(s) are car-only then the ticket/costs button will be
		/// disabled otherwise no state changes are made.
		/// DEL 7: since we now want to display costs for car journeys
		/// the tickets/costs button should not be disabled if this is 
		/// a private journey.
		/// </summary>
		public void CheckSelectedJourneyTypes()
		{
			TDJourneyViewState viewState = null;

			if (outwardExists)
			{
				ExtendItineraryManager extendItineraryManager = TDItineraryManager.Current as ExtendItineraryManager;

				// If an itinerary manager is of extend type, check to determine if ticket/costs button 
				// is available for the selected itinerary segment.
				if (extendItineraryManager != null && 
					(extendItineraryManager.FullItinerarySelected || 
					FindInputAdapter.IsCostBasedSearchMode(extendItineraryManager.SpecificFindAMode(extendItineraryManager.SelectedItinerarySegment))))
				{
					// Don't display 'Tickets/Costs' unless the journey fares page is the current page
					if (myPageId == PageId.JourneyFares)
					{
						buttonCosts.Visible = true;
					}
					else
					{
						buttonCosts.Visible = false;
					}

				}
				else
				{
					// Do not show the Tickets/Costs button when in cost-based mode
					TDItineraryManager itineraryManager = TDItineraryManager.Current;

					if (FindInputAdapter.IsCostBasedSearchMode(currentMode))
					{
						buttonCosts.Visible = false;
					}
					else
					{
						// Update the state of the ticket/costs button ONLY IF
						// the current page is not the fares page.
						// This is because if you are on the fares page, the
						// button should be permanently disabled anyway.
						
						if (myPageId != PageId.JourneyFares)
						{
							bool publicSelected = false;
							bool flightSelected = false;

							viewState = itineraryManager.JourneyViewState;

							if (outwardExists)
							{
								TDJourneyType outwardJourneyType = viewState.SelectedOutwardJourneyType;

								publicSelected =
									outwardJourneyType == TDJourneyType.PublicOriginal ||
									outwardJourneyType == TDJourneyType.PublicAmended;

								if (publicSelected)
								{
									ModeType[] modes = TDJourneyResult.GetAllModes(
										itineraryManager.JourneyResult.OutwardPublicJourney(
										itineraryManager.SelectedOutwardJourneyID));

									flightSelected = ContainsMode( modes, ModeType.Air);
								}
						
								if ( !publicSelected && returnExists )
								{
									// Return Public Journey exists - check to see if it
									// is currently selected.

									// Get the return journey type
									TDJourneyType returnJourneyType = viewState.SelectedReturnJourneyType;

									publicSelected =
										returnJourneyType == TDJourneyType.PublicAmended ||
										returnJourneyType == TDJourneyType.PublicOriginal;
								}


								if (flightSelected && publicSelected && returnExists)
								{
									ModeType[] modes = TDJourneyResult.GetAllModes(
										itineraryManager.JourneyResult.ReturnPublicJourney(
										itineraryManager.SelectedReturnJourneyID));

									flightSelected = ContainsMode( modes, ModeType.Air);
								}
							}
							//Runs if there is no outward results but if there are return results
							else if (returnExists)
							{

								TDJourneyType returnJourneyType = viewState.SelectedReturnJourneyType;

								publicSelected =
									returnJourneyType == TDJourneyType.PublicAmended ||
									returnJourneyType == TDJourneyType.PublicOriginal;

								ModeType[] modes = TDJourneyResult.GetAllModes(
									itineraryManager.JourneyResult.ReturnPublicJourney(
									itineraryManager.SelectedReturnJourneyID));

								flightSelected = ContainsMode( modes, ModeType.Air);
							}

							//display the tickets/costs button if not in find a flight mode
							if (currentMode != FindAMode.Flight)
								buttonCosts.Visible = true; 

                            // dont't display the tickets/costs and modify for find a cycle mode
                            if (currentMode == FindAMode.Cycle)
                            {
                                buttonCosts.Visible = false;
                                buttonRefineJourney.Visible = false;
                            }
						}
					}
				}
			}
		}

		/// <summary>
		/// Disables all the journey planner navigation buttons.
		/// </summary>
		private void DisableAllButtons()
		{
			// Call methods to disable all buttons.
			DisableSummaryButton();
			DisableDetailsButton();
			DisableMapsButton();
			DisableCostsButton();
			buttonRefineJourney.Visible = false;
			buttonCheckC02.Visible = false;
		}

		/// <summary>
		/// Disables the button for the current page. e.g. if on
		/// the summary page then the summary button will be disabled.
		/// </summary>
		private void DisableButtonForCurrentPage()
		{
			// "Disable" the button for the current page.
			switch(this.myPageId)
			{
				case PageId.JourneySummary:
                case PageId.CycleJourneySummary:
					DisableSummaryButton();
					break;

				case PageId.JourneyDetails:
                case PageId.CycleJourneyDetails:
					DisableDetailsButton();
					break;

				case PageId.JourneyMap:
                case PageId.CycleJourneyMap:
					DisableMapsButton();
					break;

				case PageId.JourneyFares:
					DisableCostsButton();
					break;
			}
		}
		/// <summary>
		/// The AlT text for the Summary button is dependent on the calling page.
		/// This proc sets accordingly.
		/// Note that the ALT text appears in tooltip and is read by screenreader browsers.
		/// </summary>
		private void SetSummaryButtonAltTextForCurrentPage()
		{
			// Set summary button ALT text for the current page.
			switch(this.myPageId)
			{
				case PageId.JourneySummary:
					buttonSummary.ToolTip = Global.tdResourceManager.GetString(
						"OutputNavigationControl.buttonSummary.AlternateText", TDCultureInfo.CurrentUICulture);
					break;

				case PageId.JourneyDetails:
					buttonDetails.ToolTip = Global.tdResourceManager.GetString(
						"OutputNavigationControl.buttonDetails.AlternateText", TDCultureInfo.CurrentUICulture);
					break;

				case PageId.JourneyMap:
					buttonMaps.ToolTip = Global.tdResourceManager.GetString(
						"OutputNavigationControl.buttonMaps.AlternateText", TDCultureInfo.CurrentUICulture);
					break;

				case PageId.JourneyFares:
					buttonCosts.ToolTip	= Global.tdResourceManager.GetString(
						"OutputNavigationControl.buttonCosts.AlternateText", TDCultureInfo.CurrentUICulture);
					break;
			}
		}

		#endregion

		#region Methods to disable the buttons

		/// <summary>
		/// Disables the summary button.
		/// </summary>
		private void DisableSummaryButton()
		{
			buttonSummary.Visible = false;
			imageSummary.Visible = true;
		}

		/// <summary>
		/// Disables the details button.
		/// </summary>
		private void DisableDetailsButton()
		{
			buttonDetails.Visible = false;
			imageDetails.Visible = true;
		}

		/// <summary>
		/// Disables the maps button.
		/// </summary>
		private void DisableMapsButton()
		{
			buttonMaps.Visible = false;
			imageMaps.Visible = true;
		}

		/// <summary>
		/// Disables the fares button.
		/// </summary>
		private void DisableCostsButton()
		{
			buttonCosts.Visible = false;
			imageCosts.Visible = true;
		}


		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraWiringEvents();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		private void ExtraWiringEvents() 
		{
			
			this.buttonSummary.Click += new EventHandler(this.buttonSummary_Click);
			this.buttonDetails.Click += new EventHandler(this.buttonDetails_Click);
			this.buttonMaps.Click += new EventHandler(this.buttonMaps_Click);
			this.buttonCosts.Click += new EventHandler(this.buttonCosts_Click);
			this.buttonRefineJourney.Click += new EventHandler(this.buttonRefineJourney_Click);
			this.buttonCheckC02.Click += new System.EventHandler(this.buttonCheckC02_Click);
		}
		
		#endregion

		#region Handler for the button clicks

		/// <summary>
		/// Handler for the summary image button.
		/// </summary>
		private void buttonSummary_Click(object sender, EventArgs e)
		{
			// Write to the session the TransitionEvent to go to the Summary page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current [ServiceDiscoveryKey.SessionManager];

			sessionManager.JourneyMapState.Initialise();
			sessionManager.ReturnJourneyMapState.Initialise();

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneySummary;
		}

		/// <summary>
		/// Handler for the details image button.
		/// </summary>
		private void buttonDetails_Click(object sender, EventArgs e)
		{
			// Write to the session the TransitionEvent to go to the Details Page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.JourneyMapState.Initialise();
			sessionManager.ReturnJourneyMapState.Initialise();

			//When entering the Details page, set the Road Units to Miles
			sessionManager.InputPageState.Units = RoadUnitsEnum.Miles;

            // Find a cycle has its own details page
            if (sessionManager.FindAMode == FindAMode.Cycle)
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.CycleJourneyDetails;
            }
            else
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyDetails;
            }
		}

		/// <summary>
		/// Handler for the maps image button.
		/// </summary>
		private void buttonMaps_Click(object sender, EventArgs e)
		{
			// Write to the session the TransitionEvent to go to the Map Page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.JourneyMapState.Initialise();
			sessionManager.ReturnJourneyMapState.Initialise();

			//When entering the Maps page, set the Road Units to Miles
			sessionManager.InputPageState.Units = RoadUnitsEnum.Miles;
            
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyMap;
		}

		/// <summary>
		/// Handler for the Tickets/Costs image button.
		/// </summary>
		private void buttonCosts_Click(object sender, EventArgs e)
		{
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current [ServiceDiscoveryKey.SessionManager];

			sessionManager.JourneyMapState.Initialise();
			sessionManager.ReturnJourneyMapState.Initialise();

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyFares;
		}


		/// <summary>
		/// Handler for refine journey button click.
		/// Shows a page of refine journey options appropriate to journey currently selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRefineJourney_Click(object sender, EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			//sessionManager.Session[SessionKey.Transferred] = false;
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineJourney;
		}


		/// <summary>
		/// Handler for Check C02 button click.
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCheckC02_Click(object sender, System.EventArgs e)
		{
			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			// Reset the journey emissions page state, to clear it of any previous values
			TDSessionManager.Current.JourneyEmissionsPageState.Initialise();
			
			// Determine if the selected outward journey is public or road
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			bool isCar = (viewState != null) && ((viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested) && (!viewState.OriginalJourneyRequest.IsReturnRequired)) || ((viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested) && (viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested));
			
			// Navigate to the Car emissions page or the Journey Emissions Compare Journey page dependent on journey selected
			if (!isCar) 
			{
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState = JourneyEmissionsCompareState.JourneyCompare;
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissionsCompareJourney;
			
			}
			else
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;		
		
		}
		
		#endregion Handler for the button clicks
	}
}
