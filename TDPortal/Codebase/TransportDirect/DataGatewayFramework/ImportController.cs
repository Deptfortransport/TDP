// ***********************************************
// NAME 		: ImportController.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 11/08/2003
// DESCRIPTION 	: The Import Controller obtains the input parameters for a data
// feed and invokes the FTP controller to move files to the Data Gateway servers.
// It is also responsible for moving files between the Incoming, Proccessing and 
// Holding directories.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/ImportController.cs-arc  $ 
//
//   Rev 1.3   Feb 18 2009 13:30:54   apatel
//ImportController processdatafeed method changed to pass the lastFile argument to run method. As a result of this any of the importer created/updated for multiple data file should overload run method to accept the lastFile argument.
//Resolution for 5254: Car Parking Importer Update
//
//   Rev 1.2   Mar 10 2008 15:15:46   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev Devfactory Feb 03 2008 21:18:00 apatel
//  CCN 0426 Change importer to check all the files even there is an error in multiple datafeed scenario.
//
//   Rev Devfactory Jan 26 2008 10:08:00 apatel
//   CCN 0426 Multiple Data file import related changes.
//
//   Rev 1.0   Nov 08 2007 12:20:12   mturner
//Initial revision.
//
//   Rev 1.31   May 13 2004 18:41:22   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.30   Jan 12 2004 11:55:24   jmorrissey
//Updated error codes and logged messages
//
//   Rev 1.29   Dec 17 2003 10:21:46   TKarsan
//Enhacements for Gateway Export, added failed flag for the database log entry.
//
//   Rev 1.28   Dec 16 2003 14:09:36   TKarsan
//Modifications for Data Gateway Export.
//
//   Rev 1.27   Nov 25 2003 15:41:48   TKarsan
//Fixed empty zip file and serco counters.
//
//   Rev 1.26   Nov 21 2003 20:04:24   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.25   Nov 18 2003 19:17:22   JMorrissey
//Fixed clean up issues with data gateway, fixed travel news to use properties for xsd, fixed command line importer, and updated resources for data gateway.
//
//   Rev 1.24   Nov 17 2003 19:29:02   JMorrissey
//Serco feed fixed and integrated with Gateway.
//
//   Rev 1.23   Nov 17 2003 15:47:32   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.22   Nov 14 2003 16:55:30   JMorrissey
//Change to logger string message
//
//   Rev 1.21   Nov 14 2003 16:28:22   TKarsan
//Processing last file only, tidy up of files after
//
//   Rev 1.20   Nov 14 2003 15:50:44   JMorrissey
//Now deletes zip files rather than the extracted files
//
//   Rev 1.19   Nov 13 2003 13:18:20   esevern
//various changes 
//
//   Rev 1.18   Nov 12 2003 18:48:12   JMorrissey
//Update to import method
//
//   Rev 1.17   Nov 05 2003 11:48:14   JMorrissey
//Integrated ZipReader class. This extracts the data file as datafeeds now arrive as a zip file
//
//   Rev 1.16   Nov 03 2003 17:46:12   JMorrissey
//Removed code that dealt with thresholds for no files found to import. This code is now in the FtpTask class.
//
//   Rev 1.15   Oct 29 2003 14:51:38   JMorrissey
//Added ZipReader.cs and ZipReader.xsd
//
//   Rev 1.14   Oct 06 2003 12:23:00   TKarsan
//Rolled back to 1.12 as per MTurner's instructions.
//
//   Rev 1.12   Sep 17 2003 15:38:36   MTurner
//Added command line importer class
//
//   Rev 1.11   Sep 16 2003 15:00:16   MTurner
//Changes after code review
//
//   Rev 1.10   Sep 03 2003 13:27:42   PScott
//work in progress
//
//   Rev 1.9   Sep 03 2003 11:47:40   MTurner
//Further FXCop led changes
//
//   Rev 1.8   Sep 02 2003 11:50:32   MTurner
//Fixed bug in reflection code
//
//   Rev 1.7   Sep 01 2003 14:11:40   MTurner
//Changes after comments by A. Caunt
//
//   Rev 1.6   Aug 29 2003 12:39:06   mturner
//Added error handling for SQL read()
//
//   Rev 1.5   Aug 29 2003 10:32:02   mturner
//Changes after FXCop scan
//
//   Rev 1.4   Aug 28 2003 13:52:10   mturner
//Added code to retrieve default settings from config file
//
//   Rev 1.3   Aug 27 2003 15:49:36   MTurner
//added clientflag parameter to reflect changes to FTPController class
//
//   Rev 1.2   Aug 27 2003 15:17:58   MTurner
//Changed /test from a string to a bool in order to make call to FTPController simpler
//
//   Rev 1.1   Aug 15 2003 13:35:38   mturner
//Not fully functional - checked in while on Leave
//
//   Rev 1.0   Aug 11 2003 11:29:06   mturner
//Initial revision.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description Import Controller.
	/// </summary>
	public class ImportController
	{
		public ImportController()
		{
			this.IncomingRoot = null;
			this.ProcessingRoot = null;
			this.HoldingRoot = null;
			this.BackupRoot = null;
			this.DataServiceName = null;
		}

		private static string incomingRootString;
		private static string processingRootString;
		private static string holdingRootString;
		private static string backupRootString;
		private static string dataServiceNameString;
		

		#region Public Methods
		/// <summary>
		/// Parameters are accepted from the ImportMain program and then used to retrieve additional
		/// configuration information from the ImportParameters class.
		/// All of this data is then used to instantiate an instance of ImportTask which carries out
		/// the actual proccessing of the file.
		/// </summary>
		/// <param name="dataFeed">Unique name of an import, that is the key for obtaining parameters from the data service</param>
		/// <param name="test">Used to see if data import solution supports specified feed</param>
		/// <param name="params1">Allows operator to override standard parameter 1</param>
		/// <param name="params2">Allows operator to override standard parameter 2</param>
		/// <param name="ipAddress">Alternative IP address to be used if primary server is unavailable</param>
		public int Import(string dataFeed, bool test, string params1, 
			string params2, string ipAddress, bool noTransfer)
		{
            
            int statusCode = 1;

			// Create instance of ImportParameters Class and call SetData to get config 
			// from the SQL database.
			ImportParameters importFileParameters = new ImportParameters(dataFeed);

			if(importFileParameters.SetData() == true)
			{
				// Override parameter values for parameters specified on command line at runtime
				if(params1.Length != 0) 
				{
					importFileParameters.Parameters1 = params1;
				}
				if(params2.Length != 0) 
				{
					importFileParameters.Parameters2 = params2;
				}
				if(noTransfer == false)
				{
                    // Check if a wait should happen before transfering files - 
                    // potentially could be a very large data feed and does not fully copy before below happens
                    TransferWait(dataFeed);

					statusCode = importFile(dataFeed, test, ipAddress);

					if(statusCode != 0) 
					{
						OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
							statusCode.ToString()+ ":File transfer failed for " +dataFeed);
						Logger.Write(oe);

						return statusCode;
					}
				}
				if(test == true)
				{
					// At this point it is obvious that the datafeed is configured OK.
					// This is logged and the program will then exit with a 0 return code
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Info,"Data feed " + dataFeed + " is configured");
					Logger.Write(oe);
					statusCode = 0;

					return statusCode;
				}
				// Retrieve default file paths
				if(getDirectoryPaths() != 0)
				{
					statusCode = (int)TDExceptionIdentifier.DGInvalidProperties;
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Error, statusCode.ToString()+ ":Failed to retrieve directory paths from properties");
					Logger.Write(oe);
					
					return statusCode;
				}
				try
				{
                    // Check if a wait should happen before moving/copying files - 
                    // potentially could be a very large data feed and does not fully copy before below happens
                    ProcessWait(dataFeed);

					DirectoryInfo directory = new DirectoryInfo(IncomingRoot + dataFeed);
					FileInfo[] files = directory.GetFiles("*.zip");
					
					FileInfo newZip = files[0];
					foreach(FileInfo file in files) 
					{
						if(newZip.Name.CompareTo(file.Name) < 0)
							newZip = file;
					}

                    // Log some file information
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("DataFeed[{0}] - File[{1}], Size[{2}bytes] LastWriteTime[{3}]",
                                dataFeed, newZip.Name, newZip.Length, newZip.LastWriteTime.ToString("yyyy-MM-ddTHH:mm:ss.fff"))));
                    }

					//copy file to the Processing directory
					File.Copy(IncomingRoot + dataFeed + @"\" + newZip.Name, ProcessingRoot + dataFeed + @"\" + newZip.Name, true);

					//move file to Backup dir
					File.Copy(IncomingRoot + dataFeed + @"\" + newZip.Name, BackupRoot + dataFeed + @"\" + newZip.Name, true);
					File.Delete(IncomingRoot + dataFeed + @"\" + newZip.Name);
													
					//use ZipReader to extract the datafile from the zip file
					ZipReader zipReader = new ZipReader(dataFeed, 1);
					if (zipReader.ProcessZipFile(ProcessingRoot + dataFeed, newZip.Name))
					{
                        // Use refleciton to establish the class that needs to be instantiated for 
                        // this import and then create an instance of that class.

                        object[] initArguments = {dataFeed,
							importFileParameters.Parameters1,
							importFileParameters.Parameters2,
							importFileParameters.ImportUtility,
							importFileParameters.ProcessingDir + "/"
						};

                        Assembly importAssembly = Assembly.LoadFrom(importFileParameters.ClassArchive);
                        Type reflectionClass = importAssembly.GetType(importFileParameters.ImportClass);
                        object reflectionInstance = Activator.CreateInstance(reflectionClass, initArguments);

                        //CCN 0426 if there is only one data file just call ProcessDataFeed once.
                        if (!zipReader.HasMultipleDataFeed)
                        {
                            statusCode = ProcessDataFeed(dataFeed, zipReader.DataFeedFilename, zipReader, importFileParameters, reflectionInstance);
                        }
                        else
                        {
                            Dictionary<string, int> feedstatus = new Dictionary<string, int>(); 
                            //CCN 0426 loop through multiple data files and cal ProcessDataFeed for each file.
                            
                            foreach (string feed in zipReader.DataFeedFiles)
                            {
                                statusCode = ProcessDataFeed(dataFeed, feed, zipReader, importFileParameters, reflectionInstance);

                                feedstatus.Add(feed, statusCode);
                                
                            }


                            int sCode = 0;
                            // CCN 0426 updated to try processing all data feeds and then check the statuscode for all feeds 
                            // this will then return last non zero status code in the case of error. 
                            foreach (string key in feedstatus.Keys)
                            {
                                
                                if (feedstatus[key] != 0)
                                    sCode = feedstatus[key];

                                statusCode = sCode;
                            }
                        }

                        if (statusCode == 0)
                        {
                            FtpParameters FtpFileParameters = new FtpParameters(dataFeed, 1);

                            if (FtpFileParameters.SetData() == true)
                            {
                                // write back the new current feed time to the database 
                                if (FtpFileParameters.WriteNewTime(zipReader.TimePrepared, newZip.Name) == false)
                                {
                                    OperationalEvent logEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                                        "Data Feed time was not successfully written to database for " + newZip.Name);
                                    Logger.Write(logEvent);
                                }
                            }

                            statusCode = 0;
                        }
                        else
                        {
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Error, statusCode.ToString() + ":Unable to complete import for " + dataFeed);
                            Logger.Write(oe);
                        }

					}
					else // If zip reader failed.
					{
						statusCode = (int)TDExceptionIdentifier.DGZipReaderError;
						OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
							TDTraceLevel.Error, statusCode.ToString()+ ":ZipReader failed during processing for " +dataFeed);
						Logger.Write(oe);
					}

					// Perform house keeping and log file statuses for transfer.
					CleanUpDirectories(dataFeed, newZip, statusCode == 0 ? ImportStatus.Processed : ImportStatus.Failed );
				}	
				catch(Exception e)
				{
					statusCode = (int)TDExceptionIdentifier.DGUnexpectedException;
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Error, statusCode.ToString()+ ":" +e.Message+ " Feed: " +dataFeed);
					Logger.Write(oe);
				}
			}
			else // If import params were not read.
			{
				statusCode = (int)TDExceptionIdentifier.DGInvalidConfiguration;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
					TDTraceLevel.Error, statusCode.ToString()+ ":Unable to read import parameters for " +dataFeed);
				Logger.Write(oe);
			}

			return statusCode;
		}

        /// <summary>
        /// ProcessDataFeed is a bit modified code which was inside the ImportController
        /// </summary>
        /// <param name="dataFeed">Data feed folder value.</param>
        /// <param name="dataFeedFileName">data feed file name value.</param>
        /// <param name="zipReader">object of type TDPZipReader</param>
        /// <param name="importFileParameters">import file parameters</param>
        /// <param name="reflectionInstance">Instance of class on which method to be executed.</param>
        /// <returns></returns>
        private int ProcessDataFeed(string dataFeed, string dataFeedFileName, ZipReader zipReader, ImportParameters importFileParameters, Object reflectionInstance)
        {
            int statusCode = 1;

            Type reflectionClass = reflectionInstance.GetType();
            //get extracted datafile 
            string extractedFilePath = ProcessingRoot + dataFeed + @"\" + dataFeedFileName;
            FileInfo extractedFile = new FileInfo(extractedFilePath);

            bool lastFile = false;

            
            // if there is multiple data feed, set lastFile variable for the current data feed.
            if (zipReader.HasMultipleDataFeed)
            {
               string lastFileName = zipReader.DataFeedFiles[zipReader.DataFeedFiles.Count - 1];

               if (dataFeedFileName == lastFileName)
               {
                   lastFile = true;
               }

            }

            object[] methodArguments = { extractedFile.Name };

            // if the zipreader got multiple data feel pass the lastFile value which 
            // is true if the current datafeed is the last file to process
            if (zipReader.HasMultipleDataFeed)
            {
                methodArguments = new object[] { extractedFile.Name, lastFile };
            }

            try
            {
               

                if (zipReader.FileIsPresent) // Process only if data file is present.
                {
                    Console.WriteLine("Calling " + importFileParameters.ImportClass);

                   statusCode = (int)reflectionClass.InvokeMember("Run", BindingFlags.InvokeMethod, null, reflectionInstance, methodArguments);
                }
                else
                {
                    Console.WriteLine("Not called " + importFileParameters.ImportClass);
                    statusCode = 0;
                }

                
            }
            catch (Exception e)
            {
                statusCode = (int)TDExceptionIdentifier.DGInvokingImportTaskFailed;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error, e.Message + " has occurred for " + dataFeed);
                Logger.Write(oe);
            }

            return statusCode;
        }

		#endregion

		#region Private Methods
		/// <summary>
		/// Moves the zip file in processing directory to holding and clears the
		/// processing directory. Also moves remaining files in incoming to holding.
		/// </summary>
		/// <param name="dataFeed">The feed being processed.</param>
		/// <param name="newZip">The zip file just processed.</param>
		private void CleanUpDirectories(string dataFeed, FileInfo newZip, ImportStatus importStatus)
		{
			LogExportFiles logFiles = new LogExportFiles(dataFeed);

			FileInfo[] files;
			DirectoryInfo directory;

			try
			{
				// Make log entry for processed file.
				logFiles.AddFile(newZip.Name, importStatus);

				// Move zip file to holding dir.
				File.Copy(ProcessingRoot + dataFeed + @"\" + newZip.Name, HoldingRoot + dataFeed + @"\" + newZip.Name, true);
				File.Delete(ProcessingRoot + dataFeed + @"\" + newZip.Name);

				// Delete file from backup dir.
				File.Delete(BackupRoot + dataFeed + "\\" + newZip.Name);

				// Clean up processing directory.
				directory = new DirectoryInfo(ProcessingRoot + dataFeed);
				files = directory.GetFiles();
				foreach(FileInfo processedfile in files)
				{
					try
					{
						processedfile.Delete();
					}
					catch(IOException ioe)
					{
						OperationalEvent oe = 
							new  OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Error,
							"Error deleting " + processedfile.FullName+ " "+ioe.Message);
						Logger.Write(oe);
					}
				}

				// Clean Incoming directory
				directory = new DirectoryInfo(IncomingRoot + dataFeed);
				files = directory.GetFiles();
				foreach(FileInfo theRest in files)
				{
					// Make log entry for skipped file
					logFiles.AddFile(theRest.Name, ImportStatus.Skipped);

					try
					{
						theRest.MoveTo(HoldingRoot + dataFeed + "\\" + theRest.Name);
					}
					catch(IOException ioe)
					{
						OperationalEvent oe = 
							new  OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Error,
							"Error moving " +theRest.FullName+ " "+ioe.Message);
						Logger.Write(oe);
					}
				}
				// Ready to write entries to database
				logFiles.LogFiles();
			}
			catch(Exception e)
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
					TDTraceLevel.Error, e.Message+ " Feed: " +dataFeed);
				Logger.Write(oe);
			}
		}

		/// <summary>
		/// Retrieves the default directory paths from the data feeds configuration
		/// file (Feedsconfig.csv).
		/// </summary>
		private int getDirectoryPaths()
		{
			int returnCode = 0;

			// extract directory paths using properties service
			IPropertyProvider currentProperties = Properties.Current;
			IncomingRoot   = currentProperties["Gateway.IncomingPath"];
			ProcessingRoot = currentProperties["Gateway.ProcessingPath"];		
			HoldingRoot    = currentProperties["Gateway.HoldingPath"];
			BackupRoot     = currentProperties["Gateway.BackupPath"];
			if ((IncomingRoot.Length == 0) || (ProcessingRoot.Length == 0) 
				|| (HoldingRoot.Length == 0) || (BackupRoot.Length == 0))
			{
				returnCode = 1;
			}
			return returnCode;
		}

        /// <summary>
        /// Determines and performs a sleep if a wait has been configured prior to processing the files
        /// </summary>
        /// <param name="dataFeed"></param>
        /// <returns></returns>
        private void TransferWait(string dataFeed)
        {
            int waitMilliseconds = 0;

            string wait = Properties.Current[string.Format("Gateway.Transfer.Wait.Milliseconds.{0}", dataFeed)];

            if (!string.IsNullOrEmpty(wait))
            {
                if (!Int32.TryParse(wait, out waitMilliseconds))
                    waitMilliseconds = 0;
            }

            if (waitMilliseconds > 0)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Datafeed[{0}] has been configured to wait for [{1}]ms before transfering files ", dataFeed, waitMilliseconds)));

                System.Threading.Thread.Sleep(waitMilliseconds);

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Datafeed[{0}] is now continuing", dataFeed)));
            }
        }

        /// <summary>
        /// Determines and performs a sleep if a wait has been configured prior to processing the files
        /// </summary>
        /// <param name="dataFeed"></param>
        /// <returns></returns>
        private void ProcessWait(string dataFeed)
        {
            int waitMilliseconds = 0;

            string wait = Properties.Current[string.Format("Gateway.Process.Wait.Milliseconds.{0}", dataFeed)];
                        
            if (!string.IsNullOrEmpty(wait))
            {
                if (!Int32.TryParse(wait, out waitMilliseconds))
                    waitMilliseconds = 0;
            }

            if (waitMilliseconds > 0)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Datafeed[{0}] has been configured to wait for [{1}]ms before processing incoming files ", dataFeed, waitMilliseconds)));

                System.Threading.Thread.Sleep(waitMilliseconds);

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Datafeed[{0}] is now continuing", dataFeed)));
            }
        }

		/// <summary>
		/// Creates an instance of the FTPController class.
		/// This then has its TransferFiles method invoked to transfer the data file.
		/// </summary>
		/// <param name="dataFeed">Name of the DataFeed to be imported</param>
		/// <param name="test">Bool to indicate no import functionality is performed but a check for valid configuration information is needed</param>
		/// <param name="ipAddress">IP address of alternative server if default is unavailable.</param>
		private int importFile(string dataFeed, bool test, string ipAddress)
		{
			int clientFlag = 1;

			FtpController controller = new FtpController();
			return controller.TransferFiles(dataFeed, clientFlag, test, ipAddress);
		}
        #endregion

		#region Properties
		/// <summary>
		/// Root directory for incoming data files.
		/// </summary>
		private string IncomingRoot
		{
			get
			{
				return incomingRootString;
			}
			set
			{
				incomingRootString = value;
			}
		}

		/// <summary>
		/// Root directory of the area on the server where files are placed for processing.
		/// </summary>
		private string ProcessingRoot
		{
			get
			{
				return processingRootString;
			}
			set
			{
				processingRootString = value;
			}
		}

		/// <summary>
		/// Root directory of the area on the server where files are placed after processing.
		/// </summary>
		private string HoldingRoot
		{
			get
			{
				return holdingRootString;
			}
			set
			{
				holdingRootString = value;
			}
		}

		/// <summary>
		/// Root directory of the area on the server where files are placed for backup during processing.
		/// </summary>
		private string BackupRoot
		{
			get
			{
				return backupRootString;
			}
			set
			{
				backupRootString = value;
			}
		}

		/// <summary>
		/// Name of the Data Service for the current import.
		/// </summary>
		/// <value></value>
		private string DataServiceName
		{
			get
			{
				return dataServiceNameString; 
			}
			set
			{
				dataServiceNameString = value;
			}
		}
		#endregion
	}
}
