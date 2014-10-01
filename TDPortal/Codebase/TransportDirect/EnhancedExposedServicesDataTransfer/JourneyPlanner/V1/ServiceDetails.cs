// *********************************************** 
// NAME                 : ServiceDetails.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner ServiceDetails Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/ServiceDetails.cs-arc 
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public class ServiceDetails
	{
		private string opCode;
		private string opName;
		private string servNumber;
		private string destBoard;

		/// <summary>
		/// constructor
		/// </summary>
		public ServiceDetails()
		{
			
		}

		/// <summary>
		/// The operator code of the operator that provides the service e.g. NX
		/// </summary>
		public string OperatorCode
		{
			get
			{
				return opCode;

			}
			set
			{
				opCode = value;
			}
		}

		/// <summary>
		/// The display name of the operator that provides the service.
		/// </summary>
		public string OperatorName
		{
			get
			{
				return opName;

			}
			set
			{
				opName = value;
			}
		}

		/// <summary>
		/// The service number as shown on the front of the vehicle.
		/// </summary>
		public string ServiceNumber
		{
			get
			{
				return servNumber;

			}
			set
			{
				servNumber = value;
			}
		}

		/// <summary>
		/// The destination as shown on the front of the vehicle.
		/// </summary>
		public string DestinationBoard
		{
			get
			{
				return destBoard;

			}
			set
			{
				destBoard = value;
			}
		}
	}// END CLASS DEFINITION ServiceDetails

} // TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
