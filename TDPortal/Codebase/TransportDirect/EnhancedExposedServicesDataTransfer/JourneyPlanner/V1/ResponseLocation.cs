// *********************************************** 
// NAME                 : ResponseLocation.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner ResponseLocation Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/ResponseLocation.cs-arc 
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
	[System.Serializable]
	public class ResponseLocation
	{

		private OSGridReference osGridReference;
		private Naptan naptan;
		private string locality;
		private string description;

		/// <summary>
		/// constructor
		/// </summary>
		public ResponseLocation()
		{
			
		}

		/// <summary>
		/// Grid reference of location.
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
		/// NaPTAN of location.
		/// </summary>
		public Naptan Naptan
		{
			get
			{
				return naptan;

			}
			set
			{
				naptan = value;
			}
		}

		/// <summary>
		/// Display name of location.
		/// </summary>
		public string Description
		{
			get
			{
				return description;

			}
			set
			{
				description = value;
			}
		}

		/// <summary>
		///	The locality of the location.
		/// </summary>
		public string Locality
		{
			get
			{
				return locality;

			}
			set
			{
				locality = value;
			}
		}
	}// END CLASS DEFINITION ResponseLocation

} // TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
