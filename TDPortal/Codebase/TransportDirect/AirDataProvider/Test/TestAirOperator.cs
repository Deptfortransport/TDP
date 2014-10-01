// *********************************************** 
// NAME			: TestAirOperator.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 04/06/04
// DESCRIPTION	: Class testing the funcationality of AirOperator
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirOperator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:44   mturner
//Initial revision.
//
//   Rev 1.2   Feb 08 2005 10:34:50   bflenk
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
	/// Class testing the funcationality of AirOperator
	/// </summary>
	[TestFixture]
	public class TestAirOperator
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
		/// Creates an AirOperator and checks that the values supplied to the
		/// constructor are returned by the properties.
		/// </summary>
		[Test]
		public void TestCreate() 
		{
			string expectedIata = "AA";
			string expectedName = "Air Airways";
			AirOperator testOperator = null;

			try
			{
				testOperator = new AirOperator(expectedIata, expectedName);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the AirOperator. Exception message follows. " + e.Message);
			}

			Assert.AreEqual(expectedIata, testOperator.IATACode, "Iata property differs from expected");
			Assert.AreEqual(expectedName, testOperator.Name, "Name property differs from expected");
 		}

	}
}
