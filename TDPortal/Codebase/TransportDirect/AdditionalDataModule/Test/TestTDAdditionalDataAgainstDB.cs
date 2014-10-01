// ********************************************************************* 
// NAME                 : TestTDAdditionalDataAgainstDB.cs 
// AUTHOR               : Richard Scott
// DATE CREATED         : 12/01/2005
// DESCRIPTION			: Implementation of TestTDAdditionalDataAgainstDB
// ********************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestTDAdditionalDataAgainstDB.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:17:44   mturner
//Initial revision.
//
//   Rev 1.4   Mar 31 2005 09:24:22   rscott
//Changes from code review IR1935
//
//   Rev 1.3   Mar 31 2005 08:42:14   rscott
//Header info added
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AdditionalDataModule.Test
{
	/// <summary>
	/// Test harness for TDAddtionialDataAgainstDB
	/// </summary>
	[TestFixture]
	public class TestTDAdditionalDataAgainstDB
	{
		private IAdditionalData additionalData;
		public TestTDAdditionalDataAgainstDB()
		{
		}

		[SetUp]
		public void Init()
		{		
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestAdditionalDataInitialisationAgainstDB());
			additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];			   
		}

		[TearDown]
		public void CleanUp()
		{
			additionalData = null;
		}

		#region "Tests for LookupFromNaPTAN"

		/// <summary>
		/// Test that PricingUnits and attributes are correctly set on creation for a matching return journey
		/// The journey should only have one PricingUnit in each direction
		/// </summary>
		[Test]
		public void TestLookupIndividualFieldFromNaPTAN()
		{
			// Call the service and check the returned code
			string value = additionalData.LookupFromNaPTAN(LookupType.CRS_Code, "9100BRBCNLT");
			Assert.AreEqual("ZBB", value, "Incorrect CRS Code returned");
		}

		/// <summary>
		/// Test that PricingUnits and attributes are correctly set on creation for a matching return journey
		/// The journey should only have one PricingUnit in each direction
		/// </summary>
		[Test]
		public void TestLookupAllFieldsFromNaPTAN()
		{
			// Call the service and confirm that we have the expected number of results
			LookupResult[] results = additionalData.LookupFromNaPTAN("9100BRBCNLT");
			Assert.AreEqual(2, results.Length, "Unexpected number of values returned");
	
			// Confirm that the service has returned one of each type of field
			Assert.AreEqual(LookupType.NLC_Code, results[0].Type, "NLC Code not found in position [0]");
			Assert.AreEqual(LookupType.CRS_Code, results[1].Type, "CRS Code not found in position [1]");
		}

		#endregion

		#region "Tests for LookupNaptanForCode"

		/// <summary>
		/// Test that Naptan results are returned as a string array
		/// The results should return one or more naptan values
		/// </summary>
		[Test]
		public void TestLookupNaptanByCRS()
		{
			// Call the service using a CRS code - should return 4 values
			string[] results = additionalData.LookupNaptanForCode("CLJ", LookupType.CRS_Code);
			Assert.AreEqual(4, results.Length, "Error looking up Naptan from CRS Code");	
		}

		/// <summary>
		/// Test that Naptan results are returned as a string array
		/// The results should return one or more naptan values
		/// </summary>
		[Test]
		public void TestLookupNaptanByNLC()
		{
			// Call the service using an NLC code - should return
			string[] results = additionalData.LookupNaptanForCode("0501", LookupType.NLC_Code);
			Assert.AreEqual(1, results.Length, "Error looking up Naptan from NLC Code");
		}

		/// <summary>
		/// Test that Naptan results are returned as a string array
		/// The results should return one or more naptan values
		/// </summary>
		[Test]
		public void TestLookupNaptanByNLCNoResults()
		{
			// Call the service using an NLC code - should return
			string[] results = additionalData.LookupNaptanForCode("0500", LookupType.NLC_Code);
			Assert.AreEqual(0, results.Length, "Error looking up Naptan from NLC Code length of results not 0 should return no results for NLC 0500");
		}

		#endregion

		/// <summary>
		/// Test that Naptan results are returned as a string array
		/// The results should return one or more naptan values
		/// </summary>
		[Test]
		public void TestLookupStationByCRS()
		{
			// Call the service using a CRS code - should return "CLAPHAM JUNCTION Station"
			string test1 = additionalData.LookupStationNameForCRS("CLJ");
			Assert.AreEqual("CLAPHAM JUNCTION Station", test1, "Wrong station returned");	

			// Call the service using a CRS code - should return "ABERDARE Station"
			string test2 = additionalData.LookupStationNameForCRS("ABA");
			Assert.AreEqual("ABERDARE Station", test2, "Wrong station returned");

			// Call the service using a CRS code - should return empty string
			string test3 = additionalData.LookupStationNameForCRS("BBC");
			Assert.AreEqual(string.Empty, test3, "A value other than empty string was returned");

		}
	}
}
