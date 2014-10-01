// *********************************************** 
// NAME			: IInternationalPlanner.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Interface class which defines the public methods for the international planner
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/IInternationalPlanner.cs-arc  $
//
//   Rev 1.0   Jan 29 2010 12:04:28   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Interface class which defines the public methods for the international planner
    /// </summary>
    public interface IInternationalPlanner
    {
        /// <summary>
        /// Method that returns an International Journey for the supplied request parameters
        /// </summary>
        IInternationalPlannerResult InternationalJourneyPlan(IInternationalPlannerRequest internationalJourneyRequest);
    }
}
