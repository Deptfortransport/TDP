// *********************************************** 
// NAME			: ParkAndRideCatalgue.cs
// AUTHOR		: Neil Moorhouse
// DATE CREATED	: 22/07/2005
// DESCRIPTION	: Park And Ride lookup and caching classes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ParkAndRideCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:16   mturner
//Initial revision.
//
//   Rev 1.4   Apr 07 2006 17:38:02   rhopkins
//Corrected handling of URL for Car Park
//
//   Rev 1.3   Mar 23 2006 17:58:32   build
//Automatically merged from branch for stream0025
//
//   Rev 1.2.1.0   Mar 08 2006 14:30:06   tolomolaiye
//Changes for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Sep 02 2005 15:11:12   NMoorhouse
//Updated following review comments (CR003)
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 12 2005 11:13:10   NMoorhouse
//DN058 Park And Ride, end of CUT
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 10:22:20   NMoorhouse
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
	/// Summary description for ParkAndRideCatalogue.
	/// </summary>
	public class ParkAndRideCatalogue : IParkAndRideCatalogue
	{
		#region Private members

		// Provides access to external links service
		private IExternalLinks externalLinks;

		/// <summary>
		/// Stored proc that will be used to load the park and rides
		/// </summary>
		private const string storedProcName = "GetParkAndRideData";

		/// <summary>
		/// Hashtable holding Park And Rides by Region. Keys are values of type int (region id).
		/// </summary>
		private Hashtable informationByRegion;

		private ArrayList allInformation;
			
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sourceDB">The database containing the ParkAndRide table/stored proc</param>
		public ParkAndRideCatalogue(SqlHelperDatabase sourceDB)
		{
			externalLinks = (IExternalLinks) TDServiceDiscovery.Current[ServiceDiscoveryKey.ExternalLinkService];

			LoadData(sourceDB);
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Method to get all the Park And Rides
		/// </summary>
		/// <returns>Returns List of all Park And Rides</returns>
		public ParkAndRideInfo[] GetAll()
		{
			return (ParkAndRideInfo[])allInformation.ToArray(typeof(ParkAndRideInfo));
		}

		/// <summary>
		/// Method to get the Park And Rides for a Specific Region
		/// </summary>
		/// <param name="id">Region id</param>
		/// <returns>Returns List of Park And Rides for given Region</returns>
		public ParkAndRideInfo[] GetRegion(string regionName)
		{
			// Get list of park and rides for the region
			ArrayList regionList = (ArrayList)informationByRegion[regionName];

			if (regionList == null)
			{
				return new ParkAndRideInfo[0];
			}
			else
			{
				return (ParkAndRideInfo[])regionList.ToArray(typeof(ParkAndRideInfo));
			}
		}

		/// <summary>
		/// Gets the ParkAndRideInfo object associated with the given ParkAndRideId
		/// </summary>
		/// <param name="parkAndRideId">The park and ride id</param>
		/// <returns>A Park and Ride Info object</returns>
		public ParkAndRideInfo GetScheme(int parkAndRideId)
		{
			ParkAndRideInfo[] parkAndRides = (ParkAndRideInfo[])allInformation.ToArray(typeof(ParkAndRideInfo));

			foreach (ParkAndRideInfo parkAndRideInfo in parkAndRides)
			{
				if (parkAndRideInfo.ParkAndRideId == parkAndRideId)
				{
					return parkAndRideInfo;
				}
			}

			//return null if there is no match
			return null;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Loads Park and Rides into array holding all data and hashtable
		/// </summary>
		/// <param name="sourceDB">Source Database</param>
		private void LoadData(SqlHelperDatabase sourceDB)
		{
			// Create the helper and initialise objects
			SqlHelper helper = new SqlHelper();
			informationByRegion = new Hashtable();
			allInformation = new ArrayList();

			// Open connection and get a DataReader
			helper.ConnOpen(sourceDB);

			SqlDataReader reader = helper.GetReader(storedProcName, new Hashtable(),  new Hashtable());

			// Vars to hold the data from the datareader
			int parkAndRideid;
			string id = string.Empty;
			string location = string.Empty;
			string comments = string.Empty;
			string urlLink;
			int easting;
			int northing;

			// Assign Column Ordinals 
			int parkAndRideIdOrdinal = reader.GetOrdinal("ParkAndRideId");
			int idOrdinal = reader.GetOrdinal("Id");
			int locationOrdinal = reader.GetOrdinal("Location");
			int commentsOrdinal = reader.GetOrdinal("Comments");
			int urlLinkOrdinal = reader.GetOrdinal("URLKey");
			int eastingOrdinal = reader.GetOrdinal("easting");
			int northingOrdinal = reader.GetOrdinal("northing"); 

			// Loop through the data and create park and ride object for each record
			while (reader.Read())
			{
				id = reader.GetString(idOrdinal);
				location = reader.GetString(locationOrdinal);
				if (reader.IsDBNull(commentsOrdinal))
				{
					comments = null;
				}
				else
				{
					comments = reader.GetString(commentsOrdinal);
				}
				if (reader.IsDBNull(urlLinkOrdinal))
				{
					urlLink = null;
				}
				else
				{
					//A Url key exists so get the Url details form External Links
					ExternalLinkDetail urlDetails = externalLinks[reader.GetString(urlLinkOrdinal)];
					if (urlDetails.IsValid)
					{
						//A valid Url link exists
						urlLink = urlDetails.Url;
					}
					else
					{
						urlLink = null;
					}
				}
				
				// get the easting and norting ordinals
				easting = reader.GetInt32(eastingOrdinal);
				northing = reader.GetInt32(northingOrdinal);
				parkAndRideid = reader.GetInt32(parkAndRideIdOrdinal);
				
				//Create new Park and Ride object
				ParkAndRideInfo newParkAndRide = new ParkAndRideInfo(id, location, comments, urlLink, 
					easting, northing, parkAndRideid, GetCarParkInfo(parkAndRideid, sourceDB));
				
				// Get list of park and rides for the region
				ArrayList regionList = (ArrayList)informationByRegion[id];

				// If this is the first occurrence of this region, create a new
				// list to hold the park and rides
				if (regionList == null) 
				{
					regionList = new ArrayList();
					informationByRegion.Add(id,regionList);
				}

				// Add Park And Ride object to the lists
				regionList.Add(newParkAndRide);
				allInformation.Add(newParkAndRide);
			}

			reader.Close();
			helper.ConnClose();
		}

		/// <summary>
		/// Retrieves a collection of CarParkInfo based on the supplied Park and Ride Key (id)
		/// </summary>
		/// <param name="parkAndRideKey">The park and Ride ID</param>
		/// <param name="sourceParkAndRideDB">The database source</param>
		/// <returns>A collection of car park info</returns>
		private CarParkInfo[] GetCarParkInfo(int parkAndRideKey, SqlHelperDatabase sourceParkAndRideDB)
		{
			const string storedProcName = "GetParkAndRideCarParks";

			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();
			ArrayList parkAndRideList = new ArrayList();

			int carParkId;
			string carParkName = string.Empty;
			int minimumCost;
			string comments = string.Empty;
			string urlLink;
			int easting;
			int northing;

			sqlParam.Add("@ParkAndRideKey", parkAndRideKey);

			// Open connection and get a DataReader
			helper.ConnOpen(sourceParkAndRideDB);

			SqlDataReader reader = helper.GetReader(storedProcName, sqlParam);
			
			int carParkIdOrdinal = reader.GetOrdinal("CarParkId");
			int carParkNameOrdinal = reader.GetOrdinal("CarParkName"); 
			int urlLinkOrdinal = reader.GetOrdinal("ExternalLinksId"); 
			int minimumCostOrdinal = reader.GetOrdinal("MinimumCost");
			int commentsOrdinal = reader.GetOrdinal("Comments");
			int eastingOrdinal = reader.GetOrdinal("Easting");
			int northingOrdinal = reader.GetOrdinal("Northing");
	
			//add the items from the reader to the collection
			while (reader.Read())
			{
				carParkId = reader.GetInt32(carParkIdOrdinal);
				carParkName = reader.GetString(carParkNameOrdinal);
				if (reader.IsDBNull(urlLinkOrdinal))
				{
					urlLink = null;
				}
				else
				{
					//A Url key exists so get the Url details form External Links
					ExternalLinkDetail urlDetails = externalLinks[reader.GetString(urlLinkOrdinal)];
					if (urlDetails.IsValid)
					{
						//A valid Url link exists
						urlLink = urlDetails.Url;
					}
					else
					{
						urlLink = null;
					}
				}


				if (reader.IsDBNull(minimumCostOrdinal))
				{
					minimumCost = 0;
				}
				else
				{
					minimumCost = reader.GetInt32(minimumCostOrdinal);
				}

				if (reader.IsDBNull(commentsOrdinal))
				{
					comments = null;
				}
				else
				{
					comments = reader.GetString(commentsOrdinal);
				}

				easting = reader.GetInt32(eastingOrdinal);
				northing = reader.GetInt32(northingOrdinal);

				parkAndRideList.Add(new CarParkInfo(carParkId, carParkName, urlLink, minimumCost, 
					comments, easting, northing));
			}
			
			//close the reader and connection once we have finished
			reader.Close();
			helper.ConnClose();

			//return the collection as an array of CarParkInfo
			return (CarParkInfo[])parkAndRideList.ToArray(typeof(CarParkInfo));
		}
		#endregion
	}
}