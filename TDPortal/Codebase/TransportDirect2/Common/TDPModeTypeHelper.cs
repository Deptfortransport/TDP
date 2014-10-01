// *********************************************** 
// NAME             : TDPModeTypeHelper.cs.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 20 Feb 2012
// DESCRIPTION  	: TDPModeTypeHelper.cs helper class
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common
{
    /// <summary>
    /// TDPModeTypeHelper helper class
    /// </summary>
    public class TDPModeTypeHelper
    {
        /// <summary>
        /// Parses a query string transport mode value into an TDPModeType
        /// </summary>
        public static TDPModeType GetTDPModeTypeQS(string qsMode)
        {
            try
            {
                string mode = qsMode.ToUpper().Trim();

                switch (mode)
                {
                    case "P":
                        return TDPModeType.Air;
                    case "B":
                        return TDPModeType.Bus;
                    case "O":
                        return TDPModeType.Coach;
                    case "C":
                        return TDPModeType.Telecabine;
                    case "D":
                        return TDPModeType.Drt;
                    case "F":
                        return TDPModeType.Ferry;
                    case "M":
                        return TDPModeType.Metro;
                    case "R":
                        return TDPModeType.Rail;
                    case "T":
                        return TDPModeType.Tram;
                    case "U":
                        return TDPModeType.Underground;
                    default:
                        throw new Exception("Failed to parse TDPModeType");
                }
            }
            catch
            {
                // Any exception, default to unknown
                return TDPModeType.Unknown;
            }
        }

        /// <summary>
        /// Returns a query string representation of the TDPModeType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static string GetTDPModeTypeQS(TDPModeType mode)
        {
            switch (mode)
            {
                case TDPModeType.Air:
                    return "P";
                case TDPModeType.Bus:
                    return "B";
                case TDPModeType.Coach:
                    return "O";
                case TDPModeType.Telecabine:
                    return "C";
                case TDPModeType.Drt:
                    return "D";
                case TDPModeType.Ferry:
                    return "F";
                case TDPModeType.Metro:
                    return "M";
                case TDPModeType.Rail:
                    return "R";
                case TDPModeType.Tram:
                    return "T";
                case TDPModeType.Underground:
                    return "U";
                default:
                    return string.Empty;
            }
        }
    }
}
