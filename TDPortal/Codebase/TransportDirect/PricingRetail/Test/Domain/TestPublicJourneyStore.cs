// ************************************************************** 
// NAME			: TestPublicJourneyStore.cs
// AUTHOR		: James Broome
// DATE CREATED	: 25/04/2005 
// DESCRIPTION	: Test implementation of the PublicJourneyStore class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestPublicJourneyStore.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:26   mturner
//Initial revision.
//
//   Rev 1.1   Aug 25 2005 14:41:18   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.0   Apr 26 2005 17:33:20   jbroome
//Initial revision.

using NUnit.Framework;

using System;
using System.Collections;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

using CJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Test class that tests the public properties and methods of the PublicJourneyStore class
	/// </summary>
	[TestFixture]
	public class TestPublicJourneyStore
	{
		
		# region Private members

		private static TDDateTime date1 = new TDDateTime(2005, 01, 01);
		private static TDDateTime date2 = new TDDateTime(2005, 01, 02);
		private static TDDateTime date3 = new TDDateTime(2005, 01, 03);
		private static TDDateTime date4 = new TDDateTime(2005, 01, 04);
		private static TDDateTime date5 = new TDDateTime(2005, 01, 05);
		private static TDDateTime date6 = new TDDateTime(2005, 01, 06);
		private static TDDateTime date7 = new TDDateTime(2005, 01, 07);
		private static TDDateTime date8 = new TDDateTime(2005, 01, 08);
		private static TDDateTime date9 = new TDDateTime(2005, 01, 09);
		private static TDDateTime date10 = new TDDateTime(2005, 01, 10);

		private TDDateTime[] outwardDates = new TDDateTime[6] {date1, date2, date3, date4, date5, date6};
		private TDDateTime[] returnDates = new TDDateTime[7] {date4, date5, date6, date7, date8, date9, date10};

		#endregion

		/// <summary>
		/// Default constructor
		/// </summary>
		public TestPublicJourneyStore()
		{
		}

		#region Test methods

		/// <summary>
		/// Performs necessary initialisation routines.
		/// </summary>
		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			// Initialise property service etc. 
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
		}

		/// <summary>
		/// Test method tests that a PublicJourneyStore object can be created sucessfully
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			ArrayList outwardJourneys = new ArrayList();
			ArrayList returnJourneys = new ArrayList();
			PublicJourneyStore journeyStore;

			SetUpOutwardJourneys(outwardJourneys);
			// Outward journeys only
			journeyStore = new PublicJourneyStore(outwardJourneys, returnJourneys);
			Assert.AreEqual(6, journeyStore.OutwardDates.Length, "OutwardDates collection not created correctly");
			Assert.AreEqual(6, journeyStore.GetOutwardJourneys().Length, "GetOutwardJourneys collection not created correctly");
			Assert.AreEqual(0, journeyStore.ReturnDates.Length, "ReturnDates collection not created correctly");
			Assert.AreEqual(0, journeyStore.GetReturnJourneys().Length, "GetReturnJourneys collection not created correctly");
		
			SetUpReturnJourneys(returnJourneys);	
			// Outward and return journeys
			journeyStore = new PublicJourneyStore(outwardJourneys, returnJourneys);
			Assert.AreEqual(6, journeyStore.OutwardDates.Length, "OutwardDates collection not created correctly");
			Assert.AreEqual(6, journeyStore.GetOutwardJourneys().Length, "GetOutwardJourneys collection not created correctly");
			Assert.AreEqual(7, journeyStore.ReturnDates.Length, "ReturnDates collection not created correctly");
			Assert.AreEqual(7, journeyStore.GetReturnJourneys().Length, "GetReturnJourneys collection not created correctly");
			
		}

		/// <summary>
		/// Test method tests the GetDateIndex, GetOutwardJourneysForDate and GetIndexforJourney 
		/// methods of the PublicJourneyStore class. Deals with the collections of outward journeys.
		/// </summary>
		[Test]
		public void TestOutwardJourneys()
		{
			ArrayList outwardJourneys = new ArrayList();
			ArrayList returnJourneys = new ArrayList();
			SetUpOutwardJourneys(outwardJourneys);

			PublicJourneyStore journeyStore = new PublicJourneyStore(outwardJourneys, returnJourneys);

			for (int i=0; i<outwardDates.Length; i++)
			{
				int dateIndex = journeyStore.GetDateIndex(outwardDates[i], true);
				PublicJourney[] journeys = journeyStore.GetOutwardJourneysForDate(dateIndex);
				Assert.AreEqual(i+1, journeys.Length, string.Format("GetOutwardJourneysForDate returned wrong size collection for date {0}.", i));
				for (int j=0; j<journeys.Length; j++)
				{
					Assert.AreEqual(j, journeyStore.GetIndexForJourney(dateIndex, true, journeys[j]), string.Format("GetIndexForJourney returned incorrect index for journey {0} with date index {1}.", j, dateIndex));
				}
			}
		}		

		/// <summary>
		/// Test method tests the GetDateIndex, GetReturnJourneysForDate and GetIndexforJourney 
		/// methods of the PublicJourneyStore class. Deals with the collections of return journeys.
		/// </summary>
		public void TestReturnJourneys()
		{
			ArrayList outwardJourneys = new ArrayList();
			ArrayList returnJourneys = new ArrayList();
			SetUpReturnJourneys(returnJourneys);

			PublicJourneyStore journeyStore = new PublicJourneyStore(outwardJourneys, returnJourneys);

			for (int i=0; i<returnDates.Length; i++)
			{
				int dateIndex = journeyStore.GetDateIndex(returnDates[i], false);
				PublicJourney[] journeys = journeyStore.GetReturnJourneysForDate(dateIndex);
				Assert.AreEqual(i+4, journeys.Length, string.Format("GetReturnJourneysForDate returned wrong size collection for date {0}.", i));
				for (int j=0; j<journeys.Length; j++)
				{
					Assert.AreEqual(j, journeyStore.GetIndexForJourney(dateIndex, false, journeys[j]), string.Format("GetIndexForJourney returned incorrect index for journey {0} with date index {1}.", j, dateIndex));
				}
			}
		}
		
		#endregion

		#region Non-test helper methods

		/// <summary>
		/// Method populates an array list with a collection
		/// of PublicJourney objects
		/// </summary>
		/// <param name="outwardJourneys">ArrayList of PublicJourneys</param>
		private void SetUpOutwardJourneys(ArrayList outwardJourneys)
		{
			outwardJourneys.Add(GetTestJourney(date6));
			outwardJourneys.Add(GetTestJourney(date2));
			outwardJourneys.Add(GetTestJourney(date2));
			outwardJourneys.Add(GetTestJourney(date3));
			outwardJourneys.Add(GetTestJourney(date6));
			outwardJourneys.Add(GetTestJourney(date3));
			outwardJourneys.Add(GetTestJourney(date6));
			outwardJourneys.Add(GetTestJourney(date6));
			outwardJourneys.Add(GetTestJourney(date3));
			outwardJourneys.Add(GetTestJourney(date4));
			outwardJourneys.Add(GetTestJourney(date4));
			outwardJourneys.Add(GetTestJourney(date4));
			outwardJourneys.Add(GetTestJourney(date5));
			outwardJourneys.Add(GetTestJourney(date6));
			outwardJourneys.Add(GetTestJourney(date4));
			outwardJourneys.Add(GetTestJourney(date1));
			outwardJourneys.Add(GetTestJourney(date5));
			outwardJourneys.Add(GetTestJourney(date5));
			outwardJourneys.Add(GetTestJourney(date6));
			outwardJourneys.Add(GetTestJourney(date5));
			outwardJourneys.Add(GetTestJourney(date5));
		}

		/// <summary>
		/// Method populates an array list with a collection
		/// of PublicJourney objects
		/// </summary>
		/// <param name="returnJourneys">ArrayList of PublicJourneys</param>
		private void SetUpReturnJourneys(ArrayList returnJourneys)
		{
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date6));
			returnJourneys.Add(GetTestJourney(date6));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date4));
			returnJourneys.Add(GetTestJourney(date5));
			returnJourneys.Add(GetTestJourney(date4));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date9));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date6));
			returnJourneys.Add(GetTestJourney(date4));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date7));
			returnJourneys.Add(GetTestJourney(date6));
			returnJourneys.Add(GetTestJourney(date6));
			returnJourneys.Add(GetTestJourney(date6));
			returnJourneys.Add(GetTestJourney(date8));
			returnJourneys.Add(GetTestJourney(date4));
			returnJourneys.Add(GetTestJourney(date5));
			returnJourneys.Add(GetTestJourney(date5));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date5));
			returnJourneys.Add(GetTestJourney(date10));
			returnJourneys.Add(GetTestJourney(date5));
		}

		/// <summary>
		/// Private non-test helper method.
		/// Creates a PublicJourney object and sets only the 
		/// necessary properties which are tested. 
		/// </summary>
		/// <param name="journeyDate">Date of journey</param>
		/// <returns>PublicJourney object</returns>
		private PublicJourney GetTestJourney(TDDateTime journeyDate)
		{
			PublicJourney journey = new PublicJourney();
			// Create journey legs
			PublicJourneyDetail[] details = new PublicJourneyDetail[1];
			CJP.FrequencyLeg leg = new CJP.FrequencyLeg();
			leg.board = new CJP.Event();
			leg.board.stop = new CJP.Stop();
			leg.board.stop.name = string.Empty;
			leg.alight = new CJP.Event();
			leg.alight.stop = new CJP.Stop();
			leg.alight.stop.name = string.Empty;
			
			details[0] = PublicJourneyDetail.Create(leg, null);
			details[0].Services = new ServiceDetails[1];
			details[0].Services[0] = new ServiceDetails(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

			// Create journey fares
			CJP.PricingUnit[] fares = new CJP.PricingUnit[1];
			fares[0] = new CJP.PricingUnit();
			fares[0].legs = new int[1];
			fares[0].legs[0] = 0;
		
			journey.Details = details;
			journey.Fares = fares;
			journey.JourneyDate = journeyDate;

			return journey;
		}

		#endregion

	}
}
