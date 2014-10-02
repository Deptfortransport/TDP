// *********************************************** 
// NAME                 : TestInitialisation.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: Mock ServiceInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestInitialisation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:24   mturner
//Initial revision.
//
//   Rev 1.2   Jan 12 2006 19:54:12   schand
//Moved Usernamepassword const to this class
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 05 2006 14:56:30   mtillett
//Update test classes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 05 2006 14:01:40   mtillett
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// ServiceInitialisation class
	/// </summary>
	public class TestInitialisation: IServiceInitialisation  
	{
		/// <summary>
		/// Blank contructor 
		/// </summary>
		public TestInitialisation()
		{
		}
		public const string UsernamePassword = "EnhancedExposedWebServiceTest";

		/// <summary>
		/// Implementation of Populate method for unit testing
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService					
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());	
			
			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );		
		}
	}
}
