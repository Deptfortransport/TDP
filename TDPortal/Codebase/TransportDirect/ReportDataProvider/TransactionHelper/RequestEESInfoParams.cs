// *********************************************** 
// NAME                 : RequestEESInfoParams.cs
// AUTHOR               : Mark Turner
// DATE CREATED         : 14/01/2009 
// DESCRIPTION  		: Implementation of the RequestEESInfoParams class. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestEESInfoParams.cs-arc  $ 
//
//   Rev 1.2   Jan 26 2009 11:46:48   mturner
//Changed outward and return times to be DateTime rather than Timespan objects
//
//   Rev 1.1   Jan 23 2009 10:38:16   mturner
//Added new member variables for journey planning requests
//
//   Rev 1.0   Jan 14 2009 17:56:46   mturner
//Initial revision.
//Resolution for 5215: Workstream for RS620

using System;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	/// <summary>
	/// Summary description for EESRequestInfoParams.
	/// </summary>
	public struct RequestEESInfoParams
	{
		public string origin;
		public string destination;

        public string originAlternative;
        public string destinationAlternative;

		public string serviceNumber;
        public string method;
        public string searchType;
        public string day;

	    public bool showDepartures; 
		public bool showCallingPoints;
        public bool isReturnRequired;
        public bool outwardArriveBefore;
        public bool returnArriveBefore;

        public int outwardDayOfWeek;
        public int returnDayOfWeek;
        public int walkingSpeed;
        public int maxWalkingTime;
        public int interchangeSpeed;
        public int sequence;

        public DateTime outwardTime;
        public DateTime returnTime;
	}
}
