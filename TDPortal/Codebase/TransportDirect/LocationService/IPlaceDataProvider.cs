// *********************************************** 
// NAME                 : IPlaceDataProvider
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/10/2004
// DESCRIPTION  : Interface for PlaceDataProvider classes.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/IPlaceDataProvider.cs-arc  $ 
//
//   Rev 1.2   Mar 10 2008 15:18:50   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory   Feb 13 2008 14:00:00   mmodi
//Updated to include method to populate toids
//
//   Rev 1.0   Nov 08 2007 12:25:08   mturner
//Initial revision.
//
//   Rev 1.2   Nov 15 2005 18:28:44   RPhilpott
//Add new GetNaptan() method to support getting Naptan details from teh City-to-City database.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.1   Mar 23 2005 12:11:58   jgeorge
//Updated commenting
//
//   Rev 1.0   Nov 01 2004 15:46:08   jgeorge
//Initial revision.

using System;
using System.Collections;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Interface for PlaceDataProvider classes.
	/// </summary>
	public interface IPlaceDataProvider
	{
		/// <summary>
		/// Returns all places for the specified place type in a LocationChoiceList object
		/// </summary>
		LocationChoiceList GetPlaces(PlaceType placeType);
		
		/// <summary>
		/// Returns the place with the given TDNPGID in a TDLocation object
		/// </summary>
		TDLocation GetPlace(string TDNPGID);

		/// <summary>
		/// Returns the NaPTAN with the given naptan string in a TDNaptan object
		/// </summary>
		TDNaptan GetNaptan(string naptan);

	}

	/// <summary>
	/// Interface for strategy class to load data for PlaceDataProvider
	/// </summary>
	public interface IPlaceDataLoadingStrategy
	{
		/// <summary>
		/// Loads data for use by a PlaceDataProvider
		/// </summary>
		/// <param name="placeLists">A Hashtable that will be loaded with multiple LocationChoiceList objects using PlaceType values as keys</param>
		/// <param name="locations">A Hashtable that will be loaded with multiple TDLocation objects using their TDNPGIDs as keys</param>
		/// <param name="naptans">A Hashtable that will be loaded with multiple TDNaptan objects using naptan strings as keys</param>
		void LoadPlaces(out Hashtable placeLists, out Hashtable locations, out Hashtable naptans);

        /// <summary>
        /// Populates the location with TOIDs, returns the location
        /// </summary>
        /// <param name="location">The location for which TOIDs are to be added to</param>
        /// <param name="tdnpgid">The important place id used to retrieve location from database</param>
        void LoadTOIDForPlace(ref TDLocation location, string tdnpgid);
	}
}
