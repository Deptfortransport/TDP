// *********************************************** 
// NAME                 : CustomEventLevel.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Custom event levels used to determing publishing behaviour
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/CustomEvents/CustomEventLevel.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Enumeration containing levels that a <c>CustomEvent</c> may take.
    /// </summary>
    public enum CustomEventLevel : int
    {
        Undefined,
        Off,
        On
    }
}
