// *********************************************** 
// NAME                 : RTTIErrorResponse.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: This class contains list of possible RTTI errors 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTIErrorResponse.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:40   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.1   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	///  This class contains list of possible RTTI errors
	/// </summary>
	public class RTTIErrorResponse
	{
		public RTTIErrorResponse()
		{}

		public const string InvalidRequest = "INVALID_REQUEST";
		public const string NoDataFound = "NO_DATA";
		public const string SchemaFailed = "FAILED_SCHEMA_VALIDATION";
		public const string Unavailable = "RTTI_UNAVAILABLE";

	}
}
