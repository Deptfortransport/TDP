//********************************************************************************
//NAME         : TestCoachFareFilterAndMergeHelper.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 22/10/2003
//DESCRIPTION  : Implementation of TestCoachFareFilterAndMergeHelper class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestCoachFareFilterAndMergeHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:20   mturner
//Initial revision.
//
//   Rev 1.3   Mar 06 2007 13:43:48   build
//Automatically merged from branch for stream4358
//
//   Rev 1.2.1.0   Mar 02 2007 11:20:40   asinclair
//Updated to pass an extra parameter into the new PricingResult
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.2   Nov 01 2005 17:24:34   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 22 2005 16:25:30   mguney
//Associated IR
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 22 2005 16:05:30   mguney
//Initial revision.

using System;
using System.Collections;

using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.ServiceDiscovery;
using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for TestCoachFareFilterAndMergeHelper.
	/// </summary>
	[TestFixture]
	public class TestCoachFareFilterAndMergeHelper
	{
		public TestCoachFareFilterAndMergeHelper()
		{
			
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
			CoachFareFilterAndMergeHelper helper = new CoachFareFilterAndMergeHelper();
			for (int i=0; i<tickets1.Length; i++)
			{
				for (int j=0; j<tickets2.Length; j++)
				{

					if ((i==j)||(i==0 && j>7)||(i==4 && j>7))
						Assert.AreEqual(true, helper.AreTicketsMatching(tickets1[i], tickets2[j]), string.Format("Tickets should match: i={0}, j={1}", i, j));
					else
						Assert.AreEqual(false, helper.AreTicketsMatching(tickets1[i], tickets2[j]), string.Format("Tickets should not match: i={0}, j={1}", i, j));
				}
			}
		}

		/// <summary>
		/// Test method tests the CombinePricingResultTickets method of the NatExFaresSupplier class.
		/// </summary>
		public void TestCombinePricingResultTickets()
		{
			PricingResult result = new PricingResult(3,15, false, false, false);
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

			result = new CoachFareFilterAndMergeHelper().CombinePricingResultTickets(result);

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
			
			//empty pricing result
			PricingResult emptyResult = 
				new CoachFareFilterAndMergeHelper().CombinePricingResultTickets(new PricingResult(0,0, false, false, false));
			Assert.AreEqual(0,emptyResult.Tickets.Count,"Empty pricing result has tickets.");

		}

	}

	
}
