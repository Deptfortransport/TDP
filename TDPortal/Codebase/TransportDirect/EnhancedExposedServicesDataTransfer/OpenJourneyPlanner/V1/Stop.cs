// *********************************************** 
// NAME                 : Stop.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a journey stop
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Stop.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:46   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:56   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;
namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a journey stop
    /// </summary>
    [Serializable]
    public class Stop
    {
        private string bay;
        private Coordinate coordinate;
        private string name;
        private string naPTANID;
        private bool timingPoint;
        
        public Stop(){}

        /// <summary>
        /// Read/write property that is the stop or bay number, e.g. the bay of the bus stop within a bus station.
        /// </summary>
        public string Bay
        {
            get {  return bay; }
            set { bay = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the OS grid reference of the stop.
        /// </summary>
        public Coordinate Coordinate
        {
            get {  return coordinate; }
            set { coordinate = value; }    
        }

        /// <summary>
        /// Read/write property that is the name of the stop.
        /// </summary>
        public string Name
        {
            get {  return name; }
            set { name = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the NaPTAN id of the stop.
        /// </summary>
        public string NaPTANID
        {
            get {  return naPTANID; }
            set { naPTANID = value; }    
        }
    
        /// <summary>
        /// Read/write property that if true, the vehicle is timetabled to stop at this stop, or 
        /// false if vehicle will stop but the stopping time is not timetabled.
        /// </summary>
        public bool TimingPoint
        {
            get {  return timingPoint; }
            set { timingPoint = value; }    
        }
    }
 
}