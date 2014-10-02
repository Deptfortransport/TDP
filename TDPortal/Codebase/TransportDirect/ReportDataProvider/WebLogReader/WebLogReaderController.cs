// ************************************************************ 
// NAME                 : WebLogReaderController
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Controller Class to process web logs.
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/WebLogReaderController.cs-arc  $
//
//   Rev 1.1   Dec 07 2011 13:54:32   DLane
//Updated the web log reader to ingnore the latest weblog file.
//Resolution for 5772: TDP and SJP web log reader processing locked files.
//
//   Rev 1.0   Nov 08 2007 12:40:58   mturner
//Initial revision.
//
//   Rev 1.16   Nov 11 2004 17:48:18   passuied
//Part of changes to enable WebLogReaders to read from multiple folders.
//
//   Rev 1.15   Apr 16 2004 13:38:18   geaton
//IR785 - changes to cope with hourly rotated web logs.
//
//   Rev 1.14   Mar 10 2004 11:24:32   geaton
//IR 652 and associated refactoring.
//
//   Rev 1.13   Dec 15 2003 17:51:20   geaton
//Updated to cope with zero client ip exlcudes.
//
//   Rev 1.12   Dec 15 2003 17:30:30   geaton
//Added support for filtering out log entries based on client IP address/es.
//
//   Rev 1.11   Nov 28 2003 10:40:46   geaton
//Added informational logging.
//
//   Rev 1.10   Nov 17 2003 20:15:46   geaton
//Refactored.
//
//   Rev 1.9   Oct 13 2003 18:18:10   PScott
//added traces
//
//   Rev 1.8   Oct 10 2003 16:30:42   geaton
//Removed references to Loggable task - this is no longer appropriate for this compoenent.
//
//   Rev 1.7   Oct 09 2003 10:42:44   ALole
//Updated WebLogReaderMain not to give any textual output on completion/failure.
//Updated W3CWebLogReader to correctly handle non GMT time on local machine.
//
//   Rev 1.6   Oct 07 2003 16:05:00   ALole
//Updated  TDExceptionIdentifier references
//
//   Rev 1.5   Oct 07 2003 11:56:40   PScott
//Added enums to exceptions and adjusted date time for gmt/bst change
//
//   Rev 1.4   Oct 01 2003 09:45:06   ALole
//Updated WebLogReader to support parameterisation of supported files. Also added a check to ensure that the HTTP Status code is between 200 and 299 (i.e. successful request). Changed the min page size to 5Mb.
//
//   Rev 1.3   Sep 16 2003 16:34:48   ALole
//Updated WebLogReader to use new WorkloadEvent
//
//   Rev 1.2   Sep 05 2003 12:13:46   ALole
//Changed the application name to td.weblogreader.exe
//Implemented code review changes
//Added support for not recording files under a certain size
//Only files automatically processed now are 'pages' i.e. asp, aspx, htm, html
//
//   Rev 1.1   Aug 29 2003 11:33:44   mturner
//Calls to run() changed to Run()
//
//   Rev 1.0   Aug 28 2003 13:35:22   ALole
//Initial Revision

using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.WebLogReader
{
	/// <summary>
	/// WebLogReaderController class.
	/// </summary>
	public class WebLogReaderController
	{
		private IPropertyProvider properties;
		private IWebLogReader reader;

		private readonly string logFileExtension = "log";

		/// <summary>
		/// Identifier used to prevent certain TD Portal web pages from being 
		/// logged as workload events, if used as URI query string.
		/// </summary>
		private readonly string ignoreMarker = "undvik=1";

		/// <summary>
		/// Range of HTTP status codes for which workload events should be logged.
		/// </summary>
		private readonly int minValidStatusCode = 200;
		private readonly int maxValidStatusCode = 299;

		/// <summary>
		/// Identifier used to specify that web log entries without an extension
		/// should be considered as a web page.
		/// </summary>
		private readonly string noFileExtensionMarker = "[none]";

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="properties">
		/// Property provider that supplies properties to use by the controller.
		/// </param>
		public WebLogReaderController(IPropertyProvider properties)
		{
			this.properties = properties;

			// Create a W3C web log reader to process web logs.
			reader = new W3CWebLogReader();
		}

		/// <summary>
		/// Method to process Web Log Files.
		/// </summary>
		/// <returns>
		/// Success code:
		/// Zero if processing was successful.
		/// Greater than zero if unsuccessful.
		/// </returns>
		public int Run()
		{
			int returnCode = 0;
			
			// Determine configurable properties.
			string[] validFileExtensions = properties[Keys.WebLogReaderWebPageExtensions].Split(' '); 		
			int minNonPageSize = int.Parse(properties[Keys.WebLogReaderNonPageMinimumBytes]);
			string[] clientIPExcludes = null;
			if (properties[Keys.WebLogReaderClientIPExcludes].Length != 0)
				clientIPExcludes = properties[Keys.WebLogReaderClientIPExcludes].Split(' '); 		
			else
				clientIPExcludes = new string[0];

			// Create a web log entry spec based on the properties.
			WebLogDataSpecification spec = 
				new WebLogDataSpecification(validFileExtensions,
											this.minValidStatusCode,
											this.maxValidStatusCode,
											minNonPageSize,
											clientIPExcludes,
											this.ignoreMarker,
											this.noFileExtensionMarker);

			// Get the property to get All folders to read from
			string[] webLogsFolders = properties[Keys.WebLogReaderWebLogFolders].Split(' ');
			
			// For each folder Process as with single folder
			foreach (string folder in webLogsFolders)
			{

				// the current key is formed from the template and includes the name of the current folder.
				string logDirectoryKey = string.Format(Keys.WebLogReaderLogDirectory, folder);
				string archiveDirectoryKey = string.Format(Keys.WebLogReaderArchiveDirectory, folder);

				// Determine list of files to process.
				ArrayList logFileNames = GetLogFileNames(properties[logDirectoryKey]);			
				if (logFileNames.Count == 0)
				{
					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Controller_NoWebLogs));
				}

				if (TDTraceSwitch.TraceInfo)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Controller_NumLogs, logFileNames.Count)));

				int currentFileIndex = 0;
				int filesProcessed = 0;

				try
				{	
					int workloadEventsLogged = 0;

					for (int i=0; i<logFileNames.Count; i++)
					{
						// Process a web log file.
						currentFileIndex = i;
						workloadEventsLogged = reader.ProcessWorkload((string)logFileNames[i], spec);

						// Move processed web log to archive directory.
						ArchiveWebLog((string)logFileNames[i],
							properties[logDirectoryKey],
							properties[archiveDirectoryKey]);

		
						filesProcessed++;

						if (TDTraceSwitch.TraceWarning)
						{
							if (workloadEventsLogged == 0)
								Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Controller_ZeroWorkloadEvents, (string)logFileNames[i])));
							else
								Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Controller_NumWorkloadEvents, workloadEventsLogged, (string)logFileNames[i])));
						}
					
					}

				
				}
				catch (TDException tdEx)
				{

					returnCode = (int)tdEx.Identifier;

					if (!tdEx.Logged)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure,
							TDTraceLevel.Error,
							String.Format(Messages.Controller_FailedProcessingWebLog, logFileNames[currentFileIndex], tdEx.Message, filesProcessed))); 
				}
			
				if (TDTraceSwitch.TraceInfo)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Controller_NumLogsProcessed, filesProcessed)));

			}
			return returnCode;
		}

		/// <summary>
		/// Determines list of web log files to process. Excludes active files.
		/// </summary>
		/// <param name="logDir">Directory containing log files.</param>
		/// <returns>List of all file names.</returns>
		private ArrayList GetLogFileNames(string logDir)
		{		
			// Determine list of all the web logs which have not been processed.
			string[] allFilepaths = Directory.GetFiles(logDir);
			ArrayList allLogFilepaths = new ArrayList();
			foreach (string fileName in allFilepaths)
			{
				// Only consider files with correct extension.
				string[] fileNameSplit = fileName.Split( '.' );

				if ( fileNameSplit[1] == this.logFileExtension )
					allLogFilepaths.Add(fileName);
			}

            // This code isn't working so just ignore the first file (when sorted alphapbetically
            /**
			// Determine names of active web logs which should not be processed.
			string[] activeLogFilenames = reader.GetActiveWebLogFileNames();
			ArrayList activeLogFilepaths = new ArrayList();
			foreach (string fileName in activeLogFilenames)
			{
				// Add full paths to active file names.
				string activeLogFilepath = logDir + "\\" + fileName;
				activeLogFilepaths.Add(activeLogFilepath);
			}
            */

            allLogFilepaths.Sort();

			// Determine list of inactive logs.
			ArrayList inactiveLogs = new ArrayList();
            string lastFilename = string.Empty;
            string lastMobileFilename = string.Empty;
            // Skip the last file as it'll be in use by IIS.
			foreach (string fileName in allLogFilepaths)
			{
                inactiveLogs.Add(fileName);
                if (fileName.IndexOf("MOB") >= 0)
                {
                    lastMobileFilename = fileName;
                }
                else
                {
                    lastFilename = fileName;
                }
			}

            if (lastFilename != string.Empty)
            {
                inactiveLogs.Remove(lastFilename);
            }
            if (lastMobileFilename != string.Empty)
            {
                inactiveLogs.Remove(lastMobileFilename);
            }
			return inactiveLogs;
		}

		/// <summary>
		/// Method to move a processed web log from the WebLog Dir to the Archive Dir.
		/// Catches any exceptions and rethrows them as a TDException 
		/// </summary>
		/// <param name="fileName">log file name to move</param>
		/// <param name="logDir">dir to move from</param>
		/// <param name="archiveDir">dir to move to</param>
		/// <returns></returns>
		private void ArchiveWebLog(string fileName, string logDir, string archiveDir) 
		{
			try
			{
				string[] splitFileName = fileName.Split('\\');
				File.Move( fileName, archiveDir + "\\" + splitFileName[splitFileName.Length - 1] );
			}
			catch (Exception ex)
			{
				throw new TDException(String.Format(Messages.Controller_FailedArchivingFile, ex.Message), false, TDExceptionIdentifier.RDPWebLogReaderArchiveFailed);
			}
		}
	}
}
