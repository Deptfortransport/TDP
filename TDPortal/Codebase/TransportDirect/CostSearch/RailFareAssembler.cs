// ************************************************************** 
// NAME			: RailFareAssembler.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/10/2005 
// DESCRIPTION	: RailFareAssembler
//				  This class is responsible for using the rail fare  
//				  provider (Business Objects) to obtain all available tickets     
//				  for a specified pair of locations and range of date(s).
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/RailFareAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:20   mturner
//Initial revision.
//
//   Rev 1.2   Jan 16 2006 19:16:46   RPhilpott
//Code review fixes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.1   Dec 22 2005 17:17:56   RWilby
//Update for code review
//
//   Rev 1.0   Oct 28 2005 15:31:32   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Collections;
using Logger = System.Diagnostics.Trace;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// This class is responsible for using the rail fare  
	/// provider (Business Objects) to obtain all available tickets     
	///	for a specified pair of locations and range of date(s).
	/// </summary>
	public class RailFareAssembler : FareAssembler
	{
		public RailFareAssembler()
		{}


		/// <summary>
		/// Returns a CostSearchResult for Rail journeys containing travel dates with fares information,
		/// based on the CostSearchRequest 
		/// </summary>
		public override  ICostSearchResult AssembleFares(ICostSearchRequest request)
		{
			//this ArrayList will get converted to the CostSearchResult 
			//returned at the end of the AssembleFares method
			arrayListResults = new ArrayList();

			//errors
			faresErrors = new ArrayList();
			CostSearchError csError = new CostSearchError();
			//initialise base class members
			theRequest = request;			
			//the return value
			faresResult = new CostSearchResult();					
			//array lists of travel dates for each travel mode
			ArrayList railTravelDates = new ArrayList();

			//session id needed for logging purposes
			string sessionId = request.SessionInfo.SessionId;
	
			//check if request is a return
			bool isReturn = (request.ReturnDateTime != null);	

			//assign discounts from the request to pass to the price supplier
			discounts = new Discounts(request.RailDiscountedCard, request.CoachDiscountedCard, TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.All);
				
			try
			{						
				//create array list of travel dates for all possible date permutations
				railTravelDates = CreateTravelDatePermutations(isReturn);
				//add mode to each travel date
				foreach (TravelDate item in railTravelDates)
				{
					item.TravelMode = TicketTravelMode.Rail;
				}		
				
				//call rail route price supplier to get fares information
				try
				{
					//Get the RoutePriceSupplierFactory from ServiceDiscovery
					IRoutePriceSupplierFactory routePriceSupplierFactory = (IRoutePriceSupplierFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.RoutePriceSupplierFactory];
				
					IRoutePriceSupplier routePriceSuppplier =  routePriceSupplierFactory.GetSupplier(TicketTravelMode.Rail,string.Empty);
					
					priceRouteErrors = routePriceSuppplier.PriceRoute( railTravelDates, request.OriginLocation, request.DestinationLocation, discounts,request.SessionInfo,null,0,0,null);

				}
				catch (TDException tdEx)
				{
					//log message 
					string message = tdEx.Message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString();
					Logger.WriteLine(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));	
				}
				
				//check if errors returned from the Rail Price Supplier 
				if ((priceRouteErrors != null ) && (priceRouteErrors.Length > 0))
				{
					// add errors to be added to the CostSearchResult 
					foreach (string error in priceRouteErrors)
					{
						csError.ResourceID = error.ToString();	
						//add CostSearchError to array list of errors
						faresErrors.Add(csError);	
					}
				}

				//assign availability to each ticket
				UpdateTicketAvailability(railTravelDates, TicketTravelMode.Rail);

				//update lowest probable fares info for each travel dates
				UpdateTravelDateFares(railTravelDates);

				//add updated rail TravelDates to an overall array list
				AddTravelDatesToFaresResult(railTravelDates);						
				
			}					
			catch (TDException tdEx)
			{							
				//log message 
				string message = tdEx.Message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString();
				Logger.WriteLine(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));	
			}

			//assign the overall array list of travel dates to the the CostSearchResult
			if (arrayListResults != null) 
			{
				faresResult.TravelDates = (TravelDate[])arrayListResults.ToArray(typeof(TravelDate));				
			}
			else
			{	
				//if the result has no travel dates then log the problem		
				if (TDTraceSwitch.TraceVerbose) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"The CostSearchResult returned from RailFareAssembler.AssembleFares has no TravelDates. " +
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

			//return the result
			return faresResult;
		}

	}
}
