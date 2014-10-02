// ************************************************ 
// NAME                 : JSGenerator.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Class providing methods to generate the location javascipts
// ************************************************* 

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using TransportDirect.Common;
using System.IO;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Class providing methods to generate the location javascipts
    /// </summary>
    class JSGenerator
    {
        #region Public Methods

        /// <summary>
        /// Methods to generate the location javascipt files, gets locations from the location provider for
        /// the supplied generator mode, builds and saves javascript files
        /// </summary>
        public void CreateScripts(JSGeneratorMode mode)
        {
            // to track empty scripts
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            JSLocationProvider locationProvider = new JSLocationProvider();

            // Get the location data
            Dictionary<char, List<JSLocation>> locationsGroups = locationProvider.GetJsLocationData(mode);

            Trace.Write(
                       new OperationalEvent(
                           TDEventCategory.Business,
                           TDTraceLevel.Info,
                           string.Format("Writing scripts for[{0}] at [{1}]", mode, JSGeneratorSettings.ScriptFolder(mode))));

            #region Scripts with data

            // Create scripts with location data 
            foreach (char c in locationsGroups.Keys)
            {
                CreateJs(c, locationsGroups[c], mode);
            }

            #endregion

            #region Empty Scripts

            List<char> keys = new List<char>(locationsGroups.Keys);

            // Create empty scripts
            foreach (char c in alphabet)
            {
                if (!keys.Contains(c))
                {
                    CreateJs(c, null, mode);
                }
            }

            #endregion
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a location javascript for the perticular alphabetical group
        /// </summary>
        /// <param name="scriptId">Alphabetical group of the script i.e. a, b, etc.</param>
        /// <param name="locations">Location data loaded from database and aliases</param>
        private void CreateJs(char scriptId, List<JSLocation> locations, JSGeneratorMode mode)
        {
            try
            {
                StringBuilder js = new StringBuilder();
                bool first = true;

                if (locations != null)
                {
                    js.AppendFormat("var ddData_{0} = [", scriptId);

                    foreach (JSLocation loc in locations)
                    {
                        if (!first)
                        {
                            js.Append(",");
                        }
                        else
                            first = false;
                        js.Append(loc.GetJSArrayString());
                    }

                    string dataStoreId = string.Format("{0}_{1}",
                        JSGeneratorSettings.ScriptName(mode),
                        JSGeneratorSettings.Version);

                    js.AppendFormat("]; if(typeof registerLocationData == 'function'){{ registerLocationData('{0}', '{1}', ddData_{1}); }}", dataStoreId, scriptId, scriptId);
                }

                // Write physical script
                WriteScript(scriptId, js, mode, 
                    (locations != null) ? locations.Count : 0);

            }
            catch (Exception ex)
            {
                string message = string.Format("Error generating script for[{0}]:[{1}] - {2} {3}", 
                    mode, scriptId, ex.Message, ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDEventCategory.Business,
                      TDTraceLevel.Error,
                      message));

                throw new TDException(message, true, TDExceptionIdentifier.LJSGenCreateScriptFailed);
            }

        }

        /// <summary>
        /// Writes javascirpt
        /// </summary>
        /// <param name="scriptId">Alphabetical group of the script i.e. a, b, etc.</param>
        /// <param name="js">In memory script content </param>
        private void WriteScript(char scriptId, StringBuilder js, JSGeneratorMode mode, int count)
        {
            string scriptLoc = GetScriptLocation(scriptId, mode);

            Trace.Write(
                       new OperationalEvent(
                           TDEventCategory.Business,
                           TDTraceLevel.Verbose,
                           string.Format("Writing script for[{0}] with locations count[{1}] at location[{2}] ", 
                            mode, count, scriptLoc)));
                                    
            using (StreamWriter sw = new StreamWriter(scriptLoc))
            {
                sw.Write(js.ToString());
            }
        }

        /// <summary>
        /// Returns the location where script needs generating
        /// </summary>
        /// <param name="scriptId">Alphabetical group of the script i.e. a, b, etc.</param>
        /// <returns>Path/Location for the scripts needs generating</returns>
        private string GetScriptLocation(char scriptId, JSGeneratorMode mode)
        {
            try
            {
                // Get directory to write to
                string scriptPath = JSGeneratorSettings.ScriptFolder(mode);

                if (!Directory.Exists(scriptPath))
                {
                    Directory.CreateDirectory(scriptPath);
                }

                string scriptName = string.Format("{0}_{1}_{2}.js",
                        JSGeneratorSettings.ScriptName(mode),
                        JSGeneratorSettings.Version,
                        scriptId);

                string scriptLoc = Path.Combine(scriptPath, scriptName);

                return scriptLoc;
            }
            catch (Exception ex)
            {
                string message = string.Format("Error Getting location to generate scripts : {0} ", ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDEventCategory.Business,
                      TDTraceLevel.Error,
                      message));

                throw new TDException(message, true, TDExceptionIdentifier.LJSGenGetScriptLocationFailed);
            }
        }

        #endregion
    }
}