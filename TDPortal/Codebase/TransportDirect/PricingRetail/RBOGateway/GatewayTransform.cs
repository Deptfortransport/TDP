//********************************************************************************
//NAME         : GatewayTransform.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Implementation of GatewayTransform class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/GatewayTransform.cs-arc  $
//
//   Rev 1.3   Jul 30 2010 09:18:08   mmodi
//Added code to merge together All locations applicable to a particular ticket type code and route code, before creating the TDLocation to assign to that ticket
//Resolution for 5295: Fares - SbP - Journey planned is not to original station selected
//
//   Rev 1.2   Jun 03 2010 09:23:34   mmodi
//Changes to transforming Pricing Requests for use by the RBO MR call to:
//- include origin/destination location
//- allow inclusion of Underground legs in the request
//
//And updates to set the Ticket location from the Priciing result using the "actual" nlc.
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.1   Apr 09 2010 10:30:02   mmodi
//Allow setting up a fare request for a RailReplacementBus with Walk leg scenario
//Resolution for 5500: Fares - RF 019 fares for journeys involving rail replacement bus stops
//
//   Rev 1.0   Nov 08 2007 12:37:12   mturner
//Initial revision.
//
//   Rev 1.21   Jan 18 2006 18:16:40   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.20   Nov 24 2005 18:22:56   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.19   Nov 23 2005 15:53:08   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.18   Apr 22 2005 11:46:06   RPhilpott
//Handling of "partial errors" returned by RBO's. 
//Resolution for 2247: PT - Error handling by Retail Business Objects
//
//   Rev 1.17   Apr 20 2005 12:11:24   RPhilpott
//Catch exceptions thrown when calling RBOFacade.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.16   Apr 20 2005 10:30:22   RPhilpott
//More error handling.
//
//   Rev 1.15   Apr 13 2005 13:59:32   RPhilpott
//Returning of NRS errors.
//Resolution for 2072: PT: NRS error messages.
//
//   Rev 1.14   Apr 12 2005 16:05:02   RPhilpott
//Move min/max calculation into CostSearchFacade.
//Resolution for 2107: PT: Find Fare initial search can return tickets with No Availability
//
//   Rev 1.13   Apr 08 2005 14:37:04   RPhilpott
//Remove CombinedTicketIndex update (leaving it as default value of zero).
//
//   Rev 1.12   Apr 07 2005 20:55:38   RPhilpott
//Corrections to Supplement and Availability checking.
//
//   Rev 1.11   Apr 07 2005 19:03:46   RPhilpott
//Correct allocation of tickets to TravelDates.
//
//   Rev 1.10   Apr 06 2005 18:02:48   RPhilpott
//Min/Max calculation now in TravelDate class.
//
//   Rev 1.9   Apr 05 2005 11:21:16   RPhilpott
//Filter inflexible tickets from OpenReturn list.
//
//   Rev 1.8   Apr 03 2005 18:22:28   RPhilpott
//Unit Test corrections
//
//   Rev 1.7   Mar 30 2005 16:54:50   RPhilpott
//Add upgrade information to time-based tickets.
//
//   Rev 1.6   Mar 22 2005 16:08:56   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.5   Oct 20 2003 10:19:24   acaunt
//latest version
//
//   Rev 1.4   Oct 18 2003 16:52:00   acaunt
//Updated MapResponse method
//
//   Rev 1.3   Oct 17 2003 20:19:30   acaunt
//Initial Version
//
//   Rev 1.2   Oct 17 2003 12:09:16   acaunt
//No change.
//
//   Rev 1.1   Oct 16 2003 11:51:38   acaunt
//No change.
//
//   Rev 1.0   Oct 15 2003 16:12:00   acaunt
//Initial Revision

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.JourneyPlanning.CJPInterface;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Gateway Transform is reponsible for transforming between Domain model classes to 
	/// RBO specific classes as represented by the PricingMessages classes.
	/// </summary>
	public class GatewayTransform
	{
		
		private const int NUM_ADULTS     = 1;
		private const int NUM_CHILDREN   = 1;

		private const int FIRST_CLASS    = 1;
		private const int STANDARD_CLASS = 2;
		private const int DEFAULT_CLASS  = 9;
	
		private const string PARTIAL_BO_ERROR		= "CostSearchError.PartialReturn";
		private const string UNSPECIFIED_BO_ERROR	= "CostSearchError.FaresInternalError";
		
		public GatewayTransform()
		{
		}

		/// <summary>
		/// Transforms a PricingUnit and a Discounts object into a PricingRequestDto, making use of a PricingRequestBuilder
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="discounts"></param>
		/// <returns></returns>
		public PricingRequestDto MapPriceUnitRequest(TransportDirect.UserPortal.PricingRetail.Domain.PricingUnit unit, Discounts discounts)
		{
			// Create a builder to construct the request
			PricingRequestBuilder builder = new PricingRequestBuilder();
			// Now construct the request
			builder.CreatePricingRequest(discounts);
			foreach (PublicJourneyDetail detail in unit.OutboundLegs) 
			{
				builder.AddJourneyLeg(detail, ReturnIndicator.Outbound, unit.IncludesRailReplacementBusWalk);
			}
			foreach (PublicJourneyDetail detail in unit.InboundLegs)
			{
				builder.AddJourneyLeg(detail, ReturnIndicator.Return, unit.IncludesRailReplacementBusWalk);
			}
			
			builder.SetJourneyType(unit.MatchingReturn);
			
			// And return the complete request
			return builder.GetPricingRequest();
		}

		/// <summary>
		/// Transforms a PricingResultDto into two PricingResult objects (one for singles, one for returns)
		/// </summary>
		/// <param name="pricingResultDto"></param>
		/// <returns></returns>
		public PricingResult[] MapPriceUnitResponse(PricingResultDto pricingResultDto)
		{
			// Create builders to construct the results
			PricingResultBuilder singlesBuilder = new PricingResultBuilder();
			PricingResultBuilder returnsBuilder = new PricingResultBuilder();
			
			// Construct the skeleton of the results
			singlesBuilder.CreatePricingResult(pricingResultDto);
			returnsBuilder.CreatePricingResult(pricingResultDto);
			
			// Populate the results as appropriate
			foreach (TicketDto ticket in pricingResultDto.Tickets)
			{
				if (ticket.JourneyType == JourneyType.OutwardSingle) 
				{
					singlesBuilder.AddTicketDto(ticket);
				} 
				else 
				{
					returnsBuilder.AddTicketDto(ticket);
				}
			}
			// Create the response
			PricingResult[] response = new PricingResult[2];

			response[0] = singlesBuilder.GetPricingResult();
			response[1] = returnsBuilder.GetPricingResult();


			// return errors to Gateway for attachement to PricingUnit 
			//  by adding them (rather arbitrarily) to the first 
			//  response in the returned array ...
			foreach (string errorId in pricingResultDto.ErrorResourceIds)
			{
				response[0].AddErrorMessage(errorId);
			}
			
			return response;
		}

		
		/// <summary>
		/// Creates the array of PricingRequestDto's used as input to a PriceRoute request 
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="discounts"></param>
		/// <returns></returns>
		public PricingRequestDto[] MapRouteRequests(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts)
		{

			LocationDto[] originDtoArray      = LocationConvertor.CreateLocationDtos(origin);
			LocationDto[] destinationDtoArray = LocationConvertor.CreateLocationDtos(destination);

			int numberOfAdults   = NUM_ADULTS;
			int numberOfChildren = NUM_CHILDREN;

			int ticketClass;

			if (discounts.TicketClass == TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.Second)
			{
				ticketClass = STANDARD_CLASS;
			}
			else
			{
				ticketClass = DEFAULT_CLASS;
			}
			
			// For each TravelDate (representing an outward and return date), 
			//   generate one request for each permutation of orgin/destination to get return 
			//	 and outward single tickets, and if a return date is included, another similar 
			//   set of requests to get the inward single tickets.
			//
			// The requests are sorted by origin/destination location, then outward/return date,
			//   to simplify the logic when they are passed to the RetailBusinessObjectsFacade ...

			ArrayList requests = new ArrayList(dates.Count * 2);

			foreach (TravelDate td in dates)
			{
				foreach (LocationDto originDto in originDtoArray)
				{
					foreach (LocationDto destinationDto in destinationDtoArray)
					{
						JourneyType journeyType = (td.ReturnDate == null ? JourneyType.OutwardSingle : JourneyType.Return);

						PricingRequestDto dto = new PricingRequestDto(ticketClass, numberOfAdults, numberOfChildren, discounts.RailDiscount, 
							td.OutwardDate, td.ReturnDate, originDto, destinationDto, journeyType);

						requests.Add(dto);

						if	(td.ReturnDate != null) 
						{
							// add a request to get "inward singles" for this return date
							dto = new PricingRequestDto(ticketClass, numberOfAdults, numberOfChildren, discounts.RailDiscount, 
								td.ReturnDate, null, destinationDto, originDto, JourneyType.InwardSingle);

							requests.Add(dto);
						}
					}
				}
			}
		
			requests.Sort();

			return (PricingRequestDto[])(requests.ToArray(typeof(PricingRequestDto))); 
		}

		/// <summary>
		/// Takes an array of pricing result DTO's and uses them to 
		/// update the corresponding TravelDates with ticket data.
		/// 
		/// There will typically be two PricingResultDto's returned for
		///   each original TravelDate (one for OutwardSingle and Return  
		///   tickets, and another for InwardSingle tickets). 
		/// </summary>
		/// <param name="pricingResultDto">Array of results from Fares enquiry</param>
		/// <param name="dates">ArrayList containing TravelDates which are updated by this method</param>
		public string[] MapRouteResponses(PricingResultDto[] pricingResultDtos, ArrayList dates)
		{
			UpdateTicketLocations(pricingResultDtos);

			ArrayList additionalDates = new ArrayList(dates.Count);

			ArrayList errorIds = new ArrayList();

			foreach (TravelDate td in dates)
			{
				TravelDate newTD = new TravelDate();

				newTD.OutwardDate = td.OutwardDate;
				newTD.ReturnDate  = td.ReturnDate;
				newTD.TravelMode  = td.TravelMode;

				if	(td.ReturnDate != null) 
				{
					td.TicketType	 = TicketType.Return;
					newTD.TicketType = TicketType.Singles;
				}
				else
				{
					td.TicketType	 = TicketType.OpenReturn;
					newTD.TicketType = TicketType.Single;
				}

				additionalDates.Add(newTD);

				PricingResultBuilder inwardSinglesBuilder  = null;
				PricingResultBuilder outwardSinglesBuilder = null;
				PricingResultBuilder returnsBuilder        = null;

				if	(pricingResultDtos.Length > 0)
				{
					inwardSinglesBuilder  = new PricingResultBuilder();
					outwardSinglesBuilder = new PricingResultBuilder();
					returnsBuilder		  = new PricingResultBuilder();

					// This implicitly makes the assumption that min and max  
					//  child ages are the same in all returned results ...
							
					outwardSinglesBuilder.CreatePricingResult(pricingResultDtos[0]);
					returnsBuilder.CreatePricingResult(pricingResultDtos[0]);
					inwardSinglesBuilder.CreatePricingResult(pricingResultDtos[0]);
				}
				
				foreach (PricingResultDto result in pricingResultDtos)
				{
					if	(result.JourneyType == JourneyType.InwardSingle)
					{
						if	(!result.OutwardDate.Equals(td.ReturnDate))
						{
							continue;
						}
					}
					else if	(result.JourneyType == JourneyType.OutwardSingle)
					{
						if	(!result.OutwardDate.Equals(td.OutwardDate))
						{
							continue;
						}
					}
					else								// journeyType == Return
					{
						if	(!result.OutwardDate.Equals(td.OutwardDate) || !result.ReturnDate.Equals(td.ReturnDate))
						{
							continue;
						}
					}
					
					// this result does apply to this TravelDate (and its clone, created above)

					if	(result.Tickets.Count == 0) 
					{
						continue;	// nothing more to do 
					}

					// Populate the results as appropriate
					foreach (TicketDto ticket in result.Tickets)
					{
						switch (ticket.JourneyType)
						{
							case JourneyType.InwardSingle: 
							{
								inwardSinglesBuilder.AddTicketDto(ticket);
								break;
							} 
							case JourneyType.OutwardSingle: 
							{
								outwardSinglesBuilder.AddTicketDto(ticket);
								break;
							} 
							case JourneyType.Return: 
							{
								returnsBuilder.AddTicketDto(ticket);
								break;
							} 
						}
					}
				}

				if	(td.TicketType == TicketType.OpenReturn)	// and newTD.TicketType == TicketType.Single
				{

					ArrayList outwardSingleTickets = (ArrayList)(outwardSinglesBuilder.GetPricingResult().Tickets);
						
					foreach (CostSearchTicket cst in outwardSingleTickets)
					{
						cst.TravelDateForTicket = newTD;
						newTD.AddOutwardTicket(cst);
					}

					ArrayList returnTickets = (ArrayList)(returnsBuilder.GetPricingResult().Tickets);

					foreach (CostSearchTicket cst in returnTickets)
					{
						// return tickets with no flexibilty are not "open" returns ... ignore them

						if	(cst.Flexibility != Flexibility.NoFlexibility)
						{
							cst.TravelDateForTicket = td;
							td.AddOutwardTicket(cst);
						}
					}

				}
				else	// td.TicketType == TicketType.Return and newTD.TicketType == TicketType.Singles
				{
					ArrayList returnTickets = (ArrayList)(returnsBuilder.GetPricingResult().Tickets);

					foreach (CostSearchTicket cst in returnTickets)
					{
						cst.TravelDateForTicket = td;
						td.AddReturnTicket(cst);
					}

					ArrayList outwardSingleTickets = (ArrayList)(outwardSinglesBuilder.GetPricingResult().Tickets);

					if	(outwardSingleTickets.Count == 0)
					{
						newTD.ErrorForOutward = true;
					}
					else
					{
						foreach (CostSearchTicket cst in outwardSingleTickets)
						{
							cst.TravelDateForTicket = newTD;
							newTD.AddOutwardTicket(cst);
						}
					}

					ArrayList inwardSingleTickets = (ArrayList)(inwardSinglesBuilder.GetPricingResult().Tickets);

					if	(inwardSingleTickets.Count == 0)
					{
						newTD.ErrorForInward = true;
					}
					else
					{
						foreach (CostSearchTicket cst in inwardSingleTickets)
						{
							cst.TravelDateForTicket = newTD;
							newTD.AddInwardTicket(cst);
						}
					}
				
					if	(newTD.ErrorForInward && newTD.ErrorForOutward) 
					{
						errorIds.Add(UNSPECIFIED_BO_ERROR);
					}
					else if	(newTD.ErrorForInward || newTD.ErrorForOutward) 
					{
						errorIds.Add(PARTIAL_BO_ERROR);
					}
				}		
			}

			dates.AddRange(additionalDates);

			return (string[])(errorIds.ToArray(typeof(string)));
		
		}


		/// <summary>
		/// Creates a PricingRequest for use in the ServiceParametersForFare call.
		/// </summary>
        public PricingRequestDto MapServiceParametersRequest(TDLocation origin, TDLocation destination, RailFareData fare, TDDateTime outwardDate, TDDateTime returnDate, string railcardCode)
		{
            LocationDto[] originDtoArray = LocationConvertor.CreateLocationDtos(origin);
            LocationDto[] destinationDtoArray = LocationConvertor.CreateLocationDtos(destination);

            // Determine which locations to use, default to first in the array and then try to find a match.
            // Have to assume we've found some locations otherwise fare services call will fail
            LocationDto originDto = originDtoArray[0];
            LocationDto destinationDto = destinationDtoArray[0];

            foreach (LocationDto locationDto in originDtoArray)
            {
                if ((locationDto.Nlc == fare.OriginNlc) || (locationDto.Nlc == fare.OriginNlcActual))
                {
                    originDto = locationDto;
                }
            }

            foreach (LocationDto locationDto in destinationDtoArray)
            {
                if ((locationDto.Nlc == fare.DestinationNlc) || (locationDto.Nlc == fare.DestinationNlcActual))
                {
                    destinationDto = locationDto;
                }
            }

			JourneyType journeyType = (returnDate == null ? JourneyType.OutwardSingle : JourneyType.Return);
                        
			return new PricingRequestDto(STANDARD_CLASS, NUM_ADULTS, NUM_CHILDREN, railcardCode, 
				outwardDate, returnDate, originDto, destinationDto, journeyType);

		}

		/// <summary>
		/// Performs necessary conversions to create RailServiceParameters objects 
		///   using the TTBOParametersDto's returned by the RetailBusinessObjectsFacacde.
		/// </summary>
		public RailServiceParameters[] MapServiceParametersResponses(TTBOParametersDto[] ttboDtos)
		{

			RailServiceParameters[] responses = new RailServiceParameters[ttboDtos.Length];

			for (int i = 0; i < ttboDtos.Length; i++) 
			{
				ArrayList includeLocations = new ArrayList(ttboDtos[i].IncludeCrsLocations.Length);
				ArrayList excludeLocations = new ArrayList(ttboDtos[i].ExcludeCrsLocations.Length);
				
				foreach (string crs in ttboDtos[i].IncludeCrsLocations) 
				{
					LocationDto[] locations = new LocationDto[] { new LocationDto(crs, string.Empty) };
					
					TDLocation loc = LocationConvertor.CreateTDLocation(locations);
					
					if	(loc != null) 
					{
						includeLocations.Add(loc); 
					}
				}

				foreach (string crs in ttboDtos[i].ExcludeCrsLocations) 
				{
					LocationDto[] locations = new LocationDto[] { new LocationDto(crs, string.Empty) };

					TDLocation loc = LocationConvertor.CreateTDLocation(locations);
					
					if	(loc != null) 
					{
						excludeLocations.Add(loc); 
					}
				}

				responses[i] = new RailServiceParameters(ttboDtos[i], 
					(TDLocation[])(includeLocations.ToArray(typeof(TDLocation))), 
					(TDLocation[])(excludeLocations.ToArray(typeof(TDLocation))));

				foreach	(string errorId in ttboDtos[i].ErrorResourceIds)
				{
					responses[i].AddErrorMessage(errorId);
				}
			}

			return responses;
		}



		/// <summary>
		/// Creates an array of PricingRequests for use in the ValidateServiceDetailsForFareAndJourney call.
		/// One PricingRequest is created per journey.
		/// </summary>
		public PricingRequestDto[] MapServiceValidationRequest(TDDateTime outwardDate, TDDateTime returnDate, 
			TDLocation origin, TDLocation destination, string railcardCode, 
			ArrayList outwardJourneys, ArrayList returnJourneys)
		{
			
			ArrayList requests = new ArrayList(outwardJourneys.Count + returnJourneys.Count);

			foreach (TransportDirect.UserPortal.JourneyControl.PublicJourney pj in outwardJourneys)
			{
				PricingRequestBuilder builder = new PricingRequestBuilder();
				
				builder.CreatePricingRequest(railcardCode, outwardDate, returnDate);
			
				builder.AddJourneyIndex(pj.JourneyIndex);

				foreach (PublicJourneyDetail detail in pj.Details) 
				{
                    // Fix for IR5538, Underground+Rail service flagged as valid for non-cross london fare
                    // when it should be invalid. 
                    // Journeys will have come from TTBO, so OK to add underground.
					builder.AddJourneyLeg(detail, ReturnIndicator.Outbound, false, true);
				}
			
				PricingRequestDto newRequest = builder.GetPricingRequest();
				
				newRequest.Origin	   = LocationConvertor.CreateLocationDto(origin);
				newRequest.Destination = LocationConvertor.CreateLocationDto(destination);
				
				requests.Add(newRequest);
			}
			
			foreach (TransportDirect.UserPortal.JourneyControl.PublicJourney pj in returnJourneys)
			{
				PricingRequestBuilder builder = new PricingRequestBuilder();
				
				builder.CreatePricingRequest(railcardCode, outwardDate, returnDate);

				builder.AddJourneyIndex(pj.JourneyIndex);
				
				foreach (PublicJourneyDetail detail in pj.Details) 
				{
                    // Fix for IR5538, Underground+Rail service flagged as valid for non-cross london fare
                    // when it should be invalid. 
                    // Journeys will have come from TTBO, so OK to add underground.
					builder.AddJourneyLeg(detail, ReturnIndicator.Return, false, true);
				}
			
				PricingRequestDto newRequest = builder.GetPricingRequest();
				
				newRequest.Origin	   = LocationConvertor.CreateLocationDto(origin);
				newRequest.Destination = LocationConvertor.CreateLocationDto(destination);
				
				requests.Add(newRequest);
			}
			
			return (PricingRequestDto[])(requests.ToArray(typeof(PricingRequestDto))); 
		}

        /// <summary>
        /// Method to update the ticket NLC locations in to a TDLocation, assigned to the ticket
        /// </summary>
        /// <param name="results"></param>
		private void UpdateTicketLocations(PricingResultDto[] results)
        {
            #region Build TDLocations to add for the Ticket
            
            Hashtable ticketTable = new Hashtable();

            // First, for each NLC, identify all the unique LocationDTOs which apply to it found from the tickets
            Dictionary<string, List<LocationDto>> locationDTOs = new Dictionary<string,List<LocationDto>>();

            #region Get all LocationDTOs for each NLC
            foreach (PricingResultDto result in results)
            {
                foreach (TicketDto tkt in result.Tickets)
                {
                    // Origin NLC locations
                    string nlc = tkt.OriginNlc;

                    if (locationDTOs.ContainsKey(nlc))
                    {
                        locationDTOs[nlc].AddRange(tkt.Origins);
                    }
                    else
                    {
                        locationDTOs.Add(nlc, new List<LocationDto>(tkt.Origins));
                    }

                    if(!string.IsNullOrEmpty(tkt.FareOriginNlcActual))
                    {
                        nlc = tkt.FareOriginNlcActual;

                        if (locationDTOs.ContainsKey(nlc))
                        {
                            locationDTOs[nlc].AddRange(tkt.Origins);
                        }
                        else
                        {
                            locationDTOs.Add(nlc, new List<LocationDto>(tkt.Origins));
                        }
                    }

                    // Destination NLC locations
                    nlc = tkt.DestinationNlc;

                    if (locationDTOs.ContainsKey(nlc))
                    {
                        locationDTOs[nlc].AddRange(tkt.Destinations);
                    }
                    else
                    {
                        locationDTOs.Add(nlc, new List<LocationDto>(tkt.Destinations));
                    }

                    if (!string.IsNullOrEmpty(tkt.FareDestinationNlcActual))
                    {
                        nlc = tkt.FareDestinationNlcActual;

                        if (locationDTOs.ContainsKey(nlc))
                        {
                            locationDTOs[nlc].AddRange(tkt.Destinations);
                        }
                        else
                        {
                            locationDTOs.Add(nlc, new List<LocationDto>(tkt.Destinations));
                        }
                    }
                }
            }

            #endregion

            // All applicable LocationDTOs have been found for an NLC
            foreach (KeyValuePair<string, List<LocationDto>> kvp in locationDTOs)
            {
                List<LocationDto> filteredLocationDTOs = new List<LocationDto>();

                // Remove any duplicate locations
                foreach (LocationDto locationDTO in kvp.Value)
                {
                    if (!filteredLocationDTOs.Contains(locationDTO))
                    {
                        filteredLocationDTOs.Add(locationDTO);
                    }
                }

                // Build the TDLocation
                ticketTable.Add(kvp.Key, LocationConvertor.CreateTDLocation(filteredLocationDTOs.ToArray()));
            }

            #endregion

            #region Assign the TDLocation against the ticket

            foreach (PricingResultDto result in results)
			{
				foreach (TicketDto tkt in result.Tickets)
				{
                    // Use the actual NLC following introduction of ZPBO, as these are now 
                    // used to build up the locations the ticket is valid for
                    //string originNlc = (!string.IsNullOrEmpty(tkt.FareOriginNlcActual)) ? (tkt.OriginNlc + tkt.FareOriginNlcActual) : tkt.OriginNlc;
                    //string destinationNlc = (!string.IsNullOrEmpty(tkt.FareDestinationNlcActual)) ? (tkt.DestinationNlc + tkt.FareDestinationNlcActual) : tkt.DestinationNlc;
                    string originNlc = (!string.IsNullOrEmpty(tkt.FareOriginNlcActual)) ? tkt.FareOriginNlcActual : tkt.OriginNlc;
                    string destinationNlc = (!string.IsNullOrEmpty(tkt.FareDestinationNlcActual)) ? tkt.FareDestinationNlcActual : tkt.DestinationNlc;

					if	(ticketTable.ContainsKey(originNlc))
					{
						tkt.OriginLocation = (TDLocation)(ticketTable[originNlc]);
					}
					else // Location should always exist in table, but added for completeness
					{
						tkt.OriginLocation = LocationConvertor.CreateTDLocation(tkt.Origins);
						ticketTable.Add(originNlc, tkt.OriginLocation);
					}
						
					if	(ticketTable.ContainsKey(destinationNlc))
					{
						tkt.DestinationLocation = (TDLocation)(ticketTable[destinationNlc]);
					}
                    else // Location should always exist in table, but added for completeness
					{
						tkt.DestinationLocation = LocationConvertor.CreateTDLocation(tkt.Destinations);
						ticketTable.Add(destinationNlc, tkt.DestinationLocation);
					}
				}
            }

            #endregion
        }

        /// <summary>
        /// Merges the new location naptans in to the existing old location
        /// </summary>
        /// <param name="oldLocation"></param>
        /// <param name="newLocation"></param>
        /// <returns></returns>
        private TDLocation MergeLocationNaptans(TDLocation oldLocation, TDLocation newLocation)
        {
            
            List<TDNaptan> locationNaptans = new List<TDNaptan>(oldLocation.NaPTANs);

            // Merge naptans if needed
            foreach (TDNaptan naptan in newLocation.NaPTANs)
            {
                if (!locationNaptans.Contains(naptan))
                {
                    locationNaptans.Add(naptan);
                }
            }

            oldLocation.NaPTANs = locationNaptans.ToArray();

            return oldLocation;
        }
	}
}
