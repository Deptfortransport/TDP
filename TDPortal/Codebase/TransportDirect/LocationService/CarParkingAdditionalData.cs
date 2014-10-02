// *********************************************** 
// NAME			: CarParkingAdditionalData.cs
// AUTHOR		: PETER SHELDRAKE
// DATE CREATED	: 09/01/2008 
// DESCRIPTION	: Park and Ride Additional Data 
//				  class for car parking.
// ************************************************ 
//  Rev DevFactory Feb 06 2008 22:17:00 apatel
// CCN 0426 Change type of the openingtime and closingtime of string
//
//DEVFACTORY   Nov 08 2007 12:25:02   mturner
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Summary description for CarParkingAddtionalData.
    /// </summary>
    [Serializable()]
    public class CarParkingAdditionalData
    {
        private readonly int totalSpaces = 0;
        private readonly int totalDisabledSpaces = 0;
        private readonly bool isSecure = false;
        private readonly string openingTime;
        private readonly string closingTime;
        private readonly int maxHeight;
        private readonly int maxWidth;
        private readonly bool isAdvancedReservationsAvailable;
        private readonly string carParkTypeDescription;

        /// <summary>
        /// default ctor
        /// </summary>
        public CarParkingAdditionalData()
        {

        }
        /// <summary>
        /// overloaded contructor for the population if the fields
        /// </summary>
        /// <param name="totalSpaces"></param>
        /// <param name="totalDisabledSpaces"></param>
        /// <param name="isSecure"></param>
        /// <param name="openingTime"></param>
        /// <param name="closingTime"></param>
        /// <param name="maximumHeight"></param>
        /// <param name="maximumWidth"></param>
        /// <param name="isAdvancedReservationsAvailable"></param>
        /// <param name="advancedReservationsURL"></param>
        public CarParkingAdditionalData(int totalSpaces, int totalDisabledSpaces, bool isSecure, 
                                        string openingTime, string closingTime, int maximumHeight, int maximumWidth,
                                        bool isAdvancedReservationsAvailable, string carParkTypeDescription)
        {
            this.totalSpaces = totalSpaces;
            this.totalDisabledSpaces = totalDisabledSpaces;
            this.isSecure = isSecure;
            this.openingTime = openingTime;
            this.closingTime = closingTime;

            this.maxWidth = maximumWidth;
            this.maxHeight = maximumHeight;

            this.isAdvancedReservationsAvailable = isAdvancedReservationsAvailable;
            this.carParkTypeDescription = carParkTypeDescription;
        }

        #region Properties

        public int TotalSpaces
        {
            get
            {
                return this.totalSpaces;
            }
        }

        public int TotalDisabledSpaces
        {
            get
            {
                return this.totalDisabledSpaces;
            }
        }

        public bool IsSecure
        {
            get
            {
                return this.isSecure;
            }
        }

        public string OpeningTime
        {
            get
            {
                return this.openingTime;
            }
        }

        public string ClosingTime
        {
            get
            {
                return this.closingTime;
            }
        }

        public int MaxHeight
        {
            get
            {
                return this.maxHeight;
            }
        }

        public int MaxWidth
        {
            get
            {
                return this.maxWidth;
            }
        }

        public bool IsAdvancedReservationsAvailable
        {
            get
            {
                return this.isAdvancedReservationsAvailable;
            }
        }

        public string CarParkTypeDescription
        {
            get
            {
                return this.carParkTypeDescription;
            }
        }
        #endregion
    }
}
