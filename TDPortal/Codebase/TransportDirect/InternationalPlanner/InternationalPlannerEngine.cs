// *********************************************** 
// NAME			: InternationalPlannerEngine.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which performs the core planning of international journeys.
//              : The class retrieves journey schedules data from the database and uses this to construct
//              : the required International journeys
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerEngine.cs-arc  $
//
//   Rev 1.5   Mar 04 2010 16:38:42   mmodi
//Trap a general exception when planing journeys for the different modes
//Resolution for 5436: TD Extra - Error when subtracting a timezpan from a Zero datatime
//
//   Rev 1.4   Feb 16 2010 17:43:52   mmodi
//Carry on planning journeys if one mode throws exception
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 12 2010 09:40:44   mmodi
//Updated to plan train journeys and save journeys to cache
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 09 2010 09:53:08   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:06   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:36   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which performs the core planning of international journeys
    /// </summary>
    public class InternationalPlannerEngine
    {
        #region Private members

        private InternationalPlannerHelper helper;

        private const int Message_JourneyOK_Code = 0;
        private const int Message_UnhandledExcetion_Code = 1;
        private const string Message_JourneyOK_Desc = "OK";

        private InternationalStop[] originStops;
        private InternationalStop[] destinationStops;

        private string originCityID;
        private string destinationCityID;
        private string originName;
        private string destinationName;

        private DateTime outwardDateTime;
        private DayOfWeek outwardDayOfWeek;

        private int minUserTypeLogging = 100;
        bool logResults = false;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerEngine()
		{
            // Initialise required objects
            helper = new InternationalPlannerHelper();

            originStops = new InternationalStop[0];
            destinationStops = new InternationalStop[0];

            outwardDateTime = DateTime.Now;
            outwardDayOfWeek = outwardDateTime.DayOfWeek;

            minUserTypeLogging = Convert.ToInt32(Properties.Current["InternationalPlanner.PlannerControl.MinUserTypeLogging"]);
		}
        
		#endregion

        #region Public methods

        /// <summary>
        /// Method which takes a request and creates the international journey result object containing
        /// international journeys
        /// </summary>
        public IInternationalPlannerResult PlanInternationalJourney(IInternationalPlannerRequest request)
        {
            // Check for the helper
            if (helper == null)
            {
                helper = new InternationalPlannerHelper();
            }

            // The return object
            InternationalPlannerResult result = new InternationalPlannerResult();
            List<InternationalJourney> intlJourneys = new List<InternationalJourney>();

            // Overall try-catch to trap all errors and log them if needed
            try
            {
                if (request != null)
                {
                    // Set result values from the request
                    result.RequestID = request.RequestID;
                    result.SessionID = request.SessionID;

                    // Determine whether to log database get results
                    logResults = (request.UserType >= minUserTypeLogging);

                    #region Set the variables needed by the plan journey methods

                    // City Id
                    originCityID = request.OriginCityID.Trim();
                    destinationCityID = request.DestinationCityID.Trim();

                    // Location names
                    originName = request.OriginName;
                    destinationName = request.DestinationName;

                    // Date to be used in the journey planning
                    outwardDateTime = request.OutwardDateTime;
                    outwardDayOfWeek = outwardDateTime.DayOfWeek;

                    #endregion

                    // If no origin and destination naptans supplied, stop journey planning
                    if ((request.OriginNaptans != null) && (request.OriginNaptans.Length > 0)
                        && (request.DestinationNaptans != null) && (request.DestinationNaptans.Length > 0)
                        && (!string.IsNullOrEmpty(request.OriginCityID)) && (!string.IsNullOrEmpty(request.DestinationCityID)))
                    {
                        #region Modes - Air, Coach, Rail, and Car

                        #region Get the International stops

                        #region Log

                        StringBuilder sbNaptansOrigin = new StringBuilder();
                        foreach (string naptan in request.OriginNaptans)
                        {
                            sbNaptansOrigin.Append(string.Format("[{0}] ", naptan));
                        }

                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Retrieving international stop information for Origin Naptans ( {0} ).",
                                sbNaptansOrigin.ToString()));
                        Logger.Write(oe);

                        #endregion

                        // Get all the international stops the journeys need to be planned for
                        originStops = helper.GetInternationalStop(request.OriginNaptans, logResults);

                        #region Log

                        StringBuilder sbNaptansDestination = new StringBuilder();
                        foreach (string naptan in request.DestinationNaptans)
                        {
                            sbNaptansDestination.Append(string.Format("[{0}] ", naptan));
                        }

                        oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Retrieving international stop information for Destination Naptans ( {0} ).",
                                sbNaptansDestination.ToString()));
                        Logger.Write(oe);

                        #endregion

                        destinationStops = helper.GetInternationalStop(request.DestinationNaptans, logResults);

                        #endregion

                        if ((originStops.Length > 0) && (destinationStops.Length > 0))
                        {
                            #region Plan journeys

                            InternationalJourney[] intlJourneysPlanned = null;

                            TDException tdException = null;
                            
                            // Loop through the requested modes and get the journeys
                            foreach (InternationalModeType mode in request.ModeType)
                            {
                                // If any of the plans fail due to an exception, trap it
                                // and move on to the next mode to be planned. 
                                // At the end, if any exceptions were detected, throw it. Doing this allows
                                // journeys to be returned even if a mode didn't complete
                                try
                                {
                                    switch (mode)
                                    {
                                        case InternationalModeType.Air:
                                            intlJourneysPlanned = PlanInternationalJourneyAir();
                                            break;

                                        case InternationalModeType.Coach:
                                            intlJourneysPlanned = PlanInternationalJourneyCoach();
                                            break;

                                        case InternationalModeType.Rail:
                                            intlJourneysPlanned = PlanInternationalJourneyRail();
                                            break;

                                        case InternationalModeType.Car:
                                            intlJourneysPlanned = PlanInternationalJourneyCar();
                                            break;
                                    }

                                    // Add the journeys planned to the result journeys array
                                    intlJourneys.AddRange(intlJourneysPlanned);
                                }
                                catch (TDException tdEx)
                                {
                                    // Keep the first exception thats thrown
                                    if (tdException == null)
                                    {
                                        tdException = tdEx;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (tdException == null)
                                    {
                                        tdException = new TDException(string.Format(
                                            "Error occurred attempting to plan International journeys for [{0}], Message[{1}].",
                                            mode,
                                            ex.Message), ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourney);
                                    }
                                }
                            }

                            // Was an exception detected for any of the journey planning, if so 
                            // throw it and allow the tidy up to return a result object which was not 
                            // successful, but still can contain most of the journeys
                            if (tdException != null)
                            {
                                throw tdException;
                            }

                            #endregion

                            // Indicate journey planning was OK in the result
                            result.MessageID = Message_JourneyOK_Code;
                            result.MessageDescription = Message_JourneyOK_Desc;
                        }
                        else
                        {
                            #region Log
                            string message = string.Format(
                                "International journeys could not be planned for RequestID[{0}] SessionID[{1}]. " +
                                "No international stops were found for the origin({2}) and/or destination({3}) naptans.",
                                request.RequestID,
                                request.SessionID,
                                sbNaptansOrigin,
                                sbNaptansDestination);

                            // Log warning as this might not necessarily be an error
                            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, message));
                            #endregion

                            // Throw exception to tidy up the return result object
                            throw new TDException(message, true, TDExceptionIdentifier.IPNoInternationalStopsFound);

                        }

                        #endregion
                    }
                    else
                    {
                        #region Log
                        string message = string.Format(
                            "International journeys could not be planned for RequestID[{0}] SessionID[{1}]. " +
                            "No origin/destination naptans and/or city ids were provided in the request.",
                            request.RequestID,
                            request.SessionID);

                        // Log warning as this might not necessarily be an error
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, message));
                        #endregion

                        // Throw exception to tidy up the return result object
                        throw new TDException(message, true, TDExceptionIdentifier.IPNoNaptanCodesFound);
                    }
                }
                else
                {
                    // Throw exception to tidy up the return result object
                    throw new TDException("International journeys could not be planned, no Request object was provided", 
                        false, TDExceptionIdentifier.IPNoRequestObject);
                }
            }
            catch (TDException tdEx)
            {
                #region Log error
                if (!tdEx.Logged)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                        string.Format("Error planning international journeys for RequestID[{0}] SessionID[{1}]. " +
                        "\n TDExceptionIdentifier[{2}] \n Message[{3}]. \n InnerException[{4}]. \n StackTrace[{5}].",
                        (request != null) ? request.RequestID : string.Empty,
                        (request != null) ? request.SessionID : string.Empty,
                        tdEx.Identifier,
                        tdEx.Message,
                        tdEx.InnerException,
                        tdEx.StackTrace));

                    Logger.Write(oe);
                }
                #endregion

                // Exception occurred attempting to plan journeys.
                // Indicate in the return object an error has occurred
                result.MessageID = (int)tdEx.Identifier;
                result.MessageDescription = tdEx.Message;
            }
            catch (Exception ex)
            {
                #region Log error
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Error planning international journeys for RequestID[{0}] SessionID[{1}]. " +
                    "\n Message[{2}]. \n InnerException[{3}]. \n StackTrace[{4}].",
                    (request != null) ? request.RequestID : string.Empty,
                    (request != null) ? request.SessionID : string.Empty,
                    ex.Message,
                    ex.InnerException,
                    ex.StackTrace));

                Logger.Write(oe);
                #endregion

                // Exception occurred attempting to plan journeys.
                // Indicate in the return object an error has occurred
                result.MessageID = Message_UnhandledExcetion_Code;
                result.MessageDescription = ex.Message;
            }

            // Add journeys to the result
            result.InternationalJourneys = intlJourneys.ToArray();

            // Clear the helper to free up resources for garbage collection, any international data from the database
            // is retained in the data cache
            if (helper != null)
            {
                helper = null;
            }

            return result;
        }

        #endregion

        #region Private methods

        #region Plan journeys

        /// <summary>
        /// Method to plan Air international journeys
        /// </summary>
        /// <returns></returns>
        private InternationalJourney[] PlanInternationalJourneyAir()
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Planning Air Journeys.");
            Logger.Write(oe);

            List<InternationalJourney> listIntlJourneys = new List<InternationalJourney>();

            // Only plan from Air to Air stops
            foreach (InternationalStop originStop in originStops)
            {
                if (originStop.StopType == InternationalModeType.Air)
                {
                    foreach (InternationalStop destinationStop in destinationStops)
                    {
                        if (destinationStop.StopType == InternationalModeType.Air)
                        {
                            // Don't plan a journey if its the same origin and destination
                            if (originStop.StopCode != destinationStop.StopCode)
                            {
                                // Get the journeys
                                InternationalJourney[] intlJourneys = helper.GetInternationalJourneyAir(
                                    originStop, destinationStop, originCityID, destinationCityID, outwardDateTime, logResults);

                                if (intlJourneys.Length > 0)
                                {
                                    // Update the Depart from and Arrive at location names to be that specified in the request
                                    intlJourneys = UpdateInternationalJourneysLocationName(intlJourneys, originName, destinationName);
                                }


                                // Add to the running list of journeys
                                listIntlJourneys.AddRange(intlJourneys);
                            }
                            
                        }
                    }
                }
            }

            // Log
            oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        string.Format("International planner - Number of valid Air journeys[{0}]",
                            listIntlJourneys.Count));
            Logger.Write(oe);

            return listIntlJourneys.ToArray();
        }

        /// <summary>
        /// Method to plan Coach international journeys
        /// </summary>
        /// <returns></returns>
        private InternationalJourney[] PlanInternationalJourneyCoach()
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Planning Coach Journeys.");
            Logger.Write(oe);
                        
            List<InternationalJourney> listIntlJourneys = new List<InternationalJourney>();

            // Only plan from Coach to Coach stops
            foreach (InternationalStop originStop in originStops)
            {
                if (originStop.StopType == InternationalModeType.Coach)
                {
                    foreach (InternationalStop destinationStop in destinationStops)
                    {
                        if (destinationStop.StopType == InternationalModeType.Coach)
                        {
                            // Don't plan a journey if its the same origin and destination
                            if (originStop.StopCode != destinationStop.StopCode)
                            {
                                // Get the journeys
                                InternationalJourney[] intlJourneys = helper.GetInternationalJourneyCoach(
                                    originStop, destinationStop, originCityID, destinationCityID, outwardDateTime, logResults);

                                if (intlJourneys.Length > 0)
                                {
                                    // Update the Depart from and Arrive at location names to be that specified in the request
                                    intlJourneys = UpdateInternationalJourneysLocationName(intlJourneys, originName, destinationName);
                                }


                                // Add to the running list of journeys
                                listIntlJourneys.AddRange(intlJourneys);
                            }

                        }
                    }
                }
            }

            // Log
            oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        string.Format("International planner - Number of valid Coach journeys[{0}]",
                            listIntlJourneys.Count));
            Logger.Write(oe);

            return listIntlJourneys.ToArray();
        }

        /// <summary>
        /// Method to plan Rail international journeys
        /// </summary>
        /// <returns></returns>
        private InternationalJourney[] PlanInternationalJourneyRail()
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Planning Rail Journeys.");
            Logger.Write(oe);

            List<InternationalJourney> listIntlJourneys = new List<InternationalJourney>();

            // Only plan from Rail to Rail stops
            foreach (InternationalStop originStop in originStops)
            {
                if (originStop.StopType == InternationalModeType.Rail)
                {
                    foreach (InternationalStop destinationStop in destinationStops)
                    {
                        if (destinationStop.StopType == InternationalModeType.Rail)
                        {
                            // Don't plan a journey if its the same origin and destination
                            if (originStop.StopCode != destinationStop.StopCode)
                            {
                                // Get the journeys
                                InternationalJourney[] intlJourneys = helper.GetInternationalJourneyRail(
                                    originStop, destinationStop, originCityID, destinationCityID, outwardDateTime, logResults);

                                if (intlJourneys.Length > 0)
                                {
                                    // Update the Depart from and Arrive at location names to be that specified in the request
                                    intlJourneys = UpdateInternationalJourneysLocationName(intlJourneys, originName, destinationName);
                                }


                                // Add to the running list of journeys
                                listIntlJourneys.AddRange(intlJourneys);
                            }

                        }
                    }
                }
            }

            // Log
            oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        string.Format("International planner - Number of valid Rail journeys[{0}]",
                            listIntlJourneys.Count));
            Logger.Write(oe);

            return listIntlJourneys.ToArray();
        }

        /// <summary>
        /// Method to plan Car international journeys
        /// </summary>
        /// <returns></returns>
        private InternationalJourney[] PlanInternationalJourneyCar()
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Planning Car Journeys.");
            Logger.Write(oe);


            // Get the journeys
            InternationalJourney[] intlJourneys = helper.GetInternationalJourneyCar(
                originCityID, destinationCityID, outwardDateTime, logResults);

            if (intlJourneys.Length > 0)
            {
                // Update the Depart from and Arrive at location names to be that specified in the request
                intlJourneys = UpdateInternationalJourneysLocationName(intlJourneys, originName, destinationName);
            }


            // Log
            oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        string.Format("International planner - Number of valid Car journeys[{0}]",
                            intlJourneys.Length));
            Logger.Write(oe);

            return intlJourneys;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Method which updates the depart and arrival names of the first and last detail leg
        /// to be that which is specified in the request
        /// </summary>
        /// <param name="intlJourneys"></param>
        /// <returns></returns>
        private InternationalJourney[] UpdateInternationalJourneysLocationName(InternationalJourney[] intlJourneys, string originName, string destinationName)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Updating start and end location names for journeys.");
            Logger.Write(oe);

            foreach (InternationalJourney ij in intlJourneys)
            {
                // First update the journey depart and arrive names
                ij.DepartureName = originName;
                ij.ArrivalName = destinationName;

                // And update the first and last detail legs to have the same name as the request,
                // with all others having the name of the stop
                if ((ij.JourneyDetails != null) && (ij.JourneyDetails.Length > 0))
                {
                    for (int i = 0; i < ij.JourneyDetails.Length; i++)
                    {
                        InternationalJourneyDetail ijd = ij.JourneyDetails[i];

                        // Check for international stops and update names
                        ijd.DepartureName = (ijd.DepartureStop != null) ? ijd.DepartureStop.StopName : string.Empty;
                        ijd.ArrivalName = (ijd.ArrivalStop != null) ? ijd.ArrivalStop.StopName : string.Empty;

                        #region Suffix terminal number

                        // If the name has a terminal number, suffix the location with "Terminal ..."
                        if (!string.IsNullOrEmpty(ijd.TerminalNumberFrom))
                        {
                            ijd.DepartureName = string.Format("{0} Terminal {1}", ijd.DepartureName, ijd.TerminalNumberFrom);
                        }

                        // If the name has a terminal number, suffix the location with "Terminal ..."
                        if (!string.IsNullOrEmpty(ijd.TerminalNumberTo))
                        {
                            ijd.ArrivalName = string.Format("{0} Terminal {1}", ijd.ArrivalName, ijd.TerminalNumberTo);
                        }

                        #endregion

                        // First detail leg
                        if (i == 0)
                        {
                            ijd.DepartureName = originName;
                        }
                        
                        // Last leg
                        if (i == (ij.JourneyDetails.Length - 1))
                        {
                            ijd.ArrivalName = destinationName;
                        }
                    }
                }
            }

            return intlJourneys;
        }

        #endregion

        #endregion
    }
}
