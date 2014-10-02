// ******************************************************************** 
// NAME                 : WebLogReaderMain.cs
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Entry point for the Web Log Reader Console app
// ********************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/WebLogReaderMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:58   mturner
//Initial revision.
//
//   Rev 1.7   Nov 21 2003 12:32:20   geaton
//Added success message.
//
//   Rev 1.6   Nov 17 2003 20:15:54   geaton
//Refactored.
//
//   Rev 1.5   Oct 09 2003 10:42:46   ALole
//Updated WebLogReaderMain not to give any textual output on completion/failure.
//Updated W3CWebLogReader to correctly handle non GMT time on local machine.
//
//   Rev 1.4   Sep 05 2003 12:13:46   ALole
//Changed the application name to td.weblogreader.exe
//Implemented code review changes
//Added support for not recording files under a certain size
//Only files automatically processed now are 'pages' i.e. asp, aspx, htm, html
//
//   Rev 1.3   Aug 29 2003 11:33:44   mturner
//Calls to run() changed to Run()
//
//   Rev 1.2   Aug 28 2003 13:35:28   ALole
//Initial Cut

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.ReportDataProvider.WebLogReader
{
	/// <summary>
	/// Web Log Reader class.
	/// </summary>
	class WebLogReaderMain
	{

		/// <summary>
		/// Runs the web log reader.
		/// </summary>
		/// <returns>Return code passed to client.</returns>
		private static int RunReader()
		{
			int returnCode = 0;

			try
			{
				WebLogReaderController controller = new WebLogReaderController(Properties.Current);
				returnCode = controller.Run();
			}
			catch (TDException tdEx)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Reader_Failed, tdEx.Message, tdEx.Identifier)));
				returnCode = (int)tdEx.Identifier;
			}

			return returnCode;
		}

		/// <summary>
		/// The main entry point for the application
		/// </summary>
		/// <returns>
		/// Exit Code: 
		/// Zero signals success.
		/// Greater than zero signals failure.
		/// </returns>
		[STAThread]
		public static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length > 0)
				{
					if (String.Compare(args[0], "/help", true ) == 0)
					{
						Console.WriteLine(Messages.Init_Usage);
						returnCode = 0;
					}
					else if (String.Compare( args[0], "/test", true ) == 0)
					{
						TDServiceDiscovery.Init(new WebLogReaderInitialisation());

						returnCode = 0;

						if (TDTraceSwitch.TraceInfo)
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.Reader_TestSucceeded));

						Console.WriteLine(Messages.Reader_TestSucceeded);

					}
					else
					{
						Console.WriteLine(Messages.Reader_InvalidArg);
						returnCode = (int)TDExceptionIdentifier.RDPWebLogReaderInvalidArg;
					}
				}
				else
				{
					TDServiceDiscovery.Init(new WebLogReaderInitialisation());

					if (TDTraceSwitch.TraceInfo)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.Reader_Started));
	
					returnCode = RunReader();

					if ((TDTraceSwitch.TraceInfo) && (returnCode == 0))
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.Reader_Completed));
				}
			}
			catch (TDException tdEx)
			{
				// Log error (cannot assume that TD listener has been initialised)
				if (!tdEx.Logged)
				{
					Console.Write(String.Format(Messages.Reader_Failed, tdEx.Message, tdEx.Identifier));
					Trace.Write(String.Format(Messages.Reader_Failed, tdEx.Message, tdEx.Identifier));
				}

				returnCode = (int)tdEx.Identifier;
			}

			return returnCode;
		}
	}
}

