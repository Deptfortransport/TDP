// *********************************************** 
// NAME                 : ExtendedFullItinerarySummary.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: A new page as part of the new Extension pages. Displays the full itinerary 
//							summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ExtendedFullItinerarySummary.aspx.cs-arc  $
//
//   Rev 1.6   Oct 28 2010 14:00:46   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Mar 29 2010 16:41:52   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Jan 09 2009 09:44:16   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:44:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Jul 30 2008 11:18:10   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:24:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:12   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.17   Sep 14 2007 17:29:44   asinclair
//Updated for Co2 phase 2
//
//   Rev 1.16   Sep 07 2007 15:56:44   asinclair
//Updated for CO2 phase 2
//
//   Rev 1.15   Apr 28 2006 13:17:06   COwczarek
//Pass summary type in call to FormattedFullItinerarySummary method
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.14   Apr 04 2006 16:24:36   rgreenwood
//IR3761: Removed _Init eventhandler and now triggered those method calls from Page_Load.
//Resolution for 3761: DN068 Extend: Server error instead of usual timeout page
//
//   Rev 1.13   Mar 30 2006 18:17:14   pcross
//Handling when extend available or not depending on current time relative to journey time
//Resolution for 3704: DN068 Extend: Allowed to add a 2nd extension to start despite 1st extension being planned in the past
//
//   Rev 1.12   Mar 28 2006 08:39:28   pcross
//Added Results Table Title control
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.11   Mar 24 2006 14:01:50   tmollart
//Modified InitialiseResultsViewSelectionControl so it doesnt create a new extend itinerary manager.
//Resolution for 3695: DN068: Extend: Maximum number of extensions should be 2
//
//   Rev 1.10   Mar 17 2006 15:32:34   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.9   Mar 14 2006 11:39:56   RGriffith
//Addition of HeaderControl and HeadElementControl as well as inclusion of new Resource namespace
//
//   Rev 1.8   Mar 10 2006 16:39:04   RGriffith
//Removal of Summary Title
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 08 2006 16:29:14   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.6   Mar 03 2006 16:01:46   pcross
//Skip links / layout changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 03 2006 11:25:36   NMoorhouse
//Added (common) NewSearch onto the helper class
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 02 2006 15:10:58   tolomolaiye
//Correct error that occurs when user clicks the "New Search" button
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 28 2006 12:53:00   tolomolaiye
//Removed "Back" button
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 24 2006 10:54:34   pcross
//Removed reference to deprecated option column
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 07 2006 18:34:18   NMoorhouse
//Change resource manager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 30 2006 17:33:16   RGriffith
//Initial revision.
//
//   Rev 1.0   Jan 26 2006 12:17:56   RGriffith
//Initial revision.

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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for ExtendedFullItinerarySummary.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ExtendedFullItinerarySummary : TDPage
	{
		// HTML Controls

		// Button controls
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyControl;
		
		// TD Custom Web Controls
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.ExtendJourneyLineControl extendJourneyLineControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl combinedResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsViewSelectionControl resultsViewSelectionControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public ExtendedFullItinerarySummary() : base()
		{
			// Set page Id
			pageId = PageId.ExtendedFullItinerarySummary;

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
			ExtendedFullItinerarySummary_Initialisation(sender, e);

            theOutputNavigationControl.Initialise(pageId);

			// Initialise text and button properties
			PopulateControls();

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextExtendedFullItinerarySummary);
            expandableMenuControl.AddExpandedCategory("Related links");

            //help button
            helpButton.HelpUrl = GetResource("ExtendedFullItinerarySummary.helpButton.HelpUrl");
        }

		/// <summary>
		/// Handles prerender event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			// Tailor the contents of the dropdown to only include the extend for eligible directions
			HandleExtendDirectionAvailability();

			SetupSkipLinksAndScreenReaderText();
		}

		/// <summary>
		/// Default selection of options in the dropdown includes extend start and extend end.
		/// Check if they are eligible for extend and if not, remove the options from the dropdown
		/// </summary>
		private void HandleExtendDirectionAvailability()
		{

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// Ensure correct options are displayed

			TDDateTime timeNow = TDDateTime.Now;

			if (sessionManager.ItineraryMode == ItineraryManagerMode.None)
			{
				// Get journey depart / arrival times from session

				// Get the user-selected journey from the session
				ResultsAdapter resultsAdapter = new ResultsAdapter();
				Journey outwardJourney = resultsAdapter.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, false);

				// Get the departure time for the journey
				TDDateTime departDateTime = outwardJourney.JourneyLegs[0].StartTime;

				// Get the arrival time for the journey
				TDDateTime arrivalDateTime = outwardJourney.JourneyLegs[outwardJourney.JourneyLegs.Length - 1].EndTime;

				if (departDateTime < timeNow) 
				{
					// Departure in past
					RemoveListItem(resultsViewSelectionControl.ViewSelection, "ExtendJourneyInputStart");
				} 

				if (arrivalDateTime < timeNow)
				{
					// Arrival in past - no options available
					RemoveListItem(resultsViewSelectionControl.ViewSelection, "ExtendJourneyInputEnd");
				} 

			}
			else
			{
				if (itineraryManager.OutwardDepartDateTime() < timeNow || (itineraryManager.ReturnLength > 0 && itineraryManager.ReturnArriveDateTime() < timeNow)) 
				{
					// Departure in past
					RemoveListItem(resultsViewSelectionControl.ViewSelection, "ExtendJourneyInputStart");
				} 

				if ( (itineraryManager.OutwardArriveDateTime() < timeNow)
					|| ( (itineraryManager.ReturnLength > 0)
					&& ((itineraryManager.ReturnDepartDateTime() < timeNow) || (itineraryManager.OutwardArriveDateTime() >= itineraryManager.ReturnDepartDateTime()) )
					)
					)
				{
					// Arrival in past - no options available
					RemoveListItem(resultsViewSelectionControl.ViewSelection, "ExtendJourneyInputEnd");
				} 
			}

		}

		/// <summary>
		/// Removes an item from a dropdownlist control where the item is specified by it's value property
		/// </summary>
		/// <param name="ddl">DropdownList to remove item from</param>
		/// <param name="val">String representing the value of the item to be removed</param>
		private void RemoveListItem(DropDownList ddl, string val)
		{
			foreach (ListItem item in ddl.Items)
			{
				if (item.Value == val)
				{
					ddl.Items.Remove(item);
					break;
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
			imageMainContentSkipLink1.AlternateText = GetResource("ExtendedFullItinerarySummary.imageMainContentSkipLink.AlternateText");

			imageMainContentSkipLink2.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink2.AlternateText = GetResource("ExtendedFullItinerarySummary.imageViewSelectionSkipLink.AlternateText");

		}

		/// <summary>
		/// Handles page initalise event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExtendedFullItinerarySummary_Initialisation(object sender, EventArgs e)
		{
			// Call private methods to populate individual web controls
			InitialiseJourneyLineControl();
			InitialiseResultsSummaryControl();
			InitialiseResultsViewSelectionControl();
		}

		/// <summary>
		/// Handles new search button click event.
		/// This code is designed to return to the input page the results were derived from and reset 
		/// it to a blank input values ready for a new search.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newJourneyButton_Click(object sender, EventArgs e)
		{
			RefineHelper refineHelper = new RefineHelper();
			refineHelper.NewJourney();
		}

		/// <summary>
		/// Handles the ok button event on the resultsViewSelectionControl.
		/// Transitions to the relevant view selected from the resultsViewSelectionControl drop down.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeResultViewButton_Click(object sender, EventArgs e)
		{

			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			TDSessionManager.Current.ResultsPageState.CurrentViewSelection = resultsViewSelectionControl.ViewSelection.SelectedIndex;

			if (Enum.IsDefined(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue))
			{
				TransitionEvent nextMove = (TransitionEvent)Enum.Parse(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue, true);

		
				if(nextMove == TransitionEvent.RefineCheckC02)
				{
					// Reset the journey emissions page state, to clear it of any previous values
					TDSessionManager.Current.JourneyEmissionsPageState.Initialise();
					TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState = JourneyEmissionsCompareState.JourneyCompare;
				}


				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = nextMove;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("ExtendedFullItinerarySummary.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("ExtendedFullItinerarySummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("ExtendedFullItinerarySummary.labelIntroductoryText.Text");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
			string labelTextOverride = GetResource("RefineJourney.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, false);

			// Get correct resource strings for buttons
			newJourneyButton.Text = GetResource("ExtendedFullItinerarySummary.newJourneyButton.Text");

			// Set Help Button URL
			helpButton.HelpUrl = GetResource("ExtendedFullItinerarySummary.helpButton.HelpUrl");

            addAnotherOnwardJourney.Text = GetResource("ExtendedFullItinerarySummary.AddOnwardJourney.Text");
		}


		/// <summary>
		/// Initialise the Extend Journey Line Control
		/// </summary>
		private void InitialiseJourneyLineControl()
		{
			// Use the ExtendJourneyAdapter to populate the journey line control
			ExtendJourneyAdapter journeyExtension = new ExtendJourneyAdapter();

			journeyExtension.PopulateExtendJourneyLineControl(extendJourneyLineControl);
		}

		/// <summary>
		/// Initialise the Results Summary Control
		/// </summary>
		private void InitialiseResultsSummaryControl()
		{
			// Set display properties
			combinedResultsSummaryControl.ShowDeleteColumn = false;
			combinedResultsSummaryControl.ShowSelectColumn = false;
			combinedResultsSummaryControl.ShowEmptyDeleteColumn = false;
			combinedResultsSummaryControl.ShowEmptySelectColumn = false;

			// Use new results adpater to populate the control's summary lines
			ResultsAdapter journeyResults = new ResultsAdapter();
			combinedResultsSummaryControl.SummaryLines = journeyResults.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Extend);
		}

		/// <summary>
		/// Initialise the Results View Selection Control
		/// </summary>
		private void InitialiseResultsViewSelectionControl()
		{
			// Initialise the ExtendItineraryManager
			ExtendItineraryManager extendItineraryManager = (ExtendItineraryManager)TDSessionManager.Current.ItineraryManager;

			// If Extend Journey is permitted, set list type to include the extend 
			// option, else set list type to not include the extend option.
			if (extendItineraryManager.ExtendPermitted)
			{
                resultsViewSelectionControl.ListType = DataServiceType.FullItineraryExtendPermitted;
			}
			else
			{
                resultsViewSelectionControl.ListType = DataServiceType.FullItineraryExtendNotPermitted;
			}
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

		}

		/// <summary>
		/// Sets up the necessary button event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
            this.resultsViewSelectionControl.OKButton.Click += new EventHandler(this.changeResultViewButton_Click);
			this.newJourneyButton.Click += new EventHandler(this.newJourneyButton_Click);
            this.addAnotherOnwardJourney.Click += new EventHandler(addAnotherOnwardJourney_Click);
		}

        private void addAnotherOnwardJourney_Click(object sender, EventArgs e)
        {
            TransitionEvent nextMove = TransitionEvent.ExtendJourneyInputEnd;


            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = nextMove;
        }
		#endregion
	}
}
