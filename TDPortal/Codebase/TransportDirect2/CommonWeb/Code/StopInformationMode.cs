// *********************************************** 
// NAME             : StopInformationMode.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 11 Jun 2012
// DESCRIPTION  	: Enum to specify the stop information mode
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.Web
{
    /// <summary>
    /// Enum to specify the stop information mode
    /// </summary>
    public enum StopInformationMode
    {
        StationBoardDeparture,
        StationBoardArrival
    }

    /// <summary>
    /// StopInformationModeHelper class
    /// </summary>
    public class StopInformationModeHelper
    {
        /// <summary>
        /// Parses a query string stop information mode value into an StopInformationMode
        /// </summary>
        public static StopInformationMode GetStopInformationModeQS(string qsStopInformationMode)
        {
            // Default to StationBoardDeparture is no value supplied
            if (string.IsNullOrEmpty(qsStopInformationMode))
                return StopInformationMode.StationBoardDeparture;

            string mode = qsStopInformationMode.ToUpper().Trim();

            switch (mode)
            {
                case "N":
                case "ND": // Next departures
                    return StopInformationMode.StationBoardDeparture;
                case "NA": // Next arrivals
                    return StopInformationMode.StationBoardArrival;
                default:
                    throw new TDPException(
                        string.Format("Error parsing querystring Stop Information value into an StopInformationMode, unrecognised value[{0}]", qsStopInformationMode),
                        false, TDPExceptionIdentifier.SIErrorParsingStopInformationMode);
            }
        }

        /// <summary>
        /// Returns a query string representation of the StopInformationMode
        /// </summary>
        public static string GetStopInformationModeQS(StopInformationMode mode)
        {
            switch (mode)
            {
                case StopInformationMode.StationBoardDeparture:
                    return "ND"; // Next departures
                case StopInformationMode.StationBoardArrival:
                    return "NA"; // Next arrivals
                default:
                    return "N"; // Next departures
            }
        }
    }
}
