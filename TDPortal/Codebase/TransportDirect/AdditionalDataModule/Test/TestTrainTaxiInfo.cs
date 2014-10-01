// ********************************************************************* 
// NAME                 : TestTDAdditionalData.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of TestTDAdditionalData
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestTrainTaxiInfo.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 16:17:46   mturner
//Initial revision.
//
//   Rev 1.2   Jun 21 2005 15:48:42   scraddock
//No change test of remote check out for NFH Support
//
//   Rev 1.1   Feb 07 2005 15:38:48   bflenk
//Changed Assertion to Assert
//
//   Rev 1.0   Nov 07 2003 14:05:50   RPhilpott
//Initial Revision
//
//   Rev 1.0   Oct 16 2003 20:52:32   acaunt
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Test harness for TrainTaxiInfo.
	///
	/// Uses TestStubAdditionalData to get data.  
	/// </summary>
	[TestFixture]
	public class TestTrainTaxiInfo
	{
		public TestTrainTaxiInfo()
		{
			TDServiceDiscovery.Init(new TestAdditionalDataInitialisation());
		}

		[SetUp]
		public void Init()
		{		
		}

		[TearDown]
		public void CleanUp()
		{
		}

		/// <summary>
		/// </summary>
		[Test]
		public void TestNoRowsReturned()
		{

			TrainTaxiInfo tti = new TrainTaxiInfo("RETURNSNULL");
			string[] results = tti.GetInfoLines();
			Assert.AreEqual(0, results.Length, "Unexpected number of strings returned");	
			tti = new TrainTaxiInfo("NOROWS");
			results = tti.GetInfoLines();
			Assert.AreEqual(0, results.Length, "Unexpected number of strings returned");
		
			tti = new TrainTaxiInfo("EXCEPTION");
			results = tti.GetInfoLines();
			Assert.AreEqual(0, results.Length, "Unexpected number of strings returned");
	
		}

		[Test]
		public void TestNormalThreeRows()
		{
			TrainTaxiInfo tti = new TrainTaxiInfo("9100CREWE");
			string[] results = tti.GetInfoLines();
			Assert.AreEqual(4, results.Length, "Unexpected number of strings returned");
	
			// add checks for contents ...
		}
		[Test]

		public void TestNormalTwoRows()
		{
			TrainTaxiInfo tti = new TrainTaxiInfo("9100MNCR");
			string[] results = tti.GetInfoLines();
			Assert.AreEqual(3, results.Length, "Unexpected number of strings returned");
	
			// add checks for contents ...
		}

		[Test]
		public void TestSimpleGoto()
		{
			TrainTaxiInfo tti = new TrainTaxiInfo("9100SHRY");
			string[] results = tti.GetInfoLines();
			Assert.AreEqual(6, results.Length, "Unexpected number of strings returned");
	
			// add checks for contents ...
		}

		[Test]
		public void TestMultipleGoto()
		{
			TrainTaxiInfo tti = new TrainTaxiInfo("9100FRNGDN");
			string[] results = tti.GetInfoLines();
			Assert.AreEqual(10, results.Length, "Unexpected number of strings returned");
	
			// add checks for contents ...
		}

		[Test]
		public void TestRecursiveGoto()
		{
			TrainTaxiInfo tti = new TrainTaxiInfo("9100NTWCH");
			string[] results = tti.GetInfoLines();
			Assert.AreEqual(6, results.Length, "Unexpected number of strings returned");
	
			// add checks for contents ...
		}
	}
}



