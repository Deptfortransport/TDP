// *********************************************** 
// NAME                 : Keys.cs
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: String Formatters for Properties file
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/Keys.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:54   mturner
//Initial revision.
//
//   Rev 1.8   Nov 11 2004 17:48:16   passuied
//Part of changes to enable WebLogReaders to read from multiple folders.
//
//   Rev 1.7   Jun 23 2004 10:24:24   jgeorge
//Added extra key for UseLocalTime property
//
//   Rev 1.6   Apr 16 2004 13:38:58   geaton
//IR785 - changes to cope with hourly rotated web logs.
//
//   Rev 1.5   Dec 15 2003 17:30:32   geaton
//Added support for filtering out log entries based on client IP address/es.
//
//   Rev 1.4   Nov 17 2003 20:15:20   geaton
//Removed redundant key.
//
//   Rev 1.3   Oct 13 2003 18:18:26   PScott
//added traces
//
//   Rev 1.2   Oct 01 2003 09:45:04   ALole
//Updated WebLogReader to support parameterisation of supported files. Also added a check to ensure that the HTTP Status code is between 200 and 299 (i.e. successful request). Changed the min page size to 5Mb.
//
//   Rev 1.1   Sep 05 2003 12:13:44   ALole
//Changed the application name to td.weblogreader.exe
//Implemented code review changes
//Added support for not recording files under a certain size
//Only files automatically processed now are 'pages' i.e. asp, aspx, htm, html
//
//   Rev 1.0   Aug 28 2003 13:35:18   ALole
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.WebLogReader
{
	/// <summary>
	/// Summary description for Keys.
	/// </summary>
	public class Keys
	{
		public const string WebLogReaderWebLogFolders = "WebLogReader.WebLogFolders";
		public const string WebLogReaderArchiveDirectory = "WebLogReader.{0}.ArchiveDirectory";
		public const string WebLogReaderLogDirectory = "WebLogReader.{0}.LogDirectory";
		public const string WebLogReaderNonPageMinimumBytes = "WebLogReader.NonPageMinimumBytes";
		public const string WebLogReaderWebPageExtensions = "WebLogReader.WebPageExtensions";
		public const string WebLogReaderClientIPExcludes = "WebLogReader.ClientIPExcludes";
		public const string WebLogReaderRolloverPeriod = "WebLogReader.RolloverPeriod";
		public const string WebLogReaderUseLocalTime = "WebLogReader.UseLocalTime";
	}
}
