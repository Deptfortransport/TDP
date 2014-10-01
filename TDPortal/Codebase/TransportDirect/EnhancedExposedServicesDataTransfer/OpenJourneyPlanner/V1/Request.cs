// *********************************************** 
// NAME                 : Request.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a planning request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Request.cs-arc  $
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
    /// This class represents a planning request
    /// </summary>
    [Serializable]
    public class Request
    {

        private TransportModes modeFilter;
        private Operators operatorFilter;
        private ServiceFilter serviceFilter;
        private bool depart;
        private RequestPlace destination;   
        private RequestPlace origin;
        private PublicParameters publicParameters;

        public Request(){}

        /// <summary>
        /// Read/write property that is used to control the modes of transport that are allowed in the 
        /// resulting journey. If the mode filter is not defined then all modes will be allowed.
        /// </summary>
        public TransportModes ModeFilter
        {
            get {  return modeFilter; }
            set { modeFilter = value; }    
        }
    
        /// <summary>
        /// Read/write property that is used to control which operators are allowed in the results. 
        /// </summary>
        public Operators OperatorFilter
        {
            get {  return operatorFilter; }
            set { operatorFilter = value; }    
        }

        /// <summary>
        /// Read/write property that is used to control which services are allowed in the results. 
        /// </summary>
        public ServiceFilter ServiceFilter
        {
            get {  return serviceFilter; }
            set { serviceFilter = value; }    
        }

        /// <summary>
        /// Read/write property that specifies whether the journey should arrive on or leave on the 
        /// specified time.
        /// If true, journeys will be planned to leave on or after the specified time. 
        /// If false, journeys will be planned to arrive on or before the specified time. 
        /// </summary>
        public bool Depart
        {
            get {  return depart; }
            set { depart = value; }    
        }
    
        /// <summary>
        /// Read/write property that is journey plan destination.
        /// </summary>
        public RequestPlace Destination
        {
            get {  return destination; }
            set { destination = value; }    
        }

        /// <summary>
        /// Read/write property that is journey plan origin.
        /// </summary>
        public RequestPlace Origin
        {
            get {  return origin; }
            set { origin = value; }    
        }

        /// <summary>
        /// Read/write property that specifies public journey parameters.
        /// </summary>
        public PublicParameters PublicParameters
        {
            get {  return publicParameters; }
            set { publicParameters = value; }    
        }

    }

}
 
