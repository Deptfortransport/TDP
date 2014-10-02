// *********************************************** 
// NAME                 : JourneyEmissionsCompareJourney.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/02/2007
// DESCRIPTION			: Page to display the CO2 emissions for public transport modes for a Planned Journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyEmissionsCompareJourney.aspx.cs-arc  $
//
//   Rev 1.6   Mar 30 2010 10:15:14   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.5   Feb 24 2010 13:25:34   mmodi
//Changed emissions control ID to following update to the Emissions UnitsSwitch js file
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Oct 13 2008 16:44:28   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.0   Jul 30 2008 11:13:04   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 21 2008 11:15:02   mmodi
//Handle return to replan journey page
//Resolution for 4995: Server error thrown when going back from CO2 emissions screen
//
//   Rev 1.2   Mar 31 2008 13:24:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:00   mturner
//Initial revision.
//
//   Rev 1.10   Sep 11 2007 12:01:54   mmodi
//Added expandable links control for information section
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.9   Sep 07 2007 16:56:28   mmodi
//Updated to use Expandable menu control for related links
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.8   Sep 07 2007 15:55:38   asinclair
//Updated back button code for CO2 phase 2
//
//   Rev 1.7   Apr 10 2007 15:57:10   mmodi
//Back button now takes page back to initial state before returning to the previous page
//Resolution for 4379: CO2: Back button issue on Compare CO2 for journey page
//
//   Rev 1.6   Apr 04 2007 14:48:08   mmodi
//Removed internal links
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.5   Mar 29 2007 22:17:30   asinclair
//Added new external link
//
//   Rev 1.4   Mar 09 2007 16:29:16   mmodi
//Updated related links string
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.3   Mar 05 2007 17:21:14   mmodi
//Updated to use JourneyEmisionRelatedLinksControl
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.2   Feb 28 2007 15:52:58   mmodi
//Updated Help page url and setting distance units
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.1   Feb 26 2007 11:55:38   mmodi
//Updated when page loaded from visit planner
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 17:02:56   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for JourneyEmissionsCompareJourney.
	/// </summary>
	public partial class JourneyEmissionsCompareJourney : TDPage
	{
		#region Controls
     
		// Page Controls
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;

		// MCMS Controls

		// Main Content controls
		
		protected PrinterFriendlyPageButtonControl printerFriendlyControl;

		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;		
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl2 resultsSummaryControl;
        protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCompareControl journeyEmissionsCompareControlOutward;

		// Skip links
		#endregion

		#region Private variables

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyEmissionsCompareJourney()
		{
			this.pageId = PageId.JourneyEmissionsCompareJourney;
		}

		#endregion

		#region Page_Load, Page_PreRender

		/// <summary>
		/// Page_Load
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			#region UEE, Navigation links, and Information panels

			// Client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

			// Left hand navigation menu set up
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("JourneyEmissionsCompare.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("JourneyEmissionsCompare.clientLink.LinkText");

			// Determine url to save as bookmark
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			
			string baseChannel = string.Empty;
			
			if (TDPage.SessionChannelName != null)
			{
				baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
			}
			
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyEmissionsCompare);
			
			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			string url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
			
			clientLink.BookmarkUrl = url;

			// Set up related links control
			labelInformationLinks.Text = GetResource("JourneyEmissionsCompare.InformationLinks.Text");
			informationLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.JourneyEmissionsCompareInfo);
			
			#endregion

			labelTitle.Text = GetResource("JourneyEmissionsCompare.Title");
			buttonBack.Text = GetResource("JourneyEmissionsCompare.Back");			
			buttonNext.Text = GetResource("JourneyEmissionsDistanceInputControl.Next");

            this.PageTitle = GetResource("JourneyEmissionsCompareJourney.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            
			// Set Help button url
			helpJourneyEmissions.HelpUrl = GetResource("JourneyEmissionsCompareJourney.HelpPageUrl");

			// Set up the Journey Results Summary control
			PopulateSummaryControl();

			// Set printable for controls
            journeyEmissionsCompareControlOutward.NonPrintable = true;

			// Skip links
			imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.AlternateText = GetResource("JourneyEmissionsCompare.imageMainContentSkipLink.AlternateText");

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyEmissionsCompareJourney);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Page PreRender
		/// </summary>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			SetControlVisibility();
			PopulateCompareControl();

			// Set the params to pass for units for Printer Page
			int  units = TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceUnit;

			// default value for units shown on Journey details page is miles therefore set it to this
			printerFriendlyControl.UrlParams = "Units=miles";
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Populates the Summary control to show the users selected journey
		/// </summary>
		private void PopulateSummaryControl()
		{
			// If this page is loaded from Visit Planner, then the results summary fail to load due to obsolete
			// properties in the VisitPlannerItineraryManager, therefore dont display the results summary panel
			VisitPlannerItineraryManager vpim = TDItineraryManager.Current as VisitPlannerItineraryManager;
			if (vpim == null)
			{
				// Summary table title
				PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);

				string labelTextOverride = GetResource("JourneyEmissions.labelTitleControl");
				plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, true, true);

				// Show the Transport row but not the column
				resultsSummaryControl.ShowTransportColumn = false;
				resultsSummaryControl.ShowTransportRow = true;

				// Show the dates on the leave/arrive time columns
				resultsSummaryControl.ShowLeaveArriveDate = true;

				// Populate the control with the journey
				ResultsAdapter helper = new ResultsAdapter();
				ITDSessionManager sessionManager = TDSessionManager.Current;

				// Need to check where to get the selected journey from, Journey Result, Cycle Result, or Itinerary Manager
				if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.IsValid))
                    || ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.IsValid)))
					resultsSummaryControl.SummaryLines = helper.FormattedSelectedJourneys(sessionManager, FormattedSummaryType.Replan);	
				else
					resultsSummaryControl.SummaryLines = helper.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Replan);	
			}
			else
			{
				panelSummary.Visible = false;
			}
		}

		/// <summary>
		/// Sets the visibility of the controls dependent on the page state
		/// </summary>
		private void SetControlVisibility()
		{
			// Set visibility of the controls
			if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState == JourneyEmissionsCompareState.JourneyDefault)
			{
				printerFriendlyControl.Visible = false;
				buttonNext.Visible = true;
                journeyEmissionsCompareControlOutward.Visible = true;
			}
			else if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState == JourneyEmissionsCompareState.JourneyCompare)
			{
				printerFriendlyControl.Visible = true;
				buttonNext.Visible = false;
                journeyEmissionsCompareControlOutward.Visible = true;
			}
			else
			{
				// Place in the default state
				printerFriendlyControl.Visible = false;
				buttonNext.Visible = true;
                journeyEmissionsCompareControlOutward.Visible = true;
			}
		}

		/// <summary>
		/// Populates the Compare control with state, journey info...
		/// </summary>
		private void PopulateCompareControl()
		{
            ITDSessionManager sessionManager = TDSessionManager.Current;

			// Determine where we get selected journey from Journey Result or Itinerary Manager
			// and then set the Compare emissions control flags.
			// The Compare emissions control will then populate distances and calculate emissions
			// from the session
            if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.IsValid))
                || ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.IsValid)))
                journeyEmissionsCompareControlOutward.UseSessionManager = true;
			else
                journeyEmissionsCompareControlOutward.UseItineraryManager = true;

			// Set the mode for the Compare emissions control
            if (sessionManager.JourneyEmissionsPageState.JourneyEmissionsCompareState == JourneyEmissionsCompareState.JourneyCompare)
                journeyEmissionsCompareControlOutward.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.JourneyCompare;
			else
                journeyEmissionsCompareControlOutward.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.JourneyDefault;
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
            buttonBack.Click += new EventHandler(this.buttonBack_Click);
            buttonNext.Click += new EventHandler(this.ButtonNext_Click);
		}
		#endregion

		#region Event handlers
		/// <summary>
		/// Back button event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonBack_Click(object sender, System.EventArgs e)
		{

				TransitionEvent te = TransitionEvent.JourneyEmissionsCompareJourneyBack;
		
				if (TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count > 0)
				{
					PageId returnPage = (PageId)TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();
					switch(returnPage) 
					{
						case PageId.JourneyDetails:
							te = TransitionEvent.GoJourneyDetails;
							break;
						case PageId.RefineDetails:
							te = TransitionEvent.RefineDetailsSchematic;
							break;
						case PageId.JourneyEmissions:
							te = TransitionEvent.JourneyEmissions;
							break;
						case PageId.VisitPlannerResults:
							te = TransitionEvent.VisitPlannerResultsSchematicView;
							break;
						case PageId.ExtensionResultsSummary:
							te = TransitionEvent.ExtensionResultsSummaryView;
							break;
						case PageId.ExtendedFullItinerarySummary:
							te = TransitionEvent.ExtendedFullItinerarySummary;
							break;
                        case PageId.ReplanFullItinerarySummary:
                            te = TransitionEvent.ReplanFullItinerarySummary;
                            break;
                        case PageId.CycleJourneyDetails:
                            te = TransitionEvent.CycleJourneyDetails;
                            break;
						default:
							te = TransitionEvent.JourneyEmissionsCompareJourneyBack;
							break;
					}
				}
				
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
		}

		/// <summary>
		/// Click event for next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void ButtonNext_Click(object sender, EventArgs e)
		{
			// Update the page state
			TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState = JourneyEmissionsCompareState.JourneyCompare;

			// Reload this page, which allows the controls to be reloaded in their (new) states
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissionsCompareJourney;
		}
		#endregion
	}
}
