// *********************************************** 
// NAME                 : AttributeName.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: This enum contains list of RTTI attributes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/AttributeName.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:34   mturner
//Initial revision.
//
//   Rev 1.1   Mar 01 2005 18:55:50   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.0   Feb 28 2005 16:23:04   passuied
//Initial revision.
//
//   Rev 1.1   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for AttributeName.
	/// </summary>
	public enum AttributeName
	{
		PublicTimeOfArrival=0,
		EstimatedTimeOfArrival = 1,
		ActualTimeOfArrival = 2,
		PublicTimeOfDeparture=3,
		EstimatedTimeOfDeparture = 4,
		ActualTimeOfDeparture = 5,
		ServiceNumber=6,
		OperatorCode = 7,
		StationAttributeByCRS = 8,
		Cancelled = 9,
		CancellationCode= 10,
		CircularRoute = 11,
		Via = 12, 
		TrainDepartureDelayed = 13,
		TrainArrivalDelayed = 14,
		OverDueAtArrival =15,
		OverDueAtDeparture = 16,
		RTTIErrorCode = 17,
		RTTIErrorMessage =18,
		RTTIFullTiploc =19,
		LateRunningCode = 20,
		FalseDestination = 21,
		MessageId = 22
		
		

	}
}
