// ****************************************************************
// NAME         : ICycleJourneyPlanRunnerCaller.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 10 Jun 2008
// DESCRIPTION  : An interface for CycleJourneyPlanRunnerCaller class.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/ICycleJourneyPlanRunnerCaller.cs-arc  $
//
//   Rev 1.1   Sep 08 2008 15:48:36   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:38:22   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public interface ICycleJourneyPlanRunnerCaller
    {
        ///<summary>
        ///Invoke the CyclePlannerManager for a new cycle journey request
        ///</summary>
        void InvokeCyclePlannerManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang,
            Guid cyclePlannerRequestId, TDJourneyParameters tdJourneyParameters, TDJourneyParametersCycleConverter converter,
            TDDateTime outwardDateTime, TDDateTime returnDateTime, string polylinesTransformXslt);


        ///<summary>
        ///Invoke the CyclePlannerManager for amendments to a cycle journey
        ///</summary>
        void InvokeCyclePlannerManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cyclePlannerRequestId, int referenceNumber,
            int lastSequenceNumber, ITDCyclePlannerRequest tdCyclePlannerRequest, string polylinesTransformXslt);
    }
}
