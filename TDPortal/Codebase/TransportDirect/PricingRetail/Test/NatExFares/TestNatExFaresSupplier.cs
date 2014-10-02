//********************************************************************************
//NAME         : TestNatExFaresSupplier.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 23/10/2003
//DESCRIPTION  : Implementation of TestNatExFaresSupplier class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestNatExFaresSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:32   mturner
//Initial revision.
//
//   Rev 1.13   Apr 28 2005 18:05:34   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.12   Apr 28 2005 16:03:54   RPhilpott
//Add "NoPlacesAvailable" property to PricingResult to indicate that valid fares have been found, but with no seat availability.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.11   Mar 30 2005 17:06:56   jgeorge
//Slight modification to ticket names for NatEx tickets
//
//   Rev 1.10   Mar 30 2005 16:06:16   jbroome
//Added TestCombinePricingResultTickets test method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.9   Mar 23 2005 09:47:06   jbroome
//Updated initialisation method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.8   Feb 07 2005 16:36:58   RScott
//Assertion changed to Assert
//
//   Rev 1.7   Jun 11 2004 15:37:00   acaunt
//Now uses TicketNameRule to generate appropriate SCL or NatEx ticket name.
//
//   Rev 1.6   May 28 2004 13:53:26   acaunt
//Updated to accomodate real NatEx and SCL data and with revised display rules.
//
//   Rev 1.5   May 20 2004 14:16:54   acaunt
//Test harness updated to test the additional merging of journey legs if indicates so by the CJP data
//
//   Rev 1.4   Nov 24 2003 16:37:44   acaunt
//Tests test the number of tickets returned before continuing with other assertions.
//
//   Rev 1.3   Nov 24 2003 13:33:24   CHosegood
//Replaced discount cards with the approved values.
//
//   Rev 1.2   Oct 27 2003 18:23:50   acaunt
//Modified after chaning test data
//
//   Rev 1.1   Oct 26 2003 15:52:26   acaunt
//tests added
//
//   Rev 1.0   Oct 23 2003 10:39:28   acaunt
//Initial Revision
using System;
using NUnit.Framework;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using CJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Test harness for NatExFaresSupplier
	/// </summary>
	[TestFixture]
	public class TestNatExFaresSupplier
	{

		#region Mock Pricing Unit
		/// <summary>
		///  A class to dummy up some of the behaviour of a PricingUnit so that we can examine the Price method of NatExFares 
		///  supplier
		/// </summary>
		public class MockPricingUnit: PricingUnit
		{
			/// <summary>
			///  In the constructor we initialise the base class we an arbitrary coach journey detail
			///  and then every ride fields as required to get the appropriate behaviour
			/// </summary>
			public MockPricingUnit(bool partOfMatchingReturn) : base(TestSampleJourneyData.NatExPJDDovVic,new PriceSupplierFactory())
			{
				// If we want a PricingUnit to look as if it is part of a matching return
				// we ensure that it has inbound leg information
				if (partOfMatchingReturn)
				{
					base.AddReturnUnit(this);
				}
			}
		}
		# endregion

		#region Private members
		
		public static int DISCOUNT_CARDS = 9; // Value determine from the data in data services
		public static string NO_DISCOUNT = string.Empty;
		public static string STUDENT_DISCOUNT = "National Express Student Coachcard";
		public static string CHILD_DISCOUNT = "Child Card";
		public static string ELDERLY_DISCOUNT = "Advantage50 Coachcard";

		NatExFaresSupplier supplier;
		UnprocessedFareData data;
		Hashtable singleResults;
		Hashtable returnResults;	
	
		// Fares information for comparison
		CJP.Fare adultSing;
		CJP.Fare childSing;
		CJP.Fare adultRet;
		CJP.Fare childRet;
		CJP.Fare adultDis1;
		CJP.Fare adultDis2;

		#endregion

		#region Constructor and Initialisation

		public TestNatExFaresSupplier()
		{		
		}

		[SetUp]
		public void Init()
		{		
			// Initialise property service etc.
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());

			supplier = new NatExFaresSupplier(new NatExTicketNameRule());
			data = new UnprocessedFareData(TestSampleJourneyData.NatExFaresDovNot,
				TestSampleJourneyData.NatExFaresDovNot.legs);
			supplier.PreProcess(data);
			singleResults = supplier.SingleResults;
			returnResults = supplier.ReturnResults;

			// Fares information for comparison
			adultSing = TestSampleJourneyData.NatExFaresDovNot.prices[0];
			childSing = TestSampleJourneyData.NatExFaresDovNot.prices[1];
			adultRet = TestSampleJourneyData.NatExFaresDovNot.prices[2];
			childRet = TestSampleJourneyData.NatExFaresDovNot.prices[3];
			adultDis1 = TestSampleJourneyData.NatExFaresDovNot.prices[4];
			adultDis2 = TestSampleJourneyData.NatExFaresDovNot.prices[5];
		}

		[TearDown]
		public void CleanUp()
		{
		}
		
		#endregion

		#region Test methods

		/// <summary>
		/// Test that we have obtained a PricingResult for each of the discount cards that we have configured.
		/// </summary>
		[Test]
		public void TestPreprocessingDiscountCards()
		{

			// Assert that we have the correct number of Single and Return PricingResults
			Assert.AreEqual(DISCOUNT_CARDS, singleResults.Count, "Wrong number of single PricingResults");
			Assert.AreEqual(DISCOUNT_CARDS, returnResults.Count, "Wrong number of return PricingResults");
			// Confirm the values of the different discount cards
			Assert.IsTrue(singleResults.ContainsKey(NO_DISCOUNT), "No discount card not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("National Express Student Coachcard"), "National Express Student Coachcard not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("National Express Advantage50 Coachcard"), "National Express Advantage50 Coachcard not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("National Express Young Person Coachcard"), "National Express Young Person Coachcard not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("Family Saver Coachcard"), "Family Saver Coachcard not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("Citylink 50+ Card"), "Citylink 50+ Discount Card not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("Citylink Student Card"), "Citylink Student Card not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("Young Scot Cardholder"), "Young Scot Cardholder not present in set of single discount cards");
			Assert.IsTrue(singleResults.ContainsKey("Citylink Young Person Card"), "Citylink Young Persons Discount Card not present in set of single discount cards");
		}

		/// <summary>
		/// Test that we have the corret number of single fares for each discount card.
		/// The actual values of the fares has been tested in TestPricingResuiltsBuilder
		/// </summary>
		[Test]
		public void TestPreprocessingSingleFares()
		{
			// Confirm that we have the correct number of tickets in each
			foreach(string card in singleResults.Keys)
			{
				PricingResult result = (PricingResult)singleResults[card];
				Assert.AreEqual(2, result.Tickets.Count, "Wrong number of single tickets for card "+card);
			}
		}

		/// <summary>
		/// Test that we have the corret number of return fares for each discount card.
		/// The actual values of the fares has been tested in TestPricingResuiltsBuilder
		/// </summary>
		[Test]
		public void TestPreprocessingReturnFares()
		{
			// Confirm that we have the correct number of tickets in each
			foreach(string card in returnResults.Keys)
			{
				PricingResult result = (PricingResult)returnResults[card];
				if (card.Equals(STUDENT_DISCOUNT))
				{
					Assert.AreEqual(8, result.Tickets.Count, "Wrong number of return tickets for card "+card);
				}
				else if (card.Equals(ELDERLY_DISCOUNT))
				{
					Assert.AreEqual(7, result.Tickets.Count, "Wrong number of return tickets for card "+card);
				}
					// Note, that this card isn't in our list of discounts and so there shouldn't be any additional tickets
				else if (card.Equals(CHILD_DISCOUNT))
				{
					Assert.AreEqual(6, result.Tickets.Count, "Wrong number of return tickets for card "+card);
				}
				else
				{
					Assert.AreEqual(6, result.Tickets.Count, "Wrong number of return tickets for card "+card);
				}

			}
		}

		/// <summary>
		/// Check that when we have a matching return non-flexible fares are included in the PricingResult
		/// </summary>
		[Test]
		public void TestPricingMatchingReturn()
		{
			PricingUnit pu = new MockPricingUnit(true);
			pu.AddUnprocessedFareData(data);
			Discounts discounts = new Discounts("", "", TicketClass.All);
			pu.Price(discounts);
			Assert.AreEqual(4, pu.ReturnFares.Tickets.Count, "Incorrect number of fares returned for matching return journey ");
		}

		/// <summary>
		/// Check that when we have a non matching return non-flexible fares are excluded from the PricingResult
		/// </summary>
		[Test]
		public void TestPricingNonMatchingReturn()
		{
			PricingUnit pu = new MockPricingUnit(false);
			pu.AddUnprocessedFareData(data);
			Discounts discounts = new Discounts("", "", TicketClass.All);
			pu.Price(discounts);
			Assert.AreEqual(3, pu.ReturnFares.Tickets.Count, "Incorrect number of fares returned for non-matching return journey ");
		}

		/// <summary>
		/// Check that with a discount card, the appropriate additional fares are obtains
		/// </summary>
		[Test]
		public void TestPricingDiscountCard()
		{
			PricingUnit pu = new MockPricingUnit(true);
			pu.AddUnprocessedFareData(data);
			Discounts discounts = new Discounts("", "National Express Student Coachcard", TicketClass.All);
			pu.Price(discounts);
			Assert.AreEqual(3, pu.ReturnFares.Tickets.Count,"Incorrect number of fares returned for discounted return journey ");
		}

		[Test]
		public void TestPricingUnrecognisedDiscountCard()
		{
			PricingUnit pu = new MockPricingUnit(true);
			pu.AddUnprocessedFareData(data);
			Discounts discounts = new Discounts("", "", TicketClass.All);
			pu.Price(discounts);
			Assert.AreEqual(4, pu.ReturnFares.Tickets.Count, "Incorrect number of fares returned for return journey with unrecognised discount card");
		}

		[Test]
		public void TestPricingNoFaresInformation()
		{
			PricingUnit pu = new MockPricingUnit(true);
			Discounts discounts = new Discounts("", "", TicketClass.All);
			pu.Price(discounts);
			Assert.AreEqual(0, pu.ReturnFares.Tickets.Count, "Incorrect number of fares returned when journey has no fares information");

		}

		/// <summary>
		/// Test method tests the CombinePricingResultTickets method of the NatExFaresSupplier class.
		/// </summary>
		public void TestCombinePricingResultTickets()
		{
			PricingResult result = new PricingResult(3,15, false, false);
			ArrayList tickets = new ArrayList();			
			// Create Ticket objects
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 10, float.NaN, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 10, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 10, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 10, 0, 0));
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 20, float.NaN, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE6", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 20, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 20, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 20, 0, 0));
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 10, float.NaN, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 10, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 10, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 10, 0, 0));
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE0", 20, float.NaN, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE6", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 20, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE2", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 20, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE3", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, 20, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, 10, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE2", float.NaN, float.NaN, 10, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE0", Flexibility.FullyFlexible, "SHORTCODE3", float.NaN, float.NaN, float.NaN, 10, 0, 0));
			tickets.Add( new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", 20, float.NaN, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE1", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, float.NaN, float.NaN, 30, 0, 0));
			tickets.Add( new Ticket("CODE6", Flexibility.FullyFlexible, "SHORTCODE1", 50, float.NaN, float.NaN, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE6", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, float.NaN, 30, float.NaN, 0, 0));
			tickets.Add( new Ticket("CODE6", Flexibility.FullyFlexible, "SHORTCODE1", float.NaN, float.NaN, float.NaN, 40, 0, 0));
	
			result.Tickets = tickets;

			Assert.AreEqual(24, result.Tickets.Count, "Incorrect number of Tickets in collection before call to CombinePricingResultTickets()");

			PricingResult processedResult = supplier.CombinePricingResultTickets(result);

			Assert.AreEqual(8, result.Tickets.Count, "Incorrect number of Tickets in collection after call to CombinePricingResultTickets()");

			// Convert ArrayList to array and check properties of each Ticket
			Ticket[] combinedTickets = new Ticket[8];
			result.Tickets.CopyTo(combinedTickets,0);

			Assert.AreEqual("CODE0", combinedTickets[0].Code, "Code property of first Ticket");
			Assert.AreEqual(10, combinedTickets[0].AdultFare, "AdultFare property of first Ticket");
			Assert.AreEqual(10, combinedTickets[0].ChildFare, "ChildFare property of first Ticket");
			Assert.AreEqual(10, combinedTickets[0].DiscountedAdultFare, "DiscountedAdultFare property of first Ticket");
			Assert.AreEqual(10, combinedTickets[0].DiscountedChildFare, "DiscountedChildFare property of first Ticket");

			Assert.AreEqual("CODE1", combinedTickets[1].Code, "Code property of second Ticket");
			Assert.AreEqual(20, combinedTickets[1].AdultFare, "AdultFare property of second Ticket");
			Assert.AreEqual(10, combinedTickets[1].ChildFare, "ChildFare property of second Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[1].DiscountedAdultFare, "DiscountedAdultFare property of second Ticket");
			Assert.AreEqual(30, combinedTickets[1].DiscountedChildFare, "DiscountedChildFare property of second Ticket");

			Assert.AreEqual("CODE2", combinedTickets[2].Code, "Code property of third Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[2].AdultFare, "AdultFare property of third Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[2].ChildFare, "ChildFare property of third Ticket");
			Assert.AreEqual(10, combinedTickets[2].DiscountedAdultFare, "DiscountedAdultFare property of third Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[2].DiscountedChildFare, "DiscountedChildFare property of third Ticket");

			Assert.AreEqual("CODE3", combinedTickets[3].Code, "Code property of fourth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[3].AdultFare, "AdultFare property of fourth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[3].ChildFare, "ChildFare property of fourth Ticket");
			Assert.AreEqual(20, combinedTickets[3].DiscountedAdultFare, "DiscountedAdultFare property of fourth Ticket");
			Assert.AreEqual(10, combinedTickets[3].DiscountedChildFare, "DiscountedChildFare property of fourth Ticket");

			Assert.AreEqual("CODE0", combinedTickets[4].Code, "Code property of fifth Ticket");
			Assert.AreEqual(20, combinedTickets[4].AdultFare, "AdultFare property of fifth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[4].ChildFare, "ChildFare property of fifth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[4].DiscountedAdultFare, "DiscountedAdultFare property of fifth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[4].DiscountedChildFare, "DiscountedChildFare property of fifth Ticket");

			Assert.AreEqual("CODE6", combinedTickets[5].Code, "Code property of sixth Ticket");
			Assert.AreEqual(50, combinedTickets[5].AdultFare, "AdultFare property of sixth Ticket");
			Assert.AreEqual(20, combinedTickets[5].ChildFare, "ChildFare property of sixth Ticket");
			Assert.AreEqual(30, combinedTickets[5].DiscountedAdultFare, "DiscountedAdultFare property of sixth Ticket");
			Assert.AreEqual(40, combinedTickets[5].DiscountedChildFare, "DiscountedChildFare property of sixth Ticket");

			Assert.AreEqual("CODE2", combinedTickets[6].Code, "Code property of seventh Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[6].AdultFare, "AdultFare property of seventh Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[6].ChildFare, "ChildFare property of seventh Ticket");
			Assert.AreEqual(20, combinedTickets[6].DiscountedAdultFare, "DiscountedAdultFare property of seventh Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[6].DiscountedChildFare, "DiscountedChildFare property of seventh Ticket");

			Assert.AreEqual("CODE3", combinedTickets[7].Code, "Code property of eighth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[7].AdultFare, "AdultFare property of eighth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[7].ChildFare, "ChildFare property of eighth Ticket");
			Assert.AreEqual(float.NaN, combinedTickets[7].DiscountedAdultFare, "DiscountedAdultFare property of eighth Ticket");
			Assert.AreEqual(20, combinedTickets[7].DiscountedChildFare, "DiscountedChildFare property of eighth Ticket");


			
		}
		#endregion

	}
}
