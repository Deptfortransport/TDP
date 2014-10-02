// *********************************************** 
// NAME			: LocalityTravelineCache.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/07/2005
// DESCRIPTION	: Implemention of the LocalityTravelineCache class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocalityTravelineCache.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:10   mturner
//Initial revision.
//
//   Rev 1.2   Aug 09 2005 16:15:24   RWilby
//Added //$Log: comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results

using System;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for LocalityTravelineCache.
	/// </summary>
	[Serializable()]
	public class LocalityTravelineCache
	{
		public LocalityTravelineCache()
		{
		}

		/// <summary>
		/// Adds locality / Traveline pair to LocalityTravelineCache
		/// </summary>
		/// <param name="locality">Locality</param>
		/// <param name="traveline">Traveline</param>
		public void Add(string locality, string traveline)
		{
			IPropertyProvider pp = Properties.Current;
			string CachePrefix= (string) pp[LocationServiceConstants.NPTGAccessCachePrefix];
			int CacheTimeoutSeconds= Convert.ToInt32( pp[LocationServiceConstants.NPTGAccessCacheTimeoutSeconds],CultureInfo.InvariantCulture.NumberFormat);

			ICache cache = (ICache)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cache];
			cache.Add(CachePrefix + locality,traveline,DateTime.Now.AddSeconds(CacheTimeoutSeconds));		
		}
		
		/// <summary>
		/// Returns cached Traveline for given locality
		/// </summary>
		/// <param name="locality">Locality</param>
		/// <returns>Returns Traveline if present in cache other an empty string</returns>
		public string Get(string locality)
		{
			IPropertyProvider pp = Properties.Current;
			string CachePrefix= (string) pp[LocationServiceConstants.NPTGAccessCachePrefix];

			ICache cache = (ICache)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cache];

			if (cache[CachePrefix + locality] != null)
				return (string) cache[CachePrefix + locality];
			else
				return string.Empty;
		}
	}
}
