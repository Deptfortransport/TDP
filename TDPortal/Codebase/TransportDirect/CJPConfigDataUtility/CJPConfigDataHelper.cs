// ***********************************************
// NAME 		: CJPConfigDataHelper.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 30-Jun-2010
// DESCRIPTION 	: CJP config data helper class responsible of encapsulating
//                core functionality for the CJPConfigDataUtility.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CJPConfigDataUtility/CJPConfigDataHelper.cs-arc  $
//
//   Rev 1.0   Jul 01 2010 12:38:42   apatel
//Initial revision.
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.CJPConfigDataUtility
{
    /// <summary>
    ///  CJP config data helper class providing access to database, exporting config file
    /// </summary>
    class CJPConfigDataHelper
    {
        #region Private Fields
        /// <summary>
        /// formatted string to read the location of the config files for each server
        /// </summary>
        private const string FormattedServerkey = "CJPConfigData.ConfigFile.Server{0}";

        // Stored Procedures
        private const string SP_ImportCJPConfigData = "ImportCJPConfigData";
        private const string SP_ExportCJPConfigData = "ExportCJPConfigData";
        private const string SP_DeleteCJPConfigData = "DeleteCJPConfigData";
        

        // Database
        private SqlHelper sqlHelper = new SqlHelper();
        private const SqlHelperDatabase database = SqlHelperDatabase.TransientPortalDB;
        
        #endregion

        #region Internal Methods
        /// <summary>
        /// Exports the config file
        /// Gets the path of the target file from properties
        /// </summary>
        /// <returns></returns>
        internal int ExportConfigFile()
        {
            string targetFile = Properties.Current["CJPConfigData.ConfigFilePath"];

            Console.WriteLine("Begin exproting cjp file  to '{0}'", targetFile);
            
            int status = ExportConfigFile(targetFile);

            return status;
        }

        /// <summary>
        /// Importes the config file properties for each server
        /// </summary>
        /// <returns></returns>
        internal int ImportConfigData()
        {
            int noOfServers = 0;

            try
            {
                // Delete the CJP config data before importing
                DeleteConfigData();

                // Get the no of servers value from the file property xml 
                if (int.TryParse(Properties.Current["CJPConfigData.NoOfServers"], out noOfServers))
                {
                    // iterate for each server
                    for (int i = 0; i < noOfServers; i++)
                    {
                        // Get the value of the config file to import from the file property xml
                        string configFile = Properties.Current[string.Format(FormattedServerkey, i+1)];

                        Console.WriteLine("Begin Importing file '{0}'", configFile);

                        // Import the CJP config property file
                        ImportPropertyFile(configFile);

                        Console.WriteLine("Completed Importing file '{0}'", configFile);
                    }
                }
            }
            catch (Exception ex)
            {
                OperationalEvent oe =
                    new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    "An error has occurred importing CJP config file : " + ex.Message);
                Logger.Write(oe);
                return -1;
            }

            return 0;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates a file stream and reads the xml config file with the provided file path and call database
        /// to import the config file properties in to database
        /// </summary>
        /// <param name="propertyFilePath">File path of the CJP config file</param>
        /// <returns>returns 0 if success</returns>
        private int ImportPropertyFile(string propertyFilePath)
        {
            int status = 0;

            try
            {
                using (FileStream fstream = new FileStream(propertyFilePath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(fstream))
                    {
                        status = ImportConfigDataIntoSQLServer(propertyFilePath, reader.ReadToEnd());
                    }
                }
                
            }
            catch (TDException e)
            { 
                status = -1;
                Log(TDEventCategory.Business, TDTraceLevel.Error,
                        string.Format("Error importing CJP Config property Xml File : {0}",
                            e.StackTrace));
            }

            return status;
        }

        /// <summary>
        /// Import the config data XML into the database. The XML string is passed directly
        /// into a stored procedure. The stored procedure does all the work
        /// extracing the appropriate data from the XML into the database tables.
        /// FileName of the xml is passed to determine from which file property belongs to
        /// </summary>
        /// <param name="XMLString"></param>
        private int ImportConfigDataIntoSQLServer(string fileName, string XMLString)
        {
            int result = 0;
            
            //Open the database connection
            try
            {
                Log(TDEventCategory.Database, TDTraceLevel.Info, "Connecting to " + database + " database.");
                sqlHelper.ConnOpen(database);
            }
            catch (Exception e)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Info, "Import database connection error: " + e.Message);
                result = -1;
            }

            if (result == 0)
            {
                //Call the stored procedure
                try
                {
                    Hashtable hashParams = new Hashtable();
                    hashParams.Add("@FileName", fileName);
                    hashParams.Add("@XML", XMLString);
                    Hashtable hashTypes = new Hashtable();

                    //The parameter must be set as database type of Text otherwise it will not work
                    hashTypes.Add("@FileName", SqlDbType.VarChar);
                    hashTypes.Add("@XML", SqlDbType.Text);

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Calling DatasourceImportTask SP on " + database + " database."));

                    sqlHelper.Execute(SP_ImportCJPConfigData, hashParams, hashTypes);
                    

                }
                catch (SqlException sqlEx)
                {
                    result = -1;
                    Log(TDEventCategory.Business, TDTraceLevel.Error, "Import stored procedure[" + SP_ImportCJPConfigData + "] calling error: " + sqlEx.Message);
                    
                }
                catch (Exception e)
                {
                    result = -1;
                    Log(TDEventCategory.Business, TDTraceLevel.Error, "Import stored procedure[" + SP_ImportCJPConfigData + "] calling error: " + e.Message);
                }
                finally
                {
                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
            }
            
            return result;
        }

        /// <summary>
        /// Deletes CJP Config data from database table
        /// </summary>
        private void DeleteConfigData()
        {
            try
            {
                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_DeleteCJPConfigData));

                // Build up the sql request
                Hashtable parameters = new Hashtable();
                
                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(SP_DeleteCJPConfigData, parameters);

            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_DeleteCJPConfigData, sqlEx.Message));
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Error occurred attempting to delete config data in the database[{0}], Exception Message[{1}].",
                                database, ex.Message));
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
        /// 
        /// </summary>
        /// <param name="targetFile">Filename with </param>
        /// <returns></returns>
        private int ExportConfigFile(string targetFile)
        {
            string xmlString = string.Empty;

            try
            {

                // Log
                Log(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                database, SP_ExportCJPConfigData));

                sqlHelper.ConnOpen(database);

                // Gets the xml reader and build the whole xml
                using(XmlReader reader = sqlHelper.GetXMLReader(SP_ExportCJPConfigData, new Hashtable(),new Hashtable()))
                {
                    StringBuilder xmlBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    while (reader.Read())
                    {
                        xmlBuilder.Append(reader.ReadOuterXml());
                    }

                    xmlString = xmlBuilder.ToString();
                    
                }
            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("SQL Helper error when excuting stored procedure [{0}.{1}], Message:{2}",
                                database, SP_ExportCJPConfigData, sqlEx.Message));
                throw sqlEx;
            }
            catch (Exception ex)
            {
                Log(TDEventCategory.Database, TDTraceLevel.Error,
                            string.Format("Error occurred attempting to export CJP config properties xml file from the database[{0}], Exception Message[{1}].",
                                database, ex.Message));
                throw ex;
            }
            #endregion
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            if (!string.IsNullOrEmpty(xmlString))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                doc.Save(targetFile);
            }
            else
            {
                return -1;
            }

            return 0;
            
        }

        /// <summary>
        /// Logs messages for event category and trace level specified
        /// </summary>
        /// <param name="eventCategory">TD event category enum value</param>
        /// <param name="traceLevel">TD trace level enum value</param>
        /// <param name="message"></param>
        private void Log(TDEventCategory eventCategory, TDTraceLevel traceLevel, string message)
        {
            string logMsg = "CJPConfigData: ";
            if (traceLevel == TDTraceLevel.Error)
            {
                logMsg = "CJPConfigData ERROR: ";
            }
            else if (traceLevel == TDTraceLevel.Warning)
            {
                logMsg = "CJPConfigData WARNING: ";
            }

            Logger.Write(new OperationalEvent(eventCategory, traceLevel, logMsg + message));

        }
        #endregion

    }
}
