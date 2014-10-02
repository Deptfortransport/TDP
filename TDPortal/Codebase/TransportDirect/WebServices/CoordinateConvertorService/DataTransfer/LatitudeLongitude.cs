// *********************************************** 
// NAME                 : LatitudeLongitude.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: Class to hold a Latitude Longitude object. 
//                      : This class is eturned by the web service to the caller
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/CoordinateConvertorService/DataTransfer/LatitudeLongitude.cs-arc  $
//
//   Rev 1.0   Jun 03 2009 11:34:16   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TransportDirect.WebService.CoordinateConvertorService
{
    /// <summary>
    /// Represents an Latitude Longitude grid reference
    /// </summary>
    [Serializable]
    public class LatitudeLongitude
    {
        // Private members
        private double latitude;   
		private double longitude;

        /// <summary>
        /// Constructor
        /// </summary>
        public LatitudeLongitude()
		{
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
