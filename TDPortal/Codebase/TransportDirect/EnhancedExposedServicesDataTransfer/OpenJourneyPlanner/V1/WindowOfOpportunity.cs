// *********************************************** 
// NAME                 : WindowOfOpportunity.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a window of opportunity
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/WindowOfOpportunity.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:48   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:58   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;
namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a window of opportunity
    /// </summary>
    [Serializable]
    public class WindowOfOpportunity
    {

        private DateTime end;
        private DateTime start;

        public WindowOfOpportunity(){}


        /// <summary>
        /// Read/write property that is the end of the time window.
        /// </summary>
        public DateTime End
        {
            get {  return end; }
            set { end = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the start of the time window.
        /// </summary>
        public DateTime Start
        {
            get {  return start; }
            set { start = value; }    
        }
    }
 
}