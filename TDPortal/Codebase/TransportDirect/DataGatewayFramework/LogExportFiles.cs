// ***********************************************
// NAME 		: ELogExportFiles.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 12-Dec-2003
// DESCRIPTION 	: Application to transfer feed
//              : files to remote servers.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/LogExportFiles.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:14   mturner
//Initial revision.
//
//   Rev 1.2   May 13 2004 18:40:38   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.1   Dec 17 2003 10:20:50   TKarsan
//Enhacements for Gateway Export, added failed flag for the database log entry.
//
//   Rev 1.0   Dec 16 2003 14:04:56   TKarsan
//Initial Revision

using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Datagateway.Framework
{
	public enum ImportStatus
	{
		Processed = 1,
		Failed,
		Skipped
	}

	public struct FileStatus
	{
		public string fileName;
		public ImportStatus status;

		public FileStatus(string fileName, ImportStatus status)
		{
			this.fileName = fileName;
			this.status = status;
		}
	}

	/// <summary>
	/// Logs a series of file entries to the Data Gateway export table.
	/// </summary>
	public class LogExportFiles
	{
		private DateTime time_logged = DateTime.Now;
		private string serverSource, feedName;

		private ArrayList fileList = new ArrayList();

		/// <summary>
		/// Initialises values required to create entries for log table..
		/// </summary>
		/// <param name="feedName">Feed name being processed.</param>
		public LogExportFiles(string feedName)
		{
			this.feedName = feedName;
			serverSource = ConfigurationManager.AppSettings["datagateway.export.server"];
		}

		/// <summary>
		/// Inserts an entry for a new feed file and its status into internal list.
		/// </summary>
		/// <param name="fileName">Feed file name.</param>
		/// <param name="status">Import status flag.</param>
		public void AddFile(string fileName, ImportStatus importStatus)
		{
			fileList.Add(new FileStatus(fileName, importStatus));
		}

		/// <summary>
		/// Creates log enties into database table before clearing list.
		/// </summary>
		public int LogFiles()
		{
			int returnCode = (int)TDExceptionIdentifier.DGUpdateExportStatusFailed; 
			string query;
			IPropertyProvider currentProperties = Properties.Current;
			string[] servers = currentProperties["Gateway.Export.ServerID"].Split(',');

			SqlHelper sql = new SqlHelper();

			try
			{
				sql.ConnOpen(SqlHelperDatabase.TransientPortalDB);

				foreach(string destination in servers)
				{
					foreach(FileStatus fileStatus in fileList)
					{
						query = "INSERT INTO DataGatewayExport"
							+ " (server_src, server_dst, feed_name, feed_file, status_import, time_logged)"
							+ " VALUES (" + quote(serverSource) + quote(destination) + quote(feedName)
							+ quote(fileStatus.fileName) + quote((int) fileStatus.status) + quote(time_logged)
							+ ")";
						sql.Execute(query);
					}
				}

				returnCode = 0;
			}
			catch(SqlException e)
			{
				OperationalEvent oe = new  OperationalEvent(
					TDEventCategory.Database, TDTraceLevel.Error, 
					returnCode.ToString()+ ":Failed to log import status for export. " +e.Message);
				Logger.Write(oe);
			}
			finally
			{
				sql.ConnClose();
			}

			// Clean up.
			fileList.Clear();
			return returnCode;
		}

		private static string quote(string stringValue)
		{
			return "'" + stringValue.Trim() + "', ";
		}

		private static string quote(int integerValue)
		{
			return integerValue.ToString() + ", ";
		}

		private static string quote(DateTime dateTime)
		{
			return "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
		}
	}
}
