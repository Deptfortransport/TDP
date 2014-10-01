// *********************************************** 
// NAME                 : TransportMode.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a mode of transport
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/TransportMode.cs-arc  $
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
    /// This class represents a mode of transport
    /// </summary>
    [Serializable]
    public class TransportMode
    {

        private TransportModeType mode;

        public TransportMode(){}

        /// <summary>
        /// Read/write property that is the travel mode
        /// </summary>
        public TransportModeType Mode
        {
            get {  return mode; }
            set { mode = value; }    
        }
    }
 
}