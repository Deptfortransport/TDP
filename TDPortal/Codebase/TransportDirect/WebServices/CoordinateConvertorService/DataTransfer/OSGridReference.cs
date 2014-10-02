// *********************************************** 
// NAME                 : OSGridReference.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: Class to hold an OSGR object. 
//                      : This class is eturned by the web service to the caller
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/CoordinateConvertorService/DataTransfer/OSGridReference.cs-arc  $
//
//   Rev 1.0   Jun 03 2009 11:34:16   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

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
    /// Represents a specific OSGridReference
    /// </summary>
    [Serializable]
    public class OSGridReference
    {
        // Private members
        private int easting;
        private int northing;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OSGridReference()
        {
        }

        /// <summary>
        /// Read/Write Property. Easting
        /// </summary>
        public int Easting
        {
            get { return easting; }
            set { easting = value; }
        }

        /// <summary>
        /// Read/Write Property. Northing
        /// </summary>
        public int Northing
        {
            get { return northing; }
            set { northing = value; }
        }
    }
}
