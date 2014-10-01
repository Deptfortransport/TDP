// *********************************************** 
// NAME			: ServiceOperator.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 15/07/2005 
// DESCRIPTION	: Implementation of the ServiceOperator class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ServiceOperator.cs-arc  $
//
//   Rev 1.2   Dec 10 2012 12:13:18   mmodi
//Updated accessible operator to include WEF and WEU datetimes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 05 2012 14:14:26   mmodi
//Updated for accessible operators
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:23:58   mturner
//Initial revision.
//
//   Rev 1.0   Jul 25 2005 19:43:50   pcross
//Initial revision.
//
//   Rev 1.0   Jul 18 2005 16:16:38   pcross
//Initial revision.
//

using System;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Defines and holds all data associated with a service operator (eg Virgin Trains, Arriva buses, etc).
	/// More precisely it is an operation of the operator since it is defined by operator *and* travel mode.
	/// </summary>
    [Serializable]
	public class ServiceOperator
	{
		#region Private Members
		
		private ModeType travelMode;
		private string code;
        private string serviceNumber;
		private string name;
        private string region;
        private DateTime wefDate;
        private DateTime weuDate;
		private string url;
        private bool wheelchairBookingAvailable;
        private bool assistanceBookingAvailable;
        private string bookingUrl;
        private string bookingPhoneNumber;
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServiceOperator()
		{}

		
		/// <summary>
		/// Constructor
		/// </summary>
		public ServiceOperator(ModeType travelMode, string code, string name, string url)
            : this(travelMode, code, string.Empty, name, string.Empty, DateTime.MinValue, DateTime.MaxValue, url, false, false, string.Empty, string.Empty)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public ServiceOperator(string code, string name, string url)
            : this(ModeType.Air, code, string.Empty, name, string.Empty, DateTime.MinValue, DateTime.MaxValue, url, false, false, string.Empty, string.Empty)
		{
		}

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceOperator(ModeType travelMode, string code, string serviceNumber,
            string name, string region, DateTime wefDate, DateTime weuDate, string url,
            bool wheelchairBookingAvailable, bool assistanceBookingAvailable,
            string bookingUrl, string bookingPhoneNumber)
        {
            // Populate the instance variables
            this.travelMode = travelMode;
            this.code = code;
            this.serviceNumber = serviceNumber;
            this.name = name;
            this.region = region;
            this.wefDate = wefDate;
            this.weuDate = weuDate;
            this.url = url;
            this.wheelchairBookingAvailable = wheelchairBookingAvailable;
            this.assistanceBookingAvailable = assistanceBookingAvailable;
            this.bookingUrl = bookingUrl;
            this.bookingPhoneNumber = bookingPhoneNumber;
        }

		#endregion

		#region Public Properties

        /// <summary>
        /// Read/Write. 
        /// </summary>
		public ModeType TravelMode
		{
			get {return travelMode;}
			set {travelMode = value;}
		}

        /// <summary>
        /// Read/Write. 
        /// </summary>
		public string Code
		{
			get {return code;}
			set {code = value;}
		}

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public string ServiceNumber
        {
            get { return serviceNumber; }
            set { serviceNumber = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
		public string Name
		{
			get {return name;}
			set {name = value;}
		}

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public string Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public DateTime WEFDate
        {
            get { return wefDate; }
            set { wefDate = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public DateTime WEUDate
        {
            get { return weuDate; }
            set { weuDate = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
		public string Url
		{
			get {return url;}
			set {url = value;}
		}

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public bool WheelchairBookingAvailable
        {
            get { return wheelchairBookingAvailable; }
            set { wheelchairBookingAvailable = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public bool AssistanceBookingAvailable
        {
            get { return assistanceBookingAvailable; }
            set { assistanceBookingAvailable = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public string BookingUrl
        {
            get { return bookingUrl; }
            set { bookingUrl = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public string BookingPhoneNumber
        {
            get { return bookingPhoneNumber; }
            set { bookingPhoneNumber = value; }
        }

		#endregion
	}
}
