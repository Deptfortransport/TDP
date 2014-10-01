// *********************************************** 
// NAME			: InternationalJourneyDetailType.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 02/02/2010
// DESCRIPTION	: Class which indicates the type of international journey detail
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalJourneyDetailType.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 09:53:18   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 04 2010 10:27:58   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// International journey detail type
    /// </summary>
    [Serializable()]
    public enum InternationalJourneyDetailType
    {
        TimedAir,
        TimedCoach,
        TimedRail,
        TimedCar,
        Transfer
    }
}
