//********************************************************************************
//NAME         : TestCoachJourneyFareFilter.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 22/10/2003
//DESCRIPTION  : Implementation of TestCoachJourneyFareFilter class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestCoachJourneyFareFilter.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 14:01:04   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 12 2009 09:11:04   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:48   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:37:20   mturner
//Initial revision.
//
//   Rev 1.7   Mar 14 2006 14:41:54   pcross
//Manual merge (automatic failed)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6.1.1   Mar 02 2006 17:45:34   NMoorhouse
//extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6.1.0   Jan 26 2006 20:18:48   rhopkins
//Pass new attributes in constructor for TDJourneyResult
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Dec 22 2005 15:06:42   RWilby
//Updated to fix unit tests
//
//   Rev 1.5   Nov 26 2005 11:59:36   mguney
//TestTimeRestrictionFilter method changed to handle the changes in the CoachJourneyFareFilter class.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.4   Nov 03 2005 19:21:26   RPhilpott
//Merge undiscounted and discounted CoachFareData into one.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 01 2005 17:24:40   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 31 2005 11:40:30   mguney
//SC code changed to SCL.
//
//   Rev 1.1   Oct 28 2005 14:56:16   mguney
//Associated IR.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 14:55:20   mguney
//Initial revision.


using System;
using System.Collections;
using NUnit.Framework;

using CJP = TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for TestCoachJourneyFareFilter.
	/// </summary>
	[TestFixture]
	public class TestCoachJourneyFareFilter
	{
		private Random rnd = new Random();		
		
		private const string OPERATORCODE1 = "NX";
		private const string OPERATORCODE2 = "SCL";
		private const string TICKETCODE = "Standard Return";
		private const string CHANGENAPTAN = "NAP1";
		private const int HOURSBETWEENJOURNEYS = 3;
		private const int MINUTESBETWEENLEGS = 30;

		#region Setup/Teardown		

		[SetUp]
		public void Init()
		{		
			// Initialise property service etc.
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());							
		}

		[TearDown]
		public void CleanUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();			
		}

		#endregion

		#region Test methods

		

		/// <summary>
		/// Tests filtering against unmatching operator code between the ticket and the legs of the journey.
		/// </summary>
		[Test]		
		public void TestOperatorCodeFilter()
		{
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);

			TDJourneyResult result = CreateDefaultTDResult(3,5,true);
			//Change the operator code of the first part of the second outward journey
			//so that it will be eliminated after the filtering.
			PublicJourneyDetail[] legs = ((PublicJourney)(result.OutwardPublicJourneys[1])).Details;
			legs[1].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
			//do the same for the first return journey
			legs = ((PublicJourney)(result.ReturnPublicJourneys[0])).Details;
			legs[1].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);						

			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(2,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for operator code.");
			Assert.AreEqual(2,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for operator code.");

			//test for one ticket
			result = AdjustForOneTicket(result);
			CostSearchTicket[] oneTicket = new CostSearchTicket[1];
			oneTicket[0] = coachTickets[0];

			//Change the operator code of the first part of the second outward journey
			//so that it will be eliminated after the filtering.
			legs = ((PublicJourney)(result.OutwardPublicJourneys[1])).Details;
			legs[1].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);									

			result = new CoachJourneyFareFilter().FilterJourneys(result,oneTicket);
			
			Assert.AreEqual(1,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for operator code [single ticket].");
			Assert.AreEqual(2,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for operator code [single ticket].");
		}

	
		/// <summary>
		/// Tests filtering against unmatching quota based fares between the ticket and the legs of the journey.
		/// </summary>
		[Test]
		public void TestQuotaBasedFilterTwoTickets()
		{
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);
			TDJourneyResult result = CreateDefaultTDResult(3,5,true);						
			
			//Test matching quota fares
			coachTickets[0].TicketCoachFareData.OperatorCode = OPERATORCODE1;
			coachTickets[0].TicketCoachFareData.IsQuotaFare = true;
			coachTickets[0].AdultFare = 5.85f;			

			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(3,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for quota fares.");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for quota fares.");

			//WE STILL HAVE 3 JOURNEYS EACH			
			//Test unmatching quota fares
			coachTickets[0].AdultFare = -1;
			coachTickets[1].TicketCoachFareData.IsQuotaFare = true;
			coachTickets[1].AdultFare = 5.85f;			
			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(0,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for unmatching quota fares.");
			Assert.AreEqual(0,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for unmatching quota fares.");

			//Generate tickets with unmatching ticket codes. 
			//(ticket codes and fare type of the coach fare obtained from the fare interface should match.)
			coachTickets = GenerateTickets("UNMACHINGCODE");
			coachTickets[0].AdultFare = 5.85f;
			coachTickets[1].TicketCoachFareData.IsQuotaFare = true;
			coachTickets[1].AdultFare = 5.85f;			
			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(0,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for unmatching ticket codes.");
			Assert.AreEqual(0,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for unmatching ticket codes.");
		}

		/// <summary>
		/// Tests filtering against unmatching quota based fares between the ticket and the legs of the journey.
		/// </summary>
		[Test]
		public void TestQuotaBasedFilterOneTicket()
		{
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);
			TDJourneyResult result = CreateDefaultTDResult(3,5,true);												

			result = AdjustForOneTicket(result);
			CostSearchTicket[] oneTicket = new CostSearchTicket[1];
			oneTicket[0] = coachTickets[0];
			//Test matching quota fares			
			oneTicket[0].TicketCoachFareData.IsQuotaFare = true;
			oneTicket[0].AdultFare = 5.85f;

			result = new CoachJourneyFareFilter().FilterJourneys(result,oneTicket);
			
			Assert.AreEqual(3,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for operator code [single ticket].");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for operator code [single ticket].");

			//Test unmatchingmatching quota fares			
			oneTicket[0].TicketCoachFareData.IsQuotaFare = true;
			oneTicket[0].AdultFare = -1;

			result = new CoachJourneyFareFilter().FilterJourneys(result,oneTicket);
			
			Assert.AreEqual(0,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for operator code [single ticket].");
			Assert.AreEqual(0,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for operator code [single ticket].");
		}

		/// <summary>
		/// Tests filtering against the time restriction of the ticket and the legs of the journey.
		/// </summary>
		[Test]
		public void TestTimeRestrictionFilter()
		{
			const int NUMBEROFLEGS = 5;
			DateTime firstJourneyStartTime = DateTime.Now.AddHours(1);
			DateTime secondJourneyStartTime = firstJourneyStartTime.AddHours(4);//DateTime.Now.AddHours(4);
			DateTime thirdJourneyStartTime = DateTime.Now.AddHours(7);
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);
			TDJourneyResult result = CreateDefaultTDResult(3,NUMBEROFLEGS,true);

			//Outward journeys start 1h from now.
			//Return journeys start 2 days from now.
			//Each journey starts 3h later than the previous one.
			//Each leg starts 30mins later than the previous one and ends in 20mins.
			//Create time restrictions for the ticket. 
			//There will be no restrictions which cover the 2nd outward journey so it will be filtered out.			
			TimeRestriction[] restrictions = new TimeRestriction[5];			
			
			//restrictins which cover the 1st and 3rd outward journeys
			restrictions[0] = new TimeRestriction(
				firstJourneyStartTime.AddMinutes(-5),
				firstJourneyStartTime.AddMinutes(NUMBEROFLEGS * MINUTESBETWEENLEGS).AddMinutes(5));
			restrictions[1] = new TimeRestriction(
				thirdJourneyStartTime.AddMinutes(-5),
				thirdJourneyStartTime.AddMinutes(NUMBEROFLEGS * MINUTESBETWEENLEGS).AddMinutes(5));			
			//restrictins which cover the 1st, 2nd and 3rd inward journeys
			restrictions[2] = new TimeRestriction(
				firstJourneyStartTime.AddDays(2).AddMinutes(-5),
				firstJourneyStartTime.AddDays(2).AddMinutes(NUMBEROFLEGS * MINUTESBETWEENLEGS).AddMinutes(5));
			restrictions[3] = new TimeRestriction(
				secondJourneyStartTime.AddDays(2).AddMinutes(-5),
				secondJourneyStartTime.AddDays(2).AddMinutes(NUMBEROFLEGS * MINUTESBETWEENLEGS).AddMinutes(5));			
			restrictions[4] = new TimeRestriction(
				thirdJourneyStartTime.AddDays(2).AddMinutes(-5),
				thirdJourneyStartTime.AddDays(2).AddMinutes(NUMBEROFLEGS * MINUTESBETWEENLEGS).AddMinutes(5));			

			coachTickets[0].TicketCoachFareData.TimeRestrictions = restrictions;

			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(2,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for time restriction.");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for time restriction.");
		}

		/// <summary>
		/// Tests filtering against operator code restriction of the ticket.
		/// </summary>
		[Test]
		public void TestOperatorRestrictionFilter()
		{
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);
			TDJourneyResult result = CreateDefaultTDResult(3,5,true);
			//Change the operator code of the first part of the second outward journey
			//so that it will be eliminated after the operator code filtering.
			//O2 is the cjp operator code. It corresponds to NX when looked up.
			//So it will pass the operator code check but fail in the restricted operator codes check.
			PublicJourneyDetail[] legs = ((PublicJourney)(result.OutwardPublicJourneys[1])).Details;
			legs[1].Services[0] = new ServiceDetails("O2","O2 NAME","1111","AAAA","SW","1",string.Empty);
			
			//Set the restricted operator codes.
			coachTickets[0].TicketCoachFareData.RestrictedOperatorCodes = new string[] {OPERATORCODE1,OPERATORCODE2};

			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(2,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for time restriction.");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for time restriction.");
		}

		/// <summary>
		/// Tests filtering against service number restriction of the ticket.
		/// </summary>
		[Test]
		public void TestServiceRestrictionFilter()
		{
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);
			TDJourneyResult result = CreateDefaultTDResult(3,5,true);
			//Change the service number of the first part of the second outward journey
			//so that it will be eliminated after the restricted service number filtering.			
			PublicJourneyDetail[] legs = ((PublicJourney)(result.OutwardPublicJourneys[1])).Details;
			legs[1].Services[0] = new ServiceDetails(OPERATORCODE1,"NAME","0000","AAAA","SW","1",string.Empty);
			
			//Set the restricted operator codes.
			coachTickets[0].TicketCoachFareData.RestrictedServices = new string[] {"1111"};

			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(2,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for time restriction.");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for time restriction.");
		}

		/// <summary>
		/// Tests outward only case.
		/// </summary>
		[Test]
		public void TestOutwardOnly()
		{
			CostSearchTicket[] coachTickets = GenerateTickets(TICKETCODE);
			TDJourneyResult result = CreateDefaultTDResult(3,5,true);

			result = new CoachJourneyFareFilter().FilterJourneys(result,coachTickets);
			
			Assert.AreEqual(3,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for time restriction.");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for time restriction.");

			//test for one ticket
			result = AdjustForOneTicket(result);
			CostSearchTicket[] oneTicket = new CostSearchTicket[1];
			oneTicket[0] = coachTickets[0];			

			result = new CoachJourneyFareFilter().FilterJourneys(result,oneTicket);
			
			Assert.AreEqual(3,result.OutwardPublicJourneys.Count,"Invalid number of outward journeys after filtering for operator code [single ticket].");
			Assert.AreEqual(3,result.ReturnPublicJourneys.Count,"Invalid number of return journeys after filtering for operator code [single ticket].");
		}

		#endregion

		#region Private Helper Methods

		#region Convenience Methods
		
		

		private CostSearchTicket[] GenerateTickets(string ticketCode)
		{
			//mock versions of coach tickets
			CostSearchTicket[] coachTickets = new CostSearchTicket[]
			{
				new	CostSearchTicket(ticketCode,Flexibility.FullyFlexible,"NXS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High),
				new CostSearchTicket(ticketCode,Flexibility.LimitedFlexibility,"NXB",200.00f,50.00f,float.NaN,float.NaN,150.00f,25.00f,0,16,Probability.Medium),				
			};
			coachTickets[0].TicketCoachFareData = new CoachFareData();
			coachTickets[0].TicketCoachFareData.ChangeNaptans = new string[] {};
			coachTickets[0].TicketCoachFareData.DestinationNaptan = CHANGENAPTAN;
			coachTickets[0].TicketCoachFareData.IsQuotaFare = false;
			coachTickets[0].TicketCoachFareData.OperatorCode = OPERATORCODE1;
			coachTickets[0].TicketCoachFareData.OriginNaptan = string.Empty;
			coachTickets[0].TicketCoachFareData.RestrictedOperatorCodes = new string[0];
			coachTickets[0].TicketCoachFareData.RestrictedServices = new string[0] ;
			coachTickets[0].TicketCoachFareData.TimeRestrictions = null;
				/*new TimeRestriction[] {
											new TimeRestriction(),
											new TimeRestriction()
										};*/

			coachTickets[1].TicketCoachFareData = new CoachFareData();
			coachTickets[1].TicketCoachFareData.ChangeNaptans = new string[] {};
			coachTickets[1].TicketCoachFareData.DestinationNaptan = string.Empty;
			coachTickets[1].TicketCoachFareData.IsQuotaFare = false;
			coachTickets[1].TicketCoachFareData.OperatorCode = OPERATORCODE2;
			coachTickets[1].TicketCoachFareData.OriginNaptan = CHANGENAPTAN;
			coachTickets[1].TicketCoachFareData.RestrictedOperatorCodes = new string[0] ;
			coachTickets[1].TicketCoachFareData.RestrictedServices = new string[0] ;
			coachTickets[1].TicketCoachFareData.TimeRestrictions = null;
				/*new TimeRestriction[] {
										  new TimeRestriction(),
										  new TimeRestriction()
									  };*/

			return coachTickets;
			

		}


		/// <summary>
		/// Each journey starts 3h later than the previous one.
		/// Each leg starts 30mins later than the previous one and ends in 20mins.
		/// </summary>
		/// <param name="startDateTime"></param>
		/// <param name="numberOfJourneys"></param>
		/// <param name="numberOfLegs"></param>
		/// <returns></returns>
		private CJP.JourneyResult CreateDefaultCJPJourneyResult(DateTime startDateTime,int numberOfJourneys, 
			int numberOfLegs)
		{
			CJP.JourneyResult result = new CJP.JourneyResult();
			result.publicJourneys = new CJP.PublicJourney[numberOfJourneys];
			for (int i=0;i < numberOfJourneys;i++)
			{
				result.publicJourneys[i] = new CJP.PublicJourney();
				result.publicJourneys[i].legs = new CJP.Leg[numberOfLegs];
				for (int j=0;j < numberOfLegs;j++)
				{	
					result.publicJourneys[i].legs[j] = 
						CreateLeg(startDateTime.AddMinutes((i*HOURSBETWEENJOURNEYS*60) + j*MINUTESBETWEENLEGS));
				}
			}

			return result;
		}


		/// <summary>
		/// Creates a sample leg.
		/// </summary>
		/// <param name="startDateTime"></param>
		/// <returns></returns>
		private CJP.Leg CreateLeg(DateTime startDateTime)
		{
			CJP.TimedLeg leg = new CJP.TimedLeg();
			leg.mode = CJP.ModeType.Coach;
			leg.validated = true;

			leg.board = new CJP.Event();
			leg.board.activity = CJP.ActivityType.Depart;
			leg.board.departTime = startDateTime; //new DateTime(2004, 5, 2, 15, 12, 0);
			leg.board.stop = CreateStop("Board NAPTANID","16 Hill Street");
			leg.board.geometry = CreateGeometry();

			leg.alight = new CJP.Event();
			leg.alight.activity = CJP.ActivityType.Arrive;
			leg.alight.arriveTime = startDateTime.AddMinutes(20); //new DateTime (2004, 5, 2, 16, 42, 0);
			leg.alight.stop = CreateStop("Alight NAPTANID", "Cowcaddens Underground Station");

			leg.services = CreateServiceArr( "Stanmore", "GU", "Strathclyde Transport");

			leg.destination = CreateEvent("Dest Stop NATPAN", "Stanmore");
			leg.origin = CreateEvent("Origin Stop NATPAN", "Cowcadden");

			return leg;
		}		


		/// <summary>
		/// Outward journeys start 1h from now.
		/// Return journeys start 2 days from now.
		/// Each journey starts 3h later than the previous one.
		/// Each leg starts 30mins later than the previous one and ends in 20mins.
		/// </summary>
		/// <param name="numberOfJourneys"></param>
		/// <param name="numberOfLegs"></param>
		/// <param name="hasReturn"></param>
		/// <returns></returns>
		private TDJourneyResult CreateDefaultTDResult(int numberOfJourneys, int numberOfLegs, bool hasReturn)
		{
			TDDateTime outwardTime = new TDDateTime(DateTime.Now.AddHours(1));			
			TDDateTime returnTime = outwardTime.AddDays(2); //new TDDateTime(DateTime.Now.AddDays(2));

			// Create a mock TDJourneyResult
			TDJourneyResult result = new TDJourneyResult(0, 0, outwardTime, returnTime, false, false, false);
			//add the outward result
			CJP.JourneyResult outwardResult = CreateDefaultCJPJourneyResult(outwardTime.GetDateTime(),
				numberOfJourneys,numberOfLegs);
			result.AddResult(outwardResult, true, null, null, null, null, "ssss", false, -1);
			if (hasReturn)
			{
				// add the return result
				CJP.JourneyResult returnResult = CreateDefaultCJPJourneyResult(returnTime.GetDateTime(),
					numberOfJourneys,numberOfLegs);
				result.AddResult(returnResult, false, null, null, null, null, "ssss", false, -1);
			}


			PublicJourneyDetail[] legs;
			

			//set operator codes for legs of the journeys. 
			#region set operator codes and change naptan section
			legs = ((PublicJourney)(result.OutwardPublicJourneys[0])).Details;
			for (int i=0;i < legs.Length;i++)
			{
				if (i < 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);					
				if (i==2)
				{
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
					legs[i].LegStart.Location.NaPTANs[0] = new TDNaptan(CHANGENAPTAN,null);
				}
				if (i > 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}

			//The first ticket operator code is OPERATORCODE1,			
			legs = ((PublicJourney)(result.OutwardPublicJourneys[1])).Details;
			for (int i=0;i < legs.Length;i++)
			{
				if (i < 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
				if (i==2)
				{
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
					legs[i].LegStart.Location.NaPTANs[0] = new TDNaptan(CHANGENAPTAN,null);
				}
				if (i > 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}

			legs = ((PublicJourney)(result.OutwardPublicJourneys[2])).Details;
			for (int i=0;i < legs.Length;i++)
			{
				if (i < 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
				if (i==2)
				{
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
					legs[i].LegStart.Location.NaPTANs[0] = new TDNaptan(CHANGENAPTAN,null);
				}
				if (i > 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}
	
			//set operator codes for legs of the return journeys. 
			legs = ((PublicJourney)(result.ReturnPublicJourneys[0])).Details;
			for (int i=0;i < legs.Length;i++)
			{
				if (i < 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);					
				if (i==2)
				{
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
					legs[i].LegStart.Location.NaPTANs[0] = new TDNaptan(CHANGENAPTAN,null);
				}
				if (i > 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}

			legs = ((PublicJourney)(result.ReturnPublicJourneys[1])).Details;
			for (int i=0;i < legs.Length;i++)
			{
				if (i < 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
				if (i==2)
				{
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
					legs[i].LegStart.Location.NaPTANs[0] = new TDNaptan(CHANGENAPTAN,null);
				}
				if (i > 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}

			legs = ((PublicJourney)(result.ReturnPublicJourneys[2])).Details;
			for (int i=0;i < legs.Length;i++)
			{
				if (i < 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE2,"AB NAME","1111","AAAA","SW","1",string.Empty);
				if (i==2)
				{
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
					legs[i].LegStart.Location.NaPTANs[0] = new TDNaptan(CHANGENAPTAN,null);
				}
				if (i > 2)
					legs[i].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}
			#endregion

			return result;
		}


		/// <summary>
		/// Creates a geometry for creating a leg.
		/// </summary>
		/// <returns></returns>
		private CJP.Coordinate[] CreateGeometry()
		{
			CJP.Coordinate[] coords = new CJP.Coordinate[ rnd.Next(1,4) ];
			for(int i = 0; i < coords.Length; i++)
			{
				coords[i] = new CJP.Coordinate();
				coords[i].easting = rnd.Next(100,1000);
				coords[i].northing = rnd.Next(100,1000);
			}
			return coords;
		}


		/// <summary>
		/// Creates a service for the leg.
		/// </summary>
		/// <param name="destBoard"></param>
		/// <param name="opCode"></param>
		/// <param name="opName"></param>
		/// <param name="servNum"></param>
		/// <returns></returns>
		private CJP.Service CreateService(string destBoard, string opCode, string opName, string servNum)
		{
			CJP.Service serv = new CJP.Service();
			serv.destinationBoard = destBoard;
			serv.operatorCode = opCode;
			serv.operatorName = opName;
			serv.serviceNumber = servNum;
			return serv;
		}


		/// <summary>
		/// Creates a service array.
		/// </summary>
		/// <param name="destBoard"></param>
		/// <param name="opCode"></param>
		/// <param name="opName"></param>
		/// <returns></returns>
		private CJP.Service[] CreateServiceArr(string destBoard, string opCode, string opName)
		{
			return CreateServiceArr( destBoard, opCode, opName, null);
		}


		/// <summary>
		/// Creates a service array.
		/// </summary>
		/// <param name="destBoard"></param>
		/// <param name="opCode"></param>
		/// <param name="opName"></param>
		/// <param name="servNum"></param>
		/// <returns></returns>
		private CJP.Service[] CreateServiceArr(string destBoard, string opCode, string opName, string servNum)
		{
			CJP.Service[] serv = new CJP.Service[1];
			serv[0] = CreateService( destBoard, opCode, opName, servNum );
			return serv;
		}		


		
		/// <summary>
		/// Creates a stop.
		/// </summary>
		/// <param name="naptan"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		private CJP.Stop CreateStop(string naptan, string name)
		{	
			CJP.Stop stop = new CJP.Stop();
			
			if(naptan != null)
				stop.NaPTANID = naptan;
		
			stop.name = name;
			stop.coordinate = new CJP.Coordinate();
			stop.coordinate.easting = rnd.Next(100,1000);
			stop.coordinate.northing = rnd.Next(100,1000);

			return stop;
		}


		/// <summary>
		/// Creates an event.
		/// </summary>
		/// <param name="naptan"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		private CJP.Event CreateEvent(string naptan, string name)
		{
			CJP.Event cjpEvent = new CJP.Event();
			cjpEvent.stop = CreateStop(naptan, name);
			cjpEvent.geometry = CreateGeometry();
			return cjpEvent;
		}


		/// <summary>
		/// Puts NX operator code to every leg of every journey.
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		private TDJourneyResult AdjustForOneTicket(TDJourneyResult result)
		{

			for (int i=0;i < result.OutwardPublicJourneys.Count;i++)
			{
				PublicJourneyDetail[] legs = ((PublicJourney)(result.OutwardPublicJourneys[i])).Details;
				for (int j=0;j < legs.Length;j++)
				{					
					legs[j].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
				}
				//legs[j].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
			}

			for (int i=0;i < result.ReturnPublicJourneys.Count;i++)
			{
				PublicJourneyDetail[] legs = ((PublicJourney)(result.ReturnPublicJourneys[i])).Details;
				for (int j=0;j < legs.Length;j++)
				{
					legs[j].Services[0] = new ServiceDetails(OPERATORCODE1,"AB NAME","1111","AAAA","SW","1",string.Empty);
				}
				//((PublicJourney)(result.ReturnPublicJourneys[i])).Details = legs;
			}

			return result;
		}

		#endregion		

		

		#endregion
	}
}
