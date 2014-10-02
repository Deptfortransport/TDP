// ***********************************************
// NAME 		: TestFindFlightPageState.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 10/06/2004
// DESCRIPTION 	: Tests the FindFlightPageState object
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TestFindFlightPageState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:46   mturner
//Initial revision.
//
//   Rev 1.2   Feb 08 2005 11:31:58   RScott
//Assertion changed to Assert
//
//   Rev 1.1   Sep 11 2004 18:18:58   RPhilpott
//Updated for changes to TDLocation initialisation.
//
//   Rev 1.0   Jun 10 2004 11:07:48   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using NUnit.Framework;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Tests the FindFlightPageState object
	/// </summary>
	[TestFixture]
	public class TestFindFlightPageState
	{
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void Dispose() { }

		/// <summary>
		/// Creates a new object and checks that properties are as expected. Then makes
		/// modifications to the properties and calls initialise again, checking the 
		/// properties are as expected.
		/// </summary>
		[Test]
		public void CreateAndInitialise()
		{
			FlightLocationSelectionMethod expectedOriginLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			bool expectedOriginLocationFixed = false;
			FlightLocationSelectionMethod expectedDestinationLocationSelectionMethod = FlightLocationSelectionMethod.BrowseControl;
			bool expectedDestinationLocationFixed = false;
			bool expectedTravelDetailsVisible = false;
			bool expectedTravelDetailsChanged = false;
			bool expectedAmbiguityMode = false;

			// Create the object
			FindFlightPageState inputState = new FindFlightPageState();

			// Check values against expected
			Assert.AreEqual(expectedOriginLocationSelectionMethod, inputState.OriginLocationSelectionMethod,
				"OriginLocationSelectionMethod not as expected after object creation");
			Assert.AreEqual(expectedOriginLocationFixed, inputState.OriginLocationFixed, 
				"OriginLocationFixed not as expected after object creation");
			Assert.AreEqual(expectedDestinationLocationSelectionMethod, inputState.DestinationLocationSelectionMethod,
				"DestinationLocationSelectionMethod not as expected after object creation");
			Assert.AreEqual(expectedDestinationLocationFixed, inputState.DestinationLocationFixed,
				"DestinationLocationFixed not as expected after object creation");
			Assert.AreEqual(expectedTravelDetailsVisible, inputState.TravelDetailsVisible,
				"TravelDetailsVisible not as expected after object creation");
			Assert.AreEqual(expectedTravelDetailsChanged, inputState.TravelDetailsChanged,
				"TravelDetailsChanged not as expected after object creation");
			Assert.AreEqual(expectedAmbiguityMode, inputState.AmbiguityMode,
				"AmbiguityMode not as expected after object creation");

			// Now change all the properties
			inputState.OriginLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
			inputState.OriginLocationFixed = !expectedOriginLocationFixed;
			inputState.DestinationLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
			inputState.DestinationLocationFixed = !expectedDestinationLocationFixed;
			inputState.TravelDetailsVisible = !expectedTravelDetailsVisible;
			inputState.TravelDetailsChanged = !expectedTravelDetailsChanged;
			inputState.AmbiguityMode = !expectedAmbiguityMode;

			// Now call initialise
			inputState.Initialise();

			// Check values against expected
			Assert.AreEqual(expectedOriginLocationSelectionMethod, inputState.OriginLocationSelectionMethod,
				"OriginLocationSelectionMethod not as expected after reinitialisation");
			Assert.AreEqual(expectedOriginLocationFixed, inputState.OriginLocationFixed,
				"OriginLocationFixed not as expected after reinitialisation creation");
			Assert.AreEqual(expectedDestinationLocationSelectionMethod, inputState.DestinationLocationSelectionMethod,
				"DestinationLocationSelectionMethod not as expected after reinitialisation");
			Assert.AreEqual(expectedDestinationLocationFixed, inputState.DestinationLocationFixed,
				"DestinationLocationFixed not as expected after reinitialisation");
			Assert.AreEqual(expectedTravelDetailsVisible, inputState.TravelDetailsVisible,
				"TravelDetailsVisible not as expected after reinitialisation");
			Assert.AreEqual(expectedTravelDetailsChanged, inputState.TravelDetailsChanged,
				"TravelDetailsChanged not as expected after reinitialisation");
			Assert.AreEqual(expectedAmbiguityMode, inputState.AmbiguityMode,
				"AmbiguityMode not as expected after reinitialisation");

		}

		/// <summary>
		/// Tests that the OriginLocationFixed method remains consistent with the 
		/// OriginLocationSelectionMethod when changed. Similar tests for the 
		/// DestinationLocationFixed and DestinationLocationSelectionMethod
		/// methods.
		/// </summary>
		[Test]
		public void LocationMethods()
		{
			// Create the object
			FindFlightPageState inputState = new FindFlightPageState();

			// To start with the OriginLocationFixed property is false and the
			// OriginLocationSelectionMethod is BrowseControl.

			// Changing the OriginLocationFixed property to true at this point 
			// should have no effect on the OriginLocationSelectionMethod
			// property
			inputState.OriginLocationFixed = true;
			Assert.AreEqual(true, inputState.OriginLocationFixed,
				"OriginLocationFixed value not set correctly");

			// Changing the OriginLocationSelectionMethod property at this point 
			// should have no effect on the OriginLocationFixed property
			inputState.OriginLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
			Assert.AreEqual(FlightLocationSelectionMethod.FindStationTool, inputState.OriginLocationSelectionMethod,
				"OriginLocationSelectionMethod value not set correctly");
			Assert.AreEqual(true, inputState.OriginLocationFixed,
				"OriginLocationFixed value not set correctly");

			// Changing the OriginLocationFixed property to false should change the 
			// OriginLocationSelectionMethod to BrowseControl
			inputState.OriginLocationFixed = false;
			Assert.AreEqual(FlightLocationSelectionMethod.BrowseControl, inputState.OriginLocationSelectionMethod,
				"OriginLocationSelectionMethod value not set correctly");
			Assert.AreEqual(false, inputState.OriginLocationFixed,
				"OriginLocationFixed value not set correctly");

			// Changing the OriginLocationSelectionMethod to FindStationTool should change the
			// OriginLocationFixed to true
			inputState.OriginLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
			Assert.AreEqual(FlightLocationSelectionMethod.FindStationTool, inputState.OriginLocationSelectionMethod,
				"OriginLocationSelectionMethod value not set correctly");
			Assert.AreEqual(true, inputState.OriginLocationFixed,
				"OriginLocationFixed value not set correctly");

			// Repeat for destination
			inputState.DestinationLocationFixed = true;
			Assert.AreEqual(true, inputState.DestinationLocationFixed,
				"DestinationLocationFixed value not set correctly");

			inputState.DestinationLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
			Assert.AreEqual(FlightLocationSelectionMethod.FindStationTool, inputState.DestinationLocationSelectionMethod,
				"DestinationLocationSelectionMethod value not set correctly");
			Assert.AreEqual(true, inputState.DestinationLocationFixed,
				"DestinationLocationFixed value not set correctly");

			inputState.DestinationLocationFixed = false;
			Assert.AreEqual(FlightLocationSelectionMethod.BrowseControl, inputState.DestinationLocationSelectionMethod,
				"DestinationLocationSelectionMethod value not set correctly");
			Assert.AreEqual(false, inputState.DestinationLocationFixed,
				"DestinationLocationFixed value not set correctly");

			inputState.DestinationLocationSelectionMethod = FlightLocationSelectionMethod.FindStationTool;
			Assert.AreEqual(FlightLocationSelectionMethod.FindStationTool, inputState.DestinationLocationSelectionMethod,
				"DestinationLocationSelectionMethod value not set correctly");
			Assert.AreEqual(true, inputState.DestinationLocationFixed,
				"DestinationLocationFixed value not set correctly");

		}

	}
}
