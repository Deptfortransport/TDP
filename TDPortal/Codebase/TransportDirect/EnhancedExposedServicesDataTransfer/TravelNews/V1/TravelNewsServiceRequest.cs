// *********************************************** 
// NAME                 : TravelNewsServiceRequest.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO class implementation to take input parameters for consuming travel news service. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TravelNews/V1/TravelNewsServiceRequest.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:52   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 19:38:44   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
{
	/// <summary>
	///	 DTO class implementation to take input parameters for consuming travel news service. 
	/// </summary>
	[System.Serializable]
	public class TravelNewsServiceRequest
	{
		#region Private members        		
		private TravelNewsServiceTransportType transportType;
		private TravelNewsServiceDelayType delayType;        
		private string region;
		#endregion

		#region Constructor
		public TravelNewsServiceRequest()
		{
			region = string.Empty; 
		}
		#endregion


		#region Public properties
		/// <summary>
		/// Read-Write property for Transport Type
		/// </summary>		
		public TravelNewsServiceTransportType TransportType
		{
			get {return transportType;}
			set {transportType=value;}
		}

		/// <summary>
		/// Read-Write property for DelayType
		/// </summary>
		public TravelNewsServiceDelayType DelayType
		{
			get {return delayType;}
			set {delayType=value;}
		}

		/// <summary>
		/// Read-Write property for Region
		/// </summary>
		public string Region
		{
			get {return region;}
			set {region=value;}
		}

		#endregion

	}// END CLASS DEFINITION TravelNewsServiceRequest

} // TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
