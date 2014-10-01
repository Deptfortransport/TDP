// ***********************************************
// NAME 		: ImportTask.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 11/08/2003
// DESCRIPTION 	: The Import Task class is a sub-class of Import Controller responsible
// for the actual import.  The correct subclass of ImportTask is then instantiated 
// using reflection.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/ImportTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:12   mturner
//Initial revision.
//
//   Rev 1.15   May 28 2004 10:03:08   CHosegood
//Added XML doco
//
//   Rev 1.14   Nov 17 2003 19:29:04   JMorrissey
//Serco feed fixed and integrated with Gateway.
//
//   Rev 1.13   Nov 17 2003 15:47:34   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.12   Oct 29 2003 17:35:20   acaunt
//importFilename property now returns the value of the filename variable (previously it returned the value of the importFilenameString variable which was always null
//
//   Rev 1.11   Oct 28 2003 17:14:46   acaunt
//Properties changed from private to protected
//
//   Rev 1.10   Sep 18 2003 16:01:48   MTurner
//changed parameter list for import method
//
//   Rev 1.9   Sep 17 2003 15:38:36   MTurner
//Added command line importer class
//
//   Rev 1.8   Sep 16 2003 15:00:04   MTurner
//changes after code review
//
//   Rev 1.7   Sep 10 2003 13:53:44   PScott
//code review changes
//
//   Rev 1.6   Sep 10 2003 13:47:24   mturner
//Changed return type of PerformTask()
//
//   Rev 1.5   Sep 10 2003 13:35:28   MTurner
//Changes after code review
//
//   Rev 1.4   Sep 03 2003 17:20:16   MTurner
//Changes to Event Logging
//
//   Rev 1.3   Sep 01 2003 14:11:38   MTurner
//Changes after comments by A. Caunt
//
//   Rev 1.2   Aug 29 2003 10:32:00   mturner
//Changes after FXCop scan
//
//   Rev 1.1   Aug 15 2003 13:35:40   mturner
//Not fully functional - checked in while on Leave
//
//   Rev 1.0   Aug 11 2003 14:20:46   mturner
//Initial Revision
//

using System;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using System.IO;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// The Import Task class is a sub-class of Import Controller responsible
	/// for the actual import of a data feed.  The correct subclass of ImportTask 
	/// is instantiated using reflection.
	/// </summary>
	public class ImportTask : LoggableTask
	{
		// Strings to be used in Operational events
		const string startMessage = "Import started for data feed ";
		const string finishMessage = "Import completed successfully for with error code 0 for data feed ";
		const string failMessage = "Import failed with error code ";
		const string noFileMessage = "Import failed with error code 1, Import file could not be found for data feed ";
		const string fileMessage = "Importing file: ";
		string filename = null;
		
		// Operational Events used to log events via the TD Event Logging Service
		static OperationalEvent importStart;
		static OperationalEvent importFinishSuccess;
		static OperationalEvent importFinishFail;
		static OperationalEvent importFinishNoFile;
		static OperationalEvent importDataFile;

		private string dataFeedString;
		private string importUtilityString;
		private string params1String;
		private string params2String;
		private string processingDirectoryString;
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">Parameters passed to the task</param>
        /// <param name="params2">Parameters passed to the task</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
		public ImportTask(string feed, string params1, string params2, 
							string utility, string processingDirectory)
		{
			this.dataFeed = feed;
			this.importUtility = utility;
			this.importFile = null;
			this.parameters1 = params1;
			this.parameters2 = params2;
			this.ProcessingDirectory = processingDirectory;
			
			// Initialise Operational events
			importStart         = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Info , startMessage + this.dataFeed);
			importFinishSuccess = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Info , finishMessage + this.dataFeed);
			importFinishNoFile  = new OperationalEvent(TDEventCategory.Business, 
				TDTraceLevel.Error, noFileMessage + this.dataFeed);
		}

		/// <summary>
		/// This method calls the other methods within this class in a pre-determined 
		/// order to control the sequence of logging and proccessing of an imported file.
		/// </summary>
		/// <param name="filename">The filename to be processed</param>
		public virtual int Run(string file)
		{
			filename = file;
			importDataFile = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info , 
                fileMessage + file);
			Logger.Write(importDataFile);

            // Log some file information
            if (TDTraceSwitch.TraceVerbose)
            {
                if (File.Exists(file))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("DataFeed[{0}] - File[{1}], Size[{2}bytes] LastWriteTime[{3}]",
                                    dataFeedString, fileInfo.Name, fileInfo.Length, fileInfo.LastWriteTime.ToString("yyyy-MM-ddTHH:mm:ss.fff"))));
                
                }
            }

			int result = base.Run();
			return result;
		}

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
		/// Log finish makes a call to the LogFinish method in  the LoggableTask class.
		/// This makes use of the TD Event Logging Service to Log either the success or
		/// failure of an import operation.
		/// </summary>
		/// <param name="result">The status code returned from PerformTask()</param>
		protected override void LogFinish(int result)
		{
			base.LogFinish(result);
			if(result == 0)
			{
				
				Logger.Write(importFinishSuccess);
			}
			else
			{
				if(result == 1)
				{
					Logger.Write(importFinishNoFile);
				}
				else
				{
					importFinishFail    = new OperationalEvent(TDEventCategory.Business, 
						TDTraceLevel.Error, failMessage + result);
					Logger.Write(importFinishFail);
				}
			}
		}

		/// <summary>
		/// Performs the actual proccessing of an import.
		/// </summary>
		protected override int PerformTask()
		{
			int statusCode = 0;
			CommandLineImporter importer = new CommandLineImporter(dataFeed, parameters1, parameters2, importUtility, ProcessingDirectory);
			statusCode = importer.Import(filename);
			return statusCode;
		}

		#region Properties
		/// <summary>
		/// The name of the data feed being imported
		/// </summary>
		protected string dataFeed
		{
			get
			{
				return dataFeedString; 
			}
			set
			{
				dataFeedString = value;
			}
		}

		/// <summary>
		/// The full file path of any underlying import utility to call
		/// </summary>
		protected string importUtility
		{
			get
			{
				return importUtilityString; 
			}
			set
			{
				importUtilityString = value;
			}
		}

		/// <summary>
		/// Name of file to be imported
		/// </summary>
		protected string importFile
		{
			get
			{
				return filename; 
			}
			set
			{
				filename = value;
			}
		}

		/// <summary>
		/// Parameters to pass to the underlying import utility before the file name
		/// </summary>
		protected string parameters1
		{
			get
			{
				return params1String; 
			}
			set
			{
				params1String = value;
			}
		}

		/// <summary>
		/// Parameters to pass to the underlying import utility after the file name
		/// </summary>
		protected string parameters2
		{
			get
			{
				return params2String; 
			}
			set
			{
				params2String = value;
			}
		}

		/// <summary>
		/// If a file needs to be copied to a particular directory for processing this 
		/// property is used to indicate this fact.  Otherwise the default is to process
		/// from the current location
		/// </summary>
		/// <value>Path to Processing Directory as a string or empty if no processing directory is needed</value>
		protected string ProcessingDirectory
		{
			get
			{
				return processingDirectoryString; 
			}
			set
			{
				processingDirectoryString = value;
			}
		}
		#endregion
	}
}

