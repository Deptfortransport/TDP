// *********************************************** 
// NAME                 : RequestStop.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a stop in a request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/RequestStop.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:44   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:54   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a stop in a request
    /// </summary>
    [Serializable]
    public class RequestStop
    {

        private Coordinate coordinate;
        private string naPTANID;
        private DateTime timeDate;

        public RequestStop(){}

        /// <summary>
        /// Read/write property that defines the OS grid reference of the stop.
        /// </summary>
        public Coordinate Coordinate
        {
            get {  return coordinate; }
            set { coordinate = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the national ID for the stop.
        /// </summary>
        public string NaPTANID
        {
            get {  return naPTANID; }
            set { naPTANID = value; }    
        }

        /// <summary>
        /// Read/write property that is the requested departure or arrival time at the stop. 
        /// If this RequestStop is used to describe a via point then this element specifies a stop over 
        /// time which is expressed as a relative time (a time span) and not an absolute time. 
        /// A relative time is expressed by adding the relative time value to the minimum value of 
        /// the datetime type. 
        /// </summary>
        public DateTime TimeDate
        {
            get {  return timeDate; }
            set { timeDate = value; }    
        }
    }
 
}