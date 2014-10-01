// *********************************************** 
// NAME                 : DBSActivityType.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 29/12/2004
// DESCRIPTION  : Enumeration for a stop Activity Type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSActivityType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:22   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.2   Jan 10 2005 14:42:40   passuied
//real addition 
//
//   Rev 1.1   Jan 10 2005 14:32:46   passuied
//added new type in enum
//
//   Rev 1.0   Dec 30 2004 14:23:32   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Enumeration for a stop Activity Type
	/// </summary>
	public enum DBSActivityType
	{
		Arrive,
		Depart,
		ArriveDepart,
		Unavailable
	}
}
