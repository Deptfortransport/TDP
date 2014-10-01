// *********************************************** 
// NAME                 : TravelNewsServiceDelayType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO enum implementation of delayType. For Information refer to DelayType.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TravelNews/V1/TravelNewsServiceDelayType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:52   mturner
//Initial revision.
//
//   Rev 1.1   Jan 20 2006 19:38:42   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
{
	/// <summary>
	///  DTO enum implementation of delayType. For Information refer to DelayType.
	/// </summary>
	[System.Serializable]
	public enum TravelNewsServiceDelayType
	{     
		All,
		Major,
		Recent

	}// END enum DEFINITION TravelNewsServiceDelayType

} // TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
