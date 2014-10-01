// *********************************************** 
// NAME                 : OperationalEventFilter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Filter class used to determine whether an OperationalEvent should be logged.
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/OperationalEventFilter.cs-arc  $
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
	/// Filter class used to determine whether an OperationalEvent should be logged.
	/// </summary>
    public class OperationalEventFilter : IEventFilter
    {
        private SSTraceLevelOverride overrideLevel;

        /// <summary>
        /// Default constructor
        /// </summary>
        public OperationalEventFilter() : this(SSTraceLevelOverride.None)
        {
        }

        /// <summary>
        /// Constructor allowing an override level to be specified
        /// </summary>
        /// <param name="overrideLevel"></param>
        public OperationalEventFilter(SSTraceLevelOverride overrideLevel)
        {
            this.overrideLevel = overrideLevel;
        }

        /// <summary>
        /// Determines whether the given <c>LogEvent</c> should be published.
        /// </summary>
        /// <param name="logEvent">The <c>LogEvent</c> to test for.</param>
        /// <returns>Returns <c>true</c> if the <c>LogEvent</c> should be logged, otherwise <c>false</c>. Always returns <c>false</c> if the log event passed is not an <c>OperationalEvent</c>.</returns>
        public bool ShouldLog(LogEvent logEvent)
        {
            if (logEvent is OperationalEvent)
            {
                OperationalEvent oe = (OperationalEvent)logEvent;

                if (SSTraceSwitch.TraceVerbose)
                    return (CheckLevel(SSTraceLevel.Verbose, oe.Level));
                else if (SSTraceSwitch.TraceInfo)
                    return (CheckLevel(SSTraceLevel.Info, oe.Level));
                else if (SSTraceSwitch.TraceWarning)
                    return (CheckLevel(SSTraceLevel.Warning, oe.Level));
                else if (SSTraceSwitch.TraceError)
                    return (CheckLevel(SSTraceLevel.Error, oe.Level));
                else
                    return false; // signifies all levels are off
            }

            return false;
        }

        private static bool CheckLevel(SSTraceLevel traceLevel, SSTraceLevel eventLevel)
        {
            return (eventLevel <= traceLevel);
        }
    }
}
