// *********************************************** 
// NAME                 : RequestPlace.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a place in a journey request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/RequestPlace.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:42   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:52   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a place in a journey request
    /// </summary>
    [Serializable]
    public class RequestPlace
    {
        private Coordinate coordinate;
        private string givenName;
        private string locality;
        private RequestStop[] stops;
        private RequestPlaceType type;

        public RequestPlace(){}

        /// <summary>
        /// Read/write property that defines the OS grid reference of the place.
        /// </summary>
        public Coordinate Coordinate
        {
            get {  return coordinate; }
            set { coordinate = value; }    
        }
    
        /// <summary>
        /// Read/write property that defines the name that the user selected for the place. 
        /// The journey results returned will contain this name.
        /// </summary>
        public string GivenName
        {
            get {  return givenName; }
            set { givenName = value; }    
        }

        /// <summary>
        /// Read/write property that is locality (NPTG national gazetteer id) that the place is within. 
        /// Defines which Traveline region handles this stop.
        /// </summary>
        public string Locality
        {
            get {  return locality; }
            set { locality = value; }    
        }

        /// <summary>
        /// Read/write property that defines the stops at the place. There must be at least one.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestStop[] Stops
        {
            get {  return stops; }
            set { stops = value; }    
        }

        /// <summary>
        /// Read/write property that determines the type of RequestPlace.
        /// </summary>
        public RequestPlaceType Type
        {
            get {  return type; }
            set { type = value; }    
        }
    }
 
}