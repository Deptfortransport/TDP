// *********************************************** 
// NAME                 : SSAlertLevel.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Alert levels 
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/SSAlertLevel.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:23:42   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.Common
{
    /// <summary>
    /// Enum to define available Alert Levels
    /// </summary>
    public enum AlertLevel
    {
        Unknown,
        Green,
        Amber,
        Red       
    }
}
