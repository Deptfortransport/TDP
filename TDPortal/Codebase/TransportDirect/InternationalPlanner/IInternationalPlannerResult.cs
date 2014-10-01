// *********************************************** 
// NAME			: IInternationalPlannerResult.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Interface Class which defines the public properties and methods which make up an 
//                International planner result
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/IInternationalPlannerResult.cs-arc  $
//
//   Rev 1.0   Jan 29 2010 12:04:30   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Interface Class which defines the public properties and methods which make up an 
    /// International planner result
    /// </summary>
    public interface IInternationalPlannerResult
    {
        string RequestID { get; set; }
        string SessionID { get; set; }
        
        int MessageID { get; set; }
        string MessageDescription { get; set; }
        InternationalJourney[] InternationalJourneys { get; set; }
    }
}
