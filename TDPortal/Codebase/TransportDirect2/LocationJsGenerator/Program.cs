// *********************************************** 
// NAME             : Program.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 04 Mar 2011
// DESCRIPTION  	: Entry point for LocationJSGenerator
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.LocationService;
using System.IO;
using TDP.Common.ServiceDiscovery;
using System.Diagnostics;
using TDP.Common.EventLogging;

namespace TDP.Common.LocationJsGenerator
{
    /// <summary>
    ///  Entry point for LocationJSGenerator
    /// </summary>
    class Program
    {
        /// <summary>
        ///  Entry point method for LocationJSGenerator
        /// </summary>
        /// <param name="args"></param>
        static int Main(string[] args)
        {
            int exitCode = -1;
            try
            {

                // Initialise service discovery
                TDPServiceDiscovery.Init(new Initialisation());

                // Initialise Javascript generator settings
                JSGeneratorSettings settings = new JSGeneratorSettings(args);

                JSGenerator generator = new JSGenerator();

                // Call to create Javascripts - Web
                generator.CreateScripts(JSGeneratorMode.TDPWeb);

                // Call to create Javascripts - Mobile
                generator.CreateScripts(JSGeneratorMode.TDPMobile);

                exitCode = 0;
            }
            catch (TDPException tdpEx)
            {
                exitCode = (int)tdpEx.Identifier;
            }
            catch (Exception ex)
            {
                string message = string.Format("Error generating location javascripts : {0}", ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDPEventCategory.Business,
                      TDPTraceLevel.Error,
                      message));

                exitCode = -1;
            }

            Console.WriteLine("Location Javascript Generator exited with code :{0}", exitCode);

            return exitCode;
 
        }

        
       
    }
}
