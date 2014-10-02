// *********************************************** 
// NAME         : ZonalAccessibilityImportTask.cs
// AUTHOR       : Sanjeev Johal
// DATE CREATED : 13 June 2008
// DESCRIPTION  : Import class for zonal accessibility links
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/ZonalAccessibilityImportTask.cs-arc  $ 
//
//   Rev 1.1   Jul 03 2008 13:27:52   apatel
//change the namespage zonalaccessibility to zonalservices
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 27 2008 09:45:58   apatel
//Initial revision.
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 13 2008 09:16:02   sjohal
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
	/// Class that defines method for Zonal Accessibility import
	/// </summary>
	public class ZonalAccessibilityImportTask : DatasourceImportTask
	{
		#region Error Messages
		private const string errorCsvConversion = "Unable to produce xml file from given csv file: {0} update failed for feed";
		private const string errorCsvTDError = "The following error occurred while converting csv file {0} to xml: {1}";
		private const string errorInvalidFeedName = "Invalid feed name: {0}. Update failed for feed";
		private const string messageCsvLoadSuccessful = "The csv file has been loaded correctly in XmlDocument";

		#endregion

		private const string Accessibility = "Accessibility";
		private const string zonalImport = "Zonal Accessibility Links Import";  
		private string strImportname = "zonalaccessibility";
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
		public ZonalAccessibilityImportTask(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
      		xmlschemaLocationKey = string.Format(CultureInfo.CurrentCulture, xmlschemaLocationKey, strImportname);
			xmlNamespaceKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceKey, strImportname);
			databaseKey = string.Format(CultureInfo.CurrentCulture, databaseKey, strImportname);
			storedProcedureKey = string.Format(CultureInfo.CurrentCulture, storedProcedureKey, strImportname);		

			if ( !dataFeed.Equals(Properties.Current["datagateway.sqlimport.zonalaccessibility.feedname"] ) ) 
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
		public ZonalAccessibilityImportTask(string feed, string processingDirectory) 
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
					string errorMessage = "No source file found for Zonal Accessibility Links Import";
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
				CsvXmlConverter.RowName = Accessibility;
				
				xmlDoc.Load(CsvXmlConverter);
				
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, messageCsvLoadSuccessful));
				
				// Now appending namespace attribute to the top level element
				AddAttributeToXmlDoc( xmlDoc , xmlNameSpace, (string)Properties.Current["datagateway.sqlimport.zonalaccessibility.xmlnamespace"]);
				
				// Now appending namespace:xsi attribute 		
				AddAttributeToXmlDoc(xmlDoc ,xmlNamespaceXs, (string)Properties.Current["datagateway.sqlimport.zonalaccessibility.xmlnamespacexsi"]);
				
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
