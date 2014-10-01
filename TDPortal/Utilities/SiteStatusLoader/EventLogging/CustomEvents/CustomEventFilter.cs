// *********************************************** 
// NAME                 : CustomEventFilter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class used to determine whether CustomEvents should be published.
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/CustomEvents/CustomEventFilter.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:14   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Class used to determine whether CustomEvents should be published.
    /// </summary>
    public class CustomEventFilter : IEventFilter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomEventFilter()
        {
        }

        /// <summary>
        /// Determines whether the given <c>LogEvent</c> should be published.
        /// </summary>
        /// <param name="logEvent">The <c>LogEvent</c> to test for.</param>
        /// <returns>Returns <c>true</c> if the <c>LogEvent</c> should be logged, otherwise <c>false</c>. Always returns <c>false</c> if the log event passed is not a <c>CustomEvent</c>.</returns>
        public bool ShouldLog(LogEvent logEvent)
        {
            CustomEvent ce = (CustomEvent)logEvent;

            if (CustomEventSwitch.On(ce.ClassName))
                return true;
            else
                return false;
        }
    }
}
