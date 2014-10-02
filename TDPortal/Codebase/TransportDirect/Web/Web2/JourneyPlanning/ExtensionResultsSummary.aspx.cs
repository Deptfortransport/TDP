// *********************************************** 
// NAME                 : ExtensionResultsSummary.aspx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 23/01/2006
// DESCRIPTION			: A new page as part of the new Extension pages. Displays the full itinerary 
//						  summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ExtensionResultsSummary.aspx.cs-arc  $
//
//   Rev 1.6   Oct 28 2010 14:00:24   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Mar 29 2010 16:39:22   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Jan 06 2009 13:08:32   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 30 2008 15:45:20   mmodi
//Fixed problem with summary being shown after clicking back on modify journey. Also, order of buttons and enablement of buttons sorted on modify journey screens.
//Resolution for 4911: When a journey is modified in any way, the default results tab is the summary tab
//
//   Rev 1.2   Mar 31 2008 13:24:16   mturner
//Drop3 from Dev Factory
// 
//  Rev DevFactory Mar 18 2008 15:48:00 sjohal
//  Removed Back Button
//
//  Rev DevFactory Feb 24 2008 17:13:00 apatel
//  Added back button at top left. Added image for title.
//
//  Rev DevFactory Feb 08 2008 08:49:00 apatel
//  Changed the layout of the control. Added new ExtensionOutputNavigationControl to make it look like Journey summary page.
//  Page_Initialisation method modified to intialize this control. Hide the ResultsViewSelectionControl control.
//
//   Rev 1.0   Nov 08 2007 13:29:16   mturner
//Initial revision.
//
//   Rev 1.25   Sep 14 2007 17:29:14   asinclair
//Updated for co2 phase 2
//
//   Rev 1.24   Sep 07 2007 15:58:42   asinclair
//Updated for CO2 phase 2
//
//   Rev 1.23   Apr 29 2006 11:52:06   asinclair
//Set PageState to Amend mode when Amend button is selected
//Resolution for 3984: DN068: Previously resolved extension locations are treated as ambiguous on amend
//
//   Rev 1.22   Apr 28 2006 13:26:30   COwczarek
//Initialise view state with best default selection when extend
//itinerary viewed for the first time.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.21   Apr 21 2006 14:38:28   mdambrine
//Replace the on init with a normal method call from the page_load eventhandler. This is because timeout didn't work.
//Resolution for 3972: DN068 Extend: Error page shown when 'Choose connecting journey' page times out
//
//   Rev 1.20   Apr 20 2006 17:50:12   tmollart
//Added code to clear AsyncCallState object so that its possible to research for fares.
//Resolution for 3925: DN068 Extend: no result waiting for tickets/costs for bus journeys
//
//   Rev 1.19   Apr 20 2006 16:08:42   RGriffith
//Tweak to previous fix to ensure a return result exists.
//
//   Rev 1.18   Apr 20 2006 15:50:36   RGriffith
//IR3843 - Fix to hide "Add this journey" button as well as the ResultsViewSelection control to prevent further navigation when incomplete extension results are returned.
//Resolution for 3843: DN068 Extend: Missing schematic icon and arrow when Extension added for Return only
//
//   Rev 1.17   Apr 12 2006 12:45:46   RGriffith
//IR3890 Fix - Reset fare calculation when changing selected extensions
//
//   Rev 1.16   Apr 10 2006 10:26:46   asinclair
//Added code to fix Amend issue and also to control the display of the error message
//Resolution for 3767: DN068 Extend: Inescapable loop on extend input page
//Resolution for 3827: DN068 Extend: Stuck on input page when planning 2nd extension
//Resolution for 3832: DN068 Extend: Selecting Amend on extension results options page shows Resolved extend input screen
//
//   Rev 1.15   Mar 20 2006 18:13:06   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.14   Mar 16 2006 19:01:14   pcross
//Re-added Neil's code for New Seearch which I overwrote the day after he entered it! To be fair though I did it at 5 past 4 on a Friday & I had already missed my 1st train home.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13   Mar 14 2006 19:50:10   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.12   Mar 10 2006 11:47:16   pcross
//Removed "summary od exrtensions" heading
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.11   Mar 08 2006 16:29:14   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.10   Mar 06 2006 22:25:46   rhopkins
//Set JourneyType when initialising ResultsSummaryControls, to allow for correct selection.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 03 2006 16:03:10   pcross
//Skip links / layout changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 02 2006 15:13:46   pcross
//Tidied use of error control
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Feb 24 2006 10:57:34   pcross
//Interim check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 20 2006 12:30:28   pcross
//Changed journey builder control so the host page updated accordingly
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 14 2006 16:25:42   NMoorhouse
//Linking up of pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 10 2006 10:50:14   pcross
//Minor updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Jan 31 2006 14:53:36   pcross
//Correction to navigation now page name has changed.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 31 2006 14:26:06   pcross
//Updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 27 2006 14:08:54   pcross
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
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page to present possible journeys from a journey extension request to allow user to select the
	/// desired journey extension.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ExtensionResultsSummary : TDPage
	{

		#region Control Declarations

		// HTML controls

		// Web controls

		// Button controls
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyControl;
		
		// TD custom web controls
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl;
		protected TransportDirect.UserPortal.Web.Controls.ExtendJourneyLineControl extendJourneyLineControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsViewSelectionControl resultsViewSelectionControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl outwardResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl returnResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyBuilderControl journeyBuilderControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControlReturn;

		#endregion

		#region Private Member Variables
		
		/// <summary>
		/// To hold reference to session manager
		/// </summary>
		ITDSessionManager sessionManager;

		/// <summary>
		/// To hold reference to session ItineraryManager
		/// </summary>
		ExtendItineraryManager itineraryManager;

		/// <summary>
		/// To hold reference to session view state
		/// </summary>
		TDJourneyViewState journeyViewState;
		
		/// <summary>
		/// To hold reference to current search request
		/// </summary>
		ITDJourneyRequest request;

		/// <summary>
		/// To hold reference to current search result
		/// </summary>
		ITDJourneyResult result;

		/// <summary>
		/// Summary lines for outward journey options
		/// </summary>
		FormattedJourneySummaryLines outwardSummaryLines;

		/// <summary>
		/// Summary lines for return journey options
		/// </summary>
		FormattedJourneySummaryLines returnSummaryLines;

		/// <summary>
		///  True if there is a return trip for the current journey
		/// </summary>
		private bool returnJourneySelected = false;

		private TDJourneyParametersMulti journeyParameters;

		private InputPageState pageState;

		#endregion

		#region Initialisation

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public ExtensionResultsSummary() : base()
		{
			// Set page Id
			pageId = PageId.ExtensionResultsSummary;

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
			Page_Initialisation();

           
			pageState = TDSessionManager.Current.InputPageState;

			// Handle error messages if any
			ShowErrorMessages();

			// Initialise text and button properties
			PopulateControls();

            //Added for white labelling:
            ConfigureLeftMenu("ExtensionResultsSummary.clientLink.BookmarkTitle", "ExtensionResultsSummary.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextExtensionResultsSummary);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Handles prerender event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			SetupSkipLinksAndScreenReaderText();
		}

		/// <summary>
		/// Handles page initalise event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Initialisation()
		{
			// Set up current session references
			sessionManager = TDSessionManager.Current;
			itineraryManager = (ExtendItineraryManager)TDItineraryManager.Current;
			journeyViewState = sessionManager.JourneyViewState;
			result = sessionManager.JourneyResult;
			request = sessionManager.JourneyRequest;

            // CCN 0427 Intialize the ExtensionOutputNavigationControl
            this.theOutputNavigationControl.Initialise(pageId);

			// Wires extra event handlers
			ExtraWiringEvents();

			// See if we need to handle outward AND return journeys
			returnJourneySelected = (itineraryManager.ReturnLength > 0);

			// Call private methods to populate individual web controls
			InitialiseJourneyLineControl();
			InitialiseResultsSummaryControls();
			InitialiseResultsViewSelectionControl();

			//If no jounreys have been returned, we do not want to display summary panel
			//or the 'Add extension to journey' button
			if (outwardSummaryLines == null) 
			{
				summaryPanel.Visible = false;
				journeyBuilderControl.Visible = false;
			} 
			else
			if (outwardSummaryLines.Count == 0)
			{
				journeyBuilderControl.Visible = false;
				resultsViewSelectionControl.Visible = false;
			}
			else
			if (returnJourneySelected && (returnSummaryLines.Count == 0))
			{
				journeyBuilderControl.Visible = false;
				resultsViewSelectionControl.Visible = false;
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
			imageMainContentSkipLink1.AlternateText = GetResource("ExtensionResultsSummary.imageMainContentSkipLink.AlternateText");

			imageMainContentSkipLink2.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink2.AlternateText = GetResource("ExtensionResultsSummary.imageViewSelectionSkipLink.AlternateText");

		}

		/// <summary>
		/// Moves back to previous page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void amendJourneyButton_Click(object sender, EventArgs e)
		{

			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
			pageState.AmendMode = true;

			//When we return to the input page the locations need to be set back to editable, depending
			//on which end of the journey the user was extending.
			if(TDItineraryManager.Current.ExtendEndOfItinerary)
			{
				journeyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
				journeyParameters.Destination.ClearSearch();
			}
			else
			{
				journeyParameters.OriginLocation.Status = TDLocationStatus.Valid;
				journeyParameters.Origin.ClearSearch();
			}

			//The via locations are always set back to editable
			journeyParameters.PublicViaLocation.Status = TDLocationStatus.Unspecified;
			journeyParameters.PublicVia.ClearSearch();

			journeyParameters.PrivateViaLocation.Status = TDLocationStatus.Unspecified;
			journeyParameters.PublicVia.ClearSearch();

			// Set to navigate back to the extension input page
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ExtendJourneyInput;

			// Invalidate the current journey result.
			if (result != null) 
				result.IsValid = false; 

			itineraryManager.PricingRetailOptions = null; 
			
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
		/// Journey selection changed on the outward results summary control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void outwardResultsSummaryControl_SelectionChangedEvent(object sender, CommandEventArgs e)
		{
			// Find the index of the selected radio button
			int selectedIndex = outwardResultsSummaryControl.SelectedLineIndex;

			// If the selections have changed then make sure fares are recalculated should they be viewed
			sessionManager.AsyncCallState = null;
			itineraryManager.ResetFares();

			// Update the outward selected journey in the session manager to be the outward journey selected on screen
			UpdateJourneySelection(selectedIndex, true);

		}

		/// <summary>
		/// Journey selection changed on the return results summary control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void returnResultsSummaryControl_SelectionChangedEvent(object sender, CommandEventArgs e)
		{
			// Find the index of the selected radio button
			int selectedIndex = returnResultsSummaryControl.SelectedLineIndex;

			// If the selections have changed then make sure fares are recalculated should they be viewed
			sessionManager.AsyncCallState = null;
			itineraryManager.ResetFares();

			// Update the return selected journey in the session manager to be the return journey selected on screen
			UpdateJourneySelection(selectedIndex, false);
		}

		/// <summary>
		/// Event Handler for the OK button on the results view selection control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ViewSelectionOKButton_Click(object sender, EventArgs e)
		{

			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			// Move to the selected page
			sessionManager.ResultsPageState.CurrentViewSelection = resultsViewSelectionControl.ViewSelection.SelectedIndex;

			if (Enum.IsDefined(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue))
			{
				TransitionEvent nextMove = (TransitionEvent)Enum.Parse(typeof(TransitionEvent), resultsViewSelectionControl.ViewSelection.SelectedValue, true);
				
				if(nextMove == TransitionEvent.RefineCheckC02)
				{
					// Reset the journey emissions page state, to clear it of any previous values
					TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

					TDJourneyViewState viewState = itineraryManager.JourneyViewState;
					bool isPublic = (viewState != null) && (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended);
					// Navigate to the Car emissions page or the Journey Emissions Compare Journey page dependent on journey selected
					if (isPublic)
					{
						TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState = JourneyEmissionsCompareState.JourneyCompare;
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissionsCompareJourney;
					}
					else
                        TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;	
				}
				else
				{

					sessionManager.FormShift[SessionKey.TransitionEvent] = nextMove;
				}
			}
			else
			{
				//Invalid Transition in drop down. Specific case will reload this page, however, log the error
				Logger.Write( new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, resultsViewSelectionControl.ViewSelection.SelectedValue + " is not a valid TransitionEvent")); 
			}
		}

		/// <summary>
		/// Updates the session so the selected journey matches the one selected on screen
		/// </summary>
		/// <param name="selectedIndex">The zero based index of the journey result as shown on screen</param>
		/// <param name="outward">denotes either outward or return journey result to be updated</param>
		/// <remarks>Could move to ResultsAdapter class</remarks>
		private void UpdateJourneySelection(int selectedIndex, bool outward)
		{
			// Update the journey view state with the new value of the selected outward journey
			if (outward)
			{
				journeyViewState.SelectedOutwardJourney = selectedIndex;
				journeyViewState.SelectedOutwardJourneyID = outwardSummaryLines[selectedIndex].JourneyIndex;
				journeyViewState.SelectedOutwardJourneyType = outwardSummaryLines[selectedIndex].Type;
			}
			else
			{
				journeyViewState.SelectedReturnJourney = selectedIndex;
				journeyViewState.SelectedReturnJourneyID = returnSummaryLines[selectedIndex].JourneyIndex;
				journeyViewState.SelectedReturnJourneyType = returnSummaryLines[selectedIndex].Type;
			}

		}

		#endregion

		#region Private methods

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("ExtensionResultsSummary.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            // CCN 0427 setting title image url
            imageExtensionResultSummary.ImageUrl = GetResource("RefineJourney.extendJourney.ImageURL");
            imageExtensionResultSummary.AlternateText = GetResource("ExtensionResultsSummary.imageExtensionResultSummary.AlternateText");

            // CCN 0427 Added back button
           // buttonBack.Text = GetResource("ExtendOptionsControl.Back");

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("ExtensionResultsSummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("ExtensionResultsSummary.labelIntroductoryText.Text");

			// Get correct resource strings for buttons
			amendJourneyButton.Text = GetResource("ExtensionResultsSummary.amendJourneyButton.Text");
			newJourneyButton.Text = GetResource("ExtensionResultsSummary.newJourneyButton.Text");

			// Set Help Button URL
			helpButton.HelpUrl = GetResource("ExtensionResultsSummary.helpButton.HelpUrl");

			// Summary table titles
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
			plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);

			if (!returnJourneySelected)
			{
				// Return results DO NOT exist. Set visibility of all return controls to false.
				SetReturnVisible(false);
			}

			// Initialise journey builder control
			journeyBuilderControl.ItineraryManager = itineraryManager;

		}

		/// <summary>
		/// Method to show any error messages returned from the CJP that may exist
		/// as a result of searching for journeys.
		/// </summary>
		private void ShowErrorMessages()
		{
			ITDJourneyResult journeyResult = sessionManager.JourneyResult;

			ResultsAdapter resultsAdapter = new ResultsAdapter();
			if (resultsAdapter.PopulateErrorDisplayControl(errorDisplayControl, journeyResult))
			{
				errorMessagePanel.Visible = true;
				errorDisplayControl.Visible = true;
				
			}
			else
			{
				errorMessagePanel.Visible = false;
				errorDisplayControl.Visible = false;
			}
		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		private void SetReturnVisible(bool visible)
		{
			returnSummaryPanel.Visible = visible;
			resultsTableTitleControlReturn.Visible = visible;
			returnResultsSummaryControl.Visible = visible;
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
		/// Initialise the Results Summary Controls (outward and return)
		/// </summary>
		private void InitialiseResultsSummaryControls()
		{
			// Get the formatted journey lines to display in the results summary control
			ResultsAdapter resultsAdapter = new ResultsAdapter();

			// Handle display of outward journeys
			outwardSummaryLines = resultsAdapter.FormattedOutwardJourneyLines(sessionManager,FormattedSummaryType.Extend);

			outwardResultsSummaryControl.SummaryLines = outwardSummaryLines;
			outwardResultsSummaryControl.ShowDeleteColumn = false;
			outwardResultsSummaryControl.ShowSelectColumn = true;
			outwardResultsSummaryControl.ShowEmptyDeleteColumn = false;
			outwardResultsSummaryControl.ShowEmptySelectColumn = false;

            string firstViewing = TDSessionManager.Current.GetOneUseKey(TransportDirect.UserPortal.Resource.SessionKey.FirstViewingOfResults);
            if (firstViewing == null)
                firstViewing = string.Empty;

            if (journeyViewState != null)
			{

                if (firstViewing.Length != 0) 
                {
                    int bestMatch = outwardSummaryLines.BestMatch;
                    int bestMatchIndex = outwardSummaryLines.IndexFromJourneyIndex(bestMatch);
                    journeyViewState.SelectedOutwardJourneyID = bestMatch;
                    journeyViewState.SelectedOutwardJourneyType = outwardSummaryLines[bestMatchIndex].Type;
                }

				outwardResultsSummaryControl.SelectedItemJourneyType = journeyViewState.SelectedOutwardJourneyType;
				outwardResultsSummaryControl.SelectedItemJourneyIndex = journeyViewState.SelectedOutwardJourneyID;
			}

			// Handle display of return journeys
			if (returnJourneySelected)
			{
				returnSummaryLines = resultsAdapter.FormattedReturnJourneyLines(sessionManager, FormattedSummaryType.Extend);

				returnResultsSummaryControl.SummaryLines = returnSummaryLines;
				returnResultsSummaryControl.ShowDeleteColumn = false;
				returnResultsSummaryControl.ShowSelectColumn = true;
				returnResultsSummaryControl.ShowEmptyDeleteColumn = false;
				returnResultsSummaryControl.ShowEmptySelectColumn = false;

				if (journeyViewState != null)
				{

                    if (firstViewing.Length != 0) 
                    {
                        int bestMatch = returnSummaryLines.BestMatch;
                        int bestMatchIndex = returnSummaryLines.IndexFromJourneyIndex(bestMatch);
                        journeyViewState.SelectedReturnJourneyID = bestMatch;
                        journeyViewState.SelectedReturnJourneyType = returnSummaryLines[bestMatchIndex].Type;
                    }
                    
                    returnResultsSummaryControl.SelectedItemJourneyType = journeyViewState.SelectedReturnJourneyType;
					returnResultsSummaryControl.SelectedItemJourneyIndex = journeyViewState.SelectedReturnJourneyID;
				}
			}

		}

		/// <summary>
		/// Initialise the Results View Selection Control
		/// </summary>
		private void InitialiseResultsViewSelectionControl()
		{
			resultsViewSelectionControl.ListType = DataServiceType.ExtensionResultsViews;
		}

		/// <summary>
		/// Sets up the necessary button event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{

			// Controls on this page
			this.newJourneyButton.Click += new EventHandler(newJourneyButton_Click);
			this.amendJourneyButton.Click += new EventHandler(this.amendJourneyButton_Click);

            //CCN 0427 handling back button event
            // amendJourneyButton does the same thing as back button
            //buttonBack.Click += new EventHandler(amendJourneyButton_Click);

			// Events from the summary results control(s)
			outwardResultsSummaryControl.SelectionChangedEvent += new CommandEventHandler(outwardResultsSummaryControl_SelectionChangedEvent);
			returnResultsSummaryControl.SelectionChangedEvent += new CommandEventHandler(returnResultsSummaryControl_SelectionChangedEvent);

			// Events from the ResultsViewSelectionControl
			resultsViewSelectionControl.OKButton.Click += new EventHandler(ViewSelectionOKButton_Click);

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
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}

		#endregion

	}
}