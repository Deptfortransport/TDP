// *************************************************************************** 
// NAME                 : TabSelectionManager.cs
// AUTHOR               : J George
// DATE CREATED         : 24/11/2005
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Support/TabSelectionManager.cs-arc  $
//
//   Rev 1.10   Feb 16 2010 17:54:30   mmodi
//Added International input page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Nov 02 2009 17:55:20   mmodi
//Updated for Find map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Sep 21 2009 14:57:34   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.7   Feb 06 2009 14:43:00   apatel
//Search Engine Optimisation Changes
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.6   Nov 05 2008 12:21:34   rbroddle
//Updated following code review
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.5   Oct 13 2008 16:45:02   build
//Automatically merged from branch for stream5014
//
//   Rev 1.4.1.2   Aug 22 2008 10:39:16   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.1   Jul 18 2008 13:52:32   mmodi
//Updated as part of cycle planner workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.0   Jun 20 2008 14:28:44   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.0   Jun 20 2008 14:14:36   mmodi
//Added Cycle pages
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   May 29 2008 15:37:52   mturner
//Fix for IR5012.  Changed to highlight the Journey Planning Tab when on the JourneyEmissionsCompareJourney Page.
//Resolution for 5012: Incorrect left hand links on JourneyEmissionsCompare and LocationInformation Pages
//
//   Rev 1.3   Apr 09 2008 11:39:20   apatel
//updated to set tab to tipsandtools tab on faq page.
//Resolution for 4835: Del 10 - (all themes) the home button on FAQ 'entry page' does not send the user to the homepage.
//
//   Rev 1.2   Mar 31 2008 13:27:12   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 15 2008 13:00:00   mmodi
//Updated tab selection for FindCarParkInput page based on mode
//
//   Rev 1.0   Nov 08 2007 13:28:54   mturner
//Initial revision.
//
//   Rev 1.15   May 24 2007 15:08:44   mmodi
//Added SorryPage
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.14   Mar 06 2007 12:29:56   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.13.1.0   Feb 19 2007 17:54:00   mmodi
//Added Journey Emission Compare pages
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.13   Oct 06 2006 16:35:30   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.12.1.0   Sep 14 2006 15:26:02   tmollart
//Updated with Page ID for carparks.
//
//   Rev 1.12   May 03 2006 14:36:26   mtillett
//Add ParkAndRideInput to be shown on the plan a journey tab
//Resolution for 4066: DN058 Park and Ride Phase 2:  Incorrect tab displayed
//
//   Rev 1.11   Apr 11 2006 11:19:42   mdambrine
//Add find a bus page
//Resolution for 3866: DN093 Find a Bus: Plan a journey tab not highlighted when in Find a bus page
//
//   Rev 1.10   Mar 28 2006 15:32:48   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 28 2006 09:30:12   AViitanen
//Manual merge for Travel news (stream0024)
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.8   Mar 16 2006 14:16:32   rgreenwood
//IR3613 - Added cases for BusinessLinks and ToolbarDownload pages to SelectedTab() method.
//Resolution for 3613: Homepage Phase 2: Highlighted Tab shows Homepage when Business link page shown
//
//   Rev 1.7   Mar 09 2006 17:18:38   CRees
//Added some code for the page type JourneyPlannerLocationMap to fix toolbar issue with selected tabs.
//
//   Rev 1.6   Jan 11 2006 14:21:28   tmollart
//Updated after comments from code review. 
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.5   Jan 09 2006 15:31:26   RGriffith
//Changing some CMS pages to highlight the Tips and Tools tab (not Home tab) when they are selected
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.4   Dec 09 2005 12:30:24   NMoorhouse
//Changes for the addition of new mini-homepages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.3   Dec 06 2005 18:10:58   RGriffith
//Added method LoginControlsVisibleForPage to hide login links for certain pages
//
//   Rev 1.2   Dec 06 2005 18:05:10   NMoorhouse
//Added Tab selection logic for HelpFullJP page
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Nov 29 2005 16:17:50   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.0   Nov 28 2005 13:44:54   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.Web.Support
{
	/// <summary>
    /// This class is responsible for controlling tab related behaviour of the tab menu.
    /// This implementation for the main portal is based on revision 1.1 of the implementation
    /// for the white label site.
	/// </summary>
	public class TabSelectionManager
	{
        /// <summary>
        /// Constructor.
        /// </summary>
		public TabSelectionManager()
		{

		}

        #region Public methods

        /// <summary>
        /// Determines whether navigation is enabled for the specified page.
        /// Some pages, e.g. error and timeout pages should not display navigation.
        /// </summary>
        /// <param name="pageId">The page for which to determine whether navigation is enabled</param>
        /// <returns>True if navigation enabled, false otherwise.</returns>
        public bool NavigationEnabledForPage(PageId pageId) 
        {
            switch (pageId)
            {
                case PageId.Error:
				case PageId.SorryPage:
                case PageId.Maintenance:
                case PageId.VersionViewer:
                case PageId.LogViewer:
                case PageId.UserSurvey:
                case PageId.TimeOut:
                case PageId.CycleJourneyGPXDownload:
                    return false;
                default:
                    return true;
            }
        }

		/// <summary>
		/// Determines whether login controls for the specified page should be visible.
		/// Some pages, e.g. error and timeout pages should not display the login/logout hyperlinks.
		/// </summary>
		/// <param name="pageId">The page for which to determine whether login controls are visible</param>
		/// <returns>True if login controls are visible, false otherwise.</returns>
		public bool LoginControlsVisibleForPage(PageId pageId) 
		{
			switch (pageId)
			{
				case PageId.Error:
				case PageId.SorryPage:
				case PageId.Maintenance:
				case PageId.VersionViewer:
				case PageId.LogViewer:
				case PageId.UserSurvey:
				case PageId.TimeOut:
				case PageId.WaitPage:
                case PageId.CycleJourneyGPXDownload:
					return false;
				default:
					return true;
			}
		}

        /// <summary>
        /// For the specified page, this method determines which tab is active and sets that tab to be active.
        /// The behaviour of this method is based on the existing portal code which has been refactored out
        /// of existing pages and controls into this method. The idea of a "temporary" active tab is something
        /// carried forward from existing code to minimise the risk of changes introducing bugs. Setting
        /// the temporary active tab through the TransientActiveTab property only changes which tab is visible
        /// for the page and does not change the active tab in session data. The active tab in session data is 
        /// modified in the HeaderControl which actually changes the highlighted tab.
        /// </summary>
        /// <param name="currentActiveTab">The currently active tab</param>
        /// <param name="pageId">The page for which the active tab is being determined</param>
        /// <returns>The active TabSection to be used to set the session data in the HeaderControl</returns>
        public TabSection SelectedTab(TabSection currentActiveTab, PageId pageId) 
        {
            TabSection activeTab;

            switch (pageId) 
            {
                case PageId.LoginRegister:
                    activeTab = TabSection.LoginRegister;
                    break;

                case PageId.Home:
                    activeTab = TabSection.Home;
                    break;

                // Pages that set the active tab by virtue of being loaded
                case PageId.InitialPage:
                case PageId.SiteMap:
                case PageId.SeasonalNoticeBoard:
                    activeTab = currentActiveTab;
                    break;
				case PageId.SpecialNoticeBoard:
					activeTab = TabSection.Home;
					break;

                case PageId.JourneyAdjust:
				case PageId.JourneyAccessibility:
                case PageId.CompareAdjustedJourney:
                case PageId.RetailerInformation:
                case PageId.TicketUpgrade:
				case PageId.FindFlightInput:
				case PageId.FindTrainInput:
				case PageId.FindCoachInput:
				case PageId.FindCarInput:
                case PageId.FindCycleInput:
				case PageId.FindTrunkInput:
                case PageId.FindInternationalInput:
				case PageId.FindFareInput:
				case PageId.FindFareDateSelection:
				case PageId.FindFareTicketSelection:
				case PageId.FindFareTicketSelectionReturn:
				case PageId.FindFareTicketSelectionSingles:
				case PageId.JourneyPlannerInput:
				case PageId.JourneySummary:
				case PageId.JourneyPlannerAmbiguity:
				case PageId.JourneyMap:
				case PageId.JourneyDetails:
				case PageId.JourneyFares:
				case PageId.DetailedLegMap:
				case PageId.ServiceDetails:
				case PageId.WaitPage:
				case PageId.TicketRetailers:
				case PageId.TicketRetailersHandOff:
				case PageId.JPLandingPage:
				case PageId.VisitPlannerInput:
				case PageId.VisitPlannerResults:
				case PageId.ParkAndRide:
				case PageId.ParkAndRideInput:
				case PageId.FindBusInput:
				case PageId.HomePlanAJourney:
                case PageId.JourneyEmissionsCompareJourney:
                case PageId.CycleJourneyDetails:
					activeTab = TabSection.PlanAJourney;
                    break;

                case PageId.TrafficMap:
                case PageId.NetworkMaps:
				case PageId.HomeFindAPlace:
                case PageId.FindMapInput:
                case PageId.FindMapResult:
                    activeTab = TabSection.FindAPlace;
                    break;

                case PageId.TravelNews:
                case PageId.DepartureBoards:
				case PageId.TNLandingPage:
				case PageId.HomeTravelInfo:
                    activeTab = TabSection.TravelInfo;
                    break;

                case PageId.TDOnTheMove:
				case PageId.HomeTipsTools:
				case PageId.FeedbackInitialPage:
				case PageId.ContactUsPage:
				case PageId.Links:
				case PageId.BusinessLinks:
				case PageId.ToolbarDownload:
				case PageId.JourneyEmissionsCompare:
                case PageId.FindEBCInput:
                case PageId.EBCJourneyDetails:
                case PageId.EBCJourneyMap:
                case PageId.Help: // Help(FAQ) should show tipsandtools tab as active tab
                    activeTab = TabSection.TipsAndTools;
                    break;

                // Pages that require specialized logic

				case PageId.JourneyPlannerLocationMap:
					// IR 3540 - added so that Toolbar can land on maps page and show correct header
					InputPageState pageState = TDSessionManager.Current.InputPageState;
					bool journeyPlanning = (pageState.MapMode == CurrentMapMode.FromJourneyInput || 
										pageState.MapMode == CurrentMapMode.FromFindAInput);
				
					switch (currentActiveTab)
                    {
                        case TabSection.PlanAJourney:
							if (journeyPlanning) 
							{
								activeTab = currentActiveTab;
							}
							else
							{
								activeTab = TabSection.FindAPlace;
							}
                            // end IR 3540
							break;
                        default:
                            activeTab = TabSection.FindAPlace;
                            break;
                    }
                    break;


                case PageId.LocationInformation:
				case PageId.FindStationInput:
				case PageId.FindStationResults:
				case PageId.FindStationMap:
					
					switch (currentActiveTab)
					{
						case TabSection.PlanAJourney:
							activeTab = currentActiveTab;
							break;
						default:
							activeTab = TabSection.FindAPlace;
							break;
					}
					
					break;
                
                case PageId.FindCarParkInput:
                    FindCarParkPageState carParkPageState = TDSessionManager.Current.FindCarParkPageState;
                    //default tab
                    activeTab = TabSection.FindAPlace;
                    if (carParkPageState != null)
                    {
                        switch (carParkPageState.CurrentFindMode)
                        {
                            case FindCarParkPageState.FindCarParkMode.DriveFromAndTo:
                            case FindCarParkPageState.FindCarParkMode.DriveTo:
                                activeTab = TabSection.PlanAJourney;
                                break;
                            default:
                                activeTab = TabSection.FindAPlace;
                                break;
                        }
                    }
                    break;

				case PageId.HelpFullJP:
					activeTab = currentActiveTab;
					break;

                // Printable pages do not require tab set, leave active tab unchanged

                case PageId.PrintableJourneySummary:
                case PageId.PrintableJourneyDetails:
                case PageId.PrintableCycleJourneyDetails:
                case PageId.PrintableJourneyMap:
                case PageId.PrintableJourneyMapInput:
                case PageId.PrintableJourneyMapTile:
                case PageId.PrintableCompareAdjustedJourney:
                case PageId.PrintableTicketRetailers:
                case PageId.PrintableJourneyFares:
                case PageId.PrintableTravelNews:
                case PageId.PrintableTrafficMap:
                case PageId.PrintableSoftContent:
                case PageId.PrintableHelpFullJP:
                case PageId.PrintableFindStationResults:
                case PageId.PrintableFindStationMap:
                case PageId.PrintableFindFareDateSelection:
                case PageId.PrintableFindFareTicketSelection:
                case PageId.PrintableFindFareTicketSelectionReturn:
                case PageId.PrintableFindFareTicketSelectionSingles:
                case PageId.PrintableTicketRetailersHandOff:
				case PageId.PrintableJourneyPlannerLocationMap:
				case PageId.PrintableParkAndRide:
				case PageId.PrintableServiceDetails:
				case PageId.PrintableVisitPlannerResults:
				case PageId.PrintableLinks:

                    activeTab = currentActiveTab;
                    break;

                // No tabs visible, will be disabled elsewhere, leave active tab unchanged

                case PageId.Error:
                case PageId.Maintenance:
                case PageId.VersionViewer:
                case PageId.LogViewer:
                case PageId.UserSurvey:
                case PageId.TimeOut:

                    activeTab = currentActiveTab;
                    break;

                // Unused page ids, should never get here, leave active tab unchanged

                case PageId.ClaimsInputPage:
                case PageId.ClaimsErrorsPage:
                case PageId.ClaimPrintPage:
                case PageId.Feedback:
                case PageId.GeneralMaps:
                case PageId.Map:
                case PageId.ClaimsPolicyPrinter:
                case PageId.ClaimsPolicy:
				case PageId.Empty:
				case PageId.HomeSessionAbandoned:
				case PageId.JourneyExtensionLastUndo:
				case PageId.JourneyExtensionAllUndo:
				default:

                    activeTab = currentActiveTab;
                    break;

            }

            // Return the selected tab
            return activeTab;
        }

        #endregion Public methods

	}
}
