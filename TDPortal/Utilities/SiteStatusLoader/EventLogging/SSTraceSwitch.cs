// *********************************************** 
// NAME                 : SSTraceSwitch.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Static class used to determine the level of Operational Event logging
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/SSTraceSwitch.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:20   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

using AO.Common;

namespace AO.EventLogging
{
    /// <summary>
    /// Static class used to determine logging level for Operational Events.
    /// </summary>
    public class SSTraceSwitch
    {

        /// <summary>
        /// Indicates the level of logging applied for the whole application, e.g. error
        /// </summary>
        private static SSTraceLevel currentLevel;

        static SSTraceSwitch()
        {
            currentLevel = SSTraceLevel.Undefined;

            // Register listener for any changes in level and for initialisation of level
            SSTraceListener.OperationalTraceLevelChange += new TraceLevelChangeEventHandler(TraceLevelChangeEventHandler);
        }

        private static void TraceLevelChangeEventHandler(object sender, TraceLevelEventArgs traceLevelEventArgs)
        {
            currentLevel = traceLevelEventArgs.TraceLevel;
        }

        private static bool CheckLevel(SSTraceLevel traceLevel)
        {
            if (currentLevel == SSTraceLevel.Undefined)
            {
                throw new SSException(Messages.SSTTraceSwitchNotInitialised, false, SSExceptionIdentifier.ELSTraceLevelUninitialised);
            }
            else
                return (traceLevel <= currentLevel);
        }

        /// <summary>
        /// Gets trace info indicator. <c>true</c> if informational events and above are being traced.
        /// </summary>
        public static bool TraceInfo
        {
            get { return CheckLevel(SSTraceLevel.Info); }
        }

        /// <summary></summary>
        /// Gets trace error indicator. <c>true</c> if error events and above are being traced.
        /// </summary>
        public static bool TraceError
        {
            get { return CheckLevel(SSTraceLevel.Error); }
        }

        /// <summary>
        /// Gets trace warning indicator. <c>true</c> if warning events and above are being traced.
        /// </summary>
        public static bool TraceWarning
        {
            get { return CheckLevel(SSTraceLevel.Warning); }
        }

        /// <summary>
        /// Gets the verbose indicator. <c>true</c> if events at any level are being traced.
        /// </summary>
        public static bool TraceVerbose
        {
            get { return CheckLevel(SSTraceLevel.Verbose); }
        }

    }
}
