// *********************************************** 
// NAME                 : RTTIErrorType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: This enum contains list of RTTI error types
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTIErrorType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:40   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.0   Jan 21 2005 14:21:40   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// This enum contains list of RTTI error types.
	/// </summary>

	public enum RTTIErrorType
	{
		NoResponseFound = 0,
		CommunicationError = 1,
		XmlResponseNotValid =2,
		UnableToExtract =3,
		UnableToTranslate =4
	}
}
