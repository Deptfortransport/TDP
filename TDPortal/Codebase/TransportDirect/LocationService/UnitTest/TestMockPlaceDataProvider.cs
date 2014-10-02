// *********************************************** 
// NAME                 : TestMockPlaceDataProvider
// AUTHOR               : Murat Guney
// DATE CREATED         : 14/02/2006
// DESCRIPTION			: TestMockPlaceDataProvider class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestMockPlaceDataProvider.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:48   mturner
//Initial revision.
//
//   Rev 1.0   Feb 16 2006 16:24:44   mguney
//Initial revision.

using System;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for TestMockPlaceDataProvider.
	/// </summary>
	public class TestMockPlaceDataProvider: IPlaceDataProvider, IServiceFactory
	{
		
		IPlaceDataProvider provider;
		

		/// <summary>
		/// Constructor. Creates the provider using the data file.
		/// </summary>
		public TestMockPlaceDataProvider()
		{			
			provider = new PlaceDataProvider(new FilePlaceDataLoader(TestPlaceDataProvider.LOCATIONSERVICEPLACEDATAFILE));			
		}
			

		#region IPlaceDataProvider Members

		/// <summary>
		/// Returns all places for the specified place type in a LocationChoiceList object
		/// </summary>
		/// <param name="placeType"></param>
		/// <returns></returns>
		public LocationChoiceList GetPlaces(PlaceType placeType)
		{
			return provider.GetPlaces(placeType);
		}
		
		/// <summary>
		/// Returns the place with the given TDNPGID in a TDLocation object
		/// </summary>
		public TDLocation GetPlace(string TDNPGID)
		{
			return provider.GetPlace(TDNPGID);
		}

		/// <summary>
		/// Returns the NaPTAN with the given naptan string in a TDNaptan object
		/// </summary>
		public TDNaptan GetNaptan(string naptan)
		{
			return provider.GetNaptan(naptan);
		}

		#endregion

		#region IServiceFactory Members

		/// <summary>
		/// Returns the provider.
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return provider;
		}

		#endregion
	}
}
