using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common;

namespace TDP.UserPortal.Retail
{
    /// <summary>
    /// Defines and holds all data associated with a service operator (eg Virgin Trains, Arriva buses, etc).
    /// More precisely it is an operation of the operator since it is defined by operator *and* travel mode.
    /// </summary>
    [Serializable]
    public class ServiceOperator
    {
        #region Private Members

        private TDPModeType travelMode;
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
        { }


        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceOperator(TDPModeType travelMode, string code, string name, string url)
            : this(travelMode, code, string.Empty, name, string.Empty, DateTime.MinValue, DateTime.MaxValue, url, false, false, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceOperator(string code, string name, string url)
            : this(TDPModeType.Air, code, string.Empty, name, string.Empty, DateTime.MinValue, DateTime.MaxValue, url, false, false, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceOperator(TDPModeType travelMode, string code, string serviceNumber,
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
        public TDPModeType TravelMode
        {
            get { return travelMode; }
            set { travelMode = value; }
        }

        /// <summary>
        /// Read/Write. 
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
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
            get { return name; }
            set { name = value; }
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
            get { return url; }
            set { url = value; }
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
