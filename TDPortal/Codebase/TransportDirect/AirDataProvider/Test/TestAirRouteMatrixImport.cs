// *********************************************** 
// NAME			: TestAirRouteMatrixImport.cs
// AUTHOR		: Atos Origin
// DATE CREATED	: 18/05/2004
// DESCRIPTION	: Class testing the funcationality of AirRouteMatrixImport
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirRouteMatrixImport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:46   mturner
//Initial revision.
//
//   Rev 1.5   Feb 08 2005 12:42:08   bflenk
//Ready_For_Build
//
//   Rev 1.4   Aug 26 2004 11:49:02   CHosegood
//Tests that modify existing data have been marked as IGNORE
//
//   Rev 1.3   Aug 25 2004 15:50:38   CHosegood
//Datafile name change
//
//   Rev 1.2   Aug 16 2004 09:59:14   CHosegood
//Updated tests
//
//   Rev 1.1   Jun 09 2004 18:03:02   CHosegood
//Files required by test are now located in \bin\Debug\AirDataProvider\ so that nUnit tests work on automated builds.
//
//   Rev 1.0   May 18 2004 18:41:44   CHosegood
//Initial revision.

using System;
using System.Diagnostics;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using NUnit.Framework;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Tests the funcationality of AirRouteMatrixImport
	/// </summary>
    [TestFixture]
	public class TestAirRouteMatrixImport
	{
        /// <summary>
        /// Default Constructor
        /// </summary>
		public TestAirRouteMatrixImport()
		{}

        /// <summary>
        /// Initialisation in setup method called before every test method
        /// </summary>
        [SetUp]
        public void Init() 
        {
            try 
            {
                // Initialise services
                TDServiceDiscovery.Init( new TestInitialisation() );

//                //Create the output directory if it doesn't exist
//                DirectoryInfo dir = new DirectoryInfo( Properties.Current["datagateway.rtel.output.path"] );
//                if ( !dir.Exists ) 
//                {
//                    dir.Create();
//                }

                //Create the input directory if it doesn't exist
                DirectoryInfo processingDir = new DirectoryInfo( Properties.Current["datagateway.rtel.output.path"] );
                if ( !processingDir.Exists ) 
                {
                    processingDir.Create();
                }

                //Copy the input file to the input directory for the test
                FileInfo file = new FileInfo( @".\AirDataProvider\IF066.xml.summer2004" );
                if ( file.Exists ) 
                {
                    string copyName = processingDir.FullName + @"\flightroutes.xml";

                    //If the destination file exists make sure it is not read-only so we can copy over it.
                    FileInfo dest = new FileInfo( copyName );
                    if ( dest.Exists ) 
                    {
                        File.SetAttributes( copyName, FileAttributes.Normal );
                    }

                    file.CopyTo( copyName, true );
                    File.SetAttributes( copyName, FileAttributes.Normal );
                }
                else 
                {
                    throw new Exception( file.FullName + " does not exist" );
                }
            } 
            catch ( Exception e ) 
            {
                Assert.Fail( "Error in init: " + e.Message  );
            }
        }

        /// <summary>
        /// Finalisation method called after every test method
        /// </summary>
        [TearDown]
        public void TearDown() { }

        /// <summary>
        /// Test handling of XML import file not existing.
        /// </summary>
        [Test]
        public void TestImportXMLFileNotFound() 
        {
            try 
            {
                AirRouteMatrixImportTask matrix = new AirRouteMatrixImportTask( "IF066", "", "", "", System.IO.Directory.GetCurrentDirectory() );
                int result = matrix.Run( "foo.txt" );
                Assert.IsTrue( result != 0, "File not found");
            }
            catch ( TDException e )
            {
                Assert.AreEqual(TDExceptionIdentifier.DGInputXMLFileNotFound , e.Identifier, "Expected to receive TDExceptionIdentifier.DGInputXMLFileNotFound");
            }
        }

        /// <summary>
        /// Test handling of invalid feed name.
        /// </summary>
        [Test]
        public void TestInvalidFeedName() 
        {
            try 
            {
                AirRouteMatrixImportTask matrix = new AirRouteMatrixImportTask( "IF070", "", "", "", System.IO.Directory.GetCurrentDirectory() );
                Assert.Fail( "Feedname test passed when should have failed" );
            }
            catch ( TDException e )
            {
                Assert.AreEqual(TDExceptionIdentifier.DGUnexpectedFeedName , e.Identifier, "Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName");
            }
        }

        /// <summary>
        /// Test handling of XML import file not existing.
        /// </summary>
        [Test]
        [ Ignore( "Not implemented" ) ]
        public void TestSchemaNotFound()
        {
        }

        /// <summary>
        /// Test handling of invalid XML file.
        /// </summary>
        [Test]
        [ Ignore( "Not implemented" ) ]
        public void TestInvalidXMLFile()
        {
        }

        /// <summary>
        /// Test handling of invalid XML file.
        /// </summary>
        [Test]
        [ Ignore( "Not implemented" ) ]
        public void TestStoredProcedureNotExist()
        {
        }

        /// <summary>
        /// Test handling of successful request.
        /// </summary>
        [Test]
        [Ignore( "Overwrites existing data in database" )]
        public void TestSuccess() 
        {
            int result = 0;

            FileInfo file = new FileInfo( Properties.Current["datagateway.rtel.output.path"] + @"\flightroutes.xml" );
            Assert.AreEqual( true, file.Exists, "Input file does not exist");

            AirRouteMatrixImportTask matrix = new AirRouteMatrixImportTask( "IF066", "", "", "", System.IO.Directory.GetCurrentDirectory() );
            result = matrix.Run( file.FullName );

            Assert.AreEqual(0, result );
        }
	}
}