// *********************************************** 
// NAME             : TDPCache.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: TDPCache uses the regular WebCache for storing caching
// ************************************************
// 

using System;
using System.Web;
using System.Web.Caching;
using TDP.Common.ServiceDiscovery;

namespace TDP.Common.DataServices
{
    /// <summary>
    /// TDCache uses the regular WebCache for storing caching.
    /// It does itself have any particular logic in managing the cache neither does it expose the
    /// given attributes of associating different cache object with each other.
    /// </summary>
    public class TDPCache : ICache, IServiceFactory
    {

        #region ICache compliance

        /// <summary>
        /// Gets the cached object or null if it can not be found in the cache.
        /// </summary>
        public object this[string key]
        {
            get
            {
                return WebCache[key];
            }
        }

        /// <summary>
        /// Adds a object to the cache with a given key.
        /// The life-span of this cachable uses the default for the underlying cache.
        /// </summary>
        /// <param name="key">The key which this cachable object is bound to</param>
        /// <param name="cachable">The object to cache</param>
        public void Add(string key, object cachable)
        {
            WebCache.Insert(key, cachable);
        }

        /// <summary>
        /// Adds a object to the cache with a given key.
        /// The life-span is based upon a sliding timespan, where the timespan is the interval between the time it was last accessed and when it should expire.
        /// </summary>
        /// <param name="key">The key which this cachable object is bound to</param>
        /// <param name="cachable">The object to cache</param>
        /// <param name="delay">The interval between the time the inserted object is last accessed and when that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. </param>
        public void Add(string key, object cachable, TimeSpan delay)
        {
            WebCache.Insert(key, cachable, null, DateTime.MaxValue, delay, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Adds a object to the cache with a given key.
        /// The life-span is based upon a fixed expiry date.
        /// </summary>
        /// <param name="key">The key which this cachable object is bound to</param>
        /// <param name="cachable">The object to cache</param>
        /// <param name="absoluteExpiry">The time at which the inserted object expires and is removed from the cache</param>
        public void Add(string key, object cachable, DateTime absoluteExpiry)
        {
            WebCache.Insert(key, cachable, null, absoluteExpiry, TimeSpan.Zero, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Removes a cached object with a given key
        /// </summary>
        /// <param name="key">The key for the cached object to be removed</param>
        /// <returns>True if an object was actually removed or not</returns>
        public bool Remove(string key)
        {
            return WebCache.Remove(key) != null;
        }

        #endregion

        #region Convience methods & properties

        /// <summary>
        /// Gets the web cache object
        /// </summary>
        private Cache WebCache
        {
            get
            {
                return HttpRuntime.Cache;
            }
        }

        #endregion

        #region IServiceFactory compliance

        /// <summary>
        /// Gets this own instance
        /// </summary>
        /// <returns></returns>
        public object Get() { return this; }

        #endregion

    }
}
