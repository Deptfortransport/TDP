// *********************************************** 
// NAME                 : PlaceDataProviderFactory
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/10/2004
// DESCRIPTION  : Implementation of IServiceFactory for the PlaceDataProvider
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/PlaceDataProviderFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:20   mturner
//Initial revision.
//
//   Rev 1.0   Nov 01 2004 15:46:10   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Implementation of IServiceFactory for the PlaceDataProvider. Creates the object using
	/// the Database data loader.
	/// </summary>
	public class PlaceDataProviderFactory : IServiceFactory
	{
		private IPlaceDataProvider current;

		/// <summary>
		/// Constructor
		/// </summary>
		public PlaceDataProviderFactory()
		{
			current = new PlaceDataProvider(new DatabasePlaceDataLoader(SqlHelperDatabase.PlacesDB));
		}
		
		/// <summary>
		/// Returns the current PlaceDataProvider object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}
	}
}
