// *********************************************** 
// NAME                 : VehicleType.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner VehicleType Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/VehicleType.cs-arc 
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public enum VehicleType
	{
		Bicycle,
		Car,
		CarAndTrailer,
		HeavyGoodsVehicle,
		Motorcycle,
		VanCoach,
		Walk
	}
}
