// *********************************************** 
// NAME                 : LSKeys.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 7/07/2004 
// DESCRIPTION  : Keys used in the project
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LSKeys.cs-arc  $ 
//
//   Rev 1.2   Aug 28 2012 10:19:54   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.1   May 07 2008 11:30:32   RBRODDLE
//CCN0451 Main Coach Given Name filtering
//Resolution for 4915: CCN0451 - Control use of "GivenName" tag in cjp requests
//
//   Rev 1.0   Nov 08 2007 12:25:12   mturner
//Initial revision.
//
//   Rev 1.1   Aug 10 2004 18:59:14   RPhilpott
//Add AirportVia naptan prefix property string 
//
//   Rev 1.0   Jul 09 2004 13:06:26   passuied
//Initial Revision


using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for Keys.
	/// </summary>
	[Serializable]
	public class LSKeys
	{
		private LSKeys()
		{
		}

		public const string NaptanPrefixProperties = "FindA.NaptanPrefix.{0}";
		public const string MajorStationsGazetteerIdProperties = "locationservice.majorstationsgazetteerid.{0}";
		public const string StopTypeProperties = "locationservice.StopType.{0}";
		public const string NaptanPrefixAirportVia = "FindA.NaptanPrefix.AirportVia";
        public const string FilterGivenNameTag = "LocationService.FilterGivenNameTag";
        public const string GivenNameTagFilterValues = "LocationService.GivenNameTagFilterValues";

        // Location cache properties
        public const string Cache_LoadLocations = "LocationService.Cache.LoadLocations";

        public const string Data_LocationsVersionStoredProcedure = "LocationService.Cache.LocationsVersionStoredProcedure";

        public const string Data_LocationsDatabase = "LocationService.Cache.LocationsDatabase";
        public const string Data_LocationsStoredProcedure = "LocationService.Cache.LocationsStoredProcedure";
        public const string Data_LocationStoredProcedure = "LocationService.Cache.LocationStoredProcedure";
        public const string Data_LocationUnknownStoredProcedure = "LocationService.Cache.LocationUnknownStoredProcedure";
        public const string Data_LocationsAlternateStoredProcedure = "LocationService.Cache.LocationsAlternateStoredProcedure";

        public const string Search_Cache_Locations = "LocationService.Cache.SearchLocations.InCache";
        public const string Max_SearchLocationsLimit = "LocationService.Cache.SearchLocationsLimit.Count.Max";
        public const string Max_SearchLocationsShow = "LocationService.Cache.SearchLocationsShow.Count.Max";
        public const string Max_SearchLocationsShowLimit_GroupStations = "LocationService.Cache.SearchLocationsShow.GroupStationsLimit.Count";
        public const string Max_SearchLocationsShowLimit_RailStations = "LocationService.Cache.SearchLocationsShow.RailStationsLimit.Count";
        public const string Max_SearchLocationsShowLimit_POIs = "LocationService.Cache.SearchLocationsShow.POILimit.Count";
        public const string Max_SearchLocationsShowLimit_CoachStations = "LocationService.Cache.SearchLocationsShow.CoachStationsLimit.Count";
        public const string Max_SearchLocationsShowLimit_TramStations = "LocationService.Cache.SearchLocationsShow.TramStationsLimit.Count";
        public const string Max_SearchLocationsShowLimit_FerryStations = "LocationService.Cache.SearchLocationsShow.FerryStationsLimit.Count";
        public const string Max_SearchLocationsShowLimit_AirportStations = "LocationService.Cache.SearchLocationsShow.AirportStationsLimit.Count";

        public const string CommonWords = "LocationService.Cache.CommonWords";
        public const string SimilarityIndex_NoCommonWords = "LocationService.Cache.SimilarityIndex.NoCommonWords.Min";
        public const string SimilarityIndex_NoCommonWordsAndSpace = "LocationService.Cache.SimilarityIndex.NoCommonWordsAndSpace.Min";
        public const string SimilarityIndex_IndividualWords = "LocationService.Cache.SimilarityIndex.IndividualWords.Min";
        public const string SimilarityIndex_ChildLocalityAtEnd = "LocationService.Cache.SimilarityIndex.ChildLocalityAtEnd";
	}
}
