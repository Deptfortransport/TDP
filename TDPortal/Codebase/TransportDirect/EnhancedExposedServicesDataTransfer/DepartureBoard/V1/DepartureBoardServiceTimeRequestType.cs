// *********************************************** 
// NAME                 : DepartureBoardServiceTimeRequestType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Time Request Type Enumerator
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceTimeRequestType.cs-arc  $ 
//
//   Rev 1.1   Mar 12 2008 12:04:20   build
//Manually resolve merge probelms with stream 4542 merge.
//
//   Rev 1.0.1.0   Feb 18 2008 16:17:52   pscott
//IR 5497 - Add new functioinality for FirstToday,FirstTomorrow,LastToday and Last Tomorrow
//
//   Rev 1.0   Nov 08 2007 12:22:24   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:52   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:40   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:40   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements


using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Service Time Request Type Enumerator
	/// </summary>
	[System.Serializable]
	public enum DepartureBoardServiceTimeRequestType
	{
		Now,
		Last,
		First,
		TimeToday,
		TimeTomorrow,
        LastToday,
        LastTomorrow,
        FirstToday,
        FirstTomorrow
          
	}
}
