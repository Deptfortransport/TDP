// *********************************************** 
// NAME			: IPricedServicesSupplier.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-03-01
// DESCRIPTION	: IPricedServicesSupplier interface
// ************************************************ 
//
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IPricedServicesSupplier.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 09:08:02   mmodi
//Updated interface to provide additional parameters to allow the validation of services using the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:36:48   mturner
//Initial revision.
//
//   Rev 1.4   Dec 05 2005 18:26:42   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.3   Nov 24 2005 18:23:02   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.2   Mar 22 2005 16:08:56   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.1   Mar 02 2005 18:11:02   RPhilpott
//Add restrictionCodesToReapply parameter.
//
//   Rev 1.0   Mar 01 2005 18:45:32   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Interface to be fulfilled by all "priced service" suppliers
	/// </summary>
	public interface IPricedServicesSupplier
	{

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
		RailServiceParameters[] ServiceParametersForFare(TDDateTime outwardDate, TDDateTime returnDate,
															RailFareData outwardFareData, RailFareData returnFareData); 

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
        /// <param name="tocCheckRequired">Flag indicating TOCs should be checked in the validation</param>
        /// <param name="crossLondonToCheck">Flag indicating the journeys go through london and needs cross london validation</param>
        /// <param name="zonalIndicatorToCheck">Flag indicating the journeys require zonal validation</param>
        /// <param name="visitCRSToCheck">Flag indicating the significant CRS locations which are to be checked in the validation</param>
        /// <param name="outputGL">The unaltered output from the RBO GL call containing the CRS locations to check in the validation</param>
		/// <returns>RailServiceValidationResultsDto summarising results of validation</returns>
		RailServiceValidationResultsDto ValidateServicesForFare
			(TDLocation origin, TDLocation destination, TDDateTime outwardDate, 
			   TDDateTime returnDate, RailFareData outwardFareData, RailFareData returnFareData, 
			   ArrayList outwardJourneys, ArrayList returnJourneys,
			   string outwardRestrictionCodesToReapply, string returnRestrictionCodesToReapply,
			   bool outwardTocCheckRequired, bool returnTocCheckRequired,
               bool outwardCrossLondonToCheck, bool returnCrossLondonToCheck,
               bool outwardZonalIndicatorToCheck, bool returnZonalIndicatorToCheck,
               bool outwardVisitCRSToCheck, bool returnVisitCRSToCheck,
               string outwardOutputGL, string returnOutputGL); 

	}
}
