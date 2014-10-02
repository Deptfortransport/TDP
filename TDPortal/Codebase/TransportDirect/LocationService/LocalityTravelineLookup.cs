// *********************************************** 
// NAME			: LocalityTravelineLookup.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/07/2005
// DESCRIPTION	: Implemention of the LocalityTravelineLookup class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocalityTravelineLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:10   mturner
//Initial revision.
//
//   Rev 1.2   Aug 09 2005 16:17:10   RWilby
//Added //$Log: comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results

using System;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning;
using TransportDirect.JourneyPlanning.NPTG.AccessModule;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for LocalityTravelineLookup.
	/// </summary>
	[Serializable()]
	public class LocalityTravelineLookup :ILocalityTravelineLookup, IServiceFactory
	{
		
		private NPTGAccessModule AccessModule;

		public LocalityTravelineLookup()
		{
			IPropertyProvider pp = Properties.Current;
			string ConectionString= (string) pp[LocationServiceConstants.NPTGAccessConnectionString];
			AccessModule = new NPTGAccessModule(ConectionString);
		}

		/// <summary>
		/// Returns appropriate Traveline code for a given locality
		/// </summary>
		/// <param name="locality">The Locality</param>
		/// <returns>Returns the Traveline code or an empty 
		/// string if the Traveline code cannot be found for locality</returns>
		public string GetTraveline(string locality)
		{
			try
			{
				LocalityTravelineCache ltCache = new LocalityTravelineCache();
			
				string result = ltCache.Get(locality);

				if(result.Length ==0)
				{
					string[] localities = new string[1];
					localities[0] = locality;
					JourneyPlanning.NPTG.AccessModule.RegionURL[] regionURLs = AccessModule.GetURL (localities);
					ltCache.Add(locality,regionURLs[0].TravelineRegionID);

					return regionURLs[0].TravelineRegionID;
				}
				else
					return result;
			}
			catch (Exception e)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An exception occurred in LocalityTravelineLookup.GetTraveline method.", e));
				//Return empty string in case of error
				return string.Empty;
			}
		}
		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current LocalityTravelineLookup object
		/// </summary>
		/// <returns>LocalityTravelineLookup object</returns>
		public object Get()
		{
			return this;
		}

		#endregion
	}
}
