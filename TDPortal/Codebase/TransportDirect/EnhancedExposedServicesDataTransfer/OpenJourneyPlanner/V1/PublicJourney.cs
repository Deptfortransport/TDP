// *********************************************** 
// NAME                 : PublicJourney.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents the public journey planning results
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/PublicJourney.cs-arc  $
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
    /// This class represents the public journey planning results
    /// </summary>
    [Serializable]
    public class PublicJourney
    {

        private Leg[] legs;

        public PublicJourney(){}

        /// <summary>
        /// Read/write property that is the journey legs that constitute the public journey.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Leg[] Legs
        {
            get {  return legs; }
            set { legs = value; }    
        }

    }
 
}