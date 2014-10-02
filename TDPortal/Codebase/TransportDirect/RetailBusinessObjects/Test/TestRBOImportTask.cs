// *********************************************** 
// NAME			: TestRBOImport.cs
// AUTHOR		: 
// DATE CREATED	: 29/10/03
// DESCRIPTION	: Implementation of the TestRBOImport class
// ************************************************ 

using System;
using System.IO;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Test harness for TestRBOImport
	/// </summary>
	[TestFixture]
	public class TestRBOImportTask
	{
		RBOImportTask importTask;

		private string FBO_UPDATE;
		private string RBO_UPDATE;
		private string LBO_UPDATE;
		private string RVBO_UPDATE;
		private string SBO_UPDATE;

		public TestRBOImportTask()
		{
			TDServiceDiscovery.Init(new RBOServiceInitialisation());	
			FBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.fbofeedname"];
			RBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.rbofeedname"];
			LBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.lbofeedname"];
			RVBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.rvbofeedname"];
			SBO_UPDATE = Properties.Current["datagateway.retailbusinessobjects.sbofeedname"];
		}

		[SetUp]
		public void Init()
		{		
			// Delete the target file if we have previously created one
		}

		[TearDown]
		public void CleanUp()
		{
			// Delete the target file if we have previously created one
			if (File.Exists(@".\target_file"))
			{
				File.Delete(@".\target_file");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestInvalidFeed()
		{
			// Create an RBOImportTask with an invalid feed name
			importTask = new RBOImportTask("dummy", "xxxxx", "", "", @"C:\Inetpub\wwwroot\RetailBusinessObjects\bin\");
			int result = importTask.Run("file");
			Assert.AreEqual((int)TDExceptionIdentifier.PRHUnrecognisedImportFeed, result,
				"Wrong error code returned when invalid feed name is provided");
			

		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestInvalidParameters()
		{
			// Create an RBOImportTask where the parameters for the primary server is invalid
			importTask = new RBOImportTask(RBO_UPDATE, "xxxxx", "", "", @"C:\Inetpub\wwwroot\RetailBusinessObjects\bin\");
			int result = importTask.Run("file");
			Assert.AreEqual((int)TDExceptionIdentifier.PRHInvalidImportParameters, result,
				"Wrong error code returned when primary server parameters are invalid");
			
			// Create an RBOImprotTask with valid primary server parameters, but invalid secondary server parameters
			importTask = new RBOImportTask(RBO_UPDATE, "111 222", "xxxx", "", @"C:\Inetpub\wwwroot\RetailBusinessObjects\bin");
			result = importTask.Run("file");
			Assert.AreEqual((int)TDExceptionIdentifier.PRHInvalidImportParameters, result,
				"Wrong error code returned when primary server parameters are invalid");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestInvalidFile()
		{
			// Create an RBOImportTask and attempt to import a file that doesn't exist
			importTask = new RBOImportTask(RBO_UPDATE, @".\target_file http:\\dummyurl", "", "", @"C:\Inetpub\wwwroot\RetailBusinessObjects\bin");
			//int result = importTask.Run(@".\td.userportal.retailbusiness.dll");
			int result = importTask.Run("no_such_file");
			Assert.AreEqual((int)TDExceptionIdentifier.PRHImportIOException, result,
				"Wrong error code returned when primary server parameters are invalid");

			// Create an RBOImportTask where the primary targetFile is in a non existent directory
			importTask = new RBOImportTask(RBO_UPDATE, @"C:\no_such_dir\target_file http:\\dummyurl", "", "", @"C:\Inetpub\wwwroot\RetailBusinessObjects\bin");
			result = importTask.Run(@".\td.userportal.retailbusiness.dll");
			//result = importTask.Run("no_such_file");

			Assert.AreEqual((int)TDExceptionIdentifier.PRHImportIOException, result,
				"Wrong error code returned when invalid file is passed in");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestServerUnavailable()
		{
			// Create an RBOImportTask with an invalid url
			importTask = new RBOImportTask(RBO_UPDATE, @".\target_file http:\\dummyurl", "", "", @"C:\Inetpub\wwwroot\RetailBusinessObjects\bin");
			int result = importTask.Run(@".\td.userportal.retailbusiness.dll");
			Assert.AreEqual((int)TDExceptionIdentifier.PRHServerUnavailable, result,
				"Wrong error code returned when primary server is unavailable");
		}

		/// <summary>
		/// 
		/// </summary>
		public void TestServerUnableToHousekeep()
		{

		}
	}
}