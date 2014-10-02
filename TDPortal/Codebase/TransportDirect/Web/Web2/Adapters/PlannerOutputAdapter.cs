// *********************************************** 
// NAME			: PlannerOutputAdapter.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 23 March 2005
// DESCRIPTION	: Responsible for common functionality
//				  required by output pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/PlannerOutputAdapter.cs-arc  $
//
//   Rev 1.9   Feb 05 2013 13:21:24   mmodi
//Show Walk Interchange in map journey leg dropdown for accessible journey when required
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.8   Sep 21 2011 10:37:52   DLane
//Fix to tab strip at the bottom of the results page for D2D journeys.
//Resolution for 5740: Erroneous search by price footer in D2D results
//
//   Rev 1.7   Feb 24 2010 17:48:54   mmodi
//Test for date when setting up for International mode
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 24 2010 15:31:52   mmodi
//Updated to display date for International journey
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Oct 15 2008 11:27:32   mmodi
//Corrected show bank holiday to check for null object
//
//   Rev 1.4   Oct 14 2008 13:16:54   rbroddle
//ATO585 CCN460 Better Use of Seasonal Noticeboard
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.3   Oct 13 2008 16:41:28   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.2   Aug 22 2008 10:26:12   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Jul 30 2008 11:09:32   mmodi
//Display cycle journey in results summary control
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Jun 20 2008 14:41:06   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:28   mturner
//Initial revision.
//
//   Rev 1.18   May 29 2007 13:34:40   mmodi
//Tested for a null Return date/time to prevent error when populating the results summary control title
//Resolution for 4428: CO2: View emissions following a Find cheaper
//
//   Rev 1.17   Apr 18 2007 14:32:20   mmodi
//Corrected setting of InwardArriveBefore properrty of TitleControl for the Return journey
//Resolution for 4390: CO2: Leaving by and arriving by are wrong way round
//
//   Rev 1.16   Mar 06 2007 12:29:54   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.15.1.0   Feb 20 2007 17:28:04   mmodi
//Overloaded PopulateResultsTableTitleFullItinerary to also show the Return date/time
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.15   Mar 28 2006 08:26:58   pcross
//Update to PopulateResultsTableTitleFullItinerary to allow specification of whether to show time
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.14   Mar 14 2006 11:15:06   asinclair
//Manual merge of stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13   Feb 23 2006 17:13:14   RWilby
//Merged stream3129
//
//   Rev 1.12   Feb 10 2006 12:24:26   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.11   Jan 16 2006 15:58:40   RPhilpott
//Code review changes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.10   Dec 13 2005 11:34:42   asinclair
//Merge for stream3143
//
//   Rev 1.9.2.0   Dec 12 2005 17:17:54   tmollart
//Removed code that sets selected tab.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.9.1.0   Nov 23 2005 11:14:34   jgeorge
//Removed code dealing with amend hyperlink, as this control is no longer used on the results pages.
//Resolution for 3144: DEL 8 stream: Client Links Development
//
//   Rev 1.9   Nov 15 2005 17:11:34   mguney
//Null check implemented for pageState.SelectedTravelDate in PopulateResultsTableTitles method.
//Resolution for 3072: DN040: SBP Unhandled error when New Search clicked after Extend
//
//   Rev 1.8   Nov 09 2005 12:31:56   build
//Automatically merged from branch for stream2818
//
//   Rev 1.7.1.0   Oct 14 2005 15:28:20   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.7   Apr 15 2005 19:52:08   rhopkins
//Display correct tilte when displaying journeys for Open return fares.
//
//   Rev 1.6   Apr 15 2005 12:47:32   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.5   Apr 13 2005 13:56:24   rhopkins
//Display different ResultsTableTitles depending on the TicketType for the currently selected TravelDate
//Resolution for 2140: FindFare results pages selected Ticket Type and Transport Mode displayed incorrectly
//
//   Rev 1.4   Apr 05 2005 16:24:44   rhopkins
//Added methods for initialising AmendSaveSendControl
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.3   Apr 05 2005 10:43:48   rhopkins
//Remove nullification of summary data in ViewPartitionResults
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.2   Apr 04 2005 20:24:48   rhopkins
//Add method for redirecting after changing Session partition
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.1   Mar 30 2005 16:37:50   rhopkins
//Change criteria for displaying title components
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.0   Mar 23 2005 16:56:58   rhopkins
//Initial revision.
//

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Responsible for common functionality required by Find A input pages.
    /// </summary>
    public class PlannerOutputAdapter
    {
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

        private bool returnArriveBefore;
        private bool outwardArriveBefore;

        private bool resultsStateDetermined;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PlannerOutputAdapter()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tdSessionManager">SessionManager being used by parent page</param>
        public PlannerOutputAdapter(ITDSessionManager tdSessionManager)
        {
            this.tdSessionManager = tdSessionManager;
            this.itineraryManager = tdSessionManager.ItineraryManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the properties on the results tables labels so that the correct text will be displayed.
        /// </summary>
        /// <param name="outwardControl">Title control displayed above the Outward results</param>
        /// <param name="returnControl">Title control displayed above the Return results</param>
        public void PopulateResultsTableTitles(ResultsTableTitleControl outwardControl, ResultsTableTitleControl returnControl)
        {
            PopulateResultsTableTitles(outwardControl, true);
            PopulateResultsTableTitles(returnControl, false);
        }

        /// <summary>
        /// Sets the properties on the results tables labels so that the correct text will be displayed.
        /// </summary>
        /// <param name="control">Title control displayed</param>
        /// <param name="outward">Flag indicating to populate with outward or return text</param>
        public void PopulateResultsTableTitles(ResultsTableTitleControl control, bool outward)
        {
            // Many different types of results can be displayed, so check what we are displaying
            if (!resultsStateDetermined || itineraryManager.ItineraryManagerModeChanged)
            {
                DetermineStateOfResults();
            }

            //Only show bank holiday messages if we have public journeys
            bool showBankHoliday = false;

            if (itineraryManager != null && itineraryManager.JourneyResult != null)
            {
                if (outward)
                {
                    showBankHoliday = (itineraryManager.JourneyResult.OutwardPublicJourneyCount > 0);
                }
                else
                {
                    showBankHoliday = (itineraryManager.JourneyResult.ReturnPublicJourneyCount > 0);
                }
            }

            control.ShowBankHoliday = showBankHoliday;

            if (showItinerary)
            {
                // Viewing the whole of the current Itinerary

                control.ShowTime = false;
                control.ResultsType = DisplayResultsType.ExtendedJourney;

                if (outward) // Outward journey
                {
                    control.OutwardFirstDate = itineraryManager.OutwardDepartDateTime();
                    control.OutwardLastDate = itineraryManager.OutwardArriveDateTime();
                }
                if (!outward && returnExists) // Return journey
                {
                    control.InwardFirstDate = itineraryManager.ReturnDepartDateTime();
                    control.InwardLastDate = itineraryManager.ReturnArriveDateTime();
                }
            }
            else
            {
                // Viewing results of a planned journey or extension

                if ((tdSessionManager.FindAMode == FindAMode.Car) || (tdSessionManager.FindAMode == FindAMode.Cycle))
                {
                    control.ResultsType = DisplayResultsType.FindACar;
                    control.ShowTime = true;
                }
                else if (FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
                {
                    FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
                    //pageState.SelectedTravelDate becomes null after Extend Journey. No need to proceed further.
                    if (pageState.SelectedTravelDate == null)
                        return;

                    if (outward)
                    {
                        control.ShowMode = true;
                        control.TravelMode = pageState.SelectedTravelDate.TravelDate.TravelMode;
                    }

                    TicketType selectedTicketType = pageState.SelectedTravelDate.TravelDate.TicketType;

                    if (selectedTicketType == TicketType.Single)
                    {
                        if (outward) // Outward journey only
                        {
                            control.ResultsType = DisplayResultsType.Journeys;
                            control.ShowTicketType = true;
                        }
                    }
                    else if (selectedTicketType == TicketType.OpenReturn)
                    {
                        if (outward) // Outward journey only
                        {
                            control.ResultsType = DisplayResultsType.JourneysOpenReturn;
                            control.ShowTicketType = true;
                        }
                    }
                    else
                    {
                        if (!outward) // Return journey only
                        {
                            control.ShowMode = true;
                            control.TravelMode = pageState.SelectedTravelDate.TravelDate.TravelMode;

                            control.ResultsType = DisplayResultsType.Journeys;
                        }
                    }

                }
                else if (extendInProgress)
                {
                    control.ResultsType = DisplayResultsType.Extensions;
                    control.ShowTime = true;
                }
                else
                {
                    control.ResultsType = DisplayResultsType.Journeys;
                    control.ShowTime = true;
                }

                // Populate dates of controls
                if ((tdSessionManager.FindAMode == FindAMode.International)
                    && (tdSessionManager.JourneyRequest != null))
                {
                    // Specific handling for International
                    ITDJourneyRequest journeyRequest = tdSessionManager.JourneyRequest;

                    if ((outward) && (journeyRequest.OutwardDateTime != null) && (journeyRequest.OutwardDateTime.Length > 0))
                    {
                        control.OutwardFirstDate = journeyRequest.OutwardDateTime[0];
                        control.OutwardAnytime = true;
                        control.OutwardArriveBefore = false;
                    }

                    if ((!outward && returnExists) && (journeyRequest.ReturnDateTime != null) && (journeyRequest.ReturnDateTime.Length > 0))
                    {
                        control.InwardFirstDate = journeyRequest.ReturnDateTime[0];
                        control.InwardAnytime = true;
                        control.InwardArriveBefore = false;
                    }
                }
                else if (tdSessionManager.JourneyViewState.OriginalJourneyRequest != null)
                {
                    ITDJourneyRequest journeyRequest = tdSessionManager.JourneyViewState.OriginalJourneyRequest;

                    if (outward) // Outward journey
                    {
                        control.OutwardFirstDate = journeyRequest.OutwardDateTime[0];
                        control.OutwardAnytime = journeyRequest.OutwardAnyTime;
                        control.OutwardArriveBefore = journeyRequest.OutwardArriveBefore;
                    }

                    if (!outward && returnExists) // Return journey
                    {
                        control.InwardFirstDate = journeyRequest.ReturnDateTime[0];
                        control.InwardAnytime = journeyRequest.ReturnAnyTime;
                        control.InwardArriveBefore = journeyRequest.ReturnArriveBefore;
                    }
                }
                else if (tdSessionManager.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.Cycle)
                {
                    ITDCyclePlannerRequest cycleRequest = tdSessionManager.CycleRequest;
                    // For a cycle journey 
                    if (outward) // Outward journey
                    {
                        control.OutwardFirstDate = cycleRequest.OutwardDateTime[0];
                        control.OutwardAnytime = cycleRequest.OutwardAnyTime;
                        control.OutwardArriveBefore = cycleRequest.OutwardArriveBefore;
                    }

                    if (!outward && returnExists) // Return journey
                    {
                        control.InwardFirstDate = cycleRequest.ReturnDateTime[0];
                        control.InwardAnytime = cycleRequest.ReturnAnyTime;
                        control.InwardArriveBefore = cycleRequest.ReturnArriveBefore;
                    }
                }
                else
                {
                    control.Outward = outward;
                }
            }
        }

        /// <summary>
        /// Go to the appropriate page to display the results for the selected partition.
        /// If the selected partition is the same as the current partition then no redirection will occur
        /// </summary>
        /// <param name="partitionSelected"></param>
        public void ViewPartitionResults(PartitionType partitionSelected)
        {
            tdSessionManager.Session[SessionKey.Transferred] = false;

            if (partitionSelected == PartitionType.FindAFare)
            {
                if (tdSessionManager.Partition != TDSessionPartition.CostBased)
                {
                    // Switch to Cost Based results

                    tdSessionManager.Partition = TDSessionPartition.CostBased;

                    if (tdSessionManager.HasCostBasedJourneyResults)
                    {
                        tdSessionManager.FormShift[SessionKey.ForceRedirect] = true;
                        tdSessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.Default;
                    }
                    else
                    {
                        // CostBased fares have been obtained so go to the Date Selection page
                        tdSessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareDateSelectionDefault;
                    }
                }
            }
            else
            {
                if (tdSessionManager.Partition != TDSessionPartition.TimeBased)
                {
                    // Switch to Time Based results
                    tdSessionManager.Partition = TDSessionPartition.TimeBased;

                    // Redisplay the current page showing the Time Based results
                    tdSessionManager.FormShift[SessionKey.ForceRedirect] = true;
                    tdSessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.Default;
                }
            }
        }

        /// <summary>
        /// Set up the AmendSaveSendControl during the PageLoad event for the page.
        /// </summary>
        /// <param name="amendSaveSendControl">The AmendSaveSendControl to be set up</param>
        /// <param name="pageId">PageId of the current page</param>
        public void AmendSaveSendControlPageLoad(AmendSaveSendControl amendSaveSendControl, PageId pageId)
        {
            amendSaveSendControl.Initialise(pageId);

            // Set property that will determine whether the AmendViewControl should be displayed
            amendSaveSendControl.AmendViewControl.TimeBasedFindAMode = tdSessionManager.TimeBasedFindAMode;
            if (!tdSessionManager.IsFindAMode)
            {
                // We're not looking at times / costs
                amendSaveSendControl.AmendViewControl.PartitionSelectionAvailable = false;
            }
            else if (tdSessionManager.Partition == TDSessionPartition.CostBased)
            {
                // We're in CostBased mode, so check for TimeBased results
                amendSaveSendControl.AmendViewControl.PartitionSelectionAvailable = tdSessionManager.HasTimeBasedJourneyResults;
            }
            else
            {
                // We're in TimeBased mode, so check for CostBased results
                amendSaveSendControl.AmendViewControl.PartitionSelectionAvailable = tdSessionManager.HasCostBasedFaresResults || tdSessionManager.HasCostBasedJourneyResults;
            }
        }

        /// <summary>
        /// Set up the AmendSaveSendControl during the PreRender event for the page.
        /// </summary>
        /// <param name="amendSaveSendControl">The AmendSaveSendControl to be set up</param>
        /// <param name="hyperlinkAmendSaveSend">The panel that contains the hyperlink that scrolls down to the AmendSaveSendControl</param>
        public void AmendSaveSendControlPreRender(AmendSaveSendControl amendSaveSendControl)
        {
            // Manually invoke the prerender logic of AmendSaveSendControl so that we can interrogate the control's state afterwards
            amendSaveSendControl.manualPreRender();

            // Set selected dropdown option in AmendViewControl
            if (tdSessionManager.Partition == TDSessionPartition.CostBased)
            {
                amendSaveSendControl.AmendViewControl.PartitionSelected = PartitionType.FindAFare;
            }
            else
            {
                if (tdSessionManager.IsFindAMode)
                {
                    amendSaveSendControl.AmendViewControl.PartitionSelected = PartitionType.QuickPlanner;
                }
                else
                {
                    amendSaveSendControl.AmendViewControl.PartitionSelected = PartitionType.DoorToDoor;
                }
            }
        }

        /// <summary>
        /// Sets the properties on the results tables labels so that the correct text will be displayed.
        /// </summary>
        /// <param name="titleControl">Title control displayed above the results</param>
        /// <param name="labelTextOverride">Overrides the natural title label text with the string passed in</param>
        /// <param name="showTime">Sets whether to show the time or not in the title</param>
        /// <remarks>
        /// Method added for populating the title above a full itinerary results table (RefineJourneyPlan page).
        /// Based on PopulateResultsTableTitles above which takes an outward and return control.
        /// This version only displays the date and time for the outward journey (even if a return is present).
        /// It shows the title as if we are viewing a full itinerary but actually, on 1st load of the RefineJourneyPlan
        /// page we have selected journeys but not yet added to itinerary. Subsequent visits to page may well get data
        /// from itinerary.
        public void PopulateResultsTableTitleFullItinerary(ResultsTableTitleControl titleControl, string labelTextOverride, bool showTime)
        {
            this.PopulateResultsTableTitleFullItinerary(titleControl, labelTextOverride, showTime, false);
        }

        /// <summary>
        /// Sets the properties on the results tables labels so that the correct text will be displayed.
        /// </summary>
        /// <param name="titleControl">Title control displayed above the results</param>
        /// <param name="labelTextOverride">Overrides the natural title label text with the string passed in</param>
        /// <param name="showTime">Sets whether to show the time or not in the title</param>
        /// <param name="showReturnTime">Sets whether to show the Return time or not in the title</param>
        /// <remarks>
        /// Method added for populating the title above a full itinerary results table (RefineJourneyPlan page).
        /// Based on PopulateResultsTableTitles above which takes an outward and return control.
        /// This version CAN display the date and time for the outward AND return journey, if specified.
        /// It shows the title as if we are viewing a full itinerary but actually, on 1st load of the RefineJourneyPlan
        /// page we have selected journeys but not yet added to itinerary. Subsequent visits to page may well get data
        /// from itinerary.
        public void PopulateResultsTableTitleFullItinerary(ResultsTableTitleControl titleControl, string labelTextOverride, bool showTime, bool showReturnTime)
        {
            // Many different types of results can be displayed, so check what we are displaying
            if (!resultsStateDetermined || itineraryManager.ItineraryManagerModeChanged)
            {
                DetermineStateOfResults();
            }

            //Only show bank holiday messages if we have public journeys
            titleControl.ShowBankHoliday = (InwardIsPublic || OutwardIsPublic);

            titleControl.ResultsType = DisplayResultsType.ExtendedJourney;
            titleControl.ShowTime = showTime;

            // Override the label text with that passed in
            if (labelTextOverride.Length > 0)
                titleControl.LabelTextOverride = labelTextOverride;

            if (showItinerary)
            {
                // Viewing the whole of the current Itinerary
                titleControl.OutwardFirstDate = itineraryManager.OutwardDepartDateTime();
            }
            else
            {
                // Viewing results of a planned journey or extension
                if (tdSessionManager.FindAMode == FindAMode.Car)
                {
                    titleControl.ResultsType = DisplayResultsType.FindACar;
                }
                else if (FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
                {
                    FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
                    //pageState.SelectedTravelDate becomes null after Extend Journey. No need to proceed further.
                    if (pageState.SelectedTravelDate == null)
                        return;

                    titleControl.ShowMode = true;
                    titleControl.TravelMode = pageState.SelectedTravelDate.TravelDate.TravelMode;

                    TicketType selectedTicketType = pageState.SelectedTravelDate.TravelDate.TicketType;

                    if (selectedTicketType == TicketType.Single)
                    {
                        titleControl.ResultsType = DisplayResultsType.Journeys;
                        titleControl.ShowTicketType = true;
                    }
                    else if (selectedTicketType == TicketType.OpenReturn)
                    {
                        titleControl.ResultsType = DisplayResultsType.JourneysOpenReturn;
                        titleControl.ShowTicketType = true;
                    }

                }
                else if (extendInProgress)
                {
                    titleControl.ResultsType = DisplayResultsType.Extensions;
                }
                else
                {
                    titleControl.ResultsType = DisplayResultsType.Journeys;
                }

                // Populate dates of controls
                if (tdSessionManager.JourneyViewState.OriginalJourneyRequest != null)
                {
                    ITDJourneyRequest journeyRequest = tdSessionManager.JourneyViewState.OriginalJourneyRequest;

                    titleControl.OutwardFirstDate = journeyRequest.OutwardDateTime[0];
                    titleControl.OutwardAnytime = journeyRequest.OutwardAnyTime;
                    titleControl.OutwardArriveBefore = journeyRequest.OutwardArriveBefore;

                    if (showReturnTime)
                    {
                        // Only set the return date/times if they exist
                        if ((journeyRequest.ReturnDateTime != null) && (journeyRequest.ReturnDateTime.Length > 0))
                        {
                            titleControl.InwardFirstDate = journeyRequest.ReturnDateTime[0];
                            titleControl.InwardAnytime = journeyRequest.ReturnAnyTime;
                            titleControl.InwardArriveBefore = journeyRequest.ReturnArriveBefore;
                        }
                    }
                }
                else if (tdSessionManager.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.Cycle)
                {
                    ITDCyclePlannerRequest cycleRequest = tdSessionManager.CycleRequest;
                    // For a cycle journey 
                    titleControl.OutwardFirstDate = cycleRequest.OutwardDateTime[0];
                    titleControl.OutwardAnytime = cycleRequest.OutwardAnyTime;
                    titleControl.OutwardArriveBefore = cycleRequest.OutwardArriveBefore;

                    if (showReturnTime)
                    {
                        // Only set the return date/times if they exist
                        if ((cycleRequest.ReturnDateTime != null) && (cycleRequest.ReturnDateTime.Length > 0))
                        {
                            titleControl.InwardFirstDate = cycleRequest.ReturnDateTime[0];
                            titleControl.InwardAnytime = cycleRequest.ReturnAnyTime;
                            titleControl.InwardArriveBefore = cycleRequest.ReturnArriveBefore;
                        }
                    }
                }
                else
                {
                    titleControl.Outward = true;
                }
            }
        }

        /// <summary>
        /// Determines if a cycle journey exists
        /// </summary>
        /// <returns></returns>
        public bool CycleExists(bool outward)
        {
            // check for cycle result
            ITDCyclePlannerResult cycleResult = tdSessionManager.CycleResult;
            if (cycleResult != null)
            {
                bool outwardExists = ((cycleResult.OutwardCycleJourneyCount) > 0) && cycleResult.IsValid;
                bool returnExists = ((cycleResult.ReturnCycleJourneyCount) > 0) && cycleResult.IsValid;

                return (outward ? outwardExists : returnExists);

            }
            return false;
        }



        #endregion Public Methods

        #region Public properties

        /// <summary>
        /// Read-only - returns true if the selected outward journey is public. Does not take account
        /// of extend state. 
        /// </summary>
        public bool OutwardIsPublic
        {
            get
            {
                TDJourneyViewState viewState = itineraryManager.JourneyViewState;
                return (viewState != null) &&
                    ((viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal) ||
                    (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended));
            }
        }

        /// <summary>
        /// Read-only - returns true if the selected inward journey is public. Does not take account
        /// of extend state. 
        /// </summary>
        public bool InwardIsPublic
        {
            get
            {
                TDJourneyViewState viewState = itineraryManager.JourneyViewState;
                return (viewState != null) &&
                    ((viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal) ||
                    (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended));
            }
        }

        #region Accessible journey helper methods

        /// <summary>
        /// Determines whether for an accessible journey, the walk leg should be
        /// an accessible interchange walk leg
        /// </summary>
        /// <returns>True if interchange pseudo-leg is needed.</returns>
        public bool WalkInterchangeRequired(JourneyLeg detail, JourneyControl.Journey journey, bool isAccessible)
        {
            if (detail != null && journey != null)
            {
                // Where its walk, we need to show interchange for accessible journeys 
                if ((isAccessible) && (detail.Mode == ModeType.Walk))
                {
                    // For first and last leg, leave the mode as it is,
                    // prevents showing a walk interchange mode when it is not meaningful, 
                    // e.g. going from a stop outside station and onto platform
                    if (!IsFirstLeg(journey, detail) && !IsLastLeg(journey, detail))
                    {
                        // If walk is between naptans (i.e. not to/from a postcode, and not to/from locality)
                        // then set to the accessible interchange mode
                        string naptanLegStart = GetStartNaptan(detail);
                        string naptanLegEnd = GetEndNaptan(detail);

                        if (!string.IsNullOrEmpty(naptanLegStart) && !string.IsNullOrEmpty(naptanLegEnd))
                        {
                            // Accessible interchange mode should be shown
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Establish what mode the Itinerary Manager is in and whether we have any Return results
        /// </summary>
        private void DetermineStateOfResults()
        {
            itineraryExists = (itineraryManager.Length > 0);
            extendInProgress = itineraryManager.ExtendInProgress;
            showItinerary = (itineraryExists && !extendInProgress);
            showFindA = (!showItinerary && (tdSessionManager.IsFindAMode));

            if (showItinerary)
            {
                outwardExists = (itineraryManager.OutwardLength > 0);
                returnExists = (itineraryManager.ReturnLength > 0);
            }
            else
            {
                // check for cycle result
                outwardExists = CycleExists(true);
                returnExists = CycleExists(false);

                //check for normal result
                ITDJourneyResult result = tdSessionManager.JourneyResult;
                if (result != null)
                {
                    outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || (outwardExists);
                    returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || (returnExists);

                    // Get time types for journey.
                    outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
                    returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
                }
            }

            resultsStateDetermined = true;
        }

        /// <summary>
        /// Returns true if the given legDetail is the first leg of the journey, and false if not.
        /// </summary>
        /// <returns>True or false.</returns>
        private bool IsFirstLeg(JourneyControl.Journey journey, JourneyLeg detail)
        {
            return journey.JourneyLegs[0] == detail;
        }

        /// <summary>
        /// Returns true if the given legDetail is the last leg of the journey, and false if not.
        /// </summary>
        /// <returns>True or false.</returns>
        private bool IsLastLeg(JourneyControl.Journey journey, JourneyLeg detail)
        {
            return journey.JourneyLegs[journey.JourneyLegs.Length - 1] == detail;
        }

        /// <summary>
        /// Returns the naptan of the journey leg start location
        /// </summary>
        /// <returns>Naptan of the start location</returns>
        private string GetStartNaptan(JourneyLeg journeyLeg)
        {
            TDNaptan[] naptans = journeyLeg.LegStart.Location.NaPTANs;

            if (naptans != null && naptans.Length > 0 && naptans[0].Naptan != null && naptans[0].Naptan.Length != 0)
            {
                return naptans[0].Naptan;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the naptan of the journey leg end location
        /// </summary>
        /// <returns>Naptan of the end location</returns>
        private string GetEndNaptan(JourneyLeg journeyLeg)
        {
            TDNaptan[] naptans = journeyLeg.LegEnd.Location.NaPTANs;

            if (naptans != null && naptans.Length > 0 && naptans[0].Naptan != null && naptans[0].Naptan.Length != 0)
            {
                return naptans[0].Naptan;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion Private Methods
    }
}