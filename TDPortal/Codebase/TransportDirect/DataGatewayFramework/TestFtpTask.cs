// ***********************************************
// NAME 		: TestFtpTask.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 02/04/2004
// DESCRIPTION 	: Tests FtpParameters class.
// ************************************************

using System;
using System.IO;
using System.Configuration;
using NUnit.Framework;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections;
using System.DirectoryServices;
using System.Diagnostics;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description for TestFtpTask.
	/// </summary>
	[TestFixture]
	public class TestFtpTask
	{
		private IPropertyProvider currProps;
		const string expectedPscpArgs = "-pw {0} \"{1}@{2}:{3}/*.zip\" {4}";
		const string expectedPsftpArgs = "{0}@{1} -pw {2} -b {3}~delFeeds.tmp";

		const string pscpArgsFile = @"C:\TestMockPscpOutput.txt";
		const string psftpArgsFile = @"C:\TestMockPsftpOutput.txt";

		#region Constructor/destructor

		public TestFtpTask()
		{
		}

		~TestFtpTask()
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
			currProps = Properties.Current;
		}

		[TearDown]
		public void ClearUp()
		{
			// If the two parameter output files were created, delete them
			try
			{
				if (File.Exists(pscpArgsFile))
					File.Delete(pscpArgsFile);
			}
			catch (Exception e)
			{
				Assert.IsTrue(false, "An exception occurred whilst trying to delete the Pscp arguments file (" + pscpArgsFile + "). Please delete this file manually if it exists. Exception: " + e.Message);
			}

			try
			{
				if (File.Exists(psftpArgsFile))
					File.Delete(psftpArgsFile);
			}
			catch (Exception e)
			{
				Assert.IsTrue(false,"An exception occurred whilst trying to delete the Psftp arguments file (" + psftpArgsFile + "). Please delete this file manually if it exists. Exception: " + e.Message);
			}
		}

		#endregion

		#region Tests

		/// <summary>
		/// Check that arguments for the ftp are correctly generated
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk")]
		public void ArgumentGeneration()
		{
			int dataRowCount = Convert.ToInt32(currProps["datagateway.test.ftpconfigdata.count"]);
			string[] dataItems;
			string dataFeedIn;
			int ftpClientIn, result;
			bool dataOk;
			for (int i = 1; i < dataRowCount; i++)
			{
				dataItems = currProps["datagateway.test.ftpconfigdata." + i.ToString()].Split("|".ToCharArray());
				dataFeedIn = dataItems[1];
				ftpClientIn = Convert.ToInt32(dataItems[0]);
				FtpParameters ftpParams = new FtpParameters(dataFeedIn, ftpClientIn);
				dataOk = ftpParams.SetData();

				Assert.IsTrue(dataOk, String.Format(dataFeedIn, ftpClientIn.ToString()),  "Call to FtpParameters.SetData() failed for {0} (ftpClientIn={1})");

				// Create the ftpTask
				FtpTask ftpTask = new FtpTask(
					ftpParams.DataFeed,
					ftpParams.IPAddress,
					ftpParams.Username,
					ftpParams.Password,
					ftpParams.LocalDir,
					ftpParams.RemoteDir,
					ftpParams.FilenameFilter,
					ftpParams.MissingFeedCounter,
					ftpParams.MissingFeedThreshold,
					ftpParams.DataFeedDatetime,
					ftpParams.DataFeedFileName,
					ftpParams.RemoveFiles);
			
				ftpTask.PscpExePath = "TestMockPscp.bat";
				ftpTask.PsftpExePath = "TestMockPsftp.bat";

				result = ftpTask.Run();
			
				Assert.AreEqual(0, result, String.Format("Call to FtpTask.Run() returned {0} while executing test data item {1}",  result.ToString(), i));

				ProcessResultsFile(pscpArgsFile, ftpParams, false);
				// Only expect an ftp file if DeleteFiles is true
				if (ftpParams.RemoveFiles)
                    ProcessResultsFile(psftpArgsFile, ftpParams, true);
			}
		}

		#endregion

		#region Result checking methods
		
		private void ProcessResultsFile(string fileName, FtpParameters ftpParams, bool isFtpParams)
		{
			if (File.Exists(fileName))
			{
				StreamReader sr = new StreamReader(fileName);
				StringBuilder sb = new StringBuilder();
				string lineCurr;
				if (sr.ReadLine() != null)
				{
					while ((lineCurr = sr.ReadLine()) != null)
						sb.Append(lineCurr);
				}
				sr.Close();

				string paramString = sb.ToString().Trim();

				string expected;
				if (isFtpParams)
					expected = String.Format(expectedPsftpArgs, ftpParams.Username, ftpParams.IPAddress, ftpParams.Password, TestSetup.IncomingPath);
				else
					expected = String.Format(expectedPscpArgs, ftpParams.Password, ftpParams.Username, ftpParams.IPAddress, ftpParams.RemoteDir, ftpParams.LocalDir);

				Assert.AreEqual(expected, paramString,(isFtpParams ? "PsFtp" : "Pscp") + " parameters were not as expected");
				File.Delete(fileName);
			}
			else
				Assert.Fail((isFtpParams ? "PsFtp" : "Pscp") + " output file not found.");
		}

		#endregion
	}
}
