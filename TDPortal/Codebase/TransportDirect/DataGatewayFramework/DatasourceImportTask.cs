// *********************************************** 
// NAME			: DatasourceImportTask.cs
// AUTHOR		: Atos Origin
// DATE CREATED	: 18/05/2004
// DESCRIPTION	: Import an XML file into a database using a specified
//  stored procedure.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/DatasourceImportTask.cs-arc  $
//
//   Rev 1.4   Jul 02 2010 10:20:14   apatel
//updated to throw TDException and not the .net generic exception when XML validation fails.
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.3   Jun 10 2010 12:18:42   mmodi
//Key to read xml xsi string, and log SqlException error messages
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Feb 18 2009 13:28:04   apatel
//Updated some of the methods from private to protected so it can be used by CarParkingImportTask child class
//Resolution for 5254: Car Parking Importer Update
//
//   Rev 1.1   Jan 05 2009 15:39:42   rbroddle
//Add timeout values for various gateway feeds
//Resolution for 5214: Timeout values not working for some data feeds
//
//   Rev 1.0   Nov 08 2007 12:20:10   mturner
//Initial revision.
//
//   Rev 1.4   Dec 04 2006 13:35:28   tmollart
//Modified to read an optional sql time out value from the properties database to modify comand time out values.
//Resolution for 4282: Car Parking data importer timeouts after 30 seconds and does not complete.
//
//   Rev 1.3   Mar 23 2006 17:59:50   build
//Automatically merged from branch for stream0025
//
//   Rev 1.2.1.0   Mar 15 2006 14:04:20   tolomolaiye
//Modified check for Car Park
//
//   Rev 1.2   Sep 16 2004 14:34:52   jmorrissey
//IR1587 - fixed bug in Run method causing failure of SercoDummy.bat
//
//   Rev 1.1   Jun 07 2004 14:54:20   CHosegood
//Added extra check to see if result is 0 before completing task
//
//   Rev 1.0   Jun 02 2004 11:23:40   CHosegood
//Initial revision.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Inserts XML into the Database
	/// </summary>
    public class DatasourceImportTask : ImportTask
    {
        #region Protected Members
        protected string xmlschemaLocationKey = "datagateway.sqlimport.{0}.schemea";
        protected string xmlNamespaceKey = "datagateway.sqlimport.{0}.xmlnamespace";
        protected string xmlNamespaceXsiKey = "datagateway.sqlimport.{0}.xmlnamespacexsi";
        protected string databaseKey = "datagateway.sqlimport.{0}.database";
        protected string storedProcedureKey = "datagateway.sqlimport.{0}.storedprocedure";
		protected string commandTimeOutKey = "datagateway.sqlimport.{0}.sqlcommandtimeout";

        private string commandDefaultTimeOutKey = "datagateway.default.sqlimport.sqlcommandtimeout";

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">Parameters passed to the task</param>
        /// <param name="params2">Parameters passed to the task</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
        public DatasourceImportTask(string feed, string params1, string params2, string utility,
            string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
        { }
        #endregion

        #region Overridden protected methods
        /// <summary>
        /// Imports the given file into the database.
        /// </summary>
        /// <param name="filename">The XML import file's location path.</param>
        public override int Run(string file)
        {
            int result = 0;
            //Store the file path
            
            importFile = file;

            //Check to see if the file exists
			if (!File.Exists(importFile))
			{
				importFile = ProcessingDirectory + "\\" + file;
			}
			//IR1587 - try again for file existing with processing directory in the path
			if (!File.Exists(importFile))
            {
                result = (int) TDExceptionIdentifier.DGInputXMLFileNotFound;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "File [" + file + "] could not be found."));
                //throw new TDException("DatasourceImportTask import file [" + importFile + "] could not be found.", true, TDExceptionIdentifier.DGInputXMLFileNotFound );
            }
            if ( result == 0 ) 
            {
                //base.Run ends up calling this class's overridden PerformTask method
                //which checks the XML file and imports it into the database
                if (base.Run(importFile) != 0)
                {
                    result = (int) TDExceptionIdentifier.DGDatasourceImportTaskUnexpectedError;
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Call to base class ImportTask failed for file [" + file + "].") );
                    //throw new TDException("DatasourceImportTask call to base class ImportTask failed for file [" + importFile + "].", true, TDExceptionIdentifier.DGDatasourceImportTaskUnexpectedError );
                }
            }

            //If we have reached this point then the import was successful
            return result;
        }

        /// <summary>
        /// Override the base class PerformTask method.
        /// </summary>
        /// <returns></returns>
        protected override int PerformTask()
        {
            int result = 0;

            //Extract the XML string from the file
            string xmlString = LoadXML();

            //Validate the XML file against the XSD schema specified in the XML file
            ValidateXML();

            //Update the xml to remove attributes that SQL Server will get confused by
            xmlString = UpdateXML(xmlString);

            //If the process has been successful so far.
            if ( result == 0 ) 
            {
                //Upload the XML into the database
                result = ImportDataIntoSQLServer(xmlString);
            }

            //If we have reached this point then the import was successful
            return result;
        }

        #endregion

        #region Private behaviours
        /// <summary>
        /// Extracts the XML string from the given file.
        /// </summary>
        /// <returns></returns>
        protected string LoadXML()
        {
            if ( TDTraceSwitch.TraceVerbose ) 
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Verbose, "Opening stream to XML file [" + importFile + "]" ) );
            }

            string xmlString = string.Empty;
            try 
            {
                StreamReader streamReader = new StreamReader( importFile );
                xmlString = streamReader.ReadToEnd();
                streamReader.Close();
            } 
            catch ( Exception e ) 
            {
                //Log the error which occurred while reading the XML stream
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error reading import file [" + importFile + "] occurred."));
                throw new TDException("XML parser error: " + e.Message, true, TDExceptionIdentifier.DGInvalidXMLToInput );
            }

            return xmlString;
        }
        /// <summary>
        /// Validates the XML against the XSD schema which should
        /// reside in the same path as the actual XML file.
        /// </summary>
        /// <returns></returns>
        protected void ValidateXML()
        {
            try
            {
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Setting up XML validation engine"));
                }

                string schemaLocation = Properties.Current[xmlschemaLocationKey];
                string xmlNS = Properties.Current[xmlNamespaceKey];

                if (string.Empty.Equals(schemaLocation))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML schema file not specified in Properties"));
                    throw new TDException("XML schema file not specified in Properties", true, TDExceptionIdentifier.DGXMLSchemaFileUnspecified);
                }
                if (string.Empty.Equals(xmlNS))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML namespace not specified in Properties"));
                    throw new TDException("XML namespace not specified in Properties", true, TDExceptionIdentifier.DGXMLNamespaceUnspecified);
                }

                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(xmlNS, schemaLocation);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemaSet;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);

                XmlReader xmlValidator = XmlReader.Create(importFile, settings);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Validating XML "));
                }

                //Chek the XML document
                while (xmlValidator.Read())
                {
                    if (xmlValidator.NodeType == XmlNodeType.EntityReference)
                    {
                        xmlValidator.ResolveEntity();
                    }
                }

                if (xmlValidator != null)
                    xmlValidator.Close();
            }
            catch (Exception e)
            {
                //The following line is correct as any parsing errors are raised in the
                //validation event handler, so the error needs to be simply raised up.
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML schema validation failed"));
                throw new TDException("XML schema validation failed", e, true, TDExceptionIdentifier.DGSchemaValidationFailed);
            }
        }

        /// <summary>
        /// Any anomalies found in the XML document will be raised through here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            //Cause an error to let the caller know that the XML file is invalid
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML parser error: " + args.Message));
            throw new TDException("XML parser error: " + args.Message, true, TDExceptionIdentifier.DGInvalidXMLToInput );
        }

        /// <summary>
        /// Removes the first occurance of an attribute from an XML string
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        private string RemoveAttribute(string xmlString, string attributeName)
        {
            int start = xmlString.IndexOf(attributeName + "=\"");

            if (start > -1)
            {
                int end = xmlString.IndexOf("\"", start + attributeName.Length + 2);
                return xmlString.Substring(0, start - 1) + xmlString.Substring(end + 1);
            }

            return xmlString;
        }
        /// <summary>
        /// Update the xml to remove attributes that will confuse SQL Server
        /// This must be done with string manipulation because the XML DOM will not
        /// allow the removal of the xmlns attribute
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        protected string UpdateXML(string xmlString)
        {
            string ret = xmlString;
            ret = RemoveAttribute(ret, "xmlns");
            ret = RemoveAttribute(ret, "xsi:schemaLocation");
			ret = RemoveAttribute(ret, "xmlns:xs");
            return ret;
        }
        
        /// <summary>
        /// Import the XML into the database. The XML string is passed directly
        /// into a stored procedure. The stored procedure does all the work
        /// extracing the appropriate data from the XML into the database tables.
        /// </summary>
        /// <param name="XMLString"></param>
        protected int ImportDataIntoSQLServer(string XMLString)
        {
            int result = 0;
            int commandTimeOut = -1;

            SqlHelperDatabase datasource;
            string database = Properties.Current[databaseKey];
            string storedProcedureName = Properties.Current[ storedProcedureKey ];

			// Assess if a command time out needs to be used - if not the DB instance default will be used
			if (Properties.Current[commandTimeOutKey] != null)
			{
                //use import specific timeout if present
				commandTimeOut = int.Parse(Properties.Current[commandTimeOutKey]);
			}
            else if (Properties.Current[commandDefaultTimeOutKey] != null)
            {
                //alternatively use the generic property for all gateway imports if present
                commandTimeOut = int.Parse(Properties.Current[commandDefaultTimeOutKey]);
            }

            if ( string.Empty.Equals( database ) ) 
            {
                result = (int) TDExceptionIdentifier.DGStoredProcedureUnspecified;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Database not specified in Properties"));
            }

            if ( ( result == 0 ) && (string.Empty.Equals( storedProcedureName ) ) )
            {
                result = (int) TDExceptionIdentifier.DGDatabaseUnspecified;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Stored procedure not specified in Properties"));
            }

            if ( result == 0 ) 
            {
                datasource = (SqlHelperDatabase) Enum.Parse( typeof(SqlHelperDatabase), database, true );

                //Object for managing database calling
                SqlHelper sqlHelper = new SqlHelper();

                //Open the database connection
                try
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Connecting to " + datasource.ToString() + " database."));
                    sqlHelper.ConnOpen( datasource );
                }
                catch (Exception e)
                {
                    result = (int)TDExceptionIdentifier.DGImportDBConnectionError;
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Import database connection error: " + e.Message));
                }

                if ( result == 0 ) 
                {
                    //Call the stored procedure
                    try
                    {
                        Hashtable hashParams = new Hashtable();
                        hashParams.Add("@XML", XMLString);
                        Hashtable hashTypes = new Hashtable();

                        //The parameter must be set as database type of Text otherwise it will not work
                        hashTypes.Add("@XML", SqlDbType.Text);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            IDictionaryEnumerator myEnumerator = hashParams.GetEnumerator();
                            while (myEnumerator.MoveNext())
                                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                                    TDTraceLevel.Verbose, "Element:: " + string.Format("\t{0}:\t{1}", myEnumerator.Key, myEnumerator.Value)));
                        }

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Calling DatasourceImportTask SP on " + database + " database."));

                        // Execute one type of command if the command time out is set or
                        // the overridden method if its not
                        if (commandTimeOut == -1)
                        {
                            sqlHelper.Execute(storedProcedureName, hashParams, hashTypes);
                        }
                        else
                        {
                            sqlHelper.Execute(storedProcedureName, hashParams, hashTypes, commandTimeOut);
                        }

                    }
                    catch (SqlException sqlEx)
                    {
                        result = (int)TDExceptionIdentifier.DGImportStoredProcedureError;
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Import stored procedure[" + storedProcedureName + "] calling error: " + sqlEx.Message));
                        throw new TDException("Import stored procedure[" + storedProcedureName + "] calling error: " + sqlEx.Message, sqlEx, true, TDExceptionIdentifier.DGImportStoredProcedureError);
                    }
                    catch (Exception e)
                    {
                        result = (int)TDExceptionIdentifier.DGImportStoredProcedureError;
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Import stored procedure[" + storedProcedureName + "] calling error: " + e.Message));
                        throw new TDException("Import stored procedure[" + storedProcedureName + "] calling error: " + e.Message, e, true, TDExceptionIdentifier.DGImportStoredProcedureError);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
