// *********************************************** 
// NAME			: CarParkCatalgue.cs
// AUTHOR		: Esther Severn
// DATE CREATED	: 03/08/2006
// DESCRIPTION	: Car Park lookup and caching class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkCatalogue.cs-arc  $
//
//   Rev 1.5   Apr 15 2008 11:17:26   mmodi
//Removed unused variable
//
//   Rev 1.4   Mar 20 2008 10:14:32   mturner
//Del10 patch1 from Dev factory
//
// Rev DevFactory Mar 10 2008 18:00:00 mmodi
// Corrected advanced reservations check
//
// Rev DevFactory Feb 06 2008 22:23:33 apatel
// CCN 0426 modified GetCarParkAdditionalData and GetCarParkingOperator and LoadCarParks methods to confirm to new CarParks db.
//
//   Rev 1.0   Nov 08 2007 12:25:00   mturner
//Initial revision.
//
//   Rev 1.10   Sep 22 2006 14:08:26   esevern
//Change following code review comments - added class summary description
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.9   Sep 18 2006 17:00:10   tmollart
//Modified code to make thread safe.
//Removed commented out code that I left in last time I checked the file in.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.8   Sep 14 2006 11:45:16   tmollart
//Modified to allow a greater number of stay types.
//Resolution for 4189: Car Parking: StayType and Easting/Northing amendments
//
//   Rev 1.7   Sep 08 2006 14:54:52   esevern
//Amended LoadData() - now only loads data for the specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Aug 30 2006 15:49:36   esevern
//uncommented out the access points loadData. Added check for easting and northing of an access point being an empty string (as well as null)
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Aug 30 2006 10:39:16   esevern
//changes to put catalogue in line with new access point database rules
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 29 2006 14:24:22   esevern
//Additional car park access points added. Now creates an array of access points for each car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 29 2006 10:25:08   esevern
//Interim check-in for developer build.  Corrected ordinal id's according to new values in database for car parking
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 24 2006 12:02:24   esevern
//Removed TDLocation from LoadData call
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 15 2006 15:49:56   esevern
//Interim check in for developer integration
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 15 2006 14:04:34   esevern
//Initial revision.
//

using System;
using System.Collections;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.ExternalLinkService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Caching class responsible for the loading from the database of a 
	/// specific car park's information, based on its reference number
	/// </summary>
	public class CarParkCatalogue : ICarParkCatalogue
	{
		#region Private members

		private IExternalLinks externalLinks; // Provides access to external links service
		private const string SP_ALL = "GetCarParkingData"; // Stored proc that will be used to load car parks
        private const string SP_ADDITIONAL = "GetCarParkAdditionalData";
        private const string SP_OPERATOR = "GetCarParkOperatorData";
		private const string SP_NPTG = "GetCarParkingAdminData";
		private const string SP_SCHEME = "GetCarParkingParkAndRideData";
		private const string SP_ACCESS = "GetCarParkAccessPointData";
		private const string SP_REGION = "GetCarParkingRegionData";

		private TransportDirect.Common.DatabaseInfrastructure.SqlHelperDatabase sourceDB;
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sourceDB">The database containing the CarPark table/stored proc</param>
		public CarParkCatalogue(SqlHelperDatabase sourceDB)
		{
			externalLinks = (IExternalLinks) TDServiceDiscovery.Current[ServiceDiscoveryKey.ExternalLinkService];
			
			this.sourceDB = sourceDB;
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Obtains the CarParkInfo object associated with the given CarParkId
		/// </summary>
		/// <param name="carParkId">The car park id</param>
		/// <returns>A car park info object</returns>
		public CarPark GetCarPark(string carParkRef)
		{
			return LoadCarPark(sourceDB, carParkRef);
		}

		#endregion

		#region Private methods


		/// <summary>
		/// Loads Car Parks into array holding all data.
		/// </summary>
		/// <param name="sourceDB">Source Database</param>
		/// <param name="carParkRef">Car park reference</param>
		private CarPark LoadCarPark(SqlHelperDatabase sourceDB, string carParkRef)
		{

			// Create a null car park object. This will be returned if we 
			// fail to load a carpark.
			CarPark carPark = null;

			// Create the helper and initialise objects
			SqlHelper helper = new SqlHelper();
			Hashtable sqlParam = new Hashtable();
            
			sqlParam.Add("@CarParkKey", carParkRef);
            // Open connection and get a DataReader
            helper.ConnOpen(sourceDB);
            SqlDataReader reader = helper.GetReader(SP_ALL, sqlParam);
            

			// Vars to hold the data from the datareader
			string carParkReference = string.Empty;
			string name = string.Empty;
			string location = string.Empty; 
			string address = string.Empty; 
			string postcode = string.Empty;
			string notes = string.Empty; 
			string telephone = string.Empty; 
			string carParkURL = string.Empty; 
			int minimumCost;
			string parkAndRideIndicator = string.Empty; // will be converted to bool in CarPark constructor
			string stayType; // convert to CarParkStayType
			string planningPoint = string.Empty; 
			DateTime dateRecordLastUpdated; //DateTime
			DateTime wefDate; //DateTime
			DateTime weuDate; //DateTime
			int trafficNewsRegionId;

			int accessPointMapId; 
			int accessPointEntranceId;
			int accessPointExitId;
			int [] carParkAccessPoints = new int[3];// array of CarParkAccessPoint ids

			string carParkOperatorId; //CarParkOperator
			int parkAndRideSchemeId; //ParkAndRideScheme
			int nptgAdminDistrictId; //array of NPTGAdminDistricts

			// Assign Column Ordinals 
			int carParkRefOrdinal = reader.GetOrdinal("Reference");
			int carParkNameOrdinal = reader.GetOrdinal("Name");
			int locationOrdinal = reader.GetOrdinal("Location");
			int addressOrdinal = reader.GetOrdinal("Address");
			int postcodeOrdinal = reader.GetOrdinal("Postcode");
			int notesOrdinal = reader.GetOrdinal("Notes");
			int telephoneOrdinal = reader.GetOrdinal("Telephone");
			int urlLinkOrdinal = reader.GetOrdinal("Url");
			int minimumCostOrdinal = reader.GetOrdinal("MinCost");
            
			int parkAndRideIndicatorOrdinal = reader.GetOrdinal("ParkAndRide");
            
			int stayTypeOrdinal = reader.GetOrdinal("StayType");
			int planningPointOrdinal = reader.GetOrdinal("PlanningPoint");
			int dateRecordLastUpdatedOrdinal = reader.GetOrdinal("DateRecordLastUpdated");
			int wefDateOrdinal = reader.GetOrdinal("WEFDate");
			int weuDateOrdinal = reader.GetOrdinal("WEUDate");
			int newsRegionOrdinal = reader.GetOrdinal("TrafficNewsRegionId");

			int accessPointMapOrdinal = reader.GetOrdinal("AccessPointsMapId");
			int accessPointEntranceOrdinal = reader.GetOrdinal("AccessPointsEntranceId");
			int accessPointExitOrdinal = reader.GetOrdinal("AccessPointsExitId");
			int operatorOrdinal = reader.GetOrdinal("OperatorId");
			int parkAndRideSchemeOrdinal = reader.GetOrdinal("ParkAndRideSchemeId");
			int nptgDistrictOrdinal = reader.GetOrdinal("NPTGAdminDistrictId");
            //int carParkAdditionalDataIdOrdinal = reader.GetOrdinal("AdditionalDataId");
			
			// Loop through the data and create car park object for each record
            try
            {
                while (reader.Read())
                {
                    carParkReference = reader.GetString(carParkRefOrdinal);
                    name = reader.GetString(carParkNameOrdinal);
                    location = reader.GetString(locationOrdinal);
                    address = reader.GetString(addressOrdinal);
                    postcode = reader.GetString(postcodeOrdinal);
                    notes = reader.GetString(notesOrdinal);
                    telephone = reader.GetString(telephoneOrdinal);
                    carParkURL = reader.GetString(urlLinkOrdinal);
                    minimumCost = reader.GetInt32(minimumCostOrdinal);
                    parkAndRideIndicator = reader.GetBoolean(parkAndRideIndicatorOrdinal).ToString();
                    stayType = reader.GetString(stayTypeOrdinal);
                    planningPoint = reader.GetBoolean(planningPointOrdinal).ToString();
                    dateRecordLastUpdated = reader.GetDateTime(dateRecordLastUpdatedOrdinal);
                    wefDate = reader.GetDateTime(wefDateOrdinal);
                    weuDate = reader.GetDateTime(weuDateOrdinal);
                    trafficNewsRegionId = reader.GetInt32(newsRegionOrdinal);

                    // car park map access point
                    if (reader.IsDBNull(accessPointMapOrdinal))
                    {
                        accessPointMapId = -1;
                    }
                    else
                    {
                        accessPointMapId = reader.GetInt32(accessPointMapOrdinal);
                    }
                    carParkAccessPoints[0] = accessPointMapId;

                    // car park entrance
                    if (reader.IsDBNull(accessPointEntranceOrdinal))
                    {
                        accessPointEntranceId = -1;
                    }
                    else
                    {
                        accessPointEntranceId = reader.GetInt32(accessPointEntranceOrdinal);
                    }
                    carParkAccessPoints[1] = accessPointEntranceId;

                    // car park exit
                    if (reader.IsDBNull(accessPointExitOrdinal))
                    {
                        accessPointExitId = -1;
                    }
                    else
                    {
                        accessPointExitId = reader.GetInt32(accessPointExitOrdinal);
                    }
                    carParkAccessPoints[2] = accessPointExitId;

                    carParkOperatorId = reader.GetString(operatorOrdinal);

                    if (reader.IsDBNull(parkAndRideSchemeOrdinal))
                    {
                        parkAndRideSchemeId = 0;
                    }
                    else
                    {
                        parkAndRideSchemeId = reader.GetInt32(parkAndRideSchemeOrdinal);
                    }

                    if (reader.IsDBNull(nptgDistrictOrdinal))
                    {
                        nptgAdminDistrictId = 0;
                    }
                    else
                    {
                        nptgAdminDistrictId = reader.GetInt32(nptgDistrictOrdinal);
                    }

                    carPark = new CarPark(carParkReference, name, location,
                        address, postcode, notes, telephone, carParkURL, minimumCost,
                        parkAndRideIndicator, GetStayType(stayType), planningPoint,
                        dateRecordLastUpdated, wefDate, weuDate,
                        GetTrafficNewsRegion(trafficNewsRegionId, sourceDB),
                        GetAccessPoints(carParkAccessPoints, sourceDB),
                        GetCarParkOperator(carParkOperatorId, sourceDB),
                        GetParkAndRideScheme(parkAndRideSchemeId, sourceDB),
                        GetNPTGAdminDistrict(nptgAdminDistrictId, sourceDB),
                        GetCarParkAdditionalData(carParkReference, sourceDB)
                        );
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

			reader.Close();
			helper.ConnClose();

			return carPark;
		}
        /// <summary>
        /// Get the Additional Data for the Car Park
        /// </summary>
        /// <param name="carParkingID">Car Park Reference</param>
        /// <param name="sourceDB">Database to use </param>
        /// <returns></returns>
        private CarParkingAdditionalData GetCarParkAdditionalData(string carParkingID, SqlHelperDatabase sourceDB)
        {
            //  Create a new additional data object and pass back
            CarParkingAdditionalData cpAdditionalData = null;
            Hashtable sqlParam = new Hashtable();
            SqlHelper helper = new SqlHelper();
            // Change openingTime and closingTime of type string instead of integer
            string openingTime;
            string closingTime;
            int totalSpaces;
            int totalDisabledSpaces;
            int maxWidth;
            int maxHeight;
            bool isAdvancedReservations = false ;
            bool isSecure;
            string carParkTypeDescription;
            
            // CCN 0426 using CarParking Reference instead of additionalDataId as there is no additionaldataid now.
            sqlParam.Add("@CarParkingId", carParkingID);

            // Open connection and get a DataReader
            helper.ConnOpen(sourceDB);

            SqlDataReader reader = helper.GetReader(SP_ADDITIONAL, sqlParam);

            int openingTimeOrdinal = reader.GetOrdinal("OpensAt");
            int closingTimeOrdinal = reader.GetOrdinal("ClosesAt");
            int totalSpacesOrdinal = reader.GetOrdinal("NumberOfSpaces");
            int totalDisabledSpacesOrdinal = reader.GetOrdinal("totalDisabledSpaces");
            int maxWidthOrdinal = reader.GetOrdinal("MaximumWidth");
            int maxHeightOrdinal = reader.GetOrdinal("MaximumHeight");
            int isPMSPAOrdinal = reader.GetOrdinal("PMSPA");
            int isAdvancedReservationsOrdinal = reader.GetOrdinal("AdvancedReservationsAvailable");
            int carParkTypeDescriptionOrdinal = reader.GetOrdinal("Description"); 

            if(reader.Read())
            {
                openingTime = reader.GetString(openingTimeOrdinal);
                closingTime = reader.GetString(closingTimeOrdinal);
                totalSpaces = reader.GetInt32(totalSpacesOrdinal);
                
                totalDisabledSpaces = reader.GetInt32(totalDisabledSpacesOrdinal);
                maxWidth = reader.GetInt32(maxWidthOrdinal);
                maxHeight = reader.GetInt32(maxHeightOrdinal);
                if (reader.GetString(isAdvancedReservationsOrdinal).ToLower() == "yes")
                {
                     isAdvancedReservations = true;
                }
                isSecure = (reader.GetString(isPMSPAOrdinal).ToLower() != "no");
                carParkTypeDescription = reader.GetString(carParkTypeDescriptionOrdinal);

                cpAdditionalData = new CarParkingAdditionalData(totalSpaces, totalDisabledSpaces, isSecure, openingTime, closingTime, maxHeight, maxWidth, isAdvancedReservations, carParkTypeDescription);
            }

            //close the reader and connection once we have finished
            reader.Close();
            helper.ConnClose();

            return cpAdditionalData;
        }


		/// <summary>
		/// Returns the CarParkOperator based on the car park 
		/// operator id supplied from the car park record
		/// </summary>
		/// <param name="operatorID">Operator  code</param>
		/// <param name="sourceDB">The database source</param>
		/// <returns>CarParkOperator</returns>
        // CCN 0426 removed the id column from the database so operatorId will be operator code 
		private CarParkOperator GetCarParkOperator(string operatorID, SqlHelperDatabase sourceDB) 
		{
			//	create new operator and pass back.
			CarParkOperator cpOperator = null;
			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();

			//int operatorId;
			string operatorCode = string.Empty;
			string operatorName = string.Empty;
			string urlLink = string.Empty;
			string tsAndCs = string.Empty;
			string email = string.Empty;

			sqlParam.Add("@OperatorKey", operatorID);

			// Open connection and get a DataReader
			helper.ConnOpen(sourceDB);

			SqlDataReader reader = helper.GetReader(SP_OPERATOR, sqlParam);
			
			//int idOrdinal = reader.GetOrdinal("Id");
			int codeOrdinal = reader.GetOrdinal("OperatorCode"); 
			int nameOrdinal = reader.GetOrdinal("OperatorName"); 
			int urlOrdinal = reader.GetOrdinal("OperatorURL"); 
			int tsAndCsOrdinal = reader.GetOrdinal("OperatorTsAndCs");
			int emailOrdinal = reader.GetOrdinal("OperatorEmail");
	
			//add the items from the reader to the collection
			while (reader.Read())
			{
				//operatorId = reader.GetInt32(idOrdinal);
				operatorCode = reader.GetString(codeOrdinal);
				operatorName = reader.GetString(nameOrdinal);
				
				if (reader.IsDBNull(urlOrdinal))
				{
					urlLink = null;
				}
				else
				{
					urlLink = reader.GetString(urlOrdinal);
				}


				if (reader.IsDBNull(tsAndCsOrdinal))
				{
					tsAndCs = null;
				}
				else
				{
					tsAndCs = reader.GetString(tsAndCsOrdinal);
				}

				if (reader.IsDBNull(emailOrdinal))
				{
					email = null;
				}
				else
				{
					email = reader.GetString(emailOrdinal);
				}

				cpOperator = new CarParkOperator(operatorCode, operatorName, urlLink, tsAndCs, email);
			}
		
			//close the reader and connection once we have finished
			reader.Close();
			helper.ConnClose();

			return cpOperator;
		}

		/// <summary>
		/// Returns the ParkAndRideScheme based on the scheme 
		/// id supplied from the car park record.
		/// </summary>
		/// <param name="parkAndRideSchemeId">Park and ride scheme id</param>
		/// <param name="sourceDB">The database source</param>
		/// <returns>ParkAndRideScheme</returns>
		private CarParkingParkAndRideScheme GetParkAndRideScheme(int parkAndRideSchemeID, SqlHelperDatabase sourceDB)
		{
			//		create new scheme and pass back.
			CarParkingParkAndRideScheme scheme = null;
			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();

			int schemeId;
			string location = string.Empty;
			string schemeURL = string.Empty;
			string comments = string.Empty;
			string locationEasting = string.Empty;
			string locationNorthing = string.Empty;
			string transferFrequency = string.Empty;
			string transferFrom = string.Empty;
			string transferTo = string.Empty;

			sqlParam.Add("@SchemeKey", parkAndRideSchemeID);

			// Open connection and get a DataReader
			helper.ConnOpen(sourceDB);

			SqlDataReader reader = helper.GetReader(SP_SCHEME, sqlParam);
			
			int idOrdinal = reader.GetOrdinal("Id");
			int locationOrdinal = reader.GetOrdinal("Location"); 
			int schemeURLOrdinal = reader.GetOrdinal("SchemeURL"); 
			int commentsOrdinal = reader.GetOrdinal("Comments"); 
			int eastingOrdinal = reader.GetOrdinal("LocationEasting");
			int northingOrdinal = reader.GetOrdinal("LocationNorthing");
			int frequencyOrdinal = reader.GetOrdinal("TransferFrequency"); 
			int transferFromOrdinal = reader.GetOrdinal("TransferFrom");
			int transferToOrdinal = reader.GetOrdinal("TransferTo");
	
			//add the items from the reader to the collection
			while (reader.Read())
			{

				schemeId = reader.GetInt32(idOrdinal);
				location = reader.GetString(locationOrdinal);

				if (reader.IsDBNull(schemeURLOrdinal))
				{
					schemeURL = null;
				}
				else 
				{
					schemeURL = reader.GetString(schemeURLOrdinal); 
				}

				if (reader.IsDBNull(commentsOrdinal))
				{
					comments = null;
				}
				else 
				{
					comments = reader.GetString(commentsOrdinal);
				}

				locationEasting = reader.GetString(eastingOrdinal);
				locationNorthing = reader.GetString(northingOrdinal);

				if (reader.IsDBNull(frequencyOrdinal))
				{
					transferFrequency = null;
				}
				else 
				{
					transferFrequency = reader.GetString(frequencyOrdinal); 
				}

				if (reader.IsDBNull(transferFromOrdinal))
				{
					transferFrom = null;
				}
				else 
				{
					transferFrom = reader.GetString(transferFromOrdinal); 
				}

				if (reader.IsDBNull(transferToOrdinal))
				{
					transferTo = null;
				}
				else
				{
					transferTo = reader.GetString(transferToOrdinal); 
				}
				
				scheme = new CarParkingParkAndRideScheme(location, schemeURL, comments, 
					Convert.ToInt32(locationEasting), Convert.ToInt32(locationNorthing), transferFrequency, 
					transferFrom, transferTo);
			}
		
			//close the reader and connection once we have finished
			reader.Close();
			helper.ConnClose();

			return scheme;
		}
		
		/// <summary>
		/// Returns an array of NPTGAdminDistricts based on the district 
		/// id supplied from the car park record.
		/// </summary>
		/// <param name="nptgAdminDistrictId">NPTGAdminDistrict id</param>
		/// <param name="sourceDB">The database source</param>
		/// <returns>Array of NPTGAdminDistricts</returns>
		private NPTGAdminDistrict[] GetNPTGAdminDistrict(int nptgAdminDistrictID, SqlHelperDatabase sourceDB)
		{
		// create new district obj, add to array list and pass back as array.
			ArrayList districtList = new ArrayList();
			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();

			int districtId;
			string areaCode = string.Empty;
			string districtCode = string.Empty;

			sqlParam.Add("@DistrictKey", nptgAdminDistrictID);

			// Open connection and get a DataReader
			helper.ConnOpen(sourceDB);

			SqlDataReader reader = helper.GetReader(SP_NPTG, sqlParam);
			
			int idOrdinal = reader.GetOrdinal("Id");
			int areaCodeOrdinal = reader.GetOrdinal("AdminAreaCode"); 
			int districtOrdinal = reader.GetOrdinal("DistrictCode"); 
	
			//add the items from the reader to the collection
			while (reader.Read())
			{
				districtId = reader.GetInt32(idOrdinal);
				areaCode = reader.GetString(areaCodeOrdinal);
				districtCode = reader.GetString(districtOrdinal);
		
				districtList.Add( new NPTGAdminDistrict(areaCode, districtCode) );
			}
		
			//close the reader and connection once we have finished
			reader.Close();
			helper.ConnClose();

			//return the collection as an array of NPTGAdminDistricts
			return (NPTGAdminDistrict[])districtList.ToArray(typeof(NPTGAdminDistrict));
		}


		/// <summary>
		/// Returns the access points for a car park, based on the access point  
		/// ids supplied from the car park record.
		/// </summary>
		/// <param name="accessPoints">array of access point ids</param>
		/// <param name="sourceDB">The SqlHelperDatabase source</param>
		/// <returns>Array of CarParkAccessPoints</returns>
		private CarParkAccessPoint[] GetAccessPoints(int[] accessPoints, SqlHelperDatabase sourceDB)
		{
			// create new district obj, add to array list and pass back as array.
			ArrayList accessPointsList = new ArrayList();
			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();

			string geocodeType = string.Empty;
			string easting = string.Empty;
			string northing = string.Empty;
			string streetName = string.Empty;
			string barrier = string.Empty;

			// Open connection and get a DataReader
			helper.ConnOpen(sourceDB);

			// for each of the id's, retrieve the access point record
			for(int i=0; i<accessPoints.Length; i++) 
			{
				sqlParam.Clear();

				if(accessPoints[i] > 0)
				{
					sqlParam.Add("@AccessKey", accessPoints[i]);
					SqlDataReader reader = helper.GetReader(SP_ACCESS, sqlParam);
					
					int geocodeOrdinal = reader.GetOrdinal("GeocodeType");
					int eastingOrdinal = reader.GetOrdinal("Easting");
					int northingOrdinal = reader.GetOrdinal("Northing");
					int streetOrdinal = reader.GetOrdinal("StreetName");
					int barrierOrdinal = reader.GetOrdinal("BarrierInOperation");
			
					//add the items from the reader to the collection
					while (reader.Read())
					{
						geocodeType = reader.GetString(geocodeOrdinal);

						if (reader.IsDBNull(eastingOrdinal) ||
							reader.GetString(eastingOrdinal).Equals(string.Empty))
						{
							easting = "0";
						}
						else
						{
							easting = reader.GetString(eastingOrdinal);
						}

						if (reader.IsDBNull(northingOrdinal) ||
							reader.GetString(northingOrdinal).Equals(string.Empty))
						{
							northing = "0";
						}
						else
						{
							northing = reader.GetString(northingOrdinal);
						}

						if (reader.IsDBNull(streetOrdinal))
						{
							streetName = string.Empty;
						}
						else
						{
							streetName = reader.GetString(streetOrdinal);
						}

						if (reader.IsDBNull(barrierOrdinal))
						{
							barrier = string.Empty;
						}
						else
						{
							barrier = reader.GetString(barrierOrdinal);
						}
					
						OSGridReference point = new OSGridReference(Convert.ToInt32(easting), 
																	Convert.ToInt32(northing));
				
						accessPointsList.Add( new CarParkAccessPoint(geocodeType,
							point, streetName, barrier) );
					}

					//close the reader and connection once we have finished
					reader.Close();
				}
			}

			helper.ConnClose();

			//return the collection as an array of CarParkAccessPoints
			return (CarParkAccessPoint[])accessPointsList.ToArray(typeof(CarParkAccessPoint));

		}

		/// <summary>
		/// Returns the traffic news region as a string, based on the region 
		/// id supplied from the car park record.
		/// </summary>
		/// <param name="nptgAdminDistrictId">NPTGAdminDistrict id</param>
		/// <param name="sourceDB">The database source</param>
		/// <returns>string</returns>
		private string GetTrafficNewsRegion(int trafficNewsRegionID, SqlHelperDatabase sourceDB)
		{
			// create new district obj, add to array list and pass back as array.
			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();
			string regionName = string.Empty;

			sqlParam.Add("@RegionKey", trafficNewsRegionID);

			// Open connection and get a DataReader
			helper.ConnOpen(sourceDB);
			SqlDataReader reader = helper.GetReader(SP_REGION, sqlParam);
			
			int regionOrdinal = reader.GetOrdinal("RegionName"); 
	
			while (reader.Read())
			{
				if (reader.IsDBNull(regionOrdinal))
				{
					regionName = null;
				}
				else
				{
					regionName = reader.GetString(regionOrdinal);
				}
			}
		
			//close the reader and connection once we have finished
			reader.Close();
			helper.ConnClose();

			//return the region name
			return regionName;
		}

		/// <summary>
		/// Returns the CarParkStayType based on the stay type string
		/// supplied from the car park record.
		/// </summary>
		/// <param name="theStayType">string</param>
		/// <returns>CarParkStayType</returns>
		private CarParkStayType GetStayType(string theStayType)
		{

			CarParkStayType returnType = CarParkStayType.ShortMediumLong;

			try
			{
				returnType = (CarParkStayType)Enum.Parse(typeof(CarParkStayType), theStayType, true);
			}
			catch (ArgumentException)
			{
            }

			return returnType;
	
		}

		#endregion
	}
}
