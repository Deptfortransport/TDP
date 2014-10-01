// *********************************************** 
// NAME                 : DBSMessageIdentifier.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 12/01/2005
// DESCRIPTION  : Unique id for message codes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSMessageIdentifier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:24   mturner
//Initial revision.
//
//   Rev 1.1   Mar 31 2005 19:12:46   schand
//Now added failure code for origin/destination/both . Fix for 4.4, 4.5
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.1   Jan 17 2005 14:47:38   passuied
//changes in the interface
//
//   Rev 1.0   Jan 12 2005 14:49:00   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Unique id for message codes
	/// </summary>
	public enum  DBSMessageIdentifier : int
	{
		// Message codes for General Dep board service
		UserInvalidRequestTime = 1,
		UserInvalidRequestLocationForOrigin = 2,
		UserInvalidRequestLocationForDestination = 3,
		UserInvalidRequestLocationForOriginAndDestination = 4,
		UserInvalidRequestLocationForInconsistentCodes = 5,


		// Message codes for StopEventManager (start = 100)
		CJPReturnedMessages = 100,
		CJPTimeout = 101,
		CJPCallException = 102,
		CJPAccessNullResult = 103

		// Message codes for RTTI manageer (start = 200)

	}
}
