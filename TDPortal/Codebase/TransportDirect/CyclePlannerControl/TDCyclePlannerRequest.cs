// *********************************************** 
// NAME			: TDCyclePlannerRequest.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which contains all the details needed to be used in the creation of a Cycle planner call
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/TDCyclePlannerRequest.cs-arc  $
//
//   Rev 1.4   Sep 29 2010 11:26:16   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.3   Sep 02 2008 10:37:00   mmodi
//Updated user preference object to enable serialization
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:10:04   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 04 2008 10:19:50   mmodi
//Updates to work with actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:42:02   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using System.Xml.Serialization;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class TDCyclePlannerRequest : ITDCyclePlannerRequest, ITDSessionAware, ICloneable
    {
        #region Private members

        // Dates and times
        private bool isReturnRequired = false;
        private bool outwardArriveBefore = false;
        private bool returnArriveBefore = false;
        private bool outwardAnyTime = false;
        private bool returnAnyTime = false;

        private TDDateTime[] outwardDateTime;
        private TDDateTime[] returnDateTime;

        // Locations
        private TDLocation originLocation;
        private TDLocation destinationLocation;
        private TDLocation[] cycleViaLocations;

        // Advanced options
        private string cycleJourneyType = string.Empty;
        private string penaltyFunction = string.Empty;

        private TDCycleUserPreference[] userPreferences = new TDCycleUserPreference[0];
        private TDCycleJourneyResultSettings resultSettings = null;

        // Session
        private bool isDirty = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TDCyclePlannerRequest()
        {
        }

        #endregion

        #region ITDCyclePlannerRequest Members

        #region Date and time
        /// <summary>
        /// Read/write. Is single or return journey required. True for a return journey.
        /// Default is false
        /// </summary>
        public bool IsReturnRequired
        {
            get { return isReturnRequired; }
            set { isDirty = true; isReturnRequired = value; }
        }

        /// <summary>
        /// Read/write. Is the outward date/time an arrive before.
        /// Default is false
        /// </summary>
        public bool OutwardArriveBefore
        {
            get { return outwardArriveBefore; }
            set { isDirty = true; outwardArriveBefore = value; }
        }

        /// <summary>
        /// Read/write. Is the return date/time an arrive before.
        /// Default is false
        /// </summary>
        public bool ReturnArriveBefore
        {
            get { return returnArriveBefore; }
            set { isDirty = true; returnArriveBefore = value; }
        }

        /// <summary>
        /// Read/write. Is the outward journey an Anytime journey.
        /// Default is false
        /// </summary>
        public bool OutwardAnyTime
        {
            get { return outwardAnyTime; }
            set { isDirty = true; outwardAnyTime = value; }
        }

        /// <summary>
        /// Read/write. Is the return journey an Anytime journey
        /// </summary>
        public bool ReturnAnyTime
        {
            get { return returnAnyTime; }
            set { isDirty = true; returnAnyTime = value; }
        }

        /// <summary>
        /// Read/write. The outward date/time
        /// </summary>
        public TransportDirect.Common.TDDateTime[] OutwardDateTime
        {
            get { return outwardDateTime; }
            set { isDirty = true; outwardDateTime = value; }
        }

        /// <summary>
        /// Read/write. The return date/time
        /// </summary>
        public TransportDirect.Common.TDDateTime[] ReturnDateTime
        {
            get { return returnDateTime; }
            set { isDirty = true; returnDateTime = value; }
        }

        #endregion

        #region Locations

        /// <summary>
        /// Read/write. The origin location
        /// </summary>
        public TransportDirect.UserPortal.LocationService.TDLocation OriginLocation
        {
            get { return originLocation; }
            set { isDirty = true; originLocation = value; }
        }

        /// <summary>
        /// Read/write. The destination location
        /// </summary>
        public TransportDirect.UserPortal.LocationService.TDLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { isDirty = true; destinationLocation = value; }
        }

        /// <summary>
        /// Read/write. The via location(s)
        /// </summary>
        public TransportDirect.UserPortal.LocationService.TDLocation[] CycleViaLocations
        {
            get { return cycleViaLocations; }
            set { isDirty = true; cycleViaLocations = value; }
        }

        #endregion

        #region Advanced options

        /// <summary>
        /// Read/write. The CycleJourneyType to use. This value will be used to 
        /// select the penalty function used by the cycle planner. e.g. "Fastest".
        /// Default should be set to "Fastest".
        /// The property PenaltyFunctionUsed is the value sent to the service, and 
        /// is set by the TDJourneyParametersCycleConverter
        /// </summary>
        public string CycleJourneyType
        {
            get { return cycleJourneyType; }
            set { isDirty = true; cycleJourneyType = value; }
        }
        
        /// <summary>
        /// Read/write. The actual penalty function used by the cycle planner.
        /// DO NOT POPULATE MANUALLY, THE TDJourneyParametersCycleConverter WILL SET THIS VALUE
        /// </summary>
        public string PenaltyFunction
        {
            get { return penaltyFunction; }
            set { isDirty = true; penaltyFunction = value; }
        }

        /// <summary>
        /// Read/write. Journey parameters in a key/value pairs array. 
        /// </summary>
        public TDCycleUserPreference[] UserPreferences
        {
            get { return userPreferences; }
            set { isDirty = true; userPreferences = value; }
        }

        /// <summary>
        /// Read/write. Journey result settings.
        /// </summary>
        public TDCycleJourneyResultSettings ResultSettings
        {
            get { return resultSettings; }
            set { resultSettings = value; }
        }

        #endregion

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

        #region ICloneable Members

        /// <summary>
        /// Performs a memberwise clone of this object. 
        /// </summary>
        /// <returns>A copy of this object</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    [Serializable()]
    public class TDCycleUserPreference
    {
        private string preferenceKey;
        private string preferenceValue;

        public TDCycleUserPreference()
        {
        }

        public TDCycleUserPreference(string preferenceKey, string preferenceValue)
        {
            this.preferenceKey = preferenceKey;
            this.preferenceValue = preferenceValue;
        }

        public string PreferenceKey
        {
            get { return preferenceKey; }
            set { preferenceKey = value; }
        }

        public string PreferenceValue
        {
            get { return preferenceValue; }
            set { preferenceValue = value; }
        }
    }
}
