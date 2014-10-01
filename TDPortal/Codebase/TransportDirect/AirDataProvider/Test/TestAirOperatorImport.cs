// ***********************************************
// NAME			: TestAirOperatorImport.cs
// AUTHOR		: Atos Origin
// DATE CREATED	: 18/05/2004
// DESCRIPTION	: Class testing the funcationality of AirOperatorImport
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirOperatorImport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:44   mturner
//Initial revision.
//
//   Rev 1.5   Feb 08 2005 10:50:20   bflenk
//Changed Assertion to Assert
//
//   Rev 1.4   Aug 26 2004 11:49:02   CHosegood
//Tests that modify existing data have been marked as IGNORE
//
//   Rev 1.3   Aug 13 2004 10:31:30   CHosegood
//Changed tests not yet implemented to Ignore instead of failing
//
//   Rev 1.2   Jun 29 2004 17:17:54   acaunt
//Updated paths of live directory references
//
//   Rev 1.1   Jun 09 2004 18:03:00   CHosegood
//Files required by test are now located in \bin\Debug\AirDataProvider\ so that nUnit tests work on automated builds.
//
//   Rev 1.0   Jun 09 2004 17:44:16   CHosegood
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
	/// Summary description for TestAirOperatorImport.
	/// </summary>
	[TestFixture]
	public class TestAirOperatorImport
	{
		public TestAirOperatorImport() { }

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

                //Create the input directory if it doesn't exist
                //DirectoryInfo processingDir = new DirectoryInfo( Properties.Current["datagateway.data.live.directory"] );
                DirectoryInfo processingDir = new DirectoryInfo( @"C:\temp" );
                if ( !processingDir.Exists )
                {
                    processingDir.Create();
                }

                //Copy the input file to the input directory for the test
                FileInfo file = new FileInfo( @".\AirDataProvider\IF070_037.csv" );
                File.SetAttributes( file.FullName, FileAttributes.Normal );
                if ( file.Exists )
                {
                    string copyName = processingDir.FullName + @"\airOperators.csv";
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
                throw e;
            }
        }

        /// <summary>
        /// Finalisation method called after every test method
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            //DirectoryInfo processingDir = new DirectoryInfo( Properties.Current["datagateway.data.live.directory"] );
            DirectoryInfo processingDir = new DirectoryInfo( @"C:\temp" );

            //Remove the csv file.
            FileInfo file = new FileInfo( processingDir.FullName + @"\airOperators.csv" );
            if ( file.Exists )
            {
                file.Refresh();
                file.Delete();
            }

            //Remove the xml file.
            file = new FileInfo( processingDir.FullName + @"\airOperators.xml" );
            if ( file.Exists )
            {
                file.Refresh();
                file.Delete();
            }
        }

        /// <summary>
        /// Test handling of XML import file not existing.
        /// </summary>
        [Test]
        public void TestImportXMLFileNotFound()
        {
            try
            {
                int result = 0;
                AirOperatorImportTask import = new AirOperatorImportTask( Properties.Current["datagateway.airbackend.airoperator.feedname"], "", "", "", System.IO.Directory.GetCurrentDirectory() );
                result = import.Run( "foo.txt" );
                Assert.IsTrue( result != 0 );
            }
            catch ( TDException e )
            {
                Assert.AreEqual(TDExceptionIdentifier.ADFAirOperatorImportFileNotFound , e.Identifier, "Expected to receive TDExceptionIdentifier.ADFMatrixXMLNotFound" );
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
                AirOperatorImportTask import = new AirOperatorImportTask( "foo.feed", "", "", "", System.IO.Directory.GetCurrentDirectory() );
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

            //FileInfo file = new FileInfo( Properties.Current["datagateway.data.live.directory"] + @"\airOperators.csv" );
            FileInfo file = new FileInfo( @"C:\temp" + @"\airOperators.csv" );
            Assert.AreEqual(true, file.Exists, "Input file does not exist");

            AirOperatorImportTask import = new AirOperatorImportTask( Properties.Current["datagateway.airbackend.airoperator.feedname"], "", "", "", System.IO.Directory.GetCurrentDirectory() );
            result = import.Run( file.FullName );

            Assert.AreEqual( 0, result, "Unexpcted error occured");
        }
	}
}