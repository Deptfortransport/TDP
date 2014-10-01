// *********************************************** 
// NAME                 : SSExceptionIdentifier.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Exception ids
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/SSExceptionIdentifier.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:23:42   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.Common
{
    /// <summary>
    /// Enum of available exception types
    /// </summary>
    [Serializable]
    public enum SSExceptionIdentifier
    {
        Undefined = -1,

        // Appliation
        APPMissingApplicationConfigurationFile = 001,

        // Event Logging Service
        ELSEventLogConstructor = 100,
        ELSTraceLevelUninitialised = 101,
        ELSLoggingPropertyValidatorUnknownKey = 102,
        ELSTraceListenerUnknownObject = 103,
        ELSDefaultPublisherFailed = 104,
        ELSEventLogPublisherWritingEvent = 105,
        ELSConsolePublisherWritingEvent = 106,
        ELSFilePublisherWritingEvent = 107,
        ELSQueuePublisherWritingEvent = 108,
        ELSQueuePublisherConstructor = 109,
        ELSTraceListenerConstructor = 110,
        ELSCustomOperationalConstructor = 111,
        ELSGlobalTraceLevelUndefined = 112,
        ELSSwitchOnUnknownEvent = 113,
        ELSDatabasePublisherConstructor = 114,
        ELSUnsupportedTraceListenerMethod = 115,
        
        // Property Service
        PSMissingPropertyFile = 200,
        PSLoadPropertyFileFail = 201,
        PSInvalidVersion = 202,
        PSInvalidAssembly = 203,
        PSInvalidAssemblyType = 204,
        PSBadEnum = 205,
        PSMissingProperty = 206,
        PSErrorLoggingToEventLog = 207,

        // Database
        DBError = 300,
        DBSQLHelperInsertFailed = 301,
        DBSQLHelperStoredProcedureFailure = 302,
        DBSQLHelperPrimaryKeyViolation = 303,
        DBUnsupportedDatabasePublisherEvent = 304,
        DBFailedPublishingReferenceTransactionEvent = 305,

        // Site Status Loader Service
        SSLSMissingURL = 400,
        SSLSInvalidURL = 401,
        SSLSErrorRetrievingDataFromURL = 402,
        SSLSErrorSavingDataToFile = 403,
        SSLSEventStatusParser = 404,
        SSLSEventAlertLevelSettings = 405
    }
}
