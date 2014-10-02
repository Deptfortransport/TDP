// *********************************************** 
// NAME			: TDNaptan.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 19/09/2003 
// DESCRIPTION	: Holds a Naptan and its associated OSGR 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDNaptan.cs-arc  $
//
//   Rev 1.1   Mar 19 2010 10:49:40   apatel
//Added parameterless constructor for TDAirportNaptan and added XmlInclude attribute on TDNaptan class to support xml serialisation
//Resolution for 5469: Problem with feedback viewer
//
//   Rev 1.0   Nov 08 2007 12:25:24   mturner
//Initial revision.
//
//   Rev 1.15   Apr 03 2006 15:07:28   rwilby
//Added xml comment for TransportExchangeType.
//
//   Rev 1.14   Mar 21 2006 17:38:12   jmcallister
//Added TransportExchangeType property. This supports the changes for new method GetExchangeInfoForNaptan. This uses new feature on td.interactivemapping.dll and allows us to resolve coach naptans.
//
//   Rev 1.13   Dec 01 2005 17:36:10   RPhilpott
//Fix small bug in last change ...
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.12   Dec 01 2005 16:14:00   RPhilpott
//Use NaptanCache lookup instead of calling GISQuery directly. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.11   Nov 15 2005 18:29:56   RPhilpott
//Add locality to TDNaptan (to be used when getting naptan details from City-to-City database for coach find-a-fare).
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.10   May 19 2005 15:06:54   rscott
//Changes made to the way airports are compared in CheckEquals
//
//   Rev 1.9   Mar 22 2005 16:11:58   RPhilpott
//Add "use for fare enquiries" flag for naptans obtained from city-to-city database.
//
//   Rev 1.8   Oct 05 2004 17:18:58   jgeorge
//Amended comparison method to take into account special case for airport naptans. 
//Resolution for 1685: Extend Journey - same origin/destination location not being caught
//
//   Rev 1.7   Sep 09 2004 17:48:54   RPhilpott
//Change Find-A-Flight via location to pass new "group" NaPTAN instead of individual terminals.
//Resolution for 1402: Find a Flight STN to BEB via 9200GLA gives no results
//Resolution for 1455: Air stopovers returns no journeys.
//
//   Rev 1.6   Aug 20 2004 12:41:58   RPhilpott
//Obtain OSGR and Locality from GIS Query for TDAirNaptan.
//Resolution for 1376: Find a flight journey map not displayed
//Resolution for 1404: No locality passed to CJP for Find-A-Flight requests
//
//   Rev 1.5   Jul 22 2004 12:03:42   RPhilpott
//Use IAirDataProvider, not AirDataProvider
//
//   Rev 1.4   Jul 09 2004 13:09:18   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.3   Jun 03 2004 16:14:18   passuied
//changes for integration with FindFlight
//
//   Rev 1.2   Jun 03 2004 11:42:04   passuied
//added TDAirportNaptan object
//
//   Rev 1.1   Oct 16 2003 20:21:22   RPhilpott
//Correct initialisation by default ctor.
//
//   Rev 1.0   Sep 20 2003 16:59:36   RPhilpott
//Initial Revision
//

using System;
using System.Globalization;
using System.Xml.Serialization;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Presentation.InteractiveMapping;


namespace TransportDirect.UserPortal.LocationService
{
	[Serializable()]
	public class TDAirportNaptan : TDNaptan
	{
		private string airportLocality = String.Empty;

		private static readonly int IATA_LENGTH     = 3;
		private static readonly int TERMINAL_LENGTH = 1;

        /// <summary>
        /// Default parameterless constructor added for serialization/deserialization
        /// </summary>
        public TDAirportNaptan()
            : base()
        {
        }

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="naptan">associated NaPTan</param>
		public TDAirportNaptan(TDNaptan naptan) : base( naptan.Naptan, naptan.GridReference, naptan.Name)
		{
			// if this is a naptan recognisable by the stops database (ie, for a terminal)
			//  just use it, otherwise search for "terminal 1" (which every airport will have) 

			string naptanToSearch = (naptan.Naptan.Length == PREFIX_LENGTH + IATA_LENGTH + TERMINAL_LENGTH 
											? naptan.Naptan 
											: naptan.Naptan + "1"); 

			NaptanCacheEntry nce = NaptanLookup.Get(naptanToSearch, naptan.Name);

			if	(nce.Found) 
			{
				this.GridReference = nce.OSGR;
				this.airportLocality = nce.Locality;
			}
		}
	
		

		public string AirportName
		{
			get
			{
				return Name;
			}
		}

		public string IATA
		{
			get
			{
				return Naptan.Substring(PREFIX_LENGTH, IATA_LENGTH);
			}
		}

		public string Terminal
		{
			get
			{
				string sTerminal = String.Empty;
				
				try
				{
					sTerminal = Naptan.Substring(PREFIX_LENGTH + IATA_LENGTH, TERMINAL_LENGTH);
				}
				catch
				{
					return string.Empty;
				}
				
				return sTerminal;
			}
		}

		public bool IsAirport
		{
			get { return (this.StationType == StationType.Airport); }
		}

		public string AirportLocality
		{
			get { return airportLocality; } 
		}

		public bool IsActive
		{
			get
			{
				AirDataProvider.IAirDataProvider airData = (AirDataProvider.IAirDataProvider) TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
				return (airData.GetAirportFromNaptan(Naptan) != null);
			}
		}

		public bool HasDirectRouteWithAirport(TDNaptan[] naptans)
		{
			string[] airportDestinationCodes = new string[naptans.Length];
			
			for (int i=0; i < naptans.Length; i++)
			{
				airportDestinationCodes[i] = naptans[i].Naptan.Substring(PREFIX_LENGTH, IATA_LENGTH);
			}

			string airportOriginCode = this.IATA;
			AirDataProvider.IAirDataProvider airData = (AirDataProvider.IAirDataProvider) TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			
			return airData.ValidRouteExists(new string[]{airportOriginCode}, airportDestinationCodes);
			
		}

	}

	/// <summary>
	/// Holds a Naptan and its associated OSGR.
	/// </summary>
	[Serializable()]
    [XmlInclude(typeof(TDAirportNaptan))]
	public class TDNaptan
	{
		private OSGridReference osgr;
		private string naptan = string.Empty;
		private string stringName = String.Empty;
		private bool useForfareEnquiries = true;
		private string locality = string.Empty;

		static private string airportPrefix = Properties.Current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Airport.ToString())];
		static private string railPrefix	= Properties.Current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Rail.ToString())];
		static private string coachPrefix	= Properties.Current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Coach.ToString())];
		
		static protected readonly int PREFIX_LENGTH = 4;


		/// <summary>
		/// Alternate Constructor
		/// </summary>
		/// <param name="naptan">naptan ID</param>
		/// <param name="osgr">naptan coordinates</param>
		/// <param name="description">place name</param>
		public TDNaptan(string naptan, OSGridReference osgr, string description) : this(naptan, osgr)
		{
			this.stringName = description;
		}

		/// <summary>
		/// Alternate Constructor
		/// </summary>
		/// <param name="naptan">naptan ID</param>
		/// <param name="osgr">naptan coordinates</param>
		/// <param name="description">place name</param>
		/// <param name="useForfareEnquiries">this naptan to be used for fare enquiries</param>
		public TDNaptan(string naptan, OSGridReference osgr, string description, bool useForfareEnquiries) : this(naptan, osgr, description)
		{
			this.useForfareEnquiries = useForfareEnquiries;
		}

		/// <summary>
		/// Alternate Constructor
		/// </summary>
		/// <param name="naptan">naptan ID</param>
		/// <param name="osgr">naptan coordinates</param>
		/// <param name="description">place name</param>
		/// <param name="useForfareEnquiries">this naptan to be used for fare enquiries</param>
		/// <param name="locality">the locality</param>
		public TDNaptan(string naptan, OSGridReference osgr, string description, bool useForfareEnquiries, string locality) : this(naptan, osgr, description)
		{
			this.useForfareEnquiries = useForfareEnquiries;
			this.locality = locality;
		}

		/// <summary>
		/// Alternate Constructor
		/// </summary>
		/// <param name="naptan">naptan ID</param>
		/// <param name="osgr">naptan coordinates</param>
		public TDNaptan(string naptan, OSGridReference osgr)
		{
			this.naptan = naptan;
			this.osgr = osgr;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public TDNaptan()
		{
			this.naptan = string.Empty;
			this.osgr = new OSGridReference();
		}

		/// <summary>
		/// Read only property. Transport Exchange Type.
		/// </summary>
		public string TransportExchangeType
		{
			get
			{
				string exchangeType = string.Empty;

				switch (this.StationType)
				{
					case StationType.Airport :
						exchangeType = "Air";
						break;

					case StationType.Rail :
						exchangeType = "Rail";
						break;

					case StationType.Coach :
						exchangeType = "Coach";
						break;

					default:
						break;
				}
				
				return exchangeType;
			}
		}

		/// <summary>
		/// Get/Set property. OSGRridReference coordinates
		/// </summary>
		public OSGridReference GridReference
		{
			get { return osgr; }
			set { osgr = value; }
		}

		/// <summary>
		/// Get/Set property. Naptan ID
		/// </summary>
		public string Naptan
		{
			get { return naptan; }
			set { naptan = value; }
		}

		/// <summary>
		/// Get/Set property. Naptan description
		/// </summary>
		public string Name
		{
			get { return stringName; }
			set { stringName = value; }
		}

		/// <summary>
		/// Get/Set property. whether this naptan is to be used for fare enquiries
		///  (in practice, only relevant for rail stations) 
		/// </summary>
		public bool UseForFareEnquiries
		{
			get { return useForfareEnquiries; }
			set { useForfareEnquiries = value; }
		}
		
		/// <summary>
		/// Get/Set property. locality of this naptan
		///  (in practice, only populated for naptans deriived from city-to-city database)) 
		/// </summary>
		public string Locality
		{
			get { return locality; }
			set { locality = value; }
		}

		/// <summary>
		/// Read-only property, naptan prefix
		/// </summary>
		private string Prefix
		{
			get {return naptan.Substring(0, PREFIX_LENGTH); }
		}

		/// <summary>
		/// Read-only property, station type (derived from prefix)
		/// </summary>
		public StationType StationType
		{
			get
			{
				string naptanPrefix = this.Prefix;
				
				if ( naptanPrefix == airportPrefix)
					return StationType.Airport;
			
				if (naptanPrefix == railPrefix)
					return StationType.Rail;
				
				if (naptanPrefix == coachPrefix)
					return StationType.Coach;

				return StationType.Undetermined;
			}
		}

		/// <summary>
		/// Checks whether or not two naptans are equal. The standard Equals method is not overridden
		/// as we want the option of whether or not to take airport terminal numbers into account.
		/// </summary>
		/// <param name="naptanToCheck">The naptan to compare for equality</param>
		/// <param name="ignoreAirportTerminalNumbers">Whether or not airport terminal numbers should be taken into account</param>
		/// <returns></returns>
		public bool CheckEquals(TDNaptan  naptanToCheck, bool ignoreAirportTerminalNumbers)
		{
			// We do a special check if they are both airport naptans. Otherwise, just see if they
			// are the same
			if (ignoreAirportTerminalNumbers && (this.StationType == StationType.Airport) && (naptanToCheck.StationType == StationType.Airport))
			{
				AirDataProvider.IAirDataProvider airData = (AirDataProvider.IAirDataProvider) TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
				Airport airport1, airport2;
				airport1 = airData.GetAirportFromNaptan(this.Naptan);
				airport2 = airData.GetAirportFromNaptan(naptanToCheck.Naptan);

				if (airport1 != null && airport2 != null)
				{
					if(airport1.Equals(airport2))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return (this.Naptan == naptanToCheck.Naptan);
			}
		}
	}
}
