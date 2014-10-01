// *********************************************** 
// NAME                 : ModeType.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: JourneyPlanner ModeType Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/JourneyPlanner/V1/ModeType.cs-arc  $
//
//   Rev 1.1   Jan 23 2013 11:50:24   mmodi
//Updated to support Telecabine
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.0   Nov 08 2007 12:22:30   mturner
//Initial revision.
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
	public enum ModeType
	{
		Air,
		Bus,
		Car,
		CheckIn,
		CheckOut,
		Coach,
		Cycle,
		Drt,
		Ferry,
		Metro,
		Rail,
		RailReplacementBus,
		Taxi,
        Telecabine,
		Tram,
		Transfer,
		Underground,
		Walk
	}
}