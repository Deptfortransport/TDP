// *********************************************** 
// NAME             : Program.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Feb 2012
// DESCRIPTION  	: Entry point for the Data Loader console app
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger = System.Diagnostics.Trace;
using TDP.Common.EventLogging;
using TDP.Common;
using TDP.Common.ServiceDiscovery;

namespace TDP.DataLoader
{
    /// <summary>
    /// Entry point for the Data Loader console app
    /// </summary>
    class Program
    {
        /// <summary>
        /// /// <summary>
        /// The main entry point for the application
        /// </summary>
        /// <returns>
        /// Exit Code: 
        /// Zero signals success.
        /// Greater than zero signals failure.
        /// </returns>
        /// </summary>
        /// <param name="args"></param>
        public static int Main(string[] args)
        {
            int returnCode = 0;

            // Expect dataName and (optional) noTransfer args
            string dataName = string.Empty;
            bool dataTransfer = true;
            bool dataLoad = true;
            bool isHelp = false;
            bool isTest = false;

            Console.WriteLine();

            try
            {
                // Read arguments supplied
                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        switch (arg.ToLower().Trim())
                        {
                            case "/help":
                            case "/?":
                                isHelp = true;
                                break;
                            case "/test":
                                isTest = true;
                                break;
                            case "/notransfer":
                                dataTransfer = false;
                                break;
                            case "/noload":
                                dataLoad = false;
                                break;
                            default:
                                if (!string.IsNullOrEmpty(dataName))
                                {
                                    throw new TDPException(
                                        string.Format("Unexpected argument supplied [{0}], only one dataName can be supplied", arg), 
                                        false, 
                                        TDPExceptionIdentifier.DLDataLoaderInvalidArgument);
                                }
                                dataName = arg;
                                break;
                        }
                    }
                }

                // Run with validated arguments
                returnCode = Run(dataName, dataTransfer, dataLoad, isHelp, isTest);
            }
            catch (TDPException tdpEx)
            {
                // Log error (cannot assume that listener has been initialised)
                if (!tdpEx.Logged)
                {
                    Logger.Write(string.Format(Messages.Loader_Failed, tdpEx.Message, tdpEx.Identifier));
                }
                
                Console.WriteLine(string.Format(Messages.Loader_Failed, tdpEx.Message, tdpEx.Identifier));

                returnCode = (int)tdpEx.Identifier;
            }
            catch (Exception ex)
            {
                // Any unhandled exceptions
                Logger.Write(string.Format(Messages.Loader_UnhandledError, ex.Message, ex.StackTrace));
                Console.WriteLine(string.Format(Messages.Loader_UnhandledError, ex.Message, ex.StackTrace));

                returnCode = (int)TDPExceptionIdentifier.DLDataLoaderUnexpectedException;
            }

            if (!isHelp)
            {
                string returnCodeText = (returnCode != 0) ? ((TDPExceptionIdentifier)returnCode).ToString() : string.Empty;
                
                Console.WriteLine();
                Console.WriteLine(string.Format(Messages.Loader_Exit, returnCode, returnCodeText));
            }

            return returnCode;
        }

        /// <summary>
        /// Runs the data loader
        /// </summary>
        /// <returns>Return code passed to client.</returns>
        private static int Run(string dataName, bool dataTransfer, bool dataLoad, bool isHelp, bool isTest)
        {
            // Assume success
            int returnCode = 0;

            try
            {
                // Show help message
                if (isHelp)
                {
                    Console.WriteLine(Messages.Init_Usage);
                }
                // Start test
                else if (isTest)
                {
                    Console.WriteLine(Messages.Loader_Starting);

                    TDPServiceDiscovery.Init(new DataLoaderInitialisation());

                    if (TDPTraceSwitch.TraceInfo)
                        Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, Messages.Loader_TestSucceeded));

                    Console.WriteLine();
                    Console.WriteLine(Messages.Loader_TestSucceeded);
                }
                // Data name supplied, load data
                else if (!string.IsNullOrEmpty(dataName))
                {
                    Console.WriteLine(Messages.Loader_Starting);

                    TDPServiceDiscovery.Init(new DataLoaderInitialisation());

                    if (TDPTraceSwitch.TraceInfo)
                        Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, Messages.Loader_Started));

                    // Use the controller to perform the load
                    DataLoaderController controller = new DataLoaderController();
                    returnCode = controller.Load(dataName, dataTransfer, dataLoad);

                    if ((TDPTraceSwitch.TraceInfo) && (returnCode == 0))
                        Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, Messages.Loader_Completed));
                }
                // No Data name supplied, exit
                else
                {
                    Console.WriteLine(Messages.Loader_NoArg);
                }
            }
            catch (TDPException tdpEx)
            {
                if (!tdpEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error,
                        string.Format(Messages.Loader_Failed, tdpEx.Message, tdpEx.Identifier)));
                }

                returnCode = (int)tdpEx.Identifier;
            }
            
            return returnCode;
        }
    }
}