// ************************************************************** 
// NAME			: RailServiceAssembler.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/10/2005 
// DESCRIPTION	: RailServiceAssembler.
//                This class is responsible for using the CJP to obtain
//				  the journeys for a specified date and rail ticket. 	  
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/RailServiceAssembler.cs-arc  $
//
//   Rev 1.6   Nov 24 2010 10:38:36   apatel
//Resolve the issue with SBP planner when planning for today get services from 22:00 yesterday.  For all other dates the period is 00:00 onwards. Make the SBP planner to return journeys planned 00:00 onwards for all days requested
//Resolution for 5644: SBP planner when planning for today you get services from 22:00 yesterday
//
//   Rev 1.5   Jul 18 2010 13:40:20   mmodi
//Removed code to filter stations as requested by Customer given a new issue was introduced. See SCR for details.
//Resolution for 5295: Fares - SbP - Journey planned is not to original station selected
//
//   Rev 1.4   Jul 12 2010 11:32:16   mmodi
//Updated to filter out Fare locations sent to the CJP based on the original user Input request locations to ensure the journeys returned are more relevant to the user.
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.3   Jun 03 2010 08:48:24   mmodi
//Pass in additional parameters to the Validate services method for use by the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.2   Jun 08 2009 16:22:12   mmodi
//Amended AssembleService method for 2 singles for return, to get and validate the journeys for a fare as two seperate Outward journey requests.
//Resolution for 5294: Search by Price - First Anytime Fare not available
//
//   Rev 1.1   Feb 02 2009 16:20:56   mmodi
//Include Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:19:20   mturner
//Initial revision.
//
//   Rev 1.13   May 17 2006 15:01:52   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.12   May 05 2006 16:16:02   RPhilpott
//Use NLC codes instead of location descriptions for unavailable products.
//Resolution for 4080: DD075: Unavailable fare not changed to Low availability
//
//   Rev 1.11   Apr 05 2006 16:42:46   RPhilpott
//Correct handling of unavailable "singles" fares.
//Resolution for 3765: Find Cheaper/Find a Fare: handling unavailability of single fare for a return journey
//
//   Rev 1.10   Jan 16 2006 19:16:46   RPhilpott
//Code review fixes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.9   Dec 22 2005 17:17:38   RWilby
//Update for code review
//
//   Rev 1.8   Dec 15 2005 16:21:40   RPhilpott
//Make out and return CJP calls asynchronously in parallel. 
//Resolution for 3037: DN040: User responsiveness of SBP fare requests
//
//   Rev 1.7   Dec 05 2005 18:26:40   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.6   Dec 02 2005 12:39:42   RPhilpott
//Handling of "no train found" condition -- product not-available table not to be updated in this case.
//Resolution for 3202: DN040: Specific handling of no inventory condition.
//
//   Rev 1.5   Nov 25 2005 17:44:32   RPhilpott
//Correct "two singles" case.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.4   Nov 25 2005 12:51:04   RPhilpott
//Fix stupid error reversing origin and destination for return fares.
//Resolution for 3214: DN040: Find-A-Fare return journeys wrong for return tickets
//
//   Rev 1.3   Nov 24 2005 18:23:04   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.2   Nov 18 2005 17:42:16   RPhilpott
//Fix outward/return confusion when removing invalid journeys.
//Resolution for 3135: DN040: (CG) Fare availability inconsistent between SBT and SBP
//
//   Rev 1.1   Nov 02 2005 09:34:32   RWilby
//Updated class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:31:32   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price

using System;
using System.Threading;
using System.Collections;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator;

using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// This class is responsible for using the CJP to obtain
	///	the journeys for a specified date and rail ticket. 	
	/// </summary>
	public class RailServiceAssembler : ServiceAssembler
    {
        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		public RailServiceAssembler()
		{
        }

        #endregion

        #region Public methods

        /// <summary>
		/// Returns a CostSearchResult with journey information for the selected ticket		
		/// </summary>
		public override ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket ticket)
		{
			//assign current session information
			sessionInfo = request.SessionInfo;			

			//get the travel date to which this ticket refers
			TravelDate ticketTravelDate = ticket.TravelDateForTicket;				

			TDDateTime outDate = ticketTravelDate.OutwardDate;
			TDDateTime retDate = null;
			
			if (ticketTravelDate.ReturnDate != null)
			{
				retDate = ticketTravelDate.ReturnDate;
			}
				
			//is the ticket a return?
			bool isReturn = false;					
			
			if (ticketTravelDate.TicketType == TicketType.Return)
			{
				isReturn = true;
			}
			
			//Get the PricedServicesSupplierFactory from ServiceDiscovery
			//instance of rail priced services supplier which is Gateway.cs		
			IPricedServicesSupplierFactory pricedServiceSupplierFactory = (IPricedServicesSupplierFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.PricedServiceSupplierFactory];
			pricedServicesSupplier = pricedServiceSupplierFactory.GetPricedServicesSupplier(ModeType.Rail);

			//Obtain parameters required to restrict subsequent CJP enquiry to valid services 
			//for the specified fare - this is done via calls to the RBO via ServiceParametersForFare
			RailServiceParameters[] rsParameters;
			
			if (isReturn)
			{
				//rsParameters array will contain elements for outward and return
				rsParameters = pricedServicesSupplier.ServiceParametersForFare(outDate, retDate, ticket.TicketRailFareData, ticket.TicketRailFareData);
			}
			else
			{
				//rsParameters array will contain elements for just the outward 
				rsParameters = pricedServicesSupplier.ServiceParametersForFare(outDate, null, ticket.TicketRailFareData, null);
			}							

			foreach (RailServiceParameters rsp in rsParameters)
			{
				if	(rsp.ErrorResourceIds.Length > 0)
				{
					foreach (string errorId in rsp.ErrorResourceIds)
					{
						existingResult.AddError(new CostSearchError(errorId));
					}

					// if we got errors while trying to get the parameters,
					//  it's not safe to get the journeys, so create 
					//  a dummy (empty) journey result and return... 
					CostBasedJourney dummyCostBasedJourney = new CostBasedJourney();
					dummyCostBasedJourney.CostJourneyResult = new TDJourneyResult();
					ticket.JourneysForTicket = dummyCostBasedJourney;					

					return existingResult;
				}
			}	

			TDJourneyRequest[] tdJourneyRequests = new TDJourneyRequest[isReturn ? 2 : 1];

			tdJourneyRequests[0] = BuildRailCJPRequest(request, ticket.TicketRailFareData.Origin, ticket.TicketRailFareData.Destination, rsParameters[0], ticketTravelDate, ticket.TicketRailFareData.RouteCode, true);				

			if (isReturn)
			{
				tdJourneyRequests[1] = BuildRailCJPRequest(request, ticket.TicketRailFareData.Origin, ticket.TicketRailFareData.Destination, rsParameters[1], ticketTravelDate, ticket.TicketRailFareData.RouteCode, false);
				tdJourneyRequests[0].ReturnDateTime = tdJourneyRequests[1].OutwardDateTime;						
			}					
			
			TDJourneyResult[] tdJourneyResults = GetJourneyResult(tdJourneyRequests, sessionInfo, isReturn);
			
			//Now Validate rail services returned by the CJP by re-checking restriction 					
			// codes and obtaining applicable supplement and availability information
			
			RailServiceValidationResultsDto rsValidationResults;
			
			if (isReturn)
			{						
				rsValidationResults =  pricedServicesSupplier.ValidateServicesForFare(tdJourneyRequests[0].OriginLocation, 
													tdJourneyRequests[0].DestinationLocation, 
													outDate, 
													retDate, 
													ticket.TicketRailFareData, 
													ticket.TicketRailFareData, 
													tdJourneyResults[0].OutwardPublicJourneys, 
													tdJourneyResults[0].ReturnPublicJourneys, 
													rsParameters[0].RestrictionCodesToReapply, 
													rsParameters[1].RestrictionCodesToReapply,
													rsParameters[0].ConnectingTocsToCheck, 
													rsParameters[1].ConnectingTocsToCheck,
                                                    rsParameters[0].CrossLondonToCheck,
                                                    rsParameters[1].CrossLondonToCheck,
                                                    rsParameters[0].ZonalIndicatorToCheck,
                                                    rsParameters[1].ZonalIndicatorToCheck,
                                                    rsParameters[0].VisitCRSToCheck,
                                                    rsParameters[1].VisitCRSToCheck,
                                                    rsParameters[0].OutputGL,
                                                    rsParameters[1].OutputGL);
			}
			else
			{					
				rsValidationResults =  pricedServicesSupplier.ValidateServicesForFare(tdJourneyRequests[0].OriginLocation, 
													tdJourneyRequests[0].DestinationLocation, 
													outDate, 
													null, 
													ticket.TicketRailFareData, 
													null, 
													tdJourneyResults[0].OutwardPublicJourneys, 
													new ArrayList(0), 
													rsParameters[0].RestrictionCodesToReapply, 
													null,
													rsParameters[0].ConnectingTocsToCheck, 
													false,
                                                    rsParameters[0].CrossLondonToCheck,
                                                    false,
                                                    rsParameters[0].ZonalIndicatorToCheck,
                                                    false,
                                                    rsParameters[0].VisitCRSToCheck,
                                                    false,
                                                    rsParameters[0].OutputGL,
                                                    null);
			}
		
			//add any errors returned by ValidateServicesForFare 
			foreach (string errorId in rsValidationResults.ErrorResourceIds)
			{
				existingResult.AddError(new CostSearchError(errorId));								
			}

			//Check the RailServiceValidationResultsDto and see which outward journeys are valid 
			//and available and see if any supplements apply
			
			//these flags are set when checking the validities and used with the AvailabilityEstimator later
			bool outwardServicesAvailable = true;
			bool inwardServicesAvailable  = true;
			
			//min fare applies flag
			bool minimumFareAppliesToResult = false;

			JourneyControl.PublicJourney publicJourney;
			
			foreach (JourneyValidityDto journeyValidityDto in rsValidationResults.OutwardValidities)
			{	
				//remove any journeys where the fare is invalid or there are no places available				
				if ((journeyValidityDto.Validity == JourneyValidity.InvalidFare) || (journeyValidityDto.Validity == JourneyValidity.NoPlacesAvailable))
				{
					tdJourneyResults[0].RemovePublicJourney((JourneyControl.PublicJourney)tdJourneyResults[0].OutwardPublicJourney(journeyValidityDto.JourneyIndex), true);							
				}	
					//for valid journeys 
				else
				{	
					publicJourney = (JourneyControl.PublicJourney)tdJourneyResults[0].OutwardPublicJourney(journeyValidityDto.JourneyIndex);	

					//assign across any min fare flag
					if (journeyValidityDto.Validity == JourneyValidity.MinimumFareApplies)
					{
						if	(ticket.MinimumAdultFare > ticket.DiscountedAdultFare || ticket.MinimumChildFare > ticket.DiscountedChildFare)
						{
							publicJourney.MinimumFareApplies = true;
							minimumFareAppliesToResult = true;
						}
					}

					//set flag to indicate if there are any upgrades available 
					if ((journeyValidityDto.Supplements != null) && (journeyValidityDto.Supplements.Length > 0))
					{
						publicJourney.UpgradesAvailable = true;
					}
				}						
			}
			//if no valid and available outward journeys for this ticket 
			//then update its probability to None 
			if (tdJourneyResults[0].OutwardPublicJourneyCount == 0)
			{
				ticket.Probability = Probability.None;
				
				if	(!rsValidationResults.IncludesNoInventoryResults)
				{
					outwardServicesAvailable = false;
				}
			}

			//Check the RailServiceValidationResultsDto and see which return journeys are valid 
			//and available and see if any supplements apply				
			if (isReturn)
			{						
				foreach (JourneyValidityDto journeyValidityDto in rsValidationResults.ReturnValidities)
				{
					//remove any journeys where the fare is invalid or there are no places available				
					if ((journeyValidityDto.Validity == JourneyValidity.InvalidFare) || (journeyValidityDto.Validity == JourneyValidity.NoPlacesAvailable))
					{
						tdJourneyResults[0].RemovePublicJourney((JourneyControl.PublicJourney)tdJourneyResults[0].ReturnPublicJourney(journeyValidityDto.JourneyIndex), false);								
					}
						//for valid journeys 
					else
					{	
						publicJourney = (JourneyControl.PublicJourney)tdJourneyResults[0].ReturnPublicJourney(journeyValidityDto.JourneyIndex);	

						//assign across any min fare flag
						if (journeyValidityDto.Validity == JourneyValidity.MinimumFareApplies)
						{
							if	(ticket.MinimumAdultFare > ticket.DiscountedAdultFare || ticket.MinimumChildFare > ticket.DiscountedChildFare)
							{
								publicJourney.MinimumFareApplies = true;
								minimumFareAppliesToResult = true;
							}
						}

						//set flag to indicate if there are any upgrades available
						if ((journeyValidityDto.Supplements != null) && (journeyValidityDto.Supplements.Length > 0))
						{
							publicJourney.UpgradesAvailable = true;
						}
					}
				}	

				//if no return journeys were found for a return ticket 
				//then update its probability to None and the user will then have to select another ticket
				if (tdJourneyResults[0].ReturnPublicJourneyCount == 0)
				{
					ticket.Probability = Probability.None;
					
					if	(!rsValidationResults.IncludesNoInventoryResults)
					{
						inwardServicesAvailable = false;
					}
				}
			}

			//if any MinimumFareApplies flags were found for any journey then assign
			//this to the overall result 
			if	(minimumFareAppliesToResult)
			{
				existingResult.AddError(new CostSearchError(MINIMUM_FARE_WARNING));
			}					
			
			//get instance of AvailabilityEstimator
			AvailabilityEstimatorFactory availabilityFactory = new AvailabilityEstimatorFactory();
			IAvailabilityEstimator availabilityEstimator =  availabilityFactory.GetAvailabilityEstimator(TicketTravelMode.Rail);
											
			ArrayList arrayServices = new ArrayList();
			AvailabilityResultService service;	
			bool serviceAvailable = false;						

			//get list of service availability information
			foreach (RailAvailabilityResultDto railAvailabilityResult in rsValidationResults.RailAvailabilityResults)
			{
				//create individual AvailabilityResultService and add to array list		
				serviceAvailable = (railAvailabilityResult.PlacesAvailable > 0);						
				service = new AvailabilityResultService(railAvailabilityResult.Origin.ToString(),railAvailabilityResult.Destination.ToString(), railAvailabilityResult.DepartureTime, serviceAvailable);
				arrayServices.Add(service);													
			}	
			//convert array list of services to AvailabilityResultService array
			AvailabilityResultService[] availabilityServices = (AvailabilityResultService[])arrayServices.ToArray(typeof(AvailabilityResultService));
			
			//create a AvailabilityResult and use it to update the availability estimator with the 
			//results for this ticket
			AvailabilityResult availabilityResult;
			
			if (isReturn)
			{
				availabilityResult = new AvailabilityResult(TicketTravelMode.Rail, ticket.TicketRailFareData.OriginNlc, ticket.TicketRailFareData.DestinationNlc, 
					ticket.ShortCode, ticket.TicketRailFareData.RouteCode, ticket.TravelDateForTicket.OutwardDate, ticket.TravelDateForTicket.ReturnDate, (outwardServicesAvailable && inwardServicesAvailable));
				availabilityResult.AddJourneyServices(availabilityServices);	
			}
			else
			{
				availabilityResult = new AvailabilityResult(TicketTravelMode.Rail, ticket.TicketRailFareData.OriginNlc, ticket.TicketRailFareData.DestinationNlc, 
					ticket.ShortCode, ticket.TicketRailFareData.RouteCode, ticket.TravelDateForTicket.OutwardDate, null, outwardServicesAvailable);
				availabilityResult.AddJourneyServices(availabilityServices);	
			}					
			
			availabilityEstimator.UpdateAvailabilityEstimate(availabilityResult);					

			//new CostBasedJourney to assign to the ticket
			CostBasedJourney completeCostBasedJourney = new CostBasedJourney();

			//assign the final TDJourneyResult and the TDJourneyRequest to the ticket
			completeCostBasedJourney.CostJourneyResult  = tdJourneyResults[0];
			completeCostBasedJourney.CostJourneyRequest = tdJourneyRequests[0];
			
			ticket.JourneysForTicket = completeCostBasedJourney;					
		
			return existingResult;	
			
		}			

		
		/// <summary>
		/// Overloaded version of AssembleServices method. 
		/// Returns a CostSearchResult with journey information for 2 Singles tickets		
		/// </summary>
		public override ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket outwardTicket, CostSearchTicket inwardTicket)
		{
            // The method will obtain the journeys, and then validate fares for the Outward and Return 
            // journeys seperately, i.e the return journeys will be passed through as "outward" so the
            // fare business objects handle as a single outward journey. (This corrects problem where the 
            // return journey was being treated as an actual Return journey, and thus being invalidated in
            // some scenarios, see sCR5294.

            OperationalEvent oe;
            
            oe = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Verbose, "AssembleServices (2 singles for a return) - Started.");
            Logger.Write(oe);

            //assign current session information
			sessionInfo = request.SessionInfo;					

			//now dates from the travel date
			TravelDate outwardTravelDate = outwardTicket.TravelDateForTicket;
			TravelDate inwardTravelDate = inwardTicket.TravelDateForTicket;

			//Get outward date and return date from individual tickets.	
			TDDateTime outDate = outwardTravelDate.OutwardDate;
			TDDateTime retDate = inwardTravelDate.ReturnDate;
			
			//Get the PricedServicesSupplierFactory from ServiceDiscovery
			//instance of rail priced services supplier which is Gateway.cs		
			IPricedServicesSupplierFactory pricedServiceSupplierFactory = (IPricedServicesSupplierFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.PricedServiceSupplierFactory];
			pricedServicesSupplier = pricedServiceSupplierFactory.GetPricedServicesSupplier(ModeType.Rail);
			
			//Obtain parameters required to restrict subsequent CJP enquiry to valid services 
			//for the specified fare - this is done via calls to the RBO via ServiceParametersForFare
			RailServiceParameters[] rsParametersOut;
            RailServiceParameters[] rsParametersRet;

            //rsParameters array will contain elements for the outward ticket only
            rsParametersOut = pricedServicesSupplier.ServiceParametersForFare(outDate, null, outwardTicket.TicketRailFareData, null);

            //rsParameters array will contain elements for the return ticket only
            rsParametersRet = pricedServicesSupplier.ServiceParametersForFare(retDate, null, inwardTicket.TicketRailFareData, null);

            #region Check for errors

            foreach (RailServiceParameters rsp in rsParametersOut)
			{
				if	(rsp.ErrorResourceIds.Length > 0)
				{
					foreach (string errorId in rsp.ErrorResourceIds)
					{
						existingResult.AddError(new CostSearchError(errorId));
					}

					// if we got errors while trying to get the parameters,
					//  it's not safe to get the journeys, so create 
					//  a dummy (empty) journey result and return... 
					CostBasedJourney dummyCostBasedJourney = new CostBasedJourney();
					dummyCostBasedJourney.CostJourneyResult = new TDJourneyResult();
					outwardTicket.JourneysForTicket = dummyCostBasedJourney;
					inwardTicket.JourneysForTicket = dummyCostBasedJourney;					

					return existingResult;
				}
            }


            foreach (RailServiceParameters rsp in rsParametersRet)
            {
                if (rsp.ErrorResourceIds.Length > 0)
                {
                    foreach (string errorId in rsp.ErrorResourceIds)
                    {
                        existingResult.AddError(new CostSearchError(errorId));
                    }

                    // if we got errors while trying to get the parameters,
                    //  it's not safe to get the journeys, so create 
                    //  a dummy (empty) journey result and return... 
                    CostBasedJourney dummyCostBasedJourney = new CostBasedJourney();
                    dummyCostBasedJourney.CostJourneyResult = new TDJourneyResult();
                    outwardTicket.JourneysForTicket = dummyCostBasedJourney;
                    inwardTicket.JourneysForTicket = dummyCostBasedJourney;

                    return existingResult;
                }
            }

            #endregion

            oe = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Verbose, "AssembleServices (2 singles for a return) - Getting journeys.");
            Logger.Write(oe);

            #region Get journeys

            TDJourneyRequest[] tdJourneyRequests = new TDJourneyRequest[2];

            // Create two outward journey requests, the second uses the return journey details
			tdJourneyRequests[0] = BuildRailCJPRequest(request, outwardTicket.TicketRailFareData.Origin, outwardTicket.TicketRailFareData.Destination, rsParametersOut[0], outwardTravelDate, outwardTicket.TicketRailFareData.RouteCode, true);
            tdJourneyRequests[1] = BuildRailCJPRequest(request, inwardTicket.TicketRailFareData.Origin, inwardTicket.TicketRailFareData.Destination, rsParametersRet[0], inwardTravelDate, inwardTicket.TicketRailFareData.RouteCode, true);
					
			TDJourneyResult[] tdJourneyResults = GetJourneyResult(tdJourneyRequests, sessionInfo, false);

            #endregion

            oe = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Verbose, "AssembleServices (2 singles for a return) - Validating journeys against fare.");
            Logger.Write(oe);

            #region Validate journeys against fare

            //Now Validate rail services returned by the CJP by re-checking restriction 					
			// codes and obtaining applicable supplement and availability information
            RailServiceValidationResultsDto rsValidationResultsOut;
            RailServiceValidationResultsDto rsValidationResultsRet;
            
            // First do the outward journeys
            rsValidationResultsOut = pricedServicesSupplier.ValidateServicesForFare(tdJourneyRequests[0].OriginLocation,
                                                    tdJourneyRequests[0].DestinationLocation,
                                                    outDate,
                                                    null,
                                                    outwardTicket.TicketRailFareData,
                                                    null,
                                                    tdJourneyResults[0].OutwardPublicJourneys,
                                                    new ArrayList(0),
                                                    rsParametersOut[0].RestrictionCodesToReapply,
                                                    null,
                                                    rsParametersOut[0].ConnectingTocsToCheck,
                                                    false,
                                                    rsParametersOut[0].CrossLondonToCheck,
                                                    false,
                                                    rsParametersOut[0].ZonalIndicatorToCheck,
                                                    false,
                                                    rsParametersOut[0].VisitCRSToCheck,
                                                    false,
                                                    rsParametersOut[0].OutputGL,
                                                    null);

            // Now the "return" journeys
            rsValidationResultsRet = pricedServicesSupplier.ValidateServicesForFare(tdJourneyRequests[1].OriginLocation,
                                                    tdJourneyRequests[1].DestinationLocation,
                                                    retDate,
                                                    null,
                                                    inwardTicket.TicketRailFareData,
                                                    null,
                                                    tdJourneyResults[1].OutwardPublicJourneys,
                                                    new ArrayList(0),
                                                    rsParametersRet[0].RestrictionCodesToReapply,
                                                    null,
                                                    rsParametersRet[0].ConnectingTocsToCheck,
                                                    false,
                                                    rsParametersRet[0].CrossLondonToCheck,
                                                    false,
                                                    rsParametersRet[0].ZonalIndicatorToCheck,
                                                    false,
                                                    rsParametersRet[0].VisitCRSToCheck,
                                                    false,
                                                    rsParametersRet[0].OutputGL,
                                                    null);
			
			
			//add any errors returned by ValidateServicesForFare 
			foreach (string errorId in rsValidationResultsOut.ErrorResourceIds)
			{
				existingResult.AddError(new CostSearchError(errorId));								
			}

            foreach (string errorId in rsValidationResultsRet.ErrorResourceIds)
            {
                existingResult.AddError(new CostSearchError(errorId));
            }

            #endregion

            //check the RailServiceValidationResultsDto and see which outward journeys are valid
			//and available and see if any supplements apply
			bool outwardServicesAvailable = true;
			bool inwardServicesAvailable = true;
			
			bool minimumFareAppliesToResult = false;

			JourneyControl.PublicJourney publicJourney;

            #region Outward journeys

            foreach (JourneyValidityDto journeyValidityDto in rsValidationResultsOut.OutwardValidities)
			{	
				//remove any journeys where the fare is invalid or there are no places available				
				if ((journeyValidityDto.Validity == JourneyValidity.InvalidFare) || (journeyValidityDto.Validity == JourneyValidity.NoPlacesAvailable))
				{
					tdJourneyResults[0].RemovePublicJourney((JourneyControl.PublicJourney)tdJourneyResults[0].OutwardPublicJourney(journeyValidityDto.JourneyIndex), true);							
				}	
				else
				{	
					publicJourney = (JourneyControl.PublicJourney)tdJourneyResults[0].OutwardPublicJourney(journeyValidityDto.JourneyIndex);	

					//assign across any min fare flag
					if (journeyValidityDto.Validity == JourneyValidity.MinimumFareApplies)
					{
						publicJourney.MinimumFareApplies = true;
						minimumFareAppliesToResult = true;
					}

					//set flag to indicate if there are any upgrades available 
					if ((journeyValidityDto.Supplements != null) && (journeyValidityDto.Supplements.Length > 0))
					{
						publicJourney.UpgradesAvailable = true;
					}
				}
			}
			//if no valid journeys were found for the outward ticket then update its probability accordingly,
			//the user will then have to select another outward ticket					
			if (tdJourneyResults[0].OutwardPublicJourneyCount == 0) 
			{
				outwardTicket.Probability = Probability.None;
				
				if	(!rsValidationResultsOut.IncludesNoInventoryResults)
				{
					outwardServicesAvailable = false;
				}
            }

            #endregion

            #region Return journeys

            foreach (JourneyValidityDto journeyValidityDto in rsValidationResultsRet.OutwardValidities)
			{
				//remove any journeys where the fare is invalid or there are no places available				
				if ((journeyValidityDto.Validity == JourneyValidity.InvalidFare) || (journeyValidityDto.Validity == JourneyValidity.NoPlacesAvailable))
				{
					tdJourneyResults[1].RemovePublicJourney((JourneyControl.PublicJourney)tdJourneyResults[1].OutwardPublicJourney(journeyValidityDto.JourneyIndex), true);
				}
				else
				{	
					publicJourney = (JourneyControl.PublicJourney)tdJourneyResults[1].OutwardPublicJourney(journeyValidityDto.JourneyIndex);	

					//assign across any min fare flag
					if (journeyValidityDto.Validity == JourneyValidity.MinimumFareApplies)
					{
						publicJourney.MinimumFareApplies = true;
						minimumFareAppliesToResult = true;
					}

					//set flag to indicate if there are any upgrades available
					if ((journeyValidityDto.Supplements != null) && (journeyValidityDto.Supplements.Length > 0))
					{
						publicJourney.UpgradesAvailable = true;
					}
				}
			}	
			//if no valid journeys were found for the inward ticket then update its probability accordingly,
			//the user will then have to select another inward ticket		
			if (tdJourneyResults[1].OutwardPublicJourneyCount == 0)
			{
				inwardTicket.Probability = Probability.None;
				
				if	(!rsValidationResultsRet.IncludesNoInventoryResults)
				{
					inwardServicesAvailable = false;
				}
            }

            #endregion

            //if any MinimumFareApplies flags were found for any journey then assign
			//this to the overall result
			if	(minimumFareAppliesToResult)
			{
				existingResult.AddError(new CostSearchError(MINIMUM_FARE_WARNING));
            }

            oe = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Verbose, "AssembleServices (2 singles for a return) - Updating availabilty estimator.");
            Logger.Write(oe);

            #region Update Availablity Estimator

            //get instance of AvailabilityEstimator
			AvailabilityEstimatorFactory availabilityFactory = new AvailabilityEstimatorFactory();
			IAvailabilityEstimator availabilityEstimator =  availabilityFactory.GetAvailabilityEstimator(TicketTravelMode.Rail);
					
			ArrayList arrayServices = new ArrayList();
			AvailabilityResultService service;	
			bool serviceAvailable = false;						

			//get list of service availability information
			foreach (RailAvailabilityResultDto railAvailabilityResult in rsValidationResultsOut.RailAvailabilityResults)
			{
				//create individual AvailabilityResultService and add to array list		
				serviceAvailable = (railAvailabilityResult.PlacesAvailable > 0);						
				service = new AvailabilityResultService(railAvailabilityResult.Origin.ToString(),railAvailabilityResult.Destination.ToString(), railAvailabilityResult.DepartureTime, serviceAvailable);
				arrayServices.Add(service);													
			}

            foreach (RailAvailabilityResultDto railAvailabilityResult in rsValidationResultsRet.RailAvailabilityResults)
            {
                //create individual AvailabilityResultService and add to array list		
                serviceAvailable = (railAvailabilityResult.PlacesAvailable > 0);
                service = new AvailabilityResultService(railAvailabilityResult.Origin.ToString(), railAvailabilityResult.Destination.ToString(), railAvailabilityResult.DepartureTime, serviceAvailable);
                arrayServices.Add(service);
            }

			//convert array list of services to AvailabilityResultService array
			AvailabilityResultService[] availabilityServices = (AvailabilityResultService[])arrayServices.ToArray(typeof(AvailabilityResultService));
			
			//create an AvailabilityResult and use it to update the availability estimator with the 
			//availability data returned from ValidateServicesForFare 
			AvailabilityResult availabilityResultOutward =  new AvailabilityResult(TicketTravelMode.Rail, outwardTicket.TicketRailFareData.OriginNlc, outwardTicket.TicketRailFareData.DestinationNlc, outwardTicket.ShortCode, outwardTicket.TicketRailFareData.RouteCode, outwardTravelDate.OutwardDate, null, outwardServicesAvailable);

			//add the AvailabilityResultService array to the AvailabilityResult
			availabilityResultOutward.AddJourneyServices(availabilityServices);	

			//update the availability estimator 
			availabilityEstimator.UpdateAvailabilityEstimate(availabilityResultOutward);	
		


			//now update the availability estimator for the inward ticket
			AvailabilityResult availabilityResultInward = 
				new AvailabilityResult(TicketTravelMode.Rail, inwardTicket.TicketRailFareData.OriginNlc, inwardTicket.TicketRailFareData.DestinationNlc, inwardTicket.ShortCode, inwardTicket.TicketRailFareData.RouteCode, inwardTravelDate.ReturnDate, null, inwardServicesAvailable);
			
			//use empty AvailabilityResultService array - this is because all the individual journey services 
			//were added with the outward ticket. For the inward ticket we just need to add the overall bool of whether ANY srevices
			//were valid and available for this inward ticket
			AvailabilityResultService[] availabilityInwardServices = new AvailabilityResultService[0];
			
			//add the AvailabilityResultService array to the AvailabilityResult
			availabilityResultInward.AddJourneyServices(availabilityInwardServices);	

			//update the availability estimator with the AvailabilityResult
			availabilityEstimator.UpdateAvailabilityEstimate(availabilityResultInward);

            #endregion

            oe = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Verbose, string.Format("AssembleServices (2 singles for a return) - Assigning journeys to tickets, outward[{0}] return[{1}] journeys.", tdJourneyResults[0].OutwardPublicJourneyCount, tdJourneyResults[1].OutwardPublicJourneyCount));
            Logger.Write(oe);

            #region Assign journeys to ticket

            //assign CostBasedJourney to the outward ticket
			CostBasedJourney outwardCostBasedJourney = new CostBasedJourney();
			outwardCostBasedJourney.CostJourneyRequest = tdJourneyRequests[0];
			outwardCostBasedJourney.CostJourneyResult = tdJourneyResults[0];
			outwardTicket.JourneysForTicket = outwardCostBasedJourney;	
						
			//assign CostBasedJourney to the inward ticket
			CostBasedJourney inwardCostBasedJourney = new CostBasedJourney();
			
			//assign the final TDJourneyResult and the TDJourneyRequest to the ticket
			inwardCostBasedJourney.CostJourneyRequest = tdJourneyRequests[1];
			inwardCostBasedJourney.CostJourneyResult = tdJourneyResults[1];
			inwardTicket.JourneysForTicket = inwardCostBasedJourney;

            #endregion

            oe = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Verbose, "AssembleServices (2 singles for a return) - Completed.");
            Logger.Write(oe);

			return existingResult;
        }

        #endregion

        #region Private methods

        /// <summary>
		/// Builds a TDJourneyRequest using RailServiceParameters and a TravelDate 
		/// </summary>
		/// <param name="request">ICostSearchRequest</param>
		/// <param name="origin">TDLocation</param>
		/// <param name="destination">TDLocation</param>
		/// <param name="railServiceParameters">RailServiceParameters</param>
		/// <param name="travelDate">TravelDate</param>
		/// <param name="isOutward">bool</param>
		/// <returns>TDJourneyRequest</returns>
		private TDJourneyRequest BuildRailCJPRequest(ICostSearchRequest request, TDLocation origin, TDLocation destination, RailServiceParameters railServiceParameters, TravelDate travelDate, string ticketRouteCodes, bool isOutward)
		{
			//today's date
			TDDateTime todayDate = TDDateTime.Now;

			//the journey request to build
			TDJourneyRequest tdJourneyRequest = new TDJourneyRequest();

			//Add the modes
			tdJourneyRequest.Modes =  new ModeType[]{ModeType.Rail};
						
			//set IsTrunkRequest flag
			tdJourneyRequest.IsTrunkRequest = true;

			//set locations and dates
			if (isOutward)
			{
				tdJourneyRequest.OriginLocation = origin;	
				tdJourneyRequest.DestinationLocation = destination;
			
				//set outward date				
				if (travelDate.OutwardDate != null)
				{
					tdJourneyRequest.OutwardDateTime = new TDDateTime[1];					

					//if outward date is today then set the start time to now
					if (TDDateTime.AreSameDate(travelDate.OutwardDate, todayDate))
					{
						tdJourneyRequest.OutwardDateTime[0] = todayDate;
						tdJourneyRequest.OutwardAnyTime = false;
                        tdJourneyRequest.AdjustTimeWithIntervalBefore = false;
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
				//reverse the locations
				tdJourneyRequest.OriginLocation = destination;	
				tdJourneyRequest.DestinationLocation = origin;

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

			//if railServiceParameters.AdjustedDateTime is not null adjust the OutwardDateTime			
			if (railServiceParameters.AdjustedDateTime != null)
			{
				tdJourneyRequest.OutwardDateTime[0] = railServiceParameters.AdjustedDateTime;
			}			

			//set PublicAlgorithm
			if (railServiceParameters.ChangesAllowed)
			{
				tdJourneyRequest.PublicAlgorithm = PublicAlgorithmType.Default;
			}
			else
			{
				tdJourneyRequest.PublicAlgorithm = PublicAlgorithmType.NoChanges;
			}					

			//set UseOnlySpecifiedOperators and SelectedOperators
			if ((railServiceParameters.IncludeTocs != null) && (railServiceParameters.IncludeTocs.Length > 0))
			{
				tdJourneyRequest.UseOnlySpecifiedOperators = true;

				//if UseOnlySpecifiedOperators is true then add the included TOCs 
				tdJourneyRequest.SelectedOperators = new string[railServiceParameters.IncludeTocs.Length];
				for (int i = 0; i < railServiceParameters.IncludeTocs.Length; i++)
				{
					tdJourneyRequest.SelectedOperators[i] = railServiceParameters.IncludeTocs[i].Code;
				}
			}			
			if ((railServiceParameters.ExcludeTocs != null) && (railServiceParameters.ExcludeTocs.Length > 0))
			{
				tdJourneyRequest.UseOnlySpecifiedOperators = false;

				//if UseOnlySpecifiedOperators is false then add the excluded TOCs 
				tdJourneyRequest.SelectedOperators = new string[railServiceParameters.ExcludeTocs.Length];
				for (int i = 0; i < railServiceParameters.ExcludeTocs.Length; i++)
				{
					tdJourneyRequest.SelectedOperators[i] = railServiceParameters.ExcludeTocs[i].Code;
				}
			}

			//set TrainUidFilterIsInclude
			if ((railServiceParameters.IncludeTrainUids != null) && (railServiceParameters.IncludeTrainUids.Length > 0))
			{
				tdJourneyRequest.TrainUidFilterIsInclude = true;				
				tdJourneyRequest.TrainUidFilter = railServiceParameters.IncludeTrainUids;

			}
			//set TrainUidFilterIsInclude
			if ((railServiceParameters.ExcludeTrainUids != null) && (railServiceParameters.ExcludeTrainUids.Length > 0))
			{				
				tdJourneyRequest.TrainUidFilterIsInclude = false;
				tdJourneyRequest.TrainUidFilter = railServiceParameters.ExcludeTrainUids;
			}

			//set softVias
			if ((railServiceParameters.IncludeLocations != null) &&(railServiceParameters.IncludeLocations.Length > 0))
			{
				tdJourneyRequest.PublicSoftViaLocations = railServiceParameters.IncludeLocations;			
			}
		
			//set notVias
			if ((railServiceParameters.ExcludeLocations != null) && (railServiceParameters.ExcludeLocations.Length > 0))
			{
				tdJourneyRequest.PublicNotViaLocations = railServiceParameters.ExcludeLocations;			
			}

            //set Routing guide properties
            tdJourneyRequest.RoutingGuideInfluenced = request.RoutingGuideInfluenced;
            tdJourneyRequest.RoutingGuideCompliantJourneysOnly = request.RoutingGuideCompliantJourneysOnly;

            //set the route codes, this will restrict journeys to this route
            tdJourneyRequest.RouteCodes = (string.IsNullOrEmpty(ticketRouteCodes)) ? string.Empty : ticketRouteCodes;
		
			return tdJourneyRequest;
		}			


		private TDJourneyResult[] GetJourneyResult(TDJourneyRequest[] requests, CJPSessionInfo sessionInfo, bool isReturnTicket)
		{
			TDJourneyResult[] tdJourneyResults = null;

			CJPManagerCall[] cjpCallList = new CJPManagerCall[requests.Length];

			cjpCallList[0] = new CJPManagerCall(requests[0], false, sessionInfo);

			if (requests.Length > 1)
			{
				// Ensure  return OutwardDateTime is after the outward OutwardDateTime, 
				//  (OutwardDateTime may have been adjusted by rsParameters[0].AdjustedDateTime)
				
				if (requests[0].OutwardDateTime[0] > requests[1].OutwardDateTime[0])
				{
					requests[1].OutwardDateTime[0] = requests[0].OutwardDateTime[0];
				}

				cjpCallList[1] = new CJPManagerCall(requests[1], true, sessionInfo);
			}					
			
			bool cjpCallFailed = false;

			WaitHandle[] wh = new WaitHandle[cjpCallList.Length];

			int callCount = 0;

			foreach (CJPManagerCall cjpCall in cjpCallList)
			{
				wh[callCount] = cjpCall.InvokeCJPManager();

				if	(wh[callCount] == null)
				{
					cjpCallFailed = true;
				}

				callCount++;
			}

			if	(!cjpCallFailed)
			{
				tdJourneyResults = new TDJourneyResult[(isReturnTicket || requests.Length == 0) ? 1 : 2];
				
				int cjpCallTimeOut = Int32.Parse(Properties.Current[JourneyControlConstants.CJPTimeoutMillisecs]);

				int startTime, endTime;
				
				foreach (ManualResetEvent mre in wh)
				{
					startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
					mre.WaitOne(cjpCallTimeOut, false);
					endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
					cjpCallTimeOut -= (endTime - startTime);
				}

				foreach (CJPManagerCall cjpCall in cjpCallList)
				{
					TDJourneyResult journeyResult = cjpCall.GetResult();

					if	(cjpCall.IsSuccessful)
					{
						if	(!cjpCall.IsReturn)
						{
							tdJourneyResults[0] = journeyResult;
						}
						else
						{
							// for a return ticket, add the return journeys from this result, 
							// so that first (and only) tdJourneyResult now contains both 
							// outward and return journeys ...

							if	(isReturnTicket)
							{
								for (int i = 0; i < journeyResult.OutwardPublicJourneyCount; i++)
								{
									tdJourneyResults[0].AddPublicJourney((JourneyControl.PublicJourney)journeyResult.OutwardPublicJourneys[i], false);
								}
							}
							else  // for pair of single tickets, keep results separate ...
							{
								tdJourneyResults[1] = journeyResult;
							}
						}
					}
				}
			}

			return tdJourneyResults;
        }

        #endregion
    }


	class CJPManagerCall
	{
		private delegate ITDJourneyResult CJPManagerAsyncDelegate(ITDJourneyRequest request,
																   string sessionId,
																   int userType,
																   bool referenceTransaction,
																   bool loggedOn,
																   string language,
																   bool isExtension);

		private CJPSessionInfo sessionInfo = null;
		private ITDJourneyRequest request  = null;
		private bool success  = false;
		private bool isReturn = false;

		private CJPManagerAsyncDelegate cjpManagerDelegate = null;
		private IAsyncResult cjpManagerDelegateASR = null;

		public CJPManagerCall(ITDJourneyRequest request, bool isReturn, CJPSessionInfo sessionInfo)
		{
			this.request = request;
			this.sessionInfo = sessionInfo;
			this.isReturn = isReturn;
		}

		public WaitHandle InvokeCJPManager()
		{
			try
			{
				ICJPManager cjpManager = (ICJPManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
				cjpManagerDelegate = new CJPManagerAsyncDelegate(cjpManager.CallCJP);
				cjpManagerDelegateASR = cjpManagerDelegate.BeginInvoke(request, sessionInfo.SessionId, sessionInfo.UserType, false, sessionInfo.IsLoggedOn, sessionInfo.Language, false, null, null);
				return cjpManagerDelegateASR.AsyncWaitHandle;
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, "Exception on FareSupplier call", e, sessionInfo.SessionId);
				Logger.Write(oe);
				return null;
			}
		}


		public TDJourneyResult GetResult()
		{
			TDJourneyResult result = null;

			try
			{
				if	(cjpManagerDelegateASR.IsCompleted)
				{
					success = true;
					result = (TDJourneyResult)cjpManagerDelegate.EndInvoke(cjpManagerDelegateASR);
				}
				else
				{
					Logger.Write(new OperationalEvent
						(TDEventCategory.Business,
						TDTraceLevel.Error,
						"CJPManager call timed out",
						null,
						sessionInfo.SessionId));

					success = false;
				}
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, "Exception after CJPManager call", e, sessionInfo.SessionId);
				Logger.Write(oe);
				success = false;
			}

			return result;
		}

		public bool IsSuccessful
		{
			get { return success; }
		}

		public bool IsReturn
		{
			get { return isReturn; }
		}
	}
}


