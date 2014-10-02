// *********************************************** 
// NAME                 : JourneyEmissions.aspx.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/11/2006 
// DESCRIPTION			: Displays journey emissions for selected journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyEmissions.aspx.cs-arc  $ 
//
//   Rev 1.4   Dec 19 2008 15:07:24   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 06 2008 11:06:40   apatel
//moved CommandSwitchView button to the top of the journey emissions page.
//Resolution for 4906: Carbon Dials - input page icons and table view button
//
//   Rev 1.2   Mar 31 2008 13:24:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:56   mturner
//Initial revision.
//
//   Rev 1.19   Sep 07 2007 16:56:26   mmodi
//Updated to use Expandable menu control for related links
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.18   Sep 07 2007 16:03:40   asinclair
//Updated for CO2 phase 2
//
//   Rev 1.17   Apr 03 2007 17:50:50   mmodi
//Only copies the Journey parameters to the page state if the page is in the Input mode
//Resolution for 4374: CO2: Mpg value is not used when viewing PT Emissions from Car Emissions
//
//   Rev 1.16   Mar 29 2007 22:16:58   asinclair
//Added new external link
//
//   Rev 1.15   Mar 28 2007 19:54:54   asinclair
//Added two new external links
//
//   Rev 1.14   Mar 06 2007 12:30:08   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.13.1.0   Mar 05 2007 17:17:24   mmodi
//Populated strings for related lnks controls
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.13   Jan 30 2007 16:30:04   mmodi
//Updated check when viewing page for a modified journey
//Resolution for 4351: CO2: Issue when view emissions for connecting Car journey
//
//   Rev 1.12   Jan 02 2007 12:37:26   mmodi
//Included check for Null journey params
//Resolution for 4327: Error when viewing Costs for Modified Find a Flight journey
//
//   Rev 1.11   Nov 27 2006 13:15:58   mmodi
//Removed code which cleared the Page Return stack
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.10   Nov 27 2006 10:39:28   dsawe
//amendsavesend control is invisible if modified journey
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.9   Nov 26 2006 18:56:18   mmodi
//Added code to handle Modify journeys scenarios
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.8   Nov 26 2006 18:47:10   dsawe
//deleted related links control
//
//   Rev 1.7   Nov 26 2006 15:39:34   mmodi
//Added Help page url
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.6   Nov 25 2006 13:59:54   mmodi
//Code to obtain car advanced options
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.5   Nov 24 2006 11:13:04   mmodi
//Added code to set car journey to emissions control
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Nov 19 2006 10:44:54   mmodi
//Updated layout of controls and styles
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.3   Nov 10 2006 17:01:50   mmodi
//Added version log details
//Resolution for 4240: CO2 Emissions
//

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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for JourneyEmissions.
	/// </summary>
	public partial class JourneyEmissions : TDPage
	{
		#region Private variables declaration
		private DataServices.DataServices populator;
		
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl resultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsControl journeyEmissionsControl;
		protected PrinterFriendlyPageButtonControl printerFriendlyControl;
		protected AmendSaveSendControl amendSaveSendControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
		protected System.Web.UI.WebControls.Panel panelJourneyEmissions;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
        //protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		// skip link

		#endregion

		#region Constructor
		/// <summary>
		/// CarParkInformation page contructor
		/// </summary>
		public JourneyEmissions()
		{
			this.pageId = PageId.JourneyEmissions;
			populator = (DataServices.DataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		#endregion

		#region Page_Load, OnPreRender

		protected void Page_Load(object sender, System.EventArgs e)
		{
       

			// Set up page
			labelTitle.Text = GetResource("JourneyEmissions.Title");
			buttonBack.Text = GetResource("JourneyEmissions.Back");

            this.buttonDashboardView.Text = journeyEmissionsControl.CommandSwitchDashboardViewText;

            // Left hand navigation menu set up
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

			// Set Help button url
			helpJourneyEmissions.HelpUrl = GetResource("JourneyEmissions.HelpPageUrl");

			
			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);	

			string labelTextOverride = GetResource("JourneyEmissions.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, true);

			// setup for the AmendSaveSend control and its child controls
			plannerOutputAdapter.AmendSaveSendControlPageLoad(amendSaveSendControl, this.pageId);

			PopulateFromSessionData();
			
			PopulateCarJourneys();
            
			if (!Page.IsPostBack)
			{
				PopulateCarPreferences();
			}

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyEmissions);
            expandableMenuControl.AddExpandedCategory("Related links");

            
		}

		/// <summary>
		/// OnPreRender method - overrides base and updates the visiblity
		/// of controls depending on which should be rendered. Calls base OnPreRender
		/// as the final step.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			SetupSkipLinksAndScreenReaderText();

			// Set printerFriendlyControl and dashboard view button  visibility, dependent on the state of the page
            if ((TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsState == JourneyEmissionsState.Input)
                || (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsState == JourneyEmissionsState.InputDetails))
            {
                printerFriendlyControl.Visible = false;
                buttonDashboardView.Visible = false;
            }
            else
            {
                printerFriendlyControl.Visible = true;
                buttonDashboardView.Visible = true;
            }

            if (journeyEmissionsControl.IsTableView)
            {
                journeyEmissionsControl.hideTextPanel();
            }

			base.OnPreRender(e);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Populates the Results Summary Control with the current journey
		/// </summary>
		private void PopulateFromSessionData()
		{

			resultsSummaryControl.ShowDeleteColumn = false;
			resultsSummaryControl.ShowSelectColumn = false;
			resultsSummaryControl.ShowEmptyDeleteColumn = false;
			resultsSummaryControl.ShowEmptySelectColumn = false;

			// Populate the control
			ResultsAdapter helper = new ResultsAdapter();

			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Need to check if the result is valid, if not then a Modify has been performed
			// so need to populate from the Extend Itinerary Manager
			if (sessionManager.JourneyResult.IsValid)
				resultsSummaryControl.SummaryLines = helper.FormattedSelectedJourneys(sessionManager, FormattedSummaryType.Replan);	
			else
				resultsSummaryControl.SummaryLines = helper.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Replan);

			// if a modify journey is in-progress or has been completed then do not show amendSaveSendControl
			if (sessionManager.ItineraryMode != ItineraryManagerMode.None)
				amendSaveSendControl.Visible = false;			
		}

		/// <summary>
		/// Obtains the car journeys to be used by the JourneyEmissionsControl
		/// </summary>
		private void PopulateCarJourneys()
		{
			decimal totalFuelCost = 0;

			// Need to check if the result is valid, if not then a Modify has been performed
			// so need to populate from the Extend Itinerary Manager
			if (TDSessionManager.Current.JourneyResult.IsValid)
			{
				// Get the car journeys from the session
				if (TDItineraryManager.Current.JourneyResult.OutwardRoadJourney() != null)
				{
					journeyEmissionsControl.CarRoadJourneyOutward = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
				}

				if (TDItineraryManager.Current.JourneyResult.ReturnRoadJourney() != null)
				{
					journeyEmissionsControl.CarRoadJourneyReturn = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
				}
			}
			else
			{
				// Get the car journeys from the Extend session
				if (ExtendItineraryManager.Current.OutwardLength > 0)
				{
					Journey[] journeys = ExtendItineraryManager.Current.OutwardJourneyItinerary;

					for (int i = 0; i <journeys.Length; i++)
					{
						if (journeys[i].Type == TDJourneyType.RoadCongested)
						{
							RoadJourney roadJourney = (RoadJourney)journeys[i];
							totalFuelCost = totalFuelCost + roadJourney.TotalFuelCost;
						}						
					}
				}

				if (ExtendItineraryManager.Current.ReturnLength > 0)
				{
					Journey[] journeys = ExtendItineraryManager.Current.ReturnJourneyItinerary;

					for (int i = 0; i <journeys.Length; i++)
					{
						if (journeys[i].Type == TDJourneyType.RoadCongested)
						{
							RoadJourney roadJourney = (RoadJourney)journeys[i];
							totalFuelCost = totalFuelCost + roadJourney.TotalFuelCost;
						}						
					}
				}
			}

			journeyEmissionsControl.TotalFuelCost = totalFuelCost;
		}

		/// <summary>
		/// If original journey had Advanced options (fuel consumption, and fuel cost), 
		/// this method assigns them to the JourneyEmissionsPageState for use
		/// </summary>
		private void PopulateCarPreferences()
		{
			JourneyEmissionsPageState emissionsPageState = TDSessionManager.Current.JourneyEmissionsPageState;
			TDJourneyParametersMulti journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;

			// IR4327: If unsuccessful retrieve of params, then force a reset. This scenario only occurs if original
			// journey was Flight only, and then Replanned to use Car
			if (journeyParams == null)
			{
				journeyParams = new TDJourneyParametersMulti();
				journeyParams.Initialise();
			}

			// Only update this page values when the the page state is in the Input .
			// This prevents the last user entered values on the page being reset if we return 
			// to the JourneyEmissions page from another page - e.g. the PT Journey Emissions page. This ensures
			// the last entered/selected values continue to be displayed.
			if ((emissionsPageState.JourneyEmissionsState == JourneyEmissionsState.Input) ||
				(emissionsPageState.JourneyEmissionsState == JourneyEmissionsState.InputDetails))
			{
				emissionsPageState.CarSize = journeyParams.CarSize;
				emissionsPageState.CarFuelType = journeyParams.CarFuelType;

				// False indicates user has specified a fuel consumption
				if (!journeyParams.FuelConsumptionOption)
				{
					emissionsPageState.FuelConsumptionOption = false;
					emissionsPageState.FuelConsumptionEntered = journeyParams.FuelConsumptionEntered;
					emissionsPageState.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
					emissionsPageState.FuelConsumptionValid = journeyParams.FuelConsumptionValid;

					// Set state to InputDetails so car preferences control is displayed in details mode
					if (emissionsPageState.JourneyEmissionsState == JourneyEmissionsState.Input)
						emissionsPageState.JourneyEmissionsState = JourneyEmissionsState.InputDetails;
				}

				// False indicates user has specified a fuel cost
				if (!journeyParams.FuelCostOption)
				{
					emissionsPageState.FuelCostOption = false;
					emissionsPageState.FuelCostEntered = journeyParams.FuelCostEntered;
					emissionsPageState.FuelCostValid = journeyParams.FuelCostValid;

					// Set state to InputDetails so car preferences control is displayed in details mode
					if (emissionsPageState.JourneyEmissionsState == JourneyEmissionsState.Input)
						emissionsPageState.JourneyEmissionsState = JourneyEmissionsState.InputDetails;
				}
			}
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{
			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("JourneyEmissions.imageMainContentSkipLink.AlternateText");
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}

		/// <summary>
		/// Wire up extra event handlers
		/// </summary>
		private void ExtraWiringEvents()
		{
			//this.headerControl.DefaultActionEvent += new System.EventHandler(this.buttonNext_Click);
			//this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            this.buttonDashboardView.Click += new System.EventHandler(this.buttonDashboardView_Click);
			amendSaveSendControl.AmendViewControl.SubmitButton.Click += new EventHandler(AmendViewControl_Click);
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Back button event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBack_Click(object sender, System.EventArgs e)
		{
			// Only need to induce a flag ensuring that this is on the way back from the info page.
			//TDSessionManager.Current.SetOneUseKey( SessionKey.IndirectLocationPostBack, string.Empty );
			
			// Reset the page state
			TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

			TransitionEvent te = TransitionEvent.JourneyEmissionsBack;

			if (TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count > 0)
			{
				PageId returnPage = (PageId)TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();
			
				switch(returnPage) 
				{
					case PageId.JourneyDetails:
						te = TransitionEvent.GoJourneyDetails;
						break;
					case PageId.JourneyFares:
						te = TransitionEvent.GoJourneyFares;
						break;
					case PageId.JourneyMap:
						te = TransitionEvent.GoJourneyMap;
						break;
					case PageId.RefineDetails:
						te = TransitionEvent.RefineDetailsSchematic;
						break;
					case PageId.RefineTickets:
						te = TransitionEvent.RefineTicketsView;
						break;
					case PageId.RefineMap:
						te = TransitionEvent.RefineMapView;
						break;
					case PageId.CarDetails:
						te = TransitionEvent.CarDetails;
						break;
					case PageId.ExtensionResultsSummary:
						te = TransitionEvent.ExtensionResultsSummaryView;
						break;
					case PageId.ExtendedFullItinerarySummary:
						te = TransitionEvent.ExtendedFullItinerarySummary;
						break;
					default:
						te = TransitionEvent.JourneyEmissionsBack;
						break;
				}
			}

			// Dont want to return to this page so push off this page
			// TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
		
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
		}

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendViewControl.
		/// This will switch Session partitions and display Summary page with the appropriate results.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendViewControl_Click(object sender, EventArgs e)
		{
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);

			plannerOutputAdapter.ViewPartitionResults(amendSaveSendControl.AmendViewControl.PartitionSelected);
		}

        /// <summary>
        /// Dashboard view button event handler. Changes Dashboard from diagram to table view.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void buttonDashboardView_Click(object sender, EventArgs e)
        {
            journeyEmissionsControl.SwitchDashboardView();
            this.buttonDashboardView.Text = journeyEmissionsControl.CommandSwitchDashboardViewText;
        }

		#endregion
	}
}
