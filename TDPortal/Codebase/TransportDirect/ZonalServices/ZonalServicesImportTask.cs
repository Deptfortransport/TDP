// *********************************************** 
// NAME         : ZonalServicesImportTask.cs
// AUTHOR       : Tolu Olomolaiye
// DATE CREATED : 12 December 2005
// DESCRIPTION  : Import class for zonal services
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/ZonalServicesImportTask.cs-arc  $ 
//
//   Rev 1.1   Dec 29 2008 17:02:12   RBRODDLE
//Added line to make this feed pick up the sproc timeout to allow this to be configured in the properties DB
//Resolution for 5214: Timeout values not working for some data feeds
//
//   Rev 1.0   Nov 08 2007 13:03:14   mturner
//Initial revision.
//
//   Rev 1.6   Feb 09 2006 13:57:08   jbroome
//FX Cop updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.5   Feb 08 2006 17:01:50   jbroome
//Added LogStart and LogFinish calls in RUN method.
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.4   Feb 06 2006 15:14:08   tolomolaiye
//Code review updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.3   Jan 20 2006 10:23:22   tolomolaiye
//Code review updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.2   Dec 22 2005 11:00:28   tolomolaiye
//Updated variables
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.1   Dec 21 2005 09:31:18   tolomolaiye
//Work in progress
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.0   Dec 16 2005 09:34:02   tolomolaiye
//Initial revision.

using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.ZonalServices
{
	/// <summary>
	/// Class that defines method for Zonal Services import
	/// </summary>
	public class ZonalServicesImportTask : DatasourceImportTask
	{
		#region Error Messages
		private const string errorCsvConversion = "Unable to produce xml file from given csv file: {0} update failed for feed";
		private const string errorCsvTDError = "The following error occurred while converting csv file {0} to xml: {1}";
		private const string errorInvalidFeedName = "Invalid feed name: {0}. Update failed for feed";
		private const string messageCsvLoadSuccessful = "The csv file has been loaded correctly in XmlDocument";

		#endregion

		private const string PlusBus = "PlusBus";
		private const string zonalImport = "Zonal Services Import";  
		private string strImportname = "zonalservices";
		private string csvFileName = string.Empty;	
		private bool boolValidfeedname; 		
		private StringBuilder zonalMessage;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="feed">The datafeed ID</param>
		/// <param name="params1">Parameters passed to the task</param>
		/// <param name="params2">Parameters passed to the task</param>
		/// <param name="utility">External executable used to perform the import if required</param>
		/// <param name="processingDirectory">The directory holding the data while the task is executing</param>
		public ZonalServicesImportTask(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			xmlschemaLocationKey = string.Format(CultureInfo.CurrentCulture, xmlschemaLocationKey, strImportname);
			xmlNamespaceKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceKey, strImportname);
			databaseKey = string.Format(CultureInfo.CurrentCulture, databaseKey, strImportname);
			storedProcedureKey = string.Format(CultureInfo.CurrentCulture, storedProcedureKey, strImportname);
            commandTimeOutKey = string.Format(CultureInfo.InvariantCulture, commandTimeOutKey, strImportname);

			if ( !dataFeed.Equals(Properties.Current["datagateway.sqlimport.zonalservices.feedname"] ) ) 
			{
				zonalMessage = new StringBuilder();
				zonalMessage.Append(zonalImport);
				zonalMessage.Append(" unexpected feed name: [");
				zonalMessage.Append(dataFeed);
				zonalMessage.Append("]");

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					zonalMessage.ToString() ));
			}
			else
			{
				boolValidfeedname = true;
			}
		}

		/// <summary>
		/// Overide constructor
		/// </summary>
		/// <param name="feed">The datafeed ID</param>
		/// <param name="processingDirectory">The directory holding the data while the task is executing</param>
		public ZonalServicesImportTask(string feed, string processingDirectory) 
			: this(feed, string.Empty, string.Empty, string.Empty, processingDirectory)
		{}

		#region Overridden protected methods
		/// <summary>
		/// Override of LogStart method
		/// </summary>
		protected override void LogStart()
		{
			zonalMessage = new StringBuilder();
			zonalMessage.Append(zonalImport);
			zonalMessage.Append(" update begun for feed ");
			zonalMessage.Append(dataFeed);

			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
				zonalMessage.ToString() ));			
		}
		/// <summary>
		/// Override of LogFinish method
		/// </summary>
		protected override void LogFinish(int retCode)
		{
			StringBuilder finishMessage = new StringBuilder();

			if (retCode != 0) 
			{
				// Log failure
				finishMessage.Append(retCode.ToString(CultureInfo.CurrentCulture));
				finishMessage.Append(": ");
				finishMessage.Append(zonalImport);
				finishMessage.Append("  update failed for feed ");
				finishMessage.Append(dataFeed);

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, finishMessage.ToString()));
			}
			else
			{
				// Log success
				finishMessage.Append(zonalImport);
				finishMessage.Append(" update succeeded for feed ");
				finishMessage.Append(dataFeed);

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, finishMessage.ToString()));			
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
			
			System.Uri  myImportFileName;
			XmlCsvReader CsvXmlConverter = new Common.DatabaseInfrastructure.XmlCsvReader();   

			try
			{	
				// check file name is not null 
				if (csvFileName == null ) 
				{
					string errorMessage = "No source file found for Zonal Services Import";
					throw new TDException(errorMessage, false, TDExceptionIdentifier.ZSAddAtrributeFails );
				}
					
				// getting full path 
				fullFilePath = ProcessingDirectory.ToString(CultureInfo.InvariantCulture)  +  @"\" + csvFileName ; 
				
				// getting output file name 
				outputXmlFileName = csvFileName.Substring(0, csvFileName.Length - 4).Trim()  + ".xml" ; 
				// getting output file path 
				outputXmlFilePath =  ProcessingDirectory.ToString(CultureInfo.InvariantCulture)  + @"\" + outputXmlFileName ; 

				myImportFileName = new System.Uri("file://" + fullFilePath);   
				
				// check if file exists
				if (!File.Exists(fullFilePath))
				{
					return false;
				}
				CsvXmlConverter =  new Common.DatabaseInfrastructure.XmlCsvReader(myImportFileName, xmlDoc.NameTable ) ;  
				
				CsvXmlConverter.FirstRowHasColumnNames = true;
				CsvXmlConverter.RootName = strImportname;
				CsvXmlConverter.RowName = PlusBus;
				
				xmlDoc.Load(CsvXmlConverter);
				
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, messageCsvLoadSuccessful));
				
				// Now appending namespace attribute to the top level element
				AddAttributeToXmlDoc( xmlDoc , xmlNameSpace, (string)Properties.Current["datagateway.sqlimport.zonalservices.xmlnamespace"]);
				
				// Now appending namespace:xsi attribute 		
				AddAttributeToXmlDoc(xmlDoc ,xmlNamespaceXs, (string)Properties.Current["datagateway.sqlimport.zonalservices.xmlnamespacexsi"]);
				
				// make sure output file is not read-only 
				if (File.Exists(outputXmlFilePath))
				{
					File.SetAttributes(outputXmlFilePath, FileAttributes.Normal);
				}

				//now save the file
				xmlDoc.Save(outputXmlFilePath);
					
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been converted successfully at: " + outputXmlFilePath.ToString(CultureInfo.InvariantCulture) ));

				// Assigning xml file as input file now..
				csvFileName = outputXmlFileName;				
							
				return true;
			}
			catch (TDException  tdex )
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					tdex.Message.ToString(CultureInfo.InvariantCulture)   + ": " + " update failed for feed "));				
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
				if ( (CsvXmlConverter.ReadState != ReadState.Closed) &&  (CsvXmlConverter.ReadState == ReadState.EndOfFile))
				{CsvXmlConverter.Close(); }
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
		protected virtual void AddAttributeToXmlDoc(XmlDocument xmlDoc, string attributeName , string attributeValue)
		{
			try
			{
				if (xmlDoc!= null)
				{
					// creating a new attribute 
					XmlAttribute Xmlatt = xmlDoc.CreateAttribute(attributeName);
					// assigining a value to attribute 
					Xmlatt.Value = attributeValue;

					// now appending attribute to the collection class
					XmlAttributeCollection attrColl = xmlDoc.DocumentElement.Attributes ; 
					attrColl.Append(Xmlatt);
				}
			}
			catch (TDException tdEx)
			{
				string errorMessage = "Problem adding attribute " + tdEx.Message.ToString(CultureInfo.CurrentCulture);
				throw new TDException(errorMessage, false, TDExceptionIdentifier.ZSAddAtrributeFails );
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
				if (!boolValidfeedname)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
						string.Format(CultureInfo.InvariantCulture, errorInvalidFeedName, dataFeed)));
					returnCode = (int)TDExceptionIdentifier.DGUnexpectedFeedName;
				}
				else
				{
					csvFileName = filename;
					// check if conversion is successful  
					if (! Convert()) 
					{
						returnCode = (int)TDExceptionIdentifier.SNBCsvConversionFailed;
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							string.Format(CultureInfo.InvariantCulture, errorCsvConversion, csvFileName.ToString(CultureInfo.InvariantCulture))));
						returnCode = (int)TDExceptionIdentifier.SNBCsvConversionFailed;					
					}
					else
					{	
						// now call perform task to import data to the database 
						importFile = ProcessingDirectory + "\\" + csvFileName;
						returnCode = PerformTask();
					}
				}
			}
			catch (TDException tdex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					string.Format(CultureInfo.InvariantCulture, errorCsvTDError, csvFileName.ToString(CultureInfo.InvariantCulture), 
					tdex.Message.ToString(CultureInfo.InvariantCulture))));								
				
				returnCode = (int)TDExceptionIdentifier.SNBImportFailed;
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
