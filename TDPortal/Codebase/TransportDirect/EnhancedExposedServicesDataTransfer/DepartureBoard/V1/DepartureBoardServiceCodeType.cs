// *********************************************** 
// NAME                 : DepartureBoardServiceCallingStopStatus.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Code Type. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceCodeType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:20   mturner
//Initial revision.
//
//   Rev 1.1   May 03 2007 12:29:48   mturner
//Added new code type of NAPTAN
//
//   Rev 1.0   Jan 27 2006 16:30:46   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:18   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:32   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Service Code Type indicate code type is for Bus, Rail etc
	/// </summary>
	[System.Serializable]
	public enum DepartureBoardServiceCodeType
	{
		CRS,
		SMS,
		IATA,
		Postcode,
		NAPTAN
	}
}
