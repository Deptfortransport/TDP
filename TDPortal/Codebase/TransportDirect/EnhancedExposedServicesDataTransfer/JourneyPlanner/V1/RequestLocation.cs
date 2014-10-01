// *********************************************** 
// NAME                 : RequestLocation.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner RequestLocation Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/RequestLocation.cs-arc  $
//
//   Rev 1.2   Aug 20 2009 09:40:10   mmodi
//Corrected log header
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
	[System.Serializable]
	public class RequestLocation
	{
        private LocationType locationType;
        private string postcode;
		private OSGridReference osGridReference;
		private Naptan[] naptans;
		

		/// <summary>
		/// constructor
		/// </summary>
		public RequestLocation()
		{
			
		}
        
		/// <summary>
		/// Specifies the format of the location.
		/// </summary>
		public LocationType Type
		{
			get
			{
				return locationType;

			}
			set
			{
				locationType = value;
			}
		}

		/// <summary>
		/// A valid UK postcode. Must be specified if LocationType is Postcode. Whitespace is ignored.
		/// </summary>
		public string Postcode
		{
			get
			{
				return postcode;

			}
			set
			{
				postcode = value;
			}
		}

        /// <summary>
        /// Ordnance survey grid reference (easting/northing). Must be specified if 
        /// LocationType is Coordinate or Locality. Currently unsupported.
        /// </summary>
        public OSGridReference GridReference
        {
            get
            {
                return osGridReference;

            }
            set
            {
                osGridReference = value;
            }
        }

        /// <summary>
        /// Nearest NaPTANs to location. Must be specified if LocationType is NaPTAN. Currently unsupported.
        /// </summary>
        public Naptan[] NaPTANs
        {
            get
            {
                return naptans;

            }
            set
            {
                naptans = value;
            }
        }
	}

} 
