//********************************************************************************
//NAME         : TestPricingResultsHelper.cs
//AUTHOR       : James Broome (moved to CoachFares by Murat Guney)
//DATE CREATED : 08/03/2005
//DESCRIPTION  : Test implementation of PricingResultsHelper class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestPricingResultsHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:22   mturner
//Initial revision.
//
//   Rev 1.1   Oct 22 2005 16:25:22   mguney
//Associated IR
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 22 2005 16:05:28   mguney
//Initial revision.

//***
//   Rev 1.0   Mar 23 2005 09:36:56   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.Domain;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Test class used to test the public methods 
	/// of the PricingResultsHelper class.
	/// </summary>
	[TestFixture]
	public class TestPricingResultsHelper
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestPricingResultsHelper()
		{
		}


		/// <summary>
		/// Test method
		/// Tests the ConvertFare method of the PricingResultsHelper class
		/// </summary>
		[Test]
		public void TestConvertFare()
		{
			PricingResultsHelper helper = new PricingResultsHelper();
			
			Assert.AreEqual(10.00, helper.ConvertFare(1000), "Error with ConvertFare method");
		}		

		/// <summary>
		/// Test method
		/// Tests the SetFare method of the PricingResultsHelper class
		/// </summary>
		[Test]
		public void TestSetFare()
		{
			PricingResultsHelper helper = new PricingResultsHelper();
			Ticket ticket;
			
			// Test that AdultFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetFare(ticket, true, false, 1000);
			Assert.AreEqual(10, ticket.AdultFare, "Error setting AdultFare property");
			Assert.AreEqual(0, ticket.ChildFare, "ChildFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedAdultFare, "DiscountedAdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedChildFare, "DiscountedChildFare property set incorrectly");

			// Test that ChildFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetFare(ticket, false, false, 1000);
			Assert.AreEqual(0, ticket.AdultFare, "AdultFare property set incorrectly");
			Assert.AreEqual(10, ticket.ChildFare, "Error setting AdultFare property");
			Assert.AreEqual(0, ticket.DiscountedAdultFare, "DiscountedAdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedChildFare, "DiscountedChildFare property set incorrectly");

			// Test that DiscountedAdultFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetFare(ticket, true, true, 1000);
			Assert.AreEqual(0, ticket.AdultFare, "AdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.ChildFare, "ChildFare property set incorrectly");
			Assert.AreEqual(10, ticket.DiscountedAdultFare, "Error setting DiscountedAdultFare  property");
			Assert.AreEqual(0, ticket.DiscountedChildFare, "DiscountedChildFare property set incorrectly");

			// Test that DiscountedChildFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetFare(ticket, false, true, 1000);
			Assert.AreEqual(0, ticket.AdultFare, "AdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.ChildFare, "ChildFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedAdultFare, "DiscountedAdultFare property set incorrectly");
			Assert.AreEqual(10, ticket.DiscountedChildFare, "Error setting DiscountedChildFare property");

		}		

		/// <summary>
		/// Creates and returns a new Ticket object 
		/// with Fare values initialised
		/// </summary>
		/// <returns></returns>
		private Ticket NewTicket()
		{
			Ticket ticket = new Ticket();
			ticket.AdultFare = 0;
			ticket.ChildFare = 0;
			ticket.DiscountedAdultFare = 0;
			ticket.DiscountedChildFare = 0;
			return ticket;
		}


	}
}
