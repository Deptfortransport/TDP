// *********************************************** 
// NAME                 : TrainTaxiImportTask.cs
// AUTHOR               : Mark Turner
// DATE CREATED         : 25 July 2007
// DESCRIPTION  		: Imports a TrainTaxi XML file to the database 
//                        using a SQL Server stored procedure
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TrainTaxiImportClass.cs-arc  $
//
//   Rev 1.1   Dec 29 2008 17:03:06   RBRODDLE
//Added line to make this feed pick up the sproc timeout to allow this to be configured in the properties DB
//Resolution for 5214: Timeout values not working for some data feeds
//
//   Rev 1.0   Nov 08 2007 12:25:26   mturner
//Initial revision.
//
//   Rev 1.3   Sep 13 2007 14:39:12   rbroddle
//Minor corrections following code review
//
//   Rev 1.2   Sep 13 2007 09:33:02   rbroddle
//Minor corrections
//
//   Rev 1.1   Aug 31 2007 13:49:44   rbroddle
//CCN393, stream4468 (Coach Taxi Info) work
//Resolution for 4468: Coach Stop Taxi Enhancements
//
//   Rev 1.0   Jul 25 2007 13:55:22   mturner
//Initial revision.


using System;
using System.Diagnostics;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Text;


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for TrainTaxiImportClass.
	/// </summary>
	public class TrainTaxiImportClass : DatasourceImportTask
	{
		private string parameter1 = string.Empty;
		private const string logDescription = "TrainTaxi Import" ;
		private string importPropertyName = string.Empty;	

		#region Constructor

		public TrainTaxiImportClass(string feed, string params1, string params2, string utility, string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			parameter1 = params1;
			importPropertyName = Properties.Current["datagateway.sqlimport.traintaxi.Name"]; 				    
			xmlschemaLocationKey = string.Format(CultureInfo.InvariantCulture, xmlschemaLocationKey, importPropertyName);
			xmlNamespaceKey = string.Format(CultureInfo.InvariantCulture, xmlNamespaceKey, importPropertyName);
			databaseKey = string.Format(CultureInfo.InvariantCulture, databaseKey, importPropertyName);
			storedProcedureKey = string.Format(CultureInfo.InvariantCulture, storedProcedureKey, importPropertyName);
            commandTimeOutKey = string.Format(CultureInfo.InvariantCulture, commandTimeOutKey, importPropertyName);

			// Check the feed name
			if ( !dataFeed.Equals(Properties.Current["datagateway.sqlimport.traintaxi.feedname"] ) ) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDescription + " unexpected feed name: [" + dataFeed + "]"));			
			}	
		}

		#endregion

		#region Overridden protected methods
		/// <summary>
		/// Override of LogStart method which logs to the log file before data import.
		/// </summary>
		protected override void LogStart()
		{	
			string logDesc = String.Format(CultureInfo.InvariantCulture, "{0} update begun for feed {1}", logDescription, dataFeed.ToString()); 
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
				logDesc = String.Format(CultureInfo.InvariantCulture,"{0}  update succeeded for feed {1}", logDescription, dataFeed.ToString()); 
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
					logDesc));			
			}
		}


		public override int Run(string filename)
		{
			int statusCode = 0;
			string processedFilename = string.Empty;

			try
			{
				//Load XML data into the database.  
				//If load fails for any reason set status code to a non zero value.
				if (! PreProcessxmlFile(filename, out processedFilename)) 
				{
					// pre processing has failed 
					// Log failure
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
						logDescription + "TrainTaxi import failed pre-processing xml file"));
					statusCode = (int)TDExceptionIdentifier.DGUnexpectedException;
				}
				else
				{
					// Call base class run method.
					statusCode = base.Run(processedFilename);
				}

				//Only run the batch file if we've succeeded so far
				if (statusCode == 0)
				{
					#region BatchFileProcessing
					// Call the batch file passed in params 1
					Process p = new Process();
					p.StartInfo.UseShellExecute = true;
					p.StartInfo.CreateNoWindow = true; 
					p.StartInfo.FileName = parameter1;
					p.StartInfo.Arguments = filename.ToString();
					p.Start();
					p.WaitForExit();
					statusCode = p.ExitCode;
					if ( TDTraceSwitch.TraceVerbose ) 
					{
						Logger.Write(new OperationalEvent(TDEventCategory.Business,
							TDTraceLevel.Verbose, "TrainTaxi batch file processing completed with return code " + statusCode.ToString() ) );
					}
					#endregion
				}
				
				return statusCode;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// PreProcessxmlFile() will convert input xml file to useful relational format
		/// </summary>
		private bool PreProcessxmlFile(string filename,out string processedFilename)
		{	
			char seperator = '|';
			//String array of element names from the TrainTaxi metadata
			string[] elementNames = Properties.Current["datagateway.sqlimport.traintaxi.elementnames"].Split(seperator);
			//int to keep track of which of the above we are on
			int elementCount = 0;
			
			//filenames
			string trainTaxiXmlDocName, outTrainTaxiXmlDocName = string.Empty; 

			try
			{
				#region XmlFileConversion

				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Verbose, "Opening stream to XML file [" + filename + "]" ) );
				}

				trainTaxiXmlDocName = ProcessingDirectory + filename;
				outTrainTaxiXmlDocName = ProcessingDirectory + "output.xml";

				XmlNameTable xnt = new NameTable();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = false;
                settings.ValidationType = ValidationType.None;
                settings.NameTable = xnt;
                
				XmlReader vr = XmlReader.Create(trainTaxiXmlDocName, settings);

				StreamWriter sw = new StreamWriter (outTrainTaxiXmlDocName, false, Encoding.ASCII); 
				XmlWriter xw    = new XmlTextWriter (sw);                 
    
				vr.MoveToContent(); // Move to document element   
				xw.WriteStartDocument();
				//xw.WriteStartElement("Root");
				//write namespace declarations etc.
				string namespaceText = Properties.Current[xmlNamespaceKey]; 	
				xw.WriteStartElement(vr.Prefix, vr.LocalName, namespaceText); 

				// Write out each data with correct tag
				string dataText = string.Empty;
				while( vr.Read())
				{                
					if (vr.NodeType == XmlNodeType.Element) 
					{
						switch(vr.Name)
						{   
							case "ROW":
								xw.WriteStartElement("Row"); 
								elementCount = 0;
								break;
							case  "DATA":
								dataText = vr.ReadString(); 
								dataText = dataText.Replace("©","");
								xw.WriteElementString(elementNames[elementCount], dataText); 
								elementCount++;
								break;
						}
					}
					else 
					{
						if((vr.NodeType == XmlNodeType.EndElement) && (vr.Name == "ROW") ) 
						{
							xw.WriteEndElement(); 
						}
					}//if 
				}//while  

				xw.WriteEndElement(); 
				vr.Close();
				xw.Flush();
				xw.Close();
				processedFilename = outTrainTaxiXmlDocName;
				return true;	

				#endregion

			}
			catch (Exception ex)
			{	
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					ex.Message.ToString()   + ": xml pre processing failed for TrainTaxi feed"));
				processedFilename = string.Empty;
				return false;						    
			}

		}
		#endregion
	}
}
