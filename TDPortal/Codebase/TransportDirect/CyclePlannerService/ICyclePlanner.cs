// ***********************************************
// NAME 		: ICyclePlanner.cs
// AUTHOR 		: Mitesh Modi
// DATE CREATED : 10/06/2008
// DESCRIPTION 	: This interface specifies methods for performing cycle planning asynchronously
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerService/ICyclePlanner.cs-arc  $
//
//   Rev 1.3   Aug 06 2008 14:50:14   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 01 2008 16:46:56   mmodi
//Updated to use actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:40:18   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 16:20:44   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

namespace TransportDirect.UserPortal.CyclePlannerService
{
    public interface ICyclePlanner
    {
        /// <summary>
        /// Method that returns a Cycle Journey for the supplied parameters
        /// </summary>
        CyclePlannerResult CycleJourneyPlan(CyclePlannerRequest cycleJourneyRequest);

        /// <summary>
        /// Method that returns a Gradient Profile for the supplied parameters
        /// </summary>
        GradientProfileResult GradientProfiler(GradientProfileRequest gradientProfileRequest);
    }
}
