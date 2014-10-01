// ***********************************************
// NAME 		: FtpTask.cs
// AUTHOR 		: Phil Scott
// DATE CREATED : 19/08/2003
// DESCRIPTION 	: The FtpTask class creates and executes FTP sessioins
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/FTPTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:12   mturner
//Initial revision.
//
//   Rev 1.28   May 13 2004 18:41:22   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.27   Apr 08 2004 09:11:18   jgeorge
//Added new properties to simplify testing as part of unit test refactoring exercies
//
//   Rev 1.26   Jan 12 2004 11:55:06   jmorrissey
//Updated error codes and logged messages
//
//   Rev 1.25   Nov 25 2003 15:41:50   TKarsan
//Fixed empty zip file and serco counters.
//
//   Rev 1.24   Nov 21 2003 20:04:24   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.23   Nov 17 2003 19:29:06   JMorrissey
//Serco feed fixed and integrated with Gateway.
//
//   Rev 1.22   Nov 17 2003 15:47:30   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.21   Nov 14 2003 19:12:14   JMorrissey
//Fixed test harness
//
//   Rev 1.20   Nov 14 2003 17:48:24   TKarsan
//Created PuTTY path property.
//
//   Rev 1.19   Nov 14 2003 16:55:06   JMorrissey
//added delete script for psftp command
//
//   Rev 1.18   Nov 12 2003 18:50:38   JMorrissey
//Updated delete method. Set up of psftp arguments now done in Delete method rather than Pre-Task
//
//   Rev 1.17   Nov 12 2003 14:42:16   JMorrissey
//small change to psftp argument sconstruction
//
//   Rev 1.16   Nov 12 2003 13:51:26   JMorrissey
//change to construction of psftp arguments 
//
//   Rev 1.15   Nov 11 2003 12:34:24   JMorrissey
//Now handles extrra columns added to ftp_configuration table. Also changed how pscp process is set up.
//
//   Rev 1.14   Nov 05 2003 11:41:54   JMorrissey
//Added DeleteFeeds method to clear up target server after the transfer of files
//
//   Rev 1.13   Nov 03 2003 17:44:16   JMorrissey
//Work in progress, not unit tested yet. Have changed the process used to transfer files from ftp to pscp.
//
//   Rev 1.12   Sep 17 2003 08:19:42   pscott
//Add further error codes
//
//   Rev 1.11   Sep 16 2003 11:48:38   pscott
//add new errors
//
//   Rev 1.10   Sep 10 2003 09:20:26   PScott
//Code Review changes
//

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description FtpTask.
	/// FtpTask is a class derived from the LoggableTask class.
	/// This class provides methods that perform the actual ftp script creation
	/// and ftp script execution. It also logs the actions in the log file. 
	/// The class overides base class methods where ftp specific actions are required
	/// otherwise it uses the base class methods.
	/// </summary>
	public class FtpTask : LoggableTask
	{
		// Strings to be used in Operational events
		const string finishMessage = "pSFTP transfer completed successfully with error code 0";
		const string noFileMessage = "pSFTP transfer failed with error code 1";
		const string thresholdReachedMessage = "pSFTP transfer failed with error code 2";
		const string failMessage = "pSFTP failed with terminal error code";
		const string startMessage = "pSFTP transfer started";	

        private string pscpExePath = "pscp.exe";
		private string psftpExePath = "psftp.exe";

		// Operational Events used to log events via the TD Event Logigng Service
		static OperationalEvent importStart;
		static OperationalEvent importFinishSuccess;
		static OperationalEvent importFinishFail;
		static OperationalEvent importFinishNoFile;
		static OperationalEvent importFinishNoFileThresholdReached;		

		/// <summary>
		/// default constructor
		/// </summary>
		/// <param name="dataFeed"></param>
		/// <param name="ipAddress"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="localDir"></param>
		/// <param name="remoteDir"></param>
		/// <param name="filenameFilter"></param>
		/// <param name="removeFiles"></param>
		public FtpTask( string dataFeed, 
			string ipAddress, 
			string username, 
			string password,
			string localDir, 
			string remoteDir, 
			string filenameFilter,
			int missingFeedCounter,
			int missingFeedThreshold,
			DateTime dataFeedDatetime,
			string dataFeedFilename,
			bool removeFiles)
		{
			this.dataFeedString   = dataFeed;
			this.ipAddressString     = ipAddress;
			this.usernameString   = username;
			this.passwordString   = password;
			this.localDirString   = localDir;
			this.remoteDirString  = remoteDir;
			this.filenameFilterString = filenameFilter;
			this.missingFeedCounterString = missingFeedCounter;
			this.missingFeedThresholdString = missingFeedThreshold;
			this.dataFeedDatetimeString = dataFeedDatetime;
			this.dataFeedFilenameString = dataFeedFilename;
			this.removeFilesFlag  = removeFiles;

			this.IncomingRoot = null;

			// Initialise Operational events
			importStart         = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Info , startMessage);
			importFinishSuccess = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Info , finishMessage);
			importFinishFail    = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Error, failMessage);
			importFinishNoFile  = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Error, noFileMessage);	
			importFinishNoFileThresholdReached  = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Error, thresholdReachedMessage);	
		}		

		private string dataFeedString;
		private string ipAddressString;
		private string usernameString;
		private string passwordString;
		private string localDirString;
		private string remoteDirString;
		private string filenameFilterString;
		private int missingFeedCounterString;
		private int missingFeedThresholdString;
		private DateTime dataFeedDatetimeString;
		private string dataFeedFilenameString;
		private bool removeFilesFlag;	
		//string used to hold pscp command
		private string pscpProcessArgs;
		//string used to hold psftp command
		StringBuilder psftpProcessArgs = new StringBuilder();

		//string holds path to incoming directory on the DG server
		private static string incomingRootString;
		//used to format parameters passed to external processes
		const string quote = "\"";

		//arraylist to hold name of copied files
		ArrayList copiedFiles = new ArrayList();				

		/// <summary>
		/// LogStart makes a call to the LogStart method in  the LoggableTask class.
		/// This makes use of the TD Event Logging Service to Log the beggining of an import operation.
		/// </summary>
		protected override void LogStart()
		{
			base.LogStart();
			Logger.Write(importStart);
		}

		/// <summary>
		/// Log finish makes a call to the LogFinish method in the LoggableTask class.
		/// This makes use of the TD Event Logging Service to Log either the success or
		/// failure of an import operation.
		/// </summary>
		/// <param name="result">The status code returned from PerformTask()</param>
		protected override void LogFinish(int result)
		{
			base.LogFinish(result);
			
			switch (result)
			{
				case 0:
					Logger.Write(importFinishSuccess);
					break;

				case 1:
					Logger.Write(importFinishFail);
					break;

				case 2:
					Logger.Write(importFinishNoFileThresholdReached);
					break;

				default:
					Logger.Write(importFinishFail);
					break;
			}
			
		}

		/// <summary>
		/// Invokes the ftp utility to run ftp task set up in PreTask()
		/// </summary>
		protected override int PerformTask()
		{

			int statusCode = 0;

			IPropertyProvider currentProperties = Properties.Current;
			string pathToPuTTY = currentProperties["Gateway.PathToPuTTY"];

			try
			{
				FtpParameters ftp = new FtpParameters(dataFeedString, 1);

				Console.WriteLine("performing pSFTP pull..");
				OperationalEvent oe = 
					new  OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Info,
					"Starting transfer of feed " + dataFeedString);
				Logger.Write(oe);

                Logger.Write(
                    new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                    string.Format("{0}{1} {2}", pathToPuTTY, pscpExePath, pscpProcessArgs)
                ));

				//this creates a PSCP process	
				Process p = new Process();				
				ProcessStartInfo pInfo = new ProcessStartInfo(pathToPuTTY + pscpExePath, pscpProcessArgs);
				// Standard Settings
				pInfo.UseShellExecute = false;
				pInfo.CreateNoWindow = false;
				p.StartInfo = pInfo;
				p.Start();		
				p.WaitForExit();	

				statusCode = p.ExitCode;

                // Files should have been copied, get names of files that have been copied and are now in 
                // the incoming root, and output logging info
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Listing files in Incoming folder {0}:",
                                    dataFeedString)));

                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(localDirString);
                        FileInfo[] files = dir.GetFiles();
                        if (files != null)
                        {
                            foreach (FileInfo file in files)
                            {
                                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                    string.Format("Incoming folder {0} - File[{1}], Size[{2}bytes] LastWriteTime[{3}]",
                                        dataFeedString, file.Name, file.Length, file.LastWriteTime.ToString("yyyy-MM-ddTHH:mm:ss.fff"))));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                    string.Format("Exception trying to list files in Incoming folder: {0}. {1}",
                                        ex.Message, ex.StackTrace)));
                    }
                }

				//delete the file that has been transferred
				if(statusCode == 0)
				{
					if(removeFilesFlag == true)
					{
						statusCode = DeleteFeeds();

						oe = new  OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Info,
							"Delete for feed " + dataFeedString + " completed with exit code " + statusCode);
						Logger.Write(oe);
					}
				}

				if(ftp.SetData()) // Update counters depending on status code.
				{
					int counter = -1;

					if(statusCode == 0) // Success.
					{
						if(ftp.MissingFeedCounter != 0)
							counter = ftp.CounterReset();
						else
							counter = ftp.MissingFeedCounter;
					} // If status code == 0.
					else
					{
						if(ftp.MissingFeedCounter + 1 >= ftp.MissingFeedThreshold)
						{
							counter = ftp.CounterReset();
							statusCode = 2;
						}
						else
						{
							counter = ftp.CounterIncrement();
							statusCode = 1;
						}
					} // If status code != 0.

					this.missingFeedThresholdString = counter;
					this.dataFeedDatetimeString = ftp.DataFeedDatetime;
					this.dataFeedFilenameString = ftp.DataFeedFileName;

				} // If ftp.SetData().

				oe = new  OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Info,
					"Transfer of feed " + dataFeedString + " completed with exit code " + statusCode);
				Logger.Write(oe);
				
				Console.WriteLine("pSFTP pull completed");
			}
			catch(InvalidOperationException e)
			{
				statusCode = (int)TDExceptionIdentifier.DGPerformingFTPError;
				OperationalEvent oe = new  OperationalEvent(TDEventCategory.Database,TDTraceLevel.Error,
					statusCode.ToString()+ ":Error executing pSCP process:- "+e.Message);
				Logger.Write(oe);
			}
			catch(ArgumentException e)
			{
				statusCode = (int)TDExceptionIdentifier.DGPerformingFTPError;				
				OperationalEvent oe = new  OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					statusCode.ToString()+ ":Argument error executing pSCP process:- "+e.Message);
				Logger.Write(oe);
			}

			return statusCode;
		}
		
		protected override int PreTask()
		{
			// generate pSCP command			
			Console.WriteLine("preparing pSCP command...");
			int statusCode = 0;
			try
			{	
				
				pscpProcessArgs = " -pw " + passwordString + " " + quote + usernameString + "@" +
				ipAddressString + ":" + remoteDirString + "/" + "*.zip" + quote + " " + localDirString;			

			}
			catch (InvalidOperationException e)
			{
				statusCode = (int)TDExceptionIdentifier.DGPreparingFTPError;
				OperationalEvent oe = new  OperationalEvent(
					TDEventCategory.Database, TDTraceLevel.Error,
					statusCode.ToString()+ ":Error constructing the pSCP command:- "+e.Message);
				Logger.Write(oe);
			}
			catch (IOException e)
			{
				statusCode = (int)TDExceptionIdentifier.DGPreparingFTPError;
				OperationalEvent oe = new  OperationalEvent(
					TDEventCategory.Database, TDTraceLevel.Error,
					statusCode.ToString()+ ":Error constructing the pSCP command:- "+e.Message);
			}

					
			return statusCode;
		}

		/// <summary>
		/// need to override method in LoggableTask.cs but nothing needs to happen 
		/// in here at present
		/// </summary>
		protected override void PostTask(int statusCode)
		{	
			return;			
		}

		/// <summary>
		/// Deletes files that has been transferred from the ftp server reception folder
		/// </summary>
		/// <param name="fileName"></param>
		public int DeleteFeeds()
		{
			FileInfo[] files;
			DirectoryInfo dir;				

			int statusCode = 0;

			IPropertyProvider currentProperties = Properties.Current;
			string pathToPuTTY = currentProperties["Gateway.PathToPuTTY"];

			try
			{	

				//Retrieve default file paths
				getDirectoryPaths();

				//create delete script here
				StringBuilder tempString = new StringBuilder();
				FileInfo deleteScript = new FileInfo(IncomingRoot + "~delFeeds.tmp"); 
				
				//prepare the delete script
				StreamWriter sw = deleteScript.AppendText(); 				
				tempString = new StringBuilder();
				tempString.Append("cd ");
				tempString.Append(remoteDirString);
				sw.WriteLine(tempString.ToString());

				//generate PSFTP command			
				Console.WriteLine("preparing pSFTP command...");
				
				Process p = new Process();
				// Standard Settings
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.CreateNoWindow = false;
				// name of the command to invoke
				p.StartInfo.FileName = pathToPuTTY + psftpExePath;	

				//set up psftp arguments							
				psftpProcessArgs.Append(usernameString + "@");				
				psftpProcessArgs.Append(ipAddressString);
				psftpProcessArgs.Append(" -pw " + passwordString + " -b ");
				psftpProcessArgs.Append(deleteScript);	

				//get names of files that have been copied and are now in 
				//the incoming root
				dir = new DirectoryInfo(IncomingRoot + dataFeedString);
				files = dir.GetFiles();						
				foreach(FileInfo file in files)
				{
					Console.WriteLine("deleting datafeed..");
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Database,
						TDTraceLevel.Info,
						"Deleting feed:- " + file.Name);
					Logger.Write(oe);				

					//append name of file to delete to the script file 
					sw.WriteLine( "del " + quote + file.Name + quote);
				}

				//complete script
				sw.WriteLine("quit");

				//cleanup
				sw.Flush();
				sw.Close(); //Close the Stream and the file

				p.StartInfo.Arguments = psftpProcessArgs.ToString();
				//run psftp process to delete files
				p.Start();							
				p.WaitForExit();

				File.Delete(IncomingRoot + "~delFeeds.tmp");
				files = null;
				dir = null;		
			}
			
			catch (InvalidOperationException e)
			{
				statusCode = (int)TDExceptionIdentifier.DGPerformingFTPError;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error,
					statusCode.ToString()+ ":Error executing pSFTP script:- " +e.Message);
				Logger.Write(oe);
			}
			
			// FXCOP will complain that Exception should not be caught. However,
			// because we are launching an external process, many things can go
			// wrong since the external process is not controlled by our code. This
			// is why exception is caught because if ANY sort of exception is thrown,
			// then an event should be written to the log.
			catch(Exception e)
			{
				statusCode = (int)TDExceptionIdentifier.DGPerformingFTPError;
				// Write a log event to indicate that the psftp delete command has failed.
				OperationalEvent logEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error,
					statusCode.ToString()+ ":Deleting files on target server " +remoteDirString+ " has failed for " 
					+dataFeedString+ ":" +e.Message) ;
				Logger.Write(logEvent);
			}

			return statusCode;
			
		}

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

		public string PscpExePath
		{
			get { return pscpExePath; }
			set { pscpExePath = value; }
		}

		public string PsftpExePath
		{
			get { return psftpExePath; }
			set { psftpExePath = value; }
		}

		#endregion

		#region Private Methods
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
			if ((IncomingRoot.Length == 0))
			{
				returnCode = 1;
			}
			return returnCode;
		}
		#endregion
	}



		
		
	
}

