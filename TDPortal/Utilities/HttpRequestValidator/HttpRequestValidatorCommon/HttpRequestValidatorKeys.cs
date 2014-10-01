// *********************************************** 
// NAME         : Keys.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 24/01/2011
// DESCRIPTION  : Keys class for configuration items
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidatorCommon/HttpRequestValidatorKeys.cs-arc  $
//
//   Rev 1.0   Feb 03 2011 10:15:02   mmodi
//Initial revision.
//
//   Rev 1.0   Jan 27 2011 16:24:20   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.HttpRequestValidatorCommon
{
    /// <summary>
    /// Keys class
    /// </summary>
    public static class HttpRequestValidatorKeys
    {
        public const string ConfigurationSection = "system.webServer/httpRequestValidator";

        public const string SwitchConnections = "switchConnections";
        public const string SwitchSession = "switchSession";
        public const string SwitchDomainRedirect = "switchDomainRedirect";
        public const string Duration_Minutes = "durationMinutes";
        public const string Threshold_Hits_Max = "hitsMax";
        public const string Threshold_Hits_RepeatOffender = "hitsRepeatOffender";
        public const string Threshold_Time_Milliseconds_Min = "timeMillisecondsMin";
        public const string SessionCookieName = "sessionCookieName";
        public const string URL_Extensions = "urlExtensions";
        public const string ErrorPage_URL = "errorPageURL";
        public const string ResponseStatus = "responseStatus";
        public const string EventLog_Name = "eventLogName";
        public const string EventLog_Source = "eventLogSource";
        public const string EventLog_Machine = "eventLogMachine";
        public const string EventLog_MessagePrefix = "eventLogMessagePrefix";
        public const string Connections_MonitorPeriodSeconds = "monitorPeriodSeconds";
        public const string Connections_TriggerThreshold = "connectionsTrigger";
        public const string Connections_RestoreThreshold = "connectionsRestore";
        public const string Connections_PerformanceCounter_CategoryName = "categoryName";
        public const string Connections_PerformanceCounter_CounterName = "counterName";
        public const string Connections_PerformanceCounter_InstanceName = "instanceName";
        public const string Connections_PerformanceCounter_SecondInstanceName = "secondInstanceName";
        public const string Connections_StatusFilePath = "statusFilePath";
        public const string Connections_StatusFileName = "statusFileName";
        public const string DomainRedirects_RedirectList = "redirectList";
        public const string DomainRedirects_IncludeExtensions = "includeExtensions";
        public const string CoolDown_FileLocation = "fileLocation";
        public const string CoolDown_ReportInterval = "reportIntervalSeconds";
        public const string CoolDown_PerformanceCounterName = "counterName";
        public const string MessageRedirector_FileLocation = "fileLocation";
        public const string MessageRedirector_ReportInterval = "reportIntervalSeconds";
        public const string MessageRedirector_PerformanceCounterName = "counterName";
        
        public const string Element_Monitor = "monitor";
        public const string Element_Session = "session";
        public const string Element_Extensions = "extensions";
        public const string Element_Thresholds = "thresholds";
        public const string Element_Response = "response";
        public const string Element_EventLog = "eventLog";
        public const string Element_Connections = "connections";
        public const string Element_DomainRedirects = "domainRedirects";
        public const string Element_CoolDown = "coolDown";
        public const string Element_MessageRedirector = "messageRedirector";
    }
}
