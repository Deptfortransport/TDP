// *********************************************** 
// NAME			: GradientProfileEventDisplayCategory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 17/01/2009
// DESCRIPTION	: Class which defines the enum of Gradient display types 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfileEventDisplayCategory.cs-arc  $
//
//   Rev 1.0   Jan 19 2009 11:09:46   mmodi
//Initial revision.
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public enum GradientProfileEventDisplayCategory : int
    {
        Data = 1,
        Chart,
        Table
    }
}
