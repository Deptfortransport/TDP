// *********************************************** 
// NAME             : TDPVenueRiverService.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 27 Apr 2011
// DESCRIPTION  	: Represents venue river services
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// Represents venue river services
    /// </summary>
    [Serializable]
    public class TDPVenueRiverService
    {
        #region Private Fields
        private string venueNaPTAN;
        private string venuePierNaPTAN;
        private string remotePierNaPTAN;
        private string venuePierName;
        private string remotePierName;
        #endregion

        #region Public Properties
        /// <summary>
        /// NaPTAN of the Venue location
        /// </summary>
        public string VenueNaPTAN
        {
            get { return venueNaPTAN; }
            set { venueNaPTAN = value; }
        }

        /// <summary>
        /// The NaPTAN of the river service pier that serves the venue.
        /// </summary>
        public string VenuePierNaPTAN
        {
            get { return venuePierNaPTAN; }
            set { venuePierNaPTAN = value; }
        }

        /// <summary>
        /// The NaPTAN of the remote pier of the river service
        /// </summary>
        public string RemotePierNaPTAN
        {
            get { return remotePierNaPTAN; }
            set { remotePierNaPTAN = value; }
        }

        /// <summary>
        /// The display name of the venue pier.
        /// </summary>
        public string VenuePierName
        {
            get { return venuePierName; }
            set { venuePierName = value; }
        }

        /// <summary>
        /// The display name of the remote pier.
        /// </summary>
        public string RemotePierName
        {
            get { return remotePierName; }
            set { remotePierName = value; }
        }

        #endregion
    }
}
