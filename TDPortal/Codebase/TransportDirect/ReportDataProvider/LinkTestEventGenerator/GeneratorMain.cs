// *********************************************** 
// NAME                 : LinkTestEventGenerator.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 25/09/2003 
// DESCRIPTION  : Used to generate log events for
// use in the Report Data Provider link tests.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/LinkTestEventGenerator/GeneratorMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:52   mturner
//Initial revision.
//
//   Rev 1.2   Dec 04 2003 15:58:20   geaton
//Updates to make test data more realistic.
//
//   Rev 1.1   Nov 22 2003 19:59:14   geaton
//Added infinite run option.
//
//   Rev 1.0   Oct 03 2003 09:19:40   geaton
//Initial Revision

using System;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using System.Threading;
using TransportDirect.Common.PropertyService.Properties;
using System.Globalization;

namespace TransportDirect.ReportDataProvider.LinkTestEventGenerator
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class GeneratorMain
	{
		public static bool continual = false;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool ok = true;

			try
			{
				if (args.Length == 1)
				{
					if (String.Compare(args[0], "continual", true ) == 0)
						continual = true;
					else
					{
						Console.WriteLine("Bad argument.");
						ok = false;
					}
				}
				else if (args.Length > 1)
				{
					Console.WriteLine("Bad number of arguments.");
					ok = false;
				}

				if (ok)
				{
					// initialise services, including event logging service
					TDServiceDiscovery.Init(new GeneratorInitialisation());
					
					// determine number of threads
					int numOfThreads = int.Parse(Properties.Current[Keys.NumOfThreads].ToString());
				
					
					for (int i=0; i < numOfThreads; i++)
					{
						Thread aThread = new Thread(new ThreadStart(EventGeneratorThread.Run));
						aThread.IsBackground = false; // ensure that app only exits once all threads are finished.
						aThread.Start();
						// Delay before starting next thread to ensure unique thread properties (based on time) are generated 
						Thread.Sleep(new TimeSpan(0,0,0,1,500));
					}

					Console.WriteLine("Event threads started successfully and generating...");
				}
			}
			catch(TDException tde)
			{
				
				Console.WriteLine("A TDException was caught.");
				Console.WriteLine(tde.Message);
				Console.WriteLine(tde.StackTrace);
				Console.WriteLine("Generation unsuccessful.");
				Console.WriteLine("Hit <enter> to exit.");
				Console.ReadLine();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				Console.WriteLine("Generation unsuccessful.");
				Console.WriteLine("Hit <enter> to exit.");
				Console.ReadLine();
			}
		}
	}
}
