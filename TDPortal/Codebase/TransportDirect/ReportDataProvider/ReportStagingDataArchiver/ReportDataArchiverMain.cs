// *********************************************** 
// NAME			: ReportDataArchiverMain.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 24/09/2003 
// DESCRIPTION	: Entry point for Archiver.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportStagingDataArchiver/ReportDataArchiverMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:12   mturner
//Initial revision.
//
//   Rev 1.5   Nov 25 2003 20:04:32   geaton
//Changed name of controller method so consistent with other projects.
//
//   Rev 1.4   Nov 22 2003 17:22:04   geaton
//Added test mode.
//
//   Rev 1.3   Nov 21 2003 12:16:28   geaton
//Added success message on completion.
//
//   Rev 1.2   Nov 19 2003 11:38:02   geaton
//Refactored.

using System;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.ReportStagingDataArchiver
{
	/// <summary>
	/// Implements the application entry point.
	/// </summary>
	public class ReportDataArchiverMain
	{
		/// <summary>
		/// Application entry point.
		/// </summary>
		[STAThread]
		public static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{

				if (args.Length == 1)
				{
					if (String.Compare(args[0], "/help", true ) == 0)
					{
						Console.WriteLine(Messages.Init_Usage);
						returnCode = 0;
					}
					else if (String.Compare( args[0], "/test", true ) == 0)
					{
						TDServiceDiscovery.Init(new ReportDataArchiverInitialisation());

						returnCode = 0;

						if (TDTraceSwitch.TraceInfo)
							Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, Messages.ReportStagingDataArchiver_TestSucceeded));

						Console.WriteLine(Messages.ReportStagingDataArchiver_TestSucceeded);

					}
					else
					{								
						Console.WriteLine(Messages.ReportStagingDataArchiver_InvalidArg);
						returnCode = (int)TDExceptionIdentifier.RDPStagingArchiverInvalidArg;
					}
				}
				else
				{
					TDServiceDiscovery.Init(new ReportDataArchiverInitialisation());
					
					if (TDTraceSwitch.TraceInfo)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.ReportStagingDataArchiver_Started) ));

					returnCode = (int)ReportDataArchiverController.Run();

					if ((TDTraceSwitch.TraceInfo) && (returnCode == 0))
						Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, Messages.ReportStagingDataArchiver_Completed ));
				}
			}
			catch (TDException tdEx)
			{
				// Log error (cannot assume that TD listener has been initialised)
				if (!tdEx.Logged)
				{
					Console.Write(String.Format(Messages.ReportStagingDataArchiver_Failed, tdEx.Message, tdEx.Identifier));
					Trace.Write(String.Format(Messages.ReportStagingDataArchiver_Failed, tdEx.Message, tdEx.Identifier));
				}

				returnCode = (int)tdEx.Identifier;
			}

			return returnCode;	
		}
	}
}
