// *********************************************** 
// NAME                 : TestTTBOImportProperties.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 22/09/2004
// DESCRIPTION  : Test fixture for the TTBOImportProperties
// class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestTTBOImportProperties.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:48   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:15:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.2.0   Nov 25 2005 17:59:18   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Feb 08 2005 16:24:46   RScott
//unused variable removed
//
//   Rev 1.1   Feb 08 2005 13:25:20   bflenk
//Changed Assertion to Assert
//
//   Rev 1.0   Sep 29 2004 12:43:32   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using NUnit.Framework;

namespace TransportDirect.UserPortal.AirDataProvider
{
	#region Test Fixture

	/// <summary>
	/// Test Fixture for the TTBOImportProperties class
	/// </summary>
	[TestFixture]
	public class TestTTBOImportProperties
	{
		/* The test cases required are:
		 * - No cjpservers/ttboservers properties present
		 * - cjpservers/ttboservers but blank
		 * - cjpservers/ttboservers both present
		 * - Only cjpservers present
		 * - Only ttboservers present
		 * - Duplicate entries and additional spaces in a "servers" key
		 * - Missing filelocation for a server
		 * - Missing remote object location for a server
		 */

		public TestTTBOImportProperties()
		{ }

		[SetUp]
		public void SetUp()
		{
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		[TearDown]
		public void TearDown()
		{
			// Reinstate the real property provider else subsequent tests may not work correctly
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
		}

		/// <summary>
		/// Tests the case when the servers key isn't present. An error should be thrown
		/// </summary>
		[Test]
		public void TestNoServerProperties()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProviderBase()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			bool errorRaised = false;

			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);
			}
			catch (TDException td)
			{
				if (td.Identifier == TDExceptionIdentifier.DGInvalidConfiguration)
					errorRaised = true;
			}
			catch (Exception e)
			{
				string dummy = e.Message;
				errorRaised = false;
			}

			Assert.IsTrue(errorRaised, "The expected error was not raised when no server information was present");
		}

		/// <summary>
		/// Tests the case when the servers key is present but blank. An error should be thrown
		/// </summary>
		[Test]
		public void TestBlankProperties()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider1()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			bool errorRaised = false;

			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);
			}
			catch (TDException td)
			{
				if (td.Identifier == TDExceptionIdentifier.DGInvalidConfiguration)
					errorRaised = true;
			}
			catch (Exception e)
			{
				string dummy = e.Message;
				errorRaised = false;
			}

			Assert.IsTrue(errorRaised, "The expected error was not raised when no server information was present");
		}

		/// <summary>
		/// Tests that both CJP and TTBO server details are read in correctly
		/// </summary>
		[Test]
		public void TestBothTypesCorrect()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider2()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);

				Assert.AreEqual(5, servers.Length, "The number of servers returned was not as expected");

				// Check the servers
				string[] expectedCJPServers = new string[] { "cjp1", "cjp2", "cjp3" };
				string[] expectedTTBOServers = new string[] { "ttbo1", "ttbo2" };

				for (int index = 0; index < expectedCJPServers.Length; index++)
				{
					int serverPos = FindServerIndex(servers, expectedCJPServers[index]);
					Assert.IsTrue(serverPos != -1, "Server " + expectedCJPServers[index] + " not found in results");	

					Assert.AreEqual(expectedCJPServers[index] + " file location", servers[serverPos].FileLocation, "Unexpected file location");
					Assert.AreEqual(expectedCJPServers[index] + " remote object", servers[serverPos].RemoteObject, "Unexpected remote object");
					Assert.AreEqual(ServerType.CJP, servers[serverPos].Type, "Unexpected server type");
				}

				for (int index = 0; index < expectedTTBOServers.Length; index++)
				{
					int serverPos = FindServerIndex(servers, expectedTTBOServers[index]);
					Assert.IsTrue(serverPos != -1, "Server " + expectedTTBOServers[index] + " not found in results");

					Assert.AreEqual(expectedTTBOServers[index] + " file location", servers[serverPos].FileLocation, "Unexpected file location");
					Assert.AreEqual(expectedTTBOServers[index] + " remote object", servers[serverPos].RemoteObject, "Unexpected remote object");
					Assert.AreEqual(ServerType.TTBO, servers[serverPos].Type, "Unexpected server type");
				}

			}
			catch (Exception e)
			{
				Assert.Fail("Unexpected error occurred: " + e.Message);
			}
		}


		/// <summary>
		/// Tests that CJP server details are read in correctly when there are no TTBO servers
		/// </summary>
		[Test]
		public void TestNoTTBOServers()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider3()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);

				Assert.AreEqual(3, servers.Length, "The number of servers returned was not as expected");

				// Check the servers
				string[] expectedCJPServers = new string[] { "cjp1", "cjp2", "cjp3" };

				for (int index = 0; index < expectedCJPServers.Length; index++)
				{
					int serverPos = FindServerIndex(servers, expectedCJPServers[index]);
					Assert.IsTrue(serverPos != -1, "Server " + expectedCJPServers[index] + " not found in results");

					Assert.AreEqual(expectedCJPServers[index] + " file location", servers[serverPos].FileLocation, "Unexpected file location");
					Assert.AreEqual(expectedCJPServers[index] + " remote object", servers[serverPos].RemoteObject, "Unexpected remote object");
					Assert.AreEqual(ServerType.CJP, servers[serverPos].Type, "Unexpected server type");
				}

			}
			catch (Exception e)
			{
				Assert.Fail("Unexpected error occurred: " + e.Message);
			}
		}

		/// <summary>
		/// Tests that TTBO server details are read in correctly when there are no CJP servers
		/// </summary>
		[Test]
		public void TestNoCJPServers()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider4()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);

				Assert.AreEqual(2, servers.Length, "The number of servers returned was not as expected");

				// Check the servers
				string[] expectedTTBOServers = new string[] { "ttbo1", "ttbo2" };

				for (int index = 0; index < expectedTTBOServers.Length; index++)
				{
					int serverPos = FindServerIndex(servers, expectedTTBOServers[index]);
					Assert.IsTrue(serverPos != -1, "Server " + expectedTTBOServers[index] + " not found in results");

					Assert.AreEqual(expectedTTBOServers[index] + " file location", servers[serverPos].FileLocation, "Unexpected file location");
					Assert.AreEqual(expectedTTBOServers[index] + " remote object", servers[serverPos].RemoteObject, "Unexpected remote object");
					Assert.AreEqual(ServerType.TTBO, servers[serverPos].Type, "Unexpected server type");
				}

			}
			catch (Exception e)
			{
				Assert.Fail("Unexpected error occurred: " + e.Message);
			}
		}

		public void TestDuplicateEntriesInServersKey()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider5()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);

				Assert.AreEqual(2, servers.Length, "The number of servers returned was not as expected");

				// Check the servers
				string[] expectedCJPServers = new string[] { "cjp1", "cjp2" };

				for (int index = 0; index < expectedCJPServers.Length; index++)
				{
					int serverPos = FindServerIndex(servers, expectedCJPServers[index]);
					Assert.IsTrue(serverPos != -1, "Server " + expectedCJPServers[index] + " not found in results");

					Assert.AreEqual(expectedCJPServers[index] + " file location", servers[serverPos].FileLocation, "Unexpected file location");
					Assert.AreEqual(expectedCJPServers[index] + " remote object", servers[serverPos].RemoteObject, "Unexpected remote object");
					Assert.AreEqual(ServerType.CJP, servers[serverPos].Type, "Unexpected server type");
				}

			}
			catch (Exception e)
			{
				Assert.Fail("Unexpected error occurred: " + e.Message);
			}		
		}

		public void TestMissingFileLocation()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider6()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			
			bool errorRaised = false;

			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);
			}
			catch (TDException td)
			{
				if (td.Identifier == TDExceptionIdentifier.DGInvalidConfiguration)
					errorRaised = true;
			}
			catch (Exception e)
			{
				string dummy = e.Message;
				errorRaised = false;
			}
			Assert.IsTrue(errorRaised, "The expected error was not raised when a server had no file location");

		}

		public void TestMissingRemoteObjectLocation()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockPropertyProviderFactory(new MockPropertyProvider7()));
			IPropertyProvider current = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			bool errorRaised = false;

			try
			{
				ServerDetails[] servers = TTBOImportProperties.ReadServersForFeed(current, Keys.Rail);
			}
			catch (TDException td)
			{
				if (td.Identifier == TDExceptionIdentifier.DGInvalidConfiguration)
					errorRaised = true;
			}
			catch (Exception e)
			{
				string dummy = e.Message;
				errorRaised = false;
			}
			Assert.IsTrue(errorRaised, "The expected error was not raised when a server had no remote object details");
		}

		private int FindServerIndex(ServerDetails[] details, string id)
		{
			for (int i = 0; i < details.Length; i++)
			{
				if (details[i].Id == id)
					return i;
			}
			return -1;
		}

	}




	#endregion

	#region Property provider objects

	public class MockPropertyProviderFactory : IServiceFactory
	{
		private static object current;

		public MockPropertyProviderFactory(MockPropertyProviderBase propertyProvider)
		{
			current = (object)propertyProvider;
		}

		public object Get()
		{
			return current;
		}
	}

	#region Base mock property provider

	public class MockPropertyProviderBase : IPropertyProvider
	{
		protected Hashtable propertyTable;

		public MockPropertyProviderBase()
		{
			propertyTable = new Hashtable();
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

        public event SupersededEventHandler Superseded;

        // following definition gets rid of compiler warning
        public void Supersede()
        {
            Superseded(this, new EventArgs());
        }
        
        /// Read-only Version property
		public int Version
		{
			get { return 1; }
		}

		/// Read only Indexer property
		public string this[ string key]
		{
			get { return (string)propertyTable[key]; }
		}

		
		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		/// Read only ApplicationID Property
		public string ApplicationID
		{
			get { return "AirDataProvider"; }
		}

		/// Read only GroupID property
		public string GroupID
		{
			get { return "AirDataProvider"; }
		}
	}

	#endregion

	public class MockPropertyProvider1 : MockPropertyProviderBase
	{

		public MockPropertyProvider1() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "" );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "" );

		}

	}

	public class MockPropertyProvider2 : MockPropertyProviderBase
	{

		public MockPropertyProvider2() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "cjp1 cjp2 cjp3" );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "ttbo1 ttbo2" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.filelocation", "cjp1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.remoteobject", "cjp1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.filelocation", "cjp2 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.remoteobject", "cjp2 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp3.filelocation", "cjp3 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp3.remoteobject", "cjp3 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo1.filelocation", "ttbo1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo1.remoteobject", "ttbo1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo2.filelocation", "ttbo2 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo2.remoteobject", "ttbo2 remote object" );

		}

	}

	public class MockPropertyProvider3 : MockPropertyProviderBase
	{

		public MockPropertyProvider3() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "cjp1 cjp2 cjp3" );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.filelocation", "cjp1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.remoteobject", "cjp1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.filelocation", "cjp2 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.remoteobject", "cjp2 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp3.filelocation", "cjp3 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp3.remoteobject", "cjp3 remote object" );

		}

	}

	public class MockPropertyProvider4 : MockPropertyProviderBase
	{

		public MockPropertyProvider4() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "" );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "ttbo1 ttbo2" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo1.filelocation", "ttbo1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo1.remoteobject", "ttbo1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo2.filelocation", "ttbo2 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo2.remoteobject", "ttbo2 remote object" );
		}

	}

	public class MockPropertyProvider5 : MockPropertyProviderBase
	{

		public MockPropertyProvider5() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "  cjp1 cjp2       cjp1  cjp1  cjp2   " );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.filelocation", "cjp1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.remoteobject", "cjp1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.filelocation", "cjp2 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.remoteobject", "cjp2 remote object" );

		}

	}

	public class MockPropertyProvider6 : MockPropertyProviderBase
	{

		public MockPropertyProvider6() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "cjp1 cjp2" );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.filelocation", "cjp1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp1.remoteobject", "cjp1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.filelocation", "" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.cjp2.remoteobject", "cjp2 remote object" );

		}

	}

	public class MockPropertyProvider7 : MockPropertyProviderBase
	{

		public MockPropertyProvider7() : base()
		{
			propertyTable.Add( "datagateway.ttbo.rail.cjpservers", "" );
			propertyTable.Add( "datagateway.ttbo.rail.ttboservers", "ttbo1 ttbo2" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo1.filelocation", "ttbo1 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo1.remoteobject", "ttbo1 remote object" );

			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo2.filelocation", "ttbo2 file location" );
			propertyTable.Add( "datagateway.ttbo.rail.servers.ttbo2.remoteobject", "" );

		}

	}

	#endregion

}
