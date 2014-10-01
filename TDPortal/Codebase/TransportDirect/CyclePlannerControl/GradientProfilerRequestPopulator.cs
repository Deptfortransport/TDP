// *********************************************** 
// NAME			: GradientProfilerRequestPopulator.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/07/2008
// DESCRIPTION	: Class which is responsible for populating Gradient profiler requests
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfilerRequestPopulator.cs-arc  $
//
//   Rev 1.2   Aug 22 2008 10:10:02   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 06 2008 14:49:52   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:41:04   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;

using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.LocationService;
using System.Collections;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Class to populate a TDGradientProfileRequest in to a GradientProfilerRequest
    /// </summary>
    public class GradientProfilerRequestPopulator
    {
        #region Private members

        private ITDGradientProfileRequest tdGradientProfileRequest;

        private string RESULTSETTING_POINTSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.PointSeperator";
        private string RESULTSETTING_EASTINGNORTHINGSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.EastingNorthingSeperator";
        private string RESULTSETTING_RESOLUTION = "GradientProfiler.PlannerControl.Resolution.Metres";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new GradientProfilerRequestPopulator
		/// </summary>
        /// <param name="request">Related ITDGradientProfileRequest</param>
        public GradientProfilerRequestPopulator(ITDGradientProfileRequest request)
		{
            this.tdGradientProfileRequest = request;
		}

        #endregion

        #region Public methods

        /// <summary>
        /// Creates the GradientProfilerRequest objects needed to call the Gradient Profiler service for the current 
        /// ITDGradientProfileRequest, and returns them encapsulated in an array of GradientProfilerCall objects.
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="seqNo"></param>
        /// <param name="sessionId"></param>
        /// <param name="referenceTransaction"></param>
        /// <param name="userType"></param>
        /// <param name="language"></param>
        /// <returns>Array of GradientProfilerCall objects</returns>
        public GradientProfilerCall[] PopulateRequests(int referenceNumber,
                                                        int seqNo,
                                                        string sessionId,
                                                        bool referenceTransaction,
                                                        int userType,
                                                        string language)
        {
            GradientProfilerCall[] gpCalls = new GradientProfilerCall[1]; // Assume we only ever do one call per request

            GradientProfileRequest request = null;

            request = PopulateSingleGradientProfileRequest(TDGradientProfileRequest,
                                                referenceNumber, seqNo++,
                                                sessionId, referenceTransaction,
                                                userType, language);

            gpCalls[0] = new GradientProfilerCall(request, referenceNumber, sessionId);

            return gpCalls;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a single, fully populate GradientProfileRequest object
        /// </summary>
        /// <returns></returns>
        private GradientProfileRequest PopulateSingleGradientProfileRequest(
            ITDGradientProfileRequest tdGradientProfileRequest,
            int referenceNumber,
            int seqNo,
            string sessionId,
            bool referenceTransaction,
            int userType,
            string language)
        {
            GradientProfileRequest request = new GradientProfileRequest();

            #region Initialise the request

            request.requestID = SqlHelper.FormatRef(referenceNumber) + FormatSeqNo(seqNo);
            request.referenceTransaction = referenceTransaction;
            request.sessionID = sessionId;
            request.language = language;
            request.userType = userType;

            #endregion

            #region Set up the Polyline groups

            // Go through each TDPolyline, convert into a string using eastingNorthingSeperator 
            // and pointSeperator values.
            // Then assign to the request PolylineGroup

            char pointSeperator = char.Parse(Properties.Current[RESULTSETTING_POINTSEPERATOR]);
            char eastingNorthingSeparator = char.Parse(Properties.Current[RESULTSETTING_EASTINGNORTHINGSEPERATOR]);

            // Create a temp array to hold the PolylineGroups
            ArrayList polylineGroupArray = new ArrayList();

            foreach (KeyValuePair<int, TDPolyline[]> tdPolylinesKey in tdGradientProfileRequest.TDPolylines)
            {
                // Create a web PolylineGroup
                PolylineGroup polylineGroup = new PolylineGroup();

                polylineGroup.groupID = tdPolylinesKey.Key;

                // Create a temp array to hold the Polylines
                ArrayList polylineArray = new ArrayList();

                foreach (TDPolyline tdPolyline in tdPolylinesKey.Value)
                {
                    // Set the web Polyline values
                    Polyline polyline = new Polyline();
                    polyline.polylineID = tdPolyline.ID;
                    polyline.interpolateGradient = tdPolyline.InterpolateGradient;

                    StringBuilder polylineString = new StringBuilder();

                    // Convert the OSGRs into a polyline string
                    foreach (OSGridReference osgr in tdPolyline.Geometry)
                    {
                        polylineString.Append(osgr.Easting);
                        polylineString.Append(eastingNorthingSeparator);
                        polylineString.Append(osgr.Northing);
                        polylineString.Append(pointSeperator);
                    }

                    // Set the web Polyline polyline value
                    polyline.polyline = polylineString.ToString().TrimEnd(pointSeperator);

                    // Add the web Polyline to our temp array
                    polylineArray.Add(polyline);
                }

                // All polylines have been set for this group, so add to the web PolylineGroup
                polylineGroup.polylines = (Polyline[])polylineArray.ToArray(typeof(Polyline));

                // Add the web PolylineGroup to our temp array
                polylineGroupArray.Add(polylineGroup);
            }

            // All polylines have been processed, so add to the request
            request.polylineGroups = (PolylineGroup[])polylineGroupArray.ToArray(typeof(PolylineGroup));
                                    
            #endregion

            #region Set settings

            Settings settings = new Settings();

            settings.pointSeparator = pointSeperator;
            settings.eastingNorthingSeparator = eastingNorthingSeparator;
            settings.resolution = Convert.ToInt32(Properties.Current[RESULTSETTING_RESOLUTION]);

            request.settings = settings;

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

        #endregion 

        #region Public properties

        /// <summary>
        /// Read/write-only - the TDCyclePlannerRequest supllied by the caller
        /// </summary>
        protected ITDGradientProfileRequest TDGradientProfileRequest
        {
            get { return tdGradientProfileRequest; }
            set { tdGradientProfileRequest = value; }
        }

        #endregion
    }
}
