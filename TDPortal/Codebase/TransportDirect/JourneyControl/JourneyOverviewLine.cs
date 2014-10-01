// *********************************************** 
// NAME			: JourneyOverviewLine.cs
// AUTHOR		: Steve Tsang and Dan Gath
// DATE CREATED	: 20/01/2008
// DESCRIPTION	: Implementation of the JourneyOverviewLine class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyOverviewLine.cs-arc  $
//
//   Rev 1.2   Feb 23 2010 13:31:06   mmodi
//Updated to set an overall ModeType for a JourneyOverviewLine
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Mar 10 2008 15:17:48   mturner
//Initial Del10 Codebase from Dev Factory

//Rev Dev Factory Jan 20 2008 19:00:00
//CCN 0382b City to City enhancements. Class created.
//Initial revision

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Class encapsulating all the data needed
    /// for a single line of output for each mode 
    /// in the UI's journey summary.
    /// </summary>
    public class JourneyOverviewLine
    {
        private TDJourneyType type = 0;
        private ModeType mode = ModeType.Transfer;
        private bool isForITP = false;
        private ModeType[] modes = null;
        private string numberOfJourneys = string.Empty;
        private TimeSpan duration = new TimeSpan(0);
        private float emissions = 0;
        private TDDateTime earliestDeparture = null;
        private TDDateTime latestDeparture = null;
        private Journey journey;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="modes"></param>
        /// <param name="numberOfJourneys"></param>
        /// <param name="duration"></param>
        /// <param name="emissions"></param>
        /// <param name="earliestDeparture"></param>
        /// <param name="latestDeparture"></param>
        /// <param name="journey"></param>
        public JourneyOverviewLine(TDJourneyType type, ModeType mode, bool isForITP, ModeType[] modes, string numberOfJourneys, TimeSpan duration, float emissions, TDDateTime earliestDeparture, TDDateTime latestDeparture, Journey journey)
        {
            this.type = type;
            this.mode = mode;
            this.isForITP = isForITP;
            this.modes = modes;
            this.numberOfJourneys = numberOfJourneys;
            this.duration = duration;
            this.emissions = emissions;
            this.earliestDeparture = earliestDeparture;
            this.latestDeparture = latestDeparture;
            this.journey = journey;

            // Set to Walk as there isn't any other convienient mode
            if (isForITP)
            {
                this.mode = ModeType.Walk;
            }
        }

        /// <summary>
        /// Read only property. Get the type of journey (e.g. car, public)
        /// </summary>
        public TDJourneyType Type 
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Read only property. Get the overall overview line mode (e.g. train, underground).
        /// For ITP journey mode this is set to Walk, and the IsForITP flag will be true
        /// </summary>
        public ModeType Mode
        {
            get
            {
                return mode;
            }
        }

        /// <summary>
        /// Read only property. Gets if this overview line is for an ITP journey mode
        /// </summary>
        public bool IsForITP
        {
            get
            {
                return isForITP;
            }
        }

        /// <summary>
        /// Read only property. Get the modes of the journey (e.g. train, underground)
        /// </summary>
        public ModeType[] Modes 
        {
            get
            {
                return modes;
            }
        }

        /// <summary>
        /// Read only property. Get the number of journeys for this particular mode of transport
        /// </summary>
        public string NumberOfJourneys 
        {
            get
            {
                return numberOfJourneys;
            }
        }

        /// <summary>
        /// Read only property. Get the fastest duration for this mode of transport
        /// </summary>
        public TimeSpan Duration 
        {
            get
            {
                return duration;
            }
        }

        /// <summary>
        /// Read/Write property. Get the best carbon emissions value for this mode of transport
        /// </summary>
        public float Emissions 
        {
            get
            {
                return emissions;
            }
            set
            {
                emissions = value;
            }
        }

        /// <summary>
        /// Read only property. Get the earliest departure for this mode of transport
        /// </summary>
        public TDDateTime EarliestDeparture 
        {
            get
            {
                return earliestDeparture;
            }
        }

        /// <summary>
        /// Read only property. Get the latest departure for this mode of transport
        /// </summary>
        public TDDateTime LatestDeparture
        {
            get
            {
                return latestDeparture;
            }
        }

        /// <summary>
        /// Read only property. Get the Fastest journey
        /// </summary>
        public Journey FastestJourney
        {
            get
            {
                return journey;
            }
        }

    }
}
