// *********************************************** 
// NAME                 : SSTraceLevel.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Trace level for logging
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/SSTraceLevel.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:20   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Enumeration that defines the levels that may be associated 
    /// with an <c>OperationalEvent</c>.
    /// </summary>
    public enum SSTraceLevel : int
    {
        Undefined,
        Off,
        Error,
        Warning,
        Info,
        Verbose
    }
}
