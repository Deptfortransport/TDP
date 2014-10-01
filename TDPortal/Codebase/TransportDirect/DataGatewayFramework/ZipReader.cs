// ***********************************************
// NAME 		: ZipReader.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 29/10/2003
// DESCRIPTION 	: A class that extracts data feeds from a zip file 
// and validates the associated header file			
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/ZipReader.cs-arc  $
//
//   Rev 1.4   Apr 09 2008 12:49:42   mturner
//Removed unreachable code to prevent compiler warning.
//
//   Rev 1.3   Mar 10 2008 15:15:50   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev Devfactory Feb 20 2008 15:00:00 mmodi
//Check and set for multiple files only if in property value
//
//   Rev Devfactory Jan 26 2008 10:08:00 apatel
//   CCN 0426 Multiple Data file import related changes.
//
//   Rev 1.0   Nov 08 2007 12:20:16   mturner
//Initial revision.
//
//   Rev 1.13   Jan 12 2004 17:21:12   jmorrissey
//Updated exception identifiers 
//
//   Rev 1.12   Dec 03 2003 17:22:56   JMorrissey
//Updated code that adds values to the hashtable when reading the xml in the header file
//
//   Rev 1.11   Nov 25 2003 15:41:46   TKarsan
//Fixed empty zip file and serco counters.
//
//   Rev 1.10   Nov 21 2003 20:04:26   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.9   Nov 18 2003 19:17:24   JMorrissey
//Fixed clean up issues with data gateway, fixed travel news to use properties for xsd, fixed command line importer, and updated resources for data gateway.
//
//   Rev 1.8   Nov 14 2003 20:30:38   JMorrissey
//more fixes to data gateway
//
//   Rev 1.7   Nov 14 2003 16:28:22   TKarsan
//Processing last file only, tidy up of files after
//
//   Rev 1.6   Nov 14 2003 15:41:42   TKarsan
//No timeout and updated PKZip file location in properties.
//
//   Rev 1.5   Nov 13 2003 13:18:22   esevern
//various changes 
//
//   Rev 1.4   Nov 12 2003 10:15:16   esevern
//Changes to allow recording and checking of last successful zip file extraction.
//(by Phil Scott)
//
//   Rev 1.3   Nov 04 2003 10:05:26   kcheung
//Tweaks required for switch to licenced version of pkzipc
//
//   Rev 1.2   Oct 31 2003 14:42:08   kcheung
//Updated header summarys
//
//   Rev 1.1   Oct 31 2003 13:56:08   kcheung
//Working version.
//
//   Rev 1.0   Oct 29 2003 14:50:16   JMorrissey
//Initial Revision


using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using System.IO;
using System.Threading;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description for TDPZipReader.
	/// </summary>
	public class ZipReader
	{
		/// <summary>
		/// Namespace used by the header XML and XSD.
		/// </summary>
		private string xmlNS;

		/// <summary>
		/// Schema location should be read from the properties.
		/// </summary>
		private string schemaLocation;

		/// <summary>
		/// Delegate to performing the unzipping process.
		/// </summary>
		private delegate bool UnzipProcessAsyncDelegate( string zipFileDirectory, string zipFileName );
		
		// Holds the values read from the Xml.
		private DateTime timePreparedValue = new DateTime(2000, 1, 1);
		private string dataFeedFilenameValue = String.Empty;
		private bool isFilePresent = false;
		private string DataFeed;
		private int FtpClient;

        // CCN 0426 apatel
        // New changes Relating to Multiple Data File Input
        // Zip file may have more than one data file but they will have only one header file.
        private bool hasMultipleDataFeed = false;
        private List<string> dataFeedFilenameValues = null;


		#region Methods to process the zip file

		/// <summary>
		/// Obtains the schema location from the property service.
		/// </summary>
		public ZipReader(string dataFeedIn, int ftpClientIn)
		{
			DataFeed = dataFeedIn;
			FtpClient = ftpClientIn;
			schemaLocation = Properties.Current["datagateway.zipprocessor.schemalocation"];
			xmlNS = Properties.Current["datagateway.zipprocessor.xmlnamespace"];
		}

		/// <summary>
		/// Unzips the file specified at the given location. This is
		/// achieved by calling an executable to unzip the file.
		/// </summary>
		/// <param name="zipFileDirectory">Directory containing the zip file</param>
		/// <param name="zipFilePath">Name of the zip file to process.</param>
		/// <returns>True if extraction was successful, false otherwise.</returns>
		public bool ProcessZipFile(string zipFileDirectory, string zipFileName)
		{
			string headerFilename = Properties.Current["datagateway.zipprocessor.headerxmlfilename"];
			string timePreparedNodeName = Properties.Current["datagateway.zipprocessor.timepreparednodename"];
			string supplierIDNodeName = Properties.Current["datagateway.zipprocessor.supplieridnodename"];
			string filenameNodeName = Properties.Current["datagateway.zipprocessor.filenamenodename"];
			string isPresentValueToMatch = Properties.Current["datagateway.zipprocessor.ispresentindicator"];
			string isPresentAttributeName = Properties.Current["datagateway.zipprocessor.ispresentattributename"];

			DateTime previousDataFeedDatetime;

			try
			{
				
				// Check to see if the extraction process was successful
				if(ExtractZip(zipFileDirectory, zipFileName))
				{
					Hashtable ht = ReadHeaderFile(zipFileDirectory + "\\" + headerFilename);

					timePreparedValue = XmlConvert.ToDateTime(ht[timePreparedNodeName].ToString(),XmlDateTimeSerializationMode.Unspecified);

					if(ht[isPresentAttributeName].Equals(isPresentValueToMatch))
					{
                        //Check if the zip file got multiple data files
                        //if only one file just set dataFeedFilenameValue as in original code was
                        //if more than one file dataFeedFilenameValues will be set
                        if (!hasMultipleDataFeed)
                        {
                            // CCN 0426 apatel
                            // this is the original code
                            // idea is to make sure the rest of the dataimport keep working
                            // and not effected by the new changes.
                            dataFeedFilenameValue = ht[filenameNodeName].ToString();

                        }
                        else
                        {
                            dataFeedFilenameValues = GetMultipleDataFeedFileNames(zipFileDirectory + "\\" + headerFilename);
                        }
                        isFilePresent = true;

					}

					// Compare date time for feed with prevous.
					FtpParameters FtpFileParameters = new FtpParameters(DataFeed, FtpClient);
					bool isDataOk = FtpFileParameters.SetData();
					if (isDataOk == true)
						previousDataFeedDatetime = FtpFileParameters.DataFeedDatetime;
					else
						return false;

					if (previousDataFeedDatetime != timePreparedValue)
					{
						// Verbose log successful extraction
						if (TDTraceSwitch.TraceVerbose)
						{
							OperationalEvent logEvent = new OperationalEvent
								(TDEventCategory.Business, TDTraceLevel.Verbose, "DataGateway - Successful extraction of zip file. File processed: " + zipFileDirectory + zipFileName + ". The command that was executed was ", null);

							Logger.Write(logEvent);
						}

						// Return success flag
						return true;
					}
					else
					{
						OperationalEvent logEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
							"DataGateway - Extraction of zip file " + zipFileDirectory + zipFileName);

						Logger.Write(logEvent);

						// Return false to indicate unsuccessful extraction.

						return false;
					}

				}
				else
				{	
					// Extraction process failed to complete the extraction in
					// the allowed time.

					// Write error report to log.

					OperationalEvent logEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
						"DataGateway - Extraction of zip file: " + zipFileDirectory + zipFileName + " has FAILED.");

					Logger.Write(logEvent);

					// Return false to indicate unsuccessful extraction.
					return false;
				}
			}
			catch(TDException tde)
			{
				// TDException is thrown by the XML Validation Methods
				OperationalEvent logEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error,
					"Xml Validation Failure for file: " + zipFileDirectory + zipFileName + ". Error Message: " + tde.Message + ". The command that was executed was ", null);

				Logger.Write(logEvent);

				// Return false to indicate unsuccessful extraction.
				return false;
			}
				// FXCOP will complain that Exception should not be caught. However,
				// because we are launching an external process, many things can go
				// wrong since the exteranl process is not controlled by our code. This
				// is why exception is caught because if ANY sort of exception is thrown,
				// then an event should be written to the log.
			catch(Exception e)
			{
				// Write a log event to indicate that the zip process has failed.
				OperationalEvent logEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error,
					"Zip extraction for file: " + zipFileDirectory + zipFileName + " has failed. Error Message: " + e.Message + ". The command that was executed was ", null);

				Logger.Write(logEvent);

				// Return false to indicate unsuccessful extraction.
				return false;
			}
		}


		private bool ExtractZip(string zipFileDirectory, string zipFileName)
		{
			string zipApplicationPath = Properties.Current["datagateway.zipprocessor.pkunzipexecutablepath"];
			string headerXmlName = Properties.Current["datagateway.zipprocessor.headerxmlfilename"];
			string zipArguments = Properties.Current["datagateway.zipprocessor.pkunziparguments"];

			try
			{
				// Instantiate a new process
				Process zipProcess = new Process();

				// Set the initial working directory.
				zipProcess.StartInfo.WorkingDirectory = zipFileDirectory;

				// Construct the arguments.
				string commandArguments = zipArguments + " " + "\"" + zipFileName + "\"";

				// Set the path of the .exe and arguments
				zipProcess.StartInfo.FileName = zipApplicationPath;
				zipProcess.StartInfo.Arguments = commandArguments;

				zipProcess.StartInfo.UseShellExecute = false;
				zipProcess.StartInfo.CreateNoWindow = false;

				string theCommandExecuted = zipApplicationPath + " " + commandArguments;

				// Start and wait for exit.
				zipProcess.Start();
				zipProcess.WaitForExit();

                hasMultipleDataFeed = false;

                // Check property to determine whether we should set this multiplefile bool, because 
                // importer may only need to be run once even if there are multiple files
                string datafeeds = Properties.Current["datagateway.zipprocessor.multiplefilesinfeedcheck"];
                if (!string.IsNullOrEmpty(datafeeds))
                {
                    ArrayList datafeedsarray = new ArrayList(datafeeds.Split(",".ToCharArray()));

                    if (datafeedsarray.Contains(DataFeed))
                    {
                        // this will set wether zip file have more than one datafeed values or not
                        hasMultipleDataFeed = CheckForMultipleDataFeed(zipFileDirectory + "\\" + headerXmlName);
                    }
                }

				FileInfo fileInformation = new FileInfo(zipFileDirectory + "\\" + headerXmlName);
				return fileInformation.Exists;
			}
			catch(System.ComponentModel.Win32Exception)
			{
				return false;
			}
			catch(ObjectDisposedException)
			{
				return false;
			}
			catch(InvalidOperationException)
			{
				return false;
			}
			catch(SystemException)
			{
				return false;
			}
			catch(Exception)
			{
				return false;
			}			
		}

		#endregion

		#region Get properties to return the TimePrepared and DataFile filename

		/// <summary>
		/// Get property - returns the time prepared value. This will equal String.Empty
		/// if extraction has not yet been done or an error occured during extraction.
		/// </summary>
		public DateTime TimePrepared
		{
			get
			{
				return timePreparedValue;
			}
		}

		/// <summary>
		/// Get property - returns the data feed file name. This will equal String.Empty
		/// if extraction has not yet been done or an error occurred during extraction.
		/// The caller is responsible for checking that the file atually exists.
		/// </summary>
		public string DataFeedFilename
		{
			get
			{
				return dataFeedFilenameValue;
			}
		}

		/// <summary>
		/// Ready only property indicates if data file was present.
		/// </summary>
		public bool FileIsPresent
		{
			get
			{
				return isFilePresent;
			}
		}


        /// <summary>
        /// CCN 0426 Reads only property indicates if there are multiple data files
        /// </summary>
        public bool HasMultipleDataFeed
        {
            get
            {
                return hasMultipleDataFeed;
            }

        }

        /// <summary>
        /// CCN 0426 Reads only property returns all the data files in data feed in the case of 
        /// multiple data feed.
        /// </summary>
        public List<string> DataFeedFiles
        {
            get
            {
                return dataFeedFilenameValues;
            }
        }


		#endregion

		#region Xml Methods
		
		/// <summary>
		/// Reads the header XML using a validating parser and stores
		/// the results into a hashtable associating element or attribute
		/// name with its textual value.
		/// </summary>
		/// <param name="filename">The location of the header file.</param>
		/// <returns>Table containt name and value pairs.</returns>
		private Hashtable ReadHeaderFile(string filename) 
		{
			Hashtable ht = new Hashtable();

			try
			{
				// Read the XML document.
                XmlSchemaSet sc = new XmlSchemaSet();
                sc.Add(xmlNS, schemaLocation);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;
                
                XmlReader xmlDoc = XmlReader.Create(filename,settings);
              
				string lastElement = null, xmlValue; // Used in while(...) below.

				// Read XML data.
				while(xmlDoc.Read())
				{
					switch (xmlDoc.NodeType)
					{
						case XmlNodeType.Element:
							lastElement = xmlDoc.Name;
						switch(lastElement) // Read attribute values.
						{
							case "DataFeedInfo":
								if(GetAttribute(xmlDoc, "version") != "1.0")
								{
									xmlValue = "Attribute not found: DataFeedInfo@version";
									throw new System.Exception(xmlValue);
								}
								break;
							case "InterfaceNumber":
								ht.Add("version", GetAttribute(xmlDoc, "version"));
								break;
							case "DataFeed":
								ht.Add("isPresent", GetAttribute(xmlDoc, "isPresent"));
								break;
							default:
								break;
						}
							break;
						//checks if there is another element and that it has not already been added to the hash table
						case XmlNodeType.Text:
							if((lastElement != null) & (!ht.Contains(lastElement)))
							{
								xmlValue = xmlDoc.Value;
								ht.Add(lastElement, xmlValue);
								break;
							}
							break;
						default:
							lastElement = null;
							break;
					}
				}
                if (xmlDoc != null)
                    xmlDoc.Close();
			}
			catch(System.Xml.Schema.XmlSchemaException e)
			{
				throw new TDException(e.Message, true, TDExceptionIdentifier.DGSchemaValidationFailed);
			}

            return ht;
		}

		/// <summary>
		/// Obtains an attribute value for current element. It assumes that
		/// it exist, throws an exception otherwise.
		/// </summary>
		/// <param name="xmlDoc">Document pointing to an element with attribute.</param>
		/// <param name="AttributeName">Value of the attribute to read.</param>
		/// <returns>Value of the given attribute of current element of document.</returns>
		private string GetAttribute(XmlReader xmlDoc, string AttributeName)
		{
			string attributeValue;

			xmlDoc.MoveToAttribute(AttributeName);
			if(xmlDoc.ReadAttributeValue() && xmlDoc.NodeType == XmlNodeType.Text)
			{
				attributeValue = xmlDoc.Value;
			}
			else
			{
				attributeValue = "Attribute not found: " + AttributeName;
				throw new TDException("Attribute not found in XmlValidation. Failed for " + AttributeName, false, TDExceptionIdentifier.DGSchemaValidationAttributeNotFound);
			}

			return attributeValue;
		}

        /// <summary>
        /// CCN 0426
        /// This method checkes if the zip data feed file got multiple data files.
        /// This method looks for <FileName></FileName> section in TDHeader.xml file counts it
        /// and returns true if its more than 1 file or returns false
        /// </summary>
        /// <param name="filename">the location of the header file</param>
        /// <returns>true if more than 1 data file</returns>
        private bool CheckForMultipleDataFeed(string filename)
        {
            try
            {
                // Read the XML document.
                XmlSchemaSet sc = new XmlSchemaSet();
                sc.Add(xmlNS, schemaLocation);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;

                XmlReader xmlDoc = XmlReader.Create(filename, settings);

                XmlDocument xd = new XmlDocument();
                xd.Load(xmlDoc);

                XmlNodeList nodeList = xd.GetElementsByTagName(Properties.Current["datagateway.zipprocessor.filenamenodename"]);

                return nodeList.Count > 1;

            }
            catch (System.Xml.Schema.XmlSchemaException e)
            {
                throw new TDException(e.Message, true, TDExceptionIdentifier.DGSchemaValidationFailed);
            }
        }

        /// <summary>
        /// CCN 0426
        /// Returns names of all the data files described in <Filename></Filename> section of 
        /// header xml file.
        /// </summary>
        /// <param name="filename">Location of the header file.</param>
        /// <returns>List of data files in header xml file.</returns>
        private List<string> GetMultipleDataFeedFileNames(string filename)
        {
            List<string> feeds = new List<string>();
            // Read the XML document.
            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(xmlNS, schemaLocation);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            XmlReader xmlDoc = XmlReader.Create(filename, settings);

            XmlDocument xd = new XmlDocument();
            xd.Load(xmlDoc);

            XmlNodeList nodeList = xd.GetElementsByTagName(Properties.Current["datagateway.zipprocessor.filenamenodename"]);

            foreach (XmlNode node in nodeList)
            { 
                if(!string.IsNullOrEmpty(node.InnerText))
                    feeds.Add(node.InnerText);
            }

            return feeds;

        }


		#endregion
	}


}
