// *********************************************** 
// NAME                 : RepeatVisitorType.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 12/05/2008
// DESCRIPTION          : Defines types for Repeat visitor events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RepeatVisitorType.cs-arc  $
//
//   Rev 1.0   May 14 2008 15:34:34   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Enumeration containing classifiers for <c>RepeatVisitorEvent</c>.
    /// </summary>
    public enum RepeatVisitorType : int
    {
        VisitorUnknown = 1,
        VisitorNew,
        VisitorRepeat,
        VisitorRobot
    }
}
