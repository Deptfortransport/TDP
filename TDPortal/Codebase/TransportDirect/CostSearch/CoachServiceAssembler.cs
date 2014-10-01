// ************************************************************** 
// NAME			: CoachServiceAssembler.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/10/2005 
// DESCRIPTION	: CoachServiceAssembler.
//                This class is responsible for using the CJP to obtain
//					the journeys for a specified date and coach ticket(s) 	  
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/CoachServiceAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:14   mturner
//Initial revision.
//
//   Rev 1.11   Jan 16 2006 19:16:46   RPhilpott
//Code review fixes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.10   Dec 22 2005 17:18:40   RWilby
//Updated for code review
//
//   Rev 1.9   Dec 02 2005 17:06:46   RWilby
//Updated class to set the Ticket probability to none if no return journeys are found
//Resolution for 3235: DN39 (CG): Fare was unavailable – but not greyed out on the list.
//
//   Rev 1.8   Nov 30 2005 09:37:34   RPhilpott
//Correct handling of via locations on multi-ticket requests.
//Resolution for 2991: DN040: SBP Pricing extended journey
//
//   Rev 1.7   Nov 17 2005 17:37:50   RPhilpott
//Add Vaild Status to new TDLocations. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.6   Nov 17 2005 11:37:48   RPhilpott
//Handle OpenReturn tickets like Returns if a return date is specified. 
//Resolution for 3091: DN040 - Find a Fare - server error buying ticket for return coach journey
//
//   Rev 1.5   Nov 14 2005 11:18:32   mguney
//If no valid journeys were found for the ticket then its probability is set to None so that the fare is greyed out in the UI.
//Resolution for 2998: DN040: unavailable coach tickets not greyed out in SBP
//
//   Rev 1.4   Nov 05 2005 16:34:08   RWilby
//Changed IJourneyFareFilter to IJourneyFareFilterFactory
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 03 2005 19:21:54   RPhilpott
//Merge undiscounted and discounted CoachFareData into one.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 02 2005 09:34:34   RWilby
//Updated class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 28 2005 16:49:52   RWilby
//Updated class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:31:32   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// This class is responsible for using the CJP to obtain
	///	the journeys for a specified date and coach ticket(s). 	
	/// </summary>
	public class CoachServiceAssembler : ServiceAssembler
	{
		
		/// <summary>
		/// Constructor
		/// </summary>
		public CoachServiceAssembler()
		{}

		/// <summary>
		///  Returns a CostSearchResult with journey information for the selected ticke
		/// </summary>
		/// <param name="request">ICostSearchRequest</param>
		/// <param name="existingResult">ICostSearchResult</param>
		/// <param name="ticket">CostSearchTicket</param>
		/// <returns>ICostSearchResult</returns>
		public override ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket ticket)
		{

			if (TDTraceSwitch.TraceVerbose) 
			{
				StringBuilder sb = new StringBuilder();

				sb.Append("Assembling Coach Services for: ");
				sb.Append(request.OriginLocation);
				sb.Append(" to ");
				sb.Append(request.DestinationLocation);
				sb.Append(" for ");
				sb.Append(request.OutwardDateTime.ToString("u"));
				if	(request.ReturnDateTime != null) 
				{
					sb.Append(" and ");
					sb.Append( request.ReturnDateTime.ToString("u"));
				}
				sb.Append(Environment.NewLine);
				sb.Append("Selected ticket:");
				sb.Append(ticket.Code);
				sb.Append(" for operator ");
				sb.Append(ticket.TicketCoachFareData.OperatorCode);
				
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, sb.ToString()));
			}


			//assign current session information
			sessionInfo = request.SessionInfo;			

			//get the travel date to which this ticket refers
			TravelDate ticketTravelDate = ticket.TravelDateForTicket;				
								
			//is the ticket a return?
			bool isReturn = false;					
			
			if (ticketTravelDate.TicketType == TicketType.Return || ticketTravelDate.TicketType == TicketType.OpenReturn)
			{
				if	(ticketTravelDate.ReturnDate != null) 
				{
					isReturn = true;
				}
			}
	
			//call the CJPManager to get a TDJourneyResult 
			TDJourneyRequest tdJourneyRequest = BuildCoachCJPRequest(request, ticket, true);				
			
			ICJPManager  cjpManager = (ICJPManager)  TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			TDJourneyResult tdJourneyResult = (TDJourneyResult)cjpManager.CallCJP(tdJourneyRequest, sessionInfo.SessionId, sessionInfo.UserType, false, sessionInfo.IsLoggedOn, sessionInfo.Language, false);
					
			//if the ticket has a return date call the CJPManager to get a TDJourneyResult for the return,
			//this time using the return RailServiceParameters
			if (isReturn)
			{
				TDJourneyRequest tdJourneyRequestReturn = BuildCoachCJPRequest(request, ticket, false);

				//ensure tdJourneyRequestReturn.OutwardDate is after the tdJourneyRequest.OutwardDate
				if (tdJourneyRequest.OutwardDateTime[0] > tdJourneyRequestReturn.OutwardDateTime[0])
				{
					tdJourneyRequestReturn.OutwardDateTime[0] = tdJourneyRequest.OutwardDateTime[0];
				}

				cjpManager = (ICJPManager)  TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
				TDJourneyResult tdJourneyResultReturn = (TDJourneyResult)cjpManager.CallCJP(tdJourneyRequestReturn, sessionInfo.SessionId, sessionInfo.UserType, false, sessionInfo.IsLoggedOn, sessionInfo.Language, false);

				//add the return journeys from this result, so that tdJourneyResult now contains both outward and return journeys
				for (int i = 0; i < tdJourneyResultReturn.OutwardPublicJourneyCount; i++)
				{
					tdJourneyResult.AddPublicJourney((JourneyControl.PublicJourney)tdJourneyResultReturn.OutwardPublicJourneys[i], false);
				}

				//also add in the return date to the original request, as this is needed to display return journeys on the
				//JourneySummary page
				tdJourneyRequest.ReturnDateTime = tdJourneyRequestReturn.OutwardDateTime;
			}
						
			//Filter/validate coach journey results
			IJourneyFareFilterFactory journeyFareFilterFactory = (IJourneyFareFilterFactory) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyFareFilterFactory];
			IJourneyFareFilter journeyFareFilter = journeyFareFilterFactory.GetFilter();
			tdJourneyResult =  journeyFareFilter.FilterJourneys(tdJourneyResult,GetCorrespondingTickets(ticket));

			//new CostBasedJourney to assign to the ticket
			CostBasedJourney completeCostBasedJourney = new CostBasedJourney();
			
			//assign the final TDJourneyResult and the TDJourneyRequest to the ticket
			completeCostBasedJourney.CostJourneyResult = tdJourneyResult;
			completeCostBasedJourney.CostJourneyRequest = tdJourneyRequest;
			ticket.JourneysForTicket = completeCostBasedJourney;					

			//if no valid journeys were found for the inward ticket then update its probability accordingly,
			//the user will then have to select another inward ticket		
			if (tdJourneyResult.OutwardPublicJourneyCount == 0)
			{
				ticket.Probability = Probability.None;				
			}
			
			//if no return journeys are found for a return fare then update ticket probability none
			if (isReturn && tdJourneyResult.ReturnPublicJourneyCount ==0)
			{
				ticket.Probability = Probability.None;	
			}

			return existingResult;	
		}			

		
		/// <summary>
		/// Overloaded version of AssembleServices method. 
		/// Returns a CostSearchResult with journey information for 2 Singles tickets		
		/// </summary>
		public override ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket outwardTicket, CostSearchTicket inwardTicket)
		{
			//We do not allow pairs of single tickets for Coach
			//therefore we throw a NotSupportedException

			const string ExceptionMessage = "The overloaded AssembleServices method for journey information for 2 Singles tickets is not supported for Coach";
			throw new NotSupportedException(ExceptionMessage);
		}

		/// <summary>
		/// Builds a TDJourneyRequest using TravelDate 
		/// </summary>
		/// <param name="request">ICostSearchRequest</param>
		/// <param name="ticket">CostSearchTicket</param>
		/// <param name="isOutward">bool</param>
		/// <returns>TDJourneyRequest</returns>
		private  TDJourneyRequest BuildCoachCJPRequest(ICostSearchRequest request, CostSearchTicket ticket, bool isOutward)
		{
			TravelDate travelDate = ticket.TravelDateForTicket;

			//today's date
			TDDateTime todayDate = TDDateTime.Now;

			//the journey request to build
			TDJourneyRequest tdJourneyRequest = new TDJourneyRequest();

			//Add the modes
			tdJourneyRequest.Modes =  new ModeType[]{ModeType.Coach};;
						
			//set IsTrunkRequest flag
			tdJourneyRequest.IsTrunkRequest = true;

			//set locations and dates
			if (isOutward)
			{
				AddLocationsToJourneyRequest(request,ticket, tdJourneyRequest,true);
				//set outward date				
				if (travelDate.OutwardDate != null)
				{
					tdJourneyRequest.OutwardDateTime = new TDDateTime[1];					

					//if outward date is today then set the start time to now
					if (TDDateTime.AreSameDate(travelDate.OutwardDate, todayDate))
					{
						tdJourneyRequest.OutwardDateTime[0] = todayDate;
						tdJourneyRequest.OutwardAnyTime = false;
					}
						//if outward date is not today then no adjustment needed
					else
					{					
						tdJourneyRequest.OutwardDateTime[0] = travelDate.OutwardDate;
						tdJourneyRequest.OutwardAnyTime = true;
					}
				} 
				else 
				{
					tdJourneyRequest.OutwardDateTime = new TDDateTime[0];
				}	
			}
			//if this request is for the return journey
			else
			{	
				//IsReturn
				AddLocationsToJourneyRequest(request,ticket, tdJourneyRequest,false);

				//the return date from the travel date becomes the outward date for this request
				if (travelDate.ReturnDate != null)
				{			
					tdJourneyRequest.OutwardDateTime = new TDDateTime[1];
					tdJourneyRequest.OutwardDateTime[0] = travelDate.ReturnDate;

					//if return date is today then set the start time to now
					if (TDDateTime.AreSameDate(travelDate.ReturnDate, todayDate))
					{
						tdJourneyRequest.OutwardDateTime[0] = todayDate;	
						tdJourneyRequest.OutwardAnyTime = false;
					}
						//if return date is not today then no adjustment needed
					else
					{					
						tdJourneyRequest.OutwardDateTime[0] = travelDate.ReturnDate;
						tdJourneyRequest.OutwardAnyTime = true;
					}
				} 
				else 
				{
					tdJourneyRequest.OutwardDateTime = new TDDateTime[0];
				}	
			}

			tdJourneyRequest.PublicAlgorithm = PublicAlgorithmType.Default;

			tdJourneyRequest.UseOnlySpecifiedOperators = false;

			return tdJourneyRequest;
		}			

		/// <summary>
		/// Adds Locations TDJourneyRequest for coach tickets
		/// </summary>
		/// <param name="request"></param>
		/// <param name="ticket"></param>
		/// <param name="tdJourneyRequest"></param>
		/// <param name="isOutward"></param>
		private void AddLocationsToJourneyRequest (ICostSearchRequest request,CostSearchTicket ticket, TDJourneyRequest tdJourneyRequest,bool isOutward)
		{
			//Get array containing corresponding ticket if any exist, otherwise returns array containing original ticket
			CostSearchTicket[] tickets  = GetCorrespondingTickets(ticket);
		
			if (TDTraceSwitch.TraceVerbose) 
			{
				StringBuilder sb = new StringBuilder();

				sb.Append("Found following tickets for journey: ");
				sb.Append(Environment.NewLine);

				foreach (CostSearchTicket cst in tickets)
				{
					sb.Append(cst.Code);
					sb.Append(" for operator ");
					sb.Append(cst.TicketCoachFareData.OperatorCode);
					sb.Append(" from ");
					sb.Append(cst.TicketCoachFareData.OriginNaptan);
					sb.Append(" to ");
					sb.Append(cst.TicketCoachFareData.DestinationNaptan);
					sb.Append(Environment.NewLine);
				}

				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, sb.ToString()));
			}


			//If corresponding tickets are found then combined the details
			if	(tickets.Length > 1)
			{
				foreach(CostSearchTicket costSearchTicket in tickets)
				{
					//The origin location is the ticket with leg == 0
					//The via location is the ticket with leg == 1
					//The destination is the ticket with leg ==1
					switch (costSearchTicket.LegNumber)
					{
						case 0:
						default:
						{	
							if	(isOutward)
							{
								tdJourneyRequest.OriginLocation = 
									CreateTDLocationForNaptan(costSearchTicket.TicketCoachFareData.OriginNaptan);
							}
							else
							{
								//reverse the locations
								tdJourneyRequest.DestinationLocation = 
									CreateTDLocationForNaptan(costSearchTicket.TicketCoachFareData.OriginNaptan);
							}
							break;
						}

						case 1:
						{
							if	(isOutward)
							{
								TDLocation[] publicViaLocations = new TDLocation[1];
								publicViaLocations[0] = CreateTDLocationForNaptan(costSearchTicket.TicketCoachFareData.OriginNaptan);
								tdJourneyRequest.PublicViaLocations = publicViaLocations;
								
								tdJourneyRequest.DestinationLocation = CreateTDLocationForNaptan(costSearchTicket.TicketCoachFareData.DestinationNaptan);
							}
							else
							{
								//reverse the locations
								TDLocation[] publicViaLocations = new TDLocation[1];
								publicViaLocations[0] = CreateTDLocationForNaptan(costSearchTicket.TicketCoachFareData.OriginNaptan);
								tdJourneyRequest.PublicViaLocations = publicViaLocations;

								tdJourneyRequest.OriginLocation = CreateTDLocationForNaptan(costSearchTicket.TicketCoachFareData.DestinationNaptan);
							}
							break;
						}
					}
				}
			}
			else
			{
				//Origin and destination are both from this ticket
				//and there is no 'hard via', except that if we have restriction infomation present
				//any ChangeNaPTANs in the CoachFareData will also be added as 'hard vias'
				if	(isOutward)
				{
					tdJourneyRequest.OriginLocation = request.OriginLocation;	
					tdJourneyRequest.DestinationLocation = request.DestinationLocation;
				}
				else
				{
					tdJourneyRequest.OriginLocation = request.DestinationLocation;	
					tdJourneyRequest.DestinationLocation = request.OriginLocation;
				}
					
				//if CoachFareData.ChangesNaptans  exist then add them to the PublicViaLocations property
				if (ticket.TicketCoachFareData != null && ticket.TicketCoachFareData.ChangeNaptans != null &&
					ticket.TicketCoachFareData.ChangeNaptans.Length > 0)
				{
					ArrayList publicViaLocations = new ArrayList();
					
					foreach(string changeNaptan in ticket.TicketCoachFareData.ChangeNaptans)
					{
						publicViaLocations.Add(CreateTDLocationForNaptan(changeNaptan));
					}
				
					tdJourneyRequest.PublicViaLocations = (TDLocation[])publicViaLocations.ToArray(typeof(TDLocation));
				}
			}
		}

		/// <summary>
		/// Creates a TDLocation based on Naptan string
		/// </summary>
		/// <param name="naptan">Naptan string</param>
		/// <returns>TDLocation</returns>
		private TDLocation CreateTDLocationForNaptan(string naptan)
		{
			TDLocation tdLocation = new TDLocation();
			TDNaptan tdNaptan = new TDNaptan();
			tdNaptan.Naptan = naptan;
			TDNaptan[] tdNaptans = new TDNaptan[]{tdNaptan};	
			tdLocation.NaPTANs = tdNaptans;
			tdLocation.Status = TDLocationStatus.Valid;
			return tdLocation;
		}
		
		/// <summary>
		/// Returns array containing corresponding tickets if any exist, otherwise returns array containing original ticket
		/// </summary>
		/// <param name="request"></param>
		/// <param name="ticket"></param>
		/// <param name="tdJourneyRequest"></param>
		/// <param name="isOutward"></param>
		/// <returns>CostSearchTicket[]</returns>
		private CostSearchTicket[] GetCorrespondingTickets (CostSearchTicket ticket)
		{
			ArrayList CostSearchTickets = new ArrayList();

			// If CombinedTicketIndex > 0 find the corresponding other tickets
			// with the same CombinedTicketIndex in the same ticket collection/TravelDate.														 
			if	(ticket.CombinedTicketIndex > 0)
			{
				CostSearchTicket[] costSearchTickets;	

				// Set the corresponding ticket collection for the ticket type
				if (ticket.TravelDateForTicket.TicketType == TicketType.OpenReturn ||
					ticket.TravelDateForTicket.TicketType == TicketType.Return)
				{
					costSearchTickets = ticket.TravelDateForTicket.ReturnTickets;
				}
				else
				{
					costSearchTickets = ticket.TravelDateForTicket.OutwardTickets;
				}

				// Find the corresponding ticket
				foreach	(CostSearchTicket costSearchTicket in costSearchTickets)
				{
					if	(costSearchTicket.CombinedTicketIndex == ticket.CombinedTicketIndex)
					{
						// Add corresponding ticket to array
						CostSearchTickets.Add(costSearchTicket);
					}
				}
			}
			else
			{
				// add original ticket to array
				CostSearchTickets.Add(ticket);
			}
	
			return (CostSearchTicket[])CostSearchTickets.ToArray(typeof(CostSearchTicket));
		}
	}
}
