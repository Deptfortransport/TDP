// ************************************************************ 
// NAME                 : WebLogReader
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Abstract class for all WebLogReaders.
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/WebLogReader.cs-arc  $
//
//   Rev 1.1   Nov 16 2009 13:47:38   SCraddock
//Updated to resolve incorrect logging of .jpeg files
//
//   Rev 1.0   Nov 08 2007 12:40:56   mturner
//Initial revision.
//
//   Rev 1.6   Apr 16 2004 13:38:44   geaton
//IR785 - changes to cope with hourly rotated web logs.
//
//   Rev 1.5   Mar 10 2004 11:24:24   geaton
//IR 652 and associated refactoring.
//
//   Rev 1.4   Nov 28 2003 10:40:28   geaton
//Return the number of workload events logged for use in informational logging.
//
//   Rev 1.3   Nov 17 2003 20:15:42   geaton
//Refactored.
//
//   Rev 1.2   Nov 14 2003 16:41:10   geaton
//Corrected use of abstract class and method.
//
//   Rev 1.1   Oct 01 2003 09:45:08   ALole
//Updated WebLogReader to support parameterisation of supported files. Also added a check to ensure that the HTTP Status code is between 200 and 299 (i.e. successful request). Changed the min page size to 5Mb.
//
//   Rev 1.0   Aug 28 2003 13:35:20   ALole
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.WebLogReader
{

	/// <summary>
	/// Enumeration containing log rollover periods.
	/// ie the period after which IIS creates a new web log file.
	/// </summary>
	public enum LogRolloverPeriods : int
	{
		Hourly,
		Daily
	}


	/// <summary>
	/// Structure used to define web log entry specification.
	/// This specification defines the requirements that must be met in order
	/// for the web log reader to log a <code>WorkloadEvent</code> for a web log
	/// entry in the web log being processed.
	/// </summary>
	public struct WebLogDataSpecification
	{
		private string[] webPageFileExtensions;
		public string[] WebPageFileExtensions
		{
			get {return webPageFileExtensions;}
		}

		private int minProtocolStatusCode;
		public int MinProtocolStatusCode
		{
			get {return minProtocolStatusCode;}
		}

		private int maxProtocolStatusCode;
		public int MaxProtocolStatusCode
		{
			get {return maxProtocolStatusCode;}
		}

		private int minNonWebPageSize;
		public int MinNonWebPageSize
		{
			get {return minNonWebPageSize;}
		}

		private string[] clientIPExcludes;
		public string[] ClientIPExcludes
		{
			get {return clientIPExcludes;}
		}

		private string queryIgnoreMarker;
		public string QueryIgnoreMarker
		{
			get {return queryIgnoreMarker;}
		}

        private string noFileExtensionMarker;
        public string NoFileExtensionMarker
        {
            get { return noFileExtensionMarker; }
        }

		private bool allowNoFileExtension;
		public bool AllowNoFileExtension
		{
			get {return allowNoFileExtension;}
		}

		/// <summary>
		/// Creates a web log data specification.
		/// </summary>
		/// <param name="webPageFileExtensions">Array of file extensions that are considered to be a web page.</param>
		/// <param name="minStatusCode">Minimum HTTP status code.</param>
		/// <param name="maxStatusCode">Maximum HTTP status code.</param>
		/// <param name="minSize">Minimum size in bytes for pages that do NOT have a valid file extension.</param>
		/// <param name="clientIPExcludes">IP addresses to exclude.</param>
		/// <param name="queryIgnoreMarker">Marker string that if found in query string, signifies exclusion.</param>
		/// <param name="noFileExtensionMarker">Marker string used to specify that web log entries with no file extension should be considered as a web page.</param>
		public WebLogDataSpecification(string[] webPageFileExtensions,
									   int minProtocolStatusCode,
									   int maxProtocolStatusCode,
									   int minNonWebPageSize,
									   string[] clientIPExcludes,
									   string queryIgnoreMarker,
									   string noFileExtensionMarker)
		{
			this.webPageFileExtensions = new String[webPageFileExtensions.Length];
			this.clientIPExcludes = new String[clientIPExcludes.Length];
			Array.Copy(webPageFileExtensions, this.webPageFileExtensions, webPageFileExtensions.Length);
			Array.Copy(clientIPExcludes, this.clientIPExcludes, clientIPExcludes.Length);
			this.minProtocolStatusCode = minProtocolStatusCode;
			this.maxProtocolStatusCode = maxProtocolStatusCode;
			this.minNonWebPageSize = minNonWebPageSize;
			this.queryIgnoreMarker = queryIgnoreMarker;
            this.noFileExtensionMarker = noFileExtensionMarker;
			this.allowNoFileExtension = false;
			for ( int i = 0; i < webPageFileExtensions.Length; i++ )
			{
				if (webPageFileExtensions[i] == noFileExtensionMarker)
				{
					this.allowNoFileExtension = true;
					break;
				}
			}
		}

	}

	/// <summary>
	/// Interface for web log readers.
	/// Readers for specific log formats should implement this class.
	/// </summary>
	public interface IWebLogReader
	{

		/// <summary>
		/// Processes the workload for a given web log file. 
		/// Logs WorkLoadEvent events for each entry read from web log
		/// that meets the specification passed.
		/// </summary>
		/// <param name="filePath">
		/// Full filepath to web log to process.
		/// </param>
		/// <param name="dataSpecification">
		/// Specification that must be met for web log data to be given a workload event.
		/// </param>
		/// <returns>
		/// Number of workload events logged for the file processed. This is used for informational purposes.
		/// </returns>
		/// <exception cref="TDException">
		/// Thrown if error when processing the web log.
		/// </exception>
		int ProcessWorkload(string filePath, WebLogDataSpecification dataSpecification);

		/// <summary>
		/// Returns the filenames of web logs that should be treated
		/// as active and not processed.
		/// </summary>
		/// <returns>Filenames of active web logs.</returns>
		string[] GetActiveWebLogFileNames();

	}
}

