// *********************************************** 
// NAME			: TestLocalityTravelineLookup.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 2005/07/20
// DESCRIPTION	: Class testing the funcationality of LocalityTravelineLookup
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestLocalityTravelineLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:44   mturner
//Initial revision.
//
//   Rev 1.1   Aug 09 2005 16:11:50   RWilby
//Added //$Log: comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
using System;
using System.Collections;
using System.Diagnostics;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for TestLocalityTravelineLookup.
	/// </summary>
	[TestFixture]
	public class TestLocalityTravelineLookup
	{
		public TestLocalityTravelineLookup()
		{}

		[SetUp]
		public void Init() 
		{ 
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestLocalityTravelineLookupTestInitialisation());


			Trace.Listeners.Remove("TDTraceListener");
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

		}

		[Test]
		public void TestGetTraveline ()
		{
			ILocalityTravelineLookup localityTravelineLookup = AddLocalityTravelineLookup();

			Assert.AreEqual("Y",localityTravelineLookup.GetTraveline("E0032913"),"Traveline incorrect for locality E0032913. Note: This test is depandant on CJP data");
			Assert.AreEqual("Y",localityTravelineLookup.GetTraveline("N0077058"),"Traveline incorrect for locality N0077058. Note: This test is depandant on CJP data");
			Assert.AreEqual("L",localityTravelineLookup.GetTraveline("E0034642"),"Traveline incorrect for locality E0034642. Note: This test is depandant on CJP data");
			Assert.AreEqual("EM",localityTravelineLookup.GetTraveline("E0054915"),"Traveline incorrect for locality E0054915. Note: This test is depandant on CJP data");
			Assert.AreEqual("WM",localityTravelineLookup.GetTraveline("E0057937"),"Traveline incorrect for locality E0057937. Note: This test is depandant on CJP data");
		}

		private ILocalityTravelineLookup AddLocalityTravelineLookup()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.LocalityTravelineLookup, new LocalityTravelineLookup());
			return (ILocalityTravelineLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocalityTravelineLookup];
		}
	}

	#region Initialisation class

	/// <summary>
	/// Initialisation class for Bay Text Filter test
	/// </summary>
	public class TestLocalityTravelineLookupTestInitialisation : IServiceInitialisation
	{
		public TestLocalityTravelineLookupTestInitialisation()
		{
		}

		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{		
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			// Enable Cache object
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TestTDCache() );
		}

	}

	#endregion

	/// <summary>
	/// Test implementation of ITDCache
	/// </summary>
	public class TestTDCache : ICache, IServiceFactory
	{
		private Hashtable cache;

		public TestTDCache()
		{
			cache = new Hashtable();
		}

		public void Add(string key, object cachable)
		{
			cache.Add(key,cachable);
		}

		public void Add( string key, object cachable, TimeSpan delay)
		{
			cache.Add(key,cachable);
		}
		
		public void Add( string key, object cachable, DateTime absoluteExpiry)
		{
			cache.Add(key,cachable);
		}

		public bool Remove(string key)
		{
			cache.Remove(key);
			return true;
		}

		public object this[string key] 
		{
			get 
			{
				return cache[key];
			}
		}

		#region IServiceFactory compliance
		/// <summary>
		/// Gets this own instance
		/// </summary>
		/// <returns></returns>
		public object Get() { return this; }
		#endregion

	}
}
