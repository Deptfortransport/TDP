// *********************************************** 
// NAME                 : RoadJourneyHelper.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 5 October 2005
// DESCRIPTION			: Helper class to process road journeys to match with Travel news incidents
//                        The class also enables to replan road journey avoiding any closure/blockages 
//                        and modify the existing journey result.
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/RoadJourneyHelper.cs-arc  $
//
//   Rev 1.7   Oct 12 2011 16:02:42   mmodi
//Updated logic to set the road journey "has closure" flag only when incident affects (toid) a road detail AND is active (time) for that road detail
//Resolution for 5754: Travel news replan button shown but no incident icons in the journey results
//
//   Rev 1.6   Sep 16 2011 10:01:22   mmodi
//Corrected logging of real time processing as Verbose instead of Error
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.5   Sep 15 2011 12:08:58   apatel
//Code uses one of the property of travel news item to check if its active but this property only determins if the travel news item is active for today. So, removed the code from the 'if' condition that was checking if the travel news item is active for real time information in car
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.4   Sep 14 2011 14:46:44   apatel
//Updated to apply the correct padding to travel news item start time and end time.
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.3   Sep 08 2011 13:10:20   apatel
//Updated to resolve the issues with printer friendly, padding for daily end date and daily end date adjustment issues
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.2   Sep 06 2011 11:20:26   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 02 2011 10:22:08   apatel
//Real time car changes
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Sep 01 2011 11:38:06   apatel
//Initial revision.
//Resolution for 5731: CCN 0548 - Real Time Information in Car

using System;
using System.Collections.Generic;
using System.Web;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Data.SqlClient;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;


namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Helper class to process road journeys to match with Travel news incidents
    /// The class also enables to replan road journey avoiding any closure/blockages 
    /// and modify the existing journey result.
    /// </summary>
    public class RoadJourneyHelper
    {
        #region Private Fields
        private TDJourneyParametersMulti journeyParameters;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RoadJourneyHelper()
        {
            journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method to replan road journey and modify the existing result to avoid closures at toids specified in journey parameters
        /// </summary>
        public bool ReplanRoadJourneyToAvoidClosures(bool isOutward, bool avoidClosureOnly)
        {
            ITDJourneyRequest originalRequest = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest;

            ITDJourneyResult originalResult = TDSessionManager.Current.JourneyResult;
            
            DateTime startTime = DateTime.Now;

            // Get reference/sequence number from original journey
            int referenceNumber = Convert.ToInt32(originalResult.JourneyReferenceNumber);
            int sequenceNumber = Convert.ToInt32(originalResult.LastReferenceSequence);

            

            // Build the new journey request and not modify the existing one
            ITDJourneyRequest newRequest = BuildReplanJourneyRequest(originalRequest, isOutward, avoidClosureOnly);
                      
                        
            // Journey Plan Runner
            JourneyPlanRunner.IJourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

            AsyncCallState acs = new JourneyPlanState();
            // Determine refresh interval and resource string for the wait page
            acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.AmendDateAndTime"]);
            acs.WaitPageMessageResourceFile = "langStrings";
            acs.WaitPageMessageResourceId = "WaitPageMessage.Replan";
                        
            acs.AmbiguityPage = PageId.JourneyDetails;
            acs.DestinationPage = PageId.JourneyDetails;
            acs.ErrorPage = PageId.JourneyDetails;
            
            TDSessionManager.Current.AsyncCallState = acs;

            // Invalidate any existing journeys in the session.
            // If we do not do this any failed journey requests result in a page being displayed to the user
            // that has a mixture of the new request and the old results.
            bool validateAndRunSuccess = runner.ValidateAndRun(TDSessionManager.Current,referenceNumber, sequenceNumber, newRequest);
            if (validateAndRunSuccess)
            {
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;

                // Log succesful completion event of real time road journey travel news matching
                RealTimeRoadEvent rtrEvent = new RealTimeRoadEvent(RealTimeRoadEventType.RealTimeRoadJOurneyReplanAvoidingTravelNews,
                    startTime, false, TDSessionManager.Current.Session.SessionID, validateAndRunSuccess);
                Logger.Write(rtrEvent);


                TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
                TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(TransitionEvent.FindAInputRedirectToResults);

            }

            return validateAndRunSuccess;
        }
        
        /// <summary>
        /// Method which uses the toids set for each RoadJourneyDetail and 
        /// checks the TravelNews service if any of them contain incidents
        /// </summary>
        public void ProcessRoadJourneyForTravelNewsIncidents(RoadJourney roadJourney)
        {
            if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
            {
                roadJourney.JourneyMatchedForTravelNewsIncidents = true;
                // Check if the code should perform toid processing
                bool checkJourney = false;

                if (bool.TryParse(Properties.Current["TravelNews.Toids.ProcessJourney.Switch"], out checkJourney))
                {
                    #region Log

                    // Logging variables
                    string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
                    DateTime tnStartTime = DateTime.Now;
                    DateTime journeyStartDateTime = DateTime.Now;
                    int affectedIncidentsFound = 0;
                   
                    string message = string.Format("TravelNewsToids - processing started at[{0}] for journey origin[{1}] destination[{2}]",
                        tnStartTime.ToString(dateTimeFormat),
                        journeyParameters.OriginLocation.Description, journeyParameters.DestinationLocation.Description);

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message));

                    #endregion

                    #region Build Toids list

                    List<string> toidsList = new List<string>();

                    RoadJourneyDetail[] roadJourneyDetails = roadJourney.Details;

                    // Build up a list of all the Toids to query on, more efficient than multiple single calls
                    if (roadJourneyDetails != null)
                    {
                        foreach (RoadJourneyDetail detail in roadJourneyDetails)
                        {
                            if (detail.Toid != null)
                            {
                                toidsList.AddRange(detail.Toid);
                            }
                        }
                    }

                    #endregion

                    if (toidsList.Count > 0)
                    {
                        // Get all the travel news incidents for the journey (for efficiency)
                        ITravelNewsHandler travelNewsHandler = (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];

                        TravelNewsItem[] travelNewsIncidents = travelNewsHandler.GetTravelNewsByAffectedToids(toidsList.ToArray());

                        if (travelNewsIncidents != null && travelNewsIncidents.Length > 0)
                        {
                            #region Update drive sections with affected travel news incidents

                            // Track the start and end time of the drive section, used to in the "is active" check
                            TDDateTime startTimeOfSection = roadJourney.DepartDateTime;

                            TDDateTime endTimeOfSection = startTimeOfSection;

                            List<string> currentDetailToids = new List<string>();

                            // Go through each detail and look for the toid and add affected toid/incident
                            foreach (RoadJourneyDetail detail in roadJourneyDetails)
                            {
                                // Update end time for drive section
                                endTimeOfSection = startTimeOfSection.Add(new TimeSpan(0, 0, (int)detail.Duration));

                                // Update the drive section toids to check
                                if (detail.Toid != null && detail.Toid.Length > 0)
                                {
                                    currentDetailToids = new List<string>(detail.Toid);
                                }
                                else
                                {
                                    currentDetailToids = new List<string>();
                                }

                                foreach (TravelNewsItem tnItem in travelNewsIncidents)
                                {
                                    // If the incident contains toids used in the drive section, and it
                                    // is active at the time the drive section goes through it, update the 
                                    // drive section with the incident
                                    if (IsTravelNewsAffectedForDriveSection(currentDetailToids, tnItem)
                                        && IsTravelNewsActiveForDriveSection(startTimeOfSection, endTimeOfSection, tnItem))
                                    {
                                        // Update the detail with the incident id so the UI
                                        // displays the incident against the drive section
                                        if (detail.TravelNewsIncidentIDs == null)
                                        {
                                            detail.TravelNewsIncidentIDs = new List<string>();
                                        }
                                        if (!detail.TravelNewsIncidentIDs.Contains(tnItem.Uid))
                                        {
                                            detail.TravelNewsIncidentIDs.Add(tnItem.Uid);
                                        }

                                        // If it is a closure travel news incident, then flag
                                        // in the journey. This allows the UI to display the "replan" functionality
                                        if (tnItem.IsClosure)
                                        {
                                            roadJourney.HasClosure = true;
                                        }

                                        // For logging
                                        affectedIncidentsFound += 1;
                                    }
                                }

                                startTimeOfSection = endTimeOfSection;
                            }

                            #endregion
                        }
                    }

                    #region Log

                    // Log succesful completion event of real time road journey travel news matching
                    RealTimeRoadEvent rtrEvent = new RealTimeRoadEvent(RealTimeRoadEventType.RealTimeRoadJourneyTravelNewsMatching,
                        tnStartTime, affectedIncidentsFound > 0, TDSessionManager.Current.Session.SessionID, true);
                    Logger.Write(rtrEvent);

                    DateTime tnEndTime = DateTime.Now;

                    message = string.Format("TravelNewsToids - processing ended at[{0}]", tnEndTime.ToString(dateTimeFormat));
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message));

                    TimeSpan duration = tnEndTime.Subtract(tnStartTime);

                    message = string.Format("TravelNewsToids - For journey origin[{0}] destination[{1}] on datetime[{2}]( processingTime[{3}ms] journeyToids[{4}] affectedIncidents[{5}] )",
                                journeyParameters.OriginLocation.Description, journeyParameters.DestinationLocation.Description,
                                journeyStartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                                duration.TotalMilliseconds.ToString(),
                                toidsList.Count,
                                affectedIncidentsFound);
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message));

                    #endregion
                }
            }
        }

        

        #endregion

        #region Private Methods
        /// <summary>
        /// Does a null check for the data reader column value and returns empty string if found null 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private string GetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        /// <summary>
        /// Does a null check for the data reader column value and returns false if found null 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private bool GetBool(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetBoolean(colIndex);
            else
                return false;
        }

        /// <summary>
        /// Generate a journey request for replanning a journey to avoid toids with closure\blockages
        /// </summary>
        /// <param name="originalRequest">Journey request used for original journey.</param>
        /// <returns>Journey requst for replanning the journey to avoid closures</returns>
        public ITDJourneyRequest BuildReplanJourneyRequest(ITDJourneyRequest originalRequest, bool isOutward, bool avoidClosuresOnly)
        {
            // NOTE: To create the new journey request we perform a shallow clone on
            // the previous request. This will create a new request that contains references
            // to the previous request. This is acceptable for a journey request as in
            // this scenario the locations/modes etc remain the same.
            ITDJourneyRequest request = (ITDJourneyRequest)originalRequest.Clone(); // New Journey Request that will be generated.

            request.IsOutwardRequired = isOutward;
            request.IsReturnRequired = !isOutward;
            
            // Populate the modes of journey
            request.Modes = new TransportDirect.JourneyPlanning.CJPInterface.ModeType[] { TransportDirect.JourneyPlanning.CJPInterface.ModeType.Car};
            
            // Populate the toids to avoid journey at
            string[] toidsToAvoid = GetToidsToAvoid(isOutward, avoidClosuresOnly);
                      
           
            if (isOutward)
            {
                journeyParameters.AvoidToidsListOutward = toidsToAvoid;
                request.AvoidToidsOutward = (journeyParameters.AvoidToidsListOutward == null ? new string[0] : journeyParameters.AvoidToidsListOutward);
            }
            else
            {
                journeyParameters.AvoidToidsListReturn = toidsToAvoid;
                request.AvoidToidsReturn = (journeyParameters.AvoidToidsListReturn == null ? new string[0] : journeyParameters.AvoidToidsListReturn);
            }

            return request;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns a list of unique toids to avoid with closure/blocakage
        /// </summary>
        /// <param name="isOutward">Outward/Return journey for which list of toids to avoid needed</param>
        /// <returns></returns>
        public string[] GetToidsToAvoid(bool isOutward, bool avoidClosuresOnly)
        {
            RoadJourney roadJourney = null;
            // Initialise toidsToAvoid list with already avoided list of toids so CJP doesn't return the journey
            // with toids avoided previously in subsequent journeys
            List<string> toidsToAvoid = new List<string>();

            if (isOutward)
            {
                if (journeyParameters.AvoidToidsListOutward != null)
                {
                    toidsToAvoid.AddRange(journeyParameters.AvoidToidsListOutward);
                }
            }
            else
            {
                if (journeyParameters.AvoidToidsListReturn != null)
                {
                    toidsToAvoid.AddRange(journeyParameters.AvoidToidsListReturn);
                }
            }

            if (isOutward)
            {
                roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
            }
            else
            {
                roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
            }

            foreach (RoadJourneyDetail detail in roadJourney.Details)
            {
                if (detail.TravelNewsIncidentIDs != null && detail.TravelNewsIncidentIDs.Count > 0)
                {
                    foreach (string travelNewsUid in detail.TravelNewsIncidentIDs)
                    {
                        TravelNewsItem tnItem = TravelNewsHelper.GetTravelNewsItem(travelNewsUid);
                        if (tnItem != null)
                        {
                            // if avoidClosuresOnly is false, avoid toids related with all the travel news incidents affecting the journey
                            // if avoidClosuresOnly is true, check if the travel news type is closure/blockage and
                            // only avoid toids with closure/blockage.
                            if (tnItem.IsClosure || !avoidClosuresOnly)
                            {
                                foreach (string toid in tnItem.AffectedToids)
                                {
                                    if (!toidsToAvoid.Contains(toid))
                                    {
                                        toidsToAvoid.Add(toid);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return toidsToAvoid.ToArray();
        }

        /// <summary>
        /// Determines if the travel news item has toids for the supplied drive section toids
        /// </summary>
        /// <param name="detailToids">Drive section toids to compare against</param>
        /// <param name="tnItem">Travel news item to check if active</param>
        /// <returns>true if travel news item is active, false otherwise</returns>
        private bool IsTravelNewsAffectedForDriveSection(List<string> detailToids, TravelNewsItem tnItem)
        {
            bool affected = false;

            if ((tnItem.AffectedToids != null) && (tnItem.AffectedToids.Length > 0)
                && (detailToids != null) && (detailToids.Count > 0))
            {
                foreach (string toid in tnItem.AffectedToids)
                {
                    if (detailToids.Contains(toid.Trim())
                        || detailToids.Contains(string.Format("osgb{0}", toid.ToLower().Trim())))
                    {
                        // Travel news incident affects the drive section toids
                        affected = true;
                    }
                }
            }

            return affected;
        }

        /// <summary>
        /// Determines if the travel news item is active for the given start and end date
        /// </summary>
        /// <param name="startDateTime">Start date time</param>
        /// <param name="endDateTime">End date time</param>
        /// <param name="tnItem">Travel news item to check if active</param>
        /// <returns>true if travel news item is active, false otherwise</returns>
        private bool IsTravelNewsActiveForDriveSection(TDDateTime startDateTime, TDDateTime endDateTime, TravelNewsItem tnItem)
        {
            bool active = false;

            int paddingSecs = 0;

            if (!int.TryParse(Properties.Current["TravelNews.Journey.Road.IncidentTime.Padding.Seconds"], out paddingSecs))
            {
                paddingSecs = 1800; // Default value
            }

            //The incident start date/time will be used for the start date/time
            DateTime incidentStartDateTime = tnItem.StartDateTime;

            // The earliest of expiry date/time or cleared date time will be used for the end date/time
            DateTime incidentEndDateTime = tnItem.ClearedDateTime > tnItem.ExpiryDateTime ? tnItem.ClearedDateTime
                : tnItem.ExpiryDateTime;

            
            // If the date of travel is greater than or equal to the incident start date and less that or equal to 
            // the end date the incident matches the journey date.
            if (startDateTime <= incidentEndDateTime && endDateTime >= incidentStartDateTime)    //incident matches with the road section dates
            {
                // Get the travel news item calendar - The daily start and end datetime
                TravelNewsEventDateTime[] travelNewsItemCalendar = tnItem.TravelNewsCalendar;

                if (travelNewsItemCalendar != null && travelNewsItemCalendar.Length > 0)
                {
                    // The travel news calendar only returns the dates for the day mask specified
                    // This enables not to do further check for day mask here.
                    foreach (TravelNewsEventDateTime tnEventDateTime in travelNewsItemCalendar)
                    {
                        // Get the daily start date time and end date time and apply padding
                        DateTime dailyStartDateTime = tnEventDateTime.DailyStartDateTime.AddSeconds(-1 * paddingSecs);

                        // Daily end time is calculated adding the padding
                        DateTime dailyEndDateTime = tnEventDateTime.DailyEndDateTime.AddSeconds(paddingSecs);

                        if (startDateTime <= dailyEndDateTime && endDateTime >= dailyStartDateTime)
                        {
                            active = true;
                            break; // We found the incident is active so leave the further look up 
                        }
                    }
                }
                else
                {
                    // We not got the calendar, but the journey section matches the travel news incident start and end date time
                    // Set it as active for the journey
                    active = true;

                }
                
            }

            return active;
        }
        
        #endregion
    }
}
