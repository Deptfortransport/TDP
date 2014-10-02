// *********************************************** 
// NAME                 : PrintableJourneyDetails.aspx.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 29/09/2003
// DESCRIPTION			: Printable Journey Details page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyDetails.aspx.cs-arc  $
//
//   Rev 1.14   Sep 27 2011 15:50:10   mmodi
//Fixed error when there is no RoadJourney in the session when displaying printer friendly page for non-car journey result
//Resolution for 5745: Real Time in Car - Printer friendly page details error for a non car journey
//
//   Rev 1.13   Sep 21 2011 09:53:30   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.12   Sep 01 2011 10:44:52   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.11   Feb 24 2010 13:23:46   mmodi
//Set units value for emissions control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 24 2010 10:33:50   mmodi
//Display printable Emissions control if needed
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 16 2010 11:16:14   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 12 2010 11:14:06   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 02 2010 16:37:58   pghumra
//Fixed multiple page refresh issues for printer friendly maps.
//Resolution for 5395: CODE FIX - INITIAL - DEL 10.x - Del 10.9.1 Bug printer friendly
//
//   Rev 1.6   Jan 18 2010 12:15:18   mmodi
//Add an auto refresh page if map image url not detected in session
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.5   Nov 11 2009 21:12:04   mmodi
//Updated for new mapping changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 11 2009 16:43:16   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Oct 13 2008 16:44:30   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 10 2008 10:29:36   jfrank
//removed extra page entry event logging from journey details and journey maps pages.
//Resolution for 5107: Printable Details and Map page Logging
//
//   Rev 1.3   Sep 10 2008 10:22:26   jfrank
//The printable journey details and journey maps pages were logging to many page entry events
//Resolution for 5107: Printable Details and Map page Logging
//
//   Rev 1.2   Mar 31 2008 13:25:20   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 19 2008 18:00:00   mmodi
//Updated to populate results control with modetypes value for city to city journeys
//
//   Rev 1.0   Nov 08 2007 13:30:44   mturner
//Initial revision.
//
//   Rev 1.58   Jun 28 2007 16:28:40   mmodi
//Added code to populate the Car details control with journey parameters
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.57   Jun 15 2006 10:10:56   esevern
//Code fix for vantive 3918704 - Buttons disabled when outward journey is not returned but a return journey is.  Changed printable pages to work for return only responses.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.56   Mar 14 2006 10:27:48   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.52.2.0   Mar 13 2006 16:08:14   tmollart
//Removed references to SummaryItineraryTableControl.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55   Mar 09 2006 13:07:24   MTurner
//Fix for Vantive 3714531 (IR 3217) - Printer Friendly Page missing details for a journey extended by car.
//
//   Rev 1.54   Feb 23 2006 19:01:10   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.53   Feb 10 2006 15:08:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.52.1.0   Dec 01 2005 11:48:34   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.52   Nov 18 2005 16:44:42   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.51   Aug 19 2005 13:31:02   RWilby
//Merge for stream2572
//
//   Rev 1.50   Aug 03 2005 21:08:20   asinclair
//fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.49   Jul 29 2005 09:54:46   RPhilpott
//Correct erroneous checkin (again!).
//
//   Rev 1.45   May 26 2005 11:10:24   rgeraghty
//Moved code for displaying a survey to Page_Load so it is not displayed if a timeout has occurred
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.44   Apr 24 2005 12:25:08   rgeraghty
//Updated initialiseJourneyComponents to prevent error when full itinerary selected
//Resolution for 2257: Del 7 - Car Costing - Server error when accessing printable page for extended journey details
//
//   Rev 1.43   Apr 13 2005 17:18:50   rgreenwood
//IR2103: Removed dynamic labels and replaced with resultstabletitlecontrol and findFareSelectedTicketLabelControl
//Resolution for 2103: Find A Fare, Printable Results: Ticket Name, Price & Table Heading
//
//   Rev 1.41   Apr 06 2005 11:45:58   asinclair
//Removed code not needed
//
//   Rev 1.40   Mar 30 2005 17:27:58   asinclair
//Fixed error with return pages
//
//   Rev 1.39   Mar 18 2005 14:54:32   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.38   Mar 01 2005 16:29:40   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.37   Feb 28 2005 15:19:54   asinclair
//Added code to set units based on JourneyDetails Page javascript and URL passed.
//
//   Rev 1.36   Nov 23 2004 17:33:40   SWillcock
//Modified logic for displaying journey numbers after 'Outward journey' and 'return journey' text
//Resolution for 1781: Remove Journey Numbers from Details and Map Display in Quick Planners
//
//   Rev 1.35   Oct 22 2004 15:29:20   jmorrissey
//Added javascript to open a user survey form on Page_Init - if the user survey form should be shown
//
//   Rev 1.34   Oct 13 2004 12:05:40   jmorrissey
//Updated Page_Init to add page Id to the return stack
//
//   Rev 1.33   Oct 08 2004 12:36:32   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.32   Sep 28 2004 11:39:36   esevern
//removed journey number when displaying find a car journey
//
//   Rev 1.31   Sep 21 2004 16:38:34   esevern
//IR1581 - amended outward and return journey label for find a car (there will only be one journey)
//
//   Rev 1.30   Sep 20 2004 16:51:42   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.29   Sep 17 2004 15:14:28   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.28   Aug 31 2004 15:06:46   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.27   Jul 23 2004 15:36:46   jgeorge
//Updates for Del 6.1 Find a...
//
//   Rev 1.26   Jun 25 2004 15:25:58   esevern
//corrected display of return table itinerary details
//
//   Rev 1.25   Jun 23 2004 15:51:08   jmorrissey
//Fixed Extend Journey bugs
//
//   Rev 1.24   Jun 17 2004 17:26:48   jbroome
//Updated for use with TDItineraryManager
//
//   Rev 1.23   Apr 28 2004 16:20:20   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.22   Feb 19 2004 09:59:32   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.21   Dec 04 2003 18:38:04   kcheung
//Del 5.1 updates
//
//   Rev 1.20   Dec 01 2003 15:36:28   kcheung
//Added label for unadjusted route selected.
//Resolution for 460: In the traffic map for a car journey, the adjusted route should be default
//
//   Rev 1.19   Nov 21 2003 09:45:40   kcheung
//Updated for the return requested but no outward journey found case.
//
//   Rev 1.18   Nov 19 2003 12:48:10   kcheung
//Updated to use new properties in viewstate and journeyresult
//Resolution for 132: Determing whether Journey is return or not
//Resolution for 136: Properties of JourneyViewState to determine the selected outward and return journeys
//
//   Rev 1.17   Nov 17 2003 17:02:40   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.16   Nov 07 2003 11:29:24   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.15   Nov 05 2003 13:32:48   kcheung
//Summary headers added. Commented out code removed.
//
//   Rev 1.14   Nov 05 2003 10:46:04   kcheung
//Inserted : as requested in QA
//
//   Rev 1.13   Nov 04 2003 13:54:02   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.12   Oct 31 2003 09:31:48   passuied
//Get username from commerceserver for printable pages
//
//   Rev 1.11   Oct 22 2003 10:25:30   kcheung
//Fixed so that "accouting for traffic" only appears if the user is viewing the adjusted route.
//
//   Rev 1.10   Oct 15 2003 19:19:40   kcheung
//Removed call to property to remove help text of CarAllDetailsControl as it is no longer neccessary.
//
//   Rev 1.9   Oct 14 2003 16:05:52   passuied
//added styles 
//
//   Rev 1.8   Oct 14 2003 14:47:38   passuied
//minor changes
//
//   Rev 1.7   Oct 13 2003 16:52:28   passuied
//implemented page breaks in output pages
//
//   Rev 1.6   Oct 10 2003 11:04:16   kcheung
//Updated titles on page to read from langstrings
//
//   Rev 1.5   Oct 06 2003 11:44:26   PNorell
//Updated to conform to new wireframes.
//Updated to exclude them from the ScreenFlow.
//
//   Rev 1.4   Sep 30 2003 11:32:52   PNorell
//Corrected printable outlook.
//Added support for moving outside the screen flow state as is needed by some printable pages.
//
//   Rev 1.3   Sep 30 2003 09:48:52   kcheung
//Added journey reference number label
//
//   Rev 1.2   Sep 29 2003 16:26:02   kcheung
//Good working version - all styling fixed
//
//   Rev 1.1   Sep 29 2003 13:29:10   kcheung
//Working version - needs some styling tidying up

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
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable Journey Details page
	/// </summary>
	public partial class PrintableJourneyDetails : TDPrintablePage, INewWindowPage
	{
		protected ExtensionSummaryControl extensionSummaryControl;
		protected SummaryResultTableControl SummaryResultTableControlOutward;
		protected JourneyDetailsControl JourneyDetailsControlOutward;
		protected SummaryResultTableControl SummaryResultTableControlReturn;
		protected JourneyDetailsControl JourneyDetailsControlReturn;
		protected CarAllDetailsControl CarAllDetailsControlOutward;
		protected JourneyDetailsTableControl journeyDetailsTableControlOutward;
		protected JourneyDetailsTableControl journeyDetailsTableControlReturn;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected FindFareSelectedTicketLabelControl findFareSelectedTicketLabelControl;
		protected ResultsTableTitleControl resultsTableTitleControlOutward;
		protected ResultsTableTitleControl resultsTableTitleControlReturn;
        protected JourneyEmissionsCompareControl journeyEmissionsCompareControlOutward;
        protected Panel outwardDetailPanel;
        protected Panel outwardEmissionsPanel;

		protected CarAllDetailsControl CarAllDetailsControlReturn;
		protected PrintableMapControl PrintableOutwardMap;
		protected PrintableMapControl PrintableReturnMap;

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

		//private bool outwardMapExists = false;
		//private bool returnMapExists = false;

		#region Constructor / Page_Init/ PageLoad / Initialise Methods

		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public PrintableJourneyDetails()
		{
			this.pageId = PageId.PrintableJourneyDetails;
		}

		/// <summary>
		/// Page Init event handler,
		/// which checks if the user should be redirected to the User Survey form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{

			// set printable bool here
			CarAllDetailsControlOutward.Printable = false;
			CarAllDetailsControlReturn.Printable = false;
			
		}

		/// <summary>
		/// Page Load Method
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
				CarAllDetailsControlOutward.RoadUnits = RoadUnitsEnum.Kms;
				CarAllDetailsControlReturn.RoadUnits = RoadUnitsEnum.Kms;

                journeyEmissionsCompareControlOutward.RoadUnits = RoadUnitsEnum.Kms;
			}
			else
			{
				CarAllDetailsControlOutward.RoadUnits = RoadUnitsEnum.Miles;
				CarAllDetailsControlReturn.RoadUnits = RoadUnitsEnum.Miles;

                journeyEmissionsCompareControlOutward.RoadUnits = RoadUnitsEnum.Miles;
			}

			if (FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode))
			{
				findFareSelectedTicketLabelControl.PageState = sessionManager.FindPageState;
			}

			labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

			labelInstructions.Text = Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			Initialise();

			if (outwardExists)
			{
				PrintableOutwardMap.Populate(true, false, TDSessionManager.Current.IsFindAMode );
			}

			if (returnExists)
			{
				PrintableReturnMap.Populate(false, false, TDSessionManager.Current.IsFindAMode );
			}

			CarAllDetailsControlOutward.carjourneyDetailsTableControl.MapButtonVisible = false;

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
		/// Method to initialise controls that are on the page.
		/// </summary>
		private void Initialise()
		{			
			// Initialise labels and components
			InitialiseStaticLabels();
			InitialiseJourneyComponents();

		}

		/// <summary>
		/// Method to initialise the journey details control.
		/// </summary>
		private void InitialiseJourneyComponents()
		{
			
			ITDJourneyResult journeyResult = TDItineraryManager.Current.JourneyResult;
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			PublicJourney outPJ = null;
			PublicJourney retPJ = null;

            RoadJourneyHelper roadJourneyHelper = new RoadJourneyHelper();

            bool showTravelNewsIncidents = true;

			if(outwardExists)
            {
                #region Outward journey

                if (!TDItineraryManager.Current.FullItinerarySelected)
				{
					TDJourneyParametersMulti journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;

                    RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();

                    if ((roadJourney != null) && (!roadJourney.JourneyMatchedForTravelNewsIncidents))
                    {
                        roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                    }

					CarAllDetailsControlOutward.Initialise(true, journeyParams, showTravelNewsIncidents, 
                        false);
				}

				if (showItinerary)
				{
					if (!TDItineraryManager.Current.FullItinerarySelected)
					{
						if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
						{
							// Get the journey index of the selected journey.
							int journeyIndex = viewState.SelectedOutwardJourneyID;
							// Get the public journey from journeyResult
							outPJ = journeyResult.OutwardPublicJourney(journeyIndex);
						}
						else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
						{
							// the amended journey has been selected
							outPJ = journeyResult.AmendedOutwardPublicJourney;
						}
					}
				}
				else if (showFindA)
				{
                    // Get the modes to display on the results
                    TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                    if (TDSessionManager.Current.FindPageState != null)
                        modeTypes = TDSessionManager.Current.FindPageState.ModeType;

                    findSummaryResultTableControlOutward.Initialise(true, true, outwardArriveBefore, modeTypes);
					if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						// Get the journey index of the selected journey.
						int journeyIndex = viewState.SelectedOutwardJourneyID;
						// Get the public journey from journeyResult
						outPJ = journeyResult.OutwardPublicJourney(journeyIndex);
					}
					else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						outPJ = journeyResult.AmendedOutwardPublicJourney;
					}
				}
				else
				{
					SummaryResultTableControlOutward.Initialise(true, true, outwardArriveBefore);
					if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						// Get the journey index of the selected journey.
						int journeyIndex = viewState.SelectedOutwardJourneyID;
						// Get the public journey from journeyResult
						outPJ = journeyResult.OutwardPublicJourney(journeyIndex);
					}
					else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						outPJ = journeyResult.AmendedOutwardPublicJourney;
					}
				}

                if (outPJ != null)
				{
					JourneyDetailsControlOutward.Initialise(outPJ, true, false,false,false, TDItineraryManager.Current.JourneyRequest,TDSessionManager.Current.FindAMode);
					journeyDetailsTableControlOutward.Initialise(outPJ, true, TDSessionManager.Current.FindAMode);
				}
				else
				{
                    JourneyDetailsControlOutward.Initialise(true, false, false, false, TDSessionManager.Current.FindAMode);
                    journeyDetailsTableControlOutward.Initialise(true, TDSessionManager.Current.FindAMode);
				}

				JourneyDetailsControlOutward.MyPageId = pageId;
				JourneyDetailsControlOutward.Printable = true;
				journeyDetailsTableControlOutward.MyPageId = pageId;
				journeyDetailsTableControlOutward.PrinterFriendly = true;

                //Toggle JourneyEmissions / details / table control visibility NB only outward is considered 
                //at this stage as TD Extra has no returns and CO2 control only appears for TD Extra
                outwardEmissionsPanel.Visible = viewState.ShowCO2;
                outwardDetailPanel.Visible = !viewState.ShowCO2;

                #region Setup emissions control

                //Setup CO2 control for TD Extra
                ITDSessionManager sessionManager = TDSessionManager.Current;

                sessionManager.JourneyEmissionsPageState.IsInternationalJourney = true;

                // Determine where we get selected journey from Journey Result or Itinerary Manager
                // and then set the Compare emissions control flags.
                // The Compare emissions control will then populate distances and calculate emissions
                // from the session
                if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.IsValid))
                    || ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.IsValid)))
                    journeyEmissionsCompareControlOutward.UseSessionManager = true;
                else
                    journeyEmissionsCompareControlOutward.UseItineraryManager = true;

                // Set the mode for the Compare emissions control
                journeyEmissionsCompareControlOutward.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.JourneyCompare;

                journeyEmissionsCompareControlOutward.NonPrintable = false;

                #endregion

                #endregion
            }
			else
			{
				// Hide the outward panel
				panelOutward.Visible = false;
			}

					
			if( returnExists)
            {
                #region Return journey

                if (!TDItineraryManager.Current.FullItinerarySelected)
				{
					TDJourneyParametersMulti journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;

                    RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();

                    if ((roadJourney != null) && (!roadJourney.JourneyMatchedForTravelNewsIncidents))
                    {
                        roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                    }

					CarAllDetailsControlReturn.Initialise(false, journeyParams, showTravelNewsIncidents, 
                        false);
				}
			
				literalNewPage.Visible = true;

				if (itineraryExists && !extendInProgress)
				{
					//intialise for itinerary journey / journeys...
					if (!TDItineraryManager.Current.FullItinerarySelected)
					{
						if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
						{
							// Get the journey index of the selected journey.
							int journeyIndex = viewState.SelectedReturnJourneyID;
							// Get the public journey from journeyResult
							retPJ = journeyResult.ReturnPublicJourney(journeyIndex);
						}
						else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
						{
							// the amended journey has been selected
							retPJ = journeyResult.AmendedReturnPublicJourney;
						}
					}
				}
				else if (showFindA)
				{
                    // Get the modes to display on the results
                    TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                    if (TDSessionManager.Current.FindPageState != null)
                        modeTypes = TDSessionManager.Current.FindPageState.ModeType;

                    findSummaryResultTableControlReturn.Initialise(true, false, returnArriveBefore, modeTypes);
					if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						// Get the journey index of the selected journey.
						int journeyIndex = viewState.SelectedReturnJourneyID;
						// Get the public journey from journeyResult
						retPJ = journeyResult.ReturnPublicJourney(journeyIndex);
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						retPJ = journeyResult.AmendedReturnPublicJourney;
					}
				}
				else
				{
					SummaryResultTableControlReturn.Initialise(true, false, returnArriveBefore);
					if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						// Get the journey index of the selected journey.
						int journeyIndex = viewState.SelectedReturnJourneyID;
						// Get the public journey from journeyResult
						retPJ = journeyResult.ReturnPublicJourney(journeyIndex);
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						retPJ = journeyResult.AmendedReturnPublicJourney;
					}
				}
				if (retPJ != null)
				{
                    JourneyDetailsControlReturn.Initialise(retPJ, false, false, false, false, TDItineraryManager.Current.JourneyRequest,TDSessionManager.Current.FindAMode);
                    journeyDetailsTableControlReturn.Initialise(retPJ, false, TDSessionManager.Current.FindAMode);
				}
				else
				{
					JourneyDetailsControlReturn.Initialise(false, false, false, false,TDSessionManager.Current.FindAMode);
                    journeyDetailsTableControlReturn.Initialise(false, TDSessionManager.Current.FindAMode);
				}
				JourneyDetailsControlReturn.MyPageId = pageId;
				JourneyDetailsControlReturn.Printable = true;
				journeyDetailsTableControlReturn.MyPageId = pageId;
				journeyDetailsTableControlReturn.PrinterFriendly = true;

                #endregion
            }
			else
			{
				// Hide the return panel
				returnPanel.Visible = false;
			}
				

			labelDate.Text = TDDateTime.Now.ToString("G");

			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}

			// Set the journey reference number from the result
			if (journeyResult != null)
				labelJourneyReferenceNumber.Text = journeyResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);

		}
			
		/// <summary>
		/// OnPreRender method - overrides base and updates the visiblity
		/// of controls depending on which should be rendered. Calls base OnPreRender
		/// as the final step.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			InitialiseJourneyComponents();
			SetControlVisibility();

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

		#endregion

		#region UserSurvey


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
					Page.ClientScript.RegisterClientScriptBlock(typeof(PrintableJourneyDetails),"UserSurvey", scriptRepository.GetScript("UserSurvey", javaScriptDom));
				}				
			}
		}
		#endregion 

		#region Methods to control visiblities of controls

		/// <summary>
		/// Determines which controls should be visible and sets them accordingly.
		/// </summary>
		private void SetControlVisibility()
		{
			// Get the TDJourneyResult
			ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			bool isPublic = false;

			if (itineraryExists)
				viewState = TDItineraryManager.Current.JourneyViewState;
			else
				viewState = TDSessionManager.Current.JourneyViewState;

            divMapOutward.Visible = viewState.OutwardShowMap;
            divMapReturn.Visible = viewState.ReturnShowMap;
			PrintableOutwardMap.Visible = viewState.OutwardShowMap;
			PrintableReturnMap.Visible = viewState.ReturnShowMap;

            // Get the modes to display on the results
            TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
            if (TDSessionManager.Current.FindPageState != null)
                modeTypes = TDSessionManager.Current.FindPageState.ModeType;

			if(outwardExists)
			{
				if (itineraryExists && !extendInProgress)
				{
					// Removed for Vantive Fix 3714531
					// OutwardPublicJourneyVisible(true);
					// OutwardCarJourneyVisible(false);
					// End of Code Removed for Vantive Fix 3714531
					
					// Added for Vantive Fix 3714531
					try
					{
                        JourneySummaryLine[] outwardSummary = result.OutwardJourneySummary(outwardArriveBefore, modeTypes);
						JourneySummaryLine selOut = outwardSummary[viewState.SelectedOutwardJourney];
						isPublic = selOut.Type == TDJourneyType.PublicOriginal || selOut.Type == TDJourneyType.PublicAmended;

						OutwardPublicJourneyVisible(isPublic);
						OutwardCarJourneyVisible(!isPublic);
					}
					catch
					{
						OutwardPublicJourneyVisible(true);
						OutwardCarJourneyVisible(false);
					}
					// End of Code Added for Vantive Fix 3714531
				}
				else
				{
					// Get the outward Journey Summary Lines
                    JourneySummaryLine[] outwardSummary = result.OutwardJourneySummary(outwardArriveBefore, modeTypes);

					JourneySummaryLine selOut = outwardSummary[viewState.SelectedOutwardJourney];
					isPublic = selOut.Type == TDJourneyType.PublicOriginal || selOut.Type == TDJourneyType.PublicAmended;
				
					// Determine if the selected outward journey is public or road
					OutwardPublicJourneyVisible(isPublic);
					OutwardCarJourneyVisible(!isPublic);

					this.SummaryResultTableControlOutward.Visible = !showFindA;
					this.findSummaryResultTableControlOutward.Visible = showFindA;

					// Labels underneath the summary control
					this.labelDetailsOutwardJourney.Visible = true;


					if(TDSessionManager.Current.FindAMode == FindAMode.None) 
					{
						//set route number
						labelDetailsOutwardDisplayNumber.Text = selOut.DisplayNumber;
						this.labelDetailsOutwardDisplayNumber.Visible = true;
					}
					else 
					{
						labelDetailsOutwardDisplayNumber.Visible = false;
					}
				}
			}
			else
			{
				OutwardPublicJourneyVisible(outwardExists);
				OutwardCarJourneyVisible(outwardExists);
			}

			if(returnExists)
			{
				if (itineraryExists && !extendInProgress)
				{
					// Code Removed for Vantive Fix 3714531
					// ReturnPublicJourneyVisible(true);
					// ReturnCarJourneyVisible(false);
					// End of Code Removed for Vantive Fix 3714531

					// Added for Vantive Fix 3714531
					try
					{
                        JourneySummaryLine[] returnSummary = result.ReturnJourneySummary(returnArriveBefore, modeTypes);
						JourneySummaryLine selRet = returnSummary[viewState.SelectedReturnJourney];
						isPublic = selRet.Type == TDJourneyType.PublicOriginal || selRet.Type == TDJourneyType.PublicAmended;
						
						ReturnPublicJourneyVisible(isPublic);
						ReturnCarJourneyVisible(!isPublic);
					}
					catch
					{
						ReturnPublicJourneyVisible(true);
						ReturnCarJourneyVisible(false);
					}
					// End of Code Added for Vantive Fix 3714531
				}
				else
				{
					// Get the return journey summary line
                    JourneySummaryLine[] returnSummary = result.ReturnJourneySummary(returnArriveBefore, modeTypes);

					JourneySummaryLine selRet = returnSummary[viewState.SelectedReturnJourney];
					isPublic = viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal ||
						viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended;

					// Determine if the selected outward journey is public or road
					ReturnPublicJourneyVisible(isPublic);
					ReturnCarJourneyVisible(!isPublic);	
					
					//set route number
					if(TDSessionManager.Current.FindAMode == FindAMode.None) 
					{
						labelDetailsReturnDisplayNumber.Text = selRet.DisplayNumber;
					}
					else 
					{
						labelDetailsReturnDisplayNumber.Text = string.Empty;
					}
					
				}
			}
			else
			{
				ReturnPublicJourneyVisible(returnExists);
				ReturnCarJourneyVisible(returnExists);
			}

            SetupStepsControl();

			// Labels above the summary control
			// Outward labels above the summary control
			this.SummaryResultTableControlReturn.Visible =  !showFindA;
			this.findSummaryResultTableControlReturn.Visible = showFindA;

			// Labels underneath the summary control
			this.labelDetailsReturnJourney.Visible = returnExists;
			this.labelDetailsReturnDisplayNumber.Visible = returnExists;

			//show extension control
			extensionSummaryControl.Visible = (itineraryExists && extendInProgress);
			
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


		/// <summary>
		/// Sets the visibility of all outward public journey controls.
		/// </summary>
		/// <param name="visible">True if visible, false if not visible.</param>
		private void OutwardPublicJourneyVisible(bool visible)
		{
			// Journey Details control
			this.JourneyDetailsControlOutward.Visible =
				visible &&
				TDSessionManager.Current.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode;

			this.journeyDetailsTableControlOutward.Visible =
				visible &&
				!TDSessionManager.Current.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode;
		}

		/// <summary>
		/// Sets the visibility of all return public journey controls.
		/// </summary>
		/// <param name="visible">True if visible, false if not visible.</param>
		private void ReturnPublicJourneyVisible(bool visible)
		{
			// Journey Details Control
			this.JourneyDetailsControlReturn.Visible =
				visible &&
				TDSessionManager.Current.JourneyViewState.ShowReturnJourneyDetailsDiagramMode;

			this.journeyDetailsTableControlReturn.Visible =
				visible &&
				!TDSessionManager.Current.JourneyViewState.ShowReturnJourneyDetailsDiagramMode;
		}

		/// <summary>
		/// Sets the visiblity of all outward car journey controls.
		/// </summary>
		/// <param name="visible">True if visible, false if not visible.</param>
		private void OutwardCarJourneyVisible(bool visible)
		{
			// Car Details Control
			this.CarAllDetailsControlOutward.Visible = visible;
			
			if(TDSessionManager.Current.FindAMode == FindAMode.Car) 
			{
				this.labelCarOutward.Visible = false;
			}
			else 
			{
				this.labelCarOutward.Visible = visible;
			}

		}

		/// <summary>
		/// Sets the visibility of all return car journey controls.
		/// </summary>
		/// <param name="visible">True if visible, false if not visible.</param>
		private void ReturnCarJourneyVisible(bool visible)
		{
			// Car Details Control
			this.CarAllDetailsControlReturn.Visible = visible;
			if(TDSessionManager.Current.FindAMode == FindAMode.Car) 
			{
				this.labelCarReturn.Visible = false;
			}
			else 
			{
				this.labelCarReturn.Visible = visible;
			}
		}

		#endregion

		#region Methods to set static label text

		/// <summary>
		/// Method to initialise all the static labels on the page.
		/// </summary>
		private void InitialiseStaticLabels()
		{
			labelDetailsOutwardJourney.Text = Global.tdResourceManager.GetString(
				"JourneyDetails.labelDetailsOutwardJourney.Text", TDCultureInfo.CurrentUICulture);

			labelDetailsReturnJourney.Text = Global.tdResourceManager.GetString(
				"JourneyDetails.labelDetailsReturnJourney.Text", TDCultureInfo.CurrentUICulture);

			//if find a don't show this
			if(TDSessionManager.Current.FindAMode == FindAMode.Car) 
			{
				labelCarOutward.Visible = false;
				labelCarReturn.Visible = false;
			}
			else 
			{
				labelCarOutward.Text = "(" + Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsCar", TDCultureInfo.CurrentUICulture) + ")";

				labelCarReturn.Text = "(" + Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsCar", TDCultureInfo.CurrentUICulture) + ")";
			} 

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
			//TDSessionManager.Current.FormShift[ SessionKey.SkipScreenFlow ] = true;
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
