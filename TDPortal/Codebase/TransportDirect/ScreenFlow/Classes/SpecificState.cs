// *********************************************** 
// NAME                 : SpecificState.cs 
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : SpecificState class.  This is a management class which return the the pageid of the class to be displayed according to Business logic
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/SpecificState.cs-arc  $
//
//   Rev 1.18   Jan 09 2013 14:11:22   mmodi
//Specific state for Journey ambiguity page to go to Find nearest accessible stop
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.17   Aug 18 2010 16:53:22   RBroddle
//RefineDetailsMap transition event is called when selecting "back" from RefineMap page as well as refine details so changed to make sure out and return ...ShowMap viewstate flags are reset to avoid zoom related javascript being assigned to the onclick event of the leg map hyperlink postback control on the refinedetails page later on.
//Resolution for 5593: ACP - problem displaying route leg maps in extend journey
//
//   Rev 1.16   Mar 12 2010 14:38:34   apatel
//updated GetTDDateTime method to take care of time supplied is not integer value
//Resolution for 5453: Error message displayed when trying to Extend a journey
//
//   Rev 1.15   Feb 25 2010 16:20:52   pghumra
//Code fix applied to resolve issue with date not being displayed on journey details section in the door to door planner when date of travel is different to requested date
//Resolution for 5413: CODE FIX - NEW - DEL 10.x - Issue with seasonal information change from Del 10.8
//
//   Rev 1.14   Dec 02 2009 16:03:14   mmodi
//Added logic to return back to EBC and VisitPlanner pages from Car park and Stop information pages
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Dec 02 2009 11:51:12   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Nov 11 2009 16:49:52   apatel
//updated for stop information mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 05 2009 14:56:20   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 02 2009 17:46:38   mmodi
//Updated for Find map
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Oct 15 2009 13:42:20   apatel
//EBC printer friendly page changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.8   Sep 24 2009 10:18:24   apatel
//update for Map expand button
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.7   Sep 21 2009 14:56:44   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Sep 14 2009 10:30:36   apatel
//Stop Information page related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Nov 05 2008 09:52:54   rbroddle
//Actioning code review comments for CCN460 Better use of seasonal noticeboard
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.4   Oct 14 2008 15:13:48   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jul 15 2008 16:36:18   mmodi
//Added LoginRegister
//Resolution for 5065: Log in issues - Find a map, and Help pages
//
//   Rev 1.2.1.0   Oct 09 2008 14:48:24   rbroddle
//CCN460 Better Use of Seasonal Noticeboard
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.2   May 01 2008 15:05:08   apatel
//added LoginRegister case in DoTransition method.
//Resolution for 4920: Login/Logout page issues
//
//   Rev 1.1   Mar 10 2008 15:26:42   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:47:50   mturner
//Initial revision.
//
//   Rev 1.66   Dec 07 2006 12:00:36   mturner
//Manual Merge for stream4240
//
//   Rev 1.64.2.0   Nov 08 2006 14:49:50   mmodi
//Added JourneyEmission page events
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.64   Oct 16 2006 17:48:44   mmodi
//Added CarParkInformation back event to handle LocationMapBack
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4227: Car Parking: Navigation issue from Find a Map - Car Park Information
//
//   Rev 1.63   Oct 06 2006 13:17:56   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.62.1.8   Sep 30 2006 16:24:30   mmodi
//Added Car park transitions to Refine pages
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4213: Car Parking: Extend journey navigation issue
//
//   Rev 1.62.1.7   Sep 30 2006 13:58:26   mmodi
//Added transition event to return to Journey fares from Car Parki Information
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.62.1.6   Sep 28 2006 17:16:56   mmodi
//Transition event to return back to FindCarParkMap page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.62.1.5   Sep 28 2006 15:31:38   esevern
//Added further TransitionEvent check to the PageId.CarParkInformation switch case statement in DoTransition() - fix for back button on CarParkInformation page returning user to wrong page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.62.1.4   Sep 20 2006 16:53:36   esevern
//Added car park results information page transition event
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.62.1.3   Sep 04 2006 11:48:38   mmodi
//Added FindCarParkResults transition
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.62.1.2   Sep 01 2006 15:36:14   mmodi
//Added further transition for car park information
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.62.1.1   Aug 31 2006 16:26:56   esevern
//added car park information page 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.62.1.0   Aug 02 2006 13:16:22   esevern
//added findcarpark input page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.62   May 26 2006 12:20:36   mmodi
//Change added to always redirect user (SessionAbandon) to Homepage when Logout occurs
//Resolution for 4098: Del 8.2 - Not possible to log out from Door-to-door and Maps ambiguity pages
//
//   Rev 1.61   Apr 05 2006 15:17:42   mdambrine
//Manual merge from stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.60   Mar 24 2006 17:25:30   kjosling
//Merged stream 0023
//
//   Rev 1.59   Mar 13 2006 17:31:14   NMoorhouse
//Manual merge of stream3353 -> trunk
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.58   Feb 10 2006 15:04:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.57.2.15   Mar 13 2006 16:31:24   tmollart
//Updated after code review.
//
//   Rev 1.57.2.14   Mar 01 2006 14:32:20   NMoorhouse
//Changes to support CarDetail post backs
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.13   Feb 24 2006 14:32:40   NMoorhouse
//Changes to support the addition of new page to display CarDetails
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.12   Feb 22 2006 16:07:38   asinclair
//Removed unused code
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.11   Feb 22 2006 09:34:18   NMoorhouse
//Slimmed down version of extend, replan and adjust transitions
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.10   Feb 20 2006 14:41:30   pcross
//Correction to last check-in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.9   Feb 20 2006 12:01:22   pcross
//Added events for Journey Adjust
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.8   Feb 17 2006 14:23:36   NMoorhouse
//Combined case switches for PageId's RefineJourneyPlan and ReplanFullItinerarySummary as we need to do the same processing for TransitionEvent's JourneyReplanOutward and JourneyReplanReturn
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.7   Feb 16 2006 16:40:12   pcross
//Handling transition events for adjust from Summary page and from Refine input page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.6   Feb 16 2006 15:36:46   NMoorhouse
//Added replan summary transitions
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.5   Feb 15 2006 16:15:48   tmollart
//Added replan functionality.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.4   Feb 14 2006 16:25:40   NMoorhouse
//Linking up of pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.2   Feb 09 2006 16:15:18   NMoorhouse
//Rename of ExtendedFullItineraryDetails and Map to RefineDetails and Map
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.1   Feb 07 2006 20:36:46   tmollart
//Added state processing for replan pages.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.2.0   Feb 07 2006 18:42:18   NMoorhouse
//Transitions for Extended Full Itinerary Pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.57.1.1   Jan 17 2006 18:50:12   tmollart
//Updated after code review. Removed commented out code.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.57.1.0   Dec 23 2005 11:51:14   tmollart
//Modified class to group specific transition events and remove redundant code.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.57   Nov 09 2005 12:31:44   build
//Automatically merged from branch for stream2818
//
//   Rev 1.56.1.0   Oct 14 2005 15:11:36   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.56   Aug 19 2005 12:36:06   RWilby
//Merge for stream2572
//
//   Rev 1.55   Aug 16 2005 14:34:16   RWilby
//Merge for stream2556
//
//   Rev 1.54   Aug 04 2005 15:22:58   asinclair
//Added tdonthemove to the loaction page so header link works
//
//   Rev 1.53   May 23 2005 17:22:56   rgreenwood
//IR2500: FindAFare back button, moved JourneyDetails and JourneyMaps transition event to be same as JourneySummary, when in FindAFare mode
//Resolution for 2500: PT - Back Button Missing in Find-a-Fare
//
//   Rev 1.52   Apr 27 2005 12:28:58   rgeraghty
//Updated WaitPage and FindFareTicketSelection page sections to add a FindCostBasedPageState search result error when no journeys are found and no search errors are returned
//Resolution for 2066: PT - Train - Fare Ticket Selection - text needs to change when no tickets available
//
//   Rev 1.51   Apr 20 2005 14:49:38   rgeraghty
//Code added for CostSearchError handling
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.50   Apr 20 2005 10:31:22   RPhilpott
//Changes to CostSearchResult error handling. 
//
//   Rev 1.49   Apr 19 2005 15:35:38   rgeraghty
//Changes made to TransitionEvent.FindFareServicesWaitingRefresh in WaitPage section to added a custom CostSearchError
//Resolution for 2106: PT- Find a fare will not progress from Step 2: Select a Fare page to Step 3: Select a Journey page
//
//   Rev 1.48   Apr 19 2005 13:21:58   rhopkins
//Set OneUseKey "FirstViewingOfResults" to "yes" when transitioning to the results pages, so that the default selected result will be determined.
//Resolution for 2160: PT - Coach - Bottom journey in list selected as default rather than the top on Find a Fare(Coach)
//
//   Rev 1.47   Apr 15 2005 12:47:26   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.46   Apr 15 2005 11:57:16   rgeraghty
//Changes made for JourneySummary and FindFareDateSelection pages to use corrected ticket type
//Resolution for 2148: PT - Fares Journey Planner tries to sell day returns for journeys seperated by a day
//
//   Rev 1.45   Apr 11 2005 16:38:44   jmorrissey
//Change to TransitionEvent.FindFareServicesWaitingRefresh
//Resolution for 2064: PT: Find Fare query does not go to results page for a particular journey
//
//   Rev 1.44   Apr 11 2005 08:38:50   rgeraghty
//Case statement added for Journey Summary page
//Resolution for 2058: PT: Back button missing from the Find Fare Results (Summary) Page
//
//   Rev 1.43   Apr 07 2005 14:40:10   rhopkins
//Need to direct to FindFareTicketSelectionReturn or FindFareTicketSelectionSingles if the appropriate ticket type has been selected
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.42   Mar 29 2005 10:10:10   COwczarek
//Remove assignment of journey request/result objects to
//session for cost based searches. This is now done by the
//cost search runner component.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.41   Mar 18 2005 15:43:10   COwczarek
//Change transition event processing for
//FindFareServicesWaitingRefresh and  FindFareServiceResults
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.40   Mar 17 2005 17:50:16   rhopkins
//Make FindFareDateSelection page use same specific transition handling as is used for FindFareInput
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.39   Mar 17 2005 17:16:00   rscott
//Added line to deal with TDOnTheMove
//
//   Rev 1.38   Mar 17 2005 16:59:46   COwczarek
//Work in progress
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.37   Mar 15 2005 08:37:48   tmollart
//Changed code to access the CostSearchWaitControlData and the CostSearchWaitStateData on the TDSessionManager object as opposed to the FindCostBasedPage state object.
//
//   Rev 1.36   Mar 11 2005 14:47:42   COwczarek
//Add processing for find fare ticket selection pages
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.35   Mar 10 2005 18:29:04   tmollart
//Added state processing for Find A Fare.
//
//   Rev 1.34   Mar 09 2005 17:42:00   rscott
//DEL 7 - Case statements combined and new method GetPageIdForTransitionEvent utilised.
//
//   Rev 1.32   Mar 08 2005 10:49:38   rscott
//DEL7 - New Transition Events Added for wait page FindFareWaitingRefresh, FindFareServicesWaitingRefresh.
//
//   Rev 1.31   Oct 15 2004 12:35:08   jgeorge
//Changed to use JourneyPlanStateData as well as JourneyPlanControlData for Wait page
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.30   Oct 13 2004 12:11:36   jmorrissey
//Added handling for JourneyPrintBack transition event on the User Survey page
//
//   Rev 1.29   Aug 27 2004 15:52:16   COwczarek
//Changes to DoTransition method to correctly redirect to Find A
//Resolution for 1320: Find a train station information page header
//
//   Rev 1.28   Aug 25 2004 14:50:12   COwczarek
//Changes to DoTransition method to correctly redirect to Find A
//train input page when clicking Find A tab on journey planner
//ambiguity page.
//Resolution for 1320: Find a train station information page header
//
//   Rev 1.27   Aug 24 2004 14:20:24   COwczarek
//Changes to DoTransition method to allow specific pages
//to correctly redirect to Find A input pages.
//Resolution for 1320: Find a train station information page header
//
//   Rev 1.26   Aug 20 2004 12:27:46   jgeorge
//IR1338
//
//   Rev 1.25   Jun 23 2004 12:23:34   jgeorge
//Added FindADefault transition event to DoTransition method
//
//   Rev 1.24   Mar 15 2004 19:00:52   pnorell
//Updated for the new event.
//
//   Rev 1.23   Dec 16 2003 09:52:22   PNorell
//Fixed map page states
//
//   Rev 1.22   Nov 19 2003 14:41:00   kcheung
//Header page ids added for Location Information,.
//
//   Rev 1.21   Oct 22 2003 12:20:34   RPhilpott
//Improve CJP error handling
//
//   Rev 1.20   Oct 16 2003 13:22:54   passuied
//added events for HelpfullJP
//
//   Rev 1.19   Oct 10 2003 15:15:08   passuied
//Added handler for JourneyPlannerInputDefault event in Wait Page
//
//   Rev 1.18   Oct 09 2003 18:20:58   passuied
//fixed wait page screen flow pb
//
//   Rev 1.17   Oct 07 2003 14:08:26   kcheung
//Added FindJourneys TransitionEvent for JourneyPlannerLocationMap
//
//   Rev 1.16   Sep 30 2003 20:12:32   asinclair
//Added code for switching between JP and Map screen
//
//   Rev 1.15   Sep 30 2003 09:56:38   passuied
//added some events handling for ambiguity page
//
//   Rev 1.14   Sep 29 2003 17:26:56   asinclair
//Added code to control the flow from JP Map to Maps Map
//
//   Rev 1.13   Sep 25 2003 18:06:50   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.12   Sep 25 2003 11:48:52   PNorell
//Removed excessive logging.
//
//   Rev 1.11   Sep 25 2003 11:47:02   PNorell
//added logging.
//
//   Rev 1.10   Sep 24 2003 15:04:36   passuied
//Added default case in ambiguity page events
//
//   Rev 1.9   Sep 24 2003 13:17:38   kcheung
//Added Default for JourneyPlannerLocationMap
//
//   Rev 1.8   Sep 23 2003 19:00:56   RPhilpott
//Add LocationInformation code
//
//   Rev 1.7   Sep 23 2003 18:38:32   passuied
//corrected duplicated transitionevents case
//
//   Rev 1.6   Sep 23 2003 16:54:32   passuied
//coded business logic for ambiguity page

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyPlanRunner;
using Logger = System.Diagnostics.Trace;
using System.Collections;

namespace TransportDirect.UserPortal.ScreenFlow.State
{
	/// <summary>
	/// Summary description for SpecificState.
	/// </summary>
	public class SpecificState : TDScreenState
	{
		public SpecificState(PageId pageId) : base (pageId)
		{
		}

		/// <summary>
		/// Overrides the abstract method.  Returns a pageId
		/// depending on the TransitionEvent that is currently
		/// held in the FormShift area.
		/// </summary>
		/// <returns>PageId of the next page.</returns>
		public override PageId DoTransition()
		{
			// Get the session from ServiceDiscovery.
			// Retrive the session from Service Discovery
			ITDSessionManager session = (ITDSessionManager)TDSessionManager.Current;

			// Get the TransitionEvent in the FormShift
			TransitionEvent transitionEvent = session.FormShift[SessionKey.TransitionEvent];

			PageId returnPageId = PageId.Empty;

			// The following events are not dependant on the current page and
			// just return the page ID for the transition event. There is no
			// need to perform any specific state processing when transitioning
			// to these pages.
			switch (transitionEvent)
			{
				// Tabs added as part of Home Page Phase 2
				case TransitionEvent.TipsToolsTab:
				case TransitionEvent.FindAPlaceTab:
				case TransitionEvent.TravelInfoTab:
				case TransitionEvent.PlanAJourneyTab:
				// Standard transition events
				case TransitionEvent.GoMap :
				case TransitionEvent.GoHome :
				case TransitionEvent.FindADefault :
				case TransitionEvent.LocationMapDefault :
				case TransitionEvent.FindCarInputDefault :
				case TransitionEvent.FindFareInputDefault :
				case TransitionEvent.FindCoachInputDefault :
				case TransitionEvent.FindTrainInputDefault :
				case TransitionEvent.FindTrunkInputDefault :
				case TransitionEvent.FindFlightInputDefault :
				case TransitionEvent.FindStationInputDefault :
				case TransitionEvent.FindBusInputDefault :
				case TransitionEvent.FindCarParkInputDefault :
                case TransitionEvent.FindEBCInputDefault :
                case TransitionEvent.FindMapInputDefault :
				case TransitionEvent.JourneyPlannerAmbiguityMap:
				case TransitionEvent.JourneyPlannerInputDefault :
				case TransitionEvent.SessionAbandon :
                case TransitionEvent.LoginRegister :
					returnPageId = GetPageIdForTransitionEvent();
					break;
			}

			// Test if a page ID was returned from above. If not then continue
			// with specific state processing to determine the return page ID.
			if (returnPageId == PageId.Empty)
			{
				switch (pageId)
				{
					case PageId.JourneyPlannerLocationMap :
						#region

						// Check what the transition event
					switch(transitionEvent)
					{
						case TransitionEvent.LocationMapBack :
							Stack returnStack =
								session.InputPageState.JourneyInputReturnStack;
							if(returnStack.Count == 0)
								returnPageId = PageId.JourneyPlannerLocationMap;
							else
								returnPageId = (PageId)returnStack.Pop();
							break;

						case TransitionEvent.LocationInformation:
						case TransitionEvent.LocationMapStationInfo :
							returnPageId = PageId.LocationInformation;
							break;

						case TransitionEvent.LocationMapMoreDetail :
							returnPageId = PageId.JourneyPlannerInput;
							break;

						case TransitionEvent.LocationMapUseLocation :
							if(session.InputPageState.MapMode == CurrentMapMode.FromJourneyInput)
								returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
							else
								returnPageId = PageId.JourneyPlannerLocationMap;
							break;
						case TransitionEvent.LocationMapFindJourneys :
							returnPageId = PageId.WaitPage;
							break;
						case TransitionEvent.JourneyPlannerInputErrors :
							returnPageId = PageId.JourneyPlannerAmbiguity;
							break;
						case TransitionEvent.CarParkInformation :
							returnPageId = PageId.CarParkInformation;
							break;
						
						default :
							returnPageId = PageId.JourneyPlannerLocationMap;
							break;
					}

						break;
						#endregion

					case PageId.WaitPage :
						#region
					{
						switch (transitionEvent)
						{
							case TransitionEvent.WaitingRefresh:
								AsyncCallState acs = session.AsyncCallState;
								if (acs == null)
									returnPageId = PageId.Error;
								else
									returnPageId = acs.ProcessState();
								break;
							default :
								returnPageId = GetPageIdForTransitionEvent();
								break;
						}

						break;
					
					}
						#endregion

					case PageId.JourneyPlannerAmbiguity:
						#region
					{
						switch (transitionEvent)
						{
							case TransitionEvent.JourneyPlannerAmbiguityBack:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyPlannerInput;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.JourneyPlannerAmbiguityFind:
							{
								if (session.ValidationError.ErrorIDs.Length==0)
								{
									returnPageId = PageId.WaitPage;

								}
								else
								{
									returnPageId = PageId.JourneyPlannerAmbiguity;
								}
								break;
							}
							case TransitionEvent.FindAInputRedirectToResults:
							{
								returnPageId = PageId.JourneySummary;
								break;
							}
                            case TransitionEvent.JourneyPlannerInputToMap:
                            {
                                returnPageId = PageId.FindMapInput;
                                break;
                            }
                            case TransitionEvent.FindNearestAccessibleStop:
                            {
                                returnPageId = PageId.FindNearestAccessibleStop;
                                break;
                            }
							default:
								returnPageId = PageId.JourneyPlannerAmbiguity;
								break;
						}
						break;
					}
						#endregion
					
					case PageId.LocationInformation:
						#region
					{
						switch (transitionEvent)
						{
						
							case TransitionEvent.LocationInformationBack:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyPlannerInput;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}
							default:
								returnPageId = PageId.LocationInformation;
								break;
						}
						break;
					}
						#endregion
                    case PageId.StopInformation:
                    #region
                    {
                        switch (transitionEvent)
                        {
                            case TransitionEvent.JourneyPlannerInputErrors:
                            case TransitionEvent.JourneyPlannerInputOK:
                            case TransitionEvent.JourneyPlannerInputDefault:
                            case TransitionEvent.LocationMapUseLocation:
                            case TransitionEvent.TDOnTheMove:
                            case TransitionEvent.StopServiceDetails:
                            case TransitionEvent.FindMapResult:
                                returnPageId = GetPageIdForTransitionEvent();
                                break;

                            case TransitionEvent.StopInformationBack:
                                {
                                    if (session.InputPageState.JourneyInputReturnStack.Count == 0)
                                    {
                                        returnPageId = PageId.JourneyPlannerInput;
                                    }
                                    else
                                    {
                                        returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();

                                        switch (returnPageId)
                                        { 
                                            case PageId.EBCJourneyDetails:
                                            case PageId.VisitPlannerResults:
                                                // Indicate to the page to display the map
                                                session.SetOneUseKey(SessionKey.MapView, "true");
                                                break;
                                        }
                                    }
                                    break;
                                }
                            default:
                                returnPageId = PageId.StopInformation;
                                break;
                        }
                        break;
                    }
                    #endregion
					case PageId.CarParkInformation:
						#region
					{
						switch (transitionEvent)
						{
						
							case TransitionEvent.FindCarParkInputUnambiguous:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.FindCarParkResults;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.FindCarParkResultsShowMap:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.FindCarParkMap;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.GoJourneyFares:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyFares;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.CarParkInformationBack:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.FindCarParkInput;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.GoJourneyDetails:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyDetails;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.GoJourneyMap:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyMap;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.RefineDetailsSchematic:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.RefineDetails;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.RefineTicketsView:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.RefineTickets;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.RefineMapView:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.RefineMap;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

                            case TransitionEvent.FindMapResult:
                            {
                                if (session.InputPageState.JourneyInputReturnStack.Count == 0)
                                {
                                    returnPageId = PageId.FindMapResult;
                                }
                                else
                                {
                                    returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
                                }
                                break;
                            }

                            case TransitionEvent.EBCJourneyDetails:
                            {
                                returnPageId = PageId.EBCJourneyDetails;
                                break;
                            }

                            case TransitionEvent.EBCJourneyMap:
                            {
                                returnPageId = PageId.EBCJourneyMap;
                                break;
                            }

                            case TransitionEvent.CycleJourneyDetails:
                            {
                                returnPageId = PageId.CycleJourneyDetails;
                                break;
                            }

                            case TransitionEvent.VisitPlannerResultsMapView:
                            {
                                returnPageId = PageId.VisitPlannerResults;
                                break;
                            }

							case TransitionEvent.LocationMapBack:
							{
								returnPageId = PageId.JourneyPlannerLocationMap;
								break;
							}

							default:
								returnPageId = PageId.FindCarParkInput;
								break;
						}
						break;
					}
						#endregion
					case PageId.ServiceDetails:
                    case PageId.StopServiceDetails:
					case PageId.CarDetails:
						#region
					{
						switch (transitionEvent)
						{
							case TransitionEvent.ServiceDetailsBack:
							case TransitionEvent.CarDetailsBack:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyPlannerInput;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}
							default:
								returnPageId = GetPageIdForTransitionEvent();
								break;
						}
						break;
					}
						#endregion
					
					case PageId.HelpFullJP:
						#region
					{
						switch (transitionEvent)
						{
							// by default return to previous page
							case TransitionEvent.HelpFullJPBack:
							{
								returnPageId = (PageId) session.InputPageState.JourneyInputReturnStack.Pop();
								break;
							}

							default:
							{
								returnPageId = pageId;
								break;
							}
						}
						break;
					}
						#endregion
					
					case PageId.UserSurvey :
						#region
					{
						switch (transitionEvent)
						{ 
							case TransitionEvent.JourneyPrintBack :

								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									//if no page id found on stack, go to PrintableJourneySummary page
									returnPageId = PageId.PrintableJourneySummary;
								}
								else
								{
									//return to specific printable page that we came from
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
					
							default:
							{
								returnPageId = pageId;
								break;
							}
						}
						break;
					}
						#endregion
					
					case PageId.FindFareInput :
		case PageId.FindTrainCostInput :			case PageId.FindFareDateSelection :
						#region
					switch (transitionEvent)
					{
							//If a user has submited a journey request from the FindFare input page we
							//need to test the instansiated session object to test the status of the
							//search.
						case TransitionEvent.FindFareAmbiguityResolution :
case TransitionEvent.FindTrainCostAmbiguityResolution :
							AsyncCallState acs = session.AsyncCallState;
							if (acs == null)
								returnPageId = PageId.Error;
							else 
								returnPageId = acs.ProcessState();
							break;

						case TransitionEvent.FindFareTicketSelectionDefault :
							FindCostBasedPageState pageState = (FindCostBasedPageState)session.FindPageState;
						switch (pageState.SelectedTravelDate.TravelDate.TicketType)
						{
							case TicketType.Return :
								returnPageId = PageId.FindFareTicketSelectionReturn;
								break;
							case TicketType.Singles :
								returnPageId = PageId.FindFareTicketSelectionSingles;
								break;
							default :
								returnPageId = PageId.FindFareTicketSelection;
								break;
						}
							break;

							//Look up the Page Id of the transition event and rediect user to that location.
						default :
							returnPageId = GetPageIdForTransitionEvent();
							break;
					}
						break;
						#endregion
					
					case PageId.JourneySummary:
					case PageId.JourneyDetails:
					case PageId.JourneyMap:
						#region
					switch (transitionEvent)
					{
						case TransitionEvent.JourneySummaryTicketSelectionBack:
						
							FindCostBasedPageState pageState = (FindCostBasedPageState)session.FindPageState;
						switch (pageState.SelectedTravelDate.TravelDate.TicketType)
						{
							case TicketType.Return :
								returnPageId = PageId.FindFareTicketSelectionReturn;
								break;
							case TicketType.Singles :
								returnPageId = PageId.FindFareTicketSelectionSingles;
								break;
							default :
								returnPageId = PageId.FindFareTicketSelection;
								break;
						}
							break;

						case TransitionEvent.FindCarParkResultsInfo:
							returnPageId = PageId.CarParkInformation;
							break;
						
							//Look up the Page Id of the transition event and redirect user to that location.
						default :
							returnPageId = GetPageIdForTransitionEvent();
							break;
					}
						break;

						#endregion
					
					case PageId.JourneyFares:
						#region
					switch (transitionEvent)
					{	
						case TransitionEvent.CarParkInformation:
							returnPageId = PageId.CarParkInformation;
							break;
						default :
							returnPageId = GetPageIdForTransitionEvent();
							break;
					}
						break;
						#endregion
					
					case PageId.JourneyEmissions:
						#region
					{
						switch (transitionEvent)
						{
						
							case TransitionEvent.JourneyEmissionsBack:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneySummary;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.GoJourneyFares:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyFares;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.GoJourneyDetails:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.JourneyDetails;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.RefineDetailsSchematic:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.RefineDetails;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							case TransitionEvent.RefineTicketsView:
							{
								if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
								{
									returnPageId = PageId.RefineTickets;
								}
								else
								{
									returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
								}
								break;
							}

							default:
								returnPageId = PageId.JourneyPlannerInput;
								break;
						}
						break;
					}
						#endregion			
					
					case PageId.FindFareTicketSelection:
					case PageId.FindFareTicketSelectionReturn:
					case PageId.FindFareTicketSelectionSingles:
						#region
					switch (transitionEvent)
					{

						case TransitionEvent.FindFareServiceResults :
							FindCostBasedPageState pageState = (FindCostBasedPageState)session.FindPageState;
							AsyncCallState acs = session.AsyncCallState;
							if (acs == null)
								returnPageId = PageId.Error;
							else 
								returnPageId = acs.ProcessState();
							break;

						default :
							//Look up the Page Id of the transition event and rediect user to that location.
							returnPageId = GetPageIdForTransitionEvent();
							break;
					}
						break;
						#endregion
					
					case PageId.JourneyAccessibility :
						#region
					switch (transitionEvent)
					{
						case TransitionEvent.JourneyAccessibilityBack :
							if ( session.InputPageState.JourneyInputReturnStack.Count == 0)
							{
								//if no page id found on stack, go to default page
								returnPageId = PageId.InitialPage;
							}
							else
							{
								//return to specific printable page that we came from
								returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
							}

							break;
						
							//Look up the Page Id of the transition event and redirect user to that location.
						default :
							returnPageId = GetPageIdForTransitionEvent();
							break;
					}
						break;
						#endregion
					case PageId.RefineJourneyPlan :
					case PageId.ExtendedFullItinerarySummary :
					case PageId.ExtensionResultsSummary :
					case PageId.AdjustFullItinerarySummary :
					case PageId.ReplanFullItinerarySummary :
					case PageId.RefineDetails :
					case PageId.RefineMap :
					case PageId.RefineTickets :
						#region
						ITDSessionManager sessionManager = TDSessionManager.Current;
						switch (transitionEvent)
						{
							case TransitionEvent.JourneyReplanOutward:
							case TransitionEvent.JourneyReplanReturn:
								#region
								ReplanPageState pageState;
				
								// Get a reference to session manager and create required itinerary
								// manager.
								sessionManager.ItineraryMode = ItineraryManagerMode.Replan;
								ReplanItineraryManager im = (ReplanItineraryManager)sessionManager.ItineraryManager;
								im.CreateItinerary(sessionManager, (transitionEvent == TransitionEvent.JourneyReplanOutward));

								pageState = (ReplanPageState)sessionManager.InputPageState;

								pageState.OriginalRequest = sessionManager.JourneyRequest;
				
								// Decide if journey should be replanned or user redirected to the
								// replan input page.
								if (im.JourneyForReplan.JourneyLegs.Length == 1)
								{
									// Journey must be a road journey or only have one leg. So run
									// replan direct from start to end. Calling the runner will transition
									// the user to the wait page.
									
									// As there is only one leg configure the page state object
									// to replan the single leg.
									pageState.ReplanStartJourneyDetailIndex = 0;
									pageState.ReplanEndJourneyDetailIndex = 0;

									ReplanRunner runner = new ReplanRunner();

									runner.RunReplan(
										pageState,
										sessionManager.JourneyResult,
										im,
										null,
										null);

									returnPageId = PageId.WaitPage;
								}
								else
								{
									// User must go to the replan input page.
									returnPageId = PageId.JourneyReplanInputPage;
								}

								break;
								#endregion

							case TransitionEvent.JourneyAdjustOutward :
							case TransitionEvent.JourneyAdjustReturn :
								// Reset the adjust state and note the journey direction so we can load the JourneyAdjust screen
								// with the outward/return journey as appropriate
								TDSessionManager.Current.CurrentAdjustState = new TDCurrentAdjustState(TDSessionManager.Current.JourneyRequest);
								TDSessionManager.Current.CurrentAdjustState.CurrentAmendmentType = 
									(transitionEvent == TransitionEvent.JourneyAdjustOutward ?
									TDAmendmentType.OutwardJourney : TDAmendmentType.ReturnJourney);
								
								returnPageId = PageId.JourneyAdjust;
								break;

							case TransitionEvent.ExtendJourneyInputNext:
							case TransitionEvent.ExtendJourneyInputStart:
							case TransitionEvent.ExtendJourneyInputEnd:
								#region
								if (sessionManager.ItineraryMode != ItineraryManagerMode.ExtendJourney)
								{
									sessionManager.ItineraryMode = ItineraryManagerMode.ExtendJourney;
									
                                    TDJourneyParameters journeyParams = TDSessionManager.Current.JourneyParameters;
                                    TDDateTime outwardDateTime = GetTDDateTime(journeyParams.OutwardMonthYear, journeyParams.OutwardDayOfMonth, journeyParams.OutwardHour, journeyParams.OutwardMinute);
                                    TDDateTime returnDateTime = GetTDDateTime(journeyParams.ReturnMonthYear, journeyParams.ReturnDayOfMonth, journeyParams.ReturnHour, journeyParams.ReturnMinute);
                                    
                                    TDItineraryManager.Current.CreateItinerary();

                                    TDSessionManager.Current.InputPageState.OriginalOutwardDateTime = outwardDateTime;
                                    TDSessionManager.Current.InputPageState.OriginalReturnDateTime = returnDateTime;
								}
								
								if (transitionEvent == TransitionEvent.ExtendJourneyInputStart )
								{
									ExtendItineraryManager.Current.ExtendToItineraryStartPoint();
									returnPageId = PageId.ExtendJourneyInput;
								}
								else if (transitionEvent == TransitionEvent.ExtendJourneyInputEnd )
								{
									ExtendItineraryManager.Current.ExtendFromItineraryEndPoint();
									returnPageId = PageId.ExtendJourneyInput;
								}
								else
								{
									returnPageId = PageId.ExtendJourneyInput;
								}
								break;
								#endregion

							case TransitionEvent.RefineDetailsSchematic :
								TDItineraryManager.Current.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode = true;
								TDItineraryManager.Current.JourneyViewState.ShowReturnJourneyDetailsDiagramMode = true;
								returnPageId = PageId.RefineDetails;
								break;

							case TransitionEvent.RefineDetailsTabular :
								TDItineraryManager.Current.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode = false;
								TDItineraryManager.Current.JourneyViewState.ShowReturnJourneyDetailsDiagramMode = false;
								returnPageId = PageId.RefineDetails;
								break;
							
							case TransitionEvent.RefineDetailsBack :
								#region
								TDItineraryManager itineraryManager = TDItineraryManager.Current;
								if (itineraryManager is ExtendItineraryManager)
								{
									if (itineraryManager.ExtendInProgress)
									{
										returnPageId = PageId.ExtensionResultsSummary;
									}
									else
									{
										returnPageId = PageId.ExtendedFullItinerarySummary;
									}
								}
								else if (itineraryManager is ReplanItineraryManager)
								{
									returnPageId = PageId.ReplanFullItinerarySummary;
								}
								else
								{
									returnPageId = PageId.AdjustFullItinerarySummary;
								}

                                //fix for IR5593 - this transition event is called when selecting "back" from 
                                //RefineMap page as well as refine details so make sure viewstate flags are reset
                                itineraryManager.JourneyViewState.OutwardShowMap = false;
                                itineraryManager.JourneyViewState.ReturnShowMap = false;

								break;

								#endregion

							default :
								returnPageId = GetPageIdForTransitionEvent();
								break;
						}
						break;
						#endregion

					case PageId.FindCarParkResults :
					case PageId.FindCarParkMap :
						#region
					{
						switch (transitionEvent)
						{
								// by default return to previous page
							case TransitionEvent.CarParkInformation :
							{
								returnPageId = PageId.CarParkInformation;
								break;
							}

							default:
							{
								returnPageId = GetPageIdForTransitionEvent();
								break;
							}
						}
						break;
					}
						#endregion
                // loginRegister case added to handle loginregisterback transition event.
                case PageId.LoginRegister:
                    #region
                    {
                        switch (transitionEvent)
                        {
                            // by default return to previous page
                            case TransitionEvent.LoginRegisterBack:
                                {
                                    returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
                                    break;
                                }

                            default:
                                {
                                    returnPageId = pageId;
                                    break;
                                }
                        }
                        break;
                    }
                    #endregion

                    case PageId.SeasonalNoticeBoard:
                        #region
                    switch (transitionEvent)
                    {
                        case TransitionEvent.SeasonalNoticeBoardBack:
                            if (session.InputPageState.JourneyInputReturnStack.Count == 0)
                            {
                                //if no page id found on stack, go to default page
                                returnPageId = PageId.InitialPage;
                            }
                            else
                            {
                                //return to specific page that we came from
                                returnPageId = (PageId)session.InputPageState.JourneyInputReturnStack.Pop();
                            }

                            break;

                        //Look up the Page Id of the transition event and redirect user to that location.
                        default:
                            returnPageId = GetPageIdForTransitionEvent();
                            break;
                    }
                    break;
                    #endregion

					default:
						#region
						returnPageId = PageId.InitialPage;
						break;
						#endregion
				}
			}

			return returnPageId;
		}

        /// <summary>
        /// Creates a date time from the day and time passed
        /// </summary>
        /// <param name="monthYear"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        private TDDateTime GetTDDateTime(string monthYear, string day, string hour, string minute)
        {
            int intyear, intmonth, intday, inthour, intminute;
            string[] monthyearsplit = monthYear.Split(new string[] { "/" }, StringSplitOptions.None);
            if (monthyearsplit.Length == 2)
            {
                if (!int.TryParse(monthyearsplit[0], out intmonth))
                {
                    intmonth = 0;
                }

                if (!int.TryParse(monthyearsplit[1], out intyear))
                {
                    intyear = 0;
                }

                if (!int.TryParse(day, out intday))
                {
                    intday = 0;
                }
                
                inthour = 0;
                if (!int.TryParse(hour, out inthour))
                {
                    inthour = 0;
                }

                if(!int.TryParse(minute, out intminute))
                {
                    intminute = 0;
                }

                if (intmonth != 0 && intyear != 0 && intday != 0)
                {
                    return new TDDateTime(intyear, intmonth, intday, inthour, intminute, 0);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
	}
}


