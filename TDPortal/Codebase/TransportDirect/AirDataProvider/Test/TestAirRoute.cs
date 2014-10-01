// *********************************************** 
// NAME			: TestAirRoute.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 04/06/04
// DESCRIPTION	: Class testing the funcationality of AirRoute
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:44   mturner
//Initial revision.
//
//   Rev 1.2   Feb 08 2005 11:49:56   bflenk
//Changed Assertion to Assert
//
//   Rev 1.1   Jun 14 2004 10:33:14   jgeorge
//Added unit tests
//
//   Rev 1.0   Jun 09 2004 17:05:46   jgeorge
//Initial revision.

using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Class testing the funcationality of AirRoute
	/// </summary>
	[TestFixture]
	public class TestAirRoute
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
			string expectedOrigin = "ABZ";
			string expectedDestination = "LGW";
			string expectedCompoundCode = "ABZLGW";
			AirRoute testRoute = null;

			try
			{
				testRoute = new AirRoute(expectedOrigin, expectedDestination);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the AirRoute. Exception message follows. " + e.Message);
			}

			Assert.AreEqual(expectedOrigin, testRoute.OriginAirport, "OriginAirport property differs from expected");
			Assert.AreEqual(expectedDestination, testRoute.DestinationAirport, "DestinationAirport property differs from expected");
			Assert.AreEqual(expectedCompoundCode, testRoute.CompoundName, "CompoundName property differs from expected");
		}

		/// <summary>
		/// Checks that routes which are the same are equal when compared using the Equals method
		/// </summary>
		[Test]
		public void TestEquals() 
		{
			string airport1 = "ABZ";
			string airport2 = "LGW";
			string airport3 = "LHR";

			AirRoute routeSame1 = new AirRoute(airport1, airport2);
			AirRoute routeSame2 = new AirRoute(airport1, airport2);
			AirRoute routeSame3 = new AirRoute(airport2, airport1);
			AirRoute routeDifferent1 = new AirRoute(airport1, airport3);
			AirRoute routeDifferent2 = new AirRoute(airport3, airport2);

			// Note:
			// routeSame1 and routeSame2 are identical
			// routeSame1 and routeSame3 are equal but have their origin/destination reversed
			// routeDifferent1 has the same origin as routeSame1 but a different destination
			// routeDifferent2 has the same destination as routeSame1 but a different origin

			Assert.IsTrue(routeSame1.Equals(routeSame1),"Failed reflexive test");
			Assert.IsTrue(routeSame1.Equals(routeSame2) && routeSame2.Equals(routeSame1), "Failed symmetric test");
			Assert.IsTrue(routeSame1.Equals(null) == false, "Failed when comparing to null");
			Assert.IsTrue(!routeSame1.Equals(routeDifferent1), "Routes with matching origin and different destination were equal.");
			Assert.IsTrue(!routeSame1.Equals(routeDifferent2),"Routes with different origin and matching destination were equal.");
			Assert.IsTrue(!routeSame3.Equals(routeDifferent2), "Different routes were equal.");

		}

		/// <summary>
		/// Empty test to delete.
		/// </summary>
		[Test]
		public void TestGetHashCode()
		{
			string airport1 = "ABZ";
			string airport2 = "LGW";
			string airport3 = "LHR";

			AirRoute routeSame1 = new AirRoute(airport1, airport2);
			AirRoute routeSame2 = new AirRoute(airport1, airport2);
			AirRoute routeSame3 = new AirRoute(airport2, airport1);
			AirRoute routeDifferent1 = new AirRoute(airport1, airport3);
			AirRoute routeDifferent2 = new AirRoute(airport3, airport2);

			// Note:
			// routeSame1 and routeSame2 are identical
			// routeSame1 and routeSame3 are equal but have their origin/destination reversed
			// routeDifferent1 has the same origin as routeSame1 but a different destination
			// routeDifferent2 has the same destination as routeSame1 but a different origin

			Assert.AreEqual(routeSame1.GetHashCode(), routeSame1.GetHashCode(), "Failed consistency test");
			Assert.AreEqual(routeSame1.GetHashCode(), routeSame2.GetHashCode(), "Same routes returned different hashcodes (using routes with matching origin and destination)");
			Assert.AreEqual(routeSame1.GetHashCode(), routeSame3.GetHashCode(), "Same routes returned different hashcodes (using routes with reversed origin and destination)");

			// Don't need to test that different routes have different hashcodes as this is not in the spec
			// of GetHashCode()
		}


	}
}
