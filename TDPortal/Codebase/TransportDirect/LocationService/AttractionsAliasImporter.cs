// *********************************************** 
// NAME                 : AttractionAliasImporter.cs
// AUTHOR               : John Frank
// DATE CREATED         : 28 January 2008 
// DESCRIPTION  		: Imports data into the dft_places table.  This is used to create 
//							aliases and new entries in the Attraction/facilities gazetteer.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/AttractionsAliasImporter.cs-arc  $ 
//
//   Rev 1.0   Jan 31 2008 10:32:18   jfrank
//Initial revision.
//Resolution for 4550: Attraction/Facilities Alias importer

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
    /// Attraction Gazetteer ImportTask Class that receives and processes a CSV file into the dft_places table.
    /// </summary>
    public class AttractionsAliasImporter : DatasourceImportTask
    {
        private const string propertyKey = "datagateway.sqlimport.attractionalias.Name";
        private const string propertyFeedName = "datagateway.sqlimport.attractionalias.feedname";
        private string importPropertyName = string.Empty;
        private const string logDescription = "Attraction Alias Import";
        private bool validFeedname = true;
        private string importFilename = string.Empty;	

        #region Constructor
		/// <summary>
		/// Default contructor 
		/// </summary>
		/// <param name="feed">Import feedname</param>
		/// <param name="params1">Addition information1</param>
		/// <param name="params2">Addition information1</param>
		/// <param name="utility">Utility to run</param>
		/// <param name="processingDirectory">Directory location of Gateway processing directory</param>
		public AttractionsAliasImporter
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


        public override int Run(string filename)
        {
            // initialising returnCode
            int returnCode = 0;
            string logDesc = string.Empty;
            try
            {
                importFilename = filename;
                // If conversion is sucessfull then only calling base Run method 
                if (!ConvertToXml())
                {
                    // check if feedname is valid 
                    if (validFeedname)
                    {
                        // feedname was valid and hence it must have because of invalid schema or data												
                        logDesc = String.Format(CultureInfo.InvariantCulture, "Unable to produce xml file from given csv file : {0}  update failed for feed ", importFilename.ToString());
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                            logDesc));
                        returnCode = (int)TDExceptionIdentifier.CPCsvConversionFailed;
                    }
                    else
                    {	// returning invalid feedname error number					
                        logDesc = String.Format(CultureInfo.InvariantCulture, "Invalid feed name found  : {0}  update failed for feed", importFilename.ToString());
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                            logDesc));
                        returnCode = (int)TDExceptionIdentifier.CPDataFeedNameNotFound;
                    }

                    // returning error code
                    return returnCode;
                }
                else
                {
                    // conversion of csv file to xml file was successful. Call base Run method
                    return base.Run(importFilename);
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


        /// <summary>
        /// Virtual void AddAttributeToXmlDoc() adds attribute to the document 
        /// </summary>
        /// <param name="xmlDoc">Xml document</param>
        /// <param name="attributeName">Name of the attribute</param>
        /// <param name="attributeValue">Value of the attribute</param>
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
            catch (XmlException xEx)
            {
                string logDesc = String.Format(CultureInfo.InvariantCulture, "problem adding attribute {0}", xEx.Message.ToString());
                throw new TDException(logDesc, true, TDExceptionIdentifier.CPCsvConversionFailed);
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
                //fullFilePath = fullFilePath.Append(ProcessingDirectory.ToString()).Append(importFilename);
			
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
				
				CsvXmlConverter.FirstRowHasColumnNames = false;
				CsvXmlConverter.RootName = importPropertyName;
				CsvXmlConverter.RowName = "Alias";
				
				xmlDoc.Load(CsvXmlConverter);				

				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					" csv file has been loaded correctly in XmlDocument"));
				
				// Append namespace attribute to the top level element
                AddAttributeToXmlDoc(xmlDoc, strNamespace, (string)Properties.Current["datagateway.sqlimport.attractionalias.xmlnamespace"]);
								
				// Append namespace:xsi attribute 		
                AddAttributeToXmlDoc(xmlDoc, strNamespacexs, (string)Properties.Current["datagateway.sqlimport.attractionalias.xmlnamespacexsi"]);
				
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

    }
}
