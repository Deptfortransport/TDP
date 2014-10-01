// *********************************************** 
// NAME             : ICache.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Definition of what can act as a cache in TDP
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.DataServices
{
    /// <summary>
    /// Definition of what can act as a cache in TDP.
    /// </summary>
    public interface ICache
    {

        /// <summary>
        /// Adds a object to the cache with a given key.
        /// The life-span of this cachable uses the default for the underlying cache.
        /// </summary>
        /// <param name="key">The key which this cachable object is bound to</param>
        /// <param name="cachable">The object to cache</param>
        void Add(string key, object cachable);

        /// <summary>
        /// Adds a object to the cache with a given key.
        /// The life-span is based upon a sliding timespan, where the timespan is the interval between the time it was last accessed and when it should expire.
        /// </summary>
        /// <param name="key">The key which this cachable object is bound to</param>
        /// <param name="cachable">The object to cache</param>
        /// <param name="delay">The interval between the time the inserted object is last accessed and when that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. </param>
        void Add(string key, object cachable, TimeSpan delay);

        /// <summary>
        /// Adds a object to the cache with a given key.
        /// The life-span is based upon a fixed expiry date.
        /// </summary>
        /// <param name="key">The key which this cachable object is bound to</param>
        /// <param name="cachable">The object to cache</param>
        /// <param name="absoluteExpiry">The time at which the inserted object expires and is removed from the cache</param>
        void Add(string key, object cachable, DateTime absoluteExpiry);

        /// <summary>
        /// Gets the cached object or null if it can not be found in the cache.
        /// </summary>
        object this[string key] { get; }

        /// <summary>
        /// Removes a cached object with a given key
        /// </summary>
        /// <param name="key">The key for the cached object to be removed</param>
        /// <returns>True if an object was actually removed or not</returns>
        bool Remove(string key);

    }
}