// ******************************************************************* 
// NAME			: TestTravelDatesResultSet.cs
// AUTHOR		: 
// DATE CREATED	: 07/01/2005
// DESCRIPTION	: Implementation of the TestTravelDatesResultSet class
// ******************************************************************* 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestTravelDatesResultSet.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:30   mturner
//Initial revision.
//
//   Rev 1.12   Jan 03 2006 14:14:20   mguney
//TestCombineTickets method changed to handle the changes made on the TravelDate's CombineTickets method.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.11   Nov 30 2005 09:38:54   RPhilpott
//Simplification of multi-leg ticket combination
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.10   Nov 09 2005 12:31:44   build
//Automatically merged from branch for stream2818
//
//   Rev 1.9.1.0   Nov 08 2005 16:54:56   RPhilpott
//Add tests for noew TravelDate.CombineTickets() method.
//Resolution for 2818: DEL 8 Stream: Search by Price

using System;
using System.Collections;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TestTravelDatesResultSet.
	/// </summary>
	[TestFixture]
	public class TestTravelDatesResultSet
	{
		private TravelDatesResultSet classToTest;
	
		#region mock CostSearchTickets
		private static CostSearchTicket[] singleCoachTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("NX Standard",Flexibility.FullyFlexible,"NXS",50.00f,10.00f, float.NaN, float.NaN, 25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("NX Business",Flexibility.LimitedFlexibility,"NXB",200.00f,50.00f, float.NaN, float.NaN, 150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX Cheap day single",Flexibility.LimitedFlexibility,"NXC",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX first",Flexibility.FullyFlexible,"NXF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High)
			};
		private static CostSearchTicket[] returnCoachTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("NX Standard Return",Flexibility.FullyFlexible,"NXSR",50.00f,10.00f, float.NaN, float.NaN, 25.00f,7.00f,0,16, Probability.Low),
				new CostSearchTicket("NX Business Return",Flexibility.LimitedFlexibility,"NXBR",200.00f,50.00f, float.NaN, float.NaN, 150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX Cheap day return",Flexibility.LimitedFlexibility,"NXCR",5.00f, 1.00f,  float.NaN, float.NaN, 2.00f,1.00f,0,16, Probability.Medium),
				new CostSearchTicket("NX first return",Flexibility.FullyFlexible,"NXR",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};	
		private static CostSearchTicket[] singleRailTickets = new CostSearchTicket[]
			{		
				new CostSearchTicket("Apex Single /any rail",Flexibility.FullyFlexible,"Apex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("7 day advance / Virgin",Flexibility.NoFlexibility,"7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Cheap day single /any rail",Flexibility.LimitedFlexibility,"CDS",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Standard first /any rail",Flexibility.FullyFlexible,"STF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};		
		private static CostSearchTicket[] returnRailTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("Apex Return /any rail",Flexibility.NoFlexibility,"Apex",50.00f,10.00f, float.NaN, float.NaN, 25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("7 day advance return/ Virgin",Flexibility.NoFlexibility,"7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Cheap day return /any rail",Flexibility.LimitedFlexibility,"CDR",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Standard return /any rail",Flexibility.FullyFlexible,"SR",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High)
			};
		private static CostSearchTicket[] singleAirTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("BA Economy",Flexibility.NoFlexibility,"Econ",50.00f,10.00f, float.NaN, float.NaN, 25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("BA Business",Flexibility.LimitedFlexibility,"Busi",200.00f, float.NaN, float.NaN, 10.00f,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA First Class",Flexibility.FullyFlexible,"First",1000.00f,500.00f, float.NaN, float.NaN, 400.00f,250.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA Cattle Class",Flexibility.LimitedFlexibility,"Cattle",0.99f,0.49f, float.NaN, float.NaN, 0.79f,0.09f,0,16,Probability.High)
			};
		private static CostSearchTicket[] returnAirTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("BA Economy Return",Flexibility.NoFlexibility,"Econ",50.00f,10.00f, float.NaN, float.NaN, 25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("BA Business Return",Flexibility.NoFlexibility,"Busi",200.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA First Class Return",Flexibility.NoFlexibility,"First",1000.00f,500.00f, float.NaN, float.NaN, 400.00f,250.00f,0,16,Probability.Medium),
				new CostSearchTicket("Easyjet Cattle Class Return",Flexibility.NoFlexibility,"Cattle",0.99f,0.49f, float.NaN, float.NaN, 0.79f,0.09f,0,16,Probability.High)
			};		
		private static CostSearchTicket[] maxMinTickets 
		{
			get 
			{
				CostSearchTicket[] tickets = new CostSearchTicket[10];
				tickets[0] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 50.00f, 10.00f, float.NaN, float.NaN, 25.00f, 7.00f, 0,16,Probability.High);
				tickets[1] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 60.00f, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, 0,16,Probability.Low);
				tickets[2] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 50.00f, 10.00f, float.NaN, float.NaN, 25.00f, 7.00f, 0,16,Probability.Low);
				tickets[2].CombinedTicketIndex = 1;
				tickets[3] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 50.00f, 10.00f, float.NaN, float.NaN, 25.00f, 7.00f, 0,16,Probability.Low);
				tickets[3].CombinedTicketIndex = 1;
				tickets[4] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 50.00f, 5.00f, float.NaN, float.NaN, 30.00f, 8.00f, 0,16,Probability.Low);
				tickets[5] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 50.00f, 10.00f, float.NaN, float.NaN, 5.00f, 7.00f, 0,16,Probability.Low);
				tickets[5].CombinedTicketIndex = 2;
				tickets[6] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 10.00f, 10.00f, float.NaN, float.NaN, 10.00f, 7.00f, 0,16,Probability.Low);
				tickets[6].CombinedTicketIndex = 2;
				tickets[7] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 20.00f, 10.00f, float.NaN, float.NaN, 7.00f, 7.00f, 0,16,Probability.Low);
				tickets[7].CombinedTicketIndex = 2;
				tickets[8] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 30.00f, 10.00f, float.NaN, float.NaN, 20.00f, 5.00f, 0,16,Probability.Medium);
				tickets[8].CombinedTicketIndex = 3;
				tickets[9] = new CostSearchTicket("CODE",Flexibility.NoFlexibility,"CODE", 40.00f, 40.00f, float.NaN, float.NaN, 25.00f, 5.00f, 0,16,Probability.Medium);
				tickets[9].CombinedTicketIndex = 3;
				return tickets;
			} 
		}

		private ArrayList GetTicketsAllIndexZero()
		{
			ArrayList tickets = new ArrayList();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Standard",Flexibility.FullyFlexible, "NXS", 50.00f, 10.00f, float.NaN, float.NaN, 25.00f,7.00f,0,16,Probability.High);
			cst.CombinedTicketIndex = 0;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Standard",Flexibility.LimitedFlexibility, "NXS", 200.00f, 50.00f, float.NaN, float.NaN, 150.00f,25.00f,0,16,Probability.Medium);
			cst.CombinedTicketIndex = 0;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Standard",Flexibility.LimitedFlexibility, "NXS", 5.00f, 1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium);
			cst.CombinedTicketIndex = 0;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Standard",Flexibility.FullyFlexible, "NXS", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 0;
			cst.LegNumber = 0;
			tickets.Add(cst);

			return tickets;

		}

		private ArrayList GetTicketsTwoOrphans()
		{
			ArrayList tickets = GetTicketsAllIndexZero();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Leg One",Flexibility.NoFlexibility, "NX1", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two",Flexibility.NoFlexibility, "NX2", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			return tickets;

		}

		private ArrayList GetTicketsFourMatchedPairs()
		{
			ArrayList tickets = GetTicketsAllIndexZero();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Leg One",Flexibility.LimitedFlexibility, "NX1", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two",Flexibility.FullyFlexible, "NX2", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg One",Flexibility.FullyFlexible, "NX3", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 2;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two",Flexibility.LimitedFlexibility, "NX4", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 2;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg One",Flexibility.NoFlexibility, "NX5", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 3;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two",Flexibility.LimitedFlexibility, "NX6", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 3;
			cst.LegNumber = 1;
			tickets.Add(cst);
			
			cst = new CostSearchTicket("NX Leg One",Flexibility.LimitedFlexibility, "NX7", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 4;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two",Flexibility.NoFlexibility, "NX8", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 4;
			cst.LegNumber = 1;
			tickets.Add(cst);

			return tickets;
		}

		private ArrayList GetTicketsOneMatchedPairFullFull()
		{
			ArrayList tickets = GetTicketsAllIndexZero();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Leg One", Flexibility.FullyFlexible, "NX1", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.NoFlexibility, "NX2", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.LimitedFlexibility, "NX3", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.FullyFlexible, "NX4", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			return tickets;
		}

		private ArrayList GetTicketsOneMatchedPairLtdLtd()
		{
			ArrayList tickets = GetTicketsAllIndexZero();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Leg One", Flexibility.LimitedFlexibility, "NX1", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.NoFlexibility, "NX2", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.LimitedFlexibility, "NX3", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.FullyFlexible, "NX4", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			return tickets;
		}

		private ArrayList GetTicketsOneMatchedPairNoneNone()
		{
			ArrayList tickets = GetTicketsAllIndexZero();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Leg One", Flexibility.NoFlexibility, "NX1", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.NoFlexibility, "NX2", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.LimitedFlexibility, "NX3", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.FullyFlexible, "NX4", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			return tickets;
		}

		private ArrayList GetTicketsThreeMatchedPairs()
		{
			ArrayList tickets = GetTicketsAllIndexZero();
			CostSearchTicket cst = null;

			cst = new CostSearchTicket("NX Leg One", Flexibility.FullyFlexible, "NX1", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg One", Flexibility.LimitedFlexibility, "NX2", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg One", Flexibility.NoFlexibility, "NX3", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 0;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.NoFlexibility, "NX4", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.LimitedFlexibility, "NX5", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			cst = new CostSearchTicket("NX Leg Two", Flexibility.FullyFlexible, "NX6", 40.00f, 20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16, Probability.High);
			cst.CombinedTicketIndex = 1;
			cst.LegNumber = 1;
			tickets.Add(cst);

			return tickets;
		}

		#endregion

		public TestTravelDatesResultSet()
		{
			//instance of class to test
			classToTest = new TravelDatesResultSet();				  			
		}

		[TestFixtureSetUp]
		public void Init()
		{					
			//add some test tickets 
			AddTickets();	

			//set public property to true
			classToTest.ContainsSinglesTickets = true;
		}

		[TestFixtureTearDown]
		public void CleanUp()
		{
			//remove test tickets
			RemoveTickets();				
		}

		/// <summary>
		/// The test only checks that the public property returns the correct value
		/// </summary>
		/// <returns></returns>
		[Test]
		public void ContainsSinglesTickets()
		{
			//ContainsSinglesTickets should return true as the property has been set in the Init method
			bool result = classToTest.ContainsSinglesTickets;
			Assert.IsTrue(result, "No singles tickets found when expected");

			//removeTickets resets the boolean ContainsSinglesTickets property to false 
			RemoveTickets();
			result = classToTest.ContainsSinglesTickets;
			Assert.IsFalse(result, "Singles tickets still found in results set");
		}

		/// <summary>
		/// Tests the UpdateMaxMinFares method of the TravelDate class
		/// </summary>
		[Test]
		public void TestUpdateMaxMinFares()
		{
			CostSearchTicket[] tickets = maxMinTickets;

			// #1 TicketType = Single
			TravelDate travelDateSingle = new TravelDate();
			travelDateSingle.TicketType = TicketType.Single;
			
			// Call UpdateMaxMinFares method - no tickets in collection
			travelDateSingle.UpdateMaxMinFares();
			Assert.AreEqual(float.NaN, travelDateSingle.MaxAdultFare, "UpdateMaxMinFares has incorrectly updated MaxAdultFare for Single travel date");
			Assert.AreEqual(float.NaN, travelDateSingle.MinAdultFare, "UpdateMaxMinFares has incorrectly updated MinAdultFare for Single travel date");
			Assert.AreEqual(float.NaN, travelDateSingle.MaxChildFare, "UpdateMaxMinFares has incorrectly updated MaxChildFare for Single travel date");
			Assert.AreEqual(float.NaN, travelDateSingle.MinChildFare, "UpdateMaxMinFares has incorrectly updated MinChildFare for Single travel date");
			Assert.AreEqual(float.NaN, travelDateSingle.LowestProbableAdultFare, "UpdateMaxMinFares has incorrectly updated LowestProbableAdultFare for Single travel date");
			Assert.AreEqual(float.NaN, travelDateSingle.LowestProbableChildFare, "UpdateMaxMinFares has incorrectly updated LowestProbableChildFare for Single travel date");

			// Add CostSearchTickets to collection
			travelDateSingle.OutwardTickets = tickets;

			// Call UpdateMaxMinFares method
			travelDateSingle.UpdateMaxMinFares();
			Assert.AreEqual(100, travelDateSingle.MaxAdultFare, "Incorrect value for MaxAdultFare for Single travel date");
			Assert.AreEqual(22, travelDateSingle.MinAdultFare, "Incorrect value for MinAdultFare for Single travel date");
			Assert.AreEqual(50, travelDateSingle.MaxChildFare, "Incorrect value for MaxChildFare for Single travel date");
			Assert.AreEqual(5, travelDateSingle.MinChildFare, "Incorrect value for MinChildFare for Single travel date");
			Assert.AreEqual(25, travelDateSingle.LowestProbableAdultFare, "Incorrect value for LowestProbableAdultFare for Single travel date");
			Assert.AreEqual(7, travelDateSingle.LowestProbableChildFare, "Incorrect value for LowestProbableChildFare for Single travel date");

			// #2 TicketType = OpenReturn
			TravelDate travelDateOpenReturn = new TravelDate();
			travelDateOpenReturn.TicketType = TicketType.OpenReturn;
			
			// Call UpdateMaxMinFares method - no tickets in collection
			travelDateOpenReturn.UpdateMaxMinFares();
			Assert.AreEqual(float.NaN, travelDateOpenReturn.MaxAdultFare, "UpdateMaxMinFares has incorrectly updated MaxAdultFare for Open Return travel date");
			Assert.AreEqual(float.NaN, travelDateOpenReturn.MinAdultFare, "UpdateMaxMinFares has incorrectly updated MinAdultFare for Open Return travel date");
			Assert.AreEqual(float.NaN, travelDateOpenReturn.MaxChildFare, "UpdateMaxMinFares has incorrectly updated MaxChildFare for Open Return travel date");
			Assert.AreEqual(float.NaN, travelDateOpenReturn.MinChildFare, "UpdateMaxMinFares has incorrectly updated MinChildFare for Open Return travel date");
			Assert.AreEqual(float.NaN, travelDateOpenReturn.LowestProbableAdultFare, "UpdateMaxMinFares has incorrectly updated LowestProbableAdultFare for Open Return travel date");
			Assert.AreEqual(float.NaN, travelDateOpenReturn.LowestProbableChildFare, "UpdateMaxMinFares has incorrectly updated LowestProbableChildFare for Open Return travel date");

			// Add CostSearchTickets to collection
			travelDateOpenReturn.OutwardTickets = tickets;

			// Call UpdateMaxMinFares method
			travelDateOpenReturn.UpdateMaxMinFares();
			Assert.AreEqual(100, travelDateOpenReturn.MaxAdultFare, "Incorrect value for MaxAdultFare for Open Return travel date");
			Assert.AreEqual(22, travelDateOpenReturn.MinAdultFare, "Incorrect value for MinAdultFare for Open Return travel date");
			Assert.AreEqual(50, travelDateOpenReturn.MaxChildFare, "Incorrect value for MaxChildFare for Open Return travel date");
			Assert.AreEqual(5, travelDateOpenReturn.MinChildFare, "Incorrect value for MinChildFare for Open Return travel date");
			Assert.AreEqual(25, travelDateOpenReturn.LowestProbableAdultFare, "Incorrect value for LowestProbableAdultFare for Open Return travel date");
			Assert.AreEqual(7, travelDateOpenReturn.LowestProbableChildFare, "Incorrect value for LowestProbableChildFare for Open Return travel date");

			// #3 TicketType = Return
			TravelDate travelDateReturn = new TravelDate();
			travelDateReturn.TicketType = TicketType.Return;
			
			// Call UpdateMaxMinFares method - no tickets in collection
			travelDateReturn.UpdateMaxMinFares();
			Assert.AreEqual(float.NaN, travelDateReturn.MaxAdultFare, "UpdateMaxMinFares has incorrectly updated MaxAdultFare for Return travel date");
			Assert.AreEqual(float.NaN, travelDateReturn.MinAdultFare, "UpdateMaxMinFares has incorrectly updated MinAdultFare for Return travel date");
			Assert.AreEqual(float.NaN, travelDateReturn.MaxChildFare, "UpdateMaxMinFares has incorrectly updated MaxChildFare for Return travel date");
			Assert.AreEqual(float.NaN, travelDateReturn.MinChildFare, "UpdateMaxMinFares has incorrectly updated MinChildFare for Return travel date");
			Assert.AreEqual(float.NaN, travelDateReturn.LowestProbableAdultFare, "UpdateMaxMinFares has incorrectly updated LowestProbableAdultFare for Return travel date");
			Assert.AreEqual(float.NaN, travelDateReturn.LowestProbableChildFare, "UpdateMaxMinFares has incorrectly updated LowestProbableChildFare for Return travel date");

			// Add CostSearchTickets to collection
			travelDateReturn.ReturnTickets = tickets;

			// Call UpdateMaxMinFares method
			travelDateReturn.UpdateMaxMinFares();
			Assert.AreEqual(100, travelDateReturn.MaxAdultFare, "Incorrect value for MaxAdultFare for Return travel date");
			Assert.AreEqual(22, travelDateReturn.MinAdultFare, "Incorrect value for MinAdultFare for Return travel date");
			Assert.AreEqual(50, travelDateReturn.MaxChildFare, "Incorrect value for MaxChildFare for Return travel date");
			Assert.AreEqual(5, travelDateReturn.MinChildFare, "Incorrect value for MinChildFare for Return travel date");
			Assert.AreEqual(25, travelDateReturn.LowestProbableAdultFare, "Incorrect value for LowestProbableAdultFare for Return travel date");
			Assert.AreEqual(7, travelDateReturn.LowestProbableChildFare, "Incorrect value for LowestProbableChildFare for Return travel date");

			// #4 TicketType = Singles
			TravelDate travelDateSingles = new TravelDate();
			travelDateSingles.TicketType = TicketType.Singles;
			
			// Call UpdateMaxMinFares method - no tickets in collection
			travelDateSingles.UpdateMaxMinFares();
			Assert.AreEqual(float.NaN, travelDateSingles.MaxAdultFare, "UpdateMaxMinFares has incorrectly updated MaxAdultFare for Singles travel date");
			Assert.AreEqual(float.NaN, travelDateSingles.MinAdultFare, "UpdateMaxMinFares has incorrectly updated MinAdultFare for Singles travel date");
			Assert.AreEqual(float.NaN, travelDateSingles.MaxChildFare, "UpdateMaxMinFares has incorrectly updated MaxChildFare for Singles travel date");
			Assert.AreEqual(float.NaN, travelDateSingles.MinChildFare, "UpdateMaxMinFares has incorrectly updated MinChildFare for Singles travel date");
			Assert.AreEqual(float.NaN, travelDateSingles.LowestProbableAdultFare, "UpdateMaxMinFares has incorrectly updated LowestProbableAdultFare for Singles travel date");
			Assert.AreEqual(float.NaN, travelDateSingles.LowestProbableChildFare, "UpdateMaxMinFares has incorrectly updated LowestProbableChildFare for Singles travel date");

			// Add CostSearchTickets to collection
			travelDateSingles.OutwardTickets = tickets;

			// Call UpdateMaxMinFares method - needs outward and inward tickets
			travelDateSingles.UpdateMaxMinFares();
			Assert.AreEqual(100, travelDateSingles.MaxAdultFare, "UpdateMaxMinFares has incorrectly updated MaxAdultFare for Singles travel date");
			Assert.AreEqual(22, travelDateSingles.MinAdultFare, "UpdateMaxMinFares has incorrectly updated MinAdultFare for Singles travel date");
			Assert.AreEqual(50, travelDateSingles.MaxChildFare, "UpdateMaxMinFares has incorrectly updated MaxChildFare for Singles travel date");
			Assert.AreEqual(5, travelDateSingles.MinChildFare, "UpdateMaxMinFares has incorrectly updated MinChildFare for Singles travel date");
			Assert.AreEqual(25, travelDateSingles.LowestProbableAdultFare, "UpdateMaxMinFares has incorrectly updated LowestProbableAdultFare for Singles travel date");
			Assert.AreEqual(7, travelDateSingles.LowestProbableChildFare, "UpdateMaxMinFares has incorrectly updated LowestProbableChildFare for Singles travel date");

			// Add CostSearchTickets to collection
			travelDateSingles.InwardTickets = tickets;

			// Call UpdateMaxMinFares method
			travelDateSingles.UpdateMaxMinFares();
			Assert.AreEqual(200, travelDateSingles.MaxAdultFare, "Incorrect value for MaxAdultFare for Singles travel date");
			Assert.AreEqual(44, travelDateSingles.MinAdultFare, "Incorrect value for MinAdultFare for Singles travel date");
			Assert.AreEqual(100, travelDateSingles.MaxChildFare, "Incorrect value for MaxChildFare for Singles travel date");
			Assert.AreEqual(10, travelDateSingles.MinChildFare, "Incorrect value for MinChildFare for Singles travel date");
			Assert.AreEqual(50, travelDateSingles.LowestProbableAdultFare, "Incorrect value for LowestProbableAdultFare for Singles travel date");
			Assert.AreEqual(14, travelDateSingles.LowestProbableChildFare, "Incorrect value for LowestProbableChildFare for Singles travel date");

		}

		[Test]
		public void TestCombineTickets()
		{

			TravelDate td = new TravelDate();

			// Test 1 - empty array - just make sure doesn't fall over

			ArrayList ticketsToCombine = new ArrayList();
			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(0, ticketsToCombine.Count);

			// Test 2 - all have index 0 - no changes

			ticketsToCombine = GetTicketsAllIndexZero();
			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(4, ticketsToCombine.Count);

			foreach (CostSearchTicket cst in ticketsToCombine)
			{
				Assert.AreEqual("NXS", cst.ShortCode);
				Assert.AreEqual(0, cst.CombinedTicketIndex);
				Assert.AreEqual(0, cst.LegNumber);
			}
			
			// Test 3 - two "orphaned" tickets - these are deleted

			ticketsToCombine = GetTicketsTwoOrphans();

			Assert.AreEqual(6, ticketsToCombine.Count);

			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(4, ticketsToCombine.Count);

			foreach (CostSearchTicket cst in ticketsToCombine)
			{
				Assert.AreEqual("NXS", cst.ShortCode);
				Assert.AreEqual(0, cst.CombinedTicketIndex);
				Assert.AreEqual(0, cst.LegNumber);
			}

			// Test 4 - four pairs of tickets matched 

			ticketsToCombine = GetTicketsFourMatchedPairs();

			Assert.AreEqual(12, ticketsToCombine.Count);

			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(12, ticketsToCombine.Count);

			foreach (CostSearchTicket cst in ticketsToCombine)
			{
				switch (cst.ShortCode)
				{
					case "NXS":
						Assert.AreEqual(0, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX1":
						Assert.AreEqual(100, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX2":
						Assert.AreEqual(100, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX3":
						Assert.AreEqual(101, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX4":
						Assert.AreEqual(101, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX5":
						Assert.AreEqual(102, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX6":
						Assert.AreEqual(102, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX7":
						Assert.AreEqual(103, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX8":
						Assert.AreEqual(103, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;
				}
			}

			// Test 5 - leg one is fully flexible, leg two has full/ltd/none
			//				fully is used, others are orphaned and deleted

			ticketsToCombine = GetTicketsOneMatchedPairFullFull();

			Assert.AreEqual(8, ticketsToCombine.Count);

			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(10, ticketsToCombine.Count);

			foreach (CostSearchTicket cst in ticketsToCombine)
			{
				switch (cst.ShortCode)
				{
					case "NXS":
						Assert.AreEqual(0, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX1":
						//Assert.AreEqual(100, cst.CombinedTicketIndex);
						//Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX2":
						Assert.AreEqual(100, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX3":
						Assert.AreEqual(101, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX4":
						Assert.AreEqual(102, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					default:
						Assert.Fail("Unexpected code " + cst.ShortCode);
						break;
				}
			}


			// Test 6 - leg one is ltd flexible, leg two has full/ltd/none
			//				ltd is used, others are orphaned and deleted

			ticketsToCombine = GetTicketsOneMatchedPairLtdLtd();

			Assert.AreEqual(8, ticketsToCombine.Count);

			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(10, ticketsToCombine.Count);

			foreach (CostSearchTicket cst in ticketsToCombine)
			{
				switch (cst.ShortCode)
				{
					case "NXS":
						Assert.AreEqual(0, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX1":
						//Assert.AreEqual(100, cst.CombinedTicketIndex);
						//Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX2":
						Assert.AreEqual(100, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX3":
						Assert.AreEqual(101, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX4":
						Assert.AreEqual(102, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					default:
						Assert.Fail("Unexpected code " + cst.ShortCode);
						break;
				}
			}

			// Test 7 - leg one is no flex, leg two has full/ltd/none
			//				none is used, others are orphaned and deleted

			ticketsToCombine = GetTicketsOneMatchedPairNoneNone();

			Assert.AreEqual(8, ticketsToCombine.Count);

			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(10, ticketsToCombine.Count);

			foreach (CostSearchTicket cst in ticketsToCombine)
			{
				switch (cst.ShortCode)
				{					
					case "NXS":
						Assert.AreEqual(0, cst.CombinedTicketIndex);
						Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX1":
						//Assert.AreEqual(100, cst.CombinedTicketIndex);
						//Assert.AreEqual(0, cst.LegNumber);
						break;

					case "NX2":
						Assert.AreEqual(100, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX3":
						Assert.AreEqual(101, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					case "NX4":
						Assert.AreEqual(102, cst.CombinedTicketIndex);
						Assert.AreEqual(1, cst.LegNumber);
						break;

					default:
						Assert.Fail("Unexpected code " + cst.ShortCode);
						break;
				}
			}

			// Test 8 - leg one has full/ltd/none flex, leg two has full/ltd/none
			//				all are used, each matched against similar ticket

			ticketsToCombine = GetTicketsThreeMatchedPairs();

			Assert.AreEqual(10, ticketsToCombine.Count);

			td.CombineTickets(ticketsToCombine, 100);

			Assert.AreEqual(22, ticketsToCombine.Count);

			//NX1
			Assert.AreEqual(100, ((CostSearchTicket)ticketsToCombine[4]).CombinedTicketIndex);
			Assert.AreEqual(0, ((CostSearchTicket)ticketsToCombine[4]).LegNumber);
			//NX4
			Assert.AreEqual(100, ((CostSearchTicket)ticketsToCombine[5]).CombinedTicketIndex);
			Assert.AreEqual(1, ((CostSearchTicket)ticketsToCombine[5]).LegNumber);
			//NX5
			Assert.AreEqual(101, ((CostSearchTicket)ticketsToCombine[7]).CombinedTicketIndex);
			Assert.AreEqual(1, ((CostSearchTicket)ticketsToCombine[7]).LegNumber);
			//NX6
			Assert.AreEqual(102, ((CostSearchTicket)ticketsToCombine[9]).CombinedTicketIndex);
			Assert.AreEqual(1, ((CostSearchTicket)ticketsToCombine[9]).LegNumber);
			//NX2
			Assert.AreEqual(103, ((CostSearchTicket)ticketsToCombine[10]).CombinedTicketIndex);
			Assert.AreEqual(0, ((CostSearchTicket)ticketsToCombine[10]).LegNumber);
			//NX3
			Assert.AreEqual(106, ((CostSearchTicket)ticketsToCombine[16]).CombinedTicketIndex);
			Assert.AreEqual(0, ((CostSearchTicket)ticketsToCombine[16]).LegNumber);

		}


		#region non-test helper methods
		
		/// <summary>
		/// helper method use to add tickets to a test results set
		/// </summary>
		private void AddTickets()
		{	
			//mock coach travel date
			TravelDate coachDate1 = new TravelDate(1,TDDateTime.Now,TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate1.TicketType = TicketType.Single;
			coachDate1.OutwardTickets = singleCoachTickets;	
			coachDate1.InwardTickets = singleCoachTickets;	
			coachDate1.ReturnTickets = returnCoachTickets;				
			
			//mock rail travel date
			TravelDate railDate1 = new TravelDate(2,TDDateTime.Now,TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate1.TicketType = TicketType.Singles;
			railDate1.OutwardTickets = singleRailTickets;	
			railDate1.InwardTickets = singleRailTickets;
			railDate1.ReturnTickets = returnRailTickets;
				
			//mock air travel date
			TravelDate airDate1 = new TravelDate(3,TDDateTime.Now,TicketTravelMode.Air,10.00f,50.00f,true);
			airDate1.TicketType = TicketType.Single;
			airDate1.OutwardTickets = singleAirTickets;	
			airDate1.InwardTickets = singleAirTickets;
			airDate1.ReturnTickets = returnAirTickets;	
			
			//assign example travel dates to results set
			TravelDate[] testTravelDates = new TravelDate[]
			{
				coachDate1, railDate1, airDate1	
			};		
		
			classToTest.TravelDates = testTravelDates;
		}

		/// <summary>
		/// helper method used to remove tickets from a test results set
		/// </summary>
		private void RemoveTickets()
		{
			for (int i = 0; i < classToTest.TravelDates.Length ; i ++)
			{
				for (int j = 0; j < classToTest.TravelDates[i].OutwardTickets.Length; j ++)
				{
					if (classToTest.TravelDates[i].OutwardTickets[j] != null)
					{
						classToTest.TravelDates[i].OutwardTickets[j] = null;
					}
				}
				for (int j = 0; j < classToTest.TravelDates[i].InwardTickets.Length; j ++)
				{
					if (classToTest.TravelDates[i].InwardTickets[j] != null)
					{
						classToTest.TravelDates[i].InwardTickets[j] = null;
					}
				}
				for (int j = 0; j < classToTest.TravelDates[i].ReturnTickets.Length; j ++)
				{
					if (classToTest.TravelDates[i].ReturnTickets[j] != null)
					{
						classToTest.TravelDates[i].ReturnTickets[j] = null;
					}
				}
			}	
			//set public property to false
			classToTest.ContainsSinglesTickets = false;	
		}

		#endregion

	}
}
