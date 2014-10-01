// *********************************************** 
// NAME                 : Leg.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a journey leg
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Leg.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:38   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:20:30   COwczarek
//Initial revision.
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a journey leg
    /// </summary>
    [Serializable]
    public class Leg
    {
        private Event alight;
        private Event board;
        private string description;
        private Event destination;
        private Event[] intermediatesA;
        private Event[] intermediatesB;
        private Event[] intermediatesC;
        private TransportModeType mode;
        private string[] notes;
        private Event origin;
        private Service[] services;
        private bool validated;
        private int[] vehicleFeatures;
        private WindowOfOpportunity windowOfOpportunity;
        private int typicalDuration;
        private int frequency;
        private int maxDuration;
        private int maxFrequency;
        private int minFrequency;
        private LegType type;

        public Leg(){}

        /// <summary>
        /// Read/write property that defines where the vehicle is alighted or where the continuous leg ends.
        /// </summary>
        public Event Alight
        {
            get {  return alight; }
            set { alight = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines where the vehicle is boarded or where the continuous leg starts.
        /// </summary>
        public Event Board
        {
            get {  return board; }
            set { board = value; }    
        }
    
        /// <summary>
        /// Read/write property that is additional information for flight only journeys.
        /// </summary>
        public string Description
        {
            get {  return description; }
            set { description = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines where the vehicle is going. It should only be missing when 
        /// the alight point is the destination, however, some Traveline regions do not currently support it.
        /// </summary>
        public Event Destination
        {
            get {  return destination; }
            set { destination = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines the stops between the Origin and the Board.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Event[] IntermediatesA
        {
            get {  return intermediatesA; }
            set { intermediatesA = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines the stops between the Board and the Alight. 
        /// Note: 
        /// This is also used to show that the requested via point has been passed, so even if 
        /// intermediate stops have not been requested one of these may be returned.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Event[] IntermediatesB
        {
            get {  return intermediatesB; }
            set { intermediatesB = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines the stops between the Alight and the Destination.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Event[] IntermediatesC
        {
            get {  return intermediatesC; }
            set { intermediatesC = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the travel mode of the leg.
        /// </summary>
        public TransportModeType Mode
        {
            get {  return mode; }
            set { mode = value; }    
        }
    
        /// <summary>
        /// Read/write property that contains any additional information about the leg that cannot be 
        /// represented in other fields. E.g. “School holidays only”, “The services must be booked. 
        /// Telephone 0123”. 
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] Notes
        {
            get {  return notes; }
            set { notes = value; }    
        }

        /// <summary>
        /// Read/write property that defines where the vehicle has come from. It should only be missing 
        /// when the board point is the origin, however, some Traveline regions do not currently support it.
        /// </summary>
        public Event Origin
        {
            get {  return origin; }
            set { origin = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines the service details of the vehicle being used. 
        /// This attribute should only be missing for continuous legs.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Service[] Services
        {
            get {  return services; }
            set { services = value; }    
        }
    
        /// <summary>
        /// Read/write property that indicates if this leg has been validated – only relevant for rail 
        /// and coach legs.
        /// </summary>
        public bool Validated
        {
            get {  return validated; }
            set { validated = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines the facilities on the vehicle. Currently this is only available 
        /// for rail services that have been validated. See Appendix A -Vehicle Features.
        /// </summary>
        public int[] VehicleFeatures
        {
            get {  return vehicleFeatures; }
            set { vehicleFeatures = value; }    
        }

        /// <summary>
        /// Read/write property that is defined if LegType is Frequency or Continous. If LegType is Frequency
        /// this is a window within which is should be possible to make the trip. If LegType is Continous 
        /// then this is a window within which the trip must be made in order to meet the next leg.
        /// </summary>
        public WindowOfOpportunity WindowOfOpportunity
        {
            get {  return windowOfOpportunity; }
            set { windowOfOpportunity = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the typical time to travel the leg. Defined if LegType is 
        /// Frequency or Continuous. 
        /// </summary>
        public int TypicalDuration
        {
            get {  return typicalDuration; }
            set { typicalDuration = value; }    
        }

        /// <summary>
        /// Read/write property that is the frequency that the vehicles are scheduled to run at. 
        /// Defined if LegType is Frequency.
        /// </summary>
        public int Frequency
        {
            get {  return frequency; }
            set { frequency = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the maximum time it should take to travel the leg. 
        /// Defined if LegType is Frequency.
        /// </summary>
        public int MaxDuration
        {
            get {  return maxDuration; }
            set { maxDuration = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the maximum time the traveller should have to wait for the 
        /// vehicle (0 if unavailable). Defined if LegType is Frequency.
        /// </summary>
        public int MaxFrequency
        {
            get {  return maxFrequency; }
            set { maxFrequency = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the minimum time the traveller should have to wait for the vehicle
        /// (0 if unavailable). Defined if LegType is Frequency.
        /// </summary>
        public int MinFrequency
        {
            get {  return minFrequency; }
            set { minFrequency = value; }    
        }

        /// <summary>
        /// Read/write property that defines whether the leg is time based, frequency based or continuous.
        /// </summary>
        public LegType Type
        {
            get {  return type; }
            set { type = value; }    
        }

    }
 
}