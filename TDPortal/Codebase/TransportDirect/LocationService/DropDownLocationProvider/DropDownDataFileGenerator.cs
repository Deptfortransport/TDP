// ************************************************ 
// NAME                 : DropDownDataFileGenerator.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : The DropDownDataFileGenerator class is used to create 
//                        the JavaScript data file sent to the browser 
//                        to provide the “auto-suggest” drop down functionality
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownDataFileGenerator.cs-arc  $
//
//   Rev 1.7   Jul 08 2010 09:36:04   apatel
//Code review actions
//Resolution for 5568: DropDownGaz - code review actions
//
//   Rev 1.6   Jun 21 2010 16:55:10   mmodi
//Updated logging and added TNG monitor log message
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.5   Jun 16 2010 10:20:18   apatel
//Updated 
//
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.4   Jun 14 2010 16:41:16   apatel
//Updated logging
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.3   Jun 14 2010 15:58:28   mmodi
//Added comments
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Jun 14 2010 11:54:54   apatel
//Updated
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.1   Jun 07 2010 16:10:44   apatel
//Updated
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.0   Jun 04 2010 11:27:28   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.ScriptRepository;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// DropDownDataFileGenerator class
    /// </summary>
    public class DropDownDataFileGenerator 
    {
        #region Private Fields
        List<DropDownLocation> dropDownLocationData = null;
        private DropDownLocationType ddlType;
        
        private DropDownLocationHelper helper = new DropDownLocationHelper();

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dropDownLocationData">Data to generate files for</param>
        /// <param name="ddlType">Drop down location type</param>
        public DropDownDataFileGenerator(List<DropDownLocation> ddlData, DropDownLocationType ddlType)
        {
            this.dropDownLocationData = ddlData;
            this.ddlType = ddlType;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to generate the javascript file for the data contained within the class
        /// </summary>
        /// <param name="fileName">Base name of the file</param>
        /// <param name="sequenceNo">Sequence/version number to append to the file name</param>
        /// <param name="noOfParts">Number of file parts the data should be split into</param>
        /// <returns></returns>
        public string[] GenerateJavascript(string fileName, int sequenceNo, int noOfParts)
        {
            if (noOfParts == 0 || sequenceNo < 0)
            {
                // return string array with length of 0 effectively turning off the auto-suggest functionality
                return new string[0];
            }

            string[] jsFiles = new string[noOfParts];

            try
            {
                int splitRecordCount = (int)(Math.Round(((double)dropDownLocationData.Count / (double)noOfParts)));

                int recordCountStart = 0;

                for (int scriptCount = 1; scriptCount <= noOfParts; scriptCount++)
                {
                    // Determine number of records for this file part
                    int recordsToInclude = (scriptCount == noOfParts) ? (dropDownLocationData.Count - recordCountStart) : splitRecordCount;

                    string jsScript = BuildScript(dropDownLocationData.GetRange(recordCountStart, recordsToInclude), fileName, sequenceNo, scriptCount);

                    if (!string.IsNullOrEmpty(jsScript))
                    {
                        jsFiles[scriptCount - 1] = jsScript;
                    }
                    else
                    {
                        // No script name created, could be a bad base name for file
                        throw new TDException(
                            string.Format("Drop Down Gaz - Error generating drop down data script with file name[{0}] with sequence number[{1}] and part [{2}]",
                                fileName, sequenceNo, scriptCount), false, TDExceptionIdentifier.DDGErrorCreatingDataFile);
                    }

                    recordCountStart += splitRecordCount;
                }

            }
            #region Error handling
            catch (TDException tdEx)
            {
                throw tdEx;
            }
            catch (Exception ex)
            {
                string message = string.Format("Drop Down Gaz - Error generating drop down data script with file name[{0}] sequence number[{1}]. Exception message: {2}.",
                    fileName, sequenceNo, ex.Message);

                throw new TDException(message, false, TDExceptionIdentifier.DDGErrorCreatingDataFile);
            }
            #endregion

            return jsFiles;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method which performs the processing to create the javascript data file, and 
        /// adds it to the ScriptRepository
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="fileName"></param>
        /// <param name="sequenceNo"></param>
        /// <param name="scriptCount"></param>
        /// <returns></returns>
        private string BuildScript(List<DropDownLocation> dataList, string fileName, int sequenceNo, int scriptCount)
        {
            int fileGroupCount = 0;
            int fileAliasCount = 0;
            bool first = true;

            TDResourceManager resourceManager = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.LANG_STRINGS);
			
            string scriptName = string.Format(fileName, sequenceNo, scriptCount);

            StringBuilder js = new StringBuilder();
            try
            {
                js.AppendLine(GetHeaderText(scriptName));

                js.AppendFormat("var ddData_{0} = [", scriptCount);

                foreach (DropDownLocation dropDownLocation in dataList)
                {
                    if (!first)
                    {
                        js.Append(",");
                    }
                    else
                    {
                        first = false;
                    }

                    if (dropDownLocation.IsAlias)
                        fileAliasCount++; // Used for logging

                    if (dropDownLocation.IsGroup)
                        fileGroupCount++; // Used for logging
                    
                    string locationName = dropDownLocation.LocationName;
                    if (dropDownLocation.IsGroup)
                    {
                        string groupPostfix = resourceManager.GetString(string.Format("DropDownGaz.{0}.GroupStop.Suffix.Text", dropDownLocation.DropDownType));

                        if (!string.IsNullOrEmpty(groupPostfix) && !locationName.Contains(groupPostfix))
                        {
                            locationName += " " + groupPostfix;
                        }
                    }
                    
                    js.AppendFormat("[\"{0}\",\"{1}\",\"{2}\",\"{3}\",{4}]", 
                        locationName, dropDownLocation.DisplayName, 
                        dropDownLocation.ShortCode, dropDownLocation.Naptans, dropDownLocation.IsGroup?1:0);
                }

                js.AppendFormat("]; if(typeof registerLocationData == 'function'){{ registerLocationData('{0}', ddData_{1}); }}", ddlType, scriptCount);

                // Register the script for access
                AddTempScript(scriptName, js);

                helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                    string.Format("Drop Down Gaz - Drop down data script generated with file name[{0}] includes groups[{1}] and aliases[{2}]", 
                        scriptName, fileGroupCount, fileAliasCount));

            }
            catch (Exception ex)
            {
                // Let caller deal with exceptions
                throw ex;
            }

            return scriptName;
        }

        /// <summary>
        /// Method to add the supplied script to the ScriptRepository temporary scripts
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="jScriptBuilder"></param>
        private void AddTempScript(string scriptName, StringBuilder jScriptBuilder)
        {
            ScriptRepository.ScriptRepository scriptRep;
            try
            {
                scriptRep = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
            }
            catch
            {
                // Failed to get a reference to script repository, so don't create the file
                if (TDTraceSwitch.TraceWarning)
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Warning, "Drop Down Gaz - Script repository not present in TDServiceDiscovery, so JavaScript file will not be created.");
                return;
            }

            scriptRep.AddTempScript(scriptName, "W3C_STYLE", jScriptBuilder);               
        }

        /// <summary>
        /// Returns the header text to add to the start of the data javascript files
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        private string GetHeaderText(string scriptName)
        {
            DateTime now = DateTime.Now;
            return String.Format("// {0} file generated at {1}/{2}/{3} {4}:{5}.{6}", scriptName, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);
        }

        #endregion

    }
}
