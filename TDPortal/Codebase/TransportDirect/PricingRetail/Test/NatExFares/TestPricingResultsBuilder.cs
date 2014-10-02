//********************************************************************************
//NAME         : TestPricingResultsBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 22/10/2003
//DESCRIPTION  : Implementation of TestPricingResultsBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestPricingResultsBuilder.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:32   mturner
//Initial revision.
//
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
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{

	[TestFixture]
	public class TestPricingResultsBuilder
	{

		public static string NO_DISCOUNT = string.Empty;
		public static string STUDENT_DISCOUNT = "National Express Student Coachcard";
		public static string CHILD_DISCOUNT = "Child Card";

		private Fare adultSing = TestSampleJourneyData.NatExFaresDovNot.prices[0];
		private Fare adultRet = TestSampleJourneyData.NatExFaresDovNot.prices[2];
		private Fare childRet = TestSampleJourneyData.NatExFaresDovNot.prices[3];
		private Fare adultAltRet = TestSampleJourneyData.NatExFaresDovNot.prices[4];
		private Fare adultDis1 = TestSampleJourneyData.NatExFaresDovNot.prices[5];
		private Fare adultDis2 = TestSampleJourneyData.NatExFaresDovNot.prices[6];
		private Fare childDis1 = TestSampleJourneyData.NatExFaresDovNot.prices[7];
		PricingResultsBuilder builder; 		

		public TestPricingResultsBuilder()
		{		
		}

		[SetUp]
		/// <summary>
		/// Create the builder and add three undiscounted adult fares to the fares builder
		/// Of the three fares, two have the same fares name, and a different two have the same passenger type.
		/// From this we would expect three different ticket names.
		/// </summary>
		public void Init()
		{	
			builder = new PricingResultsBuilder(new NatExTicketNameRule());	   
			builder.AddUndiscountedFare(adultSing);
			builder.AddUndiscountedFare(adultAltRet);
			builder.AddUndiscountedFare(adultRet);
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
				builder.AddDiscountedFare(adultDis1);
				Assert.Fail("Exception not thrown when adding a discounted fare before adding discount cards");
			} 
			catch (Exception)
			{
				// Okay, this is expected
			}
			builder.AddDiscountCard(CHILD_DISCOUNT);
			try 
			{
				builder.AddUndiscountedFare(adultRet);
				Assert.Fail("Exception not thrown when adding an undiscounted fare after adding discount cards");
			}
			catch (Exception)
			{
				// Okay, this is expected
			}
		}
		/// <summary>
		/// Confirm that when adding a fare with adult characteristics, it is recognised as such and has the correct ticket name
		/// </summary>
		[Test]
		public void TestAddingAdultUndiscountedFare()
		{
			Hashtable pricingResults = builder.GetPricingResults();
			// Check discount card related details
			ConfirmDiscountCards(1, pricingResults.Count);
			Assert.IsTrue(pricingResults.ContainsKey(NO_DISCOUNT), "No pricing result created for undiscounted card");
			PricingResult pricingResult = (PricingResult)pricingResults[NO_DISCOUNT];
			// Check the ticket details
			ConfirmNumberTickets(NO_DISCOUNT, 3, ((IList)pricingResult.Tickets).Count);
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[2], false);
		}

		/// <summary>
		/// Confirm that when adding a fare with child characteristics is recognised as such and has the correct ticket name
		/// </summary>
		[Test]
		public void TestAddingChildUndiscountedFare()
		{
			// Add the additional fare
			builder.AddUndiscountedFare(childRet);

			Hashtable pricingResults = builder.GetPricingResults();
			// Check discount card related details
			ConfirmDiscountCards(1, pricingResults.Count);
			Assert.IsTrue(pricingResults.ContainsKey(NO_DISCOUNT), "No pricing result created for undiscounted card");
			PricingResult pricingResult = (PricingResult)pricingResults[NO_DISCOUNT];
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmNumberTickets(NO_DISCOUNT, 4, ((IList)pricingResult.Tickets).Count);
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
		/// Confirm that adding a discounted adult fare increases the number of tickets for the matching discount card, but doesn't affect the other 'sets'
		/// of tickets.
		/// </summary>
		[Test]
		public void TestAddingAdultDiscountedFare()
		{
			// Add the discount cards and additional fare
			this.AddDiscountCards();
			builder.AddDiscountedFare(adultDis1);

			Hashtable pricingResults = builder.GetPricingResults();
			// The new ticket has an discount card of National Express Student Coachcard
			// Check that we now have four tickets in the associated PricingResult, but still three tickets in the other PricingResult
			foreach (string card in pricingResults.Keys)
			{
				if (card.Equals(STUDENT_DISCOUNT)) 
				{
					ConfirmNumberTickets(card, 4, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
				else
				{
					ConfirmNumberTickets(card, 3, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
			}
			// Check that the ticket values in the modified PricingResult are correct
			PricingResult pricingResult = (PricingResult)pricingResults[STUDENT_DISCOUNT];
			// Check ticket details and ordering (cheapest to most expensive)

			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], false);
			ConfirmTicket(adultDis1, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[2], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[3],false);
		}

		/// <summary>
		/// Confirm that adding a discounted child fare increases the number of tickets of the PricingResult with the matching
		/// discount card, but doesn't affect the other PricingResults.
		/// </summary>
		[Test]
		public void TestAddingChildDiscountedFare()
		{
			// Add the discount cards and additional fare
			this.AddDiscountCards();
			builder.AddDiscountedFare(childDis1);

			Hashtable pricingResults = builder.GetPricingResults();
			// The new ticket has an discount card of Super Child
			// Check that we now have four tickets in the associated PricingResult, but still three tickets in the other PricingResult
			foreach (string card in pricingResults.Keys)
			{
				if (card.Equals(CHILD_DISCOUNT)) 
				{
					ConfirmNumberTickets(card, 4, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
				else
				{
					ConfirmNumberTickets(card, 3, ((IList)((PricingResult)pricingResults[card]).Tickets).Count);
				}
			}
			// Check that the ticket values in the modified PricingResult are correct
			PricingResult pricingResult = (PricingResult)pricingResults[CHILD_DISCOUNT];
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmTicket(childDis1, (Ticket)pricingResult.Tickets[0],false);
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[1], false);
			ConfirmTicket(adultAltRet, (Ticket)pricingResult.Tickets[2], false);
			ConfirmTicket(adultRet, (Ticket)pricingResult.Tickets[3], false);
		}

		/// <summary>
		/// Check that when added an unrecognised discounted fare this doesn't alter any of the PricingResults
		/// </summary>
		[Test]
		public void TestAddingUnrecognisedAdultDiscountedFare()
		{
			// Add the discount cards and additional fare
			this.AddDiscountCards();
			builder.AddDiscountedFare(adultDis2);

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
			PricingResultsBuilder sclBuilder = new PricingResultsBuilder(new SCLTicketNameRule());
			sclBuilder.AddUndiscountedFare(adultSing);
			Hashtable pricingResults = sclBuilder.GetPricingResults();

			// Check discount card related details
			ConfirmDiscountCards(1, pricingResults.Count);
			Assert.IsTrue(pricingResults.ContainsKey(NO_DISCOUNT), "No pricing result created for undiscounted card");
			PricingResult pricingResult = (PricingResult)pricingResults[NO_DISCOUNT];
			// Check the ticket details
			ConfirmNumberTickets(NO_DISCOUNT, 1, ((IList)pricingResult.Tickets).Count);
			// Check ticket details and ordering (cheapest to most expensive)
			ConfirmTicket(adultSing, (Ticket)pricingResult.Tickets[0], true);
		}

		#region Helper methods to update the coach fare data
		/// <summary>
		/// Add undiscounted adult fares to the fares builder. This should introduce 2 new 'sets' of tickets
		/// </summary>
		private void AddDiscountCards()
		{
			builder.AddDiscountCard(STUDENT_DISCOUNT);
			builder.AddDiscountCard(CHILD_DISCOUNT);
		}
		#endregion

		#region Helper methods to assist with comparing expected and obtained values
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
		private void ConfirmTicket(Fare fare, Ticket ticket, bool isSCL)
		{
			ConfirmTicketName(fare, ticket, isSCL);
			ConfirmFares(fare, ticket);
			ConfirmRestriction(fare, ticket);
		}
		/// <summary>
		/// Check that the name of a Ticket object are correct, based on the fields of the source Fare object
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private void ConfirmTicketName(Fare fare, Ticket ticket, bool isSCL)
		{
			if (isSCL) 
				Assert.AreEqual(fare.fareType, ticket.Code, "Ticket name incorrect for "+GetFareDescription(fare));
			else
				Assert.AreEqual(fare.fareType, ticket.Code, "Ticket name incorrect for "+GetFareDescription(fare));
		}

		/// <summary>
		/// Check that the fare fields of a Ticket object are correct, based on the fields of the source Fare object
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private void ConfirmFares(Fare fare, Ticket ticket)
		{
			float fareValue = fare.fare/100f;
			if (fare.discountCardType.Equals(NO_DISCOUNT))
			{
				if (fare.adult) 
				{
					Assert.AreEqual(fareValue, ticket.AdultFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.ChildFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Adult fare incorrect for "+GetFareDescription(fare));
					Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
				} 
				else
				{
					Assert.AreEqual(float.NaN, ticket.AdultFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(fareValue, ticket.ChildFare, "Adult fare incorrect for "+GetFareDescription(fare));
					Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
				}
			}
			// For a discounted fare
			else 
			{
				if (fare.adult) 
				{
					Assert.AreEqual(float.NaN, ticket.AdultFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.ChildFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(fareValue, ticket.DiscountedAdultFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
					Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
				} 
				else
				{
					Assert.AreEqual(float.NaN, ticket.AdultFare, "Adult fare incorrect for "+GetFareDescription(fare));  
					Assert.AreEqual(float.NaN, ticket.ChildFare, "Adult fare incorrect for "+GetFareDescription(fare));  
					Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Adult fare incorrect for "+GetFareDescription(fare));  
					Assert.AreEqual(fareValue, ticket.DiscountedChildFare, "Adult fare incorrect for "+GetFareDescription(fare)); 
				}
			}
		}

		/// <summary>
		/// Check that the restiction field of a Ticket object is correct, based on the fareRestrictionType field of the source Fare object
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private void ConfirmRestriction(Fare fare, Ticket ticket)
		{
			switch (fare.fareRestrictionType) 
			{
				case FareType.NotFlexible:
					Assert.AreEqual(Flexibility.NoFlexibility,ticket.Flexibility, "Flexibility incorrect for "+GetFareDescription(fare));
					break;
				case FareType.LimitedFlexibility:
					Assert.AreEqual(Flexibility.LimitedFlexibility,ticket.Flexibility, "Flexibility incorrect for "+GetFareDescription(fare));
					break;
				case FareType.Flexible:
					Assert.AreEqual(Flexibility.FullyFlexible,ticket.Flexibility, "Flexibility incorrect for "+GetFareDescription(fare));
					break;
				default:
					Assert.Fail("Invalid flexiblity for "+GetFareDescription(fare));
					break;
			}
		}

		/// <summary>
		/// For child tickets check that the minimum and maximum age fields are correct
		/// </summary>
		private void ConfirmChildAges(Fare fare, PricingResult result)
		{
			if (!fare.adult)
			{
				string[] ages = fare.childAgeRange.Split(new char[]{'-'});
				int minAge = int.Parse(ages[0]);
				int maxAge = int.Parse(ages[1]);
				Assert.AreEqual(minAge, result.MinChildAge, "Min child age incorrect for "+GetFareDescription(fare));
				Assert.AreEqual(maxAge, result.MaxChildAge, "Max child age incorrect for "+GetFareDescription(fare));
			}
		}
		#endregion

		#region Helper methods for displaying errors
		private string GetFareDescription(Fare fare)
		{
			return fare.fareType+"/"+fare.passengerType+" and card "+fare.discountCardType+".";
		}
		#endregion
	}
}
