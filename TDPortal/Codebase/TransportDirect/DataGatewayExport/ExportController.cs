// ***********************************************
// NAME 		: ExportController.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 12-Dec-2003
// DESCRIPTION 	: Application to transfer feed
//              : files to remote servers.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayExport/ExportController.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:04   mturner
//Initial revision.
//
//   Rev 1.0   Dec 17 2003 17:40:34   TKarsan
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

namespace TransportDirect.DataGatewayExport
{
	/// <summary>
	/// Unsed to store transfer options ready from the database.
	/// </summary>
	struct TransferInfo
	{
		public string feed, file, target;

		public TransferInfo(string feed, string file, string target)
		{
			this.feed   = feed   ;
			this.file   = file   ;
			this.target = target ;
		}
	}

	/// <summary>
	/// Transfer controller.
	/// </summary>
	public class ExportController
	{
		private string source = null;
		private string query  = null;

		/// <summary>
		/// Set up query and constants.
		/// </summary>
		/// <param name="target"></param>
		public ExportController(string target)
		{
			// Get source server ID from config file.
            source = ConfigurationManager.AppSettings["datagateway.export.server"];

			if(target == null)
				query = "SELECT  feed_name, feed_file, server_dst"
					+ " FROM DataGatewayExport"
					+ " WHERE server_src = " + quote(source)
					+ " AND status_transfer IN (0, 1)"
					+ " ORDER BY feed_file";
			else
				query = "SELECT  feed_name, feed_file, server_dst"
					+ " FROM DataGatewayExport"
					+ " WHERE server_src = " + quote(source)
					+ " AND server_dst = " + quote(target)
					+ " AND status_transfer IN (0, 1)"
					+ " ORDER BY feed_file";
		}

		/// <summary>
		/// Reads the database for transfer entry before calling routines to transfer.
		/// </summary>
		/// <returns>1 on errors on tranfer of any feed, 0 otherwise.</returns>
		public int Run()
		{
			int returnCode = 0;
			SqlHelper sql = new SqlHelper();

			try
			{
				DateTime time_sent = DateTime.Now; // Used to update database.
				ArrayList array = new ArrayList(); // To store transfer info.

				SqlDataReader reader;
				string feed, file, target, updateQuery;

				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Database, TDTraceLevel.Error,
					"Processing feed " + source);
				Logger.Write(oe);

				Console.WriteLine("Reading transfer info.");
				sql.ConnOpen(SqlHelperDatabase.TransientPortalDB);
				reader = sql.GetReader(query);

				while(reader.Read()) // Read transfer information.
				{
					feed   = reader.GetString(0).Trim();
					file   = reader.GetString(1).Trim();
					target = reader.GetString(2).Trim();

					array.Add(new TransferInfo(feed, file, target));
				}

				reader.Close(); // Close result set.

				foreach(TransferInfo info in array) // Perform transfer.
				{
					Console.WriteLine("Transferring {0} to {1} ", info.file, info.target);

					if(Transfer(info.feed, info.file, info.target) == 0) // Transfer.
					{
						updateQuery = "UPDATE DataGatewayExport"
							+ " SET status_transfer = 2,"
							+ " time_transfer = " + quote(time_sent)
							+ " WHERE server_src = " + quote(source)
							+ " AND feed_file = "    + quote(info.file)
							+ " AND server_dst = "   + quote(info.target)
							+ " AND feed_name = "    + quote(info.feed)
							+ " AND status_transfer IN (0, 1)";

							oe = new OperationalEvent(
								TDEventCategory.Database, TDTraceLevel.Error,
								"Good " + info.file);
							Logger.Write(oe);
					}
					else
					{
						updateQuery = "UPDATE DataGatewayExport"
							+ " SET status_transfer = 1,"
							+ " time_transfer = " + quote(time_sent)
							+ " WHERE server_src = " + quote(source)
							+ " AND feed_file = "    + quote(info.file)
							+ " AND server_dst = "   + quote(info.target)
							+ " AND feed_name = "    + quote(info.feed)
							+ " AND status_transfer IN (0, 1)";

						oe = new OperationalEvent(
							TDEventCategory.Database, TDTraceLevel.Error,
							"Error " + info.file);
						Logger.Write(oe);

						returnCode = 1; // Indicate there were transfer errors.
					}

					sql.Execute(updateQuery); // Update the database.
				}

				sql.ConnClose(); // Close database connection.
			}
			catch(SqlException e)
			{
				OperationalEvent oe =  new  OperationalEvent(
					TDEventCategory.Database, TDTraceLevel.Error, e.Message);
				Logger.Write(oe);
				returnCode = 2; // Indicate severe error.
			}
			finally
			{
				if(sql.ConnIsOpen)
					sql.ConnClose();
			}

			return returnCode;
		}

		/// <summary>
		/// Calls PuTTY to perform the transfer.
		/// </summary>
		/// <param name="feed">Feed / folder name.</param>
		/// <param name="file">File name to transfer.</param>
		/// <param name="target">Target server ID.</param>
		/// <returns>3 on parameter error, or whatever PuTTY return.</returns>
		private int Transfer(string feed, string file, string target)
		{
			string hst, uid, pwd, pscpArgs, src, dst;
			IPropertyProvider currentProperties = Properties.Current;

			string pathToPuTTY = currentProperties["Gateway.PathToPuTTY"];
			string pathHolding = currentProperties["Gateway.HoldingPath"];

			#region Get and verify properties.
			hst  = currentProperties[ "Gateway.Export." + target + ".ftp"  ];
			uid  = currentProperties[ "Gateway.Export." + target + ".uid"  ];
			pwd  = currentProperties[ "Gateway.Export." + target + ".pwd"  ];

			if(hst != null) hst = hst.Trim(); else return 3;
			if(uid != null) uid = uid.Trim(); else return 3;
			if(pwd != null) pwd = pwd.Trim(); else return 3;

			hst = hst.ToLower(); // use lower case for uid and hst.
			uid = uid.ToLower();

			if(hst.Length == 0) return 3;
			if(uid.Length == 0) return 3;
			if(pwd.Length == 0) return 3;
			#endregion

			src = pathHolding + feed + "/" + file;
			dst = uid + "@" + hst + ":/HOME/" + feed;
			pscpArgs = " -pw " + pwd + " " + src + " " + dst;

			#region Call psCP to transfer the file.
			Process p = new Process();
			ProcessStartInfo pInfo = new ProcessStartInfo(pathToPuTTY + "pscp.exe", pscpArgs);
			pInfo.UseShellExecute = false;
			pInfo.CreateNoWindow = false;
			p.StartInfo = pInfo;
			p.Start();		
			p.WaitForExit();
			#endregion

			return p.ExitCode;
		}

		#region quote methods
		private static string quote(string stringValue)
		{
			return "'" + stringValue.Trim() + "'";
		}

		private static string quote(DateTime dateTime)
		{
			return "'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
		}
		#endregion
	}
}
