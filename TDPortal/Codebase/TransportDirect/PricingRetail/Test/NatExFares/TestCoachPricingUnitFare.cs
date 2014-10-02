//********************************************************************************
//NAME         : TestCoachPricingUnitFare.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Test Implementation of CoachPricingUnitFare class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestCoachPricingUnitFare.cs-arc  $
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
using TransportDirect.UserPortal.PricingRetail.Domain;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Test class used to test the creation and public properties
	/// of the CoachPricingUnitFare class.
	/// </summary>
	[TestFixture]
	public class TestCoachPricingUnitFare
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestCoachPricingUnitFare()
		{
		}

		/// <summary>
		/// Test method.
		/// Tests that a CoachPricingUnitFare object can successfully be created and that
		/// the public properties are set correctly.
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			CoachPricingUnitFare cpuf = new CoachPricingUnitFare("Start Location Naptan", 
																"End Location Naptan", 
																20, 
																"Fare Type", 
																"Fare Type Conditions",
																0,
																"Discount Card Type",
																3,
																16,
																Flexibility.FullyFlexible);

			Assert.AreEqual("Start Location Naptan", cpuf.StartLocationNaptan, "Error in creation of CoachPricingUnitFare with StartLocationNaptan");
			Assert.AreEqual("End Location Naptan", cpuf.EndLocationNaptan, "Error in creation of CoachPricingUnitFare with EndLocationNaptan");
			Assert.AreEqual(20, cpuf.FareAmount, "Error in creation of CoachPricingUnitFare with StartLocationNaptan");
			Assert.AreEqual("Fare Type", cpuf.FareType, "Error in creation of CoachPricingUnitFare with FareType");
			Assert.AreEqual("Fare Type Conditions", cpuf.FareTypeConditions, "Error in creation of CoachPricingUnitFare with FareTypeConditions");
			Assert.AreEqual(0, cpuf.PricingUnitIndex, "Error in creation of CoachPricingUnitFare with PricingUnitIndex");
			Assert.AreEqual("Discount Card Type", cpuf.DiscountCardType, "Error in creation of CoachPricingUnitFare with DiscountCardType");
			Assert.AreEqual(3, cpuf.MinChildFare, "Error in creation of CoachPricingUnitFare with MinChildFare");
			Assert.AreEqual(16, cpuf.MaxChildFare, "Error in creation of CoachPricingUnitFare with MaxChildFare");
			Assert.AreEqual(Flexibility.FullyFlexible, cpuf.Flexibility, "Error in creation of CoachPricingUnitFare with Flexibility");
						
		}
	}
}
