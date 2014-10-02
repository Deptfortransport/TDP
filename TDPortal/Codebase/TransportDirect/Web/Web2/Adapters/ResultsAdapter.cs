// *********************************************** 
// NAME                 : ResultsAdapter.cs 
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 5 October 2005
// DESCRIPTION			: Provides helper/adapter methods for use by the results pages.
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ResultsAdapter.cs-arc  $
//
//   Rev 1.13   Mar 22 2013 10:48:58   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.12   Feb 16 2010 11:15:34   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Oct 21 2009 08:45:32   apatel
//set landing page links autoplan option to off
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.10   Oct 06 2009 14:38:26   apatel
//Social bookmarking changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.9   Oct 01 2009 08:42:04   apatel
//Comments added for new methods relating to find nearest landing page
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.8   Sep 30 2009 16:25:44   apatel
//Social book marking added to Find Car Park Results
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.7   Sep 30 2009 11:46:04   apatel
//Added comments for the methods
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.6   Sep 30 2009 09:06:46   apatel
//CCN 530 Social Bookmarking code changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.5   Sep 28 2009 10:08:18   PScott
//CCN 530 Social Bookmarking
//
//   Rev 1.4   Nov 12 2008 15:02:06   pscott
//SCR5157   add bookmark on all miniplanner pages
//Resolution for 5157: Bookmarks in mini planners
//
//   Rev 1.3   Oct 13 2008 16:41:28   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Jul 30 2008 11:10:46   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:28   mturner
//Initial revision.
//
//   Rev 1.24   Apr 28 2006 13:27:18   COwczarek
//Changes to pass summary type to FormattedJourneySummaryLines constructor.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.23   Apr 21 2006 11:40:28   jbroome
//Updated for Find a Bus bookmark link
//Resolution for 3939: DN093 Find A Bus: Recalling bookmark lands user on door to door results page displaying train journeys
//
//   Rev 1.22   Apr 12 2006 10:33:58   mdambrine
//Made the bookmark link visible for find a bus
//Resolution for 3877: DN093 Find a Bus: Bookmark journey link not shown
//
//   Rev 1.21   Apr 04 2006 15:50:36   RGriffith
//IR3701 Fix: Addition of direction to FormattedJourneySummaryLine adapter to set correct origin/destination locations
//
//   Rev 1.20   Apr 03 2006 15:52:58   rwilby
//Updated comment in the CanShowClientLink method.
//
//   Rev 1.19   Mar 29 2006 14:33:26   pcross
//Updated to get sort order correct on RefineJourneyPlan
//Resolution for 3723: DN068 Adjust: Order of journey legs switched on 'Customise your journey' page
//
//   Rev 1.18   Mar 24 2006 13:59:28   pcross
//CR update
//
//   Rev 1.17   Mar 22 2006 20:27:50   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.16   Mar 21 2006 17:45:18   jmcallister
//Removed coach naptan restrictions as they are no longer necessary.
//
//   Rev 1.15   Mar 20 2006 18:03:38   pcross
//FxCop updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.14   Mar 13 2006 15:35:54   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.12   Mar 07 2006 15:47:34   pcross
//outward and return itinerary summary results should not be ordered - they should appear as they are populated - with the outward line before the return. (Actually even specifying None ends in a sort but this is on leave date only so this works ok.)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.11   Mar 06 2006 22:04:36   rhopkins
//Revert to sorting by DisplayNumber, because that was not what was causing the selection problem.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.10   Mar 02 2006 15:27:28   pcross
//CJP error message handling
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.9   Mar 02 2006 15:08:54   tolomolaiye
//Modified sort order for journey results
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.8   Feb 27 2006 15:17:48   pcross
//Corrections to corrections!
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.7   Feb 24 2006 10:33:54   pcross
//Corrections to setting up formatted results summary lines
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.6   Feb 16 2006 17:06:56   pcross
//Updated the GetRequiredJourney method to allow amended journeys to be retrieved for adjusted journeys.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.5   Feb 16 2006 10:30:44   pcross
//Added PreInterchange check method.
//Corrected the sort order of formatted results lines
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.4   Feb 15 2006 16:25:28   tmollart
//Work in progress for replan functionality.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.3   Feb 03 2006 14:43:44   pcross
//New method AtInterchange to recognise interchanges in journeys (where a short walk has been removed by the CJP)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.2   Jan 27 2006 13:53:28   pcross
//New methods to get formatted journey lines from the session manager:
//
//FormattedOutwardJourneylines
//FormattedReturnJourneylines
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.1   Jan 19 2006 15:43:02   asinclair
//Added methods for populating FormattedJourneySummaryLines for selected Public Outward and Return Journeys
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7.2.0   Dec 15 2005 20:19:54   rhopkins
//Additional methods for getting FormattedJourneySummaryLines from the Itinerary Manager.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13   Feb 23 2006 17:15:10   RWilby
//Merged stream3129
//
//   Rev 1.12   Jan 24 2006 12:23:02   jmcallister
//IR3507 - cleaned up the bookmark url created by client links for JP Landing page
//Resolution for 3507: Client links unnecessary dt parameter and extra ampresand
//
//   Rev 1.11   Jan 05 2006 17:45:52   tolomolaiye
//Code review updates for Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.10   Jan 05 2006 12:31:12   jbroome
//Added EscapeJavaScriptQuotes method.
//Resolution for 3420: Client Links: Apostrophy in bookmark causes error page
//
//   Rev 1.9   Dec 15 2005 16:23:58   jgeorge
//Rejigged code to stop link from being displayed if either the origin or destination location includes any coach naptans.
//Resolution for 3367: Client Links: Bookmark for (Any Train/Coach) to (Any Train) fails when launched
//
//   Rev 1.8   Dec 13 2005 11:26:42   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.7.1.2   Dec 06 2005 11:13:14   asinclair
//No longer uses encrypted partner id and destination
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.7.1.1   Nov 30 2005 16:59:16   jbroome
//Updated to use GetLandingPageURL() method of LandingPage helper
//
//   Rev 1.7.1.0   Nov 23 2005 11:18:58   jgeorge
//Added functions for setting up client link control
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.7   Oct 29 2005 15:36:44   jbroome
//Updated SetSelectedOutwardJourneyIndex()
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Oct 28 2005 17:11:06   tolomolaiye
//Updates following code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 26 2005 11:32:52   tmollart
//Refactored PopulateErrorMessagesControl to accept an itinerary manager object.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 18 2005 18:02:46   jbroome
//Sets the SelectedOutwardJourney to the same index as the SelectedOutwardJourneyId
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 18 2005 15:05:44   tolomolaiye
//Changed FormattedJourneyLines to JourneyLines
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 07 2005 16:12:30   tolomolaiye
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 06 2005 11:18:12   tolomolaiye
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner

using System.Collections;
using System;
using System.Web;
using System.Text;
using System.Globalization;
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// This class provides helper/adapter methods for use by results pages.
	/// These methods are not visit planner specific.
	/// </summary>
	public class ResultsAdapter : TDWebAdapter
	{
		/// <summary>
		/// Empty constructor to allow initialisation
		/// </summary>
		public ResultsAdapter()
		{
		}

		/// <summary>
		/// This method returns the index of the currently selected journey for a requested segment. 
		/// It first sets the SelectedItinerarySegment property then uses the 
		/// SelectedOutwardJourneyId property on the TDItineraryManager.
		/// </summary>
		/// <param name="itineraryManager">The itineraryManager object</param>
		/// <param name="segmentIndex">The segment index</param>
		/// <returns>Integer - The index of selected journey</returns>
		public int GetSelectedOutwardJourneyIndex(TDItineraryManager itineraryManager, int segmentIndex)
		{
			itineraryManager.SelectedItinerarySegment = segmentIndex;
			return itineraryManager.SelectedOutwardJourneyID;
		}

		/// <summary>
		/// This is called when the user has changed the selection. The method sets the currently 
		/// selected journey for a requested segment. It first must sets the SelectedItinerarySegment 
		/// property then uses the SelectedOutwardJourneyId property on the TDItineraryManager to set 
		/// this to the current journey ID.
		/// </summary>
		/// <param name="itineraryManager">The TDItineraryManager object</param>
		/// <param name="segmentIndex">The index of the current segment of the journey</param>
		/// <param name="selectedIndex">The index of the selected journey in the list</param>
		/// <param name="selectedJourneyIndex">The journey index of the selected journey</param>
		public void SetSelectedOutwardJourneyIndex(TDItineraryManager itineraryManager, int segmentIndex, int selectedIndex, int selectedJourneyIndex)
		{
			itineraryManager.SelectedItinerarySegment = segmentIndex;
			itineraryManager.SelectedOutwardJourneyIndex = selectedIndex;
			itineraryManager.SelectedOutwardJourneyID = selectedJourneyIndex;
		}

		/// <summary>
		/// Populates the Error Display Control
		/// </summary>
		/// <param name="control">The ErrorDisplayControl to populate.</param>
		/// <param name="itineraryManager">The itinerary manager which will provide the data.</param>
		/// <returns>False if there are no errors or true if otherwise.</returns>
		public bool PopulateErrorDisplayControl(ErrorDisplayControl control, TDItineraryManager itineraryManager)
		{
			//Return false if there are no errors
			if (itineraryManager.CJPMessages == null || itineraryManager.CJPMessages.Length == 0)
			{
				return false;
			}
			else
			{
				AddErrorStrings(control, itineraryManager.CJPMessages);

				if (control.ErrorStrings.Length > 0)
				{

					// For the reference number we need to know which journey result to take the
					// the reference number from. Therefore we need to iterate through the journey
					// results for the 1st invalid result and return the reference number form
					// that result object.
					for (int i=0; i < itineraryManager.Length; i++)
					{
						if (!itineraryManager.SpecificJourneyResult(i).IsValid)
						{
							control.ReferenceNumber = itineraryManager.SpecificJourneyResult(i).JourneyReferenceNumber.ToString(CultureInfo.InvariantCulture);
						}
					}
					
					//exit by returning true
					return true;
				}
				else
					return false;
			}
		}

		/// <summary>
		/// Populates an Error Display Control with errors from the CJP following a journey search
		/// </summary>
		/// <param name="control">The ErrorDisplayControl to populate.</param>
		/// <param name="itineraryManager">The session manager which will provide the data.</param>
		/// <returns>False if there are no errors or true if otherwise.</returns>
		public bool PopulateErrorDisplayControl(ErrorDisplayControl errorControl, ITDJourneyResult journeyResult)
		{
			if (journeyResult == null || journeyResult.CJPMessages.Length == 0)
				return false;
			else
			{
				AddErrorStrings(errorControl, journeyResult.CJPMessages);

				if (errorControl.ErrorStrings.Length > 0)
				{
					errorControl.ReferenceNumber = journeyResult.JourneyReferenceNumber.ToString(CultureInfo.InvariantCulture);

					// Clear the error messages in the result
					journeyResult.ClearMessages();

					return true;
				}
				else
					return false;

			}
		}

		/// <summary>
		/// Adds error strings to error display control
		/// </summary>
		/// <param name="errorControl">Control displaying CJP error messages</param>
		/// <param name="cjpMessages">Error messages to display</param>
		private void AddErrorStrings(ErrorDisplayControl errorControl, JourneyControl.CJPMessage[] cjpMessages)
		{
			ArrayList errorsList = new ArrayList();
					
			foreach (CJPMessage mess in cjpMessages)
			{
				//set the control type to either error or warning
				errorControl.Type = (mess.Type == TransportDirect.UserPortal.JourneyControl.ErrorsType.Warning) ? ErrorsDisplayType.Warning : ErrorsDisplayType.Error; 

				string text = mess.MessageText;
				if( text == null || text.Length == 0 )
				{
					text = GetResource("langStrings", mess.MessageResourceId);
				}
				errorsList.Add( text );
			}

			errorControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));
		}

		/// <summary>
		/// This method returns all outward journeys from the TDItineraryManager for the requested segment
		/// </summary>
		/// <param name="itineraryManager">The itineraryManager object</param>
		/// <param name="segmentIndex">The segment index</param>
		/// <returns>Array of JourneySummaryLine</returns>
		public JourneySummaryLine[] GetOutwardJourneyResults(TDItineraryManager itineraryManager, int segmentIndex)
		{
			ITDJourneyResult specificJourneyResult = itineraryManager.SpecificJourneyResult(segmentIndex);

			// Get the Journey Summary Lines from the result
			//JourneySummaryLine[] summaryLines = specificJourneyResult.OutwardJourneySummary(false);	
			//return summaryLines;
			return specificJourneyResult.OutwardJourneySummary(false);
		}


		/// <summary>
		/// Returns a collection of FormattedSummaryLine objects for the Full Itinerary Summary lines
		/// The collection provides sorting capability.
		/// </summary>
		/// <param name="itineraryManager">Instance of an Itinerary Manager that contains the required Itinerary</param>
		/// <returns>FormattedJourneySummaryLines containing one or two rows for the full Itinerary summaries for Outward and (optional) Return</returns>
		public FormattedJourneySummaryLines FormattedFullItinerarySummary(TDItineraryManager itineraryManager, FormattedSummaryType summaryType)
		{
			double conversionFactor =
				Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

			// Outward datetime
			TDDateTime dateTime = itineraryManager.OutwardDepartDateTime();

			int requestedDay = (dateTime != null) ? dateTime.Day : 0;

			// Gather the remaining information needed to setup the FormattedJourneySummaryLines class
			int journeyReferenceNumber = itineraryManager.JourneyReferenceNumber;
			bool anyTime = false;
			bool leaveAfter = true;

			// Note that the outward and return journeys will have been numbered as -1 and -2 respectively,
			// so sorting on JourneyIndex Descending will force journeys to be sorted into correct order.
			return new FormattedJourneySummaryLines(
                journeyReferenceNumber, 
                itineraryManager.FullItinerarySummary(),
                dateTime, 
                anyTime,
                leaveAfter,
                requestedDay,
                conversionFactor,
                String.Empty,
                String.Empty,
                int.MaxValue, 
                JourneySummaryColumn.JourneyIndex, 
                false,
                FindAMode.None, 
                true,
                summaryType,
                null);
		}
		
		/// <summary>
		/// Returns a collection of FormattedSummaryLine objects for the Return Itinerary Segments Summary lines
		/// The collection provides sorting capability. 
		/// </summary>
		/// <param name="sessionManager">Session Manager Instance</param>
		/// <returns>Formatted summary lines for outward (and return) journeys.</returns>
		public FormattedJourneySummaryLines FormattedSelectedJourneys(ITDSessionManager sessionManager, FormattedSummaryType summaryType)
		{
			ArrayList tempSummaryLines = new ArrayList();

			double conversionFactor = Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

			Journey outwardJourney = GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, false);

            // If we're working with cycle journey, then need to get info from the Cycle request
            bool isCycleJourney = (sessionManager.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.Cycle);

			ITDJourneyRequest request = sessionManager.JourneyRequest;
            TransportDirect.UserPortal.CyclePlannerControl.ITDCyclePlannerRequest cycleRequest = sessionManager.CycleRequest;

            #region Outward datetime
            TDDateTime dateTime = null;

            if (!isCycleJourney)
            {
                dateTime = ((request.OutwardDateTime != null) && (request.OutwardDateTime.Length > 0))
                    ? request.OutwardDateTime[0] : null;
            }
            else
            {
                dateTime = ((cycleRequest.OutwardDateTime != null) && (cycleRequest.OutwardDateTime.Length > 0))
                    ? cycleRequest.OutwardDateTime[0] : null;
            }

			int requestedDay = (dateTime != null) ? dateTime.Day : 0;
            #endregion

            // Add outward journey to the temporary array list of
			// journey summary lines.
			tempSummaryLines.Add( GenerateJourneySummaryLine( outwardJourney, 1, true ) );

			// If the user has specified a return journey then add this to the array
			// list of journey summary lines. Return public or road or cycle journey must
			// exist.
			if  (  ((sessionManager.JourneyResult != null) 
                    && ( sessionManager.JourneyResult.ReturnPublicJourneyCount > 0 || sessionManager.JourneyResult.ReturnRoadJourneyCount > 0))
                || ((sessionManager.CycleResult != null)
                    && (sessionManager.CycleResult.ReturnCycleJourneyCount > 0))
                )
			{
				tempSummaryLines.Add ( GenerateJourneySummaryLine( GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, true), 2, false ) );
			}

			// Dimension a new array of journey summary lines based on 
			// how many journeys we need to generate lines for.
			JourneySummaryLine[] result = (JourneySummaryLine[])tempSummaryLines.ToArray(typeof(JourneySummaryLine));

			// Gather the remaining information needed to setup the FormattedJourneySummaryLines class
			int journeyReferenceNumber = isCycleJourney ? sessionManager.CycleResult.JourneyReferenceNumber : sessionManager.JourneyResult.JourneyReferenceNumber;
			bool anyTime = isCycleJourney ? cycleRequest.OutwardAnyTime : request.OutwardAnyTime;
            bool leaveAfter = isCycleJourney ? !cycleRequest.OutwardArriveBefore : !request.OutwardArriveBefore;
            string defaultOrigin = isCycleJourney ? cycleRequest.OriginLocation.Description : request.OriginLocation.Description;
            string defaultDestination = isCycleJourney ? cycleRequest.DestinationLocation.Description : request.DestinationLocation.Description;

            // Return new formatted journey summary lines object
			// based on the journey summary lines that have been
			// made up.
			return new FormattedJourneySummaryLines(
				journeyReferenceNumber,
				result,
				dateTime,
				anyTime,
				leaveAfter,
				requestedDay,
				conversionFactor,
				defaultOrigin,
				defaultDestination,
				int.MaxValue,
				JourneySummaryColumn.JourneyIndex,
				false,
				FindAMode.None,
				true,
                summaryType,
                null);
		}


		/// <summary>
		/// Generates a journey summary line from a passed in journey.
		/// </summary>
		/// <param name="targetJourney">Journey to use</param>
		/// <param name="displayNumber">Display number for journey</param>
		/// <returns></returns>
		private JourneySummaryLine GenerateJourneySummaryLine(Journey targetJourney, int displayNumber, bool outward)
		{
			// Useful variables
			int lastLegIndex = targetJourney.JourneyLegs.Length - 1;
            TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modes = null;
            int interchangeCount = 0;
            string[] operatorNames = new string[0];

			// Road journey distance
			int roadDistance = 0;

			// Different times and distance to be used if journey is road/cycle/public transport.
			RoadJourney targetRoadJourney = (targetJourney as RoadJourney);
			if (targetRoadJourney != null)
			{
				roadDistance = targetRoadJourney.TotalDistance;
			}

            TransportDirect.UserPortal.CyclePlannerControl.CycleJourney targetCycleJourney = (targetJourney as TransportDirect.UserPortal.CyclePlannerControl.CycleJourney);
            if (targetCycleJourney != null)
            {
                roadDistance = targetCycleJourney.TotalDistance;
            }

            // Because cycle journey is not part of TDJourneyResult,
            // need to set the values manually
            if (targetCycleJourney != null)
            {
                modes = TDJourneyResult.GetAllModes(targetJourney);
                interchangeCount = 0;
            }
            else
            {   // Public/Road journeys
                modes = TDJourneyResult.GetAllModes(targetJourney);
                interchangeCount = TDJourneyResult.GetInterchangeCount(targetJourney);
                operatorNames = TDJourneyResult.GetOperatorNames(targetJourney);
            }

			// Return new journey summary line.
			return new JourneySummaryLine(
				(outward ? -1 : -2),
				targetJourney.JourneyLegs[0].LegStart.Location.Description,
				targetJourney.JourneyLegs[lastLegIndex].LegEnd.Location.Description,
				targetJourney.Type,
                modes,
				interchangeCount,
				targetJourney.JourneyLegs[0].StartTime,
				targetJourney.JourneyLegs[lastLegIndex].EndTime,
				roadDistance,
				displayNumber.ToString(CultureInfo.InvariantCulture),
				operatorNames);
		}


		/// <summary>
		/// Returns required (user selected) public/road journey as a Journey object. Takes 
		/// </summary>
		/// <param name="result">Journey result to use</param>
		/// <param name="viewState">Journey view state</param>
		/// <param name="returnRequired">Examine return journey</param>
		/// <returns></returns>
		public Journey GetRequiredJourney(ITDJourneyResult result, 
            TransportDirect.UserPortal.CyclePlannerControl.ITDCyclePlannerResult cycleResult, 
            TDJourneyViewState viewState, bool returnRequired)
		{
			if (!returnRequired)
			{
				if ( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
				{
					return result.OutwardPublicJourney(viewState.SelectedOutwardJourneyID);
				}
				else if (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended)
				{
					return result.AmendedOutwardPublicJourney;
				}
                else if (viewState.SelectedOutwardJourneyType == TDJourneyType.Cycle)
                {
                    return cycleResult.OutwardCycleJourney();
                }
                else
                {
                    return result.OutwardRoadJourney();
                }
			}
			else
			{
				if ( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
				{
					return result.ReturnPublicJourney(viewState.SelectedReturnJourneyID);
				}
				else if (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended)
				{
					return result.AmendedReturnPublicJourney;
				}
                else if (viewState.SelectedReturnJourneyType == TDJourneyType.Cycle)
                {
                    return cycleResult.ReturnCycleJourney();
                }
				else
				{
					return result.ReturnRoadJourney();
				}
			}
		}
	
		/// <summary>
		/// Gets a class representing an array of FormattedJourneySummaryLines which is the set of outward journeys
		/// returned by the request that the user can select from. These journeys are stored in the session manager.
		/// </summary>
		/// <param name="sessionManager">Instance of the session manager containing the results</param>
		/// <returns>FormattedJourneySummaryLines containing rows for the journeys in session</returns>
		public FormattedJourneySummaryLines FormattedOutwardJourneyLines(ITDSessionManager sessionManager, FormattedSummaryType summaryType)
		{

			// Get the journey result from the session
			ITDJourneyResult result = sessionManager.JourneyResult;

			// Check that we have a result (eg if we have just asked to extend journey we will have copied
			// result in session to itinerary (and cleared session down))
			if (result != null && result.IsValid)
			{

				ITDJourneyRequest request = sessionManager.JourneyRequest;
				bool arriveBefore = request.OutwardArriveBefore;

				// Get the outward journey lines
				JourneySummaryLine[] sourceLines = result.OutwardJourneySummary(arriveBefore);

				// Outward datetime
				TDDateTime dateTime = ((request.OutwardDateTime != null) && (request.OutwardDateTime.Length > 0))
					? request.OutwardDateTime[0]
					: null;

				int requestedDay = (dateTime != null) ? dateTime.Day : 0;

				double conversionFactor =
					Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

				// Now we have an array of FormattedLines but we want a FormattedJourneySummaryLines class to represent that array.
				// Gather the remaining information needed to setup that class
			
				// journey reference number
				int journeyReferenceNumber = result.JourneyReferenceNumber;
				bool anyTime = request.OutwardAnyTime;
				bool leaveAfter = !arriveBefore;
				string defaultOrigin = request.OriginLocation.Description;
				string defaultDestination = request.DestinationLocation.Description;

				return new FormattedJourneySummaryLines(journeyReferenceNumber, sourceLines, dateTime, 
					anyTime, leaveAfter, requestedDay, conversionFactor, defaultOrigin, 
					defaultDestination, int.MaxValue,
					JourneySummaryColumn.DisplayNumber,
					true, FindAMode.None, true, summaryType, null);
			}
			else
				return null;
		}

		/// <summary>
		/// Gets a class representing an array of FormattedJourneySummaryLines which is the set of return journeys
		/// returned by the request that the user can select from. These journeys are stored in the session manager.
		/// </summary>
		/// <param name="sessionManager">Instance of the session manager containing the results</param>
		/// <returns>FormattedJourneySummaryLines containing rows for the journeys in session</returns>
		public FormattedJourneySummaryLines FormattedReturnJourneyLines(ITDSessionManager sessionManager, FormattedSummaryType summaryType)
		{

			// Get the journey result from the session
			ITDJourneyResult result = sessionManager.JourneyResult;

			// Check that we have a result (eg if we have just asked to extend journey we will have copied
			// result in session to itinerary (and cleared session down))
			if (result != null && result.IsValid)
			{
				ITDJourneyRequest request = sessionManager.JourneyRequest;
				bool arriveBefore = request.ReturnArriveBefore;

				// Get the return journey lines
				JourneySummaryLine[] sourceLines = result.ReturnJourneySummary(arriveBefore);

				// Return empty array if no return journey request
				if (request.ReturnDateTime == null || request.ReturnDateTime.Length == 0 || request.ReturnDateTime[0] == null)
				{
					return new FormattedJourneySummaryLines();
				}
				
				// Return datetime
				TDDateTime dateTime = ((request.ReturnDateTime != null) && (request.ReturnDateTime.Length > 0))
					? request.ReturnDateTime[0]
					: null;

				int requestedDay = (dateTime != null) ? dateTime.Day : 0;

				double conversionFactor =
					Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

				// Now we have an array of FormattedLines but we want a FormattedJourneySummaryLines class to represent that array.
				// Gather the remaining information needed to setup that class
				
				int journeyReferenceNumber = result.JourneyReferenceNumber;
				bool anyTime = request.ReturnAnyTime;
				bool leaveAfter = !arriveBefore;
				string defaultOrigin = request.OriginLocation.Description;
				string defaultDestination = request.DestinationLocation.Description;

				return new FormattedJourneySummaryLines(journeyReferenceNumber, sourceLines, dateTime, 
					anyTime, leaveAfter, requestedDay, conversionFactor, defaultOrigin, 
					defaultDestination, int.MaxValue,
					JourneySummaryColumn.DisplayNumber,
					true, FindAMode.None, false, summaryType, null);
			}
			else
				return null;
			
		}

		/// <summary>
		/// Determines whether there is an interchange between the previous leg and the current leg.
		/// If a short walk leg has been removed (by the CJP as it's not worth displaying as normal leg) then there
		/// is a difference between the location at the end of the previous leg and the location at the start of the
		/// current leg.
		/// This short walk is represented as an interchange "pseudo-leg" on the details diagram control but since
		/// it isn't represented by a regular leg object we need to get it's start and end location from the previous
		/// leg end and current leg start respectively.
		/// </summary>
		/// <param name="detailIndex">Index of item currently being rendered</param>
		/// <returns>True if interchange pseudo-leg precedes the current leg.</returns>
		public bool AtInterchange(PublicJourney publicJourney, int detailIndex)
		{			
			if (detailIndex == 0)
			{
				return false;
			}

			// if either location does not have a valid naptan, can only compare descriptions ...
			if	((publicJourney.JourneyLegs[detailIndex].LegStart.Location.NaPTANs == null) 
				|| (publicJourney.JourneyLegs[detailIndex].LegStart.Location.NaPTANs.Length == 0) 
				|| (publicJourney.JourneyLegs[detailIndex - 1].LegEnd.Location.NaPTANs == null) 
				|| (publicJourney.JourneyLegs[detailIndex - 1].LegEnd.Location.NaPTANs.Length == 0))
			{
				if	(publicJourney.JourneyLegs[detailIndex].LegStart.Location.Description.Equals(
					publicJourney.JourneyLegs[detailIndex - 1].LegEnd.Location.Description))
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			// both locations have a valid naptan, so compare those ...
			if	(publicJourney.JourneyLegs[detailIndex].LegStart.Location.NaPTANs[0].CheckEquals(
				publicJourney.JourneyLegs[detailIndex - 1].LegEnd.Location.NaPTANs[0], false))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Determines whether there is an interchange between the current leg and the next leg.
		/// If a short walk leg has been removed (by the CJP as it's not worth displaying as normal leg) then there
		/// is a difference between the location at the end of the previous leg and the location at the start of the
		/// current leg.
		/// This short walk is represented as an interchange "pseudo-leg" on the details diagram control but since
		/// it isn't represented by a regular leg object we need to get it's start and end location from the current
		/// leg end and next leg start respectively.
		/// </summary>
		/// <param name="detailIndex">Index of item currently being rendered</param>
		/// <returns>True if interchange pseudo-leg follows the current leg.</returns>
		public bool PreInterchange(PublicJourney publicJourney, int detailIndex)
		{			
			if (detailIndex == publicJourney.Details.Length - 1)
			{
				return false;
			}

			// if either location does not have a valid naptan, can only compare descriptions ...
			if	((publicJourney.JourneyLegs[detailIndex].LegEnd.Location.NaPTANs == null) 
				|| (publicJourney.JourneyLegs[detailIndex].LegEnd.Location.NaPTANs.Length == 0) 
				|| (publicJourney.JourneyLegs[detailIndex + 1].LegStart.Location.NaPTANs == null) 
				|| (publicJourney.JourneyLegs[detailIndex + 1].LegStart.Location.NaPTANs.Length == 0))
			{
				if	(publicJourney.JourneyLegs[detailIndex].LegEnd.Location.Description.Equals(
					publicJourney.JourneyLegs[detailIndex + 1].LegStart.Location.Description))
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			// both locations have a valid naptan, so compare those ...
			if	(publicJourney.JourneyLegs[detailIndex].LegEnd.Location.NaPTANs[0].CheckEquals(
				publicJourney.JourneyLegs[detailIndex + 1].LegStart.Location.NaPTANs[0], false))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// The following group of methods Populate the BookmarkUrl and BookmarkTitle properties of the supplied ClientLinkControl.
		/// </summary>
		/// <param name="linkControl">The control to be populated.</param>
		/// <param name="request">The journey request used to create the result.</param>
		/// <remarks>
		/// The BookmarkTitle will be populated from the resource ClientLinks.DoorToDoor, with two 
		/// fields substituted with the values of JourneyRequest.OriginLocation.Description and 
		/// JourneyRequest.DestinationLocation.Description.  Both substitution strings will be trimmed 
		/// to 50 characters.
		/// </remarks>
		public void PopulateDoorToDoorClientLink(ClientLinkControl linkControl, ITDJourneyRequest request)
		{
			// Set bookmark title.
			string bookmarkTitle = resourceManager.GetString("ClientLinks.DoorToDoor.BookmarkTitle");
			string trimmedOrigin = request.OriginLocation.Description;
			if (trimmedOrigin.Length > 50)
				trimmedOrigin = trimmedOrigin.Substring(0, 50);
			// Escape single quotes that break javascript output
			trimmedOrigin = EscapeJavaScriptQuotes(trimmedOrigin);

			string trimmedDestination = request.DestinationLocation.Description;
			if (trimmedDestination.Length > 50)
				trimmedDestination = trimmedDestination.Substring(0, 50);
			// Escape single quotes that break javascript output
			trimmedDestination = EscapeJavaScriptQuotes(trimmedDestination);

			linkControl.BookmarkTitle = string.Format( TDCultureInfo.CurrentCulture, bookmarkTitle, new object[] { trimmedOrigin, trimmedDestination } );

			// Construct the URL
			LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
			targetUrl.Append("?");

			// Now build up the parameters for the remainder of the URL
			string locationType = string.Empty;
			string locationText = string.Empty;
			string locationData = string.Empty;

			// Origin location
			lpHelper.GetLocationData(request.OriginLocation, ref locationType, ref locationData, ref locationText);
			
			// Add origin data along with partner Id in the string
			string partnerId = Properties.Current["ClientLinks.PartnerId"];
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

			// Destination location
			lpHelper.GetLocationData(request.DestinationLocation, ref locationType, ref locationData, ref locationText);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

			// Miscellaneous other parameters
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal);
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

			// Exclude all other modes to replicate a Find a Bus request
			if (TDSessionManager.Current.FindAMode == FindAMode.Bus)
			{
				AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterExcludeModes, LandingPageHelperConstants.ValueModesFindABus);
			}

			// If we need a car journey, the last value in the modes array on the request will be car.
			bool carRequired = request.Modes[request.Modes.Length - 1] == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Car;
			//Add last parameter
			AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterCarDefault, carRequired.ToString(), true);
			
			linkControl.BookmarkUrl = targetUrl.ToString();
		}
        
        public void PopulateMiniPlannerClientLink(ClientLinkControl linkControl, ITDJourneyRequest request, FindAMode miniplannerMode)
        {
            // Set bookmark title.
            string bookmarkTitle = resourceManager.GetString("ClientLinks.DoorToDoor.BookmarkTitle");
            string trimmedOrigin = request.OriginLocation.Description;
            if (trimmedOrigin.Length > 50)
                trimmedOrigin = trimmedOrigin.Substring(0, 50);
            // Escape single quotes that break javascript output
            trimmedOrigin = EscapeJavaScriptQuotes(trimmedOrigin);

            string trimmedDestination = request.DestinationLocation.Description;
            if (trimmedDestination.Length > 50)
                trimmedDestination = trimmedDestination.Substring(0, 50);
            // Escape single quotes that break javascript output
            trimmedDestination = EscapeJavaScriptQuotes(trimmedDestination);

            linkControl.BookmarkTitle = string.Format(TDCultureInfo.CurrentCulture, bookmarkTitle, new object[] { trimmedOrigin, trimmedDestination });

            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Origin location
            lpHelper.GetLocationData(request.OriginLocation, ref locationType, ref locationData, ref locationText);

            // Add origin data along with partner Id in the string
            string partnerId = Properties.Current["ClientLinks.PartnerId"];
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

            // Destination location
            lpHelper.GetLocationData(request.DestinationLocation, ref locationType, ref locationData, ref locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);

            switch (miniplannerMode)
            {
                case FindAMode.Train:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeTrain);
                    break;
                case FindAMode.Car:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeRoad);
                    break;
                case FindAMode.Coach:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeCoach);
                    break;
                case FindAMode.Flight:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeAir);
                    break;
                default:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal);
                    break;
            }

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

            linkControl.BookmarkUrl = targetUrl.ToString();
        }

        /// <summary>
        /// Populates the client link control with cycle planner landing page url
        /// </summary>
        /// <param name="linkControl">Client link control</param>
        /// <param name="request">Cycle journey request</param>
        private void PopulateCyclePlannerClientLink(ClientLinkControl linkControl, ITDCyclePlannerRequest request)
        {
            // Set bookmark title.
            string bookmarkTitle = resourceManager.GetString("ClientLinks.DoorToDoor.BookmarkTitle");
            string trimmedOrigin = request.OriginLocation.Description;
            if (trimmedOrigin.Length > 50)
                trimmedOrigin = trimmedOrigin.Substring(0, 50);
            // Escape single quotes that break javascript output
            trimmedOrigin = EscapeJavaScriptQuotes(trimmedOrigin);

            string trimmedDestination = request.DestinationLocation.Description;
            if (trimmedDestination.Length > 50)
                trimmedDestination = trimmedDestination.Substring(0, 50);
            // Escape single quotes that break javascript output
            trimmedDestination = EscapeJavaScriptQuotes(trimmedDestination);

            linkControl.BookmarkTitle = string.Format(TDCultureInfo.CurrentCulture, bookmarkTitle, new object[] { trimmedOrigin, trimmedDestination });

            // partner Id 
            string partnerId = Properties.Current["ClientLinks.PartnerId"];

            linkControl.BookmarkUrl = GenerateCyclePlannerLandingUrl(request, partnerId);
        }

        /// <summary>
		/// Returns true if the Client Links control can be shown for the given mode and journey
		/// request data.
		/// </summary>
		/// <param name="mode">The current find a mode.</param>
		/// <param name="request">The journey request that will be used to generate the link.</param>
		/// <returns>True if the control can be displayed, false otherwise.</returns>
		private bool CanShowClientLink(FindAMode mode, ITDJourneyRequest request)
		{
			bool showClientLinks;

			if (TDItineraryManager.Current.ExtendInProgress)
			{
				showClientLinks = false;
			}
			else
			{
				switch (mode)
				{
					case FindAMode.None:
					case FindAMode.Bus:
                    case FindAMode.Train:
                    case FindAMode.Coach:
                    case FindAMode.Car:
                    case FindAMode.Flight:
						// Must have request data, an origin location and a destination location,
						// and origin and destination location must contain no coach naptans
						if 	(
							(request == null) || 
							(request.OriginLocation == null) || 
							(request.DestinationLocation == null) 
							) 
							showClientLinks = false;
						else
							showClientLinks = true;

						break;

					default:
						showClientLinks = false;
						break;
				}
			}
			return showClientLinks;
		}

		
        
        /// <summary>
		/// Sets up a client link control being displayed on the journey results page.
		/// </summary>
		/// <param name="linkControl">The link control to set up.</param>
		public void PopulateJourneyResultsClientLink(ClientLinkControl linkControl)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			
			linkControl.Visible = CanShowClientLink(sessionManager.FindAMode, sessionManager.JourneyRequest);

            if (sessionManager.FindAMode == FindAMode.Cycle)
            {
                if (
                    (sessionManager.CycleRequest == null) ||
                    (sessionManager.CycleRequest.OriginLocation == null) ||
                    (sessionManager.CycleRequest.DestinationLocation == null)
                    )
                {
                    linkControl.Visible = false;
                }
                else
                {
                    linkControl.Visible = true;
                }
            }
			if (linkControl.Visible)
			{
				switch (sessionManager.FindAMode)
				{
					case FindAMode.None:
					case FindAMode.Bus:
						PopulateDoorToDoorClientLink(linkControl, sessionManager.JourneyRequest);
						linkControl.LinkText = resourceManager.GetString("ClientLinks.DoorToDoor.LinkText");
						break;
					case FindAMode.Train:
                    case FindAMode.Coach:
                    case FindAMode.Flight:
                    case FindAMode.Car:
                        PopulateMiniPlannerClientLink(linkControl, sessionManager.JourneyRequest, sessionManager.FindAMode);
						linkControl.LinkText = resourceManager.GetString("ClientLinks.DoorToDoor.LinkText");
						break;
                    case FindAMode.Cycle:
                        PopulateCyclePlannerClientLink(linkControl, sessionManager.CycleRequest);
                        linkControl.LinkText = resourceManager.GetString("ClientLinks.DoorToDoor.LinkText");
                        break;

                    default:
						// Do nothing
						break;
				}

			}
		}

        

		/// <summary>
		/// Adds a querystring parameter to the URL contained in the string builder.
		/// </summary>
		/// <param name="url">StringBuilder containing the Url. This should currently end in either ? or &</param>
		/// <param name="parameter">The name of the parameter to add.</param>
		/// <param name="value">The value of the parameter to add.</param>
		private void AddParameterToUrl(StringBuilder url, string parameter, string value)
		{
			//Assume that this isn't the final parameter
			AddParameterToUrl(url, parameter, value, false);
		}

		/// <summary>
		/// Adds a querystring parameter to the URL contained in the string builder.
		/// </summary>
		/// <param name="url">StringBuilder containing the Url. This should currently end in either ? or &</param>
		/// <param name="parameter">The name of the parameter to add.</param>
		/// <param name="value">The value of the parameter to add.</param>
		/// <param name="isFinalParameter">Denotes whether is final parameter - if so no trailing amprasand is required</param>
		private void AddParameterToUrl(StringBuilder url, string parameter, string value, bool isFinalParameter)
		{
			url.Append(parameter);
			url.Append("=");
			string s = HttpUtility.UrlEncode(value);
			s = EscapeJavaScriptQuotes(s);
			url.Append(s);

			if (!isFinalParameter)
			{
				url.Append("&");
			}
		}

		/// <summary>
		/// Method replaces single quotes with
		/// necessary escape characters for JavaScript output
		/// </summary>
		/// <param name="s">input string</param>
		/// <returns>output string</returns>
		private string EscapeJavaScriptQuotes(string s)
		{
			if (s!=null)
			{
				s = s.Replace("'", "\\'");
			}

			return s;
		}



        public void PopulateBookMarkClientLink(ClientLinkControl linkControl, ITDJourneyRequest request, FindAMode miniplannerMode)
        {
            // Set bookmark title.
            string bookmarkTitle = resourceManager.GetString("ClientLinks.DoorToDoor.BookmarkTitle");
            string trimmedOrigin = request.OriginLocation.Description;
            if (trimmedOrigin.Length > 50)
                trimmedOrigin = trimmedOrigin.Substring(0, 50);
            // Escape single quotes that break javascript output
            trimmedOrigin = EscapeJavaScriptQuotes(trimmedOrigin);

            string trimmedDestination = request.DestinationLocation.Description;
            if (trimmedDestination.Length > 50)
                trimmedDestination = trimmedDestination.Substring(0, 50);
            // Escape single quotes that break javascript output
            trimmedDestination = EscapeJavaScriptQuotes(trimmedDestination);

            linkControl.BookmarkTitle = string.Format(TDCultureInfo.CurrentCulture, bookmarkTitle, new object[] { trimmedOrigin, trimmedDestination });

            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Origin location
            lpHelper.GetLocationData(request.OriginLocation, ref locationType, ref locationData, ref locationText);

            // Add origin data along with partner Id in the string
            string partnerId = Properties.Current["ClientLinks.PartnerId"];
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

            // Destination location
            lpHelper.GetLocationData(request.DestinationLocation, ref locationType, ref locationData, ref locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);

            switch (miniplannerMode)
            {
                case FindAMode.Train:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeTrain);
                    break;
                case FindAMode.Car:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeRoad);
                    break;
                case FindAMode.Coach:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeCoach);
                    break;
                case FindAMode.Flight:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeAir);
                    break;
                default:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal);
                    break;
            }

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

            linkControl.BookmarkUrl = targetUrl.ToString();
        }

        /// <summary>
        /// Constructs landing page url for partner id provided
        /// </summary>
        /// <param name="partnerId">Partner Id</param>
        /// <returns>Landing page url</returns>
        public string GenerateLandingPageUrl(string partnerId)
        {
            string landingUrl = string.Empty;

            ITDSessionManager sessionManager = TDSessionManager.Current;

            if(CanShowClientLink(sessionManager.FindAMode, sessionManager.JourneyRequest) || (sessionManager.FindAMode == FindAMode.CarPark)
                || (sessionManager.FindAMode == FindAMode.Cycle))
            {
                switch (sessionManager.FindAMode)
                {
                    case FindAMode.None:
                    case FindAMode.Bus:
                        landingUrl = GenerateDoorToDoorLandingUrl(sessionManager.JourneyRequest, partnerId);
                        //socialBookMarkLinkControl.LinkText = "SocialBookMarkLinktext";//resourceManager.GetString("ClientLinks.DoorToDoor.LinkText");
                        break;
                    case FindAMode.Train:
                    case FindAMode.Coach:
                    case FindAMode.Flight:
                    case FindAMode.Car:
                        landingUrl = GenerateMiniPlannerLandingUrl(sessionManager.JourneyRequest, sessionManager.FindAMode, partnerId);
                        break;
                    case FindAMode.CarPark:
                        landingUrl = GenerateFindNearestLandingPageUrl(sessionManager.FindCarParkPageState, partnerId);
                        break;
                    case FindAMode.Cycle:
                        landingUrl = GenerateCyclePlannerLandingUrl(sessionManager.CycleRequest, partnerId);
                        break;
                    default:
                        // Do nothing
                        break;
                }
            }

            

            return landingUrl;
        }

        private string GenerateCyclePlannerLandingUrl(ITDCyclePlannerRequest request, string partnerId)
        {
            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Origin location
            lpHelper.GetLocationData(request.OriginLocation, ref locationType, ref locationData, ref locationText);

            // Add origin data along with partner Id in the string
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

            // Destination location
            lpHelper.GetLocationData(request.DestinationLocation, ref locationType, ref locationData, ref locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);
                       
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeCycle);

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

            return targetUrl.ToString();
        }

        /// <summary>
        /// Constructs a landing page url for Find nearest car park
        /// </summary>
        /// <param name="pageState">Find car park page state</param>
        /// <param name="partnerId">Partner Id</param>
        /// <returns></returns>
        private string GenerateFindNearestLandingPageUrl(FindCarParkPageState pageState, string partnerId)
        {
            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.FindNearestLandingPage));
            targetUrl.Append("?");

            // Add entrytype and find nearest type data along with partner Id in the string
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterEntryType, "et");
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterFindNearestType, "cp");
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

            
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterFindNearestPlace, pageState.CurrentSearch.InputText);

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterFindNearestLocGaz, GetGazeteerForSearchType(pageState.CurrentSearch.SearchType));

            return targetUrl.ToString();
        }

        /// <summary>
        /// Returns a Gazeteer used as a string based on search type
        /// </summary>
        /// <param name="searchType">Search Type</param>
        /// <returns>Gazetter string</returns>
        private string GetGazeteerForSearchType(SearchType searchType)
        {
            
                string gazeteerType;
                
                switch (searchType)
                {
                    case SearchType.POI:
                        gazeteerType = FindLocationGazeteerOptions.AttractionFacility.ToString();
                        break;
                    case SearchType.Locality:
                        // Note: Search Type Locality = City/Town/Suburb - NOT City
                        gazeteerType = FindLocationGazeteerOptions.CityTownSuburb.ToString();
                        break;
                    case SearchType.MainStationAirport:
                        gazeteerType = FindLocationGazeteerOptions.StationAirport.ToString();
                        break;
                    case SearchType.AddressPostCode:
                    default:
                        gazeteerType = FindLocationGazeteerOptions.AddressPostcode.ToString();
                        break;
                }

                return gazeteerType;
            
        }

        /// <summary>
        /// Generated title for the landing page
        /// </summary>
        /// <returns>Title of the landing page</returns>
        public string GenerateLandingPageTitle()
        {
            string landingTitle = string.Empty;

            ITDSessionManager sessionManager = TDSessionManager.Current;

            if (sessionManager.FindAMode == FindAMode.CarPark)
            {
                string title = resourceManager.GetString("ClientLinks.FindCarPark.BookmarkTitle");

                landingTitle = string.Format(TDCultureInfo.CurrentCulture, title, sessionManager.FindCarParkPageState.CurrentSearch.InputText);
            }
            else
            {
                // Set bookmark title.
                string bookmarkTitle = resourceManager.GetString("ClientLinks.DoorToDoor.BookmarkTitle");

                string trimmedOrigin = sessionManager.JourneyRequest.OriginLocation.Description;
                if (trimmedOrigin.Length > 50)
                    trimmedOrigin = trimmedOrigin.Substring(0, 50);
                // Escape single quotes that break javascript output
                trimmedOrigin = EscapeJavaScriptQuotes(trimmedOrigin);

                string trimmedDestination = sessionManager.JourneyRequest.DestinationLocation.Description;
                if (trimmedDestination.Length > 50)
                    trimmedDestination = trimmedDestination.Substring(0, 50);
                // Escape single quotes that break javascript output
                trimmedDestination = EscapeJavaScriptQuotes(trimmedDestination);

                landingTitle = string.Format(TDCultureInfo.CurrentCulture, bookmarkTitle, new object[] { trimmedOrigin, trimmedDestination });
            }

            return landingTitle;
        }

        /// <summary>
        /// Constructs the landing page urls for Train, Car, Coach and Flight planners
        /// </summary>
        /// <param name="request">Journey Request</param>
        /// <param name="miniplannerMode">Find a mode</param>
        /// <param name="partnerId">Partner Id</param>
        /// <returns>Landing page url</returns>
        private string GenerateMiniPlannerLandingUrl(ITDJourneyRequest request, FindAMode miniplannerMode, string partnerId)
        {
            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Origin location
            lpHelper.GetLocationData(request.OriginLocation, ref locationType, ref locationData, ref locationText);

            // Add origin data along with partner Id in the string
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

            // Destination location
            lpHelper.GetLocationData(request.DestinationLocation, ref locationType, ref locationData, ref locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);

            switch (miniplannerMode)
            {
                case FindAMode.Train:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeTrain);
                    break;
                case FindAMode.Car:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeRoad);
                    break;
                case FindAMode.Coach:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeCoach);
                    break;
                case FindAMode.Flight:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeAir);
                    break;
                default:
                    AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal);
                    break;
            }

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

            return targetUrl.ToString();

        }

        /// <summary>
        /// Constructs landing page url for door to door planner
        /// </summary>
        /// <param name="request">Journey Request</param>
        /// <param name="partnerId">Partner Id</param>
        /// <returns>Landing page url</returns>
        private string GenerateDoorToDoorLandingUrl(ITDJourneyRequest request, string partnerId)
        {
            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Origin location
            lpHelper.GetLocationData(request.OriginLocation, ref locationType, ref locationData, ref locationText);

            // Add origin data along with partner Id in the string
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);

            // Destination location
            lpHelper.GetLocationData(request.DestinationLocation, ref locationType, ref locationData, ref locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

            // Exclude all other modes to replicate a Find a Bus request
            if (TDSessionManager.Current.FindAMode == FindAMode.Bus)
            {
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterExcludeModes, LandingPageHelperConstants.ValueModesFindABus);
            }

            // If we need a car journey, the last value in the modes array on the request will be car.
            bool carRequired = request.Modes[request.Modes.Length - 1] == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Car;
            //Add last parameter
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterCarDefault, carRequired.ToString(), true);

            return targetUrl.ToString();
        }

       /// <summary>
       /// Constructs landing page url for door to door planner
       /// </summary>
       /// <param name="origin">Origin location</param>
       /// <param name="destination">Destination location</param>
       /// <param name="partnerId">Partner Id</param>
       /// <returns></returns>
        public string GenerateDoorToDoorLandingUrl(TDLocation origin, TDLocation destination, string partnerId)
        {
            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Origin location
            if (origin != null)
            {
                lpHelper.GetLocationData(origin, ref locationType, ref locationData, ref locationText);

                // Add origin data along with partner Id in the string
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, partnerId);
            }

            if (destination != null)
            {
                // Destination location
                lpHelper.GetLocationData(destination, ref locationType, ref locationData, ref locationText);
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);
            }

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff);

            
            // If we need a car journey, the last value in the modes array on the request will be car.
            bool carRequired = true;
            
            //Add last parameter
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterCarDefault, carRequired.ToString(), true);

            return targetUrl.ToString();
        }



	}

}
