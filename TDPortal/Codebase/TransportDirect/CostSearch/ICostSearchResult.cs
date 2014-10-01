// ************************************************************** 
// NAME			: ICostSearchResult.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the ICostSearchResult Interface
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/ICostSearchResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:20   mturner
//Initial revision.
//
//   Rev 1.9   May 10 2005 11:33:08   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.8   May 06 2005 16:22:42   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.7   Apr 27 2005 16:28:10   jmorrissey
//Update to GetOutwardTickets and GetInwardTickets method definitions
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.6   Apr 20 2005 10:29:54   RPhilpott
//Improve handling of error conditions.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.5   Feb 22 2005 16:46:30   jmorrissey
//Added ClearErrors method and RequestId property
//
//   Rev 1.4   Jan 27 2005 12:30:10   jmorrissey
//Added methods that return JourneyResults and JourneyRequests for a given ticket or tickets
//
//   Rev 1.3   Jan 26 2005 15:19:56   jmorrissey
//Added GetErrors method
//
//   Rev 1.2   Jan 14 2005 15:27:02   jmorrissey
//New method - GetSingleTickets
//
//   Rev 1.1   Jan 12 2005 13:52:30   jmorrissey
//Latest versions. Still in development.
//
//   Rev 1.0   Dec 22 2004 11:59:48   jmorrissey
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// The ICostSearchFacade interface is implemented by the CostSearchFacade and 
	/// TestMockCostSearchFacade classes in the CostSearch project
	/// </summary>
	public interface ICostSearchResult
	{
		//Returns subset of travel dates for the selected ticket type
		TravelDatesResultSet GetTravelDates(TicketType selectedTicket);

		//Returns array of Single tickets for the date and travel mode supplied
		CostSearchTicket[] GetSingleTickets(TDDateTime outwardDate, TicketTravelMode mode);
		
		//Returns array of outward tickets for the date and travel mode supplied
		CostSearchTicket[] GetOutwardTickets(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode mode);
		
		//Returns array of inward tickets for the date and travel mode supplied
		CostSearchTicket[] GetInwardTickets(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode mode);
		
		//Returns array of return tickets for the date and travel mode supplied
		CostSearchTicket[] GetReturnTickets(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode mode);
		
		//Returns array of open return tickets for the date and travel mode supplied
		CostSearchTicket[] GetOpenReturnTickets(TDDateTime outwardDate, TicketTravelMode mode);
		
		//Returns the TDJourneyResult for a CostSearchTicket
		ITDJourneyResult GetJourneyResultsForTicket(CostSearchTicket ticket);			
		
		//Returns a TDJourneyResult based on two Singles CostSearchTickets 
		ITDJourneyResult GetJourneyResultsForTicket(CostSearchTicket outwardTicket, CostSearchTicket inwardTicket);
		
		//Returns the TDJourneyRequest for a CostSearchTicket
		ITDJourneyRequest GetJourneyRequestForTicket(CostSearchTicket ticket);	
		
		//Returns the TDJourneyRequest based on two Singles CostSearchTickets
		ITDJourneyRequest GetJourneyRequestForTicket(CostSearchTicket outwardTicket, CostSearchTicket inwardTicket);
		
		//Returns a specific TravelDate that matches the supplied TDDateTime, TicketTravelMode and TicketType
		TravelDate GetTravelDate(TDDateTime outwardDate, TicketTravelMode travelMode, TicketType ticketType);
		
		//Returns a specific TravelDate that matches the supplied outward and inward TDDateTimes, TicketTravelMode and TicketType
		TravelDate GetTravelDate(TDDateTime outwardDate, TDDateTime inwardDate, TicketTravelMode travelMode, TicketType ticketType );
		
		//Returns the errors associated with the CostSearchResult instance
		CostSearchError[] GetErrors();
		
		//Clears the errors associated with the CostSearchResult instance
		void ClearErrors();

		//Adds an error to the errors collection of the CostSearchResult instance
		void AddError(CostSearchError error);
		
		//Read\write ResultId property
		Guid ResultId { get; set; }

		//method to do the Get of all TravelDates
		TravelDate[] GetAllTravelDates();
		//Write TravelDates property - the method GetAllTravelDates is used to do the 'Get' 
		//because it is more efficient design
		TravelDate[] TravelDates { set;}

		//a read only property that returns a bool indicating if the instance has any TravelDates
		bool HasTravelDates {get;}

	}
}
