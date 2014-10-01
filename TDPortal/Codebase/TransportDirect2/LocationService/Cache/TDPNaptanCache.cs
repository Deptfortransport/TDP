// *********************************************** 
// NAME             : TDPNaptanCache.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Naptan lookup and caching classes
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.DataServices;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using Logger = System.Diagnostics.Trace;
using TDP.Common.EventLogging;
using TDP.Common.LocationService.GIS;
using TransportDirect.Presentation.InteractiveMapping;

namespace TDP.Common.LocationService
{
    #region TDPNaptanCache

    /// <summary>
    /// Naptan lookup and caching classes
    /// </summary>
    static class TDPNaptanCache
    {
        #region Private members

        private static readonly int NAPTAN_PREFIX_LENGTH = 4;
        private static readonly int IATA_CODE_LENGTH = 3;

        private static string airportPrefix = Properties.Current[Keys.NaptanPrefix_Airport];
        private static string coachPrefix = Properties.Current[Keys.NaptanPrefix_Coach];
        private static string railPrefix = Properties.Current[Keys.NaptanPrefix_Rail];

        #endregion

        #region Constructor

        /// <summary>
        /// Never instantiated - static methods only
        /// </summary>
        static TDPNaptanCache()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get a NaptanCacheEntry for the specified naptan.
        ///	
        /// Will always return a value, which may be:
        ///   a) retrieved directly from cache;
        ///   b) created from details obtained from a GIS query, then added to cache.
        ///        
        /// If naptan is not found by GIS query, a NaptanCacheEntry
        /// will still be cached and returned with property Found 
        /// set to false -- this is to prevent pointless repeated 
        /// database lookups for non-existent naptans.         
        /// </summary>
        public static NaptanCacheEntry Get(string naptan, string description)
        {
            // Look in cache first
            NaptanCacheEntry entry = Get(naptan);

            if (entry == null) 
                //|| ((!entry.Found) && (string.IsNullOrEmpty(entry.Name))))
            {
                try
                {
                    GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                    string naptanPrefix = NaptanPrefix(naptan);

                    bool isAirportNaptan = (naptanPrefix == airportPrefix && naptan.Length == NAPTAN_PREFIX_LENGTH + IATA_CODE_LENGTH);
                    bool isCoachNaptan = (naptanPrefix == coachPrefix);
                    
                    if (isAirportNaptan || isCoachNaptan)
                    {
                        ExchangePointSchema eps = gisQuery.GetExchangeInfoForNaptan(naptan, TransportExchangeType(naptan));
                        entry = GetNaptanCacheEntry(eps, naptan, description);
                    }
                    else
                    {
                        QuerySchema qs = gisQuery.FindStopsInfoForStops(new string[] { naptan });
                        entry = GetNaptanCacheEntry(qs, naptan, description);
                    }
                }
                catch (MapExceptionGeneral mpec)
                {
                    // Log the exception
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, 
                        string.Format("No stops corresponding to NAPTAN[{0}]. Exception error message: {1}", naptan, mpec.Message)));

                    // Create empty "not found" cache entry for this naptan
                    entry = new NaptanCacheEntry(naptan, string.Empty, string.Empty, new OSGridReference(), false);
                }

                // Add naptan entry to cache
                Add(entry);
            }

            return entry;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Reads a NaptanCacheEntry from a GIS QuerySchema.StopsRow response
        /// </summary>
        private static NaptanCacheEntry GetNaptanCacheEntry(QuerySchema qs, string naptan, string description)
        {
            NaptanCacheEntry entry;
            OSGridReference osgr = new OSGridReference();
            string locality = string.Empty;
            string name = string.Empty;
            bool naptanFound = false;

            foreach (QuerySchema.StopsRow row in qs.Stops.Rows)
            {
                if (row.atcocode == naptan)
                {
                    locality = row.natgazid;
                    name = row.commonname;
                    osgr = new OSGridReference((int)row.X, (int)row.Y);
                    naptanFound = true;

                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                            string.Format("GISQuery FindStopsInfoForStops found: Naptan[{0}] Name[{1}] OSGR[{2}] Locality[{3}]", naptan, name, osgr.ToString(), locality));
                        Logger.Write(oe);
                    }
                    break;
                }
            }

            if (!naptanFound)
            {
                if (TDPTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                        string.Format("GISQuery FindStopsInfoForStops found no stops for Naptan[{0}] with Description[{1}]", naptan, description)));
                }
            }

            entry = new NaptanCacheEntry(naptan, locality, name, osgr, naptanFound);

            return entry;
        }

        /// <summary>
        /// Reads a NaptanCacheEntry from a GIS ExchangePointSchema.ExchangePointsRow response
        /// </summary>
        private static NaptanCacheEntry GetNaptanCacheEntry(ExchangePointSchema eps, string naptan, string description)
        {
            NaptanCacheEntry entry;
            OSGridReference osgr = new OSGridReference();
            string locality = string.Empty;
            string name = string.Empty;
            bool naptanFound = false;

            foreach (ExchangePointSchema.ExchangePointsRow row in eps.ExchangePoints.Rows)
            {
                if (row.naptan == naptan)
                {
                    locality = row.locality;
                    name = row.name;
                    osgr = new OSGridReference((int)row.easting, (int)row.northing);
                    naptanFound = true;

                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, 
                            string.Format("GISQuery GetExchangeInfoForNaptan found: Naptan[{0}] Name[{1}] OSGR[{2}] Locality[{3}]", naptan, name, osgr.ToString(), locality));
                        Logger.Write(oe);
                    }
                    break;
                }
            }

            if (!naptanFound)
            {
                if (TDPTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, 
                        string.Format("GISQuery GetExchangeInfoForNaptan found no stops for Naptan[{0}] with Description[{1}]", naptan, description)));
                }
            }

            entry = new NaptanCacheEntry(naptan, locality, name, osgr, naptanFound);

            return entry;
        }

        /// <summary>
        /// Returns the Transport Exchange Type for the naptan
        /// </summary>
        private static string TransportExchangeType(string naptan)
        {
            string exchangeType = string.Empty;

            try
            {
                string naptanPrefix = NaptanPrefix(naptan);

                if (naptanPrefix == railPrefix)
                    exchangeType = "Rail";
                else if (naptanPrefix == coachPrefix)
                    exchangeType = "Coach";
                else if (naptanPrefix == airportPrefix)
                    exchangeType = "Air";
            }
            catch
            {
                // Ignore exception, invalid naptan
            }

            return exchangeType;
        }

        /// <summary>
        /// Returns the prefix for the naptan
        /// </summary>
        /// <param name="naptan"></param>
        /// <returns></returns>
        private static string NaptanPrefix(string naptan)
        {
            try
            {
                return naptan.Substring(0, NAPTAN_PREFIX_LENGTH);
            }
            catch
            {
                // Ignore exception, invalid naptan
            }

            return string.Empty;
        }
                
        #endregion

        #region Private cache methods

        /// <summary>
        /// Gets the naptan from the cache
        /// </summary>
        private static NaptanCacheEntry Get(string naptan)
        {
            ICache cache = TDPServiceDiscovery.Current.Get<ICache>(ServiceDiscoveryKey.Cache);
            return (NaptanCacheEntry)(cache[naptan]);
        }

        /// <summary>
        /// Adds the naptan to the cache
        /// </summary>
        private static void Add(string naptan, string locality, string description, OSGridReference osgr, bool found)
        {
            NaptanCacheEntry entry = new NaptanCacheEntry(naptan, locality, description, osgr, found);
            Add(entry, false);
        }

        /// <summary>
        /// Adds the naptan to the cache
        /// </summary>
        private static void Add(NaptanCacheEntry entry)
        {
            Add(entry, false);
        }

        /// <summary>
        /// Adds the naptan to the cache
        /// </summary>
        private static void Add(NaptanCacheEntry entry, bool forceAdd)
        {
            if ((entry.OSGR.IsValid) || (forceAdd))
            {
                ICache cache = TDPServiceDiscovery.Current.Get<ICache>(ServiceDiscoveryKey.Cache);

                int secs = 0;

                if (!Int32.TryParse(Properties.Current["JourneyControl.NaptanCacheTimeoutSeconds"], out secs))
                {
                    secs = 60 * 20; // Default timeout 20 mins

                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error,
                        "Missing property[JourneyControl.NaptanCacheTimeoutSeconds]. Naptan cache defaulting to 20 minutes."));
                }

                cache.Add(entry.Naptan, entry, new TimeSpan(0, 0, secs));
            }
        }

        #endregion
    }

    #endregion

    #region NaptanCacheEntry

    /// <summary>
    /// NaptanCacheEntry - holds the information about a single naptan 
    ///    that needs to be cached to avoid excessive database reads.
    /// </summary>
    [Serializable()]
    public class NaptanCacheEntry
    {
        #region Private members

        private string naptan = string.Empty;
        private string locality = string.Empty;
        private string name = string.Empty;
        private OSGridReference osgr = new OSGridReference();
        private bool found = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NaptanCacheEntry(string naptan, string locality, string name, OSGridReference osgr, bool found)
        {
            this.naptan = naptan;
            this.locality = locality;
            this.name = name;
            this.osgr = osgr;
            this.found = found;
        }

        #endregion

        #region Private members

        /// <summary>
        /// Read only.
        /// </summary>
        public string Naptan
        {
            get { return naptan; }
        }

        /// <summary>
        /// Read only.
        /// </summary>
        public string Locality
        {
            get { return locality; }
        }

        /// <summary>
        /// Read only.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Read only.
        /// </summary>
        public OSGridReference OSGR
        {
            get { return osgr; }
        }

        /// <summary>
        /// Read only.
        /// </summary>
        public bool Found
        {
            get { return found; }
        }

        #endregion

    }

    #endregion
}
