// *********************************************** 
// NAME			: CycleJourney.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which defines a CycleJourney used by the Portal created 
//              : from the Cycle Planner service Cycle Journey
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CycleJourney.cs-arc  $
//
//   Rev 1.13   Aug 24 2012 15:47:16   rbroddle
//Added new property "CalorieCount" and constructor code to populate it.
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.12   Sep 29 2010 11:26:10   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.11   Jun 03 2009 11:10:56   mmodi
//Updated to allow Latitude Longitude values to be associated with the journey
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.10   Nov 26 2008 17:03:52   mmodi
//Updated to add up duration for stopover section
//Resolution for 5185: Cycle Planner - Suspect journey duration times
//
//   Rev 1.9   Nov 07 2008 10:48:00   mturner
//Fix for IR5155 - GPX generation fails for journeys that include a Ferry.
//
//   Rev 1.8   Oct 23 2008 10:12:22   mmodi
//Updated return of OSGridReference
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.7   Oct 22 2008 15:45:34   mmodi
//Method to return all OSGRs for the journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Sep 08 2008 15:45:48   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Aug 22 2008 10:45:40   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 08 2008 12:05:46   mmodi
//Updated as part of workstream
//
//   Rev 1.3   Aug 06 2008 14:55:10   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 04 2008 10:19:42   mmodi
//Updates to work with actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:37:20   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:41:56   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using CyclePlannerWebService = TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class CycleJourney : TransportDirect.UserPortal.JourneyControl.Journey
    {
        #region Private members

        private long totalDuration;
        private int totalDistance;

        private CycleJourneyDetail[] cycleJourneyDetails;
        private CycleJourneyLegDetail[] journeyLegs = new CycleJourneyLegDetail[1]; // Inherits from JourneyLeg

        private TDLocation requestedViaLocation;

        // Gradient profile (only one per cycle journey)
        private TDGradientProfileRequest gradientProfileRequest = null;
        private TDGradientProfileResult gradientProfileResult = null;

        private bool hasFerry = false;

        // Used to track the latitude longitude geometry for the cycle journey was populated and is ok
        private bool latlongsAreValid = false;

        private int calorieCount;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public CycleJourney()
        {

        }

        /// <summary>
        /// Create a new CycleJourney from the CyclePlanner service cycle journey
        /// </summary>
        public CycleJourney(int index, CyclePlannerWebService.Journey cycleJourney, int routenum, TDLocation requestOrigin, TDLocation requestDestination, TDLocation requestedViaLocation, TDDateTime requestTime, bool arriveBefore, TDCycleJourneyResultSettings resultSettings)
        {
            this.journeyType = TDJourneyType.Cycle;
            this.routeNum = routenum;
            this.requestedViaLocation = requestedViaLocation;


            // For each "section" of the journey create a CycleJourneyDetail, and place it in 
            // the cycleJourneyDetails array

            #region Create CycleJourneyDetail items array
            
            if (cycleJourney.sections != null)
            {
                int directionCount = 0;

                cycleJourneyDetails = new CycleJourneyDetail[cycleJourney.sections.Length];

                // Loop through each section item
                for (int i = 0; i < cycleJourney.sections.Length; i++)
                {
                    CycleJourneyDetail current = new CycleJourneyDetail(cycleJourney.sections[i], resultSettings);

                    // if we hit a ferry section set flag accordingly
                    if (current.Ferry)
                    {
                        this.hasFerry = true;
                    }

                    // Add the section to the array
                    cycleJourneyDetails[directionCount] = current;

                    if (cycleJourney.sections[i].GetType() == typeof(CyclePlannerWebService.DriveSection)
                        || cycleJourney.sections[i].GetType() == typeof(CyclePlannerWebService.JunctionDriveSection)
                        || cycleJourney.sections[i].GetType() == typeof(CyclePlannerWebService.StopoverSection))
                    {
                        totalDistance += cycleJourneyDetails[directionCount].Distance;
                        totalDuration += cycleJourneyDetails[directionCount].Duration;
                    }

                    directionCount++;
                }
            }

            #endregion

            #region Create the JourneyLeg item
            // create the JourneyLeg (used by summary controls to display a high level view of the journey)
            TDDateTime startTime;
            TDDateTime endTime;

            if (arriveBefore)
            {
                // Use the start time from the cycle planner journey if present - for arrive by journeys
                // this is not necessarily same as requestTime - eg where a ferry was used...
                if (this.hasFerry && (cycleJourney.startTime != DateTime.MinValue))
                {
                    startTime = (TDDateTime)cycleJourney.startTime;
                    endTime = startTime.Add(new TimeSpan(0, 0, (int)totalDuration));
                }
                else
                {
                    endTime = requestTime;
                    startTime = endTime.Subtract(new TimeSpan(0, 0, (int)totalDuration));
                }
            }
            else
            {
                startTime = requestTime;
                endTime = startTime.Add(new TimeSpan(0, 0, (int)totalDuration));
            }

            journeyLegs[0] = new CycleJourneyLegDetail(ModeType.Cycle, requestOrigin, requestDestination, startTime, endTime);
            #endregion

            #region Calorie Calculator

            CalorieCalculator CalCalc = (CalorieCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CalorieCalculator ];
            calorieCount = (int)CalCalc.GetCycleCalorieCount(totalDuration, totalDistance);

            #endregion
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read-only property that supplies an array containing one JourneyLeg,
        /// that represents the whole cycle journey as a single leg.
        /// </summary>
        /// <remarks>
        /// This allows the JourneyLegs property of the Journey base class to be
        /// invoked polymorphically to obtain a high-level view of the journey legs
        /// </remarks>
        public override JourneyLeg[] JourneyLegs
        {
            get { return journeyLegs; }
        }

        /// <summary>
        /// Read/write property. Array of cycle instructions
        /// Write provided to allow the TDCyclePlannerResult object to populate the latitude longitude values
        /// </summary>
        public CycleJourneyDetail[] Details
        {
            get { return cycleJourneyDetails; }
            set { cycleJourneyDetails = value; }
        }

        /// <summary>
        /// Read-only property. Array of modes used in this journey.
        /// </summary>
        public override ModeType[] GetUsedModes()
        {
            return (new ModeType[] { ModeType.Cycle });
        }


        /// <summary>
        /// Read-only property. Total duration in Minutes.
        /// </summary>
        public long TotalDuration
        {
            get { return totalDuration; }
        }

        /// <summary>
        /// Read-only. Returns the total distance of the cycle journey
        /// </summary>
        public int TotalDistance
        {
            get { return totalDistance; }
        }

        /// <summary>
        /// Read/write. The request object for a Gradient profile
        /// </summary>
        public TDGradientProfileRequest GradientProfileRequest
        {
            get { return gradientProfileRequest; }
            set { gradientProfileRequest = value; }
        }

        /// <summary>
        /// Read/write. The result object for a Gradient profile
        /// </summary>
        public TDGradientProfileResult GradientProfileResult
        {
            get { return gradientProfileResult; }
            set { gradientProfileResult = value; }
        }

        /// <summary>
        /// Read-only Property. Departure time of the journey
        /// </summary>
        public TDDateTime DepartDateTime
        {
            get { return journeyLegs[0].StartTime; }
        }

        /// <summary>
        /// Read-only Property. Arrival time of the journey
        /// </summary>
        public TDDateTime ArriveDateTime
        {
            get { return journeyLegs[0].EndTime; }
        }

        /// <summary>
        /// Read/Write Property. Origin location of the journey
        /// Write provided to allow the TDCyclePlannerResult object to populate the latitude longitude value
        /// </summary>
        public TDLocation OriginLocation
        {
            get { return journeyLegs[0].LegStart.Location; }
            set { journeyLegs[0].LegStart.Location = value; }
        }

        /// <summary>
        /// Read/Write Property. Destination location of the journey
        /// Write provided to allow the TDCyclePlannerResult object to populate the latitude longitude value
        /// </summary>
        public TDLocation DestinationLocation
        {
            get { return journeyLegs[0].LegEnd.Location; }
            set { journeyLegs[0].LegEnd.Location = value; }
        }

        /// <summary>
        /// Read/Write Property. Via Location of the journey
        /// Write provided to allow the TDCyclePlannerResult object to populate the latitude longitude value
        /// </summary>
        public TDLocation RequestedViaLocation
        {
            get { return requestedViaLocation; }
            set { requestedViaLocation = value; }
        }

        /// <summary>
        /// Read/Write Property. Indicates whether the LatitudeLongitude coordinates used in this
        /// journey are valid and OK to use.
        /// </summary>
        public bool LatitudeLongitudesAreValid
        {
            get { return latlongsAreValid; }
            set { latlongsAreValid = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Calculates the start time of the journey
        /// </summary>
        /// <param name="time">The time to "leave at" or "arrive by"</param>
        /// <param name="arriveBefore">True if supplied time is the time journey ends
        /// or false if it is the time the journey starts</param>
        /// <returns>Start time of cycle journey</returns>
        public TDDateTime CalculateStartTime(TDDateTime time, bool arriveBefore)
        {
            if (arriveBefore)
            {
                if (hasFerry)
                {
                    return journeyLegs[0].StartTime;
                }
                else
                {
                    TimeSpan duration = new TimeSpan(0, 0, (int)totalDuration);
                    return time.Subtract(duration);
                }
            }
            else
            {
                return time;
            }
        }

        /// <summary>
        /// Calculates the end time of the journey
        /// </summary>
        /// <param name="time">The time to start on or arrive by</param>
        /// <param name="arriveBefore">True if supplied time is the time journey ends
        /// or false if it is the time the journey starts</param>
        /// <returns>End time of cycle journey</returns>
        public TDDateTime CalculateEndTime(TDDateTime time, bool arriveBefore)
        {
            if (arriveBefore)
            {
                if (hasFerry)
                {
                    return journeyLegs[0].EndTime;
                }
                else
                {
                    return time;
                }
            }
            else
            {
                TimeSpan duration = new TimeSpan(0, 0, (int)totalDuration);
                return time.Add(duration);
            }
        }

        /// <summary>
        /// Returns all OSGridReferences for the journey (from all the CycleJourneyDetails) as an 
        /// array of OSGridReferences
        /// </summary>
        /// <returns></returns>
        public OSGridReference[] GetOSGridReferences()
        {
            // Temp array used to group together polylines
            ArrayList gridReferences = new ArrayList();

            foreach (CycleJourneyDetail detail in Details)
            {
                // Get the geometry values for this detail
                Dictionary<int, OSGridReference[]> geometrys = detail.Geometry;

                if (geometrys !=  null && geometrys.Count > 0)
                {
                    // Each geometry gets its own Polyline, to enable more accurate gradient profiles 
                    foreach (KeyValuePair<int, OSGridReference[]> geometry in geometrys)
                    {
                        // Add the gridreference to the array
                        foreach (OSGridReference osgr in geometry.Value)
                        {
                            gridReferences.Add( osgr );
                        }
                    }
                }
            }

            return (OSGridReference[])gridReferences.ToArray(typeof(OSGridReference));
        }

        /// <summary>
        /// Returns all LatitudeLongitudes for the journey as an array of LatitudeLongitudes.
        /// </summary>
        /// <returns></returns>
        public LatitudeLongitude[] GetLatitudeLongitudes()
        {
            // Temp array used to group together polylines
            ArrayList gridReferences = new ArrayList();

            foreach (CycleJourneyDetail detail in Details)
            {
                // Get the geometry values for this detail
                LatitudeLongitude[] latlongs = detail.LatitudeLongitudes;

                if (latlongs != null && latlongs.Length > 0)
                {
                    gridReferences.AddRange(latlongs);
                }
            }

            return (LatitudeLongitude[])gridReferences.ToArray(typeof(LatitudeLongitude));
        }

        /// <summary>
        /// Read-only property. Estimated calories burnt in journey. Returns -1 if includes ferry as number 
        /// would be invalid.
        /// </summary>
        public long CalorieCount
        {
            get
            {
                if (this.hasFerry) { return -1; } else { return calorieCount; }
            }
        }

        #endregion
    }
}
