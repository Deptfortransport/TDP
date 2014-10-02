// *********************************************** 
// NAME			: EmailCycleJourneyDetailFormatter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 22 Aug 2008
// DESCRIPTION	: Provides an email implementation for formatting
// cycle journey details for output to an email
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/EmailCycleJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.3   Oct 22 2008 15:37:16   mmodi
//Added space between location instructions
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Oct 20 2008 14:24:36   mmodi
//Pass in CJP user flag
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Oct 10 2008 15:44:00   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Sep 02 2008 10:44:20   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class EmailCycleJourneyDetailFormatter : CycleJourneyDetailFormatter
    {
        #region Constructors

        /// <summary>
		/// Constructs a formatter.
		/// </summary>
        /// <param name="cycleJourney">The specific cycle journey to display</param>
        /// <param name="journeyViewState">The related journey view state, used specifically for congestion zone</param>
        /// <param name="outward">Indicates if this is an outward journey</param>
        /// <param name="roadUnits">The road units to display the journey distance in</param>
        /// <param name="print">Indicates if the instructions are displayed on a printer friendly page</param>
        /// <param name="showAllDetails">Indicates if the attributes are set to visible or hidden by default</param>
        public EmailCycleJourneyDetailFormatter(
			CycleJourney cycleJourney, 
			TDJourneyViewState journeyViewState,
			bool outward,
			RoadUnitsEnum roadUnits, 
            bool print,
            bool showAllDetails
            )
            : base(cycleJourney, journeyViewState, outward, roadUnits, print, showAllDetails, false)
		{
		}

		#endregion Constructors

        #region Override Public methods

        /// <summary>
        /// Returns culture specific strings that are used to identify
        /// the elements of the string arrays returned by GetJourneyDetails
        /// The array has strings identifying accumulated distance, directions and arrival time
        /// </summary>
        /// <returns>Heading labels identifying elements of the string arrays
        /// returned by GetJourneyDetails</returns>
        public override string[] GetDetailHeadings()
        {

            string[] details = new string[3];

            string MilesHeading = Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.headerAccumulatedDistance.Miles", TDCultureInfo.CurrentUICulture);
            string KmHeading = Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.headerAccumulatedDistance.Kms", TDCultureInfo.CurrentUICulture);

            //Switches the Km/Miles heading 
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[0] = MilesHeading;
            }
            else
            {
                details[0] = KmHeading;
            }

            details[1] = Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.headerDirections", TDCultureInfo.CurrentUICulture);

            details[2] = Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.headerArrivalTime", TDCultureInfo.CurrentUICulture);

            return details;
        }

        #endregion

        #region Override Protected methods

        /// <summary>
        /// A hook method called by ProcessCycleJourney to process each cycle journey
        /// instruction. The returned string array contains formatted details for
        /// step number, total distance, directions and arrival time in that order
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>details for each instruction</returns>
        protected override object[] ProcessCycleJourneyDetail(int journeyDetailIndex, bool showCongestionCharge)
        {
            object[] details = new object[4];

            // Position/values
            // 0 = step number
            // 1 = total distance
            // 2 = direction instruction + cycle instruction
            // 3 = arrival time

            // Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
            // displayed is the total prior to the current instruction rather than after it	
            CycleJourneyDetail cycleDetail = cycleJourney.Details[journeyDetailIndex];

            details[0] = GetCurrentStepNumber();

            if (cycleDetail.ViaLocation == true || cycleDetail.WaitSection == true)
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    details[1] = TotalDistance;
                }
                else
                {
                    details[1] = TotalKmDistance;
                }
            }
            else if (cycleDetail.StopoverSection)
            {
                details[1] = "-";
            }
            else
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    details[1] = TotalDistance;
                }
                else
                {
                    details[1] = TotalKmDistance;
                }
            }

            //is the journey a return and was congestion charge shown for the outward already?
            string directions = GetDirections(journeyDetailIndex, showCongestionCharge);
            string cycleInstructions = GetCyclePathName(journeyDetailIndex);

            if (!string.IsNullOrEmpty(cycleInstructions))
            {
                directions += "( " + cycleInstructions + " )";
            }

            details[2] = directions;
                
            details[3] = GetArrivalTime(journeyDetailIndex);

            return details;
        }

        /// <summary>
        /// A hook method called by ProcessCycleJourney to add the first element to the ordered list 
        /// of journey instructions. The returned string array contains step number, a "Leave from ..." 
        /// instruction and "not applicable" indicators for accumulated distance and arrival time.
        /// </summary>
        /// <returns>details for first instruction</returns>
        protected override object[] AddFirstDetailLine()
        {
            object[] details = new object[4];

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[1] = TotalDistance;
            }
            else
            {
                details[1] = TotalKmDistance;
            }

            //instruction
            details[2] = leaveFrom + " " + cycleJourney.OriginLocation.Description;

            //arrival time
            details[3] = string.Empty;

            return details;
        }

        /// <summary>
        /// A hook method called by ProcessCycleJourney to add the last element to the 
        /// ordered list of journey instructions. The returned string array contains step number,
        /// an "arrive at ..." instruction and arrival time, and an empty string for accumulated distance
        /// </summary>
        /// <returns>details for last instruction</returns>
        protected override object[] AddLastDetailLine()
        {
            object[] details = new object[4];

            int journeyDetailIndex = cycleJourney.Details.Length - 1;

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[1] = TotalDistance;
            }
            else
            {
                details[1] = TotalKmDistance;
            }

            //instruction
            details[2] = arriveAt + " " + cycleJourney.DestinationLocation.Description;

            //arrival time
            details[3] = GetArrivalTime(journeyDetailIndex);

            return details;
        }

        protected override string AddContinueFor(CycleJourneyDetail detail,
            bool nextDetailHasJunctionExitJunction, string routeText)
        {
            //convert metres to miles
            string milesDistance = ConvertMetresToMileage(detail.Distance);
            string distanceInKm = ConvertMetresToKm(detail.Distance);
            string distanceInMiles = string.Empty;
            string kmDistance = string.Empty;
            string ferryCrossing = Global.tdResourceManager.GetString("RouteText.FerryCrossing", TDCultureInfo.CurrentUICulture);

                distanceInMiles = milesDistance + space + miles;
                kmDistance = distanceInKm + space + "km";
            
            //check if this text should be added
            if (detail.Ferry)
            {
                //no need to add the "continues for..." message in these situations
                routeText = ferryCrossing;

                return routeText;
            }

            else if (
                (detail.Distance <= immediateTurnDistance) &&

                !((detail.JunctionType == JunctionType.Entry) &&
                detail.JunctionSection &&
                (detail.PlaceName != null && detail.PlaceName.Length > 0) &&
                nextDetailHasJunctionExitJunction) &&

                !(detail.JunctionSection &&
                ((detail.JunctionType == JunctionType.Exit) ||
                (detail.JunctionType == JunctionType.Merge)))
                )
            {
                //no need to add the "continues for..." message in these situations
                return routeText;

            }

            else if ((detail.SlipRoad) && (detail.Distance < slipRoadDistance) &&

                !((detail.JunctionType == JunctionType.Entry) &&
                detail.JunctionSection &&
                (detail.PlaceName != null && detail.PlaceName.Length > 0) &&
                nextDetailHasJunctionExitJunction) &&

                !(detail.JunctionSection &&
                ((detail.JunctionType == JunctionType.Exit) ||
                (detail.JunctionType == JunctionType.Merge)) &&
                ((detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))))
            {
                //no need to add the "continues for..." message in these situations
                return routeText;
            }
            else
            {
                //in all other cases add "continues for..." to the end of the instruction 
                routeText = routeText + continueFor + space;

                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    routeText += distanceInMiles;
                }
                else
                {
                    routeText += kmDistance;
                }
            }

            return routeText;
        }

        /// <summary>
        /// A hook method called by ProcessCycleJourney to filter the details returned to only include
        /// the instructions at the start and end index
        /// </summary>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions), filtered 
        /// to only include instructions between the start and end indexs</returns>
        protected override IList FilterCycleJourneyDetails(IList details, int startIndex, int endIndex)
        {
            ArrayList filteredDetails = new ArrayList();

            // The details array passed in will have the actual instructions, 
            // plus a first and last instruction which says leave/arrive at location.
            // The start/end index supplied do not "know" about these additional instructions, 
            // therefore, we need to include these instructions when applicable.

            for (int i = 0; i < details.Count; i++)
            {
                object[] detail = (object[])details[i];

                int stepNumber = Convert.ToInt32(detail[0]);

                // start index is "actual" first instruction, so also need to add the "leaving from" direction
                if ((startIndex == 1) && (stepNumber == 1))
                {
                    filteredDetails.Add(detail);
                }
                // end index is "actual" last instruction, so also need to add the "arrive at" direction
                else if ((endIndex == details.Count - 2) && (stepNumber == details.Count))
                {
                    filteredDetails.Add(detail);
                }
                // this detail is within the range asked for, so add to filtered array
                else if ((stepNumber >= startIndex + 1) && (stepNumber <= endIndex + 1))
                {
                    filteredDetails.Add(detail);
                }
            }

            return filteredDetails;
        }

        #endregion
    }
}
