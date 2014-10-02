// *********************************************** 
// NAME                 : TestTDLocationCyclePlanner.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/06/08
// DESCRIPTION          : Test for TDLocation for the specific cycle planner properties and methods
// ***********************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestTDLocationCyclePlanner.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2010 15:41:28   mmodi
//Commented out test as it would never pass
//Resolution for 5461: TD Extra - Code review changes
//
//   Rev 1.0   Jun 20 2008 15:35:58   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.LocationService
{
    [TestFixture]
    public class TestTDLocationCyclePlanner
    {
        #region Private members
        private IGisQuery gisQuery;

        private TDLocation location1;
        private TDLocation location2_InCycleDataArea;
        private TDLocation location3_InCycleDataArea;
        private TDLocation location4_InCycleDataArea;
        private TDLocation location5_NotInCycleDataArea;
        private TDLocation location6_NotInCycleDataArea;
        private TDLocation location7_InDifferentCycleDataArea;

        #endregion

        #region Setup and Teardown
        [TestFixtureSetUp]
        public void SetUp()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInitialisation());

            location1 = CreateLocation(453320, 336100); // Nottingham NG2 1AL

            location2_InCycleDataArea = CreateLocation(384130, 398370); // Manchester M2 1FB
            location3_InCycleDataArea = CreateLocation(385160, 398160); // Manchester M4 7HR
            location4_InCycleDataArea = CreateLocation(384920, 400590); // Manchester M8 0RB
            location5_NotInCycleDataArea = CreateLocation(435030, 386560); // Sheffield S1 4PD
            location6_NotInCycleDataArea = CreateLocation(435030, 386560); // Sheffield S1 4PD
            location7_InDifferentCycleDataArea = CreateLocation(532530, 164480); // Croydon CR0 1BJ
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        #endregion

        #region Tests

        /// <summary>
        /// Check that a TOID is found using the FindNearestITNs method
        /// </summary>
        [Test]
        public void TestFindNearestITNs()
        {
            string[] toidsBefore = location1.Toid;

            location1.PopulateToids();

            string[] toidsAfter = location1.Toid;

            Assert.Greater(toidsAfter.Length, toidsBefore.Length, "Toids have not been populated after method call, expected > 1 toid to be returned");
        }

        /*
         * The following test has been commented out because the GisQuery module used by the Test classes
         * uses stubs which returns the same dummy values for the GIS calls. 
         * In this case the value returned for the FindNearestPointOnTOID call is always the same point.
         * 
        /// <summary>
        /// Check that a Point is found using the FindNearestPointOnToid method
        /// </summary>
        [Test]
        public void TestFindNearestPointOnToid()
        {
            // Default point test
            Point pointBefore = location1.Point;

            location1.PopulatePoint();

            Point pointAfter1 = location1.Point;

            Assert.AreNotEqual(pointAfter1.X, pointBefore.X, "Point coordinate is still the default value, expected a new Point value to be returned");
            Assert.AreNotEqual(pointAfter1.Y, pointBefore.Y, "Point coordinate is still the default value, expected a new Point value to be returned");

            // Change grid reference and test Point again
            OSGridReference gridReference = new OSGridReference(463300, 306910);
            location1.GridReference = gridReference;

            location1.PopulatePoint();

            Point pointAfter2 = location1.Point;

            Assert.AreNotEqual(pointAfter2.X, pointAfter1.X, "Point coordinate has not changed following a new GridReference, expected a new Point value to be returned");
            Assert.AreNotEqual(pointAfter2.Y, pointAfter1.Y, "Point coordinate has not changed following a new GridReference, expected a new Point value to be returned");
        }
        */

        #region Cycle data area tests
        
        /*
         * The following tests have been commented out because the GisQuery module used by the Test classes
         * uses stubs which return dummy values for the GIS calls. In this case the value returned for the 
         * IsPointsInCycleDataArea call is always true.
         * 
        /// <summary>
        /// Tests that all Points are in the CycleDataArea
        /// </summary>
        [Test]
        public void TestPointsAreInCycleDataArea()
        {
            PopulateAllLocationPoints();
            
            // Test for two points
            Point[] points = new Point[] { location2_InCycleDataArea.Point, location3_InCycleDataArea.Point };

            Assert.IsTrue(IsPointsInCycleDataArea(points, false), "Point coordinates are not in the Cycle Data Area, expected them to be in a valid cycle area");

            // Test for three points
            points = new Point[] { location2_InCycleDataArea.Point, location3_InCycleDataArea.Point, location4_InCycleDataArea.Point };

            Assert.IsTrue(IsPointsInCycleDataArea(points, false), "Point coordinates are not in the Cycle Data Area, expected them to be in a valid cycle area");
        }

        /// <summary>
        /// Tests that all Points are in the Same CycleDataArea
        /// </summary>
        [Test]
        public void TestPointsAreInSameCycleDataArea()
        {
            PopulateAllLocationPoints();

            Point[] points = new Point[] { location2_InCycleDataArea.Point, location3_InCycleDataArea.Point };

            Assert.IsTrue(IsPointsInCycleDataArea(points, true), "Point coordinates are not in the SAME Cycle Data Area, expected them to be in the SAME cycle area");

            points = new Point[] { location2_InCycleDataArea.Point, location7_InDifferentCycleDataArea.Point };

            Assert.IsFalse(IsPointsInCycleDataArea(points, true), "Point coordinates are in DIFFERENT Cycle Data Area, expected the test to return false");
        }

        /// <summary>
        /// Tests that Points are in Different CycleDataArea
        /// </summary>
        [Test]
        public void TestPointsAreInDifferentCycleDataAreas()
        {
            PopulateAllLocationPoints();

            Point[] points = new Point[] { location2_InCycleDataArea.Point, location7_InDifferentCycleDataArea.Point };

            Assert.IsTrue(IsPointsInCycleDataArea(points, false), "Point coordinates are in DIFFERENT Cycle Data Area, expected the test to return true");
        }

        /// <summary>
        /// Tests that Points are not in a CycleDataArea
        /// </summary>
        [Test]
        public void TestPointsAreNotInCycleDataArea()
        {
            PopulateAllLocationPoints();

            Point[] points = new Point[] { location5_NotInCycleDataArea.Point, location6_NotInCycleDataArea.Point };

            Assert.IsFalse(IsPointsInCycleDataArea(points, false), "Point coordinates are not in a Cycle Data Area, expected the test to return false");
        }

        /// <summary>
        /// Tests a mixture of valid/invalid Points are in the CycleDataArea
        /// </summary>
        [Test]
        public void TestSinglePointIsNotInCycleDataArea()
        {
            PopulateAllLocationPoints();

            Point[] points = new Point[] { location2_InCycleDataArea.Point, location3_InCycleDataArea.Point, location5_NotInCycleDataArea.Point};

            Assert.IsFalse(IsPointsInCycleDataArea(points, false), "One of the three Point coordinates are not in a Cycle Data Area, expected the test to return false");
        }
        */
        #endregion

        #endregion

        #region Private methods
        /// <summary>
        /// Helper method to create TDLocation objects, populated with grid references
        /// </summary>
        /// <param name="ids"></param>
        private TDLocation CreateLocation(int easting, int northing)
        {
            TDLocation location = new TDLocation();

            OSGridReference gridReference = new OSGridReference(easting, northing);

            location.GridReference = gridReference;

            return location;
        }

        /// <summary>
        /// Helper method to test for Cycle data area
        /// </summary>
        /// <param name="point"></param>
        /// <param name="sameAreaOnly"></param>
        /// <returns></returns>
        private bool IsPointsInCycleDataArea(Point[] point, bool sameAreaOnly)
        {
            if (gisQuery == null)
                gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.IsPointsInCycleDataArea(point, sameAreaOnly);
        }

        /// <summary>
        /// Helper method to call the populate method for the locations
        /// </summary>
        private void PopulateAllLocationPoints()
        {
            location2_InCycleDataArea.PopulatePoint();
            location3_InCycleDataArea.PopulatePoint();
            location4_InCycleDataArea.PopulatePoint();
            location5_NotInCycleDataArea.PopulatePoint();
            location6_NotInCycleDataArea.PopulatePoint();
            location7_InDifferentCycleDataArea.PopulatePoint();
        }

        #endregion
    }

}
