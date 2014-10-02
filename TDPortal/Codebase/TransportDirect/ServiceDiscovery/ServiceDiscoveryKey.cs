#region History
// *********************************************** 
// NAME                 :  ServiceDiscoveryKey.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 10/07/2003 
// DESCRIPTION  : Definitions of the different ServiceDiscovery Keys.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ServiceDiscovery/ServiceDiscoveryKey.cs-arc  $ 
//
//   Rev 1.19   Mar 21 2013 10:13:00   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.18   Aug 28 2012 10:20:00   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.17   Aug 24 2012 16:04:58   rbroddle
//Added CalorieCalculator service key
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.16   Nov 18 2010 09:37:14   apatel
//Updated to implement cached route restriction information provider
//Resolution for 5639: Fares page breaks with connection time out errors
//
//   Rev 1.15   Jul 01 2010 12:47:26   apatel
//Updated for duplicate tiploc provider
//
//   Rev 1.14   Jun 14 2010 15:54:48   apatel
//added key for DropDownLocationProvider
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.13   Feb 11 2010 14:33:06   RBroddle
//Added key InternationalPlaceGazetteer
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.12   Feb 09 2010 09:57:50   mmodi
//Added InternationalPlannerData service key
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 09 2010 09:45:16   apatel
//Updated for TD International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Jan 29 2010 12:08:06   mmodi
//Added InternationalPlanner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Oct 06 2009 14:08:36   mmodi
//Added EnvironmentalBenefits
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.8   Sep 28 2009 10:09:06   PScott
//CCN530 Social BookMarking
//
//   Rev 1.7   Jun 03 2009 11:21:30   mmodi
//Added CoordinateConvertorFactory key (and reordered keys into alphabetical order)
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.6   Oct 14 2008 15:17:44   mmodi
//Manual merge for stream5014
//
//   Rev 1.5   Jun 27 2008 09:40:56   apatel
//CCN - 458 Accessibility Updates - Improved linking
//
//   Rev 1.4.1.2   Oct 10 2008 15:55:52   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.1   Jul 18 2008 13:48:50   mmodi
//Added gradient profiler
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.0   Jun 20 2008 14:52:06   mmodi
//Added cycle journey factories
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Apr 09 2008 18:18:52   mmodi
//Added LatestNewsFactory
//Resolution for 4808: Del 10 - Still need to write Latest News updater...
//
//   Rev 1.3   Mar 10 2008 15:26:54   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.1   Nov 29 2007 11:32:48   build
//Updated for Del 9.8
//
//   Rev 1.48   Oct 25 2007 15:18:50   mmodi
//Added LocationInformation key for location information service
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.47   May 25 2007 16:22:16   build
//Automatically merged from branch for stream4401
//
//   Rev 1.46.1.0   May 09 2007 14:44:28   mmodi
//Added CoachFaresLookup
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.46   Mar 06 2007 12:28:22   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.45.1.0   Feb 27 2007 10:24:50   mmodi
//Added JourneyEmissionsFactor key
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.45   Oct 06 2006 13:19:36   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.44.1.0   Aug 04 2006 13:48:04   esevern
//added car park catalogue
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.44   Feb 23 2006 13:58:42   RWilby
//Merged stream3129
//
//   Rev 1.43   Feb 16 2006 15:54:24   build
//Automatically merged from branch for stream0002
//
//   Rev 1.42.1.0   Dec 13 2005 14:09:58   kjosling
//Added ServiceDiscoveryKey for Zonal Services
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.42   Dec 13 2005 11:29:32   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.41   Nov 28 2005 16:03:26   mguney
//ExceptionalFares key included.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.40   Nov 24 2005 10:04:52   schand
//Meged keys req for Partner which was required for EnahncedExposedServices
//
//   Rev 1.39   Nov 09 2005 16:56:02   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.38   Oct 31 2005 14:27:30   tmollart
//Merge with stream 2638.
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.37   Sep 29 2005 10:37:42   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.36   Sep 26 2005 17:26:56   rhopkins
//Merge stream 2596 back into trunk
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.33.1.2   Sep 15 2005 17:03:06   kjosling
//Merged with version 1.35
//
//   Rev 1.33.1.1   Aug 18 2005 14:34:20   kjosling
//Added SuggestionLinkService key
//
//   Rev 1.33.1.0   Aug 03 2005 11:06:58   NMoorhouse
//Branched for stream2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.33   Jul 05 2005 13:57:30   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.32   Jul 05 2005 11:13:24   NMoorhouse
//Code merge (stream2560 -> trunk)
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.31.3.1   Jul 04 2005 18:27:42   NMoorhouse
//New Properties to support Kizoom proxy
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.31.3.0   Jun 23 2005 13:14:00   schand
//Added key for MobileDeviceType dropdown
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.31   Mar 11 2005 09:35:00   jmorrissey
//Added CostSearchRunner key
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.30   Mar 01 2005 14:13:00   jgeorge
//Added DiscountCardCatalogue
//
//   Rev 1.29   Feb 28 2005 17:24:10   passuied
//new key For DepartureBoardService component
//
//   Rev 1.28   Feb 28 2005 14:58:24   jmorrissey
//Updated CostSearchFacade to use Factory class
//
//   Rev 1.27   Feb 22 2005 16:41:20   jmorrissey
//Added CostSearchFacade key
//
//   Rev 1.26   Jan 17 2005 11:01:22   schand
//Added RTTILookupHandler for RTTIManager
//
//   Rev 1.25   Jan 04 2005 10:25:50   passuied
//added entries needed by CodeWebService and DepartureBoardService
//
//   Rev 1.24   Dec 17 2004 20:27:20   rhopkins
//Add key for CarCostCalculator
//
//   Rev 1.23   Nov 02 2004 17:58:04   Schand
//// Added ServiceDiscovery Key for SeasonalNoticeBoardData
//
//   Rev 1.22   Nov 01 2004 15:48:10   jgeorge
//Added key for PlaceDataProvider
//
//   Rev 1.21   Jun 15 2004 13:18:30   rgreenwood
//Added key for DataChangeNotification
//
//   Rev 1.20   May 12 2004 16:01:36   jgeorge
//Added new key for AirDataProvider
//
//   Rev 1.19   Apr 30 2004 13:49:40   jbroome
//DEL 5.4 Merge
//Added new key for ScriptRepository
//
//   Rev 1.18   Nov 28 2003 15:41:00   COwczarek
//Add new key for RetailXMLSchema service
//Resolution for 451: Retail Handoff does not need to read XML schema for each request
//
//   Rev 1.17   Nov 06 2003 16:26:26   PNorell
//Added Caching keys.
//
//   Rev 1.16   Oct 30 2003 14:53:56   PNorell
//Updated with new service for crypto.
//
//   Rev 1.15   Oct 21 2003 11:56:40   acaunt
//PriceSupplierFactory added
//
//   Rev 1.14   Oct 16 2003 20:53:30   acaunt
//AdditionalDataModule key added
//
//   Rev 1.13   Oct 08 2003 11:04:30   JMorrissey
//Add new enum value for TravelNews
//
//   Rev 1.12   Oct 06 2003 11:11:32   COwczarek
//Add new enum value for Retailer Catalogue
//
//   Rev 1.11   Sep 24 2003 19:34:10   RPhilpott
//Add TDMapHandoff
//
//   Rev 1.10   Sep 20 2003 16:15:06   RPhilpott
//Add GisQuery
//
//   Rev 1.9   Sep 19 2003 15:01:58   RPhilpott
//Changed CjpStub to CJP 
//
//   Rev 1.8   Sep 16 2003 12:23:54   passuied
//Added a key + minor change
//
//   Rev 1.7   Sep 08 2003 14:38:20   RPhilpott
//Add CjpManager key.
//
//   Rev 1.6   Sep 01 2003 11:07:22   passuied
//added key
//
//   Rev 1.5   Aug 12 2003 14:34:52   kcheung
//Added Cjp stub key
//
//   Rev 1.4   Jul 17 2003 12:04:26   passuied
//key added
//
//   Rev 1.3   Jul 17 2003 11:17:08   passuied
//Changes after code review

#endregion

using System;

namespace TransportDirect.Common.ServiceDiscovery
{
	/// <summary>
	/// Definitions of the different ServiceDiscovery Keys.
	/// </summary>
	public class ServiceDiscoveryKey
	{
		public const string AdditionalData = "AdditionalData";
        public const string AirDataProvider = "AirDataProvider";
        public const string BayTextFilter = "BayTextFilter";
        public const string BookmarkWebService = "BookmarkWebService";
        public const string BusinessLinksTemplateCatalogue = "BusinessLinksTemplateCatalogue";
        public const string Cache = "Cache";
        public const string CarCostCalculator = "CarCostCalculator";
        public const string CarParkCatalogue = "CarParkCatalogueFactory";
        public const string Cjp = "CJP";
        public const string CjpManager = "CjpManager";
        public const string CoachFaresInterface = "CoachFaresInterface";
        public const string CoachFaresLookup = "CoachFaresLookup";
        public const string CoachOperatorLookup = "CoachOperatorLookup";
        public const string CoachRoutesQuotaFareProvider = "CoachRoutesQuotaFareProvider";
        public const string CodeGazetteer = "CodeGazetteer";
        public const string CoordinateConvertorFactory = "CoordinateConvertorFactory";
        public const string CostSearchFacade = "CostSearchFacadeFactory";
        public const string CostSearchRunner = "CostSearchRunnerFactory";
        public const string CostSearchRunnerCaller = "CostSearchRunnerCallerFactory";
        public const string Crypto = "Crypto";
        public const string CycleAttributes = "CycleAttributes";
        public const string CycleJourneyPlanRunnerCaller = "CycleJourneyPlanRunnerCallerFactory";
        public const string CyclePlannerFactory = "CyclePlannerFactory";
        public const string CyclePlannerManager = "CyclePlannerManager";
        public const string DataChangeNotification = "DataChangeNotification";
        public const string DataServices = "DataServices";
        public const string DepartureBoardService = "DepartureBoardService";
        public const string DiscountCardCatalogue = "DiscountCardCatalogue";
        public const string DropDownLocationProvider = "DropDownLocationProvider";
        public const string DuplicateTiplocProvider = "DuplicateTiplocProvider";
        public const string EnvironmentalBenefitsCalculator = "EnvironmentalBenefitsCalculator";
        public const string ExceptionalFaresLookup = "ExceptionalFaresLookup";
        public const string ExternalLinkService = "ExternalLinkService";
        public const string GazetteerFactory = "GazetteerFactory";
        public const string GisQuery = "GisQuery";
        public const string GradientProfilerManager = "GradientProfilerManager";
        public const string InternationalJourneyPlanRunnerCaller = "InternationalJourneyPlanRunnerCallerFactory";
        public const string InternationalPlannerFactory = "InternationalPlannerFactory";
        public const string InternationalPlannerDataFactory = "InternationalPlannerDataFactory";
        public const string InternationalPlannerManager = "InternationalPlannerManager";
        public const string JourneyEmissionsFactor = "JourneyEmissionsFactor";
        public const string JourneyFareFilterFactory = "JourneyFareFilterFactory";
        public const string JourneyNoteFilter = "JourneyNoteFilterFactory";
        public const string JourneyPlannerService = "JourneyPlannerService";
        public const string JourneyPlannerSynchronousService = "JourneyPlannerSynchronousService";
        public const string JourneyPlanRunnerCaller = "JourneyPlanRunnerCallerFactory";
        public const string JourneyPriceSupplierFactory = "JourneyPriceSupplierFactory";
        public const string LatestNewsFactory = "LatestNewsFactory";
        public const string LocalityTravelineLookup = "LocalityTravelineLookup";
        public const string LocationInformation = "LocationInformation";
        public const string LocationServiceCache = "LocationServiceCache";
        public const string Logging = "Logging";
        public const string MobileBookmark = "MobileBookmark";
        public const string NetworkMapLinksService = "NetworkMapLinksService";
        public const string OperatorsService = "OperatorsService";
        public const string PageController = "PageController";
        public const string ParkAndRideCatalogue = "ParkAndRideCatalogueFactory";
        public const string PartnerCatalogue = "PartnerCatalogueFactory";
        public const string PlaceDataProvider = "PlaceDataProvider";
        public const string InternationalPlaceGazetteer = "InternationalPlaceGazetteer";
        public const string PricedServiceSupplierFactory = "PricedServiceSupplierFactory";
        public const string PriceSupplierFactory = "PriceSupplierFactory";
        public const string PropertyService = "PropertyService";
        public const string RetailerCatalogue = "RetailerCatalogue";
        public const string RetailXmlSchema = "RetailXmlSchema";
        public const string RoutePriceSupplierFactory = "RoutePriceSupplierFactory";
        public const string RouteRestrictionsCatalogue = "RouteRestrictionsCatalogue";
        public const string RTTILookupHandler = "RTTILookupHandler";
        public const string RTTIManager = "RTTIManager";
        public const string ScriptRepository = "ScriptRepository";
        public const string SeasonalNoticeBoardImport = "SeasonalNoticeBoardHandlerFactory";
        public const string SessionManager = "SessionManager";
        public const string StopEventManager = "StopEventManager";
        public const string SocialBookMarkingService = "SocialBookMarkingService";
        public const string SuggestionLinkService = "SuggestionLinkService";
        public const string TDMapHandoff = "TDMapHandoff";
        public const string TimeBasedFareSupplier = "TimeBasedFareSupplier";
        public const string TimeBasedPriceSupplier = "TimeBasedPriceSupplier";
        public const string TravelNews = "TravelNewsHandlerFactory";
        public const string VisitPlanRunnerCaller = "VisitPlanRunnerCallerFactory";
        public const string ZonalAccessibility = "ZonalAccessibility";
        public const string ZonalServices = "ZonalServices";
        public const string CalorieCalculator = "CalorieCalculator";

	}
}
