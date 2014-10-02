// *********************************************** 
// NAME                 : CoachRoutesQuotaFaresImportTask.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 04/10/2005 
// DESCRIPTION  		: This class initially converts two csv files into xml files and 
//						  then imports the data into the tables. 
//						  Whenever the new data is processed successfully, the old data in the table will be flushed or truncated.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/CoachRoutesQuotaFaresImportTask.cs-arc  $ 
//
//   Rev 1.1   Dec 05 2012 14:08:30   mmodi
//Removed unused method causing compiler warning 
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:36:40   mturner
//Initial revision.
//
//   Rev 1.2   Jan 18 2006 18:16:30   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.1   Jan 17 2006 17:44:54   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:42   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//


using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Data;
using System.Text;
using System.Data.SqlClient;  
using System.Collections;
using System.Globalization;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common ;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure ;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	// This class initially converts two csv files into xml files and 
	// then imports the data into the tables. 
	// Whenever the new data is processed successfully, the old data in the table will be flushed or truncated.
	/// </summary>
	public class CoachRoutesQuotaFaresImportTask :DatasourceImportTask    
	{   	
		#region Class Variables
		private string strImportname;
		private string  additionalImportName;				
		private string strLogdesc;
		private bool boolValidfeedname = true;		
		private string _filename;	
		private string additionalFileName;
		private string outputXmlFileName;
		private string outputAdditionalXmlFileName;		
		private string primaryXmlschemaLocationKey;
		private string additionalXmlschemaLocationKey;
		private string primaryXmlNamespaceKey;
		private string additionalXmlNamespaceKey;
		private string primaryStoredProcedureKey;
		private string additionalStoredProcedureKey;
		private bool quotaFileExists;
		private string feedname;

		private readonly int fileNameLength = 255;

		#endregion


		#region Constructor
		/// <summary>
		/// The contructor for CoachRoutesQuotaFaresImportTask which takes feedname and imports the data into tables. 
		/// </summary>
		/// <param name="feed">Import feedname</param>
		/// <param name="params1">Addition information1</param>
		/// <param name="params2">Addition information1</param>
		/// <param name="utility">Utility to run</param>
		/// <param name="processingDirectory">Directory location of Gateway processing directory</param>
		public CoachRoutesQuotaFaresImportTask
			(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{   			
			// giving some description for logging purpose
			strLogdesc = "Coach Routes and Quota Fares Import";
			
			strImportname =	Properties.Current["datagateway.sqlimport.coachroutesquotafares.Name"]; 
			
			additionalImportName = Properties.Current["datagateway.sqlimport.quotafares.Name"];
			
			primaryXmlschemaLocationKey = string.Format(CultureInfo.InvariantCulture, xmlschemaLocationKey, strImportname);
			additionalXmlschemaLocationKey = string.Format(CultureInfo.InvariantCulture, xmlschemaLocationKey, additionalImportName);
			
			primaryXmlNamespaceKey = string.Format(CultureInfo.InvariantCulture, xmlNamespaceKey, strImportname);
			additionalXmlNamespaceKey = string.Format(CultureInfo.InvariantCulture, xmlNamespaceKey, additionalImportName);
			
			databaseKey = string.Format(CultureInfo.InvariantCulture, databaseKey, strImportname);
			
			primaryStoredProcedureKey = string.Format(CultureInfo.InvariantCulture, storedProcedureKey, strImportname);		
			additionalStoredProcedureKey = string.Format(CultureInfo.InvariantCulture, storedProcedureKey, additionalImportName); 

			// Now checking the feedname is same or not??
			feedname = Properties.Current["datagateway.sqlimport.coachroutesquotafares.feedname"];
			if ( !dataFeed.Equals(feedname)) 
			{   string logDesc = string.Format(CultureInfo.InvariantCulture, "{0} unexpected feed name: [{1}]", strLogdesc, feedname + " " + dataFeed );    
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc));
				boolValidfeedname = false; 				
			}					
		}

		#endregion



		#region Overridden protected methods
		/// <summary>
		/// Override of LogStart method which logs to the log file before data import.
		/// </summary>
		protected override void LogStart()
		{	
			string logDesc = String.Format(CultureInfo.InvariantCulture, "{0} update begun for feed {1}", strLogdesc, dataFeed); 
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, logDesc));			
			
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
				logDesc = String.Format(CultureInfo.InvariantCulture, "{0} {1} update failed for feed : {2} ", strLogdesc, retCode.ToString(CultureInfo.InvariantCulture),  dataFeed); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc));
			}
			else
			{
				// Log success				
				logDesc = String.Format(CultureInfo.InvariantCulture,"{0}  update succeeded for feed {1}", strLogdesc, dataFeed); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, logDesc));			
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
				_filename = filename;
			
				// Checking feedname
				if (!boolValidfeedname)
				{
					// log the exception	and return the error
					logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found for file : {0} ", filename.ToString());   
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc));								
					return (int)TDExceptionIdentifier.SBPDataFeedNameNotFound; 
				}

				// Now performing first import for CoachRoutes data
				returnCode = ConvertPrimaryFileToXml(strImportname, filename);
				if (returnCode!=0 )
				{
					// raise the exception	and return the error
					logDesc = String.Format(CultureInfo.InvariantCulture, "TDError occurred while converting csv file to xml  : {0} ", filename.ToString());   
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc));								
					return returnCode; 
				}
				
				// Now performing additional import for QuoteFares data				
				additionalFileName = Properties.Current["datagateway.sqlimport.coachroutesquotafares.additionalFileName"];
				returnCode = ConvertQuotaFaresToXml(additionalImportName, additionalFileName);
								
				if (returnCode!= 0) 
				{
					return returnCode;
				}
				
				// instead of calling base.Run() , we can call Perform task strainght away
				return PerformTask();
				
			}
			catch (TDException tdex)
			{
				logDesc = String.Format(CultureInfo.InvariantCulture, "TDError occurred while converting csv file to xml  : {0} {1} ", _filename, tdex.Message);   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,	logDesc, tdex));												
				return (int)TDExceptionIdentifier.SBPImportFailed;    				 
			}
			catch (Exception ex)
			{
				// handling general exception here 
				logDesc = String.Format(CultureInfo.InvariantCulture, "General error occurred while converting csv file to xml  : {0} {1} ", _filename, ex.Message);   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc, ex));												
				return (int)TDExceptionIdentifier.SBPImportFailed;    				
			}
		}

		
		/// <summary>
		/// Override the base class PerformTask method.
		/// </summary>
		/// <returns>status as integer</returns>
		protected override int PerformTask()
		{ 
			int result;
			// Getting the import
			result = CoachRoutesImport(); 
			// reurning the status	
			return result;			
		} 
		
		#endregion

        			
		#region Methods
		

		/// <summary>
		/// Overloaded method.
		/// It converts the given data file to a valid Xml string 
		/// </summary>
		/// <param name="importName">The input data file name</param>
		/// <param name="importType">Indicatiing whether its primary or additional import</param>
		/// <param name="filename">The name of the data file.</param>
		/// <returns>data as Xml string </returns>
		protected string ConvertToXml(string importName, CQImportType importType, string filename)
		{				  
			StringBuilder fullFilePath		= new StringBuilder(fileNameLength); 
			StringBuilder outXmlFileName	= new StringBuilder(fileNameLength) ;
			StringBuilder strOutXmlFilePath = new StringBuilder(fileNameLength); 			
							
			string strNamespace		= @"xmlns"; 
			string strNamespacexs	= @"xmlns:xs";			
			XmlDocument xmlDoc = new XmlDocument();
			bool xmlConverted = false;  
			bool fileNotFound = false; 
			
			System.Uri  myImportFileName;
			Common.DatabaseInfrastructure.XmlCsvReader CsvXmlConverter = new Common.DatabaseInfrastructure.XmlCsvReader();   

			try
			{	
				// throw an exception if filename == null 
				if (filename == null ) 
				{   
					if (importType == CQImportType.CoachRoutesImport)
						throw new TDException("No source file found for Primary Data Import", true, TDExceptionIdentifier.SBPCsvConversionFailed);
					else
						throw new TDException("No source file found for Additional Data Import", true, TDExceptionIdentifier.SBPAdditionalCsvConversionFailed);
				}
					
				// throw an exception if filename length < 4 
				if (filename.Length < 4)				
				{	
					if (importType == CQImportType.CoachRoutesImport)
						throw new TDException(strLogdesc +  ": File name should contain atleast 5 chars", true, TDExceptionIdentifier.SBPCsvConversionFailed);
					else
						throw new TDException(strLogdesc +  ": File name should contain atleast 5 chars", true, TDExceptionIdentifier.SBPAdditionalCsvConversionFailed);
                
				}

				// getting full path 				
				fullFilePath = fullFilePath.Append(Path.Combine(ProcessingDirectory.ToString(), filename));
				 
				
				// getting output file name 
				outXmlFileName = outXmlFileName.Append(filename.Substring(0, filename.Length - 4).Trim()  + ".xml") ; 
				
				// getting output file path 				
				strOutXmlFilePath =  strOutXmlFilePath.Append(Path.Combine(ProcessingDirectory.ToString(), outXmlFileName.ToString())); 

				myImportFileName = new System.Uri("file://" + fullFilePath.ToString());   
				
				// check if file exists
				if (!File.Exists(fullFilePath.ToString()))
				{
					fileNotFound = true;
					throw new TDException(String.Format(CultureInfo.InvariantCulture, "File {0} doesn't exist", fullFilePath.ToString()), false, TDExceptionIdentifier.SBPPrimaryFileNotFound);    
				}
				
				CsvXmlConverter =  new Common.DatabaseInfrastructure.XmlCsvReader(myImportFileName, xmlDoc.NameTable ) ;  
				
				CsvXmlConverter.FirstRowHasColumnNames = true;
				CsvXmlConverter.RootName = importName;
				CsvXmlConverter.RowName  = importName + "Import";
				
				xmlDoc.Load(CsvXmlConverter);				

				if	(TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
						string.Format(CultureInfo.InvariantCulture, "[{0}] csv file has been loaded correctly in XmlDocument", importName) ));
				}                       		
				
				// Now appending namespace attribute to the top level element
				string namespaceName = string.Format(CultureInfo.InvariantCulture, "datagateway.sqlimport.{0}.xmlnamespace", importName); 
				AddAttributeToXmlDoc( xmlDoc , strNamespace, (string)Properties.Current[namespaceName]);
								
				// Now appending namespace:xsi attribute 
				string namespaceXsi = string.Format(CultureInfo.InvariantCulture, "datagateway.sqlimport.{0}.xmlnamespacexsi", importName);  
				AddAttributeToXmlDoc(xmlDoc ,strNamespacexs, (string)Properties.Current[namespaceXsi]);
				
				// Saving output file 
				xmlDoc.Save(strOutXmlFilePath.ToString());
					
				xmlConverted = true;

				if	(TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
						string.Format(CultureInfo.InvariantCulture,  " csv file has been converted successfully at: {0}",strOutXmlFilePath)  ));
				}

				// Assigning xml file as input file now..
				return outXmlFileName.ToString();											
				
			}			
			catch (TDException tdEx)
			{
				if (tdEx.Identifier == TDExceptionIdentifier.SBPPrimaryFileNotFound)
				{
					// Handling general exception
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
						string.Format(CultureInfo.InvariantCulture, "{0}: not found  for feed {1}", fullFilePath.ToString(), importName), tdEx));								
					throw new TDException(tdEx.Message, true, tdEx.Identifier); 					
				}
				else
				{
					throw new TDException(String.Format(CultureInfo.InvariantCulture, "File {0} conversion fails", fullFilePath.ToString()), false, TDExceptionIdentifier.SBPCsvConversionFailed);    
				}
			}
			catch (Exception ex)
			{	// As we don't know what exception will be thrown while doing the conversion
				// so handling general exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					string.Format(CultureInfo.InvariantCulture, "{0}: update failed for feed {1} and for file {2}", ex.Message, importName, fullFilePath.ToString()), ex));			

				throw new TDException(String.Format(CultureInfo.InvariantCulture, "File {0} conversion fails", fullFilePath.ToString()), true, TDExceptionIdentifier.SBPCsvConversionFailed);    
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
						if	(!fileNotFound)
						{
							CsvXmlConverter.Close(); 
						}
					}
					catch (Exception ex1)
					{  
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Error closing CsvXmlConvertor", ex1));			
					}
				} 				
				xmlDoc = null;								
				CsvXmlConverter = null; 
			}				
			
		}


				
		/// <summary>
		/// This functions converts the primary file into the xml string
		/// </summary>
		/// <param name="importName">The input data file name</param>
		/// <param name="filename">he name of the data file name</param>
		/// <returns>data as Xml string </returns>
		protected int ConvertPrimaryFileToXml(string importName,  string filename)
		{
			int returnCode;
			string logDesc;
			try
			{   				
				// initialising returncode to 0 
				returnCode = 0;
				// Getting the name of coverted xml file name
				outputXmlFileName = ConvertToXml(importName, CQImportType.CoachRoutesImport,filename);

				if (outputXmlFileName.Length < 1) 
				{
					// conversion has failed 
					// now checking the reason for failing ?    	
					// checking if it has valid feedname        					
					if (boolValidfeedname)
					{
						// feedname was valid and hence it must have because of invalid schema or data												
						logDesc = String.Format(CultureInfo.InvariantCulture, "Unable to produce xml file from given csv file : {0}  update failed for feed ", filename);   
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc)  );
						returnCode = (int)TDExceptionIdentifier.SBPCsvConversionFailed;						
					}
					else
					{	// returning invalid feedname error number					
						logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found  : {0}  update failed for feed", filename);   
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc)  );
						returnCode = (int)TDExceptionIdentifier.SBPDataFeedNameNotFound; 
					}
					
					// returning error code
					return returnCode;                          			
				}
				else
				{	
					// conversion of csv file to xml file was successfull and hence returning 0				
					return returnCode;
				
				}
			}
			catch (TDException tdEx)
			{
				// returning invalid feedname error number					
				logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found  : {0}  update failed for feed, general exception {1}", filename, tdEx.Message );   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc)  );
				if (tdEx.Identifier == TDExceptionIdentifier.SBPPrimaryFileNotFound)
					return (int)TDExceptionIdentifier.SBPPrimaryFileNotFound; 
				else
					return (int)TDExceptionIdentifier.SBPDataFeedNameNotFound; 
			}
		}


		/// <summary>
		/// This function loads the primary xml data and imports into tables.
		/// </summary>
		/// <returns>stutus code as integer</returns>
		protected int CoachRoutesImport()
		{
            			
			//Extract the XML string from the primary file
			string xmlString;
			StringBuilder outputXmlFullFilepath = new StringBuilder(fileNameLength); 
			string additionalImportXmlData;
			try
			{
				// Get the xml data string 
				xmlString = LoadXML(CQImportType.CoachRoutesImport);

				//Validate the XML file against the XSD schema specified in the XML file				
				outputXmlFullFilepath = outputXmlFullFilepath.Append(Path.Combine(ProcessingDirectory.ToString(), outputXmlFileName)); 

				try
				{
					ValidateXML(outputXmlFullFilepath.ToString(), CQImportType.CoachRoutesImport); 
				}
				catch(TDException tdEx)
				{
					// If Validation fails return the error code
					return (int)tdEx.Identifier;    
				}

				//Update the xml to remove attributes that SQL Server will get confused by
				xmlString = UpdateXML(xmlString);

				//If the process has been successful so far.
				if (xmlString.Length!= 0 )   				
				{   					
					try
					{
						// Now get the xml string for other additionalImports
						if (quotaFileExists)
							additionalImportXmlData = QuotaFaresImport();
						else
						   additionalImportXmlData  = string.Empty; 
					}
					catch(TDException)
					{
						// If Validation fails return the error code
						return (int)TDExceptionIdentifier.SBPAdditionalXmlValidationFails;  
					}


					//Upload the XML into the database
					if (quotaFileExists)
						return ImportDataIntoSQLServer(xmlString, CQImportType.CoachRoutesImport, additionalImportXmlData,CQImportType.QuotaFareImport);
					else
						return ImportDataIntoSQLServer(xmlString, CQImportType.CoachRoutesImport, string.Empty,CQImportType.QuotaFareImport );	

				}
				else
					return (int)TDExceptionIdentifier.SBPImportFailedForXmlToDB;   
			}
			catch(TDException tdEx)
			{
				// returning invalid feedname error number					
				string logDesc = String.Format(CultureInfo.InvariantCulture, "PrimaryDataImport failed  : {0}  import failed for the feed, general exception {1}", outputXmlFullFilepath.ToString(), tdEx.Message);   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc)  );				
				return (int)TDExceptionIdentifier.SBPImportFailedForXmlToDB; 
			}
		}


		/// <summary>
		///	 Returns xml data for the additional Import
		/// </summary>
		/// <returns>Xml data</returns>
		private string QuotaFaresImport()
		{
			StringBuilder outputXmlFullFilepath = new StringBuilder(fileNameLength);
			try
			{
				//Extract the XML string from the primary file
				string xmlString = LoadXML(CQImportType.QuotaFareImport);				 
				//Validate the XML file against the XSD schema specified in the XML file
				outputXmlFullFilepath = outputXmlFullFilepath.Append(ProcessingDirectory.ToString())
												.Append(@"\")
												.Append(outputAdditionalXmlFileName);
				
				ValidateXML(outputXmlFullFilepath.ToString(), CQImportType.QuotaFareImport); 			
				
				//Update the xml to remove attributes that SQL Server will get confused by
				return UpdateXML(xmlString);
			}
			catch (TDException tdEx)
			{
				// returning invalid feedname error number					
				string logDesc = String.Format(CultureInfo.InvariantCulture, "AdditionalDataImport failed  : {0}  import failed for the feed, general exception {1}", outputXmlFullFilepath.ToString(), tdEx.Message);   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc));				
				throw new TDException(logDesc, false, TDExceptionIdentifier.SBPImportFailedForXmlToDB);   
			}
		}


		/// <summary>
		/// COnverts the the given import to a valid Xml data
		/// </summary>
		/// <param name="importName">The import name </param>
		/// <param name="filename"> Data file name</param></param>
		/// <returns>Status code as integer</returns>
		protected int ConvertQuotaFaresToXml(string importName,  string filename)
		{	int	 returnCode;
			
			// First Check if file exists
			if (!QuotaFaresFileExists(filename))
			{	
				quotaFileExists = false;
				// If not then return 0
				return 0;
			}
			else
				quotaFileExists = true;
		        	
			outputAdditionalXmlFileName = ConvertToXml(importName, CQImportType.QuotaFareImport, filename);

			if (outputAdditionalXmlFileName.Length < 1) 
			{   					
				// checking if it has valid feedname 
				string logDesc;
				if (boolValidfeedname)
				{
					// feedname was valid and hence it must have because of invalid schema or data												
					logDesc = String.Format(CultureInfo.InvariantCulture, "Unable to produce xml file from given csv file : {0}  update failed for feed ", filename);   
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc)  );
					returnCode = (int)TDExceptionIdentifier.SBPAdditionalCsvConversionFailed;						
				}
				else
				{	// returning invalid feedname error number					
					logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found  : {0}  update failed for feed", filename);   
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDesc)  );
					returnCode = (int)TDExceptionIdentifier.SBPDataFeedNameNotFound; 
				}
					
				// returning error code
				return returnCode;
			
			}
			else
			{	
				// conversion of csv file to xml file was successfull and hence returning 0				
				return 0;
				
			}
		}
		
		
		
		/// <summary>
		///	Overloaded mthod. Import the XML into the database. The XML string is passed directly
		/// into a stored procedure. The stored procedure does all the work
		/// extracting the appropriate data from the XML into the database tables.
		/// </summary>
		/// <param name="XMLString">XMl data string</param>
		/// <param name="importType">Primary or additional Import Type </param>
		/// <param name="sqlHelper">Sql helper class</param>
		/// <param name="importTransaction">the transaction object</param>
		/// <returns>Status code as int </returns>
		private int ImportDataIntoSQLServer(string XMLString, CQImportType importType, SqlHelper sqlHelper, SqlTransaction importTransaction)
		{
			int result = 0;

			SqlHelperDatabase datasource;
			string database = Properties.Current[databaseKey];
			string storedProcedureName;

			if (importType == CQImportType.CoachRoutesImport)
				storedProcedureName = Properties.Current[primaryStoredProcedureKey ];
			else
				storedProcedureName = Properties.Current[additionalStoredProcedureKey ];
            
            // Checking database
			if ( database == null || database.Length == 0) 
			{
				result = (int) TDExceptionIdentifier.DGStoredProcedureUnspecified;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Database not specified in Properties"));
				return result;
			}

			// checking stored procedure name
			if (storedProcedureName == null || storedProcedureName.Length == 0)
			{
				result = (int) TDExceptionIdentifier.DGDatabaseUnspecified;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Stored procedure not specified in Properties"));
				return result;
			}
			
			datasource = (SqlHelperDatabase) Enum.Parse( typeof(SqlHelperDatabase), database, true );
            
			//Call the stored procedure
			try
			{
				Hashtable hashParams = new Hashtable();
				hashParams.Add("@XML", XMLString);
				Hashtable hashTypes = new Hashtable();

				//The parameter must be set as database type of Text otherwise it will not work
				hashTypes.Add("@XML", SqlDbType.Text);

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					IDictionaryEnumerator myEnumerator = hashParams.GetEnumerator();
					while ( myEnumerator.MoveNext() )
						Logger.Write(new OperationalEvent(TDEventCategory.Business,
							TDTraceLevel.Verbose, "Element:: " + string.Format(CultureInfo.InvariantCulture,  "\t{0}:\t{1}", myEnumerator.Key, myEnumerator.Value ) ) );
				}

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
				String.Format(CultureInfo.InvariantCulture, "Calling ImportDataIntoSQLServer SP on database {0} and SP {1}", database , storedProcedureName)));
				sqlHelper.Execute( storedProcedureName, hashParams, hashTypes, importTransaction);
						
			}
			catch (SqlException sqlEx)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Import stored procedure[" + storedProcedureName + "] calling error: " + sqlEx.Message, sqlEx));
				throw new TDException("Import stored procedure[" + storedProcedureName + "] calling error: " + sqlEx.Message, sqlEx, true, TDExceptionIdentifier.DGImportStoredProcedureError );
			}
			
			return result;
		}
		


		/// <summary>
		///	 Overloaded mthod. Import the XML into the database. The XML string is passed directly
		/// into a stored procedure. The stored procedure does all the work
		/// extracting the appropriate data from the XML into the database tables.
		/// </summary>
		/// <param name="XMLString">XMl data string</param>
		/// <param name="importType">Primary or additional Import Type </param>
		/// <param name="additionalXmlString">Additional XMl data string</param>
		/// <param name="additionalImportType">Additional Import Type</param>
		/// <returns>Status code as int</returns>
		private int ImportDataIntoSQLServer(string XMLString, CQImportType importType, string additionalXmlString, CQImportType additionalImportType)
		{
			int result = 0;

			SqlHelperDatabase datasource;
			string database = Properties.Current[databaseKey];			
			SqlTransaction importTransaction = null;
			
			datasource = (SqlHelperDatabase) Enum.Parse( typeof(SqlHelperDatabase), database, true );

			//Object for managing database calling
			SqlHelper sqlHelper = new SqlHelper();

			//Open the database connection
			try
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,String.Format(CultureInfo.InvariantCulture,"Connecting to {0} database.",  datasource.ToString())));
				
				// This method opens connection and returns the transaction object back
				importTransaction = sqlHelper.GetTransaction(datasource); 
			}
			catch (SqlException sqlEx)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
									String.Format(CultureInfo.InvariantCulture,"Import database connection error: {0}" , sqlEx.Message), sqlEx));
				return (int)TDExceptionIdentifier.DGImportDBConnectionError;
			}

			//Call the stored procedure
			try
			{
				// Now getting the transaction object 					
				result = ImportDataIntoSQLServer(XMLString, importType, sqlHelper , importTransaction);

				if (result == 0 && additionalXmlString.Length > 0)
				{
					importTransaction.Save("ImportCoachRoutes");  
					result =  ImportDataIntoSQLServer(additionalXmlString, additionalImportType , sqlHelper ,importTransaction);
				}

				if (result == 0)
				{
					// now commit the whole transaction 
					importTransaction.Commit(); 
				}
				else
				{	
					// Rollback the whole transaction
					importTransaction.Rollback(); 
				}
			}
			catch (Exception e)
			{
				// Rollback the whole transaction
				if (importTransaction != null)
				{
					importTransaction.Rollback();
				}

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, " [ImportDataIntoSQLServer] error: " + e.Message, e));
				
				throw new TDException("Imports fails " + e.Message, e, true, TDExceptionIdentifier.DGImportStoredProcedureError );
			}
			finally
			{
				if (sqlHelper!=null)
				{
					sqlHelper.ConnClose(); 
				}
			}
		
			return result;
		}
		
		
		#endregion


		#region HelperFunctions
		
		/// <summary>
		/// Extracts the XML string from the given file.
		/// </summary>
		/// <returns></returns>
		private string LoadXML(CQImportType importType)
		{
			StringBuilder xmlImportFileBuilder = new StringBuilder(fileNameLength);
			string xmlImportFile;

			if (importType == CQImportType.CoachRoutesImport)
				 xmlImportFile = xmlImportFileBuilder.Append(ProcessingDirectory.ToString())
										.Append(@"\")
										.Append(outputXmlFileName).ToString() ;
			 else
				 xmlImportFile = xmlImportFileBuilder.Append(ProcessingDirectory.ToString())
										.Append(@"\")
										.Append(outputAdditionalXmlFileName).ToString();
					
							
			if ( TDTraceSwitch.TraceVerbose ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business,
					TDTraceLevel.Verbose,  string.Format(CultureInfo.InvariantCulture, "Opening stream to XML file [ {0} ]", xmlImportFile)) );
			}

			string xmlString = string.Empty;
			StreamReader streamReader = null;
			try 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
					string.Format(CultureInfo.InvariantCulture, "Opening stream to XML file [ {0} ]", xmlImportFile)) );				
				streamReader = new StreamReader(xmlImportFile);
				xmlString = streamReader.ReadToEnd();				
			} 
			catch (OutOfMemoryException oMEx) 
			{
				//Log the error which occurred while reading the XML stream
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
				string.Format(CultureInfo.InvariantCulture, "Memory exception while reading import file [ {0}] occurred. Error : {1}", xmlImportFile, oMEx.Message ) ));
				throw new TDException("XML parser error: " + oMEx.Message, true, TDExceptionIdentifier.DGInvalidXMLToInput );
			}
			catch (IOException ioEx) 
			{
				//Log the error which occurred while reading the XML stream
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					string.Format(CultureInfo.InvariantCulture, "IO Error reading import file [ {0}] occurred. Error : {1}", xmlImportFile, ioEx.Message ) ));
				throw new TDException("XML parser error: " + ioEx.Message, true, TDExceptionIdentifier.DGInvalidXMLToInput );
			}

			finally 
			{
				try
				{
					if (streamReader!=null)
						streamReader.Close();
				}
				catch (Exception ex)
				{   
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
						"Error closing XML stream", ex));
				}
			}
			

			return xmlString;
		}
		
		
		/// <summary>
		/// Validates the XML against the XSD schema which should
		/// reside in the same path as the actual XML file.
		/// </summary>
		/// <returns></returns>
		private void ValidateXML(string importFilename, CQImportType importType)
		{
			string schemaLocation = string.Empty;
			string xmlNS = string.Empty;

			try
			{
				if (importType==CQImportType.CoachRoutesImport )
				{
					schemaLocation = Properties.Current[primaryXmlschemaLocationKey];								
					xmlNS = Properties.Current[ primaryXmlNamespaceKey];
				}
				else
				{
					schemaLocation = Properties.Current[additionalXmlschemaLocationKey];								
					xmlNS = Properties.Current[ additionalXmlNamespaceKey];
				}

				if (schemaLocation.Length == 0) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML schema file not specified in Properties"));
					throw new TDException("XML schema file not specified in Properties", true, TDExceptionIdentifier.DGXMLSchemaFileUnspecified );
				}

				if (xmlNS.Length == 0) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML namespace not specified in Properties"));
					throw new TDException("XML namespace not specified in Properties", true, TDExceptionIdentifier.DGXMLNamespaceUnspecified );
				}

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, String.Format(CultureInfo.InvariantCulture, "Setting up XML validation engine with schema location: {0}", schemaLocation)));
				}

                XmlSchemaSet sc = new XmlSchemaSet();
                sc.Add(xmlNS, schemaLocation);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
                settings.ValidationType = ValidationType.Schema;

                XmlReader xmlValidator = XmlReader.Create(importFilename, settings);
				
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Verbose, "Validating XML " ) );
				}

				//Chek the XML document
				while (xmlValidator.Read())
				{
					if (xmlValidator.NodeType == XmlNodeType.EntityReference)
					{
						xmlValidator.ResolveEntity();
					}
				}
                xmlValidator.Close();
			}
			catch (Exception e) 
			{
				//The following line is correct as any parsing errors are raised in the
				//validation event handler, so the error needs to be simply raised up.
				if  (importType == CQImportType.CoachRoutesImport) 
				{	
					throw new TDException(e.Message, false, TDExceptionIdentifier.SBPPrimaryXmlValidationFails);
				}
				else
				{
					throw new TDException(e.Message, false, TDExceptionIdentifier.SBPAdditionalXmlValidationFails) ;
				}
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
		protected static string RemoveAttribute(string xmlData, string attributeName)
		{
			int start = xmlData.IndexOf(attributeName + "=\"");

			if (start > -1)
			{
				int end = xmlData.IndexOf("\"", start + attributeName.Length + 2);
				return xmlData.Substring(0, start - 1) + xmlData.Substring(end + 1);
			}

			return xmlData;
		}
		
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
				throw new TDException(logDesc ,true, TDExceptionIdentifier.SBPCsvConversionFailed); 
			}

		}


		/// <summary>
		/// This function check if the given file exists
		/// </summary>
		/// <param name="filename">The data file name</param>
		/// <returns>TRue if exits else false</returns>
		private bool QuotaFaresFileExists(string filename)
		{
			StringBuilder quotaFilePath = new StringBuilder(fileNameLength); 
			
			quotaFilePath = quotaFilePath.Append(ProcessingDirectory.ToString())
								.Append(@"\")
								.Append(filename) ; 
				
			return File.Exists(quotaFilePath.ToString());
		}
		
		#endregion
	
	}


	#region Import Coach and Quota Import Enumerator
	public enum CQImportType
	{
		CoachRoutesImport = 0,
		QuotaFareImport = 1 
	}

	#endregion


}

