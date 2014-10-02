// *********************************************** 
// NAME                 : EventReceiverService.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Windows service code. Implements the 
// code required to build application as service.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/EventReceiverService.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:34   mturner
//Initial revision.
//
//   Rev 1.22   Nov 13 2003 21:19:54   geaton
//Change of service name.
//
//   Rev 1.21   Nov 06 2003 19:54:22   geaton
//Removed redundant key.
//
//   Rev 1.20   Oct 30 2003 12:25:36   geaton
//Added validation to ensure that service is configured for publishing ReceivedOperationalEvents.
//
//   Rev 1.19   Oct 13 2003 15:37:36   JTOOR
//Need to set logTextListener = null.
//
//   Rev 1.18   Oct 13 2003 15:33:16   JTOOR
//Tidy up of destrutor code.
//
//   Rev 1.17   Oct 10 2003 15:22:48   geaton
//Updated error handling and validation.
//
//   Rev 1.16   Oct 09 2003 20:04:44   geaton
//Updated trace messages.
//
//   Rev 1.15   Oct 09 2003 15:46:08   geaton
//Removed close on .NET trace listener file from constructor - this was throwing exsception. Moved to Dispose method only.
//
//   Rev 1.14   Oct 09 2003 14:36:20   pscott
//add flush
//
//   Rev 1.13   Oct 09 2003 12:33:42   geaton
//Tidied up error handling and added verbose messages to assist in debugging.
//
//   Rev 1.12   Oct 09 2003 09:34:02   pscott
//.
//
//   Rev 1.11   Oct 08 2003 15:45:02   JTOOR
// 
//
//   Rev 1.10   Oct 08 2003 15:33:12   JTOOR
// 
//
//   Rev 1.9   Oct 08 2003 15:26:14   pscott
//OperationalEvent logging fix.
//
//   Rev 1.8   Oct 08 2003 15:22:06   JTOOR
// 
//
//   Rev 1.7   Oct 08 2003 14:39:56   JTOOR
//Fixed problem with errors not being written to log, because of exception.
//
//   Rev 1.6   Oct 08 2003 14:29:08   JTOOR
//Error logging changes.
//
//   Rev 1.5   Oct 08 2003 14:06:14   pscott
//add traces
//
//   Rev 1.4   Oct 08 2003 12:05:20   JTOOR
//Error handling to write to log file.
//
//   Rev 1.3   Oct 08 2003 09:21:34   JTOOR
//Logging support implemented.
//
//   Rev 1.2   Sep 05 2003 09:49:30   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.1   Aug 29 2003 11:33:42   mturner
//Calls to run() changed to Run()
//
//   Rev 1.0   Aug 22 2003 11:49:36   jtoor
//Initial Revision

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.EventReceiver
{
	/// <summary>
	/// Windows service specific code.
	/// </summary>
	public class EventReceiverService : System.ServiceProcess.ServiceBase
	{
		private EventReceiver			eventReceiver;		
		private const string TestParameter = "/test";
		private bool testMode;
		private bool tdServicesInitialised;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EventReceiverService()
		{
			// Set up service name - this must match the AID used in the properties, and in ProjectInstaller.cs.
			this.ServiceName = "EventReceiver";

			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			eventReceiver = null;
		}

		/// <summary>
		/// The main entry point for the process
		/// </summary>
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new EventReceiverService() };
			System.ServiceProcess.ServiceBase.Run( ServicesToRun );
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{ 
			components = new System.ComponentModel.Container();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
	
		}

		/// <summary>
		/// C# Destructor.
		/// </summary>
		~EventReceiverService()
		{
			Dispose( false );
		}

		/// <summary>
		/// Called when service is started.
		/// </summary>
		/// <param name="args">Arguments passed to service.</param>
		protected override void OnStart(string[] args)
		{
			testMode = false;
			tdServicesInitialised = false;
			eventReceiver = null;

			// Initialise TD Services.
			try
			{
				TDServiceDiscovery.Init(new EventReceiverInitialisation());
				tdServicesInitialised = true;
				AutoLog = false; // Disable default logging (will be using TD Logging Service).
			}
			catch (TDException tdException)
			{
				// Log error to default trace listener in case TD Listener failed to create.
				EventLog.WriteEntry(String.Format(Messages.Init_Failed, tdException.Message), EventLogEntryType.Error, 1);
				AutoLog = false; // Prevents .NET runtime from making entry to say service has started successfully.
			}

			if (tdServicesInitialised)
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Init_Completed));
			}
			
			if ((tdServicesInitialised) && (args.Length == 1))
			{
				
				if (args[0].Equals(TestParameter))
				{
					testMode = true;

					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Service_StartedTestMode));														
				}
				else
				{
					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Service_BadParam));
				}
			}
			
				
			if (tdServicesInitialised)
			{
				if (testMode)
				{
					// do nothing...
				}
				else
				{
					// Start receiving events...
					try
					{
						eventReceiver = new EventReceiver();
						eventReceiver.Run();
					}
					catch (TDException tdException)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailedRunning, tdException.Message)));
					}
					
				}
			}
	
		}
 
		/// <summary>
		/// Stop the service.
		/// </summary>
		protected override void OnStop()
		{
			if (tdServicesInitialised)
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Service_Stopping));
			}

			if (eventReceiver != null)
			{
				eventReceiver.Dispose(true);
				eventReceiver = null;
			}							
		}		
		
	}
}
