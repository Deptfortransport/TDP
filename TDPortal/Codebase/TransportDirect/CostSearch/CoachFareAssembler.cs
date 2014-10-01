// ************************************************************** 
// NAME			: CoachFareAssembler.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/10/2005 
// DESCRIPTION	: Definition of the CoachFareAssembler
//                This class is responsible for using the coach fare  
//					providers to obtain all available tickets for a    
//					specified pair of locations and range of date(s).
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/CoachFareAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:14   mturner
//Initial revision.
//
//   Rev 1.8   Jan 16 2006 19:16:44   RPhilpott
//Code review fixes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.7   Dec 22 2005 17:19:00   RWilby
//Updated for code review
//
//   Rev 1.6   Dec 06 2005 11:32:04   mguney
//In AssembleFares method, UpdateTicketAvailability method is removed. Code for removing traveldates with no tickets as this was being done in UpdateTicketAvailability.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.5   Nov 30 2005 09:37:26   RPhilpott
//Correct handling of via locations on multi-ticket requests.
//Resolution for 2991: DN040: SBP Pricing extended journey
//
//   Rev 1.4   Nov 07 2005 18:20:24   RWilby
//Fixed CombinedTicketIndex bug
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 07 2005 17:01:16   RWilby
//Added check for null routePriceSuppplier
//
//   Rev 1.2   Nov 07 2005 12:04:44   RWilby
//Fixed bug in AssembleFares method
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 05 2005 16:36:04   RWilby
//Changed combinedTicketIndex to only increment if the route has more than 1 leg
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:31:30   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Collections;
using Logger = System.Diagnostics.Trace;

using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// This class is responsible for using the coach fare  
	/// providers to obtain all available tickets for a    
	///	specified pair of locations and range of date(s).
	/// </summary>
	public class CoachFareAssembler :FareAssembler
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public CoachFareAssembler()
		{}

	
		/// <summary>
		/// Returns a CostSearchResult for coach journeys containing travel dates with fares information,
		/// based on the CostSearchRequest 
		/// </summary>
		/// <param name="request">ICostSearchRequest</param>
		/// <returns>ICostSearchResult</returns>
		public override ICostSearchResult AssembleFares(ICostSearchRequest request)
		{
			//this ArrayList will get converted to the CostSearchResult 
			//returned at the end of the AssembleFares method
			arrayListResults = new ArrayList();

			//errors
			faresErrors = new ArrayList();
			//initialise base class members
			theRequest = request;			
			//the return value
			faresResult = new CostSearchResult();					
			//array lists of travel dates for each travel mode
			ArrayList coachTravelDates = new ArrayList();

			//session id needed for logging purposes
			string sessionId = request.SessionInfo.SessionId;
	
			//check if request is a return
			bool isReturn = (request.ReturnDateTime != null);	

			//assign discounts from the request to pass to the price supplier
			discounts = new Discounts(request.RailDiscountedCard, request.CoachDiscountedCard, TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.All);

			try
			{
				//create array list of travel dates for all possible date permutations
				coachTravelDates = CreateTravelDatePermutations(isReturn);
				//add mode to each travel date
				foreach (TravelDate item in coachTravelDates)
				{
					item.TravelMode = TicketTravelMode.Coach;
				}		
				
				//Get CoachRoutesQuotaFaresProvider from ServiceDiscovery
				ICoachRoutesQuotaFaresProvider coachRoutesQuotaFaresProvider 
					= (ICoachRoutesQuotaFaresProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachRoutesQuotaFareProvider];
			
				//combinedTicketIndexCounter is used to set a unique value to all legs for routes with more than one leg
				int combinedTicketIndexCounter = 0;
				int combinedTicketIndex = 0;

				//For each OriginLocation.NaPTANs[]/DestinationLocation.NaPTANs[] permutation find all possible routes
				foreach (TDNaptan originNaptan in  request.OriginLocation.NaPTANs)
				{
					foreach (TDNaptan destinationNaptan in  request.DestinationLocation.NaPTANs)
					{
						//Retrieve list of all possible routes for journey 
						RouteList routeList =coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares(originNaptan.Naptan,destinationNaptan.Naptan);
						
						foreach (Route route in routeList)
						{
							//Set Leg Number to zero for each new route
							int legNumber = 0;

							//Only increment the combinedTicketIndexCounter if the route has more than 1 leg
							//as otherwise we don't need to combine the tickets on the UI
							if  (route.LegList.Count > 1)
							{
								combinedTicketIndexCounter ++;
								combinedTicketIndex = combinedTicketIndexCounter;
							}
							else
							{
								combinedTicketIndex = 0;
							}

							foreach (PricingRetail.CoachRoutes.Leg leg in route.LegList)
							{
								//Call coach route price supplier to get fares information
								//Get the RoutePriceSupplierFactory from ServiceDiscovery
								IRoutePriceSupplierFactory routePriceSupplierFactory = (IRoutePriceSupplierFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.RoutePriceSupplierFactory];
				
								IRoutePriceSupplier routePriceSuppplier =  routePriceSupplierFactory.GetSupplier(TicketTravelMode.Coach,leg.CoachOperatorCode);
	
								//check for null routePriceSuppplier
								if (routePriceSuppplier != null)
								{
									//Construct two TDLocation containing just one TDNaptan for the legs origin and destination
									TDLocation originLocation = new TDLocation();
									TDNaptan legOriginNaptan = new TDNaptan();
									legOriginNaptan.Naptan = leg.StartNaPTAN;
									TDNaptan[] originLocationNaptans = new TDNaptan[]{legOriginNaptan};	
									originLocation.NaPTANs = originLocationNaptans;

									TDLocation destinationLocation = new TDLocation();
									TDNaptan legDestinationNaptans = new TDNaptan();
									legDestinationNaptans.Naptan = leg.EndNaPTAN;
									TDNaptan[] DestinationLocationNaptans = new TDNaptan[]{legDestinationNaptans};	
									destinationLocation.NaPTANs = DestinationLocationNaptans;

									priceRouteErrors = routePriceSuppplier.PriceRoute( coachTravelDates, originLocation, destinationLocation, discounts,request.SessionInfo,leg.CoachOperatorCode,combinedTicketIndex,legNumber,leg.QuotaFareList);
								
									//increment the legNumber
									//This is used by the UI layer to group tickets that apply to seperate legs of the same journey
									legNumber ++;

									//check if errors returned from the Coach Price Supplier 
									if ((priceRouteErrors != null ) && (priceRouteErrors.Length > 0))
									{
										// add errors to be added to the CostSearchResult 
										foreach (string error in priceRouteErrors)
										{
											CostSearchError csError = new CostSearchError();
											csError.ResourceID = error.ToString();	
											//add CostSearchError to array list of errors
											faresErrors.Add(csError);	
										}
									}
								}
								else
								{
									//RoutePriceSupplierFactory returned a null RoutePriceSuppplier
									//Log this has a CostSearchError : FaresInternalError
									CostSearchError csError = new CostSearchError();
									csError.ResourceID = FARES_INTERNAL_ERROR;
									//add CostSearchError to array list of errors
									faresErrors.Add(csError);
								}
							}
						}
					}
				}
			
				//remove traveldates with no tickets.
				for (int i=coachTravelDates.Count-1; i >= 0; i--)
				{				
					TravelDate thisDate = (TravelDate)coachTravelDates[i];
 
					//if no tickets for a travel date then remove it from the array list				
					if (!thisDate.HasTickets)
					{
						coachTravelDates.Remove(thisDate);						
					}
				}

				//update lowest probable fares info for each travel dates
				UpdateTravelDateFares(coachTravelDates);

				//add updated rail TravelDates to an overall array list
				AddTravelDatesToFaresResult(coachTravelDates);
	
				//assign the overall array list of travel dates to the the CostSearchResult
				if (arrayListResults != null) 
				{
					faresResult.TravelDates = (TravelDate[])arrayListResults.ToArray(typeof(TravelDate));				
				}
				else
				{	
					if (TDTraceSwitch.TraceVerbose) 
					{
			
						//if the result has no travel dates then log the problem			
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
							"The CostSearchResult returned from CoachFareAssembler.AssembleFares has no TravelDates. " +
							"Check Web log and Pricing logs for previous errors. SessionId = " + sessionId));				
					}
				}
			
				//assign list of errors to the result
				foreach (CostSearchError error in faresErrors)
				{
					faresResult.AddError(error);
				}

				//match up the result with the request
				faresResult.ResultId = request.RequestId;	

			}
			catch (TDException tdEx)
			{
				//log message 
				string message = tdEx.Message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString();

				Logger.WriteLine(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					message));	
			}

			//return the result
			return faresResult;
		}
	}
}
