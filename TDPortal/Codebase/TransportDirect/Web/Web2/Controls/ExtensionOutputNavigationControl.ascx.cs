//   Rev 1.2   Apr 30 2008 15:19:58   sbarker
//   Corrected bug where ExtendFullItinerySummary screen doesn't disable Summary button

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
    public partial class ExtensionOutputNavigationControl : TDUserControl
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

            DisableButtonForCurrentPage();

			//SetSummaryButtonAltTextForCurrentPage();

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
				//check for normal result
				ITDJourneyResult result = TDSessionManager.Current.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;
					resultExists = (outwardExists || returnExists);

					// Get time types for journey.
					outwardArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}

			#endregion Determine state of Results

			

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
                    else if (myPageId == PageId.ExtendedFullItinerarySummary)
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
                case PageId.ExtendedFullItinerarySummary:
				case PageId.ExtensionResultsSummary:
                case PageId.ReplanFullItinerarySummary:
					DisableSummaryButton();
					break;

				case PageId.RefineDetails:
					DisableDetailsButton();
					break;

				case PageId.RefineMap:
					DisableMapsButton();
					break;

				case PageId.RefineTickets:
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

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineDetailsBack;
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

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineDetailsTabular;
		}

		/// <summary>
		/// Handler for the maps image button.
		/// </summary>
		private void buttonMaps_Click(object sender, EventArgs e)
		{
            // Write to the session the TransitionEvent to go to the Details Page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineMapView
;
		}

		/// <summary>
		/// Handler for the Tickets/Costs image button.
		/// </summary>
		private void buttonCosts_Click(object sender, EventArgs e)
		{
            // Write to the session the TransitionEvent to go to the Details Page.
            ITDSessionManager sessionManager =
                (ITDSessionManager)TDServiceDiscovery.Current
                [ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineTicketsView;
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
            TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(this.myPageId);

            // Reset the journey emissions page state, to clear it of any previous values
            TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            bool isCar = (viewState != null) && (viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested);

            // Check return journey
            if ((viewState != null) && (viewState.OriginalJourneyRequest != null) && (viewState.OriginalJourneyRequest.IsReturnRequired))
            {
                isCar = isCar && (viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested);
            }

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
