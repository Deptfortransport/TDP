// *********************************************** 
// NAME         : PublicJourneyContinuousDetail.cs
// AUTHOR       : C.M. Owczarek 
// DATE CREATED : 19/02/2004 
// DESCRIPTION  : A class representing the detail of a
// continuous journey leg, e.g. walking
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourneyContinuousDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:56   mturner
//Initial revision.
//
//   Rev 1.1   Mar 16 2004 17:37:42   PNorell
//Updated for timings calculation.
//
//   Rev 1.0   Feb 19 2004 17:03:22   COwczarek
//Initial revision.

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// A class representing the detail of a continuous journey leg, e.g. walking
    /// </summary>
    [Serializable()]
    public class PublicJourneyContinuousDetail : PublicJourneyDetail
    {

        /// <summary>
        ///  Average duration of leg in minutes returned by CJP
        /// </summary>
        private int typicalDuration;

        /// <summary>
        /// Default constructor - defined to allow XML serialisation.
        /// </summary>
        public PublicJourneyContinuousDetail()
        {
        }
        
        /// <summary>
        /// Takes a CJP leg and creates a new intance of this class populated with
        /// the leg information
        /// </summary>
        /// <param name="leg">A journey leg passed back from the CJP</param>
        /// <param name="publicVia">A journey via location</param>
        public PublicJourneyContinuousDetail(Leg leg, TDLocation publicVia) : base (leg, publicVia)
        {
            ContinuousLeg continuousLeg = leg as ContinuousLeg;
			// typical duration is in minutes
            typicalDuration = continuousLeg.typicalDuration * 60;

        }

        /// <summary>
        /// The average journey duration in minutes.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public override int Duration
        {
            get { return typicalDuration; }
            set { typicalDuration = value; }
        }
        
    }
}
