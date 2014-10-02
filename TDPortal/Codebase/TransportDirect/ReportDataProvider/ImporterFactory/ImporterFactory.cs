// ************************************************** 
// NAME                 : ImporterFactory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/11/2003 
// DESCRIPTION			: Factory class for creating
// a group of Importer class instances.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/ImporterFactory.cs-arc  $
//
//   Rev 1.9   Jan 22 2013 16:48:48   DLane
//Accessible events
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.8   Jan 14 2013 14:41:58   mmodi
//Added GISQueryEvent
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Sep 06 2011 11:20:36   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.6   Mar 04 2010 13:10:36   PScott
//Added TransferMapAPIEvents
//Resolution for 5431: Reporting - MapAPIEvents not transferring to Reporting DB
//
//   Rev 1.5   Feb 18 2010 15:51:06   mmodi
//Updated for International Planner events
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Oct 06 2009 14:05:28   mmodi
//Updated for EBC events
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Jan 19 2009 11:17:22   mmodi
//Updated for Gradient profile event
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.2   Oct 13 2008 16:46:32   build
//Automatically merged from branch for stream5014
//
//   Rev 1.1.1.0   Aug 22 2008 10:55:46   mmodi
//Updated for cycle events
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   May 14 2008 15:31:10   mmodi
//Updated for repeat visitor
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.0   Nov 08 2007 12:38:46   mturner
//Initial revision.
//
//   Rev 1.13   Oct 12 2006 12:08:04   PScott
//CCR 4221 /CCN336
//Add TransferUnknownPartnerLandingEvents  stored proc
//
//   Rev 1.12   Feb 23 2006 12:07:32   RWilby
//Merged stream3129
//
//   Rev 1.11   Feb 20 2006 15:40:18   build
//Automatically merged from branch for stream0017
//
//   Rev 1.10.2.0   Jan 30 2006 11:11:08   tolomolaiye
//Changes for RTTI Internal Event
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.10   Oct 12 2005 10:33:24   schand
//Added comment
//Resolution for 2840: CCN218 - Landing Page Entry Event not working correctly on SITEST
//
//   Rev 1.9   Oct 12 2005 10:24:12   schand
//Merged stream 2610.
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.8   Feb 07 2005 14:42:46   passuied
//added code for ExposedServices Events
//
//   Rev 1.7   Feb 02 2005 18:51:10   passuied
//changes for StopEventRequestEvent
//
//   Rev 1.6   Jan 25 2005 18:40:06   schand
//Renamed TransferRTTIEventCounters to TransferRTTIEvent
//
//   Rev 1.5   Jan 24 2005 15:28:08   schand
//Added entry for RTTIEventCounter
//
//   Rev 1.4   Jul 23 2004 16:54:02   jmorrissey
//Added TransferUserFeedbackEvents
//
//   Rev 1.3   Jul 15 2004 18:04:02   acaunt
//TransferJourneyProcessingEvents now longer requires the time window passed in as a parameters
//
//   Rev 1.2   Jul 06 2004 10:13:16   jgeorge
//Added InternalRequestEvent
//
//   Rev 1.1   Dec 03 2003 14:05:50   geaton
//Added importer for SessionEvents report table.
//
//   Rev 1.0   Nov 26 2003 11:00:36   geaton
//Initial Revision

using System;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	/// <summary>
	/// Defines prototype determining parameters to pass to stored procedures for importing.
	/// All possible parameters are defined in the delegate.
	/// Delegate implementations pick the required parameters from those available.
	/// </summary>
	public delegate Hashtable AddParameters(string dateValue, int CJPWebRequestWindow);

	/// <summary>
	/// Class that creates a group of importers.
	/// </summary>
	public class ImporterFactory : IImporterFactory
	{
		/// <summary>
		/// Names of stored procedures that transfer data from staging table to report table.
		/// One stored procedure is provided per table in the reporting database.
		/// </summary>
		private string[] spNames =  { "TransferEnhancedExposedServicesEvents",
								      "TransferGazetteerEvents",
									  "TransferJourneyPlanLocationEvents",
									  "TransferJourneyPlanModeEvents",
									  "TransferJourneyProcessingEvents",
									  "TransferJourneyWebRequestEvents",
									  "TransferLoginEvents",
									  "TransferMapEvents",
									  "TransferOperationalEvents",
									  "TransferPageEntryEvents",
									  "TransferReferenceTransactionEvents",
									  "TransferRetailerHandoffEvents",
									  "TransferWorkloadEvents",
									  "TransferDataGatewayEvents",
									  "TransferUserPreferenceSaveEvents",
									  "TransferRoadPlanEvents",
									  "DummyNameJourneyPlanRequestVerboseEvents",  // No SP for transfer since data not imported from this table.
									  "DummyNameJourneyPlanResultsVerboseEvents", // No SP for transfer since data not imported from this table.
									  "TransferSessionEvents",
									  "TransferInternalRequestEvents",
									  "TransferUserFeedbackEvents",
									  "TransferRTTIEvent",
									  "TransferStopEventRequestEvent",
									  "TransferExposedServicesEvents",
									  "TransferLandingPageEvents", //Landing Page SP
									  "TransferRTTIInternalEvent",  //RTTI Event extra logging
									  "TransferUnknownPartnerLandingEvents",  //Unknown Page Landing Partners logging
                                      "TransferRepeatVisitorEvents",
                                      "TransferCyclePlannerEvents",
                                      "TransferGradientProfileEvents",
                                      "TransferEBCCalculationEvents",
                                      "TransferInternationalPlannerEvents",
                                      "TransferInternationalPlannerJourneyEvents",
                                      "TransferMapAPIEvents",
                                      "TransferRealTimeRoadEvents",
                                      "TransferGISQueryEvents",
                                      "TransferAccessibleEvents"
									};   



		/// <summary>
		/// Names of the staging tables used by the transfer stored procedures.
		/// Each row in the array defines one or more staging tables for import using
		/// the stored procedure defined in the addNames array for that row.
		/// </summary>
        private string[][] stagingNames = { new string[] { "EnhancedExposedServiceEvent" },
											new string[] { "GazetteerEvent" },
											new string[] { "JourneyPlanResultsEvent", "JourneyPlanRequestEvent", "LocationRequestEvent" },
											new string[] { "JourneyPlanRequestEvent" },
											new string[] { "JourneyWebRequestEvent", "JourneyPlanRequestEvent", "JourneyPlanResultsEvent" },
											new string[] { "JourneyWebRequestEvent" },
											new string[] { "LoginEvent" },
											new string[] { "MapEvent" },
											new string[] { "OperationalEvent" },
											new string[] { "PageEntryEvent" },
											new string[] { "ReferenceTransactionEvent" },
											new string[] { "RetailerHandoffEvent" },
											new string[] { "WorkloadEvent" },
											new string[] { "DataGatewayEvent" },
											new string[] { "UserPreferenceSaveEvent" },
											new string[] { "JourneyPlanRequestEvent" },
											new string[] { "JourneyPlanRequestVerboseEvent" },
											new string[] { "JourneyPlanResultsVerboseEvent" },
											new string[] { "PageEntryEvent" },
											new string[] { "InternalRequestEvent" },
											new string[] { "UserFeedbackEvent" }, 
											new string[] { "RTTIEvent"},
											new string[] { "StopEventRequestEvent"},
											new string[] { "ExposedServicesEvent"},
										  	new string[] { "LandingPageEvent"}, //Landing Page table
											new string[] { "RTTIInternalEvent"},
											new string[] { "LandingPageEvent"},
                                            new string[] { "RepeatVisitorEvent"},
                                            new string[] { "CyclePlannerRequestEvent" }, 
                                            new string[] { "GradientProfileEvent" },
                                            new string[] { "EBCCalculationEvent" },
                                            new string[] { "InternationalPlannerEvent" },
                                            new string[] { "InternationalPlannerResultEvent", "InternationalPlannerRequestEvent" },
                                            new string[] { "MapAPIEvent" },
                                            new string[] { "RealTimeRoadEvent" },
                                            new string[] { "GISQueryEvent" },
                                            new string[] { "AccessibleEvent" }
                                    	};   

		/// <summary>
		/// Defines methods that should be used to calculate parameters 
		/// passed to each stored procedure defined in the addNames array.
		/// </summary>
		private AddParameters[] parameterMethods = { new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 null,
													 null,
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ),	//Landing Page SP
													 new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //RTTI Extra event logging
												     new AddParameters( StoredProcParameterAdder.StandardParamAdder ),
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //Repeat visitor event logging
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //Cycle planner event
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //Gradient profile event
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //EBC calculation event
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //InternationalPlannerEvent
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //InternationalPlannerResultEvent
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), //MapAPIEvent
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ), // RealTimeRoadEvent
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder ),  //GISQueryEvent
                                                     new AddParameters( StoredProcParameterAdder.StandardParamAdder )  //AccessibleEvent
												   };
		/// <summary>
		/// Indicators that define whether the Import service is enabled.
		/// </summary>
		private bool[] importEnabled =	{ true,   // TransferEnahncedExposedServicesEvents
											true,   // TransferGazetteerEvents
											true,   // TransferJourneyPlanLocationEvents
											true,   // TransferJourneyPlanModeEvents
											true,   // TransferJourneyProcessingEvents
											true,   // TransferJourneyWebRequestEvents
											true,   // TransferLoginEvents
											true,   // TransferMapEvents
											true,   // TransferOperationalEvents
											true,   // TransferPageEntryEvents
											true,   // TransferReferenceTransactionEvents
											true,   // TransferRetailerHandoffEvents
											true,   // TransferWorkloadEvents
											true,   // TransferDataGatewayEvents
											true,	// TransferUserPreferenceSaveEvents
											true,	// TransferRoadPlanEvents
											false,  // relates to JourneyPlanRequestVerboseEvent table
											false,	// relates to JourneyPlanResultsVerboseEvent table
											true,	// TransferSessionEvents
											true,   // TransferInternalRequestEvents
											true,	// TransferUserFeedbackEvents
											true,	// TransferRTTIEventCounters
											true,	// TransferStopEventRequestEvent
											true,   // TransferExposedServicesEvents
											true,   // LandingPageEvents
											true,	// RTTI extra event logging
											true,	// TransferUnknownPartnerLandingEvents
                                            true,   // Repeat visitor event logging
                                            true,   // TransferCyclePlannerEvents
                                            true,   // TransferGradientProfileEvents
                                            true,   // TransferEBCCalculationEvents
                                            true,   // TransferInternationalPlannerEvents
                                            true,   // TransferInternationalPlannerJourneyEvents
                                            true,   // TransferMapAPIEvents
                                            true,   // TransferRealTimeRoadEvents
                                            true,    // TransferGISQueryEvents
                                            true    // TransferAccessibleEvents
										};	

		/// <summary>
		/// Indicators that define whether the Archive service is enabled.
		/// </summary>
		private bool[] archiveEnabled =	{ true, // TransferEnahncedExposedServicesEvents
										  true,   // TransferGazetteerEvents
										  true,   // TransferJourneyPlanLocationEvents
										  true,   // TransferJourneyPlanModeEvents
										  true,   // TransferJourneyProcessingEvents
										  true,   // TransferJourneyWebRequestEvents
										  true,   // TransferLoginEvents
										  true,   // TransferMapEvents
										  true,   // TransferOperationalEvents
										  true,   // TransferPageEntryEvents
										  true,   // TransferReferenceTransactionEvents
										  true,   // TransferRetailerHandoffEvents
									      true,   // TransferWorkloadEvents
										  true,   // TransferDataGatewayEvents
										  true,	  // TransferUserPreferenceSaveEvents
										  true,   // TransferRoadPlanEvents
										  true,	  // DummyNameJourneyPlanRequestVerboseEvents
										  true,   // DummyNameJourneyPlanResultsVerboseEvents
										  true,   // TransferSessionEvents
										  true,   // TransferInternalRequestEvents
										  true,	  // TransferUserFeedbackEvents
										  true,	  // TransferRTTIEventCounters	
										  true,	  // TransferStopEventRequestEvent
										  true,   // TransferExposedServicesEvents
										  true,   // TransferLandingPageEvents
										  true,	  // RTTI extra logging event
										  true,   // TransferUnknownPartnerLandingEvents
                                          true,   // Repeat visitor event
                                          true,   // TransferCyclePlannerEvents
                                          true,   // TransferGradientProfileEvents
                                          true,   // TransferEBCCalculationEvents
                                          true,   // TransferInternationalPlannerEvents
                                          true,   // TransferInternationalPlannerJourneyEvents
                                          true,   // TransferMapAPIEvents
                                          true,   // TransferRealTimeRoadEvents
                                          true,    // TransferGISQueryEvents
                                          true    // TransferAccessibleEvents
                                        };  

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ImporterFactory()
		{
		}

		/// <summary>
		/// Checks the validity of the importer group <code>importers</code> passed.
		/// Checks for situation where an Importer, that has Import disabled and 
		/// Archive enabled, and uses a staging table that is also used by another 
		/// Importer, that has Import enabled. This situation is illegal because 
		/// the shared table may be archived without respect to the Importer that
		/// uses it for import.
		/// </summary>
		/// <param name="importers">Importer group to check.</param>
		/// <returns>True if group is valid.</returns>
		/// <remarks>
		/// This method has not been implemented since the configuration of
		/// the Importer Factory is made at compile time so manual checks
		/// can be made to ensure validity of group.
		/// If in the future, runtime configuration is made possible then this
		/// method will need to be implemented.
		/// </remarks>
		private bool ValidGroup(Importer[] importers)
		{
			return true;
		}

		/// <summary>
		/// Factory method that create Importer instances.
		/// </summary>
		/// <param name="requestDate">
		/// Time that request for the importers was made.
		/// Used for auditing.
		/// </param></param>
		/// <param name="timeout">
		/// Duration in seconds to wait for SLQ commands to complete before timing out.
		/// </param>
		/// <param name="CJPWebRequestWindow">
		/// Duration of CJP web request window - required by some Importers when performing Imports ONLY.
		/// </param>
		/// <returns>Array of importers.</returns>
		/// <exception cref="TDException">
		/// Creation failed.
		/// </exception>
		public Importer[] CreateImporters(DateTime requestDate, int timeout, int CJPWebRequestWindow)
		{
			Importer[] importers = new Importer[spNames.Length];

			for (int i = 0; i < spNames.Length; i++ )
			{
				importers[i] =  new Importer(requestDate,
										     spNames[i],
											 stagingNames[i],
											 parameterMethods[i],
											 timeout,
											 importEnabled[i],
											 archiveEnabled[i],
											 CJPWebRequestWindow);
			}

			if (!ValidGroup(importers))
				throw new TDException(Messages.ImporterFactory_InvalidGroup, false, TDExceptionIdentifier.RDPDataImporterInvalidImporterGroup);

			return importers;
		}
	}
}
