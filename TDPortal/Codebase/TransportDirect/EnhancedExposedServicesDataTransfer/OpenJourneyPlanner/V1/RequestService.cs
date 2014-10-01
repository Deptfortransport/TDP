// *********************************************** 
// NAME                 : RequestService.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a service in a request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/RequestService.cs-arc  $
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
    /// This class represents a service in a request
    /// </summary>
    [Serializable]
    public class RequestService
    {

        private string operatorCode;
        private string serviceNumber;
        private string privateID;
        private RequestServiceType type;

        public RequestService(){}

        /// <summary>
        /// Read/write property that is the code of the operator.
        /// </summary>
        public string OperatorCode
        {
            get {  return operatorCode; }
            set { operatorCode = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the number of the service or a flight number. 
        /// Only valid if RequestServiceType is RequestServiceNumber.
        /// </summary>
        public string ServiceNumber
        {
            get {  return serviceNumber; }
            set { serviceNumber = value; }    
        }

        /// <summary>
        /// Read/write property that is the train UID. 
        /// Only valid if RequestServiceType is RequestServicePrivate.
        /// </summary>
        public string PrivateID
        {
            get {  return privateID; }
            set { privateID = value; }    
        }

        /// <summary>
        /// Read/write property that determines the type of RequestService.
        /// </summary>
        public RequestServiceType Type
        {
            get {  return type; }
            set { type = value; }    
        }

    }
 
}
