// ***********************************************
// NAME 		:FtpMain.cs
// AUTHOR 		: Phil Scott
// DATE CREATED : 26/08/2003
// DESCRIPTION 	: Command Line program used to pass parameters to the DataGatewayFramework dll
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFTPMain/FTPMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:28   mturner
//Initial revision.
//
//   Rev 1.11   Jan 12 2004 11:55:38   jmorrissey
//Updated error codes and logged messages
//
//   Rev 1.10   Nov 26 2003 11:19:22   acaunt
//Removed reference to Initialisation
//
//   Rev 1.9   Nov 17 2003 15:47:40   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.8   Nov 03 2003 17:48:52   JMorrissey
//Added check for return code of 2 when calling FtpController. This code means, "Ftp transfer has failed - No files found to transfer and failure threshold has been reached. Raise ticket."
//
//   Rev 1.7   Sep 17 2003 08:20:18   pscott
//return error code to system
//
//   Rev 1.6   Sep 10 2003 09:20:56   PScott
//Code Review changes
//
//   Rev 1.5   Sep 03 2003 16:35:56   PScott
//work in progress
//
//   Rev 1.4   Aug 29 2003 11:33:40   mturner
//Calls to run() changed to Run()
//
//   Rev 1.3   Aug 29 2003 09:53:32   PScott
//work in progress
//
//   Rev 1.2   Aug 27 2003 16:27:24   PScott
//Work in progress
//
//   Rev 1.1   Aug 27 2003 15:31:52   PScott
//work in progress
//
//   Rev 1.0   Aug 26 2003 16:01:56   pscott
//Initial Revision
//
//   Rev 1.1   Aug 26 2003 14:20:22   PScott
//Initial Version

using System;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using System.Diagnostics;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.FTPImport
{
	/// <summary>
	/// Command line utility for call of FTP controller class.
	/// </summary>
	class FtpMain
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		
		static int Main(string[] args)
		{
			int statusCode = 0;

			try
			{
				TDServiceDiscovery.Init(new DataGatewayFTPInitialisation());

				FtpMain app = new FtpMain();
				statusCode = app.RunApp(args);
			}
			catch (TDException tdEx)
			{
				if (!tdEx.Logged)
					Console.WriteLine(tdEx.ToString() + " has occured on initialisation.");
				statusCode = (int)tdEx.Identifier;
			}
		
			return statusCode;
		}

		public int RunApp(string[] args)
		{
			int statusFlag = 0;
			string dataFeed = string.Empty;
			string ipAddress = string.Empty;
			bool isTest = false;
			char[] stringSeperator = {':'};
			bool exitflag = false;
			if(args.Length < 1 || args.Length > 2)
			{
				displayHelp();
				statusFlag = 2;
				exitflag = true;
			}
			else
			{
				foreach(string s in args)
				{
					if(s.StartsWith("/"))
					{
						s.ToLower();
						string[] formattedArg = s.Split(stringSeperator,2);
						switch(formattedArg[0])
						{
							case "/help":
								displayHelp();
								statusFlag = 2;
								exitflag = true;
								break;

							case "/test":
								isTest = true;
								if (dataFeed == string.Empty)
								{
									Console.WriteLine("Invalid parameters, please read help text for correct usage");
									Console.WriteLine(" ");
									displayHelp();
									statusFlag = 2;
									exitflag = true;
								}
								break;

							case "/ipaddress":
								ipAddress = formattedArg[1];
								if (dataFeed == string.Empty)
								{
									Console.WriteLine("Invalid parameters, please read help text for correct usage");
									Console.WriteLine(" ");
									displayHelp();
									statusFlag = 2;
									exitflag = true;
								}
								break;

							default:
								Console.WriteLine("Invalid parameters, please read help text for correct usage");
								Console.WriteLine(" ");
								displayHelp();
								statusFlag = 2;
								exitflag = true;
								break;
						}
					}
					else
					{
						if(s.Equals(args[0]))
						{
							dataFeed = s;
						}
					}	
				}
			}

			if (exitflag == false)
			{
				FtpController iController = new FtpController();
				statusFlag = iController.TransferFiles(dataFeed,0, isTest, ipAddress);

			}
			if (statusFlag == 0)
			{
				Console.WriteLine("Success");
			}
			else
			{
				if (statusFlag == 1)
				{
					Console.WriteLine("sFtp transfer has failed - No files found to transfer.");
				}

				if (statusFlag == 2)
				{
					Console.WriteLine("sFtp transfer has failed - No files found to transfer and failure threshold has been reached. Raise ticket.");
				}

				else
				{
					Console.WriteLine("sFtp transfer has failed with error "+statusFlag+" - please consult log file.");
				}

			}
			return statusFlag;

		}

		private void displayHelp()
		{
			Console.WriteLine("td.ftppull.exe");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("execute this program from the command line by typing :-");
			Console.WriteLine("");
			Console.WriteLine("td.ftppull.exe [data feed name] [/help] [/test] [/ipaddress:[alt address]]");
			Console.WriteLine("");
			Console.WriteLine("Where :-");
			Console.WriteLine("");
			Console.WriteLine("[data feed name] is the name of the datafeed to process.");
			Console.WriteLine("");
			Console.WriteLine("/help    -  display this text");
			Console.WriteLine("/test    -  checks that data feed is configured ready for sFTP. No data transfer will take place ");
			Console.WriteLine("/ipaddress:[alt address]  - the alt address will be used instead of the one held in the database for this data feed");
			Console.WriteLine("");
			Console.WriteLine("Note - Only one of the / flags may be appended.");
		}

	}
}
