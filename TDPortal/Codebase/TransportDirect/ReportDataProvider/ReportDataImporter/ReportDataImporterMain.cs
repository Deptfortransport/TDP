// *********************************************** 
// NAME                 : ReportDataImporterMain.cs
// AUTHOR               : Andy Lole
// DATE CREATED         : 22/09/2003 
// DESCRIPTION			: Entry point for the Console app
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/ReportDataImporterMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:02   mturner
//Initial revision.
//
//   Rev 1.8   Nov 25 2003 19:01:04   geaton
//Properties no longer passed to controller.
//
//   Rev 1.7   Nov 21 2003 12:10:44   geaton
//Only report success if return code zero.
//
//   Rev 1.6   Nov 21 2003 11:29:48   geaton
//Added message for completion.
//
//   Rev 1.5   Nov 20 2003 22:44:56   geaton
//Added extra exception handling and refactored.
//
//   Rev 1.4   Nov 18 2003 21:28:00   geaton
//Refactored.
//
//   Rev 1.3   Oct 07 2003 19:54:32   ALole
//Updated to Use the correct Stored Procedures to copy between ReportDataStaging DB and ReportData DB.
//To Test it must be possible to connect to another machine with acces to the ReportData DB.
//
//   Rev 1.2   Oct 02 2003 17:27:36   ALole
//Updated to run using a single stored procedure to import data for each table.
//The code is correct, though there issuses with SqlServer not supporting Distributed Transactions.
//It has been checked in at this stage to ensure that the code is available for future changes.
//
//   Rev 1.1   Sep 24 2003 15:28:48   ALole
//Initial Draft.
//Currently using 2 sets of stored procedures - 1 set in ReportDataStagingDB and one set in ReportDataDB.
//This will be replaced with one stored procedure per table in the next version.
using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using System.Configuration;



namespace TransportDirect.ReportDataProvider.ReportDataImporter
{
	/// <summary>
	/// Main Method for the ReportDataImporter.
	/// </summary>
	public class ReportDataImporterMain
	{

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
                #region Default days offset
                // If no args supplied, try running in default days offset from config
                if (args.Length == 0)
                {
                    string daysoffset = ConfigurationManager.AppSettings["ReportDataImporter.DaysOffset.Default"];
                    if (!string.IsNullOrEmpty(daysoffset))
                    {
                        args = new string[1] { daysoffset };

                        Console.WriteLine(string.Format(Messages.ReportDataImporter_DaysOffset, daysoffset));
                    }
                }
                #endregion

                // Check arguments and run
				if (args.Length == 1)
				{
					if ((String.Compare(args[0], "/help", true ) == 0)
                        || (String.Compare(args[0], "/?", true ) == 0))
                    {
                        #region Show help
                        Console.WriteLine();
						Console.WriteLine(Messages.Init_Usage);
						returnCode = 0;
                        #endregion
                    }
					else if (String.Compare( args[0], "/test", true ) == 0)
                    {
                        #region Test mode
                        TDServiceDiscovery.Init(new ReportDataImporterInitialisation());

						returnCode = 0;

						if (TDTraceSwitch.TraceInfo)
							Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, Messages.ReportDataImporter_TestSucceeded));

						Console.WriteLine(Messages.ReportDataImporter_TestSucceeded);
                        #endregion
                    }
					else
                    {
                        #region Run importer

                        if ( PropertyValidator.IsWholeNumber(args[0]) )
						{
							TDServiceDiscovery.Init(new ReportDataImporterInitialisation());

							int dayOffset = int.Parse(args[0]);

							if (TDTraceSwitch.TraceInfo)
								Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, String.Format(Messages.ReportDataImporter_Started, args[0]) ));

							ReportDataImporterController rdImporter = new ReportDataImporterController();

							returnCode = rdImporter.Run(dayOffset);

                            if ((TDTraceSwitch.TraceInfo) && (returnCode == 0))
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, String.Format(Messages.ReportDataImporter_Completed, args[0])));
                                Console.WriteLine(String.Format(Messages.ReportDataImporter_Completed, args[0]));
                            }

						}
						else
						{
							Console.WriteLine(Messages.ReportDataImporter_InvalidArg);
							returnCode = (int)TDExceptionIdentifier.RDPDataImporterInvalidArg;
                        }

                        #endregion
                    }
				}
				else
				{
					Console.WriteLine(Messages.ReportDataImporter_InvalidArg);
					returnCode = (int)TDExceptionIdentifier.RDPDataImporterInvalidArg;
				}
			}
			catch (TDException tdEx)
			{
				// Log error (cannot assume that TD listener has been initialised)
				if (!tdEx.Logged)
				{
					Console.Write(String.Format(Messages.ReportDataImporter_Failed, tdEx.Message, tdEx.Identifier));
                    Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.ReportDataImporter_Failed, tdEx.Message, tdEx.Identifier)));
				}

				returnCode = (int)tdEx.Identifier;
			}

			return returnCode;
		}

	}
}
