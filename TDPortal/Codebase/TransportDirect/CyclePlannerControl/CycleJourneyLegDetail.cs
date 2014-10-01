// *********************************************** 
// NAME			: CycleJourneyLegDetail.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: This class represents the high level view of the cycle journey.
//				  It is the complement to the PublicJourneyDetail, and PrivateJourneyDetail class.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CycleJourneyLegDetail.cs-arc  $
//
//   Rev 1.0   Jun 20 2008 15:41:56   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class CycleJourneyLegDetail : JourneyLeg
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public CycleJourneyLegDetail()
        {
        }

        /// <summary>
		/// Constructor to create journey leg for a cycle journey
		/// </summary>
		/// <param name="departLocation">Location from which the journey departs</param>
		/// <param name="arriveLocation">Location at which the journey arrives</param>
		/// <param name="departDateTime">Date/time at which the journey departs</param>
		/// <param name="arriveDateTime">Date/time at which the journey arrives</param>
        public CycleJourneyLegDetail(ModeType mode, TDLocation departLocation, TDLocation arriveLocation, TDDateTime departDateTime, TDDateTime arriveDateTime)
		{
			this.mode = mode;

			this.legStart = new PublicJourneyCallingPoint(
				departLocation,
				null,
				departDateTime,
				PublicJourneyCallingPointType.Board);

			this.legEnd = new PublicJourneyCallingPoint(
				arriveLocation,
				arriveDateTime,
				null,
				PublicJourneyCallingPointType.Alight);
		}

        #endregion

        #region Properties

        /// <summary>
        /// Returns the array of vehicle feature indicators.
        /// There are none for a cycle journey.
        /// </summary>
        public override int[] GetVehicleFeatures()
        {
            return new int[0];
        }

        /// <summary>
        /// Gets the Display Notes.
        /// There are none for a cycle journey.
        /// </summary>
        public override string[] GetDisplayNotes()
        {
            return new string[0];
        }

        #endregion
    }
}
