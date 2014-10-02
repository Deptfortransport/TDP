// ************************************************ 
// NAME                 : DropDownLocationProviderService.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : DropDown location Provider class
//                      : This class is responsible to monitor the change notification
//                        events to generate new dropdown data javascript files. Loads 
//                        DropDownLocation data from the database and interacts with the 
//                        dropdown script generator class to generate new dropdown data 
//                        javascripts
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownLocationProviderService.cs-arc  $
//
//   Rev 1.7   Jul 12 2010 15:28:16   apatel
//Updated script removal functionality to remove all old files
//Resolution for 5572: DropDownGaz - Deletion of old data file versions
//
//   Rev 1.6   Jun 21 2010 16:55:12   mmodi
//Updated logging and added TNG monitor log message
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.5   Jun 17 2010 15:48:04   apatel
//Added logging for successfull data script file creation
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.4   Jun 15 2010 15:11:58   apatel
//Updated to generate script files in the event of server start/restart and there is no script files exists in tempscripts folder
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.3   Jun 14 2010 16:45:20   apatel
//Updated
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Jun 14 2010 11:54:56   apatel
//Updated
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.1   Jun 07 2010 16:10:46   apatel
//Updated
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.0   Jun 04 2010 11:27:32   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;
using System.IO;


namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// DropDownLocationProviderService class
    /// </summary>
    public class DropDownLocationProviderService : IDropDownLocationProviderService
    {
        #region Private Fields
        
        #region Constants
        
        private const string DataChangeNotificationGroup_Rail = "DropDownGazRail";
        private const string DataChangeNotificationGroup_Rail_Sync = "DropDownGazSyncRail";
        
        #endregion

        private bool receivingChangeNotifications;

        /// <summary>
        /// Used when creating new drop down data files
        /// </summary>
        private static readonly object lockObject = new object();
                
        /// <summary>
        /// Stores current drop down data scripts
        /// </summary>
        private Dictionary<DropDownLocationType, string[]> currentDropDownDataScripts = new Dictionary<DropDownLocationType, string[]>();
        
        /// <summary>
        /// Temporary script store to hold the new script generated untill all the servers are synchronised
        /// Once all the servers are in synch used to replace the current drop down data script for perticular drop down type.
        /// </summary>
        private Dictionary<DropDownLocationType, string[]> tempDropDownDataScripts = new Dictionary<DropDownLocationType, string[]>();

        private DropDownLocationHelper helper = new DropDownLocationHelper();
        
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes change notifications
        /// </summary>
        public DropDownLocationProviderService()
        {
            receivingChangeNotifications = RegisterForChangeNotification();
        }

        #endregion
        
        #region IDropDownLocationProviderService Members

        /// <summary>
        /// This method returns a string array of Script names that have been generated 
        /// for the JavaScript data files. The PageId value indicates which mode 
        /// the “auto-suggest” drop down logic is being added to.
        /// <example>for PageId.FindTrainInput, the DropDownLocationType.Rail data script names are returned</example>
        /// Any PageId’s not supported by the provider will return an empty string array.
        /// The Script name refers to the data script added to the ScriptRepository
        /// </summary>
        /// <param name="pageId">PageId enum value</param>
        /// <returns>string array of Script names that have been generated for the JavaScript data files</returns>
        public string[] GetDropDownLocationDataScriptName(TransportDirect.Common.PageId pageId)
        {
            try
            {
                if (helper.IsDropDownEnabled())
                {
                    if (pageId == PageId.FindTrainCostInput || pageId == PageId.FindTrainInput)
                    {
                        if (currentDropDownDataScripts.ContainsKey(DropDownLocationType.Rail))
                        {
                            return currentDropDownDataScripts[DropDownLocationType.Rail];
                        }
                        else
                        {
                            // Server may be restarted
                            // In this situation build the script names using currentsequence no 
                            // Register the scripts with the script repository if the scripts exist
                            // and return the script names
                            helper.Log(TDEventCategory.Business, TDTraceLevel.Info, "Drop Down Gaz - Web server may have been restarted, trying to create new or use existing data file scripts.");
                            if (RegisterScriptsForCurrentSequence(DropDownLocationType.Rail))
                            {
                                return currentDropDownDataScripts[DropDownLocationType.Rail];
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                helper.Log(TDEventCategory.Business, TDTraceLevel.Error,
                    string.Format("Drop Down Gaz - Unable to return script names for {0} due to error : {1}", 
                        pageId, ex.StackTrace));
            }
            return null;
        }

        
                

        /// <summary>
        /// This method uses the enabled flags set during the class constructor to return 
        /// a Boolean indicating if the drop down locations should be added to the page’s input controls
        /// </summary>
        /// <param name="pageId">PageId enum value</param>
        /// <returns>true if the AutoSuggest functionality enabled for the page</returns>
        public bool DropDownLocationEnabled(TransportDirect.Common.PageId pageId)
        {
            return helper.IsDropDownEnabled() && helper.IsDropDownEnabled(pageId);
        }

        #endregion

        
        #region Private Methods
        
        #region Change notification
        /// <summary>
        /// Registers an event handler with the data change notification service
        /// </summary>
        private bool RegisterForChangeNotification()
        {
            IDataChangeNotification notificationService;
            try
            {
                notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
            }
            catch (TDException e)
            {
                // If the SDInvalidKey TDException is thrown, return false as the notification service
                // hasn't been initialised.
                // Otherwise, rethrow the exception that was received.
                if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
                {
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising DropDownLocationProviderService");
                    return false;
                }
                else
                    throw;
            }
            catch
            {
                // Non-CLS-compliant exception
                throw;
            }

            notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
            return true;
        }
        
        #endregion
    
        #region Drop down data load

        /// <summary>
        /// Returns a list of DropDownLocation objects loaded from the database
        /// </summary>
        /// <param name="ddlType">DropDownLocationType enum value - Type of the DropDownLocation(i.e. Rail)</param>
        private List<DropDownLocation> LoadData(DropDownLocationType ddlType)
        {
            return helper.GetDropDownData(ddlType);
        }

        #endregion

        #region Drop down data file(s) generation

        /// <summary>
        /// This method is responsible for creating the javascript data file for the provided drop down location type and data
        /// </summary>
        /// <param name="ddlType">DropDownLocationType enum value - Type of the DropDownLocation(i.e. Rail)</param>
        /// <param name="ddlData">DropDownLocation data array</param>
        private void CreateDataFile(DropDownLocationType ddlType, List<DropDownLocation> ddlData)
        {
            lock (lockObject)
            {
                try
                {
                    // Version number of the new file 
                    int sequenceNo = helper.GetDropDownDataSequenceNumber(ddlType, true);

                    int dataParts = helper.DropDownDataFileParts(ddlType);

                    DropDownDataFileGenerator generator = new DropDownDataFileGenerator(ddlData, ddlType);

                    string[] jsScriptNames = generator.GenerateJavascript(helper.GetDropDownDataFileName(ddlType), sequenceNo, dataParts);

                    // Store the new data file names ready for when web server detects trigger for
                    // changing to use the new data file name
                    if (tempDropDownDataScripts.ContainsKey(ddlType))
                    {
                        tempDropDownDataScripts[ddlType] = jsScriptNames;
                    }
                    else
                    {
                        tempDropDownDataScripts.Add(ddlType, jsScriptNames);
                    }

                    generator = null;

                    helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                        string.Format("Drop Down Gaz - File with new sequence number[{0}] created for drop down type[{1}] and copied to temparory dictionary", 
                            sequenceNo, ddlType.ToString()));
                }
                catch (Exception ex)
                {
                    helper.LogTNGError(TDEventCategory.Business, TNGAlert.CreateFailed,
                        string.Format("Error encountered while generating data file drop down type[{0}], error: {1}", 
                            ddlType.ToString(), ex.Message));
                }
            }
            
        }

        /// <summary>
        /// Generates auto-suggest data file names using current data sequence no for specific drop down location type provided
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        /// <returns></returns>
        private string[] BuildScriptNamesForCurrentSeqence(DropDownLocationType dropDownLocationType)
        {
            int fileparts = helper.DropDownDataFileParts(dropDownLocationType);

            // Get the current version number used in generating the file names
            int currentSeq = helper.GetDropDownDataSequenceNumber(dropDownLocationType, false);

            string fileNameBase = helper.GetDropDownDataFileName(dropDownLocationType);

            string[] fileNames = new string[fileparts];

            for (int fileCount = 1; fileCount <= fileparts; fileCount++)
            {
                fileNames[fileCount - 1] = string.Format(fileNameBase, currentSeq, fileCount);
            }

            return fileNames;
        }

        /// <summary>
        /// Registers temp script with script repository
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        private bool RegisterTempScript(string scriptName)
        {
            ScriptRepository.ScriptRepository scriptRep = GetScriptRepository();
            if (scriptRep != null)
                return scriptRep.RegisterTempScript(scriptName, "W3C_STYLE");
            else
                return false;

        }

        /// <summary>
        /// Builds Data Script names for specific drop down location type and registers scripts with script repository
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        /// <returns></returns>
        private bool RegisterScriptsForCurrentSequence(DropDownLocationType dropDownLocationType)
        {
            bool validDataScripts = true;
            try
            {
                string[] datascripts = BuildScriptNamesForCurrentSeqence(DropDownLocationType.Rail);

                int fileparts = helper.DropDownDataFileParts(dropDownLocationType);

                int currentSeq = helper.GetDropDownDataSequenceNumber(dropDownLocationType, false);

                foreach (string scriptName in datascripts)
                {
                    // ScripRepository will test the file exists, returning false if it doesnt
                    validDataScripts = RegisterTempScript(scriptName);

                    if (!validDataScripts)
                        break;
                }

                if (validDataScripts)
                {
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                        string.Format("Drop Down Gaz - Existing file with sequence number[{0}] for drop down type[{1}] will be used.",
                            currentSeq, dropDownLocationType.ToString()));
                }
                else if (!validDataScripts)
                {
                    // ScriptRepository indicated files were not found, 
                    // try generating script again using current sequence number
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                        string.Format("Drop Down Gaz - File with sequence number[{0}] was not found for drop down type[{1}], recreating files.",
                            currentSeq, dropDownLocationType.ToString()));

                    try
                    {
                        // Re-build the script and register it in the ScriptRepository
                        DropDownDataFileGenerator generator = new DropDownDataFileGenerator(LoadData(dropDownLocationType), dropDownLocationType);

                        datascripts = generator.GenerateJavascript(helper.GetDropDownDataFileName(dropDownLocationType), currentSeq, fileparts);

                        generator = null;
                    }
                    catch (TDException tdEx)
                    {
                        // Any errors attempting to Re-create files should be logged for TNG Monitoring
                        helper.LogTNGError(TDEventCategory.Business, TNGAlert.FileMissing, tdEx.Message);
                        
                        throw tdEx;
                    }


                    helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                        string.Format("Drop Down Gaz - File with sequence number[{0}] successfully created for drop down type[{1}].",
                            currentSeq, dropDownLocationType.ToString()));

                    if (datascripts.Length > 0)
                        validDataScripts = true;
                }

                if (validDataScripts)
                {
                    // Set the current script names to use 
                    if (!currentDropDownDataScripts.ContainsKey(dropDownLocationType))
                    {
                        currentDropDownDataScripts.Add(dropDownLocationType, datascripts);
                    }
                }
            }
            #region Error handling
            catch (TDException tdEx)
            {
                if (!tdEx.Logged)
                {
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Error, tdEx.Message);
                }
                validDataScripts = false;
            }
            catch (Exception ex)
            {
                string error = string.Format("Drop Down Gaz - Error registering scripts for drop down type[{0}], error: {1}",
                    dropDownLocationType.ToString(), ex.StackTrace);

                helper.Log(TDEventCategory.Business, TDTraceLevel.Error, error);

                validDataScripts = false;
            }
            #endregion

            return validDataScripts;
        }
        #endregion

        #region Drop down data file remove old files

        /// <summary>
        /// This method removes the data files based on the “versions to retain” value from the Properties service
        /// </summary>
        /// <param name="ddlType">DropDownLocationType enum value - Type of the DropDownLocation(i.e. Rail</param>
        private void RemoveDataFiles(DropDownLocationType ddlType)
        {
            int sequenceNo = helper.GetDropDownDataSequenceNumber(ddlType, true);

            int numberOfVersionsToRetain = helper.NumberOfVersionsToRetain;

            int dataParts = helper.DropDownDataFileParts(ddlType);

            int sequenceToDelete = sequenceNo - numberOfVersionsToRetain;
            try
            {
                if (sequenceToDelete > 0)
                {
                    List<string> filesToRetain = new List<string>();


                    for (int scriptCount = 1; scriptCount <= dataParts; scriptCount++)
                    {
                        for (int seq = sequenceNo; seq > sequenceToDelete; seq--)
                        {
                            string scriptName = string.Format(helper.GetDropDownDataFileName(ddlType), seq, scriptCount);

                            filesToRetain.Add(scriptName);
                        }

                    }

                    RemoveOldTempScripts(helper.GetDropDownDataFileName(ddlType), filesToRetain);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format("Drop Down Gaz - Error removing old scripts error: {0}",
                   ex.StackTrace);

                helper.Log(TDEventCategory.Business, TDTraceLevel.Error, error);
            }
        }

        /// <summary>
        /// Removes the old DropDown temp scripts
        /// </summary>
        /// <param name="formattedScriptName">formatted script name which is used to build the script to generate</param>
        /// <param name="filesToKeep">list of files which should be kept</param>
        private void RemoveOldTempScripts(string formattedScriptName, List<string> filesToKeep)
        {
            ScriptRepository.ScriptRepository scriptRep = GetScriptRepository();

            string tempScriptFolder = scriptRep.TempScriptFolderPath;

            string searchPattern = formattedScriptName.Replace("{0}", "*").Replace("{1}", "*") + "_W3C_STYLE.js";

            string[] tempScriptFiles = Directory.GetFiles(tempScriptFolder, searchPattern);

            List<string> filesToDelete = new List<string>();

            foreach (string tempScriptFullPath in tempScriptFiles)
            {
                bool fileToKeep = false;

                foreach (string fileToRetain in filesToKeep)
                {
                    if(tempScriptFullPath.ToLower().Contains(fileToRetain.ToLower()))
                    {
                        fileToKeep = true;
                    }
                }

                if (!fileToKeep)
                {
                    FileInfo scriptInfo = new FileInfo(tempScriptFullPath);

                    string scriptName = scriptInfo.Name.Replace("_W3C_STYLE.js", "").Trim();

                    RemoveTempScript(scriptName);
                }
            }

            
        }


        /// <summary>
        /// Removes script from the temp script folder and script repository
        /// </summary>
        /// <param name="scriptName">Name of the script</param>
        private void RemoveTempScript(string scriptName)
        {
            ScriptRepository.ScriptRepository scriptRep = GetScriptRepository();

            if(scriptRep != null)
                scriptRep.RemoveTempScript(scriptName, "W3C_STYLE", true);

        }


        #endregion

        #region Script Repository
        /// <summary>
        /// Gets reference to ScriptRepository object
        /// </summary>
        /// <returns></returns>
        private ScriptRepository.ScriptRepository GetScriptRepository()
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
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Warning, 
                        "Drop Down Gaz - Script repository not present in TDServiceDiscovery, so Drop down data file will not be created.");
                return null;
            }

            return scriptRep;
        }
        #endregion

        /// <summary>
        /// Replaces currently used dropdown data scripts with newly generated data scripts.
        /// Also relases the lock placed on the Synch table for specified drop down location type
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        private void UseNewDropDownScripts(DropDownLocationType dropDownLocationType)
        {
            int fileCreatedOnServersCount = helper.GetServerSyncCount(dropDownLocationType, ServerSyncCountType.FileCreated);
            int webServerCount = helper.WebServerCount;

            // Should only enter this method when all web servers have created the new data file,
            // otherwise something has gone wrong
            if (fileCreatedOnServersCount == webServerCount)
            {
                try
                {
                    // Switch to using the new drop down data script names
                    if (tempDropDownDataScripts.ContainsKey(dropDownLocationType))
                    {
                        if (currentDropDownDataScripts.ContainsKey(dropDownLocationType))
                        {
                            currentDropDownDataScripts[dropDownLocationType] = tempDropDownDataScripts[dropDownLocationType];
                        }
                        else
                        {
                            currentDropDownDataScripts.Add(dropDownLocationType, tempDropDownDataScripts[dropDownLocationType]);
                        }

                        #region Log
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            foreach (string scriptName in tempDropDownDataScripts[dropDownLocationType])
                            {
                                helper.Log(TDEventCategory.Business, TDTraceLevel.Verbose,
                                    string.Format("Drop Down Gaz - Switched to using new drop down data file[{0}] for drop down type[{1}]",
                                        scriptName, dropDownLocationType));
                            }
                        }
                        #endregion

                        tempDropDownDataScripts.Remove(dropDownLocationType);
                    }
                    else
                    {
                        // If the temp scripts does not contain any file names, then
                        // likely that the sequence number to use has been rolled back. So clear
                        // current files list - the current Sequence number files will be 
                        // used re-add the files to the current list next time a call is made 
                        // to get the files
                        currentDropDownDataScripts.Remove(dropDownLocationType);
                    }

                    // Update the database sync status count
                    helper.UpdateSyncCount(dropDownLocationType, ServerSyncCountType.FileUsing);

                    // Check if all web servers are synched to using the new data script names
                    int fileUsingOnServersCount = helper.GetServerSyncCount(dropDownLocationType, ServerSyncCountType.FileUsing);
                    if (fileUsingOnServersCount == webServerCount)
                    {
                        // All servers are using newly created dropdown data scripts 
                        // Reset server synch status
                        helper.ResetServerSyncStatus(dropDownLocationType);
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = string.Format("Drop Down Gaz - Error occurred updating/reading the drop down sync status count, message {0}, error: {1}", 
                        ex.Message, ex.StackTrace);
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Error, errorMsg);
                }
            }
            else
            {
                // Log TNG monitoring error as web servers maybe in inconsistent state with the data file they are serving
                string errorMsg = string.Format("Unexpected File Created Count value encountered in method UseNewDropDownScripts in DropDownLocationProviderService class, expected[{0}] actual[{1}]. Web servers may not all be using the same Drop down data file.",
                    webServerCount, fileCreatedOnServersCount);
                helper.LogTNGError(TDEventCategory.Business, TNGAlert.FileMissing, errorMsg);
            }
        }

        #endregion

        #region Event Handlers

        #region Change Notification Events
        /// <summary>
        /// Used by the Data Change Notification service
        /// Responsible to start process for generating new data files for the auto-suggest dropdowns
        /// Also responsible for sync mechanism requrie to keep all the server using the same file
        /// </summary>
        private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
        {
            try
            {
                // Create new drop down data files
                if (e.GroupId == DataChangeNotificationGroup_Rail)
                {
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                        string.Format("Drop Down Gaz - Change notfication received[{0}], creating new drop down data files.", DataChangeNotificationGroup_Rail));

                    // Rail data updated. Load new data and generate new data files
                    List<DropDownLocation> railData = LoadData(DropDownLocationType.Rail);
                    CreateDataFile(DropDownLocationType.Rail, railData);

                    helper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

                    // Check if all web servers have created the new drop down data files
                    int fileCreatedOnServersCount = helper.GetServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);
                    if (fileCreatedOnServersCount == helper.WebServerCount)
                    {
                        // All servers created new files
                        // Raise change notification to sync servers to use newly created files
                        helper.UpdateChangeNotification(DropDownLocationType.Rail, ChangeNotificationType.Sync);
                    }

                    // Tidy up the old data files
                    RemoveDataFiles(DropDownLocationType.Rail);

                    railData.Clear();
                    railData = null;

                    helper.Log(TDEventCategory.Business, TDTraceLevel.Verbose,
                        string.Format("Drop Down Gaz - Change notfication completed[{0}].", DataChangeNotificationGroup_Rail));
                }
                
                // Switch to using new drop down data files
                if (e.GroupId == DataChangeNotificationGroup_Rail_Sync)
                {
                    helper.Log(TDEventCategory.Business, TDTraceLevel.Info,
                        string.Format("Drop Down Gaz - Change notfication received[{0}], switching to use new drop down data files.", DataChangeNotificationGroup_Rail_Sync));

                    UseNewDropDownScripts(DropDownLocationType.Rail);

                    helper.Log(TDEventCategory.Business, TDTraceLevel.Verbose,
                        string.Format("Drop Down Gaz - Change notfication completed[{0}].", DataChangeNotificationGroup_Rail_Sync));
                }
            }
            catch (Exception ex)
            {
                string error = string.Format("Drop Down Gaz - Error encountered while receiving change notification for group {0} : {1}", e.GroupId, ex.StackTrace);
                helper.Log(TDEventCategory.Business, TDTraceLevel.Error, error);
            }
        }

        
        #endregion
        
        #endregion
    }
}
