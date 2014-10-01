// ***********************************************
// NAME 		: TestFtpParameters.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 02/04/2004
// DESCRIPTION 	: Tests FtpParameters class.
// ************************************************

using System;
using NUnit.Framework;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description for TestFtpParameters.
	/// </summary>
	[TestFixture]
	public class TestFtpParameters
	{

		#region Constructor/Destructor
	
		~TestFtpParameters()
		{
			if (TestSetup.Initialised)
				TestSetup.TearDown();
		}

		#endregion

		#region Setup/teardown

		[SetUp]
		public void Init()
		{
			if (!TestSetup.Initialised)
				TestSetup.Setup();
		}

		#endregion
	
		#region Tests

		/// <summary>
		/// Tests that SetData successfully exits when reading a data item
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk. This test tries to load test data into the database. Although this works on a dev machine it fails on the build machine. Needs further investigation")]
		public void TestSetData()
		{
			string dataFeed;
			int clientFlag;
			FtpParameters ftpFileParameters = null;

			// Test 1 - reading a valid item 'test1'
			try
			{
				dataFeed = "test1";
				clientFlag = 0;
				ftpFileParameters = new FtpParameters(dataFeed, clientFlag);			
				bool isDataOk = ftpFileParameters.SetData();
				// Raise error if the read failed
				Assert.IsTrue(isDataOk, "Failed to exit SetData correctly when reading valid data item");
			}
			catch (Exception e)
			{
				Assert.IsTrue(false, String.Format("Exception {0} was thrown whilst executing first part of TestSetData", e.Message));
			}
			DateTime expected = new DateTime(1900, 1, 1, 0, 0, 0, 0);
			// Check that the data read is as expected
			Assert.AreEqual(0, ftpFileParameters.FtpClient,"FtpClient not as expected");
			Assert.AreEqual("test1", ftpFileParameters.DataFeed, "DataFeed not as expected");
			Assert.AreEqual("127.0.0.1", ftpFileParameters.IPAddress, "IPAddress not as expected");
			Assert.AreEqual("test1", ftpFileParameters.Username, "Username not as expected");
			Assert.AreEqual("test1_pwd", ftpFileParameters.Password, "Password not as expected");
			Assert.AreEqual("test", ftpFileParameters.LocalDir,"LocalDir not as expected");
			Assert.AreEqual("test_remote", ftpFileParameters.RemoteDir, "RemoteDir not as expected");
			Assert.AreEqual("*.*", ftpFileParameters.FilenameFilter, "FilenameFilter not as expected");
			Assert.AreEqual(0, ftpFileParameters.MissingFeedCounter, "MissingFeedCounter not as expected");
			Assert.AreEqual(0, ftpFileParameters.MissingFeedThreshold, "MissingFeedThreshold not as expected");
			Assert.AreEqual(expected, ftpFileParameters.DataFeedDatetime, "DataFeedDatetime not as expected");
			Assert.AreEqual("", ftpFileParameters.DataFeedFileName, "DataFeedFileName not as expected");
			Assert.AreEqual(true, ftpFileParameters.RemoveFiles,"RemoveFiles not as expected");

		}

		[Test]
		public void TestSetDataWithMissing()
		{
			string dataFeed;
			int clientFlag;
			bool isDataOk;
			FtpParameters ftpFileParameters;

			try
			{
				dataFeed = "NonExistant";
				clientFlag = 0;
				ftpFileParameters = new FtpParameters(dataFeed, clientFlag);
				isDataOk = ftpFileParameters.SetData();
				Assert.IsTrue(!isDataOk, "Failed to exit SetData correctly when trying to read missing data item");
			}
			catch (Exception e)
			{
				Assert.IsTrue(false, String.Format("Exception {0} was thrown whilst executing TestSetDataWithMissing", e.Message));
			}
		}

		#endregion
	}
}
