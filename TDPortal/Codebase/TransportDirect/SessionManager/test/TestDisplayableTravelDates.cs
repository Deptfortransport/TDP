// ******************************************************************* 
// NAME			: TestDisplayableTravelDates.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 01/02/2005
// DESCRIPTION	: Test the DisplayableTravelDates
// ******************************************************************* 

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Test the DisplayableTravelDates.
	/// </summary>
	[TestFixture]
	public class TestDisplayableTravelDates
	{
		private TravelDate[] travelDates;
	
		#region mock CostSearchTickets
		private static CostSearchTicket[] singleCoachTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("NX Standard",Flexibility.FullyFlexible,"NXS",50.00f,10.00f,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket("NX Business",Flexibility.LimitedFlexibility,"NXB",200.00f,50.00f,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX Cheap day single",Flexibility.LimitedFlexibility,"NXC",5.00f,1.00f,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX first",Flexibility.FullyFlexible,"NXF",40.00f,20.00f,30.00f,10.00f,0,16, Probability.High)
			};
		private static CostSearchTicket[] returnCoachTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("NX Standard Return",Flexibility.FullyFlexible,"NXSR",50.00f,10.00f,25.00f,7.00f,0,16, Probability.Low),
				new CostSearchTicket("NX Business Return",Flexibility.LimitedFlexibility,"NXBR",200.00f,50.00f,150.00f,25.00f,0,16,Probability.Medium),
				new CostSearchTicket("NX Cheap day return",Flexibility.LimitedFlexibility,"NXCR",5.00f,1.00f,2.00f,1.00f,0,16, Probability.Medium),
				new CostSearchTicket("NX first return",Flexibility.FullyFlexible,"NXR",40.00f,20.00f,30.00f,10.00f,0,16,Probability.High)
			};	
		private static CostSearchTicket[] singleRailTickets = new CostSearchTicket[]
			{		
				new CostSearchTicket("Apex Single /any rail",Flexibility.FullyFlexible,"Apex",27.10f,15.00f,40.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("7 day advance / Virgin",Flexibility.NoFlexibility,"7day",20.00f,10.00f,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Cheap day single /any rail",Flexibility.LimitedFlexibility,"CDS",5.00f,1.00f,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Standard first /any rail",Flexibility.FullyFlexible,"STF",40.00f,20.00f,30.00f,10.00f,0,16,Probability.High)
			};		
		private static CostSearchTicket[] returnRailTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("Apex Return /any rail",Flexibility.NoFlexibility,"Apex",50.00f,10.00f,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("7 day advance return/ Virgin",Flexibility.NoFlexibility,"7day",20.00f,10.00f,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("Cheap day return /any rail",Flexibility.LimitedFlexibility,"CDR",5.00f,1.00f,2.00f,1.00f,0,16,Probability.Medium),
				new CostSearchTicket("Standard return /any rail",Flexibility.FullyFlexible,"SR",40.00f,20.00f,30.00f,10.00f,0,16,Probability.High)
			};
		private static CostSearchTicket[] singleAirTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("BA Economy",Flexibility.NoFlexibility,"Econ",50.00f,10.00f,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("BA Business",Flexibility.LimitedFlexibility,"Busi",200.00f,10.00f,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA First Class",Flexibility.FullyFlexible,"First",1000.00f,500.00f,400.00f,250.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA Cattle Class",Flexibility.LimitedFlexibility,"Cattle",0.99f,0.49f,0.79f,0.09f,0,16,Probability.High)
			};
		private static CostSearchTicket[] returnAirTickets = new CostSearchTicket[]
			{
				new CostSearchTicket("BA Economy Return",Flexibility.NoFlexibility,"Econ",50.00f,10.00f,25.00f,7.00f,0,16,Probability.Low),
				new CostSearchTicket("BA Business Return",Flexibility.NoFlexibility,"Busi",200.00f,10.00f,5.00f,5.00f,0,16,Probability.Medium),
				new CostSearchTicket("BA First Class Return",Flexibility.NoFlexibility,"First",1000.00f,500.00f,400.00f,250.00f,0,16,Probability.Medium),
				new CostSearchTicket("Easyjet Cattle Class Return",Flexibility.NoFlexibility,"Cattle",0.99f,0.49f,0.79f,0.09f,0,16,Probability.High)
			};		

		#endregion

		public TestDisplayableTravelDates()
		{
			//System.Diagnostics.Debugger.Launch();
		}

		[TestFixtureSetUp]
		public void Init()
		{					
			//add some test tickets 
			AddTickets();	
		}

		[TestFixtureTearDown]
		public void CleanUp()
		{
			//remove test tickets
			RemoveTickets();				
		}

		/// <summary>
		/// The test that the DisplayableTravelDates are formatted correctly
		/// </summary>
		/// <returns></returns>
		[Test]
		public void DisplayableTravelDatesFormatting()
		{
			DisplayableTravelDates travelDatesTable = new DisplayableTravelDates(travelDates);

			travelDatesTable.MoveNext();

			Assert.AreEqual( System.DateTime.Today.DayOfWeek.ToString().Substring(0,3),
				((DisplayableTravelDate)travelDatesTable.Current).OutwardDayName,
				"Outward day name");

			Assert.AreEqual( System.DateTime.Today.Day.ToString("00")+"/"+System.DateTime.Today.Month.ToString("00"),
				((DisplayableTravelDate)travelDatesTable.Current).OutwardDayMonth,
				"Outward date day/month");

			Assert.AreEqual( "Coach",
				((DisplayableTravelDate)travelDatesTable.Current).TravelMode,
				"Transport mode");

			Assert.AreEqual( "£50.00",
				((DisplayableTravelDate)travelDatesTable.Current).LowestProbableFare,
				"Lowest probable fare");

			Assert.AreEqual( "*",
				((DisplayableTravelDate)travelDatesTable.Current).UnlikelyToBeAvailable,
				"Unlikely to be available");

			Assert.Fail("\n\n\tTODO: Reinstate MinFare/MaxFare tests when TravelDate has been updated to populate them\n\n");
// TODO: Reinstate MinFare/MaxFare tests when TravelDate has been updated to populate them
//			Assert.AreEqual( "£0.00",
//				((DisplayableTravelDate)travelDatesTable.Current).MinFare,
//				"Min fare");
//
//			Assert.AreEqual( "£20.00",
//				((DisplayableTravelDate)travelDatesTable.Current).MaxFare,
//				"Max fare");
		}

		#region non-test helper methods
		
		/// <summary>
		/// helper method use to add tickets to a test results set
		/// </summary>
		private void AddTickets()
		{	
			//mock coach travel date
			TravelDate coachDate1 = new TravelDate(TDDateTime.Now,TicketTravelMode.Coach,50.00f,true);
			coachDate1.TicketType = TicketType.Single;
			coachDate1.OutwardTickets = singleCoachTickets;	
			coachDate1.InwardTickets = singleCoachTickets;	
			coachDate1.ReturnTickets = returnCoachTickets;				
			
			//mock rail travel date
			TravelDate railDate1 = new TravelDate(TDDateTime.Now,TicketTravelMode.Rail,50.00f,true);
			railDate1.TicketType = TicketType.Singles;
			railDate1.OutwardTickets = singleRailTickets;	
			railDate1.InwardTickets = singleRailTickets;
			railDate1.ReturnTickets = returnRailTickets;
				
			//mock air travel date
			TravelDate airDate1 = new TravelDate(TDDateTime.Now,TicketTravelMode.Air,50.00f,true);
			airDate1.TicketType = TicketType.Single;
			airDate1.OutwardTickets = singleAirTickets;	
			airDate1.InwardTickets = singleAirTickets;
			airDate1.ReturnTickets = returnAirTickets;	
			
			//assign example travel dates to results set
			travelDates = new TravelDate[]
			{
				coachDate1, railDate1, airDate1	
			};		
		}

		/// <summary>
		/// helper method used to remove tickets from a test results set
		/// </summary>
		private void RemoveTickets()
		{
			for (int i = 0; i < travelDates.Length ; i ++)
			{
				for (int j = 0; j < travelDates[i].OutwardTickets.Length; j ++)
				{
					if (travelDates[i].OutwardTickets[j] != null)
					{
						travelDates[i].OutwardTickets[j] = null;
					}
				}
				for (int j = 0; j < travelDates[i].InwardTickets.Length; j ++)
				{
					if (travelDates[i].InwardTickets[j] != null)
					{
						travelDates[i].InwardTickets[j] = null;
					}
				}
				for (int j = 0; j < travelDates[i].ReturnTickets.Length; j ++)
				{
					if (travelDates[i].ReturnTickets[j] != null)
					{
						travelDates[i].ReturnTickets[j] = null;
					}
				}
			}
		}

		#endregion

	}
}
