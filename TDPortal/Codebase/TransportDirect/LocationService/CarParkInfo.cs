// *********************************************** 
// NAME			: CarParkInfo.cs
// AUTHOR		: Tolu Olomolaiye
// DATE CREATED	: 3 March 2006
// DESCRIPTION	: Class that holds data about a car park
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkInfo.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:02   mturner
//Initial revision.
//
//   Rev 1.8   Apr 25 2006 16:03:56   mtillett
//Move lookup of TOIDs from initialisation of car park info class.
//Resolution for 3989: Slow initialisation of Portal
//
//   Rev 1.7   Apr 20 2006 13:54:44   kjosling
//Updated commentary
//Resolution for 44: DEL 8.1 Workstream - Park and Ride Amendments
//
//   Rev 1.6   Mar 27 2006 16:37:36   tmollart
//Added public default constructor so class can be serialised.
//
//   Rev 1.5   Mar 20 2006 19:35:20   halkatib
//added code to prefix the toids 
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4   Mar 20 2006 15:29:30   halkatib
//Added OS Gripd property to the class
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.3   Mar 20 2006 13:46:04   tolomolaiye
//Updated with code review comments
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Mar 10 2006 16:41:04   tolomolaiye
//Further updates for Park and Ride II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.1   Mar 08 2006 14:27:26   tolomolaiye
//Work in progress
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.0   Mar 03 2006 16:59:40   tolomolaiye
//Initial revision.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2

using System;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Display information about car parks.
	/// </summary>
	[Serializable()]
	public class CarParkInfo
	{
		private readonly int carParkId;
		private readonly string carParkName;
		private readonly string externalLinksId;
		private readonly int minimumCost;
		private readonly string comments;
		private readonly int easting;
		private readonly int northing;
		private string[] toids;

		static private string TOID_PREFIX = "JourneyControl.ToidPrefix";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CarParkInfo()
		{
		}

		/// <summary>
		/// CarParkInfo Constructor
		/// Initialise all properties from the database
		/// </summary>
		/// <param name="CarParkId">The car park id</param>
		/// <param name="CarParkName">Name of the car park</param>
		/// <param name="ExternalLinksId">URL Key</param>
		/// <param name="MinimumCost">Minimum cost of car park (in pence)</param>
		/// <param name="Comments">Additional comments</param>
		/// <param name="Easting">OS grid reference easting for car park entrance</param>
		/// <param name="Northing">OS grid reference northing for car park entrance</param>
		public CarParkInfo(int carParkId, string carParkName, string externalLinksId, 
			int minimumCost, string comments, int easting, int northing)
		{
			this.carParkId = carParkId;
			this.carParkName = carParkName;
			this.externalLinksId = externalLinksId;
			this.minimumCost = minimumCost;
			this.comments = comments;
			this.easting = easting;
			this.northing = northing;		
		}
		/// <summary>
		/// Look up TOIDs by car park grid reference
		/// </summary>
		private void LookUpCarParkTOIDs()
		{
			string toidPrefix = Properties.Current[TOID_PREFIX];

			if	(toidPrefix == null) 
			{
				toidPrefix = string.Empty;
			}

			//populate the internal TOIDs array by calling a methos from GisQuery
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			QuerySchema gisResult = gisQuery.FindNearestITNs(easting, northing);

			toids = new string[gisResult.ITN.Rows.Count];

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
		}

		/// <summary>
		/// Returns a clone of the array of toids
		/// </summary>
		public string[] GetToids()
		{
			if (toids == null)
			{
				//look up TOIDs first time required
				LookUpCarParkTOIDs();
			}
			return (string[])toids.Clone();
		}

		/// <summary>
		/// Read only property. Get the car park name
		/// </summary>
		public string CarParkName
		{
			get {return carParkName;}
		}

		/// <summary>
		/// Read only property. Get the URL link
		/// </summary>
		public string UrlLink
		{
			get 
			{	
				if (externalLinksId == string.Empty)
				{
					return null;
				}
				else
				{
					return externalLinksId;
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
		/// Read only property - gets any associated comments
		/// </summary>
		public string Comments
		{
			get { return comments; }
		}

		/// <summary>
		/// Readonly property - return the grid reference for the scheme
		/// </summary>
		public OSGridReference GridReference
		{
			get
			{
				// This must be created new each time,
				// to preserve the Immutability of CarParkInfo
				return new OSGridReference(easting, northing);
			}
		}
	}
}