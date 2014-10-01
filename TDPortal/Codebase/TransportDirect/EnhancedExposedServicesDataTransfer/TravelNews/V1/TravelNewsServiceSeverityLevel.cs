// *********************************************** 
// NAME                 : TravelNewsServiceSeverityLevel.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO implementation of SeverityLevel enum. For Information refer to SeverityLevel.
// ************************************************ 
// $Log

namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
{	
	/// <summary>
	/// DTO implementation of SeverityLevel enum. For Information refer to SeverityLevel.
	/// </summary>
	[System.Serializable]
	public enum TravelNewsServiceSeverityLevel
	{
		Critical,
		Serious,
		VerySevere,
		Severe,
		Medium,
		Slight,
		VerySlight
	}
}
