// ***********************************************
// NAME 		: ImportMain.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 26/08/2003
// DESCRIPTION 	: Command Line program used to pass parameters to the DataGatewayFramework dll
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayImportMain/ImportMain.cs-arc  $
//
//   Rev 1.2   Mar 25 2013 09:50:52   mmodi
//Added comment line for futuring debugging directions
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.1   Dec 05 2012 14:18:12   mmodi
//Updated to display error message text as part of the return 
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:20:34   mturner
//Initial revision.
//
//   Rev 1.13   May 17 2004 16:51:22   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.12   Jan 12 2004 16:42:00   jmorrissey
//Updated failure return code to 4001instead of -1 in Run method when parameters are invalid
//
//   Rev 1.11   Jan 02 2004 14:30:38   JMorrissey
//Alistair's changes
//
//   Rev 1.10   Nov 26 2003 11:22:50   acaunt
//Removed reference to Initialisation
//
//   Rev 1.9   Nov 17 2003 15:47:44   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.8   Sep 16 2003 14:58:44   MTurner
//Changes after code review
//
//   Rev 1.7   Sep 03 2003 16:53:36   MTurner
//Added Service Initialisation code
//
//   Rev 1.6   Sep 03 2003 11:31:56   MTurner
//Changes after FXCop Scan
//
//   Rev 1.5   Aug 29 2003 10:32:26   mturner
//Changes after FXCop Scan
//
//   Rev 1.4   Aug 27 2003 17:23:26   MTurner
//Added database config
//
//   Rev 1.3   Aug 27 2003 15:20:14   MTurner
//Changed /test from a string to a bool in order to make call to FTPController simpler
//
//   Rev 1.2   Aug 27 2003 11:35:12   MTurner
//Added user help text 
//
//   Rev 1.1   Aug 26 2003 14:20:22   MTurner
//Initial Version

using System;
using System.Globalization;
using System.Collections;

using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using System.Diagnostics;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.DataImport
{
	/// <summary>
	/// Import Main is a command line utility that allows the default settings of the 
	/// DataGateway to be overwritten.  The settings to be changed are passed in as 
	/// command line parameters which are then validated before being used to creat an
	/// instance of the ImportController class 
	/// </summary>
	class ImportMain
	{
		// Flag to indicate whether command line parameters are valid
		bool invalidParameters = false;

		/// <summary>
		/// The main entry point for the application.  An instance of this class is
		/// created. 
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			int statusCode = 0;
			Console.WriteLine("Starting");

            // At attach a debugger, uncomment the following line of code,
            // rebuild and copy to \gateway\bin
            //System.Diagnostics.Debugger.Launch();

			try
			{
				TDServiceDiscovery.Init(new DataGatewayInitialisation());

				ImportMain app = new ImportMain();
				statusCode = app.run(args);
			}
			catch (TDException tdEx)
			{
				if (!tdEx.Logged)
					Console.WriteLine(tdEx.ToString() + " has occured on initialisation.");
				statusCode = (int)tdEx.Identifier;
			}

            string exitMessage = "Exiting with errorcode " + statusCode;
            if (statusCode != 0)
            {
                try
                {
                    exitMessage = string.Format("Exiting with errorcode {0} {1}", statusCode, ((TDExceptionIdentifier)statusCode).ToString());
                }
                catch
                {
                    // Ignore, shouldnt happen because statuscode should be a TDExceptionIdentifier if not 0
                }
            }

			Console.WriteLine(exitMessage);
			return statusCode;
		}

		/// <summary>
		/// The validation of all the command line arguments is carried out here.
		/// If any problems are found with the arguments a help message is displayed
		/// on screen and the application exits.
		/// If all arguments are valid an instance of the ImportController class is created
		/// and instantiated with the validated parameters. 
		/// </summary>
		/// <param name="args">A string[] containing the command line arguments</param>
		public int run(string[] args)
		{
			string dataFeed = string.Empty;
			string ipAddress = string.Empty;
			string params1 = string.Empty;
			string params2 = string.Empty;
			bool isTest = false;
			bool isNoTransfer = false;
			char[] stringSeperator = {':'};
			int returnCode = 0;

			if(args.Length < 1 || args.Length > 7)
			{
				invalidParameters = true;
				Console.WriteLine("Error. Invalid number of parameters provided: "+args.Length);
			}
			else
			{
				// Iterate through each of the command line arguments
				foreach(string s in args)
				{
					Console.WriteLine("Parameter: ["+s+"]");
					if(invalidParameters == false)
					{
						if(s.StartsWith("/"))
						{
							s.ToLower();
							string[] formattedArg = s.Split(stringSeperator,2);
							switch(formattedArg[0])
							{
								case "/help":
									displayHelp();
									break;

								case "/test":
									isTest = true;
									break;

								case "/ipaddress":
									if(formattedArg.Length == 2)
									{
										ipAddress = formattedArg[1];
									}
									else
									{
										Console.WriteLine("Error. Unable to split directive properly. Was split into "+formattedArg.Length+" parts "); 
										invalidParameters = true;
									}
									break;

								case "/params1":
									if(formattedArg.Length == 2)
									{
										params1 = formattedArg[1];
									}
									else
									{
										Console.WriteLine("Error. Unable to split directive properly. Was split into "+formattedArg.Length+" parts "); 
										invalidParameters = true;
									}
									break;

								case "/params2":
									if(formattedArg.Length == 2)
									{
										params2 = formattedArg[1];
									}
									else
									{
										Console.WriteLine("Error. Unable to split directive properly. Was split into "+formattedArg.Length+" parts "); 
										invalidParameters = true;
									}
									break;

								case "/notransfer":
									isNoTransfer = true;
									break;

								default:
									Console.WriteLine("Error. Unrecognised directive. "+formattedArg[0]); 
									invalidParameters = true;
									break;
							}
						}
						else
						{
							if(s.Equals(args[0]))
							{
								dataFeed = s;
							}
							else
							{
								Console.WriteLine("Error. Feedname passed in wrong location. It can be the first parameter only.");
								invalidParameters = true;
							}
						}
					}
				}
			}
			if(invalidParameters == false)
			{
				// All parameters have been validated so are now passed to a new ImportController 
				ImportController iController = new ImportController();
				returnCode = iController.Import(dataFeed, isTest, params1, params2, ipAddress, isNoTransfer);
			}
			else
			{
				returnCode = (int)TDExceptionIdentifier.DGInvalidImportArguments;
			}
			return returnCode;
		}

		private void displayHelp()
		{
			// The text below is displayed to the user showing how a valid call can be constructed
			Console.WriteLine("td.dataimport [data feed name] [/help] [/test] [/ipaddress:{alternative IP address}] [/params1:{new params}] [/params2:{new params}] [/notransfer]\n\n");
			Console.WriteLine("data feed name                      - The name of the data feed");
			Console.WriteLine("/help                               - Displays this help text and performs no further activity");
			Console.WriteLine("/test                               - Tests to see if the data feed specified is supported");
			Console.WriteLine("/ipaddress:{alternative IP address} - IP address of alternative server if the primary is unavailable");
			Console.WriteLine("/params1:{new params}               - Parameters to pass to the import utility before the file name");
			Console.WriteLine("/params2:{new params}               - Parameters to pass to the import utility after the file name");
			Console.WriteLine("/notransfer                         - Bypasses the file transfer process");
		}
	}
}
