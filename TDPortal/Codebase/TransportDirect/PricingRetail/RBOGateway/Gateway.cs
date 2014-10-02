//********************************************************************************
//NAME         : Gateway.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Implementation of Gateway class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/Gateway.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 09:13:24   mmodi
//Updated to pass in additional parameters to the Validate services and fares methods for use by the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:37:10   mturner
//Initial revision.
//
//   Rev 1.29   Oct 16 2007 13:53:02   mmodi
//Amended to accept a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.28   Mar 06 2007 13:43:46   build
//Automatically merged from branch for stream4358
//
//   Rev 1.27.1.0   Mar 02 2007 11:11:46   asinclair
//Added check for NoThroughFaresAvailable
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.27   Jan 18 2006 18:16:38   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.26   Jan 17 2006 18:12:30   RPhilpott
//Code review updates
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.25   Dec 05 2005 18:26:42   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.24   Nov 24 2005 18:22:58   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.23   Nov 23 2005 15:53:08   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.22   Nov 09 2005 12:31:42   build
//Automatically merged from branch for stream2818
//

using System;
using System.Collections;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.RetailBusinessObjects;
using TransportDirect.UserPortal.PricingRetail.Logging;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Gateway class to handle the transformation of domain objects into the API provided by the RetailBusinessObjects service,
	/// making a call to that service, and then the transformation back into domain objects
	/// </summary>
	[Serializable]
	public class Gateway : ITimeBasedFareSupplier, IPricedServicesSupplier, IRoutePriceSupplier
	{
		private const string UNSPECIFIED_BO_ERROR	= "CostSearchError.FaresInternalError";

		
		/// <summary>
		/// Default constructor 
		/// </summary>
		public Gateway()
		{
		}

		/// <summary>
		/// Implementation of IPriceSupplier.PricePricingUnit
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="discounts"></param>
		/// <returns>The updated PricingUnit</returns>
		public PricingUnit PricePricingUnit(PricingUnit pricingUnit, Discounts discounts, CJPSessionInfo sessionInfo, string requestID)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PricingRequestMessage.Message(pricingUnit)));
			}
			
			// Map the PricingUnit into a PricingRequestDto
			GatewayTransform transform = new GatewayTransform();
			PricingRequestDto request = transform.MapPriceUnitRequest(pricingUnit, discounts);
			
			// Make the request to the RetailBusinessObject service
			RetailBusinessObjectsFacade rboFacade = new RetailBusinessObjectsFacade();
			PricingResultDto response = rboFacade.GetFaresForSingleJourney(request);
			
			// Map the response into PricingResults
			PricingResult[] results = transform.MapPriceUnitResponse(response);
			pricingUnit.SetFares(results[0], results[1]);

			if(results[0].NoThroughFaresAvailable)
			{
				pricingUnit.NoThroughFares = true;

			}

			foreach (string errorId in results[0].ErrorResourceIds)
			{
				pricingUnit.AddErrorMessage(errorId);
			}
			
			if (TDTraceSwitch.TraceVerbose)
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PricingResponseMessage.Message(pricingUnit)));
			}

			return pricingUnit;
		}


		public string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, CJPSessionInfo cjpInfo, string operatorCode, int legNumber, int ticketIndex, QuotaFareList quotaFares)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PriceRouteRequestMessage.Message(dates, origin, destination, discounts)));
			}

			// Create a PricingRequestDto from the input parameters ...
			GatewayTransform transform = new GatewayTransform();
			PricingRequestDto[] requests = transform.MapRouteRequests(dates, origin, destination, discounts);

			// Make the request to the RetailBusinessObject service
			RetailBusinessObjectsFacade rboFacade = new RetailBusinessObjectsFacade();
			
			ArrayList errorIds = new ArrayList();
		
			try 
			{
				PricingResultDto[] responses = rboFacade.GetFaresForRoute(requests);
			
				// update the array of TravelDates with the ticket information returned ...
				string[] transformErrors = transform.MapRouteResponses(responses, dates);

				foreach (PricingResultDto response in responses)
				{
					foreach (string errorId in response.ErrorResourceIds)
					{
						foreach (string rid in errorIds)
						{
							if	(rid.Equals(errorId))
							{
								break;
							}
						}

						errorIds.Add(errorId);
					}
				}

				foreach (string errorId in transformErrors)
				{
					foreach (string rid in errorIds)
					{
						if	(rid.Equals(errorId))
						{
							break;
						}
					}

					errorIds.Add(errorId);
				}


				if (TDTraceSwitch.TraceVerbose) 
				{
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PriceRouteResponseMessage.Message(dates)));
				}
			
			}
			catch (Exception ex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Exception trying to to call RetailBusinessObjectsFacade.GetFaresForRoute()", ex));
				errorIds.Add(UNSPECIFIED_BO_ERROR);
			}

			return (string[])(errorIds.ToArray(typeof(string)));

		}

		/// <summary>
		/// Obtain parameters required to restrict subsequent CJP enquiry
		///  to those services that might be valid for a specified fare 
		/// </summary>
		/// <param name="outwardDate">Outward date</param>
		/// <param name="returnDate">Return date (null for single journey)</param>
		/// <param name="outwardFareData">Fare-specific information about selected outward fare</param>
		/// <param name="returnFareData">Fare-specific information about selected return fare</param>
		/// <returns>Array of RailServiceParameters DTO's containing required service parameters
		///            - the array contains two members, for outward and return journeys (if applicable)
		/// </returns>
		
		public RailServiceParameters[] ServiceParametersForFare(TDDateTime outwardDate, TDDateTime returnDate, 
																	RailFareData outwardFareData, RailFareData returnFareData)
		{

			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					ServiceParameterRequestMessage.Message(outwardDate, returnDate, outwardFareData, returnFareData)));
			}

			FareDataDto[] fareDataDtos = new FareDataDto[returnFareData == null ? 1 : 2];

			fareDataDtos[0] = new FareDataDto(outwardFareData.ShortTicketCode, outwardFareData.RouteCode, 
													outwardFareData.RestrictionCode, outwardFareData.OriginNlc, 
													outwardFareData.DestinationNlc, outwardFareData.RailcardCode, 
													outwardFareData.IsReturn, string.Empty,
													outwardFareData.Origins, outwardFareData.Destinations, false, 
                                                    outwardFareData.RawFareString, 
                                                    false, false, false, string.Empty);

			if	(returnFareData != null) 
			{
				fareDataDtos[1] = new FareDataDto(returnFareData.ShortTicketCode, returnFareData.RouteCode, 
													returnFareData.RestrictionCode, returnFareData.OriginNlc, 
													returnFareData.DestinationNlc, returnFareData.RailcardCode, 
													returnFareData.IsReturn, string.Empty,
													returnFareData.Origins, returnFareData.Destinations, false,
                                                    returnFareData.RawFareString,
                                                    false, false, false, string.Empty);
			}

			// Create a PricingRequestDto from the input parameters ...
			GatewayTransform transform = new GatewayTransform();
			PricingRequestDto request = transform.MapServiceParametersRequest(outwardFareData.Origin, outwardFareData.Destination, outwardFareData, outwardDate, returnDate, outwardFareData.RailcardCode);

			// Make the request to the RetailBusinessObject service
			RetailBusinessObjectsFacade rboFacade = new RetailBusinessObjectsFacade();

			TTBOParametersDto[] ttboParmsDtos;

			try
			{
				ttboParmsDtos = rboFacade.GetServiceParametersForFare(request, fareDataDtos);
			}
			catch (Exception ex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Exception trying to to call RetailBusinessObjectsFacade.GetServiceParametersForFare()", ex));
				
				ttboParmsDtos = new TTBOParametersDto[1];
				ttboParmsDtos[0] = new TTBOParametersDto();
				ttboParmsDtos[0].AddErrorMessage(UNSPECIFIED_BO_ERROR);
			}
			
			RailServiceParameters[] responses = transform.MapServiceParametersResponses(ttboParmsDtos);
			
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
																ServiceParameterResponseMessage.Message(responses)));
			}

			return responses;
		}



		/// <summary>
		/// Validate rail services returned by the CJP by re-checking restriction  
		///  codes and obtaining applicable supplement and availability information
		/// </summary>
		/// <param name="origin">Origin location of journey</param>
		/// <param name="destination">Destination location of journey</param>
		/// <param name="outwardDate">Outward date</param>
		/// <param name="returnDate">Return date</param>
		/// <param name="fareData">Information about selected fare</param>
		/// <param name="outwardJourneys">Array of PublicJourneys to be validated for outward direction</param>
		/// <param name="returnJourneys">Array of PublicJourneys to be validated for inward direction</param>
		/// <param name="restrictionCodesToReapply">Codes returned by the ServiceParametersForFare call</param>
		/// <returns>RailServiceValidationResultsDto summarising results of validation</returns>
		
		public RailServiceValidationResultsDto ValidateServicesForFare
					(TDLocation origin, TDLocation destination, TDDateTime outwardDate, 
						TDDateTime returnDate, RailFareData outwardFareData, RailFareData returnFareData, 
						ArrayList outwardJourneys, ArrayList returnJourneys,
						string outwardRestrictionCodesToReapply, string returnRestrictionCodesToReapply,
						bool outwardTocCheckRequired, bool returnTocCheckRequired,
                        bool outwardCrossLondonToCheck, bool returnCrossLondonToCheck,
                        bool outwardZonalIndicatorToCheck, bool returnZonalIndicatorToCheck,
                        bool outwardVisitCRSToCheck, bool returnVisitCRSToCheck,
                        string outwardOutputGL, string returnOutputGL)
		{

			
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					ServiceValidationRequestMessage.Message(origin, destination, outwardDate, returnDate, outwardFareData, returnFareData, 
																outwardJourneys, returnJourneys, outwardRestrictionCodesToReapply, returnRestrictionCodesToReapply,
                                                                outwardTocCheckRequired, returnTocCheckRequired,
                                                                outwardCrossLondonToCheck, returnCrossLondonToCheck,
                                                                outwardZonalIndicatorToCheck, returnZonalIndicatorToCheck,
                                                                outwardVisitCRSToCheck, returnVisitCRSToCheck)));
			}
			
			RetailBusinessObjectsFacade rboFacade = new RetailBusinessObjectsFacade();
		
			FareDataDto[] fareDataDtos = new FareDataDto[returnFareData == null ? 1 : 2];

			fareDataDtos[0] = new FareDataDto(outwardFareData.ShortTicketCode, outwardFareData.RouteCode, 
												outwardFareData.RestrictionCode, outwardFareData.OriginNlc,							
												outwardFareData.DestinationNlc, outwardFareData.RailcardCode, 
												outwardFareData.IsReturn, outwardRestrictionCodesToReapply,
												outwardFareData.Origins, outwardFareData.Destinations,
												outwardTocCheckRequired, outwardFareData.RawFareString,
                                                outwardCrossLondonToCheck, outwardZonalIndicatorToCheck,
                                                outwardVisitCRSToCheck, outwardOutputGL);

			if	(returnFareData != null) 
			{
				fareDataDtos[1] = new FareDataDto(returnFareData.ShortTicketCode, returnFareData.RouteCode, 
													returnFareData.RestrictionCode, returnFareData.OriginNlc,							
													returnFareData.DestinationNlc, returnFareData.RailcardCode, 
													returnFareData.IsReturn, returnRestrictionCodesToReapply,
													returnFareData.Origins, returnFareData.Destinations,
													returnTocCheckRequired, returnFareData.RawFareString,
                                                    returnCrossLondonToCheck, returnZonalIndicatorToCheck,
                                                    returnVisitCRSToCheck, returnOutputGL);
			}

			// Create an array of PricingRequestDto's from the input parameters ...
			GatewayTransform transform = new GatewayTransform();
			
			PricingRequestDto[] requests = transform.MapServiceValidationRequest(outwardDate, returnDate, origin, destination, 
																					outwardFareData.RailcardCode, 
																					outwardJourneys, returnJourneys);
			RailServiceValidationResultsDto response;

			try 
			{
				response = rboFacade.ValidateServiceDetailsForFareAndJourney(requests, fareDataDtos);
			}
			catch (Exception ex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Exception trying to to call RetailBusinessObjectsFacade.ValidateServiceDetailsForFareAndJourney()", ex));

				response = new RailServiceValidationResultsDto();
				response.AddErrorMessage(UNSPECIFIED_BO_ERROR);
			}

			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					ServiceValidationResponseMessage.Message(response)));
			}

			return response;
		}
	}
}
