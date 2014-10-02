// *********************************************** 
// NAME                 : CarParkImportTask.cs
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 9 March 2006 
// DESCRIPTION  		: Imports a Car Park XML file to the database using a SQL Server 
//							stored procedure and the DataGateway
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkImportTask.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:02   mturner
//Initial revision.
//
//   Rev 1.2   Mar 15 2006 14:05:30   tolomolaiye
//Modified exception identifiers
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.1   Mar 10 2006 16:41:04   tolomolaiye
//Further updates for Park and Ride II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.0   Mar 09 2006 12:08:52   tolomolaiye
//Initial revision.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Car Park ImportTask Class that receives and processes an XML Car park file
	/// </summary>
	public class CarParkImportTask : DatasourceImportTask  
	{
		private const string propertyKey = "datagateway.sqlimport.carpark.Name"; 
		private const string propertyFeedName = "datagateway.sqlimport.carpark.feedname";
		private string importPropertyName = string.Empty;				
		private const string logDescription = "Car Park Import" ;
		private bool validFeedname = true;		
		private string importFilename = string.Empty ;		

		#region Constructor
		/// <summary>
		/// Default contructor 
		/// </summary>
		/// <param name="feed">Import feedname</param>
		/// <param name="params1">Addition information1</param>
		/// <param name="params2">Addition information1</param>
		/// <param name="utility">Utility to run</param>
		/// <param name="processingDirectory">Directory location of Gateway processing directory</param>
		public CarParkImportTask
			(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			importPropertyName = Properties.Current[propertyKey]; 				    
			xmlschemaLocationKey = string.Format(CultureInfo.InvariantCulture, xmlschemaLocationKey, importPropertyName);
			xmlNamespaceKey = string.Format(CultureInfo.InvariantCulture, xmlNamespaceKey, importPropertyName);
			databaseKey = string.Format(CultureInfo.InvariantCulture, databaseKey, importPropertyName);
			storedProcedureKey = string.Format(CultureInfo.InvariantCulture, storedProcedureKey, importPropertyName);		

			// Check the feed name
			if ( !dataFeed.Equals(Properties.Current[propertyFeedName] ) ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDescription + " unexpected feed name: [" + dataFeed + "]"));
				validFeedname = false; 				
			}					
		}
		#endregion

		#region Overridden protected methods
		/// <summary>
		/// Override of LogStart method which logs to the log file before data import.
		/// </summary>
		protected override void LogStart()
		{	
			string logDesc = String.Format(CultureInfo.InvariantCulture, "{0} update begun for feed {1}", logDescription, dataFeed); 
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
				logDesc));			
			
		}
		/// <summary>
		/// Override of LogFinish method which logs to the log file after data import.
		/// </summary>
		protected override void LogFinish(int retCode)
		{	
			string logDesc = string.Empty;
			if (retCode != 0) 
			{
				// Log failure							 				
				logDesc = String.Format(CultureInfo.InvariantCulture, "{0} {1} update failed for feed : {2} ", logDescription, retCode.ToString(),  dataFeed.ToString()); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					logDesc));
			}
			else
			{
				// Log success				
				logDesc = String.Format(CultureInfo.InvariantCulture,"{0}  update succeeded for feed {1}", logDescription, dataFeed); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					logDesc));			
			}
		}
		
		/// <summary>
		/// Overriding base method Run 
		/// It converts csv file to xml file and then it callls base Run method
		/// </summary>
		public override int Run(string filename)
		{	
			// initialising returnCode
			int returnCode = 0;			
			string logDesc = string.Empty; 
			try
			{
				importFilename = filename;
				// If conversion is sucessfull then only calling base Run method 
				if (! ConvertToXml()) 
				{
					// check if feedname is valid 
					if (validFeedname)
					{
						// feedname was valid and hence it must have because of invalid schema or data												
						logDesc = String.Format(CultureInfo.InvariantCulture, "Unable to produce xml file from given csv file : {0}  update failed for feed ", importFilename.ToString());   
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							logDesc)  );
						returnCode = (int)TDExceptionIdentifier.CPCsvConversionFailed;						
					}
					else
					{	// returning invalid feedname error number					
						logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found  : {0}  update failed for feed", importFilename.ToString());   
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							logDesc)  );
						returnCode = (int)TDExceptionIdentifier.CPDataFeedNameNotFound; 
					}
					
					// returning error code
					return returnCode;
				}
				else
				{	
					// conversion of csv file to xml file was successful. Call base Run method
					return base.Run(importFilename) ;
				}
			}
			catch (TDException tdex)
			{
				logDesc = String.Format(CultureInfo.InvariantCulture, "TDError occurred while converting csv file to xml  : {0} {1} ", importFilename.ToString(), tdex.Message.ToString());   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					logDesc));								
				returnCode = (int)TDExceptionIdentifier.CPImportFailed;
				return returnCode;
			}
		}
		#endregion
	
		#region Methods
		/// <summary>
		/// Virtual void AddAttributeToXmlDoc() adds attribute to the document 
		/// </summary>
		/// <param name="xmlDoc">Xml document</param>
		/// <param name="attributeName">Name of the attribute</param>
		/// <param name="attributeValue">Value of the attribute</param>
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
			catch(XmlException xEx)
			{
				string logDesc = String.Format(CultureInfo.InvariantCulture,"problem adding attribute {0}", xEx.Message.ToString()); 
				throw new TDException(logDesc ,true, TDExceptionIdentifier.CPCsvConversionFailed); 
			}
		}

		/// <summary>
		/// ConvertToXml() will convert input csv file to xml output file
		/// </summary>
		protected virtual bool ConvertToXml()
		{	  
			StringBuilder fullFilePath = new StringBuilder(); 
			StringBuilder outXmlFileName = new StringBuilder() ;
			StringBuilder strOutXmlFilePath = new StringBuilder(); 			
							
			string strNamespace = @"xmlns"; 
			string strNamespacexs = @"xmlns:xs";			
			XmlDocument xmlDoc = new XmlDocument();
			bool xmlConverted = false;  
			
			System.Uri  myImportFileName;
			Common.DatabaseInfrastructure.XmlCsvReader CsvXmlConverter = new Common.DatabaseInfrastructure.XmlCsvReader();   

			try
			{	
				// if invalid feedname then return false 
				if (!validFeedname)
				{
					return false;
				}
				
				// throw an exception if filename == null 
				if (importFilename == null ) 
				{   
					throw new TDException("No source file found for " + logDescription, true, TDExceptionIdentifier.CPCsvConversionFailed);
				}
					
				// throw an exception if filename length < 4 
				if (importFilename.Length < 4)
				{					
					throw new TDException(logDescription +  ": File name should contain atleast 5 chars", true, TDExceptionIdentifier.CPCsvConversionFailed);
				}
				// get full path 
				fullFilePath = fullFilePath.Append(ProcessingDirectory.ToString()).Append(@"\").Append(importFilename) ; 
							
				// get output file name 
				outXmlFileName = outXmlFileName.Append(importFilename.Substring(0, importFilename.Length - 4).Trim()  + ".xml") ; 
				// getting output file path 
				strOutXmlFilePath = strOutXmlFilePath.Append(ProcessingDirectory.ToString()  + @"\").Append(outXmlFileName.ToString()); 

				myImportFileName = new System.Uri("file://" + fullFilePath.ToString());   
				
				// check if file exists
				if (!File.Exists(fullFilePath.ToString()))
				{
					return false;
				}
				CsvXmlConverter =  new Common.DatabaseInfrastructure.XmlCsvReader(myImportFileName, xmlDoc.NameTable ) ;  
				
				CsvXmlConverter.FirstRowHasColumnNames = true;
				CsvXmlConverter.RootName = importPropertyName;
				CsvXmlConverter.RowName = "CarParkDataImport";
				
				xmlDoc.Load(CsvXmlConverter);				

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been loaded correctly in XmlDocument"));
				
				// Append namespace attribute to the top level element
				AddAttributeToXmlDoc( xmlDoc , strNamespace, (string)Properties.Current["datagateway.sqlimport.carpark.xmlnamespace"]);
								
				// Append namespace:xsi attribute 		
				AddAttributeToXmlDoc(xmlDoc ,strNamespacexs, (string)Properties.Current["datagateway.sqlimport.carpark.xmlnamespacexsi"]);
				
				// Saving output file 
				xmlDoc.Save(strOutXmlFilePath.ToString());
					
				xmlConverted = true;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been converted successfully at: " + strOutXmlFilePath.ToString() ));

				// Assign xml file as input file..
				importFilename = outXmlFileName.ToString();				
							
				return true;
			}
			
			catch (TDException  tdex )
			{	
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					tdex.Message.ToString()   + ": update failed for feed "));				
				return false;
			}
			catch (Exception ex)
			{	// We don't know what exception will be thrown while doing the conversion
				// so we are handling general exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					ex.Message.ToString() + ": update failed for feed "));						
				return false;						    
			}
			finally
			{
				if ( ((CsvXmlConverter.ReadState != ReadState.Closed) &&  (CsvXmlConverter.ReadState == ReadState.EndOfFile)) || (!xmlConverted))
				{	
					// close the csv file
					// Handling general exception as various types of exception can be thrown ranging from
					// process lock to object null reference.  
					try
					{
						CsvXmlConverter.Close(); 
					}
					catch(Exception)
					{  						
					}
				}
				xmlDoc = null;								
				CsvXmlConverter = null;
			}				
		}
		#endregion
	}
}