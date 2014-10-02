// *********************************************** 
// NAME             : Messages.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Messages class
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService
{
    /// <summary>
    /// Messages class
    /// </summary>
    public class Messages
    {
        // Service Messages
        public const string Service_InternalError = "Departure Board Web Service encountered a problem while processing the request";
        public const string Service_SoapException = "SoapException returned from LDB Web Service";
        public const string Service_FaultException = "FaultException returned from LDB Web Service";

        public const string LDB_StationBoardRequestInvalid = "Station board request is invalid: {0}";
        public const string LDB_ServiceDetailRequestInvalid = "Service detail request is invalid: {0}";
        public const string LDB_MissingRequest = "Request is null";
        public const string LDB_MissingLocation = "Location is null or empty";
        public const string LDB_MissingServiceId = "Service Id is null or empty";

        public const string LDB_StationBoardOK = "Station board result ok";
        public const string LDB_NullStationBoardResult = "Unable to retrieve station board for {0}, null result returned for request";

        public const string LDB_ServiceDetailOK = "Service detail result ok";
        public const string LDB_NullServiceDetailResult = "Unable to retrieve service detail for {0}, null result returned, service is no longer available";

        public const int LDB_Code_StationBoardOK = 0;
        public const int LDB_Code_ServiceDetailOK = 0;        
    }
}