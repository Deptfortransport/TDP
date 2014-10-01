// *********************************************** 
// NAME                 : Operators.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents an operator code filter
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Operators.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:40   mturner
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
    /// This class represents an operator code filter
    /// </summary>
    [Serializable]
    public class Operators
    {
        private bool include;
        private OperatorCode[] operatorCodes;

        public Operators(){}

        /// <summary>
        /// Read/write property that if true, only operators specified will be allowed. 
        /// If false, all operators except those specified will be allowed.
        /// </summary>
        public bool Include
        {
            get {  return include; }
            set { include = value; }    
        }

        /// <summary>
        /// Read/write property that defines included or excluded operators.
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public OperatorCode[] OperatorCodes
        {
            get {  return operatorCodes; }
            set { operatorCodes = value; }    
        }
    }
 
}