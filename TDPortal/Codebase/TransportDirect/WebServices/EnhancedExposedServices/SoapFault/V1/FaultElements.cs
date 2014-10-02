// *********************************************** 
// NAME                 : FaultElements.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 10/01/20056
// DESCRIPTION  		: Constants for Soap Fault detail element
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/SoapFault/V1/FaultElements.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:18   mturner
//Initial revision.
//
//   Rev 1.3   Jan 20 2006 12:58:02   mtillett
//Update throw soap exception methods to allow status to be defined
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Jan 20 2006 11:56:32   COwczarek
//Added ErrorStatusValidationFailed
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 11 2006 09:17:14   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 11 2006 09:02:28   mtillett
//Initial revision.
//

using System;

namespace TransportDirect.EnhancedExposedServices.SoapFault.V1
{
	/// <summary>
	/// Definition of all keys used withing the  EnhancedExposedServices.
	/// </summary>
	public sealed class FaultElements
	{   
		//Details node namespace
		public const string DetailsNamespace = "TransportDirect.EnhancedExposedServices.SoapFault.V1";
		//Top level node name
		public const string TopLevelNodeName = "Details";

		//Top level error node name
		public const string TopLevelErrorNodeName = "Messages";

		//Child level error node name
		public const string ChildLevelErrorNodeName = "Message";

		//Error code node name
		public const string ErrorCodeNodeName = "Code";

		//Child level error node name
		public const string ErrorDescriptionNodeName = "Description";

		// StatusNode name
		public const string ErrorStatusNodeName = "Status";
     
	}
	/// <summary>
	/// Error status, part of the soap fault custom detail node
	/// </summary>
	public enum ErrorStatus
	{
		Failed,
		ValidationError,
	}
}
