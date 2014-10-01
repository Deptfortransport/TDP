// ***********************************************
// NAME 		: DataExport.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 12-Dec-2003
// DESCRIPTION 	: Application to transfer feed
//              : files to remote servers.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayExport/DataExport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:02   mturner
//Initial revision.
//
//   Rev 1.1   Dec 17 2003 17:45:24   TKarsan
//Completed Data Gateway Export.

using System;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using System.Diagnostics;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.DataGatewayExport
{
	/// <summary>
	/// Main console application module.
	/// </summary>
	class DataExport
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			int returnCode = 0;

			Console.WriteLine("Data Gateway Export");

			try
			{
				TDServiceDiscovery.Init(new DataExportInitialisation());
				returnCode = run(args);
			}
			catch (TDException tdEx)
			{
				if(!tdEx.Logged)
					Console.WriteLine(tdEx.ToString() + " has occured on initialisation.");
				returnCode = (int) tdEx.Identifier;
			}
		
			return returnCode;
		}

		/// <summary>
		/// Parses command arguments before calling the controller to perform transfer.
		/// </summary>
		/// <param name="args">Command arguments.</param>
		/// <returns>Whatever the controller returns.</returns>
		public static int run(string[] args)
		{
			bool invalidParameters = true;
			char[] parameterSeperator = {':'};
			string server = null;

			if(args.Length == 0)
			{
				invalidParameters = false;
			}
			else if(args.Length == 1) // Iterate through each of the command line arguments
			{
				foreach(string s in args)
				{
					if(s.StartsWith("/"))
					{
						string[] formattedArg = s.ToLower().Split(parameterSeperator, 2);

						switch(formattedArg[0])
						{
							case "/help":
								DisplayHelp();
								return 0;
							case "/server":
								if(formattedArg.Length == 2)
								{
									server = formattedArg[1];
									invalidParameters = false;
								}
								else
									Console.WriteLine("Must supply targer server ID.");
								break;
							default:
								Console.WriteLine("Cannot understand {0}", s);
								break;
						} // switch
					} // if starts with '/'
				} // for each args
			} // else if args count is one.
			else
			{
				DisplayHelp();
			}

			if(invalidParameters == false) // If okay call controller.
			{
				ExportController controller = new ExportController(server);

				if(server == null)
					Console.WriteLine("Processing all target feeds.");
				else
					Console.WriteLine("Processing target server {0}", server);

				return controller.Run();
			}

			return 1;
		}

		/// <summary>
		/// Displays help command list options as help text.
		/// </summary>
		private static void DisplayHelp()
		{
			Console.WriteLine("td.dataexport [/server:name] | [/help]");
			Console.WriteLine(" name       - Target server name.");
			Console.WriteLine(" /help      - Displays this help message.");
			Console.WriteLine("If /server parameter is not supplied then all");
			Console.WriteLine("servers are considered.");
		}
	}
}
