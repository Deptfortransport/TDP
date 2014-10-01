// ************************************************************** 
// NAME			: TestCostSearchResult.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 02/03/2005 
// DESCRIPTION	: Tests CostSearchResult class 
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestCostSearchResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:54   mturner
//Initial revision.
//
//   Rev 1.10   Dec 21 2005 17:45:42   RWilby
//Updated to fix unit test code
//
//   Rev 1.9   Nov 09 2005 12:23:48   build
//Automatically merged from branch for stream2818
//
//   Rev 1.8.1.0   Oct 29 2005 16:06:48   RPhilpott
//Get rid of compiler warnings
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8   May 04 2005 11:20:52   jmorrissey
//Update to TestGetCombinedJourneyResultForTickets method
//
//   Rev 1.7   Apr 27 2005 17:00:38   jmorrissey
//Update to TestGetOutward and TestGetInwardTickets methods.
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.6   Apr 20 2005 11:34:26   RPhilpott
//Change CostSearchErrors handling.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.5   Apr 05 2005 11:11:34   jmorrissey
//Updated after several changes to CostSearchResult
//
//   Rev 1.4   Mar 13 2005 16:08:18   jmorrissey
//Updated after changes to test data in TestMockCostSearchFacade. Reran all tests successfully.
//
//   Rev 1.3   Mar 08 2005 11:36:44   jmorrissey
//Completed testing.
//
//   Rev 1.2   Mar 07 2005 18:29:54   jmorrissey
//Added new NUnit test methods
//
//   Rev 1.1   Mar 04 2005 13:12:06   jmorrissey
//In Progress
//
//   Rev 1.0   Mar 04 2005 13:10:30   jmorrissey
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for TestCostSearchResult.
	/// </summary>
	[TestFixture]
	public class TestCostSearchResult
	{
		private CostSearchResult costSearchResult;
		private TestMockCostSearchFacade mockFacade;
		private TDDateTime todayDate; 

		public TestCostSearchResult()
		{
		}

		[SetUp]
		public void Init()
		{

			//initialise services
			TDServiceDiscovery.Init(new TestCostSearchInitialisation());

			//Create and populate a CostSearchResult
			costSearchResult = new CostSearchResult();

			//use existing method on the mock facade to populate some travel dates
			mockFacade = new TestMockCostSearchFacade();
			costSearchResult.TravelDates = mockFacade.GetFares();

			//date object representing today's date
			todayDate = new TDDateTime(DateTime.Today);
		}
		
		[TearDown]
		public void CleanUp()
		{			
	
		}

		/// <summary>
		/// Tests the GetErrors method
		/// </summary>
		[Test]
		public void TestGetErrors()
		{

			//assign errors and check length again
			
			for (int i = 0; i < 3; i++)
			{
				CostSearchError error = new CostSearchError();
				error.ResourceID = "Error " + i.ToString(); 
				costSearchResult.AddError(error);
			}

			//tests that GetErrors returns any expected errors
			CostSearchError[] errors = costSearchResult.GetErrors();
			Assert.IsTrue(errors.Length == 3, "CostSearchResult errors should contain 3 errors ");

		}

		/// <summary>
		/// Tests the clear errors method
		/// </summary>
		[Test]
		public void TestClearErrors()
		{
			//add errors 
			for (int i = 0; i < 3; i++)
			{
				CostSearchError error = new CostSearchError();
				error.ResourceID = "Error " + i.ToString(); 
				costSearchResult.AddError(error);
			}

			//clear the errors 
			costSearchResult.ClearErrors();

			//check that no errors left
			CostSearchError[] errors = costSearchResult.GetErrors();
			Assert.IsTrue(errors.Length == 0, "CostSearchResult errors should be empty after using ClearErrors");

		}

		/// <summary>
		/// Tests the GetTravelDate method when only an outward date is supplied
		/// </summary>
		[Test]
		public void	TestGetTravelDate()
		{
			TDDateTime outwardDate = todayDate;			
			TicketTravelMode travelMode = TicketTravelMode.Rail;
			TicketType ticketType = TicketType.Single;

			//look for a TravelDate matching these paarmeters 
			TravelDate travelDate = costSearchResult.GetTravelDate(outwardDate, travelMode, ticketType);
			
			//check the result is not null
			Assert.IsNotNull(travelDate, "The TravelDate returned by GetTravelDate is unexpectedly null."); 
			
			//check that the travel date result was as expected
			bool isCorrectTravelDate = (travelDate.TicketType == TicketType.Single) && 
				(travelDate.TravelMode == TicketTravelMode.Rail) &&
				(TDDateTime.AreSameDate(travelDate.OutwardDate,todayDate));
			
			Assert.IsTrue(isCorrectTravelDate, "The TravelDate returned by GetTravelDate is not as expected."); 

		}

		/// <summary>
		/// Tests the GetTravelDate method when an outward and return date is supplied
		/// </summary>
		[Test]
		public void	TestGetTravelDateReturn()
		{
			TDDateTime outwardDate = todayDate.AddDays(1);	
			TDDateTime returnDate = todayDate.AddDays(2);
			TicketTravelMode travelMode = TicketTravelMode.Rail;
			TicketType ticketType = TicketType.Return;

			//look for a TravelDate matching these parameters 
			TravelDate travelDate = costSearchResult.GetTravelDate(outwardDate, returnDate, travelMode, ticketType);
			
			//check the result is not null
			Assert.IsNotNull(travelDate, "The TravelDate returned by GetTravelDate is unexpectedly null."); 

			TDDateTime tomorrow = TDDateTime.Now.AddDays(1);
			TDDateTime theNextDay = TDDateTime.Now.AddDays(2);
			
			//check that the travel date result is as expected
			bool isCorrectTravelDate = 
				//is ticket type correct
				((travelDate.TicketType == TicketType.Return) && 
				//is travel mode correct
				(travelDate.TravelMode == TicketTravelMode.Rail) &&
				//is outward date correct
				(TDDateTime.AreSameDate(travelDate.OutwardDate, tomorrow)) &&
				//is return date correct
				(TDDateTime.AreSameDate(travelDate.ReturnDate, theNextDay)));
			
			Assert.IsTrue(isCorrectTravelDate, "The TravelDate returned by GetTravelDate is not as expected."); 

		}

		/// <summary>
		/// Tests the GetTravelDate method returns correct TravelDates according to TicketType 
		/// </summary>
		[Test]
		public void	TestGetTravelDates()
		{			

			TravelDatesResultSet resultSet = new TravelDatesResultSet();

			//single tickets
			resultSet = costSearchResult.GetTravelDates(TicketType.Single);
			//check result is not null
			Assert.IsNotNull(resultSet, "TravelDatesResultSet array returned is unexpectedly null");
			Assert.IsTrue(resultSet.ContainsSinglesTickets == false, "ContainsSinglesTickets property is incorrect for the returned TravelDatesResultSet");
			//the length should be 9 because TestMockCostSearchFacade sets up 9 Single travel dates  (4 coach, 4 rail, 1 air)
			Assert.IsTrue(resultSet.TravelDates.Length == 9, "TravelDatesResultSet length is incorrect");

			//return tickets - ContainsSinglesTickets property should be true as 1 Singles ticket should also have been added
			resultSet = costSearchResult.GetTravelDates(TicketType.Return);
			//check result is not null
			Assert.IsNotNull(resultSet, "TravelDatesResultSet array returned is unexpectedly null");
			Assert.IsTrue(resultSet.ContainsSinglesTickets == true, "ContainsSinglesTickets property is incorrect for the returned TravelDatesResultSet");
			//the length should be 13 because TestMockCostSearchFacade sets up 12 Return travel dates +
			//1 Singles (the Air one) will also be included as its dates do not match any of the Air returns
			Assert.IsTrue(resultSet.TravelDates.Length == 13, "TravelDatesResultSet length is incorrect");

			//singles tickets - ContainsSinglesTickets property should be true
			resultSet = costSearchResult.GetTravelDates(TicketType.Singles);
			//check result is not null
			Assert.IsNotNull(resultSet, "TravelDatesResultSet array returned is unexpectedly null");
			Assert.IsTrue(resultSet.ContainsSinglesTickets == true, "ContainsSinglesTickets property is incorrect for the returned TravelDatesResultSet");
			//the length should be 3 because TestMockCostSearchFacade sets up 3 Singles travel dates 
			Assert.IsTrue(resultSet.TravelDates.Length == 3, "TravelDatesResultSet length is incorrect");

		}

		/// <summary>
		/// Tests GetOutwardTickets method - returns Singles CostSearchTickets that match the 
		/// TravelMode and the outward date
		/// </summary>
		[Test]
		public void	TestGetOutwardTickets()
		{	
			TDDateTime outwardDate = todayDate.AddDays(1);		
			TDDateTime returnDate = todayDate.AddDays(2);	

			TicketTravelMode travelMode = TicketTravelMode.Rail;			

			//get outward CostSearchTickets for this travel date 			
			CostSearchTicket[] tickets = costSearchResult.GetOutwardTickets(outwardDate, returnDate, travelMode);
		
			//check result is not null
			Assert.IsNotNull(tickets, "CostSearchTicket array returned is unexpectedly null");

			//would expect the result to contain 4 singles tickets - based on data set up in TestMockCostSearchFacade
			Assert.IsTrue(tickets.Length == 4, "CostSearchTicket array does not contain the expected number of tickets");
	
		}

		/// <summary>
		/// Tests GetInwardTickets method - returns Singles CostSearchTickets that match the 
		/// TravelMode and the return date
		/// </summary>
		[Test]
		public void	TestGetInwardTickets()
		{	
			TDDateTime outwardDate = todayDate;
			TDDateTime returnDate = todayDate.AddDays(2);			
			TicketTravelMode travelMode = TicketTravelMode.Coach;			

			//get inward CostSearchTickets for this travel date 			
			CostSearchTicket[] tickets = costSearchResult.GetInwardTickets(outwardDate, returnDate, travelMode);
		
			//check result is not null
			Assert.IsNotNull(tickets, "CostSearchTicket array returned is unexpectedly null");

			//would expect the result to contain 3 singles tickets - based on data set up in TestMockCostSearchFacade
			Assert.IsTrue(tickets.Length == 3, "CostSearchTicket array does not contain the expected number of tickets");
	
		}

		/// <summary>
		/// Tests GetReturnTickets method - returns Return CostSearchTickets that match the 
		/// TravelMode and the outward and return date
		/// </summary>
		[Test]
		public void	TestGetReturnTickets()
		{	
			TDDateTime outwardDate = todayDate;	
			TDDateTime returnDate = todayDate.AddDays(2);			
			TicketTravelMode travelMode = TicketTravelMode.Air;			

			//get return CostSearchTickets for this travel date 			
			CostSearchTicket[] tickets = costSearchResult.GetReturnTickets(outwardDate, returnDate, travelMode);
		
			//check result is not null
			Assert.IsNotNull(tickets, "CostSearchTicket array returned is unexpectedly null");

			//would expect the result to contain 5 return tickets - based on data set up in TestMockCostSearchFacade
			Assert.IsTrue(tickets.Length == 5, "CostSearchTicket array does not contain the expected number of tickets");
	
		}

		/// <summary>
		/// Tests GetReturnTickets method - returns OpenReturn CostSearchTickets that match the 
		/// TravelMode and the outward and return date
		/// </summary>
		[Test]
		public void	TestGetOpenReturnTickets()
		{	
			TDDateTime outwardDate = todayDate;						
			TicketTravelMode travelMode = TicketTravelMode.Coach;			

			//get return CostSearchTickets for this travel date 			
			CostSearchTicket[] tickets = costSearchResult.GetOpenReturnTickets(outwardDate, travelMode);
		
			//check result is not null
			Assert.IsNotNull(tickets, "CostSearchTicket array returned is unexpectedly null");

			//would expect the result to contain 3 open return tickets - based on data set up in TestMockCostSearchFacade
			Assert.IsTrue(tickets.Length == 3, "CostSearchTicket array does not contain the expected number of tickets");
	
		}

		/// <summary>
		/// Tests GetSingleTickets method - returns Single CostSearchTickets that match the 
		/// TravelMode and the outward date
		/// </summary>
		[Test]
		public void	TestGetSingleTickets()
		{	
			TDDateTime outwardDate = todayDate;			
			TicketTravelMode travelMode = TicketTravelMode.Rail;			

			//get outward CostSearchTickets for this travel date 			
			CostSearchTicket[] tickets = costSearchResult.GetSingleTickets(outwardDate, travelMode);
		
			//check result is not null
			Assert.IsNotNull(tickets, "CostSearchTicket array returned is unexpectedly null");

			//would expect the result to contain 4 singles tickets - based on data set up in TestMockCostSearchFacade
			Assert.IsTrue(tickets.Length == 4, "CostSearchTicket array does not contain the expected number of tickets");
	
		}

		/// <summary>
		/// Tests GetJourneyRequest method - Returns the TDJourneyRequest associated with a ticket,
		/// The request is created in TestMockCostSearchFacade.CreateJourneyRequest method
		/// </summary>
		/// <param name="ticket"></param>
		[Test]
		public void TestGetJourneyRequestForTicket()
		{
			//first get some tickets
			TDDateTime outwardDate = todayDate;			
			TicketTravelMode travelMode = TicketTravelMode.Coach;					
			CostSearchTicket[] tickets = costSearchResult.GetSingleTickets(outwardDate, travelMode);

			//now test that a journey request is returned for this ticket
			ITDJourneyRequest request = new TDJourneyRequest();
			request = costSearchResult.GetJourneyRequestForTicket(tickets[0]);
			
			//check result is not null
			Assert.IsNotNull(request, "GetJourneyResultsForTicket method returned a null TDJourneyRequest for the selected ticket");

			//check some expected details of the journey request
			Assert.IsTrue(request.Modes[0].ToString() == travelMode.ToString(), "GetJourneyRequestForTicket method returned a TDJourneyRequest with incorrect travel mode");
			Assert.IsTrue(request.IsReturnRequired == false, "GetJourneyRequestForTicket method returned a TDJourneyRequest with incorrect IsReturnRequired property");
			Assert.IsTrue(request.OriginLocation.NaPTANs[0].Naptan == "9000origin1", "GetJourneyRequestForTicket method returned a TDJourneyRequest with incorrect naptans");

		}

		/// <summary>
		/// Tests the overloaded version of GetJourneyRequest method - Returns one combined TDJourneyRequest 
		/// based on two singles tickets
		/// </summary>
		[Test]
		public void TestGetCombinedJourneyRequestForTickets()
		{
			//get an outward and inward ticket from the costSearchResult
			TDDateTime outwardDate = todayDate;	
			TDDateTime returnDate = todayDate.AddDays(2);
			TicketTravelMode travelMode = TicketTravelMode.Coach;	
			CostSearchTicket[] outwardTickets = costSearchResult.GetOutwardTickets(outwardDate, returnDate, travelMode);
			CostSearchTicket[] inwardTickets = costSearchResult.GetInwardTickets(outwardDate, returnDate, travelMode);

			//test that a journey request is returned based on the two tickets
			ITDJourneyRequest journeyRequest = new TDJourneyRequest();
			journeyRequest = costSearchResult.GetJourneyRequestForTicket(outwardTickets[0],inwardTickets[0]);
			Assert.IsNotNull(journeyRequest, "GetJourneyRequestForTicket method returned a null TDJourneyRequest for the selected tickets");
		
			//check the details of the combined request
			Assert.IsTrue(journeyRequest.OriginLocation == outwardTickets[0].JourneysForTicket.CostJourneyRequest.OriginLocation, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect OriginLocation");
			Assert.IsTrue(journeyRequest.DestinationLocation == outwardTickets[0].JourneysForTicket.CostJourneyRequest.DestinationLocation, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect DestinationLocation");
			Assert.IsTrue(journeyRequest.OutwardDateTime == outwardTickets[0].JourneysForTicket.CostJourneyRequest.OutwardDateTime, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect DestinationLocation");

			Assert.IsTrue(journeyRequest.ReturnOriginLocation == inwardTickets[0].JourneysForTicket.CostJourneyRequest.OriginLocation, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect ReturnOriginLocation");
			Assert.IsTrue(journeyRequest.ReturnDestinationLocation == inwardTickets[0].JourneysForTicket.CostJourneyRequest.DestinationLocation, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect ReturnDestinationLocation");
			//note that the return date time should match the inward ticket's OUTWARD datetime
			Assert.IsTrue(journeyRequest.ReturnDateTime == inwardTickets[0].JourneysForTicket.CostJourneyRequest.OutwardDateTime, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect ReturnDateTime");

		}

		/// <summary>
		/// Tests the GetJourneyResultsForTicket method that returns the TDJourneyResult associated with a ticket
		/// </summary>
		[Test]
		public void TestGetJourneyResultForTicket()
		{
			//TestMockCostSearchFacade populates rail results only after a call to AssembleServices so do this now
			CostSearchRequest  dummyExistingRequest = new CostSearchRequest();			
			CostSearchTicket dummyTicket = new CostSearchTicket(string.Empty,Flexibility.FullyFlexible,string.Empty);
			
			mockFacade.AssembleServices(dummyExistingRequest,costSearchResult,dummyTicket);
			
			//get some tickets from the updated result
			TDDateTime outwardDate = todayDate;			
			TicketTravelMode travelMode = TicketTravelMode.Rail;	
			CostSearchTicket[] tickets = costSearchResult.GetSingleTickets(outwardDate, travelMode);

			//test that a journey result is returned for a ticket
			ITDJourneyResult journeyResult = new TDJourneyResult();
			journeyResult = costSearchResult.GetJourneyResultsForTicket(tickets[0]);
			
			//check result is not null
			Assert.IsNotNull(journeyResult, "GetJourneyResultsForTicket method returned a null TDJourneyResult for the selected ticket");

			//check some expected details of the journey result
			Assert.IsTrue(journeyResult.JourneyReferenceNumber == 4, "GetJourneyResultsForTicket method returned a TDJourneyResult with incorrect JourneyReferenceNumber");

			//check some expected details of the journey result
			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount == 1, "GetJourneyResultsForTicket method returned a TDJourneyResult with incorrect OutwardPublicJourneyCount");

			//check some expected details of the journey result
			Assert.IsTrue(journeyResult.OutwardPublicJourney(0).Details[0].LegStart.Location.Description == "Dover", "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect OutwardPublicJourneyCount");

		}

		/// <summary>
		/// Tests the overloaded version of GetJourneyResultsForTicket method that returns a TDJourneyResult 
		/// based on two separate tickets
		/// </summary>
		[Test]
		public void TestGetCombinedJourneyResultForTickets()
		{				
			//get an outward and inward ticket from the costSearchResult
			TDDateTime outwardDate = todayDate;	
			TDDateTime returnDate = todayDate.AddDays(2);
			TicketTravelMode travelMode = TicketTravelMode.Coach;	
			CostSearchTicket[] outwardTickets = costSearchResult.GetOutwardTickets(outwardDate, returnDate, travelMode);
			CostSearchTicket[] inwardTickets = costSearchResult.GetInwardTickets(outwardDate, returnDate, travelMode);

			//test that a journey result is returned for a ticket
			ITDJourneyResult journeyResult = new TDJourneyResult();
			journeyResult = costSearchResult.GetJourneyResultsForTicket(outwardTickets[0],inwardTickets[0]);
			
			//check result is not null
			Assert.IsNotNull(journeyResult, "GetJourneyResultsForTicket method returned a null TDJourneyResult for the selected tickets");

			//check that the outward journey matches that of the outward ticket passed as a parameter to GetJourneyResultsForTicket
			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount == outwardTickets[0].JourneysForTicket.CostJourneyResult.OutwardPublicJourneyCount, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect OutwardPublicJourneyCount");
			Assert.IsTrue(journeyResult.OutwardPublicJourney(0).Details[0].LegStart.Location.Description == outwardTickets[0].JourneysForTicket.CostJourneyResult.OutwardPublicJourney(0).Details[0].LegStart.Location.Description, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect OutwardPublicJourney details");
			
			//for the return, we should check that the outward details of the second ticket is now the return part of the result.
			//This is because the method being tested has combined 2 singles journeys into 1 journey result
			Assert.IsTrue(journeyResult.ReturnPublicJourneyCount == inwardTickets[0].JourneysForTicket.CostJourneyResult.OutwardPublicJourneyCount, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect ReturnPublicJourneyCount");
			Assert.IsTrue(journeyResult.ReturnPublicJourney(0).Details[0].LegStart.Location.Description == inwardTickets[0].JourneysForTicket.CostJourneyResult.OutwardPublicJourney(0).Details[0].LegStart.Location.Description, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect ReturnPublicJourney details");

			//check the IsValid property of the result
			Assert.IsTrue(journeyResult.IsValid == true, "GetJourneyResultsForTicket method returned a TDJourneyRequest with incorrect IsValid property");

		}		
	}
}
