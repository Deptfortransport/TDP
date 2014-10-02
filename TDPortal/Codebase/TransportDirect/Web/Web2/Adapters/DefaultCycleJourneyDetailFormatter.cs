// *********************************************** 
// NAME			: DefaultCycleJourneyDetailFormatter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 05 Aug 2008
// DESCRIPTION	: Provides a default implementation for formatting
// cycle journey details for output to a web page.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/DefaultCycleJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.8   Nov 05 2010 10:23:32   apatel
//Added extra null check when creating toid detail for cycle journey detail
//Resolution for 5623: Additional information available to CJP users
//Resolution for 5631: Cycle UI and EES web service shows wrong attribute description
//
//   Rev 1.7   Oct 28 2010 11:18:10   apatel
//Updated to correct the pvc comment as files assigned to wrong IR 5622 instead of IR 5623
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.6   Oct 26 2010 14:37:36   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.5   Oct 20 2008 14:24:36   mmodi
//Pass in CJP user flag
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Oct 10 2008 15:43:32   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Sep 15 2008 10:46:40   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:22:58   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 08 2008 12:05:44   mmodi
//Updated as part of workstream
//
//   Rev 1.0   Aug 06 2008 14:57:42   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Text;
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
    /// <summary>
    /// Provides a default implementation for formatting
    /// cycle journey details for output to a web page.
    /// </summary>
    public class DefaultCycleJourneyDetailFormatter : CycleJourneyDetailFormatter
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
        /// <param name="isCJPUser">When true, outputs additional direction values</param>
        public DefaultCycleJourneyDetailFormatter(
			CycleJourney cycleJourney, 
			TDJourneyViewState journeyViewState,
			bool outward,
			RoadUnitsEnum roadUnits, 
            bool print,
            bool showAllDetails,
            bool isCJPUser
            )
            : base(cycleJourney, journeyViewState, outward, roadUnits, print, showAllDetails, isCJPUser)
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
                details[0] =
                    "<span class=\"milesshow\">" + MilesHeading + "</span>"
                  + "<span class=\"kmshide\">" + KmHeading + "</span>";
            }
            else
            {
                details[0] =
                    "<span class=\"mileshide\">" + MilesHeading + "</span>"
                  + "<span class=\"kmsshow\">" + KmHeading + "</span>";
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
            object[] details = new object[9];

            // Position/values
            // 0 = step number
            // 1 = total distance
            // 2 = direction instruction
            // 3 = cycle path name
            // 4 = cycle path image
            // 5 = cycle instruction text
            // 6 = cycle attibute details
            // 7 = arrival time
            // 8 = toid detail

            // Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
            // displayed is the total prior to the current instruction rather than after it	
            CycleJourneyDetail cycleDetail = cycleJourney.Details[journeyDetailIndex];
            
            details[0] = GetCurrentStepNumber();

            if (cycleDetail.ViaLocation == true || cycleDetail.WaitSection == true)
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    details[1] = 
                        "<span class=\"milesshow\">" + TotalDistance + "</span>" 
                      + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
                }
                else
                {
                    details[1] = 
                        "<span class=\"mileshide\">" + TotalDistance + "</span>" 
                      + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
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
                    details[1] =
                        "<span class=\"milesshow\">" + TotalDistance + "</span>"
                      + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
                }
                else
                {
                    details[1] =
                        "<span class=\"mileshide\">" + TotalDistance + "</span>"
                      + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
                }
            }

            //is the journey a return and was congestion charge shown for the outward already?
            details[2] = GetDirections(journeyDetailIndex, showCongestionCharge);
            details[3] = GetCyclePathName(journeyDetailIndex);
            details[4] = GetCyclePathImage(journeyDetailIndex);
            details[5] = GetCycleInstructionDetails(journeyDetailIndex);
            details[6] = GetCycleAttributeDetails(journeyDetailIndex);
            details[7] = GetArrivalTime(journeyDetailIndex);

            StringBuilder toidBuilder = new StringBuilder();

            try
            {
                if (cycleJourney.DestinationLocation != null)
                {
                    if (cycleDetail != null && cycleDetail.Toid != null)
                    {
                        foreach (string toid in cycleDetail.Toid)
                        {
                            toidBuilder.AppendFormat("{0} ", toid);
                        }
                    }
                }

                details[8] = toidBuilder.ToString().Trim();
            }
            catch
            {
                details[8] = string.Empty;
            }

            
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
            object[] details = new object[9];

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[1] =
                        "<span class=\"milesshow\">" + TotalDistance + "</span>"
                      + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
            }
            else
            {
                details[1] =
                        "<span class=\"mileshide\">" + TotalDistance + "</span>"
                      + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
            }

            //instruction
            details[2] = leaveFrom + " <b>" + cycleJourney.OriginLocation.Description + "</b> &nbsp;";

            //cycle path name
            details[3] = string.Empty;

            //cycle path image
            details[4] = string.Empty;

            //cycle instruction text
            details[5] = string.Empty;

            //cycle attribute details
            details[6] = string.Empty;

            //arrival time
            details[7] = string.Empty;

            //toid
            details[8] = string.Empty;
            
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
            object[] details = new object[9];
            
            int journeyDetailIndex = cycleJourney.Details.Length - 1;

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[1] = 
                        "<span class=\"milesshow\">" + TotalDistance + "</span>"
                      + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
            }
            else
            {
                details[1] = 
                        "<span class=\"mileshide\">" + TotalDistance + "</span>"
                      + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
            }

            //instruction
            details[2] = arriveAt + "<b>" + cycleJourney.DestinationLocation.Description + "</b> &nbsp;";

            //cycle path name
            details[3] = string.Empty;

            //cycle path image
            details[4] = string.Empty;

            //cycle instruction text
            details[5] = string.Empty;

            //cycle attribute details
            details[6] = string.Empty;

            //arrival time
            details[7] = GetArrivalTime(journeyDetailIndex);

            StringBuilder toidBuilder = new StringBuilder();

            if (cycleJourney.DestinationLocation != null)
            {
                foreach (string toid in cycleJourney.DestinationLocation.Toid)
                {
                    toidBuilder.AppendFormat("{0} ", toid);
                }
            }

            details[8] = toidBuilder.ToString().Trim();

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

            //Switches the default display of either Miles or Kms depending on roadUnits
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                distanceInMiles = "<span class=\"milesshow\">" + milesDistance + space + miles + "</span>";
                kmDistance = "<span class=\"kmshide\">" + distanceInKm + space + "km" + "</span>";
            }
            else
            {
                distanceInMiles = "<span class=\"mileshide\">" + milesDistance + space + miles + "</span>";
                kmDistance = "<span class=\"kmsshow\">" + distanceInKm + space + "km" + "</span>";
            }

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
                routeText = routeText + continueFor + space + kmDistance + distanceInMiles;

            }

            return routeText;
        }

        /// <summary>
        /// Returns formatted string of the road name for the supplied
        /// instruction. Overrides default implmentation to enclose road
        /// name in HTML bold element.
        /// </summary>
        /// <param name="detail">Details of journey instruction</param>
        /// <returns>The road name</returns>
        protected override string FormatRoadName(CycleJourneyDetail detail)
        {
            //Check for a motorway
            if (detail.RoadNumber != null)
            {

                if ((detail.RoadNumber.StartsWith("M") | (detail.RoadNumber.EndsWith("(M)"))) && (detail.RoadName != string.Empty))
                {
                    return "<span class=\"mwayLabel\">" + detail.RoadNumber + "</span>" + "<b>" + " (" + detail.RoadName + ")" + "</b> ";
                }
                else if ((detail.RoadNumber.StartsWith("M")) | (detail.RoadNumber.EndsWith("(M)")))
                {
                    return "<span class=\"mwayLabel\">" + detail.RoadNumber + "</span>";
                }
                else
                {
                    return "<b>" + base.FormatRoadName(detail) + "</b>";
                }
            }
            else
            {
                return " ";
            }
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
