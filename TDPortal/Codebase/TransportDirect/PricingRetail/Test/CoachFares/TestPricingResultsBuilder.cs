//********************************************************************************
//NAME         : TestPricingResultsBuilder.cs
//AUTHOR       : Alistair Caunt (Moved from NatexFares to CoachFares by Murat Guney)
//DATE CREATED : 22/10/2003
//DESCRIPTION  : Implementation of TestPricingResultsBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestPricingResultsBuilder.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:22   mturner
//Initial revision.
//
//   Rev 1.10   May 25 2007 16:22:14   build
//Automatically merged from branch for stream4401
//
//   Rev 1.9.1.0   May 10 2007 16:33:14   asinclair
//Added new bool value used for new nx fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9   Dec 23 2005 15:48:50   mguney
//GetSampleFare method changed to create CoachFareForRoute.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.8   Dec 06 2005 11:29:00   mguney
//AvailabilityScore changed to Probability.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.7   Nov 29 2005 20:28:20   mguney
//Changed for Exceptional Fares.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.6   Nov 04 2005 16:19:10   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Nov 03 2005 19:21:20   RPhilpott
//Merge undiscounted and discounted CoachFareData into one.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Nov 01 2005 17:24:48   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 29 2005 16:55:46   mguney
//Fare initialisations changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 28 2005 15:55:52   mguney
//Static constants changed to normal constants.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 22 2005 16:25:16   mguney
//Associated IR
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 22 2005 16:05:28   mguney
//Initial revision.

//*********
//   Rev 1.5   Mar 30 2005 17:06:56   jgeorge
//Slight modification to ticket names for NatEx tickets
//
//   Rev 1.4   Feb 07 2005 16:37:16   RScott
//Assertion changed to Assert
//
//   Rev 1.3   Jun 11 2004 15:37:48   acaunt
//New test added for SCL tickets to ensure that NatEx and SCL ticket names are correctly generated.
//
//   Rev 1.2   May 28 2004 13:53:28   acaunt
//Updated to accomodate real NatEx and SCL data and with revised display rules.
//
//   Rev 1.1   Oct 22 2003 23:09:48   acaunt
//Implemented tests
//
//   Rev 1.0   Oct 22 2003 13:50:36   acaunt
//Initial Revision

using System;
using NUnit.Framework;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.JourneyPlanning.FaresProviderInterface;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{

	[TestFixture]
	public class TestPricingResultsBuilder
	{

		private const string NODISCOUNT = "";
		private const string STUDENTDISCOUNT = "National Express Student Coachcard";
		private const string ADULTDISCOUNT = "National Express Adult Coachcard";
		private const string CHILDDISCOUNT = "Child Card";
		private const string FARETYPESTANDARD = "Standard";
		
		private CoachFare adultSing;
		private CoachFare adultRet;
		private CoachFare childRet;
		private CoachFare adultAltRet;
		private CoachFare adultDis1;
		private CoachFare adultDis2;
		private CoachFare childDis1;

		PricingResultsBuilder builder; 		

		

		public TestPricingResultsBuilder()
		{		
			const int BASEFARE = 3200;

			// Initialise property service etc.
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());

			adultSing = GetSampleFare(true,true,BASEFARE,FARETYPESTANDARD,NODISCOUNT);
			adultRet = GetSampleFare(true,false,BASEFARE*2-100,FARETYPESTANDARD,NODISCOUNT);
			childRet = GetSampleFare(false,false,(BASEFARE*2 - 100)/2+100,FARETYPESTANDARD,NODISCOUNT);
			adultAltRet = GetSampleFare(true,false,(int)BASEFARE*3/2,FARETYPESTANDARD,NODISCOUNT);
			adultDis1 = GetSampleFare(true,false,(int)BASEFARE*3/2,FARETYPESTANDARD,STUDENTDISCOUNT);
			adultDis2 = GetSampleFare(true,false,(int)BASEFARE*3/2,FARETYPESTANDARD,ADULTDISCOUNT);
			childDis1 = GetSampleFare(false,false,(int)BASEFARE/3+100,FARETYPESTANDARD,CHILDDISCOUNT);

		}

		[SetUp]
			/// <summary>
			/// Create the builder and add three undiscounted adult fares to the fares builder
			/// Of the three fares, two have the same fares name, and a different two have the same passenger type.
			/// From this we would expect three different ticket names.
			/// </summary>
		public void Init()
		{	
			builder = new PricingResultsBuilder(false,false);	   
			builder.AddUndiscountedFare(adultSing, "NX", false);
			builder.AddUndiscountedFare(adultAltRet, "NX", false);
			builder.AddUndiscountedFare(adultRet, "NX", false);
		}

		[TearDown]
		public void CleanUp()
		{
		}

		

		/// <summary>
		/// Confirm that we can't add discounted fares until discount cards have been added and then once discount cards have 
		/// been added, that we can't add further undiscounted fares
		/// </summary>
		[Test]
		public void TestValidOperations()
		{
			try 
			{
				builder.AddDiscountedFare(adultDis1, "NX", false);
				Assert.Fail("Exception not thrown when adding a discounted CoachFare before adding discount cards");
			} 
			catch (InvalidOperationException)
			{
				// Okay, this is expected
			}
			builder.AddDiscountCard(CHILDDISCOUNT);
			try 
			{
				builder.AddUndiscountedFare(adultRet, "NX", false);
				Assert.Fail("Exception not thrown when adding an undiscounted CoachFare after adding discount cards");
			}
			catch (InvalidOperationException)
			{
				// Okay, this is expected
			}
		}
		/// <summary>
		/// Confirm that when adding a CoachFare with adult characteristics, it is recognised as such and has the correct ticket name
		/// </summary>
		[Test]
		public void TestAddingAdultUndiscountedFare()
		{
			Hashtable pricingResults = builder.GetPricingResults();
			// Check discount card related details
			ConfirmDiscountCards(1, pricingResults.Count);
			Assert.IsTrue(pricingResults.ContainsKey(NODISCOUNT), "No pricing result created for undiscounted card");
			PricingResult pricingResult = (PricingResult)pricingResults[NODISCOUNT];
			// Check the ticket details
			ConfirmNumberTickets(NODISCOUNT, 3, ((IList)pricingResult.Tickets).Count);
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[2], false);
		}

		/// <summary>
		/// Confirm that when adding a CoachFare with child characteristics is recognised as such and has the correct ticket name
		/// </summary>
		[Test]
		public void TestAddingChildUndiscountedFare()
		{
			// Add the additional fare
			builder.AddUndiscountedFare(childRet, "NX", false);

			Hashtable pricingResults = builder.GetPricingResults();
			// Check discount card related details
			ConfirmDiscountCards(1, pricingResults.Count);
			Assert.IsTrue(pricingResults.ContainsKey(NODISCOUNT), "No pricing result created for undiscounted card");
			PricingResult pricingResult = (PricingResult)pricingResults[NODISCOUNT];
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmNumberTickets(NODISCOUNT, 4, ((IList)pricingResult.Tickets).Count);
			
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], false);
			ConfirmTicket(childRet, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[2], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[3], false);
			
			ConfirmChildAges(childRet, pricingResult);
		}

		/// <summary>
		/// Confirm that adding discount cards has the effect of creating different PricingResults (one per card type + one for no discount)
		/// </summary>
		[Test]
		public void TestAddingDiscountCard()
		{
			// Add the discount cards
			this.AddDiscountCards();

			Hashtable pricingResults = builder.GetPricingResults();
			// Check that we have the correct number of PricingResults - it should be one per discount card
			ConfirmDiscountCards(3, pricingResults.Count);
			// Check that we have three tickets in each PricingResult
			foreach (string card in pricingResults.Keys)
			{
				ConfirmNumberTickets(card, 3, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
			}
		}

		/// <summary>
		/// Confirm that adding a discounted adult CoachFare increases the number of tickets for the matching discount card, but doesn't affect the other 'sets'
		/// of tickets.
		/// </summary>
		[Test]
		public void TestAddingAdultDiscountedFare()
		{
			// Add the discount cards and additional fare
			this.AddDiscountCards();
			builder.AddDiscountedFare(adultDis1, "NX", false);

			Hashtable pricingResults = builder.GetPricingResults();
			// The new ticket has an discount card of National Express Student Coachcard
			// Check that we now have four tickets in the associated PricingResult, but still three tickets in the other PricingResult
			foreach (string card in pricingResults.Keys)
			{
				if (card.Equals(STUDENTDISCOUNT)) 
				{
					ConfirmNumberTickets(card, 4, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
				else
				{
					ConfirmNumberTickets(card, 3, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
			}
			// Check that the ticket values in the modified PricingResult are correct
			PricingResult pricingResult = (PricingResult)pricingResults[STUDENTDISCOUNT];
			// Check ticket details and ordering (cheapest to most expensive)

			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], false);
			ConfirmTicket(adultDis1, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[2], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[3],false);
		}

		/// <summary>
		/// Confirm that adding a discounted child CoachFare increases the number of tickets of the PricingResult with the matching
		/// discount card, but doesn't affect the other PricingResults.
		/// </summary>
		[Test]
		public void TestAddingChildDiscountedFare()
		{
			// Add the discount cards and additional fare
			this.AddDiscountCards();
			builder.AddDiscountedFare(childDis1, "NX", false);

			Hashtable pricingResults = builder.GetPricingResults();
			// The new ticket has an discount card of Super Child
			// Check that we now have four tickets in the associated PricingResult, but still three tickets in the other PricingResult
			foreach (string card in pricingResults.Keys)
			{
				if (card.Equals(CHILDDISCOUNT)) 
				{
					ConfirmNumberTickets(card, 4, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
				else
				{
					ConfirmNumberTickets(card, 3, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
			}
			// Check that the ticket values in the modified PricingResult are correct
			PricingResult pricingResult = (PricingResult)pricingResults[CHILDDISCOUNT];
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmTicket(childDis1, (Ticket)pricingResult.Tickets[0],false);
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[2], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[3], false);
		}

		/// <summary>
		/// Check that when added an unrecognised discounted CoachFare this doesn't alter any of the PricingResults
		/// </summary>
		[Test]
		public void TestAddingUnrecognisedAdultDiscountedFare()
		{
			// Add the discount cards and additional fare
			this.AddDiscountCards();
			builder.AddDiscountedFare(adultDis2, "NX", false);

			Hashtable pricingResults = builder.GetPricingResults();
			// The new ticket has a discount card that is not recognised
			// Check that we still have three tickets in each of the PricingResults
			ConfirmDiscountCards(3, pricingResults.Count);
			// Check that we have three tickets in each PricingResult
			foreach (string card in pricingResults.Keys)
			{
				ConfirmNumberTickets(card, 3, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
			}
		}

		/// <summary>
		/// SCL fares are handled the same way as NatEx fares except that the ticket name is generated using the
		/// fareType field only (rather than the fareType and passengerType fields).
		/// </summary>
		[Test]
		public void TestSclFareName() 
		{
			PricingResultsBuilder sclBuilder = new PricingResultsBuilder(false,false);
			sclBuilder.AddUndiscountedFare(adultSing, "NX", false);
			Hashtable pricingResults = sclBuilder.GetPricingResults();

			// Check discount card related details
			ConfirmDiscountCards(1, pricingResults.Count);
			Assert.IsTrue(pricingResults.ContainsKey(NODISCOUNT), "No pricing result created for undiscounted card");
			PricingResult pricingResult = (PricingResult)pricingResults[NODISCOUNT];
			// Check the ticket details
			ConfirmNumberTickets(NODISCOUNT, 1, ((IList)pricingResult.Tickets).Count);
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], true);
		}

		#region Helper methods to update the coach CoachFare data
		/// <summary>
		/// Add undiscounted adult fares to the fares builder. This should introduce 2 new 'sets' of tickets
		/// </summary>
		private void AddDiscountCards()
		{
			builder.AddDiscountCard(STUDENTDISCOUNT);
			builder.AddDiscountCard(CHILDDISCOUNT);
		}
		#endregion

		#region Helper methods to assist with comparing expected and obtained values

		/// <summary>
		/// Get sample fare data.
		/// </summary>
		/// <param name="adult"></param>
		/// <param name="single"></param>
		/// <param name="fareValue"></param>
		/// <param name="fareType"></param>
		/// <param name="discountCardType"></param>
		/// <returns></returns>
		private CoachFare GetSampleFare(bool adult,bool single,int fareValue,string fareType,string discountCardType)
		{
			CoachFareForRoute fare = new CoachFareForRoute();
			fare.IsAdult = adult;
			fare.ChildAgeMin = 0;
			fare.ChildAgeMax = 18;
			fare.DiscountCardType = discountCardType;
			fare.Fare = fareValue;
			fare.Flexibility = Flexibility.LimitedFlexibility;
			fare.Availability = Probability.High;
			fare.FareType = fareType;
			fare.PassengerType = "Economy";
			fare.PassengerTypeConditions = string.Empty;
			fare.IsSingle = single;

			fare.OriginNaPTAN = new TDNaptan("9000ORIGIN", new OSGridReference(0, 0), "Origin");
			fare.DestinationNaPTAN = new TDNaptan("9000DESTN", new OSGridReference(0, 0), "Destination");
	
			return fare;
		}

		/// <summary>
		/// Confirm that the correct number of discount card sets have been created
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="obtained"></param>
		private void ConfirmDiscountCards(int expected, int obtained)
		{
			Assert.AreEqual(expected, obtained, "Incorrect number of discount card sets created");
		}

		/// <summary>
		/// Confirm that the correct number of tickets has been created
		/// </summary>
		/// <param name="discountCard"></param>
		/// <param name="expected"></param>
		/// <param name="obtained"></param>
		private void ConfirmNumberTickets(string discountCard, int expected, int obtained)
		{
			Assert.AreEqual(expected, obtained, "Incorrect number of tickets obtained for card ["+discountCard+"]");
		}

		/// <summary>
		/// Confirm that the details of the ticket match those of the source fare
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		private void ConfirmTicket(CoachFare fare, Ticket ticket, bool isSCL)
		{
			ConfirmTicketName(fare, ticket, isSCL);
			ConfirmFares(fare, ticket);
			ConfirmRestriction(fare, ticket);
		}
		/// <summary>
		/// Check that the name of a Ticket object are correct, based on the fields of the source CoachFare object
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private void ConfirmTicketName(CoachFare fare, Ticket ticket, bool isSCL)
		{
			if (isSCL) 
				Assert.AreEqual(fare.FareType, ticket.Code, "Ticket name incorrect for "+GetFareDescription(fare));
			else
				Assert.AreEqual(fare.FareType, ticket.Code, "Ticket name incorrect for "+GetFareDescription(fare));
		}

		/// <summary>
		/// Check that the CoachFare fields of a Ticket object are correct, based on the fields of the source CoachFare object
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private void ConfirmFares(CoachFare fare, Ticket ticket)
		{
			float fareValue = fare.Fare/100f;
			if (fare.DiscountCardType.Equals(NODISCOUNT))
			{
				if (fare.IsAdult) 
				{
					Assert.AreEqual(fareValue, ticket.AdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.ChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare));
					Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
				} 
				else
				{
					Assert.AreEqual(float.NaN, ticket.AdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(fareValue, ticket.ChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare));
					Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
				}
			}
				// For a discounted fare
			else 
			{
				if (fare.IsAdult) 
				{
					Assert.AreEqual(float.NaN, ticket.AdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.ChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(fareValue, ticket.DiscountedAdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
				} 
				else
				{
					Assert.AreEqual(float.NaN, ticket.AdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare));  
					Assert.AreEqual(float.NaN, ticket.ChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare));  
					Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Adult CoachFare incorrect for "+GetFareDescription(fare));  
					Assert.AreEqual(fareValue, ticket.DiscountedChildFare, "Adult CoachFare incorrect for "+GetFareDescription(fare)); 
				}
			}
		}

		/// <summary>
		/// Check that the restiction field of a Ticket object is correct, based on the fareRestrictionType field of the source CoachFare object
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private void ConfirmRestriction(CoachFare fare, Ticket ticket)
		{
			Assert.AreEqual(fare.Flexibility,ticket.Flexibility, "Flexibility incorrect for "+GetFareDescription(fare));			
		}

		/// <summary>
		/// For child tickets check that the minimum and maximum age fields are correct
		/// </summary>
		private void ConfirmChildAges(CoachFare fare, PricingResult result)
		{
			if (!fare.IsAdult)
			{				
				Assert.AreEqual(fare.ChildAgeMin, result.MinChildAge, "Min child age incorrect for "+GetFareDescription(fare));
				Assert.AreEqual(fare.ChildAgeMax, result.MaxChildAge, "Max child age incorrect for "+GetFareDescription(fare));
			}
		}
		#endregion

		#region Helper methods for displaying errors
		private string GetFareDescription(CoachFare fare)
		{
			return fare.FareType+"/"+fare.PassengerType+" and card "+fare.DiscountCardType+".";
		}
		#endregion
	}
}

