// *********************************************** 
// NAME			: AirOperatorImport.cs
// AUTHOR		: Atos Origin
// DATE CREATED	: 24/05/2004
// DESCRIPTION	: Processes the air route summary information genereated as part
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirOperatorImportTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:20   mturner
//Initial revision.
//
//   Rev 1.1   Aug 13 2004 10:29:06   CHosegood
//Now gets XML namespace from properties.
//
//   Rev 1.0   Jun 09 2004 17:41:48   CHosegood
//Initial revision.

using System;
using System.Diagnostics;
using System.Xml;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
    /// Data Gateway import task invoked via the Data Gateway framework(AirScheduleImportTask).
    /// Convert the air operator information (csv) into XML and store it in the database.
	/// </summary>
    public class AirOperatorImportTask : DatasourceImportTask
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AirOperatorImportTask(string feed, string params1, string params2, string utility,
            string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
        {
            xmlschemaLocationKey = string.Format( xmlschemaLocationKey, "airoperators");
            xmlNamespaceKey = string.Format( xmlNamespaceKey, "airoperators");
            databaseKey = string.Format( databaseKey, "airoperators");
            storedProcedureKey = string.Format( storedProcedureKey, "airoperators");

            if ( !dataFeed.Equals( Properties.Current["datagateway.airbackend.airoperator.feedname"] ) ) 
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "AirOperatorImport unexpected feed name: [" + dataFeed + "]"));
                throw new TDException("AirOperatorImport unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.DGUnexpectedFeedName );
            }
        }

        #endregion

        #region Overridden protected methods
        /// <summary>
        /// Override of LogStart method
        /// </summary>
        protected override void LogStart()
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                "Air Schedule update begun for feed "+dataFeed));			
        }

        /// <summary>
        /// Override of LogFinish method
        /// </summary>
        protected override void LogFinish(int retCode)
        {
            if (retCode != 0) 
            {
                // Log failure
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                    retCode.ToString()+ ": Air Schedule update failed for feed " +dataFeed));
            }
            else
            {
                // Log success
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                    "Air Schedule update succeeded for feed " +dataFeed));			
            }
        }
        /// <summary>
        /// Imports the given file into the database.
        /// </summary>
        /// <param name="filename">The XML import file's location path.</param>
        //public override int Run(string file)
        public override int Run( string file )
        {
            int result = 0;
            FileInfo info = new FileInfo( file );

            //Check to see if the file exists
            if ( !info.Exists )
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "AirOperatorImport file [" + file + "] could not be found."));
                throw new TDException("AirOperatorImport import file [" + importFile + "] could not be found.", true, TDExceptionIdentifier.ADFAirOperatorImportFileNotFound );
            }

            string xmlFileLocation = Properties.Current["datagateway.data.live.directory"] + @"\" + info.Name.Substring(0, info.Name.LastIndexOf(".") ) + ".xml";
            if ( TDTraceSwitch.TraceVerbose ) 
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Verbose, "XML output file: " + xmlFileLocation) );
            }

            importFile = file;
            try 
            {
                if ( TDTraceSwitch.TraceVerbose ) 
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Converting csv file: " + file) );
                }
                Convert( xmlFileLocation );
            } 
            catch (Exception e) 
            {
                result = (int) TDExceptionIdentifier.ADFCsvConversionFailed;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Error converting csv file: " + e.Message,e));
            }

            //If successful so far
            if ( result == 0 ) 
            {
                result = base.Run( xmlFileLocation );
            }
            return result;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Converts the csv file pointed at by <code>base.importFile</code> to XML
        /// </summary>
        /// <param name="destination">The full destination path and file to stored the XML to</param>
        private void Convert( string destination ) 
        {
            XmlTextWriter writer = null;
            StreamReader sr = null;

            try 
            {
                writer = new XmlTextWriter ( destination, System.Text.Encoding.UTF8 );

                //Use indenting for readability.
                writer.Formatting = Formatting.Indented;

                //Write the XML delcaration. 
                writer.WriteStartDocument();

                //Write the root element
                writer.WriteStartElement( "airoperators" );
                writer.WriteAttributeString("xmlns", @Properties.Current[ "datagateway.sqlimport.airoperators.xmlnamespace"] );

                if ( TDTraceSwitch.TraceVerbose ) 
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Opening stream: " + importFile) );
                }
                sr = new StreamReader( importFile );

                //For each entry in the .csv added it as an <operator/> to the XML file
                while ( sr.Peek() >= 0 ) 
                {
                    string line = sr.ReadLine();
                    string[] op = line.Split( ',' );
                    if ( TDTraceSwitch.TraceVerbose )
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Converting " + line + " to XML element") );
                    }

                    writer.WriteStartElement("operator");
                    writer.WriteAttributeString("code", op[0] );
                    writer.WriteAttributeString("name", op[1] );
                    writer.WriteEndElement();
                }

                if ( TDTraceSwitch.TraceVerbose ) 
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Closing stream: " + importFile) );
                }

                //Close the stream
                sr.Close();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            } 
            catch (Exception e ) 
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, "Error converting csv file: " + e.Message,e));
                throw new TDException( "Error converting csv file", e, true, TDExceptionIdentifier.ADFCsvConversionFailed );
            }
            finally 
            {
                //if the stream is still open, close it!
                if ( sr != null ) 
                {
                    sr.Close();
                }

                //If the writer is still open close it.
                if ( writer != null ) 
                {
                    writer.Close();
                }
            }
        }
        #endregion
    }
}