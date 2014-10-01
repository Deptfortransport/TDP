// *********************************************** 
// NAME                 : ServiceFilter.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a service filter
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/ServiceFilter.cs-arc  $
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
    /// This class represents a service filter
    /// </summary>
    [Serializable]
    public class ServiceFilter
    {
        private bool include;
        private RequestService[] services;

        public ServiceFilter(){}

        /// <summary>
        /// Read/write property that if true, only services specified will be allowed. If false, 
        /// all services except those specified will be allowed.
        /// </summary>
        public bool Include
        {
            get {  return include; }
            set { include = value; }    
        }

        /// <summary>
        /// Read/write property that is the included or excluded services.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestService[] Services
        {
            get {  return services; }
            set { services = value; }    
        }
    }
 
}