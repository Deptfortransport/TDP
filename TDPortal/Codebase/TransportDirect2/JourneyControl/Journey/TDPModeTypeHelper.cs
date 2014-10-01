// *********************************************** 
// NAME             : TDPModeTypeHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 27 Mar 2011
// DESCRIPTION  	: TDPModeTypeHelper class providing helper methods 
// ************************************************
// 

using TDP.Common;
using ICJP = TransportDirect.JourneyPlanning.CJPInterface;
using TDP.Common.LocationService;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// TDPModeTypeHelper class providing helper methods 
    /// </summary>
    public class TDPModeTypeHelper
    {
        /// <summary>
        /// Parses a CJP ModeType into an TDPModeType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPModeType GetTDPModeType(ICJP.ModeType cjpModeType)
        {
            switch (cjpModeType)
            {
                #region CJP interface mode type

                case ICJP.ModeType.Air:
                    return TDPModeType.Air;
                case ICJP.ModeType.Bus:
                    return TDPModeType.Bus;
                case ICJP.ModeType.Car:
                    return TDPModeType.Car;
                case ICJP.ModeType.CheckIn:
                    return TDPModeType.CheckIn;
                case ICJP.ModeType.CheckOut:
                    return TDPModeType.CheckOut;
                case ICJP.ModeType.Coach:
                    return TDPModeType.Coach;
                case ICJP.ModeType.Cycle:
                    return TDPModeType.Cycle;
                case ICJP.ModeType.Drt:
                    return TDPModeType.Drt;
                case ICJP.ModeType.Ferry:
                    return TDPModeType.Ferry;
                case ICJP.ModeType.Metro:
                    return TDPModeType.Metro;
                case ICJP.ModeType.Rail:
                    return TDPModeType.Rail;
                case ICJP.ModeType.RailReplacementBus:
                    return TDPModeType.RailReplacementBus;
                case ICJP.ModeType.Taxi:
                    return TDPModeType.Taxi;
                case ICJP.ModeType.Telecabine:
                    return TDPModeType.Telecabine;
                case ICJP.ModeType.Tram:
                    return TDPModeType.Tram;
                case ICJP.ModeType.Transfer:
                    return TDPModeType.Transfer;
                case ICJP.ModeType.Underground:
                    return TDPModeType.Underground;
                case ICJP.ModeType.Walk:
                    return TDPModeType.Walk;
                #endregion
                default:
                    throw new TDPException(
                        string.Format("Error parsing CJP ModeType into an TDPModeType, unrecognised value[{0}]", cjpModeType.ToString()), 
                        false, TDPExceptionIdentifier.JCErrorParsingCJPModeType);
            }
        }

        /// <summary>
        /// Parses a TDPModeType into an CJP ModeType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static ICJP.ModeType GetCJPModeType(TDPModeType tdpModeType)
        {
            switch (tdpModeType)
            {
                #region Mode type
                case TDPModeType.Air:
                    return ICJP.ModeType.Air;
                case TDPModeType.Bus:
                    return ICJP.ModeType.Bus;
                case TDPModeType.Car:
                    return ICJP.ModeType.Car;
                case TDPModeType.CheckIn:
                    return ICJP.ModeType.CheckIn;
                case TDPModeType.CheckOut:
                    return ICJP.ModeType.CheckOut;
                case TDPModeType.Coach:
                    return ICJP.ModeType.Coach;
                case TDPModeType.Cycle:
                    return ICJP.ModeType.Cycle;
                case TDPModeType.Drt:
                    return ICJP.ModeType.Drt;
                case TDPModeType.Ferry:
                    return ICJP.ModeType.Ferry;
                case TDPModeType.Metro:
                    return ICJP.ModeType.Metro;
                case TDPModeType.Rail:
                    return ICJP.ModeType.Rail;
                case TDPModeType.RailReplacementBus:
                    return ICJP.ModeType.RailReplacementBus;
                case TDPModeType.Taxi:
                    return ICJP.ModeType.Taxi;
                case TDPModeType.Telecabine:
                    return ICJP.ModeType.Telecabine;
                case TDPModeType.Tram:
                    return ICJP.ModeType.Tram;
                case TDPModeType.Transfer:
                    return ICJP.ModeType.Transfer;
                case TDPModeType.Underground:
                    return ICJP.ModeType.Underground;
                case TDPModeType.Walk:
                    return ICJP.ModeType.Walk;
                #endregion
                default:
                    throw new TDPException(
                        string.Format("Error parsing TDPModeType into an CJP ModeType, unrecognised value[{0}]", tdpModeType.ToString()),
                        false, TDPExceptionIdentifier.JCErrorParsingTDPModeType);
            }
        }

        /// <summary>
        /// Parses an ParkingInterchangeMode into an TDPModeType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPModeType GetTDPModeType(ParkingInterchangeMode pim)
        {
            switch (pim)
            {
                #region ParkingInterchangeMode mode type
                case ParkingInterchangeMode.Rail:
                    return TDPModeType.TransitRail;
                case ParkingInterchangeMode.Shuttlebus:
                    return TDPModeType.TransitShuttleBus;
                case ParkingInterchangeMode.Cycle:
                    return TDPModeType.Cycle;
                case ParkingInterchangeMode.Metro:
                    return TDPModeType.Metro;
                case ParkingInterchangeMode.Walk:
                    return TDPModeType.Walk;
                #endregion
                default:
                    throw new TDPException(
                        string.Format("Error parsing ParkingInterchangeMode into an TDPModeType, unrecognised value[{0}]", pim.ToString()),
                        false, TDPExceptionIdentifier.JCErrorParsingParkingInterchangeMode);
            }
        }
    }
}
