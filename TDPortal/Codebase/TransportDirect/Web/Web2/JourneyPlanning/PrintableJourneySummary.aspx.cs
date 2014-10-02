// *********************************************** 
// NAME                 : JourneySummary.aspx.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 29/09/2003
// DESCRIPTION			: Printable Journey Summary page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneySummary.aspx.cs-arc  $
//
//   Rev 1.4   Jan 15 2009 14:20:24   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:44:32   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.1   Oct 13 2008 10:39:32   mmodi
//Updated page id for cycle mode
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Oct 07 2008 11:44:48   mmodi
//Updated to check for cycle mode
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5125: Cycle Planner - "Server Error" is displayed when user clicks on 'Printer Friendly' button on 'Journey Summary' page
//
//   Rev 1.2   Mar 31 2008 13:25:28   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 19 2008 18:00:00   mmodi
//Updated to populate results control with modetypes value for city to city journeys
//
//   Rev 1.0   Nov 08 2007 13:30:54   mturner
//Initial revision.
//
//   Rev 1.34   Jun 15 2006 10:29:06   esevern
//removed variables summaryOutward/ReturnTable controls and HeadElementControls added automatically by IDE
//
//   Rev 1.33   Jun 15 2006 10:11:10   esevern
//Code fix for vantive 3918704 - Buttons disabled when outward journey is not returned but a return journey is.  Changed printable pages to work for return only responses.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.32   Mar 14 2006 11:26:12   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.31   Feb 23 2006 18:21:20   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.30   Feb 10 2006 15:08:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.29.1.0   Dec 01 2005 11:58:54   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.29   Nov 18 2005 16:46:04   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.28   May 26 2005 11:10:46   rgeraghty
//Moved code for displaying a survey to Page_Load so it is not displayed if a timeout has occurred
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.27   Apr 06 2005 16:10:24   tmollart
//Removed following controls:
//labelOutwardJourneys
//labelOutwardJourneysFor
//labelOutwardBankHoliday
//labelReturnJourneys
//labelReturnJourneysFor
//labelReturnBankHoliday
//
//Added:
//findFareSelectedTicketLabelControl
//resultsTableTitleControlOutward
//resultsTableTitleControlReturn
//
//   Rev 1.26   Mar 01 2005 16:30:00   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.25   Oct 22 2004 15:29:20   jmorrissey
//Added javascript to open a user survey form on Page_Init - if the user survey form should be shown
//
//   Rev 1.24   Oct 13 2004 12:05:42   jmorrissey
//Updated Page_Init to add page Id to the return stack
//
//   Rev 1.23   Oct 08 2004 12:36:32   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.22   Sep 21 2004 16:38:36   esevern
//IR1581 - amended outward and return journey label for find a car (there will only be one journey)
//
//   Rev 1.21   Sep 20 2004 16:51:46   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.20   Aug 31 2004 15:06:50   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.19   Jul 23 2004 15:36:44   jgeorge
//Updates for Del 6.1 Find a...
//
//   Rev 1.18   Jun 23 2004 15:49:50   RHopkins
//Corrected Initialisation of Summary Tables to indicate that this is a printable page
//
//   Rev 1.17   Jun 16 2004 19:40:56   RHopkins
//Displays results in correct format as shown on normal screen.
//
//   Rev 1.16   Apr 28 2004 16:20:24   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.15   Mar 26 2004 16:20:16   AWindley
//DEL5.2 QA Resolution for 673
//
//   Rev 1.14   Feb 19 2004 10:00:36   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.13   Nov 21 2003 09:45:30   kcheung
//Updated for the return requested but no outward journey found case.
//
//   Rev 1.12   Nov 19 2003 12:46:58   kcheung
//Fixed so that exception does not occur if no results are found.
//
//   Rev 1.11   Nov 17 2003 17:14:12   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.10   Nov 07 2003 11:29:26   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.9   Nov 05 2003 10:28:00   kcheung
//Added : back to times as requested in QA
//
//   Rev 1.8   Nov 04 2003 13:54:04   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.7   Oct 31 2003 09:31:44   passuied
//Get username from commerceserver for printable pages
//
//   Rev 1.6   Oct 21 2003 15:00:30   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.5   Oct 10 2003 11:04:20   kcheung
//Updated titles on page to read from langstrings
//
//   Rev 1.4   Oct 06 2003 11:44:30   PNorell
//Updated to conform to new wireframes.
//Updated to exclude them from the ScreenFlow.
//
//   Rev 1.3   Oct 01 2003 12:57:12   kcheung
//Updated bank holiday test
//
//   Rev 1.2   Sep 30 2003 09:48:54   kcheung
//Added journey reference number label
//
//   Rev 1.1   Sep 29 2003 11:37:30   kcheung
//Working version

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
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable version of the Journey Summary page.
	/// </summary>
	public partial class PrintableJourneySummary : TDPrintablePage, INewWindowPage
	{
		protected SummaryResultTableControl summaryResultTableControlOutward;
		protected SummaryResultTableControl summaryResultTableControlReturn;
		protected ExtensionSummaryControl theExtensionSummaryControl;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected FindFareSelectedTicketLabelControl findFareSelectedTicketLabelControl;
		protected ResultsTableTitleControl resultsTableTitleControlOutward;
		protected ResultsTableTitleControl resultsTableTitleControlReturn;


		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists = false;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists = false;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists = false;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress = false;

		/// <summary>
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary = false;

		/// <summary>
		/// True if the results have been planned using FindA
		/// </summary>
		private bool showFindA = false;

		private bool returnArriveBefore = false;
		private bool outwardArriveBefore = false;


		#region Constructor, PageLoad

		/// <summary>
		/// Constructor.
		/// </summary>
		public PrintableJourneySummary() : base()
		{
            pageId = PageId.PrintableJourneySummary;
		}


		/// <summary>
		/// Displays a user survey
		/// </summary>
		private void ShowSurvey() 
		{
			//check if user survey should be displayed			
			bool showSurvey = UserSurveyHelper.ShowUserSurvey();			
			
			//if user survey should be displayed...
			if (showSurvey)
			{								
				//check if JavaScript is supported by the browser
				if (bool.Parse((string) Session[((TDPage)Page).Javascript_Support]) )
				{
					//add javascript block to this page that will open a user survey window when this page closes
					string javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];
					ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
					Page.ClientScript.RegisterClientScriptBlock(typeof(PrintableJourneySummary),"UserSurvey", scriptRepository.GetScript("UserSurvey", javaScriptDom));
				}				
			}
		}

		/// <summary>
		/// Page Load Method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//check if the User Survey form should be displayed
			ShowSurvey();

			DetermineStateOfResults();

			ITDSessionManager sessionManager = TDSessionManager.Current;

            // Override the PageId if we're in cycle mode
            if (sessionManager.FindAMode == FindAMode.Cycle)
            {
                pageId = PageId.PrintableCycleJourneySummary;
            }

			if (FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode))
			{
				findFareSelectedTicketLabelControl.PageState = sessionManager.FindPageState;

                panelFindFareSteps.Visible = true;
                findFareStepsControl.Printable = true;
                findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep3;
			}

			// Initialise static labels, hypertext text and image button Urls 
			// from Resourcing Mangager.

			// Initialise static labels text
			LabelFootnote.Text = Global.tdResourceManager.GetString(
				"JourneySummary.labelFootnote.Text", TDCultureInfo.CurrentUICulture);

			labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

			labelInstructions.Text = Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			#region Initialise controls

			if (showFindA)
			{
                // Get the modes to display on the results
                TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                if (TDSessionManager.Current.FindPageState != null)
                    modeTypes = TDSessionManager.Current.FindPageState.ModeType;

                if (outwardExists)
                {
                    findSummaryResultTableControlOutward.Initialise(true, true, outwardArriveBefore, modeTypes);
                }

                if (returnExists)
                {
                    findSummaryResultTableControlReturn.Initialise(true, false, returnArriveBefore, modeTypes);
                }
			}
			else 
			{
				if (outwardExists)
				{
						summaryResultTableControlOutward.Initialise(true, true, outwardArriveBefore);
				}
				
				if (returnExists)
				{
					summaryResultTableControlReturn.Initialise(true, false, returnArriveBefore);
				}
			}

			#endregion Initialise controls


			#region Set visibility of controls

			if ( itineraryExists )
			{
				if(extendInProgress)
				{
					if (outwardExists)
					{
						summaryResultTableControlOutward.Visible = !showFindA;
						findSummaryResultTableControlOutward.Visible = showFindA;
					}

					if(returnExists)
					{
						summaryResultTableControlReturn.Visible = !showFindA;
						findSummaryResultTableControlReturn.Visible = showFindA;
					}

					theExtensionSummaryControl.Visible = true;
				}
				else
				{
					summaryResultTableControlOutward.Visible = false;
					summaryResultTableControlReturn.Visible = false;
					findSummaryResultTableControlOutward.Visible = false;
					findSummaryResultTableControlReturn.Visible = false;
					theExtensionSummaryControl.Visible = false;
				}
			}
			else // ( itineraryExists )
			{					
				if (outwardExists)
				{
					summaryResultTableControlOutward.Visible = !showFindA;
					findSummaryResultTableControlOutward.Visible = showFindA;
				}

				if(returnExists)
				{
					summaryResultTableControlReturn.Visible = !showFindA;
					findSummaryResultTableControlReturn.Visible = showFindA;
				}

				theExtensionSummaryControl.Visible = false;
			}

			if (!outwardExists)
			{
				// Outward results DO NOT exist. Set visibility of all outward controls to false.
				SetOutwardVisible(false);
			}

			if (!returnExists)
			{
				// Return results DO NOT exist. Set visibility of all return controls to false.
				SetReturnVisible(false);
			}

			#endregion Set visibility of controls


			labelDate.Text = TDDateTime.Now.ToString("G");

			labelUsername.Visible = sessionManager.Authenticated;
			labelUsernameTitle.Visible = sessionManager.Authenticated;
			if( sessionManager.Authenticated )
			{
				labelUsername.Text = sessionManager.CurrentUser.Username;
			}

			if ( showItinerary )
			{
				labelReferenceNumberTitle.Visible = false;
			}
			else
			{
				// Set the journey reference number from the result
				labelReferenceNumberTitle.Visible = true;
                labelJourneyReferenceNumber.Text =
                    (sessionManager.FindAMode != FindAMode.Cycle) ?
                    sessionManager.JourneyResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat) :
                    sessionManager.CycleResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			}

		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);
			showFindA = (!showItinerary && (TDSessionManager.Current.IsFindAMode));

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
                //check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);

				//check for normal result
				ITDJourneyResult result = TDSessionManager.Current.JourneyResult;
				if(result != null) 
				{
                    outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists;
                    returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;

					// Get time types for journey.
					outwardArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		#endregion

		#region Method to set visibility of "Return" controls

		/// <summary>
		/// Sets the visibilities of the "Outward" components.
		/// </summary>
		/// <param name="visible">True if outward components should be visible
		/// and false if outward components should not be visible.</param>
		private void SetOutwardVisible(bool visible)
		{
			//resultsTableTitleControlOutward.Visible = visible;
			//summaryOutwardTable.Visible = visible;
			outwardPanel.Visible = visible;
			summaryResultTableControlOutward.Visible = visible;
			findSummaryResultTableControlOutward.Visible = visible;
		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		private void SetReturnVisible(bool visible)
		{
			returnPanel.Visible = visible;
			summaryResultTableControlReturn.Visible = visible;
			findSummaryResultTableControlReturn.Visible = visible;
		}

		#endregion

		#region OnPreRender method

		/// <summary>
		/// On PreRender method. Sets dynamic text and calls base OnPreRender.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
			plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);
			base.OnPreRender(e);
		}

		#endregion

		#region Method to determine if the outward/return date is a bank holiday

		/// <summary>
		/// Determines if the depature/arrival date for the selected journey is a holiday or not.
		/// </summary>
		/// <param name="outward">True if testing for selected outward journey,
		/// false to test selected return journey.</param>
		/// <returns>Bank holiday text to display.</returns>
		private string GetBankHolidayText(bool outward)
		{
			// Get the selected journey from TDSessionManager
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;

			if( result == null ||
				(result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount +
				 result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) < 1 ) 
			{
				// No results.
				return string.Empty;
			}

			TDDateTime departureDate;
			TDDateTime arrivalDate;
			JourneySummaryLine summaryLine;

			// Test to see if the departure and arrival date time is a holiday

			// Get the time from the original journey request.
			if(outward)
			{
				bool arriveBefore = viewState.JourneyLeavingTimeSearchType;
				int selectedJourney = viewState.SelectedOutwardJourney;

				// Get the outward journey summary line.
				if ( showItinerary )
				{
					departureDate = TDItineraryManager.Current.OutwardDepartDateTime();
					arrivalDate = TDItineraryManager.Current.OutwardArriveDateTime();
				}
				else
				{
					summaryLine =
						result.OutwardJourneySummary(arriveBefore)[selectedJourney];

					departureDate = summaryLine.DepartureDateTime;
					arrivalDate = summaryLine.ArrivalDateTime;
				}
			}
			else
			{
				bool arriveBefore = viewState.JourneyReturningTimeSearchType;
				int selectedJourney = viewState.SelectedReturnJourney;

				// Get the return journey summary line.
				if ( showItinerary )
				{
					departureDate = TDItineraryManager.Current.ReturnDepartDateTime();
					arrivalDate = TDItineraryManager.Current.ReturnArriveDateTime();
				}
				else
				{
					summaryLine =
						result.ReturnJourneySummary(arriveBefore)[selectedJourney];

					departureDate = summaryLine.DepartureDateTime;
					arrivalDate = summaryLine.ArrivalDateTime;
				}
			}

			// Need to create new dates because dataservices will fail to match
			// hours, minutes if they are not 0.

			TDDateTime departureDateToTest = new TDDateTime
				(departureDate.Year, departureDate.Month, departureDate.Day);

			TDDateTime arrivalDateToTest = new TDDateTime
				(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day);

			// Test the departure time first.
			string departureDateResult = TestDate(departureDateToTest);

			if(departureDateResult.Length == 0)
			{
				// Departure date is not a holiday, test the arrival date
				// and return the result
				return TestDate(arrivalDateToTest);
			}
			else
				// Departure date is a holiday, return
				return departureDateResult;
		}

		/// <summary>
		/// Tests the given date to see if it is a holiday. If it is, then the
		/// relevant string will be returned otherwise an empty string will be returned.
		/// </summary>
		/// <param name="dateToTest">Date to test for holiday.</param>
		/// <returns>String of the holiday text or empty string.</returns>
		private string TestDate(TDDateTime dateToTest)
		{
			// Data services
			DataServices.DataServices ds = (DataServices.DataServices) TDServiceDiscovery.Current[ ServiceDiscoveryKey.DataServices ];
			bool england = ds.IsHoliday( DataServiceType.BankHolidays, dateToTest, DataServiceCountries.EnglandWales );
			bool scotland = ds.IsHoliday( DataServiceType.BankHolidays, dateToTest, DataServiceCountries.Scotland );
			if( england && scotland )
			{ 
				return Global.tdResourceManager.GetString(
					"JourneyPlanner.MessageHolidayInUK", TDCultureInfo.CurrentUICulture);
			}
			else if( scotland )
			{
				return Global.tdResourceManager.GetString(
					"JourneyPlanner.MessageHolidayInScotland", TDCultureInfo.CurrentUICulture);
			}
			else if( england )
			{
				return Global.tdResourceManager.GetString(
					"JourneyPlanner.MessageHolidayInEnglandWales", TDCultureInfo.CurrentUICulture);
			}
			else
			{
				return String.Empty;
			}
		}


		#endregion

		#region Method to construct dynamic text for outward/return journeys

		/// <summary>
		/// Constructs the text string for the Outward Journeys label.
		/// </summary>
		/// <returns>Formatted string</returns>
		private string GetOutwardJourneyLabelText()
		{
			string forString = Global.tdResourceManager.GetString("JourneyPlanner.labelFor", TDCultureInfo.CurrentUICulture);
			string anyString = Global.tdResourceManager.GetString("JourneyPlanner.labelAnyTime", TDCultureInfo.CurrentUICulture);

			if ( showItinerary )
			{
				if (TDItineraryManager.Current.OutwardMultipleDates)
				{
					return String.Empty;
				}
				else
				{
					// construct the leaving time string
					TDDateTime outwardLeavingTime = TDItineraryManager.Current.OutwardDepartDateTime();
					string leavingTimeDate = outwardLeavingTime.ToString("ddd dd MMM yy");
					return String.Format("{0} {1}", forString, leavingTimeDate);
				}
			}
			else
			{
				if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest != null)
				{
					// Get the outward leaving time from TDSessionManager
					TDDateTime outwardLeavingTime = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0];

					// construct the leaving time string
					string leavingTimeDate = outwardLeavingTime.ToString("ddd dd MMM yy");
					string leavingTimeTime = outwardLeavingTime.ToString("HH:mm");

					string outwardSearchType = String.Empty;

					if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardAnyTime)
					{
						return String.Format("{0} {1} {2}", forString, leavingTimeDate, anyString);
					}
					else
					{
						bool arriveBefore = TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType;

						if(arriveBefore)
							outwardSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelArrivingBefore", TDCultureInfo.CurrentUICulture);
						else
							outwardSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelLeavingAfter", TDCultureInfo.CurrentUICulture);

						// Construct the final string and return
						return String.Format("{0} {1} {2} {3}", forString, leavingTimeDate, outwardSearchType, leavingTimeTime);
					}
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Constructs the text string for the Returning Journeys label. Assumes that
		/// a validation for the existence of a return journey has already been made.
		/// </summary>
		/// <returns>Formatted string</returns>
		private string GetReturnJourneyLabelText()
		{
			string forString = Global.tdResourceManager.GetString("JourneyPlanner.labelFor", TDCultureInfo.CurrentUICulture);
			string anyString = Global.tdResourceManager.GetString("JourneyPlanner.labelAnyTime", TDCultureInfo.CurrentUICulture);

			if ( showItinerary )
			{
				if (TDItineraryManager.Current.ReturnMultipleDates)
				{
					return String.Empty;
				}
				else
				{
					// construct the leaving time string
					TDDateTime returnLeavingTime = TDItineraryManager.Current.ReturnDepartDateTime();
					string leavingTimeDate = returnLeavingTime.ToString("ddd dd MMM yy");
					return String.Format("{0} {1}", forString, leavingTimeDate);
				}
			}
			else
			{
				if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest != null)
				{
					// Get the returning leaving time from TDSessionManager
					TDDateTime returningLeavingTime = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0];

					// construct the leaving time string
					string returningTimeDate = returningLeavingTime.ToString("ddd dd MMM yy");
					string returningTimeTime = returningLeavingTime.ToString("HH:mm");

					string returningSearchType = String.Empty;

					if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnAnyTime)
					{
						return String.Format("{0} {1} {2}", forString, returningTimeDate, anyString);
					}
					else
					{
						bool arriveBefore = TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType;

						if(arriveBefore)
							returningSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelArrivingBefore", TDCultureInfo.CurrentUICulture);
						else
							returningSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelLeavingAfter", TDCultureInfo.CurrentUICulture);

						// Construct the final string and return
						return String.Format("{0} {1} {2} {3}", forString, returningTimeDate, returningSearchType, returningTimeTime);
					}
				}
				else
				{
					return String.Empty;
				}
			}
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

		}
		#endregion
	}
}
