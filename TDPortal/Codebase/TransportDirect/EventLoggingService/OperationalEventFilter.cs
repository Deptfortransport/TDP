// *********************************************** 
// NAME                 : OperationalEventFilter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Filter for an operational event.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/OperationalEventFilter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:08   mturner
//Initial revision.
//
//   Rev 1.4   Jan 18 2005 14:59:46   SWillcock
//Fixed switch statement to remove 'unreachable code detected' warnings
//
//   Rev 1.3   Jun 30 2004 17:02:50   jgeorge
//Changes for "force logging" functionality
//
//   Rev 1.2   Jul 24 2003 18:27:48   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Filter class used to determine whether an OperationalEvent should be logged.
	/// </summary>
	public class OperationalEventFilter : IEventFilter
	{
		private TDTraceLevelOverride overrideLevel;

		/// <summary>
		/// Default constructor. Will set override level to TDTracelLevelOverride.None
		/// </summary>
		public OperationalEventFilter() : this(TDTraceLevelOverride.None)
		{
		}

		/// <summary>
		/// Constructor allowing an override level to be specified
		/// </summary>
		/// <param name="overrideLevel"></param>
		public OperationalEventFilter(TDTraceLevelOverride overrideLevel)
		{
			this.overrideLevel = overrideLevel;
		}

		private static bool CheckLevel(TDTraceLevel traceLevel, TDTraceLevel eventLevel)
		{
			return (eventLevel <= traceLevel );
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
					case TDTraceLevelOverride.User:
						return true;
					default:
						OperationalEvent oe = (OperationalEvent)logEvent;

						if (TDTraceSwitch.TraceVerbose)
							return (CheckLevel(TDTraceLevel.Verbose, oe.Level));
						else if (TDTraceSwitch.TraceInfo)
							return (CheckLevel(TDTraceLevel.Info, oe.Level));
						else if (TDTraceSwitch.TraceWarning)
							return (CheckLevel(TDTraceLevel.Warning, oe.Level));
						else if (TDTraceSwitch.TraceError)
							return (CheckLevel(TDTraceLevel.Error, oe.Level));
						else
							return false; // signifies all levels are off
				}
			}

			return false;
		}
	}
}
