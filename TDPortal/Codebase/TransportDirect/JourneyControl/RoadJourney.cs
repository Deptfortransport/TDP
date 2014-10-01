// *********************************************** 
// NAME			: RoadJourney.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the RoadJourney class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/RoadJourney.cs-arc  $
//
//   Rev 1.5   Sep 21 2011 09:53:18   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.4   Sep 06 2011 11:20:30   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Sep 01 2011 10:43:20   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.2   Mar 14 2011 15:11:54   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.1   Feb 19 2010 12:07:58   mmodi
//Added constructors and properties to allow International Planner to create a road jouney
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:23:56   mturner
//Initial revision.
//
//   Rev 1.29   Aug 01 2007 17:35:00   rbroddle
//Changes to constructor, CalculateStartTime & CalculateEndTime to allow for ferrys in arrive by journeys.
//Resolution for 4470: Journey times incorrect for "Arrive By" car journeys with ferries.
//
//   Rev 1.28   Jan 12 2007 13:18:36   jfrank
//Changed for IR4277 - Adding and end instruction to a road journey ending in the congegestion zone at a time when a charge applies, if it entered at a time when a charge didn't apply.
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.27   Jan 04 2007 14:02:32   mmodi
//Added a Set to the TotalFuelCost
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.26   Nov 29 2006 16:44:44   mmodi
//Removed commented out code for Congestion charge change
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.25   Nov 24 2006 15:28:28   PScott
//CCN0342a Draft Changes
//
//   Rev 1.24   Mar 22 2006 16:28:10   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.23   Mar 14 2006 08:41:36   build
//Automatically merged from branch for stream3353
//
//   Rev 1.22.1.3   Mar 10 2006 19:02:22   rhopkins
//Instantiations of JourneyLeg are now PrivateJourneyDetail.
//Also general code review tidy.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.1.2   Mar 02 2006 17:40:42   NMoorhouse
//Extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.1.1   Feb 24 2006 12:57:06   NMoorhouse
//Updated for new CarDetails page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.1.0   Jan 26 2006 20:12:18   rhopkins
//Include instance of JourneyLeg to represent whole journey.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22   May 10 2005 15:56:02   asinclair
//Fix for IR2430
//
//   Rev 1.21   May 07 2005 15:10:20   asinclair
//Fix for IR 2430
//
//   Rev 1.20   Apr 21 2005 09:44:52   rscott
//Fixes for IR2259 Ferry Problem
//
//   Rev 1.19   Apr 16 2005 18:28:42   asinclair
//Fix for IR 1989
//
//   Rev 1.18   Feb 11 2005 15:58:04   rgreenwood
//Added calculation of totalFuelCosts
//
//   Rev 1.17   Feb 01 2005 13:49:48   asinclair
//Fixed to return Total Distance and Total Duration
//
//   Rev 1.16   Feb 01 2005 09:55:56   rgreenwood
//Added TotalFuelCost calculation, and work in progress for running cost calculation
//
//   Rev 1.15   Jan 20 2005 10:20:36   asinclair
//Work in progress - Del 7 Car Costing
//
//   Rev 1.14   Nov 26 2004 13:50:18   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.13   Sep 17 2004 15:13:02   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.12   Jun 15 2004 14:31:04   COwczarek
//Addl new methods to calculate start and end times of a road journey
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.11   Jun 08 2004 14:39:42   RPhilpott
//Add method to return list of modes used by the journey.
//
//   Rev 1.10   Nov 15 2003 18:06:04   RPhilpott
//Leave stopover sections out of RoadJourneyDetails.
//Resolution for 189: Continuous Wait Screen
//
//   Rev 1.9   Oct 15 2003 13:30:08   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.8   Oct 09 2003 19:57:44   RPhilpott
//Bodge to allow road journey display even when no congested journey returned.
//
//   Rev 1.7   Sep 11 2003 16:34:12   jcotton
//Made Class Serializable
//
//   Rev 1.6   Sep 02 2003 18:09:48   kcheung
//Added TODO bug fix required - duration of last leg is always 0
//
//   Rev 1.5   Sep 01 2003 16:28:42   jcotton
//Updated: RouteNum
//
//   Rev 1.4   Aug 27 2003 09:26:50   kcheung
//Updated constructor code to set the journeyType
//
//   Rev 1.3   Aug 26 2003 17:13:34   kcheung
//Updated constructor - takes the index now
//
//   Rev 1.2   Aug 20 2003 17:55:50   AToner
//Work in progress

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Data.SqlClient;
using System.Collections.Generic;
using TransportDirect.Common.PropertyService.Properties;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for RoadJourney.
	/// </summary>
	[Serializable()]
	public class RoadJourney : Journey
    {
        #region Private members

        private long totalDuration;
		private int totalDistance;
		private bool congestion;
		private RoadJourneyDetail[] roadJourneyDetails;
		private int totalFuelCost;
		private PrivateJourneyDetail[] journeyLegs = new PrivateJourneyDetail[1];
        private TDLocation requestOrigin;
        private TDLocation requestDestination;
		private TDLocation requestedViaLocation;
		private bool hasFerry;
        private bool matchedTravelNewsIncidents = false;

        private bool hasClosure = false;
        private bool allowReplan = true;
        
        #endregion

        #region Constructors

        /// <summary>
		/// Default RoadJourney constructor
		/// </summary>
        public RoadJourney()
        {
            journeyType = TDJourneyType.RoadCongested;
            totalDuration = 0;
            totalDistance = 0;
            congestion = false;
            roadJourneyDetails = new RoadJourneyDetail[0];
            totalFuelCost = 0;
            journeyLegs = new PrivateJourneyDetail[0];
            hasFerry = false;
            matchedTravelNewsIncidents = false;
            hasClosure = false;
            allowReplan = true;
        }


		/// <summary>
		/// Create a new RoadJourney from a CJP privateJourney
		/// </summary>
		/// <param name="privateJourney">TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney</param>
		public RoadJourney(int index, PrivateJourney privateJourney, int routenum, bool congested, TDLocation requestOrigin, TDLocation requestDestination, TDLocation requestedViaLocation, TDDateTime requestTime, bool arriveBefore) : base(index)
		{
			routeNum = routenum;
			
			this.congestion = congested;
            
			// Set the TDJourneyType
			journeyType = TDJourneyType.RoadCongested;

            //set the request origin and destination location
            this.requestOrigin = requestOrigin;
            this.requestDestination = requestDestination;

			// set the request via location
			this.requestedViaLocation = requestedViaLocation;

			this.hasFerry = false;

            this.matchedTravelNewsIncidents = false;

            // For each "section" of the journey create a RoadJourneyDetail object and place it in the
			// Details array

            RoadJourneyDetail[] roadDetails; // Temporary array used when building road journey details

			if( privateJourney.sections != null )
			{
				int directionCount = 0;
				int driveSectionCount = 0;

				roadDetails = new RoadJourneyDetail[privateJourney.sections.Length];										
				ArrayList CongestionName = new ArrayList();

                string limitedAccessText = string.Empty;
                
                for( int i = 0; i < privateJourney.sections.Length; i++ )
				{
                    // LimitedAccessText is returned as separate StopoverSection preceding the 
                    // restriction, but we want the text tacked onto the description of the 
                    // affected drive section, so don't create a detail object for it.
                    bool isLimitedAccessTextSection = (privateJourney.sections[i].GetType() == typeof(StopoverSection) && ((StopoverSection)privateJourney.sections[i]).type == StopoverSectionType.LimitedAccessRestriction);

                    if (isLimitedAccessTextSection)
                    {
                        limitedAccessText = ((StopoverSection)privateJourney.sections[i]).name;
                        continue;
                    }
                    
                    RoadJourneyDetail current = new RoadJourneyDetail(privateJourney.sections[i]);

                    if (!string.IsNullOrEmpty(limitedAccessText))
                    {
                        current.LimitedAccessText = limitedAccessText;
                        limitedAccessText = string.Empty;
                    }
                    
                    // if we hit a ferry section set flag accordingly
					if (current.IsFerry)
					{
						this.hasFerry = true;
					}

					// For congestion zones, if Stopover item is an "End", and there is £0 charge, then 
					// dont add the direction as we dont want it to be displayed.
					if (!(current.CongestionEnd && current.CongestionZoneCost <= 0))
					{
						roadDetails[directionCount] = current;

						//Get the total distance and total duration. Stop over sections not counted
						if	(privateJourney.sections[i].GetType() == typeof(DriveSection) ||
							privateJourney.sections[i].GetType() == typeof(JunctionDriveSection) ||
							privateJourney.sections[i].GetType() == typeof(StopoverSection) )
						{
							totalDistance += roadDetails[directionCount].Distance;
							totalDuration += roadDetails[directionCount].Duration;

							// Calculate the total fuel cost by adding all costs
							// for DriveSection and JunctionDriveSection
							totalFuelCost += roadDetails[driveSectionCount].CarCost;

							driveSectionCount++;
						}

						directionCount++;
					}
				}

				roadJourneyDetails = new RoadJourneyDetail[directionCount];

				Array.Copy(roadDetails, 0,  roadJourneyDetails,0, directionCount);			

			}
			else
			{
				roadJourneyDetails = null;
			}

			TDDateTime startTime;
			TDDateTime endTime;

			if (arriveBefore)
			{
				// Use the start time from the cjp journey if present - for arrive by journeys
				// this is not necessarily same as requestTime - eg where a ferry was used...
				if (this.hasFerry && (privateJourney.startTime != DateTime.MinValue) )
				{
					startTime = (TDDateTime)privateJourney.startTime;
					endTime = startTime.Add(new TimeSpan(0,0,(int)totalDuration));
				}
				else
				{
					endTime = requestTime;
					startTime = endTime.Subtract(new TimeSpan(0,0,(int)totalDuration));
				}
			}
			else
			{
				startTime = requestTime;
				endTime = startTime.Add(new TimeSpan(0,0,(int)totalDuration));
			}

			journeyLegs[0] = new PrivateJourneyDetail(ModeType.Car, requestOrigin, requestDestination, startTime, endTime);

        }

        /// <summary>
        /// Create a new RoadJourney from the provided parameters
        /// </summary>
        public RoadJourney(int index, int routeNum, RoadJourneyDetail[] details, PrivateJourneyDetail[] journeyLegs, 
            int totalDistance, long totalDuration, int totalFuelCost, double emissions, bool congested, bool allowReplan)
            : base(index)
        {
            this.routeNum = routeNum;
            this.congestion = congested;
            this.roadJourneyDetails = details;
            this.journeyLegs = journeyLegs;
            this.totalDistance = totalDistance;
            this.totalDuration = totalDuration;
            this.totalFuelCost = totalFuelCost;
            this.emissions = emissions;
            this.congestion = congested;

            journeyType = TDJourneyType.RoadCongested;
            this.hasFerry = false;
            this.matchedTravelNewsIncidents = false;

            this.hasClosure = false;
            this.allowReplan = allowReplan;
        }

        #endregion

        #region Public Properties

        /// <summary>
		/// Read only property that supplies an array containing one JourneyLeg,
		/// that represents the whole road journey as a single leg.
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
        /// Read/Write. Write provided to allow a custom PrivateJourneyDetail JouneyLeg to be added, e.g. 
        /// for one that may not be built using the CJP Private Journey.
        /// Use JourneyLegs property to read value.
        /// </summary>
        public PrivateJourneyDetail[] PrivateJourneyDetails
        {
            get { return journeyLegs; }
            set { journeyLegs = value; }
        }

		/// <summary>
		/// Read/Write property. Total duration (in seconds) calculated when RoadJourney is created using
        /// the CJP result. 
        /// Write provided to allow overwrite of the total duration if needed.
		/// </summary>
		public long TotalDuration
		{
			get { return totalDuration; }
            set { totalDuration = value; }
		}

		public int TotalDistance
		{
			get { return totalDistance; }
		}

		public bool Congestion
		{
			get { return congestion; }
		}

        
		/// <summary>
		/// Read-only property. Array of driving instructions
		/// </summary>
		public RoadJourneyDetail[] Details
		{
			get { return roadJourneyDetails; }
		}

		/// <summary>
		/// Read-only property. Total fuel cost for the journey
		/// </summary>
		/// <returns></returns>
		public int TotalFuelCost
		{
			get  { return totalFuelCost; }
			set  { totalFuelCost = value; }
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
		/// Read-only Property. Origin location of the journey
		/// </summary>
		public TDLocation OriginLocation
		{
			get { return JourneyLegs[0].LegStart.Location; }
		}

		/// <summary>
		/// Read-only Property. Destination location of the journey
		/// </summary>
		public TDLocation DestinationLocation
		{
			get { return JourneyLegs[0].LegEnd.Location; }
		}

		/// <summary>
		/// Read-only Property. Via Location of the journey
		/// </summary>
		public TDLocation RequestedViaLocation
		{
			get { return requestedViaLocation; }
        }

        /// <summary>
        /// Property to determin if the road journey is processed 
        /// to find travel news items affecting the journey
        /// The flag make sures that same journey not repeatedly processed 
        /// to find matching travel news incidents thus improves the performance
        /// by comparing only once.
        /// </summary>
        public bool JourneyMatchedForTravelNewsIncidents
        {
            get { return matchedTravelNewsIncidents; }
            set { matchedTravelNewsIncidents = value; }
        }

        /// <summary>
        /// Property to determine if the road journey is affected by closure or blockages
        /// identify using travel news affected toids.
        /// </summary>
        public bool HasClosure
        {
            get { return hasClosure; }
            set { hasClosure = value; }
        }

        /// <summary>
        /// Property to determine if the road journey should allow a replan if it 
        /// is affected by closure or blockages travel news incidents.
        /// Defaults to true
        /// </summary>
        public bool AllowReplan
        {
            get { return allowReplan; }
            set { allowReplan = value; }
        }

        #endregion

        #region Public Methods
        /// <summary>
		/// Read-only property. Array of modes used in this journey.
		/// </summary>
		public override ModeType[] GetUsedModes()
		{
			return (new ModeType[] {ModeType.Car});
		}

		/// <summary>
		/// Calculates the start time of the journey using the total duration
		/// </summary>
		/// <param name="time">The time to "leave at" or "arrive by"</param>
		/// <param name="arriveBefore">True if supplied time is the time journey ends
		/// or false if it is the time the journey starts</param>
		/// <returns>Start time of road journey</returns>
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
					TimeSpan duration = new TimeSpan( 0,0, (int)totalDuration );
					return time.Subtract(duration);
				}
			} 
			else 
			{
				return time;
			}
		}

		/// <summary>
		/// Calculates the end time of the journey using the total duration
		/// </summary>
		/// <param name="time">The time to start on or arrive by</param>
		/// <param name="arriveBefore">True if supplied time is the time journey ends
		/// or false if it is the time the journey starts</param>
		/// <returns>End time of road journey</returns>
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
				TimeSpan duration = new TimeSpan( 0,0, (int)totalDuration );
				return time.Add(duration);
			}
		}

       

        #endregion
               
    }
}
