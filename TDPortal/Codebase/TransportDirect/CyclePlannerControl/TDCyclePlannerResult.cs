// *********************************************** 
// NAME			: TDCyclePlannerResult.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which contains the journey details returned from the Cycle planner
//              : converted to a TDCyclePlannerResult
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/TDCyclePlannerResult.cs-arc  $
//
//   Rev 1.14   Sep 29 2010 11:26:18   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.13   Jun 04 2009 13:36:52   mmodi
//Only populate the latitude longitude coordinates for non-reference transactions
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.12   Jun 03 2009 11:14:16   mmodi
//Added methods to populate the Latitiude Longitude values for a Cycle journey
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.11   Dec 11 2008 14:11:46   mmodi
//Catch exception when Maps handoff fails and nullify the result
//Resolution for 5208: Cycle Planner - Results title summary throws exception when results fail
//
//   Rev 1.10   Oct 27 2008 16:05:36   mmodi
//Removed comments
//
//   Rev 1.9   Oct 27 2008 14:02:56   mturner
//Changed to not create an ESRI mapping object for InjectedTransactions
//
//   Rev 1.8   Oct 15 2008 11:26:30   mmodi
//Corrected cycle xml logging
//
//   Rev 1.7   Sep 16 2008 16:44:08   mmodi
//Updated to use Point value and not pass Toids in request
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Sep 08 2008 15:45:46   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Sep 02 2008 10:38:36   mmodi
//Display messages for CJP user
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 22 2008 10:10:04   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 06 2008 14:50:02   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 04 2008 10:19:50   mmodi
//Updates to work with actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:37:50   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:42:02   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Web;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using CyclePlannerWebService = TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

using Logger = System.Diagnostics.Trace;
using CCP = TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;
using LS = TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class TDCyclePlannerResult : ITDCyclePlannerResult, ITDSessionAware
    {
        #region Private members
        
        private int journeyReferenceNumber;
        private int lastReferenceSequence;

        private TDDateTime timeOutward;
        private TDDateTime timeReturn;
        private bool arriveBeforeOutward;
        private bool arriveBeforeReturn;

        private int routeNum;

        // Journeys
        private ArrayList outwardCycleJourney = new ArrayList(1);
        private ArrayList returnCycleJourney = new ArrayList(1);
        private int outwardJourneyIndex;
        private int returnJourneyIndex;

        // Messages and errors
        private ArrayList messageList = new ArrayList();
        private bool correctReturnNoCycleJourney;

        private bool valid;
        private bool isDirty = true;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public TDCyclePlannerResult()
        {
        }

        public TDCyclePlannerResult(int journeyReferenceNumber)
        {
            this.journeyReferenceNumber = journeyReferenceNumber;
        }

        public TDCyclePlannerResult(int journeyReferenceNumber, int startRouteNum, 
            TDDateTime timeOutward, TDDateTime timeReturn, bool arriveBeforeOutward, bool arriveBeforeReturn)
            : this(journeyReferenceNumber)
		{
			this.timeOutward = timeOutward;
			this.timeReturn = timeReturn;
			this.arriveBeforeOutward = arriveBeforeOutward;
			this.arriveBeforeReturn = arriveBeforeReturn;
			this.routeNum = startRouteNum;
		}

        #endregion

        #region Public methods

        #region Add CyclePlannerResult
        /// <summary>
        /// Add the CyclePlannerResult to this TDCyclePlannerResult.  
        /// The outward flag determines where the information will be added.
        /// </summary>
        /// <param name="result">The result from the Cycle planner</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        /// <param name="publicVia">A location for use when searching if any of the locations used are a via point</param>
        /// <param name="privateVia">A location private journey is searching via</param>
        /// <param name="sessionId">The current session id (used in logging)</param>
        /// <returns>true if at least one journey was found, false otherwise</returns>
        public bool AddResult(CyclePlannerWebService.JourneyResult result, bool outward, TDLocation cycleVia, 
            TDLocation requestOrigin, TDLocation requestDestination, string sessionId, int userType,
            string polylinesTransformXslt, bool referenceTransaction, TDCycleJourneyResultSettings resultSettings, bool eesRequest)
        {
            if (result == null)
            {
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    "CyclePlanner - Unexpected null CyclePlannerResult when attempting to add journey to result object, ref = " + journeyReferenceNumber.ToString());
                Logger.Write(oe);
                return false;
            }

            isDirty = true;

            // Check for a cycle journey result            
            if (result.journeys != null && result.journeys.Length > 0)
            {
                if (result.journeys.Length == CyclePlannerConstants.ExpectedCycleJourneyCount)
                {
                    for (int i = 0; i < result.journeys.Length; i++)
                    {
                        int newRouteNum = GetRouteNum();

                        bool handOffToMapsOK = true;

                        if (!referenceTransaction && !eesRequest)
                        {
                            handOffToMapsOK = HandOffJourneyToEsriMap(result.journeys[i], newRouteNum, sessionId, polylinesTransformXslt);
                        }

                        if (handOffToMapsOK)
                        {
                            // Create the cycle journey
                            if (outward)
                            {
                                CycleJourney cj = new CycleJourney(outwardJourneyIndex++, result.journeys[i], newRouteNum, requestOrigin, requestDestination, cycleVia, timeOutward, arriveBeforeOutward, resultSettings);

                                if (!referenceTransaction)
                                {
                                    // Populate the cycle journey latitude/longitude coordinates (needed for the GPX download)
                                    PopulateCycleJourneyCoordinates(cj);
                                }

                                outwardCycleJourney.Add(cj);
                            }
                            else
                            {
                                CycleJourney cj = new CycleJourney(returnJourneyIndex++, result.journeys[i], newRouteNum, requestDestination, requestOrigin, cycleVia, timeReturn, arriveBeforeReturn, resultSettings);

                                if (!referenceTransaction)
                                {
                                    // Populate the cycle journey latitude/longitude coordinates (needed for the GPX download)
                                    PopulateCycleJourneyCoordinates(cj);
                                }

                                returnCycleJourney.Add(cj);
                            }
                        }                        
                    }
                }
                else
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
                        "CyclePlanner - Unexpected number of cycle journeys returned, expected " + CyclePlannerConstants.ExpectedCycleJourneyCount.ToString() + " journeys, but result contained " + result.journeys.Length + " journeys - all cycle journeys discarded."));
                }
            }

            AddMessages(result.messages, outward, userType);

            bool thisPartValid = false;

            if (outward)
            {
                if (outwardCycleJourney.Count > 0)
                {
                    valid = true;
                    thisPartValid = true;
                }
            }
            else
            {
                if (returnCycleJourney.Count > 0)
                {
                    valid = true;
                    thisPartValid = true;
                }
            }
            return thisPartValid;
        }

        #endregion

        #region Journey indexes
        /// <summary>
        /// Gets the display number for the selected Outward journey
        /// </summary>
        public string OutwardDisplayNumber(int journeyIndex)
        {
            bool found = false;
            int index = 0;

            if (outwardCycleJourney != null)
            {
                for (int i = 0; i < OutwardCycleJourneyCount; i++, index++)
                {
                    found = (((CycleJourney)outwardCycleJourney[i]).JourneyIndex == journeyIndex);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if (found)
            {
                return (index + 1).ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Gets the display number for the selected Return journey
        /// </summary>
        public string ReturnDisplayNumber(int journeyIndex)
        {
            bool found = false;
            int index = 0;

            if (returnCycleJourney != null)
            {
                for (int i = 0; i < ReturnCycleJourneyCount; i++, index++)
                {
                    found = (((CycleJourney)returnCycleJourney[i]).JourneyIndex == journeyIndex);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if (found)
            {
                return (index + 1).ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Returns the SelectedOutwardJourneyIndex of the outward cycle journey specified by a JourneyIndex.
        /// JourneyIndex is the index given by the order the journey was added as a result.
        /// SelectedOutwardJourneyIndex is the index given by the chronological order in result set.
        /// </summary>
        /// <param name="journeyIndex">Journey Index to search for.</param>
        /// <returns>Index or -1 if no match was found.</returns>
        public int GetSelectedOutwardJourneyIndex(int journeyIndex)
        {
            int i = 0;
            bool found = false;

            for (i = 0; i < outwardCycleJourney.Count; i++)
            {
                found = (((CycleJourney)outwardCycleJourney[i]).JourneyIndex == journeyIndex);

                if (found)
                {
                    break;
                }
            }

            if (found)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Returns the SelectedReturnJourneyIndex of the return cycle journey specified by a JourneyIndex.
        /// JourneyIndex is the index given by the order the journey was added as a result.
        /// SelectedReturnJourneyIndex is the index given by the chronological order in result set.
        /// </summary>
        /// <param name="journeyIndex">Journey Index to search for.</param>
        /// <returns>Index or -1 if no match was found.</returns>
        public int GetSelectedReturnJourneyIndex(int journeyIndex)
        {
            int i = 0;
            bool found = false;

            for (i = 0; i < returnCycleJourney.Count; i++)
            {
                found = (((CycleJourney)returnCycleJourney[i]).JourneyIndex == journeyIndex);

                if (found)
                {
                    break;
                }
            }

            if (found)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region Get/Add journey

        /// <summary>
        /// Returns the outward cycle journey. 
        /// This overloaded method returns the first journey in the cycle journeys array
        /// </summary>
        /// <returns>A CycleJourney if it exists otherwise null.</returns>
        public CycleJourney OutwardCycleJourney()
        {
            if (outwardCycleJourney == null)
            {
                return null;
            }
            else
            {
                return (CycleJourney)outwardCycleJourney[0];
            }
        }

        /// <summary>
        /// Returns the return cycle journey.
        /// This overloaded method returns the first journey in the cycle journeys array
        /// </summary>
        /// <returns>A CycleJourney if it exists otherwise null.</returns>
        public CycleJourney ReturnCycleJourney()
        {
            if ((returnCycleJourney == null) || (returnCycleJourney.Count < 1))
            {
                return null;
            }
            else
            {
                return (CycleJourney)returnCycleJourney[0];
            }
        }

        /// <summary>
        /// Returns the return public journey with the given journey index.
        /// </summary>
        /// <param name="journeyIndex">Journey Index to search for.</param>
        /// <returns>Public journey or null if no match was found.</returns>
        public CycleJourney OutwardCycleJourney(int journeyIndex)
        {
            int i = GetSelectedOutwardJourneyIndex(journeyIndex);

            if (i >= 0)
            {
                return (CycleJourney)outwardCycleJourney[i];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the return cycle journey with the given journey index.
        /// </summary>
        /// <param name="journeyIndex">Journey Index to search for.</param>
        /// <returns>Public journey or null if no match was found.</returns>
        public CycleJourney ReturnCycleJourney(int journeyIndex)
        {
            int i = GetSelectedReturnJourneyIndex(journeyIndex);

            if (i >= 0)
            {
                return (CycleJourney)returnCycleJourney[i];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Inserts the CycleJourney to back into cycle journey array, overwriting itself.
        /// This method should only be used to inject the CycleJourney back into the Result object
        /// for a cycle journey retrieved from the Result object.
        /// If the cycle journey does not exist in the array, it is not inserted.
        /// </summary>
        /// <param name="cycleJourney"></param>
        public void AddOutwardCycleJourney(CycleJourney cycleJourney)
        {
            for (int i = 0; i < outwardCycleJourney.Count; i++)
            {
                if (((CycleJourney)outwardCycleJourney[i]).JourneyIndex == cycleJourney.JourneyIndex)
                {
                    outwardCycleJourney[i] = cycleJourney;

                    isDirty = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Inserts the CycleJourney to back into cycle journey array, overwriting itself.
        /// This method should only be used to inject the CycleJourney back into the Result object
        /// for a cycle journey retrieved from the Result object.
        /// If the cycle journey does not exist in the array, it is not inserted.
        /// </summary>
        /// <param name="cycleJourney"></param>
        public void AddReturnCycleJourney(CycleJourney cycleJourney)
        {
            for (int i = 0; i < returnCycleJourney.Count; i++)
            {
                if (((CycleJourney)returnCycleJourney[i]).JourneyIndex == cycleJourney.JourneyIndex)
                {
                    returnCycleJourney[i] = cycleJourney;

                    isDirty = true;
                    break;
                }
            }
        }

        #endregion

        #region Journey summary line

        /// <summary>
        /// Constructs a Journey Summary Lines for the outward journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <param name="modeType">Indicates transport modes to be used</param>
        /// <returns>A Journey Summary Line array for the outward journeys.</returns>
        public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType)
        {
            return GetJourneySummary(outwardCycleJourney, timeOutward, arriveBefore);
        }

        /// <summary>
        /// Constructs a Journey Summary Lines for the return journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <param name="modeType">Indicates transport modes to be used</param>
        /// <returns>A Journey Summary Line array for the outward journeys.</returns>
        public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType)
        {
            return GetJourneySummary(returnCycleJourney, timeReturn, arriveBefore);
        }

        #endregion

        #region Helper methods
        /// <summary>
        /// Tests if the end time for any outward journey exceeds the
        /// start time of any return journey
        /// </summary>
        /// <param name="journeyRequest">The original journey request</param>
        /// <returns>True if any outward journey end time exceeds the start of any
        /// return journey start time or false otherwise</returns>
        public bool CheckForReturnOverlap(ITDCyclePlannerRequest request)
        {
            bool overlap = false;

            if (request != null)
            {
                // Check the start time of the return journey against the
                // end time of the outward journey
                if (!overlap)
                {
                    CycleJourney outCycleJourney = OutwardCycleJourney();
                    CycleJourney retCycleJourney = ReturnCycleJourney();

                    if (outCycleJourney != null && retCycleJourney != null)
                    {
                        TDDateTime outEndTime = outCycleJourney.CalculateEndTime(request.OutwardDateTime[0], request.OutwardArriveBefore);
                        TDDateTime retStartTime = retCycleJourney.CalculateStartTime(request.ReturnDateTime[0], request.ReturnArriveBefore);
                        overlap = outEndTime > retStartTime;
                    }
                }
            }
            return overlap;
        }

        /// <summary>
        /// Tests if any of the journeys returned start in the past
        /// </summary>
        /// <param name="journeyRequest">The original journey request</param>
        /// <returns>True if any journey start time is in the past</returns>                
        public bool CheckForJourneyStartInPast(ITDCyclePlannerRequest request)
        {
            //date time variable to hold current date and time
            TDDateTime timeNow = TDDateTime.Now;

            if (request != null)
            {
                //check outward road journey
                CycleJourney outwardCycleJourney = OutwardCycleJourney();

                if (outwardCycleJourney != null)
                {
                    TDDateTime outStartTime = outwardCycleJourney.CalculateStartTime(request.OutwardDateTime[0], request.OutwardArriveBefore);
                    if (outStartTime < timeNow)
                    {
                        return true;
                    }
                }

                //check return road journey
                CycleJourney returnCycleJourney = ReturnCycleJourney();

                if (returnCycleJourney != null)
                {
                    TDDateTime retStartTime = returnCycleJourney.CalculateStartTime(request.ReturnDateTime[0], request.ReturnArriveBefore);
                    if (retStartTime < timeNow)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Messages

        // Array is of displayable messages, so we don't 
        //   want multiple messages with the same text ...
        public void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode)
        {
            isDirty = true;
            foreach (CyclePlannerMessage msg in messageList)
            {
                if (msg.MessageText == description && msg.MessageResourceId == resourceId)
                {
                    return;
                }
            }

            messageList.Add(new CyclePlannerMessage(description, resourceId, majorCode, minorCode));
        }

        public void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode, ErrorsType type)
        {
            isDirty = true;
            foreach (CyclePlannerMessage msg in messageList)
            {
                if (msg.MessageText == description && msg.MessageResourceId == resourceId)
                {
                    return;
                }
            }

            messageList.Add(new CyclePlannerMessage(description, resourceId, majorCode, minorCode, type));
        }

        /// <summary>
        /// Clears the current list of messages.
        /// </summary>
        public void ClearMessages()
        {
            isDirty = true;
            messageList.Clear();
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Logs messages returned by the Cycle Planner Service, adding messages to the displayable message 
        /// list where required
        /// </summary>
        /// <param name="messagesFromCyclePlanner"></param>
        /// <param name="outward"></param>
        private void AddMessages(CyclePlannerWebService.Message[] messagesFromCyclePlanner, bool outward, int userType)
        {
            if (messagesFromCyclePlanner == null || messagesFromCyclePlanner.Length == 0)
            {
                return;
            }

            // Determine if we need to show any messages for CJP users on the Portal results page
            bool showExternalMessages = (userType >= Int32.Parse(Properties.Current[CyclePlannerConstants.MinLoggingUserType]));
            
            bool logNoJourneyResponses = bool.Parse(Properties.Current[CyclePlannerConstants.LogNoJourneyResponses]);
            bool logCyclePlannerFailures = bool.Parse(Properties.Current[CyclePlannerConstants.LogCyclePlannerFailures]);

            foreach (CyclePlannerWebService.Message msg in messagesFromCyclePlanner)
            {
                if (msg.code == CyclePlannerConstants.CPOK) // OK - no need to do anything
                {
                    continue;
                }
                else if (msg.code == CyclePlannerConstants.CPNoJourneyCouldBeFound)
                {
                    // Flag that no cycle journeys were found by the Cycle Planner service,
                    // but also that this is not necessarily an error ...
                    this.correctReturnNoCycleJourney = true;

                    if (logNoJourneyResponses)
                    {
                        LogCyclePlanerMessage(msg.description, msg.code);
                    }

                    // Display actual message for CJP user
                    if (showExternalMessages)
                    {
                        AddMessageToArray(msg.description, CyclePlannerConstants.CPExternalMessage, msg.code, 0);
                    }
                }
                else
                {
                    if (logCyclePlannerFailures)
                    {
                        LogCyclePlanerMessage(msg.description, msg.code);
                    }

                    // Display actual message for CJP user
                    if (showExternalMessages)
                    {
                        AddMessageToArray(msg.description, CyclePlannerConstants.CPExternalMessage, msg.code, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Logs the message contained in the result object from the Cycle Planner Service
        /// </summary>
        /// <param name="originalMessage"></param>
        /// <param name="majorCode"></param>
        private void LogCyclePlanerMessage(string originalMessage, int majorCode)
        {
            StringBuilder logMsg = new StringBuilder();

            logMsg.Append("CyclePlanner - CyclePlannerResult included message Code: ");
            logMsg.Append(majorCode.ToString());
            logMsg.Append(",  Message: ");
            logMsg.Append(originalMessage);
            logMsg.Append(". For request ref: ");
            logMsg.Append(journeyReferenceNumber.ToString());

            OperationalEvent operationalEvent = new OperationalEvent
                (TDEventCategory.CJP, TDTraceLevel.Error, logMsg.ToString());

            Logger.Write(operationalEvent);
        }

        /// <summary>
        /// Creates a unique route number for each route within a TDCyclePlannerResult.
        /// </summary>
        /// <returns>The next integer a unary series, starting with 1</returns>
        private int GetRouteNum()
        {
            return ++routeNum;
        }


        /// <summary>
        /// Creates a Journey Summary Lines.
        /// </summary>
        /// <param name="cycleJourneys">Cycle Journeys to create summary lines for.</param>
        /// <returns>A Journey Summary Line array.</returns>
        private JourneySummaryLine[] GetJourneySummary
            (ArrayList cycleJourneys, TDDateTime time, bool arriveBefore)
        {
            int numberOfResults = 0;
            int index = 0;
            int displayNumber = 1;

            // Set numberOfResults to consist only of journeys we want to return
            if (cycleJourneys != null)
            {
                foreach (CycleJourney journey in cycleJourneys)
                {
                    numberOfResults++;
                }
            }

            // Create the Journey Summary Line array.
            JourneySummaryLine[] result = new JourneySummaryLine[numberOfResults];

            // Build the array.

            // Add the Cycle journey to the Summary Line.
            if (cycleJourneys != null)
            {
                foreach (CycleJourney journey in cycleJourneys)
                {
                    if (journey.Type == TDJourneyType.Cycle)
                    {
                        ModeType[] cycle = new ModeType[] { ModeType.Cycle };
                        TDDateTime start = journey.CalculateStartTime(time, arriveBefore);
                        TDDateTime end = journey.CalculateEndTime(time, arriveBefore);

                        result[index++] = new JourneySummaryLine(
                            journey.JourneyIndex, journey.Type, cycle,
                            0, start, end, journey.TotalDistance, displayNumber.ToString());

                        displayNumber++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Method which returns the cycle route Geometry in xml.
        /// (to be sent to the Map control)
        /// </summary>
        /// <returns></returns>
        private string GetCycleRouteXML(CyclePlannerWebService.Journey cycleJourney, string polylinesTransformXslt)
        {
            try
            {
                // Xslt file
                //string xsltPath = HttpContext.Current.Server.MapPath(Properties.Current["CyclePlanner.InteractiveMapping.Map.CycleJourney.Xslt"]);
                string xsltPath = polylinesTransformXslt; // AppDomain.CurrentDomain.BaseDirectory + (Properties.Current["CyclePlanner.InteractiveMapping.Map.CycleJourney.Xslt"]);

                // Get the xml for the cycle journey
                string cycleJourneyXml = ConvertToXML(cycleJourney);

                // Read in the xml
                StringReader stringReader = new StringReader(cycleJourneyXml);

                XmlReader xmlReader = XmlReader.Create(stringReader);
                XPathDocument xmlDocument = new XPathDocument(xmlReader);

                // Read in the xslt
                XslCompiledTransform xslTransform = new XslCompiledTransform();
                xslTransform.Load(xsltPath);

                // Set up the output 
                StringBuilder outputXml = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(outputXml);

                // Do the transformation
                xslTransform.Transform(xmlDocument, xmlWriter);

                // Tidy up
                xmlWriter.Close();
                xmlReader.Close();

                // Removing any leading/trailing spaces in the polylines
                string outputXmlFormatted = outputXml.ToString();
                outputXmlFormatted = outputXmlFormatted.Replace("<polyline> ", "<polyline>");
                outputXmlFormatted = outputXmlFormatted.Replace(" </polyline>", "</polyline>");

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        "CyclePlanner - Maps, cycle journey polyline xml generated for Mapping: " + outputXmlFormatted.ToString()));
                }

                return outputXmlFormatted;
            }
            catch (Exception ex)
            {
                string message = "CyclePlanner - Maps, exception thrown attempting to generate polyline xml to be sent to Mapping. " + ex.Message;

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));

                return string.Empty;
            }
        }

        private bool HandOffJourneyToEsriMap(CyclePlannerWebService.Journey cycleJourney, int routeNum,
            string sessionId, string polylinesTransformXslt)
        {
            try
            {
                // Ensure we have the xslt to avoid attempting handoff which will fail
                if (!string.IsNullOrEmpty(polylinesTransformXslt))
                {
                    ITDMapHandoff handoff = (ITDMapHandoff)TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];

                    // Generate the cycle route xml 
                    string cycleRouteXml = GetCycleRouteXML(cycleJourney, polylinesTransformXslt);

                    // Create an ESRI CycleRoute object 
                    // (This then allows ESRI to retrieve it as a CycleRoute and generate a map)
                    if (!string.IsNullOrEmpty(cycleRouteXml))
                    {
                        TransportDirect.Presentation.InteractiveMapping.CycleRoute cycleRoute = new TransportDirect.Presentation.InteractiveMapping.CycleRoute(cycleRouteXml);

                        // serialise so it can be saved to the database
                        MemoryStream ms = new MemoryStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ms, cycleRoute);

                        handoff.SaveJourneyResult(sessionId, routeNum, ms);

                        // all OK
                        return true;
                    }
                }
                else
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            "CyclePlanner - Maps, cycle journey polyline tranform xslt file was not provided in method HandOffJourneyToEsriMap()"));
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "CyclePlanner - Maps, exception thrown attempting to hand off journey to Mapping. " + ex.Message;

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));
            }

            // Exception or failed handing off
            return false;
        }

        /// <summary>
        /// Create an XML representtaion of the specified object,
        /// with leading whitespace trimmed
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>XML string</returns>
        private string ConvertToXML(object obj)
        {
            XmlSerializer xmls = new XmlSerializer(obj.GetType());
            StringWriter sw = new StringWriter();
            xmls.Serialize(sw, obj);
            sw.Close();
            // strip out leading spaces to conserve space
            Regex re = new Regex("\\r\\n\\s+");
            return (re.Replace(sw.ToString(), "\r\n"));
        }


        /// <summary>
        /// Method which updates the various Location and Detail OSGR coordiantes to Latitude Longitudes (which
        /// are needed by the GPX download page).
        /// </summary>
        private void PopulateCycleJourneyCoordinates(CycleJourney cycleJourney)
        {
            string logMessge = string.Empty;

            logMessge = "PopulateCycleJourneyCoordinates using CoordinateConvertor - STARTED";
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logMessge));

            try
            {
                // Get the service used to convert the coordinates
                ICoordinateConvertor coordinateConvertor = (ICoordinateConvertor)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoordinateConvertorFactory];

                CCP.OSGridReference ccpOSGR;
                CCP.LatitudeLongitude ccpLatLong;
                LS.LatitudeLongitude lsLatLong;
                
                #region Origin Location
                if (cycleJourney.OriginLocation.GridReference.IsValid)
                {
                    logMessge = string.Format("Calling CoordinateConvertor for Origin Location [{0}].", cycleJourney.OriginLocation.Description);
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logMessge));

                    // Convert the origin location
                    ccpOSGR = new CCP.OSGridReference();
                    ccpOSGR.Easting = cycleJourney.OriginLocation.GridReference.Easting;
                    ccpOSGR.Northing = cycleJourney.OriginLocation.GridReference.Northing;

                    // Call web service
                    ccpLatLong = coordinateConvertor.GetLatitudeLongitude(ccpOSGR);

                    // Convert the external LatLong to a local version
                    lsLatLong = new LS.LatitudeLongitude(ccpLatLong.Latitude, ccpLatLong.Longitude);

                    // Add the latlong coordinate to the location
                    cycleJourney.OriginLocation.LatitudeLongitudeCoordinate = lsLatLong;
                }
                #endregion

                #region Destination Location
                if (cycleJourney.DestinationLocation.GridReference.IsValid)
                {
                    logMessge = string.Format("Calling CoordinateConvertor for Destination Location [{0}].", cycleJourney.DestinationLocation.Description);
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logMessge));

                    // Convert the destination location
                    ccpOSGR = new CCP.OSGridReference();
                    ccpOSGR.Easting = cycleJourney.DestinationLocation.GridReference.Easting;
                    ccpOSGR.Northing = cycleJourney.DestinationLocation.GridReference.Northing;

                    // Call web service
                    ccpLatLong = coordinateConvertor.GetLatitudeLongitude(ccpOSGR);

                    // Convert the external LatLong to a local version
                    lsLatLong = new LS.LatitudeLongitude(ccpLatLong.Latitude, ccpLatLong.Longitude);

                    // Add the latlong coordinate to the location
                    cycleJourney.DestinationLocation.LatitudeLongitudeCoordinate = lsLatLong;
                }
                #endregion
                    
                #region Via Location
                // Convert the via location
                if ((cycleJourney.RequestedViaLocation != null) && (cycleJourney.RequestedViaLocation.GridReference.IsValid))
                {
                    logMessge = string.Format("Calling CoordinateConvertor for Via Location [{0}].", cycleJourney.RequestedViaLocation.Description);
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logMessge));

                    ccpOSGR = new CCP.OSGridReference();
                    ccpOSGR.Easting = cycleJourney.RequestedViaLocation.GridReference.Easting;
                    ccpOSGR.Northing = cycleJourney.RequestedViaLocation.GridReference.Northing;

                    // Call web service
                    ccpLatLong = coordinateConvertor.GetLatitudeLongitude(ccpOSGR);

                    // Convert the external LatLong to a local version
                    lsLatLong = new LS.LatitudeLongitude(ccpLatLong.Latitude, ccpLatLong.Longitude);

                    // Add the latlong coordinate to the location
                    cycleJourney.RequestedViaLocation.LatitudeLongitudeCoordinate = lsLatLong;
                }
                #endregion

                #region Details

                if (cycleJourney.Details != null)
                {
                    for (int j = 0; j < cycleJourney.Details.Length; j++)
                    {
                        CycleJourneyDetail detail = cycleJourney.Details[j];

                        LS.OSGridReference[] lsOSGRSs = detail.GetAllOSGRGridReferences();
                        CCP.OSGridReference[] ccpOSGRs = new CCP.OSGridReference[lsOSGRSs.Length];

                        if (lsOSGRSs.Length > 0)
                        {
                            logMessge = string.Format("Calling CoordinateConvertor for the cycle journey detail. [{0}] OSGR coordinates.", lsOSGRSs.Length.ToString());
                            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logMessge));

                            for (int i = 0; i < lsOSGRSs.Length; i++)
                            {
                                ccpOSGRs[i] = new CCP.OSGridReference();
                                ccpOSGRs[i].Easting = lsOSGRSs[i].Easting;
                                ccpOSGRs[i].Northing = lsOSGRSs[i].Northing;
                            }

                            // Call web service
                            CCP.LatitudeLongitude[] ccpLatLongs = coordinateConvertor.GetLatitudeLongitude(ccpOSGRs);

                            // Convert the external LatLongs to a local version
                            LS.LatitudeLongitude[] lsLatLongs = new LS.LatitudeLongitude[ccpLatLongs.Length];

                            for (int i = 0; i < ccpLatLongs.Length; i++)
                            {
                                lsLatLongs[i] = new LS.LatitudeLongitude(ccpLatLongs[i].Latitude, ccpLatLongs[i].Longitude);
                            }

                            // Add the latlong coordinates to the cycle journey detail
                            detail.UpdateLatitudeLongitudeCoordinates(lsLatLongs);
                        }
                    }

                    // All coordinates have been set OK. Flag placed here because the coordinates for 
                    // Details need to be converted to allow an appropriate GPX file to be created
                    cycleJourney.LatitudeLongitudesAreValid = true;
                }

                #endregion
            }
            catch (Exception ex)
            {
                // Because all of the coordinates coule not be converted, set flag in the journey to indicate
                // not to use the latitude longitude coordinates. This will prevent the GPX download file 
                // from being generated if requested.
                cycleJourney.LatitudeLongitudesAreValid = false;

                string message = "CyclePlanner - Populating the LatitudeLongitude coordinates using the CoordinateConvertor threw an exception: " + ex.Message;

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));
            }

            logMessge = string.Format("PopulateCycleJourneyCoordinates using CoordinateConvertor - COMPLETED - Success[{0}].", cycleJourney.LatitudeLongitudesAreValid.ToString());
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logMessge));
        }

        #endregion

        #region Public properties
        
        /// <summary>
        /// Read/write. Journey reference number
        /// </summary>
        public int JourneyReferenceNumber
        {
            get { return journeyReferenceNumber; }
            set { journeyReferenceNumber = value; }
        }

        /// <summary>
        /// Read/write. Last reference sequence number
        /// </summary>
        public int LastReferenceSequence
        {
            get { return lastReferenceSequence; }
            set
            {
                isDirty = true;
                lastReferenceSequence = value;
            }
        }

        /// <summary>
        /// Read. Number of Outward cycle journeys
        /// </summary>
        public int OutwardCycleJourneyCount
        {
            get { return (outwardCycleJourney == null ? 0 : outwardCycleJourney.Count); }
        }

        /// <summary>
        /// Read. Number of Return cycle journeys
        /// </summary>
        public int ReturnCycleJourneyCount
        {
            get { return (returnCycleJourney == null ? 0 : returnCycleJourney.Count); }
        }

        /// <summary>
        /// Read-write property. Exposes the last-used outward 
        /// journey index within the result
        /// </summary>
        public int OutwardJourneyIndex
        {
            get { return outwardJourneyIndex; }
            set { outwardJourneyIndex = value; }
        }

        /// <summary>
        /// Read-write property. Exposes the last-used return
        /// journey index within the result
        /// </summary>
        public int ReturnJourneyIndex
        {
            get { return returnJourneyIndex; }
            set { returnJourneyIndex = value; }
        }

        /// <summary>
        /// Read-only property giving access to all outward cycle journeys for this result object
        /// </summary>
        public ArrayList OutwardCycleJourneys
        {
            get { return outwardCycleJourney; }
        }

        /// <summary>
        /// Read-only property giving access to all return cycle journeys for this result object
        /// </summary>
        public ArrayList ReturnCycleJourneys
        {
            get { return returnCycleJourney; }
        }

        /// <summary>
        /// Read only. Indicates if cycle planner returned ok but no cycle journeys were found
        /// </summary>
        public bool CyclePlannerValidError
        {
            get { return correctReturnNoCycleJourney; }
        }

        /// <summary>
        /// Read the messages returned by the Cycle planner
        /// </summary>
        public CyclePlannerMessage[] CyclePlannerMessages
        {
            get { return (CyclePlannerMessage[])messageList.ToArray(typeof(CyclePlannerMessage)); }
        }

        /// <summary>
        /// Read/write. Set the validatity of this result object
        /// </summary>
        public bool IsValid
        {
            get { return valid; }
            set
            {
                isDirty = true;
                valid = value;
            }
        }
        #endregion

        #region ITDSessionAware implementation

        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        #endregion
    }
}
