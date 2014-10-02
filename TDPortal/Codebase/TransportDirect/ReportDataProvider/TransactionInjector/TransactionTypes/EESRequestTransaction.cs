// *********************************************** 
// NAME                 : EESRequestTransaction.cs
// AUTHOR               : Mark Turner
// DATE CREATED         : 13/01/2009 
// DESCRIPTION  	: Provides class for a RTTI Request Information reference transaction
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/EESRequestTransaction.cs-arc  $ 
//
//   Rev 1.10   May 18 2010 11:59:20   mturner
//Added extra error trapping
//Resolution for 5537: TI Active polling may miss some exceptions
//
//   Rev 1.9   Mar 22 2010 10:14:20   MTurner
//Updates to add new active poll functionality.
//Resolution for 5472: SLA02 failing to achieve SLA
//
//   Rev 1.8   Jan 30 2009 16:34:48   mturner
//Added code to allow sequence (number of journeys requested) to be altered by the XML.
//
//   Rev 1.7   Jan 30 2009 15:25:50   mturner
//Added assignments to pass in the parameters for Walk legs
//
//   Rev 1.6   Jan 26 2009 11:48:28   mturner
//Added code to populate outward and return times from the XML
//
//   Rev 1.5   Jan 23 2009 10:37:12   mturner
//Added new code for journey planning transactions
//
//   Rev 1.4   Jan 19 2009 13:37:46   mturner
//Fixed bug that was leading to method name not being populated.
//
//   Rev 1.3   Jan 14 2009 17:51:04   mturner
//Renamed params struct to make it less ambiguous
//
//   Rev 1.2   Jan 13 2009 17:38:50   mturner
//Added logic to 'skip' transactions when neccessary
//
//   Rev 1.1   Jan 13 2009 14:43:58   mturner
//Further tech refresh updates
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Jan 13 2009 09:57:50   mturner
//Initial revision.


using System;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Net;

using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TransactionHelper;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Class used to inject requests for RTTI information 
	/// into the transaction web service..
	/// </summary>
	[Serializable]
	public class EESRequestTransaction	: TDTransaction
	{	
		private string origin = string.Empty;
		private string destination = string.Empty;
        private string originAlternative = string.Empty;
        private string destinationAlternative = string.Empty;

		private string serviceNumber = string.Empty;
		private bool showDepartures;
		private bool showCallingPoints;
        private int frequency = 0;
        private int offset = 0;
        private string method = string.Empty;
        private string searchType = string.Empty;
        private string day = string.Empty;
        private TDTimeSpan outwardTime = new TDTimeSpan();
        private TDTimeSpan returnTime = new TDTimeSpan();
        private bool isReturnRequired = false;
        private bool outwardArriveBefore = false;
        private bool returnArriveBefore = false;
        private int outwardDayOfWeek = 0;
        private int returnDayOfWeek = 0;
        private int walkingSpeed = 0;
        private int maxWalkingTime = 0;
        private int interchangeSpeed = 0;
        private int sequence = 0;
        
		public EESRequestTransaction()
		{
		}

				
		#region Public Properties

		/// <summary>
		/// Public property to get/set Origin and to serialise/de-serialise the input from the file
		/// </summary>
		public string Origin
		{
			get {return origin;}
			set {origin = value;}
		}
		
		/// <summary>
		/// Public property to get/set Destination and to serialise/de-serialise the input from the file
		/// </summary>
		public string Destination
		{
			get {return destination;}
			set {destination = value;}
		}

        /// <summary>
        /// Public property to get/set Origin Alternative and to serialise/de-serialise the input from the file
        /// </summary>
        public string OriginAlternative
        {
            get { return originAlternative; }
            set { originAlternative = value; }
        }

        /// <summary>
        /// Public property to get/set Destination Alternative and to serialise/de-serialise the input from the file
        /// </summary>
        public string DestinationAlternative
        {
            get { return destinationAlternative; }
            set { destinationAlternative = value; }
        }
		
		/// <summary>
		/// Public property to get/set ServiceNumber and to serialise/de-serialise the input from the file
		/// </summary>
		public string ServiceNumber
		{
			get {return serviceNumber;}
			set {serviceNumber = value;}
		}

		/// <summary>
		/// Public property to get/set ShowDepartures and to serialise/de-serialise the input from the file
		/// </summary>
		public bool ShowDepartures
		{
			get {return showDepartures;}
			set {showDepartures = value;}
		}
		
		/// <summary>
		/// Public property to get/set frequeny of injection and to serialise/de-serialise the input from the file
		/// </summary>
		public int Frequency
		{
			get {return frequency;}
			set {frequency = value;}
		}

        /// <summary>
        /// Public property to get/set offset value for injection and to serialise/de-serialise the input from the file
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Public property to get/set ShowCallingPoints and to serialise/de-serialise the input from the file
        /// </summary>
        public bool ShowCallingPoints
        {
            get { return showCallingPoints; }
            set { showCallingPoints = value; }
        }

        /// <summary>
        /// Public property to get/set ShowCallingPoints and to serialise/de-serialise the input from the file
        /// </summary>
        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        /// <summary>
        /// Public property to get/set ShowCallingPoints and to serialise/de-serialise the input from the file
        /// </summary>
        public string SearchType
        {
            get { return searchType; }
            set { searchType = value; }
        }

        /// <summary>
        /// Public property to get/set Day to searched for and to serialise/de-serialise the input from the file
        /// </summary>
        public string Day
        {
            get { return day; }
            set { day = value; }
        }

        /// <summary>
        /// Public property to get/set Outward Journey Time and to serialise/de-serialise the input from the file
        /// </summary>
        public TDTimeSpan OutwardTime
        {
            get { return outwardTime; }
            set { outwardTime = value; }
        }

        /// <summary>
        /// Public property to get/set Return Journey Time and to serialise/de-serialise the input from the file
        /// </summary>
        public TDTimeSpan ReturnTime
        {
            get { return returnTime; }
            set { returnTime = value; }
        }

        /// <summary>
        /// Public property to get/set IsReturnRequired and to serialise/de-serialise the input from the file
        /// </summary>
        public bool IsReturnRequired
        {
            get { return isReturnRequired; }
            set { isReturnRequired = value; }
        }

        /// <summary>
        /// Public property to get/set OutwardArriveBefore and to serialise/de-serialise the input from the file
        /// </summary>
        public bool OutwardArriveBefore
        {
            get { return outwardArriveBefore; }
            set { outwardArriveBefore = value; }
        }

        /// <summary>
        /// Public property to get/set ReturnArriveBefore and to serialise/de-serialise the input from the file
        /// </summary>
        public bool ReturnArriveBefore
        {
            get { return returnArriveBefore; }
            set { returnArriveBefore = value; }
        }

        /// <summary>
        /// Public property to get/set OutwardDayOfWeek and to serialise/de-serialise the input from the file
        /// </summary>
        public int OutwardDayOfWeek
        {
            get { return outwardDayOfWeek; }
            set { outwardDayOfWeek = value; }
        }

        /// <summary>
        /// Public property to get/set ReturnDayOfWeek and to serialise/de-serialise the input from the file
        /// </summary>
        public int ReturnDayOfWeek
        {
            get { return returnDayOfWeek; }
            set { returnDayOfWeek = value; }
        }

        /// <summary>
        /// Public property to get/set WalkingSpeed and to serialise/de-serialise the input from the file
        /// </summary>
        public int WalkingSpeed
        {
            get { return walkingSpeed; }
            set { walkingSpeed = value; }
        }

        /// <summary>
        /// Public property to get/set MaxWalkingTime and to serialise/de-serialise the input from the file
        /// </summary>
        public int MaxWalkingTime
        {
            get { return maxWalkingTime; }
            set { maxWalkingTime = value; }
        }

        /// <summary>
        /// Public property to get/set Interchange Speed and to serialise/de-serialise the input from the file
        /// </summary>
        public int InterchangeSpeed
        {
            get { return interchangeSpeed; }
            set { interchangeSpeed = value; }
        }

        /// <summary>
        /// Public property to get/set Sequence and to serialise/de-serialise the input from the file
        /// </summary>
        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

        private bool activePoll;
        /// <summary>
        /// If true injection Injects a poll to the TransactionWebService
        /// when Offset and Interval dictate that no actual injection should
        /// take place.
        /// </summary>
        public bool ActivePoll
        {
            get { return activePoll; }
            set { activePoll = value; }
        }
		#endregion

		/// <summary>
		/// Submits an EES Request Info Reference Transaction.
		/// </summary>
		public override void ExecuteTransaction()
		{
            if ((DateTime.Now.Minute - offset) % frequency == 0)
            {
                try
                {
                    string resultData = String.Empty;
                    RequestEESInfoParams requestForEESInfo = new RequestEESInfoParams();

                    requestForEESInfo.origin = this.origin;
                    requestForEESInfo.destination = this.destination;
                    requestForEESInfo.originAlternative = this.originAlternative;
                    requestForEESInfo.destinationAlternative = this.destinationAlternative;

                    requestForEESInfo.serviceNumber = this.serviceNumber;
                    requestForEESInfo.showDepartures = this.showDepartures;
                    requestForEESInfo.showCallingPoints = this.showCallingPoints;
                    requestForEESInfo.method = this.method;
                    requestForEESInfo.searchType = this.searchType;
                    requestForEESInfo.day = this.day;
                    requestForEESInfo.outwardTime = TDTransaction.CalculateActualJourneyTime(new TransportDirect.Common.TDDateTime(), this.outwardDayOfWeek, this.outwardTime);
                    requestForEESInfo.returnTime = TDTransaction.CalculateActualJourneyTime(new TransportDirect.Common.TDDateTime(), this.returnDayOfWeek, this.returnTime);
                    requestForEESInfo.maxWalkingTime = this.maxWalkingTime;
                    requestForEESInfo.walkingSpeed = this.walkingSpeed;
                    requestForEESInfo.sequence = this.sequence;
                                                      
                    bool success = false;

                    Webservice.Timeout = (int)this.Timeout.TotalMilliseconds;

                    // Log the start of the call.
                    LogStart();

                    try
                    {
                        success = Webservice.RequestEESInfo(requestForEESInfo, ref resultData);
                    }
                    catch (WebException we)
                    {
                        success = false;
                        resultData = string.Format(Messages.Injector_TransactionTimedOut, we.Message);
                    }

                    if (success)
                        LogEnd(resultData);
                    else
                        LogBadEnd(resultData);
                }
                catch (Exception exception)
                {
                    LogBadEnd(String.Format(Messages.Service_EESRequestException, exception.Message));
                }
            }
            else
            {
                // activePoll can be set to true in the request XML
                // The purpose of this is to create traffic between the TI and TWS to stop
                // connections dropping out through inactivity.
                try
                {
                    if (activePoll == true)
                    {
                        Webservice.TestActive();
                    }
                    LogSkipped();
                }
                catch (Exception)
                {
                    // We are only making this call to keep open the connection
                    // so aren't actually interested in any exceptions thrown
                }
            }
		}
	}
}
