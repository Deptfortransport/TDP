// *********************************************** 
// NAME                 : Messages.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Class containing message text
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/XMLRailTicketTypeFeed/TransportDirectXmlTicketFeed/Messages.cs-arc  $ 
//
//   Rev 1.1   Sep 19 2008 16:29:00   mmodi
//Updated messages for logging
//Resolution for 5118: RTTI XML Ticket Type feed - Logging updates

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.XmlTicketFeed
{
    public class Messages
    {
        public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class. Exception ID:[{0}]. Reason[{1}].";
        public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
        public const string Init_InvalidPropertyKeys = "Missing or invalid TicketFeed property keys found on initialisation: [{0}].";
        public const string Init_Completed = "Initialisation of TicketFeed completed successfully.";
        public const string Init_Failed = "Infrastructure\n{0}"; // OperationalEvent category prefixed for TNG to parse Event Log Description field.
        public const string Init_PropertyServiceFailed = "Failed to initialise the TD Property Service: [{0}]";
        public const string Init_CryptographicServiceFailed = "Failed to initialise the TD Cryptographic Service: [{0}]";
    }
}
