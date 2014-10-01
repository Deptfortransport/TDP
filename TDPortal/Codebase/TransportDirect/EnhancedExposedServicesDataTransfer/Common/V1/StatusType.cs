// *********************************************** 
// NAME                 : CodeServiceRequest.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: Enum for indicating status of the request.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/StatusType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:18   mturner
//Initial revision.
//
//   Rev 1.3   Feb 22 2006 16:08:04   RWilby
//Added PVCS Log$ header tag.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
	/// <summary>
	/// Enum for indicating status of the request.
	/// </summary>
	[System.Serializable]
	public enum StatusType
	{
		Success = 0,
		Failed = 1,
		ValidationError = 2
	}
}
