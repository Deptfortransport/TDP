// *********************************************** 
// NAME                 : OperatorCode.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a service operator code
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/OperatorCode.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:40   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:48   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a service operator code
    /// </summary>
    [Serializable]
    public class OperatorCode
    {
        private string code;

        public OperatorCode(){}

        /// <summary>
        /// Read/write property that is the code of the operator.
        /// </summary>
        public string Code
        {
            get {  return code; }
            set { code = value; }    
        }
    }
 
}