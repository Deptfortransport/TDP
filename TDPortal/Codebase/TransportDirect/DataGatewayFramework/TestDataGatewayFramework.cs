//********************************************************************************
//NAME         : TestDataGatewayFramework.cs
//AUTHOR       : Phil Scott / Mark Turner
//DATE CREATED : 01/09/2003
//DESCRIPTION  : Nunit Test class to test DataGatewayFramework 
//DESIGN DOC   : DD037/039/041/042 DataGateway
//********************************************************************************
// Version   Ref        Name            Date         Description
// V1.0      DD00xxx    PScott/MTurner  01/09/2003   Initial version
//
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/TestDataGatewayFramework.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:14   mturner
//Initial revision.
//
//   Rev 1.22   Feb 09 2006 16:20:04   kjosling
//Turned off unit tests. Mass configuration required on build machine to enable successful execution of tests
//
//   Rev 1.21   Aug 05 2005 13:54:42   mguney
//Location for the config csv file was changed from C:\\TDPortal\\DEL5\\TransportDirect\\Resource to C:\\TDPortal\\CodeBase\\TransportDirect\\Resource
//
//   Rev 1.20   Aug 05 2005 09:43:02   mguney
//Added to the project and changed the namespace to TransportDirect.Datagateway.Framework. Also some of the failing test methods were fixed. Not complete yet.
//
//   Rev 1.20   Jul 26 2005 12:15:26   MGuney
//Added to the project and changed the namespace to TransportDirect.Datagateway.Framework.
//
//   Rev 1.19   Nov 21 2003 20:04:26   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.18   Nov 17 2003 15:47:36   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.17   Nov 14 2003 20:30:36   JMorrissey
//more fixes to data gateway
//
//   Rev 1.16   Nov 14 2003 19:12:16   JMorrissey
//Fixed test harness
//
//   Rev 1.15   Nov 14 2003 16:56:02   JMorrissey
//update Delete method
//
//   Rev 1.14   Nov 13 2003 13:18:20   esevern
//various changes 
//
//   Rev 1.13   Nov 12 2003 18:50:56   JMorrissey
//updated test delete method
//
//   Rev 1.12   Nov 11 2003 12:35:20   JMorrissey
//Split up test code into individual methods
//
//   Rev 1.11   Oct 08 2003 10:59:18   JMorrissey
//Removed unused code:
//
////TDServiceDiscovery.Init(new ConsoleInitialisation());
//
//   Rev 1.10   Sep 17 2003 08:19:46   pscott
//Add further error codes
//
//   Rev 1.9   Sep 16 2003 14:32:26   MTurner
//Changed calls to ImportController.Import() to reflect changes to parameter list.
//
//   Rev 1.8   Sep 09 2003 17:23:34   PScott
//code review changes
//
//   Rev 1.7   Sep 04 2003 08:52:08   PScott
//Final pre code review version
//
//   Rev 1.6   Sep 03 2003 13:28:32   PScott
//work in progress
//
//   Rev 1.5   Sep 03 2003 11:47:26   MTurner
//Added testing for ImportController
//
//   Rev 1.4   Sep 03 2003 10:44:22   PScott
//work in progress
//
//   Rev 1.3   Sep 02 2003 11:54:34   PScott
//work in progress
//
//   Rev 1.2   Sep 02 2003 09:19:24   PScott
//work in progress
//
//   Rev 1.1   Sep 02 2003 09:13:14   PScott
//work in progress
//
//   Rev 1.0   Sep 01 2003 14:27:04   PScott
//Initial Revision
//

/// ***************************
/// IMPORTANT EXECUTION NOTE:
/// *************************** 
/// 
/// Before running the tests:
/// 
/// 1. Run the following vbscript to create some local folders: 
/// C:\TDPortal\DEL5\TransportDirect\Resource\DataGatewayAddFeeds.vbs 
/// C:\TDPortal\DEL5\TransportDirect\Resource\FTPAddFeeds.vbs (ignore any errors)
/// 
/// 2. Create folder C:\Gateway\dat\Reception\test\test_remote  
/// 
/// 3. Ensure that the following directory and test file 
/// is present:
/// C:\TDPortal\DEL5\TransportDirect\DataGatewayFramework\TestZip\Test1\test1.zip
/// 
/// You will have this file if you have done a Get Latest from PVCS for the 
/// DataGatewayFramework project. Before running the test ensure that the "Read Only"
/// property of this test file is UNCHECKED. This must be done manually because if you
/// do a GET from PVCS - it will mark the file as READ-ONLY. 
///
/// If you want to re-run the test, you must ensure that the test file
/// is set-up correctly again.  To ensure this is done, you must carry out the
/// following steps:
/// 
/// 1. Delete the current TestZip Directory
/// 2. Re-get the the "TestZip" directory from PVCS
/// 3. Untick the "Read Only" property of the test file.

using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections;
using System.Net; 
using System.Net.Sockets;

using System.Diagnostics;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common.Logging;



//MG 26/07/2005 using TransportDirect.Datagateway.Framework;
//namespace TransportDirectNUnit
namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// NUnit test class for test of DataGatewayFramework functionality.
	/// </summary>
	/// 
	[TestFixture]
	public class TestDataGatewayFramework
	{
		//objects/variables used throughout test methods		
		private string receptionPath;
		private string incomingPath;
		private string holdingPath;
		private string processingPath;
		private string ipAddress;
		private string dataFeed;
		private string fromFile;
		private string toFile;
		private string localDirString;
		private string remoteDirString;

		private int clientFlag;
		private int statusFlag;

		private SqlCommand cm;
		private SqlConnection connection;
		private FtpParameters FtpFileParameters;
		private ImportController newController;
		private FtpController iController;

		//booleans
		private bool isDataOk;
		private bool isTest;
		private bool fExists;

		//FtpParameters
		private string username;
		private string password;
		private string localDir;
		private string remoteDir;
		private string filenameFilter;
		private int missingFeedCounter;
		private int missingFeedThreshold;
		private DateTime dataFeedDatetime;
		private string dataFeedFilename;
		private bool removeFiles;		

		/// <summary>
		/// This is required by the property service for this test module. It is instantiated once in the test constructor.
		/// </summary>
		public class TestInitialization : IServiceInitialisation
		{
			public void Populate(Hashtable serviceCache)
			{
				// Enable PropertyService
				serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
				// Enable the Event Logging Service
				ArrayList errors = new ArrayList();
			
				IEventPublisher[] customPublishers = new IEventPublisher[0];
				try
				{
					Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
				}
				catch (TDException tdEx)
				{
					Assert.IsTrue(tdEx.Message.Length == 0,"Test Initialisation Exception " + tdEx.Message);
					return;
				}
			}
		}

		public TestDataGatewayFramework()
		{
			//
			// initialise service discovery
			//
			connection = 
				new SqlConnection("Integrated Security=SSPI;Server=.;Initial Catalog=PermanentPortal;Connect Timeout=30");	
			TDServiceDiscovery.Init(new TestInitialization());

		}
		
		/// <summary>
		/// set up common test configuration, data, etc.
		/// </summary>
		[SetUp]
		public void Init()
		{

			Console.WriteLine("NOTE: Read the set-up instructions in TestDataGatewayFrameworkr.cs before running the tests!");
			
			// Read the system paths			
			System.Data.OleDb.OleDbConnection myConnection = 
				new System.Data.OleDb.OleDbConnection( 
				"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\TDPortal\\CodeBase\\TransportDirect\\Resource;" +
				"Extended Properties='text;FMT=Delimited(;);HDR=YES'");
			System.Data.OleDb.OleDbCommand myCommand = 
				new System.Data.OleDb.OleDbCommand( 
				"SELECT * FROM Feedsconfig.csv", myConnection);
			myConnection.Open();
			System.Data.OleDb.OleDbDataReader myReader  = myCommand.ExecuteReader();
			myReader.Read();
			receptionPath = myReader["HomeDir"].ToString();
			incomingPath = myReader["IncomingDir"].ToString();
			holdingPath = myReader["HoldingDir"].ToString();
			//MG needed processing path for the imort controller test
			processingPath = myReader["ProcessingDir"].ToString();
			ipAddress    = myReader["MachineId"].ToString();
			
			//Set the data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestLoad1", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			//Set the data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestLoad2a", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			//Set the data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestLoad2b", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			//Set the data for the IMPORT_CONFIGURATION table on the database			
			cm = new SqlCommand("TestLoad5", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			//Set the data for the IMPORT_CONFIGURATION table on the database for test1			
			cm = new SqlCommand("TestLoad6", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();
			
			iController = new FtpController();			
		}

		/// <summary>
		/// tidy up test objects, data, etc
		/// </summary>
		[TearDown]
		public void CleanUp()
		{			
			SqlCommand cm = new SqlCommand("DataGatewayResetAfterTest", connection);
			cm.CommandType =  CommandType.StoredProcedure;

			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();
		}

		/// <summary>
		/// test FTPParameters.SetData returns sql database members
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestSetData()
		{

			//should find details for datafeed 'test'
			dataFeed = "testPush";
			clientFlag = 0;		
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);			
			//call SetData to get config from the SQL database.		
			bool isDataOk = FtpFileParameters.SetData();
            Assert.IsTrue(isDataOk,"TestSetData - FTP Parameters read failed");
            
		}

		/// <summary>
		/// test that correct values returned from FTPParameters
		/// </summary>
		[Test]
		public void TestFTPParameters()
		{			
			//should find details for datafeed 'test'
			dataFeed = "testPush";
			//MG 28/07/2005 Update database so that the expected values can be read and tested.
			SqlHelper sqlHelper = new SqlHelper();
			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
				string userScript = String.Format(
					"IF EXISTS (SELECT * FROM FTP_CONFIGURATION WHERE Data_Feed = '{0}') " + 
					"   UPDATE FTP_CONFIGURATION SET UserName = 'TDP28Nov', Password = 'Nov28Live', " +
					"   FTP_CLIENT = 0, LOCAL_DIR = 'C:/Gateway/dat/Incoming/{0}', REMOTE_DIR = '../{1}' " +
					"   WHERE Data_Feed = '{0}' " + 
					"ELSE " +
					"   INSERT FTP_CONFIGURATION (FTP_CLIENT,DATA_FEED,IP_ADDRESS,USERNAME,PASSWORD,LOCAL_DIR," +
					"      REMOTE_DIR,FILENAME_FILTER,MISSING_FEED_COUNTER,MISSING_FEED_THRESHOLD,DATA_FEED_DATETIME," +
					"      DATA_FEED_FILENAME,REMOVE_FILES) " +
					"   VALUES (0,'{0}','LocalHost','TDP28Nov','Nov28Live','C:/Gateway/dat/Incoming/{0}'," +
					"      '../{1}','*.zip',0,1,'20030101',' ',1)",dataFeed,"TDP28Nov");
				sqlHelper.Execute(userScript);				
			}
			catch
			{
				Assert.Fail("Error updating the FTP_CONFIGURATION table for the user details.");
			}
			finally
			{
				sqlHelper.ConnClose();
			}

			clientFlag = 0;		
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);	
			//call SetData to get config from the SQL database.		
			bool isDataOk = FtpFileParameters.SetData();
			
			username             = FtpFileParameters.Username;
			password             = FtpFileParameters.Password;
			localDir             = FtpFileParameters.LocalDir;
			remoteDir            = FtpFileParameters.RemoteDir;
			filenameFilter       = FtpFileParameters.FilenameFilter;
			missingFeedCounter   = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime     = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename     = FtpFileParameters.DataFeedFileName;
			removeFiles          = FtpFileParameters.RemoveFiles;

            Assert.IsTrue(username == "TDP28Nov", "TestFTPParameters - data not as expected");
            Assert.IsTrue(password == "Nov28Live", "TestFTPParameters - data not as expected");			
		}

		/// <summary>
		/// test to ensure exit ok when reading non existant item
		/// </summary>
		[Test]
		public void TestFtpParametersEmpty()
		{
			
			dataFeed = "test2";
			clientFlag = 0;
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();
			Assert.IsFalse(isDataOk,"TestFtpParametersEmpty not exited properly");

		}

		/// <summary>
		/// create data gateway type ftp user
		/// </summary>
		[Test]
		public void TestCreateFTPUser()
		{
			
//			SqlCommand cm = new SqlCommand("TestLoad2", connection);
//			cm.CommandType =  CommandType.StoredProcedure;
//			connection.Open();
//			cm.ExecuteNonQuery();
//			connection.Close();

			//should find details for datafeed 'test' 
			//username and password information is being read from the database.
			//this infromation must be read from the config file anyway.. 
			/*dataFeed = "testPush";
			clientFlag = 1;
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();
			Assertion.Assert("TestCreateFTPUser - FTP Parameters read failed", isDataOk == true);
			
			//ensure correct line can be read from properties
			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;

			Assertion.Assert("TestCreateFTPUser - FTP Parameters - data not as expected", username == "testPush");
			Assertion.Assert("TestCreateFTPUser - FTP Parameters - data not as expected", password == "test_pwd");
			*/

			//ensure exit ok when reading non existant item
			dataFeed = "test2";
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();
			Assert.IsFalse(isDataOk,"TestCreateFTPUser - FTP Parameters not exited properly");
		}

		/// <summary>
		///  test database item without datafeed set up
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestNoUser()
		{

			//should not find details for datafeed 'test2' as they do not exist
			dataFeed = "test2";
			clientFlag = 1;

			// test database item without user set up
			cm = new SqlCommand("TestLoad3", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();
			Assert.IsFalse(isDataOk,"TestNoUser - FTP Parameters not exited properly");
		}

		/// <summary>
		/// test file transfer from remote to FTP using pscp
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestRemoteToFTPPull()
		{

			dataFeed = "testPush";
			clientFlag = 0;
			isTest = false; 
			ipAddress = "";		

			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();

			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;
			localDir		  = FtpFileParameters.LocalDir;
			remoteDir	  = FtpFileParameters.RemoteDir;
			filenameFilter = FtpFileParameters.FilenameFilter;
			missingFeedCounter = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename = FtpFileParameters.DataFeedFileName;
			removeFiles	  = FtpFileParameters.RemoveFiles;		
			
			//set up where file is being moved from and to
			localDirString = receptionPath + localDir;
			remoteDirString = receptionPath + username + @"\" + remoteDir;

			//check if dummy zip file is already present in the test_remote directory
			//if it is not, then copy it
			fExists = File.Exists(localDirString +@"\Test1.zip");
			if (fExists == false)
			{
				fromFile = @"C:\TDPortal\CodeBase\TransportDirect\DataGatewayFramework\TestZip\Test1\Test1.zip";
				toFile = remoteDirString + @"\Test1.zip";
				File.Copy(fromFile,toFile,true);
			}			
			
			//call the controller 
			statusFlag = iController.TransferFiles(dataFeed, clientFlag, isTest, ipAddress);
			
			//check that dummy zip file was copied to incoming dir for test user
			fExists = File.Exists(localDirString +@"\Test1.zip");
			Assert.IsTrue(fExists,"TestFTPToDGPull - FTP Controller TransferFiles failed - did not copy");
			Assert.IsTrue(statusFlag <= 1,"TestFTPToDGPull - FTP Controller TransferFiles failed - status > 1 returned");
		}

		/// <summary>
		/// test file transfer from FTP to DG using pscp
		/// MG:   FAILS BECAUSE OF *.zip (wildchar. ftp server security issue)
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestFTPToDGPull()
		{
			dataFeed = "testPush";
			clientFlag = 1;
			isTest = false; 
			ipAddress = "";		

			//MG 29/07/2005 Update database so that the expected values can be read and tested.
			SqlHelper sqlHelper = new SqlHelper();
			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
				string userScript = String.Format(
					"IF EXISTS (SELECT * FROM FTP_CONFIGURATION WHERE Data_Feed = '{0}') " + 
					"   UPDATE FTP_CONFIGURATION SET UserName = 'TDP28Nov', Password = 'Nov28Live', FTP_CLIENT = 1, " +
					"      Local_Dir = 'C:/Gateway/dat/Incoming/{0}', Remote_Dir = '../{1}' " +
					"   WHERE Data_Feed = '{0}' " + 
					"ELSE " +
					"   INSERT FTP_CONFIGURATION (FTP_CLIENT,DATA_FEED,IP_ADDRESS,USERNAME,PASSWORD,LOCAL_DIR," +
					"      REMOTE_DIR,FILENAME_FILTER,MISSING_FEED_COUNTER,MISSING_FEED_THRESHOLD,DATA_FEED_DATETIME," +
					"      DATA_FEED_FILENAME,REMOVE_FILES) " +
					"   VALUES (1,'{0}','LocalHost','TDP28Nov','Nov28Live','C:/Gateway/dat/Incoming/{0}'," +
					"      '../{0}','*.*',0,1,'20030101',' ',1)",dataFeed,"TDP28Nov");
				sqlHelper.Execute(userScript);
			}
			catch
			{
				Assert.Fail("Error updating the FTP_CONFIGURATION table for the user details.");
			}
			finally
			{
				sqlHelper.ConnClose();
			}


			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();

			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;
			localDir		  = FtpFileParameters.LocalDir;
			remoteDir	  = FtpFileParameters.RemoteDir;
			filenameFilter = FtpFileParameters.FilenameFilter;
			missingFeedCounter = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename = FtpFileParameters.DataFeedFileName;
			removeFiles	  = FtpFileParameters.RemoveFiles;	
			
			//set up where file is being moved from and to
			localDirString = localDir;//incomingPath + localDir;
			remoteDirString = receptionPath + username;

			//check if dummy zip file is already present in the reception directory.
			//if it is not, then create it
			fExists = File.Exists(remoteDirString +@"\Test1.zip");
			if (fExists == false)
			{
				//29/07/2005 MG Instead of copying, creating file is a better practice.
				File.Create(remoteDirString + "\\Test1.zip").Close();
				//fromFile = @"C:\TDPortal\DEL5\TransportDirect\DataGatewayFramework\TestZip\Test1\Test1.zip";
				//toFile = remoteDirString + @"\Test1.zip";
				//File.Copy(fromFile,toFile,true);
			}
			
			//call the controller 
			statusFlag = iController.TransferFiles(dataFeed, clientFlag, isTest, ipAddress);
			
			//check that dummy zip file was copied to incoming dir for test user
			fExists = File.Exists(localDirString +@"\Test1.zip");
			Assert.IsTrue(fExists, "TestFTPToDGPull - FTP Controller TransferFiles failed - did not copy");
            Assert.IsTrue(statusFlag <= 1, "TestFTPToDGPull - FTP Controller TransferFiles failed - status > 1 returned");
		}

		/// <summary>
		/// check test flag function (no copies)
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestFlagFunction()
		{			
			dataFeed = "testPush";
			
			//MG 29/07/2005 Update database so that the expected values can be read and tested.
			SqlHelper sqlHelper = new SqlHelper();
			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
				string userScript = String.Format(
					"IF EXISTS (SELECT * FROM FTP_CONFIGURATION WHERE Data_Feed = '{0}') " + 
					"   UPDATE FTP_CONFIGURATION SET UserName = 'TDP28Nov', Password = 'Nov28Live', FTP_CLIENT = 1, " +
					"      Local_Dir = 'C:/Gateway/dat/Incoming/{0}', Remote_Dir = '../{1}' " +
					"   WHERE Data_Feed = '{0}' " + 
					"ELSE " +
					"   INSERT FTP_CONFIGURATION (FTP_CLIENT,DATA_FEED,IP_ADDRESS,USERNAME,PASSWORD,LOCAL_DIR," +
					"      REMOTE_DIR,FILENAME_FILTER,MISSING_FEED_COUNTER,MISSING_FEED_THRESHOLD,DATA_FEED_DATETIME," +
					"      DATA_FEED_FILENAME,REMOVE_FILES) " +
					"   VALUES (1,'{0}','LocalHost','TDP28Nov','Nov28Live','C:/Gateway/dat/Incoming/{0}'," +
					"      '../{1}','*.*',0,1,'20030101',' ',1)",dataFeed,"TDP28Nov");
				sqlHelper.Execute(userScript);
			}
			catch
			{
				Assert.Fail("Error updating the FTP_CONFIGURATION table for the user details.");
			}
			finally
			{
				sqlHelper.ConnClose();
			}

			localDirString  = incomingPath + dataFeed;
			remoteDirString = receptionPath + dataFeed;
			//localDirString  = receptionPath + localDir;
			//remoteDirString = receptionPath + username + @"\" + remoteDir;

			//MG 28/07/2005 
			//File.Delete(localDirString +@"\Control.ini");
			//MG 28/07/2005
			File.Create(remoteDirString + "\\Test1.zip").Close();
			File.Delete(localDirString +@"\Test1.zip");
						
			//MG 28/07/2005
			//Copy operation is commented out. 
			/*fromFile = @"C:\WINNT\Control.ini";
			toFile = remoteDirString + @"\Control.ini";
			File.Copy(fromFile,toFile,true);*/
			
			isTest = true;
			ipAddress = "";
			iController = new FtpController();
			statusFlag = iController.TransferFiles(dataFeed, 1, isTest, ipAddress);

			// now check it was copied to local dir. It shouldn't because we are in test mode. (isTest=true)
			fExists = File.Exists(localDirString +@"\Test1.zip");
			//MG 28/07/2005 Commented out.Control.ini is replaced by zip file
			//fExists = File.Exists(localDirString +@"\Control.ini");
			Assert.IsFalse(fExists,"TestFlagFunction - FTP Controller TransferFiles failed - Should not have copied the file in test mode");
			Assert.IsTrue(statusFlag == 0,"TestFlagFunction - FTP Controller TransferFiles failed - status 1 returned");

			
			/*localDirString  = incomingPath + localDir;
			remoteDirString = receptionPath + username + @"\" + remoteDir;
			File.Delete(localDirString +@"\Control.ini");*/

			//fromFile = @"C:\WINNT\Control.ini";
			//MG 28/07/2005
			//Copy operation is commented out.
			/*
			toFile = remoteDirString + @"\Control.ini";
			File.Copy(fromFile,toFile,true);*/
			
			/*dataFeed = "testPush";
			isTest = true;
			ipAddress = "";
			iController = new FtpController();
			statusFlag = iController.TransferFiles(dataFeed, 1, isTest, ipAddress);

			// now check it was copied to local dir
			fExists = File.Exists(localDirString +@"\Control.ini");
			Assertion.Assert("TestFlagFunction - FTP Controller TransferFiles failed - Should not have copied the file in test mode", fExists == false );
			Assertion.Assert("TestFlagFunction - FTP Controller TransferFiles failed - status 1 returned",statusFlag == 0);
*/
		}


		/// <summary>
		/// testing of the ImportController
		/// MG : Find a proper import zip file to be processed, change the line for file creation with copy,test again.
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestImportController()
		{
			dataFeed = "testPush";
			clientFlag = 1;
			isTest = false; 
			ipAddress = "";		

			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();

			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;
			localDir		  = FtpFileParameters.LocalDir;
			remoteDir	  = FtpFileParameters.RemoteDir;
			filenameFilter = FtpFileParameters.FilenameFilter;
			missingFeedCounter = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename = FtpFileParameters.DataFeedFileName;
			removeFiles	  = FtpFileParameters.RemoveFiles;

			//MG 04/08/2005 Copy file to be processed.
			//File.Create(localDir + "\\Test1.zip").Close();
			toFile = localDir + @"\Test1.zip";
			if (!File.Exists(toFile))
			{
				fromFile = @"C:\TDPortal\CodeBase\TransportDirect\DataGatewayFramework\TestZip\Test1\Test1.zip";			
				File.Copy(fromFile,toFile,true);
			}
			File.Delete(processingPath + dataFeed + @"\Test1.zip");

			ImportController newController = new ImportController();
			newController.Import(dataFeed,false,"","","",true);

			//check that the file extracted in the processing folder has  
			//ended up in the holding directory
			//fExists = File.Exists(holdingPath + localDir + @"\Test1.zip");
			//MG Holding path was not right.
			fExists = File.Exists(holdingPath + dataFeed + @"\Test1.zip");
			Assert.IsTrue(fExists,"TestImportController -  Import Controller failed to process the file");
		}

		/// <summary>
		/// test of alternate ipAddress
		/// MG - Fails because of the wildchar (*.zip). Security issue.
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestAlternateIPAddress()
		{

			dataFeed = "testPush";
			clientFlag = 0;
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();

			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;
			localDir		  = FtpFileParameters.LocalDir;
			remoteDir	  = FtpFileParameters.RemoteDir;
			filenameFilter = FtpFileParameters.FilenameFilter;
			missingFeedCounter = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename = FtpFileParameters.DataFeedFileName;
			removeFiles	  = FtpFileParameters.RemoveFiles;			

			localDirString  = receptionPath + localDir;
			remoteDirString = receptionPath + username + @"\" + remoteDir;

			cm = new SqlCommand("TestLoad4", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();


			//set up where file is being moved from and to
			localDirString = incomingPath + localDir;
			remoteDirString = receptionPath + username;
			
			fExists = File.Exists(localDirString +@"\Test1.zip");
			if (fExists == false)
			{
				File.Delete(localDirString +@"\Test1.zip");
			}
			//check if dummy zip file is already present in the reception directory.
			//if it is not, then copy it

			fExists = File.Exists(remoteDirString +@"\Test1.zip");
			if (fExists == false)
			{
				File.Create(remoteDirString + "\\Test1.zip").Close();
				//MG 04/08/2005 Commented out, replaced by file creation.
				/*fromFile = @"C:\TDPortal\DEL5\TransportDirect\DataGatewayFramework\TestZip\Test1.zip";
				toFile = remoteDirString + @"\Test1.zip";
				File.Copy(fromFile,toFile,true);*/
			}

			dataFeed = "testPush";
			isTest = false;			
			
			ipAddress = "127.0.0.1";			
			statusFlag = iController.TransferFiles(dataFeed, 1, isTest, ipAddress);

			// now check it was copied to local dir
			fExists = File.Exists(localDirString +@"\Test1.zip");
			Assert.IsTrue(fExists,"TestAlternateIPAddress - FTP Controller TransferFiles - failed copyng the file");
			Assert.IsTrue(statusFlag < 1,"TestAlternateIPAddress - FTP Controller TransferFiles failed - status 1 returned");

			dataFeed = "testPush";
			localDirString  = incomingPath + localDir;
			File.Delete(localDirString +@"\Test1.zip");


			// now test same alt ipaddress through the import function
			newController = new ImportController();
			newController.Import(dataFeed,false,"","",ipAddress,false);
			fExists = File.Exists(holdingPath + localDir + @"\Test1.zip");
			Assert.IsTrue(fExists,"TestAlternateIPAddress -  Import Controller failed to process the file");

		}

		/// <summary>
		/// tests the deletion of files using psftp 
		/// ensure that files of the same name exist in C:\Gateway\dat\Incoming\test and 
		/// C:\Gateway\dat\Reception\test\test_remote before running this test 
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestDeleteFeeds()
		{				
			//string used to hold psftp command
			StringBuilder psftpProcessArgs = new StringBuilder();
			DirectoryInfo   dir;
			FileInfo[]      files;			

			dataFeed = "testPush";

			//clientflag is 1 meaning that DeleteFeeds will only ever be run from the DG server
			clientFlag = 1;
			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();

			ipAddress      = FtpFileParameters.IPAddress;
			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;
			localDir       = FtpFileParameters.LocalDir;
			remoteDir	   = FtpFileParameters.RemoteDir;
			filenameFilter = FtpFileParameters.FilenameFilter;
			missingFeedCounter = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename = FtpFileParameters.DataFeedFileName;
			removeFiles	  = FtpFileParameters.RemoveFiles;
			
			//MG 27/07/2005
			remoteDirString = receptionPath + username + @"\" + remoteDir;

			//set directory to the reception directory on the FTP server
			dir = new DirectoryInfo(remoteDirString);			
			//check there are no files left in the reception directory 
			files = dir.GetFiles();						
			foreach(FileInfo file in files)
			{
				//if any files found then delete
				File.Delete(file.FullName);
			}
			//remoteDirString = @"c:\cygwin\home\testPush";//receptionPath + username + @"\" + remoteDir;

			//MG 27/07/2005
			//Create local files which are assumed to be transferred 
			File.Create(localDir + "\\1.zip").Close();
			File.Create(localDir + "\\2.zip").Close();
			//Make sure that the remote directory exists
			if (!Directory.Exists(remoteDirString))
				Directory.CreateDirectory(remoteDirString);
			//Create remote files which are assumed to be transferred. 			
			//We are going to chech whether these files are going to be deleted by DeleteFeeds.
			File.Create(remoteDirString + "\\1.zip").Close();
			File.Create(remoteDirString + "\\2.zip").Close();

			FtpTask ftpTask = new FtpTask(dataFeed, ipAddress, username, password,
				localDir, remoteDir, filenameFilter,missingFeedCounter, missingFeedThreshold,
				dataFeedDatetime,dataFeedFilename,removeFiles);					

			//delete files in the incoming directory
			ftpTask.DeleteFeeds();
			
			//set directory to the reception directory on the FTP server
			dir = new DirectoryInfo(remoteDirString);
			//check there are no files left in the reception directory 
			files = dir.GetFiles();
			
			bool filesExist = false;	
			foreach(FileInfo file in files)
			{
				//if any files found boolean is false
				filesExist = true;				

			}			

			Assert.IsFalse(filesExist,"TestDeleteFeeds -  DeleteFeeds failed to delete the processed files");

		}

		/// <summary>
		/// tests that a counter on the ftp_Configuration table is incremented when a
		/// datafeed fails and an error code 1 is returned
		/// 
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. No FTP client/server configuration on the buld machine. We need to install PUTTY (in the SW folder) and ensure that the directory structure is mirrored. Needs delivery systems input for configuration")]
		public void TestMissingFeedCounter ()
		{	

			dataFeed = "test1";
			clientFlag = 1;
			isTest = false; 
			ipAddress = "";		

			FtpFileParameters = new FtpParameters(dataFeed, clientFlag);
			isDataOk = FtpFileParameters.SetData();

			username       = FtpFileParameters.Username;
			password       = FtpFileParameters.Password;
			localDir		  = FtpFileParameters.LocalDir;
			remoteDir	  = FtpFileParameters.RemoteDir;
			filenameFilter = FtpFileParameters.FilenameFilter;
			//this missing feed counter should be incremented by 1 after this test run
			missingFeedCounter = FtpFileParameters.MissingFeedCounter;
			missingFeedThreshold = FtpFileParameters.MissingFeedThreshold;
			dataFeedDatetime = FtpFileParameters.DataFeedDatetime;
			dataFeedFilename = FtpFileParameters.DataFeedFileName;
			removeFiles	  = FtpFileParameters.RemoveFiles;

			ImportController newController = new ImportController();
			newController.Import(dataFeed,false,"","","",false);

			//get back the new missing feed counter value
			isDataOk = FtpFileParameters.SetData();
			int newCounter = FtpFileParameters.MissingFeedCounter;
			bool isIncremented = false;
			if (newCounter == missingFeedCounter + 1)
			{
				isIncremented = true;
			}
			
			Assert.IsTrue(isIncremented,"TestMissingFeedCounter -  Missing Feed Counter was not incremented");

		}
	}
	
}
