// *********************************************** 
// NAME			        : LatitudeLongitude.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION	        : LatitudeLongitude class to hold a TD representation of the LatitudeLongitude coordinate
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LatitudeLongitude.cs-arc  $
//
//   Rev 1.0   Jun 03 2009 11:18:24   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// LatitudeLongitude class to hold a TD representation of the LatitudeLongitude coordinate
    /// </summary>
    [Serializable()]
    public class LatitudeLongitude
    {
        // Private members
        private double latitude;   
		private double longitude;

        /// <summary>
		/// Default constructor
		/// </summary>
        public LatitudeLongitude()
		{
            this.latitude = 0;
            this.longitude = 0;
		}

        /// <summary>
		/// Alternate constructor
		/// </summary>
		/// <param name="easting">latitude value</param>
		/// <param name="northing">longitude value</param>
		public LatitudeLongitude( double latitude, double longitude )
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
		}

		/// <summary>
        /// Read/Write Property. Latitude
		/// </summary>
        public double Latitude
		{
            get { return latitude; }
            set { latitude = value; }
		}
		
		/// <summary>
        /// Read/Write Property. Longitude
		/// </summary>
        public double Longitude
		{
            get { return longitude; }
            set { longitude = value; }
		}
    }
}
