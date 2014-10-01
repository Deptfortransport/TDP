// *********************************************** 
// NAME			: ProductProfilesImport.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Impementation of the ProductProfilesImport class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/ProductProfilesImport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:52   mturner
//Initial revision.
//
//   Rev 1.2   Apr 15 2005 13:50:14   jbroome
//Import process now accepts a .csv file and converts it to XML before import.
//
//   Rev 1.1   Mar 21 2005 10:54:24   jbroome
//Minor updates after code review
//
//   Rev 1.0   Feb 08 2005 10:38:18   jbroome
//Initial revision.

using System;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Class which inherits from DataSourceImportTask. 
	/// Imports the Product Profile data into the database.
	/// </summary>
	public class ProductProfilesImport : DatasourceImportTask
	{
		private string xmlFileName = string.Empty;
		private const string importName = "productavailability";
		
		#region Constructor
		/// <summary>
		/// Constructor calls base contructor with the only required
		/// value: feed = "Import Product Profiles" (which is used for logging purposes)
		/// </summary>
		public ProductProfilesImport(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			xmlschemaLocationKey = string.Format(CultureInfo.CurrentCulture, xmlschemaLocationKey, importName);
			xmlNamespaceKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceKey, importName);
			databaseKey = string.Format(CultureInfo.CurrentCulture, databaseKey, importName);
			storedProcedureKey = string.Format(CultureInfo.CurrentCulture, storedProcedureKey, importName);

			if ( (dataFeed == null) || (!dataFeed.Equals( Properties.Current["datagateway.sqlimport.productavailability.feedname"] ) ) ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "ProductProfilesImport unexpected feed name: [" + dataFeed + "]"));
				throw new TDException("ProductProfilesImport unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.DGUnexpectedFeedName );
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
				"ProductProfilesImport update begun for feed "+dataFeed));			
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
					retCode.ToString()+ ": ProductProfilesImport update failed for feed " +dataFeed));
			}
			else
			{
				// Log success
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					"ProductProfilesImport update succeeded for feed " +dataFeed));			
			}
		}

		/// <summary>
		/// Overridden base method Run 
		/// Converts csv file to xml file and then calls base Run method
		/// </summary>
		/// <param name="filename">string filename</param>
		/// <returns>int errorcode</returns>
		public override int Run(string fileName)
		{	
			// Initialise returnCode
			int returnCode = 0;
	
			try
			{
				// Perform csv to xml conversion
				if (ConvertToXml(fileName))
				{
					// Conversion of csv file to xml file successful
					// Call base.Run to process newly created xml file
					return base.Run(xmlFileName) ;
				}
				else
				{
					returnCode = (int)TDExceptionIdentifier.AECsvConversionFailed; 
				}
			}
			catch (TDException tdEx)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error occurred during ProductProfilesImport process for file : " + xmlFileName.ToString() + " "  + tdEx.Message.ToString()));								
				returnCode = (int)TDExceptionIdentifier.AECsvConversionFailed; 
			}

			return returnCode;
		}

		/// <summary>
		/// Method converts csv input file to xml output file
		/// </summary>
		/// <param name="filename">string filename</param>
		/// <returns>true if conversion sucessful</returns>
		private bool ConvertToXml(string fileName)
		{
			this.xmlFileName =		fileName.Substring(0, fileName.Length - 4).Trim()  + ".xml" ; 
			string xmlFilePath =	ProcessingDirectory.ToString()  + @"\" + xmlFileName ; 
			string csvFilePath =	ProcessingDirectory.ToString()  +  @"\" + fileName ; 
			Uri csvImportFile =		new Uri("file://" + csvFilePath);   
			XmlCsvReader xcr = null;
	
			// Check if file exists
			if (!File.Exists(csvFilePath))
			{
				return false;
			}

			try
			{	
				// XmlCsvReader will convert csv into xml, will be loaded into XmlDocument
				XmlDocument xmlDoc = new XmlDocument();			  
				xcr = new XmlCsvReader(csvImportFile, xmlDoc.NameTable ) ;  
				// Xml element names taken from column headings	
				xcr.FirstRowHasColumnNames = true;
				xcr.RootName = "ProductProfiles";
				xcr.RowName = "Profile";
			
				xmlDoc.Load(xcr);
	
				// Add namespace attribute to root element for import
				XmlAttribute attribute = xmlDoc.CreateAttribute("xmlns");
				attribute.Value = Properties.Current["datagateway.sqlimport.productavailability.xmlnamespace"];
				XmlAttributeCollection attributes = xmlDoc.DocumentElement.Attributes ; 
				attributes.Append(attribute);
				// Save newly converted xml file
				xmlDoc.Save(xmlFilePath);					
				
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "CSV file for import succesfully converted to XML and saved at : " + xmlFilePath.ToString() ));

				return true;
			}
			catch (ArgumentException argEx)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error occurred during CSV to XML conversion : " + fileName.ToString() + " "  + argEx.Message.ToString()));								
				return false;
			}
			catch (XmlException xmlEx)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error occurred during CSV to XML conversion : " + fileName.ToString() + " "  + xmlEx.Message.ToString()));								
				return false;
			}
			finally
			{
				// Clean up
				if (xcr != null)
				{
					xcr.Close();
				}
			}

		}

		#endregion
	}
}
