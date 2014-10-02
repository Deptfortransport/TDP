// *********************************************** 
// NAME                 : EnhancedExposedServicesKey.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 23/11/2005 
// DESCRIPTION  		: Definition of all keys used withing the  EnhancedExposedServices.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Helpers/EnhancedExposedServicesKey.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:00   mturner
//Initial revision.
//
//   Rev 1.5   Jan 11 2006 09:17:12   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Jan 05 2006 14:54:04   mtillett
//Fix up SoapException creation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 04 2006 12:37:48   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Dec 22 2005 11:24:30   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Dec 14 2005 15:58:46   schand
//Added more keys
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Nov 25 2005 18:46:38   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.Helpers
{
	/// <summary>
	/// Definition of all keys used withing the  EnhancedExposedServices.
	/// </summary>
	public sealed class EnhancedExposedServicesKey
	{   		
		// .NET file trace listener path
		public const string DefaultLogPath		= "DefaultLogPath";
  	}
}
