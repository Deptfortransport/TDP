// ************************************************ 
// NAME                 : DropDownLocationHelper.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : This class provides helper methods for core DropDownLocation classes
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownLocationHelper.cs-arc  $
//
//   Rev 1.8   Jul 08 2010 09:36:06   apatel
//Code review actions
//Resolution for 5568: DropDownGaz - code review actions
//
//   Rev 1.7   Jul 05 2010 11:05:36   mmodi
//Amended property name to get Web Server count
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
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
//   Rev 1.4   Jun 15 2010 15:12:00   apatel
//Updated to generate script files in the event of server start/restart and there is no script files exists in tempscripts folder
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.3   Jun 14 2010 16:48:08   apatel
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
//   Rev 1.0   Jun 04 2010 11:27:30   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// Enum defining types of sync operation needs to be checked in order to make 
    /// all the web server use the same data scripts
    /// </summary>
    enum ServerSyncCountType { FileCreated = 1, FileUsing = 2 }

    /// <summary>
    /// Enum defining the notification types handle by the ChangeNotification
    /// </summary>
    enum ChangeNotificationType { Data = 1, Sync = 2 }

    /// <summary>
    /// Enum defining the type of TNG monitoring alert message to raise
    /// </summary>
    enum TNGAlert { ImportFailed, CreateFailed, FileMissing }

    /// <summary>
    /// DropDownLocationHelper class
    /// </summary>
    class DropDownLocationHelper
    {
        #region Private Fields
        #region Constants
        // Stored procedures
        private const string SP_GetDropDownLocations = "GetDropDownLocations";
        private const string SP_GetSeqNumber = "GetDropDownSequenceNumber";
        private const string SP_UpdateSyncCount= "UpdateServerSynchCount";
        private const string SP_GetServerSyncCount = "GetServerSynchCount";
        private const string SP_ResetServerSyncStatus = "ResetServerSynchStatus";
        private const string SP_UpdateChangeNotification = "UpdateChangeNotification";
        #endregion

        // Database
        private SqlHelper sqlHelper = new SqlHelper();
        private const SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;

        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor for the helper class
        /// </summary>
        internal DropDownLocationHelper()
        {
        }
        #endregion

        #region Database calls
        /// <summary>
        /// Gets current/new drop down data sequence number for specific drop down type.
        /// New sequence number are used for generating new drop down data scripts.
        /// </summary>
        /// <param name="ddlType">DropDownLocation type</param>
        /// <param name="isNew">if true gets new sequence number</param>
        /// <returns></returns>
        internal int GetDropDownDataSequenceNumber(DropDownLocationType ddlType, bool isNew)
        {
            int seqNo = -1;

            try
            {

                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose, 
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_GetSeqNumber));

                // Build up the sql request
                Hashtable parameters = new Hashtable();
                parameters.Add("@DropDownType", ddlType.ToString());
                parameters.Add("@IsNewSeq", isNew);

                sqlHelper.ConnOpen(database);

                seqNo = (Int32)sqlHelper.GetScalar(SP_GetSeqNumber, parameters);

            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_GetSeqNumber, sqlEx.Message));
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to Generate rail dropdown data script from the database[{0}], Exception Message[{1}].",
                                database, ex.Message));
                throw ex;
            }
            #endregion
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            return seqNo;
        }
        
        /// <summary>
        /// Gets drop down data for perticular drop down type from database table
        /// </summary>
        /// <param name="ddlType">DropDownLocation type</param>
        /// <returns></returns>
        internal List<DropDownLocation> GetDropDownData(DropDownLocationType ddlType)
        {
            List<DropDownLocation> dropDownData = new List<DropDownLocation>();

            
            SqlDataReader sqlReader = null;
            try
            {
                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_GetDropDownLocations));
               
                sqlHelper.ConnOpen(database);
                
                // Build up the sql request
                Hashtable parameters = new Hashtable();
                parameters.Add("@DropDownType", ddlType.ToString());

                // Call stored procedure
                sqlReader = sqlHelper.GetReader(SP_GetDropDownLocations, parameters);

                #region Column ordinals
                // Assign the column ordinals
                int ordinalName = sqlReader.GetOrdinal("Name");
                int ordinalDisplayName = sqlReader.GetOrdinal("DisplayName");
                int ordinalShortCode = sqlReader.GetOrdinal("ShortCode");
                int ordinalNaptans = sqlReader.GetOrdinal("Naptans");
                int ordinalIsGroup = sqlReader.GetOrdinal("IsGroup");
                int ordinalIsAlias = sqlReader.GetOrdinal("IsAlias");
                int ordinalDropDownType = sqlReader.GetOrdinal("DropDownType");
                #endregion

                while (sqlReader.Read())
                {
                    
                    #region Read data
                    // Read the database values returned

                    string name = GetString(sqlReader,ordinalName);
                    string displayName = GetString(sqlReader,ordinalDisplayName);
                    string shortCode = GetString(sqlReader,ordinalShortCode);
                    string naptans = GetString(sqlReader,ordinalNaptans);
                    bool isGroup = sqlReader.GetBoolean(ordinalIsGroup);
                    bool isAlias = sqlReader.GetBoolean(ordinalIsAlias);
                    string dropDownType = GetString(sqlReader, ordinalDropDownType);
                    dropDownData.Add(new DropDownLocation(name,displayName,shortCode,naptans,isGroup,isAlias, dropDownType));
                    #endregion

                }

            }

            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_GetDropDownLocations, sqlEx.Message));
                              
            }
            catch (TDException tdEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to Generate rail dropdown data script from the database[{0}], TDException Message[{1}].",
                                database, tdEx.Message));
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to Generate rail dropdown data script from the database[{0}], Exception Message[{1}].",
                                database, ex.Message));
            }
            finally
            {
                //close the database connections
                if (sqlReader != null)
                    sqlReader.Close();

                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }


            #endregion

           return dropDownData;
        }

        /// <summary>
        /// Updates dropdown data script generation sync count values
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        /// <param name="syncCountType">ServerSyncCountType enum value </param>
        internal void UpdateSyncCount(DropDownLocationType dropDownLocationType, ServerSyncCountType syncCountType)
        {
            try
            {
                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_UpdateSyncCount));

                // Build up the sql request
                Hashtable parameters = new Hashtable();
                parameters.Add("@DropDownType", dropDownLocationType.ToString());
                parameters.Add("@SyncType", (int)syncCountType);

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(SP_UpdateSyncCount, parameters);

            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_UpdateSyncCount, sqlEx.Message));
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to update sync count for {0} dropdown in the database[{1}], Exception Message[{2}].",
                                dropDownLocationType, database, ex.Message));
                throw ex;
            }
            #endregion
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        /// <summary>
        /// Updates dropdown data script generation sync count values
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        /// <param name="syncCountType">ServerSyncCountType enum value </param>
        internal void UpdateChangeNotification(DropDownLocationType dropDownLocationType, ChangeNotificationType notificationType)
        {
            try
            {
                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_UpdateChangeNotification));

                // Build up the sql request
                Hashtable parameters = new Hashtable();
                parameters.Add("@DropDownType", dropDownLocationType.ToString());
                parameters.Add("@NotificationType", (int)notificationType);

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(SP_UpdateChangeNotification, parameters);

            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_UpdateChangeNotification, sqlEx.Message));
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to update change notification for {0} dropdown in the database[{1}], Exception Message[{2}].",
                                dropDownLocationType, database, ex.Message));
                throw ex;
            }
            #endregion
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        /// <summary>
        ///  Gets dropdown data script generation sync count values based on ServerSyncCountType enum value
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        /// <param name="syncCountType">ServerSyncCountType enum value</param>
        /// <returns></returns>
        internal int GetServerSyncCount(DropDownLocationType dropDownLocationType, ServerSyncCountType syncCountType)
        {

            int syncCount = -1;

            try
            {

                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_GetServerSyncCount));

                // Build up the sql request
                Hashtable parameters = new Hashtable();
                parameters.Add("@DropDownType", dropDownLocationType.ToString());
                parameters.Add("@SyncType", (int)syncCountType);

                sqlHelper.ConnOpen(database);

                syncCount = (int)sqlHelper.GetScalar(SP_GetServerSyncCount, parameters);

            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_GetServerSyncCount, sqlEx.Message));
                
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to get sync count for {0} dropdown from the database[{1}], Exception Message[{2}].",
                                dropDownLocationType,database, ex.Message));
                
            }
            #endregion
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            return syncCount;
            
        }

        
        /// <summary>
        /// Resets web server sync status 
        /// </summary>
        /// <param name="dropDownLocationType"></param>
        internal void ResetServerSyncStatus(DropDownLocationType dropDownLocationType)
        {
            try
            {
                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_ResetServerSyncStatus));
                
                // Build up the sql request
                Hashtable parameters = new Hashtable();
                parameters.Add("@DropDownType", dropDownLocationType.ToString());
                
                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(SP_ResetServerSyncStatus, parameters);

            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_ResetServerSyncStatus, sqlEx.Message));

                throw sqlEx;

            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Drop Down Gaz - Error occurred attempting to reset Sync status for {0} dropdown in the database[{1}], Exception Message[{2}].",
                                dropDownLocationType, database, ex.Message));
                throw ex;
            }
            #endregion
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        #endregion

        #region PropertyProvider properties
        /// <summary>
        /// Wrapper method to provide value of the NumberOfVersionsToRetain property in property provider
        /// </summary>
        /// <returns></returns>
        internal int NumberOfVersionsToRetain
        {
            get
            {
                int numberOfVersionsToRetain = 3;
                if (!int.TryParse(Properties.Current["DropDownGaz.Data.NumberOfVersionsToRetain"], out numberOfVersionsToRetain))
                {
                    numberOfVersionsToRetain = 3;
                }

                return numberOfVersionsToRetain;
            }
        }

        // <summary>
        /// Wrapper method to provide value of the Number of Web Server Worker Processes count for the
        /// environment
        /// </summary>
        /// <returns></returns>
        internal int WebServerCount
        {
            get
            {
                int WebServerCount = 4;
                if (!int.TryParse(Properties.Current["DropDownGaz.WebServer.WorkerProcesses.Count"], out WebServerCount))
                {
                    WebServerCount = 4;

                    Log(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                        "Drop Down Gaz - Error occurred attempting to parse Property[DropDownGaz.WebServer.WorkerProcesses.Count] into an int, Count has been set to 4.");
                }

                return WebServerCount;
            }
        }

        /// <summary>
        /// Wrapper method to provide value of the DropDownGaz data Fileparts property based on the dropdownlocation type provided
        /// </summary>
        /// <param name="ddlType">type of the drop down location</param>
        /// <returns></returns>
        internal int DropDownDataFileParts(DropDownLocationType ddlType)
        {
            int dataParts = 3;
            if (!int.TryParse(Properties.Current[string.Format("DropDownGaz.{0}.Data.Fileparts", ddlType)], out dataParts))
            {
                dataParts = 3;
            }

            return dataParts;
        }

        /// <summary>
        /// Wrapper method to provide value of the DropDownData file name based on the dropdownlocation type provided
        /// </summary>
        /// <param name="ddlType"></param>
        /// <returns></returns>
        internal string GetDropDownDataFileName(DropDownLocationType ddlType)
        {
            return Properties.Current[string.Format("DropDownGaz.{0}.Data.Filename", ddlType)];
        }

        /// <summary>
        /// Determines if the auto-suggest functionality is enabled
        /// </summary>
        /// <returns>true if the auto-suggest dropdown functionality is enabled</returns>
        internal bool IsDropDownEnabled()
        {
            bool enabled = false;

            if (!bool.TryParse(Properties.Current["DropDownGaz.Available"], out enabled))
            {
                enabled = false;
            }

            return enabled;
        }

        /// <summary>
        /// Determins whether auto-suggest functionality should be enabled for perticular page identify using pageId
        /// </summary>
        /// <param name="pageId">PageId enum value identifying a specific page</param>
        /// <returns></returns>
        internal bool IsDropDownEnabled(PageId pageId)
        {
            bool enabled = false;

            if (!bool.TryParse(Properties.Current[string.Format("DropDownGaz.{0}.Available",pageId)], out enabled))
            {
                enabled = false;
            }

            return enabled;
        }


        #endregion

        #region Error Logging
        /// <summary>
        /// Logs messages for event category and trace level specified
        /// </summary>
        /// <param name="eventCategory">TD event category enum value</param>
        /// <param name="traceLevel">TD trace level enum value</param>
        /// <param name="message"></param>
        internal void Log(TDEventCategory eventCategory, TDTraceLevel traceLevel,string message)
        {
            Logger.Write(new OperationalEvent(eventCategory, traceLevel, message));
        }

        /// <summary>
        /// Logs error messages which can be pick up by TNG
        /// </summary>
        /// <param name="eventCategory"></param>
        /// <param name="message"></param>
        internal void LogTNGError(TDEventCategory eventCategory, TNGAlert alertType, string message)
        {
            // Get the TNG Alert string
            string logMsg = Properties.Current["DropDownGaz.Alert.TNGMonitoring"];

            switch (alertType)
            {
                case TNGAlert.ImportFailed:
                    logMsg = string.Format(logMsg,
                                Properties.Current["DropDownGaz.Alert.TNGMonitoring.DataImportFailed"]);
                    break;
                case TNGAlert.CreateFailed:
                    logMsg = string.Format(logMsg,
                                Properties.Current["DropDownGaz.Alert.TNGMonitoring.DataFileCreateFailed"]);
                    break;
                case TNGAlert.FileMissing:
                    logMsg = string.Format(logMsg,
                                Properties.Current["DropDownGaz.Alert.TNGMonitoring.DataFileMissing"]);
                    break;
            }
            
            // Append the actual error message
            logMsg += message;

            Logger.Write(new OperationalEvent(eventCategory, TDTraceLevel.Error, logMsg));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Does a null check for the data reader column value and returns empty string if found null 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public string GetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        #endregion



    }
}
