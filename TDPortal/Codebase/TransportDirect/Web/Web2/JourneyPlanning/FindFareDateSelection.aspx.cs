// *********************************************** 
// NAME                 : FindFareDateSelection.aspx.cs
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 20/01/2005
// DESCRIPTION			: Page that allows the User to view the Ticket price ranges for the
//						results of a Public Transport Fare based request, and to then narrow
//						their search to a particular Outward/Return TravelDate.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindFareDateSelection.aspx.cs-arc  $
//
//   Rev 1.3   Oct 28 2010 16:28:08   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:24:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:28   mturner
//Initial revision.
//
//   Rev 1.37   Nov 17 2006 14:16:10   tmollart
//Modified code to create the correct Async Call State object based on the FindAMode.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.36   Nov 14 2006 10:19:08   rbroddle
//Merge for stream4220
//
//   Rev 1.35.1.0   Nov 12 2006 15:32:00   tmollart
//Added code to control the visibility of the coach options on the amend fare control for Rail Search by Price.
//Resolution for 4220: Rail Search by Price
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.36   Nov 12 2006 15:31:12   tmollart
//Added code to control the visibility of the coach options on the amend fare control for Rail Search by Price.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.35   May 04 2006 12:56:14   RPhilpott
//Force manual prerender of AmendFaresControl during page_load to ensure that dropdowns get populated at the correct time.
//Resolution for 4071: Del 8.1: Return fares displayed on costs page but Amend fares tab shows single
//
//   Rev 1.34   Apr 26 2006 12:20:04   RPhilpott
//Correct handling of switch from single to return fares when "Two Singles" was already selected.
//
//   Rev 1.33   Apr 10 2006 17:41:54   RPhilpott
//Restore initialisation of findFareReturnTravelDatesControl incorrectly removed for CCN 243.
//Resolution for 3859: Del 8.1 Find a Fare and Find Cheaper: Server error for single journey
//
//   Rev 1.32   Apr 05 2006 15:42:54   build
//Automatically merged from branch for stream0030
//
//   Rev 1.31.1.0   Mar 29 2006 11:31:40   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.31   Mar 24 2006 17:20:06   kjosling
//Automatically merged stream 0023
//
//   Rev 1.30.1.0   Mar 14 2006 11:58:52   kjosling
//Added support for conversions from single to return and open return journeys
//Resolution for 23: DEL 8.1 Workstream - Journey Results - Phase 1
//
//   Rev 1.30   Feb 23 2006 19:14:54   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.29   Feb 10 2006 11:06:04   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.28   Jan 10 2006 16:20:46   AViitanen
//WAI changes - amended page title (MP03001b, BL49) 
//Resolution for 3437: Del 8 - WAI changes (MP03001b)
//
//   Rev 1.27   Nov 30 2005 17:57:10   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.26   Nov 28 2005 17:03:40   mguney
//In AmendFaresControl_Click event handler  pageState.SelectedTicketType is set whether or not reformatResults is true.
//Resolution for 3132: DN040 - "Amend fare details" does not amend completely
//
//   Rev 1.25   Nov 23 2005 17:25:44   mguney
//In AmendFaresControl_Click event handler  pageState.ShowChild is set whether or not reformatResults is true.
//Resolution for 3132: DN040 - "Amend fare details" does not amend completely
//
//   Rev 1.24   Nov 14 2005 18:32:54   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.23   Nov 09 2005 17:27:58   RPhilpott
//Merge for stream2818
//
//   Rev 1.22   Nov 07 2005 18:44:10   ralonso
//commandDateSelectionSubmit imageButton changed to buttonDateSelectionSubmit tdButton
//
//   Rev 1.21   Nov 07 2005 14:41:30   ralonso
//Fixed problem with td buttons
//
//   Rev 1.20   Nov 03 2005 15:56:18   kjosling
//Automatically merged from branch for stream2638
//   Rev 1.19.1.4   Oct 24 2005 15:41:42   RGriffith
//Changes to accomodate AmendViewControl changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.19.1.3   Oct 24 2005 14:00:56   RGriffith
//Changes to accomodate AmendViewControl changes
//
//   Rev 1.19.1.1   Oct 17 2005 17:39:50   rgreenwood
//TD089 ES020: changed event types and handler for AmendFaresControl.ascx OK button
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.19.1.0   Oct 12 2005 13:14:14   rgreenwood
//Td089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.19   May 10 2005 17:41:16   rhopkins
//FxCop corrections
//
//   Rev 1.18   May 05 2005 14:17:20   tmollart
//Changed error message displaying code so that partial results error message only displayed when some results exist.
//Resolution for 2363: PT - Find A Fare - Multiple error messages displayed on Date Selection screen
//
//   Rev 1.17   Apr 29 2005 18:02:40   jmorrissey
//Update to DisplayErrors method to remove existing error when adding a Partial results error.
//Resolution for 2363: PT: Find A Fare - Multiple error messages displayed on Date Selection screen
//
//   Rev 1.16   Apr 28 2005 12:48:36   rgeraghty
//InvokeSearch mthod updated to ensure ambiguity mode stiored in session
//Resolution for 2347: Del 7 - PT - Date validation on date/transport option page
//
//   Rev 1.15   Apr 27 2005 16:47:24   jmorrissey
//Update to SubmitClick method so that Singles tickets are retrieved using the new FindFareCostFacadeAdapter.GetSinglesTicketDetails method
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.14   Apr 26 2005 10:59:46   COwczarek
//Only save drop down selection in session if OK button clicked
//Resolution for 2099: PT: Find A Fare singles ticket selection page needs a link to return ticket selection page
//
//   Rev 1.13   Apr 21 2005 13:31:34   rhopkins
//Changed way that error messages are handled so that they are displayed correctly/appropriately.
//Resolution for 2287: FindAFare date selection is not displaying errors correctly
//
//   Rev 1.12   Apr 20 2005 10:31:22   RPhilpott
//Changes to CostSearchResult error handling. 
//
//   Rev 1.11   Apr 15 2005 11:55:38   rgeraghty
//Update to error message handling
//Resolution for 2148: PT - Fares Journey Planner tries to sell day returns for journeys seperated by a day
//
//   Rev 1.10   Apr 12 2005 18:15:18   rhopkins
//Clear FareDateTable before invoking search, so that the table data will be reloaded with the new results.
//Resolution for 2114: FindFare amended searches don't display new TravelDate results
//
//   Rev 1.9   Apr 09 2005 13:12:32   tmollart
//Added force redirect statement to InvokeSearch method. This forces non synchronous rail search's to reload the page refreshing the results controls.
//
//   Rev 1.8   Apr 08 2005 20:20:18   rhopkins
//Added alt text to Help button
//
//   Rev 1.7   Apr 07 2005 16:38:16   rhopkins
//Added Help button and white-space and corrected AmendView handling.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.6   Mar 31 2005 11:52:24   rgeraghty
//Updates made due to Fx Cop changes applied to AmendFaresControl
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Mar 30 2005 16:35:42   rhopkins
//Correct event handling and error handling
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.4   Mar 17 2005 16:50:34   rhopkins
//Changed handling of events on AmendSaveSend controls.
//Also set selected TravelDate/Tickets for "Next" page.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.3   Mar 08 2005 16:55:38   rhopkins
//Partial handling of events in AmendSaveSend...
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Feb 25 2005 12:49:26   rhopkins
//Additional controls added to page
//Resolution for 1927: DEV Code Review: FAF date selection
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.CostSearch;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page that allows the User to view the Ticket price ranges for the
	///	results of a Public Transport Fare based request, and to then narrow
	///	their search to a particular Outward/Return TravelDate.
	/// </summary>
	public partial class FindFareDateSelection : TDPage, ILanguageHandlerIndependent
	{
		private const string ERROR_EXPERIENCING_DIFFICULTIES_FARES = "CostSearchError.FaresInternalError";
		private const string ERROR_UNABLE_TO_FIND_FARES = "CostSearchError.NoFaresResults";
		private const string ERROR_UNABLE_TO_FIND_FARES_OPEN_RETURN = "CostSearchError.NoOpenReturn";
		private const string ERROR_UNABLE_TO_FIND_FARES_INCOMPLETE = "CostSearchError.PartialReturn";

		// .Net Controls

		// User Controls
		protected HeaderControl headerControl;
		protected JourneyChangeSearchControl theJourneyChangeSearchControl;
		protected JourneysSearchedForControl theJourneysSearchedForControl;
		protected FindFareSingleTravelDatesControl findFareSingleTravelDatesControl;
		protected FindFareReturnTravelDatesControl findFareReturnTravelDatesControl;
		protected AmendSaveSendControl amendSaveSendControl;

		// Private members
		private FindFareInputAdapter findFareInputAdapter;
		private ITDSessionManager tdSessionManager;
		private FindCostBasedPageState pageState;
		private CostSearchParams searchParams;
		private bool invokeSearch;
		private bool reformatResults;

		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public FindFareDateSelection() : base()
		{
			pageId = PageId.FindFareDateSelection;
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			tdSessionManager = TDSessionManager.Current;
			pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
			searchParams = (CostSearchParams)tdSessionManager.JourneyParameters;
			findFareInputAdapter = new FindFareInputAdapter(pageState, tdSessionManager);

			PageTitle = GetResource("FindFareDateSelection.DefaultPageTitle")+ GetResource("FindFareDateSelection.AppendPageTitle");
			instructionLabel.Text = GetResource("FindFareDateSelection.InstructionText");

			// Setup Help
			helpLabel.Text = GetResource("FindFareDateSelectionHelpLabel");
			helpLabel.MoreHelpUrl = GetResource("FindFareDateSelectionHelpLabel.moreURL");

			buttonDateSelectionSubmit.Text = GetResource("FindFareDateSelection.Submit.Text");
			buttonDateSelectionSubmit.ToolTip = GetResource("FindFareDateSelection.Submit.ToolTip");

			if ((pageState.SelectedTicketType == TicketType.Return) || (pageState.SelectedTicketType == TicketType.Singles))
			{
				findFareSingleTravelDatesControl.Visible = false;
				findFareReturnTravelDatesControl.Visible = true;
				findFareReturnTravelDatesControl.PageState = pageState;
				findFareReturnTravelDatesControl.SearchParams = searchParams;
			}
			else
			{
				findFareReturnTravelDatesControl.Visible = false;
				findFareSingleTravelDatesControl.Visible = true;
				findFareSingleTravelDatesControl.PageState = pageState;
				findFareSingleTravelDatesControl.SearchParams = searchParams;
			}

			// Initialise AmendSaveSend panel
			amendSaveSendControl.Initialise(pageId);
			if (TDSessionManager.Current.JourneyViewState == null)
			{
				TDSessionManager.Current.JourneyViewState = new TDJourneyViewState();
				TDSessionManager.Current.JourneyViewState.SelectedTabIndex = (int)AmendPanelMode.AmendFareDetail;
			}
			else if (TDSessionManager.Current.JourneyViewState.SelectedTabIndex == (int)AmendPanelMode.None)
			{
				TDSessionManager.Current.JourneyViewState.SelectedTabIndex = (int)AmendPanelMode.AmendFareDetail;
			}
			amendSaveSendControl.AmendViewControl.TimeBasedFindAMode = tdSessionManager.TimeBasedFindAMode;
			amendSaveSendControl.AmendViewControl.PartitionSelectionAvailable = tdSessionManager.HasTimeBasedJourneyResults;

			// Code to turn off coach card drop down if this is a
			// rail cost search.
			if (tdSessionManager.FindAMode == FindAMode.RailCost)
			{
				amendSaveSendControl.AmendFaresControl.CoachCardOptionsVisible = false;
			}

			// Manually pre-render the page.
			amendSaveSendControl.AmendFaresControl.manualPreRender();

            SetUpStepsControl();

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindFareDateSelection);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			amendSaveSendControl.AmendViewControl.PartitionSelected = PartitionType.FindAFare;

			// Set up control to display correct data when page is first displayed
			amendSaveSendControl.AmendFaresControl.ShowCostBasedTicketType = pageState.SelectedTicketType;
			amendSaveSendControl.AmendFaresControl.ShowChildFares = pageState.ShowChild;
			amendSaveSendControl.AmendFaresControl.RailCard = searchParams.RailDiscountedCard;
			amendSaveSendControl.AmendFaresControl.CoachCard = searchParams.CoachDiscountedCard;

			if (pageState.SelectedTicketType == TicketType.Return)
			{
				noteLabel.Text = GetResource("FindFareDateSelection.ReturnSinglesNote");
			}
			else
			{
				noteLabel.Text = GetResource("FindFareDateSelection.SingleNote");
			}

			if ( (pageState.FareDateTable == null) || (pageState.FareDateTable.Length == 0) )
			{
				// No results have been found so don't display the "Next" button
				buttonDateSelectionSubmit.Visible = false;
			}

			DisplayErrors();

		}

		/// <summary>
		/// Display appropriate messages for any errors that have occured
		/// </summary>
		private void DisplayErrors()
		{
			AsyncCallState acs = TDSessionManager.Current.AsyncCallState;
			AsyncCallStatus resultsStatus;

			//get errors
			CostSearchError[] searchErrors = pageState.SearchResult.GetErrors();

			bool hasSearchResultErrors = (pageState.SearchResult.GetErrors().Length > 0);
			bool treatErrorsAsWarnings = false;

			//the front end should only add errors if none alraedy exist
			if (!hasSearchResultErrors)
			{
			
				if (acs == null)
				{
					// If CostSearchWaitStateData is null then some unexpected error has occurred
					pageState.SearchResult.AddError(new CostSearchError(ERROR_EXPERIENCING_DIFFICULTIES_FARES));
				}
				else
				{
				resultsStatus = acs.Status;

					if (resultsStatus == AsyncCallStatus.TimedOut)
					{
						// Search timed out
						pageState.SearchResult.AddError(new CostSearchError(ERROR_EXPERIENCING_DIFFICULTIES_FARES));
					}
					else if (resultsStatus == AsyncCallStatus.CompletedOK)
					{
						if (pageState.SearchResult == null)
						{
							// The search claims to have completed OK but the results object has not been created
							pageState.SearchResult.AddError(new CostSearchError(ERROR_EXPERIENCING_DIFFICULTIES_FARES));
						}
						else
						{
							if ( (pageState.FareDateTable == null) || (pageState.FareDateTable.Length == 0) )
							{
								// The search claims to have completed OK but no valid travel dates were found
								if (!hasSearchResultErrors)
								{
									// Unknown reason for not finding fares
									pageState.SearchResult.AddError(new CostSearchError(ERROR_UNABLE_TO_FIND_FARES));
								}
							}
							else if (pageState.FareDateTable.ErrorForOutward || pageState.FareDateTable.ErrorForInward)
							{
								// We have found some results but we got errors during the search
								pageState.SearchResult.AddError(new CostSearchError(ERROR_UNABLE_TO_FIND_FARES_INCOMPLETE));
								treatErrorsAsWarnings = true;
							}
						}
					}
				}
			}
			//if results were found for just one mode then clear the error for the failed mode,
			//and replace it with a partial results error
			else
			{		
				if ((pageState.FareDateTable != null) && (pageState.FareDateTable.Length > 0)) 
				{
					foreach (CostSearchError error in searchErrors)
					{
						//this error will exist if no coach fares returned
						if (error.ResourceID.ToString() == ERROR_UNABLE_TO_FIND_FARES)
						{
							pageState.SearchResult.ClearErrors();
							break;
						}
						//this error will exist if no rail fares returned
						if (error.ResourceID.ToString() == ERROR_EXPERIENCING_DIFFICULTIES_FARES)
						{
							pageState.SearchResult.ClearErrors();
							break;
						}						
					}
					
					//add partial results error
					pageState.SearchResult.AddError(new CostSearchError(ERROR_UNABLE_TO_FIND_FARES_INCOMPLETE));
					treatErrorsAsWarnings = true;
				}
			}

			// Display any errors that have occured
			if (treatErrorsAsWarnings)
			{
				// The errors do not prevent the User from continuing, so instructionPanel should not be altered
				findFareInputAdapter.DisplayMessages(errorMessagePanel, errorMessageLabel);
			}
			else
			{
				// The errors do prevent the User from continuing, so instructionPanel should be altered
				findFareInputAdapter.DisplayMessages(instructionPanel, errorMessagePanel, errorMessageLabel);
			}

			pageState.SearchResult.ClearErrors();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();

			// Attach the event handler to update the displayed data if the AmendFaresControl indicates a change to the User's request
			amendSaveSendControl.AmendFaresControl.DropDownListItineraryType.SelectedIndexChanged += new EventHandler(AmendFaresControl_TicketTypeChanged);
			amendSaveSendControl.AmendFaresControl.DropDownListAge.SelectedIndexChanged += new EventHandler(AmendFaresControl_AdultChildChanged);
			amendSaveSendControl.AmendFaresControl.DropDownListRailCard.SelectedIndexChanged += new EventHandler(AmendFaresControl_DiscountRailCardChanged);
			amendSaveSendControl.AmendFaresControl.DropDownListCoachCard.SelectedIndexChanged += new EventHandler(AmendFaresControl_DiscountCoachCardChanged);
			amendSaveSendControl.AmendFaresControl.OKButton.Click += new EventHandler(AmendFaresControl_Click);

			amendSaveSendControl.AmendViewControl.SubmitButton.Click += new EventHandler(AmendViewControl_Click);

			amendSaveSendControl.AmendCostSearchDateControl.OkButton.Click += new EventHandler(AmendCostSearchDateControl_Click);

			buttonDateSelectionSubmit.Click += new EventHandler(this.SubmitClick);

			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion Web Form Designer generated code

		#region EventHandlers

		//Handle the adult/child changed event.
		private void AmendFaresControl_TicketTypeChanged(object sender, EventArgs e)
		{
			reformatResults = true;
		}

		//Handle the adult/child changed event.
		private void AmendFaresControl_AdultChildChanged(object sender, EventArgs e)
		{
			reformatResults = true;
		}

		//Handle the discount rail card changed event.
		private void AmendFaresControl_DiscountRailCardChanged(object sender, EventArgs e)
		{
			invokeSearch = true;
		}

		//Handle the discount coach card changed event.
		private void AmendFaresControl_DiscountCoachCardChanged(object sender, EventArgs e)
		{
			invokeSearch = true;
		}

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendFaresControl.
		/// This will update the TravelDate data that are displayed in the results table.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendFaresControl_Click(object sender, EventArgs e)
		{
			pageState.ShowChild = amendSaveSendControl.AmendFaresControl.ShowChildFares;
			pageState.SelectedTicketType = amendSaveSendControl.AmendFaresControl.ShowCostBasedTicketType;                			

			if (invokeSearch)
			{
                searchParams.RailDiscountedCard = amendSaveSendControl.AmendFaresControl.RailCard;
                searchParams.CoachDiscountedCard = amendSaveSendControl.AmendFaresControl.CoachCard;
                InvokeSearch();
			}
			else if (reformatResults)
			{
                
                if ((pageState.SelectedTicketType == TicketType.Return) || (pageState.SelectedTicketType == TicketType.Singles))
				{
					findFareReturnTravelDatesControl.PopulateTravelDatesTable();
				}
				else
				{
					findFareSingleTravelDatesControl.PopulateTravelDatesTable();
				}
			}
		}

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendViewControl.
		/// This will switch Session partitions and go the the TimeBased results Summary page.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendViewControl_Click(object sender, EventArgs e)
		{
			if (amendSaveSendControl.AmendViewControl.PartitionSelected != PartitionType.FindAFare)
			{
				tdSessionManager.Partition = TDSessionPartition.TimeBased;
				//Transfer the user to Time Based results
				tdSessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneySummary;
			}
		}

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendCostSearchDateControl.
		/// This will invoke a new Fares search, based on the new dates.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendCostSearchDateControl_Click(object sender, EventArgs e)
		{
			searchParams.OutwardDayOfMonth = amendSaveSendControl.AmendCostSearchDateControl.SelectControlOutward.Day;
			searchParams.OutwardMonthYear = amendSaveSendControl.AmendCostSearchDateControl.SelectControlOutward.MonthYear;
			searchParams.OutwardFlexibilityDays = amendSaveSendControl.AmendCostSearchDateControl.SelectControlOutward.Flexibility;

			if (amendSaveSendControl.AmendCostSearchDateControl.SelectControlInward.Visible == true)
			{
				searchParams.ReturnDayOfMonth = amendSaveSendControl.AmendCostSearchDateControl.SelectControlInward.Day;
				searchParams.ReturnMonthYear = amendSaveSendControl.AmendCostSearchDateControl.SelectControlInward.MonthYear;
				searchParams.InwardFlexibilityDays = amendSaveSendControl.AmendCostSearchDateControl.SelectControlInward.Flexibility;

				pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
				
				
				// if we are switching from single or open return to dated return or open return,
				//  need to change ticket type, but not if we are already showing two singles ... 

				if	(searchParams.ReturnDayOfMonth != string.Empty && pageState.SelectedTicketType != TicketType.Singles)
				{
					pageState.SelectedTicketType = TicketType.Return; 
				}
				else if (searchParams.ReturnMonthYear == "OpenReturn")
				{
					pageState.SelectedTicketType = TicketType.OpenReturn;
				}
			}
			InvokeSearch();
		}

		/// <summary>
		/// Next Click event handler.  Passes control to the FindFareTicketSelection page.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void SubmitClick(object sender, EventArgs e)
		{
			int selectedIndex = -1;

			FindFareCostFacadeAdapter costFacadeAdapter = new FindFareCostFacadeAdapter(pageState);

			if ((pageState.SelectedTicketType == TicketType.Return) || (pageState.SelectedTicketType == TicketType.Singles))
			{
				selectedIndex = findFareReturnTravelDatesControl.SelectedIndex;
			}
			else
			{
				selectedIndex = findFareSingleTravelDatesControl.SelectedIndex;
			}

			if (selectedIndex < 0)
			{
				// No Travel Date has been selected
				return;
			}

			pageState.SelectedTravelDateIndex = selectedIndex;

			if (pageState.SelectedTravelDate == null)
			{
				// No valid Travel Date has been selected
				return;
			}

			TravelDate selectedTravelDate = pageState.SelectedTravelDate.TravelDate;

			TDDateTime outwardDate = selectedTravelDate.OutwardDate;
			TDDateTime inwardDate = selectedTravelDate.ReturnDate;

			switch (selectedTravelDate.TicketType)
			{
				case TicketType.Single:
				case TicketType.OpenReturn:
					pageState.SingleOrReturnTicketTable = costFacadeAdapter.GetSingleTicketDetails(true,outwardDate);
					pageState.SelectedSingleOrReturnTicketIndex = pageState.SingleOrReturnTicketTable.DefaultSelectedIndex;
					pageState.SelectedSingleDate = outwardDate;
					break;

				case TicketType.Return:
					pageState.SingleOrReturnTicketTable = costFacadeAdapter.GetReturnTicketDetails(outwardDate, inwardDate);
					pageState.SelectedSingleOrReturnTicketIndex = pageState.SingleOrReturnTicketTable.DefaultSelectedIndex;
					pageState.SelectedReturnOutwardDate = outwardDate;
					pageState.SelectedReturnInwardDate = inwardDate;
					break;

				case TicketType.Singles:
					pageState.SelectedSinglesOutwardDate = outwardDate;
					pageState.SelectedSinglesInwardDate = inwardDate;
					pageState.OutwardTicketTable = costFacadeAdapter.GetSinglesTicketDetails(true, outwardDate, inwardDate);
					pageState.InwardTicketTable = costFacadeAdapter.GetSinglesTicketDetails(false, outwardDate, inwardDate);
					pageState.SelectedOutwardTicketIndex = pageState.OutwardTicketTable.DefaultSelectedIndex;
					pageState.SelectedInwardTicketIndex = pageState.InwardTicketTable.DefaultSelectedIndex;
					break;
			}

			// If user wanted return journeys, but only singles are available for the selected
			// travel date then display a message to the user.			
			if (pageState.SelectedTicketType == TicketType.Return && 
				pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles) 
			{
				pageState.SearchResult.AddError(new CostSearchError("FindFareTicketSelectionSingles.NoReturnJourneys.Text"));
			}

			tdSessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareTicketSelectionDefault;
		}

		#endregion EventHandlers

		#region Private Methods

		/// <summary>
		/// Invokes a new search with the modified parameters
		/// </summary>
		private void InvokeSearch()
		{
			pageState.FareDateTable = null;

			// Determine refresh interval and resource string for the wait page
			int refreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindFare.FindFareDateSelection"]);
			string resourceFilename = "FindAFare";
			string resourceId = "WaitPageMessage.FindFare.FindFareDateSelection";

			// Initialise the correct Async Call State object dependent on the Find A Mode
			if ( tdSessionManager.FindAMode == FindAMode.RailCost )
			{
				findFareInputAdapter.InitialiseAsyncCallStateForRailSearchByPrice(refreshInterval, resourceFilename, resourceId);
			}
			else
			{
				findFareInputAdapter.InitialiseAsyncCallStateForFaresSearch(refreshInterval, resourceFilename, resourceId);
			}


			// Invalidate the current journey result.
			if (tdSessionManager.JourneyResult != null) 
			{ 
				tdSessionManager.JourneyResult.IsValid = false; 
			} 

			//Reset state data and search result.
			((FindCostBasedPageState)tdSessionManager.FindPageState).SearchResult = null; 			

			//Invoke the cost search runner.
			pageState.AmbiguityMode = (findFareInputAdapter.InvokeValidateAndRunFares() == AsyncCallStatus.ValidationError);

			//update the Ambiguity mode in the session to allow correct ambiguity handling in the FindFareInput page
			((FindCostBasedPageState)tdSessionManager.FindPageState).AmbiguityMode = pageState.AmbiguityMode;

			TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
			tdSessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareAmbiguityResolution;
		}

        /// <summary>
        /// Sets up the mode for the FindFareStepsConrol
        /// </summary>
        private void SetUpStepsControl()
        {
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep1;
            findFareStepsControl.SessionManager = tdSessionManager;
            findFareStepsControl.PageState = tdSessionManager.FindPageState;
        }

		#endregion Private Methods
	}
}
