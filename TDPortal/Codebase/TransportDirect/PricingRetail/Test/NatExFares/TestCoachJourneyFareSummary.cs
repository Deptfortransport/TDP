//********************************************************************************
//NAME         : TestCoachJourneyFareSummary.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Test implementation of CoachJourneyFareSummary class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestCoachJourneyFareSummary.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:30   mturner
//Initial revision.
//
//   Rev 1.1   Apr 29 2005 12:03:10   jbroome
//Updated tests after removing FirstLegIndex and LastLegIndex properties from CoachPricingUnitFare class.
//
//   Rev 1.0   Mar 23 2005 09:36:56   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Test class used to test the creation and public properties/methods
	/// of the CoachJourneyFareSummary class.
	/// </summary>
	[TestFixture]
	public class TestCoachJourneyFareSummary
	{
		/// <summary>
		/// Constructor 
		/// </summary>
		public TestCoachJourneyFareSummary()
		{
		}

		/// <summary>
		/// Tests that a CoachJourneyFareSummary can be created successfully
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			// Create CoachJourneyFareSummary object
			CoachJourneyFareSummary summary = new CoachJourneyFareSummary("Origin", "Destination");
			
			// Test that public properties have been correctly initialised
			Assert.AreEqual("Origin", summary.OriginNaptan, "Error in creating CoachJourneyFareSummary with OriginNaPTAN");
			Assert.AreEqual("Destination", summary.DestinationNaptan, "Error in creating CoachJourneyFareSummary with DestinationNaPTAN");
			Assert.AreEqual(0, summary.Journeys.Length, "Error in creating CoachJourneyFareSummary with Journeys");
			Assert.AreEqual(0, summary.JourneyFares.Length, "Error in creating CoachJourneyFareSummary with JourneyFares");
		}

		/// <summary>
		/// Tests the public AddDistinctJourney method of CoachJourneyFareSummary
		/// </summary>
		[Test]
		public void TestAddDistinctJouney()
		{
			CoachJourneyFareSummary summary = new CoachJourneyFareSummary("Origin", "Destination");
			Assert.AreEqual(0, summary.Journeys.Length, "Error in creating CoachJourneyFareSummary with Journeys");
			
			// Create PublicJourney object and add it to the summary
			PublicJourney journey1 = new PublicJourney();
			journey1.JourneyIndex = 0;
            summary.AddDistinctJourney(journey1);

			// Test that PublicJourneyObject has been added to Journeys array
			Assert.AreEqual(1, summary.Journeys.Length, "Error with AddDistinctJourney. Distinct journey1 could not be added.");

			// Try adding the same journey again
			// Only distinct journeys will be addded
			summary.AddDistinctJourney(journey1);

			// Test that PublicJourneyObject has not been added to Journeys array
			Assert.AreEqual(1, summary.Journeys.Length, "Error with AddDistinctJourney. Duplicate journey added.");

			// Create another PublicJourney object and add it to the summary
			PublicJourney journey2 = new PublicJourney();
			journey1.JourneyIndex = 1;
			summary.AddDistinctJourney(journey2);

			// Test that PublicJourneyObject has been added to Journeys array
			Assert.AreEqual(2, summary.Journeys.Length, "Error with AddDistinctJourney. Distinct journey2 could not be added.");

		}

		/// <summary>
		/// Tests the public AddDistinctJourneyFare method of CoachJourneyFareSummary
		/// </summary>
		[Test]
		public void TestAddDistinctJouneyFare()
		{
			CoachJourneyFareSummary summary = new CoachJourneyFareSummary("Origin", "Destination");
			Assert.AreEqual(0, summary.JourneyFares.Length, "Error in creating CoachJourneyFareSummary with JourneyFares");

			//Create CoachJourneyFare object and add it to the JourneyFares array
			CoachPricingUnitFare[] fares = new CoachPricingUnitFare[1];
			fares[0] = new CoachPricingUnitFare("TEST", "TEST", 0, "TEST", "TEST", 0, "TEST", 0, 10, Flexibility.FullyFlexible);
			CoachJourneyFare journeyFare1 = new CoachJourneyFare(fares);
			summary.AddDistinctJourneyFare(journeyFare1);

			// Test that CoachJourneyFare has been added to JourneyFares array
			Assert.AreEqual(1, summary.JourneyFares.Length, "Error with AddDistinctJourneyFare. Distinct journeyFare1 could not be added.");

			// Try adding exactly the same JourneyFare again
			// Only distinct JourneyFares will be added
			summary.AddDistinctJourneyFare(journeyFare1);
			// Test that CoachJourneyFare has not been added to JourneyFares array
			Assert.AreEqual(1, summary.JourneyFares.Length, "Error with AddDistinctJourneyFare. Duplicate journeyFare1 added.");
			
			// Try adding another JourneyFare, with the same IsAdult, IsDiscounted, IsSingle properties
			// This JourneyFare will already be deemed to be represented and, hence, not added
			CoachJourneyFare journeyFare2 = new CoachJourneyFare(fares);
			journeyFare2.IsAdult = journeyFare1.IsAdult;
			journeyFare2.IsDiscounted = journeyFare1.IsDiscounted;
			journeyFare2.IsSingle = journeyFare1.IsSingle;
			summary.AddDistinctJourneyFare(journeyFare2);
			// Test that CoachJourneyFare has not been added to JourneyFares array
			Assert.AreEqual(1, summary.JourneyFares.Length, "Error with AddDistinctJourneyFare. Duplicate journeyFare2 added.");
			
			// Try adding another JourneyFare, with the same IsAdult, IsDiscounted properties
			// but different IsSingle property
			// This JourneyFare will be added
			CoachJourneyFare journeyFare3 = new CoachJourneyFare(fares);
			journeyFare3.IsAdult = journeyFare1.IsAdult;
			journeyFare3.IsDiscounted = journeyFare1.IsDiscounted;
			journeyFare3.IsSingle = !journeyFare1.IsSingle;
			summary.AddDistinctJourneyFare(journeyFare3);
			// Test that CoachJourneyFare has not been added to JourneyFares array
			Assert.AreEqual(2, summary.JourneyFares.Length, "Error with AddDistinctJourneyFare. Distinct journeyFare3 not added.");

			// Try adding another JourneyFare, with the same IsAdult, IsSingle properties
			// but different IsDiscounted property
			// This JourneyFare will be added
			CoachJourneyFare journeyFare4 = new CoachJourneyFare(fares);
			journeyFare4.IsAdult = journeyFare1.IsAdult;
			journeyFare4.IsDiscounted = !journeyFare1.IsDiscounted;
			journeyFare4.IsSingle = journeyFare1.IsSingle;
			summary.AddDistinctJourneyFare(journeyFare4);
			// Test that CoachJourneyFare has not been added to JourneyFares array
			Assert.AreEqual(3, summary.JourneyFares.Length, "Error with AddDistinctJourneyFare. Distinct journeyFare4 not added.");

			// Try adding another JourneyFare, with the same IsDiscounted, IsSingle properties
			// but different IsAdult property
			// This JourneyFare will be added
			CoachJourneyFare journeyFare5 = new CoachJourneyFare(fares);
			journeyFare5.IsAdult = !journeyFare1.IsAdult;
			journeyFare5.IsDiscounted = journeyFare1.IsDiscounted;
			journeyFare5.IsSingle = journeyFare1.IsSingle;
			summary.AddDistinctJourneyFare(journeyFare5);
			// Test that CoachJourneyFare has not been added to JourneyFares array
			Assert.AreEqual(4, summary.JourneyFares.Length, "Error with AddDistinctJourneyFare. Distinct journeyFare5 not added.");
			
		}

	}
}
