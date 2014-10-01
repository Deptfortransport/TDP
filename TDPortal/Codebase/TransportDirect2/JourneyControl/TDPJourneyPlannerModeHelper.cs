// *********************************************** 
// NAME             : TDPJourneyPlannerModeHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 20 Feb 2012
// DESCRIPTION  	: TDPJourneyPlannerMode helper class
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// TDPJourneyPlannerMode helper class
    /// </summary>
    public class TDPJourneyPlannerModeHelper
    {
        /// <summary>
        /// Parses a query string journey planner moder value into an TDPJourneyPlannerMode
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPJourneyPlannerMode GetTDPJourneyPlannerModeQS(string qsPlannerMode)
        {
            try
            {
                string plannerMode = qsPlannerMode.ToUpper().Trim();

                switch (plannerMode)
                {
                    case "PT":
                        return TDPJourneyPlannerMode.PublicTransport;
                    case "RS":
                        return TDPJourneyPlannerMode.RiverServices;
                    case "PR":
                        return TDPJourneyPlannerMode.ParkAndRide;
                    case "BB":
                        return TDPJourneyPlannerMode.BlueBadge;
                    case "CY":
                        return TDPJourneyPlannerMode.Cycle;
                    default:
                        throw new Exception("Failed to parse TDPJourneyPlannerMode");
                }
            }
            catch
            {
                // Any exception, default to PublicTransport
                return TDPJourneyPlannerMode.PublicTransport;
            }
        }

        /// <summary>
        /// Returns a query string representation of the TDPJourneyPlannerMode
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static string GetTDPJourneyPlannerModeQS(TDPJourneyPlannerMode plannerMode)
        {
            switch (plannerMode)
            {
                case TDPJourneyPlannerMode.RiverServices:
                    return "RS";
                case TDPJourneyPlannerMode.ParkAndRide:
                    return "PR";
                case TDPJourneyPlannerMode.BlueBadge:
                    return "BB";
                case TDPJourneyPlannerMode.Cycle:
                    return "CY";
                default:
                    return "PT";
            }
        }
    }
}
