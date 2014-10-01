// *********************************************** 
// NAME                 : DBSValidationType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/03/2005 
// DESCRIPTION  		: Enum Validation Type 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSValidationType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:28   mturner
//Initial revision.
//
//   Rev 1.0   Mar 31 2005 19:16:16   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Enumeration used to specify which Validation type is perfomed.
	/// </summary>
	public enum DBSValidationType
	{
		Origin,
		Destination,
		Both,
		Inconsistent,
		Undefined

	}
}
