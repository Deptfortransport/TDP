// *********************************************** 
// NAME                 : PageGroup.cs 
// AUTHOR               : Miteh Modi
// DATE CREATED         : 25/04/2008 
// DESCRIPTION          : Enumeration that holds all the page groups that exist.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/PageGroup.cs-arc  $ 
//
//   Rev 1.0   May 01 2008 17:11:00   mmodi
//Initial revision.
//

using System;

namespace TransportDirect.Common
{
    /// <summary>
    /// Enumeration of all existing page groups
    /// </summary>
    public enum PageGroup
    {
        None,
        Static,
        StaticInput,
        JourneyInput,
        Result
    }
}
