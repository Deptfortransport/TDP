// *********************************************** 
// NAME                 : PublicParameters.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents journey planning parameters for a public journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/PublicParameters.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:42   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:50   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents journey planning parameters for a public journey
    /// </summary>
    [Serializable]
    public class PublicParameters
    {
        private PublicAlgorithmType algorithm;
        private DateTime extraCheckInTime;
        private bool extraCheckInTimeSpecified;
        private int interchangeSpeed;
        private IntermediateStopsType intermediateStops;
        private DateTime interval;
        private bool intervalSpecified;
        private int maxWalkDistance;
        private RequestPlace[] notVias;
        private RangeType rangeType;
        private int sequence;
        private bool sequenceSpecified;
        private RequestPlace[] softVias;
        private bool trunkPlan;
        private RequestPlace[] vias;
        private int walkSpeed;

        public PublicParameters(){}

        /// <summary>
        /// Read/write property that specifies the quality of journeys returned.
        /// </summary>
        public PublicAlgorithmType Algorithm
        {
            get {  return algorithm; }
            set { algorithm = value; }    
        }
    
        /// <summary>
        /// Read/write property that is used by .NET framework to indicate if ExtraCheckInTime property 
        /// has been set.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExtraCheckInTimeSpecified
        {
            get {  return extraCheckInTimeSpecified; }
            set { extraCheckInTimeSpecified = value; }    
        }

        /// <summary>
        /// Read/write property that for air trunk planning only, specifies the extra minutes to add to the 
        /// first check in of the journey. The time is not added to subsequent check ins because the 
        /// traveller cannot arrive early for them, they arrive when the previous flight arrives. 
        /// This is a relative time with a maximum time span of 23hrs 59mins. A relative time is expressed 
        /// by adding the relative time value to the minimum value of the datetime type.
        /// </summary>
        public DateTime ExtraCheckInTime
        {
            get {  return extraCheckInTime; }
            set { extraCheckInTime = value; }    
        }

        /// <summary>
        /// Read/write property that specifies how quickly changes can be made between vehicles. 
        /// Range is –3 to +3 (-3 = slowest, 0 = average, +3 = fastest).
        /// </summary>
        public int InterchangeSpeed
        {
            get {  return interchangeSpeed; }
            set { interchangeSpeed = value; }    
        }
    
        /// <summary>
        /// Read/write property that specifies which type of intermediate stops to return. 
        /// Intermediates stops are returned for rail legs only.
        /// </summary>
        public IntermediateStopsType IntermediateStops
        {
            get {  return intermediateStops; }
            set { intermediateStops = value; }    
        }
    
        /// <summary>
        /// Read/write property that specifies the time span (in minutes) within which to return
        /// all journeys. Depending on the value of the Depart property, a journey plan will return either:
        /// - all journeys departing in the next specified interval after the specified departure time.
        /// - all journeys arriving within the next specified interval before the specified arrival time.
        /// This is a relative time with a maximum time span of 23hrs 59mins.
        /// </summary>
        public DateTime Interval
        {
            get {  return interval; }
            set { interval = value; }    
        }

        /// <summary>
        /// Read/write property that is used by .NET framework to indicate if Interval property 
        /// has been set.
        /// </summary>    
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IntervalSpecified
        {
            get {  return intervalSpecified; }
            set { intervalSpecified = value; }    
        }

        /// <summary>
        /// Read/write property that represents the maximum distance the user is prepared to walk in metres
        /// for any single leg of the journey.
        /// </summary>
        public int MaxWalkDistance
        {
            get {  return maxWalkDistance; }
            set { maxWalkDistance = value; }    
        }
    
        /// <summary>
        /// Read/write property that specifies places to exclude from the journey plan.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestPlace[] NotVias
        {
            get {  return notVias; }
            set { notVias = value; }    
        }
    
        /// <summary>
        /// Read/write property that specifies the type of journey plan
        /// </summary>
        public RangeType RangeType
        {
            get {  return rangeType; }
            set { rangeType = value; }    
        }

        /// <summary>
        /// Read/write property that specifies the number of journeys to return either after the specified
        /// departure time or before the specified arrival time (depending on Depart property)
        /// </summary>
        public int Sequence
        {
            get {  return sequence; }
            set { sequence = value; }    
        }
    
        /// <summary>
        /// Read/write property that is used by .NET framework to indicate if Sequence property 
        /// has been set.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SequenceSpecified
        {
            get {  return sequenceSpecified; }
            set { sequenceSpecified = value; }    
        }

        /// <summary>
        /// Read/write property that specifies via places that result in no service change. For example, 
        /// a ticket requires a journey to pass through a certain station.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestPlace[] SoftVias
        {
            get {  return softVias; }
            set { softVias = value; }    
        }
    
        /// <summary>
        /// Read/write property that forces journey planning of a single mode rail, coach or air journey.
        /// If true, no requests will be made of Travelines and internal engines will be used only.
        /// </summary>
        public bool TrunkPlan
        {
            get {  return trunkPlan; }
            set { trunkPlan = value; }    
        }

        /// <summary>
        /// Read/write property that specifies via places that result in service change.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestPlace[] Vias
        {
            get {  return vias; }
            set { vias = value; }    
        }
    
        /// <summary>
        /// Read/write property that specifies walking speed in metres per minute 
        /// (e.g. slow = 40, average = 80, fast = 120).
        /// </summary>
        public int WalkSpeed
        {
            get {  return walkSpeed; }
            set { walkSpeed = value; }    
        }
    }
 
}