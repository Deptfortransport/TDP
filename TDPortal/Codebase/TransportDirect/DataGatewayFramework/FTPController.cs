// ***********************************************
// NAME 		: FtController.cs
// AUTHOR 		: Phil Scott
// DATE CREATED : 19/08/2003
// DESCRIPTION 	: The FTP Controller obtains the configuration parameters for a data
// feed and invokes the FTP task class to create and execute an secure ftp session that transfers
// files via sftp to directories specified in the configuration
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/FTPController.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:12   mturner
//Initial revision.
//
//   Rev 1.17   May 13 2004 18:41:24   AWindley
//Updated error logging, now uses TDExceptionIdentifier
//
//   Rev 1.16   Jan 12 2004 11:54:32   jmorrissey
//Updated error codes and logged messages
//
//   Rev 1.15   Nov 21 2003 20:04:22   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.14   Nov 14 2003 19:12:16   JMorrissey
//Fixed test harness
//
//   Rev 1.13   Nov 11 2003 12:36:04   JMorrissey
//Now handles extra columns added to ftp_configuration database table
//
//   Rev 1.12   Nov 03 2003 17:47:16   JMorrissey
//Removed unused code regions. Corrected spellling mistake in message string. 
//
//   Rev 1.11   Sep 17 2003 08:19:44   pscott
//Add further error codes
//
//   Rev 1.10   Sep 09 2003 17:23:30   PScott
//code review changes
//
//   Rev 1.9   Sep 01 2003 14:27:12   PScott
//work in progress
//
//   Rev 1.8   Aug 29 2003 13:13:24   PScott
//work in progress
//
//   Rev 1.7   Aug 29 2003 13:09:14   PScott
//work in progress
//
//   Rev 1.6   Aug 29 2003 12:59:04   PScott
//work in progress
//
//   Rev 1.5   Aug 29 2003 12:49:18   PScott
//work in progress
//
//   Rev 1.4   Aug 29 2003 09:54:14   PScott
//work in progress
//
//   Rev 1.3   Aug 28 2003 16:33:26   PScott
//work in progress
//
//   Rev 1.2   Aug 27 2003 15:30:56   PScott
//work in progress
//
//   Rev 1.1   Aug 26 2003 13:53:00   pscott
//work in progress
//
//   Rev 1.0   Aug 19 2003 16:46:16   PScott
//Initial Revision

using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description FTP Controller.
	/// </summary>
	public class FtpController
	{
		public FtpController()
		{
		}

		private string ipAddress;
		private string username;
		private string password;
		private string localDir;
		private string remoteDir;
		private string filenameFilter;
		private bool removeFiles;
		private static string receptionPath;
		private static string incomingPath;
		private int missingFeedCounter;
		private int missingFeedThreshold;
		private DateTime dataFeedDatetime;
		private string dataFeedFileName;
		
		#region Public Methods
		/// <summary>
		/// Parameters are accepted from the FTPMain program and then used to retrieve additional
		/// configuration information from the FtpParameters class.
		/// All of this data is then used to instantiate an instance of FTPTask which carries out
		/// the actual proccessing of the file.
		/// </summary>
		/// <param name="dataFeed">Unique name of an import, that is the key for obtaining parameters from the data service</param>
		/// <param name="test">Used to see if data import solution supports specified feed</param>
		/// <param name="ipAddress">Alternative IP address to be used if primary server is unavailable</param>
	
		public int TransferFiles(string dataFeed, int clientFlag, bool test,string ipAddressAlt)
		{
			// Create instance of Data services and call properties to obtain  Class and call SetData to get config 
			// from the SQL database.
			
			//return code of 0 indicates success
			int retCode = 0;			

			//get directory paths from properties		
			IPropertyProvider currProps = Properties.Current;
			receptionPath  = currProps["Gateway.ReceptionPath"];		
			incomingPath   = currProps["Gateway.IncomingPath"];
			if ((receptionPath.Length == 0 && clientFlag ==0)
				|| (incomingPath.Length == 0 && clientFlag == 1))
			{
				retCode = (int)TDExceptionIdentifier.DGInvalidProperties;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
					TDTraceLevel.Error, retCode.ToString()+ ":Failed to retrieve directory paths from properties");
				Logger.Write(oe);
			}
			if (retCode == 0)
			{
				// Create instance of FtpParameters Class and call SetData to get config 
				// from the SQL database.
				FtpParameters FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
				bool isDataOk = FtpFileParameters.SetData();
				if (isDataOk == true)
				{
					ipAddress      = FtpFileParameters.IPAddress ;
					// Override ip address if alt ip address exists
					if(ipAddressAlt.Length != 0) ipAddress = ipAddressAlt;
			
					username       = FtpFileParameters.Username;
					password       = FtpFileParameters.Password;
					localDir       = FtpFileParameters.LocalDir;
					remoteDir      = FtpFileParameters.RemoteDir;
					filenameFilter = FtpFileParameters.FilenameFilter;
					missingFeedCounter = FtpFileParameters.MissingFeedCounter;
					missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
					dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
					dataFeedFileName = FtpFileParameters.DataFeedFileName;

					removeFiles    = (bool) FtpFileParameters.RemoveFiles;

					if (clientFlag == 0) //coming from remote server to ftp server
					{
						StringBuilder userDirectory = new StringBuilder();
						userDirectory.Append(receptionPath);
						userDirectory.Append(dataFeed); 
						localDir = userDirectory.ToString();
					}
					else   //coming from ftp server to dg server
					{
						StringBuilder userDirectory = new StringBuilder();
						userDirectory.Append(incomingPath);
						userDirectory.Append(dataFeed); 
						localDir = userDirectory.ToString();
					}
			
					if(test == true)
					{
						// check that FTP / Gateway server is set configured.
						// check that Data feed user has directories are available
						// in the reception directory.
						retCode = (int)TDExceptionIdentifier.DGInvalidProperties;
						if(Directory.Exists(localDir)) 
						{
							retCode = 0;
						}
					}
					else
					{
						// Pass the feed and config details to a new instance of FTPTask
						FtpTask FtpFileTask = new FtpTask(dataFeed, ipAddress, username, password,
							localDir, remoteDir, filenameFilter,missingFeedCounter, missingFeedThreshold,
							dataFeedDatetime,dataFeedFileName,removeFiles);
						retCode = FtpFileTask.Run();
					}
				}
				else
				{
					retCode = (int)TDExceptionIdentifier.DGInvalidConfiguration;
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Error,
						retCode.ToString()+ ":Configuration data for" +dataFeed+ " could not be read from database");
					Logger.Write(oe);
				}
			}
			return retCode;
		}
		#endregion	
	}
}
