// *********************************************** 
// NAME                 : PublicJourneyResult.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner PublicJourneyResult Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/CodeBase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/PublicJourneyResult.cs-arc  
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

	[System.Serializable]
	public class PublicJourneyResult
	{

		private PublicJourney[] outwardPublicJourney;
		private PublicJourney[] returnPublicJourney;
		private string[] warnings;

		/// <summary>
		/// constructor
		/// </summary>
		public PublicJourneyResult()
		{
			
		}

		/// <summary>
		/// Contains any warnings that the end user of the results should be aware of, 
		/// e.g. some journeys start in the past, or outward and return journey times overlap. 
		/// This text is language sensitive. This array may be populated only if journey planning was successful.
		/// </summary>
		public string[] UserWarnings
		{
			get
			{
				return warnings;

			}
			set
			{
				warnings = value;
			}
		}

		/// <summary>
		/// Journey details for outward journey. This array may be populated only if journey planning was successful.
		/// </summary>
		public PublicJourney[] OutwardPublicJourneys
		{
			get
			{
				return outwardPublicJourney;

			}
			set
			{
				outwardPublicJourney = value;
			}
		}

		/// <summary>
		/// Journey details for return journey, if requested. This array may be populated only 
		/// if journey planning was successful and return journeys were requested.
		/// </summary>
		public PublicJourney[] ReturnPublicJourneys
		{
			get
			{
				return returnPublicJourney;

			}
			set
			{
				returnPublicJourney = value;
			}
		}
	}

} 
