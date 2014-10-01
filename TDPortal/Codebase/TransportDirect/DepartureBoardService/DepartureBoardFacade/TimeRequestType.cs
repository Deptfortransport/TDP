// *********************************************** 
// NAME                 : TimeTypeRequest.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 04/01/2005
// DESCRIPTION  : Enumeration type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/TimeRequestType.cs-arc  $
//
//   Rev 1.1   Mar 12 2008 12:00:02   build
//Manually resolve merge probelms with stream 4542 merge.
//
//   Rev 1.0.1.0   Feb 18 2008 16:45:14   pscott
//IR 5497 - Add new functioinality for FirstToday,FirstTomorrow,LastToday and Last Tomorrow
//
//   Rev 1.0   Nov 08 2007 12:21:28   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.2   Feb 16 2005 14:54:10   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.1   Jan 14 2005 10:20:02   passuied
//changes in interface
//
//   Rev 1.0   Jan 05 2005 09:59:48   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Enumeration type
	/// </summary>
	public enum TimeRequestType
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
