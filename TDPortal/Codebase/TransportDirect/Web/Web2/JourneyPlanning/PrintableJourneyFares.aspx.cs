// *********************************************** 
// NAME                 : Printable JourneyFares.aspx.cs
// AUTHOR               : SchlumbergerSema
// DATE CREATED         : 06/11/2003
// DESCRIPTION			: Printable Journey Fares page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyFares.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:25:24   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 19 2008 18:00:00   mmodi
//Updated to populate results control with modetypes value for city to city journeys
//
//   Rev 1.0   Nov 08 2007 13:30:50   mturner
//Initial revision.
//
//   Rev 1.41   Oct 06 2006 16:44:02   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.40.1.0   Oct 02 2006 16:17:34   mmodi
//Populate CarParkCosts control and set printable flag for it
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.39   Aug 08 2006 13:52:54   mmodi
//Amended to prevent Journey fare panel spanning across whole width of page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.38   Jun 15 2006 10:11:00   esevern
//Code fix for vantive 3918704 - Buttons disabled when outward journey is not returned but a return journey is.  Changed printable pages to work for return only responses.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.37   Mar 20 2006 17:59:08   RGriffith
//Fix to stop return car costs control always being displayed
//
//   Rev 1.36   Mar 14 2006 10:27:50   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.33.2.0   Mar 13 2006 16:08:16   tmollart
//Removed references to SummaryItineraryTableControl.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.35   Feb 23 2006 18:17:34   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.34   Feb 10 2006 15:08:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.33.1.0   Dec 01 2005 11:48:32   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.33   Nov 18 2005 16:45:04   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.32   Aug 04 2005 13:09:26   asinclair
//fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.31   Aug 03 2005 21:43:18   asinclair
//Check in for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.30   May 26 2005 11:10:32   rgeraghty
//Moved code for displaying a survey to Page_Load so it is not displayed if a timeout has occurred
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.29   May 20 2005 11:15:44   tmollart
//Modified DisplayCarCosts method so that changes for IR2300 are taken into account. Specifically the total control required modification so that pence are displayed at the correct time on the total.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.28   May 10 2005 17:01:42   rgreenwood
//IR 2300: amended calculation of toal cost figure
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.27   Apr 27 2005 10:20:36   COwczarek
//Fix compiler warnings
//
//   Rev 1.26   Apr 25 2005 17:05:12   jgeorge
//Updates to make car cost controls work correctly with extended journey
//Resolution for 2297: Del 7 - Car Costing - Zero cost for car journey which is part of multi-mode extended journey
//
//   Rev 1.25   Apr 25 2005 14:08:38   esevern
//interim checkin for handover - added check for return journey existing when setting running costs values/total car cost values
//
//   Rev 1.24   Apr 25 2005 10:20:58   esevern
//corrected call to viewstate.showrunning
//
//   Rev 1.23   Apr 24 2005 17:41:34   rgeraghty
//DisplayFares, DisplayCarCosts updated for full itinerary checks
//Resolution for 2295: Del 7 - PT - Server error selecting full extended journey from tickets/costs page
//
//   Rev 1.22   Apr 19 2005 19:59:18   esevern
//changed call to itemisedcostcontrol displayrunningcosts method
//
//   Rev 1.21   Apr 18 2005 14:52:52   rgeraghty
//Updated to allow simultaneous display of public and private costing controls
//Resolution for 2122: Unable to view tickets/costs for car and public transport at same time
//
//   Rev 1.20   Mar 31 2005 11:00:16   rgeraghty
//Removed unused variable and update to options
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.19   Mar 23 2005 16:19:20   esevern
//added CarCostsJourneyControl - shows breakdown of car costs
//
//   Rev 1.18   Mar 03 2005 18:04:00   rgeraghty
//FxCop code changes
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.17   Mar 01 2005 16:29:44   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.16   Mar 01 2005 15:10:30   rgeraghty
//Changes made for DEL7 journey fares
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.15   Oct 22 2004 15:29:20   jmorrissey
//Added javascript to open a user survey form on Page_Init - if the user survey form should be shown
//
//   Rev 1.14   Oct 13 2004 12:05:40   jmorrissey
//Updated Page_Init to add page Id to the return stack
//
//   Rev 1.13   Oct 08 2004 12:36:32   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.12   Sep 20 2004 16:51:44   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.11   Aug 31 2004 15:06:48   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.10   Jul 23 2004 16:16:16   jgeorge
//Updated for Del 6.1 Find a...
//
//   Rev 1.9   Jul 01 2004 16:16:00   RHopkins
//Corrected handling of Fares for Extended Journeys
//
//   Rev 1.8   Jun 21 2004 16:30:40   esevern
//added extend journey
//
//   Rev 1.7   Apr 28 2004 16:20:20   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.6   Feb 19 2004 09:59:52   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.5   Nov 27 2003 16:30:16   CHosegood
//Retailers and Fares pages now have identical behaviour for single, matching & non-matching returns.
//Resolution for 307: Return retailers control appears when single journey selected
//
//   Rev 1.4   Nov 17 2003 17:05:16   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.3   Nov 06 2003 17:59:30   CHosegood
//Inital working version

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
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable version of the journey fares page.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableJourneyFares : TDPrintablePage, INewWindowPage
	{
		#region instance variables




		protected JourneysSearchedForControl JourneysSearchedForControl1;
		protected SummaryResultTableControl SummaryResultTableControlOutward;
		protected SummaryResultTableControl SummaryResultTableControlReturn;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected ExtensionSummaryControl theExtensionSummaryControl;
		
		protected JourneyFaresControl OutboundJourneyFaresControl;
		protected JourneyFaresControl ReturnJourneyFaresControl;

		protected CarJourneyCostsControl outwardCarJourneyCostsControl;
		protected CarJourneyCostsControl returnCarJourneyCostsControl;

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

		/// <summary>
		/// True if the results have been planned using FindA
		/// </summary>
		private bool showFindA;

		/// <summary>
		/// (True = Arrive Before, False =  Leave After)
		/// </summary>
		private bool returnArriveBefore;
				
		/// <summary>
		/// (True = Arrive Before, False =  Leave After)
		/// </summary>
		private bool outwardArriveBefore;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public PrintableJourneyFares() : base()
		{
			pageId = PageId.PrintableJourneyFares;
		}
		#endregion

		#region Protected methods

		/// <summary>
		/// On PreRender method. Sets dynamic text and calls base OnPreRender.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
           			
			
			
			if(outwardExists)
			{
				labelOutwardJourneysFor.Text = GetOutwardJourneyLabelText();
				labelOutwardBankHoliday.Text = GetBankHolidayText(true);
			}

			if(returnExists)
			{
				labelReturnJourneysFor.Text = GetReturnJourneyLabelText();
				labelReturnBankHoliday.Text = GetBankHolidayText(false);
			}
			
			DisplayCarCosts();
				
			base.OnPreRender(e);

		}

		#endregion

		#region Private Methods

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
					Page.ClientScript.RegisterClientScriptBlock(typeof(PrintableJourneyFares),"UserSurvey", scriptRepository.GetScript("UserSurvey", javaScriptDom));
					
				}				
			}
		}

		/// <summary>
		/// Sets the visibilities of the "Outward" components.
		/// </summary>
		/// <param name="visible">True if outward components should be visible
		/// and false if outward components should not be visible.</param>
		private void SetOutwardVisible(bool visible)
		{
			outwardPanel.Visible = visible;
			labelOutwardJourneys.Visible = visible;
			SummaryResultTableControlOutward.Visible = visible;
			labelOutwardBankHoliday.Visible = visible;
		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		private void SetReturnVisible(bool visible)
		{
			returnPanel.Visible = visible;
			labelReturnJourneys.Visible = visible;
			SummaryResultTableControlReturn.Visible = visible;
			labelReturnBankHoliday.Visible = visible;
			literalNewPage.Visible = visible;
		}

		/// <summary>
		/// Determines if the depature/arrival date for the selected journey is a holiday or not.
		/// </summary>
		/// <param name="outward">True if testing for selected outward journey,
		/// false to test selected return journey.</param>
		/// <returns>Bank holiday text to display.</returns>
		private string GetBankHolidayText(bool outward)
		{
	
			// Get the selected journey from ItineraryManager
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;

			TDDateTime departureDate;
			TDDateTime arrivalDate;
			JourneySummaryLine summaryLine;

			if( result == null || (result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount +
				result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) < 1 ) 
			{
				// No results.
				return string.Empty;
			}

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
				return GetResource("JourneyPlanner.MessageHolidayInUK");
			}
			else if( scotland )
			{
				return GetResource( "JourneyPlanner.MessageHolidayInScotland");
			}
			else if( england )
			{
				return GetResource("JourneyPlanner.MessageHolidayInEnglandWales");
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
			string forString = GetResource("JourneyPlanner.labelFor");
			string anyString = GetResource("JourneyPlanner.labelAnyTime");
			
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
							outwardSearchType = GetResource(
								"JourneyPlanner.labelArrivingBefore");
						else
							outwardSearchType = GetResource(
								"JourneyPlanner.labelLeavingAfter");

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
			string forString = GetResource("JourneyPlanner.labelFor");
			string anyString = GetResource("JourneyPlanner.labelAnyTime");

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
						bool arriveBefore =
							TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType;

						if(arriveBefore)
							returningSearchType = GetResource("JourneyPlanner.labelArrivingBefore");
						else
							returningSearchType = GetResource("JourneyPlanner.labelLeavingAfter");

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
		/// Sets visibility of car costing outward and return controls, and 
		/// the outward and return road journey(s) allowing car costs to be 
		/// displayed 
		/// </summary>
		private void DisplayCarCosts()
		{
			// Determine whether or not to show the outward control, and if so, set it up
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			
			bool outwardIsRoad = (viewState != null) && 
				(viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested);
			bool returnIsRoad = (viewState != null) && 
				(viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested);

			if (outwardIsRoad)
			{
				outwardJourneyFaresPanel.Visible = false;
				outwardCarJourneyCostsControl.Visible = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.OtherCostsControl.IsOutward = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.IsOutward = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.IsOutward = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = TDItineraryManager.Current.JourneyViewState.ShowRunning;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.ReturnExists = returnIsRoad;

				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarRoadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.OtherCostsControl.CarRoadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.CarRoadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();

				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.Printable = true;

				outwardCarJourneyCostsControl.TotalCarCostsControl.Visible = false;

				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();
			}
			else
			{
				outwardJourneyFaresPanel.Visible = true;
				outwardCarJourneyCostsControl.Visible = false;
			}

			if ((returnIsRoad) && (returnExists))
			{
				returnJourneyFaresPanel.Visible = false;
				returnCarJourneyCostsControl.Visible = true;
				returnCarJourneyCostsControl.ItemisedCarCostsControl.IsOutward = false;
				returnCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = TDItineraryManager.Current.JourneyViewState.ShowRunning;
				returnCarJourneyCostsControl.ItemisedCarCostsControl.ReturnExists = true;			
				
				returnCarJourneyCostsControl.TotalCarCostsControl.Visible = outwardIsRoad;
				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CarRoadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.OtherCostsControl.CarRoadJourney =	TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.CarRoadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();

				returnCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.Printable = true;
			
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();

				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();
				
				if (returnCarJourneyCostsControl.TotalCarCostsControl.Visible)
				{

					decimal cost = outwardCarJourneyCostsControl.ItemisedCarCostsControl.TotalCost + 
						returnCarJourneyCostsControl.ItemisedCarCostsControl.TotalCost;

					//If either of the itemised car cost controls is set to display a decimalised total
					//then the overall total must be decimalised as well. Otherwise it must be 
					//rounded to the nearest whole number.
					if (outwardCarJourneyCostsControl.ItemisedCarCostsControl.DecimaliseTotalCost ||
						returnCarJourneyCostsControl.ItemisedCarCostsControl.DecimaliseTotalCost)
					{	
						//If we meed to show a decimalised total cost then (not sure if this is the
						//best way to do it but) get the substring of the total price for both the
						//pounds and the pence.
						string temp = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", cost);

						//Perform quick safety check as an empty string will cause an excpetion.
						if (temp.Length > 0)
						{
							returnCarJourneyCostsControl.TotalCarCostsControl.PoundsLabel.Text = temp.Substring(0,temp.Length - 3);
							returnCarJourneyCostsControl.TotalCarCostsControl.PenceLabel.Text = temp.Substring(temp.Length - 3,3);
						}
					}
					else
					{
						returnCarJourneyCostsControl.TotalCarCostsControl.PoundsLabel.Text = 
							TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol + Decimal.Round(cost, 0);
					}
				}
			}
			else 
			{
				returnJourneyFaresPanel.Visible = true;
				returnCarJourneyCostsControl.Visible = false;				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.ReturnExists = false;
			}

		}

		/// <summary>
		/// Creates a new ItineraryAdapter for the current ininerary and is
		/// responsible for initialising the outward and return fares controllers
		/// </summary>
		private void DisplayFares() 
		{
			// Determine whether or not to show the outward control, and if so, set it up
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			
			bool outwardIsPublic = (viewState != null) &&
				((viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal) || (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended));
			bool returnIsPublic = (viewState != null) && 
				((viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal) || (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended));

			if (outwardIsPublic || returnIsPublic)
			{
				PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

				ItineraryAdapter adapter = new ItineraryAdapter(options.JourneyItinerary);
				adapter.OverrideItineraryType = options.OverrideItineraryType;

				if (outwardIsPublic)
				{
					//Initialise the OutboundJourneyFaresControl
					OutboundJourneyFaresControl.Visible = true;
					OutboundJourneyFaresControl.ItineraryAdapter = adapter;			
					OutboundJourneyFaresControl.ShowChildFares = options.ShowChildFares;
					OutboundJourneyFaresControl.PrinterFriendly = true;
					OutboundJourneyFaresControl.InTableMode = options.ShowOutboundFaresInTableFormat;			
					OutboundJourneyFaresControl.IsForReturn = false;
					OutboundJourneyFaresControl.FullItinerarySelected= TDItineraryManager.Current.FullItinerarySelected;
					if (options.Discounts != null)
					{
						OutboundJourneyFaresControl.RailDiscount = options.Discounts.RailDiscount;
						OutboundJourneyFaresControl.CoachDiscount = options.Discounts.CoachDiscount;
					}
				}
				else
					OutboundJourneyFaresControl.Visible = false;

				if (returnIsPublic)
				{
					//Initialise the ReturnJourneyFaresControl 
					ReturnJourneyFaresControl.Visible = true;
					ReturnJourneyFaresControl.ItineraryAdapter = adapter;
					ReturnJourneyFaresControl.ShowChildFares = options.ShowChildFares;
					ReturnJourneyFaresControl.PrinterFriendly = true;
					ReturnJourneyFaresControl.InTableMode = options.ShowReturnFaresInTableFormat;			
					ReturnJourneyFaresControl.IsForReturn = true;		
					ReturnJourneyFaresControl.FullItinerarySelected= TDItineraryManager.Current.FullItinerarySelected;
					if (options.Discounts != null)
					{
						ReturnJourneyFaresControl.RailDiscount = options.Discounts.RailDiscount;
						ReturnJourneyFaresControl.CoachDiscount = options.Discounts.CoachDiscount;
					}
				}
				else
					ReturnJourneyFaresControl.Visible = false;

			}
			else
			{
				OutboundJourneyFaresControl.Visible = false;
				ReturnJourneyFaresControl.Visible = false;
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

		#region Event Handlers

		/// <summary>
		/// Load Event handler
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//check if the User Survey form should be displayed
			ShowSurvey();

			DetermineStateOfResults();

			ITDSessionManager sessionManager = TDSessionManager.Current;

			sessionManager.JourneyViewState.CongestionCostAdded = false;
			sessionManager.JourneyViewState.CongestionChargeAdded = false;

			// Initialise static labels, hypertext text and image button Urls 
			labelOutwardJourneys.Text = GetResource("JourneyPlanner.labelOutwardJourneys.Text");

			labelReturnJourneys.Text = GetResource("JourneyPlanner.labelReturnJourneys.Text");

			labelPrinterFriendly.Text= GetResource("StaticPrinterFriendly.labelPrinterFriendly");

			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");

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
					SummaryResultTableControlOutward.Initialise(true, true, outwardArriveBefore);
				}
				if (returnExists)
				{
					SummaryResultTableControlReturn.Initialise(true, false, returnArriveBefore);
				}
			}

			#endregion Initialise controls

			#region Set visibility of controls

			if ( itineraryExists )
			{
				if(extendInProgress)
				{
					labelOutwardJourneys.Text = GetResource(
						"JourneyPlanner.labelOutwardJourneys.ExtensionText");

					SummaryResultTableControlOutward.Visible = !showFindA;
					findSummaryResultTableControlOutward.Visible = showFindA;

					if(returnExists)
					{
						SummaryResultTableControlReturn.Visible = !showFindA;
						findSummaryResultTableControlReturn.Visible = showFindA;

						labelReturnJourneys.Text = GetResource(
							"JourneyPlanner.labelReturnJourneys.ExtensionText");
					}

					theExtensionSummaryControl.Visible = true;
				}
				else
				{
					if (outwardExists)
					{
						labelOutwardJourneys.Text = GetResource(
							"JourneyPlanner.labelOutwardJourneys.ItineraryText");
					}
					
					SummaryResultTableControlOutward.Visible = false;
					SummaryResultTableControlReturn.Visible = false;
					findSummaryResultTableControlOutward.Visible = false;
					findSummaryResultTableControlReturn.Visible = false;

					if(returnExists)
					{
						labelReturnJourneys.Text = GetResource(
							"JourneyPlanner.labelReturnJourneys.ItineraryText");
					}

					theExtensionSummaryControl.Visible = false;
				}
			}
			else 
			{
				if (outwardExists)
				{
					labelOutwardJourneys.Text = GetResource(
						"JourneyPlanner.labelOutwardJourneys.Text");

					SummaryResultTableControlOutward.Visible = !showFindA;
					findSummaryResultTableControlOutward.Visible = showFindA;
				}
				if(returnExists)
				{
					SummaryResultTableControlReturn.Visible = !showFindA;
					findSummaryResultTableControlReturn.Visible = showFindA;

					labelReturnJourneys.Text = GetResource(
						"JourneyPlanner.labelReturnJourneys.Text");
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
					sessionManager.JourneyResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			}

			//display journey fares controls
			DisplayFares();
			DisplayCarCosts();					
		}
		#endregion
	}
}
