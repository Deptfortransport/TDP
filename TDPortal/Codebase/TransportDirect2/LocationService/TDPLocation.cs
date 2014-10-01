// *********************************************** 
// NAME             : TDPLocation.cs      
// AUTHOR           : Mark Turner
// DATE CREATED     : 21 Feb 2011
// DESCRIPTION  	: Public class provide methods to obtain 
//                    Location Information for all locations
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Text;
using ICJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// Public class provide methods to obtain 
    /// Location Information for all locations
    /// </summary>
    [Serializable()]
    public class TDPLocation : ICloneable
    {
        #region Private members

        // const values for the JourneyWeb2.1 Origin and Destination NaPTAN values;
        private const string ORIGIN_NAPTAN = "Origin";
        private const string DESTINATION_NAPTAN = "Destination";

        private string id;
        private string name;
        private string displayName;
        private string locality;
        private List<string> toids;
        private List<string> naptans;
        private string parent;
        private TDPLocationType typeOfLocation; // Generic location type, e.g. "Station"
        private TDPLocationType typeOfLocationActual; // Actual location type, e.g. "StationRail"
        private OSGridReference gridRef;
        private OSGridReference cycleGridRef;
        private LatitudeLongitude latitudeLongitude;
        private bool isGNAT;
        private bool useNaPTAN;
        private string dataSetID;
        private int adminAreaCode;
        private int districtCode;
        private bool accessible; // Review - This should be in TDPGNATLocation?
        private double distanceFromSearchOSGR; // Review - This should be in TDPGNATLocation?

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for TDPLocation that populates all member variables with default data.
        /// </summary>
        /// <returns>TDPLocation containing default data</returns>
        public TDPLocation()
        {
            Initialise();
        }

        /// <summary>
        /// Constructor for TDPLocation that populates the subset of member variables
        /// used in UI drop down lists.  The other variables default to empty or null data.
        /// </summary>
        /// <returns>TDPLocation containing default data</returns>
        public TDPLocation(string locationDisplayName, TDPLocationType locationType, TDPLocationType locationTypeActual, string identifier)
        {
            Initialise();

            // Initialise data
            this.displayName = locationDisplayName;
            this.typeOfLocation = locationType;
            this.typeOfLocationActual = locationTypeActual;
            this.id = identifier;

            //Depending on the type of record the ID may also be another field so for completeness set it
            switch (typeOfLocation)
            {
                case TDPLocationType.Venue:
                    this.naptans.Add(identifier);
                    break;
                case TDPLocationType.Station:
                case TDPLocationType.StationAirport:
                case TDPLocationType.StationCoach:
                case TDPLocationType.StationFerry:
                case TDPLocationType.StationRail:
                case TDPLocationType.StationTMU:
                    this.naptans.Add(identifier);
                    break;
                case TDPLocationType.Locality:
                    this.locality = identifier;
                    break;
                case TDPLocationType.Postcode:
                    //Do nothing for postcodes as the identifier is the displayName which has already been set
                    break;
                case TDPLocationType.Address:
                    //Do nothing for postcodes as the identifier is the displayName which has already been set
                    break;
                case TDPLocationType.StationGroup:
                    this.dataSetID = identifier;
                    break;
                case TDPLocationType.POI:
                    this.dataSetID = identifier;
                    break;
                case TDPLocationType.CoordinateEN:
                    this.gridRef  = new OSGridReference(identifier);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Constructor for TDPLocation that populates all member variables with the supplied input data.
        /// </summary>
        /// <returns>TDPLocation containing supplied data</returns>
        public TDPLocation(string locationName, string locationDisplayName, string locationLocality,
                           List<string> toids, List<string> naptans, string locationParent, 
                            TDPLocationType locationType, TDPLocationType locationTypeActual,
                            OSGridReference gridReference, OSGridReference cycleGridReference, 
                            bool isGNATStation, bool useNaptan, 
                            int adminAreaCode, int districtCode,
                            string sourceID)
        {
            //Set up member variables
            this.name = locationName;
            this.displayName = locationDisplayName;
            this.locality = locationLocality;
            this.toids = toids;
            this.naptans = naptans;
            this.parent = locationParent;
            this.typeOfLocation = locationType;
            this.typeOfLocationActual = locationTypeActual;
            this.gridRef = gridReference;
            this.cycleGridRef = cycleGridReference;
            this.isGNAT = isGNATStation;
            this.useNaPTAN = useNaptan;
            this.adminAreaCode = adminAreaCode;
            this.districtCode = districtCode;
            this.dataSetID = sourceID;
            this.latitudeLongitude = new LatitudeLongitude(0, 0);
            
            //Set unique id the field this is taken from depends on the TDPLocationType
            switch (typeOfLocation)
            {
                case TDPLocationType.Venue:
                    id = this.naptans[0];
                    break;
                case TDPLocationType.Station:
                case TDPLocationType.StationAirport:
                case TDPLocationType.StationCoach:
                case TDPLocationType.StationFerry:
                case TDPLocationType.StationRail:
                case TDPLocationType.StationTMU:
                    id = this.naptans[0];
                    break;
                case TDPLocationType.Locality:
                    id = this.locality;
                    break;
                case TDPLocationType.Postcode:
                    id = this.name;
                    break;
                case TDPLocationType.Address:
                    id = this.name;
                    break;
                case TDPLocationType.StationGroup:
                    id = this.dataSetID;
                    break;
                case TDPLocationType.POI:
                    id = this.dataSetID;
                    break;
                case TDPLocationType.CoordinateEN:
                    id = this.gridRef.ToString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Constructor for TDPLocation that takes a CJP Leg and uses the information to populate
        /// </summary>
        /// <param name="cjpLegEvent">A CJP Leg class</param>
        public TDPLocation(ICJP.Event cjpLegEvent)
        {
            Initialise();

            // Use values from the CJP leg to populate location
            if ((cjpLegEvent != null) && (cjpLegEvent.stop != null))
            {
                typeOfLocation = TDPLocationType.Station;
                
                name = (string.IsNullOrEmpty(cjpLegEvent.stop.name)) ? string.Empty : cjpLegEvent.stop.name;
                displayName = name;

                if (cjpLegEvent.stop.coordinate != null)
                {
                    gridRef = new OSGridReference(cjpLegEvent.stop.coordinate.easting, cjpLegEvent.stop.coordinate.northing);
                }

                string newNaptan = (string.IsNullOrEmpty(cjpLegEvent.stop.NaPTANID)) ? string.Empty : cjpLegEvent.stop.NaPTANID;

                // Check for valid naptan
                if (newNaptan.Equals(ORIGIN_NAPTAN) || newNaptan.Equals(DESTINATION_NAPTAN))
                {
                    newNaptan = string.Empty;
                }

                if (!string.IsNullOrEmpty(newNaptan))
                {
                    naptans.Add(newNaptan);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a hash code of this TDPLocation. 
        /// </summary>
        /// <returns></returns>
        public virtual int GetTDPHashCode()
        {
            // Does not use native GetHashCode as this can return different hash codes 
            // for instances of the "same" object.
            
            // string, int, etc return the same hashcode if they have the same value
            int hashCode = id.GetHashCode() ^ name.GetHashCode() ^ displayName.GetHashCode() ^
                locality.GetHashCode() ^ parent.GetHashCode() ^ isGNAT.GetHashCode() ^
                useNaPTAN.GetHashCode() ^ adminAreaCode.GetHashCode() ^ districtCode.GetHashCode() ^
                dataSetID.GetHashCode() ^ typeOfLocation.GetHashCode();
            
            // list object returns different instance hashcodes, so manually add
            foreach (string naptan in naptans)
            {
                hashCode = hashCode ^ naptan.GetHashCode();
            }
            foreach (string toid in toids)
            {
                hashCode = hashCode ^ toid.GetHashCode();
            }

            // OSGR object returns different instance hashcodes, so manually add
            hashCode = hashCode ^ gridRef.GetTDPHashCode() ^ cycleGridRef.GetTDPHashCode() ^ latitudeLongitude.GetTDPHashCode();

            return hashCode;
        }

        /// <summary>
        /// Returns a formatted string representing the contents of this TDPLocation
        /// </summary>
        /// <returns></returns>
        public virtual string ToString(bool htmlLineBreaks)
        {
            string linebreak = (htmlLineBreaks) ? "<br />" : string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("id: {0} {1}", id, linebreak));
            sb.AppendLine(string.Format("name: {0} {1}", name, linebreak));
            sb.AppendLine(string.Format("displayName: {0} {1}", displayName, linebreak));
            sb.AppendLine(string.Format("locality: {0} {1}", locality, linebreak));
            sb.Append("toids: ");
            foreach (string toid in toids)
            {
                sb.Append(toid);
                sb.Append(" ");
            }
            sb.AppendLine(linebreak);
            sb.Append("naptans: ");
            foreach (string naptan in naptans)
            {
                sb.Append(naptan);
                sb.Append(" ");
            }
            sb.AppendLine(linebreak);
            sb.AppendLine(string.Format("parent: {0} {1}", parent, linebreak));
            sb.AppendLine(string.Format("typeOfLocation: {0} {1}", typeOfLocation.ToString(), linebreak));
            sb.AppendLine(string.Format("typeOfLocationActual: {0} {1}", typeOfLocationActual.ToString(), linebreak));
            sb.AppendLine(string.Format("gridRef: {0},{1} {2}", gridRef.Easting, gridRef.Northing, linebreak));
            sb.AppendLine(string.Format("cycleGridRef: {0},{1} {2}", cycleGridRef.Easting, cycleGridRef.Northing, linebreak));
            sb.AppendLine(string.Format("latitudeLongitude: {0},{1} {2}", latitudeLongitude.Latitude, latitudeLongitude.Longitude, linebreak));
            sb.AppendLine(string.Format("isGNAT: {0} {1}", isGNAT.ToString(), linebreak));
            sb.AppendLine(string.Format("useNaPTAN: {0} {1}", useNaPTAN.ToString(), linebreak));
            sb.AppendLine(string.Format("adminAreaCode: {0} {1}", adminAreaCode.ToString(), linebreak));
            sb.AppendLine(string.Format("districtCode: {0} {1}", districtCode.ToString(), linebreak));
            sb.AppendLine(string.Format("dataSetID: {0} {1}", dataSetID, linebreak));

            return sb.ToString();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. ID
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Read/Write. Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Read/Write. DisplayName
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        /// <summary> 
        /// Read/Write. Locality
        /// </summary>
        public string Locality
        {
            get { return locality; }
            set { locality = value; }
        }

        /// <summary>
        /// Read/Write. Toid
        /// </summary>
        public List<string> Toid
        {
            get { return toids; }
            set { toids = value; }
        }

        /// <summary>
        /// Read/Write. Naptan
        /// </summary>
        public List<string> Naptan
        {
            get { return naptans; }
            set { naptans = value; }
        }

        /// <summary>
        /// Read/Write. Parent
        /// </summary>
        public string Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// Read/Write. TypeOfLocation
        /// </summary>
        public TDPLocationType TypeOfLocation
        {
            get { return typeOfLocation; }
            set { typeOfLocation = value; }
        }

        /// <summary>
        /// Read/Write. TypeOfLocationActual
        /// </summary>
        public TDPLocationType TypeOfLocationActual
        {
            get { return typeOfLocationActual; }
            set { typeOfLocationActual = value; }
        }

        /// <summary>
        /// Read/Write. GridRef
        /// </summary>
        public OSGridReference GridRef
        {
            get { return gridRef; }
            set { gridRef = value; }
        }

        /// <summary>
        /// Read/Write. CycleGridRef
        /// </summary>
        public OSGridReference CycleGridRef
        {
            get { return cycleGridRef; }
            set { cycleGridRef = value; }
        }

        /// <summary>
        /// Read/Write. Holds the latitude longitude value.
        /// This is currently only set for use by Cycle journeys (for GPX download)
        /// </summary>
        public LatitudeLongitude LatitudeLongitudeCoordinate
        {
            get { return latitudeLongitude; }
            set { latitudeLongitude = value; }
        }

        /// <summary>
        /// Read/Write. IsGNAT.
        /// Indicates whether accessible journeys can be planned to this location
        /// </summary> 
        public bool IsGNAT
        {
            get { return isGNAT; }
            set { isGNAT = value; }
        }

        /// <summary>
        /// Read/Write. UseNaPTAN. 
        /// </summary> 
        /// <remarks>
        /// Returns UseNaPTAN value if TDPLocationType = Venue, 
        /// otherwise ignore this property, default is false
        /// </remarks>
        public bool UseNaPTAN
        {
            get
            {
                if (typeOfLocation == TDPLocationType.Venue)
                {
                    return useNaPTAN;
                }
                return false;
            }
            set { useNaPTAN = value; }
        }

        /// <summary>
        /// Read/Write. DataSetID
        /// </summary>
        public string DataSetID
        {
            get { return dataSetID; }
            set { dataSetID = value; }
        }
        
        /// <summary>
        /// NPTG Administrative Area code for the location
        /// </summary>
        public int AdminAreaCode
        {
            get { return adminAreaCode; }
            set { adminAreaCode = value; }
        }

        /// <summary>
        /// NPTG District code for the location
        /// </summary>
        public int DistrictCode
        {
            get { return districtCode; }
            set { districtCode = value; }
        }

        /// <summary>
        /// Read/Write. Indicates location is accessible.
        /// </summary> 
        public bool Accessible
        {
            get { return accessible; }
            set { accessible = value; }
        }

        /// <summary>
        /// Distance from search OSGR, default is 0
        /// </summary>
        public double DistanceFromSearchOSGR
        {
            get { return distanceFromSearchOSGR; }
            set { distanceFromSearchOSGR = value; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialises the class to default values
        /// </summary>
        private void Initialise()
        {
            id = string.Empty;
            name = string.Empty;
            displayName = string.Empty;
            locality = string.Empty;
            toids = new List<string>();
            naptans = new List<string>();
            parent = string.Empty;
            typeOfLocation = TDPLocationType.Unknown;
            typeOfLocationActual = TDPLocationType.Unknown;
            gridRef = new OSGridReference(0, 0);
            cycleGridRef = new OSGridReference(0, 0);
            latitudeLongitude = new LatitudeLongitude(0, 0);
            isGNAT = false;
            useNaPTAN = false;
            dataSetID = string.Empty;
            adminAreaCode = 0;
            districtCode = 0;
        }

        #endregion

        #region ICloneable methods

        /// <summary>
        /// Creates a deep copy of this object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.CopyTo(new TDPLocation());
        }

        /// <summary>
        /// Creates a deep copy of this object into a TDPStopLocation type
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object Clone(TDPStopLocation target)
        {
            if (target == null)
                target = new TDPStopLocation();

            return (TDPStopLocation)this.CopyTo(target);
        }

        /// <summary>
        /// Deep copies this TDPLocation location object's values into the target location
        /// </summary>
        /// <param name="target">TDPLocation object to copy into</param>
        /// <returns>TDPLocation object copied into</returns>
        protected virtual TDPLocation CopyTo(TDPLocation target)
        {
            target.Initialise();

            target.id = this.id;
            target.name = this.name;
            target.displayName = this.displayName;
            target.locality = this.locality;

            if (this.toids != null)
                target.toids = new List<string>(this.toids);

            if (this.naptans != null)
                target.naptans = new List<string>(this.naptans);

            target.parent = this.parent;
            target.typeOfLocation = this.typeOfLocation;
            target.typeOfLocationActual = this.typeOfLocationActual;

            if (this.gridRef != null)
                target.gridRef = new OSGridReference(this.gridRef.Easting, this.gridRef.Northing);

            if (this.cycleGridRef != null)
                target.cycleGridRef = new OSGridReference(this.cycleGridRef.Easting, this.cycleGridRef.Northing);

            if (this.latitudeLongitude != null)
                target.latitudeLongitude = new LatitudeLongitude(this.latitudeLongitude.Latitude, this.latitudeLongitude.Longitude);

            target.isGNAT = this.isGNAT;
            target.useNaPTAN = this.useNaPTAN;
            target.dataSetID = this.dataSetID;
            target.adminAreaCode = this.adminAreaCode;
            target.districtCode = this.districtCode;

            return target;
        }

        #endregion
    }
}
