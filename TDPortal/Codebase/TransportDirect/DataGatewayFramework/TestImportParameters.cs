// ***********************************************
// NAME 		: TestImportParameters.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 02/04/2004
// DESCRIPTION 	: Tests TestImportParameters class.
// ************************************************

using System;
using NUnit.Framework;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Summary description for TestFtpParameters.
	/// </summary>
	[TestFixture]
	public class TestImportParameters
	{

		#region Constructor/Destructor
	
		~TestImportParameters()
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
		[Ignore("ProjectNewkirk")]
		public void TestSetData()
		{
			string dataFeed;
			ImportParameters importParameters = null;

			// Test 1 - reading a valid item 'test1'
			try
			{
				dataFeed = "test";
				importParameters = new ImportParameters(dataFeed);			
				bool isDataOk = importParameters.SetData();
				// Raise error if the read failed
				Assert.IsTrue(isDataOk, "Failed to exit SetData correctly when reading valid data item");
			}
			catch (Exception e)
			{
				Assert.IsTrue(false, String.Format("Exception {0} was thrown whilst executing first part of TestSetData", e.Message));
			}
			// Check that the data read is as expected
			Assert.AreEqual("test", importParameters.DataFeed, "DataFeed not as expected");
			Assert.AreEqual("IMPORTERTEST", importParameters.ImportClass, "ImportClass not as expected");
			Assert.AreEqual("IMPORTTEST", importParameters.ClassArchive, "ClassArchive not as expected");
			Assert.AreEqual("TEST.bat", importParameters.ImportUtility,"ImportUtility not as expected");
			Assert.AreEqual("bcd", importParameters.Parameters1, "Parameters1 not as expected");
			Assert.AreEqual("wxy", importParameters.Parameters2, "Parameters2 not as expected");
			Assert.AreEqual(@"C:\Temp", importParameters.ProcessingDir, "ProcessingDir not as expected");
		}

		[Test]
		public void TestSetDataWithMissing()
		{
			string dataFeed;
			bool isDataOk;
			ImportParameters importParameters;

			try
			{
				dataFeed = "NonExistant";
				importParameters = new ImportParameters(dataFeed);
				isDataOk = importParameters.SetData();
				Assert.IsTrue(!isDataOk, "Failed to exit SetData correctly when trying to read missing data item");
			}
			catch (Exception e)
			{
				Assert.IsTrue(false,String.Format("Exception {0} was thrown whilst executing TestSetDataWithMissing", e.Message));
			}
		}

		#endregion
	}
}
