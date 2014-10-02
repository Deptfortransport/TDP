// *********************************************** 
// NAME			: TestAvailabilityResult.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityResult class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/AvailabilityEstimator/TestAvailabilityResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:16   mturner
//Initial revision.
//
//   Rev 1.3   Apr 23 2005 11:16:36   jbroome
//Updated after changes to AvailabilityResult class
//
//   Rev 1.2   Mar 18 2005 14:47:04   jbroome
//Added missing class documentation comments (Code Review)
//
//   Rev 1.1   Feb 17 2005 14:43:40   jbroome
//Updated test fixtures after changes to real classes
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:56:18   jbroome
//Initial revision.

using System;
using System.Collections;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Test class which tests the valid creation and public 
	/// methods of the AvailabilityResult class
	/// </summary>
	[TestFixture]
	public class TestAvailabilityResult
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityResult()
		{
	
		}

		#region Test Methods

		/// <summary>
		/// Test method for creating AvailabilityResult object with valid input
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			//Create availabilityresult object with one service
			AvailabilityResult result = new AvailabilityResult( TicketTravelMode.Rail, 
																"originGroup",
																"destinationGroup",
																"SVR",
																new TDDateTime(2004, 01, 01),
																new TDDateTime(2004, 01, 02),
																true);
		
			Assert.AreEqual(TicketTravelMode.Rail, result.Mode, "Mode property value not set correctly");
			Assert.AreEqual("originGroup", result.OriginGroup, "Origin Group property value not set correctly");
			Assert.AreEqual("destinationGroup", result.DestinationGroup, "Destination Group property value not set correctly");
			Assert.AreEqual("SVR", result.TicketCode, "TicketCode property value not set correctly");
			Assert.AreEqual(0, result.JourneyCount, "JourneyCount property not initialised correctly");
			Assert.IsTrue(TDDateTime.AreSameDate(new TDDateTime(2004, 01, 01), result.OutwardDate), "OutwardDate property value not set correctly");
			Assert.IsTrue(TDDateTime.AreSameDate(new TDDateTime(2004, 01, 02), result.ReturnDate), "ReturnDate property value not set correctly");
			Assert.AreEqual(true, result.Available, "Available property value not set correctly");
		}

		/// <summary>
		/// Tests the AddJourneyServices method of the AvailabilityResult class
		/// </summary>
		[Test]
		public void TestAddJourneyServices()
		{
			// Create AvailabilityResult object 
			AvailabilityResult result = new AvailabilityResult(TicketTravelMode.Rail, "TEST", "TEST", "TEST", new TDDateTime(), new TDDateTime(), true);

			// Test JourneyCount = 0
			Assert.AreEqual(0, result.JourneyCount, "JourneyCount property not initialised correctly");

			// Test add valid AvailabilityResultService array - size 1
			AvailabilityResultService[] services = new AvailabilityResultService[1];
			services[0] = new AvailabilityResultService("TEST", "TEST", new TDDateTime(), true);
			try
			{
				result.AddJourneyServices(services);	
			}
			catch
			{
				Assert.Fail("Error in AddJourneyServices method");
			}
			Assert.AreEqual(1, result.JourneyCount, "JourneyCount property not incremented correctly");
			
			// Test add valid AvailabilityResultService array - size 3
			services = new AvailabilityResultService[3];
			services[0] = new AvailabilityResultService("TEST1", "TEST1", new TDDateTime(), true);
			services[1] = new AvailabilityResultService("TEST2", "TEST2", new TDDateTime(), true);
			services[2] = new AvailabilityResultService("TEST3", "TEST3", new TDDateTime(), false);
			try
			{
				result.AddJourneyServices(services);	
			}
			catch
			{
				Assert.Fail("Error in AddJourneyServices method");
			}
			Assert.AreEqual(2, result.JourneyCount, "JourneyCount property not incremented correctly");

			// Test null object should not be added or Journey Count incremented
			try
			{
				result.AddJourneyServices(null);	
			}
			catch
			{
				Assert.Fail("Error in AddJourneyServices method");
			}
			Assert.AreEqual(2, result.JourneyCount, "Null object was incorrectly added to collection");
		}

		/// <summary>
		/// Tests the GetServicesForJourney method of the AvailabilityResult class
		/// </summary>
		[Test]
		public void TestGetServicesForJourney()
		{
			// Create AvailabilityResult object 
			AvailabilityResult result = new AvailabilityResult(TicketTravelMode.Rail, "TEST", "TEST", "TEST", new TDDateTime(), new TDDateTime(), false);

			// Test JourneyCount = 0
			Assert.AreEqual(0, result.JourneyCount, "JourneyCount property not initialised correctly");

			// Test GetServicesForJourney with index 0
			Assert.AreEqual(null, result.GetServicesForJourney(0), "Error with GetServicesForJourney, journey index 0");

			// Test GetServicesForJourney with index 1
			Assert.AreEqual(null, result.GetServicesForJourney(1), "Error with GetServicesForJourney, journey index 1");

			# region Add AvailabilityResultService arrays

			// Add valid AvailabilityResultService array - size 3
			AvailabilityResultService[] services = new AvailabilityResultService[3];
			services[0] = new AvailabilityResultService("TEST1_1", "TEST1_1", new TDDateTime(), true);
			services[1] = new AvailabilityResultService("TEST1_2", "TEST1_2", new TDDateTime(), true);
			services[2] = new AvailabilityResultService("TEST1_3", "TEST1_3", new TDDateTime(), false);
			result.AddJourneyServices(services);	
			Assert.AreEqual(1, result.JourneyCount, "First AvailabilityResultService array not successfully added");

			// Add valid AvailabilityResultService array - size 4
			services = new AvailabilityResultService[4];
			services[0] = new AvailabilityResultService("TEST2_1", "TEST2_1", new TDDateTime(), true);
			services[1] = new AvailabilityResultService("TEST2_2", "TEST2_2", new TDDateTime(), true);
			services[2] = new AvailabilityResultService("TEST2_3", "TEST2_3", new TDDateTime(), false);
			services[3] = new AvailabilityResultService("TEST2_3", "TEST2_3", new TDDateTime(), false);
			result.AddJourneyServices(services);	
			Assert.AreEqual(2, result.JourneyCount, "Second AvailabilityResultService array not successfully added");
		
			// Add valid AvailabilityResultService array - size 2
			services = new AvailabilityResultService[2];
			services[0] = new AvailabilityResultService("TEST3_1", "TEST3_1", new TDDateTime(), true);
			services[1] = new AvailabilityResultService("TEST3_2", "TEST3_2", new TDDateTime(), true);
			result.AddJourneyServices(services);	
			Assert.AreEqual(3, result.JourneyCount, "Third AvailabilityResultService array not successfully added");

			# endregion

			// Test GetServicesForJourney with index 0
			AvailabilityResultService[] journeyServices = result.GetServicesForJourney(0);
			Assert.AreEqual(3, journeyServices.Length, "Error in GetServicesForJourney method");
			Assert.AreEqual("TEST1_1", journeyServices[0].Origin, "Error in GetServicesForJourney method");

			// Test GetServicesForJourney with index 0
			journeyServices = result.GetServicesForJourney(1);
			Assert.AreEqual(4, journeyServices.Length, "Error in GetServicesForJourney method");
			Assert.AreEqual("TEST2_1", journeyServices[0].Origin, "Error in GetServicesForJourney method");

			// Test GetServicesForJourney with index 2
			journeyServices = result.GetServicesForJourney(2);
			Assert.AreEqual(2, journeyServices.Length, "Error in GetServicesForJourney method");
			Assert.AreEqual("TEST3_1", journeyServices[0].Origin, "Error in GetServicesForJourney method");

			// Test GetServicesForJourney with index out of bounds
			journeyServices = result.GetServicesForJourney(5);
			Assert.IsTrue(journeyServices == null, "Error in GetServicesForJourney method with out of bounds index");
		}


		#endregion

	}
}
