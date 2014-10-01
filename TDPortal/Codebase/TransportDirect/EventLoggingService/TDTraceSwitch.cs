// *********************************************** 
// NAME                 : TDTraceSwitch.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Used determine logging level 
// for operational events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/TDTraceSwitch.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:10   mturner
//Initial revision.
//
//   Rev 1.6   Oct 07 2003 13:40:50   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.5   Jul 30 2003 11:55:12   geaton
//Updated comments for properties.
//
//   Rev 1.4   Jul 25 2003 14:14:44   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.3   Jul 24 2003 18:27:56   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	
	/// <summary>
	/// Static class used to determine logging level for Operational Events.
	/// </summary>
	public class TDTraceSwitch
	{

		/// <summary>
		/// Indicates the level of logging applied for the whole application, e.g. error
		/// </summary>
		private static TDTraceLevel currentLevel;

		static TDTraceSwitch()
		{		
			currentLevel = TDTraceLevel.Undefined;

			// Register listener for any changes in level and for initialisation of level
			TDTraceListener.OperationalTraceLevelChange += new TraceLevelChangeEventHandler(TraceLevelChangeEventHandler);
		}

		private static void TraceLevelChangeEventHandler(object sender, TraceLevelEventArgs traceLevelEventArgs)
		{
			currentLevel = traceLevelEventArgs.TraceLevel;
		}
	
		private static bool CheckLevel(TDTraceLevel traceLevel)
		{
			if (currentLevel == TDTraceLevel.Undefined)
			{
				throw new TDException(Messages.TDTraceSwitchNotInitialised, false, TDExceptionIdentifier.ELSTraceLevelUninitialised);
			}
			else
				return (traceLevel <= currentLevel);
		}

		/// <summary>
		/// Gets trace info indicator. <c>true</c> if informational events and above are being traced.
		/// </summary>
		public static bool TraceInfo
		{
			get {return CheckLevel(TDTraceLevel.Info);}
		}

		/// <summary></summary>
		/// Gets trace error indicator. <c>true</c> if error events and above are being traced.
		/// </summary>
		public static bool TraceError
		{
			get {return CheckLevel(TDTraceLevel.Error);}
		}

		/// <summary>
		/// Gets trace warning indicator. <c>true</c> if warning events and above are being traced.
		/// </summary>
		public static bool TraceWarning
		{
			get {return CheckLevel(TDTraceLevel.Warning);}
		}
		
		/// <summary>
		/// Gets the verbose indicator. <c>true</c> if events at any level are being traced.
		/// </summary>
		public static bool TraceVerbose
		{
			get {return CheckLevel(TDTraceLevel.Verbose);}
		}

	}
}
