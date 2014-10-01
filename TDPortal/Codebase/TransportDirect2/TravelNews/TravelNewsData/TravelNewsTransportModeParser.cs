// *********************************************** 
// NAME             : TravelNewsTransportModeParser.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 08 Mar 2012
// DESCRIPTION  	: TravelNewsTransportModeParser for parsing a text string into a TDPModeType
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common;

namespace TDP.UserPortal.TravelNews.TravelNewsData
{
    /// <summary>
    /// TravelNewsTransportModeParser for parsing a text string into a TDPModeType
    /// </summary>
    public static class TravelNewsTransportModeParser
    {
        #region Public methods

        /// <summary>
        /// Gets the an TDPModeType using spcified travel news mode of transport string
        /// </summary>
        /// <param name="value">venue string</param>
        public static TDPModeType GetTDPModeType(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Mode of transport is a free text string according to the xml schema, 
                // so best attempt is to check for matching strings.
                value = value.ToLower().Trim().Replace(" ", string.Empty);

                if (value.Contains("air"))
                    return TDPModeType.Air;
                else if (value.Contains("bus"))
                    return TDPModeType.Bus;
                else if (value.Contains("cable"))
                    return TDPModeType.Telecabine;
                else if (value.Contains("car"))
                    return TDPModeType.Car;
                else if (value.Contains("road"))
                    return TDPModeType.Car;
                else if (value.Contains("coach"))
                    return TDPModeType.Coach;
                else if (value.Contains("cycle"))
                    return TDPModeType.Cycle;
                else if (value.Contains("lightrail"))
                    return TDPModeType.Drt;
                else if (value.Contains("ferry"))
                    return TDPModeType.Ferry;
                else if (value.Contains("metro"))
                    return TDPModeType.Metro;
                else if (value.Contains("rail"))
                    return TDPModeType.Rail;
                else if (value.Contains("telecabine"))
                    return TDPModeType.Telecabine;
                else if (value.Contains("tram"))
                    return TDPModeType.Tram;
                else if (value.Contains("underground"))
                    return TDPModeType.Underground;
                else if (value.Contains("walk"))
                    return TDPModeType.Walk;
                else if (value.Contains("eurotunnel"))
                    return TDPModeType.EuroTunnel;
            }

            // Return default
            return TDPModeType.Unknown;
        }




        #endregion
    }
}
