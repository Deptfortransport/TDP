// ***********************************************
// NAME 		: TDCache.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 05/11/2003
// DESCRIPTION 	: A mock testing object for the Cache service. 
//				  This class is copied from SessionManager in order not to cause circular reference problem.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestMockCache.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 16 2006 16:28:12   mguney
//Initial revision.
//
//   Rev 1.1   Nov 06 2003 16:43:50   PNorell
//Updated and fixed the compilation error I previously introduced with forgetting the include the imports (and fullfill all the interfaces).
//
//   Rev 1.0   Nov 06 2003 16:25:20   PNorell
//Initial Revision
using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// A test cache which only stores thing in a simple hashtable.
	/// </summary>
	public class TestMockCache : ICache, IServiceFactory
	{
		private Hashtable htCache = new Hashtable();

		/// <summary>
		/// Adds a object to the cache with a given key.
		/// The life-span of this cachable uses the default for the underlying cache.
		/// </summary>
		/// <param name="key">The key which this cachable object is bound to</param>
		/// <param name="cachable">The object to cache</param>
		public void Add( string key, object cachable)
		{
			htCache[key] = cachable;
		}

		/// <summary>
		/// Adds a object to the cache with a given key.
		/// The life-span is based upon a sliding timespan, where the timespan is the interval between the time it was last accessed and when it should expire.
		/// </summary>
		/// <param name="key">The key which this cachable object is bound to</param>
		/// <param name="cachable">The object to cache</param>
		/// <param name="delay">The interval between the time the inserted object is last accessed and when that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. </param>
		public void Add( string key, object cachable, TimeSpan delay)
		{
			this.Add( key, cachable );
		}

		/// <summary>
		/// Adds a object to the cache with a given key.
		/// The life-span is based upon a fixed expiry date.
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
		/// <returns>Always returns True</returns>
		public bool Remove( string key )
		{
			htCache.Remove( key );
			return true;
		}

		/// <summary>
		/// Returns the current instance.
		/// </summary>
		/// <returns></returns>
		public object Get() { return this; }
		
	}
}
