#region Version history
// *********************************************** 
// NAME			: TDExceptionIdentifier.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 02.10.03
// DESCRIPTION	: Defines an enumeration to uniquely identify exceptions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDExceptionIdentifier.cs-arc  $
//
//   Rev 1.35   Mar 21 2013 10:12:54   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.34   Jan 22 2013 16:48:44   DLane
//Accessible events
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.33   Jan 14 2013 14:41:52   mmodi
//Added GISQueryEvent
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.32   Dec 05 2012 14:19:02   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.31   Oct 04 2012 14:23:24   mmodi
//Location service cache GIS call error identifier
//Resolution for 5857: Gaz - Code review updates
//
//   Rev 1.30   Aug 24 2012 15:42:00   rbroddle
//Added CYCalorieMETDataSQLCommandFailed exception for cycle calorie counter.
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.29   Aug 24 2012 15:22:46   mmodi
//Added Location suggest exceptions
//Resolution for 5832: CCN Gaz
//
//   Rev 1.28   Feb 07 2012 12:43:52   DLane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.27   Sep 29 2010 11:26:08   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.26   Jul 01 2010 12:46:58   apatel
//Updated for CJP Config data import/export utility
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code
//
//   Rev 1.25   Jun 21 2010 16:55:58   mmodi
//Updated Drop down gaz identifier
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.24   Jun 10 2010 12:19:18   mmodi
//Added Drop Down Gaz importer exceptions
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.23   Apr 23 2010 16:40:50   mmodi
//International Planner data error id
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.22   Mar 04 2010 16:39:30   mmodi
//International planner exception for general error planning journeys
//Resolution for 5436: TD Extra - Error when subtracting a timezpan from a Zero datatime
//
//   Rev 1.21   Feb 18 2010 15:51:46   mmodi
//Identifiers for publishing International Planner events
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.20   Feb 16 2010 17:42:36   mmodi
//Updated for International Planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.19   Feb 04 2010 10:25:04   mmodi
//Updated International Planner exceptions
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.18   Jan 29 2010 12:06:40   mmodi
//Added International Planner errors
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.17   Nov 13 2009 11:43:36   apatel
//RDPFailedPublishingMapAPIEvent enum added.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.16   Oct 11 2009 20:50:36   mmodi
//Updated EBC exception
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.15   Oct 06 2009 13:50:58   mmodi
//Updated for EBC
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.14   Sep 21 2009 14:48:14   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.13   Sep 08 2009 13:27:20   mmodi
//Exception for max number of journeys in request
//Resolution for 5318: Car exposed service - Multiple journey limit property
//
//   Rev 1.12   Aug 10 2009 16:25:40   mmodi
//Added id for coordinate location resolution
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.11   Aug 04 2009 12:34:12   mmodi
//Added exceptions for Car journey planner exposed service
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.10   Jun 03 2009 11:06:40   mmodi
//New exceptions for CoordinateConvertor
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.9   Mar 16 2009 13:11:44   build
//Manual merge of stream 5215
//
//   Rev 1.7.1.1   Jan 14 2009 17:48:20   mturner
//More tech refresh updates
//
//   Rev 1.7.1.0   Jan 13 2009 14:41:24   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.7   Oct 14 2008 13:22:14   mturner
//Manual merge for stream 5014
//
//   Rev 1.5.1.5   Sep 19 2008 16:31:48   mmodi
//Added exceptions for RTTI Ticket Type feed logging
//Resolution for 5118: RTTI XML Ticket Type feed - Logging updates
//
//   Rev 1.5.1.4   Aug 22 2008 10:05:54   mmodi
//Event logged exceptions
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5.1.3   Aug 08 2008 12:05:48   mmodi
//Updated as part of workstream
//
//   Rev 1.5.1.2   Aug 01 2008 16:39:14   mmodi
//Updated
//
//   Rev 1.5.1.1   Jul 18 2008 13:54:58   mmodi
//Updated cycle planner exceptions
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5.1.0   Jun 20 2008 15:01:42   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   May 14 2008 15:36:26   mmodi
//Added repeat visitor exception event
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.4   Apr 03 2008 15:22:52   mmodi
//Theme errors
//Resolution for 4631: Del 10 - Transaction injector is not working correctly
//
//   Rev 1.3   Mar 10 2008 15:15:18   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev Devfactory Jan 26 2008 08:01 apatel
//  CCN - 426 Added new TDExceptionIdentifier enum value 
//  DGInputXMLFileHasInvalidCharacters to use with Car Parking Data Importer 
//  character encoding validation.
//
//   Rev 1.1   Nov 29 2007 10:27:52   mturner
//Updated for Del 9.8
//
//   Rev 1.118   Oct 25 2007 15:16:56   mmodi
//Added LocationInformationService import exceptions
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.117   Aug 31 2007 14:29:00   rbroddle
//CCN393, (Coach Taxi Info) work, merged in from stream4468
//Resolution for 4468: Coach Stop Taxi Enhancements
//
//   Rev 1.116   Jan 12 2007 14:10:12   mmodi
//Added exceptions for Feedback page
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.115   Oct 06 2006 10:39:52   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.114.2.1   Aug 14 2006 11:58:38   esevern
//Merge of change on 1.114.1 - Added Car Park exception for invalid xml file
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.114.2.0   Aug 14 2006 10:51:34   esevern
//added identifier for no car parks found in max radius
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.114   Mar 23 2006 17:54:20   build
//Automatically merged from branch for stream0025
//
//   Rev 1.113.1.1   Mar 20 2006 13:45:52   tolomolaiye
//Updated with code review comments
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.113.1.0   Mar 15 2006 14:02:36   tolomolaiye
//Added identifiers fro car park
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.113   Feb 24 2006 10:33:28   RWilby
//merged stream3129
//
//   Rev 1.112   Feb 16 2006 15:54:32   build
//Automatically merged from branch for stream0002
//
//   Rev 1.111.1.0   Jan 19 2006 17:41:20   tolomolaiye
//Added exception for zonal services
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.111   Dec 05 2005 15:19:26   rgreenwood
//TD109: Added Invalid Culture Channel exception
//
//   Rev 1.110   Dec 02 2005 13:40:54   rgreenwood
//TD109: Added filenotfound exception identifier for ToolBarDownload
//
//   Rev 1.109   Nov 09 2005 12:23:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.108.1.2   Oct 25 2005 10:48:32   schand
//Corrected the integer code for Enum 
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.108.1.1   Oct 11 2005 11:06:20   schand
//Added more ex ids for Search by Price
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.108.1.0   Oct 10 2005 14:44:44   schand
//Added ExceptionIds for Search by Price
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.108   Sep 27 2005 11:35:28   asinclair
//Merge for 2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.107.1.1   Aug 24 2005 16:03:46   kjosling
//Added identifiers for SuggestionLink creation process
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.107.1.0   Aug 15 2005 11:43:58   Schand
//Added ExceptionIdentifier code for Park and Ride Import
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.107   Jul 15 2005 13:37:58   NMoorhouse
//Exception Identifiers for Mobile Bookmark
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.106   Jun 23 2005 09:45:10   NMoorhouse
//IR2537 - New exception code added
//Resolution for 2537: Mobile - No codes found for 'London' rail locations - location too vague
//
//   Rev 1.105   Apr 15 2005 13:40:46   jbroome
//Added new exception for Product Profile import csv conversion
//
//   Rev 1.104   Apr 09 2005 15:07:34   schand
//Added identifier for RTTI transaction injector.
//
//   Rev 1.103   Mar 13 2005 16:41:18   jmorrissey
//Added new CostSearchFacade identifiers
//
//   Rev 1.102   Mar 08 2005 16:22:20   schand
//Added an Identifier for TDMobilePages event request
//
//   Rev 1.101   Mar 01 2005 17:25:06   jmorrissey
//Added CostSearch identifiers
//
//   Rev 1.100   Feb 17 2005 14:32:54   jbroome
//Added AvailabilityDataMaintenance exceptions
//Resolution for 1923: DEV Code Review : Availability Estimator
//Resolution for 1924: DEV Code Review : Availability Data Maintenance
//
//   Rev 1.99   Feb 08 2005 10:15:42   jbroome
//Added AvailabilityEstimator exceptions
//
//   Rev 1.98   Feb 07 2005 14:36:26   passuied
//added id for ExposedServices Event
//
//   Rev 1.97   Feb 02 2005 18:07:16   passuied
//added exception id
//
//   Rev 1.96   Jan 31 2005 14:28:24   passuied
//added exception for Dep board service	
//
//   Rev 1.95   Jan 28 2005 11:22:12   asinclair
//Last Check in was not correct
//
//   Rev 1.94   Jan 27 2005 11:55:52   asinclair
//Fixed error causing build faults
//
//   Rev 1.93   Jan 26 2005 18:05:56   passuied
//real change ( forget previous check in)
//
//   Rev 1.91   Jan 25 2005 18:42:08   schand
//Renamed RTTIEventCounters to RTTIEventFailed
//
//   Rev 1.90   Jan 24 2005 14:39:04   schand
//Added RTTICounterEvent identifier for RTTI event request
//
//   Rev 1.89   Jan 24 2005 14:36:42   jgeorge
//New exception for InternalRequestEvent invalid FunctionType.

#endregion

using System;

namespace TransportDirect.Common
{

    /// <summary>
    /// Enum of available exception types
    /// </summary>
    [Serializable]
    public enum TDExceptionIdentifier
    {
        Undefined = -1,

        // The enumeration values are defined in TDPortal\DEL5\Exceptions.XLS
        // Ranges of id's correlate to the id of the associated component's design document. Eg Event Logging Service Design document is 1, and the id range is 100 - 199.

        // Event Logging Service - 100 - 199
        ELSEventLogConstructor = 100,
        ELSEventLogPublisherWritingEvent = 101,
        ELSFilePublisherWritingEvent = 102,
        ELSConsolePublisherWritingEvent = 109,
        ELSEmailPublisherWritingEvent = 105,
        ELSCustomEmailPublisherSavingStream = 128,
        ELSCustomEmailPublisherWritingEvent = 127,
        ELSCustomEmailPublisherUnsupportedEvent = 126,
        ELSCustomEmailPublisherConstructor = 130,
        ELSQueuePublisherConstructor = 106,
        ELSQueuePublisherWritingEvent = 107,
        ELSGlobalTraceLevelUndefined = 111,
        ELSSwitchOnUnknownEvent = 113,
        ELSLoggingPropertyValidatorUnknownKey = 114,
        ELSDefaultPublisherFailed = 104,
        ELSPropertyValidatorStringToEnum = 110,
        ELSCustomEventSwitchUndefined = 112,
        ELSCustomOperationalConstructor = 115,
        ELSTDTraceListenerConstructor = 118,
        ELSTraceLevelUninitialised = 121,
        ELSTDTraceListenerUnknownObject = 103,
		ELSInvalidFunctionType = 131,

        // *** Report Data Provider Components 2600 - 3000 ****

        // Custom Database publishers - continues ad 2800
        RDPSQLHelperInsertFailed = 2600,
        RDPUnsupportedDatabasePublisherEvent = 2601,
        RDPSQLHelperStoredProcedureFailure = 2602,
        RDPTraceListenerException = 2603,
        RDPFailedPublishingJourneyWebRequestEvent = 2604,
        RDPFailedPublishingLocationRequestEvent = 2605,
        RDPFailedPublishingOperationalEvent = 2606,
        RDPFailedPublishingWorkloadEvent = 2607,
        RDPFailedPublishingRetailerHandoffEvent = 2608,
        RDPFailedPublishingReferenceTransactionEvent = 2609,
        RDPFailedPublishingPageEntryEvent = 2610,
        RDPFailedPublishingMapEvent = 2611,
        RDPFailedPublishingLoginEvent = 2612,
        RDPFailedPublishingJourneyPlanRequestVerboseEvent = 2613, 
        RDPFailedPublishingJourneyPlanResultsVerboseEvent = 2614,
        RDPFailedPublishingJourneyPlanRequestEvent = 2615,
        RDPFailedPublishingJourneyPlanResultsEvent = 2616,
        RDPFailedPublishingGazetteerEvent = 2617,
        RDPFailedPublishingDataGatewayEvent = 2618,
        RDPFailedPublishingUserPreferenceSaveEvent = 2619,
        RDPTDPCustomEventPublisherConstruction = 2620,
        RDPCJPCustomEventPublisherConstruction = 2621,
        RDPOperationalEventPublisherConstruction = 2622,
		RDPFailedPublishingInternalRequestEvent = 2623,
		RDPFailedPublishingUserFeedbackEvent = 2624,
        RDPFailedPublishingRepeatVisitorEvent = 2625,
        RDPFailedPublishingCyclePlannerRequestEvent = 2626,
        RDPFailedPublishingCyclePlannerResultEvent = 2627,
        RDPFailedPublishingGradientProfileEvent = 2628,
        RDPFailedPublishingEBCCalculationEvent = 2629, // Continues at 2800

        // Web Log Reader
        RDPWebLogReaderDefaultLoggerFailed = 2630,
        RDPWebLogReaderTDServiceAddFailed = 2631,
        RDPWebLogReaderTDTraceInitFailed = 2632,
        RDPWebLogReaderUnknownPropertyKey = 2633,
        RDPWebLogReaderInvalidProperties = 2634,
        RDPWebLogReaderArchiveFailed = 2635,
        RDPWebLogReaderMissingFields = 2636,
        RDPWebLogReaderNoFieldTokens = 2637,
        RDPWebLogReaderInvalidArg = 2638,
        RDPWebLogReaderFailedReadingWebLog = 2639,
        RDPWebLogReaderFailedStoringWebLogData = 2640,

        // Report Data Staging Archiver
        RDPStagingArchiverDefaultLoggerFailed = 2641,
        RDPStagingArchiverTDServiceAddFailed = 2642,
        RDPStagingArchiverTDTraceInitFailed = 2643,
        RDPStagingArchiverUnknownPropertyKey = 2644,
        RDPStagingArchiverInvalidProperties = 2645,
        RDPStagingArchiverInvalidArg = 2646,
        RDPStagingArchiverLatestImportedMismatch = 2647,

        // Report Data Importer
        RDPDataImporterDefaultLoggerFailed = 2651,
        RDPDataImporterTDServiceAddFailed = 2652,
        RDPDataImporterTDTraceInitFailed = 2653,
        RDPDataImporterUnknownPropertyKey = 2654,
        RDPDataImporterInvalidProperties = 2655,
        RDPDataImporterRequestAuditUpdateFailed = 2656,
        RDPDataImporterLinkInitFailed = 2657,
        RDPDataImporterFailureDuringImport = 2658,
        RDPDataImporterLinkRemoveFailed = 2659,
        RDPDataImporterInvalidArg = 2660,
        RDPDataImporterStoredProcedureFailed = 2661,
        RDPDataImporterFailedToGetLatestImportedDate = 2662,
        RDPDataImporterConnectionFailure = 2663,
        RDPDataImporterImportDisabled = 2664,
        RDPDataImporterArchiveDisabled = 2665,
        RDPDataImporterFailureDuringArchive = 2666,
        RDPDataImporterInvalidImporterGroup = 2667,

        // Event Receiver
        RDPEventReceiverQueueReceiveInitFailed = 2670,
        RDPEventReceiverStartFailure = 2671,
        RDPEventReceiverInitFailed = 2672,
        RDPEventReceiverUnknownPropertyKey = 2673,

		

        // Transaction Web Service
        RDPTransactionServiceConversionToClassError = 2680,
        RDPTransactionServiceConversionToBinaryError = 2681,
        RDPTransactionServiceUnknownPropertyKey = 2682,
        RDPTransactionServiceDefaultLoggerFailed = 2683,
        RDPTransactionServiceTDTraceInitFailed = 2684,
        RDPTransactionServiceInvalidProperties = 2685,
        RDPTransactionServiceCJPConfigFailed = 2686,
        RDPTransactionServiceTDServiceAddFailed = 2687,

        // Transaction Injector
        RDPTransactionInjectorPropertiesServiceFailed = 2690,
        RDPTransactionInjectorTDTraceInitFailed = 2691,
        RDPTransactionInjectorInvalidProperties = 2692,
        RDPTransactionInjectorUnknownPropertyKey = 2693,
        RDPTransactionInjectorFailedCreatingJourneyRequest = 2694,
        RDPTransactionInjectorFailedCreatingSoftContent = 2695, // No longer used
        RDPTransactionInjectorUnknownTransactionClass = 2696,
        RDPTransactionInjectorFailedCreatingPricing = 2697, // No longer used
        RDPTransactionInjectorFailedCreatingStationInfo = 2698, // No longer used
        RDPTransactionInjectorFailedCreatingTravelineChecker = 2701,
		RDPTravelineCheckerError = 2702,
		RDPTransactionInjectorFailedCreatingEESInfo = 2703,
        RDPTransactionInjectorFailedCreatingTravelNews = 2704,

		// Event Data Loader
		EDLFailedConvertingTDTraceLevel = 2699,
		EDLFailedConvertingTDEventCategory = 2700,

        // Custom Database publishers - continued from 2629
        RDPFailedPublishingInternationalPlannerEvent = 2800,
        RDPFailedPublishingInternationalPlannerRequestEvent = 2801,
        RDPFailedPublishingInternationalPlannerResultEvent = 2802,
        RDPFailedPublishingGISQueryEvent = 2803,
        RDPFailedPublishingAccessibleEvent = 2804,


        // Session Manager
        SMUnsetParameters = 200,
        SMInvalidTDJourneyParametersType = 201,

		SMUserTypeUtilityDefaultLoggerFailed = 202,
		SMUserTypeUtilityTDServiceAddFailed = 203,
		SMUserTypeUtilityTDTraceInitFailed = 204,
		SMUserTypeUtilityInvalidArg = 205,
		SMUserTypeUtilityUserNotFound = 206,

        // Service Discovery
        SDInvalidKey = 500,
        SDBaseDateRefInvalid = 501,

        // Property Service
        PSInvalidPropertyFile = 600,
        PSInvalidVersion = 601,
        PSDatabaseFailure = 602,
        PSInvalidAssembly = 604,
        PSInvalidAssemblyType = 605,
        PSBadEnum = 606,
        PSLockedPropertyNotEncrypted = 607,
        PSUnencryptedPropertyExists = 608,
        PSMissingProperty = 609,


        // Screen Flow Mechanism
        SFMInvalidXml = 800,
        SFMConstructorFailed = 801,
        SFMPropertiesInvalid = 802,
        SFMScreenFlowTableError = 803,
        SFMInvalidScreenFlowTableIndex = 805,
        SFMScreenFlowNodeError = 806,
        SFMInvalidScreenFlowTableIndex2 = 807,
        SFMInvalidNonEmptyArray = 808,
        SFMArrayEntryValidationFailed = 809,
        SFMPreviousValidationFailed = 810,
        SFMScreenFlowTableError2 = 811, 
        SFMArrayEntryValidationFailed2 = 813,
        SFMInvalidGetNextID = 820,
        SFMInvalidPageTransition = 821,

        // Base templates and controls
        BTCSendPasswordFailed = 901,
        BTCRetrievalOfProfileFailed = 902,
        BTCCreationOfProfileFailed = 903,
        BTCRegisterUserFailed = 904,
        BTCSendJourneyDetailsFailed = 905,
        BTCInvalidNumberOfMonths = 906,
        BTCBadMonthYearFormat = 907,
		BTCBadStationTypeEnumParsing = 908,
		WEBCultureTableNotInitialised = 910,
        WEBScriptRepositoryInvalidScriptName = 911,

        // Location Service
        LSGazopURLUnavailable = 1100,
        LSFinalDrillDownInvalid = 1101,
        LSPickListExists = 1102,
        LSDrillDownInvalid = 1103,
        LSPostCodeMatchInvalid = 1104,
        LSPlaceNameMatchInvalid = 1105,
        LSFetchRecordInvalid = 1106,
        LSFetchRecordNoMatch = 1107,
        LSAddressDrillLacksChildren = 1108,
        LSAllStationsMaxReturnedInvalid = 1109,
        LSAllStationsDrillLacksChildren = 1110,
        LSAddressPostCodeGazetteerIDInvalid = 1111,
        LSAllStationGazetteerIDInvalid = 1112,
        LSAttractionGazetteerIDInvalid = 1113,
        LSLocalityGazetteerIDInvalid = 1114,
        LSMajorStationGazetteerIDInvalid = 1115,
        LSAttractionMaxReturnPropertyInvalid = 1116,
        LSAttractionLacksChildren = 1117,
        LSLocalityLacksChildren = 1118,
        LSLocalityHasChildren = 1119,
        LSMajorStationMaxReturnPropertyInvalid = 1120,
        LSMajorStationLacksChildren = 1121,
        LSGizQueryPropertyInvalid = 1122,
        LSWrongType = 1123,
        LSAddressPostCodeGazetteerHasChildren = 1124,
		LSNoStationFoundInMaxRadius = 1125,
		LSNoAllStationTypesFound = 1126,
		LSCodeGazetteerIDInvalid = 1127,
		LSCodeGazetteerMaxReturnPropertyInvalid = 1128,
		LSCodeGazetteerUndefinedCodeType = 1129,
		LSPlaceNameMatchTooVague = 1130,
		LSInvalidFullUKPostcode = 1131,
		LSNoCarParksFoundInMaxRadius = 1132,
        LSNaptanCacheLookupError = 1133,
        LSNaptanCacheLookupNaptanInvalid = 1134,
        LSGISQueryCallError = 1135,

        // Data Services
        DSNULLParameters = 2301,
        DSSQLHelperDatabaseNotFound = 2302,
        DSDataAccessError = 2303,
        DSQueryExecuteError = 2304,
        DSUnknownType = 2305,
        DSMethodIllegalType = 2306,
		DSStoredProcedureNotPresent = 2307,
		DSPollingIntervalNotFound = 2308,
		DSGroupsNotFound = 2309,
		DSDatabaseNamePropertyNotValid = 2310,
		DSDatabaseNamePropertyNotPresent = 2311,

        // Pricing & Retail Handoff
        PRHRetailerCatalogueSQLCommandFailed = 3400,
        PRHInvalidPricingRequest = 3401,
        PRHInvalidInputHeader = 3402,
        PRHPoolSizeLessThanMinimum = 3403,
        PRHInvalidProperties = 3404,
        PRHEngineAddFailed = 3405,
        PRHMaximumPoolSizeExceeded = 3406,
        PRHBOReleaseFailed = 3407,
        PRHBOGetIntanceFailed = 3408,
        PRHBOPoolCreationFailed = 3409,
        PRHBusinessObjectEngineDllCallFailed = 3410,
        PRHInvalidInputWhenParsingIniFile = 3411,
        PRHValueNotFoundInIniFile = 3412,
        PRHKeyNotFoundInIniFile = 3413,
        PRHFailedToOpenIni = 3414,
        PRHSignalReleaseFailed = 3415,
        PRHBusinessObjectEngineTimedOut = 3416,
        PRHInvalidImportParameters = 3417,
        PRHImportIOException = 3418,
        PRHUnrecognisedImportFeed = 3419,
        PRHServerUnavailable = 3420,
        PRHUnableToBeginHousekeeping = 3421,
        PRHUnexpectedImportException = 3422,
        PRHAttemptToCreateInvalidPricingUnit = 3423,
        PRHUnabletoPricePricingUnit = 3424,
        PRHErrorReadingRetailXmlSchema = 3425,
        PRHErrorValidatingHandoffXml = 3426,
        PRHErrorValidatingRetailXmlSchema = 3427,
        PRHOutputLengthMustBeNonNegative = 3428,
        PRHUnexpectedError = 3429,

        //TravelNews
        TNImportUnexpectedError = 3500,
        TNImportFileNotFound = 3501,
        TNImportInvalidXML = 3502,
        TNImportDBConnectionError = 3503,
        TNImportStoredProcedureError = 3504,
        TNSQLHelperStoredProcedureFailure = 3505,
        TNSQLHelperError = 3506,

        // DataGateway
        DGSchemaValidationFailed = 3700,
        DGSchemaValidationAttributeNotFound = 3701,
        DGCryptoInitFailed = 3702,
        DGPropertyInitFailed = 3703,
        DGTraceListenerInitFailed = 3704,

        DGInvalidImportArguments = 4000,
        DGInvalidProperties = 4001,
        DGPreparingFTPError = 4002,
        DGPerformingFTPError = 4003,
        DGZipReaderError = 4004,
        DGInvokingImportTaskFailed = 4005,
        DGInvalidConfiguration = 4006,
        DGCommandLineImportError = 4007,		
        DGUnexpectedException = 4008,
        DGUpdateExportStatusFailed = 4009,

        DGFileCopyInvalidParameters = 4010,
        DGFileCopyFileNotFound = 4011,
        DGFileCopyDirectoryNotFound = 4012,
        DGFileCopyIOException = 4013,
        DGFileCopyUnexpectedException = 4014,
        DGImportStoredProcedureError = 4016,
        DGInputXMLFileNotFound = 4017,
        DGDatasourceImportTaskUnexpectedError = 4018,
        DGImportDBConnectionError = 4019,
        DGInvalidXMLToInput = 4020,
        DGUnexpectedFeedName = 4021,
        DGXMLNamespaceUnspecified = 4022,
        DGXMLSchemaFileUnspecified = 4023,
        DGDatabaseUnspecified = 4024,
        DGStoredProcedureUnspecified = 4025,
		DGCSVtoXMLConversionFailed = 4026,
        DGInputXMLFileHasInvalidCharacters = 4027, // CCN 426 Added for Character encoding check

        ADFAirOperatorImportFileNotFound = 5100,
        ADFRouteMatrixImportFileNotFound = 5101,
        ADFAirRouteMatrixImportFailedManualTTBORollbackRequired = 5102,
        ADFRTELProcessFailed = 5103,
        ADFTTBOAirBuilderFailed = 5104,
        ADFTTBOAirBuilderInvalidParameters = 5105,
        ADFTTBOAirBuilderLogReadFailed = 5106,
        ADFTTBOAirBuilderReportedNotReady = 5107,
        ADFTTBOAirBuilderUnexpectedError = 5108,
        ADFTTBOImportTaskFailed = 5109,
        ADFCsvConversionFailed = 5110,
        ADFRtelCifOutputNotFound = 5111,
        ADFRtelXmlOutputNotFound = 5112,
        ADFDataChangeNotificaitonError = 5113,
        ADFTTBOInputFileNotFound = 5114,
        ADFAirOperatorUnexpectedError = 5115,

		//UserSurvey
		USEDefaultLoggerFailed = 6000,
		USECryptoInitFailed = 6001,
		USEPropertyServiceInitFailed = 6002,	
		USETraceListenerInitFailed = 6003,
		USESqlHelperError = 6004,
		USEDeleteSentSurveysSPFailure = 6005,
		USEGetUnsentSurveysSPFailure = 6006,
		USEEmailingFailure = 6007,
		USEMissingProperty = 6008,
		USECreateAttachmentFailure = 6009,
		USEAddUserSurveySPFailure = 6010,
		USEFlagSentSurveysSPFailure = 6011,

		// SeasonalNoticeBoard
		SNBCsvConversionFailed = 7000,
		SNBSchemaNotMatched = 7001,
		SNBImportFailed = 7002,
		SNBDataFeedNameNotFound = 7003,

		// UpdateCmsEntries
		UCEUpdateCmsEntriesTDServiceAddFailed = 8000,
		UCEFailedGettingCmsEntriesDB = 8001,
		UCEFailedUpdatingCmsPosting = 8002,
		UCEFailedProcessingCmsEntries = 8003,

		// RTTI Manager
		RTTINoXmlRequestFound = 9000,
		RTTISocketCommunicationError=9001,
		RTTIUnabletoReadSocketData=9002,
		RTTIEventFailed = 9003,

		// Departureboard service
		DBSCJPTimeout = 9500,
		DBSEmptyLocationArray = 9501,
		DBSAddStopEventRequestEventFailed = 9502,
        
		// ExposedServices
		EXSAddExpposedServicesEventFailed = 9600,

		// AvailabilityEstimator
		AEInvalidUpdateAvailabilityEstimateCall = 9700,
		AEDefaultLoggerFailed = 9701,
		AETDServiceAddFailed = 9702,
		AETDTraceInitFailed = 9703,
		AEEmailingFailure = 9704, 
		AEInvalidArguments = 9705,
		AENoArguments = 9706,
		AEStoredProcedureError = 9707, 
		AEFileCreationError = 9708, 
		AECsvConversionFailed = 9709,

		//MobileBookmark
		MobileBookmarkSendError = 9800,
		MobileBookmarkInValidUserName = 9801,
		MobileBookmarkInValidRequest = 9802,

		//CostSearchRunner
		CSRCallAssembleFaresFailed = 10001,
		CSRCallAssembleServicesFailed = 10002,
		CSRSessionSerialiserFailed = 10003, 

		//CostSearchFacade
		CSFAssembleFaresCoachFailed = 10004,
		CSFAssembleFaresRailFailed = 10005,
		CSFAssembleFaresAirFailed = 10006,

		// TDMobile
		TDMobilePageEventLogging = 11001,

		// Park And Ride Data Import
		PRDCsvConversionFailed = 12000,
		PRDSchemaNotMatched = 12001,
		PRDImportFailed = 12002,
		PRDDataFeedNameNotFound = 12003,

		//Suggestion Link Service
		SLSUnhandledSubstitutionParameter = 13000,
		SLSLinkTextFormatError = 13001,
		SLSInvalidSubstitutionParameter = 13002,
		SLSCultureUnsupported = 13003,
		SLSInvalidContextError = 13004,

		// Search by Price 
		SBPCsvConversionFailed = 14000,
		SBPSchemaNotMatched = 14001,
		SBPImportFailed = 14002,
		SBPDataFeedNameNotFound = 14003,
		SBPAdditionalCsvConversionFailed = 14004,
		SBPAdditionalCsvNotExist = 14005,
		SBPImportFailedForXmlToDB = 14006,
		SBPPrimaryXmlValidationFails = 14007,
		SBPAdditionalXmlValidationFails = 14008,
		SBPPrimaryFileNotFound = 14009,

		//Enhanced Exposed Services
		EESAddTDExpposedServicesEventFailed = 15001,
		EESGeneralErrorCode = 15002,
		EESWSDLRequestValidationFailed = 15003,
		EESWSDLResponseValidationFailed = 15003,
		EESTravelNewsServiceInValidRegion = 15004,
		EESTravelNewsServiceInValidRequest = 15005,		
		EESTaxiInformationInValidRequest = 15006,
		EESCodeServiceInValidRequest = 15007,
		ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode = 15008,
		EESAccessDenied = 15009,
        ESSCarJourneyPlannerUniqueOSGridReferenceNotFoundForPostcode = 15010,
        ESSCarJourneyPlannerUniqueOSGridReferenceNotFoundForNaPTAN = 15011,
        EESCarJourneyPlannerOSGridReferenceForLocationNotSupported = 15012,
        EESCycleJourneyPlannerDistanceBetweenLocationsTooGreat = 15013,
        EESGradientProfileNoPolylineSepcified = 15014,

		//ToolBarDownload
		TBDFileNotFound = 16001,
		TBDInvalidCultureChannel = 16002,

		//Zonal Services
		ZSAddAtrributeFails = 17001,

		//Journey Planner Exposed Service
		JPResolvePostcodeFailed = 18001,
		JPOutwardDateTimeInPast = 18002,
		JPReturnDateTimeNotSupplied = 18003,
		JPReturnDateTimeEarlierThanOutwardDateTime = 18004,
		JPMissingReturnOrigin = 18005,
		JPMissingReturnDestination = 18006,
		JPCJPErrorsOccured = 18007,
        JPMissingJourneyInRequest = 18008,
        JPJourneyParametersParseError = 18009,
        JPResolveNaPTANFailed = 18010,
        JPResolveCoordinateFailed = 18011,
        JPFailedToPlanJourney = 18012,
        JPFailedToPlanAllJourneys = 18013,
        JPWarningMessage = 18014,
        JPExceededNumberOfJourneysInRequest = 18015,

		// White labeling
		TDPartnerLookupError = 19001,
		TDPartnerIdInvalid = 19002,

		//Car Park data import
		CPCsvConversionFailed = 20000,
		CPSchemaNotMatched = 20001,
		CPImportFailed = 20002,
		CPDataFeedNameNotFound = 20003,
		CPInvalidXmlFile = 20004,

		// User Feedback
		UFESqlHelperError = 21001,
		UFECreateUserFeedbackFailure = 21002,
		UFEUpdateUserFeedbackFailure = 21003,
		UFEGetUserFeedbackIdFailure = 21004,
		UFEGetUserFeedbackRecordFailure = 21005,
		UFESendEmailFailure = 21006,

		// Location Information (Airport links) import
		LIMissingFileName = 22001,
		LIFileDoesNotExist = 22002,
		LIAddAttributeFailed = 22003,
		LICsvConversionFailed = 22004,
		LIImportFailed = 22005,

        // Theme Infrastructure
        TIInvalidThemeFile = 23001,
        TIInvalidVersion = 23002,

        //RailTicketType
        XMLRTTInitCryptoFailed = 24001,
        XMLRTTInitPropertiesFailed = 24002,
        XMLRTTInitTraceListenerFailed = 24003,
        XMLRTTPopulateTicketTypeTable = 24004,
        XMLRTTTicketParser = 24005,

        // Cycle Planner
        CYSoapException = 25001,
        CYWebException = 25002,
        CYXmlMissingXSLTFile = 25003,
        CYXmlGPXFileTransformationFailed = 25004,
        CYCalorieMETDataSQLCommandFailed = 25005,

        // Gradient Profiler
        GRInvalidGradientProfileResult = 26001,

        // Coordinate Convertor Service
        CCServiceTDTraceInitFailed = 27001,
        CCServiceTDServiceAddFailed = 27002,
        CCSoapException = 27003,
        CCWebException = 27004,
        
        // Environmental Benefits Calculator
        EBCErrorCalculatingJourneyDateTime = 28001,
        EBCErrorCalculatingHighValueMotorwayDistance = 28002,

        // Map API Web Service
        RDPFailedPublishingMapAPIEvent = 29001,

        // InternationalPlanner 
        IPNoRequestObject = 30001,
        IPErrorRetrievingInternationalStop = 30002,
        IPUnrecognisedInternationalStopModeType = 30003,
        IPUnrecognisedDaysOfOperation = 30004,
        IPErrorRetrievingInternationalJourneyAir = 30005,
        IPErrorRetrievingInternationalJourneyCoach = 30006,
        IPErrorRetrievingInternationalJourneyRail = 30007,
        IPErrorRetrievingInternationalJourneyCar = 30008,
        IPErrorRetrievingInterchangeTime = 30009,
        IPErrorRetrievingTransferTime = 300010,
        IPErrorRetrievingInternationalCity = 30011,
        IPNoNaptanCodesFound = 30012,
        IPNoInternationalStopsFound = 30013,
        IPErrorRetrievingPermittedInternationalJourneys = 30014,
        IPErrorRetrievingInternationalJourney = 30015,
        IPErrorRetrievingInternationalData = 30016,

        // Drop Down Gaz 
        DDGCsvConversionFailed = 31001,
        DDGSchemaNotMatched = 31002,
        DDGImportFailed = 31003,
        DDGDataFeedNameNotFound = 31004,
        DDGInvalidXmlFile = 31005,
        DDGErrorCreatingDataFile = 310006,

        // CJP Config Data Importer
        CCDInitialisationFailed = 32001,
        CCDTDTraceInitFailed = 32002,
        CCDImportFailed = 32003,
        CCDExportFailed = 32004,

        // Batch Journey Planning
        BJPRegisterFailed = 33001,
        BJPSendEmailFailed = 33002,
        BJPUploadBatchFailed = 33003,
        BJPServiceInitFailed = 33004,

        // Location JS Generator
        LJSGenInitialisationFailed = 34001,
        LJSGenMissingProperty = 34002,
        LJSGenCreateScriptFailed = 34003,
        LJSGenGetScriptLocationFailed = 34004,
        LJSGenAliasDataLoadFailed = 34005,
        LJSGenAliasLocatonAddFailed = 34006,
        LJSGenLocatonDataLoadFailed = 34007,

        // Accessible Locations 
        ALImportFailed = 35001,
        ALCsvFileNotFound = 35002,
        ALInvalidXmlAttribute = 35003,

        // Accessible Journeys
        AJErrorParsingCJPAssistanceServiceType = 36001,
        AJErrorParsingCJPAccessSummary = 36002,

        // Journey Note Filter
        JNFImportFailed = 37001,
        JNFCsvFileNotFound = 37002,
        JNFInvalidXmlAttribute = 37003,

        // Departure Board Web Service
        DBWSInitialisationFailed = 38001,
        DBWSLDBWebServiceUrlUnavailable = 38002,
        DBWSLDBWebServiceSoapException = 38003,
        DBWSLDBWebServiceFaultException = 38004,
        DBWSLDBRequestInvalid = 38005,
        DBWSLDBNullStationBoardResult = 38006,
        DBWSLDBStationBoardMessage = 38007,
        DBWSLDBNullServiceDetailResult = 38008
    }
}
