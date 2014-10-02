//********************************************************************************
//NAME         : TestCoachJourneyFare.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Test implementation of CoachJourneyFare class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestCoachJourneyFare.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:30   mturner
//Initial revision.
//
//   Rev 1.2   Apr 29 2005 12:03:10   jbroome
//Updated tests after removing FirstLegIndex and LastLegIndex properties from CoachPricingUnitFare class.
//
//   Rev 1.1   Apr 05 2005 14:40:24   jbroome
//Added testing of DiscountCard property
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.0   Mar 23 2005 09:36:54   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Test class used to test the creation and public properties/methods
	/// of the CoachJourneyFare class.
	/// </summary>
	[TestFixture]
	public class TestCoachJourneyFare
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestCoachJourneyFare()
		{
		}

		/// <summary>
		/// Tests that a CoachJourneyFare can successfully be created and that 
		/// public properties are set correctly.
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			
			CoachPricingUnitFare fare1 = new CoachPricingUnitFare("NaPTAN1", "NaPTAN2", 10, "", "", 0, "", 0, 10, Flexibility.FullyFlexible);
			CoachPricingUnitFare fare2 = new CoachPricingUnitFare("NaPTAN3", "NaPTAN4", 20, "", "", 0, "", 0, 10, Flexibility.FullyFlexible);
			CoachPricingUnitFare fare3 = new CoachPricingUnitFare("NaPTAN5", "NaPTAN6", 30, "", "", 0, "", 0, 10, Flexibility.FullyFlexible);
			CoachPricingUnitFare fare4 = new CoachPricingUnitFare("NaPTAN5", "NaPTAN6", 30, "", "", 0, "DISCOUNTED", 0, 10, Flexibility.FullyFlexible);

			CoachPricingUnitFare[] pricingUnits = new CoachPricingUnitFare[3] {fare1, fare2, fare3};
			CoachPricingUnitFare[] discountedPricingUnits = new CoachPricingUnitFare[3] {fare1, fare4, fare3};

			//Test creating a CoachJourneyFare with non-disounted pricing units.
			CoachJourneyFare journeyFare = new CoachJourneyFare(pricingUnits);
			Assert.AreEqual(60, journeyFare.TotalAmount, "Error in creating CoachJourneyFare with TotalAmount");
			Assert.AreEqual(false, journeyFare.IsDiscounted, "Error in creating CoachJourneyFare. IsDiscounted should be false");
			Assert.AreEqual(string.Empty, journeyFare.DiscountCard, "Error in creating CoachJourneyFare. DiscountCard property should not be set");
			
			//Test creating a CoachJourneyFare with disounted pricing units.
			journeyFare = new CoachJourneyFare(discountedPricingUnits);
			Assert.AreEqual(70, journeyFare.TotalAmount, "Error in creating CoachJourneyFare with TotalAmount");
			Assert.AreEqual(true, journeyFare.IsDiscounted, "Error in creating CoachJourneyFare. IsDiscounted should be true");
			Assert.AreEqual("DISCOUNTED", journeyFare.DiscountCard, "Error in creating CoachJourneyFare. DiscountCard property not set correctly");

		}

	}
}
