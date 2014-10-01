// *********************************************** 
// NAME			: TDGradientProfileResult.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/07/2008
// DESCRIPTION	: Implementation of the ITDGradientProfileResult
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/TDGradientProfileResult.cs-arc  $
//
//   Rev 1.3   Feb 12 2009 12:44:20   mturner
//Fixed session management bug
//Resolution for 5245: Cycle Planner - After Amend Map page shows wrong journey
//
//   Rev 1.2   Aug 22 2008 10:09:56   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 06 2008 14:56:26   mmodi
//Updated as part of workstream 
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:43:40   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.CyclePlannerService;

using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class TDGradientProfileResult : ITDGradientProfileResult, ITDSessionAware
    {
        #region Private members
        
        // Messages from the Gradient Profiler
        private ArrayList messageList = new ArrayList();

        // Gradient Profile height points
        private Dictionary<int, TDHeightPoint[]> tdHeightPoints = new Dictionary<int,TDHeightPoint[]>();

        // The interval distance between height points in the gradient profile result
        private int resolution = 10;

        private int referenceNumber = 0;

        private bool isValid = true;
        private bool isDirty = true;
        
        #endregion

        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public TDGradientProfileResult()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TDGradientProfileResult(int referenceNumber)
        {
            this.referenceNumber = referenceNumber;
        }


        #endregion

        #region Private methods

        /// <summary>
        /// Adds messages returned by the Cycle planner service to the messages list
        /// </summary>
        /// <param name="messagesFromCyclePlanner"></param>
        /// <param name="outward"></param>
        private void AddMessages(Message[] messagesFromGradientProfiler)
        {
            if (messagesFromGradientProfiler == null || messagesFromGradientProfiler.Length == 0)
            {
                return;
            }

            bool logGradientProfilerFailures = bool.Parse(Properties.Current[CyclePlannerConstants.GradientProfilerLogFailures]);

            foreach (Message msg in messagesFromGradientProfiler)
            {
                if (msg.code == CyclePlannerConstants.GPOK) // OK - no need to do anything
                {
                    continue;
                }
                else
                {
                    if (logGradientProfilerFailures)
                    {
                        LogGradientProfilerMessage(msg.description, msg.code);
                    }
                }
            }
        }

        /// <summary>
        /// Logs the message contained in the result object from the Gradient Profiler Service
        /// </summary>
        /// <param name="originalMessage"></param>
        /// <param name="majorCode"></param>
        private void LogGradientProfilerMessage(string originalMessage, int majorCode)
        {
            StringBuilder logMsg = new StringBuilder();

            logMsg.Append("GradientProfiler - GradientProfilerResult included message Code: ");
            logMsg.Append(majorCode.ToString());
            logMsg.Append(",  Message: ");
            logMsg.Append(originalMessage);
            logMsg.Append(". For request ref: ");
            logMsg.Append(referenceNumber.ToString());

            OperationalEvent operationalEvent = new OperationalEvent
                (TDEventCategory.CJP, TDTraceLevel.Error, logMsg.ToString());

            Logger.Write(operationalEvent);
        }

        #endregion

        #region Messages

        // Array is of displayable messages, so we don't 
        // want multiple messages with the same text ...
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

        // Array is of displayable messages, so we don't 
        // want multiple messages with the same text ...
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

        #region Add GradientProfile

        /// <summary>
        /// Add the GradientProfileResult to this TDGradientProfileResult.  
        /// The outward flag determines where the information will be added.
        /// </summary>
        /// <param name="gpResult">The result from the GradientProfiler</param>
        /// <param name="sessionId">The current session id (used in logging)</param>
        /// <returns>true if at least one journey was found, false otherwise</returns>
        public bool AddResult(GradientProfileResult gpResult, string sessionId)
        {
            if (gpResult == null)
            {
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Business, TDTraceLevel.Error,
                    "Unexpected null GradientProfileResult, requestId = " + referenceNumber);
                Logger.Write(oe);
                return false;
            }

            isDirty = true;

            // Check for a gradient profiler result            
            if (gpResult.gradientProfile.groups != null && gpResult.gradientProfile.groups.Length > 0)
            {
                // Holds our height points
                ArrayList tempHeightPoints = new ArrayList();

                // Go through each Group
                for (int i = 0; i < gpResult.gradientProfile.groups.Length; i++)
                {
                    // Reset the array ready for height points to be added for this group
                    tempHeightPoints.Clear();

                    // Go through each HeightPoint in the Group
                    HeightPoint[] heightPoints = gpResult.gradientProfile.groups[i].heightPoints;

                    if ((heightPoints != null) && (heightPoints.Length > 0))
                    {
                        for (int j = 0; j < heightPoints.Length; j++)
                        {
                            // Add to our temp array
                            tempHeightPoints.Add(new TDHeightPoint(heightPoints[j].pointID, heightPoints[j].height));
                        }
                    }

                    // Add our height points array to the dictionary
                    if (!tdHeightPoints.ContainsKey(gpResult.gradientProfile.groups[i].groupID))
                    {
                        TDHeightPoint[] newHeightPoints = (TDHeightPoint[])tempHeightPoints.ToArray(typeof(TDHeightPoint));
                        tdHeightPoints.Add(gpResult.gradientProfile.groups[i].groupID, newHeightPoints);
                    }
                    else
                    {
                        OperationalEvent oe = new OperationalEvent(
                            TDEventCategory.Business, TDTraceLevel.Info,
                            "Gradient profile result contained a Group with a duplicate ID, GroupID: " + gpResult.gradientProfile.groups[i].groupID
                            + ". requestId = " + referenceNumber);
                        Logger.Write(oe);
                    }
                }
            }
            else
            {
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Business, TDTraceLevel.Info,
                    "Gradient profile result contained no gradient height data, requestId = " + referenceNumber);
                Logger.Write(oe);
            }

            AddMessages(gpResult.messages);

            isValid = false;

            // Result is only valid if we have gradient profile data
            if (tdHeightPoints.Count > 0)
            {
                isValid = true;
            }

            return isValid;
        }

        #endregion

        #region ITDGradientProfileResult Members

        /// <summary>
        /// Read the messages returned by the Gradient Profiler
        /// </summary>
        public CyclePlannerMessage[] Messages
        {
            get { return (CyclePlannerMessage[])messageList.ToArray(typeof(CyclePlannerMessage)); }
        }

        public Dictionary<int, TDHeightPoint[]> TDHeightPoints
        {
            get { return tdHeightPoints; }
            set 
            {
                IsDirty = true;
                tdHeightPoints = value; 
            }
        }

        /// <summary>
        /// Read. The size (metres) of the intervals between the points for which the Gradient profiler
        /// used to get the land height.
        /// </summary>
        public int Resolution
        {
            get { return resolution; }
        }

        /// <summary>
        /// Read. Whether this Gradient profile result is valid (and therefore can be used)
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
        }

        #endregion

        #region ITDSessionAware Members

        /// <summary>
        /// Read/write property indicating whether or not the object has changed since
        /// it was last saved. 
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        #endregion
    }
}
