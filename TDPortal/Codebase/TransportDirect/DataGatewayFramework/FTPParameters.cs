// ***********************************************
// NAME 		: FtpParameters.cs
// AUTHOR 		: Phil Scott
// DATE CREATED : 18/08/2003
// DESCRIPTION 	: A class that retrieves the FTP configuration parameters 
//              : from the SQL FTP_configuration database.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/FTPParameters.cs-arc  $ 
//
//   Rev 1.1   Nov 25 2010 14:23:48   rbroddle
//FTPparameters.cs changed to update date time value in FTP Configuration table using correct 24hr time format.
//Resolution for 5646: PermanentPortal.FTP_CONFIGURATION Data_Feed_DateTime value is being populated incorrectly
//
//   Rev 1.0   Nov 08 2007 12:20:12   mturner
//Initial revision.
//
//   Rev 1.13   May 13 2004 18:41:22   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.12   Nov 25 2003 15:41:50   TKarsan
//Fixed empty zip file and serco counters.
//
//   Rev 1.11   Nov 21 2003 20:04:22   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.10   Nov 12 2003 10:15:16   esevern
//Changes to allow recording and checking of last successful zip file extraction.
//(by Phil Scott)
//
//   Rev 1.9   Nov 11 2003 12:33:10   JMorrissey
//Now handles extrra columns added to ftp_configuration table
//
//   Rev 1.8   Sep 09 2003 17:23:30   PScott
//code review changes
//
//   Rev 1.7   Aug 29 2003 12:59:02   PScott
//work in progress
//
//   Rev 1.6   Aug 29 2003 12:49:16   PScott
//work in progress
//
//   Rev 1.5   Aug 29 2003 09:54:16   PScott
//work in progress
//
//   Rev 1.4   Aug 28 2003 16:33:22   PScott
//work in progress
//
//   Rev 1.3   Aug 27 2003 16:20:30   PScott
//work in progress
//
//   Rev 1.2   Aug 27 2003 15:30:54   PScott
//work in progress
//
//   Rev 1.1   Aug 26 2003 13:53:14   pscott
//work in progress
//
//   Rev 1.0   Aug 18 2003 16:30:26   PScott
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// This class loads FTP configuration Parameters via the sql database.
	/// The properties are then be accessed by the FTP controller which passes 
	/// these details on to FtpTask for the creation of FTP scripts
	/// </summary>
	public class FtpParameters 
	{
		private string dataFeedName;
		private string ipAddressString;
		private int    ftpClientflag;
		private bool   removeFilesFlag;
		private string usernameString;
		private string passwordString;
		private string localDirString;
		private string remoteDirString;
		private string filenameFilterString;
		private int missingFeedCounter;
		private int missingFeedThreshold;
		private DateTime dataFeedDatetime;
		private string dataFeedFileName;

		public FtpParameters(string dataFeedIn, int ftpClientIn)
		{
			DataFeed = dataFeedIn;
			FtpClient = ftpClientIn;
		}

		#region Properties
		/// <summary>
		/// ftpClient is an indicator stating if the FTP parameters are 
		/// 1 - for the FTP servers or 
		/// 2 - Gateway servers
		/// </summary>
		public int FtpClient
		{
			get
			{
				return this.ftpClientflag;
			}
			set
			{
				this.ftpClientflag = value;
			}
		}

		/// <summary>
		/// DataFeed is the unique data feed name that provides the key for obtaining
		/// other parameters from the data service.
		/// </summary>
		/// 
		public string DataFeed
		{
			get
			{
				return this.dataFeedName;
			}
			set
			{
				this.dataFeedName = value;
			}
		}

		/// <summary>
		/// ipAddress - the machine address of the data feed
		/// </summary>
		public string IPAddress
		{
			get
			{
				return this.ipAddressString;
			}
			set
			{
				this.ipAddressString = value;
			}
		}

		/// <summary>
		/// username is the log in username for the ftp session
		/// </summary>
		public string Username
		{
			get
			{
				return this.usernameString;
			}
			set
			{
				this.usernameString = value;
			}
		}

		/// <summary>
		/// password is the log in username for the ftp session
		/// </summary>
		public string Password
		{
			get
			{
				return this.passwordString;
			}
			set
			{
				this.passwordString = value;
			}
		}

		/// <summary>
		/// localDir is the directory on the local machine
		/// </summary>
		public string LocalDir
		{
			get
			{
				return this.localDirString;
			}
			set
			{
				this.localDirString = value;
			}
		}

		/// <summary>
		/// remoteDir is the directory path on the remote machine to FTP files from to
		/// </summary>
		public string RemoteDir
		{
			get
			{
				return this.remoteDirString;
			}
			set
			{
				this.remoteDirString = value;
			}
		}

		/// <summary>
		/// filenameFilter is the pattern that transferred files should
		/// satisfy e.g. '*.csv'
		/// </summary>
		public string FilenameFilter
		{
			get
			{
				return this.filenameFilterString;
			}
			set
			{
				this.filenameFilterString = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int MissingFeedCounter
		{
			get
			{
				return this.missingFeedCounter;
			}
			set
			{
				this.missingFeedCounter = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int MissingFeedThreshold
		{
			get
			{
				return this.missingFeedThreshold;
			}
			set
			{
				this.missingFeedThreshold = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime DataFeedDatetime
		{
			get
			{
				return this.dataFeedDatetime;
			}
			set
			{
				this.dataFeedDatetime = value;
			}
		}

		/// <summary>
		/// Previously successfully processed feed.
		/// </summary>
		public string DataFeedFileName
		{
			get
			{
				return this.dataFeedFileName;
			}
			set
			{
				this.dataFeedFileName = value;
			}
		}

			/// <summary>
		/// removeFiles is an indicator that the files will be removed from 
		/// the remote directory when successfully transfered
		/// </summary>
		public bool RemoveFiles
			{
			get
			{
				return this.removeFilesFlag;
			}
			set
			{
				this.removeFilesFlag = value;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// The configuration data for this particular import is read from the SQL table
		/// by use of td.common.databaseinfrastructure.dll. The values retreived are then 
		/// assigned to the corresponding properties of this class.
		/// </summary>
		public bool SetData()
		{
			bool readOk = false;
			SqlHelper sql = new SqlHelper();

			try
			{
				// Build the SQL query and then use it to retrieve the correct config data.
				string sqlQueryString;
				sqlQueryString = "SELECT data_feed,ip_address,username,password,local_dir,remote_dir,filename_filter," +
					"missing_feed_counter, missing_feed_threshold, data_feed_datetime, data_feed_filename, remove_files "
					+ "FROM ftp_configuration "
					+ "WHERE data_feed = '" + this.DataFeed + "' AND ftp_client = " + this.FtpClient.ToString();

				SqlDataReader configurationDataReader;
				sql.ConnOpen(SqlHelperDatabase.DefaultDB);
				configurationDataReader = sql.GetReader(sqlQueryString);
				if (configurationDataReader.Read())
				{
					// Assign retrieved parameters to the corresponding properties
					this.IPAddress      = configurationDataReader.GetString(1); 
					this.Username       = configurationDataReader.GetString(2);
					this.Password       = configurationDataReader.GetString(3); 
					this.LocalDir       = configurationDataReader.GetString(4);
					this.RemoteDir      = configurationDataReader.GetString(5);
					this.FilenameFilter = configurationDataReader.GetString(6);
					this.missingFeedCounter = configurationDataReader.GetInt32(7);
					this.missingFeedThreshold = configurationDataReader.GetInt32(8);
					this.dataFeedDatetime = configurationDataReader.GetDateTime(9);
					this.dataFeedFileName = configurationDataReader.GetString(10);
					this.RemoveFiles    = configurationDataReader.GetBoolean(11);
					readOk = true;
				}
				configurationDataReader.Close();
			}
			finally
			{
				if (readOk == false)
				{
					OperationalEvent oe = 
						new  OperationalEvent(	TDEventCategory.Database,
						TDTraceLevel.Error,
						"An error has occurred reading FTP configuration table");
					Logger.Write(oe);
				}
				sql.ConnClose();
			}
			return readOk;
		}

		/// <summary>
		/// Writes new zip file name and prepared time to ftp table.
		/// </summary>
		/// <param name="currentFeedDatetime">Prepared time</param>
		/// <param name="filename">zip file name</param>
		/// <returns></returns>
		public bool WriteNewTime(DateTime currentFeedDatetime, string filename)
		{
			bool wroteOk = false;

			SqlHelper sql = new SqlHelper();

			try
			{
				// Build the SQL query and then use it to retrieve the correct config data.
				string sqlQueryString;
				sqlQueryString = "UPDATE ftp_configuration set "
                    + "data_feed_datetime = '" + currentFeedDatetime.ToString("yyyy-MM-dd HH:mm:ss") + "', "
					+ "data_feed_filename = '" + filename + "' "
					+ "WHERE data_feed = '" + this.DataFeed  + "' "
					+ "AND ftp_client = "  + this.FtpClient.ToString();

				//open connection to the database
				sql.ConnOpen(SqlHelperDatabase.DefaultDB);
				sql.Execute(sqlQueryString);
				sql.ConnClose();

				dataFeedFileName = filename;
				dataFeedDatetime = currentFeedDatetime;

				wroteOk = true;
			}
			finally
			{
				sql.ConnClose();
			}


			return wroteOk;
		}

		/// <summary>
		/// Increments the current counter for the threshold.
		/// </summary>
		/// <returns>New value after the increment.</returns>
		public int CounterIncrement()
		{
			int counter = -1;

			SqlHelper sql = new SqlHelper();

			try
			{
				string sqlQueryString;
				sql.ConnOpen(SqlHelperDatabase.DefaultDB);

				sqlQueryString = "UPDATE ftp_configuration SET"
					+ " missing_feed_counter = missing_feed_counter + 1"
					+ " WHERE data_feed = '" + this.DataFeed  + "'"
					+ " AND ftp_client = "  + this.FtpClient.ToString();
				sql.Execute(sqlQueryString);

				sqlQueryString = "SELECT missing_feed_counter"
					+ " FROM ftp_configuration"
					+ " WHERE data_feed = '" + this.DataFeed  + "'"
					+ " AND ftp_client = "  + this.FtpClient.ToString();
				SqlDataReader reader = sql.GetReader(sqlQueryString);

				if (reader.Read())
				{
					counter = reader.GetInt32(0);
					missingFeedCounter = counter;
				}

				reader.Close();
				sql.ConnClose();
			}
			finally
			{
				sql.ConnClose();
			}


			return counter;
		}

		/// <summary>
		/// Resets the current counter to zero for the threshold.
		/// </summary>
		/// <returns>New value after reset, should be zero.</returns>
		public int CounterReset()
		{
			int counter = -1;

			SqlHelper sql = new SqlHelper();

			try
			{
				string sqlQueryString;
				sql.ConnOpen(SqlHelperDatabase.DefaultDB);

				sqlQueryString = "UPDATE ftp_configuration SET"
					+ " missing_feed_counter = 0"
					+ " WHERE data_feed = '" + this.DataFeed  + "'"
					+ " AND ftp_client = "  + this.FtpClient.ToString();
				sql.Execute(sqlQueryString);

				sqlQueryString = "SELECT missing_feed_counter"
					+ " FROM ftp_configuration"
					+ " WHERE data_feed = '" + this.DataFeed  + "'"
					+ " AND ftp_client = "  + this.FtpClient.ToString();
				SqlDataReader reader = sql.GetReader(sqlQueryString);

				if (reader.Read())
				{
					counter = reader.GetInt32(0);
					missingFeedCounter = counter;
				}

				reader.Close();
				sql.ConnClose();
			}
			finally
			{
				sql.ConnClose();
			}


			return counter;
		}

		#endregion
	}
}
