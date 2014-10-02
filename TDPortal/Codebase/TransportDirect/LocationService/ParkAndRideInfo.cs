// *********************************************** 
// NAME			: ParkAndRideInfo.cs
// AUTHOR		: Neil Moorhouse
// DATE CREATED	: 22/07/2005
// DESCRIPTION	: Implementation of the ParkAndRideInfo class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ParkAndRideInfo.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:18   mturner
//Initial revision.
//
//   Rev 1.4   Mar 27 2006 16:37:40   tmollart
//Added public default constructor so class can be serialised.
//
//   Rev 1.3   Mar 23 2006 17:58:32   build
//Automatically merged from branch for stream0025
//
//   Rev 1.2.1.3   Mar 20 2006 16:53:40   halkatib
//Added Comments to show immutability of the osgrid property on the parkandride info object.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2.1.2   Mar 20 2006 13:46:22   tolomolaiye
//Updated with code review comments
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2.1.1   Mar 16 2006 12:35:30   tolomolaiye
//Enusre car park info is returned as a clone
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2.1.0   Mar 08 2006 14:30:06   tolomolaiye
//Changes for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Sep 02 2005 15:11:12   NMoorhouse
//Updated following review comments (CR003)
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 12 2005 11:12:30   NMoorhouse
//DN058 Park And Ride, end of CUT
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 10:22:20   NMoorhouse
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Class representing a ParkAndRide entity
	/// </summary>
	[Serializable]
	public class ParkAndRideInfo
	{
		#region Private members

		private readonly string regionId;
		private readonly string location;
		private readonly string comments;
		private readonly string urlLink;
		private readonly int easting;
		private readonly int northing;
		private readonly int parkAndRideId;
		private readonly CarParkInfo[] carParks;

		#endregion

		#region Constructor

		/// <summary>
		/// Default Public Constructor
		/// </summary>
		public ParkAndRideInfo()
		{
		}


		/// <summary>
		/// Standard constructor. Object is immutable so all property values are supplied
		/// at object creation.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="Location"></param>
		/// <param name="Comments"></param>
		/// <param name="URLKeys"></param>
		internal ParkAndRideInfo(string regionId, string location, string comments, 
			string urlLink, int easting, int northing, int parkAndRideId, CarParkInfo[] carParks)
		{
			this.regionId = regionId;
			this.location = location;
			this.comments = comments;
			this.urlLink = urlLink;
			this.easting = easting;
			this.northing = northing;
			this.parkAndRideId = parkAndRideId;
			this.carParks = carParks;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Region Id element of Park And Ride entity (r)
		/// </summary>
		public string RegionId
		{
			get { return regionId; }
		}

		/// <summary>
		/// Location element of Park And Ride entity (r)
		/// </summary>
		public string Location
		{
			get { return location; }
		}

		/// <summary>
		/// Comments element of Park And Ride entity (r)
		/// </summary>
		public string Comments
		{
			get { return comments; }
		}

		/// <summary>
		/// URL Link element of Park And Ride entity (r)
		/// </summary>
		public string UrlLink
		{
			get { return urlLink; }
		}

		/// <summary>
		/// Readonly property - return the grid reference for the scheme
		/// </summary>
		public OSGridReference SchemeGridReference
		{
			get 
			{ 
				// This must be created new each time,
				// to preserve the Immutability of ParkAndRideInfo
				return new OSGridReference(easting, northing); 
			}
		}

		/// <summary>
		/// Readonly property - return the park and ride id
		/// </summary>
		public int ParkAndRideId
		{
			get { return parkAndRideId; }
		}

		/// <summary>
		/// Returns a clone of the array of CarParkInfo
		/// </summary>
		public CarParkInfo[] GetCarParks()
		{
			return (CarParkInfo[])carParks.Clone();
		}

		#endregion

		/// <summary>
		/// Searches the internal array to find a car park that matches one of the supplied TOIDs
		/// </summary>
		/// <param name="toids">An array of TOIDs</param>
		/// <returns>A valid car park</returns>
		public CarParkInfo MatchCarPark(string[] toids)
		{
			foreach (CarParkInfo carPark in carParks)
			{
				foreach (string toid in toids)
				{
					//check if the toid is in the array 
					if (Array.IndexOf(carPark.GetToids(), toid) != -1)
						return carPark;
				}
			}
			
			//return null if none found
			return null;
		}
	}
}