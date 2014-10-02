// *********************************************** 
// NAME                 : TestInternationalPlaceGazetteer.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/03/2010
// DESCRIPTION          : Test for TestInternationalPlaceGazetteer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestInternationalPlaceGazetteer.cs-arc  $
//
//   Rev 1.0   Mar 16 2010 16:43:04   mmodi
//Initial revision.
//Resolution for 5461: TD Extra - Code review changes
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Test class for TestInternationalPlaceGazetteer.
    /// </summary>
    [TestFixture]
    public class TestInternationalPlaceGazetteer
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInitialisation());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
        }

        /// <summary>
        /// Method to test retrieving a location from the International Planner Gazetteer
        /// </summary>
        [Test]
        public void TestFindLocation()
        {
            InternationalPlaceGazetteer intlGaz = (InternationalPlaceGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlaceGazetteer];

            // Get all the international cities
            LocationQueryResult queryResult = intlGaz.FindLocation(string.Empty, false);

            Assert.GreaterOrEqual(queryResult.LocationChoiceList.Count, 1, "No international places were returned by the InternationalGazetteer, expected at least 1.");

            // Set up the location to test for
            LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[0];

            TDLocation location = new TDLocation();

            // Populate the location
            intlGaz.GetLocationDetails(ref location, string.Empty, false, string.Empty, string.Empty, choice, 0, false);

            Assert.IsTrue(!string.IsNullOrEmpty(location.CityId), "Expected location to have a City Id");
            Assert.IsTrue(location.NaPTANs.Length > 0, "Expected location to have Naptans");
            Assert.IsNotNull(location.Country, "Expected location to have a Country object");
        }
    }
}
