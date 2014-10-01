// *********************************************** 
// NAME			: CyclePlannerRequestPopulator.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which is responsible for populating Cycle planner requests for the Cycle planner service
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerRequestPopulator.cs-arc  $
//
//   Rev 1.7   Sep 29 2010 11:26:14   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.6   Feb 09 2010 09:45:12   apatel
//Updated for TD International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Oct 10 2008 15:40:16   mmodi
//Removed hard coded preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Sep 16 2008 16:44:06   mmodi
//Updated to use Point value and not pass Toids in request
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Sep 02 2008 10:37:04   mmodi
//Updated user preference object to enable serialization
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:10:02   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 04 2008 10:19:44   mmodi
//Updates to work with actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:42:00   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Class to populate a TDCyclePlannerRequest in to a CyclePlannerRequest
    /// </summary>
    public class CyclePlannerRequestPopulator
    {
        #region Private members

        private string TOID_PREFIX = "JourneyControl.ToidPrefix";

        private string JOURNEYREQUEST_INCLUDETOIDS = "CyclePlanner.PlannerControl.JourneyRequest.IncludeToids";

        private string JOURNEYRESULTSETTING_INCLUDETOIDS = "CyclePlanner.PlannerControl.JourneyResultSetting.IncludeToids";
        private string JOURNEYRESULTSETTING_INCLUDEGEOMETRY = "CyclePlanner.PlannerControl.JourneyResultSetting.IncludeGeometry";
        private string JOURNEYRESULTSETTING_INCLUDETEXT = "CyclePlanner.PlannerControl.JourneyResultSetting.IncludeText";
        private string JOURNEYRESULTSETTING_POINTSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.PointSeperator";
        private string JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.EastingNorthingSeperator";

        private ITDCyclePlannerRequest tdCyclePlannerRequest;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new CyclePlannerRequestPopulator
		/// </summary>
        /// <param name="request">Related ITDCyclePlannerRequest</param>
        public CyclePlannerRequestPopulator(ITDCyclePlannerRequest request)
		{
            this.tdCyclePlannerRequest = request;
		}

        #endregion

        #region Public methods

        /// <summary>
        /// Creates the CyclePlannerRequest objects needed to call the Cycle planner service for the current 
        /// ITDCyclePlannerRequess, and returns them encapsulated in an array of CyclePlannerCall objects.
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="seqNo"></param>
        /// <param name="sessionId"></param>
        /// <param name="referenceTransaction"></param>
        /// <param name="userType"></param>
        /// <param name="language"></param>
        /// <returns>Array of CJPCall objects</returns>
        public CyclePlannerCall[] PopulateRequests(int referenceNumber,
                                                        int seqNo,
                                                        string sessionId,
                                                        bool referenceTransaction,
                                                        int userType,
                                                        string language)
        {
            CyclePlannerCall[] cpCalls = new CyclePlannerCall[tdCyclePlannerRequest.IsReturnRequired ? 2 : 1];

            JourneyRequest request = null;

            request = PopulateSingleCycleRequest(TDCyclePlannerRequest, false,
                                                referenceNumber, seqNo++,
                                                sessionId, referenceTransaction,
                                                userType, language);

            cpCalls[0] = new CyclePlannerCall(request, false, referenceNumber, sessionId);

            if (tdCyclePlannerRequest.IsReturnRequired)
            {
                request = PopulateSingleCycleRequest(TDCyclePlannerRequest, true,
                                                referenceNumber, seqNo++,
                                                sessionId, referenceTransaction,
                                                userType, language);

                cpCalls[1] = new CyclePlannerCall(request, true, referenceNumber, sessionId);
            }

            return cpCalls;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a single, fully populate CyclePlannerRequest object
        /// </summary>
        /// <returns></returns>
        private JourneyRequest PopulateSingleCycleRequest(
            ITDCyclePlannerRequest tdCyclePlannerRequest,
            bool returnJourney,
            int referenceNumber,
            int seqNo,
            string sessionId,
            bool referenceTransaction,
            int userType,
            string language)
        {
            JourneyRequest request = new JourneyRequest();

            #region Initialise the request

            request.requestID = SqlHelper.FormatRef(referenceNumber) + FormatSeqNo(seqNo);
            request.referenceTransaction = referenceTransaction;
            request.sessionID = sessionId;
            request.language = language;
            request.userType = userType;

            #endregion

            #region Set locations and date/times, plus any TOIDs
            TDDateTime arriveTime = null;
            TDDateTime departTime = null;

            if (returnJourney)
            {
                request.depart = !tdCyclePlannerRequest.ReturnArriveBefore;

                if (tdCyclePlannerRequest.ReturnArriveBefore)
                {
                    arriveTime = tdCyclePlannerRequest.ReturnDateTime[0];
                }
                else
                {
                    departTime = tdCyclePlannerRequest.ReturnDateTime[0];
                }

                request.origin = PopulateRequestPlace(tdCyclePlannerRequest.DestinationLocation, departTime);
                request.destination = PopulateRequestPlace(tdCyclePlannerRequest.OriginLocation, arriveTime);
            }
            else
            {
                request.depart = !tdCyclePlannerRequest.OutwardArriveBefore;

                if (tdCyclePlannerRequest.OutwardArriveBefore)
                {
                    arriveTime = tdCyclePlannerRequest.OutwardDateTime[0];
                }
                else
                {
                    departTime = tdCyclePlannerRequest.OutwardDateTime[0];
                }

                request.origin = PopulateRequestPlace(tdCyclePlannerRequest.OriginLocation, departTime);
                request.destination = PopulateRequestPlace(tdCyclePlannerRequest.DestinationLocation, arriveTime);
            }

            #endregion

            #region Set via locations and date/time

            if (tdCyclePlannerRequest.CycleViaLocations != null
                && tdCyclePlannerRequest.CycleViaLocations.Length > 0
                && tdCyclePlannerRequest.CycleViaLocations[0].Status == TDLocationStatus.Valid)
            {
                request.vias = new RequestPlace[1];
                request.vias[0] = PopulateRequestPlace(tdCyclePlannerRequest.CycleViaLocations[0], null);
            }
            else
            {
                request.vias = new RequestPlace[0];
            }

            #endregion

            #region Set user preferences

            ArrayList userPreferences = new ArrayList();

            foreach (TDCycleUserPreference preference in tdCyclePlannerRequest.UserPreferences)
            {
                UserPreference userPreference = new UserPreference();
                userPreference.parameterID = Convert.ToInt32(preference.PreferenceKey);
                userPreference.parameterValue = preference.PreferenceValue;

                userPreferences.Add(userPreference);
            }

            request.userPreferences = (UserPreference[])userPreferences.ToArray(typeof(UserPreference));

            #endregion

            #region Set penalty function

            request.penaltyFunction = tdCyclePlannerRequest.PenaltyFunction;

            #endregion

            #region Set journey result settings

            JourneyResultSettings resultSettings = new JourneyResultSettings();

            // Get result setting values from properties
            resultSettings.includeToids = bool.Parse(Properties.Current[JOURNEYRESULTSETTING_INCLUDETOIDS]);
            resultSettings.includeGeometry = bool.Parse(Properties.Current[JOURNEYRESULTSETTING_INCLUDEGEOMETRY]);
            resultSettings.includeText = bool.Parse(Properties.Current[JOURNEYRESULTSETTING_INCLUDETEXT]);
            resultSettings.pointSeparator = char.Parse(Properties.Current[JOURNEYRESULTSETTING_POINTSEPERATOR]);
            resultSettings.eastingNorthingSeparator = char.Parse(Properties.Current[JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR]);

            // If result settings are provided override the result settings for the cycle service
            if (tdCyclePlannerRequest.ResultSettings != null)
            {
                TDCycleJourneyResultSettings tdResultSettings = tdCyclePlannerRequest.ResultSettings;
                resultSettings.includeGeometry = tdResultSettings.IncludeGeometry;
                resultSettings.eastingNorthingSeparator = tdResultSettings.EastingNorthingSeparator;
                resultSettings.pointSeparator = tdResultSettings.PointSeparator;
                

            }

            request.journeyResultSettings = resultSettings;

            #endregion

            return request;
        }

        /// <summary>
        /// Formats a sequence number for use as a request-id
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns>Formatted string</returns>
        private string FormatSeqNo(int seqNo)
        {
            return seqNo.ToString("-0000");
        }

        /// <summary>
        /// Takes a TDLocation, and converts it into a CyclePlannerWebService RequestPlace
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private RequestPlace PopulateRequestPlace(TDLocation location, TDDateTime dateTime)
        {
            RequestPlace requestPlace = new RequestPlace();

            #region name

            requestPlace.givenName = location.Description;

            #endregion

            #region timedate

            if (dateTime != null)
            {
                requestPlace.timeDate = dateTime.GetDateTime();
            }

            #endregion

            #region coordinate
            requestPlace.coordinate = new Coordinate();
            
            // If there is a valid point, use that otherwise use the OSGR
            if ((location.Point != null) && (location.Point.X > 0) && (location.Point.Y > 0))
            {
                requestPlace.coordinate.easting = Convert.ToInt32(location.Point.X);
                requestPlace.coordinate.northing = Convert.ToInt32(location.Point.Y);
            }
            else
            {
                requestPlace.coordinate.easting = location.GridReference.Easting;
                requestPlace.coordinate.northing = location.GridReference.Northing;
            }

            #endregion

            #region road points

            ITN[] requestRoadPoints = null;

            // Check if we should include Toid's in the request sent 
            if (bool.Parse(Properties.Current[JOURNEYREQUEST_INCLUDETOIDS]))
            {
                #region Include toids in the request

                // check for toids
                if ((location.Toid == null) || (location.Toid.Length <= 0))
                {
                    location.PopulateToids();
                }

                // toids from the location object
                string[] toid = location.Toid;

                // ESRI's queries often return large numbers of duplicate 
                // TOIDs for an OSGR - remove duplicates
                SortedList sortedToids = new SortedList();

                for (int i = 0; i < toid.Length; i++)
                {
                    try
                    {
                        sortedToids.Add(toid[i], toid[i]);
                    }
                    catch (ArgumentException)
                    {
                        // nothing to do - just means it's a duplicate
                    }
                }

                IList editedToidList = sortedToids.GetValueList();

                string toidPrefix = Properties.Current[TOID_PREFIX];

                if (toidPrefix == null)
                {
                    toidPrefix = string.Empty;
                }

                requestRoadPoints = new ITN[editedToidList.Count];

                for (int i = 0; i < editedToidList.Count; i++)
                {
                    string currentToid = (string)editedToidList[i];

                    requestRoadPoints[i] = new ITN();

                    if ((toidPrefix.Length > 0) && !(currentToid.StartsWith(toidPrefix)))
                    {
                        requestRoadPoints[i].TOID = toidPrefix + currentToid;
                    }
                    else
                    {
                        requestRoadPoints[i].TOID = currentToid;
                    }

                    requestRoadPoints[i].node = false;		// ESRI supplies us with links, not nodes

                    if (dateTime != null)
                    {
                        requestRoadPoints[i].timeDate = dateTime.GetDateTime();
                    }
                }

                #endregion
            }
            else
            {
                // Don't pass any Toids, cycle planner will use the Point coordinate passed to plan the journey from/to
                requestRoadPoints = new ITN[0];
            }
            
            requestPlace.roadPoints = requestRoadPoints;

            #endregion

            return requestPlace;
        }

        #endregion 

        #region Public properties

        /// <summary>
        /// Read/write-only - the TDCyclePlannerRequest supllied by the caller
        /// </summary>
        protected ITDCyclePlannerRequest TDCyclePlannerRequest
        {
            get { return tdCyclePlannerRequest; }
            set { tdCyclePlannerRequest = value; }
        }

        #endregion
    }
}
