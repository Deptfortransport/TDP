// ************************************************ 
// NAME                 : Program.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Entry point for LocationJSGenerator.
//                        Used to create the location JavaScript data files sent to the browser 
//                        to provide the “auto-suggest” drop down functionality
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationJsGenerator/Program.cs-arc  $
//
//   Rev 1.0   Aug 28 2012 10:35:42   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Entry point for LocationJSGenerator
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point for LocationJSGenerator
        /// </summary>
        /// <param name="args"></param>
        static int Main(string[] args)
        {
            int exitCode = -1;
            try
            {
                // Initialise service discovery
                TDServiceDiscovery.Init(new Initialisation());

                // Initialise Javascript generator settings
                JSGeneratorSettings settings = new JSGeneratorSettings(args);

                JSGenerator generator = new JSGenerator();

                // Call to create Javascripts
                generator.CreateScripts(JSGeneratorMode.TDPDefault);
                generator.CreateScripts(JSGeneratorMode.TDPNoGroups);
                generator.CreateScripts(JSGeneratorMode.TDPNoGroupsNoLocalitiesNoPOIs);
                generator.CreateScripts(JSGeneratorMode.TDPNoLocalities);
                generator.CreateScripts(JSGeneratorMode.TDPNoLocalitiesNoPOIs);

                exitCode = 0;
            }
            catch (TDException tdex)
            {
                exitCode = (int)tdex.Identifier;
            }
            catch (Exception ex)
            {
                string message = string.Format("Error generating location javascripts : {0}", ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDEventCategory.Business,
                      TDTraceLevel.Error,
                      message));

                exitCode = -1;
            }

            // Exit
            string exitMessage = string.Format("Location Javascript Generator exited with code: {0}", exitCode);

            Trace.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, exitMessage));
            Console.WriteLine(exitMessage);

            return exitCode;
        }
    }
}
