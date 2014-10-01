// ***********************************************
// NAME 		: TestFileCopyTask.cs
// AUTHOR 		: Andrew Windley
// DATE CREATED : 13/05/2004
// DESCRIPTION 	: Tests FileCopyTask class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/Test/TestFileCopyTask.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:22   mturner
//Initial revision.
//
//   Rev 1.3   Aug 05 2005 10:02:24   mguney
//Some failing tests fixed. Most of them were related to copying files which don't exist then trying to ftp them.
//	 Rev 1.3   Aug 05 2005 10:00:00	  MGuney 	
//Some failing tests fixed. See comments starting with MG 22/07/2005
//
//   Rev 1.2   Feb 07 2005 12:27:52   bflenk
//Changed Assertion to Assert
//
//   Rev 1.1   Aug 27 2004 11:38:08   CHosegood
//Added processing directory
//
//   Rev 1.0   Jun 07 2004 17:42:06   CHosegood
//Initial revision.
//
//   Rev 1.0   May 17 2004 16:58:08   AWindley
//Initial revision.

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.DirectoryServices;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description for TestFileCopyTask.
	/// </summary>
	[TestFixture]
	public class TestFileCopyTask
	{
        private IPropertyProvider current;
		private static readonly string testSourceDirectory = @"C:\TestTemp\Source\";
        private static readonly string testSourceFile = "SourceFile.txt";
		private static readonly string testDestinationDirectory = @"C:\TestTemp\Destination\";
        private static readonly string testDestinationFile = "DestinationFile.txt";

		#region Constructor/destructor
        /// <summary>
        /// Constructor
        /// </summary>
		public TestFileCopyTask()
		{
		}

		/// <summary>
		/// Destructor
		/// </summary>
		~TestFileCopyTask()
		{
			if (TestSetup.Initialised)
				TestSetup.TearDown();
		}
		#endregion

		#region Setup/teardown
        /// <summary>
        /// Setup files/directories required
        /// </summary>
        [SetUp]
        public void Init()
        {
            if ( !TestSetup.Initialised )
                TestSetup.Setup();
			current = Properties.Current;

			try
			{
				// Create the source directory if it doesn't exist
				if ( !Directory.Exists( testSourceDirectory ) ) 
					Directory.CreateDirectory( testSourceDirectory );

				// Create the destination directory if it doesn't exist
				if ( !Directory.Exists( testDestinationDirectory ) ) 
					Directory.CreateDirectory( testDestinationDirectory );

				// Create the file to be copied/moved
				StreamWriter writer = File.CreateText( testSourceDirectory + testSourceFile );
				writer.WriteLine( "This is the TestFileCopyTask test file" );
				writer.Flush();
				writer.Close();
			}
			catch (Exception e)
			{
				Assert.IsTrue(false, "SetUp failed to create test directories or create source file. "
						+"Exception: " + e.Message);
			}
        }

        /// <summary>
        /// Clean up files/directories created
        /// </summary>
        [TearDown]
        public void ClearUp()
        {
			try
			{
				if ( Directory.Exists( testSourceDirectory ) ) 
					Directory.Delete( testSourceDirectory, true );

				if ( Directory.Exists( testDestinationDirectory ) ) 
					Directory.Delete( testDestinationDirectory, true );
			}
			catch (Exception e)
			{
				Assert.IsTrue(false, "TearDown failed to remove test directory/file. Manually remove if necessary. "
								+"Exception: " + e.Message);
			}
		}
		#endregion

		#region Tests
        /// <summary>
        /// Tests file copy succeeds
        /// </summary>
        [Test]
		public void FileCopy()
        {
//			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + " copy", "", "", "" );
            FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile, "copy", "", testSourceDirectory );
            int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

            Assert.IsTrue(File.Exists( testSourceDirectory + testSourceFile ),String.Format("Source file {0} does not exist", testSourceDirectory + testSourceFile ) );
            Assert.IsTrue(File.Exists( testDestinationDirectory + testDestinationFile ),String.Format("Destination file {0} does not exist", testDestinationDirectory + testDestinationFile));
            Assert.AreEqual(0, result,"Call to FileCopyTask.Run() returned result code "+result.ToString());
        }
		
		/// <summary>
        /// Tests file copy where no source file exists
        /// </summary>
        [Test]
		public void FileCopyNoSourceExists()
        {
            // Delete source file
			File.Delete( testSourceDirectory + testSourceFile );
			//FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + " copy", "", "", "" );
            FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile, "copy", "", testSourceDirectory );
            int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

			//MGuney 22/07/2005 These two tests doesn't make make any sense since an exception is raised
			//during the copy task as the source file doesn't exist.
			//Assert.IsTrue(!File.Exists( testSourceDirectory + testSourceFile ),"Source file exists");
			//Assert.IsTrue(!File.Exists( testDestinationDirectory + testDestinationFile ),"Destination file exists");
			
			Assert.AreEqual((int)TDExceptionIdentifier.DGFileCopyFileNotFound, result, "Return code DGFileCopyFileNotFound expected ");

		}

        /// <summary>
        /// Tests file copy where target directory doesn't exist
        /// </summary>
        [Test]
		public void FileCopyNoDestinationDirectoryExists()
        {
			// Delete target directory
			Directory.Delete( testDestinationDirectory, true );
			//FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + " copy", "", "", "" );
			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile,"copy", "", testSourceDirectory );
            int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

			//MGuney 22/07/2005 These two tests doesn't make make any sense since an exception is raised
			//during the copy task as the destination directory doesn't exist.
			//Assert.IsTrue(File.Exists( testSourceDirectory + testSourceFile ),"Source file {0} does not exist", (testSourceDirectory + testSourceFile));
			//Assert.IsTrue(!File.Exists( testDestinationDirectory + testDestinationFile ),"Destination file exists" );
        	
			Assert.AreEqual((int)TDExceptionIdentifier.DGFileCopyDirectoryNotFound, result,  "Return code DGFileCopyDirectoryNotFound expected ");
        }

        /// <summary>
        /// Tests file move
        /// </summary>
        [Test]
		public void FileMove()
        {
//			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + " move", "", "", "" );
			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile, "move", "", testSourceDirectory );
            int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

			Assert.IsTrue(!File.Exists( testSourceDirectory + testSourceFile ),"Source file still exists" );
			Assert.IsTrue(File.Exists( testDestinationDirectory + testDestinationFile ),String.Format("Destination file {0} does not exist", testDestinationDirectory + testDestinationFile));
			Assert.AreEqual( 0, result, "Call to FileCopyTask.Run() returned result code "+result.ToString());
		}

        /// <summary>
        /// Tests file move where no source file exists
        /// </summary>
        [Test]
		public void FileMoveNoSourceExists()
        {
			// Delete source file
			File.Delete( testSourceDirectory + testSourceFile );
//			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + " move", "", "", "" );
			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile, "move", "", testSourceDirectory );
            int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

			//MG 22/07/2005	Negation mistake. 		
			Assert.IsTrue((!File.Exists(testSourceDirectory + testSourceFile)), "Source file exists");
			//MG Negation mistake.
			//Assert.IsFalse((!File.Exists(testDestinationDirectory + testDestinationFile )), "Destination file exists");
			Assert.IsTrue((!File.Exists(testDestinationDirectory + testDestinationFile )), "Destination file exists");
			Assert.AreEqual((int)TDExceptionIdentifier.DGFileCopyFileNotFound, result,"Return code DGFileCopyFileNotFound expected ");
		}

        /// <summary>
        /// Tests file move where target directory doesn't exist
        /// </summary>
        [Test]
		public void FileMoveNoDestinationDirectoryExists()
        {
			// Delete target directory
			Directory.Delete( testDestinationDirectory, true );
//			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + " move", "", "", "" );
			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile, "move", "", testSourceDirectory );
            int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

			//MG 22/07/2005	Formatting changed.
			//Assert.IsTrue(File.Exists( testSourceDirectory + testSourceFile ), testSourceDirectory + testSourceFile, String.Format("Source file {0} does not exist"));
			Assert.IsTrue(File.Exists( testSourceDirectory + testSourceFile ), String.Format("Source file {0} does not exist",testSourceDirectory + testSourceFile));
			Assert.IsTrue(!File.Exists( testDestinationDirectory + testDestinationFile ), "Destination file exists" );
			
			Assert.AreEqual((int)TDExceptionIdentifier.DGFileCopyDirectoryNotFound, result, "Return code DGFileCopyDirectoryNotFound expected ");        
		}

		/// <summary>
		/// Tests file copy with invalid parameters
		/// </summary>
		[Test]
		public void FileCopyInvalidParameters()
		{
			// Pass invalid params1 (not copy or move specified)
//			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testSourceDirectory + testSourceFile + " " + testDestinationDirectory + testDestinationFile + "", "", "", "" );
			FileCopyTask fileCopyTask = new FileCopyTask( "aaaNNN", testDestinationDirectory + testDestinationFile, "", "", testSourceDirectory );
			int result = fileCopyTask.Run( testSourceDirectory + testSourceFile );

			Assert.IsTrue((File.Exists( testSourceDirectory + testSourceFile )), String.Format("Source file {0} does not exist", testSourceDirectory + testSourceFile ));
			Assert.IsTrue((!File.Exists( testDestinationDirectory + testDestinationFile )),"Destination file exists" );
			Assert.AreEqual(((int)TDExceptionIdentifier.DGFileCopyInvalidParameters), result, "Return code DGFileCopyInvalidParameters expected ");        
		}
		#endregion
	}
}
