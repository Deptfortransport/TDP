// *********************************************** 
// NAME                 : SSTraceLevelOverride.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Trave level override (User for custom events)
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/SSTraceLevelOverride.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:20   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    public enum SSTraceLevelOverride : int
    {
        None,
        User
    }
}
