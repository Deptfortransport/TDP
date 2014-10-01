// *********************************************** 
// NAME			: InternationalModeType.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which indicates the mode of international journey
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalModeType.cs-arc  $
//
//   Rev 1.2   Feb 09 2010 09:53:18   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:02   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:32   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// International journey mode types
    /// </summary>
    [Serializable()]
    public enum InternationalModeType
    {
        Air = 0,
        Coach,
        Rail,
        Car,
        None
    }
}
