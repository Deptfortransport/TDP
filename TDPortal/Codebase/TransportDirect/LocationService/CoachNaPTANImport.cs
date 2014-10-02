// *********************************************** 
// NAME                 : CoachNaPTANImport.cs
// AUTHOR               : Rich Broddle
// DATE CREATED         : 29 August 2007
// DESCRIPTION  		: Imports NaPTAN_Lookup csv file to the database 
//                        using a SQL Server stored procedure. Data is used
//						  to x-ref 9000 codes against real coach NaPTANs
//						  for supply of location info from AtosAdditionalData DB
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CoachNaPTANImport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:04   mturner
//Initial revision.
//
//   Rev 1.0   Aug 31 2007 13:52:56   rbroddle
//Initial revision.
//Resolution for 4468: Coach Stop Taxi Enhancements
//

using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Xml;
using System.Text;



namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for CoachNaPTANImport class.
	/// </summary>
	public class CoachNaPTANImport : DatasourceImportTask
	{
		private string xmlFileName = string.Empty;
		private const string importName = "coachnaptanlookup";
		private string parameter1 = string.Empty;
		
		#region Constructor
		/// <summary>
		/// Constructor calls base contructor 
		/// </summary>
		public CoachNaPTANImport(string feed, string params1, string params2, string utility,
			string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			parameter1 = params1;
			xmlschemaLocationKey = string.Format(CultureInfo.CurrentCulture, xmlschemaLocationKey, importName);
			xmlNamespaceKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceKey, importName);
			databaseKey = string.Format(CultureInfo.CurrentCulture, databaseKey, importName);
			storedProcedureKey = string.Format(CultureInfo.CurrentCulture, storedProcedureKey, importName);

			if ( (dataFeed == null) || (!dataFeed.Equals( Properties.Current["datagateway.sqlimport.coachnaptanlookup.feedname"] ) ) ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "CoachNaPTANImport unexpected feed name: [" + dataFeed + "]"));
				throw new TDException("CoachNaPTANImport unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.DGUnexpectedFeedName );
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
				"Coach NaPTAN Lookup update begun for feed "+dataFeed));			
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
					retCode.ToString()+ ": Coach NaPTAN Lookup update failed for feed " +dataFeed));
			}
			else
			{
				// Log success
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					"Coach NaPTAN Lookup Import update succeeded for feed " +dataFeed));			
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
				#region BatchFileProcessing

				// Call the batch file passed in params 1
				Process p = new Process();
				p.StartInfo.UseShellExecute = true;
				p.StartInfo.CreateNoWindow = true; 
				p.StartInfo.FileName = parameter1;
				p.Start();
				p.WaitForExit();
				returnCode = p.ExitCode;
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Verbose, "Coach NaPTAN Lookup batch file processing completed with return code " + returnCode.ToString() ) );
				}

				#endregion

				//Only continue if batch file processing worked
				if (returnCode == 0)
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
						returnCode = (int)TDExceptionIdentifier.DGCSVtoXMLConversionFailed; 
					}
				}
			}
			catch (TDException tdEx)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"Error occurred during CoachNaPTANImport process for file : " + xmlFileName.ToString() 
					+ " "  + tdEx.Message.ToString()));								
				returnCode = (int)TDExceptionIdentifier.DGCSVtoXMLConversionFailed; 
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
				xcr.RootName = "CoachNaPTANLookups";
				xcr.RowName = "NaPTAN";
			
				xmlDoc.Load(xcr);
	
				// Add namespace attribute to root element for import
				XmlAttribute attribute = xmlDoc.CreateAttribute("xmlns");
				attribute.Value = Properties.Current["datagateway.sqlimport.coachnaptanlookup.xmlnamespace"];
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
