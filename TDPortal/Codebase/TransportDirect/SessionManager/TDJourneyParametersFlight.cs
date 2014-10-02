// ***********************************************
// NAME 		: TDJourneyParametersFlight.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 21/05/2004
// DESCRIPTION 	: Journey parameters for find a flight functions
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDJourneyParametersFlight.cs-arc  $
//
//   Rev 1.1   Dec 06 2010 12:54:54   apatel
//Code updated to implement show all show 10 feature for journey results and to remove anytime option from the input page.
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.0   Nov 08 2007 12:48:40   mturner
//Initial revision.
//
//   Rev 1.12   Aug 19 2005 14:06:54   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.11.1.2   Aug 19 2005 10:55:02   asinclair
//Fixed for code review
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.11.1.1   Aug 09 2005 19:54:06   asinclair
//Removed commented out code
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.11.1.0   Jul 27 2005 18:11:42   asinclair
//Check in to fix build errors.  Work in progress
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.11   Nov 25 2004 11:19:18   jgeorge
//Moved DirectFlightsOnly property into base class
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.10   Sep 13 2004 17:58:38   RPhilpott
//Support new TTBO station group structure by using only "group" airport naptans for all Find-A-Flight locations (origin, destination and via). 
//Resolution for 1402: Find a Flight STN to BEB via 9200GLA gives no results
//Resolution for 1455: Air stopovers returns no journeys.
//
//   Rev 1.9   Sep 09 2004 17:48:54   RPhilpott
//Change Find-A-Flight via location to pass new "group" NaPTAN instead of individual terminals.
//Resolution for 1402: Find a Flight STN to BEB via 9200GLA gives no results
//Resolution for 1455: Air stopovers returns no journeys.
//
//   Rev 1.8   Jul 14 2004 14:47:04   RPhilpott
//Move "any time" parameters from flight to base class to make them accessible to non-flight traunk journeys.
//
//   Rev 1.7   Jul 08 2004 15:14:20   jgeorge
//Review comments
//
//   Rev 1.6   Jun 28 2004 20:49:12   JHaydock
//JourneyPlannerInput clear page and back buttons for extend journey
//
//   Rev 1.5   Jun 25 2004 12:25:10   RPhilpott
//Make support for operatror selection accessible to mult-modal journeys as well as flights.
//
//   Rev 1.4   Jun 17 2004 16:34:12   jgeorge
//Added tests
//
//   Rev 1.3   Jun 10 2004 10:18:08   jgeorge
//Updated property names
//
//   Rev 1.2   Jun 09 2004 17:14:18   jgeorge
//Interim check in
//
//   Rev 1.1   Jun 02 2004 14:02:10   jgeorge
//Work in progress
//
//   Rev 1.0   May 26 2004 08:54:50   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using System.Text;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Journey parameters for find a flight functions
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class TDJourneyParametersFlight : TDJourneyParameters
	{
		#region Private variables

		private AirRegion originSelectedRegion;
		private Airport[] originSelectedAirports;
		private AirRegion destinationSelectedRegion;
		private Airport[] destinationSelectedAirports;
		private Airport viaSelectedAirport;
		private TDLocation viaLocation;

		private int extraCheckInTime;
		private int outwardStopover;
		private int returnStopover;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Calls the base constructor
		/// </summary>
		public TDJourneyParametersFlight() : base()
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The region selected by the user for the origin location
		/// <seealso cref="SetOutwardDetails"/>
		/// </summary>
		public AirRegion OriginSelectedRegion()
		{
			return originSelectedRegion;
		}

		/// <summary>
		/// The airports selected by the user for the origin location
		/// <seealso cref="SetOutwardDetails"/>
		/// </summary>
		public Airport[] OriginSelectedAirports()
		{
			return (Airport[])originSelectedAirports.Clone();
		}

		/// <summary>
		/// The region selected by the user for the destination location
		/// <seealso cref="SetReturnDetails"/>
		/// </summary>
		public AirRegion DestinationSelectedRegion()
		{
			return destinationSelectedRegion;
		}

		/// <summary>
		/// The airports selected by the user for the destination location
		/// <seealso cref="SetReturnDetails"/>
		/// </summary>
		public Airport[] DestinationSelectedAirports()
		{
			return (Airport[])destinationSelectedAirports.Clone();
		}

		/// <summary>
		/// The airport selected by the user for the stopover airport.
		/// When set, updates the ViaLocation property accordingly
		/// </summary>
		public Airport ViaSelectedAirport
		{
			get { return viaSelectedAirport; }
			set 
			{ 
				viaSelectedAirport = value; 
				if (value == null)
					viaLocation = new TDLocation();
				else
					viaLocation = BuildLocation(null, new Airport[] { value }, true);
			}
		}

		/// <summary>
		/// The location to use for the stopover airport.
		/// When set, updates the ViaSelectedAirport property accordingly,
		/// which sets the actual value of the viaLocation variable.
		/// Writes a warning if the location contains more than one airport.
		/// </summary>
		public TDLocation ViaLocation
		{
			get { return viaLocation; }
			set 
			{ 
				if (value == null || value.Status != TDLocationStatus.Valid)
					viaSelectedAirport = null;
				else
				{
					Airport[] airports = BuildAirports(value);
					if (airports.Length != 1)
						// More than one airport in the TDLocation - write a warning then use only
						// the first
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "The ViaLocation property of TDJourneyParametersFlight was set to a value whose Naptans represent more than one airport. Only the first was used."));

					// Set using the property to ensure that the location is updated to reflect this
					if (airports.Length == 0)
						ViaSelectedAirport = null;
					else
						ViaSelectedAirport = airports[0];
						
				}
			}
		}

		/// <summary>
		/// The number of additional minutes to allow for check in time
		/// </summary>
		public int ExtraCheckInTime
		{
			get { return extraCheckInTime; }
			set { extraCheckInTime = value; }
		}

		/// <summary>
		/// The number of hours to use for the outward stopover
		/// </summary>
		public int OutwardStopover
		{
			get { return outwardStopover; }
			set { outwardStopover = value; }
		}

		/// <summary>
		/// The number of hours to use for the return stopover
		/// </summary>
		public int ReturnStopover
		{
			get { return returnStopover; }
			set { returnStopover = value; }
		}

		/// <summary>
		/// When the location property is set, ensures the airport information
		/// is updated accordingly.
		/// </summary>
		public override TDLocation OriginLocation
		{
			get { return base.OriginLocation; }
			set 
			{ 
				base.OriginLocation = value; 

				// Convert Naptans in the location into airports
				originSelectedRegion = null;
				originSelectedAirports = BuildAirports(value);

				// Now set the location from the airports extracted - this will ensure that 
				// the description, status and naptans are all correct.
				base.OriginLocation = BuildLocation(originSelectedRegion, originSelectedAirports, false);
			}
		}

		/// <summary>
		/// When the location property is set, ensures the airport information
		/// is updated accordingly.
		/// </summary>
		public override TDLocation DestinationLocation
		{
			get { return base.DestinationLocation; }
			set 
			{ 
				// Convert Naptans in the location into airports
				destinationSelectedRegion = null;
				destinationSelectedAirports = BuildAirports(value);

				// Now set the location from the airports extracted - this will ensure that 
				// the description, status and naptans are all correct.
				base.DestinationLocation = BuildLocation(destinationSelectedRegion, destinationSelectedAirports, false);
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Sets all of the parameters to default values
		/// </summary>
		public override void Initialise()
		{
			base.Initialise();

			InitialiseGeneric();

			// TDLocations set to empty unspecified locations
			originSelectedRegion = null;
			originSelectedAirports = new Airport[0];
			destinationSelectedRegion = null;
			destinationSelectedAirports = new Airport[0];
			
			// For find a flight, change the time to Any Time.
			OutwardAnyTime = false;
			ReturnAnyTime = false;
			
		}

		public override string InputSummary()
		{

			StringBuilder summary = new StringBuilder(240);
			summary.Append("\nFrom: ");
			summary.Append(OriginLocation.Description);
			summary.Append(" ");
			summary.Append(OriginLocation.SearchType.ToString());
			summary.Append("\n");

			summary.Append("To: ");
			summary.Append(DestinationLocation.Description);
			summary.Append(" ");
			summary.Append(DestinationLocation.SearchType.ToString());
			summary.Append("\n");
			
			if (OutwardArriveBefore)
			{
				summary.Append("Outward journey: Arrving by ");

			}
			else
			{
				summary.Append("Outward journey: Leaving after ");
			}

			if (OutwardHour != "Any")
			{
				summary.Append(OutwardHour);
				summary.Append(":");
				summary.Append(OutwardMinute);
				summary.Append(" ");
			}

			else
			{
				summary.Append("Anytime");
				summary.Append(" ");
			}

			summary.Append(OutwardDayOfMonth);
			summary.Append("/");
			summary.Append(OutwardMonthYear);
			summary.Append("\n");
			
			if (IsReturnRequired)
			{
	
				if (ReturnArriveBefore)
				{
					summary.Append("Return journey: Arrving by ");

				}
				else
				{
					summary.Append("Return journey: Leaving after ");
				}

				if (ReturnHour != "Any")
				{
					summary.Append(ReturnHour);
					summary.Append(":");
					summary.Append(ReturnMinute);
					summary.Append(" ");
				}

				else
				{
					summary.Append("Anytime");
					summary.Append(" ");
				}

				summary.Append(ReturnDayOfMonth);
				summary.Append("/");
				summary.Append(ReturnMonthYear);
				summary.Append("\n");
			}
			else
			{
				summary.Append("Return journey: none");
				summary.Append("\n");
			}

			return summary.ToString();

		}

		/// <summary>
		/// Initialisation for outward or return extend journey information only
		/// </summary>
		/// <param name="originReset">Whether to initialise origin or destination information</param>
		public override void Initialise(bool originReset)
		{
			base.Initialise(originReset);

			InitialiseGeneric();

			if (originReset)
			{
				// TDLocations set to empty unspecified locations
				originSelectedRegion = null;
				originSelectedAirports = new Airport[0];
			}
			else
			{
				// TDLocations set to empty unspecified locations
				destinationSelectedRegion = null;
				destinationSelectedAirports = new Airport[0];
			}
		}

		/// <summary>
		/// Initialisation common to all other initialisation methods
		/// </summary>
		private void InitialiseGeneric()
		{
			extraCheckInTime = 0;
			outwardStopover = 0;
			returnStopover = 0;

			viaSelectedAirport = null;
			viaLocation = new TDLocation();
		}

		/// <summary>
		/// Updates the OriginSelectedRegion and OriginSelectedAirports
		/// properties with the specified data, and populates OriginLocation
		/// </summary>
		/// <param name="region"></param>
		/// <param name="airports"></param>
		public void SetOriginDetails(AirRegion region, Airport[] airports)
		{
			// Region isn't really important to anything other than the
			// user so no processing is required for this
			originSelectedRegion = region;

			// Need to build a TDLocation from the airports list
			if (airports == null)
				originSelectedAirports = new Airport[0];
			else
				originSelectedAirports = airports;

			base.OriginLocation = BuildLocation(originSelectedRegion, originSelectedAirports, false);
		}

		/// <summary>
		/// Updates the OriginSelectedAirports property with the specified data, 
		/// OriginSelectedRegion with null and populates OriginLocation
		/// </summary>
		/// <param name="region"></param>
		/// <param name="airports"></param>
		public void SetOriginDetails(Airport[] airports)
		{
			SetOriginDetails(null, airports);
		}

		/// <summary>
		/// Updates the DestinationSelectedRegion and DestinationSelectedAirports
		/// properties with the specified data, and populates DestinationLocation
		/// </summary>
		/// <param name="region"></param>
		/// <param name="airports"></param>
		public void SetDestinationDetails(AirRegion region, Airport[] airports)
		{
			destinationSelectedRegion = region;

			// Need to build a TDLocation from the airports list
			if (airports == null)
				destinationSelectedAirports = new Airport[0];
			else
				destinationSelectedAirports = airports;

			base.DestinationLocation = BuildLocation(destinationSelectedRegion, destinationSelectedAirports, false);
		}

		/// <summary>
		/// Updates the DestinationSelectedAirports property with the specified data, 
		/// DestinationSelectedRegion with null and populates DestinationLocation
		/// </summary>
		/// <param name="region"></param>
		/// <param name="airports"></param>
		public void SetDestinationDetails(Airport[] airports)
		{
			SetDestinationDetails(null, airports);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Given the selected region and airports, builds a TDLocation object. One or both
		/// of the parameters can be null. If the region is provided and airports is empty/null,
		/// then all airports for that region will be used. Otherwise the given airports will
		/// be used.
		/// </summary>
		/// <param name="region">If supplied, this is used in the event that the airports parameter
		/// is null or has 0 length to retrieve the full list of airports for the region</param>
		/// <param name="airports"></param>
		/// <returns>A TDLocation object containing all naptans for each airport, and with
		/// the description set to the names of all of the airports.</returns>
		private TDLocation BuildLocation(AirRegion region, Airport[] airports, bool isVia)
		{
			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			TDLocation location = new TDLocation();
			location.Status = TDLocationStatus.Unspecified;

			if ((region != null) && ( airports.Length == 0 ))
			{
				// No airports are selected... in this case, we assume they all are.
				airports = (Airport[])airData.GetRegionAirports(region.Code).ToArray(typeof(Airport));
			}

			// For Find-A-Flight locations, we need to use the group NaPTAN (9200XXX), 
			//  to be expanded into the appropriate individual terminals by the TTBO. 

			if (airports.Length != 0)
			{
				TDAirportNaptan[] airNaptans = new TDAirportNaptan[airports.Length];

				for (int i = 0; i < airports.Length; i++)
				{
					airNaptans[i] =  new TDAirportNaptan(new TDNaptan(airports[i].GlobalNaptan, new OSGridReference()));
				}

				location.NaPTANs = airNaptans;
				location.Description = CombineAirportNames(airports);
				location.Status = TDLocationStatus.Valid;
			}

			return location;
		}

		/// <summary>
		/// Builds an array of the airports represented by the Naptans contained 
		/// in TDLocation
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		private Airport[] BuildAirports(TDLocation location)
		{
			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			ArrayList airports = new ArrayList();
			Airport curr;
			foreach (TDNaptan naptan in location.NaPTANs)
			{
				curr = airData.GetAirportFromNaptan(naptan.Naptan);
				if (curr != null && !airports.Contains(curr))
					airports.Add(curr);
			}
			return (Airport[])airports.ToArray(typeof(Airport));
		}

		/// <summary>
		/// Given an array of airports, returns a string containing a semicolon separated list
		/// of the airport names.
		/// </summary>
		/// <param name="airports"></param>
		/// <returns></returns>
		private string CombineAirportNames(Airport[] airports)
		{
			string[] data = new string[airports.Length];
			for (int i = 0; i < airports.Length; i++)
				data[i] = airports[i].Name;
			Array.Sort(data);
			return String.Join("; ", data);
		}

		/// <summary>
		/// Converts an array of naptan strings to an array of TDAirportNaptan objects.
		/// </summary>
		/// <param name="naptans"></param>
		/// <returns></returns>
		private TDAirportNaptan[] StringsToNaptans(string[] naptans)
		{
			TDAirportNaptan[] returnData = new TDAirportNaptan[naptans.Length];
			for (int i = 0; i < naptans.Length; i++)
				returnData[i] = new TDAirportNaptan(new TDNaptan(naptans[i], new OSGridReference()));
			return returnData;
		}

		#endregion

	}
}