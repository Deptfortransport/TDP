//NAME         : TestCoachFareMergePolicy.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Test implementation of CoachFareMergePolicy class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestCoachFareMergePolicy.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:30   mturner
//Initial revision.
//
//   Rev 1.4   Aug 25 2005 14:41:20   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.3   Apr 29 2005 12:03:08   jbroome
//Updated tests after removing FirstLegIndex and LastLegIndex properties from CoachPricingUnitFare class.
//
//   Rev 1.2   Apr 16 2005 16:01:22   jbroome
//Added TestIsInwardJourneyValid() method
//
//   Rev 1.1   Mar 30 2005 16:05:24   jbroome
//Added TestAreTicketsMatching test method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.0   Mar 23 2005 09:36:54   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.PricingRetail.Domain;
using CJP = TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Tests the methods within the CoachFareMergePolicy class
	/// </summary>
	[TestFixture]
	public class TestCoachFareMergePolicy
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestCoachFareMergePolicy()
		{
		}

		#region Test methods

		/// <summary>
		/// Tests the static AreFaresMatching method of the 
		/// CoachFareMergePolicy class 
		/// </summary>
		[Test]
		public void TestAreFaresMatching()
		{
			// Create fares for 3 pricing units (operators)

			//Pricing Unit 1
			CoachPricingUnitFare[] operator1Fares = new CoachPricingUnitFare[7];
			operator1Fares[0] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 0, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator1Fares[1] = new CoachPricingUnitFare("START2", "END1", 100, "TYPE1", "CONDITIONS1", 0, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator1Fares[2] = new CoachPricingUnitFare("START1", "END2", 100, "TYPE1", "CONDITIONS1", 0, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator1Fares[3] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE2", "CONDITIONS1", 0, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator1Fares[4] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 0, "DISCOUNT2", 0, 10, Flexibility.FullyFlexible);
			operator1Fares[5] = new CoachPricingUnitFare("START1", "END1", 200, "TYPE1", "CONDITIONS1", 0, "DISCOUNT2", 0, 10, Flexibility.FullyFlexible);
			operator1Fares[6] = new CoachPricingUnitFare("START1", "END1", 200, "TYPE1", "CONDITIONS1", 0, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			
			//Pricing Unit 2
			CoachPricingUnitFare[] operator2Fares = new CoachPricingUnitFare[7];
			operator2Fares[0] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 1, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator2Fares[1] = new CoachPricingUnitFare("START2", "END1", 100, "TYPE1", "CONDITIONS1", 1, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator2Fares[2] = new CoachPricingUnitFare("START1", "END2", 100, "TYPE1", "CONDITIONS1", 1, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator2Fares[3] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE2", "CONDITIONS1", 1, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator2Fares[4] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 1, "DISCOUNT2", 0, 10, Flexibility.FullyFlexible);
			operator2Fares[5] = new CoachPricingUnitFare("START1", "END1", 200, "TYPE1", "CONDITIONS1", 1, "DISCOUNT2", 0, 10, Flexibility.FullyFlexible);
			operator2Fares[6] = new CoachPricingUnitFare("START1", "END1", 200, "TYPE1", "CONDITIONS1", 1, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			
			//Pricing Unit 3
			CoachPricingUnitFare[] operator3Fares = new CoachPricingUnitFare[7];
			operator3Fares[0] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 2, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator3Fares[1] = new CoachPricingUnitFare("START2", "END1", 100, "TYPE1", "CONDITIONS1", 2, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator3Fares[2] = new CoachPricingUnitFare("START1", "END2", 100, "TYPE1", "CONDITIONS1", 2, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator3Fares[3] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE2", "CONDITIONS1", 2, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			operator3Fares[4] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 2, "DISCOUNT2", 0, 10, Flexibility.FullyFlexible);
			operator3Fares[5] = new CoachPricingUnitFare("START1", "END1", 100, "TYPE1", "CONDITIONS1", 2, "DISCOUNT2", 0, 10, Flexibility.FullyFlexible);
			operator3Fares[6] = new CoachPricingUnitFare("START1", "END1", 200, "TYPE1", "CONDITIONS1", 2, "DISCOUNT1", 0, 10, Flexibility.FullyFlexible);
			
			
			// TEST USING SINGLE PRICING UNIT

			// Create a CoachJourneyFare using a size one array of CoachPricingUnitFare, using the
			// first CoachPricingUnitFare from operator1Fares
			CoachPricingUnitFare[] fares1 = new CoachPricingUnitFare[1] {operator1Fares[0]};
			CoachJourneyFare journeyFare1 = new CoachJourneyFare(fares1);
			journeyFare1.IsAdult = true;

			// Do the same for all the other CoachPricingUnitFare from operator1Fares and try to match
			// against the first CoachJourneyFare. 
			for (int i=1; i<operator1Fares.Length; i++)
			{
				CoachPricingUnitFare[] fares2 = new CoachPricingUnitFare[1] {operator1Fares[i]};
				CoachJourneyFare journeyFare2 = new CoachJourneyFare(fares2);
				journeyFare2.IsAdult = true;
				// Only operator1Fares[4] and operator1Fares[5] should match
				if (i==4 || i==5)
					Assert.AreEqual(true, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using a single pricing unit, journeyFare1 should match with journeyFare2 when i = {0} and both fares are Adult.", i));
				else
					Assert.AreEqual(false, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using a single pricing unit, journeyFare1 should not match with journeyFare2 i = {0} and both fares are Adult.", i));
				
				// Change journeyFare2 to child fare and test again
				journeyFare2.IsAdult = false;
				// operator1Fares[6] should now match as well
				if (i==4 || i==5 || i==6)
					Assert.AreEqual(true, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using a single pricing unit, journeyFare1 should match with journeyFare2 when i = {0} and journeyFare2 is a Child fare.", i));
				else
					Assert.AreEqual(false, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using a single pricing unit, journeyFare1 should not match with journeyFare2 when i = {0} and journeyFare2 is a Child fare.", i));
			}


			// TEST USING TWO PRICING UNITS (OPERATORS)

			// Create a CoachJourneyFare using a size two array of CoachPricingUnitFare, using the
			// first CoachPricingUnitFare from operator1Fares and operator2Fares
			fares1 = new CoachPricingUnitFare[2] {operator1Fares[0], operator2Fares[0]};
			journeyFare1 = new CoachJourneyFare(fares1);
			journeyFare1.IsAdult = true;

			// Do the same for all the other CoachPricingUnitFare from operator1Fares 
			// and operator2Fares and try to match against the first CoachJourneyFare. 
			for (int i=1; i<operator1Fares.Length; i++)
			{
				CoachPricingUnitFare[] fares2 = new CoachPricingUnitFare[2] {operator1Fares[i], operator2Fares[i]};
				CoachJourneyFare journeyFare2 = new CoachJourneyFare(fares2);
				journeyFare2.IsAdult = true;
				// Should only match when i = 4 or 5
				if (i==4 || i==5)
					Assert.AreEqual(true, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using two pricing units, journeyFare1 should match with journeyFare2 when i = {0} and both fares are Adult.", i));
				else
					Assert.AreEqual(false, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using two pricing units, journeyFare1 should not match with journeyFare2 when i = {0} and both fares are Adult.", i));
				
				// Change journeyFare2 to child fare and test again
				journeyFare2.IsAdult = false;
				// i = 6 should now match as well
				if (i==4 || i==5 || i==6)
					Assert.AreEqual(true, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using two pricing units, journeyFare1 should match with journeyFare2 when i = {0} and journeyFare2 is a Child fare.", i));
				else
					Assert.AreEqual(false, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using two pricing units, journeyFare1 should not match with journeyFare2 when i = {0} and journeyFare2 is a Child fare.", i));
			}
			
			// TEST USING THREE PRICING UNITS (OPERATORS)

			// Create a CoachJourneyFare using a size three array of CoachPricingUnitFare, using the
			// first CoachPricingUnitFare from operator1Fares and operator2Fares and operator3Fares
			fares1 = new CoachPricingUnitFare[3] {operator1Fares[0], operator2Fares[0], operator3Fares[0]};
			journeyFare1 = new CoachJourneyFare(fares1);
			journeyFare1.IsAdult = true;

			// Do the same for all the other CoachPricingUnitFare from operator1Fares, 
			// operator2Fares and operator3Fares and try to match against the first CoachJourneyFare. 
			for (int i=1; i<operator1Fares.Length; i++)
			{
				CoachPricingUnitFare[] fares2 = new CoachPricingUnitFare[3] {operator1Fares[i], operator2Fares[i], operator3Fares[i]};
				CoachJourneyFare journeyFare2 = new CoachJourneyFare(fares2);
				journeyFare2.IsAdult = true;
				// Should only match when i = 4 or 5
				if (i==4 || i==5)
					Assert.AreEqual(true, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using three pricing units, journeyFare1 should match with journeyFare2 when i = {0} and both fares are Adult.", i));
				else
					Assert.AreEqual(false, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using three pricing units, journeyFare1 should not match with journeyFare2 when i = {0} and both fares are Adult.", i));
				
				// Change journeyFare2 to child fare and test again
				journeyFare2.IsAdult = false;
				// i = 6 should now match as well
				if (i==4 || i==5 || i==6)
					Assert.AreEqual(true, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using three pricing units, journeyFare1 should match with journeyFare2 when i ={0} and journeyFare2 is a Child fare.", i));
				else
					Assert.AreEqual(false, CoachFareMergePolicy.AreFaresMatching(journeyFare1, journeyFare2), string.Format("Using three pricing units, journeyFare1 should not match with journeyFare2 when i = {0} and journeyFare2 is a Child fare.", i));
			}
		}	
		
		/// <summary>
		/// Tests the static AreTicketsMatching method of the CoachFareMergePolicy class
		/// </summary>
		[Test]
		public void TestAreTicketsMatching()
		{
			Ticket[] tickets1 = new Ticket[8];
			tickets1[0] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 10, float.NaN, float.NaN, float.NaN, 0, 0);
			tickets1[1] = new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 10, float.NaN, float.NaN, 0, 0);
			tickets1[2] = new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 10, float.NaN, 0, 0);
			tickets1[3] = new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 10, 0, 0);
			tickets1[4] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 20, float.NaN, float.NaN, float.NaN, 0, 0);
			tickets1[5] = new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 20, float.NaN, float.NaN, 0, 0);
			tickets1[6] = new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 20, float.NaN, 0, 0);
			tickets1[7] = new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 20, 0, 0);

			Ticket[] tickets2 = new Ticket[11];
			tickets2[0] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 10, float.NaN, float.NaN, float.NaN, 0, 0);
			tickets2[1] = new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 10, float.NaN, float.NaN, 0, 0);
			tickets2[2] = new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 10, float.NaN, 0, 0);
			tickets2[3] = new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 10, 0, 0);
			tickets2[4] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 20, float.NaN, float.NaN, float.NaN, 0, 0);
			tickets2[5] = new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 20, float.NaN, float.NaN, 0, 0);
			tickets2[6] = new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 20, float.NaN, 0, 0);
			tickets2[7] = new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 20, 0, 0);
			tickets2[8] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 10, float.NaN, float.NaN, 0, 0);
			tickets2[9] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 10, float.NaN, 0, 0);
			tickets2[10] = new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 10, 0, 0);

			// Test each ticket from first array against each ticket from second array
			for (int i=0; i<tickets1.Length; i++)
			{
				for (int j=0; j<tickets2.Length; j++)
				{
					if ((i==j)||(i==0 && j>7)||(i==4 && j>7))
						Assert.AreEqual(true, CoachFareMergePolicy.AreTicketsMatching(tickets1[i], tickets2[j]), string.Format("Tickets should match: i={0}, j={1}", i, j));
					else
						Assert.AreEqual(false, CoachFareMergePolicy.AreTicketsMatching(tickets1[i], tickets2[j]), string.Format("Tickets should not match: i={0}, j={1}", i, j));
				}
			}
		}

		/// <summary>
		/// Tests the static CanFaresBeCombined method of the CoachFareMergePolicy class
		/// </summary>
		[Test]
		public void TestCanFaresBeCombined()
		{
			CJP.Fare[] operator1Fares = new CJP.Fare[5];
			CJP.Fare[] operator2Fares = new CJP.Fare[5];
				
			// Set up operator1Fares[0]
			operator1Fares[0] = new CJP.Fare();
			operator1Fares[0].fareType = "FARETYPE1"; 
			operator1Fares[0].fareRestrictionType = CJP.FareType.Flexible;
			operator1Fares[0].fare = 1000;
			operator1Fares[0].adult = true;
			operator1Fares[0].discountCardType = "DISCOUNTCARDTYPE1";
			operator1Fares[0].single = true;
			// Create operator2Fares[0] as copy of operator1Fares[0]
			operator2Fares[0] = (CJP.Fare)operator1Fares[0].Clone();
			
			// Set up operator1Fares[1]
			operator1Fares[1] = new CJP.Fare();
			operator1Fares[1].fareType = "FARETYPE2"; 
			operator1Fares[1].fareRestrictionType = CJP.FareType.LimitedFlexibility;
			operator1Fares[1].fare = 1000;
			operator1Fares[1].adult = true;
			operator1Fares[1].discountCardType = "DISCOUNTCARDTYPE2";
			operator1Fares[1].single = true;
			// Create operator2Fares[1] as copy of operator1Fares[1]
			operator2Fares[1] = (CJP.Fare)operator1Fares[1].Clone();
			
			// Set up operator1Fares[2]
			operator1Fares[2] = new CJP.Fare();
			operator1Fares[2].fareType = "FARETYPE3"; 
			operator1Fares[2].fareRestrictionType = CJP.FareType.NotFlexible;
			operator1Fares[2].fare = 1000;
			operator1Fares[2].adult = false;
			operator1Fares[2].discountCardType = string.Empty;
			operator1Fares[2].single = true;
			// Create operator2Fares[2] as copy of operator1Fares[2]
			operator2Fares[2] = (CJP.Fare)operator1Fares[2].Clone();

			// Set up operator1Fares[3]
			operator1Fares[3] = new CJP.Fare();
			operator1Fares[3].fareType = "FARETYPE4"; 
			operator1Fares[3].fareRestrictionType = CJP.FareType.Flexible;
			operator1Fares[3].fare = 1000;
			operator1Fares[3].adult = true;
			operator1Fares[3].discountCardType = string.Empty;
			operator1Fares[3].single = false;
			// Create operator2Fares[3] as copy of operator1Fares[3]
			operator2Fares[3] = (CJP.Fare)operator1Fares[3].Clone();

			// Set up operator1Fares[4]
			operator1Fares[4] = new CJP.Fare();
			operator1Fares[4].fareType = "FARETYPE5"; 
			operator1Fares[4].fareRestrictionType = CJP.FareType.LimitedFlexibility;
			operator1Fares[4].fare = 1000;
			operator1Fares[4].adult = false;
			operator1Fares[4].discountCardType = string.Empty;
			operator1Fares[4].single = true;
			// Create operator2Fares[4] as copy of operator1Fares[4]
			operator2Fares[4] = (CJP.Fare)operator1Fares[4].Clone();

			// Try combining each operator1 fare with each operator2 fare 
			for (int i=0; i<operator1Fares.Length; i++)
			{
				for (int j=0; j<operator2Fares.Length; j++)
				{
					if (i==j)
					{
						if (operator2Fares[j].discountCardType == string.Empty)
						{
							// Fares can be combined
							Assert.AreEqual(true, CoachFareMergePolicy.CanFaresBeCombined(operator1Fares[i], operator2Fares[j]), String.Format("Fare {0} from Operator1 and fare {1} from Operator 2 should be combined", i, j));
						}
						else
						{
							// Fares can not be combined
							// Can only match a discounted fare with an undiscounted fare from 
							// another pricing unit as discount cards are different between operators
							Assert.AreEqual(false, CoachFareMergePolicy.CanFaresBeCombined(operator1Fares[i], operator2Fares[j]), String.Format("Fare {0} from Operator1 and fare {1} from Operator 2 shoukd not be combined", i, j));
						}
					}
					else
					{
						// Fares can not be combined
						Assert.AreEqual(false, CoachFareMergePolicy.CanFaresBeCombined(operator1Fares[i], operator2Fares[j]), String.Format("Fare {0} from Operator1 and fare {1} from Operator 2 shoukd not be combined", i, j));
					}
				}
			}
		}
		
		/// <summary>
		/// Tests the static IsInwardJourneyValid method of the CoachFareMergePolicy class
		/// </summary>
		[Test]
		public void TestIsInwardJourneyValid()
		{
			// Initialise Property service etc. Needed when creating PublicJourneyDetail objects (?).
			SetUp();
			
			// Create test PublicJourney objects.
			PublicJourney outwardJourney = GetTestJourney(2, "START", "END", "NX");
			PublicJourney inwardJourney1 = GetTestJourney(1, "END", "START", "OPERATOR");
			PublicJourney inwardJourney2 = GetTestJourney(2, "START", "END", "OPERATOR");
			PublicJourney inwardJourney3 = GetTestJourney(2, "END", "START", "MB");
			PublicJourney inwardJourney4 = GetTestJourney(2, "END", "START", "NX");
			
			// Test each inward journey against the outward journey. Only number 4 should be valid.			
			Assert.IsFalse(CoachFareMergePolicy.IsInwardJourneyValid(outwardJourney, inwardJourney1), "Inward journey 1 should not be valid");
			Assert.IsFalse(CoachFareMergePolicy.IsInwardJourneyValid(outwardJourney, inwardJourney2), "Inward journey 2 should not be valid");
			Assert.IsFalse(CoachFareMergePolicy.IsInwardJourneyValid(outwardJourney, inwardJourney3), "Inward journey 3 should not be valid");
			Assert.IsTrue(CoachFareMergePolicy.IsInwardJourneyValid(outwardJourney, inwardJourney4), "Inward journey 4 should be valid");
		
		}

		#endregion
		
		#region Non-Test methods

		/// <summary>
		/// Intialisation method sets up relevant services needed for tests.
		/// Deliberately not marked with [Setup] attribute as only needed by TestIsInwardJourneyValid.
		/// </summary>
		private void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			// Initialise property service etc. 
			TDServiceDiscovery.Init(new TestNatExFaresInitialisation("LIVE"));

		}

        /// <summary>
        /// Private non-test helper method.
        /// Creates a PublicJourney object and sets only the properties which
        /// are tested in IsInwardJourneyValid(). 
        /// </summary>
        /// <param name="pricingUnits">Number of pricing units to add to journey</param>
        /// <param name="origin">Origin string</param></param>
        /// <param name="destination">Destination string</param>
        /// <param name="operatorCode">Operator code string</param>
        /// <returns>PublicJourney object</returns>
		private PublicJourney GetTestJourney(int pricingUnits, string origin, string destination, string operatorCode)
		{
			PublicJourney outwardJourney = new PublicJourney();
			// Create journey legs
			PublicJourneyDetail[] details = new PublicJourneyDetail[pricingUnits];
			for (int i=0; i<pricingUnits; i++)
			{
				CJP.FrequencyLeg leg = new CJP.FrequencyLeg();
				leg.board = new CJP.Event();
				leg.board.stop = new CJP.Stop();
				leg.board.stop.name = origin;
				leg.alight = new CJP.Event();
				leg.alight.stop = new CJP.Stop();
				leg.alight.stop.name = destination;
				
				details[i] = PublicJourneyDetail.Create(leg, null);
				details[i].Services = new ServiceDetails[1];
				details[i].Services[0] = new ServiceDetails(operatorCode, "", "", "", "", "", "");
			}

			// Create journey fares
			CJP.PricingUnit[] fares = new CJP.PricingUnit[pricingUnits];
			for (int i=0; i<pricingUnits; i++)
			{
				fares[i] = new CJP.PricingUnit();
				fares[i].legs = new int[1];
				fares[i].legs[0] = i;
			}
		
			outwardJourney.Details = details;
			outwardJourney.Fares = fares;

			return outwardJourney;
		}

		#endregion

	}
}
