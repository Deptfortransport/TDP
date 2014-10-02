// *********************************************** 
// NAME                 : JourneyAdjust.aspx.cs
// AUTHOR               : Paul Cross
// DATE CREATED         : 03/02/2006
// DESCRIPTION			: Journey Adjust Page
// NOTE					: This page completely rewritten for re-plan rewrite. Therefore all
//						  previous comments removed.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyAdjust.aspx.cs-arc  $
//
//   Rev 1.6   Oct 29 2010 09:16:32   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Mar 29 2010 16:39:26   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Jan 13 2009 11:40:48   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 03 2008 13:10:22   dgath
//Corrected typo in code to fetch text for New Search button
//
//   Rev 1.2   Mar 31 2008 13:24:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:54   mturner
//Initial revision.
//
//Rev DevFatory Feb 13th 14:34:00 dgath
//Added cancelButton (originally part of AdjustOptionsControl but now part of this page) 
//to control declarations region, assigned text to button in SetDisplayOptionsForControls(), 
//and set action for button in ExtraWiringEvents().
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.38   Apr 05 2006 15:25:12   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.37   Mar 31 2006 16:13:04   RGriffith
//Addition of Default Click
//
//   Rev 1.36   Mar 22 2006 15:32:40   NMoorhouse
//Fix error with Confirmation mode not being reset
//Resolution for 3668: Extend, Replan & Adjust: Adjust Input Non-java confirmation problem
//
//   Rev 1.35   Mar 20 2006 18:15:56   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.34   Mar 17 2006 12:37:44   pcross
//ArriveBefore indicator no longer needed on JourneyAdjustSegmentControl
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.33   Mar 17 2006 11:38:34   pcross
//Bug corrections
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.32   Mar 14 2006 19:50:10   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.31   Mar 13 2006 16:07:04   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.9   Mar 10 2006 14:40:00   pcross
//Updates for non-JS confirmation screen to work
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.8   Mar 08 2006 21:00:52   pcross
//FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.7   Mar 03 2006 16:04:08   pcross
//Skip links / layuout changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.6   Mar 02 2006 15:16:28   pcross
//Mainly screen flow
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.5   Feb 20 2006 12:31:40   pcross
//Picking up whether outward / return journey is being displayed from a session variable
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.4   Feb 16 2006 17:12:48   pcross
//Navigation correction
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.3   Feb 16 2006 10:29:06   pcross
//Interim check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.2   Feb 14 2006 11:39:00   RGriffith
//Changes to add Confirmation mode to page for non javascript users
//
//   Rev 1.28.3.1   Feb 10 2006 10:51:12   pcross
//Interim check-in of re-write of this page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.3.0   Feb 06 2006 12:08:08   pcross
//Interim check in. Updated Adjust functionality
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page to present options on adjusting the timings of a journey.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyAdjust : TDPage
	{

		#region Control Declarations

		// Web controls

		// Button controls
        
        /// <summary>
        /// Read only property. Exposes the Cancel button to parent pages.
        /// </summary>
        public TDButton CancelButton
        {
            get { return cancelButton; }
        }
		
		// TD custom web controls
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.AdjustOptionsControl adjustOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.RefinePageOptionsControl pageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyAdjustSegmentControl journeyAdjustSegmentControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyAdjustTableGridControl journeyAdjustTableGridControl;

		#endregion

		#region Private member variables

		/// <summary>
		/// Indicates whether we want to show adjust options for the outward or the return journey
		/// </summary>
		bool isOutwardJourney = true;

		/// <summary>
		/// The outward or return journey the user has selected to make adjustments to
		/// </summary>
		PublicJourney journeyToAdjust;

		/// <summary>
		/// Current session object
		/// </summary>
		ITDSessionManager sessionManager;

		/// <summary>
		/// ViewState object to access user selections relating to journeys
		/// </summary>
		TDJourneyViewState viewState;

		/// <summary>
		/// AdjustState object to access user selections relating to adjust process
		/// </summary>
		TDCurrentAdjustState adjustState;
		
		/// <summary>
		/// Page state to handle screen flow
		/// </summary>
		InputPageState adjustPageState;


		/// <summary>
		/// True if Javascript is turned off and the user is asked to confirm the selection when attempting to get to the next page
		/// </summary>
		private bool confirmationMode = false;

		/// <summary>
		/// True if initial user search was to arrive before a time. False if leave after search.
		/// </summary>
		private bool arriveBefore = true;

		/// <summary>
		/// Dependent on user selection from the timings dropdown.
		/// Indicates the user search to be performed in adjust. Set in SetSelectedAdjustOptions().
		/// </summary>
		private bool selectedArriveBefore = true;

		/// <summary>
		/// Dependent on user selection from the timings dropdown.
		/// Indicates the timing adjustment to be performed. Set in SetSelectedAdjustOptions().
		/// If 0 then this actually means use the default adjustment in the AdjustRoute class (from TransportDirect.JourneyControl.AdjustJourney.TimeAdjustment property).
		/// If > 1 then this is a specifically selected adjustment time set by user.
		/// </summary>
		private int selectedTimingAdjustment = 0;

		/// <summary>
		/// Dependent on user selection from the timings dropdown.
		/// Indicates the SelectedValue property of the adjust timings dropdown. Set in SetSelectedAdjustOptions().
		/// </summary>
		private string selectedAdjustTimingsDropdownValue = String.Empty;

		/// <summary>
		/// Dependent on user selection from the locations dropdown.
		/// Indicates the SelectedValue property of the adjust locations dropdown. Set in SetSelectedAdjustOptions().
		/// </summary>
		private string selectedAdjustLocationsDropdownValue = String.Empty;

		/// <summary>
		/// Selected leg index from the user selection in the locations dropdown
		/// Set in SetSelectedAdjustOptions().
		/// </summary>
		private uint selectedLegIndex = 0;

		/// <summary>
		/// Suffix used in locations dropdown when the location is an interchange
		/// </summary>
		private const string InterchangeIndicator = "_interchange";

		/// <summary>
		/// Matches the timings dropdown value for generic Arrive Earlier search
		/// </summary>
		private const string ArriveEarlier = "ArriveEarlier";

		/// <summary>
		/// Matches the timings dropdown value for generic Leave Later search
		/// </summary>
		private const string LeaveLater = "LeaveLater";
		
		/// <summary>
		/// Value in the timings / locations dropdown if no selection made
		/// </summary>
		private const string UNREFINED_TEXT = "unrefined";


		#endregion

		#region Initialisation

		/// <summary>
		/// Constructor - sets the page Id.
		/// </summary>
		public JourneyAdjust() : base()
		{
			// Set page ID
			pageId = PageId.JourneyAdjust;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handles page load event
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set up session variables
			sessionManager = TDSessionManager.Current;
			viewState = sessionManager.JourneyViewState;
			adjustState = sessionManager.CurrentAdjustState;
			adjustPageState = sessionManager.InputPageState;

			isOutwardJourney = (adjustState.CurrentAmendmentType == TDAmendmentType.OutwardJourney ? true : false);
			confirmationMode = sessionManager.JourneyViewState.ConfirmationMode;

			arriveBefore = GetArriveBeforeIndicator();

			// Initialise text and button properties
			PopulateStaticControls();

            //Added for white labelling:
            ConfigureLeftMenu("JourneyAdjust.clientLink.BookmarkTitle", "JourneyAdjust.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyAdjust);
            expandableMenuControl.AddExpandedCategory("Related links");

            //Help button
            Helpbuttoncontrol1.HelpUrl = GetResource("JourneyAdjust.helpButton.HelpUrl");

		}

		/// <summary>
		/// Handle the state dependent page settings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			// Set up text and button properties
			PopulateStateDependentControls();

			SetDisplayOptionsForControls();

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
			imageMainContentSkipLink1.AlternateText = GetResource("JourneyAdjust.imageMainContentSkipLink.AlternateText");

		}

		/// <summary>
		/// Return to the refine journey main page
		/// </summary>
		private void commandBack_Click(object sender, System.EventArgs e)
		{
			if (confirmationMode)
			{
				confirmationMode = false;
				TDSessionManager.Current.JourneyViewState.ConfirmationMode = false;
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyAdjust;
			}
			else
			{
				// Save the user selections in case we return to this screen
				SaveUserSelections();

				// If following a re-adjust request from a results page, want to return to those results
				// - examine the page stack and return to the page in the stack
				Stack returnStack = adjustPageState.JourneyInputReturnStack;
				PageId returnPageId;
				if(returnStack.Count == 0)
					// Return to the initial refine journey input page
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineJourneyPlan;
				else
				{
					// If the page in the stack is a results page then return there
					returnPageId = (PageId)returnStack.Pop();
					if (returnPageId == PageId.AdjustFullItinerarySummary)
						sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.AdjustFullItinerarySummary;

					// Clear page stack (should be already, but just to make sure!)
					returnStack.Clear();
				}

			}
		}

		/// <summary>
		/// Save selections and adjust the journey
		/// </summary>
		private void commandSubmit_Click(object sender, System.EventArgs e)
		{
			// Do nothing if the user has not selected a dropdown value for both timing and location
			if (adjustOptionsControl.AdjustLocations.SelectedValue == UNREFINED_TEXT ||
				adjustOptionsControl.AdjustTimings.SelectedValue == UNREFINED_TEXT)
			{
				// Do nothing
			}
			else
			{

				if (!confirmationMode)
				{
					// If Javascript is not enabled - go to the confirmation page
					if (!base.IsJavascriptEnabled)
					{
						// Save the user selections so they carry through to the confirmation screen
						SaveUserSelections();

						confirmationMode = true;
						TDSessionManager.Current.JourneyViewState.ConfirmationMode = true;
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyAdjust;
					}
					else
					{
						// Call PerformAdjustmentRoutine which will save the user selections to session and
						// perform all the steps required for making an adjustment.
						PerformAdjustmentRoutine();
					}
				}
				else
				{
					//Just confirmed so reset session indicator
					TDSessionManager.Current.JourneyViewState.ConfirmationMode = false;

					// Call PerformAdjustmentRoutine which will save the user selections to session and
					// perform all the steps required for making an adjustment.
					PerformAdjustmentRoutine();
				}
			}
		}

		/// <summary>
		/// Handles the button click to toggle the display format of outward details 
		/// between tabular and graphical formats
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void buttonShowTableDiagram_Click(object sender, EventArgs e)
		{
			// Save the user selections in case we return to this screen
			SaveUserSelections();

			bool showDiagramMode = viewState.ShowOutwardJourneyDetailsDiagramMode;
			viewState.ShowOutwardJourneyDetailsDiagramMode = !showDiagramMode;			
		}

		#endregion

		#region Private methods
		
		/// <summary>
		/// Method to set display properties for page and controls. Called in PreRender event.
		/// </summary>
		private void SetDisplayOptionsForControls()
		{
			if (!confirmationMode)
			{
				// Get correct resource strings for labels
				labelTitle.Text = GetResource("JourneyAdjust.labelTitle.Text");
				labelIntroductoryText.Text = GetResource("JourneyAdjust.labelIntroductoryText.Text");
                cancelButton.Text = GetResource("AdjustOptionsControl.CancelButton.Text");
				adjustOptionsControlPanel.Visible = true;
			}
			else
			{
				// Get correct resource strings for labels
				labelTitle.Text = GetResource("JourneyAdjustConfirmation.labelTitle.Text");
				labelIntroductoryText.Text = GetResource("JourneyAdjustConfirmation.labelIntroductoryText.Text");
				
				adjustOptionsControlPanel.Visible = false;
			}
		}

		/// <summary>
		/// Method to initialise static page and control properties
		/// </summary>
		private void PopulateStaticControls()
		{
			// Set Help Button URL
			//helpButton.HelpUrl = GetResource("JourneyAdjust.helpButton.HelpUrl");

			// Set up the adjust options control
			FindAMode journeyMode;
			journeyMode = GetJourneyMode();
			adjustOptionsControl.FindMode = journeyMode;
			adjustOptionsControl.ArriveBefore = arriveBefore;
			
			if (viewState != null)
			{
				// If there's an adjusted journey, show that, otherwise we haven't done any previous
				// adjusts, so get the selected journey from the orig resultset
				if (isOutwardJourney)
				{
					if (sessionManager.JourneyResult.AmendedOutwardPublicJourney != null)
						journeyToAdjust = sessionManager.JourneyResult.AmendedOutwardPublicJourney;
					else
						journeyToAdjust = sessionManager.JourneyResult.OutwardPublicJourney(viewState.SelectedOutwardJourneyID);
				}
				else
				{
					if (sessionManager.JourneyResult.AmendedReturnPublicJourney != null)
						journeyToAdjust = sessionManager.JourneyResult.AmendedReturnPublicJourney;
					else
						journeyToAdjust = sessionManager.JourneyResult.ReturnPublicJourney(viewState.SelectedReturnJourneyID);
				}
			}
			
			adjustOptionsControl.PublicJourney = journeyToAdjust;

            this.PageTitle = GetResource("JourneyPlanner.JourneyAdjustPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Set the journey direction text label
			if (isOutwardJourney)
				labelJourneyDirection.Text = GetResource("JourneyAdjust.OutwardHeadingText");
			else
				labelJourneyDirection.Text = GetResource("JourneyAdjust.ReturnHeadingText");
			
			// Set up page options control
			pageOptionsControl.AllowBack = true;
			pageOptionsControl.AllowClear = false;
            newJourneyButton.Text = GetResource("langStrings", "JourneyAdjust.newJourneyButton.Text");

		}

		/// <summary>
		/// Method to initialise state dependent page and control properties
		/// </summary>
		/// <remarks>This will be called after events run on postback</remarks>
		private void PopulateStateDependentControls()
		{
			// Set up detail diagram / table control
			if (viewState.ShowOutwardJourneyDetailsDiagramMode)
			{
				// Show diagram control for outward
				journeyAdjustSegmentControl.Initialise(journeyToAdjust, true, true);
				journeyAdjustSegmentControl.MyPageId = pageId;
				journeyAdjustSegmentControl.Visible = true;
				journeyAdjustTableGridControl.Visible = false;

				// Show correct button text
				buttonShowTableDiagram.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowTable.Text");
			}
			else 
			{
				// Show table control for outward
				journeyAdjustTableGridControl.Initialise(journeyToAdjust, isOutwardJourney);
				journeyAdjustTableGridControl.MyPageId = pageId;
				journeyAdjustTableGridControl.Visible = true;
				journeyAdjustSegmentControl.Visible = false;

				// Show correct button text
				buttonShowTableDiagram.Text = GetResource("langStrings", "JourneyDetailsControl.buttonShowDiagram.Text");
			}

			// If this page has been loaded before and user selections made, set the selections to those previously chosen
			TDCurrentAdjustState tsj = TDSessionManager.Current.CurrentAdjustState;
			if (tsj != null)
			{
				adjustOptionsControl.AdjustTimings.SelectedValue = tsj.SelectedAdjustTimingsDropdownValue;
				adjustOptionsControl.AdjustLocations.SelectedValue = tsj.SelectedAdjustLocationsDropdownValue;
			}

		}

		/// <summary>
		/// To show the most suitable journey adjust durations, get the likely journey type from
		/// the FindAMode.
		/// If the search was door to door then we return a FindAMode of None as we don't know the
		/// journey mode and it could be a mixture. Similarly for city to city (this could be multi-modal in future).
		/// </summary>
		/// <returns>FindA mode</returns>
		private FindAMode GetJourneyMode()
		{
			FindAMode findMode;
			if (sessionManager.FindPageState == null)
				findMode = FindAMode.None;
			else
				switch (sessionManager.FindPageState.Mode)
				{
					case FindAMode.Coach :
					case FindAMode.Train :
					case FindAMode.Flight :
						findMode = sessionManager.FindPageState.Mode;
						break;
					default :
						findMode = FindAMode.None;
						break;
				}

			return findMode;

		}

		/// <summary>
		/// Indicates whether the journeys in session were retrieved using an "arrive before" search or a
		/// "leave after" search.
		/// </summary>
		/// <returns>True if "arrive before"</returns>
		private bool GetArriveBeforeIndicator()
		{
			if (isOutwardJourney)
			{
				return sessionManager.JourneyRequest.OutwardArriveBefore;
			}
			else
			{
				return sessionManager.JourneyRequest.ReturnArriveBefore;
			}

		}

		#region Method to perform the adjustment routine

		/// <summary>
		/// Saves the user selections to session and executes the routine to adjust the journey.
		/// </summary>
		private void PerformAdjustmentRoutine()
		{
			// Get the user selections and save into TDCurrentAdjustState object in session
			SaveUserSelections();

			TDCurrentAdjustState tsj = TDSessionManager.Current.CurrentAdjustState;

			// Get the current journey result from TDSessionManger.
			ITDJourneyResult result = TDSessionManager.Current.JourneyResult;

			// Outward/return is already set
			ITDAdjustRoute adjustRoute = new AdjustRoute();

			// Call BuildJourneyRequest passing the TDCurrentAdjustState, to
			// get the adjusted journey request.
			ITDJourneyRequest adjustedJourneyRequest = adjustRoute.BuildJourneyRequest(tsj);

			tsj.AmendedJourneyRequest = adjustedJourneyRequest;

			JourneyPlanRunner.JourneyPlanRunner jpr = new JourneyPlanRunner.JourneyPlanRunner( Global.tdResourceManager );

			AsyncCallState acs = new JourneyPlanState();
			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.Adjust"]);
			acs.WaitPageMessageResourceFile = "RefineJourney";
			acs.WaitPageMessageResourceId = "WaitPageMessage.Adjust";

			acs.AmbiguityPage = pageId;
			acs.DestinationPage = PageId.CompareAdjustedJourney;
			acs.ErrorPage = PageId.CompareAdjustedJourney;
			sessionManager.AsyncCallState = acs;

			// Always succeeds validation
			jpr.ValidateAndRun( result.JourneyReferenceNumber, tsj.JourneyReferenceSequence, adjustedJourneyRequest, sessionManager, GetChannelLanguage(TDPage.SessionChannelName) );

			// Write to the session the TransitionEvent
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;

		}

		/// <summary>
		/// Saves the user selections to session and executes the routine to adjust the journey.
		/// </summary>
		private void SaveUserSelections()
		{
			// Set the user selections into local variables
			SetSelectedAdjustOptions();

			ITDJourneyRequest originalJourneyRequest = viewState.OriginalJourneyRequest;

			TDCurrentAdjustState tsj = new TDCurrentAdjustState(originalJourneyRequest);
			if( TDSessionManager.Current.CurrentAdjustState != null && 
				TDSessionManager.Current.CurrentAdjustState.JourneyReferenceSequence == 0)
			{
				tsj.JourneyReferenceSequence = TDSessionManager.Current.CurrentAdjustState.JourneyReferenceSequence;
			}
                
			// Set the journey in TDCurrentAdjustState
			tsj.AmendedJourney = (journeyToAdjust as JourneyControl.PublicJourney);
			tsj.CurrentAmendmentType = isOutwardJourney ? TDAmendmentType.OutwardJourney : TDAmendmentType.ReturnJourney;

			// Get the user selections passed in and assign to TDCurrentAdjustState object
			tsj.SelectedRouteNode = selectedLegIndex;
			tsj.SelectedRouteNodeSearchType = selectedArriveBefore;
			tsj.MinimumTime = selectedTimingAdjustment;
			tsj.SelectedAdjustTimingsDropdownValue = selectedAdjustTimingsDropdownValue;
			tsj.SelectedAdjustLocationsDropdownValue = selectedAdjustLocationsDropdownValue;

			// Register the state in TDCurrentAdjustState
			TDSessionManager.Current.CurrentAdjustState = tsj;
		}

		/// <summary>
		/// Notes the values selected by the user from the timings and locations dropdowns.
		/// Each timings selection represents 2 values - eg
		/// 1. ArriveBefore, default time adjustment
		/// 2. LeaveAfter, default time adjustment
		/// 3. [ArriveBefore / LeaveAfter] - dependent on initial user search, time adjustment value
		/// </summary>
		private void SetSelectedAdjustOptions()
		{
			// Timings selection
			selectedAdjustTimingsDropdownValue = adjustOptionsControl.AdjustTimings.SelectedValue;

			switch (selectedAdjustTimingsDropdownValue)
			{
				case ArriveEarlier:
					selectedArriveBefore = true;
					selectedTimingAdjustment = 0;
					break;
				case LeaveLater:
					selectedArriveBefore = false;
					selectedTimingAdjustment = 0;
					break;
				case UNREFINED_TEXT:
					break;
				default:
					// Dependent on the search that the user performed initially
					selectedArriveBefore = arriveBefore;
					selectedTimingAdjustment = Convert.ToInt32(adjustOptionsControl.AdjustTimings.SelectedValue, TDCultureInfo.CurrentCulture.NumberFormat);
					break;
			}

			// Location selection
			selectedAdjustLocationsDropdownValue = adjustOptionsControl.AdjustLocations.SelectedValue;

			// Continue if we have a selection
			if (selectedAdjustLocationsDropdownValue != UNREFINED_TEXT)
			{
				// See if the selected value is from an interchange (id so it will be appended by "_Interchange" and
				// we will need to strip)
				int interchangePos = selectedAdjustLocationsDropdownValue.IndexOf(InterchangeIndicator);
				if (interchangePos > 0)
				{
					// This is an interchange point - get the value alone
					selectedLegIndex = Convert.ToUInt32(selectedAdjustLocationsDropdownValue.Substring(0, interchangePos), TDCultureInfo.CurrentCulture.NumberFormat);
				}
				else
				{
					selectedLegIndex = Convert.ToUInt32(selectedAdjustLocationsDropdownValue, TDCultureInfo.CurrentCulture.NumberFormat);
				}
			}

		}

		#endregion

		/// <summary>
		/// Extra Wiring of Events
		/// </summary>
		private void ExtraWiringEvents()
		{
			headerControl.DefaultActionEvent += new System.EventHandler(this.commandSubmit_Click);
			pageOptionsControl.Submit += new System.EventHandler(this.commandSubmit_Click);
			adjustOptionsControl.NextButton.Click += new System.EventHandler(this.commandSubmit_Click);
			CancelButton.Click += new System.EventHandler(this.commandBack_Click);
			pageOptionsControl.Back += new System.EventHandler(this.commandBack_Click);
			buttonShowTableDiagram.Click += new EventHandler(buttonShowTableDiagram_Click);
            this.newJourneyButton.Click += new EventHandler(newJourneyButton_Click);
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

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
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