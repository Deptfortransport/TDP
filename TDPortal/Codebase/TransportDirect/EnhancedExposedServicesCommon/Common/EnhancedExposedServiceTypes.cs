// *********************************************** 
// NAME                 : EnhancedExposedServiceTypes.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 07/02/2005
// DESCRIPTION  : 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesCommon/Common/EnhancedExposedServiceTypes.cs-arc  $ 
//
//   Rev 1.2   Sep 29 2010 11:27:42   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.1   Aug 04 2009 13:33:50   mmodi
//Added CarJourneyPlannerSynchronous web service type
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:22:04   mturner
//Initial revision.
//
//   Rev 1.8   Apr 11 2006 17:02:56   mtillett
//Manual merge for stream0036
//
//   Rev 1.7.1.0   Apr 07 2006 12:00:28   asinclair
//Add open journey planner service
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.8   Apr 06 2006 16:04:34   COwczarek
//Add open journey planner service
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.7   Jan 26 2006 14:56:32   mdambrine
//adding synchronous journeyplanner
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jan 23 2006 10:20:42   mdambrine
//Addition of operation and service type for journeyplanner
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 19 2006 12:17:12   RWilby
//Added FindNearest service
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.4   Jan 16 2006 13:46:56   schand
//Added taxi onfo service
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 13 2006 15:28:02   schand
//Updated service type for codeservice
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 12 2006 19:58:04   schand
//Updated correct departure board service name
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 10 2006 16:57:20   schand
//Added some service types
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 10 2006 15:28:52   schand
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.Common
{
	/// <summary>
	/// Enumerator for EnhancedExposedServiceTypes.
	/// If you add any entry, do create a sql script to add this in Reporting.EnhancedExposedServicesType table
	/// </summary>
	public enum EnhancedExposedServiceTypes
	{
		TransportDirect_EnhancedExposedServices_TestWebService,
		TransportDirect_EnhancedExposedServices_DepartureBoard_V1,
		TransportDirect_EnhancedExposedServices_TravelNews_V1,
		TransportDirect_EnhancedExposedServices_CodeHandler_V1,
		TransportDirect_EnhancedExposedServices_TaxiInformation_V1,
		TransportDirect_EnhancedExposedServices_FindNearest_V1,
		TransportDirect_EnhancedExposedServices_JourneyPlanner_V1,
		TransportDirect_EnhancedExposedServices_JourneyPlannerSynchronous_V1,
        TransportDirect_EnhancedExposedServices_OpenJourneyPlanner_V1,
        TransportDirect_EnhancedExposedServices_CarJourneyPlannerSynchronous_V1,
        TransportDirect_EnhancedExposedServices_CycleJourneyPlannerSynchronous_V1,
        TransportDirect_EnhancedExposedServices_GradientProfile_V1
}
}
