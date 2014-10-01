// *********************************************** 
// NAME                 : TestInitialisation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006
// DESCRIPTION  		: Mock ServiceInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Test/TestInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:50   mturner
//Initial revision.
//
//   Rev 1.2   Jan 30 2006 15:33:50   schand
//Removed the initialisation of properties services and crypto
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 17 2006 17:42:56   schand
//Using AdditionalModuleStub instead of real data
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 17 2006 14:18:28   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using NUnit.Framework; 
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.AdditionalDataModule;    

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Test
{
	/// <summary>
	/// ServiceInitialisation class
	/// </summary>
	public class TestInitialisation: IServiceInitialisation  
	{
		
        		

		/// <summary>
		/// Implementation of Populate method for unit testing
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{   		
            //Add additional data module
			serviceCache.Add( ServiceDiscoveryKey.AdditionalData ,  new TestStubAdditionalData());		

		}
	}
}
