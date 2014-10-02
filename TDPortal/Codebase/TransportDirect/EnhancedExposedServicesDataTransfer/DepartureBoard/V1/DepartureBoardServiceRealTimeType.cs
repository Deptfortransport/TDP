// *********************************************** 
// NAME                 : DepartureBoardServiceRealTimeType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Enumeration for Departure Board Service RealTime type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceRealTimeType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:22   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:50   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:36   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:36   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Enumeration for Departure Board Service RealTime type
	/// </summary>
	[System.Serializable]
	public enum DepartureBoardServiceRealTimeType
	{
		Recorded,
		Estimated
	}
}