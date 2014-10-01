// *********************************************** 
// NAME                 : CodeServiceModeType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO implementation of TDModeType enum. For Information refer to TDModeType.
// ************************************************ 
// $Log

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
{	
	/// <summary>
	/// DTO implementation of TDModeType enum. For Information refer to TDModeType.
	/// </summary>
	[System.Serializable]
	public enum CodeServiceModeType
	{
		Undefined,
		Rail,
		Bus,
		Coach,
		Air,
		Ferry,
		Metro
	}
}
