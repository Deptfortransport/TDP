// *********************************************** 
// NAME                 : EnhancedExposedOperationType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 10/01/2006 
// DESCRIPTION  		: Enumerator class for Enhanced Exposed Service Operation Type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesCommon/Common/EnhancedExposedOperationType.cs-arc  $ 
//
//   Rev 1.4   Apr 30 2012 13:29:26   DLane
//Updates for EES event recording in batch
//Resolution for 5805: Record batch EES events in the reporting db
//
//   Rev 1.3   Jan 12 2012 15:33:58   PScott
//Add fuel Genie find nearest methods to EES
//Resolution for 5781: Fuel Genie EES
//
//   Rev 1.2   Sep 29 2010 11:27:42   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.1   Aug 04 2009 13:33:08   mmodi
//Added PlanCarJourney
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:22:04   mturner
//Initial revision.
//
//   Rev 1.7   Apr 11 2006 11:55:24   build
//Automatically merged from branch for stream0036
//
//   Rev 1.6.1.0   Apr 07 2006 12:02:44   asinclair
//Add open journey planner operation
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.7   Apr 06 2006 16:04:08   COwczarek
//Add open journey planner operation
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.6   Mar 13 2006 14:17:38   RWilby
//Added FindNearestLocality operation
//Resolution for 3624: EES FindNearestLocality web method
//
//   Rev 1.5   Jan 23 2006 10:20:42   mdambrine
//Addition of operation and service type for journeyplanner
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 19 2006 12:11:20   RWilby
//Added operations for Find Nearest service
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.3   Jan 16 2006 13:46:28   schand
//Added methods for code service and taxi info
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 13 2006 17:18:06   mtillett
//Add correct names for the test web methods
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 13 2006 15:22:20   schand
//Added all EES operaration type
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 10 2006 15:21:24   schand
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.Common
{
	/// <summary>
	/// Enumerator class for Enhanced Exposed Service Operation Type
	/// </summary>
	public enum EnhancedExposedOperationType
	{
		//testing only
		WSDLValidation,
		RequestContextData,
		//departureboard methods
		GetDepartureBoard,
		GetDepartureBoardTimeRequestTypes,
		// Travel News methods
		GetTravelNewsAvailableRegions,
		GetTravelNewsDetails,
		GetTravelNewsDetailsByUid,
		GetTravelNewsHeadlines,
		GetTravelNewsRegion,
		// Code Service methods
		FindCode,
		FindText,
		// Taxi Information
		GetTaxiInfo,
		//Find Nearest
		GetGridReference,
		FindNearestAirports,
		FindNearestStations,
		FindNearestBusStops,
		FindNearestLocality,
        // Journey Planning
		PlanPublicJourney,
        PlanJourney,
        PlanCarJourney,
        PlanCycleJourney,
        //Cycle Planner Methods
        GetCycleAlgorithms,
        GetCycleAttributes,
        GetCycleRequestPreferences,
        //Gradient Profile
        GetGradientProfile,
        //FuelGenie
        FindNearestFuelGenie,
        FindFuelGenieSites,
        BatchCarJourneyOutward,
        BatchCarJourneyReturn,
        BatchCycleJourneyOutward,
        BatchCycleJourneyReturn,
        BatchPublicJourneyOutward,
        BatchPublicJourneyReturn
    }
}
