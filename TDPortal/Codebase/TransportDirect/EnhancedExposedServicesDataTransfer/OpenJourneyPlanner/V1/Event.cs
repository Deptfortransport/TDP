// *********************************************** 
// NAME                 : Event.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a journey event
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Event.cs-arc  $
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
    /// This class represents a journey event
    /// </summary>
    [Serializable]
    public class Event
    {

        public Event(){}

        private ActivityType activity;
        private bool arriveTimeSpecified;
        private DateTime arriveTime;
        private bool confirmedVia;
        private bool departTimeSpecified;
        private DateTime departTime;
        private Coordinate[] geometry;
        private Stop stop;

        /// <summary>
        /// Read/write property that is This indicates if this stop has an arrive time, departure time or both.
        /// If Activity is ArriveDepart or Request then both ArriveTime and DepartTime will be present.
        /// If Activity is Arrive, only ArriveTime will be present.If Activity is Depart or Pass, only 
        /// DepartTime will be present.
        /// If the Activity is Frequency, neither ArriveTime or DepartTime will be present.
        /// </summary>
        public ActivityType Activity
        {
            get {  return activity; }
            set { activity = value; }    
        }

        /// <summary>
        /// Read/write property that is used by .NET framework to indicate if ArriveTime property 
        /// has been set.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ArriveTimeSpecified
        {
            get {  return arriveTimeSpecified; }
            set { arriveTimeSpecified = value; }    
        }

        /// <summary>
        /// Read/write property that is the arrival time at the stop.
        /// </summary>
        public System.DateTime ArriveTime
        {
            get {  return arriveTime; }
            set { arriveTime = value; }    
        }
    
        /// <summary>
        /// Read/write property that is true if this is one of the requested via places, false otherwise.
        /// </summary>
        public bool ConfirmedVia
        {
            get {  return confirmedVia; }
            set { confirmedVia = value; }    
        }
    
        /// <summary>
        /// Read/write property that is used by .NET framework to indicate if DepartTime property 
        /// has been set.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DepartTimeSpecified
        {
            get {  return departTimeSpecified; }
            set { departTimeSpecified = value; }    
        }

        /// <summary>
        /// Read/write property that is the departure time at the stop.
        /// </summary>
        public System.DateTime DepartTime
        {
            get {  return departTime; }
            set { departTime = value; }    
        }
    

        /// <summary>
        /// Read/write property that is a list of co-ordinates to define where the vehicle travels from 
        /// this event’s stop to the next event. May not be returned by some Traveline regions.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Coordinate[] Geometry
        {
            get {  return geometry; }
            set { geometry = value; }    
        }

        /// <summary>
        /// Read/write property that is the NaPTAN stop id, the name of the stop and its co-ordinate.
        /// </summary>
        public Stop Stop
        {
            get {  return stop; }
            set { stop = value; }    
        }
    }
}