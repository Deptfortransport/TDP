// *********************************************** 
// NAME             : TDPCheckConstraintHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 24 May 2011
// DESCRIPTION  	: TDPCheckConstraintHelper class providing helper methods
// ************************************************
// 

using TDP.Common;
using ICJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// TDPCheckConstraintHelper class providing helper methods
    /// </summary>
    public class TDPCheckConstraintHelper
    {
        /// <summary>
        /// Parses a check process strring into a CJP CheckProcess type
        /// </summary>
        public static ICJP.CheckProcess GetCJPCheckProcess(string tdpCheckProcess)
        {
            if (!string.IsNullOrEmpty(tdpCheckProcess))
            {
                switch (tdpCheckProcess.ToLower().Trim())
                {
                    #region Check process
                    case "securitycheck":
                        return ICJP.CheckProcess.securityCheck;
                    case "egress":
                        return ICJP.CheckProcess.egress;
                    #endregion
                    default:
                        throw new TDPException(
                            string.Format("Error parsing Check Process string into an CJP CheckProcess, unrecognised value[{0}]", tdpCheckProcess),
                            false, TDPExceptionIdentifier.JCErrorParsingCheckConstraint);
                }
            }
            
            return ICJP.CheckProcess.unknown;
        }

        /// <summary>
        /// Parses a congestion reason strring into a CJP CongestionReason type
        /// </summary>
        public static ICJP.CongestionReason GetCJPCongestionReason(string tdpCongestionReason)
        {
            if (!string.IsNullOrEmpty(tdpCongestionReason))
            {
                switch (tdpCongestionReason.ToLower().Trim())
                {
                    #region Congestion reason
                    case "queue":
                        return ICJP.CongestionReason.queue;
                    case "crowding":
                        return ICJP.CongestionReason.crowding;
                    #endregion
                    default:
                        throw new TDPException(
                            string.Format("Error parsing Congestion Reason string into an CJP CongestionReason, unrecognised value[{0}]", tdpCongestionReason),
                            false, TDPExceptionIdentifier.JCErrorParsingCheckConstraint);
                }
            }

            return ICJP.CongestionReason.unknown;
        }
    }
}
