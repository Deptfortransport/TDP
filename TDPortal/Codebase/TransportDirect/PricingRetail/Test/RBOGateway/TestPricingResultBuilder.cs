//********************************************************************************
//NAME         : TestPricingResultBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 19/10/2003
//DESCRIPTION  : Implementation of TestPricingResultBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/RBOGateway/TestPricingResultBuilder.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:36   mturner
//Initial revision.
//
//   Rev 1.9   Nov 17 2005 17:40:28   RPhilpott
//Omit Metro from legs passed to RBO.
//Resolution for 3089: DN040: Manchester Metro on Find-A-Fare
//
//   Rev 1.8   Apr 17 2005 18:17:06   RPhilpott
//Del 7 unit testing.
//
//   Rev 1.7   Feb 07 2005 15:39:50   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Nov 24 2003 16:38:40   acaunt
//New test added to check that tickets with the same ticket code but different route code are handled correctly.
//
//   Rev 1.5   Nov 04 2003 16:07:34   acaunt
//Includes tests to ensure that NaNs are handled
//
//   Rev 1.4   Oct 27 2003 16:16:04   acaunt
//Updated test case to allow for mapping of ticket codes into ticket names. Also now checks for correct flexibility
//
//   Rev 1.3   Oct 23 2003 10:26:12   acaunt
//TrainDto.TicketName -> TrainDto.TicketCode
//
//   Rev 1.2   Oct 20 2003 21:41:30   acaunt
//Added all tests
//
//   Rev 1.1   Oct 20 2003 16:47:30   acaunt
//Implemented tests
//
//   Rev 1.0   Oct 20 2003 10:19:08   acaunt
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Test harness for PricingResultBuilder
	/// </summary>
	[TestFixture]
	public class TestPricingResultBuilder
	{

		/// <summary>
		/// Test harness for PricingRequestBuilder
		/// </summary>
		private PricingResultBuilder builder;
		private IDataServices dataServices;

		public TestPricingResultBuilder()
		{
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
			dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		[SetUp]
		public void Init()
		{		
			builder = new PricingResultBuilder();
			builder.CreatePricingResult(TestSampleJourneyData.PricingResultDto);
		}

		[TearDown]
		public void CleanUp()
		{
			builder = null;
		}

		/// <summary>
		///
		/// </summary>
		[Test]
		public void TestCreateResult()
		{
			// Confirm that the details from the PricingResultDto have been correctly captured
			PricingResult result = builder.GetPricingResult();
			Assert.AreEqual(TestSampleJourneyData.PricingResultDto.MinChildAge, result.MinChildAge, "Min child age not correctly set");
			Assert.AreEqual(TestSampleJourneyData.PricingResultDto.MaxChildAge, result.MaxChildAge, "Max child age not correctly set");

		}

		[Test]
		public void TestTicketTypesReturned()
		{
			// first, add time-based tickets and check that result is *not* a CostSearchTicket
			
			builder.AddTicketDto(TestSampleJourneyData.SingleDiscountedStandard);
			builder.AddTicketDto(TestSampleJourneyData.SingleUndiscountedStandard);
			PricingResult result = builder.GetPricingResult();
			
			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");
			Ticket ticket = (Ticket)result.Tickets[0];

			Assert.IsFalse(ticket is CostSearchTicket);

			// now create a new build and add cost-based tickets and check that result *is* a CostSearchTicket

			builder = new PricingResultBuilder();
			builder.CreatePricingResult(TestSampleJourneyData.PricingResultDto);

			builder.AddTicketDto(TestSampleJourneyData.SingleDiscountedStandardCostSearch);
			builder.AddTicketDto(TestSampleJourneyData.SingleUndiscountedStandardCostSearch);
			result = builder.GetPricingResult();
			
			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");
			ticket = (Ticket)result.Tickets[0];

			Assert.IsTrue(ticket is CostSearchTicket);

			Assert.AreEqual(GetTicketCode(TestSampleJourneyData.SingleUndiscountedStandardCostSearch), ticket.Code, "Incorrect ticket name");
			Assert.AreEqual(GetTicketFlexibility(TestSampleJourneyData.SingleUndiscountedStandardCostSearch), ticket.Flexibility, "Incorrect ticket flexibility");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.AdultFare, ticket.AdultFare, "Incorrect adult fare");
			Assert.AreEqual(TestSampleJourneyData.SingleDiscountedStandardCostSearch.AdultFare, ticket.DiscountedAdultFare, "Incorrect discounted adult fare");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.ChildFare, ticket.ChildFare, "Incorrect child fare");
			Assert.AreEqual(TestSampleJourneyData.SingleDiscountedStandardCostSearch.ChildFare, ticket.DiscountedChildFare, "Incorrect discounted child fare");
		

			// check the supplementary data associated with a CostSearchTicket ...
	
			RailFareData fareData = ((CostSearchTicket)ticket).TicketRailFareData;

			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.RestrictionCode, fareData.RestrictionCode, "Incorrect r/c");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.OriginNlc, fareData.OriginNlc, "Incorrect origin NLC");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.DestinationNlc, fareData.DestinationNlc, "Incorrect dest NLC");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.RouteCode, fareData.RouteCode, "Incorrect route code");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandardCostSearch.TicketCode, fareData.ShortTicketCode, "Incorrect short ticket code");
			Assert.AreEqual(TestSampleJourneyData.SingleDiscountedStandardCostSearch.Railcard, fareData.RailcardCode, "Incorrect railcard code");
		
		
		}
		[Test]

		
		public void TestMatchingTicketTypesLowerDiscounted()
		{
			// Add ticketDtos corresponding to the same type, one discounted and one undiscounted
			builder.AddTicketDto(TestSampleJourneyData.ReturnDiscountedStandard);
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedStandard);
			PricingResult result = builder.GetPricingResult();
			// Confirm that the ticket object has been correctly created
			// Because the discounted tickets are lower in value than the undiscounted then they should take their original values
			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");
			Ticket ticket = (Ticket)result.Tickets[0];
			Assert.AreEqual(GetTicketCode(TestSampleJourneyData.ReturnDiscountedStandard), ticket.Code, "Incorrect ticket name");
			Assert.AreEqual(GetTicketFlexibility(TestSampleJourneyData.ReturnDiscountedStandard), ticket.Flexibility, "Incorrect ticket flexibility");
			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedStandard.AdultFare, ticket.AdultFare, "Incorrect adult fare");
			Assert.AreEqual(TestSampleJourneyData.ReturnDiscountedStandard.AdultFare, ticket.DiscountedAdultFare, "Incorrect discounted adult fare");
			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedStandard.ChildFare, ticket.ChildFare, "Incorrect child fare");
			Assert.AreEqual(TestSampleJourneyData.ReturnDiscountedStandard.ChildFare, ticket.DiscountedChildFare, "Incorrect discounted child fare");
		}


		[Test]
		public void TestMatchingTicketTypesHigherDiscounted()
		{
			// Add ticketDtos corresponding to the same type, one discounted and one undiscounted
			builder.AddTicketDto(TestSampleJourneyData.SingleDiscountedStandard);
			builder.AddTicketDto(TestSampleJourneyData.SingleUndiscountedStandard);
			
			PricingResult result = builder.GetPricingResult();
			// Confirm that the ticket object has been correctly created
			
			// Because the discounted tickets are higher in value than the undisocounted then they should take the undiscounted values
			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");
			Ticket ticket = (Ticket)result.Tickets[0];
			
			Assert.AreEqual(GetTicketCode(TestSampleJourneyData.SingleDiscountedStandard), ticket.Code, "Incorrect ticket name");
			Assert.AreEqual(GetTicketFlexibility(TestSampleJourneyData.SingleDiscountedStandard), ticket.Flexibility, "Incorrect ticket flexibility");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandard.AdultFare, ticket.AdultFare, "Incorrect adult fare");
			Assert.AreEqual(TestSampleJourneyData.SingleUndiscountedStandard.ChildFare, ticket.ChildFare, "Incorrect child fare");

			// Del 7 change - if discounted fare ends up same as undiscounted, set it to NaN
			//  (which is interpreted as "no discount available" by the UI)

			Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Incorrect discounted adult fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Incorrect discounted child fare");
		}

		[Test]
		public void TestMatchingTicketTypesDifferentRouteCodes()
		{
			// Add ticketDtos corresponding to the same ticket type, but different route codes
			builder.AddTicketDto(TestSampleJourneyData.SingleUndiscountedStandard);
			builder.AddTicketDto(TestSampleJourneyData.AnotherSingleUndiscountedStandard);
			PricingResult result = builder.GetPricingResult();
			// Confirm that these have been recognised as different tickets
			Assert.AreEqual(2, result.Tickets.Count, "Incorrect number of tickets created");
		}

		[Test]
		public void TestDifferentTicketTypes()
		{
			// Add ticketDtos corresponding to different types
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedFirst);
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedStandard);
			PricingResult result = builder.GetPricingResult();
			// Confirm that the ticket object has been correctly created
			Assert.AreEqual(2, result.Tickets.Count, "Incorrect number of tickets created");
			foreach (Ticket ticket in result.Tickets)
			{
				if (ticket.Code.Equals(GetTicketCode(TestSampleJourneyData.ReturnUndiscountedFirst))) 
				{
					Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.AdultFare, ticket.AdultFare, "Incorrect adult first fare");
					Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.ChildFare, ticket.ChildFare, "Incorrect child first fare");
				} 
				else 
				{
					Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedStandard.AdultFare, ticket.AdultFare, "Incorrect adult standard fare");
					Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedStandard.ChildFare, ticket.ChildFare, "Incorrect child standard fare");
				}
			}
		}

		[Test]
		public void TestNoDiscountsProvided()
		{
			// Add a ticketDto, but no discount fares
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedFirst);
			PricingResult result = builder.GetPricingResult();

			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");		
			Ticket ticket = (Ticket)result.Tickets[0];
			
			// DEL 7 change - if no discount available, discounted fare now returned as NaN.

			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.AdultFare, ticket.AdultFare, "Incorrect adult  fare");
			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.ChildFare, ticket.ChildFare, "Incorrect child  fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Incorrect discounted adult  fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Incorrect discounted child  fare");
		}

		[Test]
		public void TestExplicitNoDiscounts()
		{
			// Add a ticketDto, but no discount fares, with a discounted ticketDto with fare values of NaN
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedFirst);
			builder.AddTicketDto(TestSampleJourneyData.ReturnFirstNoDiscounts);
			PricingResult result = builder.GetPricingResult();

			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");		
			Ticket ticket = (Ticket)result.Tickets[0];
			
			// DEL 7 change - if no discount available, discounted fare now returned as NaN.

			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.AdultFare, ticket.AdultFare, "Incorrect adult  fare");
			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.ChildFare, ticket.ChildFare, "Incorrect child  fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Incorrect discounted adult  fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Incorrect discounted child  fare");
		}

		[Test]
		public void TestExplicitNoChild()
		{
			// Add a ticketDto which has a fare for an adult only (not child)
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedNoChild);
			PricingResult result = builder.GetPricingResult();

			// Confirm that the child fare has been updated to match that of the adult
			Assert.AreEqual(1, result.Tickets.Count, "Incorrect number of tickets created");		
			Ticket ticket = (Ticket)result.Tickets[0];
			
			// DEL 7 change - if no discount available, discounted fare now returned as NaN.

			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.AdultFare, ticket.AdultFare, "Incorrect adult  fare");
			Assert.AreEqual(TestSampleJourneyData.ReturnUndiscountedFirst.AdultFare, ticket.ChildFare, "Incorrect child  fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedAdultFare, "Incorrect discounted adult  fare");
			Assert.AreEqual(float.NaN, ticket.DiscountedChildFare, "Incorrect discounted child  fare");
		}

		[Test]
		public void TestNoFares()
		{
			// Add a ticketDto with only NaN fares
			builder.AddTicketDto(TestSampleJourneyData.ReturnNoFares);
			PricingResult result = builder.GetPricingResult();

			// Confirm that no tickets have been created
			Assert.AreEqual(0, result.Tickets.Count, "Incorrect number of tickets created");		
		}

		private string GetTicketCode(TicketDto ticketDto)
		{
			// Find the details object corresponding to the ticket code
			CategorisedHashData info = dataServices.FindCategorisedHash(DataServiceType.DisplayableRailTickets, ticketDto.TicketCode);
			return info.Value;
		}

		private Flexibility GetTicketFlexibility(TicketDto ticketDto)
		{
			// Find the details object corresponding to the ticket code
			CategorisedHashData info = dataServices.FindCategorisedHash(DataServiceType.DisplayableRailTickets, ticketDto.TicketCode);
			return  (Flexibility)Enum.Parse(typeof(Flexibility), info.Category);
		}
	}
}
