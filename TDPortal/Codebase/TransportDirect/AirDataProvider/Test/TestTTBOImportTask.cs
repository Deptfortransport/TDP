// *********************************************** 
// NAME                 : TestTTBOImportTask.cs 
// AUTHOR               : Atos Origin
// DATE CREATED         : 18/05/2004
// DESCRIPTION  : NUnit tests for testing
// TTBOImportTask class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestTTBOImportTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:48   mturner
//Initial revision.
//
//   Rev 1.5   Feb 09 2006 16:04:06   kjosling
//Disabled faulty unit tests pre DEL8.1 merger
//
//   Rev 1.4   Feb 08 2005 14:21:16   bflenk
//Assertion changed to Assert
//
//   Rev 1.3   Aug 26 2004 11:49:02   CHosegood
//Tests that modify existing data have been marked as IGNORE
//
//   Rev 1.2   Aug 13 2004 20:23:52   CHosegood
//cjp 6.0.0.0 changes.
//
//   Rev 1.1   Jun 10 2004 11:55:20   CHosegood
//Removed commentent out code.
//
//   Rev 1.0   Jun 09 2004 17:44:16   CHosegood
//Initial revision.
//
//   Rev 1.0   May 18 2004 10:55:48   CHosegood
//Initial revision.

using System;
using System.IO;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Summary description for TestTTBOImportTask.
	/// </summary>
    [TestFixture]
	public class TestTTBOImportTask
	{
        #region Instance Members
		private TTBOImportTask importTask;

        private string railFeedName = string.Empty;
        private string airFeedName = string.Empty;
        private string gatewayServer1 = string.Empty;
        private string gatewayServer2 = string.Empty;
        private string railCjpServer1 = string.Empty;
        private string railCjpServer2 = string.Empty;
        private string airCjpServer1 = string.Empty;
        private string airCjpServer2 = string.Empty;

        private static readonly string originalAirInputFile = @".\AirDataProvider\TTBO_Data_air.zip";
        private static readonly string originalRailInputFile = @".\AirDataProvider\TTBO_Data_rail.zip";
        private static readonly string processingDirectory = @"c:\temp\";
        private static readonly string testingInputFile = "TTBOUpdate.zip";
        #endregion

        /// <summary>
        /// 
        /// </summary>
		public TestTTBOImportTask() { }

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Init() 
        {
            // Initialise services
            TDServiceDiscovery.Init( new TestInitialisation() );

            railFeedName = Properties.Current["datagateway.ttbo.rail.feedname"];
            airFeedName = Properties.Current["datagateway.airbackend.schedules.feedname"];

            gatewayServer1 = Properties.Current["datagateway.ttbo.gatewayserver1"];
            gatewayServer2 = Properties.Current["datagateway.ttbo.gatewayserver2"];
            railCjpServer1 = Properties.Current["datagateway.ttbo.rail.cjpserver1"];
            railCjpServer2 = Properties.Current["datagateway.ttbo.rail.cjpserver2"];
            airCjpServer1 = Properties.Current["datagateway.ttbo.air.cjpserver1"];
            airCjpServer2 = Properties.Current["datagateway.ttbo.air.cjpserver2"];
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown] public void TearDown() 
        {
            string property = string.Empty;

            //Delete processing file.
            FileInfo processingFile = new FileInfo( processingDirectory + testingInputFile );
            if ( processingFile.Exists ) 
            {
                File.SetAttributes( processingFile.FullName, FileAttributes.Normal );
                processingFile.Delete();
            }

            //Delete rail cjpserver1 input file.
            property = Properties.Current["datagateway.ttbo.rail.cjpserver1"];
            if ( (property != string.Empty) && ( property.Split(' ').Length > 0 ) )
            {
                processingFile = new FileInfo( property.Split(' ')[0] );
                if ( processingFile.Exists ) 
                {
                    File.SetAttributes( processingFile.FullName, FileAttributes.Normal );
                    processingFile.Delete();
                }
            }

            //Delete rail cjpserver2 input file.
            property = Properties.Current["datagateway.ttbo.rail.cjpserver2"];
            if ( (property != string.Empty) && ( property.Split(' ').Length > 0 ) )
            {
                processingFile = new FileInfo( property.Split(' ')[0] );
                if ( processingFile.Exists ) 
                {
                    File.SetAttributes( processingFile.FullName, FileAttributes.Normal );
                    processingFile.Delete();
                }
            }

            //Delete air cjpserver1 input file.
            property = Properties.Current["datagateway.ttbo.air.cjpserver1"];
            if ( (property != string.Empty) && ( property.Split(' ').Length > 0 ) )
            {
                processingFile = new FileInfo( property.Split(' ')[0] );
                if ( processingFile.Exists ) 
                {
                    File.SetAttributes( processingFile.FullName, FileAttributes.Normal );
                    processingFile.Delete();
                }
            }

            //Delete air cjpserver2 input file.
            property = Properties.Current["datagateway.ttbo.air.cjpserver2"];
            if ( (property != string.Empty) && ( property.Split(' ').Length > 0 ) )
            {
                processingFile = new FileInfo( property.Split(' ')[0] );
                if ( processingFile.Exists ) 
                {
                    File.SetAttributes( processingFile.FullName, FileAttributes.Normal );
                    processingFile.Delete();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestInvalidFeed()
        {
            // Create an TTBOImportTask with an invalid feed name
            importTask = new TTBOImportTask( "dummy", "xxxxx", "", "", processingDirectory );
            int result = importTask.Run( testingInputFile );
            Assert.AreEqual((int) TDExceptionIdentifier.DGUnexpectedFeedName, result, "Wrong error code returned when invalid feed name is provided");
        }

//        /// <summary>
//        /// 
//        /// </summary>
//        [Test]
//        public void TestInvalidParametersRail()
//        {
//            // Create an TTBOImportTask where the parameters for the primary server is invalid
//            importTask = new TTBOImportTask(railFeedName, "xxxxx", "", "", processingDirectory);
//            int result = importTask.Run( testingInputFile );
//            Assertion.AssertEquals("Wrong error code returned when primary server parameters are invalid (RAIL)", (int)TDExceptionIdentifier.PRHInvalidImportParameters, result);
//
//            // Create an TTBOImprotTask with valid primary server parameters, but invalid secondary server parameters
//            importTask = new TTBOImportTask(railFeedName, "111 222", "xxxx", "", processingDirectory);
//            result = importTask.Run( testingInputFile );
//            Assertion.AssertEquals("Wrong error code returned when primary server parameters are invalid (RAIL)", (int)TDExceptionIdentifier.PRHInvalidImportParameters, result);
//        }
//
//        /// <summary>
//        /// 
//        /// </summary>
//        [Test]
//        public void TestInvalidParametersAir()
//        {
//            // Create an TTBOImportTask where the parameters for the primary server is invalid
//            importTask = new TTBOImportTask(airFeedName, "xxxxx", "", "", processingDirectory);
//            int result = importTask.Run( testingInputFile );
//            Assertion.AssertEquals("Wrong error code returned when primary server parameters are invalid (AIR)", (int)TDExceptionIdentifier.PRHInvalidImportParameters, result);
//			
//            // Create an TTBOImprotTask with valid primary server parameters, but invalid secondary server parameters
//            importTask = new TTBOImportTask(airFeedName, "111 222", "xxxx", "", processingDirectory);
//            result = importTask.Run( testingInputFile );
//            Assertion.AssertEquals("Wrong error code returned when primary server parameters are invalid (AIR)", (int)TDExceptionIdentifier.PRHInvalidImportParameters, result);
//        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
		[Ignore("ProjectNewkirk. This test appears to be dependant on a configuration setup on machine sg035027 which does not exist. There are also incorrect property names in the config file causing the test to fail")]
        public void TestInvalidFileRail()
        {
            // Create an TTBOImportTask and attempt to import a file that doesn't exist (RAIL)
            importTask = new TTBOImportTask(railFeedName, @".\target_file http:\\dummyurl", "", "", processingDirectory );
            //int result = importTask.Run(@".\td.userportal.retailbusiness.dll");
            int result = importTask.Run("no_such_file");
            Assert.AreEqual((int)TDExceptionIdentifier.PRHImportIOException, result, "Wrong error code returned when primary server parameters are invalid (RAIL)");

            // Create an TTBOImportTask where the primary targetFile is in a non existent directory (RAIL)
            importTask = new TTBOImportTask(railFeedName, @"C:\no_such_dir\target_file http:\\dummyurl", "", "", processingDirectory );
            result = importTask.Run(@".\td.userportal.retailbusiness.dll");
            Assert.AreEqual((int)TDExceptionIdentifier.PRHImportIOException, result, "Wrong error code returned when invalid file is passed in (RAIL)");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
		[Ignore("ProjectNewkirk. This test appears to be dependant on a configuration setup on machine sg035027 which does not exist. There are also incorrect property names in the config file causing the test to fail")]
        public void TestInvalidFileAir()
        {
            // Create an TTBOImportTask and attempt to import a file that doesn't exist (AIR)
            importTask = new TTBOImportTask(airFeedName, @".\target_file http:\\dummyurl", "", "", processingDirectory );
            //int result = importTask.Run(@".\td.userportal.retailbusiness.dll");
            int result = importTask.Run("no_such_file");
            Assert.AreEqual((int)TDExceptionIdentifier.PRHImportIOException, result, "Wrong error code returned when primary server parameters are invalid (AIR)");

            // Create an TTBOImportTask where the primary targetFile is in a non existent directory (AIR)
            importTask = new TTBOImportTask(airFeedName, @"C:\no_such_dir\target_file http:\\dummyurl", "", "", processingDirectory );
            result = importTask.Run(@".\td.userportal.retailbusiness.dll");
            Assert.AreEqual((int)TDExceptionIdentifier.PRHImportIOException, result, "Wrong error code returned when invalid file is passed in (AIR)");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
		[Ignore("ProjectNewkirk. This test appears to be dependant on a configuration setup on machine sg035027 which does not exist. There are also incorrect property names in the config file causing the test to fail")]
        public void TestServerUnavailableRail()
        {
            //          Copy the input file to the processing directory
            FileInfo info = new FileInfo( originalRailInputFile );
            info.CopyTo( processingDirectory + testingInputFile, true );

            // Create an TTBOImportTask with an invalid url (RAIL)
            importTask = new TTBOImportTask(railFeedName, @".\target_file http:\\dummyurl", "", "", processingDirectory );
            int result = importTask.Run( testingInputFile );
            Assert.AreEqual((int)TDExceptionIdentifier.PRHServerUnavailable, result, "Wrong error code returned when primary server is unavailable (RAIL)");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
		[Ignore("ProjectNewkirk. This test appears to be dependant on a configuration setup on machine sg035027 which does not exist. There are also incorrect property names in the config file causing the test to fail")]
        public void TestServerUnavailableAir()
        {
            //          Copy the input file to the processing directory
            FileInfo info = new FileInfo( originalAirInputFile );
            info.CopyTo( processingDirectory + testingInputFile, true );

            // Create an TTBOImportTask with an invalid url (AIR)
            importTask = new TTBOImportTask(airFeedName, @".\target_file http:\\dummyurl", "", "", processingDirectory );
            int result = importTask.Run( testingInputFile );
            Assert.AreEqual((int)TDExceptionIdentifier.PRHServerUnavailable, result, "Wrong error code returned when primary server is unavailable (AIR)");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Ignore( "Overwrites existing timetables" )]
        public void TestUpdateSuccessRail()
        {
//          Copy the input file to the processing directory
            FileInfo info = new FileInfo( originalRailInputFile );
            info.CopyTo( processingDirectory + testingInputFile, true );

            int result = 0;
            importTask = new TTBOImportTask( railFeedName, string.Empty, string.Empty, string.Empty, processingDirectory );
            result = importTask.Run( testingInputFile );

            Assert.AreEqual(0, result, "Success expected for Air Update");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Ignore( "Overwrites existing timetables" )]
        public void TestUpdateSuccessAir()
        {
//          Copy the input file to the processing directory
            FileInfo info = new FileInfo( originalAirInputFile );
            info.CopyTo( processingDirectory + testingInputFile, true );

            int result = 0;
            importTask = new TTBOImportTask( airFeedName, string.Empty, string.Empty, string.Empty, processingDirectory );
            result = importTask.Run(  testingInputFile );

            Assert.AreEqual(0, result, "Success expected for Air Update");
        }
	}
}