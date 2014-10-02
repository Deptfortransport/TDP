// *********************************************** 
// NAME                 : PrintableJourneyMap.aspx.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 29/09/2003
// DESCRIPTION			: The printable journey maps
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyMap.aspx.cs-arc  $
//
//   Rev 1.7   Feb 02 2010 16:37:56   pghumra
//Fixed multiple page refresh issues for printer friendly maps.
//Resolution for 5395: CODE FIX - INITIAL - DEL 10.x - Del 10.9.1 Bug printer friendly
//
//   Rev 1.6   Jan 18 2010 12:15:52   mmodi
//Add an auto refresh page if map image url not detected in session
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.5   Nov 20 2009 09:28:26   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Jan 15 2009 11:09:20   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:44:32   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 10 2008 10:28:42   jfrank
//Removed extra page entry event logging from printable journey details and printable journey maps pages.
//Resolution for 5107: Printable Details and Map page Logging
//
//   Rev 1.3   Sep 10 2008 10:22:28   jfrank
//The printable journey details and journey maps pages were logging to many page entry events
//Resolution for 5107: Printable Details and Map page Logging
//
//   Rev 1.2   Mar 31 2008 13:25:26   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 19 2008 18:00:00   mmodi
//Updated to populate results control with modetypes value for city to city journeys
//
//   Rev 1.0   Nov 08 2007 13:30:52   mturner
//Initial revision.
//
//   Rev 1.44   Jun 15 2006 10:21:58   esevern
//removed HeadElementControl added automatically by IDE
//
//   Rev 1.43   Jun 15 2006 10:11:06   esevern
//Code fix for vantive 3918704 - Buttons disabled when outward journey is not returned but a return journey is.  Changed printable pages to work for return only responses.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.42   Mar 14 2006 10:28:10   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.39.2.0   Mar 13 2006 16:08:16   tmollart
//Removed references to SummaryItineraryTableControl.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.41   Feb 23 2006 18:18:58   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.40   Feb 10 2006 15:08:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.39.1.1   Dec 12 2005 17:02:50   tmollart
//Removed code to reinstate journey results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.39.1.0   Dec 01 2005 11:48:30   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.39   Nov 18 2005 16:45:24   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.38   Aug 04 2005 13:10:06   asinclair
//Fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.37   May 26 2005 11:10:38   rgeraghty
//Moved code for displaying a survey to Page_Load so it is not displayed if a timeout has occurred
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.36   Apr 14 2005 14:44:24   rgreenwood
//IR2103: Fixed Ticket Name & Price and Journey Type display issue
//Resolution for 2103: Find A Fare, Printable Results: Ticket Name, Price & Table Heading
//
//   Rev 1.35   Apr 13 2005 14:55:56   asinclair
//Added fix for 2044
//
//   Rev 1.34   Apr 08 2005 16:04:02   rhopkins
//Change PageId for PrintableJourneyMap so that it is compatible with the PrinterFriendlyPageButtonControl
//
//   Rev 1.33   Mar 01 2005 16:29:52   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.32   Oct 22 2004 15:29:20   jmorrissey
//Added javascript to open a user survey form on Page_Init - if the user survey form should be shown
//
//   Rev 1.31   Oct 13 2004 12:05:40   jmorrissey
//Updated Page_Init to add page Id to the return stack
//
//   Rev 1.30   Oct 08 2004 12:36:32   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.29   Sep 27 2004 15:37:12   esevern
//added call to ReinstateParametersForResults on pageload
//
//   Rev 1.28   Sep 21 2004 16:38:34   esevern
//IR1581 - amended outward and return journey label for find a car (there will only be one journey)
//
//   Rev 1.27   Sep 20 2004 16:51:44   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.26   Sep 17 2004 15:23:44   esevern
//obtain value for find a mode from TDSessionManager when populating outward mapcontrol
//
//   Rev 1.25   Sep 07 2004 14:15:18   CHosegood
//Now uses TDSessionManager.Current.IsFindAMode to find out if it's a FindA for the populate method of the map control.
//Resolution for 1437: Printable trunk maps show all modes in key
//
//   Rev 1.24   Aug 31 2004 15:06:50   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.23   Jul 23 2004 15:56:48   jgeorge
//Updates for Del 6.1 Find a...
//
//   Rev 1.22   Jun 22 2004 16:49:10   jbroome
//Ensured correct journey label text displayed above summary tables.
//
//   Rev 1.21   Jun 22 2004 12:20:20   JHaydock
//FindMap page done. Corrections to printable map controls and pages. Various updates to Find pages.
//
//   Rev 1.20   Jun 17 2004 17:26:08   jbroome
//Updated retrieval of JourneyViewState and Journey Result.
//
//   Rev 1.19   Jun 09 2004 15:41:36   RPhilpott
//Add explicit support for FindA options
//
//   Rev 1.18   Jun 07 2004 15:34:44   jbroome
//ExtendJourney - added support for TDItineraryManager.
//
//   Rev 1.17   Apr 28 2004 16:20:22   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.16   Feb 19 2004 10:00:16   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.15   Nov 21 2003 10:08:16   kcheung
//Added support for no outward result returned case.
//
//   Rev 1.14   Nov 17 2003 17:07:34   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.13   Nov 05 2003 15:16:12   kcheung
//Updated commenting
//
//   Rev 1.12   Nov 05 2003 10:46:06   kcheung
//Inserted : as requested in QA
//
//   Rev 1.11   Oct 31 2003 09:31:40   passuied
//Get username from commerceserver for printable pages
//
//   Rev 1.10   Oct 22 2003 12:55:02   passuied
//hidden username if not authenticated
//
//   Rev 1.9   Oct 13 2003 16:52:24   passuied
//implemented page breaks in output pages
//
//   Rev 1.8   Oct 10 2003 11:04:18   kcheung
//Updated titles on page to read from langstrings
//
//   Rev 1.7   Oct 06 2003 11:44:28   PNorell
//Updated to conform to new wireframes.
//Updated to exclude them from the ScreenFlow.
//
//   Rev 1.6   Oct 03 2003 14:52:46   PNorell
//Added file headers.
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
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printer friendly journey map page.
	/// </summary>
	public partial class PrintableJourneyMap : TDPrintablePage, INewWindowPage
	{

		protected PrintableMapControl mapOutward;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected PrintableMapControl mapReturn;
		protected SummaryResultTableControl summaryResultTableControlOutward;
		protected SummaryResultTableControl summaryResultTableControlReturn;
       	protected ExtensionSummaryControl theExtensionSummaryControl;
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


		/// <summary>
		/// Constructor - sets the Page Id.
		/// </summary>
		public PrintableJourneyMap()
		{
			pageId = PageId.PrintableJourneyMap;
		}

		/// <summary>
		/// OnPreRender method - overrides base and constructs the dynamic
		/// controls. Calls base OnPreRender as the final step.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{

			// Construct the dynamic labels 			
			if(outwardExists)
			{
				PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
				plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);
				base.OnPreRender(e);

			}

			if(returnExists)
			{
				PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
				plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);
				base.OnPreRender(e);

			}

            if (!outwardExists && !returnExists)
            {
                base.OnPreRender(e);
            }
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
					Page.ClientScript.RegisterClientScriptBlock(typeof(PrintableJourneyMap),"UserSurvey", scriptRepository.GetScript("UserSurvey", javaScriptDom));
				}				
			}
		}

		/// <summary>
		/// Page Load Method. Sets-up the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//check if the User Survey form should be displayed
			ShowSurvey();
  
			ITDSessionManager sessionManager = TDSessionManager.Current;

			sessionManager.JourneyViewState.CongestionChargeAdded = false;

			DetermineStateOfResults();

			//DEL 7 - Sets the Units for Road jounreys on the Printable page
			string UrlQueryString = string.Empty;
			//The Query params is set using javascript on the non-printable page
			UrlQueryString = Request.Params["units"];
			if (UrlQueryString =="kms")
			{
				mapOutward.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Kms;
				mapReturn.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Kms;
			}
			else
			{
				mapOutward.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Miles;
				mapReturn.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Miles;
			}

			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			//IR2103 - set text on findFareSelectedTicketLabelControl
			if (FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode))
			{
				findFareSelectedTicketLabelControl.PageState = sessionManager.FindPageState;
			}

			labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

			labelInstructions.Text = Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			TDJourneyViewState viewState = itineraryManager.JourneyViewState;
			ITDJourneyResult result = itineraryManager.JourneyResult;

			// Map outward is visible only if outward results exist.
			if (result != null) 
			{
				labelReference.Text = result.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			}

			theExtensionSummaryControl.Visible = itineraryManager.ExtendInProgress;

            if (outwardExists)
			{
				panelMapOutward.Visible = true;
                
				if (showItinerary)
				{
					summaryResultTableControlOutward.Visible = false;
				}
				else
				{
					summaryResultTableControlOutward.Visible = !showFindA;
					findSummaryResultTableControlOutward.Visible = showFindA;

                    if (showFindA)
                    {
                        // Get the modes to display on the results
                        TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                        if (TDSessionManager.Current.FindPageState != null)
                            modeTypes = TDSessionManager.Current.FindPageState.ModeType;

                        findSummaryResultTableControlOutward.Initialise(true, true, viewState.JourneyLeavingTimeSearchType, modeTypes);
                    }
                    else
                        summaryResultTableControlOutward.Initialise(true, true, viewState.JourneyLeavingTimeSearchType);

				}

				theExtensionSummaryControl.Visible = itineraryManager.ExtendInProgress;

				mapOutward.Populate(true, false, TDSessionManager.Current.IsFindAMode );

				if (returnExists)
				{
					panelMapReturn.Visible = true;
					literalNewPage.Visible = true;

                    if (showItinerary)
					{
						summaryResultTableControlReturn.Visible = false;
					}
					else
					{
						summaryResultTableControlReturn.Visible = !showFindA;
						findSummaryResultTableControlReturn.Visible = showFindA;

                        if (showFindA)
                        {
                            // Get the modes to display on the results
                            TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                            if (TDSessionManager.Current.FindPageState != null)
                                modeTypes = TDSessionManager.Current.FindPageState.ModeType;

                            findSummaryResultTableControlReturn.Initialise(true, false, viewState.JourneyReturningTimeSearchType, modeTypes);
                        }
                        else
                            summaryResultTableControlReturn.Initialise(true, false, viewState.JourneyReturningTimeSearchType);

					}

					mapReturn.Populate(false, false, TDSessionManager.Current.IsFindAMode ); //false
				}

				labelReferenceTitle.Visible = (result != null);
				labelReference.Visible = (result != null);

				labelDateTime.Text = TDDateTime.Now.ToString("G");

				labelUsernameTitle.Visible = sessionManager.Authenticated;
				labelUsername.Visible = sessionManager.Authenticated;
				if( sessionManager.Authenticated )
				{
					labelUsername.Text = sessionManager.CurrentUser.Username;
                }

            }

            SetupStepsControl();
		}

        /// <summary>
        /// Page Pre Render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            mapReturn.Visible = this.IsJavascriptEnabled && (returnExists && ((viewState.ReturnMapSelected) || (viewState.ReturnShowMap)));
            mapOutward.Visible = mapOutward.Visible && !mapReturn.Visible;
            
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
				//check for normal result
				ITDJourneyResult result = TDSessionManager.Current.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;

					// Get time types for journey.
					outwardArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		/// <summary>
		/// Determines if the depature/arrival date for the selected journey is a holiday or not.
		/// </summary>
		/// <param name="outward">True if testing for selected outward journey,
		/// false to test selected return journey.</param>
		/// <returns>Bank holiday text to display.</returns>
		private string GetBankHolidayText(bool outward)
		{
			// Get the selected journey
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

			// Test to see if the departure and arrival date time is a holiday

			// Get the time from the original journey request.
			if(outward)
			{
				bool arriveBefore = viewState.JourneyLeavingTimeSearchType;
				int selectedJourney = viewState.SelectedOutwardJourney;

				// Get the outward journey summary line.
				JourneySummaryLine summaryLine =
					result.OutwardJourneySummary(arriveBefore)[selectedJourney];

				departureDate = summaryLine.DepartureDateTime;
				arrivalDate = summaryLine.ArrivalDateTime;
			}
			else
			{
				bool arriveBefore = viewState.JourneyReturningTimeSearchType;
				int selectedJourney = viewState.SelectedReturnJourney;

				// Get the return journey summary line.
				JourneySummaryLine summaryLine =
					result.ReturnJourneySummary(arriveBefore)[selectedJourney];

				departureDate = summaryLine.DepartureDateTime;
				arrivalDate = summaryLine.ArrivalDateTime;
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

		/// <summary>
		/// Constructs the text string for the Outward Journeys label.
		/// </summary>
		/// <returns>Formatted string</returns>
		private string GetOutwardJourneyLabelText()
		{
			bool showItinerary = ((TDItineraryManager.Current.Length > 0) && (!TDItineraryManager.Current.ExtendInProgress));

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
					TDDateTime outwardLeavingTime =
						TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0];

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
						bool arriveBefore =
							TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType;

						if(arriveBefore)
							outwardSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelArrivingBefore", TDCultureInfo.CurrentUICulture);
						else
							outwardSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelLeavingAfter", TDCultureInfo.CurrentUICulture);

						// Construct the final string and return
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
			bool showItinerary = ((TDItineraryManager.Current.Length > 0) && (!TDItineraryManager.Current.ExtendInProgress));
	
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
					TDDateTime returningLeavingTime =
						TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0];

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

        /// <summary>
        /// Populates the Fare steps control
        /// </summary>
        private void SetupStepsControl()
        {
            if (FindInputAdapter.IsCostBasedSearchMode(TDSessionManager.Current.FindAMode))
            {
                panelFindFareSteps.Visible = true;
                findFareStepsControl.Printable = true;
                findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep3;
            }
        }

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
