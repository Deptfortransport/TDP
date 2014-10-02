// ************************************************ 
// NAME                 : DropDownLocationImporter.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : DropDown location importer class
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownLocationImporter.cs-arc  $
//
//   Rev 1.3   Sep 04 2012 15:01:14   mmodi
//Updated to execute batch file, required for the LocationSuggest functionality
//Resolution for 5838: Gaz - Updates required for Production setup
//
//   Rev 1.2   Jun 21 2010 16:55:12   mmodi
//Updated logging and added TNG monitor log message
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.1   Jun 10 2010 12:22:04   mmodi
//Added importer code
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.0   Jun 04 2010 11:27:32   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

using Logger = System.Diagnostics.Trace;
using System.Diagnostics;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// DropDownLocation importer class
    /// </summary>
    public class DropDownLocationImporter : DatasourceImportTask
    {
        #region Private members

        #region Error Messages

        private const string errorCsvConversion = "Unable to produce xml file from given csv file: {0} update failed for feed";
        private const string errorCsvTDError = "The following error occurred while converting csv file {0} to xml: {1}";
        private const string errorInvalidFeedName = "Invalid feed name: {0}. Update failed for feed";
        private const string messageCsvLoadSuccessful = "The csv file has been loaded correctly in XmlDocument";

        #endregion

        private const string importName = "dropdownalias";
        private const string importDescription = "Drop Down Alias Import";

        private string parameter1 = string.Empty;
        private string csvFileName = string.Empty;
        private string csvToXmlRowName = "Alias";
        private bool validFeedname;
        private StringBuilder message;
        
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
        public DropDownLocationImporter(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
            xmlschemaLocationKey = string.Format(CultureInfo.CurrentCulture, xmlschemaLocationKey, importName);
            xmlNamespaceKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceKey, importName);
            xmlNamespaceXsiKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceXsiKey, importName);
            databaseKey = string.Format(CultureInfo.CurrentCulture, databaseKey, importName);
			storedProcedureKey = string.Format(CultureInfo.CurrentCulture, storedProcedureKey, importName);
            commandTimeOutKey = string.Format(CultureInfo.CurrentCulture, commandTimeOutKey, importName);

            // Store the parameter in the class
            parameter1 = params1;

			if ( !dataFeed.Equals(Properties.Current[string.Format("datagateway.sqlimport.{0}.feedname", importName)] ) ) 
			{
                message = new StringBuilder();
                message.Append(importDescription);
                message.Append(" unexpected feed name: [");
                message.Append(dataFeed);
                message.Append("]");

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message.ToString()));
			}
			else
			{
				validFeedname = true;
			}
        }

        #endregion

        #region Overridden protected methods

        /// <summary>
        /// Override of LogStart method
        /// </summary>
        protected override void LogStart()
        {
            message = new StringBuilder();
            message.Append(importDescription);
            message.Append(" update begun for feed ");
            message.Append(dataFeed);

            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, message.ToString()));
        }

        /// <summary>
        /// Override of LogFinish method
        /// </summary>
        protected override void LogFinish(int retCode)
        {
            message = new StringBuilder();

            if (retCode != 0)
            {
                // Log failure
                message.Append(retCode.ToString(CultureInfo.CurrentCulture));
                message.Append(": ");
                message.Append(importDescription);
                message.Append("  update failed for feed ");
                message.Append(dataFeed);

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message.ToString()));

                // Log a TNG monitoring error
                DropDownLocationHelper helper = new DropDownLocationHelper();
                helper.LogTNGError(TDEventCategory.Business, TNGAlert.ImportFailed, message.ToString());
            }
            else
            {
                // Log success
                message.Append(importDescription);
                message.Append(" update succeeded for feed ");
                message.Append(dataFeed);

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, message.ToString()));
            }
        }
        #endregion

        #region Run and Convert methods
        /// <summary>
        /// Converts values in the csv file to xml format
        /// </summary>
        /// <returns>true if conversion is successful, false otherwise</returns>
        private bool Convert()
        {
            string fullFilePath = string.Empty;
            string outputXmlFileName = string.Empty;
            string outputXmlFilePath = string.Empty;

            string xmlNameSpace = @"xmlns";
            string xmlNamespaceXs = @"xmlns:xs";
            XmlDocument xmlDoc = new XmlDocument();

            System.Uri myImportFileName;
            XmlCsvReader CsvXmlConverter = new Common.DatabaseInfrastructure.XmlCsvReader();

            try
            {
                // check file name is not null 
                if (csvFileName == null)
                {
                    string errorMessage = string.Format("No source file found for {0}", importDescription);
                    throw new TDException(errorMessage, false, TDExceptionIdentifier.DDGCsvConversionFailed);
                }

                // getting full path 
                fullFilePath = ProcessingDirectory.ToString(CultureInfo.InvariantCulture) + @"\" + csvFileName;

                // getting output file name 
                outputXmlFileName = csvFileName.Substring(0, csvFileName.Length - 4).Trim() + ".xml";
                // getting output file path 
                outputXmlFilePath = ProcessingDirectory.ToString(CultureInfo.InvariantCulture) + @"\" + outputXmlFileName;

                myImportFileName = new System.Uri("file://" + fullFilePath);

                // check if file exists
                if (!File.Exists(fullFilePath))
                {
                    return false;
                }
                CsvXmlConverter = new Common.DatabaseInfrastructure.XmlCsvReader(myImportFileName, xmlDoc.NameTable);

                CsvXmlConverter.FirstRowHasColumnNames = true;
                CsvXmlConverter.RootName = importName;
                CsvXmlConverter.RowName = csvToXmlRowName;

                xmlDoc.Load(CsvXmlConverter);

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, messageCsvLoadSuccessful));

                // Now appending namespace attribute to the top level element
                AddAttributeToXmlDoc(xmlDoc, xmlNameSpace, (string)Properties.Current[xmlNamespaceKey]);

                // Now appending namespace:xsi attribute 		
                AddAttributeToXmlDoc(xmlDoc, xmlNamespaceXs, (string)Properties.Current[xmlNamespaceXsiKey]);

                // make sure output file is not read-only 
                if (File.Exists(outputXmlFilePath))
                {
                    File.SetAttributes(outputXmlFilePath, FileAttributes.Normal);
                }

                //now save the file
                xmlDoc.Save(outputXmlFilePath);

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "The csv file has been converted successfully at: " + outputXmlFilePath.ToString(CultureInfo.InvariantCulture)));

                // Assigning xml file as input file now..
                csvFileName = outputXmlFileName;

                return true;
            }
            catch (TDException tdex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    tdex.Message.ToString(CultureInfo.InvariantCulture) + ": " + " update failed for feed "));
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    ex.Message.ToString(CultureInfo.InvariantCulture)));
                return false;
            }
            finally
            {
                if ((CsvXmlConverter.ReadState != ReadState.Closed) && (CsvXmlConverter.ReadState == ReadState.EndOfFile))
                { CsvXmlConverter.Close(); }
                xmlDoc = null;
                CsvXmlConverter = null;
            }
        }

        /// <summary>
        /// Adds attributes to the xml document 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        protected virtual void AddAttributeToXmlDoc(XmlDocument xmlDoc, string attributeName, string attributeValue)
        {
            try
            {
                if (xmlDoc != null)
                {
                    // creating a new attribute 
                    XmlAttribute Xmlatt = xmlDoc.CreateAttribute(attributeName);
                    // assigining a value to attribute 
                    Xmlatt.Value = attributeValue;

                    // now appending attribute to the collection class
                    XmlAttributeCollection attrColl = xmlDoc.DocumentElement.Attributes;
                    attrColl.Append(Xmlatt);
                }
            }
            catch (TDException tdEx)
            {
                string errorMessage = "Problem adding attribute " + tdEx.Message.ToString(CultureInfo.CurrentCulture);
                throw new TDException(errorMessage, false, TDExceptionIdentifier.DDGCsvConversionFailed);
            }
        }

        /// <summary>
        /// Overidden method of the base run method. Validate the source data file and calls the
        /// Convert method to process the data. If conversion is successful the run method
        /// of the base class is called, otherwise the error code is returned
        /// </summary>
        /// <param name="filename">The name of the file containing the csv data to be validated</param>
        /// <returns>integer - return value</returns>
        public override int Run(string filename)
        {
            int returnCode = 0;
                        
            try
            {
                LogStart();

                //go no further if the feedname is invalid
                if (!validFeedname)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                        string.Format(CultureInfo.InvariantCulture, errorInvalidFeedName, dataFeed)));
                    returnCode = (int)TDExceptionIdentifier.DGUnexpectedFeedName;
                }
                else
                {
                    csvFileName = filename;
                    // check if conversion is successful  
                    if (!Convert())
                    {
                        returnCode = (int)TDExceptionIdentifier.DDGCsvConversionFailed;
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                            string.Format(CultureInfo.InvariantCulture, errorCsvConversion, csvFileName.ToString(CultureInfo.InvariantCulture))));
                        returnCode = (int)TDExceptionIdentifier.DDGCsvConversionFailed;
                    }
                    else
                    {
                        // now call perform task to import data to the database 
                        importFile = ProcessingDirectory + "\\" + csvFileName;
                        returnCode = PerformTask();
                    }

                    // Call the batch file if it is required
                    if (returnCode == 0 && !string.IsNullOrEmpty(parameter1))
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
                            string.Format("Running batch file {0}", parameter1)));

                        Process p = new Process();
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.FileName = parameter1;
                        p.Start();
                        p.WaitForExit();
                        returnCode = p.ExitCode;
                    }
                }
            }
            catch (TDException tdex)
            {
                string errorMessage = string.Format(CultureInfo.InvariantCulture, errorCsvTDError, csvFileName.ToString(CultureInfo.InvariantCulture),
                    tdex.Message.ToString(CultureInfo.InvariantCulture));

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, errorMessage));

                // Log TNG monitoring error
                DropDownLocationHelper helper = new DropDownLocationHelper();
                helper.LogTNGError(TDEventCategory.Business, TNGAlert.ImportFailed, errorMessage);
                
                returnCode = (int)TDExceptionIdentifier.DDGImportFailed;
            }
            finally
            {
                LogFinish(returnCode);
            }

            return returnCode;
        }

        #endregion
    }
}
