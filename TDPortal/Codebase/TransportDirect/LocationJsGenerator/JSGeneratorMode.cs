// ************************************************ 
// NAME                 : JSGeneratorMode.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : JSGeneratorMode enumeration to allow js to be created for a target application mode
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationJsGenerator/JSGeneratorMode.cs-arc  $
//
//   Rev 1.0   Aug 28 2012 10:35:34   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// JSGeneratorMode enumeration to allow js to be created for a target application mode
    /// </summary>
    public enum JSGeneratorMode
    {
        TDPDefault,
        TDPNoGroups,
        TDPNoGroupsNoLocalitiesNoPOIs,
        TDPNoLocalities,
        TDPNoLocalitiesNoPOIs,
    }
}
