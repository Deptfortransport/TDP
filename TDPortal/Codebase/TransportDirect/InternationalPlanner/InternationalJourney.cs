// *********************************************** 
// NAME			: InternationalJourney.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which defines an International journey
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalJourney.cs-arc  $
//
//   Rev 1.6   Sep 14 2010 18:01:56   rbroddle
//Fixed problem with filtering on effective dates
//Resolution for 5551: TDExtra journeys cache sometimes returning invalid results
//
//   Rev 1.4   Jun 11 2010 16:44:24   RBroddle
//Added Effective From and Effective to date properties for filtering
//Resolution for 5551: TDExtra journeys cache sometimes returning invalid results
//
//   Rev 1.3   Mar 05 2010 11:48:00   mmodi
//Updated to allow a deep clone of the journey to be returned from the data cache
//
//   Rev 1.2   Feb 09 2010 09:53:16   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:02   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:32   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which defines an International journey
    /// </summary>
    [Serializable()]
    public class InternationalJourney
    {
        #region Private members

        private string departureName;
        private string arrivalName;
        private string departureCityID;
        private string arrivalCityID; 
        private string departureStopNaptan;
        private string arrivalStopNaptan; 
        private DateTime departureDateTime; 
        private DateTime arrivalDateTime;
        private DateTime effectiveFromDate;
        private DateTime effectiveToDate; 
        private DayOfWeek[] daysOfOperation;
        private double durationMinutes;
        private InternationalModeType modeType = InternationalModeType.None;

        private InternationalJourneyDetail[] journeyDetails;

        private double emissions;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InternationalJourney()
        {
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The Departure Name for the journey
        /// </summary>
        public string DepartureName
        {
            get { return departureName; }
            set { departureName = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival Name for the journey
        /// </summary>
        public string ArrivalName
        {
            get { return arrivalName; }
            set { arrivalName = value; }
        }

        /// <summary>
        /// Read/Write. The Departure city ID for the journey
        /// </summary>
        public string DepartureCityID
        {
            get { return departureCityID; }
            set { departureCityID = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival city ID for the journey
        /// </summary>
        public string ArrivalCityID
        {
            get { return arrivalCityID; }
            set { arrivalCityID = value; }
        }

        /// <summary>
        /// Read/Write. The Departure stop naptan for the journey
        /// </summary>
        public string DepartureStopNaptan
        {
            get { return departureStopNaptan; }
            set { departureStopNaptan = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival stop naptan for the journey
        /// </summary>
        public string ArrivalStopNaptan
        {
            get { return arrivalStopNaptan; }
            set { arrivalStopNaptan = value; }
        }

        /// <summary>
        /// Read/Write. The Departure date time for the journey, in local time
        /// </summary>
        public DateTime DepartureDateTime
        {
            get { return departureDateTime; }
            set { departureDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival date time for the journey, in local time
        /// </summary>
        public DateTime ArrivalDateTime
        {
            get { return arrivalDateTime; }
            set { arrivalDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The Effective from date time for the journey, in local time
        /// </summary>
        public DateTime EffectiveFromDate
        {
            get { return effectiveFromDate; }
            set { effectiveFromDate = value; }
        }

        /// <summary>
        /// Read/Write. The Effective to date time for the journey, in local time
        /// </summary>
        public DateTime EffectiveToDate
        {
            get { return effectiveToDate; }
            set { effectiveToDate = value; }
        }

        /// <summary>
        /// Read/Write. The days of the week the journey operates on
        /// </summary>
        public DayOfWeek[] DaysOfOperation
        {
            get { return daysOfOperation; }
            set { daysOfOperation = value; }
        }

        /// <summary>
        /// Read/Write. The total duration in minutes for the journey, 
        /// including all check-in/outs and city transfers
        /// </summary>
        public double DurationMinutes
        {
            get { return durationMinutes; }
            set { durationMinutes = value; }
        }

        /// <summary>
        /// Read/Write. The overall mode of the journey
        /// </summary>
        public InternationalModeType ModeType
        {
            get { return modeType; }
            set { modeType= value; }
        }

        /// <summary>
        /// Read/Write. The journey detail legs for the journey
        /// </summary>
        public InternationalJourneyDetail[] JourneyDetails
        {
            get { return journeyDetails; }
            set { journeyDetails = value; }
        }

        /// <summary>
        /// Read/Write. The Emissions (in Grammes) for the journey. This is only populated for 
        /// a Car international journey
        /// </summary>
        public double Emissions
        {
            get { return emissions; }
            set { emissions = value; }
        }

        #endregion

        #region Clone method

        /// <summary>
        /// Method to create a deep clone of this International Journey
        /// </summary>
        /// <returns></returns>
        public InternationalJourney DeepClone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, this);
            ms.Position = 0;

            object obj = bf.Deserialize(ms);

            ms.Close();

            return (InternationalJourney)obj;
        }

        #endregion
    }
}
