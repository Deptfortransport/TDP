// *********************************************** 
// NAME			: ItineraryType.cs
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Enum of available Itinerary Types
// ************************************************ 
using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Enum of available Itinerary Types
	/// </summary>
    [Serializable()]
    public enum ItineraryType 
	{
		/// <summary>
		/// Indicates that the outward and inward journeys have been priced separately.
		/// Both single and return fares may be available for both outward and inward
		/// journeys, but the return fares do not cover both parts of the journey.
		/// </summary>
		Single, 
		
		/// <summary>
		/// Indicates that the outward and inward journeys could be priced together. There
		/// will be single fares for both outward and inward journeys, as well as return fares
		/// for the outward journey which also cover the inward journey.
		/// </summary>
		Return
	}

}
