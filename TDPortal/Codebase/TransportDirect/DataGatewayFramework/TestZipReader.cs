//********************************************************************************
//NAME         : TestZipReader.cs
//AUTHOR       : Kenny Cheung
//DATE CREATED : 29/10/2003
//DESCRIPTION  : Nunit Test class to test the ZipReader
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/TestZipReader.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:16   mturner
//Initial revision.
//
//   Rev 1.12   Feb 10 2006 09:13:28   kjosling
//Turned off unit tests
//
//   Rev 1.11   Feb 09 2005 10:13:48   RScott
//Updated DEL5 refs to CodeBase
//
//   Rev 1.10   Feb 07 2005 14:32:44   bflenk
//Assertion changed to Assert
//
//   Rev 1.9   Apr 07 2004 17:18:38   jgeorge
//Unit test refactoring exercise
//
//   Rev 1.8   Nov 21 2003 20:04:26   TKarsan
//Inserted a new column in FTP configuration table for filename, checking exit codes after reflection.
//
//   Rev 1.7   Nov 14 2003 15:41:40   TKarsan
//No timeout and updated PKZip file location in properties.
//
//   Rev 1.6   Nov 13 2003 13:18:22   esevern
//various changes 
//
//   Rev 1.5   Nov 12 2003 10:15:14   esevern
//Changes to allow recording and checking of last successful zip file extraction.
//(by Phil Scott)
//
//   Rev 1.4   Nov 05 2003 15:06:36   JMorrissey
//Changed references to pkunzip to pkzip, as this is the application referenced in the properties xml file
//
//   Rev 1.3   Oct 31 2003 14:18:58   kcheung
//Updated description more,
//
//   Rev 1.2   Oct 31 2003 14:12:30   kcheung
//Updated description
//
//   Rev 1.1   Oct 31 2003 13:56:02   kcheung
//Complete with tests

/// ***************************
/// IMPORTANT EXECUTION NOTE:
/// ***************************
/// 
/// You must the PKZIP executable located in your C Drive. (i.e. C:\Pkzip.exe
/// must exist.)  The path to this file is configurable through the properties
/// service. For the NUnit tests, the properties Xml file simply points to the
/// "C:\PKZIP.exe".
/// 
/// Before running the tests, ensure that the directories containing the zip
/// files are set-up correctly. The main directory containing the test zip
/// files is named "TestZip". Inside this directory there are four sub-directories,
/// each containing some test files.
/// 
/// Before running the test ensure that the "Read Only"
/// property of each of the test files inside each of the Test Folders is UNCHECKED.
/// 
/// This must be done manually because if you
/// do a GET from PVCS - it will mark the file as READ-ONLY. Since the ZipProcess
/// will attempt to delete the file after extraction, the test will fail if this
/// step has not bee carried out. 
///
/// If you want to re-run the test, you must ensure that the directories and files
/// are set-up correctly again.  To ensure this is done, you must carry out the
/// following steps:
/// 
/// 1. Delete the current TestZip Directory
/// 2. Re-get the the "TestZip" directory from PVCS
/// 3. Untick the "Read Only" property of each of the test files.
/// 
/// This will ensure that the directories and files are set-up correctly.

/// Additionally, when running the tests, the NUnit program may freeze for about 10 seconds
/// between each of the tests. This is the normal. The reason why this happens is that
/// the code is waiting for the the PKZIP process to return or for it to timeout.

using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
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


namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// NUnit test class for the ZipReader
	/// </summary
	/// >
	/// 

	[TestFixture]
	public class TestZipReader
	{

		private SqlCommand cm;
		private SqlConnection connection;
		/// <summary>
		/// This is required by the property service for this test module. It is instantiated once in the test constructor.
		/// </summary>

		public TestZipReader()
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
			
			//create test user in the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestZip1", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			//Update date user data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestZip2", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();
		}
		[TearDown]
		public void CleanUp()
		{			
			SqlCommand cm = new SqlCommand("TestZip3", connection);
			cm.CommandType =  CommandType.StoredProcedure;

			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();
		}

		/// <summary>
		/// Test a normal extraction process. The initial state of the test directory
		/// should contain ONLY the zip file specified.
		/// 
		/// 
		/// Test Directory: C:\TDPortal\CodeBase\TransportDirect\DataGatewayFramework\TestZip\Test1\
		/// Test Zip File : test1.zip
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestZipReaderOne()
		{
			Console.WriteLine("NOTE: Read the set-up instructions in TestZipReader.cs before running the tests!");


			//Update date user data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestZip2", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();


			ZipReader zipReader = new ZipReader("SID9999",0);
			bool goodExtraction = zipReader.ProcessZipFile(@"..\..\TestZip\Test1\", "test1.zip");

			// Assert that the zip file has been correctly extracted
			Assert.IsTrue(goodExtraction, "ZIP File was not correctly extracted");

			string filename = zipReader.DataFeedFilename;
			string timePrepared = zipReader.TimePrepared.ToString();
			
			// Write For Debug
			Console.WriteLine(filename);
			Console.WriteLine(timePrepared);

			Assert.AreEqual("TDPZipConfig.txt", filename, "Filename was not as expected");
			Assert.AreEqual("28/10/2003 17:19:32", timePrepared, "Time Prepared was not as expected");
		}

		/// <summary>
		/// Test an extraction where the HEADER.XML will fail validation against
		/// the schema.
		/// 
		/// Test Directory: C:\TDPortal\CodeBase\TransportDirect\DataGatewayFramework\TestZip\Test2\
		/// Test Zip File : test2.zip
		/// </summary>
		[Test]
		public void TestZipReaderTwo()
		{
			Console.WriteLine("NOTE: Read the set-up instructions in TestZipReader.cs before running the tests!");
			//Update date user data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestZip2", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			ZipReader zipReader = new ZipReader("SID9999",0);
			bool goodExtraction = zipReader.ProcessZipFile("C:\\TDPortal\\CodeBase\\TransportDirect\\DataGatewayFramework\\TestZip\\Test2\\", "test2.zip");

			// Assert that the zip file returns false since validation has failed.
			Assert.IsTrue(!goodExtraction, "Zip file validation has failed.");
		}

		/// <summary>
		/// Test an extraction where the zip file does not contain HEADER.XML.
		/// 
		/// Test Directory: C:\TDPortal\CodeBase\TransportDirect\DataGatewayFramework\TestZip\Test3\
		/// Test Zip File : test4.zip
		/// </summary>
		[Test]
		public void TestZipReaderThree()
		{
			Console.WriteLine("NOTE: Read the set-up instructions in TestZipReader.cs before running the tests!");
			//Update date user data on the FTP_CONFIGURATION table on the database			
			cm = new SqlCommand("TestZip2", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			ZipReader zipReader = new ZipReader("SID9999",0);
			bool goodExtraction = zipReader.ProcessZipFile("C:\\TDPortal\\CodeBase\\TransportDirect\\DataGatewayFramework\\TestZip\\Test3\\", "test4.zip");

			// Assert that the zip file returns false since validation has failed.
			Assert.IsTrue(!goodExtraction, "Zip file validation failed");
		}
	}
}
