// *********************************************** 
// NAME			: NaptanLookup.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-03-04 
// DESCRIPTION	: Naptan lookup and caching classes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/NaptanLookup.cs-arc  $
//
//   Rev 1.2   Oct 10 2012 14:27:58   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.1   Feb 16 2010 17:51:48   mmodi
//Updated to allow force save of non existent naptans to avoid repeated database calls
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:25:14   mturner
//Initial revision.
//
//   Rev 1.4   Jun 01 2006 17:41:02   rphilpott
//Replace "magic number" with meaningfully named constants.
//Resolution for 4103: Find Cheaper - journeys not always returned.
//
//   Rev 1.3   Apr 25 2006 17:27:46   COwczarek
//NaptanCache.Add no longer allows naptans with invalid OSGR to be added.
//Resolution for 3853: DN068 Adjust: Location name lost after adjust (causes map button issue)
//
//   Rev 1.2   Mar 22 2006 17:29:46   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.1   Mar 21 2006 17:40:28   jmcallister
//Added support for new method GetExchangeInfoForNaptan. This uses new feature on td.interactivemapping.dll and allows us to resolve coach naptans.
//
//   Rev 1.0   Mar 22 2005 16:15:22   RPhilpott
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.Presentation.InteractiveMapping;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.LocationService
{

	/// <summary>
	/// NaptanLookup - provides a static method to get naptan details 
	///    from the the cache if possible, otherwise from the Stops
	///    database (adding them to cache at the same time).
	/// </summary>
	public class NaptanLookup
	{

		private static readonly int NAPTAN_PREFIX_LENGTH = 4;
		private static readonly int IATA_CODE_LENGTH = 3;

		/// <summary>
		/// Never instantiated - static methods only
		/// </summary>
		private NaptanLookup()
		{
		}
	
		private static NaptanCacheEntry GetNaptanCacheEntry (QuerySchema qs, string naptan, string description)
		{
			NaptanCacheEntry entry;
			OSGridReference osgr = new OSGridReference();
			string locality		 = string.Empty;
			string name			 = string.Empty; 
			bool naptanFound	 = false;
	 
			foreach (QuerySchema.StopsRow row in qs.Stops.Rows)
			{
				if  (row.atcocode == naptan)
				{
					locality	= row.natgazid;
					name		= row.commonname;
					osgr		= new OSGridReference((int)row.X, (int)row.Y);
					naptanFound = true;

					if  (TDTraceSwitch.TraceVerbose)
					{
						OperationalEvent oe = new OperationalEvent( TDEventCategory.Infrastructure,
							TDTraceLevel.Verbose, "Naptan[" + naptan + "] Easting["+osgr.Easting+"] Northing[" + osgr.Northing + "] Name[" + name + "]");
						Logger.Write(oe);
					}
					break;
				}
			}

			if  (!naptanFound) 
			{
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Verbose, String.Format("GISQuery found no stops corresponding to NAPTAN [{0}] with description [{1}]", naptan, description)));
                }
			}

			entry = new NaptanCacheEntry(naptan, locality, name, osgr, naptanFound);

			return entry;
		}

		private static NaptanCacheEntry GetNaptanCacheEntry (ExchangePointSchema qs, string naptan, string description)
		{
			NaptanCacheEntry entry;
			OSGridReference osgr = new OSGridReference();
			string locality		 = string.Empty;
			string name			 = string.Empty; 
			bool naptanFound	 = false;
	 
			foreach (ExchangePointSchema.ExchangePointsRow row in qs.ExchangePoints.Rows)
			{
				if  (row.naptan == naptan)
				{
					locality	= row.locality;
					//locality = row.Table.Locale.Name;	
					name		= row.name;
					osgr		= new OSGridReference((int)row.easting, (int)row.northing);
					naptanFound = true;

					if  (TDTraceSwitch.TraceVerbose)
					{
						OperationalEvent oe = new OperationalEvent( TDEventCategory.Infrastructure,
							TDTraceLevel.Verbose, "Naptan[" + naptan + "] Easting["+osgr.Easting+"] Northing[" + osgr.Northing + "] Name[" + name + "]");
						Logger.Write(oe);
					}
					break;
				}
			}

			if  (!naptanFound) 
			{
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Verbose, String.Format("GISQuery found no stops corresponding to NAPTAN [{0}] with description [{1}]", naptan, description)));
                }
			}

			entry = new NaptanCacheEntry(naptan, locality, name, osgr, naptanFound);

			return entry;
		}


		/// <summary>
		/// Get a NaptanCacheEntry for the specified naptan.
		///	
		/// Will always return a value, which may be:
		///   a) retrieved directly from cache;
		///   b) created from details obtained from a GIS query,
		///        then added to cache.
		///        
		/// If naptan is not found by GIS query, a NaptanCacheEntry
		/// will still be cached and returned with property Found 
		/// set to false -- this is to prevent pointless repeated 
		/// database lookups for non-existent naptans.         
		/// </summary>
		public static NaptanCacheEntry Get(string naptan, string description)
		{
			
			NaptanCacheEntry entry = NaptanCache.Get(naptan);

			if	((entry == null) || ((!entry.Found) && (entry.Description == null || entry.Description.Length == 0)))
			{
				try 
				{
					IGisQuery query = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

					TDNaptan tempNaptan = new TDNaptan();
					tempNaptan.Naptan = naptan; 

					if	(((tempNaptan.StationType == StationType.Airport && naptan.Length == NAPTAN_PREFIX_LENGTH + IATA_CODE_LENGTH) 
							|| tempNaptan.StationType == StationType.Coach))
					{
						entry = GetNaptanCacheEntry(query.GetExchangeInfoForNaptan(tempNaptan), naptan, description);
					}
					else
					{
						QuerySchema qs = query.FindStopsInfoForStops(new string[] { naptan });
						entry = GetNaptanCacheEntry(qs, naptan, description);
					}
				}
				catch (MapExceptionGeneral mpec)
				{
					// Log the exception
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, "No stops corresponding to NAPTAN " + naptan + ". Exception error message:" + mpec.Message));

					// create empty "not found" cache entry for this naptan
					entry = new NaptanCacheEntry(naptan, string.Empty, string.Empty, new OSGridReference(), false);
				}

				NaptanCache.Add(entry);				
                   
			}

			return entry;
		}
	}


	/// <summary>
	/// NaptanCache - provides static methods to add naptan details 
	///    to the cache and then subsequently retrieve them ...
	/// </summary>
	public class NaptanCache
	{
		private const string PROP_NAPTANCACHE_TIMEOUT = "JourneyControl.NaptanCacheTimeoutSeconds";
		private const int DEFAULT_TIMEOUT = 60 * 20;    // used if property not found

		/// <summary>
		/// Never instantiated - static methods only
		/// </summary>
		private NaptanCache()
		{
		}

		public static NaptanCacheEntry Get(string naptan)
		{
			ICache cache = (ICache)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cache];
			return (NaptanCacheEntry)(cache[naptan]);
		}

        public static void Add(NaptanCacheEntry entry)
        {
            NaptanCache.Add(entry, false);
        }

        public static void Add(string naptan, string locality, string description, OSGridReference osgr, bool found)
        {
            NaptanCache.Add(new NaptanCacheEntry(naptan, locality, description, osgr, found));
        }

        public static void Add(NaptanCacheEntry entry, bool forceAdd)
		{
            if ((entry.OSGR.IsValid) || (forceAdd))
            {
                ICache cache = (ICache)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cache];
			
                int secs = DEFAULT_TIMEOUT; 
                    
                try 
                {
                    secs = Convert.ToInt32( Properties.Current[PROP_NAPTANCACHE_TIMEOUT],
                        System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Error, "Illegal definition of property " + PROP_NAPTANCACHE_TIMEOUT + ". " + PROP_NAPTANCACHE_TIMEOUT + " should contain a valid number, defaulting to 20 minutes."));
                }

                cache.Add(entry.Naptan, entry, new TimeSpan(0, 0, secs));
            }
		}
	}


	/// <summary>
	/// NaptanCacheEntry - holds the information about a single naptan 
	///    that needs to be cached to avoid excessive database reads.
	/// </summary>

	[Serializable()]
	public class NaptanCacheEntry
	{
		private string naptan = string.Empty;
		private string locality = string.Empty;
		private string description = string.Empty;
		private OSGridReference osgr = new OSGridReference();
		private bool found = false;

		public NaptanCacheEntry(string naptan, string locality, string description, OSGridReference osgr, bool found)
		{
			this.naptan = naptan;
			this.locality = locality;
			this.description = description;
			this.osgr = osgr;
			this.found = found;
		}

		public string Naptan 
		{
			get { return naptan; }
		}

		public string Locality 
		{
			get { return locality; }
		}

		public string Description 
		{
			get { return description; }
		}

		public OSGridReference OSGR  
		{
			get { return osgr; }
		}

		public bool Found  
		{
			get { return found; }
		}
	}
}
