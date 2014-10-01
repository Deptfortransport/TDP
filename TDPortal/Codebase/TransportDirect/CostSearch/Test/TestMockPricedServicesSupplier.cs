// ************************************************************** 
// NAME			: TestMockPricedServicesSupplier.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 02/03/2005 
// DESCRIPTION	: Mock implemention of IRoutePriceSupplier
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockPricedServicesSupplier.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 08:49:30   mmodi
//Updated validate services method interface to use the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 16:21:56   mturner
//Initial revision.
//
//   Rev 1.3   Dec 05 2005 18:26:42   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.2   Nov 24 2005 18:23:04   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.1   Nov 02 2005 09:31:12   RWilby
//Updated Unit Tests
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:26:56   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Collections;

using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for TestMockPricedServicesSupplier.
	/// </summary>
	public class TestMockPricedServicesSupplier : IPricedServicesSupplier
	{
		public TestMockPricedServicesSupplier()
		{
		}
		
		/// <summary>
		/// Obtain parameters required to restrict subsequent CJP enquiry
		///  to those services that might be valid for a specified fare 
		/// </summary>
		/// <param name="outwardDate">Outward date</param>
		/// <param name="returnDate">Return date</param>
		/// <param name="outwardFareData">Fare-specific information about selected outward fare</param>
		/// <param name="returnFareData">Fare-specific information about selected return fare</param>
		/// <returns>Array of RailServiceParameters DTO's containing required service parameters
		///            - the array contains two members, for outward and return journeys (if applicable)
		/// </returns>
		
		public	RailServiceParameters[] ServiceParametersForFare(TDDateTime outwardDate, TDDateTime returnDate, 
																	RailFareData outwardFareData, RailFareData returnFareData)
		{
			
			TTBOParametersDto ttboParmsDtos = new TTBOParametersDto();
			TDLocation[] inLocs = null;
			TDLocation[] exLocs = null;

			RailServiceParameters railServiceParams = new RailServiceParameters(ttboParmsDtos,inLocs,exLocs);

			RailServiceParameters[] railServiceParamsArray = new RailServiceParameters[2];

			railServiceParamsArray[0] = railServiceParams;
			railServiceParamsArray[1] = railServiceParams;

			return railServiceParamsArray;
		}

		/// <summary>
		/// Validate rail services returned by the CJP by re-checking restriction  
		///  codes and obtaining applicable supplement and availability information
		/// </summary>
		/// <param name="origin">Origin location of journey</param>
		/// <param name="destination">Destination location of journey</param>
		/// <param name="outwardDate">Outward date</param>
		/// <param name="returnDate">Return date</param>
		/// <param name="outwardFareData">Information about selected outward fare</param>
		/// <param name="returnFareData">Information about selected return fare</param>
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
			return new RailServiceValidationResultsDto();
		}
	}
}