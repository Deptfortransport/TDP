// *********************************************** 
// NAME			: TestPricingUnit.cs
// AUTHOR		: 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the TestPricingUnit class
// ************************************************ 

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Test Harness for PricingUnit
	/// While TestPricingUnitMergePolicy concentrates of testing the decision of how
	/// PricingUnits and JourneyDetails relate to one another TestPricingUnit checks that 
	/// a PricingUnit's properties have been correctly set after the interactions.
	/// </summary>
	[TestFixture]
	public class TestPricingUnit
	{
		// Fares information for comparison
		private TransportDirect.JourneyPlanning.CJPInterface.PricingUnit fares;
		private TransportDirect.JourneyPlanning.CJPInterface.Fare adultSing;
		private TransportDirect.JourneyPlanning.CJPInterface.Fare childSing;
		private TransportDirect.JourneyPlanning.CJPInterface.Fare adultRet;
		private TransportDirect.JourneyPlanning.CJPInterface.Fare childRet;
		private TransportDirect.JourneyPlanning.CJPInterface.Fare adultDis1;
		private TransportDirect.JourneyPlanning.CJPInterface.Fare adultDis2;

		[TestFixtureSetUp]
		public void Init()
		{				
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());	

			fares = TestSampleJourneyData.NatExFaresDovNot;
			adultSing = TestSampleJourneyData.NatExFaresDovNot.prices[0];
			childSing = TestSampleJourneyData.NatExFaresDovNot.prices[1];
			adultRet = TestSampleJourneyData.NatExFaresDovNot.prices[2];
			childRet = TestSampleJourneyData.NatExFaresDovNot.prices[3];
			adultDis1 = TestSampleJourneyData.NatExFaresDovNot.prices[4];
			adultDis2 = TestSampleJourneyData.NatExFaresDovNot.prices[5];
		}

		[TestFixtureTearDown]
		public void CleanUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		
		/// <summary>
		/// Test that PricingUnit fields are correctly set at creation.
		/// </summary>
		[Test]
		public void TestCreationValidInput()
		{
			PricingUnit pu1;
			// Create pu1 with a train pricing unit
			pu1 = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);
			// Check the attributes are correctly set
			Assert.AreEqual(pu1.Mode, TestSampleJourneyData.TrainPJDDovVic.Mode, "Mode of PricingUnit not correctly set");
			Assert.AreEqual(pu1.OperatorCode, TestSampleJourneyData.TrainPJDDovVic.Services[0].OperatorCode, "OperatorCode of PricingUnit not correctly set");
			// Check that we have the expected number legs in the outbound direction
			Assert.AreEqual(1, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
			// Check that we have the expected number of legs in the inbound direction
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");
			// Check that the IncludesUnderground property is false
			Assert.IsTrue(!pu1.IncludesUnderground, "IncludesUnderground incorrectly set to true");
		}

		/// <summary>
		/// Test that PricingUnit fields are correctly set at creation.
		/// </summary>
		[Test]
		public void TestCreationInvalidInput()
		{
			PricingUnit pu1;
			// Confirm that creating pu1 with an inappropriate JourneyDetail will throw an exception.
			try 
			{
				pu1 = new PricingUnit(TestSampleJourneyData.DiffPJDDovNot);
				Assert.Fail("PricingUnit didn't throw an exception when being created with an inappropriate JourneyDetail");
			} 
			catch (TDException e)
			{
				//This is expected
				string temp = e.Message;
			}
		}

		/// <summary>
		/// Test that attempting to add journeys of incompatible modes to a PricingUnit fails.
		/// </summary>
		[Test]
		public void TestMergeInvalidMerge()
		{
			PricingUnit pu1;
			// Create pu1 with a train pricing unit
			pu1 = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);

            RoutingGuideSection rgs = new RoutingGuideSection();

			// Check that attempting to added a coach JourneyDetail returns false and does not affect the PricingUnit
			Assert.IsTrue(!pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.NatExPJDVicNot}, rgs),"Adding a coach JourneyDetail to a train PricingUnit does not return false");

			Assert.AreEqual(1, pu1.OutboundLegs.Count,"Incorrect number of outbound legs");
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");			

            // Check that attempting to added a walking JourneyDetail returns false and does not affect the PricingUnit
            Assert.IsTrue(!pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.WalkPJDNot}, rgs),"Adding a walking JourneyDetail that is the last leg to a train PricingUnit does not return false");

			Assert.AreEqual(1, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");			

		}

		/// <summary>
		/// Test that attempting to add journeys of compatible modes to a PricingUnit succeeds and 
		/// correctly modifies the PricingUnit.
		/// </summary>
		[Test]
		public void TestMergeValidMerge()
		{
			PricingUnit pu1;
			// Create pu1 with a train pricing unit
			pu1 = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);

            RoutingGuideSection rgs = new RoutingGuideSection();

			// Attempt to merge an underground JourneyDetail.
			// This should return false as there are no more rail steps afterwards
			Assert.IsTrue(!pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.UnderPJDStpVic}, rgs), "Adding a underground JourneyDetail to a train PricingUnit (with no subsequent rail steps) returns true");
			
			// Attempt to merge an underground JourneyDetail where there are
			// subsequent rail steps. This should succeed and set the IncludesUnderground flag
			Assert.IsTrue(pu1.Merge(0, new PublicJourneyDetail[2]{TestSampleJourneyData.UnderPJDStpVic,
					   TestSampleJourneyData.TrainPJDVicDov}, rgs),
						"Adding a underground JourneyDetail to a train PricingUnit (with subsequent rail steps) does not return true");

			Assert.AreEqual(2, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");
			Assert.IsTrue(pu1.IncludesUnderground,"IncludesUnderground not set to true");		

			// Attempt to merge a 2nd  JourneyDetail. This should return true, and modify the number of legs
			pu1 = new PricingUnit(TestSampleJourneyData.NatExPJDDovVic);
			Assert.IsTrue(pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.NatExPJDVicNot}, rgs), "Adding a 2nd train JourneyDetail to a train PricingUnit does not return true");
			
			Assert.AreEqual(2, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");			

            // Attempt to merge a walking JourneyDetail to a train pricing unit.
			// where there is no subsequent rail leg
			// This should fail
            pu1 = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);
            Assert.IsTrue(!pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.WalkPJDNot}, rgs), "Adding a walking JourneyDetail to a train PricingUnit (with no subsequent rail steps) returns true");
            
			// Attempt to merge a walking JourneyDetail to a train pricing unit.
			// where there is a subsequent rail leg
			// This should fail
			pu1 = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);
			Assert.IsTrue(pu1.Merge(0, new PublicJourneyDetail[2]{TestSampleJourneyData.WalkPJDNot,
																	TestSampleJourneyData.TrainPJDNotStp}, rgs),
							"Adding a walking JourneyDetail to a train PricingUnit (with subsequent rail steps) does not return true");

			Assert.AreEqual(2, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
            Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");			


		}

		[Test]
		public void TestMergeOverride()
		{
			PricingUnit pu1;
			// Attempt to merge a NatEx fare to a SCL fare without Atkins override information
			// This should return false and leave the number of legs and operator code unaltered
			pu1 = new PricingUnit(TestSampleJourneyData.SCLPJDDovVic);

            RoutingGuideSection rgs = new RoutingGuideSection();

			// Check that the operator code is currently a SCL one
			Assert.AreEqual(TestSampleJourneyData.SCLPJDDovVic.Services[0].OperatorCode,
				pu1.OperatorCode, "Value of operator code not SCL as required");
			// Check that attempting to added a coach JourneyDetail returns false and does not affect the PricingUnit
			Assert.IsTrue(!pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.NatExPJDVicNot}, rgs), "Adding an scl JourneyDetail to a natEx JourneyDetail without override does not return false");
			
			Assert.AreEqual(1, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");			
			// Check that the operator code is still a SCL one
			Assert.AreEqual(TestSampleJourneyData.SCLPJDDovVic.Services[0].OperatorCode,
				pu1.OperatorCode, "Value of operator code not SCL as required");		

			// Attempt to merge a NatEx fare to a SCL fare with Atkins override information
			// This should return true, modify the number of legs, and the operator code should change to NatEx
			pu1 = new PricingUnit(TestSampleJourneyData.SCLPJDDovVic);
			// Check that the operator code is currently a SCL one
			Assert.AreEqual(TestSampleJourneyData.SCLPJDDovVic.Services[0].OperatorCode,
				pu1.OperatorCode, "Value of operator code not SCL as required");
			
			// Check that attempting to added a coach JourneyDetail returns false and does not affect the PricingUnit
			Assert.IsFalse(pu1.Merge(0, new PublicJourneyDetail[1]{TestSampleJourneyData.NatExPJDVicNot}, rgs), "Adding an scl JourneyDetail to a natEx JourneyDetail with override does not return true");
			
			Assert.AreEqual(1, pu1.OutboundLegs.Count, "Incorrect number of outbound legs");
			Assert.AreEqual(0, pu1.InboundLegs.Count, "Incorrect number of return legs");	
		}

		/// <summary>
		/// Test the matching PricingUnits are recognised as such.
		/// </summary>
		[Test]
		public void TestMatchesValid()
		{
			PricingUnit pu1;
			// Create pu1 with a train pricing unit
			pu1 = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);
			// Create pu2 with a matching pricing unit.
			PricingUnit pu2 = new PricingUnit(TestSampleJourneyData.TrainPJDVicDov);
			// Confirm that they match
			Assert.IsTrue(pu1.Matches(pu2), "Matching PricingUnits not recognised");

			// Add the matching return
			pu1.AddReturnUnit(pu2);
			// Check that the number of outbound and inbound legs is correct
			Assert.AreEqual(1, pu1.OutboundLegs.Count,"Incorrect number of outbound legs");
			Assert.AreEqual(1, pu1.InboundLegs.Count, "Incorrect number of return legs");			

		}

		/// <summary>
		/// Test that non matching PricingUnits are recognised as such.
		/// </summary>
		[Test]
		public void TestMatchesInValid()
		{
			PricingUnit pu1;
			// Create pu1 with a coach pricing unit
			pu1 = new PricingUnit(TestSampleJourneyData.SCLPJDDovVic);
			// Create pu2 with a non-matching pricing unit.
			PricingUnit pu2 = new PricingUnit(TestSampleJourneyData.SCLPJDStpDov);
			// Confirm that they don't match
			Assert.IsTrue(!pu1.Matches(pu2), "Non matching PricingUnits incorrectly detected");
		}
	}
}
