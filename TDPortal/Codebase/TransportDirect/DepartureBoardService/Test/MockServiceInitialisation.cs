// *********************************************** 
// NAME                 : MockServiceInitialisation
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 10/01/2005
// DESCRIPTION  : Mock class needed by unit tests. Service initialisation.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/MockServiceInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 17:17:50   passuied
//Initial revision.
//
//   Rev 1.3   Jan 19 2005 16:28:40   schand
//integration of RTTI + SE manager!
//
//   Rev 1.2   Jan 18 2005 17:36:18   passuied
//changed after update of CjpInterface
//
//   Rev 1.1   Jan 14 2005 20:59:32   passuied
//back up of unit test. under construction
//
//   Rev 1.0   Jan 10 2005 16:37:06   passuied
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.UserPortal.AdditionalDataModule;


namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Mock class needed by unit tests. Service initialisation.
	/// </summary>
	public class MockServiceInitialisation : IServiceInitialisation
	{
		public MockServiceInitialisation()
		{
		}

		public void Populate (Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new MockProperties());
			serviceCache.Add(ServiceDiscoveryKey.CodeGazetteer, new MockCrsCodeGazetteerValid()); 
			serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventMockManager());
			
		}


	}
}
