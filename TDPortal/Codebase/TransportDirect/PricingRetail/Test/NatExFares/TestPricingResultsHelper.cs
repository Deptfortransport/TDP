//********************************************************************************
//NAME         : TestPricingResultsHelper.cs
//AUTHOR       : James Broome
//DATE CREATED : 08/03/2005
//DESCRIPTION  : Test implementation of PricingResultsHelper class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestPricingResultsHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:34   mturner
//Initial revision.
//
//   Rev 1.0   Mar 23 2005 09:36:56   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.Domain;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
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
		/// Tests the ConvertFlexibility method of the PricingResultsHelper class
		/// </summary>
		[Test]
		public void TestConvertFlexibility()
		{
			PricingResultsHelper helper = new PricingResultsHelper();
			
			// Test FareType.Flexible
			Fare fare1 = new Fare();
			fare1.fareRestrictionType = FareType.Flexible; 
			Assert.AreEqual(Flexibility.FullyFlexible, helper.ConvertFlexibility(fare1), "Error converting FareType.Flexible into Flexibility.FullyFlexible");

			// Test FareType.LimitedFlexibility
			Fare fare2 = new Fare();
			fare2.fareRestrictionType = FareType.LimitedFlexibility; 
			Assert.AreEqual(Flexibility.LimitedFlexibility, helper.ConvertFlexibility(fare2), "Error converting FareType.LimitedFlexibility into Flexibility.LimitedFlexibility");

			// Test FareType.NotFlexible
			Fare fare3 = new Fare();
			fare3.fareRestrictionType = FareType.NotFlexible; 
			Assert.AreEqual(Flexibility.NoFlexibility, helper.ConvertFlexibility(fare3), "Error converting FareType.NotFlexible into Flexibility.NoFlexibility");

		}

		/// <summary>
		/// Test method
		/// Tests the GenerateChildAges method of the PricingResultsHelper class
		/// </summary>
		[Test]
		public void TestGenerateChildAges()
		{
			PricingResultsHelper helper = new PricingResultsHelper();
			// Set up AgeRange strings
			string ageRange1 = string.Empty;
			string ageRange2 = "1";
			string ageRange3 = "4-";
			string ageRange4 = "-4";
			string ageRange5 = "ALSKFJALSDJ";
			string ageRange6 = "3-5";
			string ageRange7 = "10-15";
			string ageRange8 = "3-5-";
			string ageRange9 = "3-5-8";

			int[] defaultAges = new int[2] {helper.DEFAULT_MIN, helper.DEFAULT_MAX} ;
			// Test invalid strings return default values
			Assert.AreEqual(defaultAges, helper.GenerateChildAges(ageRange1), "Error generating child ages with ageRange1");
			Assert.AreEqual(defaultAges, helper.GenerateChildAges(ageRange2), "Error generating child ages with ageRange2");
			Assert.AreEqual(defaultAges, helper.GenerateChildAges(ageRange3), "Error generating child ages with ageRange3");
			Assert.AreEqual(defaultAges, helper.GenerateChildAges(ageRange4), "Error generating child ages with ageRange4");
			Assert.AreEqual(defaultAges, helper.GenerateChildAges(ageRange5), "Error generating child ages with ageRange5");

			int[] validAges1 = new int[2] {3,5} ;
			int[] validAges2 = new int[2] {10,15} ;
			// Test valid strings return correct values
			Assert.AreEqual(validAges1, helper.GenerateChildAges(ageRange6), "Error generating child ages with ageRange6");
			Assert.AreEqual(validAges2, helper.GenerateChildAges(ageRange7), "Error generating child ages with ageRange7");
			Assert.AreEqual(validAges1, helper.GenerateChildAges(ageRange8), "Error generating child ages with ageRange8");
			Assert.AreEqual(validAges1, helper.GenerateChildAges(ageRange9), "Error generating child ages with ageRange9");


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
		/// Test method
		/// Tests the SetConvertedFare method of the PricingResultsHelper class
		/// </summary>
		[Test]
		public void TestSetConvertedFare()
		{
			PricingResultsHelper helper = new PricingResultsHelper();
			Ticket ticket;
			
			// Test that AdultFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetConvertedFare(ticket, true, false, 10);
			Assert.AreEqual(10, ticket.AdultFare, "Error setting AdultFare property");
			Assert.AreEqual(0, ticket.ChildFare, "ChildFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedAdultFare, "DiscountedAdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedChildFare, "DiscountedChildFare property set incorrectly");

			// Test that ChildFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetConvertedFare(ticket, false, false, 10);
			Assert.AreEqual(0, ticket.AdultFare, "AdultFare property set incorrectly");
			Assert.AreEqual(10, ticket.ChildFare, "Error setting AdultFare property");
			Assert.AreEqual(0, ticket.DiscountedAdultFare, "DiscountedAdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.DiscountedChildFare, "DiscountedChildFare property set incorrectly");

			// Test that DiscountedAdultFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetConvertedFare(ticket, true, true, 10);
			Assert.AreEqual(0, ticket.AdultFare, "AdultFare property set incorrectly");
			Assert.AreEqual(0, ticket.ChildFare, "ChildFare property set incorrectly");
			Assert.AreEqual(10, ticket.DiscountedAdultFare, "Error setting DiscountedAdultFare  property");
			Assert.AreEqual(0, ticket.DiscountedChildFare, "DiscountedChildFare property set incorrectly");

			// Test that DiscountedChildFare property is set correctly
			ticket = NewTicket();
			// Call set fare method
			helper.SetConvertedFare(ticket, false, true, 10);
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
