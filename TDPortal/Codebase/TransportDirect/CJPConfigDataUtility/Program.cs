// ***********************************************
// NAME 		: Program.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 30-Jun-2010
// DESCRIPTION 	: Main entry poing for the CJPConfigDataUtility executable
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CJPConfigDataUtility/Program.cs-arc  $
//
//   Rev 1.0   Jul 01 2010 12:38:46   apatel
//Initial revision.
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.CJPConfigDataUtility
{
    /// <summary>
    /// Program class entry poing for CLR to run the importer
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            
            //return code for this application
            int statusCode = 0;

            Console.WriteLine("Starting");
            try
            {
                TDServiceDiscovery.Init(new Initialisation());

                CJPConfigDataHelper helper = new CJPConfigDataHelper();

                if (args.Length == 1 && args[0] == "\\export")
                {
                    Console.WriteLine("Exporting the config file");
                    statusCode = helper.ExportConfigFile();
                    Console.WriteLine("Export completed");
                }
                else if (args.Length == 1 && args[0] == "\\import")
                {
                    Console.WriteLine("Begin Importing CJP Config files");
                    statusCode = helper.ImportConfigData();
                    Console.WriteLine("End Importing CJP Config files");
                }
                else
                {
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine("             CJP CONFIG IMPORT/EXPORT UTILITY            ");
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine();
                    Console.WriteLine("Use \\import to import the CJP config files in to database.");
                    Console.WriteLine("Use \\export argument to export the config file");
                    Console.WriteLine("Use Xml property file to set the import/export file locations:  ");
                    Console.WriteLine("The properties are as below : ");
                    Console.WriteLine("   - Export file path property : CJPConfigData.ConfigFilePath ");
                    Console.WriteLine("   - import files No of Server property : CJPConfigData.NoOfServers ");
                    Console.WriteLine("   - import file path property : CJPConfigData.ConfigFile.Server<n> where <n> is 1,2,3,... ");
                    Console.WriteLine();
                    Console.WriteLine("*********************************************************");

                }
                
            }
            catch (TDException tdEx)
            {
                if (!tdEx.Logged)
                    Console.WriteLine(tdEx.ToString() + " has occured running CJP config data importer");
                statusCode = (int)tdEx.Identifier;
            }
            Console.WriteLine("Exiting with errorcode " + statusCode);
            return statusCode;
        }
    }
}
