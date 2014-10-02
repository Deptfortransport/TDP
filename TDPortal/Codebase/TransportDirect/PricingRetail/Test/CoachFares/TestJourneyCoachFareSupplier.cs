//********************************************************************************
//NAME         : TestJourneyCoachFareSupplier.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-10-31
//DESCRIPTION  : NUnit tests of JourneyCoachFareSupplier
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestJourneyCoachFareSupplier.cs-arc  $
//
//   Rev 1.1   Feb 02 2009 16:56:20   mmodi
//Corrected nunit errors
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:37:20   mturner
//Initial revision.
//
//   Rev 1.7   May 25 2007 16:22:14   build
//Automatically merged from branch for stream4401
//
//   Rev 1.6.1.0   May 09 2007 14:48:38   mmodi
//Added Test for new Coach fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6   Jan 03 2006 12:36:08   mguney
//TestMockFareInterface changes applied.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.5   Dec 01 2005 09:50:46   mguney
//MockFareInterface is replaced with TestMockFareInterface in the related test classes and failing tests fixed.
//Resolution for 3267: Mock classes should be prefixed with Test.
//
//   Rev 1.4   Nov 29 2005 15:55:28   mguney
//TestExceptionalFares method included.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.3   Nov 29 2005 14:47:28   mguney
//Adjusted according to the changes made on the JourneyCoachFare supplier and on the MockFareInterface.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.2   Nov 07 2005 20:49:40   RPhilpott
//Open Returns are in Outwards Ticket Collection.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 06 2005 19:34:54   RPhilpott
//Associate with IR,
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Nov 06 2005 19:34:10   RPhilpott
//Initial revision.
//

using System;
using NUnit.Framework;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

using CJP =  TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Test harness for JourneyCoachFareSupplier
	/// </summary>
	[TestFixture]
	public class TestJourneyCoachFareSupplier
	{				

		#region Constructor and Initialisation

		
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

		[Test]
		public void TestGetSampleStartDate()
		{
			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			TimeSpan span = supplier.GetSampleStartTime("09:30");

			Assert.AreEqual(span.Hours, 9);
			Assert.AreEqual(span.Minutes, 30);

			span = supplier.GetSampleStartTime("09.30");
			Assert.AreEqual(span.Hours, 9);
			Assert.AreEqual(span.Minutes, 30);

			span = supplier.GetSampleStartTime("23:59");
			Assert.AreEqual(span.Hours, 23);
			Assert.AreEqual(span.Minutes, 59);

			span = supplier.GetSampleStartTime("00:00");
			Assert.AreEqual(span.Hours, 0);
			Assert.AreEqual(span.Minutes, 0);

			span = supplier.GetSampleStartTime(null);
			Assert.AreEqual(span.Hours, 0);
			Assert.AreEqual(span.Minutes, 0);

			span = supplier.GetSampleStartTime(string.Empty);
			Assert.AreEqual(span.Hours, 0);
			Assert.AreEqual(span.Minutes, 0);

		}

		[Test]
		public void TestGetUniqueOutwardDates()
		{
			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			ArrayList dates = new ArrayList();

			Hashtable results = supplier.GetUniqueOutwardDates(dates);

			Assert.AreEqual(results.Count, 0);

			TravelDate date1 = new TravelDate();
			date1.OutwardDate = new TDDateTime(2005, 03, 01);
			date1.ReturnDate  = new TDDateTime(2005, 03, 01);
			dates.Add(date1);

			results = supplier.GetUniqueOutwardDates(dates);

			Assert.AreEqual(results.Count, 1);
			Assert.IsTrue(results.ContainsKey(date1.OutwardDate));

			dates.Clear();

			date1 = new TravelDate();
			date1.OutwardDate = new TDDateTime(2005, 03, 01);
			date1.ReturnDate  = new TDDateTime(2005, 03, 01);
			dates.Add(date1);

			TravelDate date2 = new TravelDate();
			date2.OutwardDate = new TDDateTime(2005, 03, 01);
			date2.ReturnDate  = new TDDateTime(2005, 03, 02);
			dates.Add(date2);

			results = supplier.GetUniqueOutwardDates(dates);

			Assert.AreEqual(results.Count, 1);
			Assert.IsTrue(results.ContainsKey(date1.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date2.OutwardDate));

			dates.Clear();

			date1 = new TravelDate();
			date1.OutwardDate = new TDDateTime(2005, 03, 01);
			date1.ReturnDate  = new TDDateTime(2005, 03, 01);
			dates.Add(date1);

			date2 = new TravelDate();
			date2.OutwardDate = new TDDateTime(2005, 03, 01);
			date2.ReturnDate  = new TDDateTime(2005, 03, 02);
			dates.Add(date2);

			TravelDate date3 = new TravelDate();
			date3.OutwardDate = new TDDateTime(2005, 03, 02);
			date3.ReturnDate  = new TDDateTime(2005, 03, 02);
			dates.Add(date3);

			TravelDate date4 = new TravelDate();
			date4.OutwardDate = new TDDateTime(2005, 03, 02);
			date4.ReturnDate  = new TDDateTime(2005, 03, 03);
			dates.Add(date4);

			results = supplier.GetUniqueOutwardDates(dates);

			Assert.AreEqual(results.Count, 2);
			Assert.IsTrue(results.ContainsKey(date1.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date2.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date3.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date4.OutwardDate));

			// add to existing list (not cleared) ...

			TravelDate date5 = new TravelDate();
			date5.OutwardDate = new TDDateTime(2005, 03, 03);
			date5.ReturnDate  = new TDDateTime(2005, 03, 05);
			dates.Add(date5);

			TravelDate date6 = new TravelDate();
			date6.OutwardDate = new TDDateTime(2005, 03, 01);
			date6.ReturnDate  = new TDDateTime(2005, 03, 04);
			dates.Add(date6);

			results = supplier.GetUniqueOutwardDates(dates);

			Assert.AreEqual(results.Count, 3);
			Assert.IsTrue(results.ContainsKey(date1.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date2.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date3.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date4.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date5.OutwardDate));
			Assert.IsTrue(results.ContainsKey(date6.OutwardDate));

		}

		[Test]
		public void TestCreatePricingResults()
		{
			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			TestMockFareInterface mock = new TestMockFareInterface(CoachFaresInterfaceType.ForJourney);

			CoachFare[] fares = (mock.GetCoachFares(null)).Fares;

			Discounts discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);

			PricingResult[] results = supplier.CreatePricingResults(fares, discounts, "NX", false);

			Assert.AreEqual(results[0].MinChildAge, 5);
			Assert.AreEqual(results[0].MaxChildAge, 18);
			Assert.AreEqual(results[0].Tickets.Count, 1);

			Assert.AreEqual(results[1].MinChildAge, 5);
			Assert.AreEqual(results[1].MaxChildAge, 18);
			Assert.AreEqual(results[1].Tickets.Count, 1);
			
			Ticket cst = (Ticket)(results[0].Tickets[0]); 

			Assert.AreEqual(cst.Flexibility, Flexibility.FullyFlexible);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) , Decimal.Round(Convert.ToDecimal(2.6),2));
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) , Decimal.Round(Convert.ToDecimal(0.9),2));
			Assert.AreEqual(cst.DiscountedAdultFare , float.NaN);
			Assert.AreEqual(cst.DiscountedChildFare , float.NaN);
			
			
			cst = (Ticket)(results[1].Tickets[0]); 

			Assert.AreEqual(cst.Flexibility, Flexibility.FullyFlexible);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) , Decimal.Round(Convert.ToDecimal(5.85),2));
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) , Decimal.Round(Convert.ToDecimal(4.5),2));
			Assert.AreEqual(cst.DiscountedAdultFare , float.NaN);
			Assert.AreEqual(cst.DiscountedChildFare , float.NaN);
			
			
			discounts = new Discounts(string.Empty, TestMockFareInterface.DISCOUNTCARDTYPENATEXCOACHMONTHLY, TicketClass.All);

			results = supplier.CreatePricingResults(fares, discounts, "NX", false);

			Assert.AreEqual(results[0].MinChildAge, 5);
			Assert.AreEqual(results[0].MaxChildAge, 18);
			Assert.AreEqual(results[0].Tickets.Count, 1);

			Assert.AreEqual(results[1].MinChildAge, 5);
			Assert.AreEqual(results[1].MaxChildAge, 18);
			Assert.AreEqual(results[1].Tickets.Count, 1);
			
			cst = (Ticket)(results[0].Tickets[0]); 

			Assert.AreEqual(cst.Flexibility, Flexibility.LimitedFlexibility);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) , Decimal.Round(Convert.ToDecimal(2.6),2));
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) , Decimal.Round(Convert.ToDecimal(0.9),2));
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.DiscountedAdultFare),2) , Decimal.Round(Convert.ToDecimal(2.4),2));
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.DiscountedChildFare),2) , Decimal.Round(Convert.ToDecimal(0.7),2));

			cst = (Ticket)(results[1].Tickets[0]); 

			Assert.AreEqual(cst.Flexibility, Flexibility.LimitedFlexibility);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) , 5.85);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) , 4.5);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.DiscountedAdultFare),2) , 5.7);
			Assert.AreEqual(Decimal.Round(Convert.ToDecimal(cst.DiscountedChildFare),2) , 4.3);
			
		}

		[Test]
		public void TestExceptionalFares()
		{
			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			TestMockFareInterface mock = new TestMockFareInterface(CoachFaresInterfaceType.ForJourney);

			CoachFare[] fares = (mock.GetCoachFares(null)).Fares;

			Discounts discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);

			//2 EX tickets excluded.
			//2 day return ticket excluded, because day return parameter is true
			PricingResult[] results = supplier.CreatePricingResults(fares, discounts, "NX", true);

			Assert.AreEqual(results[1].Tickets.Count, 3);
			//2 EX tickets excluded.
			//2 day return tickets excluded, because day return parameter is true
			results = supplier.CreatePricingResults(fares, discounts, "NX", false);

			Assert.AreEqual(results[1].Tickets.Count, 1);
		}

		[Test]
		public void TestNewCoachFares()
		{
			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			TestMockFareInterface mock = new TestMockFareInterface(CoachFaresInterfaceType.ForJourney);

			CoachFare[] fares = (mock.GetNewCoachFares(null)).Fares;

			Discounts discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);

			Assert.AreEqual(fares.Length, 4);		
		}


		[Test]
		public void TestGetFaresForSingleDate()
		{
			
			TDDateTime dateTime = new TDDateTime(2005, 11, 03, 09, 30, 0, 0);

			TDLocation origin = new TDLocation();
			TDNaptan originNaPTAN = new TDNaptan("9000ORIGIN", new OSGridReference(0, 0), "Origin");
			origin.NaPTANs = new TDNaptan[] { originNaPTAN };
			
			TDLocation destination = new TDLocation();
			TDNaptan destinationNaPTAN = new TDNaptan("9000DESTN", new OSGridReference(0, 0), "Destination");
			destination.NaPTANs = new TDNaptan[] { destinationNaPTAN };

			CJPSessionInfo info = new CJPSessionInfo();
			
			info.SessionId = "Test session id";
			info.OriginAppDomainFriendlyName = "Test domain";
			info.Language = "en-LN";

			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			CoachFare[] fares = supplier.GetFaresForSingleDate(dateTime, CoachFaresInterfaceType.ForJourney, "NX", origin, destination, info);

			// method mostly just translates input params into a FaresRequest, 
			//  so check first that it's done it correctly ...

			FareRequest request = supplier.FareRequest;

			Assert.AreEqual("9000ORIGIN", request.OriginNaPTAN.Naptan);
			Assert.AreEqual("9000DESTN", request.DestinationNaPTAN.Naptan);

			Assert.AreEqual("Test session id", request.CjpRequestInfo.SessionId);
			Assert.AreEqual("Test domain", request.CjpRequestInfo.OriginAppDomainFriendlyName);
			
			Assert.AreEqual(dateTime, request.OutwardStartDateTime);
			Assert.IsNull(request.OutwardEndDateTime);
			Assert.IsNull(request.ReturnStartDateTime);
			Assert.IsNull(request.ReturnEndDateTime);

			// now check that it reurned all the fares from the mock interface ...

			ICoachFaresInterfaceFactory factory = (ICoachFaresInterfaceFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresInterface];
			IFaresInterface faresInterface = factory.GetFaresInterface(CoachFaresInterfaceType.ForJourney);
			FareResult result = faresInterface.GetCoachFares(request);

			Assert.AreEqual(fares.Length, result.Fares.Length);

		}

		[Test]
		public void TestPriceRoute()
		{
			Discounts discounts;

			TDLocation origin = new TDLocation();
			TDNaptan originNaPTAN = new TDNaptan("9000ORIGIN", new OSGridReference(0, 0), "Origin");
			origin.NaPTANs = new TDNaptan[] { originNaPTAN };
			
			TDLocation destination = new TDLocation();
			TDNaptan destinationNaPTAN = new TDNaptan("9000DESTN", new OSGridReference(0, 0), "Destination");
			destination.NaPTANs = new TDNaptan[] { destinationNaPTAN };

			CJPSessionInfo info = new CJPSessionInfo();
			
			info.SessionId = "Test session id";
			info.OriginAppDomainFriendlyName = "Test domain";
			info.Language = "en-LN";

			ArrayList dates;
			QuotaFareList quotaFareList;

			
			JourneyCoachFareSupplier supplier = new JourneyCoachFareSupplier();			

			
			// TEST CASE 1
			//	one travel date 
			//  no quota fares
			//  discount card present

			dates = CreateTravelDates(1);
			quotaFareList = CreateQuotaFares(1);

			discounts = new Discounts(string.Empty, TestMockFareInterface.DISCOUNTCARDTYPENATEXCOACHWEEKLY, TicketClass.All);			

			string[] errors = supplier.PriceRoute(dates, origin, destination, discounts, info, "NX", 0, 0, quotaFareList);

			Assert.AreEqual(0, errors.Length);
			Assert.AreEqual(2, dates.Count);
			
			TravelDate td = (TravelDate)dates[0];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Return);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(3, td.OutwardTickets.Length);

			CostSearchTicket cst = td.OutwardTickets[0];

			Assert.AreEqual(5,    cst.MinChildAge);
			Assert.AreEqual(18,   cst.MaxChildAge);
			Assert.AreEqual(5.85, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
			Assert.AreEqual(4.50, Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) );
			Assert.AreEqual(5.65, Decimal.Round(Convert.ToDecimal(cst.DiscountedAdultFare),2) );
			Assert.AreEqual(4.20, Decimal.Round(Convert.ToDecimal(cst.DiscountedChildFare),2) );
			Assert.AreEqual(TestMockFareInterface.FARE_TYPE_STANDARD_RETURN, cst.Code);

			td = (TravelDate)dates[1];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Single);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(1, td.OutwardTickets.Length);

			cst = td.OutwardTickets[0];

			Assert.AreEqual(5,    cst.MinChildAge);
			Assert.AreEqual(18,   cst.MaxChildAge);
			Assert.AreEqual(2.60, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
			Assert.AreEqual(0.90, Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) );
			Assert.AreEqual(2.30, Decimal.Round(Convert.ToDecimal(cst.DiscountedAdultFare),2) );
			Assert.AreEqual(0.60, Decimal.Round(Convert.ToDecimal(cst.DiscountedChildFare),2) );
			Assert.AreEqual(TestMockFareInterface.FARE_TYPE_STANDARD_SINGLE, cst.Code);


			// TEST CASE 2
			//	three travel dates (all with unique outward dates) 
			//  one quota fare
			//  no discount card present

			dates = CreateTravelDates(2);
			quotaFareList = CreateQuotaFares(2);

			discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);			

			errors = supplier.PriceRoute(dates, origin, destination, discounts, info, "NX", 0, 0, quotaFareList);

			Assert.AreEqual(0, errors.Length);
			Assert.AreEqual(6, dates.Count);
			
			td = (TravelDate)dates[0];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Return);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(3, td.OutwardTickets.Length);

			cst = td.OutwardTickets[0];

			Assert.AreEqual(5,    cst.MinChildAge);
			Assert.AreEqual(18,   cst.MaxChildAge);
			Assert.AreEqual(5.85, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
			Assert.AreEqual(4.50, Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) );
			Assert.AreEqual(float.NaN, cst.DiscountedAdultFare );
			Assert.AreEqual(float.NaN, cst.DiscountedChildFare );
			Assert.AreEqual(TestMockFareInterface.FARE_TYPE_STANDARD_RETURN, cst.Code);
			Assert.IsFalse(cst.TicketCoachFareData.IsQuotaFare);

			td = (TravelDate)dates[1];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Return);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(3, td.OutwardTickets.Length);

			td = (TravelDate)dates[2];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Return);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(3, td.OutwardTickets.Length);

			// "single" TD's are added at end of existing array, so start at 3 ...
			
			td = (TravelDate)dates[3];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Single);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(2, td.OutwardTickets.Length);

			cst = td.OutwardTickets[0];

			Assert.AreEqual(5,    cst.MinChildAge);
			Assert.AreEqual(18,   cst.MaxChildAge);
			Assert.AreEqual(2.60, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
			Assert.AreEqual(0.90, Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) );
			Assert.AreEqual(float.NaN, cst.DiscountedAdultFare );
			Assert.AreEqual(float.NaN, cst.DiscountedChildFare );
			Assert.AreEqual(TestMockFareInterface.FARE_TYPE_STANDARD_SINGLE, cst.Code);
			Assert.IsFalse(cst.TicketCoachFareData.IsQuotaFare);
			Assert.AreEqual("9000ORIGIN", cst.TicketCoachFareData.OriginNaptan);
			Assert.AreEqual("9000DESTN", cst.TicketCoachFareData.DestinationNaptan);

			cst = td.OutwardTickets[1];

			Assert.AreEqual(2.10, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
			Assert.AreEqual(float.NaN, cst.ChildFare );
			Assert.AreEqual(float.NaN, cst.DiscountedAdultFare );
			Assert.AreEqual(float.NaN, cst.DiscountedChildFare );
			Assert.AreEqual("Fun Fare", cst.Code);

			Assert.IsTrue(cst.TicketCoachFareData.IsQuotaFare);
			Assert.AreEqual("9000QO", cst.TicketCoachFareData.OriginNaptan);
			Assert.AreEqual("9000QD", cst.TicketCoachFareData.DestinationNaptan);
			
			td = (TravelDate)dates[4];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Single);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(2, td.OutwardTickets.Length);

			td = (TravelDate)dates[5];

			Assert.IsTrue(td.HasTickets);
			Assert.AreEqual(td.TicketType, TicketType.Single);
			Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
			Assert.AreEqual(2, td.OutwardTickets.Length);


			// TEST CASE 3
			//	49 travel dates (7 with unique outward dates) 
			//  three quota fares
			//  discount card present

			discounts = new Discounts(string.Empty, TestMockFareInterface.DISCOUNTCARDTYPENATEXCOACHMONTHLY, TicketClass.All);			

			dates = CreateTravelDates(3);
			quotaFareList = CreateQuotaFares(3);

			errors = supplier.PriceRoute(dates, origin, destination, discounts, info, "NX", 0, 0, quotaFareList);

			Assert.AreEqual(0, errors.Length);
			Assert.AreEqual(98, dates.Count);
			
			for (int i = 0; i < 49; i++) 
			{
				td = (TravelDate)dates[i];

				Assert.IsTrue(td.HasTickets);
				Assert.AreEqual(td.TicketType, TicketType.Return);
				Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
				Assert.AreEqual(1, td.OutwardTickets.Length);

				cst = td.OutwardTickets[0];

				Assert.AreEqual(5,    cst.MinChildAge);
				Assert.AreEqual(18,   cst.MaxChildAge);
				Assert.AreEqual(5.85, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
				Assert.AreEqual(4.50, Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) );
				Assert.AreEqual(5.70, Decimal.Round(Convert.ToDecimal(cst.DiscountedAdultFare),2) );
				Assert.AreEqual(4.30, Decimal.Round(Convert.ToDecimal(cst.DiscountedChildFare),2) );
				Assert.AreEqual(TestMockFareInterface.FARE_TYPE_STANDARD_RETURN, cst.Code);
				Assert.IsFalse(cst.TicketCoachFareData.IsQuotaFare);
			}
	
			// "single" TD's are added at end of existing array, so start at 49 ...
			
			for (int i = 49; i < 98; i++) 
			{
				td = (TravelDate)dates[i];

				Assert.IsTrue(td.HasTickets);
				Assert.AreEqual(td.TicketType, TicketType.Single);
				Assert.AreEqual(td.TravelMode, TicketTravelMode.Coach);
				
				// one ordinary (discounted) fare, plus three quota-based ...
				Assert.AreEqual(4, td.OutwardTickets.Length);
	
				cst = td.OutwardTickets[0];

				Assert.AreEqual(5,    cst.MinChildAge);
				Assert.AreEqual(18,   cst.MaxChildAge);
				Assert.AreEqual(2.60, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
				Assert.AreEqual(0.90, Decimal.Round(Convert.ToDecimal(cst.ChildFare),2) );
				Assert.AreEqual(2.40, Decimal.Round(Convert.ToDecimal(cst.DiscountedAdultFare),2) );
				Assert.AreEqual(0.70, Decimal.Round(Convert.ToDecimal(cst.DiscountedChildFare),2) );
				Assert.AreEqual(TestMockFareInterface.FARE_TYPE_STANDARD_SINGLE, cst.Code);
				Assert.IsFalse(cst.TicketCoachFareData.IsQuotaFare);
				Assert.AreEqual("9000ORIGIN", cst.TicketCoachFareData.OriginNaptan);
				Assert.AreEqual("9000DESTN", cst.TicketCoachFareData.DestinationNaptan);

				cst = td.OutwardTickets[1];

				Assert.AreEqual(1.00, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
				Assert.AreEqual(float.NaN, cst.ChildFare );
				Assert.AreEqual(float.NaN, cst.DiscountedAdultFare );
				Assert.AreEqual(float.NaN, cst.DiscountedChildFare );
				Assert.AreEqual("Fun Fare 100", cst.Code);

				Assert.IsTrue(cst.TicketCoachFareData.IsQuotaFare);
				Assert.AreEqual("9000QO", cst.TicketCoachFareData.OriginNaptan);
				Assert.AreEqual("9000QD", cst.TicketCoachFareData.DestinationNaptan);

				cst = td.OutwardTickets[2];

				Assert.AreEqual(2.00, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
				Assert.AreEqual(float.NaN, cst.ChildFare );
				Assert.AreEqual(float.NaN, cst.DiscountedAdultFare );
				Assert.AreEqual(float.NaN, cst.DiscountedChildFare );
				Assert.AreEqual("Fun Fare 200", cst.Code);

				Assert.IsTrue(cst.TicketCoachFareData.IsQuotaFare);
				Assert.AreEqual("9000QO", cst.TicketCoachFareData.OriginNaptan);
				Assert.AreEqual("9000QD", cst.TicketCoachFareData.DestinationNaptan);
				
				cst = td.OutwardTickets[3];

				Assert.AreEqual(3.00, Decimal.Round(Convert.ToDecimal(cst.AdultFare),2) );
				Assert.AreEqual(float.NaN, cst.ChildFare );
				Assert.AreEqual(float.NaN, cst.DiscountedAdultFare );
				Assert.AreEqual(float.NaN, cst.DiscountedChildFare );
				Assert.AreEqual("Fun Fare 300", cst.Code);

				Assert.IsTrue(cst.TicketCoachFareData.IsQuotaFare);
				Assert.AreEqual("9000QO", cst.TicketCoachFareData.OriginNaptan);
				Assert.AreEqual("9000QD", cst.TicketCoachFareData.DestinationNaptan);
		
			}

		}

		#endregion

		#region Helper methods

		private ArrayList CreateTravelDates(int testCase)
		{
			ArrayList results = new ArrayList();

			switch (testCase)
			{
				case 1:
				{
					TravelDate td = new TravelDate();
					td.TravelMode = TicketTravelMode.Coach;
					td.OutwardDate = new TDDateTime(2005, 11, 03);
					td.ReturnDate = new TDDateTime(2005, 11, 03);

					results.Add(td);

					break;
				}

				case 2:
				{
					TravelDate td = new TravelDate();
					td.TravelMode = TicketTravelMode.Coach;
					td.OutwardDate = new TDDateTime(2005, 11, 03);
					td.ReturnDate = new TDDateTime(2005, 11, 03);

					results.Add(td);

					td = new TravelDate();
					td.TravelMode = TicketTravelMode.Coach;
					td.OutwardDate = new TDDateTime(2005, 11, 04);
					td.ReturnDate = new TDDateTime(2005, 11, 04);

					results.Add(td);

					td = new TravelDate();
					td.TravelMode = TicketTravelMode.Coach;
					td.OutwardDate = new TDDateTime(2005, 11, 05);
					td.ReturnDate = new TDDateTime(2005, 11, 05);

					results.Add(td);

					break;
				}

				case 3:
				{
					for (int outwardDay = 1; outwardDay < 8; outwardDay++) 
					{
						for (int returnDay = 15; returnDay < 22; returnDay++) 
						{
							TravelDate td = new TravelDate();
							td.TravelMode = TicketTravelMode.Coach;
							td.OutwardDate = new TDDateTime(2005, 11, outwardDay);
							td.ReturnDate = new TDDateTime(2005, 11, returnDay);
							results.Add(td);
						}
					}
					break;
				}
			}

			return results;
		}

		private QuotaFareList CreateQuotaFares(int testCase)
		{
			QuotaFareList results = new QuotaFareList();

			switch (testCase)
			{
				case 1:
				{
					// nothing - list is empty
					break;
				}

				case 2:
				{
					QuotaFare qf = new QuotaFare();

					qf.OriginNaPTAN = "9000QO";
					qf.DestinationNaPTAN = "9000QD";
					qf.Fare = 210;
					qf.TicketType = "Fun Fare";
					results.Add(qf);	
					break;
				}

			
				case 3:
				{
					QuotaFare qf = new QuotaFare();

					qf.OriginNaPTAN = "9000QO";
					qf.DestinationNaPTAN = "9000QD";
					qf.Fare = 100;
					qf.TicketType = "Fun Fare 100";
					results.Add(qf);	

					qf = new QuotaFare();

					qf.OriginNaPTAN = "9000QO";
					qf.DestinationNaPTAN = "9000QD";
					qf.Fare = 200;
					qf.TicketType = "Fun Fare 200";
					results.Add(qf);	
					
					qf = new QuotaFare();

					qf.OriginNaPTAN = "9000QO";
					qf.DestinationNaPTAN = "9000QD";
					qf.Fare = 300;
					qf.TicketType = "Fun Fare 300";
					results.Add(qf);	

					break;
				}

			
			}

			return results;
		}
		
		#endregion



	}
}
