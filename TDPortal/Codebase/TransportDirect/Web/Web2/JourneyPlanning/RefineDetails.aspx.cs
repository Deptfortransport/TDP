// *********************************************** 
// NAME                 : RefineDetails.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 30/01/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Displays the full itinerary details of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/RefineDetails.aspx.cs-arc  $
//
//   Rev 1.10   Mar 29 2010 16:40:36   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.9   Feb 16 2010 11:16:16   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 12 2010 11:14:08   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Nov 29 2009 12:45:38   mmodi
//Updated state to indicate which journey map is to be shown
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 23 2009 10:35:12   mmodi
//Updated selected map leg index
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 11 2009 16:43:18   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.4   Jan 06 2009 11:10:00   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jul 08 2008 10:24:36   apatel
//made diagram details to show by default
//Resolution for 5041: Extended journey detail page displays in table mode by default
//
//   Rev 1.2   Mar 31 2008 13:25:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:12   mturner
//Initial revision.
//
//   Rev 1.10   Sep 14 2007 17:28:28   asinclair
//removed Co2 button
//
//   Rev 1.9   Mar 06 2007 12:29:58   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.8.1.0   Feb 20 2007 17:33:16   mmodi
//Added Compare CO2 emissions buttons
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.8   Mar 20 2006 14:47:04   NMoorhouse
//Updated following review comments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 14 2006 11:42:48   NMoorhouse
//Post stream3353 merge: added new HeadElementControl, HeaderControls and reference new ResourceManager
//
//   Rev 1.6   Mar 10 2006 09:53:26   NMoorhouse
//Fixed problem display car extension details
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 06 2006 18:17:34   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 03 2006 16:08:04   pcross
//Skip links / layout changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 02 2006 15:13:14   tolomolaiye
//Ensure itinerary manager is initialised
//
//   Rev 1.2   Feb 24 2006 14:37:24   NMoorhouse
//Changes to the extend journey option
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 14 2006 16:25:44   NMoorhouse
//Linking up of pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 09 2006 16:21:50   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 08 2006 15:55:40   NMoorhouse
//Updated with changes to SD
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 07 2006 18:47:26   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
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

using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for RefineDetails.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RefineDetails : TDPage
	{
		// HTML Controls
		protected System.Web.UI.WebControls.Panel combinedResultsSummaryPanel;
		protected System.Web.UI.WebControls.Panel summaryPanel;
		

		// Button controls
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyControl;
		
		// TD Web Controls
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsControl journeyDetailsControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsControl journeyDetailsControlReturn;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableControl journeyDetailsTableControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableControl journeyDetailsTableControlReturn;
		protected TransportDirect.UserPortal.Web.Controls.JourneyBuilderControl addExtensionControl;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress;

		/// <summary>
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary;

		private bool returnArriveBefore;
		protected TransportDirect.UserPortal.Web.Controls.TDButton Tdbutton1;
		private bool outwardArriveBefore;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public RefineDetails() : base()
		{
			// Set page Id
			pageId = PageId.RefineDetails;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Event Handlers
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

            if (!IsPostBack)
            {
                itineraryManager.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode = true;
                itineraryManager.JourneyViewState.ShowReturnJourneyDetailsDiagramMode = true;
            }

			// Initialise text and button properties
			PopulateControls();

			// Initialise journey details controls
			InitialiseJourneyDetailsControls();

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextRefineDetails);
            expandableMenuControl.AddExpandedCategory("Related links");

		}

		/// <summary>
		/// Handles the page prerender event. This performs any last-minute updates to the controls
		/// that are displayed to the user
		/// </summary>
		protected void OnPreRender(object sender, System.EventArgs e)
		{
			itineraryManager = TDItineraryManager.Current;
			SetControlVisibility();

			SetupSkipLinksAndScreenReaderText();
		}

		/// <summary>
		/// Handles back button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backButton_Click(object sender, EventArgs e)
		{
			// Navigate back to summary page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineDetailsBack;
		}

		/// <summary>
		/// Handles the button click to toggle the display format of outward details 
		/// between tabular and graphical formats
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void buttonShowTableOutward_Click(object sender, EventArgs e)
		{
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			bool showDiagramMode = viewState.ShowOutwardJourneyDetailsDiagramMode;
			viewState.ShowOutwardJourneyDetailsDiagramMode = !showDiagramMode;			
		}

		/// <summary>
		/// Handles the button click to toggle the display format of return details 
		/// between tabular and graphical formats
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void buttonShowTableReturn_Click(object sender, EventArgs e)
		{
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			bool showDiagramMode = viewState.ShowReturnJourneyDetailsDiagramMode;
			viewState.ShowReturnJourneyDetailsDiagramMode = !showDiagramMode;			
		}

		/// <summary>
		/// Handler for map button being pressed on the segment control (control used for public journeys)
		/// When pressed, load the map with the journey details for the selected leg only.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void journeyDetailsControlOutward_MapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            viewState.OutwardShowMap = true;
            viewState.ReturnShowMap = false;

			// Now save selected leg as a one use key
			int selectedLeg = e.LegIndex;
			TDSessionManager.Current.SetOneUseKey(SessionKey.MapOutwardSelectedLeg, selectedLeg.ToString(TDCultureInfo.CurrentUICulture));
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineMapView;
		}

		/// <summary>
		/// Handler for map button being pressed on the segment control (control used for public journeys)
		/// When pressed, load the map with the journey details for the selected leg only.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void journeyDetailsControlReturn_MapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            viewState.OutwardShowMap = false;
            viewState.ReturnShowMap = true;

			// Now save selected leg as a one use key
			int selectedLeg = e.LegIndex;
			TDSessionManager.Current.SetOneUseKey(SessionKey.MapReturnSelectedLeg, selectedLeg.ToString(TDCultureInfo.CurrentUICulture));
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineMapView;
		}


		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("RefineDetails.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("RefineDetails.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("RefineDetails.labelIntroductoryText.Text");
			labelOutwardDirection.Text = GetResource("RefineDetails.labelOutwardDirection.Text");
			labelReturnDirection.Text = GetResource("RefineDetails.labelReturnDirection.Text");

			// Get correct resource strings for buttons
			backButton.Text = GetResource("RefineDetails.backButton.Text");

			// Set Help Button URL
			helpButton.HelpUrl = GetResource("RefineDetails.helpButton.HelpUrl");

			addExtensionControl.ItineraryManager = itineraryManager;

		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;

					// Get time types for journey.
					outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		/// <summary>
		/// Initialise the Journey Details Controls
		/// </summary>
		private void InitialiseJourneyDetailsControls()
		{
			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			DetermineStateOfResults();

			// Initialise those controls required for displaying an outward journey or extension
			if (outwardExists)
			{
				if (!itineraryManager.FullItinerarySelected)
				{
					// An individual journey is selected

					// Determine the journey details object to use in initialising the
					// details controls

					Journey outJourney = null;
		
					if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						// the original journey has been selected
						outJourney = journeyResult.OutwardPublicJourney(
							viewState.SelectedOutwardJourneyID);
					}
					else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						outJourney = journeyResult.AmendedOutwardPublicJourney;
					} 
					else if (viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested) 
					{
						//private journey has been selected
						outJourney = journeyResult.OutwardRoadJourney();
					}

					if (outJourney != null) 
					{
                        journeyDetailsControlOutward.Initialise(outJourney, true, false, false, true, itineraryManager.JourneyRequest, tdSessionManager.FindAMode);
						journeyDetailsControlOutward.MyPageId = pageId;
						journeyDetailsTableControlOutward.Initialise(outJourney, true, tdSessionManager.FindAMode);
						journeyDetailsTableControlOutward.MyPageId = pageId;
					}
				}
				else
				{
					// Full journey summary selected
                    journeyDetailsControlOutward.Initialise(true, false, false, true, tdSessionManager.FindAMode);
					journeyDetailsControlOutward.MyPageId = pageId;	  
					journeyDetailsTableControlOutward.Initialise(true, tdSessionManager.FindAMode);
					journeyDetailsTableControlOutward.MyPageId = pageId;
					labelOutwardSummary.Text = String.Concat(GetResource("RefineDetails.JourneySummary.TextElement1"), itineraryManager.OutwardDepartLocation().Description, GetResource("RefineDetails.JourneySummary.TextElement2"), itineraryManager.OutwardDepartDateTime().ToString("HH:mm"));
				}
			}

			if (returnExists)
			{
				if (!itineraryManager.FullItinerarySelected)
				{
					// An individual journey is selected

					// Determine the journey details object to use in initialising the
					// details controls

					Journey retJourney = null;

					if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						// the original journey has been selected
						retJourney = journeyResult.ReturnPublicJourney(
							viewState.SelectedReturnJourneyID);
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						retJourney = journeyResult.AmendedReturnPublicJourney;
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
					{
						//private journey has been selected
						retJourney = journeyResult.ReturnRoadJourney();
					}

					if (retJourney != null) 
					{
						journeyDetailsControlReturn.Initialise(retJourney, false, false, false, true, itineraryManager.JourneyRequest,tdSessionManager.FindAMode);
						journeyDetailsControlReturn.MyPageId = pageId;
						journeyDetailsTableControlReturn.Initialise(retJourney, false, tdSessionManager.FindAMode);
						journeyDetailsTableControlReturn.MyPageId = pageId;			
					}
			
				}
				else
				{
					// Full journey summary selected
					journeyDetailsControlReturn.Initialise(false, false,false,true,tdSessionManager.FindAMode);			
					journeyDetailsControlReturn.MyPageId = pageId;
					journeyDetailsTableControlReturn.Initialise(false, tdSessionManager.FindAMode);
					journeyDetailsTableControlReturn.MyPageId = pageId;
					labelReturnSummary.Text = String.Concat(GetResource("RefineDetails.JourneySummary.TextElement1"), itineraryManager.ReturnDepartLocation().Description, GetResource("RefineDetails.JourneySummary.TextElement2"), itineraryManager.ReturnDepartDateTime().ToString("HH:mm"));
				}

			}

		}

		/// <summary>
		/// Determines which controls should be visible
		/// </summary>
		private void SetControlVisibility()
		{
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			//Only show add extension button if an extend is in progress
			addExtensionControl.Visible = itineraryManager.ExtendInProgress;

			if (outwardExists)
			{
				outwardPanel.Visible = true;

				// Journey Details control is visible if diagram mode
				journeyDetailsControlOutward.Visible = viewState.ShowOutwardJourneyDetailsDiagramMode;

				// Journey Details table control is visible if not diagram mode.
				journeyDetailsTableControlOutward.Visible = !viewState.ShowOutwardJourneyDetailsDiagramMode;

				// Set the relevat text for diagram-table toggle button
				if (viewState.ShowOutwardJourneyDetailsDiagramMode) 
				{
					buttonShowTableOutward.Text = GetResource("RefineDetails.buttonShowTable.Text");
				} 
				else 
				{
					buttonShowTableOutward.Text = GetResource("RefineDetails.buttonShowDiagram.Text");
				}
			}
			else
			{
				outwardPanel.Visible = false;
			}

			if (returnExists)
			{
				returnPanel.Visible = true;

				// Journey Details Control is visible if diagram mode
				journeyDetailsControlReturn.Visible = viewState.ShowReturnJourneyDetailsDiagramMode;
		
				// Journey Details table control is visible if table mode
				journeyDetailsTableControlReturn.Visible = !viewState.ShowReturnJourneyDetailsDiagramMode;

				// Set the relevat text for diagram-table toggle button
				if (viewState.ShowReturnJourneyDetailsDiagramMode) 
				{
					buttonShowTableReturn.Text = GetResource("RefineDetails.buttonShowTable.Text");
				} 
				else 
				{
					buttonShowTableReturn.Text = GetResource("RefineDetails.buttonShowDiagram.Text");
				}
			}
			else
			{
				returnPanel.Visible = false;
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
			imageMainContentSkipLink1.AlternateText = GetResource("RefineDetails.imageMainContentSkipLink.AlternateText");

		}

		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Web Form Designer generated code
		/// </summary>
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
			this.PreRender += new System.EventHandler(this.OnPreRender);

		}

		/// <summary>
		/// Sets up the necessary button event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.backButton.Click += new EventHandler(this.backButton_Click);
			this.buttonShowTableOutward.Click += new EventHandler(this.buttonShowTableOutward_Click);
			this.buttonShowTableReturn.Click += new EventHandler(this.buttonShowTableReturn_Click);
			this.journeyDetailsControlOutward.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlOutward_MapButtonClicked);
			this.journeyDetailsControlReturn.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlReturn_MapButtonClicked);
			this.journeyDetailsTableControlOutward.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlOutward_MapButtonClicked);
			this.journeyDetailsTableControlReturn.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlReturn_MapButtonClicked);
		}
		#endregion
	}
}
