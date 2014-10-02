// *********************************************** 
// NAME             : DBWSLocation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Nov 2013
// DESCRIPTION  	: DBWSLocation class for a location used in a departure board service
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSLocation class for a location used in a departure board service
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(DBWSLocationCallingPoint))] 
    public class DBWSLocation
    {
        #region Private variables

		private string locationName = string.Empty;
		private string locationNameVia = string.Empty;
        private string locationCRS = string.Empty;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSLocation()
		{
		}

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Location Name
        /// </summary>
        public string LocationName
		{
            get { return locationName; }
            set { locationName = value; }
		}

        /// <summary>
        /// Read/Write. Optional location via text that should be displayed after the location name, 
        /// to indicate further information about an ambiguous service route
        /// </summary>
        public string LocationNameVia
		{
            get { return locationNameVia; }
            set { locationNameVia = value; }
        }

        /// <summary>
        /// Read/Write. Location CRS code
        /// </summary>
        public string LocationCRS
        {
            get { return locationCRS; }
            set { locationCRS = value; }
        }

        #endregion
    }
}