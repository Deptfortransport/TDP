// *********************************************** 
// NAME			: LocationServiceConstants.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 2005-07-18 
// DESCRIPTION	: Constant strings and property keys
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocationSeriveConstants.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:12   mturner
//Initial revision.
//
//   Rev 1.1   Aug 09 2005 16:18:38   RWilby
//Added //$Log: comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for LocationSeriveConstants.
	/// </summary>
	public class LocationServiceConstants
	{
		#region PropertyKeys
		public static readonly string NPTGAccessConnectionString  = "LocationService.NPTGAccess.ConnectionString";
		public static readonly string NPTGAccessCachePrefix = "LocationService.NPTGAccess.CachePrfix";
		public static readonly string NPTGAccessCacheTimeoutSeconds  = "LocationService.NPTGAccess.CacheTimeoutSeconds";
		#endregion


		/// <summary>
		/// Class contains static readonly members only - never instantiated
		/// </summary>
		public LocationServiceConstants()
		{}
	}
}
