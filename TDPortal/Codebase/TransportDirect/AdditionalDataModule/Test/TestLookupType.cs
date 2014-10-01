// ********************************************************************* 
// NAME                 : TestLookupType.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of TestLookupType
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestLookupType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 16:17:42   mturner
//Initial revision.
//
//   Rev 1.2   Feb 07 2005 15:23:12   bflenk
//Change Assertion to Assert
//
//   Rev 1.1   Nov 07 2003 16:29:32   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.0   Oct 16 2003 20:52:30   acaunt
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Test harness for LookupType
	/// </summary>
	[TestFixture]
	public class TestLookupType
	{

		public TestLookupType()
		{
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
		/// Test that the expected types are retrievd based on their names
		/// </summary>
		[Test]
		public void TestLookup()
		{
			Assert.AreEqual(LookupType.CRS_Code, LookupType.FindLookupType("CRS Code"), "CRS Code type not correctly mapped");
			Assert.AreEqual(LookupType.NLC_Code, LookupType.FindLookupType("NLC Code"), "NLC Code type not correctly mapped");
		}

	}
}