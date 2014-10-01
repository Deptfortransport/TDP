// *********************************************** 
// NAME                 : SSEventCategory.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Catergories for events
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/SSEventCategory.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Enumeration containing the categories that can be assicated with
    /// an <c>OperationalEvent</c>.
    /// The first element is given a value of 1 so that a non-default
    /// value appears when publishing to Windows Event Logs.
    /// </summary>
    public enum SSEventCategory : int
    {
        Business = 1,
        Database,
        ThirdParty,
        Infrastructure
    }
}
