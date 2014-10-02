// *********************************************** 
// NAME                 : SeasonalNoticeBoardDataImport.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 27/10/2004 
// DESCRIPTION  : Class that receives and processes
// an XML seasonal data file via the Data Gateway
// Whenever data is processed, the old data in the table will be flushed or truncated.
// ************************************************ 

using System;
using System.Collections;
using System.Data;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using TransportDirect.Common ;
using TransportDirect.Common.DatabaseInfrastructure ;
using TransportDirect.Common.Logging;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.SeasonalNoticeBoardImport
{
	// <summary>
	// Summary description for SeasonalNoticeBoardDataImport.
	// Constructor calls base contructor with the only required
	// value: feed = "Seasonal Notice Board Data Import" (which is used for logging purposes)	
	// <param name="feed">The datafeed ID</param>
	// <param name="params1"> Parameters passed to the task</param>
	// <param name="params2">Parameters passed to the task</param>
	// <param name="utility">External executable used to perform the import if required</param>
	// <param name="processingDirectory">The directory holding the data while the task is executing</param>
	// </summary>
	public class SeasonalNoticeBoardDataImport : TransportDirect.Datagateway.Framework.DatasourceImportTask    
	{
		private string strImportname = "seasonalInformation";
		private string strLogdesc = "SeasonalNoticeBoardData Import" ;
		private bool boolValidfeedname = true;		
		private string _filename = "" ;		
 
		/// <summary>
		/// Constructor which calls base contructor 
		/// </summary>
		public SeasonalNoticeBoardDataImport(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)

		{
			xmlschemaLocationKey = string.Format( xmlschemaLocationKey, strImportname);
			xmlNamespaceKey = string.Format( xmlNamespaceKey, strImportname);
			databaseKey = string.Format( databaseKey, strImportname);
			storedProcedureKey = string.Format( storedProcedureKey, strImportname);		

			// Now checking the feedname is same or not??
			if ( !dataFeed.Equals(Properties.Current["datagateway.sqlimport.seasonalInformation.feedname"] ) ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "SeasonalNoticeBoardDataImport unexpected feed name: [" + dataFeed + "]"));
				boolValidfeedname = false; 
				//throw new TDException("SeasonalNoticeBoardDataImport unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.SNBDataFeedNameNotFound );
			}
			
		}


	
		#region Overridden protected methods
		/// <summary>
		/// Override of LogStart method
		/// </summary>
		protected override void LogStart()
		{
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
				strLogdesc + " update begun for feed " + dataFeed));			
			
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
					retCode.ToString()+ ": "  + strLogdesc + " update failed for feed " +dataFeed));
			}
			else
			{
				// Log success
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					strLogdesc + " update succeeded for feed " +dataFeed));			
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
			try
			{
				_filename = filename;
				// If conversion is sucessfull then only calling base Run method 
				if (! Convert()) 
				{
					// conversion has failed 
					// now checking the reason for failing ?
	
					// checking if it has valid feedname 
					if (boolValidfeedname)
					{
						// feedname was valid and hence it must have because of invalid schema or data
						returnCode = (int)TDExceptionIdentifier.SNBCsvConversionFailed;
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							"Unable to produce xml file from given csv file"    + ": " + _filename.ToString() + " "  + " update failed for feed "  )  );
						returnCode = (int)TDExceptionIdentifier.SNBCsvConversionFailed;						
					}
					else
					{	// returning invalid feedname error number					
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							"Invalid feed name found "    + ": " + _filename.ToString() + " "  + " update failed for feed "  )  );
						returnCode = (int)TDExceptionIdentifier.SNBDataFeedNameNotFound; 
					}
					
					// returning error code
					return returnCode;
			
				}
				else
				{	
					// conversion of csv file to xml file was successfull and hence calling base Run method
					return base.Run(_filename) ;
				}
			}
			catch (TDException tdex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"TDError occured while converting csv file to xml "    + ": " + _filename.ToString() + " "  + tdex.Message.ToString()));								
				returnCode = (int)TDExceptionIdentifier.SNBImportFailed;
				return returnCode;
			}
			
			
			
			
		}
        #endregion
		
		/// <summary>
		/// virtual void AddAttributeToXmlDoc() adds attribute to the document 
		/// </summary>
		protected virtual void AddAttributeToXmlDoc(XmlDocument xmlDoc, string strAttributeName , string strAttributeValue)
		{
			try
			{
				if (xmlDoc!= null)
				{

					// creating a new attribute 
					XmlAttribute Xmlatt = xmlDoc.CreateAttribute(strAttributeName);
					// assigining a value to attribute 
					Xmlatt.Value = strAttributeValue;

					// now appending attribute to the collection class
					XmlAttributeCollection attrColl = xmlDoc.DocumentElement.Attributes ; 
					attrColl.Append(Xmlatt);


				}

			}
			catch(Exception ex)
			{
				throw new Exception("problem adding attribute " + ex.Message.ToString());
			}

		}


		/// <summary>
		/// Convert() will convert input csv file to xml output file
		/// </summary>
		protected virtual bool Convert()
		{	

			string strOutXmlFolder = "" ;
			string strFullFilePath = "" ; 
			string strOutXmlFileName = "" ;
			string strOutXmlFilePath = ""; 			
							
			string strNamespace = @"xmlns"; 
			string strNamespacexs = @"xmlns:xs";
			//string strSchemaLocation = @"xs:schemaLocation";
			XmlDocument xmlDoc = new XmlDocument();			  
			
			System.Uri  myImportFileName;
			Common.DatabaseInfrastructure.XmlCsvReader CsvXmlConverter = new Common.DatabaseInfrastructure.XmlCsvReader();   

			try
			{	
				// if invalid feedname then return false 
				if (!boolValidfeedname)
				{
					return false;
				}
				
				// throw an exception if filename == null 
				if (_filename == null ) 
				{
						throw (new Exception("No source file found for SeasonalNoticeBoardDataImport")) ;  				
				}
					
					// throw an exception if filename length < 4 
				if (_filename.Length < 4)
				{
					throw (new Exception("SeasonalNoticeBoardDataImport: File name should contain atleast 5 chars")) ;  					
				}
			   // getting full path 
			   strFullFilePath = ProcessingDirectory.ToString()  +  @"\" + _filename ; 
				// getting output folder name
			   strOutXmlFolder = ProcessingDirectory.ToString();
				
				// getting output file name 
			   strOutXmlFileName = _filename.Substring(0, _filename.Length - 4).Trim()  + ".xml" ; 
				// getting output file path 
			   strOutXmlFilePath =  ProcessingDirectory.ToString()  + @"\" + strOutXmlFileName ; 

				myImportFileName = new System.Uri("file://" + strFullFilePath);   
				
				// check if file exists
				if (!File.Exists(strFullFilePath))
				{
					return false;
				}
				CsvXmlConverter =  new Common.DatabaseInfrastructure.XmlCsvReader(myImportFileName, xmlDoc.NameTable ) ;  
				
					CsvXmlConverter.FirstRowHasColumnNames = true;
					CsvXmlConverter.RootName = strImportname;
					CsvXmlConverter.RowName = "SeasonalDataImport";
				
					xmlDoc.Load(CsvXmlConverter);

					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
						" csv file has been loaded correctly in XmlDocument"));
				
		
				
					// Now appending namespace attribute to the top level element
					AddAttributeToXmlDoc( xmlDoc , strNamespace, (string)Properties.Current["datagateway.sqlimport.seasonalInformation.xmlnamespace"]);
				
					// Now appending namespace:xsi attribute 		
					AddAttributeToXmlDoc(xmlDoc ,strNamespacexs, (string)Properties.Current["datagateway.sqlimport.seasonalInformation.xmlnamespacexsi"]);
				
					// Saving output file 
					xmlDoc.Save(strOutXmlFilePath);
					
				
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been converted sucessfully at: " + strOutXmlFilePath.ToString() ));

				// Assigning xml file as input file now..
				_filename = strOutXmlFileName;				
							
				return true;
			}
			catch (TDException  tdex )
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					tdex.Message.ToString()   + ": " + " update failed for feed "));				
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
	}



	

}
