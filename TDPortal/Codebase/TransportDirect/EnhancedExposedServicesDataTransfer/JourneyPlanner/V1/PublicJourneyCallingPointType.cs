// *********************************************** 
// NAME                 : PublicJourneyCallingPointType.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner PublicJourneyCallingPointType Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/PublicJourneyCallingPointType.cs-arc  
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public enum PublicJourneyCallingPointType
	{
		Origin,
		Destination,
		Board,
		Alight,
		CallingPoint,
		PassingPoint,
		OriginAndBoard,
		DestinationAndAlight
	}
}