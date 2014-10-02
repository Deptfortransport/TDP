// *********************************************** 
// NAME                 : PlaceDataProvider
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/10/2004
// DESCRIPTION  : Provides place data to important place gazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/PlaceDataProvider.cs-arc  $ 
//
//   Rev 1.3   Mar 10 2008 15:18:58   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory   Feb 13 2008 14:00:00   mmodi
//Updated to only populate toids when get place is called, rather than during initial data load, performance enhancement
//
//   Rev 1.0   Nov 08 2007 12:25:18   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//DatabasePlaceDataLoader updated to also load car planning points (TOIDs)
//for City to City, including checking whether outward and return 
//time property values sit within start and end time range of planning point
//
//   Rev 1.6   Mar 30 2006 12:17:12   build
//Automatically merged from branch for stream0018
//
//   Rev 1.5.1.0   Feb 16 2006 16:36:02   mguney
//LoadPlaces method change to populate cityInterchangeList for the TDLocation for each naptan.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.5   Nov 15 2005 18:28:46   RPhilpott
//Add new GetNaptan() method to support getting Naptan details from teh City-to-City database.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.4   Mar 23 2005 12:11:58   jgeorge
//Updated commenting
//
//   Rev 1.3   Mar 23 2005 11:57:58   jgeorge
//Added code to populate UseForFareEnquiries field on TDNaptan objects
//
//   Rev 1.2   Nov 18 2004 09:13:00   jgeorge
//Updated commenting
//
//   Rev 1.1   Nov 01 2004 17:35:50   jgeorge
//Correction - new TDLocation objects should have their status set to Valid
//
//   Rev 1.0   Nov 01 2004 15:46:08   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using System.Xml;
using System.IO;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Provides place data to important place gazetteer
	/// </summary>
	public class PlaceDataProvider : IPlaceDataProvider
	{
		#region Private variables

		private Hashtable choiceLists;
		private Hashtable locations;
		private Hashtable naptans;

		private IPlaceDataLoadingStrategy loadingStrategy;

		#endregion

		#region Constructor

		/// <summary>
		/// Internal constructor ensuring that objects of this class can't be created from
		/// outside the namespace. Triggers a load of all the data.
		/// </summary>
		internal PlaceDataProvider(IPlaceDataLoadingStrategy loadingStrategy)
		{
			this.loadingStrategy = loadingStrategy;
			LoadDataCache();
		}

		#endregion

		#region Implementation of IPlaceDataProvider

		/// <summary>
		/// Returns all places for the specified place type in a LocationChoiceList object
		/// </summary>
		public LocationChoiceList GetPlaces(PlaceType placeType)
		{
			// Retrieve the value from the hashtable
			if (choiceLists.ContainsKey(placeType))
			{
				return (LocationChoiceList)choiceLists[placeType];
			}
			else
			{
				return new LocationChoiceList();
			}
		}

		/// <summary>
		/// Returns the place with the given TDNPGID in a TDLocation object
		/// </summary>
		public TDLocation GetPlace(string TDNPGID)
		{
			if (locations.ContainsKey(TDNPGID))
            {
                TDLocation location = (TDLocation)locations[TDNPGID];

                if (location.Toid.Length <= 0)
                {
                    // Load the TOIDs for location
                    loadingStrategy.LoadTOIDForPlace(ref location, TDNPGID);

                    // Save it to the hashtable for future use
                    locations.Remove(TDNPGID);
                    locations.Add(TDNPGID, location);
                }

                return location;
            }
			else
				return null;
		}

		/// <summary>
		/// Returns a NaPTAN with the given naptan string in a TDNaptan object
		/// </summary>
		public TDNaptan GetNaptan(string naptan)
		{
			if (naptans.ContainsKey(naptan))
			{
				return (TDNaptan)naptans[naptan];
			}
			else
			{
				return null;
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Loads all data from the database
		/// </summary>
		private void LoadDataCache()
		{
			Hashtable newChoiceLists;
			Hashtable newLocations;
			Hashtable newNaptans;

			loadingStrategy.LoadPlaces(out newChoiceLists, out newLocations, out newNaptans);

			// Lock the object and replace old values with new
			lock(this)
			{
				choiceLists = newChoiceLists;
				locations = newLocations;
				naptans = newNaptans;
			}
		}

		#endregion

	}

	/// <summary>
	/// Implementation of IPlaceDataLoader that will load data from a database
	/// </summary>
	public class DatabasePlaceDataLoader : IPlaceDataLoadingStrategy
	{
		#region Constants

		/// <summary>
		/// Initial size to create the choiceLists hashtable at
		/// </summary>
		private const int initialSizeChoiceLists = 200;

		/// <summary>
		/// Initial size to create the locations hashtable at
		/// </summary>
		private const int initialSizeLocations = 1000;

		/// <summary>
		/// Initial size to create the naptans hashtable at
		/// </summary>
		private const int initialSizeNaptans = 5000;

		/// <summary>
		/// Name of the stored proc used for loading all places
		/// </summary>
		private const string storedProcNameGetAllPlaces = "GetPlaces";

		/// <summary>
		/// Name of the stored proc used for loading specific place info
		/// </summary>
		private const string storedProcNameGetPlaceTimeData = "GetPlaceNaptans";

        /// <summary>
        /// Name of the stored proc used for returning car points based on TDNPGID
        /// </summary>
        private const string storedProcNameGetPlaceLocationData = "GetPlaceLocations";

		#endregion

		#region Private variables

		SqlHelperDatabase sourceDatabase; 

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sourceDatabase">Database containing the data. Must contain the expected stored procedures.</param>
		public DatabasePlaceDataLoader(SqlHelperDatabase sourceDatabase)
		{
			this.sourceDatabase = sourceDatabase;
		}

		#endregion

		#region Implementation of IPlaceDataLoadingStrategy

		/// <summary>
		/// LoadPlaces method loads data into the hashtables in the expected format
		/// </summary>
		/// <param name="placeLists"></param>
		/// <param name="locations"></param>
		/// <param name="naptans"></param>
		public void LoadPlaces(out Hashtable placeLists, out Hashtable locations, out Hashtable naptans)
		{
			placeLists = new Hashtable(initialSizeChoiceLists);
			locations = new Hashtable(initialSizeLocations);
			naptans = new Hashtable(initialSizeNaptans);
			
			SqlDataReader reader;

			// Get all PlaceType values
			PlaceType[] placeTypes = (PlaceType[])Enum.GetValues(typeof(PlaceType));

			// Create hashtables containing parameters and data types for the stored procs
			Hashtable getAllPlacesValues = new Hashtable(1);
			Hashtable getAllPlacesTypes = new Hashtable(1);

			getAllPlacesValues.Add("@PlaceType", null);
			getAllPlacesTypes.Add("@PlaceType", SqlDbType.VarChar);

			Hashtable getPlaceTimeDataValues = new Hashtable(1);
			Hashtable getPlaceTimeDataTypes = new Hashtable(1);

			getPlaceTimeDataValues.Add("@TDNPGID", null);
			getPlaceTimeDataTypes.Add("@TDNPGID", SqlDbType.Int);

			//Initialise the SQL Helper class
			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen(sourceDatabase);


			foreach (PlaceType currentType in placeTypes)
			{
				// Create a location choice list to store the data in
				LocationChoiceList places = new LocationChoiceList();

				// Execute the stored proc for this place
				getAllPlacesValues["@PlaceType"] = currentType.ToString();
				reader = sqlHelper.GetReader(storedProcNameGetAllPlaces, getAllPlacesValues, getAllPlacesTypes);
				
				// Go through the reader and create the objects for each row
				while (reader.Read())
				{
					// Create the location choice object
					places.Add(new LocationChoice(reader.GetString(2),
						false,
						currentType.ToString(),
						reader.GetInt32(0).ToString(),
						new OSGridReference(),
						string.Empty,
						0.0,
						reader.GetString(3),
						string.Empty,
						false));
				}
				reader.Close();

				// Add to the hashtable
				placeLists.Add(currentType, places);

                
                // Now go through each object in that list and get the information to build a
				// TDLocation for it
				foreach (LocationChoice currentChoice in places)
				{
					getPlaceTimeDataValues["@TDNPGID"] = Convert.ToInt32(currentChoice.PicklistValue);

					TDLocation newLocation = new TDLocation();
					newLocation.Description = currentChoice.Description;
					
					ArrayList naptanList = new ArrayList();
					ArrayList cityInterchangeList = new ArrayList();

                    #region Naptans
                    reader = sqlHelper.GetReader(storedProcNameGetPlaceTimeData, getPlaceTimeDataValues, getPlaceTimeDataTypes);

					while (reader.Read())
					{
						//populate the naptan list
						TDNaptan naptan = new TDNaptan(reader.GetString(0), 
							new OSGridReference(reader.GetInt32(2), 
							reader.GetInt32(3)), 
							currentChoice.Description, 
							reader.GetString(4) == "Y",
							currentChoice.Locality);

						naptanList.Add(naptan);
						//populate the city interchange list
						bool useDirectAir = reader.GetBoolean(5);
						bool useCombinedAir = reader.GetBoolean(6);
						//get the modes (comma separated)
						object modesString = reader.GetValue(7);
						ModeType[] modeTypes;
						
						if ((modesString == DBNull.Value) || (((string)modesString).Length == 0))							
							modeTypes = new ModeType[0];
						else
						{
							string[] modes = ((string)modesString).Split(new char[] {','});
							modeTypes = new ModeType[modes.Length];
							for (int i=0;i < modes.Length;i++)
							{
								modeTypes[i] = (ModeType)Enum.Parse(typeof(ModeType),modes[i],true);
							}
						}

						cityInterchangeList.Add(
							CityInterchange.CreateCityInterchange(modeTypes,useDirectAir,useCombinedAir,naptan));
						
					}

					reader.Close();
                    #endregion

                    #region Set the newlocation values
                    newLocation.NaPTANs = (TDNaptan[])naptanList.ToArray(typeof(TDNaptan));
					newLocation.CityInterchanges = 
						(CityInterchange[])cityInterchangeList.ToArray(typeof(CityInterchange));
					newLocation.Locality = currentChoice.Locality;
					newLocation.Status = TDLocationStatus.Valid;

                    #endregion

                    locations.Add(currentChoice.PicklistValue, newLocation);

					// now add naptans to the naptan hashtable 
					//  to support naptan-keyed lookup 

					foreach (TDNaptan naptan in naptanList)
					{
						if	(!naptans.ContainsKey(naptan.Naptan))
						{
							naptans.Add(naptan.Naptan, naptan);
						}
					}
				}
			}
		}

        /// <summary>
        /// Populates the location with TOIDs, returns the location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tdnpgid"></param>
        public void LoadTOIDForPlace(ref TDLocation location, string tdnpgid)
        {
            ArrayList toidList = new ArrayList();
            OSGridReference gridReference = new OSGridReference();

            // Initialise the SQL Helper class
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.ConnOpen(sourceDatabase);

            // Set the parameters
            Hashtable getPlaceTimeDataValues = new Hashtable(1);
            Hashtable getPlaceTimeDataTypes = new Hashtable(1);

            getPlaceTimeDataValues.Add("@TDNPGID", null);
            getPlaceTimeDataTypes.Add("@TDNPGID", SqlDbType.Int);

            // Set the location id to get
            getPlaceTimeDataValues["@TDNPGID"] = Convert.ToInt32(tdnpgid);

            // Execute storedprocedure
            SqlDataReader reader = sqlHelper.GetReader(storedProcNameGetPlaceLocationData, getPlaceTimeDataValues, getPlaceTimeDataTypes);

            while (reader.Read())
            {
                // Check start time and end time validity
                // Note. We can only check them against the default configured times in the Properties table
                // because at this point we cannot obtain the user entered times. This logic may need to be considered
                // fully in the future if needed
                string startTime = reader.GetInt32(3).ToString().PadLeft(4, '0');
                string endTime = reader.GetInt32(4).ToString().PadLeft(4, '0');

                if (ValidateLocationDateTime(startTime, endTime))
                {
                    // Get the easting and northing values from the reader           
                    int easting = reader.GetInt32(1);
                    int northing = reader.GetInt32(2);

                    // Create the OSGridReference because car journeys need an OSGR
                    gridReference = new OSGridReference(easting, northing);

                    // Convert to a TOID by calling the method from GisQuery and add to the array list
                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    QuerySchema gisResult = new QuerySchema();
                    gisResult = gisQuery.FindNearestITNs(easting, northing);

                    for (int i = 0; i < gisResult.ITN.Rows.Count; i++)
                    {
                        QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
                        toidList.Add(row.toid);
                    }
                }

            }

            // Clean up
            reader.Close();

            if (sqlHelper.ConnIsOpen)
                sqlHelper.ConnClose();

            
            // Only add Car Toids and GridReference if there are some defined
            if (toidList.Count > 0)
            {
                location.Toid = (string[])toidList.ToArray(typeof(string));
                location.GridReference = gridReference;
            }
        }

		#endregion

        #region Private methods

        /// <summary>
        /// Returns true, if the database configure journey time is within the time range values provided
        /// </summary>
        /// <param name="startTime">The start time range value</param>
        /// <param name="endTime">The end time range value</param>
        /// <returns></returns>
        private bool ValidateLocationDateTime(string startTime, string endTime)
        {
            DateTime now = DateTime.Now;

            // Get the Car Journey property date/times
            string carOutwardTime = Properties.Current["CityToCity.CarJourney.OutwardTime"];
            string carReturnTime = Properties.Current["CityToCity.CarJourney.ReturnTime"];

            DateTime outwardJourneyDateTime = new DateTime(now.Year, now.Month, now.Day, int.Parse(carOutwardTime.Substring(0, 2)), int.Parse(carOutwardTime.Substring(2, 2)), 0);
            DateTime returnJourneyDateTime = new DateTime(now.Year, now.Month, now.Day, int.Parse(carReturnTime.Substring(0, 2)), int.Parse(carReturnTime.Substring(2, 2)), 0);

            // Get the Location start and end date/times
            DateTime rangeStartDateTime = new DateTime(now.Year, now.Month, now.Day, int.Parse(startTime.Substring(0, 2)), int.Parse(startTime.Substring(2, 2)), 0);

            DateTime rangeEndDateTime = new DateTime();
            if (endTime == "2400")
            {
                DateTime tomorrow = DateTime.Now.AddDays(1);
                rangeEndDateTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0);
            }
            else
            {
                rangeEndDateTime = new DateTime(now.Year, now.Month, now.Day, int.Parse(endTime.Substring(0, 2)), int.Parse(endTime.Substring(2, 2)), 0);
            }

            // If the property start times are within the car location time range, then ok to use
            if (((outwardJourneyDateTime > rangeStartDateTime) && (outwardJourneyDateTime < rangeEndDateTime))
                &&
                ((returnJourneyDateTime > rangeStartDateTime) && (returnJourneyDateTime < rangeEndDateTime)))
            {
                return true;

            }
            else
            { 
                return false;
            }
        }

        #endregion

    }

	/// <summary>
	/// Implementation of IPlaceDataLoader that will load data from an XML file
	/// </summary>
	public class FilePlaceDataLoader : IPlaceDataLoadingStrategy
	{
		#region Private variables

		string filePath;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="filePath">The path of the file containing place data to load</param>
		public FilePlaceDataLoader(string filePath)
		{
			this.filePath = filePath;
		}

		#endregion

		#region Implementation of IPlaceDataLoadingStrategy

		/// <summary>
		/// LoadPlaces method loads data into the hashtables in the expected format
		/// </summary>
		/// <param name="placeLists"></param>
		/// <param name="locations"></param>
		/// <param name="naptans"></param>
		public void LoadPlaces(out Hashtable placeLists, out Hashtable locations, out Hashtable naptans)
		{
			placeLists = new Hashtable();
			locations = new Hashtable();
			naptans = new Hashtable();

			XmlDocument document = new XmlDocument();
			document.Load(filePath);

			PlaceType[] placeTypes = (PlaceType[])Enum.GetValues(typeof(PlaceType));

			foreach (PlaceType currentType in placeTypes)
			{
				LocationChoiceList list = new LocationChoiceList();
				XmlNodeList placeNodes = document.SelectNodes("//places/place[@type='" + currentType.ToString() + "']");
				foreach (XmlNode node in placeNodes)
				{
					LocationChoice newLocationChoice = new LocationChoice(node.Attributes["name"].Value, false, string.Empty, node.Attributes["id"].Value, new OSGridReference(), string.Empty, 0, node.Attributes["locality"].Value, string.Empty, false);
					list.Add(newLocationChoice);
					// Create a TDLocation
					TDLocation newLocation = new TDLocation();
					newLocation.Description = newLocationChoice.Description;
					newLocation.Locality = newLocationChoice.Locality;

					// Load the naptans
					ArrayList naptanList = new ArrayList();

					XmlNodeList xmlNaptans = node.SelectNodes("./naptan-time-relationship/naptan");
					
					foreach (XmlNode naptanNode in xmlNaptans)
					{
						naptanList.Add( new TDNaptan( naptanNode.Attributes["id"].Value, 
							new OSGridReference(Convert.ToInt32(naptanNode.Attributes["osgr-easting"].Value), Convert.ToInt32(naptanNode.Attributes["osgr-northing"].Value)), 
							string.Empty, 
							naptanNode.Attributes["use-for-fare-enquiries"].Value == "Y",
							newLocation.Locality) );
					}

					newLocation.NaPTANs = (TDNaptan[])naptanList.ToArray(typeof(TDNaptan));
					newLocation.Status = TDLocationStatus.Valid;
					locations.Add(newLocationChoice.PicklistValue, newLocation);

					foreach (TDNaptan naptan in naptanList)
					{
						if	(!naptans.ContainsKey(naptan.Naptan))
						{
							naptans.Add(naptan.Naptan, naptan);
						}
					}
				}
				placeLists.Add(currentType, list);
			}
		}

        /// <summary>
        /// Populates the location with TOIDs, returns the location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tdnpgid"></param>
        public void LoadTOIDForPlace(ref TDLocation location, string id)
        {
            // Not implemented
        }
		#endregion

	}

}
