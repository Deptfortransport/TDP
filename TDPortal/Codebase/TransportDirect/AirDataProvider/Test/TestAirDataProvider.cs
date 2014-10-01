// *********************************************** 
// NAME			: TestAirDataProvider.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 04/06/04
// DESCRIPTION	: Class testing the funcationality of AirDataProvider
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestAirDataProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:42   mturner
//Initial revision.
//
//   Rev 1.6   Feb 14 2005 09:38:04   RScott
//warning "sortingStyleSheet declarded but never used resolved"
//
//   Rev 1.5   Feb 08 2005 10:25:36   bflenk
//Changed Assertion to Assert
//
//   Rev 1.4   Jul 08 2004 13:18:14   jgeorge
//Updated with FxCop recommendations
//
//   Rev 1.3   Jul 08 2004 12:58:34   jgeorge
//Updated after stored procedure changes
//
//   Rev 1.2   Jun 17 2004 10:24:42   jgeorge
//Added test for reloading data
//
//   Rev 1.1   Jun 16 2004 17:22:48   jgeorge
//All tests except Change Notification added
//
//   Rev 1.0   Jun 09 2004 17:05:48   jgeorge
//Initial revision.

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.ScriptRepository;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using NUnit.Framework;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Class testing the funcationality of AirDataProvider
	/// </summary>
	[TestFixture]
	public class TestAirDataProvider
	{
		private static readonly string dataFile1 = @"AirDataProvider\testData1.xml";
		private static readonly string dataFile2 = @"AirDataProvider\testData2.xml";
		
		//never used
		//private static readonly string sortingStylesheet = @"AirDataProvider\testDataSortingTemplate.xslt";
		
		private static readonly string scriptsFolderPath = Directory.GetCurrentDirectory() + @"\AirDataProvider\TestScriptRepository";
		private static readonly string scriptsFilePath = scriptsFolderPath + @"\scripts.xml";
		private static readonly string tempScriptsFolderPath = scriptsFolderPath + @"\tempscripts";

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{ 
			// Clear down temp scripts folder
			DirectoryInfo scriptsFolder = new DirectoryInfo(tempScriptsFolderPath);
			if (scriptsFolder.Exists)
				scriptsFolder.Delete(true);

			TDServiceDiscovery.Init(new AirDataProviderTestInitialisation());
			RemoveExtraServices();
			AirDataTestHelper.BackupCurrentData();
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{ 
			AirDataTestHelper.RestoreOriginalData();
		}

		#region Helper methods

		/// <summary>
		/// Helper method. Adds the ScriptRepository service to the TDServiceDiscovery cache.
		/// </summary>
		/// <returns>The newly created instance of ScriptRepository</returns>
		private ScriptRepository.ScriptRepository AddScriptRepository()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.ScriptRepository, new ScriptRepository.ScriptRepositoryFactory(scriptsFolderPath, scriptsFilePath));
			return (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
		}

		/// <summary>
		/// Removes the script repository, air data provider and data change notification services 
		/// from the Service Discovery by replacing their factories with the TestMockServiceRemovalFactory
		/// </summary>
		private void RemoveExtraServices()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.ScriptRepository, new TestMockServiceRemovalFactory(ServiceDiscoveryKey.ScriptRepository));
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.AirDataProvider, new TestMockServiceRemovalFactory(ServiceDiscoveryKey.AirDataProvider));
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockServiceRemovalFactory(ServiceDiscoveryKey.DataChangeNotification));
		}

		/// <summary>
		/// Helper method. Adds the AirDataProvider service to the TDServiceDiscovery cache.
		/// </summary>
		/// <returns>The newly created instance of AirDataProvider</returns>
		private IAirDataProvider AddAirDataProvider()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());
			return (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
		}

		/// <summary>
		/// Adds the mock DataChangeNotification service to the cache
		/// </summary>
		/// <returns></returns>
		private TestMockDataChangeNotification AddDataChangeNotification()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			return (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
		}

		/// <summary>
		/// Removes the 9200 code from the front of the airport code as stored in the XML file
		/// </summary>
		/// <param name="airportCodeFromFile"></param>
		/// <returns></returns>
		private string ProcessAirportCode(string airportCodeFromFile)
		{
			return airportCodeFromFile.Substring(4, 3);
		}

		#endregion

		#region TestInitialisationWithoutScriptRepository

		/// <summary>
		/// Test to ensure that when the AirDataProvider is initialised without the ScriptRepository
		/// present in the service cache, the temporary script file is not created, and the 
		/// AirDataProvider reports this back.
		/// </summary>
		[Test]
		public void TestInitialisationWithoutScriptRepository()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);

			IAirDataProvider airData = AddAirDataProvider();

			Assert.IsTrue(!airData.ScriptGenerated, "AirDataProvider shows temporary script was created when ScriptRepository not present.");

			// The adp thinks that the script was generated - verify that it's present.
			// We know it was empty when the script repository was initialised.
			DirectoryInfo scriptsFolder = new DirectoryInfo(tempScriptsFolderPath);
			if (scriptsFolder.Exists)
				Assert.AreEqual(0, scriptsFolder.GetFiles("*.js").Length, "An unexpected number of scripts was found in the temporary script repository folder.");
		}

		#endregion

		#region TestInitialisationWithScriptRepository

		/// <summary>
		/// Test to ensure that when the AirDataProvider is initialised with the ScriptRepository
		/// present in the TDServiceDiscovery cache, the temporary script file is created.
		/// </summary>
		[Test]
		public void TestInitialisationWithScriptRepository()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);

			AddScriptRepository();
			IAirDataProvider airData = AddAirDataProvider();

			Assert.IsTrue(airData.ScriptGenerated, "Temporary script was not created successfully.");

			// The adp thinks that the script was generated - verify that it's present.
			// We know it was empty when the script repository was initialised.
			DirectoryInfo scriptsFolder = new DirectoryInfo(tempScriptsFolderPath);
			if (scriptsFolder.Exists)
				Assert.AreEqual(1, scriptsFolder.GetFiles("*.js").Length, "An unexpected number of scripts was found in the temporary script repository folder.");
			else
				Assert.Fail("Temporary scripts folder was not created.");
		}

		#endregion

		#region TestGetAirportMethods

		/// <summary>
		/// Tests the following methods:
		///		GetAirport
		///		GetAirports
		///		GetAirportsFromNaptans
		///		GetAirportFromNaptan
		/// </summary>
		[Test]
		public void TestGetAirportMethods()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);
			
			// Check that GetAirport returns null when an invalid airport code is passed
			Assert.IsNull(airData.GetAirport(string.Empty), "GetAirport did not return null when called with an empty string");
			Assert.IsNull(airData.GetAirport("NOTANIATA"), "GetAirport did not return null when called with an invalid airport code");

			// Check that all airports can be retrieved and are as expected
			ArrayList expectedAirports = new ArrayList();
			foreach (XmlNode currNode in data.GetElementsByTagName("flightroute"))
			{
				string currIataOrigin = ProcessAirportCode(currNode.Attributes["origincode"].Value);
				string currIataDestination = ProcessAirportCode(currNode.Attributes["destinationcode"].Value);
				XmlNode currAirportNodeOrigin = data.SelectSingleNode("//airport[@code='" + currNode.Attributes["origincode"].Value + "']");
				XmlNode currAirportNodeDestination = data.SelectSingleNode("//airport[@code='" + currNode.Attributes["destinationcode"].Value + "']");
				Airport currAirportOrigin = airData.GetAirport(currIataOrigin);
				Airport currAirportDestination = airData.GetAirport(currIataDestination);
				Assert.IsNotNull(currAirportOrigin, "Calling GetAirport with airportCode=" + currIataOrigin + " returned null.");
				Assert.IsNotNull(currAirportDestination, "Calling GetAirport with airportCode=" + currIataDestination + " returned null.");
				if (currAirportOrigin != null)
				{
					Assert.AreEqual(currIataOrigin, currAirportOrigin.IATACode, "Retrieved airport Iata code not as expected.");
					Assert.AreEqual(currAirportNodeOrigin.Attributes["name"].Value, currAirportOrigin.Name, "Retrieved airport name not as expected.");
					Assert.AreEqual(Convert.ToInt32(currAirportNodeOrigin.Attributes["terminals"].Value), currAirportOrigin.NoOfTerminals, "Retrieved airport no of terminals not as expected.");
					if (!expectedAirports.Contains(currAirportOrigin))
						expectedAirports.Add(currAirportOrigin);
				}
				if (currAirportDestination != null)
				{
					Assert.AreEqual(currIataDestination, currAirportDestination.IATACode, "Retrieved airport Iata code not as expected.");
					Assert.AreEqual(currAirportNodeDestination.Attributes["name"].Value, currAirportDestination.Name, "Retrieved airport name not as expected.");
					Assert.AreEqual(Convert.ToInt32(currAirportNodeDestination.Attributes["terminals"].Value), currAirportDestination.NoOfTerminals, "Retrieved airport no of terminals not as expected.");
					if (!expectedAirports.Contains(currAirportDestination))
						expectedAirports.Add(currAirportDestination);
				}
			}

			// Now check that the arraylist we have just built matches that returned by GetAirports, ie
			// there are no missing or unexpected items
			ArrayList returnedAirports = airData.GetAirports();
			Assert.AreEqual(expectedAirports.Count, returnedAirports.Count, "GetAirports returned an unexpected number of airports");
			foreach (Airport currAirport in expectedAirports)
				Assert.IsTrue(returnedAirports.Contains(currAirport), "Airport " + currAirport.IATACode + " missing from GetAirports return data");

			// test GetAirportFromNaptan returns null when an invalid naptan is passed
			Assert.IsNull(airData.GetAirportFromNaptan(string.Empty), "GetAirportFromNaptan did not return null when called with an empty string");
			Assert.IsNull(airData.GetAirportFromNaptan("XCSD"), "GetAirportFromNaptan did not return null when called with a naptan of 'XCSD'");
			Assert.IsNull(airData.GetAirportFromNaptan("9200XXX4"), "GetAirportFromNaptan did not return null when called with a naptan of '9200XXX4'");
			Assert.IsNull(airData.GetAirportFromNaptan("9200XXXD7737DSYHHDF"), "GetAirportFromNaptan did not return null when called with a naptan of '9200XXXD7737DSYHHDF'");

			// Now test GetAirportFromNaptan for every naptan in every expected airport
			foreach (Airport currAirport in expectedAirports)
				foreach (string s in currAirport.Naptans)
					Assert.AreEqual(currAirport, airData.GetAirportFromNaptan(s), "GetAirportFromNaptan returned an incorrect airport");
			
			// Test GetAirportsFromNaptans when an empty array is passed
			ArrayList expectedEmpty = airData.GetAirportsFromNaptans(new string[0]);
			Assert.IsNotNull(expectedEmpty, "GetAirportsFromNaptans returned null when called with no naptans");
			Assert.AreEqual(0, expectedEmpty.Count, "GetAirportsFromNaptans did not return an empty ArrayList when called with an empty string array");

			// Finally test GetAirportsFromNaptans with all the naptans in all the expected airports
			ArrayList allNaptans = new ArrayList();
			foreach (Airport currAirport in expectedAirports)
				allNaptans.AddRange(currAirport.Naptans);
			returnedAirports = airData.GetAirportsFromNaptans( (string[])allNaptans.ToArray(typeof(string)) );
			Assert.IsNotNull(returnedAirports, "GetAirportsFromNaptans returned null when called with all expected naptans");
			Assert.AreEqual(expectedAirports.Count, returnedAirports.Count, "GetAirportsFromNaptans returned an incorrect number of airports when called with all expected naptans");

		}

		#endregion

		#region TestGetRegionMethods

		/// <summary>
		/// Tests the following methods:
		///		GetRegion
		///		GetRegions
		/// </summary>
		[Test]
		public void TestGetRegionMethods()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			// Check that GetRegion returns null when an invalid region code is passed
			Assert.IsNull(airData.GetRegion(5161), "GetRegion did not return null when called with an invalid region code");

			// Check that all regions can be retrieved and are as expected
			ArrayList expectedRegions = new ArrayList();
			foreach (XmlNode currNode in data.GetElementsByTagName("airregion"))
			{
				// Verify that we expect the region to be used
				bool regionIsUsed = false;
				XmlNodeList regionAirportNodes = data.SelectNodes("//regionairport[@airregioncode='" + currNode.Attributes["code"].Value + "']");
				foreach (XmlNode currRegionAirport in regionAirportNodes)
				{
					// See if the airport specified has a route
					XmlNodeList matchingOrigins = data.SelectNodes("//flightroute[@origincode='" + currRegionAirport.Attributes["airportcode"].Value + "']");
					regionIsUsed = (matchingOrigins.Count > 0);
					if (regionIsUsed)
						break;

					XmlNodeList matchingDestinations = data.SelectNodes("//flightroute[@destinationcode='" + currRegionAirport.Attributes["airportcode"].Value + "']");
					regionIsUsed = (matchingDestinations.Count > 0);
					if (regionIsUsed)
						break;
				}

				if (!regionIsUsed)
					continue;

				int currCode = Convert.ToInt32(currNode.Attributes["code"].Value);
				AirRegion currRegion = airData.GetRegion(currCode);
				Assert.IsNotNull(currRegion, "Calling GetRegion with code=" + currCode.ToString() + " returned null.");
				if (currRegion != null)
				{
					Assert.AreEqual(currCode, currRegion.Code, "Retrieved region code not as expected.");
					Assert.AreEqual(currNode.Attributes["name"].Value, currRegion.Name, "Retrieved region name not as expected.");
					expectedRegions.Add(currRegion);
				}
			}

			// Now check that the arraylist we have just built matches that returned by GetRegions, ie
			// there are no missing or unexpected items
			ArrayList returnedRegions = airData.GetRegions();
			Assert.AreEqual(expectedRegions.Count, returnedRegions.Count, "GetRegions returned an unexpected number of regions");
			foreach (AirRegion currRegion in expectedRegions)
				Assert.IsTrue(returnedRegions.Contains(currRegion), "Region " + currRegion.Code + " missing from GetRegions return data");

		}

		#endregion

		#region TestGetOperatorMethods

		/// <summary>
		/// Tests the following methods:
		///		GetOperator
		///		GetOperators
		/// </summary>
		[Test]
		public void TestGetOperatorMethods()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			// Check that GetOperator returns null when an invalid operator code is passed
			Assert.IsNull(airData.GetOperator(string.Empty), "GetOperator did not return null when called with an empty string");
			Assert.IsNull(airData.GetOperator("kjhgkhgkjh"), "GetOperator did not return null when called with an invalid operator code");

			// Check that all operators can be retrieved and are as expected
			ArrayList expectedOperators = new ArrayList();
			foreach (XmlNode currNode in data.GetElementsByTagName("operator"))
			{
				AirOperator currOperator = airData.GetOperator(currNode.Attributes["code"].Value);
				Assert.IsNotNull(currOperator, "Calling GetOperator with code=" + currNode.Attributes["code"].Value + " returned null.");
				if (currOperator != null)
				{
					Assert.AreEqual(currNode.Attributes["code"].Value, currOperator.IATACode, "Retrieved operator code not as expected.");
					Assert.AreEqual(currNode.Attributes["name"].Value, currOperator.Name, "Retrieved operator name not as expected.");
					expectedOperators.Add(currOperator);
				}
			}

			// Now check that the arraylist we have just built matches that returned by GetOperators, ie
			// there are no missing or unexpected items
			ArrayList returnedOperators = airData.GetOperators();
			Assert.AreEqual(expectedOperators.Count, returnedOperators.Count, "GetOperators returned an unexpected number of operators");
			foreach (AirOperator currOperator in expectedOperators)
				Assert.IsTrue(returnedOperators.Contains(currOperator), "Operator " + currOperator.IATACode + " missing from GetOperators return data");

			// Additional check on returnedOperators to ensure that operators are returned in the correct order

			//			XmlNodeList sortedOperatorNodes = getSortedNodeList(data, "airoperator");
			//			for (int index = 0; index < returnedOperators.Count - 1; index++)
			//			{
			//				AirOperator currOperator = airData.GetOperator(Convert.ToInt32(sortedOperatorNodes[index].Attributes["code"].Value));
			//				Assertion.AssertEquals("Operators returned by GetOperators are in the wrong order", currOperator, returnedOperators[index]);
			//			}


		}

		#endregion

		#region TestRegionAirportMappings

		/// <summary>
		/// Tests the following:
		///		GetRegionAirports
		///		GetAirportRegions
		/// </summary>
		[Test]
		public void TestRegionAirportMappings()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			// For each region, check that the GetRegionAirports method returns
			// the correct values
		
			// Build the expected data from the file
			foreach (XmlNode currNode in data.GetElementsByTagName("airregion"))
			{
				AirRegion currRegion = airData.GetRegion(Convert.ToInt32(currNode.Attributes["code"].Value));

				ArrayList expectedAirports = new ArrayList();
				foreach (XmlNode currAirport in data.SelectNodes("//regionairport[@airregioncode='" + currNode.Attributes["code"].Value + "']"))
				{
					bool airportIsUsed = false;
					// Verify that there is a flight associated with the current airport
					// See if the airport specified has a route
					XmlNodeList matchingOrigins = data.SelectNodes("//flightroute[@origincode='" + currAirport.Attributes["airportcode"].Value + "']");
					airportIsUsed = airportIsUsed || (matchingOrigins.Count > 0);

					XmlNodeList matchingDestinations = data.SelectNodes("//flightroute[@destinationcode='" + currAirport.Attributes["airportcode"].Value + "']");
					airportIsUsed = airportIsUsed || (matchingDestinations.Count > 0);

					if (airportIsUsed)
						expectedAirports.Add(airData.GetAirport(ProcessAirportCode(currAirport.Attributes["airportcode"].Value)));
				}

				// Compare to expected
				if (currRegion == null && expectedAirports.Count > 0)
					Assert.Fail("Missing region " + currNode.Attributes["code"].Value);
				else if (currRegion != null)
				{
					ArrayList actualAirports = airData.GetRegionAirports(currRegion.Code);

					Assert.AreEqual(expectedAirports.Count, actualAirports.Count, "Wrong number of airports returned for region " + currRegion.Code.ToString());
					foreach (Airport a in expectedAirports)
						Assert.IsTrue(actualAirports.Contains(a), "Expected airport " + a.IATACode + " missing from airports for " + currRegion.Code.ToString());
				}
			}

			// Repeat the other way round for GetAirportRegions
			foreach (XmlNode currNode in data.GetElementsByTagName("flightroute"))
			{
				Airport currAirport = airData.GetAirport(ProcessAirportCode(currNode.Attributes["origincode"].Value));
				ArrayList expectedRegions = new ArrayList();
				foreach (XmlNode currRegion in data.SelectNodes("//regionairport[@airportcode='" + currNode.Attributes["origincode"].Value + "']"))
					expectedRegions.Add(airData.GetRegion(Convert.ToInt32(currRegion.Attributes["airregioncode"].Value)));


				ArrayList actualRegions = airData.GetAirportRegions(currAirport.IATACode);

				// Compare to expected
				Assert.AreEqual(expectedRegions.Count, actualRegions.Count, "Wrong number of regions returned for airport " + currAirport.IATACode);
				foreach (AirRegion r in expectedRegions)
					Assert.IsTrue(actualRegions.Contains(r), "Expected region " + r.Code + " missing from regions for " + currAirport.IATACode);
			}

		}


		#endregion

		#region TestGetRoutes

		/// <summary>
		/// Tests the GetRoutes method
		/// </summary>
		[Test]
		public void TestGetRoutes()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			// Build an expected arraylist
			ArrayList expectedRoutes = new ArrayList();
			foreach (XmlNode curr in data.GetElementsByTagName("flightroute"))
			{
				AirRoute newRoute = new AirRoute(ProcessAirportCode(curr.Attributes["origincode"].Value), ProcessAirportCode(curr.Attributes["destinationcode"].Value));
				if (!expectedRoutes.Contains(newRoute))
					expectedRoutes.Add(newRoute);
			}

			ArrayList actualRoutes = airData.GetRoutes();
			Assert.AreEqual(expectedRoutes.Count, actualRoutes.Count, "Wrong number of routes returned");
			// Compare data
			foreach (AirRoute r in expectedRoutes)
				Assert.IsTrue(actualRoutes.Contains(r), "Route " + r.CompoundName + " missing from actual routes.");
		}				
			
		#endregion

		#region TestRouteMethods

		/// <summary>
		/// Tests the following:
		///		GetValidDestinationRegions
		///		GetValidOriginAirports
		/// </summary>
		[Test]
		public void TestRouteMethods()
		{
			// Build route tables
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			//		ArrayList GetValidDestinationAirports(Airport[] originAirports); // ArrayList of Airport
			//		ArrayList GetValidDestinationRegions(Airport[] originAirports); // ArrayList of AirRegion
			//		ArrayList GetValidOriginAirports(Airport[] destinationAirports); // ArrayList of Airport
			//		ArrayList GetValidOriginRegions(Airport[] destinationAirports); // ArrayList of AirRegion
			Airport[] inputAirports = (Airport[])airData.GetAirports().ToArray(typeof(Airport));
			ArrayList expectedAirports = new ArrayList();
			ArrayList expectedRegions = new ArrayList();
			foreach (AirRoute r in airData.GetRoutes())
			{
				if (!expectedAirports.Contains(airData.GetAirport(r.OriginAirport)))
					expectedAirports.Add(airData.GetAirport(r.OriginAirport));
				if (!expectedAirports.Contains(airData.GetAirport(r.DestinationAirport)))
					expectedAirports.Add(airData.GetAirport(r.DestinationAirport));
			}
			foreach (Airport a in expectedAirports)
				foreach (AirRegion r in airData.GetAirportRegions(a.IATACode))
					if (!expectedRegions.Contains(r))
						expectedRegions.Add(r);
			
			ArrayList results = airData.GetValidDestinationAirports(inputAirports);
			Assert.AreEqual(expectedAirports.Count, results.Count, "Number of airports returned from GetValidDestinationAirports not as expected");
			foreach (Airport a in expectedAirports)
				Assert.IsTrue(results.Contains(a), "Airport " + a.IATACode + " not found in actual results");

			results = airData.GetValidDestinationRegions(inputAirports);
			Assert.AreEqual(expectedRegions.Count, results.Count, "Number of regions returned from GetValidDestinationRegions not as expected");

			results = airData.GetValidOriginAirports(inputAirports);
			Assert.AreEqual(expectedAirports.Count, results.Count, "Number of airports returned from GetValidOriginAirports not as expected");

			results = airData.GetValidOriginRegions(inputAirports);
			Assert.AreEqual(expectedRegions.Count, results.Count, "Number of regions returned from GetValidOriginRegions not as expected");

			foreach (AirRegion currTestRegion in airData.GetRegions())
			{
				inputAirports = (Airport[])airData.GetRegionAirports(currTestRegion.Code).ToArray(typeof(Airport));
				// Get expected results using the other overload for each method, as that has now been tested
				expectedAirports = airData.GetValidDestinationAirports(inputAirports);
				expectedRegions = airData.GetValidDestinationRegions(inputAirports);

				results = airData.GetValidDestinationAirports(currTestRegion);
				Assert.AreEqual(expectedAirports.Count, results.Count, "Number of airports returned from GetValidDestinationAirports not as expected");

				results = airData.GetValidDestinationRegions(currTestRegion);
				Assert.AreEqual(expectedRegions.Count, results.Count, "Number of airports returned from GetValidDestinationRegions not as expected");

				results = airData.GetValidOriginAirports(currTestRegion);
				Assert.AreEqual(expectedAirports.Count, results.Count, "Number of airports returned from GetValidOriginAirports not as expected");

				results = airData.GetValidOriginRegions(currTestRegion);
				Assert.AreEqual(expectedRegions.Count, results.Count, "Number of airports returned from GetValidOriginRegions not as expected");

			}
			
		}
		#endregion

		#region TestOperatorMethods

		/// <summary>
		/// Tests the following
		///		GetAirportOperators
		///		GetRouteOperators
		/// </summary>
		[Test]
		public void TestOperatorMethods()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			// Load the flight routes and operators data
			Hashtable routeOperators = new Hashtable();
			Hashtable airportOperators = new Hashtable();
			foreach (XmlNode curr in data.GetElementsByTagName("flightroute"))
			{
				AirRoute currRoute = new AirRoute(ProcessAirportCode(curr.Attributes["origincode"].Value), ProcessAirportCode(curr.Attributes["destinationcode"].Value));
				AirOperator currOperator = airData.GetOperator(curr.Attributes["operatorcode"].Value);
				Airport originAirport = airData.GetAirport(currRoute.OriginAirport);
				Airport destinationAirport = airData.GetAirport(currRoute.DestinationAirport);
				Assert.IsNotNull(currOperator, "Failed to get operator object for operator code " + curr.Attributes["operatorcode"].Value);

				if (routeOperators[currRoute] == null)
					routeOperators[currRoute] = new ArrayList();

				if (airportOperators[originAirport] == null)
					airportOperators[originAirport] = new ArrayList();

				if (airportOperators[destinationAirport] == null)
					airportOperators[destinationAirport] = new ArrayList();

				if (!((ArrayList)routeOperators[currRoute]).Contains(currOperator))
					((ArrayList)routeOperators[currRoute]).Add(currOperator);

				if (!((ArrayList)airportOperators[originAirport]).Contains(currOperator))
					((ArrayList)airportOperators[originAirport]).Add(currOperator);

				if (!((ArrayList)airportOperators[destinationAirport]).Contains(currOperator))
					((ArrayList)airportOperators[destinationAirport]).Add(currOperator);

			}

			// Now, for each airport compare the values we read from the xml to the air data provider
			// values
			foreach (Airport a in airData.GetAirports())
			{
				ArrayList actualOperators = airData.GetAirportOperators(new Airport[] { a });
				ArrayList expectedOperators = (ArrayList)airportOperators[a];
				if (actualOperators == null)
					actualOperators = new ArrayList();
				if (expectedOperators == null)
					expectedOperators = new ArrayList();
				Assert.AreEqual(expectedOperators.Count, actualOperators.Count, "Wrong number of operators for airport " + a.IATACode);
				// Make sure they are the same
				foreach (AirOperator o in expectedOperators)
					Assert.IsTrue(actualOperators.Contains(o), "Operator " + o.IATACode + " not found in actual results");
			}

			foreach (AirRoute a in airData.GetRoutes())
			{
				ArrayList actualOperators = airData.GetRouteOperators(new AirRoute[] { a });
				ArrayList expectedOperators = (ArrayList)routeOperators[a];
				if (actualOperators == null)
					actualOperators = new ArrayList();
				if (expectedOperators == null)
					expectedOperators = new ArrayList();
				Assert.AreEqual(expectedOperators.Count, actualOperators.Count, "Wrong number of operators for route " + a.CompoundName);
				foreach (AirOperator o in expectedOperators)
					Assert.IsTrue(actualOperators.Contains(o), "Operator " + o.IATACode + " not found in actual results");
			}

		}


		#endregion

		#region TestValidRouteMethods

		/// <summary>
		/// Tests the following:
		///		GetValidRoutes
		///		ValidRouteExists
		/// </summary>
		[Test]
		public void TestValidRouteMethods()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			IAirDataProvider airData = AddAirDataProvider();

			XmlDocument data = new XmlDocument();
			data.Load(dataFile1);

			Airport[] allAirports = (Airport[])airData.GetAirports().ToArray(typeof(Airport));
			string[] allAirportCodes = new string[allAirports.Length];
			for (int index = 0; index < allAirports.Length; index++)
				allAirportCodes[index] = allAirports[index].IATACode;

			ArrayList allRoutes = airData.GetRoutes();

			//		ArrayList GetValidRoutes(Airport[] originAirports, Airport[] destinationAirports);
			//		bool ValidRouteExists(string[] originAirportCodes, string[] destinationAirportCodes);
			
			ArrayList resultRoutes = airData.GetValidRoutes(allAirports, allAirports);
			// All routes should be returned
			Assert.AreEqual(allRoutes.Count, resultRoutes.Count, "Expected number of routes differed from actual when calling GetValidRoutes");
			foreach (AirRoute r in allRoutes)
				Assert.IsTrue(resultRoutes.Contains(r), "Expected route " + r.CompoundName + " not found in results from GetValidRoutes");

			bool expectedResult = allRoutes.Count > 0;
			Assert.AreEqual(expectedResult, airData.ValidRouteExists(allAirportCodes, allAirportCodes), "ValidRouteExists returns unexpected value");
			
			// Now we know whether or not these methods work, and we know GetRegionAirports works, we
			// can use this knowledge to test the remainder of the routines
			foreach (AirRegion r in airData.GetRegions())
			{
				Airport[] currRegionAirports = (Airport[])airData.GetRegionAirports(r.Code).ToArray(typeof(Airport));
				
				//		ArrayList GetValidRoutes(AirRegion originRegion, Airport[] destinationAirports);
				//		bool ValidRouteExists(int originRegionCode, string[] destinationAirportCodes);

				ArrayList expectedResults = airData.GetValidRoutes(currRegionAirports, allAirports);
				resultRoutes = airData.GetValidRoutes(r, allAirports);

				// compare
				Assert.AreEqual(expectedResults.Count, resultRoutes.Count, "Expected number of routes differed from actual when calling GetValidRoutes");
				foreach (AirRoute route in expectedResults)
					Assert.IsTrue(resultRoutes.Contains(route), "Expected route " + route.CompoundName + " not found in results from GetValidRoutes");

				expectedResult = expectedResults.Count > 0;
				Assert.AreEqual(expectedResult, airData.ValidRouteExists(r.Code, allAirportCodes), "ValidRouteExists returns unexpected value");


				//		ArrayList GetValidRoutes(Airport[] originAirports, AirRegion destinationRegion);
				//		bool ValidRouteExists(string[] originAirportsCode, int destinationRegionCode);
				expectedResults = airData.GetValidRoutes(allAirports, currRegionAirports);
				resultRoutes = airData.GetValidRoutes(allAirports, r);

				// compare
				Assert.AreEqual(expectedResults.Count, resultRoutes.Count, "Expected number of routes differed from actual when calling GetValidRoutes");
				foreach (AirRoute route in expectedResults)
					Assert.IsTrue(resultRoutes.Contains(route));

				expectedResult = expectedResults.Count > 0;
				Assert.AreEqual(expectedResult, airData.ValidRouteExists(allAirportCodes, r.Code));


				//		ArrayList GetValidRoutes(AirRegion originRegion, AirRegion destinationRegion);
				//		bool ValidRouteExists(int originRegionCode, int destinationRegionCode);
				foreach (AirRegion rInner in airData.GetRegions())
				{
					Airport[] currInnerRegionAirports = (Airport[])airData.GetRegionAirports(rInner.Code).ToArray(typeof(Airport));
					expectedResults = airData.GetValidRoutes(currRegionAirports, currInnerRegionAirports);
					resultRoutes = airData.GetValidRoutes(r, rInner);

					// compare
					Assert.AreEqual(expectedResults.Count, resultRoutes.Count, "Expected number of routes differed from actual when calling GetValidRoutes");
					foreach (AirRoute route in expectedResults)
						Assert.IsTrue(resultRoutes.Contains(route), "Expected route " + route.CompoundName + " not found in results from GetValidRoutes");

					expectedResult = expectedResults.Count > 0;
					Assert.AreEqual(expectedResult, airData.ValidRouteExists(r.Code, rInner.Code), "ValidRouteExists returns unexpected value");

				}

			}

		}

		#endregion

		#region TestDataReload

		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// For the sake of simplicity, the second data file that is loaded contains no
		/// data, which also verifies that the ADP will load successfully when no data is
		/// present.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			AirDataTestHelper.LoadDataFile(dataFile1);
			AddScriptRepository();
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
			IAirDataProvider airData = AddAirDataProvider();

			int originalAirportCount = airData.GetAirports().Count;
			
			Assert.AreEqual(true, airData.ScriptGenerated, "Script not created on initialisation");

			// Delete old data and load new file (dataFile2 contains no data)
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.AirDataProviderDB);
			AirDataTestHelper.ClearTableDown("FlightRoutes", helper);
			AirDataTestHelper.ClearTableDown("RegionAirports", helper);
			AirDataTestHelper.ClearTableDown("AirRegions", helper);
			AirDataTestHelper.ClearTableDown("Airports", helper);
			AirDataTestHelper.ClearTableDown("Operators", helper);
			helper.ConnClose();
			AirDataTestHelper.LoadDataFile(dataFile2);

			// Check that data hasn't changed
			Assert.AreEqual(originalAirportCount, airData.GetAirports().Count, "Data changed too early");
			
			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("Air");
			
			// Check that the data has changed
			Assert.AreEqual(0, airData.GetAirports().Count, "Data not successfully reloaded");
			Assert.AreEqual(true, airData.ScriptGenerated, "Script not created on reload");
		}

		#endregion
	}

	#region Database helper class

	public sealed class AirDataTestHelper
	{
		private const string tempTablePrefix = "tempTestBackup";

		private AirDataTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.AirDataProviderDB);
			
			BackupTableAndClearDown("FlightRoutes", helper);
			BackupTableAndClearDown("RegionAirports", helper);
			BackupTableAndClearDown("AirRegions", helper);
			BackupTableAndClearDown("Airports", helper);
			BackupTableAndClearDown("Operators", helper);

		}

		public static void RestoreOriginalData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.AirDataProviderDB);

			ClearTableDown("FlightRoutes", helper);
			ClearTableDown("RegionAirports", helper);
			ClearTableDown("AirRegions", helper);
			ClearTableDown("Airports", helper);
			ClearTableDown("Operators", helper);

			RestoreFromBackup("Operators", helper);
			RestoreFromBackup("Airports", helper);
			RestoreFromBackup("AirRegions", helper);
			RestoreFromBackup("RegionAirports", helper);
			RestoreFromBackup("FlightRoutes", helper);

			RemoveBackupTable("FlightRoutes", helper);
			RemoveBackupTable("RegionAirports", helper);
			RemoveBackupTable("AirRegions", helper);
			RemoveBackupTable("Airports", helper);
			RemoveBackupTable("Operators", helper);


		}

		/// <summary>
		/// Drops the specified table.
		/// </summary>
		/// <param name="tableName">Name of the table to drop</param>
		/// <param name="connectedHelper"></param>
		private static void DropTable(string tableName, SqlHelper connectedHelper)
		{
			try
			{
				connectedHelper.Execute("drop table " + tableName);
			}
			catch (SqlException s)
			{
				// Allow a sql error with msg code 3701
				if (s.Number != 3701)
					throw new Exception("An unexpected error occurred", s);
			}
		}

		/// <summary>
		/// Backs up the specified table and then clears it down.
		/// BE VERY CAREFUL OF CASCADING CONSTRAINTS - ensure you back up related tables in the
		/// correct order
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void BackupTableAndClearDown(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			DropTable(tempTableName, connectedHelper);
			connectedHelper.Execute(String.Format("select * into {0} from {1}", tempTableName, tableName));
			ClearTableDown(tableName, connectedHelper);
		}

		/// <summary>
		/// Restores the data
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="connectedHelper"></param>
		private static void RestoreFromBackup(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			connectedHelper.Execute(String.Format("insert into {0} select * from {1}", tableName, tempTableName));
		}

		/// <summary>
		/// Deletes the contents of the specified helper
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="connectedHelper"></param>
		public static void ClearTableDown(string tableName, SqlHelper connectedHelper)
		{
			connectedHelper.Execute("delete " + tableName);
		}

		/// <summary>
		/// Restores the data
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="connectedHelper"></param>
		private static void RemoveBackupTable(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			DropTable(tempTableName, connectedHelper);
		}

		private static string AttributeValueForDatabase(XmlNode node, string attributeName)
		{
			XmlAttribute attribute;
			attribute = node.Attributes[attributeName];
			if (attribute != null)
				return attribute.Value.Replace("'", "''");
			else
				return string.Empty;
		}

		public static bool LoadDataFile(string dataFile)
		{
			XmlDocument xmlData = new XmlDocument();
			xmlData.Load(dataFile);

			//Data is in a straightforward format:
			//Rootnode is called "airDataProviderTestData"
			//Five types of child node:
			//     <airport code="" name="" terminals="">
			//     <airregion code="" name="" itemorder="">
			//     <flightroute origincode="" destinationcode="" operatorcode="">
			//     <operator code="" name="">
			//     <regionairport airportcode="" airregioncode="">
			//The following SQL dumps the current tables into xml of this format
			//(note special characters are not accounted for)
			//
			//select '<airport code="' + code + '" name="' + name + '" terminals="' + cast(terminals as varchar(2)) + '"/>' from airports
			//union
			//select '<airregion code="' + cast(code as varchar(2)) + '" name="' + name + '" itemorder="' + cast(itemorder as varchar(2)) + '"/>' from airregions
			//union
			//select '<flightroute origincode="' + origincode + '" destinationcode="' + destinationcode + '" operatorcode="' + operatorcode + '"/>' from flightroutes
			//union
			//select '<operator code="' + code + '" name="' + name + '"/>' from operators
			//union
			//select '<regionairport airportcode="' + airportcode + '" airregioncode="' + cast(airregioncode as varchar(2)) + '"/>' from regionairports

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.AirDataProviderDB);
			
			string insertAirport = "insert into airports (code, name, terminals) values ('{0}', '{1}', {2})";
			string insertAirRegion = "insert into airregions (code, name, itemorder) values ({0}, '{1}', {2})";
			string insertFlightRoute = "insert into flightroutes (origincode, destinationcode, operatorcode) values ('{0}', '{1}', '{2}')";
			string insertOperator = "insert into operators (code, name) values ('{0}', '{1}')";
			string insertRegionAirport = "insert into regionairports (airportcode, airregioncode) values ('{0}', {1})";

			// First get airports
			XmlNodeList currNodes = xmlData.GetElementsByTagName("airport");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(insertAirport, AttributeValueForDatabase(curr, "code"), AttributeValueForDatabase(curr, "name"), AttributeValueForDatabase(curr, "terminals")));

			currNodes = xmlData.GetElementsByTagName("airregion");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(insertAirRegion, AttributeValueForDatabase(curr, "code"), AttributeValueForDatabase(curr, "name"), AttributeValueForDatabase(curr, "itemorder")));

			currNodes = xmlData.GetElementsByTagName("operator");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(insertOperator, AttributeValueForDatabase(curr, "code"), AttributeValueForDatabase(curr, "name")));

			currNodes = xmlData.GetElementsByTagName("regionairport");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(insertRegionAirport, AttributeValueForDatabase(curr, "airportcode"), AttributeValueForDatabase(curr, "airregioncode")));

			currNodes = xmlData.GetElementsByTagName("flightroute");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(insertFlightRoute, AttributeValueForDatabase(curr, "origincode"), AttributeValueForDatabase(curr, "destinationcode"), AttributeValueForDatabase(curr, "operatorcode")));

			helper.ConnClose();

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for air data provider test
	/// </summary>
	public class AirDataProviderTestInitialisation : IServiceInitialisation
	{
		public AirDataProviderTestInitialisation()
		{
		}

		// Need to add a file property provider
		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{
			ArrayList errors = new ArrayList();

			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// create custom email publisher
				IEventPublisher[] customPublishers = new IEventPublisher[0];	
			
				// create and add TDTraceListener instance to the listener collection	
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
			}
			catch (TDException tdEx)
			{
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdEx.Message); // prepend with existing exception message

				// append all messages returned by TDTraceListener constructor
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				// log message using .NET default trace listener
				Trace.WriteLine(message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString());			

				// rethrow exception - use the initial exception id as the id
				throw new TDException(message.ToString(), tdEx, false, tdEx.Identifier);
			}
		}

	}

	#endregion

}
