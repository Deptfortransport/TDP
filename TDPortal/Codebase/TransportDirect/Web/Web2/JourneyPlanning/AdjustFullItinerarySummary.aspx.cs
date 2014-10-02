// *********************************************** 
// NAME                 : AdjustFullItinerarySummary.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: A new page as part of the new Adjust pages. Displays the full itinerary 
//							summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/AdjustFullItinerarySummary.aspx.cs-arc  $
//
//   Rev 1.5   Oct 28 2010 14:35:50   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.4   Mar 29 2010 16:40:40   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.3   Jan 14 2009 13:35:50   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:54   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.15   Apr 28 2006 13:16:22   COwczarek
//Pass summary type in call to FormattedSelectedJourneys method.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.14   Mar 28 2006 08:30:58   pcross
//Removed summary title
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.13   Mar 17 2006 15:32:32   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.12   Mar 14 2006 19:49:58   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.11   Mar 14 2006 13:19:30   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10   Mar 08 2006 16:29:16   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.9   Mar 03 2006 11:25:36   NMoorhouse
//Added (common) NewSearch onto the helper class
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 03 2006 11:12:56   pcross
//Added skip links
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 02 2006 15:11:00   pcross
//Handling screen flow and what re-adjust options to show.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Feb 24 2006 10:53:14   pcross
//Addition of results table title control, layout corrections
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 20 2006 12:28:22   pcross
//Added screen navigation code
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 16 2006 17:23:20   pcross
//Bug correction
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 16 2006 17:11:10   pcross
//Added navigation (NM)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 07 2006 18:34:16   NMoorhouse
//Change resource manager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 30 2006 17:38:48   RGriffith
//Changes for renaming of Extended Summary pages
//
//   Rev 1.0   Jan 30 2006 13:02:20   RGriffith
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for AdjustFullItinerarySummary.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class AdjustFullItinerarySummary : TDPage
	{
		// HTML Controls

		// Button controls
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyControl;
		
		// TD Custom Web Controls
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl combinedResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsViewSelectionControl resultsViewSelectionControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public AdjustFullItinerarySummary() : base()
		{
			// Set page Id
			pageId = PageId.AdjustFullItinerarySummary;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Private member variables
		
		ITDSessionManager sessionManager;

		InputPageState inputPageState;

		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			sessionManager = TDSessionManager.Current;
			inputPageState = sessionManager.InputPageState;

			// Clear stack ready for use
			inputPageState.JourneyInputReturnStack.Clear();

			// Call private methods to populate individual web controls
			InitialiseResultsSummaryControl();
			InitialiseResultsViewSelectionControl();

			// Initialise text and button properties
			PopulateControls();

            //Added for white labelling:
            ConfigureLeftMenu("ClientLinks.DoorToDoor.LinkText", "", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextAdjustFullItinerarySummary);
            expandableMenuControl.AddExpandedCategory("Related links");

            //help button
            helpButton.HelpUrl = GetResource("AdjustFullItinerarySummary.helpButton.HelpUrl");
		}

		/// <summary>
		/// PreRender adjustments
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			// Tailor the contents of the dropdown to only include the adjust for eligible directions
			HandleAdjustDirectionAvailability();

			SetupSkipLinksAndScreenReaderText();

		}

		/// <summary>
		/// Default selection of options in the dropdown includes adjust outward and adjust return.
		/// Check if they are eligible for adjust and if not, remove the options from the dropdown
		/// </summary>
		private void HandleAdjustDirectionAvailability()
		{

			RefineHelper refineHelper = new RefineHelper();

			if (!refineHelper.IsAdjustAvailable(true))
				// Remove both outward and return adjust options from dropdown
				RemoveListItem(resultsViewSelectionControl.ViewSelection, "JourneyAdjustOutward");

			if (!refineHelper.IsAdjustAvailable(false))
				// Remove both outward and return adjust options from dropdown
				RemoveListItem(resultsViewSelectionControl.ViewSelection, "JourneyAdjustReturn");

		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("AdjustFullItinerarySummary.imageMainContentSkipLink.AlternateText");

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
		/// Return to original journey input page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void amendJourneyButton_Click(object sender, EventArgs e)
		{
			// Get the original input page and set transition event to return there
			SetInputPageToReturnTo();

			// Call method to invalidate and reset the results on the session.
			InvalidateAndResetResults();
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
		/// Gets the original input page and sets transition event to return there
		/// </summary>
		private void SetInputPageToReturnTo()
		{
			// redirect user to the appropriate input page.
			if (sessionManager.FindAMode == FindAMode.None)
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
			else
			{
				sessionManager.FindPageState.PrepareForAmendJourney();
				sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(sessionManager.FindAMode);
			}
		}

		/// <summary>
		/// Invalidates current journey result on the session and also clears other results
		/// dependent on cost or time based mode.
		/// </summary>
		private void InvalidateAndResetResults()
		{
			// Invalidate the current journey result. Set the mode for which the results pertain to as being
			// none so that clicking the Find A tab will then redirect to the default find A input page.
			if (sessionManager.JourneyResult != null) 
			{ 
				sessionManager.JourneyResult.IsValid = false; 
			} 

			//Reset results dependent on mode.
			if (FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode)) 
			{ 
				//Reset search result.
				((FindCostBasedPageState)sessionManager.FindPageState).SearchResult = null; 
			} 
			else 
			{ 
				TDItineraryManager.Current.PricingRetailOptions = null; 
			}

			sessionManager.CurrentAdjustState = null;
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("AdjustFullItinerarySummary.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("AdjustFullItinerarySummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("AdjustFullItinerarySummary.labelIntroductoryText.Text");

			// Get correct resource strings for buttons
			amendJourneyButton.Text = GetResource("AdjustFullItinerarySummary.amendJourneyButton.Text");
			newJourneyButton.Text = GetResource("AdjustFullItinerarySummary.newJourneyButton.Text");

			// Set Help Button URL
			helpButton.HelpUrl = GetResource("AdjustFullItinerarySummary.helpButton.HelpUrl");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
			string labelTextOverride = GetResource("RefineJourney.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, false);

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
			combinedResultsSummaryControl.SummaryLines = journeyResults.FormattedSelectedJourneys(sessionManager,FormattedSummaryType.Adjust);
		}

		/// <summary>
		/// Initialise the Results View Selection Control
		/// </summary>
		private void InitialiseResultsViewSelectionControl()
		{
			resultsViewSelectionControl.ListType = DataServiceType.FullItineraryAdjust;

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
			this.amendJourneyButton.Click += new EventHandler(this.amendJourneyButton_Click);
			this.newJourneyButton.Click += new EventHandler(this.newJourneyButton_Click);
		}

		/// <summary>
		/// Handles the ok button event on the resultsViewSelectionControl.
		/// Transitions to the relevant view selected from the resultsViewSelectionControl drop down.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeResultViewButton_Click(object sender, EventArgs e)
		{
			sessionManager.ResultsPageState.CurrentViewSelection = resultsViewSelectionControl.ViewSelection.SelectedIndex;

			if (Enum.IsDefined(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue))
			{
				TransitionEvent nextMove = (TransitionEvent)Enum.Parse(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue, true);
				
				// If the request is to re-adjust results then add this page to the page stack so back button from
				// adjust input page will return here instead of refine journey plan page
				if (nextMove == TransitionEvent.JourneyAdjustOutward || nextMove == TransitionEvent.JourneyAdjustReturn)
					inputPageState.JourneyInputReturnStack.Push(PageId.AdjustFullItinerarySummary);

				sessionManager.FormShift[SessionKey.TransitionEvent] = nextMove;
			}
		}


		#endregion

	}
}
