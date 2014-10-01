// *********************************************** 
// NAME                 : Messages.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Messags used for logging
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/Messages.cs-arc  $
//
//   Rev 1.2   Apr 09 2009 10:40:16   mmodi
//Updated messages
//Resolution for 5273: Site Status Loader
//
//   Rev 1.1   Apr 06 2009 16:06:02   mmodi
//Updated messages
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:23:40   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.Common
{
    public class Messages
    {
        // PROPERTIES - Initialise error messages
        public const string PSApplicationConfigFileMissing = "Unable to read values in Application Configuration file, please check file exists at [{0}].";
        public const string PSPropertyFileMissing = "Property file for application is missing. Unable to setup properties service.";
        public const string PSPropertyLoadFail = "Error occurred attempting to load properties in property file [{0}].";
        public const string PSPropertyLogEventFail = "Error occurred attempting to write to the Event Log. Exception: {0}";

        // PROPERTIES - PropertyValidator Messages
        public const string PVPropertyValueBad = "Property with key [{0}] has invalid value of [{1}]. Usage: [{2}].";
        public const string PVClassNotFoundInAssembly = "Class [{0}] specified in property key [{1}] not found in assembly [{2}].";
        public const string PVBadAssembly = "Assembly specified in property key [{0}] cannot be loaded for the reason: [{1}]. Ensure the assembly name specified in properties does not include .dll extension. A full file path is also not necessary.";
        public const string PVPropertyValueMissing = "A value for the property specified in key [{0}] is missing.";
        public const string PVInvalidPropertyKey = "The property key [{0}] is invalid and must be removed completely.";
        public const string PVInvalidPropertyLength = "Property [{0}] specified with key [{1}] has an invalid length. Length must be between [{2}] and [{3}].";
        public const string PVInvalidPropertyLengthMin = "Property [{0}] specified with key [{1}] has an invalid length. Length must be at least [{2}].";
        public const string PVEnumConversionError = "Error converting property value [{0}] to enumerated type [{1}].";
        public const string PVPropertyKeyMissing = "The property key [{0}] has not been defined. This property key is mandatory.";
        public const string PVAssemblyTypeReadFail = "Unable to inspect types defined in assembly [{0}]. This assembly has been declared as containing the class specified in key [{1}]. Ensure that assemblies containing types used by this assembly are in same directory as the assembly specified.";

        // EVENT LOGGING - Property validation messages
        public const string ELPVUndefinedPublisher = "A Publisher specified for an event [{0}] has an id [{1}] that does not match any of the publishers available.";
        public const string ELPVFilePublisherDirectoryBad = "Directory [{0}] of file publisher specified in property [{1}] does not exist.";
        public const string ELPVFilePublisherRotationBad = "Rotation value [{0}] of file publisher specified in property [{1}] must be an integer and greater than zero.";
        public const string ELPVQueuePublisherMSMQBad = "MSMQ [{0}] of queue publisher specified in property [{1}] cannot be written to. MSMQ may not exist or path may be invalid.";
        public const string ELPVQueuePublisherDeliveryBad = "Delivery method [{0}] of queue publisher specified in property [{1}] is invalid. Must take value Express|Recoverable.";
        public const string ELPVLoggingPropertyValidatorKeyBad = "LoggingPropertyValidators unable to validate key [{0}]";
        public const string ELPVEventLogPublisherEventLogBad = "Event Log [{0}] on machine [{1)] belonging to event log publisher does not exist. Values were specified in properties [{2}] and [{3}].";
        public const string ELPVEventLogPublisherEventLogNotFound = "Event Log belonging to event log publisher cannot be found. Reason: [{0}]. Values were specified in properties [{1}] and [{2}].";
        public const string ELPVEventHasZeroPublishers = "One or more publishers have not been defined for the event specified in key [{0}]. It is mandatory to specify one or more publishers for this event.";
        public const string ELPVIncorrectClassType = "Class [{0}] specified in property key [{1}] does not derive from class [{2}].";

        public const string ELPVOperationalEventLevelInvalid = "An Operational Event has been created using an invalid TraceLevel.";

        // EVENT LOGGING - Standard Publisher messages
        public const string ELConsolePublisherWriteEvent = "Exception occured when Console Publisher attempted to write log event to the console.";
        public const string ELEventLogPublisherConstructor = "Exception [{0}] occured when constructing an Event Log Publisher:  Event Log Name:[{1}] Event Log Source:[{2}] Event Log Machine:[{3}]";
        public const string ELEventLogPublisherWriteEvent = "Exception occured when publishing the event using Event Log Publisher:- Event Log Name:[{0}] Event Log Source:[{1}] Event Log Machine:[{2}]";
        public const string ELFilePublisherConstructor = "Exception occured while attempting to set-up the File Publisher.  The value(s) that may have caused the exception are:{0}";
        public const string ELFilePublisherWriteEvent = "Exception occured while attempting to write the log event [{0}] to the file [{1}].";
        public const string ELQueuePublisherConstructor = "Exception occured while attempting to set-up the Message Queue.  The value(s) that may have caused the exception are:{0}";
        public const string ELQueuePublisherWriteEvent = "Exception occured while attempting to send the logEvent to the Message Queue. The value(s) that may have caused the exception are:{0}";

        // EVENT LOGGING - SSTraceListener messages
        public const string SSTTraceListenerUnsupportedPrototype = "Unsupported TraceListener write prototype called with parameter(s): [{0}]. Change this call to use Write(object) prototype!";
        public const string SSTTraceSwitchNotInitialised = "The TraceSwitch class instance has not been initialised. Ensure that the TraceListener has been created first.";
        public const string SSTTraceListenerWriteUnimplemented = "A prototype of TraceListener.Write has been used but is not supported - use void Write(object) instead.";
        public const string SSTTraceListenerConstructor = "One or more errors occurred during the construction of TraceListener for component with property Application Id [{0}] and Group Id [{1}].";
        public const string SSTTraceListenerDefaultPublisherFailure = "Failed to write an event using the default publisher. The default publisher was used because another publisher failed to write the event - check inner exception for details.";
        public const string SSTTConfiguredPublisherFailed = "Failed to write a custom event of type [{0}] using it's configured publisher [{1}]. Reason: [{2}]";
        public const string SSTTDefaultFormatterOutput = "SS-EVENT\t{0}\tEventClassName:[{1}]\tWARNING:This event data was formatted using DefaultFormatter and may have resulted in data loss.";
        public const string SSTTraceListenerSubscriberNotificationFailed = "TraceListener failed to notify subscriber/s with property values.";
        public const string SSTTraceListenerPropertyChange = "TraceListener received notification from property provider that properties have changed. The properties that changed may or may not be relevent to the Event Logging Service.";
        public const string SSTTraceListenerDefaultPublisherUsed = "A custom event of type [{0}] was published using the default publisher.";
        public const string SSTTraceListenerUnknownClass = "An attempt to log an object that is not of type LogEvent occurred.";
        public const string SSTCustomPublisherArrayInvalid = "Custom Publisher array cannot be null. Number of elements must be zero or more.";
        public const string SSTCustomPublisherInvalidId = "Custom Publisher Class [{0}] does not have a valid identifier assigned to it. Current identifier value is [{1}].";
        public const string SSTCustomPublisherNameMismatch = "Custom Publisher Class [{0}] has an invalid class name or invalid identifier. According to the given class identifier the class name should be specified in property key [{1}].";
        public const string SSTCustomEventSwitchNotInitialised = "The CustomEventSwitch class instance has not been initialised. Ensure that the TraceListener has been created first.";
        public const string SSTUnknownCustomEventClassName = "The name of the Custom Event class [{0}] is unknown. Ensure: (1) the correct Custom Event class 'name' is passed to the CustomEventLevelSwitch. (2) The TraceListener has been added to the trace listener collection. (3) The Custom Event class has been defined in the logging service configuration properties.";
        public const string SSTDatabasePublisherConstructorFailed = "Database Publisher constructor failed with errors: {0}";
        public const string SSTDatabasePublisherUnsupportedEventType = "Unsupported event type [{0}] found when publishing to database using database publisher [{1}].";
        
        // DATABASE
        public const string DBSQLHelperError = "SQL Helper error when excuting stored procedure [{0}]. Message: [{1}]";
        public const string DBSQLHelperTypeError = "SQL Helper Type error when excuting stored procedure [{0}]. Message: [{1}]";
        public const string DBSQLHelperInsertFailed = "SQL Helper failed to insert the expected number of rows [{0}] when excuting stored procedure [{1}]. Return code of [{2}] was returned.";
        public const string DBSQLHelperPrimaryKeyError = "SQL Helper failed when executing stored procedure [{0}] because of a Primary Key violation. Message: [{1}]";
        public const string DBSQLDateTimeOverflow = "Failure before call to stored procedure [{0}] - The event :{1} has a DateTime that is not compatible with SqlDateTime";
        public const string DBError = "Error occurred performing a database action. Exception: {0}.";
        public const string DBEventPublishFailure = "Failed to publish event of type [{0}] to database. Reason: [{1}]";

        // SITE STATUS LOADER SERVICE
        public const string SSLSInitialisationFailed = "Infrastructure\nInitialisation Failed\n{0}";
        public const string SSLSInitialisationCompleted = "Initialisation of {0} completed successfully.";
        public const string SSLSSerivceStartFailed = "{0} failed to start.";
        public const string SSLSLoaderError = "Error occurred on Site status loader thread [{0}] Exception: {1}";
        public const string SSLSLoaderServiceStartFailed = "Error occurred attempting to start the Site status loader thread [{0}] Exception: {1}";
        public const string SSLSLoaderServiceStarting = "Site status loader thread [{0}] is starting.";
        public const string SSLSLoaderServiceStartsAt = "Site status loader thread [{0}] starts at: {1}h:{2}m:{3}s";
        public const string SSLSLoaderServiceStartsTimeSetupFailed = "Error occurred setting up the time to start the Site status loader thread [{0}]. Thread is being started now.";
        public const string SSLSLoaderServiceRestartTimeSetupFailed = "Error occurred setting up the restart time of the Site status loader thread timer [{0}]. Restart time being set to 00:00:00.";
        public const string SSLSLoaderServiceStarted = "Site status loader thread [{0}] started.";
        public const string SSLSServiceStarted = "{0} started.";
        public const string SSLSServicePaused = "{0} paused.";
        public const string SSLSServiceContinued = "{0} continued.";
        public const string SSLSServiceStopped = "{0} stopped.";
        public const string SSLSServiceRunning = "{0} event status poller is running. ";
        public const string SSLSLoaderServiceThreadTurnedOff = "Site status loader thread [{0}] is configured not to run. Site status data will not be downloaded.";
        public const string SSLSServiceTimerIntervalMissing = "The timer interval value property has not been defined, defaulting to an interval of [{0}] milliseconds.";
        public const string SSLSServiceTimerIsBeingRestarted = "Site status loader thread [{0}] timer is being restarted.";
        public const string SSLSServiceTimerHasRestarted = "Site status loader thread [{0}] timer has restarted.";
        public const string SSLSErrorParsingScheduleDays = "Error attempting to parse the property [{0}] in to an array of days to run service on. Defaulting to every day of the week.";
        public const string SSLSGettingSiteStatusData = "Site status loader thread [{0}] Getting site status data.";
        public const string SSLSMissingSiteStatusURL = "Site status data URL in property [{0}] is missing or invalid.";
        public const string SSLSErrorGeneratingHistoricSiteStatusURL = "Site status loader thread [{0}] Error generating URL to download site status data from: [{1}]";
        public const string SSLSGetSiteStatusFromURLFail = "Error attempting to obtain Site status data from URL [{0}]. Exception: {1}";
        public const string SSLSSiteStatusDataSavedToFile = "Site status data has been saved to file [{0}]";
        public const string SSLSErrorParsingSaveToFileFlag = "Error occurred attempting to parse the bool flag for saving site status data to a file. Not saving data to a file. Exception: {0}";
        public const string SSLSMissingLocationToSaveDataTo = "Location to save site status data file is missing or invalid, attempted to save to [{0}] [{1}]";
        public const string SSLSErrorSavingDataToFile = "Error occurred attempting to save site status data to file [{0}] Exception: {1}";
        public const string SSLSErrorSettingUpEventsToMonitor = "Error occurred setting up the events to monitor specified in property [{0}].";
        public const string SSLSErrorSettingUpAlertLevelsForEvents = "Error occurred setting up the alert levels for events to monitor.";
        public const string SSLSMissingPropertyForEventToMonitor = "All properties were not found for Event being monitored [{0}].";
        public const string SSLSUpdatingSiteStatusAlertLevel = "Site status loader thread [{0}] Updating site status event alert levels.";
        public const string SSLSLoggingSiteStatusEvents = "Site status loader thread [{0}] Logging site status events.";
        public const string SSLSLoggingStatusEvent = "Logging event status for [{0}].";
        public const string SSLSNotLoggingStatusEvent = "Not logging event status for [{0}], this has already been logged on a previous site status check.";
        public const string SSLSLoggedSiteStatusEvent = "Historic site status event was written to the database: [{0}].";
        public const string SSLSHistoricSiteStatusDataCount = "Historic site status data downloaded on [{0}] contained [{1}] valid event status records. Any of these records written to the database will be logged.";
        public const string SSLSErrorParsingHistoricSiteStatusEvent = "Error occurred attempting to parse site status data from historic download file, [{0}] Exception: {1}";
        public const string SSLSUnableToSetupMonitorStatusFile = "Error occurred attempting to set up a status monitoring file. Error [{0}]";
        public const string SSLSErrorDuringUpdateMonitorStatusFile = "Error occurred attempting to update the status monitoring file. Error [{0}]";
        public const string SSLSErrorReadingCSVData = "Error occurred attempting to read the site status data CSV file. Error [{0}]";
        public const string SSLSUpdateAlertLevelHistory = "Updating the alert levels history for the configured time period.";
        public const string SSLSUpdateAlertLevelHistoryError = "Error occurred updating the alert level counts. Exception: {0}";
        public const string SSLSTestStatusEventDataCreated = "Site status loader thread [{0}] Status data file created.";
        public const string SSLSErrorGeneratingHistoricSiteStatusFile = "Site status loader thread [{0}] Error generating file to save site status data to: [{1}]";
        public const string SSLSEventStatusDetails = "Event status details: {0} \n";
        public const string SSLSAlertRed = "{0}[{1}]. \n";
        public const string SSLSAlertAmber = "{0}[{1}]. \n";
        public const string SSLSAlertMessageStatus = "Event status returned was [{0}]. \n";
        public const string SSLSAlertMessageTimeThreshold = "Threshold is configured to be [{0}] milliseconds, event duration was [{1}] milliseconds. \n";
        public const string SSLSDownloadError = "{0}\n";
        public const string SSLSDatabaseCurrentError = "{0}\n";
        public const string SSLSDatabaseDayError = "{0}\n";
        
        // SITE STATUS MONITOR APPLICATION
        public const string SSMAInitialisationFailed = "Infrastructure\nInitialisation Failed\n{0}";
        public const string SSMAInitialisationCompleted = "Initialisation of {0} completed successfully.";
        public const string SSMAStarting = "Site status monitor application being started.";
        public const string SSMAStarted = "Site status monitor application has started successfully.";
        public const string SSMARunning = "Site status monitor application is running. Monitoring service [{0}]";
        public const string SSMAExiting = "Exiting application {0}.";
        public const string SSMAServiceRunning = "Running";
        public const string SSMAServiceStopped = "Stopped";
        public const string SSMAServicePaused = "Paused";
        public const string SSMAServiceRunningUnknown = "Running (unknown)";
        public const string SSMAServiceRunningRed = "Running (Red alert)";
        public const string SSMAServiceRunningAmber = "Running (Amber alert)";
        public const string SSMAServiceRedAlert = "Red alert";
        public const string SSMAServiceAmberAlert = "Amber alert";
        public const string SSMAConnectingToService = "Attempting to connect to service [{0}]";
        public const string SSMAConnectedToService = "Connected to service [{0}] successfully.";
        public const string SSMAServiceTimerRunning = "Site status monitor timer is running with interval [{0}]";
        public const string SSMAErrorConnectingToService = "Error occurred attempting to connect to service. Exception: {0}";
        public const string SSMAUpdateSiteStatusForm = "Updating with latest site status data.";        
        public const string SSMAUpdateSiteStatusMonitoringError = "Error occurred attempting to monitor site status for service [{0}]. Exception: {1}";
        public const string SSMASiteStatusFilePropertyError = "Error occurred attempting to read properties for Site status file [{0}]. Exception: {1}";
        public const string SSMASiteStatusFileMissing = "Site status file [{0}] was not found. Unable to monitor status alert levels.";
        public const string SSMASiteStatusFileMissingShort = "Site status file was not found.";
        public const string SSMASiteStatusFileError = "Error occurred attempting to read the site status file at [{0}]. Exception: {1}";       
        public const string SSMAServiceStart = "Site status monitor application has sent a Start request to service [{0}]";
        public const string SSMAServicePause = "Site status monitor application has sent a Pause request to service [{0}]";
        public const string SSMAServiceContinue = "Site status monitor application has sent a Continue request to service [{0}]";
        public const string SSMAServiceStop = "Site status monitor application has sent a Stop request to service [{0}]";
        public const string SSMAServiceStartError = "Error occurred attempting to Start service [{0}]. Exception: {1}";
        public const string SSMAServicePauseError = "Error occurred attempting to Pause service [{0}]. Exception: {1}";
        public const string SSMAServiceContinueError = "Error occurred attempting to Continue service [{0}]. Exception: {1}";
        public const string SSMAServiceStopError = "Error occurred attempting to Stop service [{0}]. Exception: {1}";
        public const string SSMAPropertiesLoadError = "Properties could not be loaded, file was missing.";
        public const string SSMAPropertiesSaved = "Properties were changed and saved successfully.";
        public const string SSMAPropertiesSaveError = "Properties failed to save. Exception: {0}";


        static Messages()
        {
        }
    }
}
