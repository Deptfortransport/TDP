// *********************************************** 
// NAME			: CarPark.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 07/08/2006 
// DESCRIPTION	: Class which holds all data about a Car Park
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarPark.cs-arc  $
//
//   Rev 1.2   Mar 10 2008 15:18:44   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:25:00   mturner
//Initial revision.
//
//   Rev 1.8   Oct 12 2006 15:44:38   mmodi
//Added Unknown staytype enumeration value
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4224: Car Parking: Update xsd to include an Unknown stay type
//
//   Rev 1.7   Sep 25 2006 18:13:24   mmodi
//Amended to call FindNearestITNs when Map car park and no streetname available
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4200: Car Parking: Outward journey not planned for Exeter car park
//
//   Rev 1.6   Sep 22 2006 14:44:38   mmodi
//Added public methods to retrieve coordinates
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4198: Car Parking: End point on Map for return car journey incorrect
//
//   Rev 1.5   Sep 21 2006 16:39:48   mmodi
//Corrected Get Toids request to look for street name
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4182: Car Parking: Entrance and Exit coordinates not used in journey planning
//
//   Rev 1.4   Sep 20 2006 16:29:48   mmodi
//PlanningPoint comparison strings altered to ensure true or false correctly returned
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4182: Car Parking: Entrance and Exit coordinates not used in journey planning
//
//   Rev 1.3   Sep 14 2006 11:44:56   tmollart
//Modified to allow a greater number of stay types.
//Resolution for 4189: Car Parking: StayType and Easting/Northing amendments
//
//   Rev 1.2   Aug 29 2006 10:07:44   esevern
//Changed ParkAndRideScheme to be CarParkingParkAndRideScheme
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 17 2006 15:42:50   esevern
//Amended PlanningPoint property to return a boolean after performing a comparison on the planning point string
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 08 2006 10:04:06   mmodi
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2

using System;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.LocationService
{
	public enum CarParkStayType
	{
		Short,
		Medium,
		Long,
		ShortMedium,
		MediumLong,
		ShortMediumLong,
		Unknown
	}
	
	/// <summary>
	/// Displays and stores information about a Car Park
	/// </summary>
	[Serializable()]
	public class CarPark
	{
		#region Private properties
		private readonly string carParkReference;
		private readonly string name;
		private readonly string location;
		private readonly string address;
		private readonly string postcode;
		private readonly string notes;
		private readonly string telephone;
		private readonly string carParkURL;



		private readonly int minimumCost;
		private readonly string parkAndRideIndicator;
		private readonly CarParkStayType stayType;
		private readonly string planningPoint;
		private readonly DateTime dateRecordLastUpdated;
		private readonly DateTime wefDate;
		private readonly DateTime weuDate;
		private readonly string trafficNewsRegionName;

		private readonly CarParkAccessPoint[] accessPoints;
		private readonly CarParkOperator carParkoperator;
		private readonly CarParkingParkAndRideScheme parkAndRideScheme;
		private readonly NPTGAdminDistrict[] nptgAdminAndDistrict;
        private readonly CarParkingAdditionalData additionalData;

		private int easting;
		private int northing;
		private string streetname;
		private string[] mapToids;
		private string[] entranceToids;
		private string[] exitToids;

		static private string TOID_PREFIX = "JourneyControl.ToidPrefix";

		// constants for string comparison
		private const string PLANNING_POINT_Y = "TRUE";
		private const string PLANNING_POINT_N = "FALSE";
		private const string PARKING_EXIT = "Exit";
		private const string PARKING_ENTRANCE = "Entrance";
		private const string PARKING_MAP = "Map";

		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CarPark()
		{
		}

		/// <summary>
		/// CarPark Constructor
		/// Initialise all properties from the database
		/// </summary>
		/// <param name="carParkReference">The car park reference</param>
		/// <param name="name">Name of the car park</param>
		/// <param name="location">Car park location</param>
		/// <param name="address">Car park address</param>
		/// <param name="postcode">Car park postcode</param>
		/// <param name="notes">Additional comments</param>
		/// <param name="telephone">Car park telephone</param>
		/// <param name="carParkURL">Car park URL</param>
		/// <param name="minimumCost">Minimum cost of car park (in pence)</param>
		/// <param name="parkAndRideIndicator">Is car park a park and ride location</param>
		/// <param name="stayType">Short, Medium, or Long stay</param>
		/// <param name="planningPoint">Yes or No to indicate is Map coordinates are used</param>
		/// <param name="dateRecordLastUpdated">Record last updated</param>
		/// <param name="wefDate">Car park record With effect from date </param>
		/// <param name="weuDate">Car park record With effect until date</param>
		/// <param name="trafficNewsRegionName">Name of the Traffic News Region of the Car Park location</param>
		/// <param name="accessPoints">Array of Access Points of map </param>
		/// <param name="carParkOperator">Car park Operator</param>
		/// <param name="parkAndRideScheme">Park and ride scheme</param>
		/// <param name="nptgAdminDistrict">Array of NPTG admin/district codes</param>
		public CarPark(	string carParkReference,
						string name, 
						string location, 
						string address, 
						string postcode,
						string notes, 
						string telephone, 
						string carParkURL, 
						int minimumCost,
						string parkAndRideIndicator,
						CarParkStayType stayType, 
						string planningPoint, 
						DateTime dateRecordLastUpdated, DateTime wefDate, DateTime weuDate,
						string trafficNewsRegionName,
						CarParkAccessPoint[] accessPoints, 
						CarParkOperator carParkOperator, 
						CarParkingParkAndRideScheme parkAndRideScheme, 
						NPTGAdminDistrict[] nptgAdminDistrict,
                        CarParkingAdditionalData additionalData)
		{
			this.carParkReference = carParkReference;
			this.name = name;
			this.location = location;
			this.address = address;
			this.postcode = postcode;
			this.notes = notes;
			this.telephone = telephone;
			this.carParkURL = carParkURL;
			this.minimumCost = minimumCost;
			this.parkAndRideIndicator = parkAndRideIndicator;
			this.stayType = stayType;
			this.planningPoint = planningPoint;
			this.dateRecordLastUpdated = dateRecordLastUpdated;
			this.wefDate = wefDate;
			this.weuDate = weuDate;
			this.trafficNewsRegionName = trafficNewsRegionName;
		
			this.accessPoints = accessPoints;
			this.carParkoperator = carParkOperator;
			this.parkAndRideScheme = parkAndRideScheme;
			this.nptgAdminAndDistrict = nptgAdminDistrict;
            this.additionalData = additionalData;
		}

		#endregion

		#region Car Park Public Properties
        public CarParkingAdditionalData CarParkAdditionalData
        {
            get { return additionalData; }
        }
        
        /// <summary>
		/// Read only property. Get the car park reference
		/// </summary>
		public string CarParkReference
		{
			get {return carParkReference;}
		}

		/// <summary>
		/// Read only property. Get the car park name
		/// </summary>
		public string Name
		{
			get {return name;}
		}

		/// <summary>
		/// Read only property. Get the car park location
		/// </summary>
		public string Location
		{
			get { return location; }
		}

		/// <summary>
		/// Read only property. Get the car park address
		/// </summary>
		public string Address
		{
			get { return address; }
		}

		/// <summary>
		/// Read only property. Get the car park postcode
		/// </summary>
		public string Postcode
		{
			get { return postcode; }
		}

		/// <summary>
		/// Read only property - gets any associated comments
		/// </summary>
		public string Notes
		{
			get { return notes; }
		}

		/// <summary>
		/// Read only property. Get the car park telephone
		/// </summary>
		public string Telephone
		{
			get { return telephone; }
		}

		/// <summary>
		/// Read only property. Get the URL link
		/// </summary>
		public string CarParkURL
		{
			get 
			{	
				if (carParkURL == string.Empty)
				{
					return null;
				}
				else
				{
					return carParkURL;
				}
			}
		}

		/// <summary>
		/// Read only property. Returns the minimum cost in pence. The MinimumCost is -1 if the cost is not
		/// known and 0 if parking is free
		/// </summary>
		public int MinimumCost
		{
			get { return minimumCost; }
		}		

		/// <summary>
		/// Read only property. Get the Park and Ride indicator
		/// </summary>
		public string ParkAndRideIndicator
		{
			get { return parkAndRideIndicator; }
		}

		/// <summary>
		/// Read only property. Get car park stay type
		/// </summary>
		public CarParkStayType StayType
		{
			get { return stayType; }
		}

		/// <summary>
		/// Read only property. Get the planning point indicator
		/// </summary>
		public bool PlanningPoint
		{
			get 
			{ 
				// compare the strings ignoring case
				if(string.Compare(planningPoint.Trim().ToUpper(),PLANNING_POINT_Y,true) == 0) 
					return true;
				else
					return false; 
			}
		}

		/// <summary>
		/// Read only property. Get the record last updated date
		/// </summary>
		public DateTime DateRecordLastUpdated
		{
			get { return dateRecordLastUpdated; }
		}

		/// <summary>
		/// Read only property. Get the record with effect from date
		/// </summary>
		public DateTime WEFDate
		{
			get { return wefDate; }
		}

		/// <summary>
		/// Read only property. Get the record with effect until date
		/// </summary>
		public DateTime WEUDate
		{
			get { return weuDate; }
		}

		/// <summary>
		/// Readonly property. Traffic News Region value.
		/// </summary>
		public string TrafficNewsRegionName
		{
			get { return trafficNewsRegionName; }
		}

		#endregion

		#region Custom user type Public Properties
		/// <summary>
		/// Readonly property. Access points value.
		/// </summary>
		public CarParkAccessPoint[] AccessPoints
		{
			get { return accessPoints; }
		}

		/// <summary>
		/// Readonly property. Car Park operator
		/// </summary>
		public CarParkOperator CarParkOperator
		{
			get { return carParkoperator; }
		}

		/// <summary>
		/// Readonly property. Park and ride scheme.
		/// </summary>
		public CarParkingParkAndRideScheme ParkAndRideScheme
		{
			get { return parkAndRideScheme; }
		}

		/// <summary>
		/// Readonly property. Nptg Admin and District.
		/// </summary>
		public NPTGAdminDistrict[] NptgAdminAndDistrict
		{
			get { return nptgAdminAndDistrict; }
		}

		#endregion

		#region Toids Public Property

		/// <summary>
		/// Returns a clone of the array of Map toids
		/// </summary>
		public string[] GetMapToids()
		{
			if (mapToids == null)
			{
				//look up TOIDs first time required
				mapToids = LookUpCarParkTOIDs(PARKING_MAP);
			}
			return (string[])mapToids.Clone();
		}

		/// <summary>
		/// Returns a clone of the array of Entrance toids
		/// </summary>
		public string[] GetEntranceToids()
		{
			if (entranceToids == null)
			{
				//look up TOIDs first time required
				entranceToids = LookUpCarParkTOIDs(PARKING_ENTRANCE);
			}
			return (string[])entranceToids.Clone();
		}

		/// <summary>
		/// Returns a clone of the array of Exit toids
		/// </summary>
		public string[] GetExitToids()
		{
			if (exitToids == null)
			{
				//look up TOIDs first time required
				exitToids = LookUpCarParkTOIDs(PARKING_EXIT);
			}
			return (string[])exitToids.Clone();
		}
        
		#endregion

		#region Private methods

		/// <summary>
		/// Look up TOIDs by car park grid reference dependent on the Access Point to use
		/// </summary>
		private string[] LookUpCarParkTOIDs(string geocode)
		{
			string toidPrefix = Properties.Current[TOID_PREFIX];

			if	(toidPrefix == null) 
			{
				toidPrefix = string.Empty;
			}

			//populate the internal TOIDs array by calling a method from GisQuery
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			//establish coordinates to search on
			foreach (CarParkAccessPoint accessPoint in accessPoints)
			{
				if (accessPoint.GeocodeType == geocode)
				{
					easting = accessPoint.GridReference.Easting;
					northing = accessPoint.GridReference.Northing;
					streetname = accessPoint.StreetName;
				}
			}
			
			QuerySchema gisResult = new QuerySchema();

			// Only do a street name search if it's an entrance or exit car park, 
			// and the street name is available
			if (geocode == PARKING_MAP)
				gisResult = gisQuery.FindNearestITNs(easting, northing);    
			else
			{
				if (streetname == string.Empty)
					gisResult = gisQuery.FindNearestITNs(easting, northing);    
				else
					gisResult = gisQuery.FindNearestITN(easting, northing, streetname, true);
			}
			
			string[] toids = new string[gisResult.ITN.Rows.Count];

			for ( int i=0; i < gisResult.ITN.Rows.Count; i++)
			{
				QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
				if	((toidPrefix.Length > 0) && !(row.toid.StartsWith(toidPrefix)))
				{
					toids[i] = toidPrefix + row.toid;
				}
				else
				{
					toids[i] = row.toid;
				}
			}

			return toids;
		}

		/// <summary>
		/// Returns the specified gridreference from AccessPoints array
		/// </summary>
		private OSGridReference GetGridReference(string geocode)
		{
			OSGridReference gridreference = new OSGridReference();

			foreach (CarParkAccessPoint accessPoint in accessPoints)
			{
				if (accessPoint.GeocodeType == geocode)
					gridreference = accessPoint.GridReference;
			}

			return gridreference;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Returns the Map gridreference from AccessPoints array
		/// </summary>
		public OSGridReference GetMapGridReference()
		{
			return GetGridReference(PARKING_MAP);
		}

		/// <summary>
		/// Returns the Entrance gridreference from AccessPoints array
		/// </summary>
		public OSGridReference GetEntranceGridReference()
		{
			return GetGridReference(PARKING_ENTRANCE);
		}

		/// <summary>
		/// Returns the Exit gridreference from AccessPoints array
		/// </summary>
		public OSGridReference GetExitGridReference()
		{
			return GetGridReference(PARKING_EXIT);
		}

		#endregion
	}
}
