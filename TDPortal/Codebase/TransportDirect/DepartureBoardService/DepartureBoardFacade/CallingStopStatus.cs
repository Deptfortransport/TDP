// *********************************************** 
// NAME                 : CallingStopStatus.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 11/01/2005
// DESCRIPTION  : Enumeration specifying if a stopEvents has calling stops, or not or if the information 
// is unknown at this stage
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/CallingStopStatus.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:22   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:30   passuied
//Initial revision.
//
//   Rev 1.0   Jan 11 2005 16:31:58   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Enumeration specifying if a stopEvents has calling stops, or not or if the information 
	/// is unknown at this stage
	/// </summary>
	public enum CallingStopStatus
	{
		Unknown,
		HasCallingStops,
		NoCallingStops
	}
}
