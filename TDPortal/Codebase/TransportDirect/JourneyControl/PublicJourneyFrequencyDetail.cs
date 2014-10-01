// *********************************************** 
// NAME         : PublicJourneyFrequencyDetail.cs
// AUTHOR       : C.M. Owczarek 
// DATE CREATED : 19/02/2004 
// DESCRIPTION  : A class representing the detail of a
// frequency based journey leg, e.g. tube, bus
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourneyFrequencyDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:56   mturner
//Initial revision.
//
//   Rev 1.0   Feb 19 2004 17:03:34   COwczarek
//Initial revision.

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// A class representing the detail of a
    /// frequency based journey leg, e.g. tube, bus
    /// </summary>
    [Serializable()]
    public class PublicJourneyFrequencyDetail : PublicJourneyDetail
    {

        /// <summary>
        /// Transport frequency per minute returned by CJP
        /// </summary>
        private int frequency;
        /// <summary>
        /// Minimum transport frequency per minute returned by CJP
        /// </summary>
        private int minFrequency;
        /// <summary>
        /// Minimum transport frequency per minute returned by CJP
        /// </summary>
        private int maxFrequency;
        /// <summary>
        /// Average duration of leg in minutes returned by CJP
        /// </summary>
        private int typicalDuration; 
        /// <summary>
        /// Max duration of leg in minutes returned by CJP
        /// </summary>
        private int maxDuration;

        /// <summary>
        /// Default constructor - defined to allow XML serialisation.
        /// </summary>
        public PublicJourneyFrequencyDetail()
        {
        }
        
        /// <summary>
        /// Takes a CJP leg and creates a new intance of this class populated with
        /// the leg information
        /// </summary>
        /// <param name="leg">A journey leg passed back from the CJP</param>
        /// <param name="publicVia">A journey via location</param>
        public PublicJourneyFrequencyDetail(Leg leg, TDLocation publicVia) : base (leg, publicVia)
        {
            FrequencyLeg frequencyLeg = leg as FrequencyLeg;
            frequency = frequencyLeg.frequency;
            minFrequency = frequencyLeg.minFrequency;
            maxFrequency = frequencyLeg.maxFrequency;
            typicalDuration = frequencyLeg.typicalDuration;
            maxDuration = frequencyLeg.maxDuration;
        }

        /// <summary>
        /// Frequency in minutes.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        
        /// <summary>
        /// Minimum frequency in minutes.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public int MinFrequency
        {
            get { return minFrequency; }
            set { minFrequency = value; }
        }

        /// <summary>
        /// Maximum frequency in minutes.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public int MaxFrequency
        {
            get { return maxFrequency; }
            set { maxFrequency = value; }
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
        
        /// <summary>
        /// The maximum journey duration in minutes.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public int MaxDuration
        {
            get { return maxDuration; }
            set { maxDuration = value; }
        }

    }
}