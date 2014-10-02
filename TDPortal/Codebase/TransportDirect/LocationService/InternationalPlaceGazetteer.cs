// *********************************************** 
// NAME                 : InternationalPlaceGazetteer
// AUTHOR               : Rich Broddle
// DATE CREATED         : 10/02/2010
// DESCRIPTION	        : Implementation of the ITDGazetteer interface for international planner
//                      : works in a similar way to the city-city gazetteer.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/InternationalPlaceGazetteer.cs-arc  $ 
//
//   Rev 1.4   Mar 02 2010 16:46:08   mmodi
//Removed unused variable causing a warning
//
//   Rev 1.3   Feb 18 2010 17:03:40   mmodi
//Updated following database change
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 16 2010 14:39:50   mmodi
//Corrected load issues and and added location details needed for International journey planning
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 11 2010 14:27:32   rbroddle
//Minor corrections
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 10 2010 17:01:46   rbroddle
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//


using System;

using TransportDirect.Common.Logging;
using TransportDirect.Common;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Collections;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Implementation of ITDGazetteer which retrieves data for international city locations
	/// </summary>
	[Serializable]
	public class InternationalPlaceGazetteer : ITDGazetteer
    {

        #region Private variables

		private bool userLoggedOn;
		private string sessionID;
        private LocationChoiceList internationalChoiceList;
        private Hashtable locationChoiceCountries; // Temp value to use when creating TDLocations for each choice list object
        private Hashtable locations;
        private Hashtable naptans;

        #endregion 

        /// <summary>
        /// Name of the stored proc used for loading all places
        /// </summary>
        private const string storedProcNameGetAllInternationalCities = "GetAllInternationalCities";

        /// <summary>
        /// Name of the stored proc used for loading specific place info
        /// </summary>
        private const string storedProcNameGetInternationalStopsForCity = "GetInternationalStopsForCity";

		/// <summary>
		/// Constructor
		/// </summary>
	    public InternationalPlaceGazetteer(string sessionID, bool userLoggedOn)
		{
			this.userLoggedOn = userLoggedOn;
			this.sessionID = sessionID;

            SqlHelperDatabase sourceDatabase = SqlHelperDatabase.InternationalDataDB; 
            SqlDataReader reader;

            //Setup hash tables for param to pass to storedProcNameGetInternationalStopsForCity
            Hashtable getInternationalStopDataValues = new Hashtable(1);
            Hashtable getInternationalStopDataTypes = new Hashtable(1);
            getInternationalStopDataValues.Add("@CityID", null);
            getInternationalStopDataTypes.Add("@CityID", SqlDbType.Int);

            //Initialise the SQL Helper class
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.ConnOpen(sourceDatabase);

            // instantiate a location choice list to store the data in
            internationalChoiceList = new LocationChoiceList();
            locationChoiceCountries = new Hashtable();

            reader = sqlHelper.GetReader(storedProcNameGetAllInternationalCities, new Hashtable());

            // Go through the reader and create the objects for each row
            while (reader.Read())
            {
                // Create the location choice object (which equates to an International City)
                internationalChoiceList.Add
                    (new LocationChoice(reader.GetString(1),
                    false,
                    String.Empty,
                    Convert.ToString(reader.GetInt32(0)), // The City ID (used as the Picklist value)
                    new OSGridReference(reader.GetInt32(3), reader.GetInt32(4)),
                    string.Empty,
                    0.0,
                    reader.GetString(5),
                    string.Empty,
                    false));

                // Create the country this International City is for (needed for when creating the TDLocation
                TDCountry country = new TDCountry();
                country.CountryCode = reader.GetString(5);
                country.IANACode = reader.GetString(6);
                country.AdminCodeUIC = reader.GetString(7);
                country.TimeZone = Convert.ToDouble(reader.GetInt16(8));

                locationChoiceCountries.Add(Convert.ToString(reader.GetInt32(0)), country);
            }
            reader.Close();

            // Reset the class locations and naptans list
            locations = new Hashtable();
            naptans = new Hashtable();

            // Now go through each object in that list and get the information to build a
            // TDLocation for it
            foreach (LocationChoice currentChoice in internationalChoiceList)
            {
                getInternationalStopDataValues["@CityID"] = Convert.ToInt32(currentChoice.PicklistValue);

                TDLocation newLocation = new TDLocation();
                newLocation.Description = currentChoice.Description;

                ArrayList naptanList = new ArrayList();
                ArrayList cityInterchangeList = new ArrayList();

                #region Naptans
                reader = sqlHelper.GetReader(storedProcNameGetInternationalStopsForCity, getInternationalStopDataValues, getInternationalStopDataTypes);

                while (reader.Read())
                {
                    //populate the naptan list
                    TDNaptan naptan = new TDNaptan(reader.GetString(1),
                        new OSGridReference(reader.GetInt32(4),
                        reader.GetInt32(5)),
                        reader.GetString(3),
                        false,
                        currentChoice.Locality);

                    naptanList.Add(naptan);
                }
                reader.Close();
                #endregion

                
                #region Set the newlocation values
                
                newLocation.NaPTANs = (TDNaptan[])naptanList.ToArray(typeof(TDNaptan));
                newLocation.Locality = currentChoice.Locality;
                newLocation.Country = (TDCountry)locationChoiceCountries[currentChoice.PicklistValue];
                newLocation.CityId = currentChoice.PicklistValue;
                newLocation.GridReference = currentChoice.OSGridReference;
                newLocation.Status = TDLocationStatus.Valid;

                #endregion

                // Add to the locations list
                locations.Add(currentChoice.PicklistValue, newLocation);

                // now add naptans to the naptan hashtable 
                // to support naptan-keyed lookup 
                foreach (TDNaptan naptan in naptanList)
                {
                    if (!naptans.ContainsKey(naptan.Naptan))
                    {
                        naptans.Add(naptan.Naptan, naptan);
                    }
                }
            }

            // No longer need the countries data
            locationChoiceCountries = null;
	    }

		#region Implementation of ITDGazetteer
		/// <summary>
		/// Returns a list of places. Both parameters are ignored
		/// </summary>
		/// <param name="text"></param>
		/// <param name="fuzzy"></param>
		/// <returns></returns>
		public LocationQueryResult FindLocation(string text, bool fuzzy)
		{
			LocationQueryResult result = new LocationQueryResult(string.Empty);
            result.LocationChoiceList = internationalChoiceList;
			return result;
		}

		/// <summary>
		/// This method will always throw an error, as no LocationChoice objects returned
		/// from previous searches will have children
		/// </summary>
		/// <param name="text"></param>
		/// <param name="fuzzy"></param>
		/// <param name="pickList"></param>
		/// <param name="queryRef"></param>
		/// <param name="choice"></param>
		/// <returns></returns>
		public LocationQueryResult DrillDown (string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice)
		{
			// Throw an error. We can never drilldown using this gazetteer
			OperationalEvent oe = new OperationalEvent(
				TDEventCategory.Business,
				sessionID,
				TDTraceLevel.Error,
				"Unable to drill into a choice without children");

			Trace.Write(oe);
			throw new TDException("Unable to drill into a choice without children", true, TDExceptionIdentifier.LSAddressDrillLacksChildren);
		}

		/// <summary>
		/// Populates the TDLocation object. All parameters but choice are ignored
		/// </summary>
		/// <param name="location"></param>
		/// <param name="text"></param>
		/// <param name="fuzzy"></param>
		/// <param name="pickList"></param>
		/// <param name="queryRef"></param>
		/// <param name="choice"></param>
		/// <param name="maxDistance"></param>
		/// <param name="disableGisQuery"></param>
		/// <param name="localityRequired"></param>
		public void GetLocationDetails (ref TDLocation location, string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice, int maxDistance, bool disableGisQuery)
		{
			location = (TDLocation)locations[choice.PicklistValue];
		}

		/// <summary>
		/// Does nothing, as TDLocation objects retrieved using this Gazetteer will always
		/// be fully populated
		/// </summary>
		/// <param name="location"></param>
		public void PopulateLocality(ref TDLocation location)
		{
			return;
		}

		/// <summary>
		/// Does nothing, Toids are irrelevant to this Gazetteer
		/// </summary>
		/// <param name="location"></param>
		public void PopulateToids(ref TDLocation location)
		{
			return;
		}

		/// <summary>
		/// Returns false
		/// </summary>
		public bool SupportHierarchicSearch
		{
			get { return false; }
		}

		#endregion

	}
}
