// *********************************************** 
// NAME                 : LocationType.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner LocationType Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/LocationType.cs-arc  $
//
//   Rev 1.1   Aug 04 2009 13:43:56   mmodi
//Exposed NaPTAN as a LocationType
//Resolution for 5308: CCN520 NaPTANs in Journey Planner Synchronous web service
//
//   Rev 1.0   Nov 08 2007 12:22:28   mturner
//Initial revision.
//
//   Rev 1.2   Feb 09 2006 11:26:52   mdambrine
//Removed the other possibility for locationtype only postcode
//
//   Rev 1.1   Jan 25 2006 16:19:12   mdambrine
//add serializable attribute
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:04:18   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

namespace TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1
{
	[System.Serializable]
	public enum LocationType
	{
		//These elements have been commented out because they will be added later
		Coordinate,
//		Locality,
		Postcode,
        NaPTAN
	}
}