// *********************************************** 
// NAME         : PublicJourneyFrequencyDetail.cs
// AUTHOR       : C.M. Owczarek 
// DATE CREATED : 19/02/2004 
// DESCRIPTION  : A class representing the detail of a
// timed journey leg, e.g. rail
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourneyTimedDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:56   mturner
//Initial revision.
//
//   Rev 1.1   Aug 20 2004 12:16:00   jgeorge
//Interim check in for IR1354
//
//   Rev 1.0   Feb 19 2004 17:03:36   COwczarek
//Initial revision.

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// A class representing the detail of a timed journey leg, e.g. rail
    /// </summary>
    [Serializable()]
    public class PublicJourneyTimedDetail : PublicJourneyDetail
    {
        
        /// <summary>
        /// Leg duration calculated from leg arrival and departure times
        /// </summary>
        private int duration;
        
        /// <summary>
        /// Default constructor - defined to allow XML serialisation.
        /// </summary>
        public PublicJourneyTimedDetail()
        {
        }
        
        /// <summary>
        /// Takes a CJP leg and creates a new intance of this class populated with
        /// the leg information
        /// </summary>
        /// <param name="leg">A journey leg passed back from the CJP</param>
        /// <param name="publicVia">A journey via location</param>
        public PublicJourneyTimedDetail(Leg leg, TDLocation publicVia, Leg previousLeg, Leg subsequentLeg) : base (leg, publicVia, previousLeg, subsequentLeg)
        {
            duration = (int)((leg.alight.arriveTime.Ticks - leg.board.departTime.Ticks) / TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// Duration of leg in minutes.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public override int Duration {
            get {return duration;}
            set {duration = value;}
        }

    }
}
