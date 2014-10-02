// *********************************************** 
// NAME			: EBCCarJourneyDetailFormatter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 20/09/2009
// DESCRIPTION	: Provides an EBC implementation for formatting
// car journey details for output to a web page.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/EBCCarJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.9   Mar 14 2011 15:11:58   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.8   Nov 19 2010 12:25:58   apatel
//updated formatter to add information required by CJP power user.
//Resolution for 5642: EBC page brakes with an error
//
//   Rev 1.7   May 04 2010 15:05:54   RHopkins
//Added Car Sharing to Car Results page
//Resolution for 5527: Add CarSharing link to FindACar
//
//   Rev 1.6   Oct 26 2009 10:07:00   mmodi
//Display distances to 2dp
//
//   Rev 1.5   Oct 21 2009 10:51:02   mmodi
//Set flag to hide the congestion charge
//
//   Rev 1.4   Oct 19 2009 10:02:04   mmodi
//Corrected Trip km heading
//
//   Rev 1.3   Oct 16 2009 17:05:36   mmodi
//Output metres distance is CJP user
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 15 2009 13:20:58   mmodi
//Updated to not show congestion symbol, and call new EBC high value motorway method
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 11 2009 12:41:06   mmodi
//Updated to displau high value motorway icon
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 15:00:36   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.EnvironmentalBenefits;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Provides an EBC implementation for formatting
    /// car journey details for output to a web page.
    /// </summary>
    public class EBCCarJourneyDetailFormatter : CarJourneyDetailFormatter
    {
        #region Private members

        // Never showing map buttons for EBC output
        private bool mapButtonDisplay = false;

        // Used to identify high value motorways
        private EB.EnvironmentalBenefitsCalculator calculator;

        #endregion

        #region Constructors
		
        /// <summary>
		/// Constructs a formatter.
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
		/// <param name="currentCulture">Culture for returned text</param>
		public EBCCarJourneyDetailFormatter(
			RoadJourney roadJourney, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print, bool isCJPUser
			) : base(roadJourney, journeyViewState, outward, currentCulture, roadUnits, print)
		{
            this.calculator = (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];

            // Set formatter properties
            displayTollCharge = false;
            displayCongestionSymbol = false;
            displayCongestionCharge = false;
            distanceDecimalPlaces = 2;
            this.isCJPUser = isCJPUser;
		}

		#endregion

        #region Public methods

        /// <summary>
        /// Returns culture specific strings that are used to identify
        /// the elements of the string arrays returned by GetJourneyDetails
        /// The array has strings identifying accumulated distance, directions and arrival time
        /// </summary>
        /// <returns>Heading labels identifying elements of the string arrays
        /// returned by GetJourneyDetails</returns>
        public override string[] GetDetailHeadings()
        {

            string[] details = new string[4];

            string MilesHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerAccumulatedDistance", TDCultureInfo.CurrentUICulture);
            string KmHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerAccumulatedDistance.TripKm", TDCultureInfo.CurrentUICulture); ;

            //Switches the Km/Miles heading 
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[0] = "<span class=\"milesshow\">" + MilesHeading + "</span>" + "<span class=\"kmshide\">" + KmHeading + "</span>";
            }
            else
            {
                details[0] = "<span class=\"mileshide\">" + MilesHeading + "</span>" + "<span class=\"kmsshow\">" + KmHeading + "</span>";
            }

            details[1] = Global.tdResourceManager.GetString(
                "CarJourneyDetailsTableControl.headerDirections", TDCultureInfo.CurrentUICulture);

            details[2] = Global.tdResourceManager.GetString(
                "CarJourneyDetailsTableControl.headerArrivalTime", TDCultureInfo.CurrentUICulture);

            // Show direction distance instead of arrival time
            string forMilesHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerForDistance.Miles", TDCultureInfo.CurrentUICulture);
            string forKmHeading = Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.headerForDistance.Km", TDCultureInfo.CurrentUICulture); ;

            //Switches the Km/Miles heading 
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[3] = "<span class=\"milesshow\">" + forMilesHeading + "</span>" + "<span class=\"kmshide\">" + forKmHeading + "</span>";
            }
            else
            {
                details[3] = "<span class=\"mileshide\">" + forMilesHeading + "</span>" + "<span class=\"kmsshow\">" + forKmHeading + "</span>";
            }

            return details;
        }

        /// <summary>
        /// Returns items to be shown in the header/footer of the output
        /// </summary>
        /// <returns>Array of objects</returns>
        public override object[] GetFooterHeadings()
        {
            object[] details = new object[16];

            details[0] = string.Empty; // Think logo
            details[1] = string.Empty; // Think text
            details[4] = false;        // Show flag

            details[2] = string.Empty; // Congestion symbol
            details[3] = string.Empty; // Congestiton text
            details[5] = false;        // Show flag

            details[6] = string.Empty; // Petrol efficient logo
            details[7] = string.Empty; // CO2 emissions link text
            details[8] = false;        // Show flag

            details[9] = string.Empty;  // Car park link text
            details[10] = string.Empty; // Find nearest car park logo
            details[11] = false;        // Show flag

            details[12] = false;        // Show flag
            details[13] = string.Empty; // Car sharing logo
            details[14] = string.Empty; // Car sharing link text
            details[15] = string.Empty; // Car sharing link URL

            return details;
        }
        
        #endregion

        #region Protected methods

        /// <summary>
        /// A hook method called by processRoadJourney to process each road journey
        /// instruction. The returned string array contains formatted details for
        /// step number, total distance, directions and arrival time in that order
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
        /// <returns>details for each instruction</returns>
        protected override object[] processRoadJourneyDetail(int journeyDetailIndex, bool showCongestionCharge)
        {
            object[] details = new object[9];

            // Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
            // displayed is the total prior to the current instruction rather than after it	
            RoadJourneyDetail roaddetail = roadJourney.Details[journeyDetailIndex];
            RoadJourneyDetail previousroaddetail;

            if (journeyDetailIndex > 0)
            {
                previousroaddetail = roadJourney.Details[journeyDetailIndex - 1];
            }
            else
            {
                previousroaddetail = roadJourney.Details[journeyDetailIndex];
            }

            details[0] = GetCurrentStepNumber();

            if (roaddetail.viaLocation == true || roaddetail.wait == true)
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    details[1] = "<span class=\"milesshow\">" + TotalDistance + "</span>" + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
                }
                else
                {
                    details[1] = "<span class=\"mileshide\">" + TotalDistance + "</span>" + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
                }

            }
            else if (roaddetail.IsStopOver)
            {
                details[1] = "-";
            }
            else
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    details[1] = "<span class=\"milesshow\">" + TotalDistance + "</span>" + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
                }
                else
                {
                    details[1] = "<span class=\"mileshide\">" + TotalDistance + "</span>" + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
                }
            }

            //Vantive 3868786
            //is the journey a return and was congestion charge shown for the outward already?
            string direction = GetDirections(journeyDetailIndex, showCongestionCharge);

            //Add on the "high value motorway" symbol
            if (calculator.GoesThroughHighValueMotorwaySection(journeyDetailIndex, roadJourney.Details))
            {
                direction = AddHighValueMotorwaySymbol(direction);
            }

            // Add on any cjp user information
            if (isCJPUser)
            {
                direction = AddDirectionDistanceMetres(direction, journeyDetailIndex);
            }

            details[2] = direction;
            details[3] = GetArrivalTime(journeyDetailIndex);
            details[4] = GetMapInfo(journeyDetailIndex);
            details[5] = mapButtonDisplay;

            //direction distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[6] = "<span class=\"milesshow\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmshide\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
                details[7] = "<span class=\"milesshow\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmshide\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
            }
            else
            {
                details[6] = "<span class=\"mileshide\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmsshow\">" + GetDistance(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
                details[7] = "<span class=\"mileshide\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Miles) + "</span>" + "<span class=\"kmsshow\">" + GetSpeed(journeyDetailIndex, RoadUnitsEnum.Kms) + "</span>";
            }

            StringBuilder toidBuilder = new StringBuilder();
            try
            {
                foreach (string toid in roaddetail.Toid)
                {
                    toidBuilder.AppendFormat("{0} ", toid);
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
        /// A hook method called by processRoadJourney to add the first element to the  ordered list 
        /// of journey instructions. The returned string array contains step number, a "Leave from ..." 
        /// instruction and "not applicable" indicators for accumulated distance and arrival time.
        /// </summary>
        /// <returns>details for first instruction</returns>
        protected override object[] addFirstDetailLine()
        {
            StringBuilder stringDetails = new StringBuilder();
            object[] details = new object[9];

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            details[1] = string.Empty;

            details[2] = leaveFrom + " <b>" + roadJourney.OriginLocation.Description + "</b>";
            
            //arrival time
            details[3] = string.Empty;

            //map info
            details[4] = string.Empty;
            details[5] = mapButtonDisplay;

            //direction distance
            details[6] = string.Empty;

            //speed
            details[7] = string.Empty;

            try
            {
                //Link Toids
                StringBuilder toidBuilder = new StringBuilder();

                if (roadJourney != null && roadJourney.OriginLocation != null)
                {
                    if (roadJourney.OriginLocation.Toid != null)
                    {
                        foreach (string toid in roadJourney.OriginLocation.Toid)
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
        /// A hook method called by processRoadJourney to add the last element to the 
        /// ordered list of journey instructions. The returned string array contains step number,
        /// an "arrive at ..." instruction and arrival time, and an empty string for accumulated distance
        /// </summary>
        /// <returns>details for last instruction</returns>
        protected override object[] addLastDetailLine()
        {
            StringBuilder stringDetails = new StringBuilder();
            object[] details = new object[9];

            int journeyDetailIndex = roadJourney.Details.Length - 1;

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                details[1] = "<span class=\"milesshow\">" + TotalDistance + "</span>" + "<span class=\"kmshide\">" + TotalKmDistance + "</span>";
            }
            else
            {
                details[1] = "<span class=\"mileshide\">" + TotalDistance + "</span>" + "<span class=\"kmsshow\">" + TotalKmDistance + "</span>";
            }

            //instruction
            stringDetails.Append(arriveAt);
            stringDetails.Append("<b>");
            stringDetails.Append(roadJourney.DestinationLocation.Description);
            stringDetails.Append("</b>");
            stringDetails.Append(" &nbsp; ");

            details[2] = stringDetails.ToString();

            //arrival time
            details[3] = GetArrivalTime(journeyDetailIndex);

            //map info
            details[4] = string.Empty;
            details[5] = mapButtonDisplay;

            //direction distance
            details[6] = string.Empty;

            //speed
            details[7] = string.Empty;

            try
            {
                //Link Toids
                StringBuilder toidBuilder = new StringBuilder();

                if (roadJourney != null && roadJourney.DestinationLocation != null)
                {
                    if (roadJourney.DestinationLocation.Toid != null)
                    {
                        foreach (string toid in roadJourney.DestinationLocation.Toid)
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
        /// Override AddContinueFor method which returns the routetext unaltered
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="nextDetailHasJunctionExitJunction"></param>
        /// <param name="routeText"></param>
        /// <returns></returns>
        protected override string AddContinueFor(RoadJourneyDetail detail,
            bool nextDetailHasJunctionExitJunction, string routeText)
        {
            return routeText;
        }

        /// <summary>
        /// Override AddLimitedAccessText method, returning the routetext unaltered
        /// </summary>
        /// <param name="detail">The detail</param>
        /// <param name="routeText">Existing instruction text</param>
        /// <returns>Text with limited access text appended</returns>
        protected override string AddLimitedAccessText(RoadJourneyDetail detail, string routeText)
        {
            return routeText;
        }

        /// <summary>
        /// A hook method called by processRoadJourney to add the 'Think' symbol, link and message to the end of
        /// the ordered list of journey instructions.
        /// </summary>
        /// <returns>An empty object array</returns>
        protected override object[] addThinkSymbol()
        {
            object[] details = new object[2];

            details[0] = string.Empty;
            details[1] = string.Empty;

            return details;
        }

        /// <summary>
        /// A hook method called by processRoadJourney to add the Traffic warning symbol and message to the end of
        /// the ordered list of journey instructions.
        /// </summary>
        /// <returns>An empty object array</returns>
        protected override object[] addTrafficWarning()
        {
            object[] details = new object[2];

            details[0] = string.Empty;
            details[1] = string.Empty;

            return details;
        }

        /// <summary>
        /// Returns formatted string of the road name for the supplied
        /// instruction. Overrides default implmentation to enclose road
        /// name in HTML bold element.
        /// </summary>
        /// <param name="detail">Details of journey instruction</param>
        /// <returns>The road name</returns>
        protected override string FormatRoadName(RoadJourneyDetail detail)
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
        #endregion

    }
}
