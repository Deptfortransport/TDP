// ************************************************************** 
// NAME			: CostSearchFacade.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the CostSearchFacade class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/CostSearchFacade.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:16   mturner
//Initial revision.
//
//   Rev 1.42   Nov 09 2005 12:23:44   build
//Automatically merged from branch for stream2818
//
//   Rev 1.41.1.2   Nov 07 2005 19:59:34   RWilby
//Updates AssembleServices methods to get the TravelMode from ICostSearchticket instead of the ICostSearchRequest
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.41.1.1   Nov 02 2005 09:34:34   RWilby
//Updated class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.41.1.0   Oct 28 2005 15:28:40   RWilby
//Refactored class for TD096 -Search by Price
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.41   Apr 29 2005 10:21:56   jbroome
//AvailabilityEstimates for return tickets now use outward and return dates.
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.40   Apr 27 2005 09:54:42   jmorrissey
//Update to BuildRaulCjpRequest method
//Resolution for 2290: PT - Coach - Session data for cost based searching
//
//   Rev 1.39   Apr 25 2005 18:50:28   jmorrissey
//Implemented fix in AssembleServices (Singles) method.
//Resolution for 2191: PT - Correct dates not being used to obtain services for selected ticket
//
//   Rev 1.38   Apr 25 2005 17:53:26   jmorrissey
//In AssemblesFares method, the call to PriceRoute for coach now returns coach journeys in a separate object called PublicJourneyStore.
//
//AssembleServices no longer handles coach mode. This has moved to CostSearchRunner.
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.37   Apr 25 2005 10:12:52   jbroome
//Work in progress - updated after change to PriceRoute method
//
//   Rev 1.36   Apr 23 2005 18:40:58   RPhilpott
//Do not add minimum fare warning unless minimum is more thhan the actual discounted fare.
//Resolution for 2312: Del 7 - PT - over-enthusiatic use of "minimum fares" warning
//
//   Rev 1.35   Apr 23 2005 12:53:38   jmorrissey
//After amendments to work with updated AvailabilityEstimator class
//
//   Rev 1.34   Apr 22 2005 17:08:50   jmorrissey
//Now sets a ticket's Probability to none if no journeys found for a ticket.
//Resolution for 2106: PT- Find a fare will not progress from Step 2: Select a Fare page to Step 3: Select a Journey page
//
//   Rev 1.33   Apr 22 2005 14:23:34   jmorrissey
//Updated after FxCop run
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.32   Apr 22 2005 11:08:34   jmorrissey
//Change to updating Availability data
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.31   Apr 20 2005 13:53:02   jmorrissey
//Fixes for return journeys.
//Resolution for 2123: PT - Train - Return journey results produced in triplicate
//
//   Rev 1.30   Apr 20 2005 10:29:52   RPhilpott
//Improve handling of error conditions.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.29   Apr 19 2005 18:12:16   jmorrissey
//Updated after nunit testing.
//Resolution for 2123: PT - Train - Return journey results produced in triplicate
//Resolution for 2150: PT - Slow response times on journey selection page
//
//   Rev 1.28   Apr 19 2005 11:27:00   jmorrissey
//Fix for AvailabilityEstimator bug
//
//   Rev 1.27   Apr 14 2005 16:03:42   jmorrissey
//Update to UpdateTicketAvailability method for IR2100.  All fares information now made by call to UpdateMaxMinFares in TravelDate class. This call is made from UpdateTravelDateFares.
//Resolution for 2100: PT: Find a Fare not correctly handling missing fare information
//
//   Rev 1.26   Apr 12 2005 16:37:14   jmorrissey
//Change to UpdateTicketAvailability method so that tickets with NoAvailability are removed from the results
//Resolution for 2107: PT: Find Fare initial search can return tickets with No Availability
//
//   Rev 1.25   Apr 12 2005 11:44:42   RPhilpott
//Set MinimumFare message in TDJourneyResult.
//Resolution for 2071: PT: Minimum train fares not flagged
//
//   Rev 1.24   Apr 11 2005 17:04:56   RPhilpott
//Correct usage of PublicJourney.JourneyIndex property.
//Resolution for 2070: PT: Incorrect trains displayed for specific date/fare.
//
//   Rev 1.23   Apr 09 2005 13:29:18   jmorrissey
//Now sets MinimumFareApplies on each PublicJourney correctly. Removes any PublicJourney where NoPlacesAvailable.
//
//   Rev 1.22   Apr 09 2005 12:51:42   jmorrissey
//Changed some private method names. Now calls UpdateAvailabilityEstimate twice for Singles tickets.
//
//   Rev 1.21   Apr 09 2005 10:27:36   jmorrissey
//Change to AddTravelDatesToFaresResult. Now returns results for more than one mode. 
//
//   Rev 1.20   Apr 08 2005 16:05:50   jmorrissey
//Changes to AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket ticket) method
//
//   Rev 1.19   Apr 07 2005 17:55:44   jmorrissey
//Change to BuildRailCjpRequest
//
//   Rev 1.18   Apr 07 2005 11:02:38   jmorrissey
//Update to AddTravelDatesToFaresResult method. Filters out any TravelDates that do not have any tickets.
//
//   Rev 1.17   Apr 06 2005 17:09:12   jmorrissey
//Updated so that CostSearchResult.ResultId is only set when AssembleFares completes successfully. Added some extra checks for null objects and improved exception handling.
//
//   Rev 1.16   Apr 05 2005 18:18:36   jmorrissey
//Updates to error handling. Removed fare calculation code again! This is back in the price supplier code again.
//
//   Rev 1.15   Apr 04 2005 11:14:18   jmorrissey
//Update to AssembleServices methods. Now correctly sets IsValid property of TDJourneyResult.
//
//   Rev 1.14   Apr 04 2005 09:12:46   jmorrissey
//Restored fare handling code in CalculateFaresRanges
//
//   Rev 1.13   Apr 01 2005 20:03:58   jmorrissey
//Fixed typo with CoachTravelDates\RailTravelDates\AirTravelDates
//
//   Rev 1.12   Apr 01 2005 18:40:08   jmorrissey
//Updated CreateTravelDatePermutations method
//
//   Rev 1.11   Apr 01 2005 10:54:18   jmorrissey
//Removed some code that calculates max and min fares. This functionality is already doen by the price suppliers
//
//   Rev 1.10   Mar 31 2005 17:02:40   jmorrissey
//Factored out some code into CalculateFaresRanges method
//
//   Rev 1.9   Mar 30 2005 17:59:48   jmorrissey
//Updated after change to Ticket structure
//
//   Rev 1.8   Mar 29 2005 17:01:44   jmorrissey
//After further integration
//
//   Rev 1.7   Mar 22 2005 17:54:30   jmorrissey
//After initial back end integration
//
//   Rev 1.6   Mar 22 2005 10:57:26   jmorrissey
//Added overload of AssembleServices method that takes an outward and inward ticket. Fixed warnings. Changed how journey result is stored on tickets for coach and air modes in AssembleServices
//
//   Rev 1.5   Mar 15 2005 17:56:32   jmorrissey
//Removed GetDateRange method and moved functionality to TDDateTime in common project
//
//   Rev 1.4   Mar 14 2005 16:25:06   jmorrissey
//Updated after design changes.
//
//   Rev 1.3   Mar 13 2005 17:01:24   jmorrissey
//Checked in to allow integration testing. However updates still ongoing and Nunit tests to be run on this code.
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.2   Feb 22 2005 16:50:34   jmorrissey
//Update to AssembleServices signature
//
//   Rev 1.1   Jan 12 2005 13:52:30   jmorrissey
//Latest versions. Still in development.
//
//   Rev 1.0   Dec 22 2004 11:59:48   jmorrissey
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for CostSearchFacade.
	/// </summary>
	public class CostSearchFacade : ICostSearchFacade
	{

		/// <summary>
		/// default constructor
		/// </summary>
		public CostSearchFacade()
		{}

		#region ICostSearchFacade implemention
		/// <summary>
		/// Returns a CostSearchResult containing travel dates with fares information,
		/// based on the CostSearchRequest 
		/// </summary>
		public ICostSearchResult AssembleFares(ICostSearchRequest request)
		{
			ICostSearchResult result = new CostSearchResult();
			ArrayList resultTravelDates = new ArrayList();
			ArrayList resultErrors = new ArrayList();

			result.ResultId = request.RequestId;
			
			foreach (TicketTravelMode mode in request.TravelModes)
			{
				FareAssembler fareassembler = FareAssembler.GetAssembler(mode);

				ICostSearchResult modeResult =  fareassembler.AssembleFares(request);
				
				//Add to arraylist to combine modeResults to main result
				foreach (TravelDate td in modeResult.GetAllTravelDates())
				{
					resultTravelDates.Add(td);
				}
				foreach (CostSearchError error in modeResult.GetErrors())
				{
					resultErrors.Add(error);
				}
			}

			//Convert arraylist to array
			result.TravelDates  = (TravelDate[])resultTravelDates.ToArray(typeof(TravelDate));
			
			foreach (CostSearchError error in resultErrors)
			{
				result.AddError(error);
			}

			return result;
		}


		/// <summary>
		/// Returns a CostSearchResult with journey information for the selected ticket		
		/// </summary>
		public ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket ticket)
		{			
			//Get appropriate ServiceAssembler for TravelMode
			ServiceAssembler serviceAssembler = ServiceAssembler.GetAssembler(ticket.TravelDateForTicket.TravelMode);

			//Invoke ServiceAssembler.AssembleServices method
			ICostSearchResult result =  serviceAssembler.AssembleServices(request,existingResult,ticket);
				
			return result;
		}			

		
		/// <summary>
		/// Overloaded version of AssembleServices method. 
		/// Returns a CostSearchResult with journey information for 2 Singles tickets		
		/// </summary>
		public ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket outwardTicket, CostSearchTicket inwardTicket)
		{
	
			//Get appropriate ServiceAssembler for TravelMode
			ServiceAssembler serviceAssembler = ServiceAssembler.GetAssembler(outwardTicket.TravelDateForTicket.TravelMode);
			
			//Invoke ServiceAssembler.AssembleServices method
			ICostSearchResult result =  serviceAssembler.AssembleServices(request,existingResult,outwardTicket,inwardTicket);
				
			return result;
		}

		#endregion
	}
}
