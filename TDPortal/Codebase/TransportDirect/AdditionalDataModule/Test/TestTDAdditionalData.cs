// ********************************************************************* 
// NAME                 : TestTDAdditionalData.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of TestTDAdditionalData
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestTDAdditionalData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 16:17:44   mturner
//Initial revision.
//
//   Rev 1.8   Mar 31 2005 09:24:18   rscott
//Changes from code review IR1935
//
//   Rev 1.7   Feb 07 2005 15:22:24   bflenk
//Changed Assetion to Assert
//
//   Rev 1.6   Jan 12 2005 16:57:58   RScott
//updated to accomodate new method LookupStationNameForCRS()
//
//   Rev 1.5   Jan 11 2005 15:15:54   RScott
//Update NUnit tests
//
//   Rev 1.4   May 28 2004 16:09:10   asinclair
//and now changed it back to 5 again
//
//   Rev 1.3   May 26 2004 16:22:48   asinclair
//Changed the number of values returned in TestLookupAllFieldsFromNaPTAN from 5 to 2 to ensure test passes.
//
//   Rev 1.2   Nov 21 2003 16:55:00   acaunt
//TDAdditionalData now returns string.empty is an invalid NaPTAN (null or empty) is provided
//
//   Rev 1.1   Nov 07 2003 16:29:30   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.0   Oct 16 2003 20:52:32   acaunt
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Test harness for TDAddtionialData - against Stub
	/// </summary>
	[TestFixture]
	public class TestTDAdditionalData
	{
		private IAdditionalData additionalData;
		public TestTDAdditionalData()
		{
			
		}

		[SetUp]
		public void Init()
		{	
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestAdditionalDataInitialisation());
			additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];			   
		}

		[TearDown]
		public void CleanUp()
		{
			additionalData = null;
		}
		/// <summary>
		/// Tests the outcome of a null value being returned
		/// </summary>
		[Test]
		public void TestNullValue()
		{
			string value = additionalData.LookupFromNaPTAN(LookupType.CRS_Code, null);
			Assert.AreEqual("", value, "Empty string not returned when null is passed to module");

			value = additionalData.LookupFromNaPTAN(LookupType.CRS_Code, string.Empty);
			Assert.AreEqual("", value, "Empty string not returned when empty string is passed to module");
		
		}
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
		/// Tests looking up both NLC and CRS codes from a naptan
		/// </summary>
		[Test]
		public void TestLookupAllFieldsFromNaPTAN()
		{
			// Call the service and confirm that we have the expected number of results
			LookupResult[] results = additionalData.LookupFromNaPTAN("9100BRBCNLT");
			Assert.AreEqual(5, results.Length, "Unexpected number of values returned");
	
			// Confirm that the service has returned one of each type of field
			Assert.AreEqual(LookupType.NLC_Code, results[0].Type, "NLC Code not found in position [0]");
			Assert.AreEqual(LookupType.CRS_Code, results[1].Type, "CRS Code not found in position [1]");
		}

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

#region "Tests to return NAPTANS from CRS or NLC Codes"

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
			Assert.AreEqual("9100BRBCNLT", results[0], "Error looking up Naptan from NLC Code");
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
	}
}
