// *********************************************** 
// NAME             : TDPTraceSwitch.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Static class used to determine logging level for Operational Events
// ************************************************



namespace TDP.Common.EventLogging
{
    /// <summary>
    /// Static class used to determine logging level for Operational Events.
    /// </summary>
    public class TDPTraceSwitch
    {
        #region Private Fields
        /// <summary>
        /// Indicates the level of logging applied for the whole application, e.g. error
        /// </summary>
        private static TDPTraceLevel currentLevel;
        #endregion

        #region Constructors
        /// <summary>
        /// Static constructor
        /// </summary>
        static TDPTraceSwitch()
        {
            currentLevel = TDPTraceLevel.Undefined;

            // Register listener for any changes in level and for initialisation of level
            TDPTraceListener.OperationalTraceLevelChange += new TraceLevelChangeEventHandler(TraceLevelChangeEventHandler);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Hanlder for OperationalTraceLevelChange event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="traceLevelEventArgs">TraceLevelEventArgs object</param>
        private static void TraceLevelChangeEventHandler(object sender, TraceLevelEventArgs traceLevelEventArgs)
        {
            currentLevel = traceLevelEventArgs.TraceLevel;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Checks if the supplied trace level is less than or equal to current trace level
        /// </summary>
        /// <param name="traceLevel">Trace level</param>
        /// <returns>True if the supplied trace level is less than or equal to current level otherwise false</returns>
        private static bool CheckLevel(TDPTraceLevel traceLevel)
        {
            if (currentLevel == TDPTraceLevel.Undefined)
            {
                throw new TDPException(Messages.TDPTraceSwitchNotInitialised, false, TDPExceptionIdentifier.ELSTraceLevelUninitialised);
            }
            else
                return (traceLevel <= currentLevel);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets trace info indicator. <c>true</c> if informational events and above are being traced.
        /// </summary>
        public static bool TraceInfo
        {
            get { return CheckLevel(TDPTraceLevel.Info); }
        }

        /// <summary></summary>
        /// Gets trace error indicator. <c>true</c> if error events and above are being traced.
        /// </summary>
        public static bool TraceError
        {
            get { return CheckLevel(TDPTraceLevel.Error); }
        }

        /// <summary>
        /// Gets trace warning indicator. <c>true</c> if warning events and above are being traced.
        /// </summary>
        public static bool TraceWarning
        {
            get { return CheckLevel(TDPTraceLevel.Warning); }
        }

        /// <summary>
        /// Gets the verbose indicator. <c>true</c> if events at any level are being traced.
        /// </summary>
        public static bool TraceVerbose
        {
            get { return CheckLevel(TDPTraceLevel.Verbose); }
        }
        #endregion

    }
}
