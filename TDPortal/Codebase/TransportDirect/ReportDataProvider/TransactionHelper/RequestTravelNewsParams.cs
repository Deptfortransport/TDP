// *********************************************** 
// NAME			: RequestTravelNewsParams.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 14/01/2009
// DESCRIPTION	: Implementation of the RequestTravelNewsParams class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestTravelNewsParams.cs-arc  $
//
//   Rev 1.0   Jan 14 2009 17:54:48   mturner
//Initial revision.
//Resolution for 5215: Workstream for RS620

using System;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
    public struct RequestTravelNewsParams
    {
        public string region;
        public string transport;
        public string delays;
        public string type;

        public int day;
    }
}