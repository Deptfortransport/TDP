// *********************************************** 
// NAME                 : ReplanFullItinerarySummary.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: A new page as part of the new Extension pages. Displays the full itinerary 
//							summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ReplanFullItinerarySummary.aspx.cs-arc  $
//
//   Rev 1.8   Oct 29 2010 11:17:24   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.7   Jul 27 2010 12:14:26   mmodi
//Ensure left hand menu shown when journey planning fails
//Resolution for 4809: Door 2 Door Journey bugs
//
//   Rev 1.6   Mar 29 2010 16:40:38   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.5   Jan 06 2009 14:22:38   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   May 21 2008 11:15:52   mmodi
//Setup navigation control with page id
//Resolution for 4995: Server error thrown when going back from CO2 emissions screen
//
//   Rev 1.3   Apr 11 2008 13:44:50   apatel
//corrected left hand menu
//Resolution for 4779: IE7 - Full Itinerary screen
//
//   Rev 1.2   Mar 31 2008 13:25:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:18   mturner
//Initial revision.
//
//   Rev 1.20   May 03 2006 11:39:52   rwilby
//Added logic to evaluate new ReplanItineraryManager.HasOverlappingJourneys property and display a warning message if true.
//Resolution for 4056: DN068: Replan - No warning message is shown when a time overlap occurs
//
//   Rev 1.19   Apr 28 2006 13:24:58   COwczarek
//Pass summary type in call to FormattedFullItinerarySummary method.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.18   Mar 31 2006 12:51:44   asinclair
//FxCop fixes
//
//   Rev 1.17   Mar 28 2006 09:07:46   pcross
//Added Results Table Title control
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.16   Mar 23 2006 13:54:28   NMoorhouse
//Use new Replan Itinerary properties to indicate whether outward and/or return journeys have already been replanned
//Resolution for 3663: Extend, Replan & Adjust: Replan a return journey and result allows to replan return again
//
//   Rev 1.15   Mar 22 2006 19:08:38   NMoorhouse
//Fix problem with this page away inserting a replan (even if one wasn't requested)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.14   Mar 17 2006 15:32:32   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.13   Mar 14 2006 11:39:56   RGriffith
//Addition of HeaderControl and HeadElementControl as well as inclusion of new Resource namespace
//
//   Rev 1.12   Mar 10 2006 15:14:50   RGriffith
//FxCopSuggested Changes
//
//   Rev 1.11   Mar 09 2006 16:19:28   tmollart
//Work in progress. Fixed error which meant when errors occured it was not possible to do anything on the page.
//
//   Rev 1.10   Mar 09 2006 11:41:46   asinclair
//Added the Error Display Control and methods to display CJP error message
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 03 2006 16:13:24   pcross
//Skip links
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 03 2006 11:25:36   NMoorhouse
//Added (common) NewSearch onto the helper class
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Feb 24 2006 11:11:10   pcross
//Removed reference to deprecated option column
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Feb 20 2006 19:55:14   asinclair
//Fixed bug - should be using AmededJourneyResult
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 17 2006 14:19:08   NMoorhouse
//Set up the transitions
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 15 2006 16:30:48   tmollart
//Work in progress. Added code to add results to itinerary.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 07 2006 19:46:46   tmollart
//Work in progress. Modified events and added in code to handling creating the replan itinerary.
//
//   Rev 1.2   Feb 07 2006 18:34:22   NMoorhouse
//Change resource manager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 30 2006 17:38:46   RGriffith
//Changes for renaming of Extended Summary pages
//
//   Rev 1.0   Jan 30 2006 13:02:24   RGriffith
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
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for ReplanFullItinerarySummary.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ReplanFullItinerarySummary : TDPage
    {
        #region Controls
        // HTML Controls

		// Button controls
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyControl;
		
		// TD Custom Web Controls
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl combinedResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsViewSelectionControl resultsViewSelectionControl;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
        //protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
        //protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
        protected TransportDirect.UserPortal.Web.Controls.ExtensionOutputNavigationControl theOutputNavigationControl;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor for the Page
		/// </summary>
		public ReplanFullItinerarySummary() : base()
		{
			// Set page Id
			pageId = PageId.ReplanFullItinerarySummary;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
        }

        #endregion

        #region Event Handlers
        /// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ITDSessionManager sm = TDSessionManager.Current;

            // Check for journey planning errors
			if (sm.AmendedJourneyResult.CJPMessages.GetLength(0) == 0)
			{	
				ReplanItineraryManager im = (ReplanItineraryManager)sm.ItineraryManager;
				ReplanPageState pageState = (ReplanPageState)sm.InputPageState;

				AsyncCallState acs = sm.AsyncCallState;

				if ((acs != null) && (sm.AmendedJourneyResult != null) && sm.AmendedJourneyResult.IsValid)
				{
					im.InsertReplan(sm.AmendedJourneyResult, pageState.ReplanStartJourneyDetailIndex, pageState.ReplanEndJourneyDetailIndex, (pageState.CurrentAmendmentType == TDAmendmentType.OutwardJourney));
					sm.AsyncCallState = null;
				}

				// Call private methods to populate individual web controls
				PopulateResultsSummaryControl();
				PopulateResultsViewSelectionControl();

                // Initailise the control so it knows the current page
                theOutputNavigationControl.Initialise(this.PageId);

                errorMessagePanel.Visible = false;
				errorDisplayControl.Visible = false ;

				//IR4056: Check for overlapping journeys
				if (im.HasOverlappingJourneys)
				{
					//Show warning for overlapping journeys
					errorDisplayControl.Type = ErrorsDisplayType.Warning;

					errorDisplayControl.ErrorStrings = 
						new string[1] {GetResource("ReplanFullItinerarySummary.OverlappingJourneysWarning")};

					errorMessagePanel.Visible = true;
					errorDisplayControl.Visible = true;
				}
			}
			else
			{
                // Errors occurred during planning, indicates no journeys
				ArrayList errorsList = new ArrayList();
				foreach (CJPMessage message in sm.AmendedJourneyResult.CJPMessages)
				{
					if(message.Type == ErrorsType.Warning)
					{
						errorDisplayControl.Type = ErrorsDisplayType.Warning;
					}

					string text = message.MessageText;

					if( text == null || text.Length == 0 )
					{
						string errResource = message.MessageResourceId;

						text = GetResource("langStrings", errResource);

						errorsList.Add( text );
					}

					errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));
					
					if (errorDisplayControl.ErrorStrings.Length > 0)
					{
						errorMessagePanel.Visible = true;
						errorDisplayControl.Visible = true;
						summaryPanel.Visible = false;
					}
					else
					{
						errorMessagePanel.Visible = false;
						errorDisplayControl.Visible = false ;
					}
				}				
				// Clear the error messages.
				// sm.AmendedJourneyResult.ClearMessages();

                // Hide controls that shouldn't be displayed following journey planning error
                labelIntroductoryText.Visible = false;
                theOutputNavigationControl.Visible = false;
			} 

            PopulateControls();

            // Left hand navigation menu set up
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextReplanFullItinerarySummary);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// PreRender event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			SetupSkipLinksAndScreenReaderText();
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("ReplanFullItinerarySummary.imageMainContentSkipLink.AlternateText");

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
			TDSessionManager.Current.ResultsPageState.CurrentViewSelection = resultsViewSelectionControl.ViewSelection.SelectedIndex;

			if (Enum.IsDefined(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue))
			{
				TransitionEvent nextMove = (TransitionEvent)Enum.Parse(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue, true);
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
            this.PageTitle = GetResource("ReplanFullItinerarySummary.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Get correct resource strings for labels
			label1.Text = GetResource("ReplanFullItinerarySummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("ReplanFullItinerarySummary.labelIntroductoryText.Text");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
			string labelTextOverride = GetResource("RefineJourney.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, false);

			// Get correct resource strings for buttons
			newJourneyButton.Text = GetResource("ExtendedFullItinerarySummary.newJourneyButton.Text");

			// Set Help Button URL
			helpButton.HelpUrl = GetResource("ReplanFullItinerarySummary.helpButton.HelpUrl");
		}

		/// <summary>
		/// Initialise the Results Summary Control
		/// </summary>
		private void PopulateResultsSummaryControl()
		{
			// Set display properties
			combinedResultsSummaryControl.ShowDeleteColumn = false;
			combinedResultsSummaryControl.ShowSelectColumn = false;
			combinedResultsSummaryControl.ShowEmptyDeleteColumn = false;
			combinedResultsSummaryControl.ShowEmptySelectColumn = false;

			// Use new results adpater to populate the control's summary lines
			ResultsAdapter journeyResults = new ResultsAdapter();
			combinedResultsSummaryControl.SummaryLines = journeyResults.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Replan);
		}

		/// <summary>
		/// Initialise the Results View Selection Control
		/// </summary>
		private void PopulateResultsViewSelectionControl()
		{
			// Initialise the ExtendItineraryManager
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			ReplanItineraryManager replanManager =  itineraryManager as ReplanItineraryManager; 

			if (itineraryManager.ReturnExists && !(replanManager.ReturnReplanned))
			{
				// If all return journeys available for replan then populate drop down
				// list with a replan return option
				resultsViewSelectionControl.ListType = DataServiceType.FullItineraryReplanReturn;
			}
			else if (!(replanManager.OutwardReplanned))
			{
				// If all return journeys available for replan then populate drop down
				// list with a replan return option
				resultsViewSelectionControl.ListType = DataServiceType.FullItineraryReplanOutward;
			}
			else
			{
				// If all no outward or return journeys available to replan then populate drop down
				// list without replan options
				resultsViewSelectionControl.ListType = DataServiceType.FullItineraryReplanNotPermitted;
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
		}
		#endregion
	}
}
