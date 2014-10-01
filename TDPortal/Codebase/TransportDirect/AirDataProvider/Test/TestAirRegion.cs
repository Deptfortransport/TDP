// *********************************************** 
// NAME			: TestAirRegion.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 04/06/04
// DESCRIPTION	: Class testing the funcationality of AirRegion
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirRegion.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:44   mturner
//Initial revision.
//
//   Rev 1.2   Feb 08 2005 11:54:50   bflenk
//Changed Assertion to Assert
//
//   Rev 1.1   Jun 14 2004 10:33:14   jgeorge
//Added unit tests
//
//   Rev 1.0   Jun 09 2004 17:05:48   jgeorge
//Initial revision.

using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Class testing the funcationality of AirRegion
	/// </summary>
	[TestFixture]
	public class TestAirRegion
	{
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() { }

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() { }

		/// <summary>
		/// Creates an AirRegion and checks that the values supplied to the
		/// constructor are returned by the properties.
		/// </summary>
		[Test]
		public void TestCreate() 
		{
			int expectedCode = 1;
			string expectedName = "London";
			AirRegion testRegion = null;

			try
			{
				testRegion = new AirRegion(expectedCode, expectedName);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the AirRegion. Exception message follows. " + e.Message);
			}

			Assert.AreEqual(expectedCode, testRegion.Code, "Code property differs from expected");
			Assert.AreEqual(expectedName, testRegion.Name, "Name property differs from expected");
		}

	}
}
