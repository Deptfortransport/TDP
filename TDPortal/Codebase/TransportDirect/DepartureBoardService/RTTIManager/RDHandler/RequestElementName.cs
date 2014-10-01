// *********************************************** 
// NAME                 : RequestElementName.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: This enum contains list of RTTI Element name
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RequestElementName.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:38   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.0   Jan 21 2005 14:21:38   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// This enum contains list of RTTI Element name.
	/// </summary>
	public enum RequestElementName
	{
		StationRequestByCRS= 0,
		TripRequestByCRS = 1,
		TrainRequest = 2
	}
}
