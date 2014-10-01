// *********************************************** 
// NAME			: TestMockCache.cs
// AUTHOR		: Marcus Tillett
// DATE CREATED	: 02/02/06
// DESCRIPTION	: Mock TDCache class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/Test/TestMockCache.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:17:44   mturner
//Initial revision.
//
//   Rev 1.0   Feb 02 2006 13:52:36   mtillett
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Summary description for TestMockCache.
	/// </summary>
	public class TestMockCache : ICache, IServiceFactory
	{
		private Hashtable htCache = new Hashtable();

		/// <summary>
		/// Adds a object to the cache with a given key, the cache never expires for the mock test.
		/// </summary>
		/// <param name="key">The key which this cachable object is bound to</param>
		/// <param name="cachable">The object to cache</param>
		public void Add( string key, object cachable)
		{
			htCache[key] = cachable;
		}

		/// <summary>
		/// Adds a object to the cache with a given key, the cache never expires for the mock test.
		/// </summary>
		/// <param name="key">The key which this cachable object is bound to</param>
		/// <param name="cachable">The object to cache</param>
		/// <param name="delay">The interval between the time the inserted object is last accessed and when that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. </param>
		public void Add( string key, object cachable, TimeSpan delay)
		{
			this.Add( key, cachable );
		}

		/// <summary>
		/// Adds a object to the cache with a given key, the cache never expires for the mock test.
		/// </summary>
		/// <param name="key">The key which this cachable object is bound to</param>
		/// <param name="cachable">The object to cache</param>
		/// <param name="absoluteExpiry">The time at which the inserted object expires and is removed from the cache</param>
		public void Add( string key, object cachable, DateTime absoluteExpiry)
		{
			this.Add( key, cachable );
		}


		/// <summary>
		/// Gets the cached object or null if it can not be found in the cache.
		/// </summary>
		public object this[string key] 
		{ 
			get 
			{
				return htCache[key];
			}
		}

		/// <summary>
		/// Removes a cached object with a given key
		/// </summary>
		/// <param name="key">The key for the cached object to be removed</param>
		/// <returns>Always returns true even if no object was not removed</returns>
		public bool Remove( string key )
		{
			htCache.Remove( key );
			return true;
		}
		/// <summary>
		/// Implementation of the Get method of IServiceFactory
		/// </summary>
		/// <returns></returns>
		public object Get() { return this; }
		
	}
}
