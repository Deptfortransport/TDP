// *********************************************** 
// NAME			: Itinerary.cs
// AUTHOR		: 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the TestPricingUnitMergePolicy class
// ************************************************ 

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Test harness for PricingUnitMergePolicy
	/// </summary>
	[TestFixture]
	public class TestPricingRetailMergePolicy
	{
		PricingUnit trainPU;
		PricingUnit natExPU;
		PricingUnit sclPU;
        PricingUnit railReplacementPU;

		PricingUnit trainPU2;
		PricingUnit natExPU2;
		PricingUnit sclPU2;
        PricingUnit railReplacementPU2;

        public TestPricingRetailMergePolicy()
		{
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());

			// Create a PricingUnits for train, NatEx and SCL, using the data in TestSampleJourneyData		
			trainPU = new PricingUnit(TestSampleJourneyData.TrainPJDDovVic);
			natExPU = new PricingUnit(TestSampleJourneyData.NatExPJDDovVic);
			sclPU = new PricingUnit(TestSampleJourneyData.SCLPJDDovVic);
            railReplacementPU = new PricingUnit(TestSampleJourneyData.RailReplacementPDJDovVic);

			trainPU2 = new PricingUnit(TestSampleJourneyData.TrainPJDVicDov);
			natExPU2 = new PricingUnit(TestSampleJourneyData.NatExPJDVicDov);
			sclPU2= new PricingUnit(TestSampleJourneyData.SCLPJDStpDov);
            railReplacementPU2 = new PricingUnit(TestSampleJourneyData.RailReplacementPDJDovVic);

            // Set up the routing guide section needed for the Pricing Units
            trainPU.AddRoutingGuideSectionId(1);
            trainPU.RoutingGuideCompliant = true;

            sclPU.AddRoutingGuideSectionId(1);
            sclPU.RoutingGuideCompliant = true;
		}

		[SetUp]
		public void Init()
		{								   
		}

		[TearDown]
		public void CleanUp()
		{
		}

		/// <summary>
		/// Test that only the appropriate types of JourneyDetails are acceptable for creating PricingUnits
		/// </summary>

		[Test]
		public void TestWillAccept()
		{
			//Confirm that train, natEx and SCL JourneyDetails can be used to create PricingUnits
			Assert.IsTrue(PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.TrainPJDDovVic), "Train JourneyDetail not accepted for a PricingUnit");
			Assert.IsTrue(PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.NatExPJDDovVic), "NatEx JourneyDetail not accepted for a PricingUnit");
			Assert.IsTrue(PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.SCLPJDDovVic), "SCL JourneyDetail not accepted for a PricingUnit");

			//Confirm that other coach operator and the other modes are rejected
			Assert.IsTrue(!PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.CoachPJDNotStp), "None NatEx and SCL coach operator accepted for a PricingUnit - this should not happen");
			Assert.IsTrue(!PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.UnderPJDStpVic), "Underground accepted for a PricingUnit - this should not happen");
			Assert.IsTrue(!PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.DiffPJDDovNot), "Car accepted for a PricingUnit - this should not happen");

			//Confirm that rail replacement JourneyDetails can be used to create PricingUnits
			Assert.IsTrue(PricingUnitMergePolicy.WillAccept(TestSampleJourneyData.RailReplacementPDJDovVic), "Rail replacement JourneyDetail not accepted for a PricingUnit");
		}

		/// <summary>
		/// Test that only appropriate JourneyDetails can be merged into an existing PricingUnit
		/// </summary>
		[Test]
		public void TestCanMerge()
		{
            // Routing guide section use to control whether train legs are compliant. This object is created 
            // as required for each test
            RoutingGuideSection rgsCompliant = new RoutingGuideSection(1, new int[0], true);
            RoutingGuideSection rgsNotCompliant = new RoutingGuideSection(-1, new int[0], false);

			// after IR 1562, the method signatures for these unit tests have been changed,
			// passing an array of journey details and the current index

            Assert.IsTrue(PricingUnitMergePolicy.CanMerge(trainPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.TrainPJDStpNot }, rgsCompliant), 
				"Could not add Train JourneyDetail to Train PricingUnit");

            Assert.IsTrue(PricingUnitMergePolicy.CanMerge(natExPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.NatExPJDVicNot }, rgsNotCompliant),
				"Could not add NatEx JourneyDetail to NatEx PricingUnit");

            Assert.IsTrue(PricingUnitMergePolicy.CanMerge(sclPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.SCLPJDStpDov }, rgsNotCompliant),
				"Could not add SCL JourneyDetail to SCL PricingUnit");

			//Confirm that we can add an Underground JourneyDetail to a rail PricingUnit.
			Assert.IsTrue(PricingUnitMergePolicy.CanMerge(
					trainPU,
					new int[0],
					0,
					new PublicJourneyDetail[2]{TestSampleJourneyData.UnderPJDVicStp,
												TestSampleJourneyData.TrainPJDStpNot},
                    rgsCompliant),
				"Could not add Underground JourneyDetail to Train PricingUnit");
		
			//Test that you can't mix and match valid PricingUnits and JourneyDetail
            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(trainPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.SCLPJDStpDov }, rgsNotCompliant),
				"Adding a SCL JourneyDetail to Train PricingUnit was not rejected");

            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(natExPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.TrainPJDStpNot }, rgsCompliant),
				"Adding a Train JourneyDetail to  NatEx PricingUnit was not rejected");

            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(sclPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.NatExPJDVicNot }, rgsNotCompliant),
				"Adding a NatEx JourneyDetail to SCL PricingUnit was not rejected");
		
			//Test that you can not add any other types of JourneyDetail to the PricingUnits
            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(trainPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.DiffPJDDovNot }, rgsNotCompliant),
				"Adding a Car JourneyDetail to Train PricingUnit was not rejected");

            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(natExPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.CoachPJDNotStp }, rgsNotCompliant),
				"Adding an Other Coach JourneyDetail to  NatEx PricingUnit was not rejected");

            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(sclPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.UnderPJDVicStp }, rgsNotCompliant),
				"Adding an Underground JourneyDetail to SCL PricingUnit was not rejected");
			
			// Confirm that we can add a walking JourneyDetail to a rail PricingUnit as long as
			// there is another rail leg afterwards
            Assert.IsTrue(PricingUnitMergePolicy.CanMerge(
					trainPU,
					new int[0],
					0,
					new PublicJourneyDetail[2]{
												TestSampleJourneyData.WalkPJDNot,
												TestSampleJourneyData.TrainPJDStpNot},
                    rgsCompliant),
				"Could not add a walking JourneyDetail to Train PricingUnit where there is a subsequent rail leg");

			// Confirm that we cannot add a walking JourneyDetail to a rail PricingUnit
			// if there is not another rail leg afterwards
			Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(
				trainPU,
				new int[0],
				0,
				new PublicJourneyDetail[2]{
											  TestSampleJourneyData.WalkPJDNot,
											  TestSampleJourneyData.UnderPJDVicStp},
                rgsNotCompliant),
				"Could not add a walking JourneyDetail to Train PricingUnit where there is no subsequent rail leg");

			//Confirm that we cannot add a walking JourneyDetail to a rail PricingUnit if there are no more legs
            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(trainPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.WalkPJDNot }, rgsNotCompliant),
				"Adding a walking JourneyDetail to Train PricingUnit was not rejected if the last leg");

			//Confirm that we can add a rail replacement JourneyDetail to a rail PricingUnit
            Assert.IsTrue(PricingUnitMergePolicy.CanMerge(trainPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.RailReplacementPDJDovVic }, rgsCompliant),
				"Could not add Rail Replacement JourneyDetail to Train PricingUnit");

            //Confirm that we cannot add a rail replacement to a coach PricingUnit
            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(natExPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.RailReplacementPDJDovVic }, rgsCompliant),
				"Adding a Rail Replacement JourneyDetail to NatEx PricingUnit was not rejected");

            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(sclPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.RailReplacementPDJDovVic }, rgsCompliant),
				"Adding a Rail Replacement JourneyDetail to SCL PricingUnit was not rejected");
		
			//Confirm that an SCL and NatEx leg can not naturally be merged
            Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(sclPU, new int[0], 0, new PublicJourneyDetail[1] { TestSampleJourneyData.NatExPJDVicNot }, rgsNotCompliant),
				"Adding a NatEx JourneyDetail to SCL PricingUnit was not rejected");

			//Confirm that with inappropriate override information an SCL and NatEx leg can not naturally be merged
			Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(
					sclPU,
					new int[2]{0,1},
					2,
					new PublicJourneyDetail[3]{
												  TestSampleJourneyData.SCLPJDStpDov,
												  TestSampleJourneyData.SCLPJDDovVic,
												  TestSampleJourneyData.NatExPJDVicNot},
                    rgsNotCompliant),
				"Adding a NatEx JourneyDetail to SCL PricingUnit with inappropriate override was not rejected");
			
			
			Assert.IsTrue(!PricingUnitMergePolicy.CanMerge(
					sclPU,
					new int[1]{0},
					1,
					new PublicJourneyDetail[2]{
												  TestSampleJourneyData.SCLPJDDovVic,
												  TestSampleJourneyData.NatExPJDVicNot},
                    rgsNotCompliant),
				"Adding a NatEx JourneyDetail to SCL PricingUnit with inappropriate override was not rejected");

			//Confirm that with override information the SCL and NatEx legs can be merged
			Assert.IsTrue(PricingUnitMergePolicy.CanMerge(
					sclPU,
					new int[2]{0,1},
					1,
					new PublicJourneyDetail[3]{
												  TestSampleJourneyData.SCLPJDDovVic,
												  TestSampleJourneyData.NatExPJDVicNot, // this is the target leg here
												  TestSampleJourneyData.NatExPJDNotVic},
                    rgsCompliant),
				"Adding a NatEx JourneyDetail to SCL PricingUnit with override was rejected");

            //Confirm that a rail leg (not RG Compliant) can not be added to a rail PricingUnit (is RG Compliant)
            Assert.IsFalse(PricingUnitMergePolicy.CanMerge(
                trainPU,
                new int[0],
                0,
                new PublicJourneyDetail[1] { TestSampleJourneyData.TrainPJDStpNot }, 
                rgsNotCompliant),
				"Could add a Non RGCompliant Train JourneyDetail to a RGCompliant Train PricingUnit");

            //Confirm that a rail leg (RG Compliant, but different RG section) can not be added to 
            // a rail PricingUnit (RG Compliant)
            rgsCompliant = new RoutingGuideSection(2, new int[0], true);
            Assert.IsFalse(PricingUnitMergePolicy.CanMerge(
                trainPU,
                new int[0],
                0,
                new PublicJourneyDetail[1] { TestSampleJourneyData.TrainPJDStpNot },
                rgsNotCompliant),
                "Could add a RGCompliant Train JourneyDetail to a RGCompliant Train PricingUnit, when they belong to two different RoutingGuideSections");
		}


		/// <summary>
		/// Test that only matching PricingUnits are recognised as such
		/// </summary>
		[Test]
		public void TestMatching()
		{
			//Confirm that journeys with the same operator and opposite start and end points are matching
			Assert.IsTrue(PricingUnitMergePolicy.AreMatching(trainPU, trainPU2), 
				"Train PricingUnits not recognised as matching");
			Assert.IsTrue(PricingUnitMergePolicy.AreMatching(natExPU, natExPU2), 
				"NatEx PricingUnits not recognised as matching");	

			//Confirm that journeys with the same operator, but not opposite start and end points are not matching
			Assert.IsTrue(!PricingUnitMergePolicy.AreMatching(sclPU, sclPU2), 
				"SCL PricingUnits recognised as matching - they are not");
	
			//Confirm that journeys with different operators are not matching even with they have opposite start and end points
			Assert.IsTrue(!PricingUnitMergePolicy.AreMatching(trainPU, natExPU2),
				"Train and coach PricingsUnits recognised as matching - they are not");

			//Confirm that journeys with the same operator and opposite start and end points are matching even if one has a rail replacement
			Assert.IsTrue(PricingUnitMergePolicy.AreMatching(railReplacementPU, trainPU2),
				"Train/Rail replacement PricingUnits not recognised as matching");
		}
	}
}
