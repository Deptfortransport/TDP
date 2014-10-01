// *********************************************** 
// NAME                 : ResponseLocation.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold details of the location for a CarJourneyRequest
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/ResponseLocation.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:18   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details of the location for a CarJourneyRequest
    /// </summary>
    [System.Serializable]
    public class ResponseLocation
    {
        private string description;
        private LocationType locationType;
        private OSGridReference osGridReference;
        private Naptan[] naptans;
        private string postcode;

        /// <summary>
        /// Constructor
        /// </summary>
        public ResponseLocation()
        {

        }

        /// <summary>
        /// Specifies the name description of the location.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Specifies the format of the location.
        /// </summary>
        public LocationType Type
        {
            get { return locationType; }
            set { locationType = value; }
        }

        /// <summary>
        /// Ordnance survey grid reference (easting/northing). 
        /// Must be specified if LocationType is Coordinate.
        /// </summary>
        public OSGridReference GridReference
        {
            get { return osGridReference; }
            set { osGridReference = value; }
        }

        /// <summary>
        /// Nearest NaPTANs to location. 
        /// Must be specified if LocationType is NaPTAN. 
        /// Currently unsupported.
        /// </summary>
        public Naptan[] NaPTANs
        {
            get { return naptans; }
            set { naptans = value; }
        }

        /// <summary>
        /// A valid UK postcode. 
        /// Must be specified if LocationType is Postcode. Whitespace is ignored. 
        /// Currently unsupported.
        /// </summary>
        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }
    }
}
