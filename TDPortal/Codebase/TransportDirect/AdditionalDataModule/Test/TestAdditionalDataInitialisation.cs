// *********************************************** 
// NAME			: TestAdditionalDataInitialisation.cs
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the TestAdditionalDataInitialisation class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestAdditionalDataInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:17:42   mturner
//Initial revision.
//
//   Rev 1.6   Feb 02 2006 13:55:20   mtillett
//Add TestMockCache to serviceCache - to fix unit tests
//
//   Rev 1.5   Mar 31 2005 09:24:26   rscott
//Changes from code review IR1935
//
//   Rev 1.4   Mar 31 2005 08:44:22   rscott
//PVCS information added
using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Initialisation class to be included in the AdditionalDataModule test harnesses
	/// </summary>
	public class TestAdditionalDataInitialisation : IServiceInitialisation
	{
		public TestAdditionalDataInitialisation()
		{

		}

		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService	
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			//Toggle commenting out between Stub and factory when testing against the database
			serviceCache.Add (ServiceDiscoveryKey.AdditionalData, new TestStubAdditionalData());
			//serviceCache.Add (ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory());
			
		}
	}

	/// <summary>
	/// Initialisation class to be included in the AdditionalDataModule test harnesses
	/// when testing against the database.
	/// </summary>
	public class TestAdditionalDataInitialisationAgainstDB : IServiceInitialisation
	{
		public TestAdditionalDataInitialisationAgainstDB()
		{

		}

		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService	
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			//Toggle commenting out between Stub and factory when testing against the database
			serviceCache.Add (ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory());

			serviceCache.Add (ServiceDiscoveryKey.Cache, new TestMockCache());

		}
	}
}

