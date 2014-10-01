// *********************************************** 
// NAME                 : Result.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a planning result
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Result.cs-arc  $
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
    /// This class represents a planning result
    /// </summary>
    [Serializable]
    public class Result 
    {

        private Message[] messages;
        private PublicJourney[] publicJourneys;

        public Result(){}

        /// <summary>
        /// Read/write property that indicates if an error condition has arisen during journey planning.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Message[] Messages
        {
            get {  return messages; }
            set { messages = value; }    
        }

        /// <summary>
        /// Read/write property that specifies the journey results.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public PublicJourney[] PublicJourneys
        {
            get {  return publicJourneys; }
            set { publicJourneys = value; }    
        }

    }
 
}