// *********************************************** 
// NAME                 : ParkAndRideImportTask.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : //2005 
// DESCRIPTION  		: ParkAndRideImportTask Class that receives and processes
// an XML park and ride data file via the Data Gateway.  
// Whenever data is processed, the old data in the table will be flushed or truncated.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ParkAndRideImportTask.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:18   mturner
//Initial revision.
//
//   Rev 1.4   Aug 25 2005 10:38:12   Schand
//Additional change for Code review.
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.3   Aug 25 2005 10:11:06   Schand
//FxCop and Code Review changes
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.2   Aug 15 2005 11:42:54   Schand
//Updates for ExceptionIdentifier code
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 11 2005 18:50:02   Schand
//Updates for FxCop review.
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 11:15:32   Schand
//Initial revision.


using System;
using System.Collections;
using System.Data;
using System.Text;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using TransportDirect.Common ;
using TransportDirect.Common.DatabaseInfrastructure ;
using TransportDirect.Common.Logging;
using System.Globalization;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// ParkAndRideImportTask Class that receives and processes
	// an XML park and ride data file via the Data Gateway.  
	// Whenever data is processed, the old data in the table will be flushed or truncated.
	/// </summary>
	public class ParkAndRideImportTask : TransportDirect.Datagateway.Framework.DatasourceImportTask  
	{

		private string strImportname = string.Empty;				
		private string strLogdesc = "Park and Ride Import" ;
		private bool boolValidfeedname = true;		
		private string _filename = "" ;		

		#region Constructor
		/// <summary>
		/// The contructor for ParkAndRideImportTask which takes feedname and imports the data into tables. 
		/// </summary>
		/// <param name="feed">Import feedname</param>
		/// <param name="params1">Addition information1</param>
		/// <param name="params2">Addition information1</param>
		/// <param name="utility">Utility to run</param>
		/// <param name="processingDirectory">Directory location of Gateway processing directory</param>
		public ParkAndRideImportTask
			(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			strImportname =	Properties.Current["datagateway.sqlimport.parkandride.Name"]; //parkandride					    
			xmlschemaLocationKey = string.Format(CultureInfo.InvariantCulture, xmlschemaLocationKey, strImportname);
			xmlNamespaceKey = string.Format(CultureInfo.InvariantCulture, xmlNamespaceKey, strImportname);
			databaseKey = string.Format(CultureInfo.InvariantCulture, databaseKey, strImportname);
			storedProcedureKey = string.Format(CultureInfo.InvariantCulture, storedProcedureKey, strImportname);		

			// Now checking the feedname is same or not??
			if ( !dataFeed.Equals(Properties.Current["datagateway.sqlimport.parkandride.feedname"] ) ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, strLogdesc + " unexpected feed name: [" + dataFeed + "]"));
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
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
				logDesc.ToString()));			
			
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
				logDesc = String.Format(CultureInfo.InvariantCulture, "{0} {1} update failed for feed : {2} ", strLogdesc, retCode.ToString(),  dataFeed.ToString()); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					logDesc.ToString()));
			}
			else
			{
				// Log success				
				logDesc = String.Format(CultureInfo.InvariantCulture,"{0}  update succeeded for feed {1}", strLogdesc, dataFeed); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					logDesc.ToString()));			
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
				// If conversion is sucessfull then only calling base Run method 
				if (! ConvertToXml()) 
				{
					// conversion has failed 
					// now checking the reason for failing ?
	
					// checking if it has valid feedname 
					if (boolValidfeedname)
					{
						// feedname was valid and hence it must have because of invalid schema or data												
						logDesc = String.Format(CultureInfo.InvariantCulture, "Unable to produce xml file from given csv file : {0}  update failed for feed ", _filename.ToString());   
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							logDesc)  );
						returnCode = (int)TDExceptionIdentifier.PRDCsvConversionFailed;						
					}
					else
					{	// returning invalid feedname error number					
						logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found  : {0}  update failed for feed", _filename.ToString());   
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
							logDesc.ToString())  );
						returnCode = (int)TDExceptionIdentifier.PRDDataFeedNameNotFound; 
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
				logDesc = String.Format(CultureInfo.InvariantCulture, "TDError occurred while converting csv file to xml  : {0} {1} ", _filename.ToString(), tdex.Message.ToString());   
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					logDesc));								
				returnCode = (int)TDExceptionIdentifier.PRDImportFailed;
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
			{	string logDesc = String.Format(CultureInfo.InvariantCulture,"problem adding attribute {0}", xEx.Message.ToString()); 
				throw new TDException(logDesc ,true, TDExceptionIdentifier.PRDCsvConversionFailed); 
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
			bool xmlConverted =false;  
			
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
					throw new TDException("No source file found for " + strLogdesc, true, TDExceptionIdentifier.PRDCsvConversionFailed);
				}
					
				// throw an exception if filename length < 4 
				if (_filename.Length < 4)
				{					
					throw new TDException(strLogdesc +  ": File name should contain atleast 5 chars", true, TDExceptionIdentifier.PRDCsvConversionFailed);
				}
				// getting full path 
				fullFilePath = fullFilePath.Append(ProcessingDirectory.ToString()).Append(@"\").Append(_filename) ; 
							
				
				// getting output file name 
				outXmlFileName = outXmlFileName.Append(_filename.Substring(0, _filename.Length - 4).Trim()  + ".xml") ; 
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
				CsvXmlConverter.RootName = strImportname;
				CsvXmlConverter.RowName = "ParkAndRideDataImport";
				
				xmlDoc.Load(CsvXmlConverter);				

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been loaded correctly in XmlDocument"));
				
		
				
				// Now appending namespace attribute to the top level element
				AddAttributeToXmlDoc( xmlDoc , strNamespace, (string)Properties.Current["datagateway.sqlimport.parkandride.xmlnamespace"]);

								
				// Now appending namespace:xsi attribute 		
				AddAttributeToXmlDoc(xmlDoc ,strNamespacexs, (string)Properties.Current["datagateway.sqlimport.parkandride.xmlnamespacexsi"]);
				
				// Saving output file 
				xmlDoc.Save(strOutXmlFilePath.ToString());
					
				xmlConverted = true;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been converted successfully at: " + strOutXmlFilePath.ToString() ));

				// Assigning xml file as input file now..
				_filename = outXmlFileName.ToString();				
							
				return true;
			}
			
			catch (TDException  tdex )
			{	
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					tdex.Message.ToString()   + ": update failed for feed "));				
				return false;
			
			}
			catch (Exception ex)
			{	// As we don't know what exception will be thrown while doing the conversion
				// so handling general exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					ex.Message.ToString()   + ": update failed for feed "));				
				
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
