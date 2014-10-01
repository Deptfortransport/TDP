// *********************************************** 
// NAME             : OperationalEventFilter.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Filter class used to determine whether an OperationalEvent should be logged.
// ************************************************            


namespace TDP.Common.EventLogging
{
    /// <summary>
    /// Filter class used to determine whether an OperationalEvent should be logged.
    /// </summary>
    public class OperationalEventFilter : IEventFilter
    {
        private TDPTraceLevelOverride overrideLevel;

        /// <summary>
        /// Default constructor. Will set override level to TDPTracelLevelOverride.None
        /// </summary>
        public OperationalEventFilter()
            : this(TDPTraceLevelOverride.None)
        {
        }

        /// <summary>
        /// Constructor allowing an override level to be specified
        /// </summary>
        /// <param name="overrideLevel"></param>
        public OperationalEventFilter(TDPTraceLevelOverride overrideLevel)
        {
            this.overrideLevel = overrideLevel;
        }

        /// <summary>
        /// Checks if the event trace level is less than or equal to the specified trace level
        /// </summary>
        /// <param name="traceLevel">Trace level to compare againse</param>
        /// <param name="eventLevel">Trace level to compare</param>
        /// <returns>true if the event trace level is less than or equal to the specified trace level</returns>
        private static bool CheckLevel(TDPTraceLevel traceLevel, TDPTraceLevel eventLevel)
        {
            return (eventLevel <= traceLevel);
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
                switch (overrideLevel)
                {
                    case TDPTraceLevelOverride.User:
                        return true;
                    default:
                        OperationalEvent oe = (OperationalEvent)logEvent;

                        if (TDPTraceSwitch.TraceVerbose)
                            return (CheckLevel(TDPTraceLevel.Verbose, oe.Level));
                        else if (TDPTraceSwitch.TraceInfo)
                            return (CheckLevel(TDPTraceLevel.Info, oe.Level));
                        else if (TDPTraceSwitch.TraceWarning)
                            return (CheckLevel(TDPTraceLevel.Warning, oe.Level));
                        else if (TDPTraceSwitch.TraceError)
                            return (CheckLevel(TDPTraceLevel.Error, oe.Level));
                        else
                            return false; // signifies all levels are off
                }
            }

            return false;
        }
    }
}
