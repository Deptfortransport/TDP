// *********************************************** 
// NAME                 : DBSRangeType.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 13/01/2005
// DESCRIPTION  : Enumeration used to specify which range type should be used in a request.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSRangeType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:24   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.0   Jan 14 2005 10:17:06   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Enumeration used to specify which range type should be used in a request.
	/// </summary>
	public enum DBSRangeType
	{
		Sequence,
		Interval
	}
}
