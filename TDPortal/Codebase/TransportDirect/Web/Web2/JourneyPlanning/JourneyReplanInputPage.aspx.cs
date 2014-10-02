// *********************************************** 
// NAME				 : JourneyDetails.aspx.cs
// AUTHOR			 : Neil Moorhouse
// DATE CREATED		 : 04/01/2006
// DESCRIPTION		 : Journey Replan Input Page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyReplanInputPage.aspx.cs-arc  $
//
//   Rev 1.4   Mar 29 2010 16:40:34   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.3   Jan 13 2009 13:57:20   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:12   mturner
//Initial revision.
//
//   Rev 1.22   Apr 10 2006 16:46:54   tmollart
//Modified code to correctly check null grid references.
//Resolution for 3833: DN068 Replan: Expected error message not shown when replanning from location with 0,0 co-ordinates
//
//   Rev 1.21   Mar 31 2006 16:13:22   RGriffith
//Addition of Default Click
//
//   Rev 1.20   Mar 23 2006 14:45:58   NMoorhouse
//Fix back button error
//Resolution for 3663: Extend, Replan & Adjust: Replan a return journey and result allows to replan return again
//
//   Rev 1.19   Mar 23 2006 13:51:42   NMoorhouse
//Added properties to indicate whether outward and/or return journeys have been replanned
//Resolution for 3663: Extend, Replan & Adjust: Replan a return journey and result allows to replan return again
//
//   Rev 1.18   Mar 22 2006 19:11:08   NMoorhouse
//Change to ensure 'back' goes back to the correct page (could also be ReplanFullItinerarySummary)
//Resolution for 3649: Extend, Replan & Adjust: Issues with Replan of second direction
//
//   Rev 1.17   Mar 22 2006 15:33:02   NMoorhouse
//Fix error with Confirmation mode not being reset
//Resolution for 3649: Extend, Replan & Adjust: Issues with Replan of second direction
//
//   Rev 1.16   Mar 20 2006 14:47:02   NMoorhouse
//Updated following review comments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.15   Mar 17 2006 16:00:28   tmollart
//Added code to detect matching toids and report error to user.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.14   Mar 14 2006 19:50:12   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.13   Mar 14 2006 11:42:46   NMoorhouse
//Post stream3353 merge: added new HeadElementControl, HeaderControls and reference new ResourceManager
//
//   Rev 1.12   Mar 13 2006 13:15:26   NMoorhouse
//Made the code more robust - use as instead of cast to ReplanPageState on page load (incase we don't have a replan page state)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.11   Mar 10 2006 09:48:16   NMoorhouse
//Updates for FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10   Mar 06 2006 18:17:32   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 03 2006 16:04:42   pcross
//Skip links
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 01 2006 14:52:26   pcross
//Correction to adjust segment highlighting to interchanges are always selected on either side of a selected leg.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Feb 24 2006 10:59:48   pcross
//Replan segment and table controls now require arrive before / leave after parameter to be passed in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Feb 20 2006 12:36:20   pcross
//Sends in an indicator to whether we did an arrive before search or a leave after search into segments control so correct highlighting is done
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 17 2006 14:02:24   tmollart
//Added location validation.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 15 2006 16:26:38   tmollart
//Work in progress for Replan.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 09 2006 17:48:30   RGriffith
//Changes to add a confirmation page for non-javascript users.
//
//   Rev 1.2   Feb 07 2006 19:49:46   tmollart
//Work in progress. General modifications plus additional code for replan handling.
//
//   Rev 1.1   Jan 20 2006 09:32:22   NMoorhouse
//Updated during CUT
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 19 2006 11:21:14   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Drawing;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for JourneyReplanInputPage.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyReplanInputPage : TDPage
	{
		// ### Remove after HomepagePhase 2 merge ###
		// Literals
		protected System.Web.UI.WebControls.Literal literalPageTitle;
		
		// Labels

		// Web-user controls
		protected TransportDirect.UserPortal.Web.Controls.JourneyChangeSearchControl journeyChangeSearchControl1;
		protected TransportDirect.UserPortal.Web.Controls.JourneyReplanSegmentControl journeyReplanSegmentControl1;
		protected TransportDirect.UserPortal.Web.Controls.JourneyReplanTableGridControl journeyReplanTableGridControl1;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl1;
		protected TransportDirect.UserPortal.Web.Controls.RefinePageOptionsControl pageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.FooterControl footerControl1;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		// Error message constants
		private const string ERROR_NO_CHECKBOXES_SELECTED = "JourneyReplanInputPage.errorDisplayControl.ErrorString";
		private const string ERROR_FIRST_LOCATION_INVALID = "JourneyReplanInputPage.errorDisplayControl.FirstLocationInvalid";
		private const string ERROR_SECOND_LOCATION_INVALID = "JourneyReplanInputPage.errorDisplayControl.SecondLocationInvalid";
		private const string ERROR_MATCHING_TOIDS = "JourneyReplanInputPage.errorDisplayControl.JourneyToShort";

		// TDButtons

		protected TDJourneyViewState journeyViewState;
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
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress;

		/// <summary>
		/// True if the results have been planned using FindA
		/// </summary>
		private bool showFindA;
		
		/// <summary>
		/// True if Javascript is turned off and the user is asked to confirm the selection when attempting to get to the next page
		/// </summary>
		private bool confirmationMode;
		
		/// <summary>
		/// Constructor - sets the page Id
		/// </summary>
		public JourneyReplanInputPage() : base()
		{
			pageId = PageId.JourneyReplanInputPage;
		}


		/// <summary>
		/// Page Load Method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;
			
			tdSessionManager.JourneyViewState.CongestionChargeAdded = false;
			tdSessionManager.JourneyViewState.VisitedCongestionCompany.Clear();

			journeyChangeSearchControl1.HelpUrl = GetResource("JourneyReplanInputPage.HelpPageUrl");

            journeyChangeSearchControl1.GenericBackButtonVisible = true;
         

			confirmationMode = tdSessionManager.JourneyViewState.ConfirmationMode;

			InitialiseJourneyComponents();

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyReplanInputPage);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Initialisation Method
		/// </summary>
		private void Initialise()
		{
			// Initialise static labels, hypertext text and image button Urls 
			// from Resourcing Mangager.
			// Initialise static labels text

            this.PageTitle = GetResource("JourneyReplanInputPage.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			if (!confirmationMode)
			{
				labelReplanInputTitle.Text = GetResource("JourneyReplanInputPage.labelReplanInputTitle");
				labelReplanInputSubheading.Text = GetResource("JourneyReplanInputPage.labelReplanInputSubheading");
			}
			else
			{
				labelReplanInputTitle.Text = GetResource("JourneyReplanConfirmation.labelReplanConfirmationTitle");
				labelReplanInputSubheading.Text = GetResource("JourneyReplanConfirmation.labelReplanConfirmationSubheading");
			}		
		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);
			showFindA = (!showItinerary && (tdSessionManager.IsFindAMode));

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
				}
			}
		}

		/// <summary>
		/// Initialises page components based on whether the page should be displaying
		/// journey details for a journey or journey extensions and also whether a return
		/// journey has been planned.
		/// </summary>
		private void InitialiseJourneyComponents()
		{
			DetermineStateOfResults();

			TDJourneyViewState viewState = tdSessionManager.JourneyViewState;
			ReplanPageState replanPageState = tdSessionManager.InputPageState as ReplanPageState;

			if (replanPageState != null)
			{			
				// Note: We are only dealing with a public transport journey at this time so
				// it has to be casted as such.
				PublicJourney journeySelectedForReplan = (PublicJourney)replanPageState.JourneySelectedForReplan;

				//Display Outward or Return Detail
				//if outward
				if (replanPageState.CurrentAmendmentType == TDAmendmentType.OutwardJourney)
				{
					labelJourneyDirection.Text = GetResource("JourneyReplanInputPage.OutwardHeadingText");

					if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						if (viewState.ShowOutwardJourneyDetailsDiagramMode)  
						{
							journeyReplanSegmentControl1.Initialise(journeySelectedForReplan, true, false, false, false, true, true);
							journeyReplanSegmentControl1.MyPageId = pageId;
						} 
						else 
						{
							journeyReplanTableGridControl1.Initialise(journeySelectedForReplan, true, -1);
							journeyReplanTableGridControl1.MyPageId = pageId;
						}
					}

				} 
					//Else return
				else
				{
					labelJourneyDirection.Text = GetResource("JourneyReplanInputPage.ReturnHeadingText");

					if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						if (viewState.ShowOutwardJourneyDetailsDiagramMode) 
							// Note: Check is on OutwardDiagramMode as same viewState variable is used to check table/diagram mode in both directions
						{
							journeyReplanSegmentControl1.Initialise(journeySelectedForReplan, false, false, false, false, true, true);
							journeyReplanSegmentControl1.MyPageId = pageId;
						} 
						else 
						{
							journeyReplanTableGridControl1.Initialise(journeySelectedForReplan, false, -1);
							journeyReplanTableGridControl1.MyPageId = pageId;
						}
					}

				}
			}

		}

		/// <summary>
		/// OnPreRender method - overrides base and updates the visiblity
		/// of controls depending on which should be rendered. Calls base OnPreRender
		/// as the final step.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			// Set the page title and subheading
			Initialise();

			//pageOptionsControl.AllowBack = true;
			pageOptionsControl.AllowClear = !confirmationMode;

			itineraryManager = TDItineraryManager.Current;

			InitialiseJourneyComponents();

			showItinerary =  itineraryManager.Length > 0;
			
			SetControlVisibility();

			SetupSkipLinksAndScreenReaderText();

			base.OnPreRender(e);
		}

		/// <summary>
		/// Determines which controls should be visible
		/// </summary>
		private void SetControlVisibility()
		{
			TDJourneyViewState viewState = tdSessionManager.JourneyViewState;

			// Journey Details control is visible if diagram mode
			journeyReplanSegmentControl1.Visible = viewState.ShowOutwardJourneyDetailsDiagramMode;

			// Journey Details table control is visible if not diagram mode.
			journeyReplanTableGridControl1.Visible = !viewState.ShowOutwardJourneyDetailsDiagramMode;

			if (viewState.ShowOutwardJourneyDetailsDiagramMode) 
			{
				buttonShowTableDiagram.Text = GetResource("JourneyDetailsControl.buttonShowTable.Text");
			} 
			else 
			{
				buttonShowTableDiagram.Text = GetResource("JourneyDetailsControl.buttonShowDiagram.Text");
			}

			//Display the ErrorDisplayControl if it has been populated with error message
			if(errorDisplayControl1.ErrorStrings.Length > 0)
			{
				errorMessagePanel.Visible = true;
				errorDisplayControl1.Visible = true;
			}
			else
			{
				errorMessagePanel.Visible = false;
				errorDisplayControl1.Visible = false;
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
			imageMainContentSkipLink1.AlternateText = GetResource("JourneyReplanInputPage.imageMainContentSkipLink.AlternateText");

		}

		/// <summary>
		/// Handles the button click to toggle the display format of outward details 
		/// between tabular and graphical formats
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void buttonShowTableDiagram_Click(object sender, EventArgs e)
		{
			bool showDiagramMode = itineraryManager.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode;
			itineraryManager.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode = !showDiagramMode;			
		}

		/// <summary>
		/// Handles the JourneyElementSelectionChanged event from journey replan segment control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void journeyReplanSegmentControl_JourneyElementSelectionChanged(object sender, EventArgs e)
		{
			//Get selected public journey element indexes to be replan as car
			if (!confirmationMode)
			{
				ArrayList selectedJourneyElement = journeyReplanSegmentControl1.SelectedJourneyElementValues;
			
				if (selectedJourneyElement != null)
				{
					if (selectedJourneyElement.Count > 0)
					{
						//store start journey detail index in Page State
						((ReplanPageState)tdSessionManager.InputPageState).ReplanStartJourneyDetailIndex = (int) selectedJourneyElement[0];
						((ReplanPageState)tdSessionManager.InputPageState).ReplanEndJourneyDetailIndex = (int) selectedJourneyElement[(selectedJourneyElement.Count-1)];

					
					}
					else
					{
						//no journey elements selected so reset start and end values
						((ReplanPageState)tdSessionManager.InputPageState).ReplanStartJourneyDetailIndex = -1;
						((ReplanPageState)tdSessionManager.InputPageState).ReplanEndJourneyDetailIndex = -1;
					}
				}
			}
		}

		/// <summary>
		/// Handles the JourneyElementSelectionChanged event from journey replan table grid control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void journeyReplanTableGridControl_JourneyElementSelectionChanged(object sender, EventArgs e)
		{
			//Get selected public journey element indexes to be replan as car
			if (!confirmationMode)
			{
				ArrayList selectedJourneyElement = journeyReplanTableGridControl1.SelectedJourneyElementValues;
			
				if (selectedJourneyElement != null)
				{
					if (selectedJourneyElement.Count > 0)
					{
						//store start journey detail index in Page State
						((ReplanPageState)tdSessionManager.InputPageState).ReplanStartJourneyDetailIndex = (int) selectedJourneyElement[0];
						//store end journey detail index in Page State
						((ReplanPageState)tdSessionManager.InputPageState).ReplanEndJourneyDetailIndex = (int) selectedJourneyElement[(selectedJourneyElement.Count-1)];
					}
					else
					{
						//no journey elements selected so reset start and end values
						((ReplanPageState)tdSessionManager.InputPageState).ReplanStartJourneyDetailIndex = -1;
						((ReplanPageState)tdSessionManager.InputPageState).ReplanEndJourneyDetailIndex = -1;
					}
				}
			}
		}

		/// <summary>
		/// Resets the page, clearing all checkboxes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandClear_Click(object sender, System.EventArgs e)
		{
			//reset start and end values
			((ReplanPageState)tdSessionManager.InputPageState).ReplanStartJourneyDetailIndex = -1;
			((ReplanPageState)tdSessionManager.InputPageState).ReplanEndJourneyDetailIndex = -1;
		}

		/// <summary>
		/// Returns the user to the input page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void buttonBack_Click(object sender, System.EventArgs e)
        {
            //if in confirmation mode
            if (confirmationMode)
            {
                confirmationMode = false;
                tdSessionManager.JourneyViewState.ConfirmationMode = false;
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyReplanReturn;
            }
            else
            {
                //If already completed a replan
                if (((ReplanItineraryManager)itineraryManager).OutwardReplanned || ((ReplanItineraryManager)itineraryManager).ReturnReplanned)
                {
                    //Redirect back to results page 
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ReplanFullItinerarySummary;
                }
                else
                {
                    //Redirect back to refine journey plan page
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineJourneyPlan;
                }
            }
        }

		/// <summary>
		/// Submit, replan the journey
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandSubmit_Click(object sender, System.EventArgs e)
		{		
	
			ArrayList errorsList = new ArrayList();

			bool replanJourney = true;
			
			if ((((ReplanPageState)tdSessionManager.InputPageState).ReplanStartJourneyDetailIndex == -1) &&
				(((ReplanPageState)tdSessionManager.InputPageState).ReplanEndJourneyDetailIndex == -1))
			{
				replanJourney = false;
				errorsList.Add(GetResource(ERROR_NO_CHECKBOXES_SELECTED));
			}
			else
			{
				// If not in confirmation mode - check for javascript
				if (!confirmationMode)
				{
					TDPage tdPage = (TDPage)this.Page;
					// If Javascript is not enabled - got to the confirmation page
					if (!tdPage.IsJavascriptEnabled)
					{
						// Set boolean to not replan the journey
						replanJourney = false;
					
						// Change confirmation mode setting
						confirmationMode = true;
						tdSessionManager.JourneyViewState.ConfirmationMode = true;
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyReplanReturn;
					}
				}
				else
				{
					//Just confirmed so reset session indicator
					tdSessionManager.JourneyViewState.ConfirmationMode = false;
				}
			}

			// If replan is valid (ie: javascript is on or confirmation has been made and valid selection 
			// has been made) perform validation on the page and if appropriate run the replan.
			if (replanJourney)
			{
				ReplanItineraryManager itineraryManager = (ReplanItineraryManager)tdSessionManager.ItineraryManager;
				ReplanPageState pageState = (ReplanPageState)tdSessionManager.InputPageState;
				bool validationErrorsExist = false;

				ReplanRunner runner = new ReplanRunner();

				PublicJourneyDetail startDetail = (PublicJourneyDetail)pageState.JourneySelectedForReplan.JourneyLegs[pageState.ReplanStartJourneyDetailIndex];
				PublicJourneyDetail endDetail = (PublicJourneyDetail)pageState.JourneySelectedForReplan.JourneyLegs[pageState.ReplanEndJourneyDetailIndex];
				
				// Validate first location.
				if (!startDetail.LegStart.Location.GridReference.IsValid)
				{
					errorsList.Add(GetResource(ERROR_FIRST_LOCATION_INVALID));
					validationErrorsExist = true;
				}

				// Validate second location.
				if (!endDetail.LegEnd.Location.GridReference.IsValid)
				{
					errorsList.Add(GetResource(ERROR_SECOND_LOCATION_INVALID));
					validationErrorsExist = true;
				}

				// Get toids for the two selected locations if there are no validation errors.
				if (!validationErrorsExist)
				{
					startDetail.LegStart.Location.PopulateToids();
					endDetail.LegEnd.Location.PopulateToids();

					bool matchingToids = startDetail.LegStart.Location.IsMatchingTOIDGroup(endDetail.LegEnd.Location);

					// If there are matching Toids an error message must be displayed to the user.
					if (matchingToids)
					{
						errorsList.Clear();
						errorsList.Add(GetResource(ERROR_MATCHING_TOIDS));
						validationErrorsExist = true;
					}
				}

				// If no validation errors at this stage run the journey plan.
				if (!validationErrorsExist)
				{
					runner.RunReplan(
						pageState,
						tdSessionManager.JourneyResult,
						itineraryManager,
						null,
						null);

					//NOTE: Calling the runner provides the transition event to the wait page.
				}
			}

			// If error messages are present then display the messages to the user.
			if (errorsList.Count > 0)
			{
				errorDisplayControl1.ErrorStrings = (string[])errorsList.ToArray(typeof(string));
			}
		}

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

		/// <summary>
		/// Extra Wiring of Events
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.journeyReplanSegmentControl1.JourneyElementSelectionChanged += new EventHandler(journeyReplanSegmentControl_JourneyElementSelectionChanged);
			this.journeyReplanTableGridControl1.JourneyElementSelectionChanged += new EventHandler(journeyReplanTableGridControl_JourneyElementSelectionChanged);
			this.buttonShowTableDiagram.Click += new EventHandler(this.buttonShowTableDiagram_Click);
            this.journeyChangeSearchControl1.GenericBackButton.Click += new EventHandler(this.buttonBack_Click);
			this.pageOptionsControl.Clear += new System.EventHandler(this.commandClear_Click);
			this.pageOptionsControl.Submit += new System.EventHandler(this.commandSubmit_Click);
			this.headerControl.DefaultActionEvent += new System.EventHandler(this.commandSubmit_Click);
		}
		#endregion
	}
}
