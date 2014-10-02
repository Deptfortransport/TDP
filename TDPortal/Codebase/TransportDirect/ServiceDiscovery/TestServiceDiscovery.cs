// *********************************************** 
// NAME                 :  TestServiceDiscovery.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 10/07/2003 
// DESCRIPTION  : Test class for the service discovery class. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ServiceDiscovery/TestServiceDiscovery.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:14   mturner
//Initial revision.
//
//   Rev 1.4   Feb 07 2005 08:55:50   RScott
//Assertion changed to Assert
//
//   Rev 1.3   Oct 03 2003 13:38:44   PNorell
//Updated the new exception identifier.
//
//   Rev 1.2   Jul 18 2003 16:38:06   passuied
//Test Exception handling added
//
//   Rev 1.1   Jul 17 2003 11:17:08   passuied
//Changes after code review

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;

namespace TransportDirect.Common.ServiceDiscovery.NUnit
{

	public class ServiceFactoryTest : IServiceFactory
	{
		public object Get()
		{
			return "serviceFactoryTest";

		}
	}

	public class ServiceFactoryTestReplaced : IServiceFactory
	{
		public object Get()
		{
			return "serviceFactoryTestReplaced";
		}
	}

	public class InitialisationContextTest : IServiceInitialisation
	{
		public void Populate (Hashtable serviceCache)
		{

				serviceCache.Add (ServiceDiscoveryKey.PropertyService, new ServiceFactoryTest());

			
		}
	}
	
	/// <summary>
	/// Unit tests for the TDServiceDiscovery class.
	/// </summary>
	[TestFixture]
	public class TestTDServiceDiscovery
	{
		public TestTDServiceDiscovery()
		{
		}

		[Test]
		public void TestInit()
		{
			// Call Current which must be null
			Assert.IsNull(TDServiceDiscovery.Current,"Before first Init, Current should be null");

			// Call Init and retest Current
			TDServiceDiscovery.Init(new InitialisationContextTest());
			Assert.IsNotNull(TDServiceDiscovery.Current,"After Init, Current should not be null");
			
		}

		[Test]
		public void TestIndexer()
		{
			// after the init the serviceCache table should contain 1 key (PropertyService)
			Assert.AreEqual("serviceFactoryTest",(string)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService],"The factory returned by the Indexer should be of type ServiceFactoryTest");

		}

		[Test]
		public void TestException()
		{
			bool exceptionThrown = false;
			try
			{
				exceptionThrown = true;
				string result = (string)TDServiceDiscovery.Current["foo"];
			}
			catch (TDException e)
			{
				Assert.AreEqual(TDExceptionIdentifier.SDInvalidKey, e.Identifier, "Bad Exception ID thrown");
			}

			Assert.AreEqual(true, exceptionThrown, "No exception has been thrown");
		}

		[Test]
		public void TestServiceForTest()
		{
			// Replace the ServiceFactory with a new one and check that the object returned is the object returned by the new serviceFactory
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new ServiceFactoryTestReplaced());

			Assert.AreEqual("serviceFactoryTestReplaced",
				(string)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService],
				"The factory returned by the Indexer should be of type ServiceFactoryTest");
		}
	}
}
