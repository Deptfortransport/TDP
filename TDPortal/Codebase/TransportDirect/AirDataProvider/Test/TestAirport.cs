// *********************************************** 
// NAME			: TestAirport.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 04/06/04
// DESCRIPTION	: Class testing the funcationality of Airport
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:44   mturner
//Initial revision.
//
//   Rev 1.4   Feb 08 2005 11:40:24   bflenk
//Another Assertion to Assert change
//
//   Rev 1.3   Feb 08 2005 11:38:20   bflenk
//Changed Assertion to Assert
//
//   Rev 1.2   Aug 11 2004 11:55:40   CHosegood
//Naptain now ends with 1 even if only 1 terminal exists.
//
//   Rev 1.1   Jun 14 2004 10:33:14   jgeorge
//Added unit tests
//
//   Rev 1.0   Jun 09 2004 17:05:48   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using NUnit.Framework;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Class testing the funcationality of Airport
	/// </summary>
	[TestFixture]
	public class TestAirport
	{
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new AirportTestInitialisation());
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() { }

		/// <summary>
		/// Creates an Airport and checks that the values supplied to the
		/// constructor are returned by the properties.
		/// </summary>
		[Test]
		public void TestCreate() 
		{ 
			string expectedIata = "LGW";
			string expectedName = "London Gatwick";
			int expectedTerminals = 3;
			Airport testAirport = null;

			try
			{
				testAirport = new Airport(expectedIata, expectedName, expectedTerminals);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the Airport. Exception message follows. " + e.Message);
			}

			Assert.AreEqual(expectedIata, testAirport.IATACode, "Iata property differs from expected");
			Assert.AreEqual(expectedName, testAirport.Name, "Name property differs from expected");
			Assert.AreEqual(expectedTerminals, testAirport.NoOfTerminals, "NoOfTerminals property differs from expected");
		}

		/// <summary>
		/// Verifies that the array of naptans is correctly generated for an airport with a single terminal
		/// </summary>
		[Test]
		public void TestSingleTerminalNaptan() 
		{ 
			// Create an airport with a single terminal
			string expectedIata = "LGW";
			string expectedName = "London Gatwick";
			int expectedTerminals = 1;
			string expectedNaptan = "9200" + expectedIata + expectedTerminals;
			Airport testAirport = null;

			try
			{
				testAirport = new Airport(expectedIata, expectedName, expectedTerminals);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the Airport. Exception message follows. " + e.Message);
			}

			// Check that the number of naptans is as expected
			Assert.AreEqual(expectedTerminals, testAirport.Naptans.Length, "Naptans.Length property differs from expected");
			Assert.AreEqual(expectedNaptan, testAirport.Naptans[0], "Naptans[0] property differs from expected");
		}

		/// <summary>
		/// Verifies that the array of naptans is correctly generated for an airport with multiple terminals
		/// </summary>
		[Test]
		public void TestMultipleTerminalNaptan() 
		{ 
			// Create an airport with a multiple terminals
			IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			string expectedIata = "LGW";
			string expectedName = "London Gatwick";
			int expectedTerminals = 4;
			string expectedNaptanPrefix = properties["FindA.NaptanPrefix.Airport"] + expectedIata;
			string[] expectedNaptans = new string[] { expectedNaptanPrefix + "1", expectedNaptanPrefix + "2", expectedNaptanPrefix + "3", expectedNaptanPrefix + "4" };
			Airport testAirport = null;

			try
			{
				testAirport = new Airport(expectedIata, expectedName, expectedTerminals);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the Airport. Exception message follows. " + e.Message);
			}

			// Check that the number of naptans is as expected
			Assert.AreEqual(expectedTerminals, testAirport.Naptans.Length, "Naptans.Length property differs from expected");
			Assert.AreEqual(expectedNaptans[0], testAirport.Naptans[0], "Naptans[0] property differs from expected");
			Assert.AreEqual(expectedNaptans[1], testAirport.Naptans[1], "Naptans[1] property differs from expected");
			Assert.AreEqual(expectedNaptans[2], testAirport.Naptans[2], "Naptans[2] property differs from expected");
			Assert.AreEqual(expectedNaptans[3], testAirport.Naptans[3], "Naptans[3] property differs from expected");
		}

		/// <summary>
		/// Tests the equals method
		/// </summary>
		[Test]
		public void TestEquals()
		{
			Airport airport1 = new Airport("AAA", "Airport 1", 3);
			Airport airport2 = new Airport("AAA", "Airport 1", 3);
			Airport airport3 = new Airport("BBB", "Airport 1", 3);
			Airport airport4 = new Airport("AAA", "Airport 2", 3);
			Airport airport5 = new Airport("AAA", "Airport 1", 5);

			// The first two are equal to each other
			// The rest each differ in one field
			Assert.IsTrue(airport1.Equals(airport1), "Airport.Equals failed reflexivity test");
			Assert.IsTrue(airport1.Equals(airport2) == airport2.Equals(airport1),"Airport.Equals failed symmetry test for equal objects");
			Assert.IsTrue(airport1.Equals(airport3) == airport3.Equals(airport1), "Airport.Equals failed symmetry test for different objects");
			Assert.IsTrue(airport1.Equals(airport2), "Airport.Equals failed when comparing two equal airports");
			Assert.IsTrue(airport1.Equals(null) == false, "Airport.Equals failed when comparing to null");
			Assert.IsTrue(airport1.Equals(airport3) == false, "Airport.Equals failed when comparing two airports with different IATA codes");
			Assert.IsTrue(airport1.Equals(airport4) == false, "Airport.Equals failed when comparing two airports with different names");
			Assert.IsTrue(airport1.Equals(airport5) == false, "Airport.Equals failed when comparing two airports with different number of terminals");
		}

	}

	/// <summary>
	/// Initialisation class for airport test
	/// </summary>
	public class AirportTestInitialisation : IServiceInitialisation
	{
		// Need to add a file property provider
		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
		}

	}

}
